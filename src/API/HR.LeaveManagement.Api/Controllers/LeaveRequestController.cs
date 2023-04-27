using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class LeaveRequestController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveRequestController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    public async Task<ActionResult<List<LeaveRequestListDto>>> Get()
    {
        var leaveRequests = await _mediator.Send(new GetLeaveRequestListRequest());
        return Ok(leaveRequests);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LeaveRequestDto>> GetById(int id)
    {
        var leaveRequest = await _mediator.Send(new GetLeaveRequestDetailRequest(){Id = id});
        return Ok(leaveRequest);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Post([FromBody] CreateLeaveRequestDto leaveRequest)
    {
        var command = new CreateLeaveRequestCommand(){createLeaveRequestDto = leaveRequest};
        int Id = await _mediator.Send(command);

        return Ok(Id);
    }

    [HttpPut()]
    public async Task<ActionResult> Put([FromBody] UpdateLeaveRequestDto leaveRequest)
    {
        var command = new UpdateLeaveRequestCommand(){updateLeaveRequestDto = leaveRequest};
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("changeapproval")]

    public async Task<ActionResult> ChangeApproval([FromBody] ChangeLeaveRequestApprovalDto leaveRequest)
    {
        var command = new UpdateLeaveRequestCommand(){changeLeaveRequestApprovalDto = leaveRequest};
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteLeaveRequestCommand(){Id = id};
        await _mediator.Send(command);

        return NoContent();
    }
}
