using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using EE5207.Project.Attendances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EE5207.Project.Courses.Dto
{
    [AutoMap(typeof(Attendance))]
    public class UpdateAttendanceDto : FullAuditedEntityDto<Guid>
    {
        public int Percentage { get; set; }

        public int PresentDays { get; set; }

        public long StudentId { get; set; }

        public Guid CourseId { get; set; }

    }
}
