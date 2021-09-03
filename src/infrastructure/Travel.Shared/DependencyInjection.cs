using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Travel.Domain.Settings;
using Travel.Application.Common.Interfaces;
using Travel.Shared.Services;
using Travel.Shared.Files;

namespace Travel.Shared
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureShared(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();

            return services;
        }
    }
}
