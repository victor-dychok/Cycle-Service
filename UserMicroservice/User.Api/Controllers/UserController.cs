using Application.User.Application.Query.GetCount;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Comands.CreateUser;
using Users.Application.Comands.DeleteUser;
using Users.Application.Comands.GiveUserAdminRole;
using Users.Application.Comands.GiveUserMasterRole;
using Users.Application.Comands.UpdateUser;
using Users.Application.Comands.UpdateUserPassword;
using Users.Application.Query.GetAll;
using Users.Application.Query.GetById;
using Users.Application.Requests.Comands.GiveUserCenterAdminRoleComand;
using Users.Domain;

namespace User.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        public UserController()
        {
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetListQuery query,
            IMediator mediator,
            CancellationToken token)
        {
            var result = await mediator.Send(query, token);
            var count = await mediator.Send(new GetCountQuery() { Name = query.Name }, token);
            HttpContext.Response.Headers.Append("X-Total-Count", count.ToString());
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, IMediator mediator, CancellationToken token)
        {
            var item = await mediator.Send(new GetByIdQuery { Id = id }, token);
            return Ok(item);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromQuery]CreateUserComand item, IMediator mediator, CancellationToken cancellationToken)
        {
            var postedItem = await mediator.Send(item, cancellationToken);
            return Created($"/users/{postedItem.Id}", postedItem);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromQuery]UpdateUserComand newItem, IMediator mediator, CancellationToken cancellationToken)
        {
            var item = await mediator.Send(newItem, cancellationToken);
            return Ok(item);
        }


        [HttpPut("MakeAdmin")]
        public async Task<IActionResult> MakeAdmin([FromQuery] GiveUserAdminRoleComand comand, IMediator mediator, CancellationToken cancellationToken)
        {
            var item = await mediator.Send(comand, cancellationToken);
            return Ok(item);
        }

        [HttpPut("MakeMaster")]
        public async Task<IActionResult> MakeMaster([FromQuery] GiveUserMasterRoleComand comand, IMediator mediator, CancellationToken cancellationToken)
        {
            var item = await mediator.Send(comand, cancellationToken);
            return Ok(item);
        }

        [HttpPut("MakeCenterAdmin")]
        public async Task<IActionResult> MakeCenterAdmin([FromQuery] GiveUserCenterAdminRoleComand comand, IMediator mediator, CancellationToken cancellationToken)
        {
            var item = await mediator.Send(comand, cancellationToken);
            return Ok(item);
        }

        [HttpPut("ResetPassword ")]
        public async Task<IActionResult> ResetPassword( [FromQuery] ResetPasswordComand newItem, IMediator mediator, CancellationToken cancellationToken)
        {
            var item = await mediator.Send(newItem, cancellationToken);
            return Ok(item);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, IMediator mediator)
        {
            return Ok(await mediator.Send(new DeleteUserComand() { Id = id }));
        }
    }
}
