using AutoMapper;
using Exceptions;
using HeavyArchives.Application.Interfaces;
using HeavyArchives.Application.Services;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Entities.M2MTables;
using HeavyArchives.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Tests.ApplicationLayer.Service
{
    public class BandServiceTest
    {
        [Fact]
        public async Task AddPersonToBand_ShouldThrowIfPersonDeadAndActive()
        {
            var mockRepo = new Mock<IBandRepository>();
            var mockUow = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();

            Person deadPerson = new Person()
            {
                Id = 1,
                DateOfDeath = DateOnly.FromDateTime(DateTime.UtcNow) //samo da nije null
            };

            Band band = new Band()
            {
                Id = 1,
                Name = "Cactus",
            };

            BandMemberStatus bandMemberStatus = new BandMemberStatus()
            {
                Id=1,
                Name="Active"
            };

            mockUow.Setup(u => u.Persons.GetPersonWithDetailsAsync(1)).ReturnsAsync(deadPerson);
            mockUow.Setup(u => u.Bands.GetBandWithDetailsAsync(1)).ReturnsAsync(band);
            mockUow.Setup(u => u.BandMemberStatuses.GetStatusWithDetailsByNameAsync("Active")).ReturnsAsync(bandMemberStatus);

            //mapper napisi
            BandService bandService = new BandService(mockUow.Object, mockMapper.Object);

            await Assert.ThrowsAsync<InvalidLogicException>(async () =>
            {
                await bandService.AddMemberAsync(band.Id, deadPerson.Id, "Active", "guitar");
            });


        }
    }
}
