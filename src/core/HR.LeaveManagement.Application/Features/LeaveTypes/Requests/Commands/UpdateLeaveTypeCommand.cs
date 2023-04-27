using HR.LeaveManagement.Application.DTOs.LeaveType;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;

public class UpdateLeaveTypeCommand: IRequest
{
    public LeaveTypeDto leaveTypeDto {get; set;}
}
