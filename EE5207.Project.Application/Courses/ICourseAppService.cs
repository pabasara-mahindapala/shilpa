using Abp.Application.Services;
using Abp.Application.Services.Dto;
using EE5207.Project.Courses.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EE5207.Project.Courses
{
    public interface ICourseAppService : IApplicationService
    {
        Task<ListResultDto<CourseListDto>> GetAll();

        Task<CourseDetailDto> Get(EntityDto<Guid> input);

        Task Create(CreateCourseDto input);

        Task Delete(EntityDto<Guid> input);

        Task Update(UpdateCourseDto input);

        Task EnrollStudent(long StudentId, Guid CourseId);

        Task<List<string>> GetStudents(Guid courseId);

        Task MarkAttendance(UpdateCourseDto input);
    }
}
