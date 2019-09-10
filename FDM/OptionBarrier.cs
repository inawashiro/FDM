using System;
using MathNet.Numerics.Distributions;

namespace FDM
{
    public class OptionBarrier
    {
        public static void SetInitialCondition(
            double[,] pVArray,
            double boundaryPrice,
            double strike,
            double barrier,
            bool isCall)
        {
            int xNum = pVArray.GetLength(1);

            for (int i = 0; i < xNum; i++)
            {
                double initialPV = i * boundaryPrice / xNum;
                if (isCall && initialPV < barrier)
                {
                    pVArray[0, i] = Math.Max(initialPV - strike, 0);
                }
                else if (!isCall && barrier < initialPV)
                {
                    pVArray[0, i] = Math.Max(strike - initialPV, 0);
                }
            }
        }

        public static void SetBoundaryCondition(
            double[,] pVArray,
            double boundaryPrice,
            double strike,
            bool isCall)
        {
            int tNum = pVArray.GetLength(0);
            int xNum = pVArray.GetLength(1);

            for (int l = 1; l < tNum; l++)
            {
                int sign = isCall ? 1 : -1;

                pVArray[l, 0] = 0;
                pVArray[l, xNum - 1] = 0;
            }
        }

        private static double CalculatePV(
            double initialPV,
            double strike,
            double maturity,
            double domesticRate,
            double foreignRate,
            double volatility,
            double barrier,
            bool isCall)
        {
            int sign = isCall ? 1 : -1;

            Func<double, double, double> CalcD = (threshold, scale) =>
            {
                double d = (Math.Log(initialPV / threshold) + (domesticRate - foreignRate) * maturity) / (volatility * Math.Sqrt(maturity));
                return d + scale * volatility * Math.Sqrt(maturity);
            };
            double dPlus = CalcD(strike, 0.5);
            double dMinus = CalcD(strike, -0.5);
            double dBarrierPlus = CalcD(barrier, 1);
            double dBarrierMinus = CalcD(barrier, -1);

            return
                sign * initialPV * Math.Exp(-foreignRate * maturity) * (Normal.CDF(0, 1, sign * dPlus) - Normal.CDF(0, 1, sign * dBarrierPlus))
                    - sign * strike * Math.Exp(-domesticRate * maturity) * (Normal.CDF(0, 1, sign * dMinus) - Normal.CDF(0, 1, sign * dBarrierMinus));
        }

        public static double[,] CalculatePVArray(
            double[,] pVArray,
            double boundaryPrice,
            double strike,
            double maturity,
            double domesticRate,
            double foreignRate,
            double volatility,
            double barrier,
            bool isCall)
        {
            int tNum = pVArray.GetLength(0);
            int xNum = pVArray.GetLength(1);

            for (int l = 1; l < tNum; l++)
            {
                double subMaturity = l * maturity / tNum;

                for (int i = 0; i < xNum; i++)
                {
                    double initialPV = i * boundaryPrice / xNum;

                    pVArray[l, i] =
                        CalculatePV(
                            initialPV,
                            strike,
                            subMaturity,
                            domesticRate,
                            foreignRate,
                            volatility,
                            barrier,
                            isCall);
                }
            }
            return pVArray;
        }
    }
}
