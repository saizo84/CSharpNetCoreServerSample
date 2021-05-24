using System;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Sample.Proto;
using SampleServer.Auth;

namespace SampleServer.PacketHandler
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        public LoginRequest Packet { get; private set; }
        public LoginCommand(LoginRequest request)
        {
            Packet = request;
        }
    }

    public class LoginCommandHandler
        : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly ILoginHandler _loginHandler; 
        public LoginCommandHandler(ILoginHandler loginHandler)
        {
            _loginHandler = loginHandler;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var response = new LoginResponse();
            (response.ResultCode, response.UserInfo) = await _loginHandler.Login(
                request.Packet.Guid, request.Packet.DeviceInfo, request.Packet.PlatformPassword);

            return response;
        }
    }
}
