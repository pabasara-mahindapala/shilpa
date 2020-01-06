using Abp.Application.Services;
using Abp.Application.Services.Dto;
using EE5207.Project.Publications.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EE5207.Project.Publications
{
    public interface IPublicationAppService : IApplicationService
    {
        Task<ListResultDto<PublicationListDto>> GetAll();

        Task<PublicationDetailDto> Get(EntityDto<Guid> input);

        Task Create(CreatePublicationDto input);

        Task Delete(EntityDto<Guid> input);

        Task Update(UpdatePublicationDto input);
    }
}
