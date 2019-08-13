using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDM
{
    class BSVanillaOptionValueCalculator : IOptionValueCalculator
    {
        public double[,] CalculatePV(int spatialDivisionNum,
                                              int temporalDivisionNum,
                                              double boundaryOptionValue,
                                              double strike,
                                              double maturity,
                                              double domesticRate,
                                              double foreignRate,
                                              double volatility,
                                              bool isCall)
        {
            double[,] optionValuesArray = new double[temporalDivisionNum, spatialDivisionNum];
            for (int l = 0; l < temporalDivisionNum; l++)
            {
                //boundary conditions
                optionValuesArray[l, 0] = 0;
                optionValuesArray[l, spatialDivisionNum - 1] = 0;

                for (int i = 1; i < spatialDivisionNum - 1; i++)
                {
                    double initialOptionValue = i * boundaryOptionValue / spatialDivisionNum;
                    //initial condition
                    optionValuesArray[0, i] = Math.Max(initialOptionValue - strike, 0);
                }
            }

            double dx = boundaryOptionValue / spatialDivisionNum;
            double dt = maturity / temporalDivisionNum;

            for (int l = 0; l < temporalDivisionNum - 1; l++)
            {
                for (int i = 1; i < spatialDivisionNum - 1; i++)
                {
                    double initialOptionValue = i * boundaryOptionValue / spatialDivisionNum;

                    double a1 = domesticRate * initialOptionValue;
                    double b11 = 0.5 * volatility * volatility * initialOptionValue * initialOptionValue;

                    double a = b11 * dt / (dx * dx) - 0.5 * a1 * dt / dx;
                    double b = 1 + domesticRate * dt - 2 * b11 * dt / (dx * dx);
                    double c = b11 * dt / (dx * dx) + 0.5 * a1 * dt / dx;

                    optionValuesArray[l + 1, i] = a * optionValuesArray[l, i - 1]
                                                  + b * optionValuesArray[l, i]
                                                  + c * optionValuesArray[l, i + 1];
                }
            }
            return optionValuesArray;
        }
    }
}
