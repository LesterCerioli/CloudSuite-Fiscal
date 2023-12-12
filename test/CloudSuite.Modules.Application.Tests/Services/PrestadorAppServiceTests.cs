using AutoMapper;
using CloudSuite.Modules.Application.Handlers.Prestador;
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
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CloudSuite.Modules.Application.Tests.Services
{
    public class PrestadorAppServiceTests
    {
        [Theory]
        [InlineData("54.446.262/0001-03", "385.903.095.437", "289.409.128.328", "21.904.074-6", "Olivia e Sophia Filmagens Ltda", "Olivia e Sophia Filmagens", 1)]
        [InlineData("06.485.306/0001-61", "080.863.092.761", "289.088.794.339", "49.599.874-6", "Isaac e Danilo Pães e Doces Ltda", "Isaac e Danilo Pães e Doces", 2)]
        [InlineData("99.802.891/0001-67", "354.156.758.100", "519.481.861.989", "41.462.104-9", "Alessandra e Clarice Casa Noturna Ltda", "Alessandra e Clarice Casa Noturna", 3)]
        public async Task GetByCnpj_ShouldReturnsCompanyViewModel(string cnpj, string inscricaoMunicipal, string inscricaoEstadual, string docTomadorEstrangeiro, string socialReason, string nomeFantasia, int tipo)
        {
            var prestadorRepositoryMock = new Mock<IPrestadorRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var prestadorAppService = new PrestadorAppService(
                prestadorRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var address = new Address(Guid.NewGuid(), new City(Guid.NewGuid(), "oudricandrai", new State(Guid.NewGuid(), "ba", "Bahia", new Country("Salvaodr", "123", true, true, true, false, false), Guid.NewGuid())), new District(), "John Doe", "rua dos patos");
            var prestadorEntity = new Prestador(new Cnpj(cnpj), inscricaoMunicipal, inscricaoEstadual, docTomadorEstrangeiro, socialReason, nomeFantasia, address, tipo);
            prestadorRepositoryMock.Setup(repo => repo.GetByCnpj(cnpj)).ReturnsAsync(prestadorEntity);

            var expectedViewModel = new PrestadorViewModel()
            {
                Id = prestadorEntity.Id,
                Cnpj = cnpj,
                InscricaoMunicipal = inscricaoMunicipal,
                InscricaoEstadual = inscricaoEstadual,
                DocTomadorEstrangeiro = docTomadorEstrangeiro,
                SocialReason = socialReason,
                NomeFantasia = nomeFantasia,
                Tipo = tipo
            };

            mapperMock.Setup(mapper => mapper.Map<PrestadorViewModel>(prestadorEntity)).Returns(expectedViewModel);

            // Act
            var result = await prestadorAppService.GetByCnpj(cnpj);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        [Theory]
        [InlineData("26.514.455/0001-19")]
        [InlineData("50.159.125/0001-37")]
        [InlineData("22.924.442/0001-67")]
        public async Task GetByCnpj_ShouldHandleNullRepositoryResult(string cnpj)
        {
            // Arrange
            var prestadorRepositoryMock = new Mock<IPrestadorRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var prestadorAppService = new PrestadorAppService(
                prestadorRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            prestadorRepositoryMock.Setup(repo => repo.GetByCnpj(It.IsAny<Cnpj>()))
                .ReturnsAsync((Prestador)null); // Simulate null result from the repository

            // Act
            var result = await prestadorAppService.GetByCnpj(cnpj);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("26.514.455/0001-19")]
        [InlineData("50.159.125/0001-37")]
        [InlineData("22.924.442/0001-67")]
        public async Task GetByCnpj_ShouldHandleInvalidMappingResult(string cnpj)
        {
            // Arrange
            var prestadorRepositoryMock = new Mock<IPrestadorRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var prestadorAppService = new PrestadorAppService(
                prestadorRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            prestadorRepositoryMock.Setup(repo => repo.GetByCnpj(It.IsAny<Cnpj>()))
                .ThrowsAsync(new ArgumentException("Invalid data")); // Simulate null result from the repository

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => prestadorAppService.GetByCnpj(cnpj));
        }

        [Theory]
        [InlineData("54.446.262/0001-03", "385.903.095.437", "289.409.128.328", "21.904.074-6", "Olivia e Sophia Filmagens Ltda", "Olivia e Sophia Filmagens", 1)]
        [InlineData("06.485.306/0001-61", "080.863.092.761", "289.088.794.339", "49.599.874-6", "Isaac e Danilo Pães e Doces Ltda", "Isaac e Danilo Pães e Doces", 2)]
        [InlineData("99.802.891/0001-67", "354.156.758.100", "519.481.861.989", "41.462.104-9", "Alessandra e Clarice Casa Noturna Ltda", "Alessandra e Clarice Casa Noturna", 3)]
        public async Task GetByInscricaoEstadual_ShouldReturnsCompanyViewModel(string cnpj, string inscricaoMunicipal, string inscricaoEstadual, string docTomadorEstrangeiro, string socialReason, string nomeFantasia, int tipo)
        {
            var prestadorRepositoryMock = new Mock<IPrestadorRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var prestadorAppService = new PrestadorAppService(
                prestadorRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var address = new Address(Guid.NewGuid(), new City(Guid.NewGuid(), "oudricandrai", new State(Guid.NewGuid(), "ba", "Bahia", new Country("Salvaodr", "123", true, true, true, false, false), Guid.NewGuid())), new District(), "John Doe", "rua dos patos");
            var prestadorEntity = new Prestador(new Cnpj(cnpj), inscricaoMunicipal, inscricaoEstadual, docTomadorEstrangeiro, socialReason, nomeFantasia, address, tipo);

            prestadorRepositoryMock.Setup(repo => repo.GetByInscricaoEstadual(inscricaoEstadual)).ReturnsAsync(prestadorEntity);

            var expectedViewModel = new PrestadorViewModel()
            {
                Id = prestadorEntity.Id,
                Cnpj = cnpj,
                InscricaoMunicipal = inscricaoMunicipal,
                InscricaoEstadual = inscricaoEstadual,
                DocTomadorEstrangeiro = docTomadorEstrangeiro,
                SocialReason = socialReason,
                NomeFantasia = nomeFantasia,
                Tipo = tipo
            };

            mapperMock.Setup(mapper => mapper.Map<PrestadorViewModel>(prestadorEntity)).Returns(expectedViewModel);

            // Act
            var result = await prestadorAppService.GetByInscricaoEstadual(inscricaoEstadual);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        [Theory]
        [InlineData("289.409.128.328")]
        [InlineData("289.088.794.339")]
        [InlineData("519.481.861.989")]
        public async Task GetByInscricaoEstadual_ShouldHandleNullRepositoryResult(string inscricaoEstadual)
        {
            // Arrange
            var prestadorRepositoryMock = new Mock<IPrestadorRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var prestadorAppService = new PrestadorAppService(
                prestadorRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            prestadorRepositoryMock.Setup(repo => repo.GetByInscricaoEstadual(It.IsAny<string>()))
                .ReturnsAsync((Prestador)null); // Simulate null result from the repository

            // Act
            var result = await prestadorAppService.GetByInscricaoEstadual(inscricaoEstadual);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("289.409.128.328")]
        [InlineData("289.088.794.339")]
        [InlineData("519.481.861.989")]
        public async Task GetByInscricaoEstadual_ShouldHandleInvalidMappingResult(string inscricaoEstadual)
        {
            // Arrange
            var prestadorRepositoryMock = new Mock<IPrestadorRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var prestadorAppService = new PrestadorAppService(
                prestadorRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            prestadorRepositoryMock.Setup(repo => repo.GetByInscricaoEstadual(It.IsAny<string>()))
                .ThrowsAsync(new ArgumentException("Invalid data")); // Simulate null result from the repository

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => prestadorAppService.GetByInscricaoEstadual(inscricaoEstadual));
        }

        [Theory]
        [InlineData("54.446.262/0001-03", "385.903.095.437", "289.409.128.328", "21.904.074-6", "Olivia e Sophia Filmagens Ltda", "Olivia e Sophia Filmagens", 1)]
        [InlineData("06.485.306/0001-61", "080.863.092.761", "289.088.794.339", "49.599.874-6", "Isaac e Danilo Pães e Doces Ltda", "Isaac e Danilo Pães e Doces", 2)]
        [InlineData("99.802.891/0001-67", "354.156.758.100", "519.481.861.989", "41.462.104-9", "Alessandra e Clarice Casa Noturna Ltda", "Alessandra e Clarice Casa Noturna", 3)]
        public async Task GetByDocTomadorEstrangeiro_ShouldReturnsCompanyViewModel(string cnpj, string inscricaoMunicipal, string inscricaoEstadual, string docTomadorEstrangeiro, string socialReason, string nomeFantasia, int tipo)
        {
            var prestadorRepositoryMock = new Mock<IPrestadorRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var prestadorAppService = new PrestadorAppService(
                prestadorRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var address = new Address(Guid.NewGuid(), new City(Guid.NewGuid(), "oudricandrai", new State(Guid.NewGuid(), "ba", "Bahia", new Country("Salvaodr", "123", true, true, true, false, false), Guid.NewGuid())), new District(), "John Doe", "rua dos patos");
            var prestadorEntity = new Prestador(new Cnpj(cnpj), inscricaoMunicipal, inscricaoEstadual, docTomadorEstrangeiro, socialReason, nomeFantasia, address, tipo);

            prestadorRepositoryMock.Setup(repo => repo.GetByDocTomadorEstrangeiro(docTomadorEstrangeiro)).ReturnsAsync(prestadorEntity);

            var expectedViewModel = new PrestadorViewModel()
            {
                Id = prestadorEntity.Id,
                Cnpj = cnpj,
                InscricaoMunicipal = inscricaoMunicipal,
                InscricaoEstadual = inscricaoEstadual,
                DocTomadorEstrangeiro = docTomadorEstrangeiro,
                SocialReason = socialReason,
                NomeFantasia = nomeFantasia,
                Tipo = tipo
            };

            mapperMock.Setup(mapper => mapper.Map<PrestadorViewModel>(prestadorEntity)).Returns(expectedViewModel);

            // Act
            var result = await prestadorAppService.GetByDocTomadorEstrangeiro(docTomadorEstrangeiro);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        [Theory]
        [InlineData("Soluções Inovadoras Ltda")]
        [InlineData("Tecnologia Avançada S.A")]
        [InlineData("Serviços Globais Eireli")]
        public async Task GetByDocTomadorEstrangeiro_ShouldHandleNullRepositoryResult(string docTomandorEstrangeiro)
        {
            // Arrange
            var prestadorRepositoryMock = new Mock<IPrestadorRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var prestadorAppService = new PrestadorAppService(
                prestadorRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            prestadorRepositoryMock.Setup(repo => repo.GetByDocTomadorEstrangeiro(It.IsAny<string>()))
                .ReturnsAsync((Prestador)null); // Simulate null result from the repository

            // Act
            var result = await prestadorAppService.GetByDocTomadorEstrangeiro(docTomandorEstrangeiro);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("21.904.074-6")]
        [InlineData("49.599.874-6")]
        [InlineData("41.462.104-9")]
        public async Task GetByDocTomadorEstrangeiro_ShouldHandleInvalidMappingResult(string docTomadorEstrangeiro)
        {
            // Arrange
            var prestadorRepositoryMock = new Mock<IPrestadorRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var prestadorAppService = new PrestadorAppService(
                prestadorRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            prestadorRepositoryMock.Setup(repo => repo.GetByDocTomadorEstrangeiro(It.IsAny<string>()))
                .ThrowsAsync(new ArgumentException("Invalid data")); // Simulate null result from the repository

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => prestadorAppService.GetByDocTomadorEstrangeiro(docTomadorEstrangeiro));
        }

        [Theory]
        [InlineData("54.446.262/0001-03", "385.903.095.437", "289.409.128.328", "21.904.074-6", "Olivia e Sophia Filmagens Ltda", "Olivia e Sophia Filmagens", 1)]
        [InlineData("06.485.306/0001-61", "080.863.092.761", "289.088.794.339", "49.599.874-6", "Isaac e Danilo Pães e Doces Ltda", "Isaac e Danilo Pães e Doces", 2)]
        [InlineData("99.802.891/0001-67", "354.156.758.100", "519.481.861.989", "41.462.104-9", "Alessandra e Clarice Casa Noturna Ltda", "Alessandra e Clarice Casa Noturna", 3)]
        public async Task GetByInscricaoMunicipal_ShouldReturnsCompanyViewModel(string cnpj, string inscricaoMunicipal, string inscricaoEstadual, string docTomadorEstrangeiro, string socialReason, string nomeFantasia, int tipo)
        {
            var prestadorRepositoryMock = new Mock<IPrestadorRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var prestadorAppService = new PrestadorAppService(
                prestadorRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var address = new Address(Guid.NewGuid(), new City(Guid.NewGuid(), "oudricandrai", new State(Guid.NewGuid(), "ba", "Bahia", new Country("Salvaodr", "123", true, true, true, false, false), Guid.NewGuid())), new District(), "John Doe", "rua dos patos");
            var prestadorEntity = new Prestador(new Cnpj(cnpj), inscricaoMunicipal, inscricaoEstadual, docTomadorEstrangeiro, socialReason, nomeFantasia, address, tipo);

            prestadorRepositoryMock.Setup(repo => repo.GetByInscricaoMunicipal(inscricaoMunicipal)).ReturnsAsync(prestadorEntity);

            var expectedViewModel = new PrestadorViewModel()
            {
                Id = prestadorEntity.Id,
                Cnpj = cnpj,
                InscricaoMunicipal = inscricaoMunicipal,
                InscricaoEstadual = inscricaoEstadual,
                DocTomadorEstrangeiro = docTomadorEstrangeiro,
                SocialReason = socialReason,
                NomeFantasia = nomeFantasia,
                Tipo = tipo
            };

            mapperMock.Setup(mapper => mapper.Map<PrestadorViewModel>(prestadorEntity)).Returns(expectedViewModel);

            // Act
            var result = await prestadorAppService.GetByInscricaoMunicipal(inscricaoMunicipal);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        [Theory]
        [InlineData("Soluções Inovadoras Ltda")]
        [InlineData("Tecnologia Avançada S.A")]
        [InlineData("Serviços Globais Eireli")]
        public async Task GetByInscricaoMunicipal_ShouldHandleNullRepositoryResult(string inscricaoMunicipal)
        {
            // Arrange
            var prestadorRepositoryMock = new Mock<IPrestadorRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var prestadorAppService = new PrestadorAppService(
                prestadorRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            prestadorRepositoryMock.Setup(repo => repo.GetByInscricaoMunicipal(It.IsAny<string>()))
                .ReturnsAsync((Prestador)null); // Simulate null result from the repository

            // Act
            var result = await prestadorAppService.GetByInscricaoMunicipal(inscricaoMunicipal);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("Produtos Sustentáveis Eireli")]
        [InlineData("Desenvolvimento Rápido Ltda")]
        [InlineData("Integração Contínua S.A")]
        public async Task GetByInscricaoMunicipal_ShouldHandleInvalidMappingResult(string inscricaoMunicipal)
        {
            // Arrange
            var prestadorRepositoryMock = new Mock<IPrestadorRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var prestadorAppService = new PrestadorAppService(
                prestadorRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            prestadorRepositoryMock.Setup(repo => repo.GetByInscricaoMunicipal(It.IsAny<string>()))
                .ThrowsAsync(new ArgumentException("Invalid data")); // Simulate null result from the repository

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => prestadorAppService.GetByInscricaoMunicipal(inscricaoMunicipal));
        }

        [Theory]
        [InlineData("54.446.262/0001-03", "385.903.095.437", "289.409.128.328", "21.904.074-6", "Olivia e Sophia Filmagens Ltda", "Olivia e Sophia Filmagens", 1)]
        [InlineData("06.485.306/0001-61", "080.863.092.761", "289.088.794.339", "49.599.874-6", "Isaac e Danilo Pães e Doces Ltda", "Isaac e Danilo Pães e Doces", 2)]
        [InlineData("99.802.891/0001-67", "354.156.758.100", "519.481.861.989", "41.462.104-9", "Alessandra e Clarice Casa Noturna Ltda", "Alessandra e Clarice Casa Noturna", 3)]
        public async Task GetByNomeFantasia_ShouldReturnsCompanyViewModel(string cnpj, string inscricaoMunicipal, string inscricaoEstadual, string docTomadorEstrangeiro, string socialReason, string nomeFantasia, int tipo)
        {
            var prestadorRepositoryMock = new Mock<IPrestadorRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var prestadorAppService = new PrestadorAppService(
                prestadorRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var address = new Address(Guid.NewGuid(), new City(Guid.NewGuid(), "oudricandrai", new State(Guid.NewGuid(), "ba", "Bahia", new Country("Salvaodr", "123", true, true, true, false, false), Guid.NewGuid())), new District(), "John Doe", "rua dos patos");
            var prestadorEntity = new Prestador(new Cnpj(cnpj), inscricaoMunicipal, inscricaoEstadual, docTomadorEstrangeiro, socialReason, nomeFantasia, address, tipo);

            prestadorRepositoryMock.Setup(repo => repo.GetByNomeFantasia(nomeFantasia)).ReturnsAsync(prestadorEntity);

            var expectedViewModel = new PrestadorViewModel()
            {
                Id = prestadorEntity.Id,
                Cnpj = cnpj,
                InscricaoMunicipal = inscricaoMunicipal,
                InscricaoEstadual = inscricaoEstadual,
                DocTomadorEstrangeiro = docTomadorEstrangeiro,
                SocialReason = socialReason,
                NomeFantasia = nomeFantasia,
                Tipo = tipo
            };

            mapperMock.Setup(mapper => mapper.Map<PrestadorViewModel>(prestadorEntity)).Returns(expectedViewModel);

            // Act
            var result = await prestadorAppService.GetByNomeFantasia(nomeFantasia);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        [Theory]
        [InlineData("Soluções Inovadoras Ltda")]
        [InlineData("Tecnologia Avançada S.A")]
        [InlineData("Serviços Globais Eireli")]
        public async Task GetByNomeFantasia_ShouldHandleNullRepositoryResult(string nomeFantasia)
        {
            // Arrange
            var prestadorRepositoryMock = new Mock<IPrestadorRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var prestadorAppService = new PrestadorAppService(
                prestadorRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            prestadorRepositoryMock.Setup(repo => repo.GetByNomeFantasia(It.IsAny<string>()))
                .ReturnsAsync((Prestador)null); // Simulate null result from the repository

            // Act
            var result = await prestadorAppService.GetByNomeFantasia(nomeFantasia);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("Produtos Sustentáveis Eireli")]
        [InlineData("Desenvolvimento Rápido Ltda")]
        [InlineData("Integração Contínua S.A")]
        public async Task GetByNomeFantasia_ShouldHandleInvalidMappingResult(string nomeFantasia)
        {
            // Arrange
            var prestadorRepositoryMock = new Mock<IPrestadorRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var prestadorAppService = new PrestadorAppService(
                prestadorRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            prestadorRepositoryMock.Setup(repo => repo.GetByNomeFantasia(It.IsAny<string>()))
                .ThrowsAsync(new ArgumentException("Invalid data")); // Simulate null result from the repository

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => prestadorAppService.GetByNomeFantasia(nomeFantasia));
        }

        [Theory]
        [InlineData("54.446.262/0001-03", "385.903.095.437", "289.409.128.328", "21.904.074-6", "Olivia e Sophia Filmagens Ltda", "Olivia e Sophia Filmagens", 1)]
        [InlineData("06.485.306/0001-61", "080.863.092.761", "289.088.794.339", "49.599.874-6", "Isaac e Danilo Pães e Doces Ltda", "Isaac e Danilo Pães e Doces", 2)]
        [InlineData("99.802.891/0001-67", "354.156.758.100", "519.481.861.989", "41.462.104-9", "Alessandra e Clarice Casa Noturna Ltda", "Alessandra e Clarice Casa Noturna", 3)]
        public async Task Save_ShouldReturnsCompanyViewModel(string cnpj, string inscricaoMunicipal, string inscricaoEstadual, string docTomadorEstrangeiro, string socialReason, string nomeFantasia, int tipo)
        {
            var prestadorRepositoryMock = new Mock<IPrestadorRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var prestadorAppService = new PrestadorAppService(
                prestadorRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var createCommand = new CreatePrestadorCommand()
            {
                Cnpj = cnpj,
                InscricaoMunicipal = inscricaoMunicipal,
                InscricaoEstadual = inscricaoEstadual,
                DocTomadorEstrangeiro = docTomadorEstrangeiro,
                SocialReason = socialReason,
                NomeFantasia = nomeFantasia,
                Tipo = tipo
            };

            // Act
            await prestadorAppService.Save(createCommand);

            // Assert
            prestadorRepositoryMock.Verify(repo => repo.Add(It.IsAny<Prestador>()), Times.Once);
        }

        [Theory]
        [InlineData("54.446.262/0001-03", "385.903.095.437", "289.409.128.328", "21.904.074-6", "Olivia e Sophia Filmagens Ltda", "Olivia e Sophia Filmagens", 1)]
        [InlineData("06.485.306/0001-61", "080.863.092.761", "289.088.794.339", "49.599.874-6", "Isaac e Danilo Pães e Doces Ltda", "Isaac e Danilo Pães e Doces", 2)]
        [InlineData("99.802.891/0001-67", "354.156.758.100", "519.481.861.989", "41.462.104-9", "Alessandra e Clarice Casa Noturna Ltda", "Alessandra e Clarice Casa Noturna", 3)]
        public async Task Save_ShouldHandleNullRepositoryResult(string cnpj, string inscricaoMunicipal, string inscricaoEstadual, string docTomadorEstrangeiro, string socialReason, string nomeFantasia, int tipo)
        {
            var prestadorRepositoryMock = new Mock<IPrestadorRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var prestadorAppService = new PrestadorAppService(
                prestadorRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var createCommand = new CreatePrestadorCommand()
            {
                Cnpj = cnpj,
                InscricaoMunicipal = inscricaoMunicipal,
                InscricaoEstadual = inscricaoEstadual,
                DocTomadorEstrangeiro = docTomadorEstrangeiro,
                SocialReason = socialReason,
                NomeFantasia = nomeFantasia,
                Tipo = tipo
            };

            prestadorRepositoryMock.Setup(repo => repo.Add(It.IsAny<Prestador>())).Throws(new NullReferenceException());

            // Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => prestadorAppService.Save(createCommand));
        }

        [Theory]
        [InlineData("54.446.262/0001-03", "385.903.095.437", "289.409.128.328", "21.904.074-6", "Olivia e Sophia Filmagens Ltda", "Olivia e Sophia Filmagens", 1)]
        [InlineData("06.485.306/0001-61", "080.863.092.761", "289.088.794.339", "49.599.874-6", "Isaac e Danilo Pães e Doces Ltda", "Isaac e Danilo Pães e Doces", 2)]
        [InlineData("99.802.891/0001-67", "354.156.758.100", "519.481.861.989", "41.462.104-9", "Alessandra e Clarice Casa Noturna Ltda", "Alessandra e Clarice Casa Noturna", 3)]
        public async Task Save_ShouldHandleInvalidMappingResult(string cnpj, string inscricaoMunicipal, string inscricaoEstadual, string docTomadorEstrangeiro, string socialReason, string nomeFantasia, int tipo)
        {
            // Arrange
            var prestadorRepositoryMock = new Mock<IPrestadorRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var prestadorAppService = new PrestadorAppService(
                prestadorRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var createCommand = new CreatePrestadorCommand()
            {
                Cnpj = cnpj,
                InscricaoMunicipal = inscricaoMunicipal,
                InscricaoEstadual = inscricaoEstadual,
                DocTomadorEstrangeiro = docTomadorEstrangeiro,
                SocialReason = socialReason,
                NomeFantasia = nomeFantasia,
                Tipo = tipo
            };

            // Act       
            prestadorRepositoryMock.Setup(repo => repo.Add(It.IsAny<Prestador>()))
            .Throws(new ArgumentException("Invalid data"));

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(() => prestadorAppService.Save(createCommand));
        }

    }
}
