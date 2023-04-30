using HR.LeaveManagement.Application.DTOs.LeaveType;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Requests;

public class GetLeaveTypeListRequest: IRequest<List<LeaveTypeDto>>
{
}
