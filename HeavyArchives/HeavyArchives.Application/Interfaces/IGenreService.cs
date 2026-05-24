using HeavyArchives.Application.DTOs.Genres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Application.Interfaces
{
    public interface IGenreService
    {
        Task<ICollection<GenreDTO>> GetAllGenresAsync();
        Task<GenreDTO?> GetGenreByIdAsync(int id);
        Task<GenreDTO?> GetGenreByNameAsync(string name);
        Task<ICollection<GenreDTO>> GetGenresByBandAsync(string bandName);
        Task<GenreDTO> CreateGenreAsync(ManipulateGenreDTO createdDTO);
        Task DeleteGenreAsync(int id);
        Task<GenreDTO >UpdateGenreAsync(int id, ManipulateGenreDTO updateDTO);
    }
}
