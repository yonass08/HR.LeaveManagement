using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.DTOs.LeaveAllocation.Validators;

public class ILeaveAllocationDtoValidator : AbstractValidator<ILeaveAllocationDto>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public ILeaveAllocationDtoValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;

        RuleFor(p => p.NumberOfDays)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than {ComparisonValue}");

        RuleFor(p => p.period)
            .GreaterThan(DateTime.Now.Year).WithMessage("{PropertyName} must be greater than {ComparisonValue}");

        RuleFor(p => p.LeaveTypeId)
            .GreaterThan(0)
            .MustAsync(async (id, token) =>
            {
                return await _leaveTypeRepository.Exists(id);

            }).WithMessage("{PropertyName} does not exist");

    }

}
