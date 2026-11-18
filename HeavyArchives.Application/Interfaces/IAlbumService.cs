using HeavyArchives.Application.DTOs.Album;

namespace HeavyArchives.Application.Interfaces
{
    public interface IAlbumService
    {
        Task<ICollection<AlbumDTO>> GetAllAlbumsAsync();
        Task<AlbumDTO> GetAlbumByIdAsync(int id);
        Task<ICollection<AlbumDTO>> GetAlbumsByBandAsync(string bandName);
        Task<ICollection<AlbumDTO>> GetAlbumsByYearAsync(int year);
        Task<AlbumDTO > UpdateAlbumAsync(int id, UpdateAlbuumDTO updatedBandDTO);
        Task DeleteAlbumAsync(int id);
        Task<AlbumDTO> CreateAlbumAsync(CreateAlbumDTO bandDTO);
        Task<AlbumDTO> GetAlbumByNameAsync(string name);
        Task<ICollection<AlbumDTO>> GetAlbumsByTypeAsync(string type);
    }
}
