using Abp.Application.Services;
using Abp.Application.Services.Dto;
using EE5207.Project.MultiTenancy.Dto;

namespace EE5207.Project.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
