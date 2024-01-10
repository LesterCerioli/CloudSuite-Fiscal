using AutoMapper;
using CloudSuite.Modules.Application.Services.Implementation;
using CloudSuite.Modules.Application.ViewModels;
using CloudSuite.Modules.Common.ValueObjects;
using CloudSuite.Modules.Domain.Contracts;
using CloudSuite.Modules.Domain.Models;
using Microsoft.VisualBasic;
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
    public class DarfAppServiceTests
    {
        [Theory]
        [InlineData("Outubro", "10-12-2023", "2022", 788.32, "2378432323", "2783672392", "736483362634", "09-11-2016", "05-03-2017", "24.953.200/0001-27", "234", "2234", 2234.23, 223456.21, 3234435.11)]
        [InlineData("Novembro", "10-12-2023", "2023", 900.50, "2387432323", "2783672393", "736483362635", "10-12-2016", "06-04-2017", "90.827.628/0001-58", "235", "2235", 2235.23, 223457.21, 3234436.11)]
        [InlineData("Dezembro", "10-12-2023", "2024", 1000.60, "2396432323", "2783672394", "736483362636", "11-01-2017", "07-05-2017", "91.629.606/0001-46", "236", "2236", 2236.23, 223458.21, 3234437.11)]
        public async Task GetByReferenceMonth_ShouldReturnsCompanyViewModel(string referenceMonth, DateTime dueDate, string referenceYear, decimal darfPaymentValue, string recuboDeclaroNumero, string documentNumber, string barCode, DateTime validationDate, DateTime periodoApuracao, string cnpj, string receitaCode, string mainValue, decimal amountFine, decimal interest, decimal totalValue)
        {
            var darfRepositoryMock = new Mock<IDarfRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var darfAppService = new DarfAppService(
                darfRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var darfTaxEntity = new Darf(referenceMonth, dueDate, referenceYear, darfPaymentValue, recuboDeclaroNumero, documentNumber, barCode, validationDate,  periodoApuracao, new Cnpj(cnpj), receitaCode, mainValue, amountFine, interest, totalValue);
            darfRepositoryMock.Setup(repo => repo.GetByReferenceMonth(referenceMonth)).ReturnsAsync(darfTaxEntity);

            var expectedViewModel = new DarfViewModel()
            {
                Id = darfTaxEntity.Id,
                ReferenceMonth = referenceMonth,
                DueDate = darfTaxEntity.DueDate,
                ReferenceYear = referenceYear,
                DarfPaymentValue = darfPaymentValue,
                RecuboDeclaroNumero = recuboDeclaroNumero,
                DocumentNumber = documentNumber,
                BarCode = barCode,
                ValidationDate = darfTaxEntity.ValidationDate,
                PeriodoApuracao = periodoApuracao,
                Cnpj = cnpj,
                ReceitaCode = receitaCode,
                MainValue = mainValue,
                AmountFine = amountFine //verificar sobre as instancias da entidade que naõ tem todas as propriedades delcaradas
            };

            mapperMock.Setup(mapper => mapper.Map<DarfViewModel>(darfTaxEntity)).Returns(expectedViewModel);

            // Act
            var result = await darfAppService.GetByReferenceMonth(referenceMonth);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        [Theory]
        [InlineData("janeiro")]
        [InlineData("feveiro")]
        [InlineData("março")]
        public async Task GetByReferenceMonth_ShouldHandleNullRepositoryResult(string referenceMonth)
        {
            // Arrange
            var darfRepositoryMock = new Mock<IDarfRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var darfAppService = new DarfAppService(
                darfRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            darfRepositoryMock.Setup(repo => repo.GetByReferenceMonth(It.IsAny<string>()))
                .ReturnsAsync((Darf)null); // Simulate null result from the repository

            // Act
            var result = await darfAppService.GetByReferenceMonth(referenceMonth);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("janeiro")]
        [InlineData("feveiro")]
        [InlineData("março")]
        public async Task GetByReferenceMonth_ShouldHandleInvalidMappingResult(string referenceMonth)
        {
            // Arrange
            var darfRepositoryMock = new Mock<IDarfRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var darfAppService = new DarfAppService(
                darfRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            darfRepositoryMock.Setup(repo => repo.GetByReferenceMonth(It.IsAny<string>()))
                .ThrowsAsync(new ArgumentException("Invalid data")); // Simulate null result from the repository

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => darfAppService.GetByReferenceMonth(referenceMonth));
        }

        [Theory]
        [InlineData("Outubro", "10-12-2023", "2020", 788.32, "2378432323", "65783453567", "5683765877689", "02-05-2020", "12-10-2019", "26.235.779/0001-18", "234", "2234", 65473.23, 756.21, 23534.11)]
        [InlineData("Novembro", "10-12-2023", "2023", 349.32, "123244632", "35467365763", "35873467245672", "08-05-2023", "12-10-2019", "67.014.958/0001-02", "234", "2234", 2234.23, 967856.21, 7456.11)]
        [InlineData("Dezembro", "10-12-2023", "2019", 1234332.32, "7685667658", "9746578769", "736483362634", "04-05-2018", "12-10-2019", "80.031.981/0001-73", "234", "2234", 89754.23, 3245.21, 234.11)]
        public async Task GetByDueDate_ShouldReturnsCompanyViewModel(string referenceMonth, DateTime dueDate, string referenceYear, decimal darfPaymentValue, string recuboDeclaroNumero, string documentNumber, string barCode, DateTime validationDate, DateTime periodoApuracao, string cnpj, string receitaCode, string mainValue, decimal amountFine, decimal interest, decimal totalValue)
        {
            var darfRepositoryMock = new Mock<IDarfRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var darfAppService = new DarfAppService(
                darfRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var darfEntity = new Darf(referenceMonth, dueDate, referenceYear, darfPaymentValue, recuboDeclaroNumero, documentNumber, barCode, validationDate, periodoApuracao, new Cnpj(cnpj), receitaCode, mainValue, amountFine, interest, totalValue); darfRepositoryMock.Setup(repo => repo.GetByDueDate(darfEntity.DueDate)).ReturnsAsync(darfEntity);

            var expectedViewModel = new DarfViewModel()
            {
                Id = darfEntity.Id,
                ReferenceMonth = referenceMonth,
                DueDate = darfEntity.DueDate,
                ReferenceYear = referenceYear,
                DarfPaymentValue = darfPaymentValue,
                RecuboDeclaroNumero = recuboDeclaroNumero,
                DocumentNumber = documentNumber,
                BarCode = barCode,
                ValidationDate = darfEntity.ValidationDate,
                PeriodoApuracao = periodoApuracao,
                Cnpj = cnpj,
                ReceitaCode = receitaCode,
                MainValue = mainValue,
                AmountFine = amountFine //verificar sobre as instancias da entidade que naõ tem todas as propriedades delcaradas
            };

            mapperMock.Setup(mapper => mapper.Map<DarfViewModel>(darfEntity)).Returns(expectedViewModel);

            // Act
            var result = await darfAppService.GetByDueDate(darfEntity.DueDate);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        [Theory]
        [InlineData("10-12-2023")]
        [InlineData("09-11-2016")]
        [InlineData("05-03-2017")]
        public async Task GetByDueDate_ShouldHandleNullRepositoryResult(DateTime dueDate)
        {
            // Arrange
            var darfRepositoryMock = new Mock<IDarfRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var darfAppService = new DarfAppService(
                darfRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            darfRepositoryMock.Setup(repo => repo.GetByDueDate(It.IsAny<DateTime>()))
                .ReturnsAsync((Darf)null); // Simulate null result from the repository

            // Act
            var result = await darfAppService.GetByDueDate(dueDate);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("10-06-2023")]
        [InlineData("09-04-2022")]
        [InlineData("05-03-2020")]
        public async Task GetByDueDate_ShouldHandleInvalidMappingResult(DateTime dueDate)
        {
            // Arrange
            var darfRepositoryMock = new Mock<IDarfRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var darfAppService = new DarfAppService(
                darfRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            darfRepositoryMock.Setup(repo => repo.GetByDueDate(It.IsAny<DateTime>()))
                .ThrowsAsync(new ArgumentException("Invalid data")); // Simulate null result from the repository

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => darfAppService.GetByDueDate(dueDate));
        }

        [Theory]
        [InlineData("Outubro", "10-12-2023", "2020", 788.32, "2378432323", "65783453567", "5683765877689", "02-05-2020", "12-10-2019", "07.272.847/0001-74", "234", "2234", 65473.23, 756.21, 23534.11)]
        [InlineData("Novembro", "10-12-2023", "2023", 349.32, "123244632", "35467365763", "35873467245672", "08-05-2023", "12-10-2019", "57.912.682/0001-26", "234", "2234", 2234.23, 967856.21, 7456.11)]
        [InlineData("Dezembro", "10-12-2023", "2019", 1234332.32, "7685667658", "9746578769", "736483362634", "04-05-2018", "12-10-2019", "31.033.790/0001-35", "234", "2234", 89754.23, 3245.21, 234.11)]
        public async Task GetByDocumentNumber_ShouldReturnsCompanyViewModel(string referenceMonth, DateTime dueDate, string referenceYear, decimal darfPaymentValue, string recuboDeclaroNumero, string documentNumber, string barCode, DateTime validationDate, DateTime periodoApuracao, string cnpj, string receitaCode, string mainValue, decimal amountFine, decimal interest, decimal totalValue)
        {
            var darfRepositoryMock = new Mock<IDarfRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var darfAppService = new DarfAppService(
                darfRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var darfEntity = new Darf(referenceMonth, dueDate, referenceYear, darfPaymentValue, recuboDeclaroNumero, documentNumber, barCode, validationDate, periodoApuracao, new Cnpj(cnpj), receitaCode, mainValue, amountFine, interest, totalValue); darfRepositoryMock.Setup(repo => repo.GetByDueDate(darfEntity.DueDate)).ReturnsAsync(darfEntity);
            darfRepositoryMock.Setup(repo => repo.GetByDocumentNumber(documentNumber)).ReturnsAsync(darfEntity);

            var expectedViewModel = new DarfViewModel()
            {
                Id = darfEntity.Id,
                ReferenceMonth = referenceMonth,
                DueDate = darfEntity.DueDate,
                ReferenceYear = referenceYear,
                DarfPaymentValue = darfPaymentValue,
                RecuboDeclaroNumero = recuboDeclaroNumero,
                DocumentNumber = documentNumber,
                BarCode = barCode,
                ValidationDate = darfEntity.ValidationDate,
                PeriodoApuracao = periodoApuracao,
                Cnpj = cnpj,
                ReceitaCode = receitaCode,
                MainValue = mainValue,
                AmountFine = amountFine //verificar sobre as instancias da entidade que naõ tem todas as propriedades delcaradas
            };

            mapperMock.Setup(mapper => mapper.Map<DarfViewModel>(darfEntity)).Returns(expectedViewModel);

            // Act
            var result = await darfAppService.GetByDocumentNumber(documentNumber);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        [Theory]
        [InlineData("janeiro")]
        [InlineData("feveiro")]
        [InlineData("março")]
        public async Task GetByDocumentNumber_ShouldHandleNullRepositoryResult(string documentNumber)
        {
            // Arrange
            var darfRepositoryMock = new Mock<IDarfRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var darfAppService = new DarfAppService(
                darfRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            darfRepositoryMock.Setup(repo => repo.GetByDocumentNumber(It.IsAny<string>()))
                .ReturnsAsync((Darf)null); // Simulate null result from the repository

            // Act
            var result = await darfAppService.GetByDocumentNumber(documentNumber);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("janeiro")]
        [InlineData("feveiro")]
        [InlineData("março")]
        public async Task GetByDocumentNumber_ShouldHandleInvalidMappingResult(string documentNumber)
        {
            // Arrange
            var darfRepositoryMock = new Mock<IDarfRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var darfAppService = new DarfAppService(
                darfRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            darfRepositoryMock.Setup(repo => repo.GetByDocumentNumber(It.IsAny<string>()))
                .ThrowsAsync(new ArgumentException("Invalid data")); // Simulate null result from the repository

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => darfAppService.GetByDocumentNumber(documentNumber));
        }

        [Theory]
        [InlineData("Outubro", "10-12-2023", "2020", 788.32, "2378432323", "65783453567", "5683765877689", "02-05-2020", "12-10-2019", "00.541.001/0001-43", "234", "2234", 65473.23, 756.21, 23534.11)]
        [InlineData("Novembro", "10-12-2023", "2023", 349.32, "123244632", "35467365763", "35873467245672", "08-05-2023", "12-10-2019", "34.364.071/0001-95", "234", "2234", 2234.23, 967856.21, 7456.11)]
        [InlineData("Dezembro", "10-12-2023", "2019", 1234332.32, "7685667658", "9746578769", "736483362634", "04-05-2018", "12-10-2019", "28.533.472/0001-29", "234", "2234", 89754.23, 3245.21, 234.11)]
        public async Task GetByValidationDate_ShouldReturnsCompanyViewModel(string referenceMonth, DateTime dueDate, string referenceYear, decimal darfPaymentValue, string recuboDeclaroNumero, string documentNumber, string barCode, DateTime validationDate, DateTime periodoApuracao, string cnpj, string receitaCode, string mainValue, decimal amountFine, decimal interest, decimal totalValue)
        {
            var darfRepositoryMock = new Mock<IDarfRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var darfAppService = new DarfAppService(
                darfRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var darfEntity = new Darf(referenceMonth, dueDate, referenceYear, darfPaymentValue, recuboDeclaroNumero, documentNumber, barCode, validationDate, periodoApuracao, new Cnpj(cnpj), receitaCode, mainValue, amountFine, interest, totalValue); darfRepositoryMock.Setup(repo => repo.GetByDueDate(darfEntity.DueDate)).ReturnsAsync(darfEntity);
            darfRepositoryMock.Setup(repo => repo.GetByValidationDate(darfEntity.ValidationDate)).ReturnsAsync(darfEntity);

            var expectedViewModel = new DarfViewModel()
            {
                Id = darfEntity.Id,
                ReferenceMonth = referenceMonth,
                DueDate = darfEntity.DueDate,
                ReferenceYear = referenceYear,
                DarfPaymentValue = darfPaymentValue,
                RecuboDeclaroNumero = recuboDeclaroNumero,
                DocumentNumber = documentNumber,
                BarCode = barCode,
                ValidationDate = darfEntity.ValidationDate,
                PeriodoApuracao = periodoApuracao,
                Cnpj = cnpj,
                ReceitaCode = receitaCode,
                MainValue = mainValue,
                AmountFine = amountFine //verificar sobre as instancias da entidade que naõ tem todas as propriedades delcaradas
            };

            mapperMock.Setup(mapper => mapper.Map<DarfViewModel>(darfEntity)).Returns(expectedViewModel);

            // Act
            var result = await darfAppService.GetByValidationDate(darfEntity.ValidationDate);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        [Theory]
        [InlineData("10-06-2023")]
        [InlineData("09-04-2022")]
        [InlineData("05-03-2020")]
        public async Task GetByValidationDate_ShouldHandleNullRepositoryResult(DateTime validationDate)
        {
            // Arrange
            var darfRepositoryMock = new Mock<IDarfRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var darfAppService = new DarfAppService(
                darfRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            darfRepositoryMock.Setup(repo => repo.GetByValidationDate(It.IsAny<DateTime>()))
                .ReturnsAsync((Darf)null); // Simulate null result from the repository

            // Act
            var result = await darfAppService.GetByValidationDate(validationDate);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("10-06-2023")]
        [InlineData("09-04-2022")]
        [InlineData("05-03-2020")]
        public async Task GetByValidationDate_ShouldHandleInvalidMappingResult(DateTime validationDate)
        {
            // Arrange
            var darfRepositoryMock = new Mock<IDarfRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var darfAppService = new DarfAppService(
                darfRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            darfRepositoryMock.Setup(repo => repo.GetByValidationDate(It.IsAny<DateTime>()))
                .ThrowsAsync(new ArgumentException("Invalid data")); // Simulate null result from the repository

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => darfAppService.GetByValidationDate(validationDate));
        }

    }
}
