using System.Threading.Tasks;
using Abp.Application.Services;
using EE5207.Project.Sessions.Dto;

namespace EE5207.Project.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
