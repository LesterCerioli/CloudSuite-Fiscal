using AutoMapper;
using CloudSuite.Modules.Application.Handlers.Address;
using CloudSuite.Modules.Application.Handlers.City;
using CloudSuite.Modules.Application.Services.Implementation;
using CloudSuite.Modules.Application.ViewModels;
using CloudSuite.Modules.Common.ValueObjects;
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
    public class AddressAppServiceTests
    {
        [Theory]
        [InlineData("54.446.262/0001-03", "Tony Stark")]
        [InlineData("06.485.306/0001-61", "Jessica Jones")]
        [InlineData("99.802.891/0001-67", "Steve Rogers")]
        public async Task GetByAddressLine1_ShouldReturnsCompanyViewModel(string addressLine, string contactName)
        {
            var addressRepositoryMock = new Mock<IAddressRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var tomadorServicoAppService = new AddressAppService(
                addressRepositoryMock.Object,
                mediatorHandlerMock.Object,
                mapperMock.Object);

            var addressEntity = new Address(Guid.NewGuid(), new City(Guid.NewGuid(), "oudricandrai", new State(Guid.NewGuid(), "ba", "Bahia", new Country("Salvaodr", "123", true, true, true, false, false), Guid.NewGuid())), new District(), "John Doe", addressLine);

            addressRepositoryMock.Setup(repo => repo.GetByAddressLine1(addressLine)).ReturnsAsync(addressEntity);

            var expectedViewModel = new AddressViewModel()
            {
                Id = addressEntity.Id,
                ContactName = contactName,
                AddressLine = addressLine
            };

            mapperMock.Setup(mapper => mapper.Map<AddressViewModel>(addressEntity)).Returns(expectedViewModel);

            // Act
            var result = await tomadorServicoAppService.GetByAddressLine1(addressLine);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        [Theory]
        [InlineData("Soluções Inovadoras Ltda")]
        [InlineData("Tecnologia Avançada S.A")]
        [InlineData("Serviços Globais Eireli")]
        public async Task GetByAddressLine1_ShouldHandleNullRepositoryResult(string addressLine)
        {
            // Arrange
            var addressRepositoryMock = new Mock<IAddressRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var tomadorServicoAppService = new AddressAppService(
                addressRepositoryMock.Object,
                mediatorHandlerMock.Object,
                mapperMock.Object);

            addressRepositoryMock.Setup(repo => repo.GetByAddressLine1(It.IsAny<string>()))
                .ReturnsAsync((Address)null); // Simulate null result from the repository

            // Act
            var result = await tomadorServicoAppService.GetByAddressLine1(addressLine);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("Produtos Sustentáveis Eireli")]
        [InlineData("Desenvolvimento Rápido Ltda")]
        [InlineData("Integração Contínua S.A")]
        public async Task GetByAddressLine1_ShouldHandleInvalidMappingResult(string addressLine)
        {
            // Arrange
            var addressRepositoryMock = new Mock<IAddressRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var tomadorServicoAppService = new AddressAppService(
                addressRepositoryMock.Object,
                mediatorHandlerMock.Object,
                mapperMock.Object);

            addressRepositoryMock.Setup(repo => repo.GetByAddressLine1(It.IsAny<string>()))
                .ThrowsAsync(new ArgumentException("Invalid data")); // Simulate null result from the repository

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => tomadorServicoAppService.GetByAddressLine1(addressLine));
        }

        [Theory]
        [InlineData("47.720.204/0001-60", "Peter Parker")]
        [InlineData("19.807.699/0001-24", "Mary Jane")]
        [InlineData("74.658.700/0001-04", "Ned Stark")]
        public async Task Save_ShouldReturnsCompanyViewModel(string addressLine, string contactName)
        {
            var addressRepositoryMock = new Mock<IAddressRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var addressAppService = new AddressAppService(
                addressRepositoryMock.Object,
                mediatorHandlerMock.Object,
                mapperMock.Object);

            var addressEntity = new Address(Guid.NewGuid(), new City(Guid.NewGuid(), "oudricandrai", new State(Guid.NewGuid(), "ba", "Bahia", new Country("Salvaodr", "123", true, true, true, false, false), Guid.NewGuid())), new District(), "John Doe", addressLine);


            var createCommand = new CreateAddressCommand()
            {
                ContactName = contactName,
                AddressLine1 = addressLine
            };

            // Act
            await addressAppService.Save(createCommand);

            // Assert
            addressRepositoryMock.Verify(repo => repo.Add(It.IsAny<Address>()), Times.Once);
        }

        [Theory]
        [InlineData("65.988.820/0001-89", "Wally West")]
        [InlineData("65.486.979/0001-03", "Bruce Wayne")]
        [InlineData("34.414.259/0001-09", "Dick Grayson")]
        public async Task Save_ShouldHandleNullRepositoryResult(string addressLine, string contactName)
        {
            var addressRepositoryMock = new Mock<IAddressRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var addressAppService = new AddressAppService(
                addressRepositoryMock.Object,
                mediatorHandlerMock.Object,
                mapperMock.Object);

            var addressEntity = new Address(Guid.NewGuid(), new City(Guid.NewGuid(), "oudricandrai", new State(Guid.NewGuid(), "ba", "Bahia", new Country("Salvaodr", "123", true, true, true, false, false), Guid.NewGuid())), new District(), "John Doe", addressLine);


            var createCommand = new CreateAddressCommand()
            {
                ContactName = contactName,
                AddressLine1 = addressLine
            };

            addressRepositoryMock.Setup(repo => repo.Add(It.IsAny<Address>())).Throws(new NullReferenceException());

            // Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => addressAppService.Save(createCommand));
        }

        [Theory]
        [InlineData("11.247.786/0001-62", "Harley Quinzel")]
        [InlineData("57.463.010/0001-80", "Jason Todd")]
        [InlineData("06.780.664/0001-05", "Barry Allen")]
        public async Task Save_ShouldHandleInvalidMappingResult(string addressLine, string contactName)
        {
            // Arrange
            var addressRepositoryMock = new Mock<IAddressRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var addressAppService = new AddressAppService(
                addressRepositoryMock.Object,
                mediatorHandlerMock.Object,
                mapperMock.Object);

            var addressEntity = new Address(Guid.NewGuid(), new City(Guid.NewGuid(), "oudricandrai", new State(Guid.NewGuid(), "ba", "Bahia", new Country("Salvaodr", "123", true, true, true, false, false), Guid.NewGuid())), new District(), "John Doe", addressLine);


            var createCommand = new CreateAddressCommand()
            {
                ContactName = contactName,
                AddressLine1 = addressLine
            };

            // Act       
            addressRepositoryMock.Setup(repo => repo.Add(It.IsAny<Address>()))
            .Throws(new ArgumentException("Invalid data"));

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(() => addressAppService.Save(createCommand));
        }
    }
}
