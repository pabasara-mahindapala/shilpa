using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
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

        public CourseAppService(IRepository<Course, Guid> courseRepository, IRepository<Enrollment, Guid> enrollmentRepository, IRepository<User, long> userRepository)
        {
            _courseRepository = courseRepository;
            _enrollmentRepository = enrollmentRepository;
            _userRepository = userRepository;
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

        public async Task Delete(EntityDto<Guid> input)
        {
            await _courseRepository.DeleteAsync(input.Id);
        }

        public async Task Update(UpdateCourseDto input)
        {
            var @course = input.MapTo<Course>();
            await _courseRepository.UpdateAsync(@course);
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
    }
}
