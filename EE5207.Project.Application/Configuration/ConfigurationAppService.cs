﻿using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using EE5207.Project.Configuration.Dto;

namespace EE5207.Project.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : ProjectAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
