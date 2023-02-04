using HotelListingAPI_MC_Data.Entities;
using HotelListingAPI_MC_Data.Entities.CountryEntities;
using HotelListingAPI_MC_Data.Entities.HotelEntities;
using HotelListingAPI_MC_Data.Entities.UserEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelListingAPI_MC_Data
{
    public class HotelListingDbContext : IdentityDbContext<APIUser>
    {
        public HotelListingDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<HotelEntity> Hotels { get; set; }
        public DbSet<CountryEntity> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CountryEntityConfiguration());
            modelBuilder.ApplyConfiguration(new HotelEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());                       
        }
    }
}
