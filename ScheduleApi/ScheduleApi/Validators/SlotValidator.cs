using System;
using FluentValidation;
using ScheduleApi.Infrastructure.Entitys;

namespace ScheduleApi.Validators
{
    public class SlotValidator : AbstractValidator<Slot>
    {
        const string errorMessage = "Time should be 1-hour period of time that spreads from the beginning of any hour until the beginning of the next hour.";
        public SlotValidator()
        {
            RuleFor(c => c.DateStart)
                .Must((date) => IsValid(date)).WithMessage("Invalid Start Date." + errorMessage);
            RuleFor(c => c.DateEnd)
               .Must((date) => IsValid(date)).WithMessage("Invalid End Date." + errorMessage);
            RuleFor(c => c)
             .Must((slot) => IsValid(slot.DateStart, slot.DateEnd)).WithMessage("Invalid slot" + errorMessage);

        }

        private bool IsValid(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
            {
                return false;
            }
            if (endDate.Date != startDate.Date)
            {
                return false;
            }
            if (endDate.Hour - startDate.Hour != 1)
            {
                return false;
            }
            return true;
        }

        public bool IsValid(DateTime date)
        {
            return date.Minute == 0 && date.Second == 0;
        }
    }
}
