using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;

namespace WebApiClientes.Filters
{
    public class GeneralExceptionFilter : ExceptionFilterAttribute
    {
        public void OnException(ExceptionContext context)
        {
            var problem = new ProblemDetails
            {
                Status = 500,
                Title = "Erro inpesperado",
                Detail = "Ocorreu um erro inesperado na solcitação",
                Type = context.Exception.GetType().Name
            };

            Console.WriteLine($"Tipo da exceção {context.Exception.GetType().Name}, mensagem {context.Exception.Message}, stack trace {context.Exception.StackTrace}");


            switch (context.Exception)
            {
                case SqlException:
                    problem.Status = 503;
                    problem.Title = "Serviço indisponível";
                    problem.Detail = "Erro inesperado ao se comunicar com o banco de dados";
                    context.HttpContext.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                    context.Result = new ObjectResult(problem);
                    break;
                case NullReferenceException:
                    problem.Status = 417;
                    problem.Title = "Erro inesperado no sistema";
                    context.HttpContext.Response.StatusCode = StatusCodes.Status417ExpectationFailed;
                    context.Result = new ObjectResult(problem);
                    break;
                default:
                    problem.Status = 500;
                    problem.Title = "Erro inesperado. Tente novamente";
                    context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Result = new ObjectResult(problem);
                    break;
            }
        }
    }
}
