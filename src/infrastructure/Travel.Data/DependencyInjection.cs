using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Travel.Application.Common.Interfaces;
using Travel.Data.Context;

namespace Travel.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureData(this IServiceCollection services)
        {
            services.AddDbContext<TravelDbContext>(options => options
              .UseSqlite("Data Source=TravelTourDatabase.sqlite3"));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<TravelDbContext>());

            return services;
        }
    }
}
