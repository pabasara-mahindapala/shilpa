using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;

namespace EE5207.Project.Publications.Dto
{
    [AutoMap(typeof(Publication))]
    public class CreatePublicationDto : FullAuditedEntityDto<Guid>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string FilePath { get; set; }

        public int Downloads { get; set; }
    }
}
