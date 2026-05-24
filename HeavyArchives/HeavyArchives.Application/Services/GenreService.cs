using AutoMapper;
using Exceptions;
using HeavyArchives.Application.DTOs.Genres;
using HeavyArchives.Application.Interfaces;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Interfaces;
using System.Threading.Tasks;

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

        public async Task<GenreDTO> CreateGenreAsync(ManipulateGenreDTO createdDTO)
        {
            Genre genre = mapper.Map<Genre>(createdDTO);

            await uow.Genres.InsertAsync(genre);
            await uow.SaveChangesAsync();
            return mapper.Map<GenreDTO>(genre);
        }

        public async Task DeleteGenreAsync(int id)
        {
            Genre? genre =await uow.Genres.GetByIdAsync(id);
            if (genre is null) throw new EntityNotExistant("Genre wasn't found");
            await uow.Genres.DeleteAsync(genre);

            await uow.SaveChangesAsync();
        }

        public async Task<ICollection<GenreDTO>> GetAllGenresAsync()
        {
            var genres =await uow.Genres.GetAllGenresAsync();
            return mapper.Map<ICollection<GenreDTO>>(genres);
        }

        public async Task<GenreDTO?> GetGenreByIdAsync(int id)
        {
            var genre =await uow.Genres.GetGenreAsync(id);
            if (genre is null) throw new EntityNotExistant("Genre does not exist");
            return mapper.Map<GenreDTO>(genre);
        }

        public async Task<GenreDTO?> GetGenreByNameAsync(string name)
        {
            var genre =await uow.Genres.GetGenreByNameAsync(name);
            if (genre is null) throw new EntityNotExistant("Genre does not exist");
            return mapper.Map<GenreDTO>(genre);
        }

        public async Task<ICollection<GenreDTO>> GetGenresByBandAsync(string bandName)
        {
            var genres =await uow.Genres.GetGenresByBandAsync(bandName);
            return mapper.Map<ICollection<GenreDTO>>(genres);
        }

        public async Task<GenreDTO> UpdateGenreAsync(int id, ManipulateGenreDTO updateDTO)
        {
            Genre? genre =await uow.Genres.GetGenreWithDetailsAsync(id);
            if (genre is null) throw new EntityNotExistant("Genre wasn't found");

            mapper.Map(updateDTO, genre);
            await uow.SaveChangesAsync();

            return mapper.Map<GenreDTO>(genre);
        }
    }
}
