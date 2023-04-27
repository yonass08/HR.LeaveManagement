using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands;

public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    private readonly IMapper _mapper;

    public UpdateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _leaveTypeRepository = leaveTypeRepository;

        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
    {

        if (request.updateLeaveRequestDto != null)
        {
            var Validator = new UpdateLeaveRequestDtoValidator(_leaveTypeRepository);
            var ValidationResult = await Validator.ValidateAsync(request.updateLeaveRequestDto);

            if (ValidationResult.IsValid == false)
                throw new ValidationException(ValidationResult);

            var leaveRequest = await _leaveRequestRepository.Get(request.updateLeaveRequestDto.Id);
            _mapper.Map(request.updateLeaveRequestDto, leaveRequest);
            await _leaveRequestRepository.Update(leaveRequest);
        }
        else if (request.changeLeaveRequestApprovalDto != null)
        {
            int Id = request.changeLeaveRequestApprovalDto.Id;
            bool Exists = await _leaveRequestRepository.Exists(Id);
            if (Exists == false)
                throw new NotFoundException(nameof(LeaveRequest), Id);
            await _leaveRequestRepository.ChangeApprovalStatus(request.changeLeaveRequestApprovalDto.Id, request.changeLeaveRequestApprovalDto.Approved);
        }


        return Unit.Value;
    }
}
