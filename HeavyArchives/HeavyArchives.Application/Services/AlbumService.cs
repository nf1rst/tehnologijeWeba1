using AutoMapper;
using Exceptions;
using HeavyArchives.Application.DTOs.Album;
using HeavyArchives.Application.Interfaces;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Interfaces;
using System.Threading.Tasks;

namespace HeavyArchives.Application.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public AlbumService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow= uow;
            this.mapper=mapper;
        }


        public async Task DeleteAlbumAsync(int id)
        {
            Album? target = await uow.Albums.GetByIdAsync(id);
            if (target is null) throw new EntityNotExistant("Album wasn't found");

            await uow.Albums.DeleteAsync(target);
            await uow.SaveChangesAsync();
        }

        public async Task<AlbumDTO?> GetAlbumByIdAsync(int id) //moze vratit null za kontroler da ovaj vrati 404 not found
        {
            Album? target = await uow.Albums.GetAlbumWithDetailsByIdAsync(id); //ako ovdje vrati getByID iz baznog repozitorija, nece imat bendove includane
            if (target is null) throw new EntityNotExistant($"Album with id {id} was not found");
            return mapper.Map<AlbumDTO>(target);
                
        }

        public async Task<ICollection<AlbumDTO>> GetAlbumsByBandAsync(string bandName)
        {
            var albums = await uow.Albums.GetAlbumsByBandNameAsync(bandName);
            return mapper.Map<ICollection<AlbumDTO>>(albums);
        }

        public async Task<ICollection<AlbumDTO>> GetAlbumsByYearAsync(int year)
        {
            var albums =await  uow.Albums.GetAlbumsByYearAsync(year);
            return mapper.Map<ICollection<AlbumDTO>>(albums);
        }

        public async Task<ICollection<AlbumDTO>> GetAllAlbumsAsync()
        {
            var albums = await uow.Albums.GetAlbumsWithDetailsAsync();
            return mapper.Map<ICollection<AlbumDTO>>(albums);
        }

        public async Task<AlbumDTO> UpdateAlbumAsync(int id, UpdateAlbuumDTO updatedAlbumDTO)
        {
            Album? album = await uow.Albums.GetAlbumWithDetailsByIdAsync(id);
            if (album is null) throw new EntityNotExistant("Album wasn't found");

            Band? band = await uow.Bands.GetByIdAsync(updatedAlbumDTO.BandId);
            if (band is null) throw new EntityNotExistant($"Band with id [{id}] was not found");

            mapper.Map(updatedAlbumDTO, album);

            await uow.SaveChangesAsync();
            return mapper.Map<AlbumDTO>(album);
        }

        public async Task<AlbumDTO> GetAlbumByNameAsync(string name)
        {
            Album? album = await uow.Albums.GetAlbumByNameAsync(name);
            return mapper.Map<AlbumDTO>(album);
        }

        public async Task<ICollection<AlbumDTO>> GetAlbumsByTypeAsync(string type)
        {
            var albums = await uow.Albums.GetAlbumsByTypeAsync(type);
            return mapper.Map<ICollection<AlbumDTO>>(albums);
        }

        public async Task<AlbumDTO> CreateAlbumAsync(CreateAlbumDTO bandDTO)
        {
            var band =await uow.Bands.GetBandWithDetailsAsync(bandDTO.BandId);
            if (band is null) throw new Exceptions.EntityNotExistant("Band does not exist"); // ako bend ne postoji onda servis vraca null jer ne moze ubacit album za bend koji ne postoji

            var album = mapper.Map<Album>(bandDTO); //createAlbum -> album

            await uow.Albums.InsertAsync(album);

            album.Band = band; 
            await uow.SaveChangesAsync();

            return  mapper.Map<AlbumDTO>(album); //album -> albumDTO

        }

   
    }
}
