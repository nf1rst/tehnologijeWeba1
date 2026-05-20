using AutoMapper;
using HeavyArchives.Application.DTOs.BandMemberStatus;
using HeavyArchives.Application.Interfaces;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Interfaces;

namespace HeavyArchives.Application.Services
{
    public class BandMemberStatusService : IBandMemberStatusService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public BandMemberStatusService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        public BandMemberStatusDTO CreateBandMemberStatus(ManipulateBandMemberStatusDTO createDTO)
        {
            BandMemberStatus status = mapper.Map<BandMemberStatus>(createDTO);

            uow.BandMemberStatuses.Insert(status);
            return mapper.Map<BandMemberStatusDTO>(status);
        }

        public void DeleteBandMemberStatus(int id)
        {
            if (!uow.BandMemberStatuses.Exists(id)) throw new Exceptions.EntityNotExistant("Band member status with that ID does not exist");
            BandMemberStatus status = uow.BandMemberStatuses.GetById(id)!;
            uow.BandMemberStatuses.Delete(status);
            uow.SaveChanges();
        }

        public ICollection<BandMemberStatusDTO> GetAllStatuses()
        {
            var statuses = uow.BandMemberStatuses.GetAllStatusesWithDetails();
            return mapper.Map<ICollection<BandMemberStatusDTO>>(statuses);
        }

        public BandMemberStatusDTO UpdateBandMemberStatus(int id, ManipulateBandMemberStatusDTO updateDTO)
        {
            BandMemberStatus status = uow.BandMemberStatuses.GetStatusWithDetailsByID(id);

            mapper.Map(updateDTO, status);
            return mapper.Map<BandMemberStatusDTO>(status);

        }
    }
}
