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
        [InlineData("783628093873837433", "01.761.475/0001-63", "247.154.700-12", "ERP SOLUTIONS", "Jane Doe", 2300.93, 28638.22, 89273982.67, 5655.55, "9100.21", 5100.00, 20.00)]
        [InlineData("783628093873837434", "83.610.414/0001-60", "890.157.620-15", "ERP SYSTEMS", "Jim Doe", 2400.93, 29638.22, 89273982.68, 5755.55, "9200.21", 5200.00, 30.00)]
        [InlineData("783628093873837435", "40.056.095/0001-80", "698.444.790-96", "ERP PRODUCTS", "Jill Doe", 2500.93, 30638.22, 89273982.69, 5855.55, "9300.21", 5300.00, 40.00)]
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
        [InlineData("336.651.000-54")]
        [InlineData("151.730.680-94")]
        [InlineData("904.820.060-13")]
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
        [InlineData("783628093873837436", "29.216.824/0001-85", "789.147.380-35", "ERP SERVICES 2", "Jack Doe", 2600.93, 31638.22, 89273982.70, 5955.55, "9400.21", 5400.00, 50.00)]
        [InlineData("783628093873837437", "60.931.894/0001-65", "750.279.680-00", "ERP SOLUTIONS 2", "Jenny Doe", 2700.93, 32638.22, 89273982.71, 6055.55, "9500.21", 5500.00, 60.00)]
        [InlineData("783628093873837438", "79.690.535/0001-91", "751.321.810-23", "ERP SYSTEMS 2", "James Doe", 2800.93, 33638.22, 89273982.72, 6155.55, "9600.21", 5600.00, 70.00)]
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
        [InlineData("783628093873837439", "29.017.235/0001-78", "452.698.320-97", "ERP PRODUCTS 2", "Joe Doe", 2900.93, 34638.22, 89273982.73, 6255.55, "9700.21", 5700.00, 80.00)]
        [InlineData("783628093873837440", "26.472.567/0001-54", "715.369.590-73", "ERP SERVICES 3", "Jill Doe", 3000.93, 35638.22, 89273982.74, 6355.55, "9800.21", 5800.00, 90.00)]
        [InlineData("783628093873837441", "01.713.617/0001-17", "132.979.400-18", "ERP SOLUTIONS 3", "John Doe", 3100.93, 36638.22, 89273982.75, 6455.55, "9900.21", 5900.00, 100.00)]
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
        [InlineData("783628093873837442", "67.294.626/0001-29", "480.494.770-10", "ERP SYSTEMS 3", "Jane Doe", 3200.93, 37638.22, 89273982.76, 6555.55, "10000.21", 6000.00, 110.00)]
        [InlineData("783628093873837443", "36.150.663/0001-30", "275.273.940-05", "ERP PRODUCTS 3", "Jim Doe", 3300.93, 38638.22, 89273982.77, 6655.55, "10100.21", 6100.00, 120.00)]
        [InlineData("783628093873837444", "71.527.684/0001-22", "455.962.340-60", "ERP SERVICES 4", "Jill Doe", 3400.93, 39638.22, 89273982.78, 6755.55, "10200.21", 6200.00, 130.00)]
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
        [InlineData("783628093873837445", "22.297.468/0001-22", "070.125.290-16", "ERP SOLUTIONS 4", "Jack Doe", 3500.93, 40638.22, 89273982.79, 6855.55, "10300.21", 6300.00, 140.00)]
        [InlineData("783628093873837446", "67.910.808/0001-87", "860.703.670-06", "ERP SYSTEMS 4", "Jenny Doe", 3600.93, 41638.22, 89273982.80, 6955.55, "10400.21", 6400.00, 150.00)]
        [InlineData("783628093873837447", "17.104.436/0001-23", "218.294.270-08", "ERP PRODUCTS 4", "James Doe", 3700.93, 42638.22, 89273982.81, 7055.55, "10500.21", 6500.00, 160.00)]
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
        [InlineData("783628093873837448", "74.296.226/0001-18", "371.290.770-26", "ERP SERVICES 5", "Joe Doe", 3800.93, 43638.22, 89273982.82, 7155.55, "10600.21", 6600.00, 170.00)]
        [InlineData("783628093873837449", "51.148.874/0001-21", "602.231.040-78", "ERP SOLUTIONS 5", "Jill Doe", 3900.93, 44638.22, 89273982.83, 7255.55, "10700.21", 6700.00, 180.00)]
        [InlineData("783628093873837450", "92.020.339/0001-78", "849.594.310-75", "ERP SYSTEMS 5", "John Doe", 4000.93, 45638.22, 89273982.84, 7355.55, "10800.21", 6800.00, 190.00)]
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
