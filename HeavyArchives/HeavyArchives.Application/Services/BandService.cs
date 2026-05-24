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

            band.GenreBand.Add(new GenreBand() { Genre=genre, Band=band});
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

            //da bi ovdje mogao usporedivat mora se u entitetu inicijalizirat svaka lista 
            if (band.BandPerson.Any(bp => bp.PersonId == personId)) throw new Exceptions.RelationExistsException("That band member is already associated with the band"); // ako vec postoji bend member nemoj ga dodat 2 put 

            band.BandPerson.Add(new BandPerson { PersonId = personId, BandId = bandId, Status=statusFound, RoleInBand=role }); // ovaj status dio treba fact checkat

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

            band.Country = country; //jer mapper ignorira

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

            var genre =await uow.Genres.GetGenreWithDetailsAsync(genreId);
            if (genre is null) throw new EntityNotExistant("Genre wasn't found");

            var entry = band.GenreBand.FirstOrDefault(b => b.GenreId == genreId);
            if (entry is null) throw new Exceptions.NoRelationException("That genre is not assiciated with the band");// ne postoji taj genre za odredeni bend 

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

            var entry = band.BandPerson.FirstOrDefault(b=>b.PersonId==personId);
            if (entry is null) throw new NoRelationException("That person is not associated with the band");

            band.BandPerson.Remove(entry);
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
            band.Country = country;

            await uow.Bands.UpdateAsync(band);
            await uow.SaveChangesAsync();
            return mapper.Map<BandMinimumInfoDTO>(band);
        }

        public async Task<BandWithMembersDTO> GetBandByNameAsync(string name)
        {
            Band? band =await uow.Bands.GetBandWithDetailsByNameAsync(name);
            return mapper.Map<BandWithMembersDTO>(band);
        }

        public async Task<ICollection<BandMinimumInfoDTO>> GetActiveBandsAsync()
        {
            var bands = await uow.Bands.GetActiveBandsAsync();
            return mapper.Map<ICollection<BandMinimumInfoDTO>>(bands);
        }
    }
}
