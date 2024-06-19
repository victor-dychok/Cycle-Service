using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Requests.Comands.CreateCenter;
using ServiceCenter.Application.Requests.Comands.CreateService;
using ServiceCenter.Application.Requests.Comands.DeleteCenter;
using ServiceCenter.Application.Requests.Comands.DeleteService;
using ServiceCenter.Application.Requests.Comands.TransferAdministratorRights;
using ServiceCenter.Application.Requests.Comands.UpdateCenter;
using ServiceCenter.Application.Requests.Comands.UpdateService;
using ServiceCenter.Application.Requests.Query.GetAllCenters;
using ServiceCenter.Application.Requests.Query.GetCenterById;
using ServiceCenter.Application.Requests.Query.GetServiceById;

namespace ServiceCenter.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("repair-requests")]
    public class ServiceCenterController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet("center")]
        public async Task<IActionResult> GetAllCenters(
                [FromQuery] GetAllCentersQuery query,
                IMediator mediator,
                CancellationToken token)
        {
            var result = await mediator.Send(query, token);
            //var count = await mediator.Send(new GetCountQuery(), token);
            //HttpContext.Response.Headers.Append("X-Total-Count", count.ToString());
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("service")]
        public async Task<IActionResult> GetAllServices(
                [FromQuery] GetAllCentersQuery query,
                IMediator mediator,
                CancellationToken token)
        {
            var result = await mediator.Send(query, token);
            //var count = await mediator.Send(new GetCountQuery(), token);
            //HttpContext.Response.Headers.Append("X-Total-Count", count.ToString());
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("center/{id:int}")]
        public async Task<IActionResult> GetCenterById(int id, IMediator mediator, CancellationToken token)
        {
            var item = await mediator.Send(new GetCenterByIdQuery { Id = id }, token);
            return Ok(item);
        }

        [AllowAnonymous]
        [HttpGet("service/{id:int}")]
        public async Task<IActionResult> GetServiceById(int id, IMediator mediator, CancellationToken token)
        {
            var item = await mediator.Send(new GetServiceByIdQuery { Id = id }, token);
            return Ok(item);
        }

        [HttpPost("center")]
        public async Task<IActionResult> CreateCenter([FromQuery]CreateCenterComand comand, IMediator mediator, CancellationToken token)
        {
            var item = await mediator.Send(comand, token);
            return Ok(item);
        }

        [HttpPut("center")]
        public async Task<IActionResult> UpdateCenter([FromQuery] UpdateCenterCommand comand, IMediator mediator, CancellationToken token)
        {
            var item = await mediator.Send(comand, token);
            return Ok(item);
        }

        [HttpDelete("center")]
        public async Task<IActionResult> DeleteCenter([FromQuery] DeleteCenterComand comand, IMediator mediator, CancellationToken token)
        {
            var item = await mediator.Send(comand, token);
            return Ok(item);
        }
        [HttpPut("transfer-rights")]
        public async Task<IActionResult> TransferAdminRights([FromQuery] TransferAdministratorRightsComand comand, IMediator mediator, CancellationToken token)
        {
            var item = await mediator.Send(comand, token);
            return Ok(item);
        }

        [HttpPost("service")]
        public async Task<IActionResult> CreateService([FromQuery] CreateServiceCommand comand, IMediator mediator, CancellationToken token)
        {
            var item = await mediator.Send(comand, token);
            return Ok(item);
        }

        [HttpPut("service")]
        public async Task<IActionResult> UpdateService([FromQuery] UpdateServiceCommand comand, IMediator mediator, CancellationToken token)
        {
            var item = await mediator.Send(comand, token);
            return Ok(item);
        }


        [HttpDelete("service")]
        public async Task<IActionResult> DeleteService([FromQuery] DeleteServiceCommand comand, IMediator mediator, CancellationToken token)
        {
            var item = await mediator.Send(comand, token);
            return Ok(item);
        }

    }
}
