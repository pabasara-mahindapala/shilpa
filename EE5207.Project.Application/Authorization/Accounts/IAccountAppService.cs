using System.Threading.Tasks;
using Abp.Application.Services;
using EE5207.Project.Authorization.Accounts.Dto;

namespace EE5207.Project.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
