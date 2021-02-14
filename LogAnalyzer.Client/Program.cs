using System;

namespace LogAn.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var analyzer = new LogAnalyzer();
            var isValid = analyzer.IsValidLogFileNameGettingFromConfig("good.slf");
            Console.WriteLine(isValid);
            Console.ReadKey();
        }
    }
}
