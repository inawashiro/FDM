using System;
using MathNet.Numerics.Distributions;

namespace FDM
{
    public class OptionVanilla
    {
        public static void SetInitialCondition(
            double[,] pVArray,
            double boundaryPrice,
            double strike,
            bool isCall)
        {
            int xNum = pVArray.GetLength(1);
            double dx = boundaryPrice / xNum;

            for (int i = 0; i < xNum; i++)
            {
                double initialPrice = i * dx;
                int sign = isCall ? 1 : -1;

                pVArray[0, i] = Math.Max(sign * (initialPrice - strike), 0);
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
                pVArray[l, 0] = isCall ? 0 : strike;
                pVArray[l, xNum - 1] = isCall ? Math.Max(boundaryPrice - strike, 0) : 0;
            }
        }

        private static double CalculatePV(
            double initialPrice,
            double strike,
            double maturity,
            double domesticRate,
            double foreignRate,
            double volatility,
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
            
            return sign * initialPrice * Math.Exp(-foreignRate * maturity) * Normal.CDF(0, 1, sign * dPlus)
                - sign * strike * Math.Exp(-domesticRate * maturity) * Normal.CDF(0, 1, sign * dMinus);
        }

        public static double[,] CalculatePVArray(
            double[,] pVArray,
            double boundaryPrice,
            double strike,
            double maturity,
            double domesticRate,
            double foreignRate,
            double volatility,
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
                    double initialPrice = i * dx;

                    pVArray[l, i] =
                        CalculatePV(
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
