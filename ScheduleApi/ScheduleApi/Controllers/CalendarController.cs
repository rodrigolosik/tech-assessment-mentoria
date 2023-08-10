using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ScheduleApi.Infrastructure;
using ScheduleApi.Infrastructure.Entitys;
using ScheduleApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalendarController : ControllerBase
    {
        private readonly IScheduleRepository _repository;
        private readonly ISlotRepository _slotRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<Schedule> _scheduleValidator;

        public CalendarController(
            IScheduleRepository repository,
            ISlotRepository slotRepository,
            IMapper mapper,
            IValidator<Schedule> scheduleValidator)
        {
            _repository = repository;
            _slotRepository = slotRepository;
            _mapper = mapper;
            _scheduleValidator = scheduleValidator;
        }

        [HttpPost("availability")]
        public async Task<IActionResult> Post(SchedulesDto dto)
        {
            try
            {
                var schedule = _mapper.Map<Schedule>(dto);

                var validation = _scheduleValidator.Validate(schedule);

                if (!validation.IsValid)
                    return BadRequest(string.Join(", ", validation.Errors));

                await _repository.AddAsync(schedule);
                await _repository.SaveChangesAsync();

                return Created("Calendar/availability", dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("availability/{person}")]
        public async Task<IActionResult> Get(string person, [FromQuery] List<string> interviewers)
        {
            try
            {
                var slots = await _repository.GetSlots(person, interviewers);

                if (slots is null)
                    return BadRequest($"Person {person} not found. Please check if the name is right and try again.");

                else if (!slots.Any())
                    return NoContent();

                var dto = _mapper.Map<List<SlotDto>>(slots);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        #region Test
        [HttpGet("TestData")]
        public async Task<IActionResult> PopulateTestData()
        {

            var allSlots = await _slotRepository.GetAllAsync();
            foreach (var item in allSlots)
            {
                await _slotRepository.DeleteAsync(item);
            }
            await _slotRepository.SaveChangesAsync();
            var all = await _repository.GetAllAsync();
            foreach (var item in all)
            {
                await _repository.DeleteAsync(item);
            }
            await _repository.SaveChangesAsync();
            var marySlots = new List<Slot>();
            for (int i = 14; i < 19; i++)
            {
                marySlots.Add(
                        new Slot
                        {
                            DateStart = new DateTime(2021, 6, i, 9, 0, 0),
                            DateEnd = new DateTime(2021, 6, i, 10, 0, 0)
                        });
                marySlots.Add(
                      new Slot
                      {
                          DateStart = new DateTime(2021, 6, i, 10, 0, 0),
                          DateEnd = new DateTime(2021, 6, i, 11, 0, 0)
                      });
                marySlots.Add(
                      new Slot
                      {
                          DateStart = new DateTime(2021, 6, i, 11, 0, 0),
                          DateEnd = new DateTime(2021, 6, i, 12, 0, 0)
                      });
                marySlots.Add(
                      new Slot
                      {
                          DateStart = new DateTime(2021, 6, i, 12, 0, 0),
                          DateEnd = new DateTime(2021, 6, i, 13, 0, 0)
                      });
                marySlots.Add(
                      new Slot
                      {
                          DateStart = new DateTime(2021, 6, i, 13, 0, 0),
                          DateEnd = new DateTime(2021, 6, i, 14, 0, 0)
                      });
                marySlots.Add(
                      new Slot
                      {
                          DateStart = new DateTime(2021, 6, i, 14, 0, 0),
                          DateEnd = new DateTime(2021, 6, i, 15, 0, 0)
                      });
                marySlots.Add(
                   new Slot
                   {
                       DateStart = new DateTime(2021, 6, i, 15, 0, 0),
                       DateEnd = new DateTime(2021, 6, i, 16, 0, 0)
                   });
            }

            var dianaSlots = new List<Slot>
            {
                new Slot
                {
                    DateStart = new DateTime(2021, 6, 14, 12, 0, 0),
                    DateEnd = new DateTime(2021, 6, 14, 13, 0, 0)
                },
                new Slot
                {
                    DateStart = new DateTime(2021, 6, 14, 13, 0, 0),
                    DateEnd = new DateTime(2021, 6, 14, 14, 0, 0)
                },
                new Slot
                {
                    DateStart = new DateTime(2021, 6, 14, 15, 0, 0),
                    DateEnd = new DateTime(2021, 6, 14, 16, 0, 0)
                },
                new Slot
                {
                    DateStart = new DateTime(2021, 6, 14, 14, 0, 0),
                    DateEnd = new DateTime(2021, 6, 14, 15, 0, 0)
                },
                new Slot
                {
                    DateStart = new DateTime(2021, 6, 14, 15, 0, 0),
                    DateEnd = new DateTime(2021, 6, 14, 16, 0, 0)
                },
                new Slot
                {
                    DateStart = new DateTime(2021, 6, 14, 16, 0, 0),
                    DateEnd = new DateTime(2021, 6, 14, 17, 0, 0)
                },
                new Slot
                {
                    DateStart = new DateTime(2021, 6, 14, 17, 0, 0),
                    DateEnd = new DateTime(2021, 6, 14, 18, 0, 0)
                },
                //tuesday next week
                new Slot
                {
                    DateStart = new DateTime(2021, 6, 15, 9, 0, 0),
                    DateEnd = new DateTime(2021, 6, 15, 10, 0, 0)
                },
                new Slot
                {
                    DateStart = new DateTime(2021, 6, 15, 10, 0, 0),
                    DateEnd = new DateTime(2021, 6, 15, 11, 0, 0)
                },
                new Slot
                {
                    DateStart = new DateTime(2021, 6, 15, 11, 0, 0),
                    DateEnd = new DateTime(2021, 6, 15, 12, 0, 0)
                },
                //Thursday next week
                new Slot
                {
                    DateStart = new DateTime(2021, 6, 17, 9, 0, 0),
                    DateEnd = new DateTime(2021, 6, 17, 10, 0, 0)
                },
                new Slot
                {
                    DateStart = new DateTime(2021, 6, 17, 10, 0, 0),
                    DateEnd = new DateTime(2021, 6, 17, 11, 0, 0)
                },
                new Slot
                {
                    DateStart = new DateTime(2021, 6, 17, 11, 0, 0),
                    DateEnd = new DateTime(2021, 6, 17, 12, 0, 0)
                }
            };

            var entity = new Schedule
            {
                Name = "John",
                Role = "Candidate",
                Slots = new List<Slot>
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
                        },
                        new Slot{
                            DateStart= new DateTime(2021,6,18,9,0,0),
                            DateEnd= new DateTime(2021,6,18,10,0,0)
                        },
                        //Wednesday
                          new Slot{
                            DateStart= new DateTime(2021,6,16,10,0,0),
                            DateEnd= new DateTime(2021,6,16,11,0,0)
                        },
                         new Slot{
                            DateStart= new DateTime(2021,6,16,11,0,0),
                            DateEnd= new DateTime(2021,6,16,12,0,0)
                        }

                    }
            };
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
            entity = new Schedule
            {
                Name = "Mary",
                Role = "Interviewer",
                Slots = marySlots
            };
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
            entity = new Schedule
            {
                Name = "Diana",
                Role = "Interviewer",
                Slots = dianaSlots
            };
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
            return Ok();
        }
        #endregion

    }
}
