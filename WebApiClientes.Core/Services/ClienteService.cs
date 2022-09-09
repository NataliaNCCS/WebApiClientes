using WebApiClientes.Core.Models;
using WebApiClientes.Core.Interfaces;
namespace WebApiClientes.Core.Services
{
    public class ClienteService : IClienteService
    {
        public IClienteRepository _clienteRepository;
        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public List<Cadastro> Consultar()
        {
            return _clienteRepository.ConsultarTodosOsCadastrosDoDB();
        }

        public bool ConsultarPorCpf(string cpf)
        {
            return _clienteRepository.ConsultarPorCpfNoDB(cpf);
        }

        public bool InserirCadastro(Cadastro cadastro)
        {
            return _clienteRepository.InserirCadastrosNoDB(cadastro);
        }

        public bool AtualizarCadastro(string cpf, Cadastro cadastro)
        {
            return _clienteRepository.AtualizarCadastroNoDB(cpf, cadastro);
        }

        public bool DeletarCadastro(string cpf)
        {
            return _clienteRepository.DeletarCadastroNoDB(cpf);
        }

    }
}