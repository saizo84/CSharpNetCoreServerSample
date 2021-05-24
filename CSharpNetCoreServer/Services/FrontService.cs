using Grpc.Core;
using MediatR;
using Sample.Proto;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleServer.PacketHandler;

namespace CSharpNetCoreServer
{
    public class FrontService : NetCoreService.NetCoreServiceBase
    {
        private readonly ILogger<FrontService> _logger;
        private readonly ISender _senderMediator;
        public FrontService(ILogger<FrontService> logger,
            ISender senderMediator)
        {
            _logger = logger;
            _senderMediator = senderMediator;
        }

        public override async Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
        {
            _logger.LogTrace($"login Guid {request.Guid}");

            var response = await _senderMediator.Send(new LoginCommand(request));
            return response;
        }
    }
}
