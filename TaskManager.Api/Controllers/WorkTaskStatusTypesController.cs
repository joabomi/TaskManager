using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Features.WorkTaskStatusType.Commands.CreateWorkTaskStatusType;
using TaskManager.Application.Features.WorkTaskStatusType.Commands.DeleteWorkTaskStatusType;
using TaskManager.Application.Features.WorkTaskStatusType.Commands.UpdateWorkTaskStatusType;
using TaskManager.Application.Features.WorkTaskStatusType.Queries.GetAllWorkTaskStatusTypes;
using TaskManager.Application.Features.WorkTaskStatusType.Queries.GetWorkTaskStatusTypeDetails;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManager.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkTaskStatusTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public WorkTaskStatusTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/<WorkTaskStatusTypesController>
    [HttpGet]
    public async Task<ActionResult<List<WorkTaskStatusTypeDto>>> Get()
    {
        var workTaskStatusTypes = await _mediator.Send(new GetAllWorkTaskStatusTypesQuery());
        return Ok(workTaskStatusTypes);
    }

    // GET api/<WorkTaskStatusTypesController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<WorkTaskStatusTypeDetailsDto>> Get(int id)
    {
        var workTaskStatusType = await _mediator.Send(new GetWorkTaskStatusTypeDetailsQuery(id));
        return Ok(workTaskStatusType);
    }

    // POST api/<WorkTaskStatusTypesController>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> Post(CreateWorkTaskStatusTypeCommand statusType)
    {
        var response = await _mediator.Send(statusType);
        return CreatedAtAction(nameof(Get), new {id = response});
    }

    // PUT api/<WorkTaskStatusTypesController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Put(UpdateWorkTaskStatusTypeCommand statustype)
    {
        await _mediator.Send(statustype);
        return NoContent();
    }

    // DELETE api/<WorkTaskStatusTypesController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(DeleteWorkTaskStatusTypeCommand statusType)
    {
        await _mediator.Send(statusType);
        return NoContent();
    }
}
