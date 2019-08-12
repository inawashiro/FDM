using System;

namespace FDM
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new BSCalculatorFactory(100, 100, 1.0, 120);
            Console.WriteLine($"SpatialDivisionNum = {test.SpatialDivisionNum}");
            Console.WriteLine($"TemporalDivisionNum = {test.TemporaryDivisionNum}");
            Console.WriteLine($"Maturity = {test.Maturity}");
            Console.WriteLine($"MaxValue = {test.MaxValue}");
        }
    }
}
