using System.Data.Common;
using System.Data.Entity;
using Abp.Zero.EntityFramework;
using EE5207.Project.Attendances;
using EE5207.Project.Authorization.Roles;
using EE5207.Project.Authorization.Users;
using EE5207.Project.Courses;
using EE5207.Project.MultiTenancy;
using EE5207.Project.Publications;

namespace EE5207.Project.EntityFramework
{
    public class ProjectDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...
        public virtual IDbSet<Publication> Publications { get; set; }
        public virtual IDbSet<Course> Courses { get; set; }
        public virtual IDbSet<Attendance> Attendances { get; set; }

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public ProjectDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in ProjectDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of ProjectDbContext since ABP automatically handles it.
         */
        public ProjectDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public ProjectDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public ProjectDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
