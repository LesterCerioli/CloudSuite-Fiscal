using AutoMapper;
using CloudSuite.Modules.Application.Handlers.District;
using CloudSuite.Modules.Application.Handlers.FederalTax;
using CloudSuite.Modules.Application.Services.Contracts;
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
using System.Xml.Linq;
using Xunit;

namespace CloudSuite.Modules.Application.Tests.Services
{
    public class DistrictAppServiceTests
    {
        [Theory]
        [InlineData("District1", "Type1", "Location1")]
        [InlineData("District2", "Type2", "Location2")]
        [InlineData("District3", "Type3", "Location3")]
        public async Task GetByName_ShouldReturnsCompanyViewModel(string name, string type, string location)
        {
            var districRepositoryMock = new Mock<IDistrictRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var districtAppService = new DistrictAppService(
                districRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object
                );

            var districtEntity = new District(Guid.NewGuid(), name, type, location);
            districRepositoryMock.Setup(repo => repo.GetByName(name)).ReturnsAsync(districtEntity);

            var expectedViewModel = new DistrictViewModel()
            {
                Id = districtEntity.Id,
                Name = name,
                Type = type,
                Location = location
            };

            mapperMock.Setup(mapper => mapper.Map<DistrictViewModel>(districtEntity)).Returns(expectedViewModel);

            // Act
            var result = await districtAppService.GetByName(name);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        [Theory]
        [InlineData("District4")]
        [InlineData("District5")]
        [InlineData("District6")]
        public async Task GetByName_ShouldHandleNullRepositoryResult(string name)
        {
            // Arrange
            var districRepositoryMock = new Mock<IDistrictRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var districtAppService = new DistrictAppService(
                districRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object
            );

            districRepositoryMock.Setup(repo => repo.GetByName(It.IsAny<string>()))
                .ReturnsAsync((District)null); // Simulate null result from the repository

            // Act
            var result = await districtAppService.GetByName(name);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("District7")]
        [InlineData("District8")]
        [InlineData("District9")]
        public async Task GetByName_ShouldHandleInvalidMappingResult(string name)
        {
            // Arrange
            var districRepositoryMock = new Mock<IDistrictRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var districtAppService = new DistrictAppService(
                districRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object
            );

            districRepositoryMock.Setup(repo => repo.GetByName(It.IsAny<string>()))
                .ThrowsAsync(new ArgumentException("Invalid data")); // Simulate null result from the repository

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => districtAppService.GetByName(name));
        }

        [Theory]
        [InlineData("District1", "Type1", "Location1")]
        [InlineData("District2", "Type2", "Location2")]
        [InlineData("District3", "Type3", "Location3")]
        public async Task Save_ShouldReturnsCompanyViewModel(string name, string type, string location)
        {
            var districRepositoryMock = new Mock<IDistrictRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var districtAppService = new DistrictAppService(
                districRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object
            );

            var createCommand = new CreateDistrictCommand()
            {
               Name = name,
               Type = type,
               Location = location
            };

            // Act
            await districtAppService.Save(createCommand);

            // Assert
            districRepositoryMock.Verify(repo => repo.Add(It.IsAny<District>()), Times.Once);
        }

        [Theory]
        [InlineData("District1", "Type1", "Location1")]
        [InlineData("District2", "Type2", "Location2")]
        [InlineData("District3", "Type3", "Location3")]
        public async Task Save_ShouldHandleNullRepositoryResult(string name, string type, string location)
        {
            var districRepositoryMock = new Mock<IDistrictRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var districtAppService = new DistrictAppService(
                districRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object
            );

            var createCommand = new CreateDistrictCommand()
            {
                Name = name,
                Type = type,
                Location = location
            };

            districRepositoryMock.Setup(repo => repo.Add(It.IsAny<District>())).Throws(new NullReferenceException());

            // Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => districtAppService.Save(createCommand));
        }

        [Theory]
        [InlineData("District1", "Type1", "Location1")]
        [InlineData("District2", "Type2", "Location2")]
        [InlineData("District3", "Type3", "Location3")]
        public async Task Save_ShouldHandleInvalidMappingResult(string name, string type, string location)
        {
            // Arrange
            var districRepositoryMock = new Mock<IDistrictRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var districtAppService = new DistrictAppService(
                districRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object
            );

            var createCommand = new CreateDistrictCommand()
            {
                Name = name,
                Type = type,
                Location = location
            };

            // Act       
            districRepositoryMock.Setup(repo => repo.Add(It.IsAny<District>()))
            .Throws(new ArgumentException("Invalid data"));

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(() => districtAppService.Save(createCommand));
        }

    }
}
