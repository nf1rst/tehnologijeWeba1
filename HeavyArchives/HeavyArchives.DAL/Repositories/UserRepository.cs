using HeavyArchives.DAL.Data;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HeavyArchives.DAL.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {

        //ovaj cancellation token koji je u svima ce se treba poslat u rezultat kasnije kad impl async
        private readonly HeavyArchivesContext context;
        public UserRepository(HeavyArchivesContext context):base(context)
        {
            this.context = context;
        }
        public bool UsernameExists(string username)
        {
            return context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Username == username)!=null?true:false;
        }

        public ICollection<User> GetAllUsersWithDetails()
        {
            return context.Users
                .Include(u => u.Role)
                .ToList();
        }

        public User? GetUserWithDetailsByUsername(string username)
        {
            return context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Username== username);
        }

        public User GetUserWithDetailsByRefreshToken(string refreshToken)
        {
            return context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.RefreshToken == refreshToken);
        }

        public User GetUserWithDetailsById(int id)
        {
            return context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Id == id);
        }
    }
}
