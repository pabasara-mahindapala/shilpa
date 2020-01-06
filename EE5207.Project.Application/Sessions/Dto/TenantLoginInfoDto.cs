using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using EE5207.Project.MultiTenancy;

namespace EE5207.Project.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}