using AutoMapper;
using Exceptions;
using HeavyArchives.Application.DTOs.BandMemberStatus;
using HeavyArchives.Application.Interfaces;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Interfaces;
using System.Threading.Tasks;

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
        public async Task<BandMemberStatusDTO> CreateBandMemberStatusAsync(ManipulateBandMemberStatusDTO createDTO)
        {
            BandMemberStatus status = mapper.Map<BandMemberStatus>(createDTO);

            await uow.BandMemberStatuses.InsertAsync(status);
            await uow.SaveChangesAsync();
            return mapper.Map<BandMemberStatusDTO>(status);
        }

        public async Task DeleteBandMemberStatusAsync(int id)
        {
            BandMemberStatus? status =  await uow.BandMemberStatuses.GetByIdAsync(id);
            if (status is null) throw new Exceptions.EntityNotExistant("Band member status with that ID does not exist");

            await uow.BandMemberStatuses.DeleteAsync(status);
            await uow.SaveChangesAsync();
        }

        public async Task<ICollection<BandMemberStatusDTO>> GetAllStatusesAsync()
        {
            var statuses = await uow.BandMemberStatuses.GetAllStatusesWithDetailsAsync();
            return mapper.Map<ICollection<BandMemberStatusDTO>>(statuses);
        }

        public async Task<BandMemberStatusDTO> UpdateBandMemberStatusAsync(int id, ManipulateBandMemberStatusDTO updateDTO)
        {
            BandMemberStatus? status = await uow.BandMemberStatuses.GetStatusWithDetailsByIDAsync(id);
            if (status is null) throw new EntityNotExistant("Band member status not found");
            mapper.Map(updateDTO, status);
            await uow.SaveChangesAsync();
            return mapper.Map<BandMemberStatusDTO>(status);

        }
    }
}
