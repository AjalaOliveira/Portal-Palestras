using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Palestras.Domain.Models;

namespace Palestras.Infra.Data.Mappings
{
    public class PalestraMap : IEntityTypeConfiguration<Palestra>
    {
        public void Configure(EntityTypeBuilder<Palestra> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnName("Id");

            builder.Property(c => c.Titulo)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Email)
                .HasColumnType("varchar(100)")
                .HasMaxLength(11)
                .IsRequired();
        }
    }
}