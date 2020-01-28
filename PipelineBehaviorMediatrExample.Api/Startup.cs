using System;
using System.Linq;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PipelineBehaviorMediatrExample.Application.Validators;
using PipelineBehaviorMediatrExample.Domain.Contracts;
using PipelineBehaviorMediatrExample.Infra.Repository;

namespace PipelineBehaviorMediatrExample.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var assemblies = Assembly.GetEntryAssembly().GetReferencedAssemblies().Select(Assembly.Load);
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FailFastBehaviorValidator<,>));
            services.AddMediatR(assemblies.ToArray());

            AddMediatr(services);

            services
                .AddControllers()
                .AddFluentValidation(opt =>
                {
                    var assemblies = Assembly.GetEntryAssembly().GetReferencedAssemblies().Select(Assembly.Load);
                    opt.RegisterValidatorsFromAssemblies(assemblies);
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void AddMediatr(IServiceCollection services)
        {
            var assemblies = Assembly.GetEntryAssembly().GetReferencedAssemblies().Select(Assembly.Load);
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FailFastBehaviorValidator<,>));
            services.AddMediatR(assemblies.ToArray());
        }
    }
}
