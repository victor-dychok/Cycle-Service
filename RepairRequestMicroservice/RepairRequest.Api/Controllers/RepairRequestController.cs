using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepairRequest.Application.Comands.CreateRequest;
using RepairRequest.Application.Comands.DeleteRequest;
using RepairRequest.Application.Query.GetAll;
using RepairRequest.Application.Query.GetById;
using RepairRequest.Application.Query.GetCount;
using RepairRequest.Application.Requests.Comands.AcceptRequest;
using RepairRequest.Application.Requests.Comands.CancelRequest;
using RepairRequest.Application.Requests.Comands.SetCompletedStatus;
using RepairRequest.Application.Requests.Comands.SetDoneStatus;
using RepairRequest.Application.Requests.Comands.UpdateRequest;

namespace RepairRequest.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("repair-requests")]
    public class RepairRequestController : ControllerBase
    {
        public RepairRequestController()
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
            var count = await mediator.Send(new GetCountQuery(), token);
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

        [HttpPost]
        public async Task<IActionResult> Post(CreateRequestComand item, IMediator mediator, CancellationToken cancellationToken)
        {
            var postedItem = await mediator.Send(item, cancellationToken);
            return Created($"/users/{postedItem.Id}", postedItem);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateRequestComand newItem, IMediator mediator, CancellationToken cancellationToken)
        {
            var item = await mediator.Send(newItem, cancellationToken);
            return Ok(item);
        }

        [HttpPut("accept-request")]
        public async Task<IActionResult> AcceptRequest(AcceptRequestComand command, IMediator mediator, CancellationToken cancellationToken)
        {
            var item = await mediator.Send(command, cancellationToken);
            return Ok(item);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> CancelRequest(CancelRequestComand command, IMediator mediator, CancellationToken cancellationToken)
        {
            var item = await mediator.Send(command, cancellationToken);
            return Ok(item);
        }

        [HttpPut("done")]
        public async Task<IActionResult> SetDone(SetDoneStatusComand command, IMediator mediator, CancellationToken cancellationToken)
        {
            var item = await mediator.Send(command, cancellationToken);
            return Ok(item);
        }

        [HttpPut("complet")]
        public async Task<IActionResult> SetCompleated(SetCompletedStatusComand command, IMediator mediator, CancellationToken cancellationToken)
        {
            var item = await mediator.Send(command, cancellationToken);
            return Ok(item);
        }
    }
}
