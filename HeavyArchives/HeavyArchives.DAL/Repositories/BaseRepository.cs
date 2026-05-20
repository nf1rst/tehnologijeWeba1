using HeavyArchives.DAL.Data;
using HeavyArchives.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public void Delete(T entity)
        {
            context.Remove(entity);
        }

        public bool Exists(int id)
        { 
            var found = dbSet.Find(id);
            return found==null?false:true;
        }

        public ICollection<T> GetAll()
        {
            return dbSet.ToList(); //ovo vraca praznu listu ali nikad null
        }

        public T? GetById(int id)
        {
            return dbSet.Find(id);
        }

        public T Insert(T entity)
        {
            context.Add(entity);
            return entity;
        }

        public void Update(T entity)
        {
            dbSet.Update(entity); // sto ovo zapravo radi i hoce li utjecat na id
        }
    }
}
