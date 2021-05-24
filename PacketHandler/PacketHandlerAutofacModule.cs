using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;
// using AutoMapper.Contrib.Autofac.DependencyInjection;

namespace SampleServer.PacketHandler
{
    internal class PacketHandlerAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterMediatR(typeof(PacketHandlerAutofacModule).Assembly);

            // builder.RegisterAutoMapper(typeof(PacketHandlerAutofacModule).Assembly);

        }
    }
}
