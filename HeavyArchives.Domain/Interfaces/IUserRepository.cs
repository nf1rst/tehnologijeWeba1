using HeavyArchives.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Domain.Interfaces
{
    public interface IUserRepository:IRepository<User>
    {
        Task<User?> GetUserWithDetailsByUsernameAsync(string username);
        Task<bool> UsernameExistsAsync(string username);
        Task<ICollection<User>> GetAllUsersWithDetailsAsync();
        Task<User?> GetUserWithDetailsByRefreshTokenAsync(string refreshToken);
        Task<User?> GetUserWithDetailsByIdAsync(int id);
    }
}
