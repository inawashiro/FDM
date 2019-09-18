//using System;
//using MathNet.Numerics.Distributions;

//namespace FDM
//{
//    public class OptionExchange
//    {
//        public static double[,,] SetInitialCondition(
//            double[,,] pVArray,
//            double[] boundaryPrice)
//        {
//            int xNum = pVArray.GetLength(1);
//            int yNum = pVArray.GetLength(2);
//            double dx = boundaryPrice[0] / xNum;
//            double dy = boundaryPrice[1] / yNum;

//            for (int i = 0; i < xNum; i++)
//            {
//                for (int j = 0; j < yNum; j++)
//                {
//                    var initialPrice = new double[] { Math.Exp(i * dx), Math.Exp(j * dy) };
//                    pVArray[0, i, j] =
//                        Math.Max(initialPrice[1] - initialPrice[0], 0);
//                }
//            }
//            return pVArray;
//        }

//        public static double[,,] SetBoundaryCondition(
//            double[,,] pVArray,
//            double[] boundaryPrice)
//        {
//            int tNum = pVArray.GetLength(0);
//            int xNum = pVArray.GetLength(1);
//            int yNum = pVArray.GetLength(2);

//            for (int l = 1; l < tNum; l++)
//            {
//                pVArray[l, 0, 0] = 0;
//                pVArray[l, 0, yNum - 1] = Math.Max(Math.Exp(boundaryPrice[1]) - 1, 0);
//                pVArray[l, xNum - 1, 0] = Math.Max(1 - Math.Exp(boundaryPrice[0]), 0);
//                pVArray[l, xNum - 1, yNum - 1] = Math.Max(Math.Exp(boundaryPrice[1]) - Math.Exp(boundaryPrice[0]), 0);
//            }
//            return pVArray;
//        }

//        private static double CalculateAnalyticPV(
//            double[] initialPrice,
//            double maturity,
//            double[] foreignRate,
//            double[] volatility,
//            double correlation)
//        {
//            Func<double, double, double> CalcD = (threshold, scale) =>
//            {
//                double crossVolatility =
//                    Math.Sqrt(
//                        volatility[0] * volatility[0]
//                        + volatility[1] * volatility[1]
//                        - 2 * correlation * volatility[0] * volatility[1]);
//                double d =
//                    (Math.Log(initialPrice[1] * Math.Exp(-foreignRate[1] * maturity) / threshold))
//                    / (crossVolatility * Math.Sqrt(maturity));
//                return d + scale * crossVolatility * Math.Sqrt(maturity);
//            };
//            double dPlus = CalcD(initialPrice[0] * Math.Exp(-foreignRate[0] * maturity), 0.5);
//            double dMinus = CalcD(initialPrice[0] * Math.Exp(-foreignRate[0] * maturity), -0.5);

//            return
//                initialPrice[1] * Math.Exp(-foreignRate[1] * maturity) * Normal.CDF(0, 1, dPlus)
//                - initialPrice[0] * Math.Exp(-foreignRate[0] * maturity) * Normal.CDF(0, 1, dMinus);
//        }

//        public static double[,,] MakeAnalyticPVArray(
//            double[,,] pVArray,
//            double[] boundaryPrice,
//            double maturity,
//            double[] foreignRate,
//            double[] volatility,
//            double correlation)
//        {
//            int tNum = pVArray.GetLength(0);
//            int xNum = pVArray.GetLength(1);
//            int yNum = pVArray.GetLength(2);
//            double dt = maturity / tNum;
//            double dx = boundaryPrice[0] / xNum;
//            double dy = boundaryPrice[1] / xNum;

//            for (int l = 1; l < tNum; l++)
//            {
//                double time = l * dt;

//                for (int i = 0; i < xNum; i++)
//                {
//                    for (int j = 0; j < yNum; j++)
//                    {
//                        var initialPrice = new double[] { Math.Exp(i * dx), Math.Exp(j * dy) };
//                        pVArray[l, i, j] =
//                            CalculateAnalyticPV(
//                                initialPrice,
//                                time,
//                                foreignRate,
//                                volatility,
//                                correlation);
//                    }
//                }
//            }
//            return pVArray;
//        }
//    }
//}
