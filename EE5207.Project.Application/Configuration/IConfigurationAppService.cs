using System.Threading.Tasks;
using Abp.Application.Services;
using EE5207.Project.Configuration.Dto;

namespace EE5207.Project.Configuration
{
    public interface IConfigurationAppService: IApplicationService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}