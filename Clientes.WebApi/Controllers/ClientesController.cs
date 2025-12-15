using Clientes.WebApi.Interfaces;
using Clientes.WebApi.Models;
using System.Threading.Tasks;
using System.Web.Http;

namespace Clientes.WebApi.Controllers
{
    [RoutePrefix("api/clientes")]
    public class ClientesController : ApiController
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> ListarTodos()
        {
            var clientes = await _clienteService.ListarTodos();

            if (clientes == null || clientes.Count == 0)
            {
                return NotFound();
            }

            return Ok(clientes);
        }

        [HttpGet]
        [Route("{id:long}")]
        public async Task<IHttpActionResult> BuscarPorID(long id)
        {
            var cliente = await _clienteService.BuscarPorIDAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> BuscarPorNome([FromUri] string nome)
        {
            var clientes = await _clienteService.BuscarPorNome(nome);
            if (clientes == null || clientes.Count == 0)
            {
                return NotFound();
            }
            return Ok(clientes);
        }

        [HttpGet]
        [Route("contar")]
        public async Task<IHttpActionResult> ContarClientes()
        {
            var count = await _clienteService.ContarClientesAsync();
            return Ok(new { Count = count });
        }

        [HttpDelete]
        [Route("{id:long}")]
        public async Task<IHttpActionResult> Deletar(long id)
        {
            if (await _clienteService.DeletarAsync(id))
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Salvar([FromBody] Cliente cliente)
        {
            await _clienteService.SalvarAsync(cliente);
            return Ok(new { cliente.ID });
        }
    }
}