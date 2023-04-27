using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeaveAllocationController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveAllocationController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    public async Task<ActionResult<List<LeaveAllocationDto>>> Get()
    {
        var leaveAllocations = await _mediator.Send(new GetLeaveAllocationListRequest());
        return Ok(leaveAllocations);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LeaveAllocationDto>> GetById(int id)
    {
        var leaveAllocation = await _mediator.Send(new GetLeaveAllocationDetailRequest(){Id = id});
        return Ok(leaveAllocation);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Post([FromBody] CreateLeaveAllocationDto leaveAllocation)
    {
        var command = new CreateLeaveAllocationCommand(){createLeaveAllocationDto = leaveAllocation};
        int Id = await _mediator.Send(command);

        return Ok(Id);
    }

    [HttpPut()]
    public async Task<ActionResult> Put([FromBody] UpdateLeaveAllocationDto leaveAllocation)
    {
        var command = new UpdateLeaveAllocationCommand(){updateLeaveAllocationDto = leaveAllocation};
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteLeaveAllocationCommand(){Id = id};
        await _mediator.Send(command);

        return NoContent();
    }
}
