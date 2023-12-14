﻿using AutoMapper;
using CloudSuite.Modules.Application.Handlers.CancelOrder;
using CloudSuite.Modules.Application.Handlers.Note;
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
    public class CancelOrderAppServiceTests
    {

        [Theory]
        [InlineData("10-06-2020", "09-11-2016", "Desistencia", "34.202.565/0001-73")]
        [InlineData("05-03-2017", "05-03-2017", "Produto falso", "40.367.759/0001-21")]
        [InlineData("09-11-2016", "10-09-2023", "Não quero mais", "33.576.577/0001-03")]
        public async Task GetByCnpj_ShouldReturnsCompanyViewModel(DateTimeOffset requestDate, DateTimeOffset timeDate, string cancelReason, string cnpj)
        {
            var cancelOrderRepositoryRepositoryMock = new Mock<ICancelOrderRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var cancelOrderAppService = new CancelOrderAppService(
                cancelOrderRepositoryRepositoryMock.Object,
                mediatorHandlerMock.Object,
                mapperMock.Object);

            var cancelOrder = new CancelOrder(null, DateTimeOffset.UtcNow, cnpj);
            var ideCancelamento = new IdeCancelamento(new CancelOrder(new IdeCancelamento(cancelOrder, cancelReason, timeDate),requestDate, new Cnpj(cnpj)), "Teste de cancelamento", DateTimeOffset.Now);

            var cancelOrder1 = new CancelOrder(ideCancelamento, DateTimeOffset.UtcNow, cnpj);

            cancelOrderRepositoryRepositoryMock.Setup(repo => repo.GetbyCnpj(cnpj)).ReturnsAsync(cancelOrder1);

            var expectedViewModel = new CancelOrderViewModel()
            {
                Id = cancelOrder1.Id,
                RequestDate = requestDate,
                Cnpj = cnpj
            };

            mapperMock.Setup(mapper => mapper.Map<CancelOrderViewModel>(cancelOrder1)).Returns(expectedViewModel);

            // Act
            var result = await cancelOrderAppService.GetbyCnpj(cnpj);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        [Theory]
        [InlineData("66.564.975/0001-50")]
        [InlineData("33.999.555/0001-48")]
        [InlineData("92.772.088/0001-88")]
        public async Task GetbyCnpj_ShouldHandleNullRepositoryResult(string cnpj)
        {
            // Arrange
            var cancelOrderRepositoryRepositoryMock = new Mock<ICancelOrderRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var ideCancelamentoAppService = new CancelOrderAppService(
                cancelOrderRepositoryRepositoryMock.Object,
                mediatorHandlerMock.Object,
                mapperMock.Object);

            cancelOrderRepositoryRepositoryMock.Setup(repo => repo.GetbyCnpj(It.IsAny<Cnpj>()))
                .ReturnsAsync((CancelOrder)null); // Simulate null result from the repository

            // Act
            var result = await ideCancelamentoAppService.GetbyCnpj(new Cnpj(cnpj));

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("81.390.750/0001-19")]
        [InlineData("54.403.570/0001-51")]
        [InlineData("20.249.017/0001-94")]
        public async Task GetbyCnpj_ShouldHandleInvalidMappingResult(string cnpj)
        {
            // Arrange
            var cancelOrderRepositoryRepositoryMock = new Mock<ICancelOrderRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var ideCancelamentoAppService = new CancelOrderAppService(
                cancelOrderRepositoryRepositoryMock.Object,
                mediatorHandlerMock.Object,
                mapperMock.Object);

            cancelOrderRepositoryRepositoryMock.Setup(repo => repo.GetbyCnpj(It.IsAny<Cnpj>()))
                .ThrowsAsync(new ArgumentException("Invalid data")); // Simulate null result from the repository

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => ideCancelamentoAppService.GetbyCnpj(new Cnpj(cnpj)));
        }

        [Theory]
        [InlineData("10-06-2020", "09-11-2016", "Desistencia", "47.224.882/0001-32")]
        [InlineData("05-03-2017", "05-03-2017", "Produto falso", "42.483.790/0001-53")]
        [InlineData("09-11-2016", "10-09-2023", "Não quero mais", "65.025.775/0001-67")]
        public async Task GetByRequestDate_ShouldReturnsCompanyViewModel(DateTimeOffset requestDate, DateTimeOffset timeDate, string cancelReason, string cnpj)
        {
            var cancelOrderRepositoryRepositoryMock = new Mock<ICancelOrderRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var cancelOrderAppService = new CancelOrderAppService(
                cancelOrderRepositoryRepositoryMock.Object,
                mediatorHandlerMock.Object,
                mapperMock.Object);

            var cancelOrder = new CancelOrder(null, DateTimeOffset.UtcNow, cnpj);
            var ideCancelamento = new IdeCancelamento(new CancelOrder(new IdeCancelamento(cancelOrder, cancelReason, timeDate), requestDate, new Cnpj(cnpj)), "Teste de cancelamento", DateTimeOffset.Now);

            var cancelOrder1 = new CancelOrder(ideCancelamento, DateTimeOffset.UtcNow, cnpj);

            cancelOrderRepositoryRepositoryMock.Setup(repo => repo.GetByRequestDate(requestDate)).ReturnsAsync(cancelOrder1);

            var expectedViewModel = new CancelOrderViewModel()
            {
                Id = cancelOrder1.Id,
                RequestDate = requestDate,
                Cnpj = cnpj
            };

            mapperMock.Setup(mapper => mapper.Map<CancelOrderViewModel>(cancelOrder1)).Returns(expectedViewModel);

            // Act
            var result = await cancelOrderAppService.GetByRequestDate(requestDate);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        [Theory]
        [InlineData("10-12-2023")]
        [InlineData("09-11-2016")]
        [InlineData("05-03-2017")]
        public async Task GetByRequestDate_ShouldHandleNullRepositoryResult(DateTimeOffset requestDate)
        {
            // Arrange
            var cancelOrderRepositoryRepositoryMock = new Mock<ICancelOrderRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var ideCancelamentoAppService = new CancelOrderAppService(
                cancelOrderRepositoryRepositoryMock.Object,
                mediatorHandlerMock.Object,
                mapperMock.Object);

            cancelOrderRepositoryRepositoryMock.Setup(repo => repo.GetByRequestDate(It.IsAny<DateTimeOffset>()))
                .ReturnsAsync((CancelOrder)null); // Simulate null result from the repository

            // Act
            var result = await ideCancelamentoAppService.GetByRequestDate(requestDate);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("10-02-2019")]
        [InlineData("09-11-2016")]
        [InlineData("05-03-2017")]
        public async Task GetByRequestDate_ShouldHandleInvalidMappingResult(DateTimeOffset requestDate)
        {
            // Arrange
            var cancelOrderRepositoryRepositoryMock = new Mock<ICancelOrderRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var ideCancelamentoAppService = new CancelOrderAppService(
                cancelOrderRepositoryRepositoryMock.Object,
                mediatorHandlerMock.Object,
                mapperMock.Object);

            cancelOrderRepositoryRepositoryMock.Setup(repo => repo.GetByRequestDate(It.IsAny<DateTimeOffset>()))
                .ThrowsAsync(new ArgumentException("Invalid data")); // Simulate null result from the repository

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => ideCancelamentoAppService.GetByRequestDate(requestDate));
        }

        [Theory]
        [InlineData("10-06-2020", "75.524.498/0001-90")]
        [InlineData("05-03-2017", "06.546.252/0001-05")]
        [InlineData("09-11-2016", "93.020.793/0001-91")]
        public async Task Save_ShouldReturnsCompanyViewModel(DateTimeOffset requestDate, string cnpj)
        {
            var cancelOrderRepositoryRepositoryMock = new Mock<ICancelOrderRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var ideCancelamentoAppService = new CancelOrderAppService(
                cancelOrderRepositoryRepositoryMock.Object,
                mediatorHandlerMock.Object,
                mapperMock.Object);

            var createCommand = new CreateCancelOrderCommand()
            {
                RequestDate = requestDate,
                Cnpj = cnpj
            };

            // Act
            await ideCancelamentoAppService.Save(createCommand);

            // Assert
            cancelOrderRepositoryRepositoryMock.Verify(repo => repo.Add(It.IsAny<CancelOrder>()), Times.Once);
        }

        [Theory]
        [InlineData("10-06-2020", "09-11-2016", "Desistencia", "45.241.026/0001-60")]
        [InlineData("05-03-2017", "05-03-2017", "Produto falso", "79.644.315/0001-21")]
        [InlineData("09-11-2016", "10-09-2023", "Não quero mais", "48.248.533/0001-13")]
        public async Task Save_ShouldHandleNullRepositoryResult(DateTimeOffset requestDate, DateTimeOffset timeDate, string cancelReason, string cnpj)
        {
            var cancelOrderRepositoryRepositoryMock = new Mock<ICancelOrderRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var ideCancelamentoAppService = new CancelOrderAppService(
                cancelOrderRepositoryRepositoryMock.Object,
                mediatorHandlerMock.Object,
                mapperMock.Object);

            var createCommand = new CreateCancelOrderCommand()
            {
                RequestDate = requestDate,
                Cnpj = cnpj
            };

            cancelOrderRepositoryRepositoryMock.Setup(repo => repo.Add(It.IsAny<CancelOrder>())).Throws(new NullReferenceException());

            // Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => ideCancelamentoAppService.Save(createCommand));
        }

        [Theory]
        [InlineData("10-06-2020", "09-11-2016", "Desistencia", "47.224.882/0001-32")]
        [InlineData("05-03-2017", "05-03-2017", "Produto falso", "42.483.790/0001-53")]
        [InlineData("09-11-2016", "10-09-2023", "Não quero mais", "65.025.775/0001-67")]
        public async Task Save_ShouldHandleInvalidMappingResult(DateTimeOffset requestDate, DateTimeOffset timeDate, string cancelReason, string cnpj)
        {
            // Arrange
            var cancelOrderRepositoryRepositoryMock = new Mock<ICancelOrderRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var ideCancelamentoAppService = new CancelOrderAppService(
                cancelOrderRepositoryRepositoryMock.Object,
                mediatorHandlerMock.Object,
                mapperMock.Object);

            var createCommand = new CreateCancelOrderCommand()
            {
                RequestDate = requestDate,
                Cnpj = cnpj
            };

            // Act       
            cancelOrderRepositoryRepositoryMock.Setup(repo => repo.Add(It.IsAny<CancelOrder>()))
            .Throws(new ArgumentException("Invalid data"));

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(() => ideCancelamentoAppService.Save(createCommand));
        }
    }
}
