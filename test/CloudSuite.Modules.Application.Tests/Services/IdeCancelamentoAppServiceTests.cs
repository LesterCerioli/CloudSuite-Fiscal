using AutoMapper;
using CloudSuite.Modules.Application.Services.Implementation;
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
        [InlineData("54.446.262/0001-03")]
        [InlineData("06.485.306/0001-61")]
        [InlineData("99.802.891/0001-67")]
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
        [InlineData("54.446.262/0001-03")]
        [InlineData("06.485.306/0001-61")]
        [InlineData("99.802.891/0001-67")]
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
