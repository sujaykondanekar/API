namespace MD.ProfileManagement.DataContract
{
    public class Profile
    {
        public int? Id { get; set; }

        public string UserId { get; set; }

        public string ProfileName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public double Weight { get; set; }

        public double Height { get; set; }

        public int Age { get; set; }

        public string Report { get; set; }
    }
}