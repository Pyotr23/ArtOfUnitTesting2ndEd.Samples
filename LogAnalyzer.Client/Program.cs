using System;

namespace LogAn.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var analyzer = new LogAnalyzerUsingFactoryMethod();
            var isValid = analyzer.IsValidLogFileName("good.slf");
            Console.WriteLine(isValid);
            Console.ReadKey();
        }
    }
}
