using AutoFixture;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using ScheduleApi.Controllers;
using ScheduleApi.Infrastructure;
using ScheduleApi.Infrastructure.Entitys;
using ScheduleApi.Models;
using ScheduleApi.Profiles;
using ScheduleApi.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SheduleTest
{
    public class CalendarControllerTests
    {
        private readonly IMapper _mapper;
        private readonly IScheduleRepository _scheduleRepositoryMock;
        private readonly ISlotRepository _slotRepositoryMock;
        private readonly ScheduleValidator _scheduleValidator;
        private readonly IFixture _fixture;

        public CalendarControllerTests()
        {
            var config = CreateAutoMapperConfiguration();
            _mapper = config.CreateMapper();
            _fixture = new Fixture();
            _scheduleRepositoryMock = Substitute.For<IScheduleRepository>();
            _slotRepositoryMock = Substitute.For<ISlotRepository>();
            _scheduleValidator = new ScheduleValidator();
        }

        [Fact]
        public async Task CreateAvailability_Should_CreateWith_BadRequest()
        {
            // Arrange
            var scheduleDto = _fixture.Create<SchedulesDto>();
            var calendarController = new CalendarController(_scheduleRepositoryMock, _slotRepositoryMock, _mapper, _scheduleValidator);

            // Act
            var response = await calendarController.Post(scheduleDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Theory]
        [InlineData("Candidate")]
        [InlineData("Interviewer")]
        public async Task CreateAvailability_Should_CreateWith_Ok(string role)
        {
            // Arrange
            var slotDtos = _fixture.Build<SlotDto>()
                .With(x => x.DateStart, new DateTime(2023, 08, 10, 12, 00, 00))
                .With(x => x.DateEnd, new DateTime(2023, 08, 10, 13, 00, 00))
                .CreateMany(2)
                .ToList();

            var scheduleDto = _fixture.Build<SchedulesDto>()
                .With(x => x.Role, role)
                .With(x => x.Slots, slotDtos)
                .Create();

            var calendarController = new CalendarController(_scheduleRepositoryMock, _slotRepositoryMock, _mapper, _scheduleValidator);

            // Act
            var response = await calendarController.Post(scheduleDto);

            // Assert
            Assert.IsType<CreatedResult>(response);
        }

        [Fact]
        public async Task CreateAvailability_Should_CreateWith_InternalServerError()
        {
            // Arrange
            var scheduleDto = _fixture.Create<SchedulesDto>();
            var mapper = Substitute.For<IMapper>();

            var calendarController = new CalendarController(_scheduleRepositoryMock, _slotRepositoryMock, mapper, _scheduleValidator);

            // Act
            var response = await calendarController.Post(scheduleDto);

            // Assert
            Assert.IsType<ObjectResult>(response);
        }

        [Fact]
        public async Task CheckAvailability_Should_CreateWith_BadRequest()
        {
            // Arrange
            var person = _fixture.Create<string>();
            var interviewers = _fixture.CreateMany<string>(2).ToList();

            var calendarController = new CalendarController(_scheduleRepositoryMock, _slotRepositoryMock, _mapper, _scheduleValidator);

            // Act
            var response = await calendarController.Get(person, interviewers);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task CheckAvailability_Should_CreateWith_NoContent()
        {
            // Arrange
            var person = _fixture.Create<string>();
            var interviewers = _fixture.CreateMany<string>(2).ToList();

            _scheduleRepositoryMock.GetSlots(person, interviewers).Returns(new List<Slot>());

            var calendarController = new CalendarController(_scheduleRepositoryMock, _slotRepositoryMock, _mapper, _scheduleValidator);

            // Act
            var response = await calendarController.Get(person, interviewers);

            // Assert
            Assert.IsType<NoContentResult>(response);
        }

        [Fact]
        public async Task CheckAvailability_Should_CreateWith_Ok()
        {
            // Arrange
            var person = _fixture.Create<string>();
            var interviewers = _fixture.CreateMany<string>(2).ToList();
            var slots = _fixture.CreateMany<Slot>(5).ToList();

            _scheduleRepositoryMock.GetSlots(person, interviewers).Returns(slots);

            var calendarController = new CalendarController(_scheduleRepositoryMock, _slotRepositoryMock, _mapper, _scheduleValidator);

            // Act
            var response = await calendarController.Get(person, interviewers);

            // Assert
            Assert.IsType<OkObjectResult>(response);
        }

        [Fact]
        public async Task CheckAvailability_Should_CreateWith_InternalServerError()
        {
            // Arrange
            var person = _fixture.Create<string>();
            var interviewers = _fixture.CreateMany<string>(2).ToList();

            _scheduleRepositoryMock.GetSlots(person, interviewers).Throws<Exception>();

            var calendarController = new CalendarController(_scheduleRepositoryMock, _slotRepositoryMock, _mapper, _scheduleValidator);

            // Act
            var response = await calendarController.Get(person, interviewers);

            // Assert
            Assert.IsType<ObjectResult>(response);
        }


        private MapperConfiguration CreateAutoMapperConfiguration()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ScheduleProfile>();
                cfg.AddProfile<SlotProfile>();
            });
        }
    }
}
