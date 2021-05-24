using System;
using Xunit;
using SampleServer.PacketHandler;
using Autofac;
using MediatR;
using System.Threading.Tasks;
using Sample.Proto;

namespace PacketHandlerTest
{
    public class LoginUnitTest : IClassFixture<PacketHandlerUnitTestFixture>
    {
        protected PacketHandlerUnitTestFixture _fixture;
        //protected readonly ILifetimeScope _lifetimeScope;
        //protected readonly ISender _sender;

        public LoginUnitTest(PacketHandlerUnitTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task LoginTest()
        {
            var request = new LoginRequest();
            request.Guid = Guid.Empty.ToString();
            request.DeviceInfo = new DeviceInfo();
            request.DeviceInfo.OsType = OsTypes.Pc;
            request.DeviceInfo.OsVersion = "WIN10";
            request.DeviceInfo.MCC = "450"; // ¥Î«—πŒ±π

            request.PlatformPassword = "1234";
            var response = await _fixture._mediator.Send(new LoginCommand(request));
            Assert.Equal(ResultCodes.Success, response.ResultCode);
        }
    }
}
