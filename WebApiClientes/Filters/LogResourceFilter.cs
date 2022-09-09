using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace WebApiClientes.Filters
{
    public class LogResourceFilter : IResourceFilter
    {
        Stopwatch _timer;

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.WriteLine("Filtro de Resource LogResourceFilter (APÓS) OnResourceExecuted");
            _timer.Stop();
            Console.WriteLine($"Tempo de execução da aplicação: {_timer.ElapsedMilliseconds} ms");

        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            if (!context.HttpContext.Request.Headers.Keys.Contains("Code"))
            {
                context.HttpContext.Request.Headers.Add("Code", Guid.NewGuid().ToString());
            }

            Console.WriteLine("Filtro de Resource LogResourceFilter (ANTES) OnResourceExecuting");

            _timer = Stopwatch.StartNew();

        }
    }
}
