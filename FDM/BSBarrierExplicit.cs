using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDM
{
    public static class BSBarrierExplicit
    {
        private static void SetInitialCondition(
            double[,] pVArray,
            double boundaryPV,
            double strike,
            double barrier,
            bool isCall)
        {
            int xNum = pVArray.GetLength(1);

            for (int i = 0; i < xNum; i++)
            {
                double initialPV = i * boundaryPV / xNum;
                if (isCall && initialPV < barrier)
                {
                    pVArray[0, i] = Math.Max(initialPV - strike, 0);
                }
                else if (isCall == false && barrier < initialPV)
                {
                    pVArray[0, i] = Math.Max(strike - initialPV, 0);
                }
            }
        }

        private static void SetBoundaryCondition(
            double[,] pVArray,
            double boundaryPV,
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

        public static double[,] CalculatePVArray(
            double[,] pVArray,
            double boundaryPV,
            double strike,
            double barrier,
            double boundaryTime,
            double domesticRate,
            double foreignRate,
            double volatility,
            bool isCall)
        {
            SetInitialCondition(pVArray, boundaryPV, strike, barrier, isCall);
            SetBoundaryCondition(pVArray, boundaryPV, strike, isCall);

            int tNum = pVArray.GetLength(0);
            int xNum = pVArray.GetLength(1);

            double dx = boundaryPV / xNum;
            double dt = boundaryTime / tNum;

            for (int l = 1; l < tNum; l++)
            {
                for (int i = 1; i < xNum - 1; i++)
                {
                    double initialPV = i * boundaryPV / xNum;

                    double a1 = (domesticRate - foreignRate) * initialPV;
                    double b11 = 0.5 * volatility * volatility * initialPV * initialPV;

                    double a = b11 * dt / (dx * dx) - 0.5 * a1 * dt / dx;
                    double b = 1 + domesticRate * dt - 2 * b11 * dt / (dx * dx);
                    double c = b11 * dt / (dx * dx) + 0.5 * a1 * dt / dx;

                    pVArray[l, i] =
                        a * pVArray[l - 1, i - 1]
                        + b * pVArray[l - 1, i]
                        + c * pVArray[l - 1, i + 1];
                }
            }
            return pVArray;
        }
    }
}