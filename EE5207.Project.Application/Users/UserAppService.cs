using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.IdentityFramework;
using Abp.Notifications;
using EE5207.Project.Attendances;
using EE5207.Project.Authorization;
using EE5207.Project.Authorization.Roles;
using EE5207.Project.Authorization.Users;
using EE5207.Project.Courses;
using EE5207.Project.Roles.Dto;
using EE5207.Project.Users.Dto;
using Microsoft.AspNet.Identity;

namespace EE5207.Project.Users
{
    [AbpAuthorize(PermissionNames.Pages_Users)]
    public class UserAppService : AsyncCrudAppService<User, UserDto, long, PagedResultRequestDto, CreateUserDto, UpdateUserDto>, IUserAppService
    {
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<Attendance, Guid> _attendanceRepository;
        private readonly IRepository<Course, Guid> _courseRepository;
        private readonly IUserNotificationManager _userNotificationManager;

        public UserAppService(
            IRepository<User, long> repository,
            UserManager userManager,
            IRepository<User, long> userRepository,
            IRepository<Role> roleRepository,
            RoleManager roleManager,
            IRepository<Attendance, Guid> attendanceRepository,
            IRepository<Course, Guid> courseRepository,
            IUserNotificationManager userNotificationManager)
            : base(repository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _roleManager = roleManager;
            _attendanceRepository = attendanceRepository;
            _courseRepository = courseRepository;
            _userNotificationManager = userNotificationManager;
        }

        public override async Task<UserDto> Get(EntityDto<long> input)
        {
            var user = await base.Get(input);
            var userRoles = await _userManager.GetRolesAsync(user.Id);
            user.Roles = userRoles.Select(ur => ur).ToArray();
            return user;
        }

        public override async Task<UserDto> Create(CreateUserDto input)
        {
            CheckCreatePermission();

            var user = ObjectMapper.Map<User>(input);

            user.TenantId = AbpSession.TenantId;
            user.Password = new PasswordHasher().HashPassword(input.Password);
            user.IsEmailConfirmed = true;

            //Assign roles
            user.Roles = new Collection<UserRole>();
            foreach (var roleName in input.RoleNames)
            {
                var role = await _roleManager.GetRoleByNameAsync(roleName);
                user.Roles.Add(new UserRole(AbpSession.TenantId, user.Id, role.Id));
            }

            CheckErrors(await _userManager.CreateAsync(user));

            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(user);
        }

        public override async Task<UserDto> Update(UpdateUserDto input)
        {
            CheckUpdatePermission();

            var user = await _userManager.GetUserByIdAsync(input.Id);

            MapToEntity(input, user);

            CheckErrors(await _userManager.UpdateAsync(user));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRoles(user, input.RoleNames));
            }

            return await Get(input);
        }

        public override async Task Delete(EntityDto<long> input)
        {
            var user = await _userManager.GetUserByIdAsync(input.Id);
            await _userManager.DeleteAsync(user);
        }

        public async Task<ListResultDto<RoleDto>> GetRoles()
        {
            var roles = await _roleRepository.GetAllListAsync();
            return new ListResultDto<RoleDto>(ObjectMapper.Map<List<RoleDto>>(roles));
        }

        public async Task<string> GetRole(long userId)
        {
            var role = await _userManager.GetRolesAsync(userId);

            return role[0].ToString();
        }

        public async Task<ListResultDto<UserNotification>> GetUserNotifications(long userId)
        {
            var user = await _userManager.GetUserByIdAsync(userId);
            var userNotifications = _userNotificationManager.GetUserNotifications(user.ToUserIdentifier());


            return new ListResultDto<UserNotification>(ObjectMapper.Map<List<UserNotification>>(userNotifications));
        }

        public async Task<List<string>> GetUserAttendances(long userId)
        {

            var ids = (from attendance in _attendanceRepository.GetAll()
                       join course in _courseRepository.GetAll() on attendance.CourseId equals course.Id
                       where attendance.StudentId == userId
                       select course.Name).Concat
                       (from attendance in _attendanceRepository.GetAll()
                        join course in _courseRepository.GetAll() on attendance.CourseId equals course.Id
                        where attendance.StudentId == userId
                        select attendance.Percentage.ToString()).ToList();

            //var ids = _attendanceRepository.GetAll().Where(a => a.StudentId == userId).ToList();

            //return new ListResultDto<AttendanceDto>(ObjectMapper.Map<List<AttendanceDto>>(ids));

            return new List<string>(ids);

        }

        protected override User MapToEntity(CreateUserDto createInput)
        {
            var user = ObjectMapper.Map<User>(createInput);
            return user;
        }

        protected override void MapToEntity(UpdateUserDto input, User user)
        {
            ObjectMapper.Map(input, user);
        }

        protected override IQueryable<User> CreateFilteredQuery(PagedResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Roles);
        }

        protected override async Task<User> GetEntityByIdAsync(long id)
        {
            var user = Repository.GetAllIncluding(x => x.Roles).FirstOrDefault(x => x.Id == id);
            return await Task.FromResult(user);
        }

        protected override IQueryable<User> ApplySorting(IQueryable<User> query, PagedResultRequestDto input)
        {
            return query.OrderBy(r => r.UserName);
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}