using AutoMapper;
using CloudSuite.Modules.Application.Handlers.City;
using CloudSuite.Modules.Application.Handlers.TomadorServico;
using CloudSuite.Modules.Application.Services.Implementation;
using CloudSuite.Modules.Application.ViewModels;
using CloudSuite.Modules.Domain.Contracts;
using CloudSuite.Modules.Domain.Models;
using Moq;
using NetDevPack.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CloudSuite.Modules.Application.Tests.Services
{
    public class CityAppServiceTests
    {
        [Theory]
        [InlineData("Maceio")]
        [InlineData("Salvador")]
        [InlineData("Rio de janeiro")]
        public async Task GetByCityName_ShouldReturnsCompanyViewModel(string cityName)
        {
            var cityRepositoryMock = new Mock<ICityRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var countryAppService = new CityAppService(
                cityRepositoryMock.Object,
                mediatorHandlerMock.Object,
                mapperMock.Object);

            var state = new State(Guid.NewGuid(), "Ba", "Bahia", new Country("Brazil", "736872364", true, false, true, false, true), Guid.NewGuid());
            var cityEntity = new City(state.Id, cityName, state);
            cityRepositoryMock.Setup(repo => repo.GetByCityName(cityName)).ReturnsAsync(cityEntity);

            var expectedViewModel = new CityViewModel()
            {
                Id = cityEntity.Id,
                CityName = cityName
            };

            mapperMock.Setup(mapper => mapper.Map<CityViewModel>(cityEntity)).Returns(expectedViewModel);

            // Act
            var result = await countryAppService.GetByCityName(cityName);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        [Theory]
        [InlineData("Berlim")]
        [InlineData("Cidade do Mexico")]
        [InlineData("Pequim")]
        public async Task GetByCityName_ShouldHandleNullRepositoryResult(string cityName)
        {
            // Arrange
            var cityRepositoryMock = new Mock<ICityRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var countryAppService = new CityAppService(
                cityRepositoryMock.Object,
                mediatorHandlerMock.Object,
                mapperMock.Object);

            cityRepositoryMock.Setup(repo => repo.GetByCityName(It.IsAny<string>()))
                .ReturnsAsync((City)null); // Simulate null result from the repository

            // Act
            var result = await countryAppService.GetByCityName(cityName);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("Santiago")]
        [InlineData("Montivideu")]
        [InlineData("Assunção")]
        public async Task GetByCityName_ShouldHandleInvalidMappingResult(string cityName)
        {
            //Arrange
            var cityRepositoryMock = new Mock<ICityRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var countryAppService = new CityAppService(
                cityRepositoryMock.Object,
                mediatorHandlerMock.Object,
                mapperMock.Object);

            cityRepositoryMock.Setup(repo => repo.GetByCityName(It.IsAny<string>()))
                .ThrowsAsync(new ArgumentException("Invalid data")); // Simulate null result from the repository

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => countryAppService.GetByCityName(cityName));
        }

        [Theory]
        [InlineData("Niteroi")]
        [InlineData("Vitoria")]
        [InlineData("Manaus")]
        public async Task Save_ShouldReturnsCompanyViewModel(string cityName)
        {
            var cityRepositoryMock = new Mock<ICityRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var countryAppService = new CityAppService(
                cityRepositoryMock.Object,
                mediatorHandlerMock.Object,
                mapperMock.Object);

            var createCommand = new CreateCityCommand()
            {
                CityName = cityName
            };

            // Act
            await countryAppService.Save(createCommand);

            // Assert
            cityRepositoryMock.Verify(repo => repo.Add(It.IsAny<City>()), Times.Once);
        }

        [Theory]
        [InlineData("Cuiba")]
        [InlineData("Curitiba")]
        [InlineData("Florianopolis")]
        public async Task Save_ShouldHandleNullRepositoryResult(string cityName)
        {
            var cityRepositoryMock = new Mock<ICityRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var countryAppService = new CityAppService(
                cityRepositoryMock.Object,
                mediatorHandlerMock.Object,
                mapperMock.Object);

            var createCommand = new CreateCityCommand()
            {
                CityName = cityName
            };

            cityRepositoryMock.Setup(repo => repo.Add(It.IsAny<City>())).Throws(new NullReferenceException());

            // Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => countryAppService.Save(createCommand));
        }

        [Theory]
        [InlineData("Belem")]
        [InlineData("Recife")]
        [InlineData("João Pessoa")]
        public async Task Save_ShouldHandleInvalidMappingResult(string cityName)
        {
            // Arrange
            var cityRepositoryMock = new Mock<ICityRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var countryAppService = new CityAppService(
                cityRepositoryMock.Object,
                mediatorHandlerMock.Object,
                mapperMock.Object);

            var createCommand = new CreateCityCommand()
            {
                CityName = cityName
            };

            // Act       
            cityRepositoryMock.Setup(repo => repo.Add(It.IsAny<City>()))
            .Throws(new ArgumentException("Invalid data"));

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(() => countryAppService.Save(createCommand));
        }
    }
}
