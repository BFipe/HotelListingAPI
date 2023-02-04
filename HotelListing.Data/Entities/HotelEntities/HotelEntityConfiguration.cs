using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListingAPI_MC_Data.Entities.HotelEntities
{
    public class HotelEntityConfiguration : IEntityTypeConfiguration<HotelEntity>
    {
        public void Configure(EntityTypeBuilder<HotelEntity> builder)
        {
            builder.HasKey(q => q.HotelEntityId);
            builder.Property(q => q.HotelEntityId).ValueGeneratedOnAdd();

            builder.Property(q => q.Rating).IsRequired(false);
        }
    }
}
