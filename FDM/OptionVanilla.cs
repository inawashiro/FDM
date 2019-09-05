using System;

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

            for (int i = 0; i < xNum; i++)
            {
                double initialPV = i * boundaryPrice / xNum;

                int sign = isCall ? 1 : -1;

                pVArray[0, i] = Math.Max(sign * (initialPV - strike), 0);
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
                pVArray[l, xNum - 1] =
                    isCall ? Math.Max(((double)xNum - 1) / (double)xNum * boundaryPrice - strike, 0) : 0;
            }
        }
    }
}
