using Microsoft.AspNetCore.Mvc.Filters;
using WebApiClientes.Core.Services;

namespace WebApiClientes.Filters
{
    public class TimerResourceFilter : IResourceFilter
    {
        TimerService _timer;
        public TimerResourceFilter(TimerService timer)
        {
            timer = _timer;
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            _timer.Stop();
            Console.WriteLine($"Tempo de execução da aplicação: {_timer}");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            _timer.Start();
        }
    }
}
