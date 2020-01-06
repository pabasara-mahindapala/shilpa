using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EE5207.Project.Courses.Dto
{
    [AutoMapFrom(typeof(Course))]
    public class CourseDetailDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        public string Venue { get; set; }

        public string Day { get; set; }

        public string Time { get; set; }

        public long TeacherId { get; set; }
    }
}
