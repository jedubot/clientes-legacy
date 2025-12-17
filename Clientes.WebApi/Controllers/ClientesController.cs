using Clientes.WebApi.Interfaces;
using Clientes.WebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Clientes.WebApi.Controllers
{
    [Route("api/clientes")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> ListarTodos()
        {
            var clientes = await _clienteService.ListarTodos();

            if (clientes == null || clientes.Count == 0)
            {
                return NotFound();
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

        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> BuscarPorNome([FromQuery] string nome)
        {
            var clientes = await _clienteService.BuscarPorNome(nome);
            if (clientes == null || clientes.Count == 0)
            {
                return NotFound();
            }
            return Ok(clientes);
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
            return Ok(new { cliente.ID });
        }
    }
}