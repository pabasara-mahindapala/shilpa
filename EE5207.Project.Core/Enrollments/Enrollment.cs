using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EE5207.Project.Enrollments
{
    public class Enrollment : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        public virtual int TenantId { get; set; }

        public virtual long StudentId { get; set; }

        public virtual Guid CourseId { get; set; }

        public static Enrollment Create(int tenantId, long studentId, Guid courseId)    //add constructer
        {
            var @enrollment = new Enrollment
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                StudentId = studentId,
                CourseId = courseId,
            };

            return @enrollment;
        }
    }
}
