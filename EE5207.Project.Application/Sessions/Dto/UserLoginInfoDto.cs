using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using EE5207.Project.Authorization.Users;
using EE5207.Project.Users;

namespace EE5207.Project.Sessions.Dto
{
    [AutoMapFrom(typeof(User))]
    public class UserLoginInfoDto : EntityDto<long>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string UserName { get; set; }

        public string EmailAddress { get; set; }
    }
}
