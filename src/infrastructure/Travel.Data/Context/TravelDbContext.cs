using Microsoft.EntityFrameworkCore;
using Travel.Domain.Entities;
using Travel.Application.Common.Interfaces;

namespace Travel.Data.Context
{
    public class TravelDbContext : DbContext, IApplicationDbContext
    {
        public TravelDbContext(DbContextOptions<TravelDbContext> options) : base(options)
        {
        }

        public DbSet<TourList> TourLists { get; set; }
        public DbSet<TourPackage> TourPackages { get; set; }
    }
}
