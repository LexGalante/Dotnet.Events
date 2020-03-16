
    using System;
using System.Threading;

namespace Dotnet.Events.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var longProcess = new LongProcess();
            longProcess.BeforeEvent += () =>
            {
                Console.WriteLine("Trigger Before Event");

                return new Random().Next(1000, 9999);
            };

            longProcess.AfterEvent += () =>
            {
                Console.WriteLine("Trigger After Event");

                return default;
            };

            longProcess.Run();
        }
    }
}
