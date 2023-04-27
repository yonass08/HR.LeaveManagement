using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;

public class UpdateLeaveAllocationCommand: IRequest
{
    public UpdateLeaveAllocationDto updateLeaveAllocationDto {get; set;}
    
}
