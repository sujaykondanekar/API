namespace MD.ProfileManagement.DataSource
{
    using MD.ProfileManagement.DataContract;
    using MD.ProfileManagement.DataSource.DataModel;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ProfileManagementDbContext : DbContext
    {
        // Your context has been configured to use a 'ProfileManagementDbContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'ProfileManagement.DataSource.ProfileManagementDbContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ProfileManagementDbContext' 
        // connection string in the application configuration file.
        public ProfileManagementDbContext()
            : base("name=ProfileManagementDbContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<MemberProfile> Profiles { get; set; }

        public virtual DbSet<SlimLabReport> LabReports { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<ProfileManagementDbContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }

   

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}