using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Domain.Interfaces
{
    public interface IRepository<T> where T:class
    {
        T? GetById(int id); //ovaj moze vratit null zato ?
        ICollection<T> GetAll();
        T Insert(T entity);
        void Update(T entity); 
        void Delete(T entity);
        bool Exists(int id);
    }
}
