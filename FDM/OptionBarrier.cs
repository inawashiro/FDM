using System;
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
                double initialPV = i * boundaryPrice / xNum;
                if (isCall && initialPV < barrier)
                {
                    pVArray[0, i] = Math.Max(initialPV - strike, 0);
                }
                else if (!isCall && barrier < initialPV)
                {
                    pVArray[0, i] = Math.Max(strike - initialPV, 0);
                }
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
                int sign = isCall ? 1 : -1;

                pVArray[l, 0] = 0;
                pVArray[l, xNum - 1] = 0;
            }
        }
    }
}
