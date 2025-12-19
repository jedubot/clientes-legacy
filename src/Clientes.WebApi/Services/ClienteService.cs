using Clientes.WebApi.Interfaces;
using Clientes.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clientes.WebApi.Services
{
    public class ClienteService(IRepository<Cliente> clienteRepository) : IClienteService
    {
        private readonly IRepository<Cliente> _clienteRepository = clienteRepository;

        public async Task Atualizar(Cliente cliente)
        {
            _clienteRepository.Update(cliente);
            await _clienteRepository.SaveChangesAsync();
        }

        public async Task<Cliente> BuscarPorIDAsync(long id)
        {
            return await _clienteRepository.GetByIDAsync(id);
        }

        public async Task<IList<Cliente>> BuscarPorNomeAsync(string nome)
        {
            return await _clienteRepository.FindAsync(c => c.Nome.Contains(nome));
        }

        public async Task<long> ContarClientesAsync()
        {
            return await _clienteRepository.CountAsync();
        }

        public async Task<bool> DeletarAsync(long id)
        {
            var cliente = await _clienteRepository.GetByIDAsync(id);
            if (cliente != null)
            {
                _clienteRepository.Remove(cliente);
                await _clienteRepository.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IList<Cliente>> ListarTodosAsync()
        {
            return await _clienteRepository.GetAllAsync();
        }

        public async Task SalvarAsync(Cliente cliente)
        {
            cliente.CriadoEm = DateTime.UtcNow;
            _clienteRepository.Add(cliente);
            await _clienteRepository.SaveChangesAsync();
        }
    }
}