using HeavyArchives.DAL.Data;
using HeavyArchives.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Abstractions;

namespace HeavyArchives.DAL.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly HeavyArchivesContext context;
        protected readonly DbSet<T> dbSet; // tako da mogu izvrisat entitet T a ne nesto konretno 100 puta
        public BaseRepository(HeavyArchivesContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>(); // PROUCI OVO DALJE sta radi
        }


        public Task DeleteAsync(T entity)
        {
            context.Remove(entity);
            return Task.CompletedTask;
        }

        public async  Task<bool> ExistsAsync(int id)
        {
            return await dbSet.FindAsync(id) is not null;
        }


        public async Task<ICollection<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }


        public async Task<T?> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }



        public async Task<T> InsertAsync(T entity)
        {
            await context.AddAsync(entity);
            return entity;
        }
        public Task UpdateAsync(T entity)
        {
            dbSet.Update(entity);
            return Task.CompletedTask;
        }
    }
}
