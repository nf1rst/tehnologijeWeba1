using EncyclopaediaBisemutum.Application.DTOs.Auth;
using EncyclopaediaBisemutum.Application.DTOs.User;
using HeavyArchives.Application.DTOs.Auth;

namespace EncyclopaediaBisemutum.Application.Interfaces
{
    public interface IAdminService
    {
        Task<ICollection<UserDTO>> GetAllUsersAsync();
        Task<UserWithDetailsDTO?> GetUserByIdAsync(int id);
        Task<UserWithDetailsDTO?> GetUserByUsernameAsync(string username);
        Task DeleteUserAsync(int id);
        Task<UserWithDetailsDTO?> AddAdminUser(UserRegisterWithDetailsDTO adminUserDTO); 
    }
}
