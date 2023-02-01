using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListingAPI_MC.Data.Entities.CountryEntities
{
    public class CountryEntityConfiguration : IEntityTypeConfiguration<CountryEntity>
    {
        public void Configure(EntityTypeBuilder<CountryEntity> builder)
        {
            builder.HasKey(x => x.CountryId);
            builder.Property(x => x.CountryId).ValueGeneratedOnAdd();
            builder.HasMany(q => q.Hotels).WithOne(q => q.Country).HasForeignKey(q => q.CountryId);
        }
    }
}