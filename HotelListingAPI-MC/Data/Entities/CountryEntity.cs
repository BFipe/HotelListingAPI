using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListingAPI_DATA.Entities
{
    public class CountryEntity
    {
        public int CountryId { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public List<HotelEntity> Hotels { get; set; }
    }

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