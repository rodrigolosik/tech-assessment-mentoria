using FluentValidation;
using ScheduleApi.Infrastructure.Entitys;

namespace ScheduleApi.Validators
{
    public class ScheduleValidator : AbstractValidator<Schedule>
    {
        public ScheduleValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(c => c.Role).NotEmpty().WithMessage("Role is required");
            RuleFor(c => c.Role).Must(c => c == "Candidate" || c == "Interviewer")
                                .WithMessage("Role should be Candidate or Interviewer");
            RuleForEach(c => c.Slots).SetValidator(new SlotValidator());
        }
    }
}
