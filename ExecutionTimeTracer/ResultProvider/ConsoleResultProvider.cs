using System;

namespace ExecutionTimeTracer.ResultProvider
{
    public class ConsoleResultProvider: IResultProvider
    {
        public void WriteResult(string result)
        {
            Console.WriteLine(result);
        }
    }
}