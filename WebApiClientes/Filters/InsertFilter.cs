using WebApiClientes.Core.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using WebApiClientes.Core.Models;

namespace WebApiClientes.Filters
{

    public class InsertFilter : ActionFilterAttribute
    {
        IClienteService _clienteService;

        public InsertFilter(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        override public void OnActionExecuting(ActionExecutingContext context)
        {
            Cadastro cadastro = (Cadastro)context.ActionArguments["cadastro"];
            
            if (_clienteService.ConsultarPorCpf(cadastro.CPF) != null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status409Conflict);
            }
        }
    }
}
