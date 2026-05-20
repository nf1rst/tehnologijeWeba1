using AutoMapper;
using Exceptions;
using HeavyArchives.Application.DTOs.Album;
using HeavyArchives.Application.Interfaces;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Interfaces;

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

        public AlbumDTO CreateAlbum(CreateAlbumDTO createAlbumDTO)
        {
            var band = uow.Bands.GetBandWithDetails(createAlbumDTO.BandId);
            if (band is null) throw new Exceptions.EntityNotExistant("Band does not exist"); // ako bend ne postoji onda servis vraca null jer ne moze ubacit album za bend koji ne postoji

            var album = mapper.Map<Album>(createAlbumDTO); //createAlbum -> album

            uow.Albums.Insert(album);

            album.Band = band; // mora se postavit rucno jer nije u mapperu
            uow.SaveChanges();

            return mapper.Map<AlbumDTO>(album); //album -> albumDTO
        }

        public void DeleteAlbum(int id)
        {
            var target = uow.Albums.GetById(id);
            if (target is null) throw new EntityNotExistant("Album wasn't found");

            uow.Albums.Delete(target);
            uow.SaveChanges();
        }

        public AlbumDTO? GetAlbumById(int id) //moze vratit null za kontroler da ovaj vrati 404 not found
        {
            Album target = uow.Albums.GetAlbumWithDetailsById(id); //ako ovdje vrati getByID iz baznog repozitorija, nece imat bendove includane
            return mapper.Map<AlbumDTO>(target);
                
        }

        public ICollection<AlbumDTO> GetAlbumsByBand(string bandName)
        {
            var albums = uow.Albums.GetAlbumsByBandName(bandName);
            return mapper.Map<ICollection<AlbumDTO>>(albums);
        }

        public ICollection<AlbumDTO> GetAlbumsByYear(int year)
        {
            var albums = uow.Albums.GetAlbumsByYear(year);
            return mapper.Map<ICollection<AlbumDTO>>(albums);
        }

        public ICollection<AlbumDTO> GetAllAlbums()
        {
            var albums = uow.Albums.GetAlbumsWithDetails();
            return mapper.Map<ICollection<AlbumDTO>>(albums);
        }

        public AlbumDTO UpdateAlbum(int id, UpdateAlbuumDTO updatedAlbumDTO)
        {
            Album album = uow.Albums.GetAlbumWithDetailsById(id);
            if (album is null) throw new EntityNotExistant("Album wasn't found");

            mapper.Map(updatedAlbumDTO, album);

            uow.SaveChanges();
            return mapper.Map<AlbumDTO>(album);
        }

        public AlbumDTO GetAlbumByName(string name)
        {
            Album album = uow.Albums.GetAlbumByName(name);
            return mapper.Map<AlbumDTO>(album);
        }

        public ICollection<AlbumDTO> GetAlbumsByType(string type)
        {
            var albums = uow.Albums.GetAlbumsByType(type);
            return mapper.Map<ICollection<AlbumDTO>>(albums);
        }
    }
}
