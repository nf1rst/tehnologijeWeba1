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
        ICollection<GenreDTO> GetAllGenres();
        GenreDTO? GetGenreById(int id);
        GenreDTO? GetGenreByName(string name);
        ICollection<GenreDTO> GetGenresByBand(string bandName);
        GenreDTO CreateGenre(ManipulateGenreDTO createdDTO);
        void DeleteGenre(int id);
        GenreDTO UpdateGenre(int id, ManipulateGenreDTO updateDTO);
    }
}
