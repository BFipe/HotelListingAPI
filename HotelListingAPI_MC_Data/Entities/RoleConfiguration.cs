using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListingAPI_MC_Data.Entities
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole()
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                 new IdentityRole()
                 {
                     Name = "Manager",
                     NormalizedName = "MANAGER"
                 },
                 new IdentityRole()
                 {
                     Name = "User",
                     NormalizedName = "USER"
                 }
                );
        }
    }
}
