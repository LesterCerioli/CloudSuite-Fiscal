using AutoMapper;
using CloudSuite.Modules.Application.Handlers.FederalTax;
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
    public class TomadorServicoAppServiceTests
    {
        [Theory]
        [InlineData("54.446.262/0001-03", "385.903.095.437", "289.409.128.328", "21.904.074-6", "Olivia e Sophia Filmagens Ltda", "Olivia e Sophia Filmagens", 1)]
        [InlineData("06.485.306/0001-61", "080.863.092.761", "289.088.794.339", "49.599.874-6", "Isaac e Danilo Pães e Doces Ltda", "Isaac e Danilo Pães e Doces", 2)]
        [InlineData("99.802.891/0001-67", "354.156.758.100", "519.481.861.989", "41.462.104-9", "Alessandra e Clarice Casa Noturna Ltda", "Alessandra e Clarice Casa Noturna", 3)]
        public async Task GetByCnpj_ShouldReturnsCompanyViewModel(string cnpj, string inscricaoMunicipal, string inscricaoEstadual, string docTomadorEstrangeiro, string socialReason, string nomeFantasia, int tipo)
        {
            var tomadorServicoRepositoryMock = new Mock<ITomadorServicoRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var tomadorServicoAppService = new TomadorServicoAppService(
                tomadorServicoRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var address = new Address(Guid.NewGuid(), new City(Guid.NewGuid(), "oudricandrai", new State(Guid.NewGuid(), "ba","Bahia",new Country("Salvaodr", "123", true, true, true,false,false), Guid.NewGuid())), new District(), "John Doe", "rua dos patos");
            var tomadorServicoEntity = new TomadorServico(new Cnpj(cnpj), inscricaoMunicipal, inscricaoEstadual, docTomadorEstrangeiro, socialReason, nomeFantasia, address, tipo);
            tomadorServicoRepositoryMock.Setup(repo => repo.GetByCnpj(cnpj)).ReturnsAsync(tomadorServicoEntity);

            var expectedViewModel = new TomadorServicoViewModel()
            {
                Id = tomadorServicoEntity.Id,
                Cnpj = cnpj,
                InscricaoMunicipal = inscricaoMunicipal,
                InscricaoEstadual = inscricaoEstadual,
                DocTomadorEstrangeiro = docTomadorEstrangeiro,
                SocialReason = socialReason,
                NomeFantasia = nomeFantasia,
                Tipo = tipo
            };

            mapperMock.Setup(mapper => mapper.Map<TomadorServicoViewModel>(tomadorServicoEntity)).Returns(expectedViewModel);

            // Act
            var result = await tomadorServicoAppService.GetByCnpj(cnpj);

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
            var tomadorServicoRepositoryMock = new Mock<ITomadorServicoRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var tomadorServicoAppService = new TomadorServicoAppService(
                tomadorServicoRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            tomadorServicoRepositoryMock.Setup(repo => repo.GetByCnpj(It.IsAny<Cnpj>()))
                .ReturnsAsync((TomadorServico)null); // Simulate null result from the repository

            // Act
            var result = await tomadorServicoAppService.GetByCnpj(cnpj);

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
            var tomadorServicoRepositoryMock = new Mock<ITomadorServicoRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var tomadorServicoAppService = new TomadorServicoAppService(
                tomadorServicoRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            tomadorServicoRepositoryMock.Setup(repo => repo.GetByCnpj(It.IsAny<Cnpj>()))
                .ThrowsAsync(new ArgumentException("Invalid data")); // Simulate null result from the repository

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => tomadorServicoAppService.GetByCnpj(cnpj));
        }

        [Theory]
        [InlineData("54.446.262/0001-03", "385.903.095.437", "289.409.128.328", "21.904.074-6", "Olivia e Sophia Filmagens Ltda", "Olivia e Sophia Filmagens", 1)]
        [InlineData("06.485.306/0001-61", "080.863.092.761", "289.088.794.339", "49.599.874-6", "Isaac e Danilo Pães e Doces Ltda", "Isaac e Danilo Pães e Doces", 2)]
        [InlineData("99.802.891/0001-67", "354.156.758.100", "519.481.861.989", "41.462.104-9", "Alessandra e Clarice Casa Noturna Ltda", "Alessandra e Clarice Casa Noturna", 3)]
        public async Task GetBySocialReason_ShouldReturnsCompanyViewModel(string cnpj, string inscricaoMunicipal, string inscricaoEstadual, string docTomadorEstrangeiro, string socialReason, string nomeFantasia, int tipo)
        {
            var tomadorServicoRepositoryMock = new Mock<ITomadorServicoRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var tomadorServicoAppService = new TomadorServicoAppService(
                tomadorServicoRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var address = new Address(Guid.NewGuid(), new City(Guid.NewGuid(), "oudricandrai", new State(Guid.NewGuid(), "ba", "Bahia", new Country("Salvaodr", "123", true, true, true, false, false), Guid.NewGuid())), new District(), "John Doe", "rua dos patos");
            var tomadorServicoEntity = new TomadorServico(new Cnpj(cnpj), inscricaoMunicipal, inscricaoEstadual, docTomadorEstrangeiro, socialReason, nomeFantasia, address, tipo);
            
            tomadorServicoRepositoryMock.Setup(repo => repo.GetBySocialReason(socialReason)).ReturnsAsync(tomadorServicoEntity);

            var expectedViewModel = new TomadorServicoViewModel()
            {
                Id = tomadorServicoEntity.Id,
                Cnpj = cnpj,
                InscricaoMunicipal = inscricaoMunicipal,
                InscricaoEstadual = inscricaoEstadual,
                DocTomadorEstrangeiro = docTomadorEstrangeiro,
                SocialReason = socialReason,
                NomeFantasia = nomeFantasia,
                Tipo = tipo
            };

            mapperMock.Setup(mapper => mapper.Map<TomadorServicoViewModel>(tomadorServicoEntity)).Returns(expectedViewModel);

            // Act
            var result = await tomadorServicoAppService.GetBySocialReason(socialReason );

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        [Theory]
        [InlineData("Soluções Inovadoras Ltda")]
        [InlineData("Tecnologia Avançada S.A")]
        [InlineData("Serviços Globais Eireli")]
        public async Task GetBySocialReason_ShouldHandleNullRepositoryResult(string socialReason)
        {
            // Arrange
            var tomadorServicoRepositoryMock = new Mock<ITomadorServicoRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var tomadorServicoAppService = new TomadorServicoAppService(
                tomadorServicoRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            tomadorServicoRepositoryMock.Setup(repo => repo.GetBySocialReason(It.IsAny<string>()))
                .ReturnsAsync((TomadorServico)null); // Simulate null result from the repository

            // Act
            var result = await tomadorServicoAppService.GetBySocialReason(socialReason);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("Produtos Sustentáveis Eireli")]
        [InlineData("Desenvolvimento Rápido Ltda")]
        [InlineData("Integração Contínua S.A")]
        public async Task GetBySocialReason_ShouldHandleInvalidMappingResult(string socialReason)
        {
            // Arrange
            var tomadorServicoRepositoryMock = new Mock<ITomadorServicoRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var tomadorServicoAppService = new TomadorServicoAppService(
                tomadorServicoRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            tomadorServicoRepositoryMock.Setup(repo => repo.GetBySocialReason(It.IsAny<string>()))
                .ThrowsAsync(new ArgumentException("Invalid data")); // Simulate null result from the repository

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => tomadorServicoAppService.GetBySocialReason(socialReason));
        }

        [Theory]
        [InlineData("54.446.262/0001-03", "385.903.095.437", "289.409.128.328", "21.904.074-6", "Olivia e Sophia Filmagens Ltda", "Olivia e Sophia Filmagens", 1)]
        [InlineData("06.485.306/0001-61", "080.863.092.761", "289.088.794.339", "49.599.874-6", "Isaac e Danilo Pães e Doces Ltda", "Isaac e Danilo Pães e Doces", 2)]
        [InlineData("99.802.891/0001-67", "354.156.758.100", "519.481.861.989", "41.462.104-9", "Alessandra e Clarice Casa Noturna Ltda", "Alessandra e Clarice Casa Noturna", 3)]
        public async Task Save_ShouldReturnsCompanyViewModel(string cnpj, string inscricaoMunicipal, string inscricaoEstadual, string docTomadorEstrangeiro, string socialReason, string nomeFantasia, int tipo)
        {
            var tomadorServicoRepositoryMock = new Mock<ITomadorServicoRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var tomadorServicoAppService = new TomadorServicoAppService(
                tomadorServicoRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var createCommand = new CreateTomadorServicoCommand()
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
            await tomadorServicoAppService.Save(createCommand);

            // Assert
            tomadorServicoRepositoryMock.Verify(repo => repo.Add(It.IsAny<TomadorServico>()), Times.Once);
        }

        [Theory]
        [InlineData("54.446.262/0001-03", "385.903.095.437", "289.409.128.328", "21.904.074-6", "Olivia e Sophia Filmagens Ltda", "Olivia e Sophia Filmagens", 1)]
        [InlineData("06.485.306/0001-61", "080.863.092.761", "289.088.794.339", "49.599.874-6", "Isaac e Danilo Pães e Doces Ltda", "Isaac e Danilo Pães e Doces", 2)]
        [InlineData("99.802.891/0001-67", "354.156.758.100", "519.481.861.989", "41.462.104-9", "Alessandra e Clarice Casa Noturna Ltda", "Alessandra e Clarice Casa Noturna", 3)]
        public async Task Save_ShouldHandleNullRepositoryResult(string cnpj, string inscricaoMunicipal, string inscricaoEstadual, string docTomadorEstrangeiro, string socialReason, string nomeFantasia, int tipo)
        {
            var tomadorServicoRepositoryMock = new Mock<ITomadorServicoRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var tomadorServicoAppService = new TomadorServicoAppService(
                tomadorServicoRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var createCommand = new CreateTomadorServicoCommand()
            {
                Cnpj = cnpj,
                InscricaoMunicipal = inscricaoMunicipal,
                InscricaoEstadual = inscricaoEstadual,
                DocTomadorEstrangeiro = docTomadorEstrangeiro,
                SocialReason = socialReason,
                NomeFantasia = nomeFantasia,
                Tipo = tipo
            };

            tomadorServicoRepositoryMock.Setup(repo => repo.Add(It.IsAny<TomadorServico>())).Throws(new NullReferenceException());

            // Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => tomadorServicoAppService.Save(createCommand));
        }

        [Theory]
        [InlineData("54.446.262/0001-03", "385.903.095.437", "289.409.128.328", "21.904.074-6", "Olivia e Sophia Filmagens Ltda", "Olivia e Sophia Filmagens", 1)]
        [InlineData("06.485.306/0001-61", "080.863.092.761", "289.088.794.339", "49.599.874-6", "Isaac e Danilo Pães e Doces Ltda", "Isaac e Danilo Pães e Doces", 2)]
        [InlineData("99.802.891/0001-67", "354.156.758.100", "519.481.861.989", "41.462.104-9", "Alessandra e Clarice Casa Noturna Ltda", "Alessandra e Clarice Casa Noturna", 3)]
        public async Task Save_ShouldHandleInvalidMappingResult(string cnpj, string inscricaoMunicipal, string inscricaoEstadual, string docTomadorEstrangeiro, string socialReason, string nomeFantasia, int tipo)
        {
            // Arrange
            var tomadorServicoRepositoryMock = new Mock<ITomadorServicoRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var tomadorServicoAppService = new TomadorServicoAppService(
                tomadorServicoRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var createCommand = new CreateTomadorServicoCommand()
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
            tomadorServicoRepositoryMock.Setup(repo => repo.Add(It.IsAny<TomadorServico>()))
            .Throws(new ArgumentException("Invalid data"));

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(() => tomadorServicoAppService.Save(createCommand));
        }

    }
}
