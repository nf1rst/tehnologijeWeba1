using AutoMapper;
using Exceptions;
using HeavyArchives.Application.DTOs.Genres;
using HeavyArchives.Application.Interfaces;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Interfaces;

namespace HeavyArchives.Application.Services
{
    public class GenreService:IGenreService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public GenreService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public GenreDTO CreateGenre(ManipulateGenreDTO createdDTO)
        {
            Genre genre = mapper.Map<Genre>(createdDTO);

            uow.Genres.Insert(genre);
            uow.SaveChanges();
            return mapper.Map<GenreDTO>(genre);
        }

        public void DeleteGenre(int id)
        {
            Genre? genre = uow.Genres.GetById(id);
            if (genre is null) throw new EntityNotExistant("Genre wasn't found");
            uow.Genres.Delete(genre);

            uow.SaveChanges();
        }

        public ICollection<GenreDTO> GetAllGenres()
        {
            var genres = uow.Genres.GetAllGenres();
            return mapper.Map<ICollection<GenreDTO>>(genres);
        }

        public GenreDTO? GetGenreById(int id)
        {
            var genre = uow.Genres.GetGenre(id);
            if (genre is null) throw new EntityNotExistant("Genre does not exist");
            return mapper.Map<GenreDTO>(genre);
        }

        public GenreDTO? GetGenreByName(string name)
        {
            var genre = uow.Genres.GetGenreByName(name);
            if (genre is null) throw new EntityNotExistant("Genre does not exist");
            return mapper.Map<GenreDTO>(genre);
        }

        public ICollection<GenreDTO> GetGenresByBand(string bandName)
        {
            var genres = uow.Genres.GetGenresByBand(bandName);
            return mapper.Map<ICollection<GenreDTO>>(genres);
        }

        public GenreDTO UpdateGenre(int id, ManipulateGenreDTO updateDTO)
        {
            Genre? genre = uow.Genres.GetGenreWithDetails(id);
            if (genre is null) throw new EntityNotExistant("Genre wasn't found");

            mapper.Map(updateDTO, genre);
            uow.SaveChanges();

            return mapper.Map<GenreDTO>(genre);
        }
    }
}
