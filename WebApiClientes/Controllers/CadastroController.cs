using Microsoft.AspNetCore.Mvc;
using WebApiClientes.Core.Interfaces;
using WebApiClientes.Core.Models;
using WebApiClientes.Filters;

namespace WebApiClientes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [TypeFilter(typeof(LogResourceFilter))]
    [TypeFilter(typeof(TimerResourceFilter))]

    public class CadastroController : ControllerBase
    {
        public IClienteService _clienteService;

        public CadastroController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }


        [HttpGet("/clientes/consultar")]
        public ActionResult<List<Cadastro>> Consultar()
        {
            var cadastros = _clienteService.Consultar();
            return Ok(cadastros);
        }


        [HttpGet("/clientes/{cpf}/consultar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult ConsultarPorCPF(string cpf)
        {
            var cpfConsultado = _clienteService.ConsultarPorCpf(cpf);

            if (!cpfConsultado)
            {
                return BadRequest();
            }

            return Ok(cpfConsultado);
        }

        [HttpPost("/clientes/inserir")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ServiceFilter(typeof(InsertFilter))]
        public IActionResult Inserir(Cadastro cadastro)
        {
            bool cadastrosInseridos = _clienteService.InserirCadastro(cadastro);

            if (!cadastrosInseridos)
            {
                return BadRequest();
            }

            return Created(nameof(Inserir), cadastro);

        }

        
        [HttpPut("/clientes/{cpf}/atualizar")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ServiceFilter(typeof(UpdateFilter))]

        public IActionResult Atualizar(string cpf, Cadastro newCadastro)
        {
            bool cadastroAtualizado = _clienteService.AtualizarCadastro(cpf, newCadastro);

            if (!cadastroAtualizado)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return NoContent();

        }

        [HttpDelete("/clientes/{cpf}/deletar")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Deletar(string cpf)
        {
            var cadastroDeletado = _clienteService.DeletarCadastro(cpf);

            if (!cadastroDeletado)
            {
                return BadRequest();
            }

            return NoContent();

        }
    }
}
