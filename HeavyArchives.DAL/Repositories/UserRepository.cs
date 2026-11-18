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
        public UserRepository(HeavyArchivesContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<User?> GetUserWithDetailsByUsernameAsync(string username)
        {
            return await context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == username) != null ? true : false;
        }

        public async  Task<ICollection<User>> GetAllUsersWithDetailsAsync()
        {
            return await context.Users
                .Include(u => u.Role)
                .ToListAsync();
        }

        public async Task<User?> GetUserWithDetailsByRefreshTokenAsync(string refreshToken)
        {
            return await context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        }

        public async Task<User?> GetUserWithDetailsByIdAsync(int id)
        {
            return await context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
