
using Bilbayt.Web.API.BackgroundJob.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bilbayt.Web.API.BackgroundJob
{
    public static class JobServiceExtensions
    {
        public static IServiceCollection AddBackgroundJob<TService>(this IServiceCollection services, IConfiguration configuration) where TService : BackgroundService
        {
            services.AddSingleton<IJobServiceConfig<TService>, JobServiceConfig<TService>>(options =>
            {
                return new JobServiceConfig<TService>
                {
                    CronExpression = configuration.GetValue<string>("cronExpression"),
                    IsEnabled = configuration.GetValue<bool>("isEnabled")
                };
            });
            services.AddHostedService<TService>();

            return services;
        }
    }
}
