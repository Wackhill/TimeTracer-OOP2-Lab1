using System.Threading;
using ExecutionTimeTracer.ResultProvider;

namespace ExecutionTimeTracer
{
    internal class Program
    {
        private static Tracer.Tracer _tracer = new Tracer.Tracer();
        public static void Main(string[] args)
        {
            Test1();

            ConsoleResultProvider consoleResultProvider = new ConsoleResultProvider();
            
            JsonSerializer json = new JsonSerializer();
            consoleResultProvider.WriteResult(json.Serialize(_tracer.GetTraceResult()));
        }
        
        private static void Test1()
        {
            _tracer.StartTrace();

            Thread.Sleep(1000);
            Test2();
            
            _tracer.StopTrace();
        }
        
        private static void Test2()
        {
            _tracer.StartTrace();
            
            Test3();
            Fak(1);

            _tracer.StopTrace();
        }

        private static void Test3()
        {
            _tracer.StartTrace();

            Factorial(5);

            _tracer.StopTrace();
        }

        private static long Factorial(int number)
        {
            _tracer.StartTrace();
            
            Thread.Sleep(100);
            
            if (number == 1)
            {
                _tracer.StopTrace();
                return 1;
            }
            else
            {
                _tracer.StopTrace();
                return number * Factorial(number - 1);
            }
        }
        
        private static long Fak(int number)
        {
            _tracer.StartTrace();
            
            _tracer.StopTrace();

            return 10;
        }
    }
}