using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;

namespace SampleServer.Auth
{
    public class AuthAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAutoMapper(typeof(AuthAutofacModule).Assembly);

            builder.RegisterType<GuestLoginHandler>()
                .As<ILoginHandler>()
                .InstancePerLifetimeScope();
        }
    }
}
