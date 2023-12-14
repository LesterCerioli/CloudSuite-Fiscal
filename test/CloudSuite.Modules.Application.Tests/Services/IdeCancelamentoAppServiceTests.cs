using AutoMapper;
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
    public class IdeCancelamentoAppServiceTests
    {
        [Theory]
        [InlineData("10-06-2020", "09-11-2016", "Desistencia", "47.224.882/0001-32")]
        [InlineData("05-03-2017", "05-03-2017", "Produto falso", "78.705.891/0001-79")]
        [InlineData("09-11-2016", "10-09-2023", "Não quero mais", "39.946.453/0001-40")]
        public async Task GetByCancelReason_ShouldReturnsCompanyViewModel(DateTimeOffset requestDate, DateTimeOffset timeDate, string cancelReason, string cnpj)
        {
            var cancelOrderRepositoryRepositoryMock = new Mock<IIdeCancelamentoRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var cancelOrderAppService = new IdeCancelamentoAppService(
                cancelOrderRepositoryRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var cancelOrder = new CancelOrder(null, DateTimeOffset.UtcNow, cnpj);
            var ideCancelamento = new IdeCancelamento(new CancelOrder(new IdeCancelamento(cancelOrder, cancelReason, timeDate), requestDate, new Cnpj(cnpj)), "Teste de cancelamento", DateTimeOffset.Now);

            var cancelOrder1 = new CancelOrder(ideCancelamento, DateTimeOffset.UtcNow, cnpj);

            cancelOrderRepositoryRepositoryMock.Setup(repo => repo.GetByCancelReason(cancelReason)).ReturnsAsync(ideCancelamento);

            var expectedViewModel = new IdeCancelamentoViewModel()
            {
                Id = cancelOrder1.Id,
                CancelReason = cancelReason,
                TimeDate = timeDate
            };

            mapperMock.Setup(mapper => mapper.Map<IdeCancelamentoViewModel>(ideCancelamento)).Returns(expectedViewModel);

            // Act
            var result = await cancelOrderAppService.GetByCancelReason(cancelReason);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        [Theory]
        [InlineData("53.210.856/0001-58")]
        [InlineData("49.087.732/0001-50")]
        [InlineData("21.957.521/0001-01")]
        public async Task GetByCancelReason_ShouldHandleNullRepositoryResult(string cancelReason)
        {
            // Arrange
            var ideCancelamentoRepositoryMock = new Mock<IIdeCancelamentoRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var ideCancelamentoAppService = new IdeCancelamentoAppService(
                ideCancelamentoRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            ideCancelamentoRepositoryMock.Setup(repo => repo.GetByCancelReason(It.IsAny<string>()))
                .ReturnsAsync((IdeCancelamento)null); // Simulate null result from the repository

            // Act
            var result = await ideCancelamentoAppService.GetByCancelReason(cancelReason);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("09.890.041/0001-75")]
        [InlineData("06.485.306/0001-61")]
        [InlineData("74.771.710/0001-51")]
        public async Task GetByCancelReason_ShouldHandleInvalidMappingResult(string cancelReason)
        {
            // Arrange
            var ideCancelamentoRepositoryMock = new Mock<IIdeCancelamentoRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var ideCancelamentoAppService = new IdeCancelamentoAppService(
                ideCancelamentoRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            ideCancelamentoRepositoryMock.Setup(repo => repo.GetByCancelReason(It.IsAny<string>()))
                .ThrowsAsync(new ArgumentException("Invalid data")); // Simulate null result from the repository

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => ideCancelamentoAppService.GetByCancelReason(cancelReason));
        }

        [Theory]
        [InlineData("10-06-2020", "09-11-2016", "Desistencia", "87.305.383/0001-94")]
        [InlineData("05-03-2017", "05-03-2017", "Produto falso", "84.199.132/0001-84")]
        [InlineData("09-11-2016", "10-09-2023", "Não quero mais", "14.345.739/0001-40")]
        public async Task GetByTimeDate_ShouldReturnsCompanyViewModel(DateTimeOffset requestDate, DateTimeOffset timeDate, string cancelReason, string cnpj)
        {
            var cancelOrderRepositoryRepositoryMock = new Mock<IIdeCancelamentoRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var cancelOrderAppService = new IdeCancelamentoAppService(
                cancelOrderRepositoryRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var cancelOrder = new CancelOrder(null, DateTimeOffset.UtcNow, cnpj);
            var ideCancelamento = new IdeCancelamento(new CancelOrder(new IdeCancelamento(cancelOrder, cancelReason, timeDate), requestDate, new Cnpj(cnpj)), "Teste de cancelamento", DateTimeOffset.Now);

            var cancelOrder1 = new CancelOrder(ideCancelamento, DateTimeOffset.UtcNow, cnpj);

            cancelOrderRepositoryRepositoryMock.Setup(repo => repo.GetByTimeDate(timeDate)).ReturnsAsync(ideCancelamento);

            var expectedViewModel = new IdeCancelamentoViewModel()
            {
                Id = cancelOrder1.Id,
                CancelReason = cancelReason,
                TimeDate = timeDate
            };

            mapperMock.Setup(mapper => mapper.Map<IdeCancelamentoViewModel>(ideCancelamento)).Returns(expectedViewModel);

            // Act
            var result = await cancelOrderAppService.GetByTimeDate(timeDate);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        [Theory]
        [InlineData("10-12-2023")]
        [InlineData("09-11-2016")]
        [InlineData("05-03-2017")]
        public async Task GetByTimeDate_ShouldHandleNullRepositoryResult(DateTimeOffset timeDate)
        {
            // Arrange
            var ideCancelamentoRepositoryMock = new Mock<IIdeCancelamentoRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var ideCancelamentoAppService = new IdeCancelamentoAppService(
                ideCancelamentoRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            ideCancelamentoRepositoryMock.Setup(repo => repo.GetByTimeDate(It.IsAny<DateTimeOffset>()))
                .ReturnsAsync((IdeCancelamento)null); // Simulate null result from the repository

            // Act
            var result = await ideCancelamentoAppService.GetByTimeDate(timeDate);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("10-12-2023")]
        [InlineData("09-11-2016")]
        [InlineData("05-03-2017")]
        public async Task GetByTimeDate_ShouldHandleInvalidMappingResult(DateTimeOffset timeDate)
        {
            // Arrange
            var ideCancelamentoRepositoryMock = new Mock<IIdeCancelamentoRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var ideCancelamentoAppService = new IdeCancelamentoAppService(
                ideCancelamentoRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            ideCancelamentoRepositoryMock.Setup(repo => repo.GetByTimeDate(It.IsAny<DateTimeOffset>()))
                .ThrowsAsync(new ArgumentException("Invalid data")); // Simulate null result from the repository

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => ideCancelamentoAppService.GetByTimeDate(timeDate));
        }
    }
}
