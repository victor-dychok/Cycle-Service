using Grpc.Core;
using GrpcGreeter;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Users.Application.Query.GetById;

namespace User.Api.gRPC
{
    public class GRPCUserService : UsersService.UsersServiceBase
    {
        private readonly IMediator _mediator;
        public GRPCUserService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public override async Task<UserReply> GetUser(GetUserRequest request, ServerCallContext context)
        {
            var query = new GetByIdQuery()
            {
                Id = Convert.ToInt32(request.ApplicationUserId),
            };

            var dto = await _mediator.Send(query, context.CancellationToken);
            var reply = new UserReply()
            {
                ApplicationUserId = dto.Id.ToString(),
                Login = dto.Login
            };
            reply.Roles.AddRange(dto.Roles);
            return reply;
        }
    }
}
