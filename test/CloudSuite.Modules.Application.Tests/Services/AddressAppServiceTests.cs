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
        [InlineData("54.446.262/0001-03", "John Doe")]
        [InlineData("06.485.306/0001-61", "Jessica Jones")]
        [InlineData("99.802.891/0001-67", "Malory")]
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
        [InlineData("54.446.262/0001-03", "John Doe")]
        [InlineData("06.485.306/0001-61", "Jessica Jones")]
        [InlineData("99.802.891/0001-67", "Malory")]
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
                Id = addressEntity.Id,
                ContactName = contactName,
                AddressLine1 = addressLine
            };

            // Act
            await addressAppService.Save(createCommand);

            // Assert
            addressRepositoryMock.Verify(repo => repo.Add(It.IsAny<Address>()), Times.Once);
        }

        [Theory]
        [InlineData("54.446.262/0001-03", "John Doe")]
        [InlineData("06.485.306/0001-61", "Jessica Jones")]
        [InlineData("99.802.891/0001-67", "Malory")]
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
                Id = addressEntity.Id,
                ContactName = contactName,
                AddressLine1 = addressLine
            };

            addressRepositoryMock.Setup(repo => repo.Add(It.IsAny<Address>())).Throws(new NullReferenceException());

            // Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => addressAppService.Save(createCommand));
        }

        [Theory]
        [InlineData("54.446.262/0001-03", "John Doe")]
        [InlineData("06.485.306/0001-61", "Jessica Jones")]
        [InlineData("99.802.891/0001-67", "Malory")]
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
                Id = addressEntity.Id,
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
