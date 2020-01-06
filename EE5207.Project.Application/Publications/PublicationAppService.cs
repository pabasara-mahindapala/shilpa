using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Notifications;
using Abp.Runtime.Session;
using Abp.UI;
using EE5207.Project.Publications.Dto;

namespace EE5207.Project.Publications
{
    public class PublicationAppService : ProjectAppServiceBase, IPublicationAppService
    {
        private readonly IRepository<Publication, Guid> _publicationRepository;
        private readonly INotificationPublisher _notificationPublisher;

        public PublicationAppService(IRepository<Publication, Guid> publicationRepository, INotificationPublisher notificationPublisher)
        {
            _publicationRepository = publicationRepository;
            _notificationPublisher = notificationPublisher;
        }

        public async Task Create(CreatePublicationDto input)
        {
            var @publication = Publication.Create(1, input.Name, input.Description, input.FilePath, input.Downloads);

            await _publicationRepository.InsertAsync(@publication);
        }

        public async Task<PublicationDetailDto> Get(EntityDto<Guid> input)
        {
            var @publication = await _publicationRepository
                .GetAsync(input.Id);


            if (@publication == null)
            {
                throw new UserFriendlyException("Could not find the publication");
            }

            return @publication.MapTo<PublicationDetailDto>();
        }

        public async Task<ListResultDto<PublicationListDto>> GetAll()
        {
            var publications = await _publicationRepository
                .GetAllListAsync();

            var user = await UserManager.GetUserByIdAsync(3);

            await _notificationPublisher.PublishAsync(
                notificationName: "NewsNotification",
                data: new MessageNotificationData("And that's the way the news goes"),
                severity: NotificationSeverity.Success,
                userIds: new[] { user.ToUserIdentifier() }
            );

            return new ListResultDto<PublicationListDto>(publications.MapTo<List<PublicationListDto>>());
        }

        public async Task Delete(EntityDto<Guid> input)
        {
            await _publicationRepository.DeleteAsync(input.Id);
        }

        public async Task Update(UpdatePublicationDto input)
        {
            var @publication = input.MapTo<Publication>();
            //@publication.TenantId = AbpSession.GetTenantId();
            await _publicationRepository.UpdateAsync(@publication);
        }
    }
}
