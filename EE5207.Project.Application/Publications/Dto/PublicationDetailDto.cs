﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EE5207.Project.Publications.Dto
{
    [AutoMapFrom(typeof(Publication))]
    public class PublicationDetailDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string FilePath { get; set; }

        public int Downloads { get; set; }
    }
}
