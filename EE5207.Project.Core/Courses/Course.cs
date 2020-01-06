using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EE5207.Project.Courses
{
    public class Course : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        public virtual int TenantId { get; set; }

        public virtual string Name { get; set; }

        public virtual string Venue { get; set; }

        public virtual string Day { get; set; }

        public virtual string Time { get; set; }

        public virtual long TeacherId { get; set; }

        public static Course Create(int tenantId, string name, string venue, string day, string time, long teacherId)//add constructer
        {
            var @course = new Course
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                Name = name,
                Venue = venue,
                Day = day,
                Time = time,
                TeacherId = teacherId
            };

            return @course;
        }
    }
}
