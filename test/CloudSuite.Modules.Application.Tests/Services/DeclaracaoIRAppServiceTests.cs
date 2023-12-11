using AutoMapper;
using CloudSuite.Modules.Application.Handlers.DeclaracaoIR;
using CloudSuite.Modules.Application.Services.Implementation;
using CloudSuite.Modules.Application.ViewModels;
using CloudSuite.Modules.Common.ValueObjects;
using CloudSuite.Modules.Domain.Contracts;
using CloudSuite.Modules.Domain.Models;
using Moq;
using NetDevPack.Mediator;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CloudSuite.Modules.Application.Tests.Services
{
    public class DeclaracaoIRAppServiceTests
    {
        [Theory]
        [InlineData("783628093873837432", "02.692.316/0001-17", "284.144.750-27", "ERP SERVICES", "John doe", 2200.93, 27638.22, 89273982.66, 5555.55, "9000.21", 5000.00, 10.00)]
        [InlineData("123456789012345678", "45.836.508/0001-62", "145.914.340-01", "WEB DESIGN", "Maria silva", 3300.93, 27638.22, 89273982.66, 5555.55, "9000.21", 5000.00, 10.00)]
        [InlineData("987654321098765432", "38.270.839/0001-12", "949.828.900-05", "MARKETING", "José carlos", 4400.93, 27638.22, 89273982.66, 5555.55, "9000.21", 5000.00, 10.00)]
        public async Task GetByDeclaracaoNumero_ShouldReturnsCompanyViewModel(string declaracaoNumero, string cnpj, string cpf, string companyName, string businessHeader, decimal totalIncome, decimal socialSecurity, decimal complementContribution, decimal alimony, string taxWithheld, decimal paidValueToBusiness, decimal profitsDividends)
        {
            var declaracaoIRRepositoryMock = new Mock<IDeclaracaoIRRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var declaracaoIRAppService = new DeclaracaoIRAppService(
                declaracaoIRRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var declaracaoIREntity = new DeclaracaoIR(declaracaoNumero, new Cnpj(cnpj), new Cpf(cpf), companyName, businessHeader, totalIncome, socialSecurity, complementContribution, alimony, taxWithheld, paidValueToBusiness, profitsDividends);
            declaracaoIRRepositoryMock.Setup(repo => repo.GetByDeclaracaoNumero(declaracaoNumero)).ReturnsAsync(declaracaoIREntity);

            var expectedViewModel = new DeclaracaoIRViewModel()
            {
                Id = declaracaoIREntity.Id,
                DeclaracaoNumero = declaracaoNumero,
                CompanyName = companyName,
                Cnpj = cnpj,
                Cpf = cpf,
                BusinessHeader = businessHeader,
                TotalIncome = totalIncome,
                SocialSecurity = socialSecurity,
                ComplementContribution = complementContribution,
                Alimony = alimony,
                TaxWithheld = taxWithheld,
                PaidValueToBusiness = paidValueToBusiness,
                ProfitsDividends = profitsDividends
            };

            mapperMock.Setup(mapper => mapper.Map<DeclaracaoIRViewModel>(declaracaoIREntity)).Returns(expectedViewModel);

            // Act
            var result = await declaracaoIRAppService.GetByDeclaracaoNumero(declaracaoNumero);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        [Theory]
        [InlineData("783628093873837432")]
        [InlineData("897908437243446387")]
        [InlineData("332730263493456234")]
        public async Task GetByDeclaracaoNumero_ShouldHandleNullRepositoryResult(string declaracaoNumero)
        {
            // Arrange
            var declaracaoIRRepositoryMock = new Mock<IDeclaracaoIRRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var declaracaoIRAppService = new DeclaracaoIRAppService(
                declaracaoIRRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            declaracaoIRRepositoryMock.Setup(repo => repo.GetByDeclaracaoNumero(It.IsAny<string>()))
                .ReturnsAsync((DeclaracaoIR)null); // Simulate null result from the repository

            // Act
            var result = await declaracaoIRAppService.GetByDeclaracaoNumero(declaracaoNumero);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("783628093873837432")]
        [InlineData("897908437243446387")]
        [InlineData("332730263493456234")]
        public async Task GetByDeclaracaoNumero_ShouldHandleInvalidMappingResult(string declaracaoNumero)
        {
            // Arrange
            var declaracaoIRRepositoryMock = new Mock<IDeclaracaoIRRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var declaracaoIRAppService = new DeclaracaoIRAppService(
                declaracaoIRRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            declaracaoIRRepositoryMock.Setup(repo => repo.GetByDeclaracaoNumero(It.IsAny<string>()))
                .ThrowsAsync(new ArgumentException("Invalid data")); // Simulate null result from the repository

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => declaracaoIRAppService.GetByDeclaracaoNumero(declaracaoNumero));
        }

        [Theory]
        [InlineData("783628093873837432", "02.692.316/0001-17", "284.144.750-27", "ERP SERVICES", "John doe", 2200.93, 27638.22, 89273982.66, 5555.55, "9000.21", 5000.00, 10.00)]
        [InlineData("123456789012345678", "45.836.508/0001-62", "145.914.340-01", "WEB DESIGN", "Maria silva", 3300.93, 27638.22, 89273982.66, 5555.55, "9000.21", 5000.00, 10.00)]
        [InlineData("987654321098765432", "38.270.839/0001-12", "949.828.900-05", "MARKETING", "José carlos", 4400.93, 27638.22, 89273982.66, 5555.55, "9000.21", 5000.00, 10.00)]
        public async Task GetByCnpj_ShouldReturnsCompanyViewModel(string declaracaoNumero, string cnpj, string cpf, string companyName, string businessHeader, decimal totalIncome, decimal socialSecurity, decimal complementContribution, decimal alimony, string taxWithheld, decimal paidValueToBusiness, decimal profitsDividends)
        {
            var declaracaoIRRepositoryMock = new Mock<IDeclaracaoIRRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var declaracaoIRAppService = new DeclaracaoIRAppService(
                declaracaoIRRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var declaracaoIREntity = new DeclaracaoIR(declaracaoNumero, new Cnpj(cnpj), new Cpf(cpf), companyName, businessHeader, totalIncome, socialSecurity, complementContribution, alimony, taxWithheld, paidValueToBusiness, profitsDividends);
            declaracaoIRRepositoryMock.Setup(repo => repo.GetByCnpj(cnpj)).ReturnsAsync(declaracaoIREntity);

            var expectedViewModel = new DeclaracaoIRViewModel()
            {
                Id = declaracaoIREntity.Id,
                DeclaracaoNumero = declaracaoNumero,
                CompanyName = companyName,
                Cnpj = cnpj,
                Cpf = cpf,
                BusinessHeader = businessHeader,
                TotalIncome = totalIncome,
                SocialSecurity = socialSecurity,
                ComplementContribution = complementContribution,
                Alimony = alimony,
                TaxWithheld = taxWithheld,
                PaidValueToBusiness = paidValueToBusiness,
                ProfitsDividends = profitsDividends
            };

            mapperMock.Setup(mapper => mapper.Map<DeclaracaoIRViewModel>(declaracaoIREntity)).Returns(expectedViewModel);

            // Act
            var result = await declaracaoIRAppService.GetByCnpj(cnpj);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        [Theory]
        [InlineData("51.269.727/0001-00")]
        [InlineData("07.017.643/0001-97")]
        [InlineData("05.363.524/0001-60")]
        public async Task GetByCnpj_ShouldHandleNullRepositoryResult(string cnpj)
        {
            // Arrange
            var declaracaoIRRepositoryMock = new Mock<IDeclaracaoIRRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var declaracaoIRAppService = new DeclaracaoIRAppService(
                declaracaoIRRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            declaracaoIRRepositoryMock.Setup(repo => repo.GetByCnpj(It.IsAny<Cnpj>()))
                .ReturnsAsync((DeclaracaoIR)null); // Simulate null result from the repository

            // Act
            var result = await declaracaoIRAppService.GetByCnpj(cnpj);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("70.798.790/0001-88")]
        [InlineData("26.419.285/0001-93")]
        [InlineData("90.285.713/0001-31")]
        public async Task GetByCnpj_ShouldHandleInvalidMappingResult(string cnpj)
        {
            // Arrange
            var declaracaoIRRepositoryMock = new Mock<IDeclaracaoIRRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var declaracaoIRAppService = new DeclaracaoIRAppService(
                declaracaoIRRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            declaracaoIRRepositoryMock.Setup(repo => repo.GetByCnpj(It.IsAny<Cnpj>()))
                .ThrowsAsync(new ArgumentException("Invalid data")); // Simulate null result from the repository

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => declaracaoIRAppService.GetByCnpj(cnpj));
        }

        [Theory]
        [InlineData("783628093873837432", "02.692.316/0001-17", "284.144.750-27", "ERP SERVICES", "John doe", 2200.93, 27638.22, 89273982.66, 5555.55, "9000.21", 5000.00, 10.00)]
        [InlineData("123456789012345678", "45.836.508/0001-62", "145.914.340-01", "WEB DESIGN", "Maria silva", 3300.93, 27638.22, 89273982.66, 5555.55, "9000.21", 5000.00, 10.00)]
        [InlineData("987654321098765432", "38.270.839/0001-12", "949.828.900-05", "MARKETING", "José carlos", 4400.93, 27638.22, 89273982.66, 5555.55, "9000.21", 5000.00, 10.00)]
        public async Task GetByCpf_ShouldReturnsCompanyViewModel(string declaracaoNumero, string cnpj, string cpf, string companyName, string businessHeader, decimal totalIncome, decimal socialSecurity, decimal complementContribution, decimal alimony, string taxWithheld, decimal paidValueToBusiness, decimal profitsDividends)
        {
            var declaracaoIRRepositoryMock = new Mock<IDeclaracaoIRRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var declaracaoIRAppService = new DeclaracaoIRAppService(
                declaracaoIRRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var declaracaoIREntity = new DeclaracaoIR(declaracaoNumero, new Cnpj(cnpj), new Cpf(cpf), companyName, businessHeader, totalIncome, socialSecurity, complementContribution, alimony, taxWithheld, paidValueToBusiness, profitsDividends);
            declaracaoIRRepositoryMock.Setup(repo => repo.GetByCpf(new Cpf(cpf))).ReturnsAsync(declaracaoIREntity);

            var expectedViewModel = new DeclaracaoIRViewModel()
            {
                Id = declaracaoIREntity.Id,
                DeclaracaoNumero = declaracaoNumero,
                CompanyName = companyName,
                Cnpj = cnpj,
                Cpf = cpf,
                BusinessHeader = businessHeader,
                TotalIncome = totalIncome,
                SocialSecurity = socialSecurity,
                ComplementContribution = complementContribution,
                Alimony = alimony,
                TaxWithheld = taxWithheld,
                PaidValueToBusiness = paidValueToBusiness,
                ProfitsDividends = profitsDividends
            };

            mapperMock.Setup(mapper => mapper.Map<DeclaracaoIRViewModel>(declaracaoIREntity)).Returns(expectedViewModel);

            // Act
            var result = await declaracaoIRAppService.GetByCpf(new Cpf(cpf));

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        [Theory]
        [InlineData("436.610.750-39")]
        [InlineData("416.975.770-08")]
        [InlineData("376.012.410-09")]
        public async Task GetByCpf_ShouldHandleNullRepositoryResult(string cpf)
        {
            // Arrange
            var declaracaoIRRepositoryMock = new Mock<IDeclaracaoIRRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var declaracaoIRAppService = new DeclaracaoIRAppService(
                declaracaoIRRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            declaracaoIRRepositoryMock.Setup(repo => repo.GetByCpf(It.IsAny<Cpf>()))
                .ReturnsAsync((DeclaracaoIR)null); // Simulate null result from the repository

            // Act
            var result = await declaracaoIRAppService.GetByCpf(new Cpf(cpf));

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("70.798.790/0001-88")]
        [InlineData("26.419.285/0001-93")]
        [InlineData("90.285.713/0001-31")]
        public async Task GetByCpf_ShouldHandleInvalidMappingResult(string cpf)
        {
            // Arrange
            var declaracaoIRRepositoryMock = new Mock<IDeclaracaoIRRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var declaracaoIRAppService = new DeclaracaoIRAppService(
                declaracaoIRRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            declaracaoIRRepositoryMock.Setup(repo => repo.GetByCpf(It.IsAny<Cpf>()))
                .ThrowsAsync(new ArgumentException("Invalid data")); // Simulate null result from the repository

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => declaracaoIRAppService.GetByCpf(new Cpf(cpf)));
        }

        [Theory]
        [InlineData("783628093873837432", "02.692.316/0001-17", "284.144.750-27", "ERP SERVICES", "John doe", 2200.93, 27638.22, 89273982.66, 5555.55, "9000.21", 5000.00, 10.00)]
        [InlineData("123456789012345678", "45.836.508/0001-62", "145.914.340-01", "WEB DESIGN", "Maria silva", 3300.93, 27638.22, 89273982.66, 5555.55, "9000.21", 5000.00, 10.00)]
        [InlineData("987654321098765432", "38.270.839/0001-12", "949.828.900-05", "MARKETING", "José carlos", 4400.93, 27638.22, 89273982.66, 5555.55, "9000.21", 5000.00, 10.00)]
        public async Task GetByTotalIncome_ShouldReturnsCompanyViewModel(string declaracaoNumero, string cnpj, string cpf, string companyName, string businessHeader, decimal totalIncome, decimal socialSecurity, decimal complementContribution, decimal alimony, string taxWithheld, decimal paidValueToBusiness, decimal profitsDividends)
        {
            var declaracaoIRRepositoryMock = new Mock<IDeclaracaoIRRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var declaracaoIRAppService = new DeclaracaoIRAppService(
                declaracaoIRRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var declaracaoIREntity = new DeclaracaoIR(declaracaoNumero, new Cnpj(cnpj), new Cpf(cpf), companyName, businessHeader, totalIncome, socialSecurity, complementContribution, alimony, taxWithheld, paidValueToBusiness, profitsDividends);
            declaracaoIRRepositoryMock.Setup(repo => repo.GetByTotalIncome(totalIncome)).ReturnsAsync(declaracaoIREntity);

            var expectedViewModel = new DeclaracaoIRViewModel()
            {
                Id = declaracaoIREntity.Id,
                DeclaracaoNumero = declaracaoNumero,
                CompanyName = companyName,
                Cnpj = cnpj,
                Cpf = cpf,
                BusinessHeader = businessHeader,
                TotalIncome = totalIncome,
                SocialSecurity = socialSecurity,
                ComplementContribution = complementContribution,
                Alimony = alimony,
                TaxWithheld = taxWithheld,
                PaidValueToBusiness = paidValueToBusiness,
                ProfitsDividends = profitsDividends
            };

            mapperMock.Setup(mapper => mapper.Map<DeclaracaoIRViewModel>(declaracaoIREntity)).Returns(expectedViewModel);

            // Act
            var result = await declaracaoIRAppService.GetByTotalIncome(totalIncome);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        [Theory]
        [InlineData(22.10)]
        [InlineData(55.23)]
        [InlineData(11.23)]
        public async Task GetByTotalIncome_ShouldHandleNullRepositoryResult(decimal totalIncome)
        {
            // Arrange
            var declaracaoIRRepositoryMock = new Mock<IDeclaracaoIRRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var declaracaoIRAppService = new DeclaracaoIRAppService(
                declaracaoIRRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            declaracaoIRRepositoryMock.Setup(repo => repo.GetByTotalIncome(It.IsAny<decimal>()))
                .ReturnsAsync((DeclaracaoIR)null); // Simulate null result from the repository

            // Act
            var result = await declaracaoIRAppService.GetByTotalIncome(totalIncome);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData(84.32)]
        [InlineData(11.09)]
        [InlineData(22.11)]
        public async Task GetByTotalIncome_ShouldHandleInvalidMappingResult(decimal totalIncome)
        {
            // Arrange
            var declaracaoIRRepositoryMock = new Mock<IDeclaracaoIRRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var declaracaoIRAppService = new DeclaracaoIRAppService(
                declaracaoIRRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            declaracaoIRRepositoryMock.Setup(repo => repo.GetByTotalIncome(It.IsAny<decimal>()))
                .ThrowsAsync(new ArgumentException("Invalid data")); // Simulate null result from the repository

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => declaracaoIRAppService.GetByTotalIncome(totalIncome));
        }

        [Theory]
        [InlineData("783628093873837432", "02.692.316/0001-17", "284.144.750-27", "ERP SERVICES", "John doe", 2200.93, 27638.22, 89273982.66, 5555.55, "9000.21", 5000.00, 10.00)]
        [InlineData("123456789012345678", "45.836.508/0001-62", "145.914.340-01", "WEB DESIGN", "Maria silva", 3300.93, 27638.22, 89273982.66, 5555.55, "9000.21", 5000.00, 10.00)]
        [InlineData("987654321098765432", "38.270.839/0001-12", "949.828.900-05", "MARKETING", "José carlos", 4400.93, 27638.22, 89273982.66, 5555.55, "9000.21", 5000.00, 10.00)]
        public async Task GetByAlimony_ShouldReturnsCompanyViewModel(string declaracaoNumero, string cnpj, string cpf, string companyName, string businessHeader, decimal totalIncome, decimal socialSecurity, decimal complementContribution, decimal alimony, string taxWithheld, decimal paidValueToBusiness, decimal profitsDividends)
        {
            var declaracaoIRRepositoryMock = new Mock<IDeclaracaoIRRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var declaracaoIRAppService = new DeclaracaoIRAppService(
                declaracaoIRRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var declaracaoIREntity = new DeclaracaoIR(declaracaoNumero, new Cnpj(cnpj), new Cpf(cpf), companyName, businessHeader, totalIncome, socialSecurity, complementContribution, alimony, taxWithheld, paidValueToBusiness, profitsDividends);
            declaracaoIRRepositoryMock.Setup(repo => repo.GetByAlimony(alimony)).ReturnsAsync(declaracaoIREntity);

            var expectedViewModel = new DeclaracaoIRViewModel()
            {
                Id = declaracaoIREntity.Id,
                DeclaracaoNumero = declaracaoNumero,
                CompanyName = companyName,
                Cnpj = cnpj,
                Cpf = cpf,
                BusinessHeader = businessHeader,
                TotalIncome = totalIncome,
                SocialSecurity = socialSecurity,
                ComplementContribution = complementContribution,
                Alimony = alimony,
                TaxWithheld = taxWithheld,
                PaidValueToBusiness = paidValueToBusiness,
                ProfitsDividends = profitsDividends
            };

            mapperMock.Setup(mapper => mapper.Map<DeclaracaoIRViewModel>(declaracaoIREntity)).Returns(expectedViewModel);

            // Act
            var result = await declaracaoIRAppService.GetByAlimony(alimony);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        [Theory]
        [InlineData(22.10)]
        [InlineData(55.23)]
        [InlineData(11.23)]
        public async Task GetByAlimony_ShouldHandleNullRepositoryResult(decimal alimony)
        {
            // Arrange
            var declaracaoIRRepositoryMock = new Mock<IDeclaracaoIRRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var declaracaoIRAppService = new DeclaracaoIRAppService(
                declaracaoIRRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            declaracaoIRRepositoryMock.Setup(repo => repo.GetByAlimony(It.IsAny<decimal>()))
                .ReturnsAsync((DeclaracaoIR)null); // Simulate null result from the repository

            // Act
            var result = await declaracaoIRAppService.GetByAlimony(alimony);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData(84.32)]
        [InlineData(11.09)]
        [InlineData(22.11)]
        public async Task GetByAlimony_ShouldHandleInvalidMappingResult(decimal alimony)
        {
            // Arrange
            var declaracaoIRRepositoryMock = new Mock<IDeclaracaoIRRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var declaracaoIRAppService = new DeclaracaoIRAppService(
                declaracaoIRRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            declaracaoIRRepositoryMock.Setup(repo => repo.GetByAlimony(It.IsAny<decimal>()))
                .ThrowsAsync(new ArgumentException("Invalid data")); // Simulate null result from the repository

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => declaracaoIRAppService.GetByAlimony(alimony));
        }

        [Theory]
        [InlineData("783628093873837432", "02.692.316/0001-17", "284.144.750-27", "ERP SERVICES", "John doe", 2200.93, 27638.22, 89273982.66, 5555.55, "9000.21", 5000.00, 10.00)]
        [InlineData("123456789012345678", "45.836.508/0001-62", "145.914.340-01", "WEB DESIGN", "Maria silva", 3300.93, 27638.22, 89273982.66, 5555.55, "9000.21", 5000.00, 10.00)]
        [InlineData("987654321098765432", "38.270.839/0001-12", "949.828.900-05", "MARKETING", "José carlos", 4400.93, 27638.22, 89273982.66, 5555.55, "9000.21", 5000.00, 10.00)]
        public async Task GetByPaidValueToBusiness_ShouldReturnsCompanyViewModel(string declaracaoNumero, string cnpj, string cpf, string companyName, string businessHeader, decimal totalIncome, decimal socialSecurity, decimal complementContribution, decimal alimony, string taxWithheld, decimal paidValueToBusiness, decimal profitsDividends)
        {
            var declaracaoIRRepositoryMock = new Mock<IDeclaracaoIRRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var declaracaoIRAppService = new DeclaracaoIRAppService(
                declaracaoIRRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var declaracaoIREntity = new DeclaracaoIR(declaracaoNumero, new Cnpj(cnpj), new Cpf(cpf), companyName, businessHeader, totalIncome, socialSecurity, complementContribution, alimony, taxWithheld, paidValueToBusiness, profitsDividends);
            declaracaoIRRepositoryMock.Setup(repo => repo.GetByPaidValueToBusiness(paidValueToBusiness)).ReturnsAsync(declaracaoIREntity);

            var expectedViewModel = new DeclaracaoIRViewModel()
            {
                Id = declaracaoIREntity.Id,
                DeclaracaoNumero = declaracaoNumero,
                CompanyName = companyName,
                Cnpj = cnpj,
                Cpf = cpf,
                BusinessHeader = businessHeader,
                TotalIncome = totalIncome,
                SocialSecurity = socialSecurity,
                ComplementContribution = complementContribution,
                Alimony = alimony,
                TaxWithheld = taxWithheld,
                PaidValueToBusiness = paidValueToBusiness,
                ProfitsDividends = profitsDividends
            };

            mapperMock.Setup(mapper => mapper.Map<DeclaracaoIRViewModel>(declaracaoIREntity)).Returns(expectedViewModel);

            // Act
            var result = await declaracaoIRAppService.GetByPaidValueToBusiness(paidValueToBusiness);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        [Theory]
        [InlineData(22.10)]
        [InlineData(55.23)]
        [InlineData(11.23)]
        public async Task GetByPaidValueToBusiness_ShouldHandleNullRepositoryResult(decimal paidValueToBusiness)
        {
            // Arrange
            var declaracaoIRRepositoryMock = new Mock<IDeclaracaoIRRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var declaracaoIRAppService = new DeclaracaoIRAppService(
                declaracaoIRRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            declaracaoIRRepositoryMock.Setup(repo => repo.GetByPaidValueToBusiness(It.IsAny<decimal>()))
                .ReturnsAsync((DeclaracaoIR)null); // Simulate null result from the repository

            // Act
            var result = await declaracaoIRAppService.GetByPaidValueToBusiness(paidValueToBusiness);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData(84.32)]
        [InlineData(11.09)]
        [InlineData(22.11)]
        public async Task GetByPaidValueToBusiness_ShouldHandleInvalidMappingResult(decimal paidValueToBusiness)
        {
            // Arrange
            var declaracaoIRRepositoryMock = new Mock<IDeclaracaoIRRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var declaracaoIRAppService = new DeclaracaoIRAppService(
                declaracaoIRRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            declaracaoIRRepositoryMock.Setup(repo => repo.GetByPaidValueToBusiness(It.IsAny<decimal>()))
                .ThrowsAsync(new ArgumentException("Invalid data")); // Simulate null result from the repository

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => declaracaoIRAppService.GetByPaidValueToBusiness(paidValueToBusiness));
        }

        [Theory]
        [InlineData("783628093873837432", "02.692.316/0001-17", "284.144.750-27", "ERP SERVICES", "John doe", 2200.93, 27638.22, 89273982.66, 5555.55, "9000.21", 5000.00, 10.00)]
        [InlineData("123456789012345678", "45.836.508/0001-62", "145.914.340-01", "WEB DESIGN", "Maria silva", 3300.93, 27638.22, 89273982.66, 5555.55, "9000.21", 5000.00, 10.00)]
        [InlineData("987654321098765432", "38.270.839/0001-12", "949.828.900-05", "MARKETING", "José carlos", 4400.93, 27638.22, 89273982.66, 5555.55, "9000.21", 5000.00, 10.00)]
        public async Task GetByProfitsDividends_ShouldReturnsCompanyViewModel(string declaracaoNumero, string cnpj, string cpf, string companyName, string businessHeader, decimal totalIncome, decimal socialSecurity, decimal complementContribution, decimal alimony, string taxWithheld, decimal paidValueToBusiness, decimal profitsDividends)
        {
            var declaracaoIRRepositoryMock = new Mock<IDeclaracaoIRRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var declaracaoIRAppService = new DeclaracaoIRAppService(
                declaracaoIRRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var declaracaoIREntity = new DeclaracaoIR(declaracaoNumero, new Cnpj(cnpj), new Cpf(cpf), companyName, businessHeader, totalIncome, socialSecurity, complementContribution, alimony, taxWithheld, paidValueToBusiness, profitsDividends);
            declaracaoIRRepositoryMock.Setup(repo => repo.GetByProfitsDividends(profitsDividends)).ReturnsAsync(declaracaoIREntity);

            var expectedViewModel = new DeclaracaoIRViewModel()
            {
                Id = declaracaoIREntity.Id,
                DeclaracaoNumero = declaracaoNumero,
                CompanyName = companyName,
                Cnpj = cnpj,
                Cpf = cpf,
                BusinessHeader = businessHeader,
                TotalIncome = totalIncome,
                SocialSecurity = socialSecurity,
                ComplementContribution = complementContribution,
                Alimony = alimony,
                TaxWithheld = taxWithheld,
                PaidValueToBusiness = paidValueToBusiness,
                ProfitsDividends = profitsDividends
            };

            mapperMock.Setup(mapper => mapper.Map<DeclaracaoIRViewModel>(declaracaoIREntity)).Returns(expectedViewModel);

            // Act
            var result = await declaracaoIRAppService.GetByProfitsDividends(profitsDividends);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        [Theory]
        [InlineData(22.10)]
        [InlineData(55.23)]
        [InlineData(11.23)]
        public async Task GetByProfitsDividends_ShouldHandleNullRepositoryResult(decimal profitsDividends)
        {
            // Arrange
            var declaracaoIRRepositoryMock = new Mock<IDeclaracaoIRRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var declaracaoIRAppService = new DeclaracaoIRAppService(
                declaracaoIRRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            declaracaoIRRepositoryMock.Setup(repo => repo.GetByProfitsDividends(It.IsAny<decimal>()))
                .ReturnsAsync((DeclaracaoIR)null); // Simulate null result from the repository

            // Act
            var result = await declaracaoIRAppService.GetByProfitsDividends(profitsDividends);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData(84.32)]
        [InlineData(11.09)]
        [InlineData(22.11)]
        public async Task GetByProfitsDividends_ShouldHandleInvalidMappingResult(decimal profitsDividends)
        {
            // Arrange
            var declaracaoIRRepositoryMock = new Mock<IDeclaracaoIRRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var declaracaoIRAppService = new DeclaracaoIRAppService(
                declaracaoIRRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            declaracaoIRRepositoryMock.Setup(repo => repo.GetByProfitsDividends(It.IsAny<decimal>()))
                .ThrowsAsync(new ArgumentException("Invalid data")); // Simulate null result from the repository

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => declaracaoIRAppService.GetByProfitsDividends(profitsDividends));
        }

        [Theory]
        [InlineData("783628093873837432", "02.692.316/0001-17", "284.144.750-27", "ERP SERVICES", "John doe", 2200.93, 27638.22, 89273982.66, 5555.55, "9000.21", 5000.00, 10.00)]
        [InlineData("123456789012345678", "45.836.508/0001-62", "145.914.340-01", "WEB DESIGN", "Maria silva", 3300.93, 27638.22, 89273982.66, 5555.55, "9000.21", 5000.00, 10.00)]
        [InlineData("987654321098765432", "38.270.839/0001-12", "949.828.900-05", "MARKETING", "José carlos", 4400.93, 27638.22, 89273982.66, 5555.55, "9000.21", 5000.00, 10.00)]
        public async Task Save_ShouldReturnsCompanyViewModel(string declaracaoNumero, string cnpj, string cpf, string companyName, string businessHeader, decimal totalIncome, decimal socialSecurity, decimal complementContribution, decimal alimony, string taxWithheld, decimal paidValueToBusiness, decimal profitsDividends)
        {
            var declaracaoIRRepositoryMock = new Mock<IDeclaracaoIRRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var declaracaoIRAppService = new DeclaracaoIRAppService(
                declaracaoIRRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var createCompanyCommand = new CreateDeclaracaoIRCommand()
            {
                DeclaracoaNumero = declaracaoNumero,
                Cnpj = cnpj,
                Cpf = new Cpf(cpf),
                CompanyName = companyName,
                BusinessHeader = businessHeader,
                TotalIncome = totalIncome,
                SocialSecurity = socialSecurity,
                ComplementContribution = complementContribution,
                Alimony = alimony,
                TaxWithheld = taxWithheld,
                PaidValueToBusiness = paidValueToBusiness,
                ProfitsDividends = profitsDividends
            };

            // Act
            await declaracaoIRAppService.Save(createCompanyCommand);

            // Assert
            declaracaoIRRepositoryMock.Verify(repo => repo.Add(It.IsAny<DeclaracaoIR>()), Times.Once);
        }

        [Theory]
        [InlineData("783628093873837432", "02.692.316/0001-17", "284.144.750-27", "ERP SERVICES", "John doe", 2200.93, 27638.22, 89273982.66, 5555.55, "9000.21", 5000.00, 10.00)]
        [InlineData("123456789012345678", "45.836.508/0001-62", "145.914.340-01", "WEB DESIGN", "Maria silva", 3300.93, 27638.22, 89273982.66, 5555.55, "9000.21", 5000.00, 10.00)]
        [InlineData("987654321098765432", "38.270.839/0001-12", "949.828.900-05", "MARKETING", "José carlos", 4400.93, 27638.22, 89273982.66, 5555.55, "9000.21", 5000.00, 10.00)]
        public async Task Save_ShouldHandleNullRepositoryResult(string declaracaoNumero, string cnpj, string cpf, string companyName, string businessHeader, decimal totalIncome, decimal socialSecurity, decimal complementContribution, decimal alimony, string taxWithheld, decimal paidValueToBusiness, decimal profitsDividends)
        {
            var declaracaoIRRepositoryMock = new Mock<IDeclaracaoIRRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var declaracaoIRAppService = new DeclaracaoIRAppService(
                declaracaoIRRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var createCompanyCommand = new CreateDeclaracaoIRCommand()
            {
                DeclaracoaNumero = declaracaoNumero,
                Cnpj = cnpj,
                Cpf = new Cpf(cpf),
                CompanyName = companyName,
                BusinessHeader = businessHeader,
                TotalIncome = totalIncome,
                SocialSecurity = socialSecurity,
                ComplementContribution = complementContribution,
                Alimony = alimony,
                TaxWithheld = taxWithheld,
                PaidValueToBusiness = paidValueToBusiness,
                ProfitsDividends = profitsDividends
            };

            declaracaoIRRepositoryMock.Setup(repo => repo.Add(It.IsAny<DeclaracaoIR>())).Throws(new NullReferenceException());

            // Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => declaracaoIRAppService.Save(createCompanyCommand));
        }

        [Theory]
        [InlineData("783628093873837432", "02.692.316/0001-17", "284.144.750-27", "ERP SERVICES", "John doe", 2200.93, 27638.22, 89273982.66, 5555.55, "9000.21", 5000.00, 10.00)]
        [InlineData("123456789012345678", "45.836.508/0001-62", "145.914.340-01", "WEB DESIGN", "Maria silva", 3300.93, 27638.22, 89273982.66, 5555.55, "9000.21", 5000.00, 10.00)]
        [InlineData("987654321098765432", "38.270.839/0001-12", "949.828.900-05", "MARKETING", "José carlos", 4400.93, 27638.22, 89273982.66, 5555.55, "9000.21", 5000.00, 10.00)]
        public async Task Save_ShouldHandleInvalidMappingResult(string declaracaoNumero, string cnpj, string cpf, string companyName, string businessHeader, decimal totalIncome, decimal socialSecurity, decimal complementContribution, decimal alimony, string taxWithheld, decimal paidValueToBusiness, decimal profitsDividends)
        {
            // Arrange
            var declaracaoIRRepositoryMock = new Mock<IDeclaracaoIRRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var declaracaoIRAppService = new DeclaracaoIRAppService(
                declaracaoIRRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var createCompanyCommand = new CreateDeclaracaoIRCommand()
            {
                DeclaracoaNumero = declaracaoNumero,
                Cnpj = cnpj,
                Cpf = new Cpf(cpf),
                CompanyName = companyName,
                BusinessHeader = businessHeader,
                TotalIncome = totalIncome,
                SocialSecurity = socialSecurity,
                ComplementContribution = complementContribution,
                Alimony = alimony,
                TaxWithheld = taxWithheld,
                PaidValueToBusiness = paidValueToBusiness,
                ProfitsDividends = profitsDividends
            };

            // Act       
            declaracaoIRRepositoryMock.Setup(repo => repo.Add(It.IsAny<DeclaracaoIR>()))
            .Throws(new ArgumentException("Invalid data"));

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(() => declaracaoIRAppService.Save(createCompanyCommand));
        }
    }
}
