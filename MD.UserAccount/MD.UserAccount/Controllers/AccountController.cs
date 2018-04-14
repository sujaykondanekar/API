using MD.UserAccount.Helper;
using MD.UserAccount.Models;
using MD.UserAccount.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MD.UserAccount.Controllers
{
    //[Authorize]
    [RoutePrefix("account")]
    public class AccountController : ApiController
    {
        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        // GET api/Account/UserInfo      
        [Route("userInfo")]
        [HttpGet]
        public UserInfo GetUserInfo()
        {
            return new UserInfo
            {
                Email = User.Identity.GetUserName(),
                UserId = User.Identity.GetUserId()
            };
        }

        // POST api/Account/Logout
        [Route("logout")]
        [HttpPost]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        // POST api/Account/ChangePassword
        [Route("password/change")]
        [HttpPost]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
                model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/SetPassword
        [Route("password/set")]
        [HttpPost]
        public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }


        // POST api/Account/RemoveLogin
        [Route("login/remove")]
        [HttpDelete]
        public async Task<IHttpActionResult> RemoveLogin(RemoveLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result;

            if (model.LoginProvider == LocalLoginProvider)
            {
                result = await UserManager.RemovePasswordAsync(User.Identity.GetUserId());
            }
            else
            {
                result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(),
                    new UserLoginInfo(model.LoginProvider, model.ProviderKey));
            }

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("register")]
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

            IdentityResult result = await UserManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                if (result.Errors != null && result.Errors.Where(error => error.Contains("is already taken")).Any())
                {
                    return BadRequest($"Email {model.Email} is already taken");
                }
                return GetErrorResult(result);
            }

            return Created(new Uri($"{Request.RequestUri.GetLeftPart(UriPartial.Authority)}/token"), new { Email = model.Email });
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("external/login")]
        public async Task<IHttpActionResult> LoginUsingExternalProvider(ProviderAndAccessToken model)
        {
            string id = null;
            string userName = null;
            ExternalProvider externalProvider;
            if (!Enum.TryParse<ExternalProvider>(model.Provider, out externalProvider))
            {
                return BadRequest($"Invalid provider : {model.Provider}");
            }

            if (externalProvider == ExternalProvider.facebook)
            {
                try
                {
                    var fbclient = new Facebook.FacebookClient(model.Token);
                    dynamic fb = fbclient.Get("/me?locale=en_US&fields=name,email");
                    id = fb.id;
                    userName = fb.email;
                }
                catch (Exception ex)
                {
                    HttpContent contentPost = new StringContent("Facebook : " + ex.Message, Encoding.UTF8, "application/text");
                    var msg = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                    {

                        Content = contentPost

                    };
                    throw new HttpResponseException(msg);
                }
            }

            //TODO: Google, LinkedIn

            ApplicationUser user = await UserManager.FindAsync(new UserLoginInfo(model.Provider, id));
            bool hasRegistered = user != null;

            string accessToken = null;
            var identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);
            var props = new AuthenticationProperties()
            {
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddDays(Settings.TokenExpirationDurationInDays)
            };

            if (hasRegistered)
            {
                ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(UserManager,
                    OAuthDefaults.AuthenticationType);
                ClaimsIdentity cookieIdentity = await user.GenerateUserIdentityAsync(UserManager,
                    CookieAuthenticationDefaults.AuthenticationType);

                AuthenticationProperties properties = ApplicationOAuthProvider.CreateProperties(user.UserName);
                Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);


                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                identity.AddClaim(new Claim("role", "user"));

            }
            else
            {
                if (await UserManager.FindByEmailAsync(userName) != null || await UserManager.FindByNameAsync(userName) != null)
                {
                    return BadRequest($"External user {userName} is already registered");
                }

                user = new ApplicationUser() { UserName = userName, Email = userName };

                IdentityResult result = await UserManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }

                result = await UserManager.AddLoginAsync(user.Id, new UserLoginInfo(model.Provider, id));
                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }

                identity.AddClaim(new Claim(ClaimTypes.Name, userName));
            }

            identity.AddClaim(new Claim("role", "user"));
            var ticket = new AuthenticationTicket(identity, props);
            accessToken = Startup.OAuthOptions.AccessTokenFormat.Protect(ticket);

            return Ok(accessToken);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }


        #endregion
    }
}
