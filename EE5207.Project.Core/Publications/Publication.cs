using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EE5207.Project.Publications
{
    public class Publication : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        public virtual int TenantId { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public string FilePath { get; set; }

        public virtual int Downloads { get; set; }

        public static Publication Create(int tenantId, string name, string description, string filePath, int downloads)//add constructer
        {
            var @publication = new Publication
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                Name = name,
                Description = description,
                FilePath = filePath,
                Downloads = downloads
            };

            return @publication;
        }

    }
}
