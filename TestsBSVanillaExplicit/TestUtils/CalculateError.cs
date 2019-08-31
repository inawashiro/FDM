using FDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;

namespace FDM
{
    public class CalculateError
    {
        public static double MaxAbsoluteError(double[,] pVFDM, double[,] pVAnalytic)
        {
            int tNum = pVAnalytic.GetLength(0);
            int xNum = pVFDM.GetLength(1);
            var absoluteErrorArray = new double[xNum];

            for (int i = 0; i < xNum; i++)
            {
                absoluteErrorArray[i] = Math.Abs(pVFDM[tNum - 1, i] - pVAnalytic[tNum - 1, i]);
            }
            return absoluteErrorArray.Max();
        }
    }
}