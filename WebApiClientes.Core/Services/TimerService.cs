using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiClientes.Core.Interfaces;

namespace WebApiClientes.Core.Services
{
    public class TimerService : ITimerService
    {
        Stopwatch _timer;
        public void Start()
        {
            _timer = Stopwatch.StartNew();
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}
