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
        public BandWithMembersDTO AddGenre(int bandId, int genreId)
        {
            var band = uow.Bands.GetBandWithDetails(bandId);
            if (band is null) throw new EntityNotExistant("Band wasn't found");
            var genre = uow.Genres.GetGenreWithDetails(genreId);
            if (genre is null) throw new EntityNotExistant("Genre wasn't found");

            if (band.GenreBand.Any(gb => gb.GenreId == genreId)) throw new Exceptions.RelationExistsException("That genre is already attibuted to the band"); //ako zanr vec postoji da podupla

            band.GenreBand.Add(new GenreBand() { Genre=genre, Band=band});
            uow.SaveChanges();
            return mapper.Map<BandWithMembersDTO>(band);
        }

        public BandWithMembersDTO AddMember(int bandId, int personId, string status, string role)
        {
            var band = uow.Bands.GetBandWithDetails(bandId);
            if (band is null) throw new EntityNotExistant("Band wasn't found");
            var person = uow.Persons.GetPersonWithDetails(personId);
            if (person is null) throw new EntityNotExistant("Person wasn't found");

            BandMemberStatus? statusFound = uow.BandMemberStatuses.GetStatusWithDetailsByName(status);
            if (statusFound is null) throw new EntityNotExistant("Status you've put doesn't exist");

            //da bi ovdje mogao usporedivat mora se u entitetu inicijalizirat svaka lista 
            if (band.BandPerson.Any(bp => bp.PersonId == personId)) throw new Exceptions.RelationExistsException("That band member is already associated with the band"); // ako vec postoji bend member nemoj ga dodat 2 put 

            band.BandPerson.Add(new BandPerson { PersonId = personId, BandId = bandId, Status=statusFound, RoleInBand=role }); // ovaj status dio treba fact checkat

            uow.SaveChanges();
            return mapper.Map<BandWithMembersDTO>(band);
        }

        public BandDTO CreateBand(ManipulateBandDTO createdBandDTO)
        {
            Country? country = uow.Countries.GetCountryWithDetailsByName(createdBandDTO.CounrtyName);
            if(country is null && createdBandDTO.CounrtyName != null)
            {
                country=new Country() { Name=createdBandDTO.CounrtyName };
                uow.Countries.Insert(country);
            }
            Band band = mapper.Map<Band>(createdBandDTO);

            band.Country = country; //jer mapper ignorira

            uow.Bands.Insert(band);
            uow.SaveChanges();
            return mapper.Map<BandDTO>(band);
        }

        public void DeleteBand(int id)
        {
            var band = uow.Bands.GetById(id);
            if (band is null) throw new Exceptions.EntityNotExistant("Band wasn't found");

            uow.Bands.Delete(band);
            uow.SaveChanges();
        }

        public ICollection<BandMinimumInfoDTO> GetAllBands()
        {
            var bands = uow.Bands.GetAllBands();
            return mapper.Map<ICollection<BandMinimumInfoDTO>>(bands);
        }

        public ICollection<BandMinimumInfoDTO> GetBandsByCountry(string countryName)
        {
            var bands= uow.Bands.GetBandByCountry(countryName);
            return mapper.Map<ICollection<BandMinimumInfoDTO>>(bands);
        }

        public ICollection<BandMinimumInfoDTO> GetBandsByGenre(string genreName)
        {
            var bands= uow.Bands.GetBandsByGenre(genreName);
            return mapper.Map<ICollection<BandMinimumInfoDTO>>(bands);
        }

        public ICollection<BandMinimumInfoDTO> GetBandsDetailsWithMembers()
        {
            var bands = uow.Bands.GetBandsWithDetails();
            return mapper.Map<ICollection<BandMinimumInfoDTO>>(bands);
        }

        public BandWithMembersDTO GetBandById(int id)
        {
            Band? b= uow.Bands.GetBandWithDetails(id);
            if (b is null) throw new EntityNotExistant("Band wasn't found"); 
            return mapper.Map<BandWithMembersDTO>(b);
        }

        public BandWithMembersDTO RemoveGenre(int bandId, int genreId)
        {               
            var band = uow.Bands.GetBandWithDetails(bandId);
            if (band is null) throw new EntityNotExistant("Band wasn't found");

            var genre = uow.Genres.GetGenreWithDetails(genreId);
            if (genre is null) throw new EntityNotExistant("Genre wasn't found");

            var entry = band.GenreBand.FirstOrDefault(b => b.GenreId == genreId);
            if (entry is null) throw new Exceptions.NoRelationException("That genre is not assiciated with the band");// ne postoji taj genre za odredeni bend 

            band.GenreBand.Remove(entry);
            uow.SaveChanges();
            return mapper.Map<BandWithMembersDTO>(band);
        }

        public BandWithMembersDTO RemoveMember(int bandId, int personId)
        { 
            var band = uow.Bands.GetBandWithDetails(bandId);
            if (band is null) throw new EntityNotExistant("Band wasn't found");

            var person = uow.Persons.GetPersonWithDetails(personId);
            if (person is null) throw new EntityNotExistant("Person wasn't found");

            var entry = band.BandPerson.FirstOrDefault(b=>b.PersonId==personId);
            if (entry is null) throw new NoRelationException("That person is not associated with the band");

            band.BandPerson.Remove(entry);
            uow.SaveChanges();
            return mapper.Map<BandWithMembersDTO>(band);
        }

        public BandMinimumInfoDTO UpdateBand(int id, ManipulateBandDTO updatedBand)
        {
            Country? country = uow.Countries.GetCountryWithDetailsByName(updatedBand.CounrtyName);
            if(country is null && updatedBand.CounrtyName != null)
            {
                country=new Country() { Name= updatedBand.CounrtyName };
                uow.Countries.Insert(country);
            }

            Band? band = uow.Bands.GetBandWithDetails(id);
            if (band is null) throw new EntityNotExistant("Band does not exist");

            mapper.Map(updatedBand, band);
            band.Country = country;

            uow.Bands.Update(band);
            uow.SaveChanges();
            return mapper.Map<BandMinimumInfoDTO>(band);
        }

        public BandWithMembersDTO GetBandByName(string name)
        {
            Band? band = uow.Bands.GetBandWithDetailsByName(name);
            return mapper.Map<BandWithMembersDTO>(band);
        }

        public ICollection<BandMinimumInfoDTO> GetActiveBands()
        {
            var bands = uow.Bands.GetActiveBands();
            return mapper.Map<ICollection<BandMinimumInfoDTO>>(bands);
        }
    }
}
