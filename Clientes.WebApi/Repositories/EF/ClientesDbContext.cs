using Clientes.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Clientes.WebApi.Repositories.EF
{
    public class ClientesDbContext(DbContextOptions<ClientesDbContext> options) : DbContext(options)
    {
        public DbSet<Cliente> Clientes => Set<Cliente>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientesDbContext).Assembly);
        }
    }
}