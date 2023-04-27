using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveType.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands;

public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;

    public UpdateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var Validator = new UpdateLeaveTypeDtoValidator();
        var ValidationResult = await Validator.ValidateAsync(request.leaveTypeDto);

        if (ValidationResult.IsValid == false)
            throw new ValidationException(ValidationResult);

        var leaveType = await _leaveTypeRepository.Get(request.leaveTypeDto.Id);

        if (leaveType == null)
            throw new NotFoundException(nameof(LeaveType), request.leaveTypeDto.Id);

        _mapper.Map(request.leaveTypeDto, leaveType);
        await _leaveTypeRepository.Update(leaveType);

        return Unit.Value;
    }
}
