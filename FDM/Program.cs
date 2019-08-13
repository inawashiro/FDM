using System;

namespace FDM
{
    class Program
    {
        static void Main(string[] args)
        {
            var bSVanillaOptionValueCalculator = new BSVanillaOptionValueCalculator();
            var bSVanillaOptionPV
                = bSVanillaOptionValueCalculator.CalculatePV(10, 10, 200, 110, 1, 0.1, 2.0, 0.3, true);

            for (int l = 0; l < 10; l++)
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.Write($"{bSVanillaOptionPV[l, i]}, ");
                }
                Console.WriteLine();
            }
        }
    }
}
