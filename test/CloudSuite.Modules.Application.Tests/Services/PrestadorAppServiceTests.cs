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
        [InlineData("22.022.699/0001-23", "123.456.789.01", "987.654.321.10", "12.345.678-9", "Empresa Exemplo Ltda", "Empresa Exemplo", 2)]
        [InlineData("23.302.139/0001-95", "234.567.890.12", "876.543.210.98", "23.456.789-0", "Exemplo Comércio Ltda", "Exemplo Comércio", 3)]
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
        [InlineData("86.847.798/0001-27")]
        [InlineData("86.412.279/0001-36")]
        [InlineData("97.785.424/0001-40")]
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
        [InlineData("49.317.440/0001-66")]
        [InlineData("02.142.194/0001-95")]
        [InlineData("00.012.377/0001-60")]
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
        [InlineData("96.746.617/0001-20", "345.678.901.23", "765.432.109.87", "34.567.890-1", "Comércio Exemplo Ltda", "Comércio Exemplo", 4)]
        [InlineData("74.952.543/0001-45", "456.789.012.34", "654.321.098.76", "45.678.901-2", "Ltda Exemplo Empresa", "Ltda Exemplo", 5)]
        [InlineData("07.782.203/0001-26", "567.890.123.45", "543.210.987.65", "56.789.012-3", "Empresa Ltda Exemplo", "Empresa Ltda", 6)]
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
        [InlineData("1298046792380167508972365879346578")]
        [InlineData("7380146587903465783645897643875343")]
        [InlineData("8398493849382039480239840239840293")]
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
        [InlineData("84.230.466/0001-73", "678.901.234.56", "432.109.876.54", "67.890.123-4", "Exemplo Empresa Ltda", "Exemplo Empresa", 7)]
        [InlineData("20.488.026/0001-38", "789.012.345.67", "321.098.765.43", "78.901.234-5", "Comércio Ltda Exemplo", "Comércio Ltda", 8)]
        [InlineData("08.603.960/0001-58", "890.123.456.78", "210.987.654.32", "89.012.345-6", "Ltda Comércio Exemplo", "Ltda Comércio", 9)]
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
        [InlineData("42.834.733/0001-71", "123.456.789.01", "987.654.321.10", "12.345.678-9", "Empresa Exemplo Ltda", "Empresa Exemplo", 11)]
        [InlineData("85.289.558/0001-91", "234.567.890.12", "876.543.210.98", "23.456.789-0", "Exemplo Comércio Ltda", "Exemplo Comércio", 12)]
        [InlineData("76.389.759/0001-70", "345.678.901.23", "765.432.109.87", "34.567.890-1", "Comércio Exemplo Ltda", "Comércio Exemplo", 13)]
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
        [InlineData("71.376.604/0001-85", "456.789.012.34", "654.321.098.76", "45.678.901-2", "Ltda Exemplo Empresa", "Ltda Exemplo", 14)]
        [InlineData("94.610.220/0001-80", "567.890.123.45", "543.210.987.65", "56.789.012-3", "Empresa Ltda Exemplo", "Empresa Ltda", 15)]
        [InlineData("36.781.699/0001-11", "678.901.234.56", "432.109.876.54", "67.890.123-4", "Exemplo Empresa Ltda", "Exemplo Empresa", 16)]
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
        [InlineData("74.362.685/0001-52", "789.012.345.67", "321.098.765.43", "78.901.234-5", "Comércio Ltda Exemplo", "Comércio Ltda", 17)]
        [InlineData("19.361.788/0001-90", "890.123.456.78", "210.987.654.32", "89.012.345-6", "Ltda Comércio Exemplo", "Ltda Comércio", 18)]
        [InlineData("43.295.388/0001-08", "901.234.567.89", "109.876.543.21", "90.123.456-7", "Exemplo Ltda Comércio", "Exemplo Ltda", 19)]
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
        [InlineData("98.451.053/0001-23", "456.789.012.34", "654.321.098.76", "45.678.901-2", "Ltda Exemplo Empresa", "Ltda Exemplo", 23)]
        [InlineData("70.631.189/0001-04", "567.890.123.45", "543.210.987.65", "56.789.012-3", "Empresa Ltda Exemplo", "Empresa Ltda", 24)]
        [InlineData("93.380.587/0001-92", "678.901.234.56", "432.109.876.54", "67.890.123-4", "Exemplo Empresa Ltda", "Exemplo Empresa", 25)]
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
        [InlineData("95.214.406/0001-83", "123.456.789.01", "987.654.321.10", "12.345.678-9", "Empresa Exemplo Ltda", "Empresa Exemplo", 20)]
        [InlineData("37.182.511/0001-81", "234.567.890.12", "876.543.210.98", "23.456.789-0", "Exemplo Comércio Ltda", "Exemplo Comércio", 21)]
        [InlineData("19.193.804/0001-82", "345.678.901.23", "765.432.109.87", "34.567.890-1", "Comércio Exemplo Ltda", "Comércio Exemplo", 22)]
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
