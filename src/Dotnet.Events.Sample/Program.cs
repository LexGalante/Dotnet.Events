
    using System;
using System.Threading;

namespace Dotnet.Events.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("Exemplo de evento");

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

            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("Exemplo de predicate");

            var salarios = new double[] { 3.500, 2.550, 7667.00 };
            Predicate<double> predicate = VerificaSalariosAltos;
            var salariosAltos = Array.Find(salarios, predicate);
            Console.WriteLine($"{salariosAltos}");

            Console.WriteLine("-------------------------------------------------------------");
        }

        public static bool VerificaSalariosAltos(double salario) => salario >= 5000;
    }
}
