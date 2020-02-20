using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using EE5207.Project.Attendances;
using EE5207.Project.Authorization.Users;
using EE5207.Project.Courses.Dto;
using EE5207.Project.Enrollments;

namespace EE5207.Project.Courses
{
    public class CourseAppService : ProjectAppServiceBase, ICourseAppService
    {
        private readonly IRepository<Course, Guid> _courseRepository;
        private readonly IRepository<Enrollment, Guid> _enrollmentRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<Attendance, Guid> _attendanceRepository;

        public CourseAppService(IRepository<Course, Guid> courseRepository, IRepository<Enrollment, Guid> enrollmentRepository, IRepository<User, long> userRepository, IRepository<Attendance, Guid> attendanceRepository)
        {
            _courseRepository = courseRepository;
            _enrollmentRepository = enrollmentRepository;
            _userRepository = userRepository;
            _attendanceRepository = attendanceRepository;
        }

        public async Task<ListResultDto<CourseListDto>> GetAll()
        {
            var courses = await _courseRepository
                .GetAllListAsync();

            return new ListResultDto<CourseListDto>(courses.MapTo<List<CourseListDto>>());
        }

        public async Task Create(CreateCourseDto input)
        {
            var @course = Course.Create(1, input.Name, input.Venue, input.Day, input.Time, input.TeacherId);

            await _courseRepository.InsertAsync(@course);
        }

        public async Task<CourseDetailDto> Get(EntityDto<Guid> input)
        {
            var @course = await _courseRepository
                .GetAsync(input.Id);


            if (@course == null)
            {
                throw new UserFriendlyException("Could not find the course");
            }

            return @course.MapTo<CourseDetailDto>();
        }

        public async Task<UpdateAttendanceDto> GetAttendance(Guid id)
        {
            var attendance = await _attendanceRepository
                .GetAsync(id);


            if (attendance == null)
            {
                throw new UserFriendlyException("Could not find the attendance");
            }

            return attendance.MapTo<UpdateAttendanceDto>();
        }

        public async Task Delete(EntityDto<Guid> input)
        {
            await _courseRepository.DeleteAsync(input.Id);
        }

        public async Task Update(UpdateCourseDto input)
        {
            var @course = input.MapTo<Course>();
            await _courseRepository.UpdateAsync(@course);
        }

        public async Task UpdateAttendance(UpdateAttendanceDto input)
        {
            var @attendance = input.MapTo<Attendance>();
            await _attendanceRepository.UpdateAsync(@attendance);
        }

        public async Task EnrollStudent(long StudentId, Guid CourseId)
        {
            var @enrollment = Enrollment.Create(1, StudentId, CourseId);

            await _enrollmentRepository.InsertAsync(@enrollment);
        }

        public async Task<List<string>> GetStudents(Guid courseId)
        {
            var students = (from enrollment in _enrollmentRepository.GetAll()
                            join student in _userRepository.GetAll() on enrollment.StudentId equals student.Id
                            where enrollment.CourseId == courseId
                            select student.UserName).Distinct().ToList();

            return new List<string>(students);
        }

        public async Task MarkAttendance(UpdateCourseDto input)
        {
            var conductedDays = (from coursetable in _courseRepository.GetAll()
                                 where coursetable.Id == input.Id
                                 select coursetable.ConductedDays).ToList();

            conductedDays[0]++;
            input.ConductedDays = conductedDays[0];
            var @course = input.MapTo<Course>();
            await _courseRepository.UpdateAsync(@course);

        }

        public async Task<Guid> MarkStudent(long StudentId, Guid CourseId)
        {
            var attendance = (from attendancetable in _attendanceRepository.GetAll()
                              where attendancetable.StudentId == StudentId
                              where attendancetable.CourseId == CourseId
                              select attendancetable.Id).ToList();

            return attendance[0];

        }
    }
}
