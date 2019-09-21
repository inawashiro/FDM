using System;
using MathNet.Numerics.Distributions;

namespace FDM
{
    public class OptionVanilla
    {
        public static double[,] SetInitialCondition(
            double[,] pVArray,
            double[] boundaryPrice,
            double strike,
            bool isCall)
        {
            int xNum = pVArray.GetLength(1);
            double dx = boundaryPrice[0] / xNum;

            for (int i = 0; i < xNum; i++)
            {
                double initialPrice = Math.Exp(i * dx);
                int sign = isCall ? 1 : -1;

                pVArray[0, i] = Math.Max(sign * (initialPrice - strike), 0);
            }
            return pVArray;
        }

        public static double[,] SetBoundaryCondition(
            double[,] pVArray,
            double[] boundaryPrice,
            double strike,
            bool isCall)
        {
            int tNum = pVArray.GetLength(0);
            int xNum = pVArray.GetLength(1);
			
			for (int l = 1; l < tNum; l++)
            {
                pVArray[l, 0] = isCall ? 0 : strike;
                pVArray[l, xNum - 1] = isCall ? Math.Max(Math.Exp(boundaryPrice[0]) - strike, 0) : 0;
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
            bool isCall)
        {
            int signCall = isCall ? 1 : -1;

            Func<double, int, double> CalcD = (threshold, signBS) =>
            {
                double d =
                    (Math.Log(initialPrice / threshold) + (domesticRate - foreignRate[0]) * maturity)
                    / (volatility[0] * Math.Sqrt(maturity));
                return d + signBS * 0.5 * volatility[0] * Math.Sqrt(maturity);
            };
            double dPlus = CalcD(strike, 1);
            double dMinus = CalcD(strike, -1);

            double foreignProbability = Normal.CDF(0, 1, signCall * dPlus);
            double domesticProbability = Normal.CDF(0, 1, signCall * dMinus);

            return
                signCall * initialPrice * Math.Exp(-foreignRate[0] * maturity) * foreignProbability
                - signCall * strike * Math.Exp(-domesticRate * maturity) * domesticProbability;
        }

        public static double[,] MakeAnalyticPVArray(
            double[,] pVArray,
            double[] boundaryPrice,
            double strike,
            double maturity,
            double domesticRate,
            double[] foreignRate,
            double[] volatility,
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
                            isCall);
                }
            }
            return pVArray;
        }
    }
}
