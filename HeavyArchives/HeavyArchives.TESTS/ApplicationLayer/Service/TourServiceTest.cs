using AutoMapper;
using Exceptions;
using HeavyArchives.Application.DTOs.Concert;
using HeavyArchives.Application.DTOs.Tour;
using HeavyArchives.Application.Services;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Interfaces;
using Moq;
using System.Threading.Tasks;

namespace HeavyArchives.Tests.ApplicationLayer.Service
{
    public class TourServiceTest
    {
        [Fact]
        public async Task CreateTour_ShouldThrowInvalidExceptionIfBandInactive()
        {
            //arrange, act, assert
            var mockUow = new Mock<IUnitOfWork>();
            var mockPersonRepo = new Mock<IPersonRepository>();
            var mockBandRepo = new Mock<IBandRepository>();
            var mockMapper = new Mock<IMapper>();

            //ne smije zvrsit prije nego je poceo  - OK
            //ne smije headliner bit band koji nije Active - OK 
            //ne smije se desit da su koncerti na isti dan u razlicitim cities

            var experimentPerson = new Person() { Id = 1, Fname = "Eric", Lname = "Idle" };
            var experimentBand = new Band() { Id = 2, Name = "charcarodontosaurus", Status = "Inactive" };

            ManipulateTourDTO tour = new ManipulateTourDTO()
            {
                TourManagerID = 1,
                HeadlinerBand = experimentBand.Name
            };


            mockUow.Setup(u => u.Persons).Returns(mockPersonRepo.Object);
            mockUow.Setup(u=>u.Bands).Returns(mockBandRepo.Object);

           mockPersonRepo.Setup(r => r.GetByIdAsync(tour.TourManagerID)).ReturnsAsync(experimentPerson);
            mockBandRepo.Setup(r => r.GetBandWithDetailsByNameAsync(tour.HeadlinerBand)).ReturnsAsync(experimentBand);

            mockMapper.Setup(m => m.Map<Tour>(It.IsAny<ManipulateTourDTO>())).Returns(new Tour());
            // bez ovoga ne radi jer se stalno vraca null 

            var tourService = new TourService(mockUow.Object, mockMapper.Object);

            await Assert.ThrowsAsync<InvalidLogicException>(async ()=>
            await tourService.CreateTourAsync(tour)
            );

        }

        [Fact]
        public async Task CreateTour_ShouldThrowInvalidLogicExceptionIfEndBeforeStart()
        {
            var mockUow = new Mock<IUnitOfWork>();
            var mockPersonRepo = new Mock<IPersonRepository>();
            var mockBandRepo = new Mock<IBandRepository>();
            var mockMapper = new Mock<IMapper>();

            ManipulateTourDTO experimentTour = new ManipulateTourDTO()
            {
                StartingDate = DateOnly.FromDateTime(DateTime.Now.AddDays(3)),
                FinishingDate = DateOnly.FromDateTime(DateTime.Now),
                TourManagerID=1,
                HeadlinerBand="gnjida"
            };

            mockPersonRepo.Setup(p => p.GetByIdAsync(experimentTour.TourManagerID)).ReturnsAsync(new Person());
            mockBandRepo.Setup(b => b.GetBandWithDetailsByNameAsync(experimentTour.HeadlinerBand)).ReturnsAsync(new Band() { Status = "Active" });

            mockUow.Setup(u=>u.Persons).Returns(mockPersonRepo.Object);
            mockUow.Setup(u=>u.Bands).Returns(mockBandRepo.Object);

            var tourService = new TourService(mockUow.Object, mockMapper.Object);

            Tour expectedTour = new Tour()
            {
                StartingDate = experimentTour.StartingDate,
                FinishingDate = experimentTour.FinishingDate,
            };

            mockMapper.Setup(m => m.Map<Tour>(experimentTour)).Returns(expectedTour);

            await Assert.ThrowsAsync<InvalidLogicException>(async () =>
            {
                await tourService.CreateTourAsync(experimentTour);
            });
           
        }

        [Fact]
        public void AddConcertAsync_ShouldThrowInvalidLogicExceptionIfConcertWithSameStartingTimeAlreadyAssociatedWithTour()
        {
            //if the concert daates are the same but the cities are different throw exception
            var mockUow = new Mock<IUnitOfWork>();
            var mockTourRepo = new Mock<ITourRepository>();
            var mockConcertRepo = new Mock<IConcertRepository>();
            var mockMapper = new Mock<IMapper>();

            mockUow.Setup(u => u.Concerts).Returns(mockConcertRepo.Object);
            mockUow.Setup(u => u.Tours).Returns(mockTourRepo.Object);

            Concert experimentConcertA = new Concert()
            {
                Id=1,
                StartingTime = DateTime.UtcNow,
            };


            Concert experimentConcertB = new Concert()
            {
                Id=2,
                StartingTime = DateTime.UtcNow,
            };

            Tour experimentTour = new Tour()
            {
                Id = 1,
                Concerts = new List<Concert> { experimentConcertB}
            };

            ConcertDTO concertDTOA = new ConcertDTO() { Id = experimentConcertA.Id, StartingTime = experimentConcertA.StartingTime };
            ConcertDTO concertDTOB = new ConcertDTO() { Id = experimentConcertB.Id, StartingTime = experimentConcertB.StartingTime };
            TourDTO tourDto = new TourDTO() { Concerts = new List<ConcertDTO> { concertDTOB } };

            mockMapper.Setup(m => m.Map<TourDTO>(experimentTour)).Returns(tourDto);

            mockTourRepo.Setup(t => t.GetTourWithDetailsByIdAsync(experimentTour.Id)).ReturnsAsync(experimentTour);
            mockConcertRepo.Setup(t => t.GetConcertWithDetailsByIdAsync(experimentConcertA.Id)).ReturnsAsync(experimentConcertA);
            mockConcertRepo.Setup(c => c.GetConcertsWithDetailsByTourAsync(experimentTour.Name)).ReturnsAsync(experimentTour.Concerts);

            TourService tourService = new TourService(mockUow.Object, mockMapper.Object);

            Assert.ThrowsAsync<InvalidLogicException>(async () =>
            {
               await tourService.AddConcertAsync(experimentTour.Id, experimentConcertA.Id);
            });

        }
    }
}
