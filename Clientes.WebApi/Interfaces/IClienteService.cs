using Clientes.WebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clientes.WebApi.Interfaces
{
    public interface IClienteService
    {
        Task<IList<Cliente>> BuscarPorNome(string nome);
        Task<Cliente> BuscarPorIDAsync(long id);
        Task<long> ContarClientesAsync();
        Task<bool> DeletarAsync(long id);
        Task<IList<Cliente>> ListarTodos();
        Task SalvarAsync(Cliente cliente);
    }
}