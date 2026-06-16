using AutoMapper;
using Exceptions;
using HeavyArchives.Application.DTOs.Concert;
using HeavyArchives.Application.Services;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Interfaces;
using Moq;

namespace HeavyArchives.Tests.ApplicationLayer.Service
{
    public class ConcertServiceTest
    {
        [Fact]
        public void SellTicket_ShouldThrowLimitReachedExceptionIfSoldTicketsGreaterThanCapacityOfVenue()
        {
            var mockUow = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();

            Concert experimentConcert = new Concert()
            {
                Id=1,
                Capacity = 10,
                TicketsSold = 11
            };
            ConcertDTO expConcertDTO = new ConcertDTO()
            {
                Id = experimentConcert.Id,
                Capacity = experimentConcert.Capacity,
                TicketsSold=experimentConcert.TicketsSold
            };
            mockUow.Setup(u => u.Concerts.GetByIdAsync(experimentConcert.Id)).ReturnsAsync(experimentConcert);

            mockMapper.Setup(m => m.Map<ConcertDTO>(experimentConcert)).Returns(expConcertDTO);

            ConcertService concertService = new ConcertService(mockUow.Object, mockMapper.Object);

            Assert.ThrowsAsync<LimitReachedException>(async () =>
            {
                await concertService.SellTicketAsync(experimentConcert.Id);
            });

        }
    }
}
