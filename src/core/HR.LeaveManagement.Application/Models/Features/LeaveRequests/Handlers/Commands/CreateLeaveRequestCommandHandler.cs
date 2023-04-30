using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using MediatR;
using HR.LeaveManagement.Application.Contracts.Infrastructure;
using HR.LeaveManagement.Application.Models;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, int>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IEmailSender _emailSender;
    private readonly IMapper _mapper;

    public CreateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository,
                                            ILeaveTypeRepository leaveTypeRepository,
                                            IEmailSender emailSender, 
                                            IMapper mapper)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _emailSender = emailSender;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var Validator = new CreateLeaveRequestDtoValidator(_leaveTypeRepository);
        var ValidationResult = await Validator.ValidateAsync(request.createLeaveRequestDto);

        if (ValidationResult.IsValid == false)
            throw new ValidationException(ValidationResult);

        var leaveRequest = _mapper.Map<LeaveRequest>(request.createLeaveRequestDto);
        leaveRequest = await _leaveRequestRepository.Add(leaveRequest);

        var email = new Email(){
            To = "yonasgebre08@gmail.com",
            Body = $"Your leave request has been successfully created",
            Subject = "Leave Request Submission "
        };

        try
        {
            await _emailSender.sendEmail(email);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return leaveRequest.Id;
    }
}
