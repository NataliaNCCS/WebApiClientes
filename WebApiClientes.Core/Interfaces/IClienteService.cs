using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiClientes.Core.Models;

namespace WebApiClientes.Core.Interfaces
{
    public interface IClienteService
    {
        List<Cadastro> Consultar();
        bool ConsultarPorCpf(string cpf);
        bool InserirCadastro(Cadastro cadastro);
        bool AtualizarCadastro(string cpf, Cadastro cadastro);
        bool DeletarCadastro(string cpf);
    }
}
