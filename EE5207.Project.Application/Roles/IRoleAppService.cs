using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using EE5207.Project.Roles.Dto;

namespace EE5207.Project.Roles
{
    public interface IRoleAppService : IAsyncCrudAppService<RoleDto, int, PagedResultRequestDto, CreateRoleDto, RoleDto>
    {
        Task<ListResultDto<PermissionDto>> GetAllPermissions();
    }
}
