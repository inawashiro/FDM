using System;
using MathNet.Numerics.Distributions;

namespace FDM
{
    public class OptionExchange
    {
        public static double[,,] SetInitialCondition(
            double[,,] pVArray,
            double[] boundaryPrice)
        {
            var xNum = new int[] { pVArray.GetLength(1), pVArray.GetLength(2) };
            var dx = new double[] { boundaryPrice[0] / xNum[0] , boundaryPrice[1] / xNum[1] };

            for (int i = 0; i < xNum[0]; i++)
            {
                for (int j = 0; j < xNum[1]; j++)
                {
                    pVArray[0, i, j] = Math.Max(Math.Exp(j * dx[1]) - Math.Exp(i * dx[0]), 0);
                }
            }
            return pVArray;
        }

        public static double[,,] SetBoundaryCondition(
            double[,,] pVArray,
            double[] boundaryPrice)
        {
            int tNum = pVArray.GetLength(0);
            var xNum = new int[] { pVArray.GetLength(1) , pVArray.GetLength(2) } ;
            var dx = new double[] { boundaryPrice[0] / xNum[0], boundaryPrice[1] / xNum[1] };

            for (int l = 1; l < tNum; l++)
            {
                for (int i = 0; i < xNum[0]; i++)
                {
                    pVArray[l, i, 0] = Math.Max(1 - Math.Exp(i * dx[0]), 0);
                    pVArray[l, i, xNum[1] - 1] = Math.Max(Math.Exp(boundaryPrice[1]) - Math.Exp(i * dx[0]), 0);
                }
                for (int j = 0; j < xNum[1]; j++)
                {
                    pVArray[l, 0, j] = Math.Max(Math.Exp(j * dx[1]) - 1, 0);
                    pVArray[l, xNum[0] - 1, j] = Math.Max(Math.Exp(j * dx[1]) - Math.Exp(boundaryPrice[0]), 0);
                }
            }
            return pVArray;
        }

        private static double CalculateAnalyticPV(
            double[] initialPrice,
            double maturity,
            double[] foreignRate,
            double[] volatility,
            double correlation)
        {
            Func<double, double, double> CalcD = (threshold, scale) =>
            {
                double crossVolatility =
                    Math.Sqrt(
                        volatility[0] * volatility[0]
                        + volatility[1] * volatility[1]
                        - 2 * correlation * volatility[0] * volatility[1]);
                double d =
                    (Math.Log(initialPrice[1] * Math.Exp(-foreignRate[1] * maturity) / threshold))
                    / (crossVolatility * Math.Sqrt(maturity));
                return d + scale * crossVolatility * Math.Sqrt(maturity);
            };
            double dPlus = CalcD(initialPrice[0] * Math.Exp(-foreignRate[0] * maturity), 0.5);
            double dMinus = CalcD(initialPrice[0] * Math.Exp(-foreignRate[0] * maturity), -0.5);

            return
                initialPrice[1] * Math.Exp(-foreignRate[1] * maturity) * Normal.CDF(0, 1, dPlus)
                - initialPrice[0] * Math.Exp(-foreignRate[0] * maturity) * Normal.CDF(0, 1, dMinus);
        }

        public static double[,,] MakeAnalyticPVArray(
            double[,,] pVArray,
            double[] boundaryPrice,
            double maturity,
            double[] foreignRate,
            double[] volatility,
            double correlation)
        {
            int tNum = pVArray.GetLength(0);
            int xNum = pVArray.GetLength(1);
            int yNum = pVArray.GetLength(2);
            double dt = maturity / tNum;
            double dx = boundaryPrice[0] / xNum;
            double dy = boundaryPrice[1] / xNum;

            for (int l = 1; l < tNum; l++)
            {
                double time = l * dt;

                for (int i = 0; i < xNum; i++)
                {
                    for (int j = 0; j < yNum; j++)
                    {
                        var initialPrice = new double[] { Math.Exp(i * dx), Math.Exp(j * dy) };
                        pVArray[l, i, j] =
                            CalculateAnalyticPV(
                                initialPrice,
                                time,
                                foreignRate,
                                volatility,
                                correlation);
                    }
                }
            }
            return pVArray;
        }
    }
}
