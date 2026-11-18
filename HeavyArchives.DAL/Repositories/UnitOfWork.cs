using HeavyArchives.DAL.Data;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Interfaces;
using HeavyArchives.Domain.Interfaces.M2M;
using System.Threading.Tasks;

namespace HeavyArchives.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
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
        public IBandMemberStatusRepository BandMemberStatuses{ get; }

        public IBandPersonRepository BandPersons { get; }

        public IBandLabelRepository BandLabel { get; }

        public IBandThemeRepository BandThemes { get; }

        public IBandTourRepository BandTours { get; }

        public IGenreBandRepository GenreBands { get; }

        public ILabelThemeRepository LabelThemes { get; }

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
            IBandMemberStatusRepository bandMemberStatuses,
            IBandPersonRepository bandPersons,
            IBandLabelRepository bandLabels,
            IBandThemeRepository bandThemes,
            IBandTourRepository bandTour,
            IGenreBandRepository genreBands,
            ILabelThemeRepository labelTheme
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
            BandPersons = bandPersons;
            BandLabel = bandLabels;
            BandThemes = bandThemes;
            BandTours = bandTour;
            GenreBands = genreBands;
            LabelThemes = labelTheme;
        }

        //https://www.learnentityframeworkcore.com/saving/save-changes
        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
