using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Notifications;
using EE5207.Project.Attendances;
using EE5207.Project.Roles.Dto;
using EE5207.Project.Users.Dto;

namespace EE5207.Project.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UpdateUserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task<string> GetRole(long userId);

        Task<ListResultDto<UserNotification>> GetUserNotifications(long userId);

        //Task<ListResultDto<AttendanceDto>> GetUserAttendances(long userId);
        //Task<List<IGrouping<string, int>>> GetUserAttendances(long userId);
        Task<List<string>> GetUserAttendances(long userId);
    }
}