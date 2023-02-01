using HotelListingAPI_MC.Data.Entities;
using HotelListingAPI_MC.Data.Entities.CountryEntities;
using HotelListingAPI_MC.Data.Entities.HotelEntities;
using HotelListingAPI_MC.Data.Entities.UserEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListingAPI_DATA
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
