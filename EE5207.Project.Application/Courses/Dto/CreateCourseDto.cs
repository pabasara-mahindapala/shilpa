using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EE5207.Project.Courses.Dto
{
    [AutoMap(typeof(Course))]
    public class CreateCourseDto : FullAuditedEntityDto<Guid>
    {
        [Required]
        public string Name { get; set; }

        public string Venue { get; set; }

        public string Day { get; set; }

        public string Time { get; set; }

        public long TeacherId { get; set; }

    }
}
