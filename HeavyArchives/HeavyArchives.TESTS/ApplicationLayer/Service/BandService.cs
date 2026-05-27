using HeavyArchives.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Tests.ApplicationLayer.Service
{
    public class BandService
    {
        [Fact]
        public void AddPersonToBand_ShouldThrowIfPersonDeadAndActive()
        {
            var mockRepo = new Mock<IBandRepository>();
            

        }
    }
}
