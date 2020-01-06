using Abp.AutoMapper;
using EE5207.Project.Sessions.Dto;

namespace EE5207.Project.Web.Models.Account
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}