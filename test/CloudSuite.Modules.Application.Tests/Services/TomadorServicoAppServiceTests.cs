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
        [InlineData("16.241.879/0001-01", "123.456.789.01", "987.654.321.10", "12.345.678-9", "Empresa Exemplo Ltda", "Empresa Exemplo", 2)]
        [InlineData("07.096.138/0001-85", "234.567.890.12", "876.543.210.98", "23.456.789-0", "Exemplo Comércio Ltda", "Exemplo Comércio", 3)]
        [InlineData("58.613.029/0001-29", "345.678.901.23", "765.432.109.87", "34.567.890-1", "Comércio Exemplo Ltda", "Comércio Exemplo", 4)]
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
        [InlineData("79.823.202/0001-93")]
        [InlineData("15.040.196/0001-15")]
        [InlineData("27.719.020/0001-73")]
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
        [InlineData("33.712.183/0001-27")]
        [InlineData("91.127.707/0001-19")]
        [InlineData("94.172.886/0001-02")]
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
        [InlineData("05.360.030/0001-22", "385.903.095.437", "289.409.128.328", "21.904.074-6", "Olivia e Sophia Filmagens Ltda", "Olivia e Sophia Filmagens", 1)]
        [InlineData("41.524.399/0001-97", "080.863.092.761", "289.088.794.339", "49.599.874-6", "Isaac e Danilo Pães e Doces Ltda", "Isaac e Danilo Pães e Doces", 2)]
        [InlineData("43.342.939/0001-47", "354.156.758.100", "519.481.861.989", "41.462.104-9", "Alessandra e Clarice Casa Noturna Ltda", "Alessandra e Clarice Casa Noturna", 3)]
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
        [InlineData("67.933.719/0001-56", "123.456.789.01", "987.654.321.10", "12.345.678-9", "Empresa Exemplo Ltda", "Empresa Exemplo", 11)]
        [InlineData("00.748.903/0001-55", "234.567.890.12", "876.543.210.98", "23.456.789-0", "Exemplo Comércio Ltda", "Exemplo Comércio", 12)]
        [InlineData("86.135.585/0001-72", "345.678.901.23", "765.432.109.87", "34.567.890-1", "Comércio Exemplo Ltda", "Comércio Exemplo", 13)]
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
        [InlineData("63.845.970/0001-08", "789.012.345.67", "321.098.765.43", "78.901.234-5", "Comércio Ltda Exemplo", "Comércio Ltda", 8)]
        [InlineData("27.073.895/0001-40", "890.123.456.78", "210.987.654.32", "89.012.345-6", "Ltda Comércio Exemplo", "Ltda Comércio", 9)]
        [InlineData("67.507.784/0001-10", "901.234.567.89", "109.876.543.21", "90.123.456-7", "Exemplo Ltda Comércio", "Exemplo Ltda", 10)]
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
        [InlineData("96.686.461/0001-39", "456.789.012.34", "654.321.098.76", "45.678.901-2", "Ltda Exemplo Empresa", "Ltda Exemplo", 5)]
        [InlineData("33.676.375/0001-25", "567.890.123.45", "543.210.987.65", "56.789.012-3", "Empresa Ltda Exemplo", "Empresa Ltda", 6)]
        [InlineData("93.074.859/0001-26", "678.901.234.56", "432.109.876.54", "67.890.123-4", "Exemplo Empresa Ltda", "Exemplo Empresa", 7)]
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
