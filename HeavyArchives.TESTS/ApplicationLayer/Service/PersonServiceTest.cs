using AutoMapper;
using Exceptions;
using HeavyArchives.Application.DTOs.Person;
using HeavyArchives.Application.Services;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncyclopaediaBisemutum.Tests.ApplicationLayer.Service
{
    public class PersonServiceTest
    {
        [Fact]
        public async Task CreatePerson_ShouldThrowInvalidLogicExceptionIfDateOfDeathBeforeDateOfBirth()
        {
            var mockUow = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();

            ManipulatePersonDTO person = new ManipulatePersonDTO()
            {
                DateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow),
                DateOfDeath = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-5))
            };

            var personService = new PersonService(mockUow.Object, mockMapper.Object);

            await Assert.ThrowsAsync<InvalidLogicException>(async() =>
            {
               await personService.CreatePersonAsync(person);
            });
        }
    }
}
