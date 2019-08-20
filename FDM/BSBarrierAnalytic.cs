//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using MathNet.Numerics.Distributions;

//namespace FDM
//{
//    public static class BSBarrierAnalytic
//    {
//        //meaningless method, but remained for introducing interface
//        private static void SetBoundaryCondition(
//            int tNum,
//            int xNum,
//            double[,] pVArray,
//            double boundaryPV,
//            double strike,
//            bool isCall) { }
        
//        private static void SetInitialCondition(
//            int xNum,
//            double[,] pVArray,
//            double boundaryPV,
//            double strike,
//            bool isCall,
//            double barrier)
//        {
//            for (int i = 1; i < xNum - 1; i++)
//            {
//                double initialPV = i * boundaryPV / xNum;

//                int sign = isCall ? 1 : -1;

//                pVArray[0, i] = initialPV < barrier ? Math.Max(sign * (initialPV - strike), 0) : 0;
//            }
//        }

//        public static double CalculatePV(
//            double initialPV,
//            double strike,
//            double maturity,
//            double domesticRate,
//            double foreignRate,
//            double volatility,
//            bool isCall)
//        {
//            double d = (Math.Log(initialPV / strike) + domesticRate * maturity) / (volatility * Math.Sqrt(maturity));
//            double dPlus = d + 0.5 * volatility * Math.Sqrt(maturity);
//            double dMinus = d - 0.5 * volatility * Math.Sqrt(maturity);

//            int sign = isCall ? 1 : -1;

//            double pV =
//                sign * initialPV * Normal.CDF(0, 1, sign * dPlus)
//                - sign * strike * Math.Exp(-domesticRate * maturity) * Normal.CDF(0, 1, sign * dMinus);

//            return pV;
//        }

//        public static double[,] Make2DArray(
//            int tNum,
//            int xNum,
//            double boundaryPV,
//            double strike,
//            double boundaryTime,
//            double domesticRate,
//            double foreignRate,
//            double volatility,
//            bool isCall,
//            double barrier)
//        {
//            var pVArray = new double[tNum, xNum];

//            SetBoundaryCondition(tNum, xNum, pVArray, boundaryPV, strike, isCall);
//            SetInitialCondition(xNum, pVArray, boundaryPV, strike, isCall, barrier);

//            for (int l = 1; l < tNum; l++)
//            {
//                double maturity = l * boundaryTime / tNum;

//                for (int i = 0; i < xNum; i++)
//                {
//                    double initialPV = i * boundaryPV / xNum;

//                    pVArray[l, i] =
//                        CalculatePV(
//                            initialPV,
//                            strike,
//                            maturity,
//                            domesticRate,
//                            foreignRate,
//                            volatility,
//                            isCall);
//                }
//            }
//            return pVArray;
//        }
//    }
//}