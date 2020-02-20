using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EE5207.Project.Attendances
{
    public class Attendance : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        public virtual int TenantId { get; set; }

        public virtual int Percentage { get; set; }

        public virtual int PresentDays { get; set; }

        public virtual long StudentId { get; set; }

        public virtual Guid CourseId { get; set; }

        public static Attendance Create(int tenantId, int percentage, long studentId, Guid courseId)//add constructer
        {
            var @attendance = new Attendance
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                Percentage = percentage,
                PresentDays = 0,
                StudentId = studentId,
                CourseId = courseId,
            };

            return @attendance;
        }
    }
}
