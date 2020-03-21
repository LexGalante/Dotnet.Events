
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace Dotnet.Events.Sample
{
    class Program
    {
        delegate void ShowText();

        delegate void Show(string text);

        delegate int RandomInt();

        delegate string UpperCaseText();

        public static LongProcess longProcess = new LongProcess();

        static void Main(string[] args)
        {
            ExampleSimpleDelegate();
            ExampleDelegateReturn();
            ExampleRemoveEvent();
            ExampleCreateEvent();
            ExampleActionAndFunc();
            ExampleWithFuncAsParameter();
            ExampleEventHandler();
        }

        public static void ExampleSimpleDelegate()
        {
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("example simple delegate");

            ShowText showText = PrintDateTime;
            showText += PrintTimeSpan;
            showText += PrintEnviroment;
            showText += PrintEnviromentCpu;
            Console.WriteLine($"{showText.GetInvocationList().Length}");
            showText -= PrintEnviromentCpu;
            Console.WriteLine($"{showText.GetInvocationList().Length}");
            Console.WriteLine(showText.Method);
            Console.WriteLine(showText.Target);
            showText.Invoke();

            Show show = (string text) => Console.WriteLine(text);
            show.Invoke("Show....");

            Console.WriteLine("-------------------------------------------------------------");
        }

        public static void ExampleDelegateReturn()
        {
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("example delegate return value");

            RandomInt del = GetRandomInt;
            del += GetRandomInt;
            del += GetRandomInt;
            del += GetRandomInt;
            del += GetRandomInt;
            del += GetRandomInt;
            del += GetRandomInt;
            del += GetRandomInt;
            del += GetRandomInt;
            del += GetRandomInt;
            del += GetRandomInt;
            del += GetRandomInt;

            Console.WriteLine($"actions in delagate: {del.GetInvocationList().Length}");

            foreach (RandomInt d in del.GetInvocationList())
                Console.WriteLine(d());

            Console.WriteLine("-------------------------------------------------------------");
        }

        public static void ExampleCreateEvent() 
        {
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("created event example");

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

        public static void ExampleRemoveEvent()
        {
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("remove event example");

            longProcess.BeforeEvent -= () =>
            {
                Console.WriteLine("Trigger Before Event");

                return new Random().Next(1000, 9999);
            };

            longProcess.AfterEvent -= () =>
            {
                Console.WriteLine("Trigger After Event");

                return default;
            };

            longProcess.Run();

            Console.WriteLine("-------------------------------------------------------------");
        }

        public static void ExampleActionAndFunc() 
        {
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("action example");

            Action action = () => Console.WriteLine("example action");
            action.Invoke();

            Func<int, int, int> func = (int min, int max) => new Random().Next(min, max);
            var result = func.Invoke(1000, 9999);
            Console.WriteLine($"example func with result: {result}");

            Console.WriteLine("-------------------------------------------------------------");
        }

        public static void ExampleWithFuncAsParameter() 
        {
            Console.WriteLine("-------------------------------------------------------------");
            var result = ProcessDelegate((string[] parameters) => string.Join(' ', parameters), new string[] { "example", "delegate", "as", "parameter" });
            Console.WriteLine(result);

            Console.WriteLine("-------------------------------------------------------------");
        }

        public static void ExampleEventHandler()
        {
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("example event handler");
            var shorProcess = new ShortProcess();
            shorProcess.Process += (object sender, CustomEventArgs e) =>
            {
                Console.WriteLine($"event handler are invoked, sender type: {sender.GetType().Name}, and args: {e.GetType().Name}, {e.Text}");
            };

            shorProcess.Run(new CustomEventArgs() 
            {
                Text = "custom event args"
            });

            Console.WriteLine("-------------------------------------------------------------");
        }

        public static void PrintDateTime() => Console.WriteLine(DateTime.Now.ToShortDateString());

        public static void PrintTimeSpan() => Console.WriteLine(TimeSpan.FromMinutes(1));

        public static void PrintEnviroment() => Console.WriteLine(Environment.OSVersion.ToString());

        public static void PrintEnviromentCpu() => Console.WriteLine(Environment.ProcessorCount);

        public static string ProcessDelegate(Expression<Func<string[], string>> expression, string[] parameters)
        {
            var compileResult = expression.Compile();

            return compileResult.Invoke(parameters);
        }

        public static int GetRandomInt() => new Random().Next(1000,9999);

        public static bool VerificaSalariosAltos(double salario) => salario >= 5000;
    }
}
