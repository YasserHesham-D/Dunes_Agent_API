using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace Domain.Models.MTM
{
    public class LocationServices
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool IsDeleted { get; set; } = false;

        public Guid ServiceId { get; set; }
        public virtual Service Service { get; set; } 

        public Guid LocationId { get; set; }
        public virtual Location Location { get; set; } 

        public decimal KidsPrice { get; set; }
        public decimal ChildsPrice { get; set; }
        public decimal AdultsPrice { get; set; }

    }

    public class LocationServicesConfigration : IEntityTypeConfiguration<LocationServices>
    {
        public void Configure(EntityTypeBuilder<LocationServices> builder)
        {

            builder.ToTable("LocationServices");

            // Primary Key
            builder.HasKey(bs => bs.Id);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

            // Indexes (optional for performance)
            // builder.HasIndex(bs => new { bs.ServiceId, bs.LocationId});

            // Relationships ------------------------------


            builder.HasOne(bs => bs.Service)
                .WithMany(b => b.LocationServices)
                .HasForeignKey(bs => bs.ServiceId)
                .OnDelete(DeleteBehavior.NoAction);

           
            builder.HasOne(bs => bs.Location)
                .WithMany(s => s.Services)
                .HasForeignKey(bs => bs.LocationId)
                .OnDelete(DeleteBehavior.NoAction);

           

           

            builder.Property(bs => bs.KidsPrice)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

            builder.Property(bs => bs.ChildsPrice)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

            builder.Property(bs => bs.AdultsPrice)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

           

        }
    }
}
