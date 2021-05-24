using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace DBHandle
{
    internal class DBHandleModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HandleAuthDB>()
                .As<IHandleAuthDB>()
                .InstancePerLifetimeScope();

            builder.RegisterType<HandleUserDB>()
                .As<IHandleUserDB>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DatabaseHandler>()
                .As<IDatabaseHandler>()
                .InstancePerLifetimeScope();

            builder.Register(c =>
            {
                var config = c.Resolve<IConfiguration>();
                var connectionString = config.GetSection("ConnectionStrings:AuthContext");
                if (connectionString is null)
                {
                    return null;
                }

                var opt = new DbContextOptionsBuilder<AuthContext>()
                .UseMySql(connectionString.Value,
                    MySqlServerVersion.LatestSupportedServerVersion,
                    mySqlOptionsAction =>
                    {
                        mySqlOptionsAction.MigrationsAssembly("CSharpNetCoreServer");
                        mySqlOptionsAction.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                    });

                return new AuthContext(opt.Options);
            })
                .AsSelf()
                .InstancePerLifetimeScope();
        }
    }
}
