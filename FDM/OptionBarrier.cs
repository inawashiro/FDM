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
                double dx = boundaryPrice / xNum;
                double initialPrice = Math.Exp(i * dx);

                if (isCall && initialPrice < barrier)
                {
                    pVArray[0, i] = Math.Max(initialPrice - strike, 0);
                }
                else if (!isCall && barrier < initialPrice)
                {
                    pVArray[0, i] = Math.Max(strike - initialPrice, 0);
                }
            }
        }

        public static void SetBoundaryCondition(
            double[,] pVArray)
        {
            int tNum = pVArray.GetLength(0);
            int xNum = pVArray.GetLength(1);

            for (int l = 1; l < tNum; l++)
            {
                pVArray[l, 0] = 0;
                pVArray[l, xNum - 1] = 0;
            }
        }

        private static double CalculatePV(
            double initialPrice,
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
                double d = (Math.Log(initialPrice / threshold) + (domesticRate - foreignRate) * maturity) / (volatility * Math.Sqrt(maturity));
                return d + scale * volatility * Math.Sqrt(maturity);
            };
            double dPlus = CalcD(strike, 0.5);
            double dMinus = CalcD(strike, -0.5);
            double dBarrierPlus = CalcD(barrier, 1);
            double dBarrierMinus = CalcD(barrier, -1);

            return
                sign * initialPrice * Math.Exp(-foreignRate * maturity) * (Normal.CDF(0, 1, sign * dPlus) - Normal.CDF(0, 1, sign * dBarrierPlus))
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
            double dt = maturity / tNum;
            double dx = boundaryPrice / xNum;

            for (int l = 1; l < tNum; l++)
            {
                double time = l * dt;

                for (int i = 0; i < xNum; i++)
                {
                    double initialPrice = Math.Exp(i * dx);

                    pVArray[l, i] =
                        CalculatePV(
                            initialPrice,
                            strike,
                            time,
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
