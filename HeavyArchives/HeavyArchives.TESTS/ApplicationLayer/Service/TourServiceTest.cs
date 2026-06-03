using AutoMapper;
using Exceptions;
using HeavyArchives.Application.DTOs.Tour;
using HeavyArchives.Application.Services;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
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
            ManipulateTourDTO tour = new ManipulateTourDTO()
            {
                TourManagerID = 1,
                HeadlinerBand = "charcarodontosaurus"
            };

            var experimentPerson = new Person() { Id=1,Fname = "Eric", Lname = "Idle" };
            var experimentBand = new Band() {Id=2 ,Name = "charcarodontosaurus", Status = "Inactive" };

            mockUow.Setup(u => u.Persons).Returns(mockPersonRepo.Object);
            mockUow.Setup(u=>u.Bands).Returns(mockBandRepo.Object);

            mockPersonRepo.Setup(r => r.GetByIdAsync(tour.TourManagerID)).ReturnsAsync(experimentPerson);
            mockBandRepo.Setup(r => r.GetBandWithDetailsByNameAsync(tour.HeadlinerBand)).ReturnsAsync(experimentBand);

            var tourService = new TourService(mockUow.Object, mockMapper.Object);

            var exception =await Assert.ThrowsAsync<InvalidLogicException>(async ()=>
            await tourService.CreateTourAsync(tour)
            );

            mockUow.Verify(u => u.SaveChangesAsync(), Times.Never); // provjera jesu li se saveali podaci ako je krivo bilo 
        }

        [Fact]
        public async Task CreateTour_ShouldThrowInvalidLogicExceptionIfEndBeforeStart()
        {
            var mockUow = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();

            ManipulateTourDTO experimentTour = new ManipulateTourDTO()
            {
                StartingDate = DateOnly.FromDateTime(DateTime.Now.AddDays(3)),
                FinishingDate = DateOnly.FromDateTime(DateTime.Now)
            };

            var tourService = new TourService(mockUow.Object, mockMapper.Object);

            await Assert.ThrowsAsync<InvalidLogicException>(async () =>
            {
                await tourService.CreateTourAsync(experimentTour);
            });
           
        }
    }
}
