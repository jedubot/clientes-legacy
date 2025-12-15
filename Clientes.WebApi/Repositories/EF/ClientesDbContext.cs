using Clientes.WebApi.Models;
using System.Data.Entity;

namespace Clientes.WebApi.Repositories.EF
{
    public class ClientesDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }

        public ClientesDbContext() : base("name=ClientesDbContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(typeof(ClientesDbContext).Assembly);
        }
    }
}