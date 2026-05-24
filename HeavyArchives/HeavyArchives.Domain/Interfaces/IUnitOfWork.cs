namespace HeavyArchives.Domain.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IBandRepository Bands { get; }
        IAlbumRepository Albums { get; }
        ICountryRepository Countries { get; }
        IGenreRepository Genres { get; }
        ILabelRepository Labels { get; }
        IPersonRepository Persons { get; }
        IThemeRepository Themes { get; }
        IUserRepository Users { get; }
        IContactRepository Contacts { get; }
        IConcertRepository Concerts { get; }
        ICityRepository Cities { get; }
        ITourRepository Tours { get; }
        IBandMemberStatusRepository BandMemberStatuses { get; }
        //zasto ne i roles ovdje stavit 

        Task<int> SaveChangesAsync();

    }
}
