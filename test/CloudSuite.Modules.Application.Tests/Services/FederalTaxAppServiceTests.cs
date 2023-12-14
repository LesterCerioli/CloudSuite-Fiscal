using AutoMapper;
using CloudSuite.Modules.Application.Handlers.DeclaracaoIR;
using CloudSuite.Modules.Application.Handlers.FederalTax;
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
    public class FederalTaxAppServiceTests
    {
        [Theory]
        [InlineData(2234.12, 69584.321, 9483.292, 11.234, 2200.93, true, true, true, true, true)]
        [InlineData(1234.56, 78910.111, 1213.141, 15.161, 1718.19, false, false, false, false, false)]
        [InlineData(2021.22, 2324.252, 2627.282, 29.303, 3132.33, true, false, true, false, true)]
        public async Task GetByVPIS_ShouldReturnsCompanyViewModel(decimal vpis, decimal vcofins, decimal vir, decimal vinss, decimal vcsll, bool vpisspecified, bool vConfinsspecified, bool vIrsSpecifed, bool vInssSpecifed, bool vCsllspecified)
        {
            var federalTaxRepositoryMock = new Mock<IFederalTaxRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var federalTaxAppService = new FederalTaxAppService(
                federalTaxRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var federalTaxEntity = new FederalTax(vpis, vcofins, vir, vinss, vcsll, vpisspecified, vConfinsspecified, vIrsSpecifed, vInssSpecifed, vCsllspecified);
            federalTaxRepositoryMock.Setup(repo => repo.GetByVPIS(vpis)).ReturnsAsync(federalTaxEntity);

            var expectedViewModel = new FederalTaxViewModel()
            {
                Id = federalTaxEntity.Id,
                VPIS = vpis,
                VCOFINS = vcofins,
                VIR = vir,
                VINSS = vinss,
                VCSLL = vcsll,
                VPISSpecified = vpisspecified,
                VCOFINSSpecified = vConfinsspecified,
                VIRSpecified = vpisspecified,
                VINSSSpecified = vpisspecified,
                VCSLLSpecified = vpisspecified
            };

            mapperMock.Setup(mapper => mapper.Map<FederalTaxViewModel>(federalTaxEntity)).Returns(expectedViewModel);

            // Act
            var result = await federalTaxAppService.GetByVPIS(vpis);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        [Theory]
        [InlineData(2234.12)]
        [InlineData(384237859.23)]
        [InlineData(873.21)]
        public async Task GetByVPIS_ShouldHandleNullRepositoryResult(decimal vpis)
        {
            // Arrange
            var federalTaxRepositoryMock = new Mock<IFederalTaxRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var federalTaxAppService = new FederalTaxAppService(
                federalTaxRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            federalTaxRepositoryMock.Setup(repo => repo.GetByVPIS(It.IsAny<decimal>()))
                .ReturnsAsync((FederalTax)null); // Simulate null result from the repository

            // Act
            var result = await federalTaxAppService.GetByVPIS(vpis);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData(4567.89)]
        [InlineData(1234.56)]
        [InlineData(7891.01)]
        public async Task GetByVPIS_ShouldHandleInvalidMappingResult(decimal vpis)
        {
            // Arrange
            var federalTaxRepositoryMock = new Mock<IFederalTaxRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var federalTaxAppService = new FederalTaxAppService(
                federalTaxRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            federalTaxRepositoryMock.Setup(repo => repo.GetByVPIS(It.IsAny<decimal>()))
                .ThrowsAsync(new ArgumentException("Invalid data")); // Simulate null result from the repository

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => federalTaxAppService.GetByVPIS(vpis));
        }

        [Theory]
        [InlineData(3456.78, 91011.121, 1314.151, 16.171, 1819.20, false, true, false, true, false)]
        [InlineData(1234.56, 78910.111, 1213.141, 15.161, 1718.19, false, false, false, false, false)]
        [InlineData(2021.22, 2324.252, 2627.282, 29.303, 3132.33, true, false, true, false, true)]
        public async Task GetByVCOFINS_ShouldReturnsCompanyViewModel(decimal vpis, decimal vcofins, decimal vir, decimal vinss, decimal vcsll, bool vpisspecified, bool vConfinsspecified, bool vIrsSpecifed, bool vInssSpecifed, bool vCsllspecified)
        {
            var federalTaxRepositoryMock = new Mock<IFederalTaxRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var federalTaxAppService = new FederalTaxAppService(
                federalTaxRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var federalTaxEntity = new FederalTax(vpis, vcofins, vir, vinss, vcsll, vpisspecified, vConfinsspecified, vIrsSpecifed, vInssSpecifed, vCsllspecified);
            federalTaxRepositoryMock.Setup(repo => repo.GetByVCOFINS(vcofins)).ReturnsAsync(federalTaxEntity);

            var expectedViewModel = new FederalTaxViewModel()
            {
                Id = federalTaxEntity.Id,
                VPIS = vpis,
                VCOFINS = vcofins,
                VIR = vir,
                VINSS = vinss,
                VCSLL = vcsll,
                VPISSpecified = vpisspecified,
                VCOFINSSpecified = vConfinsspecified,
                VIRSpecified = vpisspecified,
                VINSSSpecified = vpisspecified,
                VCSLLSpecified = vpisspecified
            };

            mapperMock.Setup(mapper => mapper.Map<FederalTaxViewModel>(federalTaxEntity)).Returns(expectedViewModel);

            // Act
            var result = await federalTaxAppService.GetByVCOFINS(vcofins);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        [Theory]
        [InlineData(2234.12)]
        [InlineData(384237859.23)]
        [InlineData(873.21)]
        public async Task GetByVCOFINS_ShouldHandleNullRepositoryResult(decimal vcofins)
        {
            // Arrange
            var federalTaxRepositoryMock = new Mock<IFederalTaxRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var federalTaxAppService = new FederalTaxAppService(
                federalTaxRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            federalTaxRepositoryMock.Setup(repo => repo.GetByVCOFINS(It.IsAny<decimal>()))
                .ReturnsAsync((FederalTax)null); // Simulate null result from the repository

            // Act
            var result = await federalTaxAppService.GetByVCOFINS(vcofins);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData(1234.56)]
        [InlineData(7891.01)]
        [InlineData(4567.89)]
        public async Task GetByVCOFINS_ShouldHandleInvalidMappingResult(decimal vcofins)
        {
            // Arrange
            var federalTaxRepositoryMock = new Mock<IFederalTaxRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var federalTaxAppService = new FederalTaxAppService(
                federalTaxRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            federalTaxRepositoryMock.Setup(repo => repo.GetByVCOFINS(It.IsAny<decimal>()))
                .ThrowsAsync(new ArgumentException("Invalid data")); // Simulate null result from the repository

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => federalTaxAppService.GetByVCOFINS(vcofins));
        }

        [Theory]
        [InlineData(2234.12, 69584.321, 9483.292, 11.234, 2200.93, true, true, true, true, true)]
        [InlineData(6234.43, 345.21, 384237859.23, 342.12, 83.21, false, true, false, true, false)]
        [InlineData(22.30, 3321.32, 321.95, 873.21, 55.32, false, false, false, false, false)]
        public async Task GetByVIR_ShouldReturnsCompanyViewModel(decimal vpis, decimal vcofins, decimal vir, decimal vinss, decimal vcsll, bool vpisspecified, bool vConfinsspecified, bool vIrsSpecifed, bool vInssSpecifed, bool vCsllspecified)
        {
            var federalTaxRepositoryMock = new Mock<IFederalTaxRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var federalTaxAppService = new FederalTaxAppService(
                federalTaxRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var federalTaxEntity = new FederalTax(vpis, vcofins, vir, vinss, vcsll, vpisspecified, vConfinsspecified, vIrsSpecifed, vInssSpecifed, vCsllspecified);
            federalTaxRepositoryMock.Setup(repo => repo.GetByVIR(vir)).ReturnsAsync(federalTaxEntity);

            var expectedViewModel = new FederalTaxViewModel()
            {
                Id = federalTaxEntity.Id,
                VPIS = vpis,
                VCOFINS = vcofins,
                VIR = vir,
                VINSS = vinss,
                VCSLL = vcsll,
                VPISSpecified = vpisspecified,
                VCOFINSSpecified = vConfinsspecified,
                VIRSpecified = vpisspecified,
                VINSSSpecified = vpisspecified,
                VCSLLSpecified = vpisspecified
            };

            mapperMock.Setup(mapper => mapper.Map<FederalTaxViewModel>(federalTaxEntity)).Returns(expectedViewModel);

            // Act
            var result = await federalTaxAppService.GetByVIR(vir);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        [Theory]
        [InlineData(1234.56)]
        [InlineData(7891.01)]
        [InlineData(4567.89)]
        public async Task GetByVIR_ShouldHandleNullRepositoryResult(decimal vir)
        {
            // Arrange
            var federalTaxRepositoryMock = new Mock<IFederalTaxRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var federalTaxAppService = new FederalTaxAppService(
                federalTaxRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            federalTaxRepositoryMock.Setup(repo => repo.GetByVIR(It.IsAny<decimal>()))
                .ReturnsAsync((FederalTax)null); // Simulate null result from the repository

            // Act
            var result = await federalTaxAppService.GetByVIR(vir);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData(3210.98)]
        [InlineData(7654.32)]
        [InlineData(1098.76)]
        public async Task GetByVIR_ShouldHandleInvalidMappingResult(decimal vir)
        {
            // Arrange
            var federalTaxRepositoryMock = new Mock<IFederalTaxRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var federalTaxAppService = new FederalTaxAppService(
                federalTaxRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            federalTaxRepositoryMock.Setup(repo => repo.GetByVIR(It.IsAny<decimal>()))
                .ThrowsAsync(new ArgumentException("Invalid data")); // Simulate null result from the repository

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => federalTaxAppService.GetByVIR(vir));
        }

        [Theory]
        [InlineData(1234.56, 78910.111, 1213.141, 15.161, 1718.19, false, false, false, false, false)]
        [InlineData(2021.22, 2324.252, 2627.282, 29.303, 3132.33, true, false, true, false, true)]
        [InlineData(3456.78, 91011.121, 1314.151, 16.171, 1819.20, false, true, false, true, false)]
        public async Task GetByVINSS_ShouldReturnsCompanyViewModel(decimal vpis, decimal vcofins, decimal vir, decimal vinss, decimal vcsll, bool vpisspecified, bool vConfinsspecified, bool vIrsSpecifed, bool vInssSpecifed, bool vCsllspecified)
        {
            var federalTaxRepositoryMock = new Mock<IFederalTaxRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var federalTaxAppService = new FederalTaxAppService(
                federalTaxRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var federalTaxEntity = new FederalTax(vpis, vcofins, vir, vinss, vcsll, vpisspecified, vConfinsspecified, vIrsSpecifed, vInssSpecifed, vCsllspecified);
            federalTaxRepositoryMock.Setup(repo => repo.GetByVINSS(vinss)).ReturnsAsync(federalTaxEntity);

            var expectedViewModel = new FederalTaxViewModel()
            {
                Id = federalTaxEntity.Id,
                VPIS = vpis,
                VCOFINS = vcofins,
                VIR = vir,
                VINSS = vinss,
                VCSLL = vcsll,
                VPISSpecified = vpisspecified,
                VCOFINSSpecified = vConfinsspecified,
                VIRSpecified = vpisspecified,
                VINSSSpecified = vpisspecified,
                VCSLLSpecified = vpisspecified
            };

            mapperMock.Setup(mapper => mapper.Map<FederalTaxViewModel>(federalTaxEntity)).Returns(expectedViewModel);

            // Act
            var result = await federalTaxAppService.GetByVINSS(vinss);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        [Theory]
        [InlineData(2234.12)]
        [InlineData(384237859.23)]
        [InlineData(873.21)]
        public async Task GetByVINSS_ShouldHandleNullRepositoryResult(decimal vir)
        {
            // Arrange
            var federalTaxRepositoryMock = new Mock<IFederalTaxRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var federalTaxAppService = new FederalTaxAppService(
                federalTaxRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            federalTaxRepositoryMock.Setup(repo => repo.GetByVINSS(It.IsAny<decimal>()))
                .ReturnsAsync((FederalTax)null); // Simulate null result from the repository

            // Act
            var result = await federalTaxAppService.GetByVINSS(vir);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData(2234.12)]
        [InlineData(384237859.23)]
        [InlineData(873.21)]
        public async Task GetByVINSS_ShouldHandleInvalidMappingResult(decimal vinss)
        {
            // Arrange
            var federalTaxRepositoryMock = new Mock<IFederalTaxRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var federalTaxAppService = new FederalTaxAppService(
                federalTaxRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            federalTaxRepositoryMock.Setup(repo => repo.GetByVINSS(It.IsAny<decimal>()))
                .ThrowsAsync(new ArgumentException("Invalid data")); // Simulate null result from the repository

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => federalTaxAppService.GetByVINSS(vinss));
        }

        [Theory]
        [InlineData(2021.22, 2324.252, 2627.282, 29.303, 3132.33, true, false, true, false, true)]
        [InlineData(3456.78, 91011.121, 1314.151, 16.171, 1819.20, false, true, false, true, false)]
        [InlineData(9876.54, 32109.876, 5432.109, 21.098, 7654.32, true, true, false, false, true)]
        public async Task GetByVCSLL_ShouldReturnsCompanyViewModel(decimal vpis, decimal vcofins, decimal vir, decimal vinss, decimal vcsll, bool vpisspecified, bool vConfinsspecified, bool vIrsSpecifed, bool vInssSpecifed, bool vCsllspecified)
        {
            var federalTaxRepositoryMock = new Mock<IFederalTaxRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var federalTaxAppService = new FederalTaxAppService(
                federalTaxRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var federalTaxEntity = new FederalTax(vpis, vcofins, vir, vinss, vcsll, vpisspecified, vConfinsspecified, vIrsSpecifed, vInssSpecifed, vCsllspecified);
            federalTaxRepositoryMock.Setup(repo => repo.GetByVCSLL(vcsll)).ReturnsAsync(federalTaxEntity);

            var expectedViewModel = new FederalTaxViewModel()
            {
                Id = federalTaxEntity.Id,
                VPIS = vpis,
                VCOFINS = vcofins,
                VIR = vir,
                VINSS = vinss,
                VCSLL = vcsll,
                VPISSpecified = vpisspecified,
                VCOFINSSpecified = vConfinsspecified,
                VIRSpecified = vpisspecified,
                VINSSSpecified = vpisspecified,
                VCSLLSpecified = vpisspecified
            };

            mapperMock.Setup(mapper => mapper.Map<FederalTaxViewModel>(federalTaxEntity)).Returns(expectedViewModel);

            // Act
            var result = await federalTaxAppService.GetByVCSLL(vcsll);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        [Theory]
        [InlineData(1234.56)]
        [InlineData(7891.01)]
        [InlineData(4567.89)]
        public async Task GetByVCSLL_ShouldHandleNullRepositoryResult(decimal vcsll)
        {
            // Arrange
            var federalTaxRepositoryMock = new Mock<IFederalTaxRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var federalTaxAppService = new FederalTaxAppService(
                federalTaxRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            federalTaxRepositoryMock.Setup(repo => repo.GetByVCSLL(It.IsAny<decimal>()))
                .ReturnsAsync((FederalTax)null); // Simulate null result from the repository

            // Act
            var result = await federalTaxAppService.GetByVCSLL(vcsll);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData(3210.98)]
        [InlineData(7654.32)]
        [InlineData(1098.76)]
        public async Task GetByVCSLL_ShouldHandleInvalidMappingResult(decimal vcsll)
        {
            // Arrange
            var federalTaxRepositoryMock = new Mock<IFederalTaxRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var federalTaxAppService = new FederalTaxAppService(
                federalTaxRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            federalTaxRepositoryMock.Setup(repo => repo.GetByVCSLL(It.IsAny<decimal>()))
                .ThrowsAsync(new ArgumentException("Invalid data")); // Simulate null result from the repository

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => federalTaxAppService.GetByVCSLL(vcsll));
        }

        [Theory]
        [InlineData(6677.88, 9911.001, 2233.445, 41.667, 8899.11, true, false, false, true, false)]
        [InlineData(2233.44, 5566.777, 8899.111, 51.223, 3344.55, false, false, true, true, false)]
        [InlineData(6677.88, 9911.001, 2233.445, 61.667, 8899.11, true, true, false, false, true)]
        public async Task Save_ShouldReturnsCompanyViewModel(decimal vpis, decimal vcofins, decimal vir, decimal vinss, decimal vcsll, bool vpisspecified, bool vConfinsspecified, bool vIrsSpecifed, bool vInssSpecifed, bool vCsllspecified)
        {
            var federalTaxRepositoryMock = new Mock<IFederalTaxRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var federalTaxAppService = new FederalTaxAppService(
                federalTaxRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var createCommand = new CreateFederalTaxCommand()
            {
                VPIS = vpis,
                VCOFINS = vcofins,
                VIR = vir,
                VINSS = vinss,
                VCSLL = vcsll,
                VPISSpecified = vpisspecified,
                VCOFINSSpecified = vConfinsspecified,
                VIRSpecified = vIrsSpecifed,
                VINSSSpecified = vInssSpecifed,
                VCSLLSpecified = vCsllspecified
            };

            // Act
            await federalTaxAppService.Save(createCommand);

            // Assert
            federalTaxRepositoryMock.Verify(repo => repo.Add(It.IsAny<FederalTax>()), Times.Once);
        }

        [Theory]
        [InlineData(2233.44, 5566.777, 8899.111, 71.223, 3344.55, false, true, true, false, false)]
        [InlineData(6677.88, 9911.001, 2233.445, 81.667, 8899.11, true, false, true, false, true)]
        [InlineData(2233.44, 5566.777, 8899.111, 91.223, 3344.55, false, true, false, true, false)]
        public async Task Save_ShouldHandleNullRepositoryResult(decimal vpis, decimal vcofins, decimal vir, decimal vinss, decimal vcsll, bool vpisspecified, bool vConfinsspecified, bool vIrsSpecifed, bool vInssSpecifed, bool vCsllspecified)
        {
            var federalTaxRepositoryMock = new Mock<IFederalTaxRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var federalTaxAppService = new FederalTaxAppService(
                federalTaxRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var createCommand = new CreateFederalTaxCommand()
            {
                VPIS = vpis,
                VCOFINS = vcofins,
                VIR = vir,
                VINSS = vinss,
                VCSLL = vcsll,
                VPISSpecified = vpisspecified,
                VCOFINSSpecified = vConfinsspecified,
                VIRSpecified = vIrsSpecifed,
                VINSSSpecified = vInssSpecifed,
                VCSLLSpecified = vCsllspecified
            };

            federalTaxRepositoryMock.Setup(repo => repo.Add(It.IsAny<FederalTax>())).Throws(new NullReferenceException());

            // Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => federalTaxAppService.Save(createCommand));
        }

        [Theory]
        [InlineData(6677.88, 9911.001, 2233.445, 101.667, 8899.11, true, true, true, false, false)]
        [InlineData(2233.44, 5566.777, 8899.111, 111.223, 3344.55, false, false, false, true, true)]
        [InlineData(6677.88, 9911.001, 2233.445, 121.667, 8899.11, true, true, false, true, false)]
        public async Task Save_ShouldHandleInvalidMappingResult(decimal vpis, decimal vcofins, decimal vir, decimal vinss, decimal vcsll, bool vpisspecified, bool vConfinsspecified, bool vIrsSpecifed, bool vInssSpecifed, bool vCsllspecified)
        {
            // Arrange
            var federalTaxRepositoryMock = new Mock<IFederalTaxRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var federalTaxAppService = new FederalTaxAppService(
                federalTaxRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var createCommand = new CreateFederalTaxCommand()
            {
                VPIS = vpis,
                VCOFINS = vcofins,
                VIR = vir,
                VINSS = vinss,
                VCSLL = vcsll,
                VPISSpecified = vpisspecified,
                VCOFINSSpecified = vConfinsspecified,
                VIRSpecified = vIrsSpecifed,
                VINSSSpecified = vInssSpecifed,
                VCSLLSpecified = vCsllspecified
            };

            // Act       
            federalTaxRepositoryMock.Setup(repo => repo.Add(It.IsAny<FederalTax>()))
            .Throws(new ArgumentException("Invalid data"));

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(() => federalTaxAppService.Save(createCommand));
        }

    }

}
