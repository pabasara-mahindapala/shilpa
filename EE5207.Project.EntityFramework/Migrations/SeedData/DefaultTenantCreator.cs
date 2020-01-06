using System.Linq;
using EE5207.Project.EntityFramework;
using EE5207.Project.MultiTenancy;

namespace EE5207.Project.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly ProjectDbContext _context;

        public DefaultTenantCreator(ProjectDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}
