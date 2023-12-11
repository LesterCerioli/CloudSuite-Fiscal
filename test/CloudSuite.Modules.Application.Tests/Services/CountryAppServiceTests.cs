using AutoMapper;
using CloudSuite.Modules.Application.Handlers.Country;
using CloudSuite.Modules.Application.Services.Implementation;
using CloudSuite.Modules.Application.ViewModels;
using CloudSuite.Modules.Domain.Contracts;
using CloudSuite.Modules.Domain.Models;
using Moq;
using NetDevPack.Mediator;
using NetDevPack.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CloudSuite.Modules.Application.Tests.Services
{
    public class CountryAppServiceTests
    {
        [Theory]
        [InlineData("Brazil", "BRA", true, true, true, true, true)]
        [InlineData("Argentina", "ARG", true, false, true, false, true)]
        [InlineData("Estados Unidos", "EUA", false, false, false, false, false)]
        public async Task GetCountryByCountryName_ShouldReturnsCompanyViewModel(string countryName, string code3, bool isBillingEnable, bool isCityEnabled, bool isShippingEnabled, bool isZipCodeEnable, bool isDistrictEnable)
        {
            var countryRepositoryMock = new Mock<ICountryRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var countryAppService = new CountryAppService(
                countryRepositoryMock.Object,
                mediatorHandlerMock.Object,
                mapperMock.Object);

            var countryEntity = new Country(countryName, code3, isBillingEnable, isShippingEnabled, isCityEnabled, isZipCodeEnable, isDistrictEnable);
            countryRepositoryMock.Setup(repo => repo.GetbyCountryName(countryName)).ReturnsAsync(countryEntity);

            var expectedViewModel = new CountryViewModel()
            {
                Id = countryEntity.Id,
                CountryName = countryName,
                Code = code3,
                IsBillingEnabled = isBillingEnable,
                IsShippingEnabled = isShippingEnabled,
                IsCityEnabled = isCityEnabled,
                IsZipCodeEnabled = isZipCodeEnable,
                IsDistrictEnabled = isDistrictEnable
            };

            mapperMock.Setup(mapper => mapper.Map<CountryViewModel>(countryEntity)).Returns(expectedViewModel);

            // Act
            var result = await countryAppService.GetbyCountryName(countryName);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        [Theory]
        [InlineData("Alemanha")]
        [InlineData("Mexico")]
        [InlineData("China")]
        public async Task GetCountryByCountryName_ShouldHandleNullRepositoryResult(string countryName)
        {
            // Arrange
            var countryRepositoryMock = new Mock<ICountryRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var countryAppService = new CountryAppService(
                countryRepositoryMock.Object,
                mediatorHandlerMock.Object,
                mapperMock.Object);

            countryRepositoryMock.Setup(repo => repo.GetbyCountryName(It.IsAny<string>()))
                .ReturnsAsync((Country)null); // Simulate null result from the repository

            // Act
            var result = await countryAppService.GetbyCountryName(countryName);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("Chile")]
        [InlineData("Uruguai")]
        [InlineData("Paraguai")]
        public async Task GetCountryByCountryName_ShouldHandleInvalidMappingResult(string countryName)
        {
           //Arrange
            var countryRepositoryMock = new Mock<ICountryRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var countryAppService = new CountryAppService(
                countryRepositoryMock.Object,
                mediatorHandlerMock.Object,
                mapperMock.Object);

            countryRepositoryMock.Setup(repo => repo.GetbyCountryName(It.IsAny<string>()))
                .ThrowsAsync(new ArgumentException("Invalid data")); // Simulate null result from the repository

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => countryAppService.GetbyCountryName(countryName));
        }

        [Theory]
        [InlineData("Irlanda", "IRL", true, true, true, true, true)]
        [InlineData("Japão", "JPA", true, false, true, false, true)]
        [InlineData("China", "CHN", false, false, false, false, false)]
        public async Task Save_ShouldAddCompanyToRepository(string countryName, string code3, bool isBillingEnable, bool isCityEnabled, bool isShippingEnabled, bool isZipCodeEnable, bool isDistrictEnable)
        {
            //Arrange
            var countryRepositoryMock = new Mock<ICountryRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var countryAppService = new CountryAppService(
                countryRepositoryMock.Object,
                mediatorHandlerMock.Object,
                mapperMock.Object);

            var createCompanyCommand = new CreateCountryCommand()
            {
                CountryName = countryName,
                Code3 = code3,
                IsBillingEnabled = isBillingEnable,
                IsShippingEnabled = isShippingEnabled,
                IsCityEnabled = isCityEnabled,
                IsZipCodeEnabled = isZipCodeEnable,
                IsDistrictEnabled = isDistrictEnable
            };

            // Act
            await countryAppService.Save(createCompanyCommand);

            // Assert
            countryRepositoryMock.Verify(repo => repo.Add(It.IsAny<Country>()), Times.Once);
        }

        [Theory]
        [InlineData("Irlanda", "IRL", true, true, true, true, true)]
        [InlineData("Japão", "JPA", true, false, true, false, true)]
        [InlineData("China", "CHN", false, false, false, false, false)]
        public async Task Save_ShouldHandleNullRepositoryResult(string countryName, string code3, bool isBillingEnable, bool isCityEnabled, bool isShippingEnabled, bool isZipCodeEnable, bool isDistrictEnable)
        {
            //Arrange
            var countryRepositoryMock = new Mock<ICountryRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var countryAppService = new CountryAppService(
                countryRepositoryMock.Object,
                mediatorHandlerMock.Object,
                mapperMock.Object);

            var createCompanyCommand = new CreateCountryCommand()
            {
                CountryName = countryName,
                Code3 = code3,
                IsBillingEnabled = isBillingEnable,
                IsShippingEnabled = isShippingEnabled,
                IsCityEnabled = isCityEnabled,
                IsZipCodeEnabled = isZipCodeEnable,
                IsDistrictEnabled = isDistrictEnable
            };

            countryRepositoryMock.Setup(repo => repo.Add(It.IsAny<Country>())).Throws(new NullReferenceException());

            // Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => countryAppService.Save(createCompanyCommand));

        }

        [Theory]
        [InlineData("Libano", "LIB", true, true, true, true, true)]
        [InlineData("Israel", "ISR", true, false, true, false, true)]
        [InlineData("França", "FRA", false, false, false, false, false)]
        public async Task Save_ShouldHandleInvalidMappingResult(string countryName, string code3, bool isBillingEnable, bool isCityEnabled, bool isShippingEnabled, bool isZipCodeEnable, bool isDistrictEnable)
        {

            //Arrange
            var countryRepositoryMock = new Mock<ICountryRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var countryAppService = new CountryAppService(
                countryRepositoryMock.Object,
                mediatorHandlerMock.Object,
                mapperMock.Object);

            var createCompanyCommand = new CreateCountryCommand()
            {
                CountryName = countryName,
                Code3 = code3,
                IsBillingEnabled = isBillingEnable,
                IsShippingEnabled = isShippingEnabled,
                IsCityEnabled = isCityEnabled,
                IsZipCodeEnabled = isZipCodeEnable,
                IsDistrictEnabled = isDistrictEnable
            };

            // Act       
            countryRepositoryMock.Setup(repo => repo.Add(It.IsAny<Country>()))
            .Throws(new ArgumentException("Invalid data"));

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(() => countryAppService.Save(createCompanyCommand));
        }

    }
}
