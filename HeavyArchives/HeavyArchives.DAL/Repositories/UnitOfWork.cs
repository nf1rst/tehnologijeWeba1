using HeavyArchives.DAL.Data;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Interfaces;

namespace HeavyArchives.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
        //unit of work omogucava da iako svaki repozitorij prima svoju instancu dbContexta ja mogu sa sve changes u unit of work spremit sve 
        //umjesto za svakog posebno
        //isto omogucuje da ako unutar inste funkcije negdje kasnije pukne nesto vezano uz bazu, ovo je kao rollback u bazama da ne doda nista a ne na pol
    {
        public IAlbumRepository Albums{ get;}
        public IBandRepository Bands{ get;}
        public IPersonRepository Persons{ get;}
        public ILabelRepository Labels{ get;}
        public ICountryRepository Countries{ get;}
        public IGenreRepository Genres{ get; }
        public IThemeRepository Themes{ get;}
        public IUserRepository Users{ get;}
        public IContactRepository Contacts{ get; }
        public ITourRepository Tours{ get; }
        public IConcertRepository Concerts{ get; }
        public ICityRepository Cities{ get; }
        public IBandMemberStatusRepository BandMemberStatuses{ get; set; }
        private readonly HeavyArchivesContext context;

        public UnitOfWork
            (
            HeavyArchivesContext context,
            IBandRepository bands, 
            IAlbumRepository albums,
            IPersonRepository persons,
            ILabelRepository labels,
            IGenreRepository genres,
            ICountryRepository countries,
            IThemeRepository themes,
            IUserRepository users,
            IContactRepository contacts,
            ITourRepository tours,
            IConcertRepository concerts,
            ICityRepository cities,
            IBandMemberStatusRepository bandMemberStatuses
            )
        {
            this.context = context;
            Albums = albums;
            Bands = bands;
            Persons = persons;
            Labels = labels;
            Countries = countries;
            Genres = genres;
            Themes = themes;
            Users = users;
            Contacts = contacts;
            Tours = tours;
            Concerts = concerts;
            Cities = cities;
            BandMemberStatuses = bandMemberStatuses;          
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }
    }
}
