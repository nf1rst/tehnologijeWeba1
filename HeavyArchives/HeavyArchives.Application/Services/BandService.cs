using AutoMapper;
using Exceptions;
using HeavyArchives.Application.DTOs.Band;
using HeavyArchives.Application.DTOs.BandMemberStatus;
using HeavyArchives.Application.DTOs.Genres;
using HeavyArchives.Application.DTOs.Person;
using HeavyArchives.Application.Interfaces;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Entities.M2MTables;
using HeavyArchives.Domain.Interfaces;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace HeavyArchives.Application.Services
{
    public class BandService : IBandService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public BandService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper=mapper;
        }
        public async Task<BandWithMembersDTO> AddGenreAsync(int bandId, int genreId)
        {
            var band = await uow.Bands.GetBandWithDetailsAsync(bandId);
            if (band is null) throw new EntityNotExistant("Band wasn't found");
            var genre = await uow.Genres.GetGenreWithDetailsAsync(genreId);
            if (genre is null) throw new EntityNotExistant("Genre wasn't found");

            if (band.GenreBand.Any(gb => gb.GenreId == genreId)) throw new Exceptions.RelationExistsException("That genre is already attibuted to the band"); //ako zanr vec postoji da podupla

            //in memory 
            band.GenreBand.Add(new GenreBand() { BandId=bandId, GenreId=genreId});
            //await uow.GenreBands.AddGenreBandAsync(new GenreBand() { BandId=bandId, GenreId=genreId });

            await uow.SaveChangesAsync();
            return mapper.Map<BandWithMembersDTO>(band);
        }

        public async Task<BandWithMembersDTO> AddMemberAsync(int bandId, int personId, string status, string role)
        {
            var band = await  uow.Bands.GetBandWithDetailsAsync(bandId);
            if (band is null) throw new EntityNotExistant("Band wasn't found");

            var person = await uow.Persons.GetPersonWithDetailsAsync(personId);
            if (person is null) throw new EntityNotExistant("Person wasn't found");

            BandMemberStatus? statusFound = await  uow.BandMemberStatuses.GetStatusWithDetailsByNameAsync(status);
            if (statusFound is null) throw new EntityNotExistant("Status you've put doesn't exist");

            if (person.DateOfDeath != null && statusFound.Name == "Active") throw new InvalidLogicException("Person cannot be dead and active in a band");

            
            BandPerson? entry=await uow.BandPersons.GetBandPersonByParamAsync(personId, bandId);
            if (entry is not null)throw new Exceptions.RelationExistsException("That band member is already associated with the band"); // ako vec postoji bend member nemoj ga dodat 2 put 

            BandPerson? forInsert = new BandPerson()
            {
                Band = band,
                Person = person,
                Status = statusFound,
                RoleInBand = role
            };

            band.BandPerson.Add(forInsert);
            //await uow.BandPersons.AddBandPersonAsync(new BandPerson() { Band = band, Person = person });
            
            await uow.SaveChangesAsync();
            return mapper.Map<BandWithMembersDTO>(band);
        }

        public async Task<BandDTO> CreateBandAsync(ManipulateBandDTO createdBandDTO)
        {
            Country? country =await uow.Countries.GetCountryWithDetailsByNameAsync(createdBandDTO.CounrtyName);
            if(country is null && createdBandDTO.CounrtyName != null)
            {
                country=new Country() { Name=createdBandDTO.CounrtyName };
                await uow.Countries.InsertAsync(country);
            }
            Band band = mapper.Map<Band>(createdBandDTO);

            band.Country = country!; //jer mapper ignorira

            await uow.Bands.InsertAsync(band);
            await uow.SaveChangesAsync();
            return mapper.Map<BandDTO>(band);
        }

        public async Task DeleteBandAsync(int id)
        {
            var band = await uow.Bands.GetByIdAsync(id);
            if (band is null) throw new Exceptions.EntityNotExistant("Band wasn't found");

            await uow.Bands.DeleteAsync(band);
            await uow.SaveChangesAsync();
        }

        public async Task<ICollection<BandMinimumInfoDTO>> GetAllBandsAsync()
        {
            var bands =await uow.Bands.GetAllBandsAsync();
            return mapper.Map<ICollection<BandMinimumInfoDTO>>(bands);
        }

        public async Task<ICollection<BandMinimumInfoDTO>> GetBandsByCountryAsync(string countryName)
        {
            var bands=await uow.Bands.GetBandByCountryAsync(countryName);
            return mapper.Map<ICollection<BandMinimumInfoDTO>>(bands);
        }

        public async Task<ICollection<BandMinimumInfoDTO>> GetBandsByGenreAsync(string genreName)
        {
            var bands=await uow.Bands.GetBandsByGenreAsync(genreName);
            return mapper.Map<ICollection<BandMinimumInfoDTO>>(bands);
        }

        public async Task<ICollection<BandMinimumInfoDTO>> GetBandsDetailsWithMembersAsync()
        {
            var bands =await uow.Bands.GetBandsWithDetailsAsync();
            return mapper.Map<ICollection<BandMinimumInfoDTO>>(bands);
        }

        public async Task<BandWithMembersDTO> GetBandByIdAsync(int id)
        {
            Band? b=await uow.Bands.GetBandWithDetailsAsync(id);
            if (b is null) throw new EntityNotExistant("Band wasn't found"); 
            return mapper.Map<BandWithMembersDTO>(b);
        }

        public async Task<BandWithMembersDTO> RemoveGenreAsync(int bandId, int genreId)
        {               
            var band =await uow.Bands.GetBandWithDetailsAsync(bandId);
            if (band is null) throw new EntityNotExistant("Band wasn't found");

            //var genre =await uow.Genres.GetGenreWithDetailsAsync(genreId);
            //if (genre is null) throw new EntityNotExistant("Genre wasn't found");

            var entry = band.GenreBand.FirstOrDefault(b => b.GenreId == genreId);
            if (entry is null) throw new Exceptions.NoRelationException("That genre is not assiciated with the band or not existant");// ne postoji taj genre za odredeni bend 

            //await uow.GenreBands.RemoveGenreBand(entry);
            band.GenreBand.Remove(entry);
            
            await uow.SaveChangesAsync();
            return mapper.Map<BandWithMembersDTO>(band);
        }

        public async Task<BandWithMembersDTO> RemoveMemberAsync(int bandId, int personId)
        { 
            var band = await uow.Bands.GetBandWithDetailsAsync(bandId);
            if (band is null) throw new EntityNotExistant("Band wasn't found");

            var person =await uow.Persons.GetPersonWithDetailsAsync(personId);
            if (person is null) throw new EntityNotExistant("Person wasn't found");

            //3
            var entry = await uow.BandPersons.GetBandPersonByParamAsync(personId, bandId);
            if (entry is null) throw new NoRelationException("That person is not associated with the band");

            await uow.BandPersons.RemoveBandPersonAsync(entry);
            await uow.SaveChangesAsync();
            return mapper.Map<BandWithMembersDTO>(band);
        }

        public async Task<BandMinimumInfoDTO> UpdateBandAsync(int id, ManipulateBandDTO updatedBand)
        {
            Country? country =await uow.Countries.GetCountryWithDetailsByNameAsync(updatedBand.CounrtyName);
            if(country is null && updatedBand.CounrtyName != null)
            {
                country=new Country() { Name= updatedBand.CounrtyName };
                await uow.Countries.InsertAsync(country);
            }

            Band? band =await uow.Bands.GetBandWithDetailsAsync(id);
            if (band is null) throw new EntityNotExistant("Band does not exist");

            mapper.Map(updatedBand, band);
            band.Country = country!;

            await uow.Bands.UpdateAsync(band);
            await uow.SaveChangesAsync();
            return mapper.Map<BandMinimumInfoDTO>(band);
        }

        public async Task<BandWithMembersDTO> GetBandByNameAsync(string name)
        {
            Band? band =await uow.Bands.GetBandWithDetailsByNameAsync(name);
            if (band is null) throw new EntityNotExistant("Band with that name is a figment of you imagination");
            return mapper.Map<BandWithMembersDTO>(band);
        }

        public async Task<ICollection<BandMinimumInfoDTO>> GetActiveBandsAsync()
        {
            var bands = await uow.Bands.GetActiveBandsAsync();
            return mapper.Map<ICollection<BandMinimumInfoDTO>>(bands);
        }

        public async Task<ICollection<BandMinimumInfoDTO>> GetBandsForPersonByPersonId(int personId)
        {
            ICollection<BandPerson> bandPersons = await uow.BandPersons.GetBandPersonByPersonId(personId);
            if (bandPersons is null) throw new EntityNotExistant($"Band person with personID [{personId}] was not found");

            return mapper.Map<ICollection<BandMinimumInfoDTO>>(bandPersons);
        }

        public async Task<BandDTO> AddThemeAsync(int bandId, int themeId)
        {
            var band = await uow.Bands.GetBandWithDetailsAsync(bandId);
            if (band is null) throw new EntityNotExistant($"Band with id {bandId} was not found");
            var theme = await uow.Themes.GetThemeWithDetailsByIdAsync(themeId);
            if (theme is null) throw new EntityNotExistant($"Theme with id {themeId} was not found");

            BandTheme? entry = band.BandTheme.FirstOrDefault(bt=>bt.ThemeId==themeId);
            if (entry is not null) throw new InvalidLogicException("The theme is already associated with the band");

            await uow.BandThemes.AddBandThemeAsync(new BandTheme { Band=band, Theme=theme});
            await uow.SaveChangesAsync();
            return mapper.Map<BandDTO>(band);
        }

        public async Task<BandDTO> RemoveThemeAsync(int bandId, int themeId)
        {
            var band = await uow.Bands.GetBandWithDetailsAsync(bandId);
            if (band is null) throw new EntityNotExistant($"Band with id {bandId} was not found");
            var theme = await uow.Themes.GetThemeWithDetailsByIdAsync(themeId);
            if (theme is null) throw new EntityNotExistant($"Theme with id {themeId} was not found");

            BandTheme? entry = band.BandTheme.FirstOrDefault(bt => bt.ThemeId == themeId);
            if (entry is  null) throw new InvalidLogicException("The theme is not associated with the band");

            await uow.BandThemes.RemoveBandThemeAsync(entry);
            await uow.SaveChangesAsync();
            return mapper.Map<BandDTO>(band);
        }
    }
}
