using FluentValidation;

namespace HR.LeaveManagement.Application.DTOs.LeaveType.Validators;

public class ILeaveTypeDtoValidator: AbstractValidator<ILeaveTypeDto>
{
    public ILeaveTypeDtoValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} should not be empty")
            .NotNull().WithMessage("{PropertyName} should not be null")
            .MaximumLength(50).WithMessage("{PropertyName} can not exceed 50 characters");

        RuleFor(p => p.DefaultDays)
            .NotNull().WithMessage("{PropertyName} is required")
            .GreaterThan(0)
            .LessThan(50);
    }
}
