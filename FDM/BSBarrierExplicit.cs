//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace FDM
//{
//    public class BSBarrierExplicit
//    {
//        private static void SetBoundaryCondition(
//            int tNum,
//            int xNum,
//            double[,] pVArray,
//            double boundaryPV,
//            double strike,
//            bool isCall)
//        {
//            for (int l = 0; l < tNum; l++)
//            {
//                int sign = isCall ? 1 : -1;

//                pVArray[l, 0] = 0;
//                pVArray[l, xNum - 1] = Math.Max(sign * (((double)xNum - 1) / (double)xNum * boundaryPV - strike), 0);
//            }
//        }
//        private static void SetInitialCondition(
//            int xNum,
//            double[,] pVArray,
//            double boundaryPV,
//            double strike,
//            bool isCall)
//        {
//            for (int i = 1; i < xNum - 1; i++)
//            {
//                double initialPV = i * boundaryPV / xNum;

//                int sign = isCall ? 1 : -1;

//                pVArray[0, i] = Math.Max(sign * (initialPV - strike), 0);
//            }
//        }
//        public static double[,] CalculatePVArray(
//            int xNum,
//            int tNum,
//            double boundaryPV,
//            double strike,
//            double boundaryTime,
//            double domesticRate,
//            double foreignRate,
//            double volatility,
//            bool isCall)
//        {
//            double[,] pVArray = new double[tNum, xNum];

//            SetBoundaryCondition(tNum, xNum, pVArray, boundaryPV, strike, isCall);
//            SetInitialCondition(xNum, pVArray, boundaryPV, strike, isCall);

//            double dx = boundaryPV / xNum;
//            double dt = boundaryTime / tNum;

//            for (int l = 1; l < tNum; l++)
//            {
//                for (int i = 1; i < xNum - 1; i++)
//                {
//                    double initialPV = i * boundaryPV / xNum;

//                    double a1 = domesticRate * initialPV;
//                    double b11 = 0.5 * volatility * volatility * initialPV * initialPV;

//                    double a = b11 * dt / (dx * dx) - 0.5 * a1 * dt / dx;
//                    double b = 1 + domesticRate * dt - 2 * b11 * dt / (dx * dx);
//                    double c = b11 * dt / (dx * dx) + 0.5 * a1 * dt / dx;

//                    pVArray[l, i] =
//                        a * pVArray[l - 1, i - 1]
//                        + b * pVArray[l - 1, i]
//                        + c * pVArray[l - 1, i + 1];
//                }
//            }
//            return pVArray;
//        }
//    }
//}