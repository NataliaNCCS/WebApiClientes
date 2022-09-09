using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiClientes.Core.Models;

namespace WebApiClientes.Core.Interfaces
{
    public interface IClienteRepository
    {
        List<Cadastro> ConsultarTodosOsCadastrosDoDB();
        bool InserirCadastrosNoDB(Cadastro cadastro);
        bool DeletarCadastroNoDB(string cpf);
        bool AtualizarCadastroNoDB(string cpf, Cadastro cadastro);
        bool ConsultarPorCpfNoDB(string cpf);


    }
}
