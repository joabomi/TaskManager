using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Features.WorkTask.Commands.CreateWorkTask;
using TaskManager.Application.Features.WorkTask.Commands.DeleteWorkTask;
using TaskManager.Application.Features.WorkTask.Commands.UpdateWorkTask;
using TaskManager.Application.Features.WorkTask.Queries.GetAllWorkTasks;
using TaskManager.Application.Features.WorkTask.Queries.GetWorkTaskDetails;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManager.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkTasksController : ControllerBase
{
    private readonly IMediator _mediator;

    public WorkTasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/<WorkTasksController>
    [HttpGet]
    public async Task<ActionResult<List<WorkTaskDto>>> Get()
    {
        var workTasks = await _mediator.Send(new GetAllWorkTasksQuery());
        return Ok(workTasks);
    }

    // GET api/<WorkTasksController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<WorkTaskDetailsDto>> Get(int id)
    {
        var workTask = await _mediator.Send(new GetWorkTaskDetailsQuery(id));
        return Ok(workTask);
    }

    // POST api/<WorkTasksController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> Post(CreateWorkTaskCommand workTask)
    {
        var response = await _mediator.Send(workTask);
        return CreatedAtAction(nameof(Get), new { id = response }, response);
    }

    // PUT api/<WorkTasksController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Put(int id, UpdateWorkTaskCommand workTask)
    {
        workTask.Id = id;
        await _mediator.Send(workTask);
        return NoContent();
    }

    // DELETE api/<WorkTasksController>/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteWorkTaskCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}
