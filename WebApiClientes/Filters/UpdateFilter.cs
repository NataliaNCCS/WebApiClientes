using WebApiClientes.Core.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using WebApiClientes.Core.Models;

namespace WebApiClientes.Filters
{
    public class UpdateFilter : ActionFilterAttribute
    {
        IClienteService _clienteService;

        public UpdateFilter(IClienteService clienteService)
        {
            _clienteService = clienteService;   
        }

        override public void OnActionExecuting(ActionExecutingContext context)
        {
            string cpf = (string)context.ActionArguments["cpf"];

            if (_clienteService.ConsultarPorCpf(cpf) == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
            }

        }

    }
}
