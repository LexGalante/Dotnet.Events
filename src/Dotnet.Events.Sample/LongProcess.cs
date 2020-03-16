using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dotnet.Events.Sample
{
    public class LongProcess
    {
        public delegate int LongEvent();

        public event LongEvent BeforeEvent;

        public event LongEvent AfterEvent;

        public void Run()
        {
            var x = BeforeEvent?.Invoke();

            Console.WriteLine($"Trigger Run Slepping {x} miliseconds");
            Thread.Sleep(x ?? 1000);

            AfterEvent?.Invoke();
        }
    }
}
