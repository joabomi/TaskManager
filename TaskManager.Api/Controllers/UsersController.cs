using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Features.WorkTask.Queries.GetWorkTaskDetails;
using TaskManager.Application.Features.WorkTaskUser.Queries.GetAllWorkTaskUsers;
using TaskManager.Application.Features.WorkTaskUser.Queries.GetWorkTaskUserDetails;
using TaskManager.Identity.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<ActionResult<List<WorkTaskUserDto>>> Get()
        {
            var users = await _mediator.Send(new GetAllWorkTaskUserQuery());
            return Ok(users);
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkTaskUserDetailsDto>> Get(string id)
        {
            var user = await _mediator.Send(new GetWorkTaskUserDetailsQuery(id));
            return Ok(user);
        }

        //// POST api/<UsersController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<UsersController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<UsersController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
