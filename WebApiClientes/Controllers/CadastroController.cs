using WebApiClientes.Repository;
using Microsoft.AspNetCore.Mvc;

namespace WebApiClientes.Controllers
{
    [ApiController]
    [Route("[controller]")] // nesse formato, é pego o nome da própria controller
    [Consumes("application/json")]
    [Produces("application/json")]
    public class CadastroController : ControllerBase
    {
        IConfiguration _configuration;
        ClienteRepository repositoryCliente;

        public CadastroController(IConfiguration configuration)
        {
            _configuration = configuration;
            repositoryCliente = new ClienteRepository(_configuration); //acessa o db

        }


        [HttpGet("/clientes/consultar")]
        public ActionResult<List<Cadastro>> Consultar()
        {
            var cadastros = repositoryCliente.ConsultarTodosOsCadastrosDoDB();
            return Ok(cadastros);
            // ou return StatusCode(200);
        }

        [HttpGet("/clientes/{cpf}/consultar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult ConsultarPorCPF(string cpf)
        {
            var cpfConsultado = repositoryCliente.ConsultarPorCpfNoDB(cpf);

            if (!cpfConsultado)
            {
                return BadRequest();
            }

            return Ok(cpfConsultado);
        }

        [HttpPost("/clientes/inserir")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Inserir(Cadastro cadastro)
        {
            bool cadastrosInseridos = repositoryCliente.InserirCadastrosNoDB(cadastro);

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

        public IActionResult Atualizar(string cpf, Cadastro newCadastro)
        {
            bool cadastroAtualizado = repositoryCliente.AtualizarCadastroNoDB(cpf, newCadastro);

            if (!cadastroAtualizado)
            {
                return BadRequest();
            }

            return NoContent();

        }

        [HttpDelete("/clientes/{cpf}/deletar")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Deletar(string cpf)
        {
            var cadastroDeletado = repositoryCliente.DeletarCadastroNoDB(cpf);

            if (!cadastroDeletado)
            {
                return BadRequest();
            }

            return NoContent();

        }
    }
}
