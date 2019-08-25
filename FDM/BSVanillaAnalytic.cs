using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;

namespace FDM
{
    public static class BSVanillaAnalytic
    {
        private static void SetInitialCondition(
            int xNum,
            double[,] pVArray,
            double boundaryPV,
            double strike,
            bool isCall)
        {
            for (int i = 0; i < xNum; i++)
            {
                double initialPV = i * boundaryPV / xNum;

                int sign = isCall ? 1 : -1;

                pVArray[0, i] = Math.Max(sign * (initialPV - strike), 0);
            }
        }

        public static double CalculatePV(
            double initialPV,
            double strike,
            double maturity,
            double domesticRate,
            double foreignRate,
            double volatility,
            bool isCall)
        {
            Func<double, double, double> CalcD = (threshold, scale) =>
            {
                double d = (Math.Log(initialPV / threshold) + (domesticRate - foreignRate) * maturity) / (volatility * Math.Sqrt(maturity));
                return d + scale * volatility * Math.Sqrt(maturity);
            };
            double dPlus = CalcD(strike, 0.5);
            double dMinus = CalcD(strike, -0.5);

            int sign = isCall ? 1 : -1;

            return
                sign * initialPV * Math.Exp(-foreignRate * maturity) * Normal.CDF(0, 1, sign * dPlus)
                - sign * strike * Math.Exp(-domesticRate * maturity) * Normal.CDF(0, 1, sign * dMinus);
        }

        public static double[,] Make2DArray(
            double[,] pVArray,
            double boundaryPV,
            double strike,
            double boundaryTime,
            double domesticRate,
            double foreignRate,
            double volatility,
            bool isCall)
        {
            int tNum = pVArray.GetLength(0);
            int xNum = pVArray.GetLength(1);

            SetInitialCondition(xNum, pVArray, boundaryPV, strike, isCall);

            for (int l = 1; l < tNum; l++)
            {
                double maturity = l * boundaryTime / tNum;

                for (int i = 0; i < xNum; i++)
                {
                    double initialPV = i * boundaryPV / xNum;

                    pVArray[l, i] =
                        CalculatePV(
                            initialPV,
                            strike,
                            maturity,
                            domesticRate,
                            foreignRate,
                            volatility,
                            isCall);
                }
            }
            return pVArray;
        }
    }
}