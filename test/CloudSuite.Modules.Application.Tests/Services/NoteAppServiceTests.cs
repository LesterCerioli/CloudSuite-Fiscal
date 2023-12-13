using AutoMapper;
using CloudSuite.Modules.Application.Handlers.City;
using CloudSuite.Modules.Application.Handlers.Note;
using CloudSuite.Modules.Application.Services.Implementation;
using CloudSuite.Modules.Application.ViewModels;
using CloudSuite.Modules.Common.ValueObjects;
using CloudSuite.Modules.Domain.Contracts;
using CloudSuite.Modules.Domain.Models;
using Moq;
using NetDevPack.Mediator;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xunit;

namespace CloudSuite.Modules.Application.Tests.Services
{
    public class NoteAppServiceTests
    {
        [Theory]
        [InlineData("54.446.262/0001-03", "82763", 22.22)]
        [InlineData("06.485.306/0001-61", "23657", 32.87)]
        [InlineData("99.802.891/0001-67", "87677", 94.65)]
        public async Task GetByCnpj_ShouldReturnsCompanyViewModel(string cnpj, string noteNumber, decimal value)
        {
            var noteRepositoryMock = new Mock<INoteRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var noteAppService = new NoteAppService(
                noteRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var address = new Address(Guid.NewGuid(), new City(Guid.NewGuid(), "oudricandrai", new State(Guid.NewGuid(), "ba", "Bahia", new Country("Salvaodr", "123", true, true, true, false, false), Guid.NewGuid())), new District(), "John Doe", "rua dos patos");
            var tomadorServico = new TomadorServico(new Cnpj("54.446.262/0001-03"), "385.903.095.437", "385.903.095.437", "21.904.074-6", "Olivia e Sophia Filmagens Ltda", "Olivia e Sophia Filmagens", address, 1);
            var country = new Country("Brasil", "7632732643", true, false, true, true, false);
            var district = new District(Guid.NewGuid(), "District1", "Type1", "Location1");
            var prestador = new Prestador(new Cnpj("54.446.262/0001-03"), "385.903.095.437", "385.903.095.437", "21.904.074-6", "Olivia e Sophia Filmagens Ltda", "Olivia e Sophia Filmagens", address, 1);

            var noteEntity = new Note(tomadorServico, address, country, district, prestador, new Cnpj(cnpj), noteNumber, value);

            noteRepositoryMock.Setup(repo => repo.GetByCnpj(cnpj)).ReturnsAsync(noteEntity);

            var expectedViewModel = new NoteViewModel()
            {
                Id = noteEntity.Id,
                Cnpj = cnpj,
                NoteNumber = noteNumber,
                EmissionDate = DateTime.Now,
                Value = value
            };

            mapperMock.Setup(mapper => mapper.Map<NoteViewModel>(noteEntity)).Returns(expectedViewModel);

            // Act
            var result = await noteAppService.GetByCnpj(cnpj);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        [Theory]
        [InlineData("54.446.262/0001-03")]
        [InlineData("06.485.306/0001-61")]
        [InlineData("99.802.891/0001-67")]
        public async Task GetByCnpj_ShouldHandleNullRepositoryResult(string cnpj)
        {
            // Arrange
            var noteRepositoryMock = new Mock<INoteRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var noteAppService = new NoteAppService(
                noteRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            noteRepositoryMock.Setup(repo => repo.GetByCnpj(It.IsAny<Cnpj>()))
                .ReturnsAsync((Note)null); // Simulate null result from the repository

            // Act
            var result = await noteAppService.GetByCnpj(cnpj);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("54.446.262/0001-03")]
        [InlineData("06.485.306/0001-61")]
        [InlineData("99.802.891/0001-67")]
        public async Task GetByCnpj_ShouldHandleInvalidMappingResult(string cnpj)
        {
            // Arrange
            var noteRepositoryMock = new Mock<INoteRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var noteAppService = new NoteAppService(
                noteRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            noteRepositoryMock.Setup(repo => repo.GetByCnpj(It.IsAny<Cnpj>()))
                .ThrowsAsync(new ArgumentException("Invalid data")); // Simulate null result from the repository

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => noteAppService.GetByCnpj(cnpj));
        }

        [Theory]
        [InlineData("54.446.262/0001-03", "82763782367323", 22.22)]
        [InlineData("06.485.306/0001-61", "23657289348467", 32.87)]
        [InlineData("99.802.891/0001-67", "87375648390303", 94.65)]
        public async Task GetByNoteNumber_ShouldReturnsCompanyViewModel(string cnpj, string noteNumber, decimal value)
        {
            var noteRepositoryMock = new Mock<INoteRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var noteAppService = new NoteAppService(
                noteRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var address = new Address(Guid.NewGuid(), new City(Guid.NewGuid(), "oudricandrai", new State(Guid.NewGuid(), "ba", "Bahia", new Country("Salvaodr", "123", true, true, true, false, false), Guid.NewGuid())), new District(), "John Doe", "rua dos patos");
            var tomadorServico = new TomadorServico(new Cnpj("54.446.262/0001-03"), "385.903.095.437", "385.903.095.437", "21.904.074-6", "Olivia e Sophia Filmagens Ltda", "Olivia e Sophia Filmagens", address, 1);
            var country = new Country("Brasil", "7632732643", true, false, true, true, false);
            var district = new District(Guid.NewGuid(), "District1", "Type1", "Location1");
            var prestador = new Prestador(new Cnpj("54.446.262/0001-03"), "385.903.095.437", "385.903.095.437", "21.904.074-6", "Olivia e Sophia Filmagens Ltda", "Olivia e Sophia Filmagens", address, 1);

            var noteEntity = new Note(tomadorServico, address, country, district, prestador, new Cnpj(cnpj), noteNumber, value);

            noteRepositoryMock.Setup(repo => repo.GetByNoteNumber(noteNumber)).ReturnsAsync(noteEntity);

            var expectedViewModel = new NoteViewModel()
            {
                Id = noteEntity.Id,
                Cnpj = cnpj,
                NoteNumber = noteNumber,
                EmissionDate = DateTime.Now,
                Value = value
            };

            mapperMock.Setup(mapper => mapper.Map<NoteViewModel>(noteEntity)).Returns(expectedViewModel);

            // Act
            var result = await noteAppService.GetByNoteNumber(noteNumber);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        [Theory]
        [InlineData("82763782367323")]
        [InlineData("23657289348467")]
        [InlineData("87375648390303")]
        public async Task GetByNoteNumber_ShouldHandleNullRepositoryResult(string noteNumber)
        {
            // Arrange
            var noteRepositoryMock = new Mock<INoteRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var noteAppService = new NoteAppService(
                noteRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            noteRepositoryMock.Setup(repo => repo.GetByNoteNumber(It.IsAny<string>()))
                .ReturnsAsync((Note)null); // Simulate null result from the repository

            // Act
            var result = await noteAppService.GetByNoteNumber(noteNumber);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("23213123")]
        [InlineData("23343254")]
        [InlineData("54643223")]
        public async Task GetByNoteNumber_ShouldHandleInvalidMappingResult(string noteNumber)
        {
            // Arrange
            var noteRepositoryMock = new Mock<INoteRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var noteAppService = new NoteAppService(
                noteRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            noteRepositoryMock.Setup(repo => repo.GetByNoteNumber(It.IsAny<string>()))
                .ThrowsAsync(new ArgumentException("Invalid data")); // Simulate null result from the repository

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => noteAppService.GetByNoteNumber(noteNumber));
        }

        [Theory]
        [InlineData("54.446.262/0001-03", "82763782367323", 22.22)]
        [InlineData("06.485.306/0001-61", "23657289348467", 32.87)]
        [InlineData("99.802.891/0001-67", "87375648390303", 94.65)]
        public async Task GetByValue_ShouldReturnsCompanyViewModel(string cnpj, string noteNumber, decimal value)
        {
            var noteRepositoryMock = new Mock<INoteRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var noteAppService = new NoteAppService(
                noteRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var address = new Address(Guid.NewGuid(), new City(Guid.NewGuid(), "oudricandrai", new State(Guid.NewGuid(), "ba", "Bahia", new Country("Salvaodr", "123", true, true, true, false, false), Guid.NewGuid())), new District(), "John Doe", "rua dos patos");
            var tomadorServico = new TomadorServico(new Cnpj("54.446.262/0001-03"), "385.903.095.437", "385.903.095.437", "21.904.074-6", "Olivia e Sophia Filmagens Ltda", "Olivia e Sophia Filmagens", address, 1);
            var country = new Country("Brasil", "7632732643", true, false, true, true, false);
            var district = new District(Guid.NewGuid(), "District1", "Type1", "Location1");
            var prestador = new Prestador(new Cnpj("54.446.262/0001-03"), "385.903.095.437", "385.903.095.437", "21.904.074-6", "Olivia e Sophia Filmagens Ltda", "Olivia e Sophia Filmagens", address, 1);

            var noteEntity = new Note(tomadorServico, address, country, district, prestador, new Cnpj(cnpj), noteNumber, value);


            noteRepositoryMock.Setup(repo => repo.GetByValue(value)).ReturnsAsync(noteEntity);

            var expectedViewModel = new NoteViewModel()
            {
                Id = noteEntity.Id,
                Cnpj = cnpj,
                NoteNumber = noteNumber,
                EmissionDate = DateTime.Now,
                Value = value
            };

            mapperMock.Setup(mapper => mapper.Map<NoteViewModel>(noteEntity)).Returns(expectedViewModel);

            // Act
            var result = await noteAppService.GetByValue(value);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        [Theory]
        [InlineData(32.56)]
        [InlineData(87.87)]
        [InlineData(98.12)]
        public async Task GetByValue_ShouldHandleNullRepositoryResult(decimal value)
        {
            // Arrange
            var noteRepositoryMock = new Mock<INoteRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var noteAppService = new NoteAppService(
                noteRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            noteRepositoryMock.Setup(repo => repo.GetByValue(It.IsAny<decimal>()))
                .ReturnsAsync((Note)null); // Simulate null result from the repository

            // Act
            var result = await noteAppService.GetByValue(value);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData(32.56)]
        [InlineData(87.87)]
        [InlineData(98.12)]
        public async Task GetByValue_ShouldHandleInvalidMappingResult(decimal value)
        {
            // Arrange
            var noteRepositoryMock = new Mock<INoteRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var noteAppService = new NoteAppService(
                noteRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            noteRepositoryMock.Setup(repo => repo.GetByValue(It.IsAny<decimal>()))
                .ThrowsAsync(new ArgumentException("Invalid data")); // Simulate null result from the repository

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => noteAppService.GetByValue(value));
        }

        [Theory]
        [InlineData("54.446.262/0001-03", "82763782367323", 22.22)]
        [InlineData("06.485.306/0001-61", "23657289348467", 32.87)]
        [InlineData("99.802.891/0001-67", "87375648390303", 94.65)]
        public async Task GetByEmissionDate_ShouldReturnsCompanyViewModel(string cnpj, string noteNumber, decimal value)
        {
            var noteRepositoryMock = new Mock<INoteRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var noteAppService = new NoteAppService(
                noteRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var address = new Address(Guid.NewGuid(), new City(Guid.NewGuid(), "oudricandrai", new State(Guid.NewGuid(), "ba", "Bahia", new Country("Salvaodr", "123", true, true, true, false, false), Guid.NewGuid())), new District(), "John Doe", "rua dos patos");
            var tomadorServico = new TomadorServico(new Cnpj("54.446.262/0001-03"), "385.903.095.437", "385.903.095.437", "21.904.074-6", "Olivia e Sophia Filmagens Ltda", "Olivia e Sophia Filmagens", address, 1);
            var country = new Country("Brasil", "7632732643", true, false, true, true, false);
            var district = new District(Guid.NewGuid(), "District1", "Type1", "Location1");
            var prestador = new Prestador(new Cnpj("54.446.262/0001-03"), "385.903.095.437", "385.903.095.437", "21.904.074-6", "Olivia e Sophia Filmagens Ltda", "Olivia e Sophia Filmagens", address, 1);

            var noteEntity = new Note(tomadorServico, address, country, district, prestador, new Cnpj(cnpj), noteNumber, value);


            noteRepositoryMock.Setup(repo => repo.GetByEmissionDate(noteEntity.EmissionDate)).ReturnsAsync(noteEntity);

            var expectedViewModel = new NoteViewModel()
            {
                Id = noteEntity.Id,
                Cnpj = cnpj,
                NoteNumber = noteNumber,
                EmissionDate = DateTime.Now,
                Value = value
            };

            mapperMock.Setup(mapper => mapper.Map<NoteViewModel>(noteEntity)).Returns(expectedViewModel);

            // Act
            var result = await noteAppService.GetByEmissionDate(noteEntity.EmissionDate);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        [Theory]
        [InlineData("10-12-2023")]
        [InlineData("09-11-2016")]
        [InlineData("05-03-2017")]
        public async Task GetByEmissionDate_ShouldHandleNullRepositoryResult(DateTime emissionDate)
        {
            // Arrange
            var noteRepositoryMock = new Mock<INoteRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var noteAppService = new NoteAppService(
                noteRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            noteRepositoryMock.Setup(repo => repo.GetByEmissionDate(It.IsAny<DateTime>()))
                .ReturnsAsync((Note)null); // Simulate null result from the repository

            // Act
            var result = await noteAppService.GetByEmissionDate(emissionDate);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("08-05-2022")]
        [InlineData("02-07-2018")]
        [InlineData("12-10-2019")]
        public async Task GetByEmissionDate_ShouldHandleInvalidMappingResult(DateTime emissionDate)
        {
            // Arrange
            var noteRepositoryMock = new Mock<INoteRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var noteAppService = new NoteAppService(
                noteRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            noteRepositoryMock.Setup(repo => repo.GetByEmissionDate(It.IsAny<DateTime>()))
                .ThrowsAsync(new ArgumentException("Invalid data")); // Simulate null result from the repository

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => noteAppService.GetByEmissionDate(emissionDate));
        }

        [Theory]
        [InlineData("54.446.262/0001-03", "82763782367323", 22.22)]
        [InlineData("06.485.306/0001-61", "23657289348467", 32.87)]
        [InlineData("99.802.891/0001-67", "87375648390303", 94.65)]
        public async Task Save_ShouldReturnsCompanyViewModel(string cnpj, string noteNumber, decimal value)
        {
            var noteRepositoryMock = new Mock<INoteRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var noteAppService = new NoteAppService(
                noteRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var createCommand = new CreateNoteCommand()
            {
                Cnpj = cnpj,
                NoteNumber = noteNumber,
                Value = value                
            };

            // Act
            await noteAppService.Save(createCommand);

            // Assert
            noteRepositoryMock.Verify(repo => repo.Add(It.IsAny<Note>()), Times.Once);
        }

        [Theory]
        [InlineData("54.446.262/0001-03", "82763782367323", 22.22)]
        [InlineData("06.485.306/0001-61", "23657289348467", 32.87)]
        [InlineData("99.802.891/0001-67", "87375648390303", 94.65)]
        public async Task Save_ShouldHandleNullRepositoryResult(string cnpj, string noteNumber, decimal value)
        {
            var noteRepositoryMock = new Mock<INoteRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var noteAppService = new NoteAppService(
                noteRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var createCommand = new CreateNoteCommand()
            {
                Cnpj = cnpj,
                NoteNumber = noteNumber,
                Value = value
            };

            noteRepositoryMock.Setup(repo => repo.Add(It.IsAny<Note>())).Throws(new NullReferenceException());

            // Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => noteAppService.Save(createCommand));
        }

        [Theory]
        [InlineData("54.446.262/0001-03", "82763782367323", 22.22)]
        [InlineData("06.485.306/0001-61", "23657289348467", 32.87)]
        [InlineData("99.802.891/0001-67", "87375648390303", 94.65)]
        public async Task Save_ShouldHandleInvalidMappingResult(string cnpj, string noteNumber, decimal value)
        {
            // Arrange
            var noteRepositoryMock = new Mock<INoteRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var noteAppService = new NoteAppService(
                noteRepositoryMock.Object,
                mapperMock.Object,
                mediatorHandlerMock.Object);

            var createCommand = new CreateNoteCommand()
            {
                Cnpj = cnpj,
                NoteNumber = noteNumber,
                Value = value
            };

            // Act       
            noteRepositoryMock.Setup(repo => repo.Add(It.IsAny<Note>()))
            .Throws(new ArgumentException("Invalid data"));

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(() => noteAppService.Save(createCommand));
        }
    }
}
