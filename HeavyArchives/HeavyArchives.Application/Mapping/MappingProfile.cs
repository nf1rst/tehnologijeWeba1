using AutoMapper;
using HeavyArchives.Application.DTOs.Album;
using HeavyArchives.Application.DTOs.Auth;
using HeavyArchives.Application.DTOs.Band;
using HeavyArchives.Application.DTOs.BandMemberStatus;
using HeavyArchives.Application.DTOs.Concert;
using HeavyArchives.Application.DTOs.Contact;
using HeavyArchives.Application.DTOs.Country;
using HeavyArchives.Application.DTOs.Genres;
using HeavyArchives.Application.DTOs.Label;
using HeavyArchives.Application.DTOs.Person;
using HeavyArchives.Application.DTOs.Theme;
using HeavyArchives.Application.DTOs.Tour;
using HeavyArchives.Domain.Entities;

namespace HeavyArchives.Application.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            //sve propertyje ce s istim imenom ce automatski mapirat, ja rucno moram napravit ove koji se zovu drugacije
            CreateMap<Person, PersonDTO>()
                .ForMember(
                dest => dest.PlaceOfBirth, opt => opt.MapFrom(
                    src => src.PlaceOfBirth!=null?
                        src.PlaceOfBirth.Name
                        :"Unknown"
                    ));

            CreateMap<Person, PersonFewDetailsDTO>()
                .ForMember
                (dest => dest.Role, opt => opt.MapFrom(
                    src => src.BandPerson.Select(bp => bp.RoleInBand)
                    .FirstOrDefault() // zato sto DTO ocekuje jedan string, a ovaj selectg vraca IEnumerable pa bude cudni rezultat
                    ));
            
            //insertza insert u bazu 
            //ignorira plceofbirthID jer person prima cijeli country objekt ne id
            CreateMap<ManipulatePersonDTO, Person>()
                .ForMember(dest => dest.PlaceOfBirth, opt => opt.Ignore())
                .ForMember(dest => dest.BandPerson, opt => opt.Ignore());

            CreateMap<Person, PersonWithBandsDTO>()
                .ForMember(dest => dest.Bands, opt => opt.MapFrom(
                    src => src.BandPerson.Select(bp => bp.Band.Name)));

            CreateMap<Album, AlbumDTO>()
                .ForMember(
                dest => dest.BandName, opt => opt.MapFrom(
                src => src.Band!=null?
                src.Band.Name:"Unknown"));

            CreateMap<UpdateAlbuumDTO, Album>()
                .ForMember(dest => dest.Band, opt => opt.Ignore());

            CreateMap<CreateAlbumDTO, Album>()
                .ForMember(dest => dest.Band, opt => opt.Ignore());

            CreateMap<Band, BandDTO>()
                .ForMember(
                    dest => dest.CoutnryName, opt => opt.MapFrom(
                    src => src.Country != null ? src.Country.Name : "Unknown"))
                .ForMember(
                dest => dest.Genres, opt => opt.MapFrom(
                src => src.GenreBand.Select(gb => gb.Genre)));

            CreateMap<Band, BandMinimumInfoDTO>()
                .ForMember(
                    dest=>dest.CoutnryName, opt=>opt.MapFrom(
                    src=>src.Country.Name));

            CreateMap<ManipulateBandDTO, Band>()
                .ForMember(dest => dest.Country, opt => opt.Ignore())
                .ForMember(dest => dest.Albums, opt => opt.Ignore())
                .ForMember(dest => dest.BandTheme, opt => opt.Ignore())
                .ForMember(dest => dest.BandLabel, opt => opt.Ignore())
                .ForMember(dest => dest.BandPerson, opt => opt.Ignore())
                .ForMember(dest => dest.GenreBand, opt => opt.Ignore());

            CreateMap<Band, BandWithMembersDTO>()
                .ForMember(
                    dest => dest.CoutnryName, opt => opt.MapFrom(
                    src => src.Country!=null?
                    src.Country.Name
                    :"Unknown"))
                .ForMember(
                    dest => dest.CurrentMembers, opt => opt.MapFrom(
                    src => src.BandPerson
                        .Where(bp=>bp.Status!=null && bp.Status.Name!="Inactive")
                        .Select(bp=>bp.Person))
                )
                .ForMember(
                    dest=>dest.PastMembers, opt =>opt.MapFrom(
                    src=>src.BandPerson
                        .Where(bp=>bp.Status!=null && bp.Status.Name=="Inactive")
                        .Select(bp=>bp.Person))
                )
                .ForMember(
                    dest=>dest.Genres, opt=>opt.MapFrom(
                    src=>src.GenreBand.Select(gb=>gb.Genre)))
                .ForMember(
                    dest=>dest.Contact, opt=>opt.MapFrom(
                    src=>src.Contact));//da ovo zna mora dolje postojat Contact->contactDTO

            CreateMap<Band, BandWithRolesForPersonDTO>()
                .ForMember(
                dest => dest.Band, opt => opt.MapFrom(
                    src => src.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(
                    src => src.BandPerson.Select(bp => bp.Status.Name))) //ovo treba provjerit
                .ForMember(
                dest => dest.Role, opt => opt.MapFrom(
                    src => src.BandPerson.Select(bp => bp.RoleInBand)));

            CreateMap<Label, LabelDTO>()
                .ForMember(dest => dest.Themes, opt => opt.MapFrom(
                        src => src.LabelTheme.Select(lt => lt.Theme)))
                .ForMember(dest => dest.Bands, opt => opt.MapFrom(
                    src => src.BandLabel.Select(bl => bl.Band)));

            CreateMap<Label, JustLabelDTO>();

            CreateMap<Label, LabelWithThemesDTO>()
                .ForMember(dest=>dest.Themes, opt=>opt.MapFrom(
                    src=>src.LabelTheme.Select(lt=>lt.Theme)));

            CreateMap<ManipulateLabelDTO, Label>()
                .ForMember(dest => dest.LabelTheme, opt => opt.Ignore())
                .ForMember(dest => dest.BandLabel, opt => opt.Ignore());

            CreateMap<Label, LabelWithMembersDTO>()
                .ForMember(
                dest => dest.Bands, opt => opt.MapFrom(
                    src => src.BandLabel.Select(bl => bl.Band)));

            CreateMap<ManipulateGenreDTO, Genre>();

            CreateMap<Genre, GenreDTO>();

            CreateMap<Theme, ThemeDTO>();
            CreateMap<Theme, MinimalThemeDTO>();
            CreateMap<ManipulateThemeDTO, Theme>()
                .ForMember(dest=>dest.BandTheme, opt=>opt.Ignore())
                .ForMember(dest=>dest.LabelTheme, opt=>opt.Ignore());



            //contact fali, i nesto za coutnry dodat i update


            CreateMap<Country, CountryDTO>(); // sve se isto zove pa ne treba nista

            CreateMap<ManipulateCountryDTO, Country>();

            CreateMap<Contact, ContactDTO>()
                .ForMember(
                dest=>dest.Band, opt=>opt.MapFrom(
                    src=>src.Band.Name));

            CreateMap<ManipulateContactDTO, Contact>();

            CreateMap<UpdateContactDTO, Contact>();

            CreateMap<User, UserDTO>();


            CreateMap<Concert, ConcertDTO>()
                .ForMember(
                dest => dest.City, opt => opt.MapFrom(
                    src => src.City.Name))
                .ForMember(
                dest => dest.PartOfTour, opt => opt.MapFrom(
                    src => src.Tour.Name));

            CreateMap<ManipulateConcertDTO, Concert>()
                .ForMember(dest => dest.City, opt => opt.Ignore())
                .ForMember(dest => dest.Tour, opt => opt.Ignore())
                .ForMember(dest => dest.City, opt => opt.Ignore());

            CreateMap<ManipulateBandMemberStatusDTO, BandMemberStatus>()
                .ForMember(
                dest => dest.Name, opt => opt.MapFrom(
                    src => src.Status))
                .ForMember(
                dest => dest.BandPerson, opt => opt.Ignore());

            CreateMap<BandMemberStatus, BandMemberStatusDTO>()
                .ForMember(
                dest => dest.Status, opt => opt.MapFrom(
                    src => src.Name));


            CreateMap<Tour, TourDTO>()
                .ForMember(dest => dest.HeadlinerBand, opt => opt.MapFrom(src=>src.TourBand.FirstOrDefault(tb=>tb.IsHeadliner==true).Band.Name))//rucno
                .ForMember(dest=>dest.SupportBands, opt=>opt.MapFrom(src=>src.TourBand.Where(tb=>tb.IsHeadliner==false).Select(tb=>tb.Band.Name))) // rucno
                .ForMember(dest => dest.TourManger, opt => opt.MapFrom(src => src.TourManager.Fname + " " + src.TourManager.Lname));

            CreateMap<ManipulateTourDTO, Tour>()
                .ForMember(dest => dest.TourBand, opt => opt.Ignore()) //rucno
                .ForMember(dest => dest.TourManager, opt => opt.Ignore())
                .ForMember(dest => dest.Concerts, opt => opt.Ignore()); //headeliner i tourManager rucno



        }

    }
}
