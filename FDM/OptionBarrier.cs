using System;
using MathNet.Numerics.Distributions;

namespace FDM
{
    public class OptionBarrier
    {
        public static double[,] SetInitialCondition(
            double[,] pVArray,
            double[] boundaryPrice,
            double strike,
            double barrier,
            bool isCall)
        {
            int xNum = pVArray.GetLength(1);

            for (int i = 0; i < xNum; i++)
            {
                double dx = boundaryPrice[0] / xNum;
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
            return pVArray;
        }

        public static double[,] SetBoundaryCondition(
            double[,] pVArray)
        {
            int tNum = pVArray.GetLength(0);
            int xNum = pVArray.GetLength(1);

            for (int l = 1; l < tNum; l++)
            {
                pVArray[l, 0] = 0;
                pVArray[l, xNum - 1] = 0;
            }
            return pVArray;
        }

        private static double CalculateAnalyticPV(
            double initialPrice,
            double strike,
            double maturity,
            double domesticRate,
            double[] foreignRate,
            double[] volatility,
            double barrier,
            bool isCall)
        {
            int sign = isCall ? 1 : -1;

            Func<double, double, double> CalcD = (threshold, scale) =>
            {
                double d =
                    (Math.Log(initialPrice / threshold) + (domesticRate - foreignRate[0]) * maturity)
                    / (volatility[0] * Math.Sqrt(maturity));
                return d + scale * volatility[0] * Math.Sqrt(maturity);
            };
            double dPlus = CalcD(strike, 0.5);
            double dMinus = CalcD(strike, -0.5);
            double dBarrierPlus = CalcD(barrier, 0.5);
            double dBarrierMinus = CalcD(barrier, -0.5);

            return
                sign * initialPrice * Math.Exp(-foreignRate[0] * maturity) * (Normal.CDF(0, 1, sign * dPlus) - Normal.CDF(0, 1, sign * dBarrierPlus))
                - sign * strike * Math.Exp(-domesticRate * maturity) * (Normal.CDF(0, 1, sign * dMinus) - Normal.CDF(0, 1, sign * dBarrierMinus));
        }

        public static double[,] MakeAnalyticPVArray(
            double[,] pVArray,
            double[] boundaryPrice,
            double strike,
            double maturity,
            double domesticRate,
            double[] foreignRate,
            double[] volatility,
            double barrier,
            bool isCall)
        {
            int tNum = pVArray.GetLength(0);
            int xNum = pVArray.GetLength(1);
            double dt = maturity / tNum;
            double dx = boundaryPrice[0] / xNum;

            for (int l = 1; l < tNum; l++)
            {
                double time = l * dt;

                for (int i = 0; i < xNum; i++)
                {
                    double initialPrice = Math.Exp(i * dx);

                    pVArray[l, i] =
                        CalculateAnalyticPV(
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
