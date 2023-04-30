using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands;

public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;

    public UpdateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var Validator = new UpdateLeaveAllocationDtoValidator(_leaveTypeRepository);
        var ValidationResult = await Validator.ValidateAsync(request.updateLeaveAllocationDto);

        if (ValidationResult.IsValid == false)
            throw new ValidationException(ValidationResult);

        var leaveAllocation = await _leaveAllocationRepository.Get(request.updateLeaveAllocationDto.Id);
        _mapper.Map(request.updateLeaveAllocationDto, leaveAllocation);
        await _leaveAllocationRepository.Update(leaveAllocation);

        return Unit.Value;
    }
}
