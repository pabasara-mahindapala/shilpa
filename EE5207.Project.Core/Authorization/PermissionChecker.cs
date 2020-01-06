using Abp.Authorization;
using EE5207.Project.Authorization.Roles;
using EE5207.Project.Authorization.Users;

namespace EE5207.Project.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
