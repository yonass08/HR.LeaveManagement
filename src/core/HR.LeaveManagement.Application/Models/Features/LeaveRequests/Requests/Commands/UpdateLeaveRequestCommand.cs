using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;

public class UpdateLeaveRequestCommand: IRequest
{
    public UpdateLeaveRequestDto updateLeaveRequestDto {get; set;}

    public ChangeLeaveRequestApprovalDto changeLeaveRequestApprovalDto {get; set;}
}
