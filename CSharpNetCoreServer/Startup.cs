using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using Autofac;
using System.Reflection;
using System.Collections.Generic;
using DBHandle;
using SampleServer.PacketHandler;

namespace CSharpNetCoreServer
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc(options =>
            {
                options.EnableDetailedErrors = true;
                options.MaxReceiveMessageSize = 1 * 1024 * 1024; // 1 megabyte
                options.MaxSendMessageSize = 1 * 1024 * 1024; // 1 megabyte
            });

            var connectionString = _configuration.GetConnectionString("AuthContext");
            if (connectionString is not null)
            {
                services.AddDbContextPool<DBHandle.AuthContext>(options =>
                options.UseMySql(connectionString, MySqlServerVersion.LatestSupportedServerVersion,
                mySqlOptionsAction =>
                {
                    mySqlOptionsAction.MigrationsAssembly("CSharpNetCoreServer");
                    mySqlOptionsAction.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                }));
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<FrontService>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var assemblies = new List<Assembly>();
            assemblies
                .AddDatabase()
                .AddPackets();

            builder.RegisterAssemblyModules(assemblies.ToArray());
            builder.RegisterInstance(_configuration);
        }
    }
}
