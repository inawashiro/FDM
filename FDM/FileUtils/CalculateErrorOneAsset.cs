using FDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;

namespace FDM
{
    public class CalculateErrorOneAsset
    {
        public static double MaxAbsolute(double[,] pVFDM, double[,] pVAnalytic)
        {
            int tNum = pVAnalytic.GetLength(0);
            int xNum = pVFDM.GetLength(1);
            var absoluteErrorArray = new double[xNum];

            for (int i = (int)(0.1 * xNum); i < (int)(0.9 * xNum); i++)
            {
                absoluteErrorArray[i] = Math.Abs(pVFDM[tNum - 1, i] - pVAnalytic[tNum - 1, i]);
            }
            return absoluteErrorArray.Max();
        }

        public static double[] AbsoluteArray(double[,] pVFDM, double[,] pVAnalytic)
        {
            int tNum = pVAnalytic.GetLength(0);
            int xNum = pVFDM.GetLength(1);
            var absoluteErrorArray = new double[xNum];

            for (int i = 0; i < xNum; i++)
            {
                absoluteErrorArray[i] = Math.Abs(pVFDM[tNum - 1, i] - pVAnalytic[tNum - 1, i]);
            }
            return absoluteErrorArray;
        }
    }
}