using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Features.WorkTaskPriorityType.Commands.CreateWorkTaskPriorityType;
using TaskManager.Application.Features.WorkTaskPriorityType.Commands.DeleteWorkTaskPriorityType;
using TaskManager.Application.Features.WorkTaskPriorityType.Commands.UpdateWorkTaskPriorityType;
using TaskManager.Application.Features.WorkTaskPriorityType.Queries.GetAllWorkTaskPriorityTypes;
using TaskManager.Application.Features.WorkTaskPriorityType.Queries.GetWorkTaskPriorityDetails;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManager.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class WorkTaskPriorityTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public WorkTaskPriorityTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/<WorkTaskPriorityTypesController>
    [HttpGet]
    public async Task<ActionResult<List<WorkTaskPriorityTypeDto>>> Get()
    {
        var workTaskPriorityTypes = await _mediator.Send(new GetAllWorkTaskPriorityTypesQuery());
        return Ok(workTaskPriorityTypes);
    }

    // GET api/<WorkTaskPriorityTypesController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<WorkTaskPriorityTypeDetailsDto>> Get(int id)
    {
        var workTaskPriorityType = await _mediator.Send(new GetWorkTaskPriorityTypeDetailsQuery(id));
        return Ok(workTaskPriorityType);
    }

    // POST api/<WorkTaskPriorityTypesController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> Post(CreateWorkTaskPriorityTypeCommand priorityType)
    {
        var response = await _mediator.Send(priorityType);
        return CreatedAtAction(nameof(Get), new { id = response }, response);
    }

    // PUT api/<WorkTaskPriorityTypesController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Put(UpdateWorkTaskPriorityTypeCommand priorityType)
    {
        await _mediator.Send(priorityType);
        return NoContent();
    }

    // DELETE api/<WorkTaskPriorityTypesController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteWorkTaskPriorityTypeCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}
