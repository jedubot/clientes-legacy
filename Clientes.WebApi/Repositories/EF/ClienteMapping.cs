using Clientes.WebApi.Models;
using System.Data.Entity.ModelConfiguration;

namespace Clientes.WebApi.Repositories.EF
{
    public class ClienteMapping : EntityTypeConfiguration<Cliente>
    {
        public ClienteMapping()
        {
            ToTable("Clientes");
            HasKey(c => c.ID);
            Property(c => c.Cpf)
                .IsRequired()
                .HasMaxLength(11);
            Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(100);
            Property(c => c.Nascimento)
                .IsRequired();
            Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(100);
            Property(c => c.CriadoEm)
                .IsRequired();
        }
    }
}