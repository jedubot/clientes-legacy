using Clientes.WebApi.Interfaces;
using Clientes.WebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Clientes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController(IClienteService clienteService) : ControllerBase
    {
        private readonly IClienteService _clienteService = clienteService;

        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> Listar([FromQuery] string? nome)
        {
            IList<Cliente> clientes;

            if (!string.IsNullOrEmpty(nome))
            {
                clientes = await _clienteService.BuscarPorNomeAsync(nome);
            }
            else
            {
                clientes = await _clienteService.ListarTodosAsync();
            }

            return Ok(clientes);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<Cliente>> BuscarPorID(long id)
        {
            var cliente = await _clienteService.BuscarPorIDAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        [HttpGet("contar")]
        public async Task<ActionResult<object>> ContarClientes()
        {
            var count = await _clienteService.ContarClientesAsync();
            return Ok(new { Count = count });
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Deletar(long id)
        {
            if (await _clienteService.DeletarAsync(id))
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<object>> Salvar([FromBody] Cliente cliente)
        {
            await _clienteService.SalvarAsync(cliente);
            return CreatedAtAction(nameof(BuscarPorID), new { id = cliente.ID }, cliente);
        }
    }
}