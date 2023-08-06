using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using FluentValidation.TestHelper;
using ScheduleApi.Infrastructure.Entitys;
using ScheduleApi.Validators;
using Xunit;

namespace SheduleTest
{
    public class TestValidator
    {
        private readonly ScheduleValidator _validator;
        private readonly Fixture fixture;
        public TestValidator()
        {
            _validator = new ScheduleValidator();
            fixture = new Fixture();
        }
        [Fact]
        public void GivenAnInvalidRole_ShouldHaveValidationError()
        {
            var role = fixture.Create<string>();
            _validator.ShouldHaveValidationErrorFor(model => model.Role, role);
        }
        [Theory]
        [InlineData("Candidate")]
        [InlineData("Interviewer")]
        public void GivenAValidRole_ShouldNotHaveValidationError(string role)
            => _validator.ShouldNotHaveValidationErrorFor(model => model.Role, role);

        [Fact]
        public void GivenAnInvalidTimeSlot_ShouldHaveValidationError()
        {
            var slots = fixture.CreateMany<Slot>().ToList();
            _validator.ShouldHaveValidationErrorFor(model => model.Slots, slots);
        }

        [Fact]
        public void GivenAnValidSchedule_ShouldNotHaveValidationError()
        {
            var slots = new List<Slot>
                    {
                        new Slot{
                                DateStart= new DateTime(2021,6,14,9,0,0),
                                DateEnd= new DateTime(2021,6,14,10,0,0)
                            },
                        new Slot{
                                DateStart= new DateTime(2021,6,15,9,0,0),
                                DateEnd= new DateTime(2021,6,15,10,0,0)
                            },
                        new Slot{
                                DateStart= new DateTime(2021,6,16,9,0,0),
                                DateEnd= new DateTime(2021,6,16,10,0,0)
                            },
                        new Slot{
                            DateStart= new DateTime(2021,6,17,9,0,0),
                            DateEnd= new DateTime(2021,6,17,10,0,0)
                        }

            };
            var schedule = fixture.Build<Schedule>()
                                    .With(c => c.Role, "Candidate")
                                    .With(c => c.Slots, slots)
                                   .Create();

            var validation = _validator.Validate(schedule);
            Assert.True(validation.IsValid);
        }

        [Fact]
        public void GivenAnInValidSchedule_ShouldHaveValidationError()
        {
            var slots = new List<Slot>
                    {
                        new Slot{
                                DateStart= new DateTime(2021,6,14,9,0,0),
                                DateEnd= new DateTime(2021,6,14,11,0,0)
                            }

            };
            var schedule = fixture.Build<Schedule>()
                                    .With(c => c.Role, "Candidate")
                                    .With(c => c.Slots, slots)
                                   .Create();

            var validation = _validator.Validate(schedule);
            Assert.False(validation.IsValid);
            Assert.Contains("Slots", validation.Errors[0].PropertyName);
        }
    }
}
