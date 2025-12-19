using Clientes.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clientes.WebApi.Repositories.EF
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");
            builder.HasKey(c => c.ID);
            builder.Property(c => c.Cpf)
                .IsRequired()
                .HasMaxLength(11);
            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(c => c.Nascimento)
                .IsRequired();
            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(c => c.CriadoEm)
                .IsRequired();
        }
    }
}