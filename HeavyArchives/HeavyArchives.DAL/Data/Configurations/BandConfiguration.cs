using HeavyArchives.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HeavyArchives.DAL.Data.Configurations
{
    public class BandConfiguration : IEntityTypeConfiguration<Band>
    {
        //jel potrebno to odvajat u klase ili se moze sve samo u context odjednom napravit s
        public void Configure(EntityTypeBuilder<Band> builder)
        {
            builder.ToTable(nameof(Band));
            builder.HasKey(b => b.Id);

            builder
                .HasMany(b => b.Albums)
                .WithOne(a => a.Band)
                .HasForeignKey(a => a.BandId);

            builder.
                HasMany(b => b.BandLabel)
                .WithOne(bl => bl.Band)
                .HasForeignKey(b => b.BandId);

            builder
                .HasMany(b => b.Albums)
                .WithOne(a => a.Band)
                .HasForeignKey(a => a.BandId);

            /// ...
        }
    }
}
