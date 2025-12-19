using Clientes.WebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clientes.WebApi.Interfaces
{
    public interface IClienteService
    {
        Task<IList<Cliente>> BuscarPorNomeAsync(string nome);
        Task<Cliente> BuscarPorIDAsync(long id);
        Task<long> ContarClientesAsync();
        Task<bool> DeletarAsync(long id);
        Task<IList<Cliente>> ListarTodosAsync();
        Task SalvarAsync(Cliente cliente);
    }
}