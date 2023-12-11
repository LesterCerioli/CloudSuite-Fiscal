using AutoMapper;
using CloudSuite.Modules.Application.Handlers.Country;
using CloudSuite.Modules.Application.Handlers.DAS;
using CloudSuite.Modules.Application.Services.Implementation;
using CloudSuite.Modules.Application.ViewModels;
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
    public class DASAppServiceTests
    {
        [Theory]
        [InlineData("Outubro", "08-05-2022", "2023", "2000", "283789234789347", "6587346857969934863")]
        [InlineData("Março", "15-04-1993", "2022", "3500", "283789234789347", "6587346857969934863")]
        [InlineData("Dezembro", "18-07-1993", "2020", "5000", "039485627830451", "0293803924834545939")]
        public async Task GetDASByReferenceMonth_ShouldReturnsCompanyViewModel(string referenceMonth, DateTime dueDate, string referenceYear, string paymentValue, string documentNumber, string barCode)
        {
            var dasRepositoryMock = new Mock<IDASRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var dasAppService = new DASAppService(
                dasRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var dasEntity = new DAS(referenceMonth, dueDate, referenceYear, paymentValue, documentNumber, barCode);
            dasRepositoryMock.Setup(repo => repo.GetByReferenceMonth(referenceMonth)).ReturnsAsync(dasEntity);

            var expectedViewModel = new DASViewModel()
            {
                Id = dasEntity.Id,
                ReferenceMonth = referenceMonth,
                DueDate = dueDate,
                ReferenceYear = referenceYear,
                PaymentValue = paymentValue,
                DocumentNumber = documentNumber,
                BarCode = barCode
            };

            mapperMock.Setup(mapper => mapper.Map<DASViewModel>(dasEntity)).Returns(expectedViewModel);

            // Act
            var result = await dasAppService.GetByReferenceMonth(referenceMonth);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        [Theory]
        [InlineData("Outubro")]
        [InlineData("Março")]
        [InlineData("Dezembro")]
        public async Task GetDASByReferenceMonth_ShouldHandleNullRepositoryResult(string referenceMonth)
        {
            // Arrange
            var dasRepositoryMock = new Mock<IDASRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var countryAppService = new DASAppService(
                dasRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            dasRepositoryMock.Setup(repo => repo.GetByReferenceMonth(It.IsAny<string>()))
                .ReturnsAsync((DAS)null); // Simulate null result from the repository

            // Act
            var result = await countryAppService.GetByReferenceMonth(referenceMonth);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("Janeiro")]
        [InlineData("Abril")]
        [InlineData("Agosto")]
        public async Task GetDASByReferenceMonth_ShouldHandleInvalidMappingResult(string referenceMonth)
        {
            // Arrange
            var dasRepositoryMock = new Mock<IDASRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var countryAppService = new DASAppService(
                dasRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            dasRepositoryMock.Setup(repo => repo.GetByReferenceMonth(It.IsAny<string>()))
                .ThrowsAsync(new ArgumentException("Invalid data")); // Simulate null result from the repository

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => countryAppService.GetByReferenceMonth(referenceMonth));
        }

        [Theory]
        [InlineData("Outubro", "08-05-2022", "2023", "2000", "283789234789347", "6587346857969934863")]
        [InlineData("Março", "15-04-1993", "2022", "3500", "283789234789347", "6587346857969934863")]
        [InlineData("Dezembro", "18-07-1993", "2020", "5000", "039485627830451", "0293803924834545939")]
        public async Task GetDASByDueDate_ShouldReturnsCompanyViewModel(string referenceMonth, DateTime dueDate, string referenceYear, string paymentValue, string documentNumber, string barCode)
        {
            var dasRepositoryMock = new Mock<IDASRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var countryAppService = new DASAppService(
                dasRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var dasEntity = new DAS(referenceMonth, dueDate, referenceYear, paymentValue, documentNumber, barCode);
            dasRepositoryMock.Setup(repo => repo.GetByReferenceMonth(referenceMonth)).ReturnsAsync(dasEntity);

            var expectedViewModel = new DASViewModel()
            {
                Id = dasEntity.Id,
                ReferenceMonth = referenceMonth,
                DueDate = dueDate,
                ReferenceYear = referenceYear,
                PaymentValue = paymentValue,
                DocumentNumber = documentNumber,
                BarCode = barCode
            };

            mapperMock.Setup(mapper => mapper.Map<DASViewModel>(dasEntity)).Returns(expectedViewModel);

            // Act
            var result = await countryAppService.GetByDueDate(dueDate);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        [Theory]
        [InlineData("11-02-2023")]
        [InlineData("17-12-2023")]
        [InlineData("02-11-2023")]
        public async Task GetDASByDueDate_ShouldHandleNullRepositoryResult(DateTime dueDate)
        {
            // Arrange
            var dasRepositoryMock = new Mock<IDASRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var dasAppService = new DASAppService(
                dasRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            dasRepositoryMock.Setup(repo => repo.GetByReferenceMonth(It.IsAny<string>()))
                .ReturnsAsync((DAS)null); // Simulate null result from the repository

            // Act
            var result = await dasAppService.GetByDueDate(dueDate);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("11-02-2023")]
        [InlineData("17-12-2023")]
        [InlineData("02-11-2023")]
        public async Task GetDASByDueDate_ShouldHandleInvalidMappingResult(DateTime dueDate)
        {
            // Arrange
            var dasRepositoryMock = new Mock<IDASRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var dasAppService = new DASAppService(
                dasRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            dasRepositoryMock.Setup(repo => repo.GetByDueDate(It.IsAny<DateTime>()))
                .ThrowsAsync(new ArgumentException("Invalid data")); // Simulate null result from the repository

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => dasAppService.GetByDueDate(dueDate));
        }

        [Theory]
        [InlineData("Outubro", "08-05-2022", "2023", "2000", "283789234789347", "6587346857969934863")]
        [InlineData("Março", "15-04-1993", "2022", "3500", "283789234789347", "6587346857969934863")]
        [InlineData("Dezembro", "18-07-1993", "2020", "5000", "039485627830451", "0293803924834545939")]
        public async Task GetByDocumentNumber_ShouldReturnsCompanyViewModel(string referenceMonth, DateTime dueDate, string referenceYear, string paymentValue, string documentNumber, string barCode)
        {
            var dasRepositoryMock = new Mock<IDASRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var countryAppService = new DASAppService(
                dasRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var dasEntity = new DAS(referenceMonth, dueDate, referenceYear, paymentValue, documentNumber, barCode);
            dasRepositoryMock.Setup(repo => repo.GetByDocumentNumber(referenceMonth)).ReturnsAsync(dasEntity);

            var expectedViewModel = new DASViewModel()
            {
                Id = dasEntity.Id,
                ReferenceMonth = referenceMonth,
                DueDate = dueDate,
                ReferenceYear = referenceYear,
                PaymentValue = paymentValue,
                DocumentNumber = documentNumber,
                BarCode = barCode
            };

            mapperMock.Setup(mapper => mapper.Map<DASViewModel>(dasEntity)).Returns(expectedViewModel);

            // Act
            var result = await countryAppService.GetByDocumentNumber(documentNumber);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        [Theory]
        [InlineData("283789234789347")]
        [InlineData("987984737290123")]
        [InlineData("584930923745053")]
        public async Task GetByDocumentNumber_ShouldHandleNullRepositoryResult(string documentNumber)
        {
            // Arrange
            var dasRepositoryMock = new Mock<IDASRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var dasAppService = new DASAppService(
                dasRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            dasRepositoryMock.Setup(repo => repo.GetByReferenceMonth(It.IsAny<string>()))
                .ReturnsAsync((DAS)null); // Simulate null result from the repository

            // Act
            var result = await dasAppService.GetByDocumentNumber(documentNumber);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("738403759301202")]
        [InlineData("123893304935723")]
        [InlineData("874634598235874")]
        public async Task GetByDocumentNumber_ShouldHandleInvalidMappingResult(string documentNumber)
        {
            // Arrange
            var dasRepositoryMock = new Mock<IDASRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var dasAppService = new DASAppService(
                dasRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            dasRepositoryMock.Setup(repo => repo.GetByDocumentNumber(It.IsAny<string>()))
                .ThrowsAsync(new ArgumentException("Invalid data")); // Simulate null result from the repository

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => dasAppService.GetByDocumentNumber(documentNumber));
        }

        [Theory]
        [InlineData("Outubro", "08-05-2022", "2023", "2000", "283789234789347", "6587346857969934863")]
        [InlineData("Março", "15-04-1993", "2022", "3500", "283789234789347", "6587346857969934863")]
        [InlineData("Dezembro", "18-07-1993", "2020", "5000", "039485627830451", "0293803924834545939")]
        public async Task GetByReferenceYear_ShouldReturnsCompanyViewModel(string referenceMonth, DateTime dueDate, string referenceYear, string paymentValue, string documentNumber, string barCode)
        {
            var dasRepositoryMock = new Mock<IDASRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var dasAppService = new DASAppService(
                dasRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var dasEntity = new DAS(referenceMonth, dueDate, referenceYear, paymentValue, documentNumber, barCode);
            dasRepositoryMock.Setup(repo => repo.GetByReferenceYear(referenceMonth)).ReturnsAsync(dasEntity);

            var expectedViewModel = new DASViewModel()
            {
                Id = dasEntity.Id,
                ReferenceMonth = referenceMonth,
                DueDate = dueDate,
                ReferenceYear = referenceYear,
                PaymentValue = paymentValue,
                DocumentNumber = documentNumber,
                BarCode = barCode
            };

            mapperMock.Setup(mapper => mapper.Map<DASViewModel>(dasEntity)).Returns(expectedViewModel);

            // Act
            var result = await dasAppService.GetByReferenceYear(referenceYear);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        [Theory]
        [InlineData("283789234789347")]
        [InlineData("987984737290123")]
        [InlineData("584930923745053")]
        public async Task GetByReferenceYear_ShouldHandleNullRepositoryResult(string referenceYear)
        {
            // Arrange
            var dasRepositoryMock = new Mock<IDASRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var dasAppService = new DASAppService(
                dasRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            dasRepositoryMock.Setup(repo => repo.GetByReferenceMonth(It.IsAny<string>()))
                .ReturnsAsync((DAS)null); // Simulate null result from the repository

            // Act
            var result = await dasAppService.GetByReferenceYear(referenceYear);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("738403759301202")]
        [InlineData("123893304935723")]
        [InlineData("874634598235874")]
        public async Task GetByReferenceYear_ShouldHandleInvalidMappingResult(string referenceYear)
        {
            // Arrange
            var dasRepositoryMock = new Mock<IDASRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var dasAppService = new DASAppService(
                dasRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            dasRepositoryMock.Setup(repo => repo.GetByDocumentNumber(It.IsAny<string>()))
                .ThrowsAsync(new ArgumentException("Invalid data")); // Simulate null result from the repository

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => dasAppService.GetByReferenceYear(referenceYear));
        }

        [Theory]
        [InlineData("Outubro", "08-05-2022", "2023", "2000", "283789234789347", "6587346857969934863")]
        [InlineData("Março", "15-04-1993", "2022", "3500", "283789234789347", "6587346857969934863")]
        [InlineData("Dezembro", "18-07-1993", "2020", "5000", "039485627830451", "0293803924834545939")]
        public async Task Save_ShouldAddCompanyToRepository(string referenceMonth, DateTime dueDate, string referenceYear, string paymentValue, string documentNumber, string barCode)
        {
            // Arrange
            var dasRepositoryMock = new Mock<IDASRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var dasAppService = new DASAppService(
                dasRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var createDASCommand = new CreateDASCommand()
            {
                ReferenceMonth = referenceMonth,
                DueDate = dueDate,
                ReferenceYear = referenceYear,
                PaymentValue = paymentValue,
                DocumentNumber = documentNumber,
                BarCode = barCode
            };

            // Act
            await dasAppService.Save(createDASCommand);

            // Assert
            dasRepositoryMock.Verify(repo => repo.Add(It.IsAny<DAS>()), Times.Once);
        }

        [Theory]
        [InlineData("Outubro", "08-05-2022", "2023", "2000", "283789234789347", "6587346857969934863")]
        [InlineData("Março", "15-04-1993", "2022", "3500", "283789234789347", "6587346857969934863")]
        [InlineData("Dezembro", "18-07-1993", "2020", "5000", "039485627830451", "0293803924834545939")]
        public async Task Save_ShouldHandleNullRepositoryResult(string referenceMonth, DateTime dueDate, string referenceYear, string paymentValue, string documentNumber, string barCode)
        {
            //Arrange
            var dasRepositoryMock = new Mock<IDASRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var dasAppService = new DASAppService(
                dasRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var createDASCommand = new CreateDASCommand()
            {
                ReferenceMonth = referenceMonth,
                DueDate = dueDate,
                ReferenceYear = referenceYear,
                PaymentValue = paymentValue,
                DocumentNumber = documentNumber,
                BarCode = barCode
            };

            dasRepositoryMock.Setup(repo => repo.Add(It.IsAny<DAS>())).Throws(new NullReferenceException());

            // Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => dasAppService.Save(createDASCommand));

        }

        [Theory]
        [InlineData("Outubro", "08-05-2022", "2023", "2000", "283789234789347", "6587346857969934863")]
        [InlineData("Março", "15-04-1993", "2022", "3500", "283789234789347", "6587346857969934863")]
        [InlineData("Dezembro", "18-07-1993", "2020", "5000", "039485627830451", "0293803924834545939")]
        public async Task Save_ShouldHandleInvalidMappingResult(string referenceMonth, DateTime dueDate, string referenceYear, string paymentValue, string documentNumber, string barCode)
        {

            //Arrange
            var dasRepositoryMock = new Mock<IDASRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var dasAppService = new DASAppService(
                dasRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var createDASCommand = new CreateDASCommand()
            {
                ReferenceMonth = referenceMonth,
                DueDate = dueDate,
                ReferenceYear = referenceYear,
                PaymentValue = paymentValue,
                DocumentNumber = documentNumber,
                BarCode = barCode
            };

            // Act       
            dasRepositoryMock.Setup(repo => repo.Add(It.IsAny<DAS>()))
            .Throws(new ArgumentException("Invalid data"));

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(() => dasAppService.Save(createDASCommand));
        }

    }
}
