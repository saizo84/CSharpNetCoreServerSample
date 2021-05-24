using Autofac;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;
using DBHandle;
using SampleServer.PacketHandler;
using SampleServer.Auth;

namespace PacketHandlerTest
{
    public class PacketHandlerUnitTestFixture : IDisposable
    {
        public readonly ContainerBuilder _containerBuilder;
        public readonly ILifetimeScope _lifetimeScope;
        public readonly ISender _mediator;
        // public readonly IMapper _mapper;

        public PacketHandlerUnitTestFixture()
        {
            _containerBuilder = new ContainerBuilder();
            ContainerConfigure();
            _lifetimeScope = _containerBuilder.Build();
            _mediator = _lifetimeScope.Resolve<ISender>();
        }

        protected virtual void ContainerConfigure()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("settings.json")
                .Build();

            _containerBuilder.RegisterInstance(configuration)
                .As<IConfiguration>();

            var assemblies = new List<Assembly>();
            assemblies
                .AddDatabase()
                .AddPackets()
                .AddAuth();

            _containerBuilder.RegisterAssemblyModules(assemblies.ToArray());
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            // Cleanup
            //if (disposing)
            //{
                
            //}
        }
    }
}
