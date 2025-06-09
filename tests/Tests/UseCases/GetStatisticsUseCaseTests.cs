namespace Tests.UseCases
{
    using Application.DTOs;
    using Application.Exceptions;
    using Application.UseCases;
    using Domain.Abstractions;
    using Domain.DTOs;
    using Domain.Entities;
    using Domain.Interfaces;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class GetStatisticsUseCaseTests
    {
        private Mock<IUnitOfWork> unitOfWorkMock = null!;
        private GetStatisticsUseCase useCase = null!;

        [SetUp]
        public void Setup()
        {
            this.unitOfWorkMock = new Mock<IUnitOfWork>();
            this.unitOfWorkMock.Setup(x => x.IpInfoRepository).Returns(() => new Mock<IIpInfoRepository>().Object);
            this.useCase = new GetStatisticsUseCase(this.unitOfWorkMock.Object);
        }

        [Test]
        public async Task ExecuteAsync_ReturnsExpectedStatistics()
        {
            // Arrange
            /*var countryA = new Country { Id = 1, Name = "Argentina", ISOCode = "AR", DistanceToBuenosAiresInKm = 1000 };
            var countryB = new Country { Id = 2, Name = "España", ISOCode = "ES", DistanceToBuenosAiresInKm = 8000 };

            var queries = new List<IpInfo>
            {
                new () { IP = "1.1.1.1", Country = countryA, RequestDate = DateTime.UtcNow },
                new () { IP = "2.2.2.2", Country = countryA, RequestDate = DateTime.UtcNow },
                new () { IP = "3.3.3.3", Country = countryB, RequestDate = DateTime.UtcNow },
            };*/

            var dbStats = new StatisticsDTO
            {
                AverageDistanceInvocations = 2980,
                /*MaxCountryCode = max.ISOCode,
                MaxCountryName = max.Name,
                MaxDistanceToBuenosAiresKm = max.DistanceKm,
                MaxInvocationTimes = max.InvocationTimes,
                MinCountryCode = min.ISOCode,
                MinCountryName = min.Name,
                MinDistanceToBuenosAiresKm = min.DistanceKm,
                MinInvocationTimes = min.InvocationTimes,*/
            };

            this.unitOfWorkMock
                .Setup(r => r.IpInfoRepository.GetStatisticsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(dbStats);

            // Act
            var result = await this.useCase.ExecuteAsync(CancellationToken.None);

            // Assert
            Assert.That(result.MinDistanceCountry.DistanceToBuenosAiresKm, Is.EqualTo(0));
            Assert.That(result.MinDistanceCountry.CountryName, Is.EqualTo("Argentina (AR)"));
            Assert.That(result.MinDistanceCountry.InvocationTimes, Is.EqualTo(2));
            Assert.That(result.MaxDistanceCountry.DistanceToBuenosAiresKm, Is.EqualTo(10000));
            Assert.That(result.MaxDistanceCountry.CountryName, Is.EqualTo("España (ES)"));
            Assert.That(result.MaxDistanceCountry.InvocationTimes, Is.EqualTo(2));
            Assert.That(result.AverageDistanceInvocations, Is.EqualTo(2980));
        }

        [Test]
        public void ExecuteAsync_ThrowsException_WhenNoData()
        {
            this.unitOfWorkMock
                .Setup(r => r.IpInfoRepository.GetStatisticsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new StatisticsDTO());

            Assert.ThrowsAsync<StatisticsNotAvailableException>(() => this.useCase.ExecuteAsync(CancellationToken.None));
        }
    }

}
