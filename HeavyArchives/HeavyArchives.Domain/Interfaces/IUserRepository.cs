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
        User? GetUserWithDetailsByUsername(string username);
        bool UsernameExists(string username);
        ICollection<User> GetAllUsersWithDetails();
        User GetUserWithDetailsByRefreshToken(string refreshToken);
        User GetUserWithDetailsById(int id);
    }
}
