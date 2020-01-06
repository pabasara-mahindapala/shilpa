using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using EE5207.Project.Courses.Dto;

namespace EE5207.Project.Courses
{
    public class CourseAppService : ProjectAppServiceBase, ICourseAppService
    {
        private readonly IRepository<Course, Guid> _courseRepository;

        public CourseAppService(IRepository<Course, Guid> courseRepository)
        {
            _courseRepository = courseRepository;
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

    }
}
