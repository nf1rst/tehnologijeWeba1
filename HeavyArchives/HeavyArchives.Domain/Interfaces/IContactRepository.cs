using HeavyArchives.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Domain.Interfaces
{
    public interface IContactRepository:IRepository<Contact>
    {
        Task<Contact? >GetContactForBandAsync(string bandName);

    }
}
