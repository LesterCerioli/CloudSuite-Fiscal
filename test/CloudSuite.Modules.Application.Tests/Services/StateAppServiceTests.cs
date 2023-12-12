using AutoMapper;
using CloudSuite.Modules.Application.Handlers.State;
using CloudSuite.Modules.Application.Handlers.TomadorServico;
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
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CloudSuite.Modules.Application.Tests.Services
{
    public class StateAppServiceTests
    {
        [Theory]
        [InlineData("54.446.262/0001-03", "385.903.095.437")]
        [InlineData("06.485.306/0001-61", "080.863.092.761")]
        [InlineData("99.802.891/0001-67", "354.156.758.100")]
        public async Task GetByStateName_ShouldReturnsCompanyViewModel(string stateName, string uf)
        {
            var stateRepositoryMock = new Mock<IStateRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var stateAppService = new StateAppService(
                stateRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var country = new Country("Brazil", "736872364", true, false, true, false, true);
            var stateEntity = new State(Guid.NewGuid(), uf, stateName, country, Guid.NewGuid());

            stateRepositoryMock.Setup(repo => repo.GetByStateName(stateName)).ReturnsAsync(stateEntity);

            var expectedViewModel = new StateViewModel()
            {
                Id = stateEntity.Id,
                StateName = stateName,
                UF = uf
            };

            mapperMock.Setup(mapper => mapper.Map<StateViewModel>(stateEntity)).Returns(expectedViewModel);

            // Act
            var result = await stateAppService.GetByStateName(stateName);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        [Theory]
        [InlineData("Bahia")]
        [InlineData("Rio de Janeiro")]
        [InlineData("São Paulo")]
        public async Task GetByStateName_ShouldHandleNullRepositoryResult(string stateName)
        {
            // Arrange
            var stateRepositoryMock = new Mock<IStateRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var stateAppService = new StateAppService(
                stateRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            stateRepositoryMock.Setup(repo => repo.GetByStateName(It.IsAny<string>()))
                .ReturnsAsync((State)null); // Simulate null result from the repository

            // Act
            var result = await stateAppService.GetByStateName(stateName);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("Produtos Sustentáveis Eireli")]
        [InlineData("Desenvolvimento Rápido Ltda")]
        [InlineData("Integração Contínua S.A")]
        public async Task GetByStateName_ShouldHandleInvalidMappingResult(string stateName)
        {
            // Arrange
            var stateRepositoryMock = new Mock<IStateRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var stateAppService = new StateAppService(
                stateRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            stateRepositoryMock.Setup(repo => repo.GetByStateName(It.IsAny<string>()))
                .ThrowsAsync(new ArgumentException("Invalid data")); // Simulate null result from the repository

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => stateAppService.GetByStateName(stateName));
        }

        [Theory]
        [InlineData("54.446.262/0001-03", "385.903.095.437")]
        [InlineData("06.485.306/0001-61", "080.863.092.761")]
        [InlineData("99.802.891/0001-67", "354.156.758.100")]
        public async Task GetByUF_ShouldReturnsCompanyViewModel(string stateName, string uf)
        {
            var stateRepositoryMock = new Mock<IStateRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var stateAppService = new StateAppService(
                stateRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var country = new Country("Brazil", "736872364", true, false, true, false, true);
            var stateEntity = new State(Guid.NewGuid(), uf, stateName, country, Guid.NewGuid());

            stateRepositoryMock.Setup(repo => repo.GetByUF(uf)).ReturnsAsync(stateEntity);

            var expectedViewModel = new StateViewModel()
            {
                Id = stateEntity.Id,
                StateName = stateName,
                UF = uf
            };

            mapperMock.Setup(mapper => mapper.Map<StateViewModel>(stateEntity)).Returns(expectedViewModel);

            // Act
            var result = await stateAppService.GetByUF(uf);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        [Theory]
        [InlineData("Bahia")]
        [InlineData("Rio de Janeiro")]
        [InlineData("São Paulo")]
        public async Task GetByUF_ShouldHandleNullRepositoryResult(string uf)
        {
            // Arrange
            var stateRepositoryMock = new Mock<IStateRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var stateAppService = new StateAppService(
                stateRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            stateRepositoryMock.Setup(repo => repo.GetByUF(It.IsAny<string>()))
                .ReturnsAsync((State)null); // Simulate null result from the repository

            // Act
            var result = await stateAppService.GetByUF(uf);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("Produtos Sustentáveis Eireli")]
        [InlineData("Desenvolvimento Rápido Ltda")]
        [InlineData("Integração Contínua S.A")]
        public async Task GetByUF_ShouldHandleInvalidMappingResult(string uf)
        {
            // Arrange
            var stateRepositoryMock = new Mock<IStateRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var stateAppService = new StateAppService(
                stateRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            stateRepositoryMock.Setup(repo => repo.GetByUF(It.IsAny<string>()))
                .ThrowsAsync(new ArgumentException("Invalid data")); // Simulate null result from the repository

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => stateAppService.GetByUF(uf));
        }

        [Theory]
        [InlineData("54.446.262/0001-03", "385.903.095.437")]
        [InlineData("06.485.306/0001-61", "080.863.092.761")]
        [InlineData("99.802.891/0001-67", "354.156.758.100")]
        public async Task Save_ShouldReturnsCompanyViewModel(string stateName, string uf)
        {
            var stateRepositoryMock = new Mock<IStateRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var stateAppService = new StateAppService(
                stateRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var createCommand = new CreateStateCommand()
            {
                UF = uf,
                StateName = stateName
            };

            // Act
            await stateAppService.Save(createCommand);

            // Assert
            stateRepositoryMock.Verify(repo => repo.Add(It.IsAny<State>()), Times.Once);
        }

        [Theory]
        [InlineData("54.446.262/0001-03", "385.903.095.437")]
        [InlineData("06.485.306/0001-61", "080.863.092.761")]
        [InlineData("99.802.891/0001-67", "354.156.758.100")]
        public async Task Save_ShouldHandleNullRepositoryResult(string stateName, string uf)
        {
            var stateRepositoryMock = new Mock<IStateRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var stateAppService = new StateAppService(
                stateRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var createCommand = new CreateStateCommand()
            {
                UF = uf,
                StateName = stateName
            };

            stateRepositoryMock.Setup(repo => repo.Add(It.IsAny<State>())).Throws(new NullReferenceException());

            // Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => stateAppService.Save(createCommand));
        }

        [Theory]
        [InlineData("54.446.262/0001-03", "385.903.095.437")]
        [InlineData("06.485.306/0001-61", "080.863.092.761")]
        [InlineData("99.802.891/0001-67", "354.156.758.100")]
        public async Task Save_ShouldHandleInvalidMappingResult(string stateName, string uf)
        {
            // Arrange
            var stateRepositoryMock = new Mock<IStateRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var stateAppService = new StateAppService(
                stateRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var createCommand = new CreateStateCommand()
            {
                UF = uf,
                StateName = stateName
            };

            // Act       
            stateRepositoryMock.Setup(repo => repo.Add(It.IsAny<State>()))
            .Throws(new ArgumentException("Invalid data"));

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(() => stateAppService.Save(createCommand));
        }
    }
}
