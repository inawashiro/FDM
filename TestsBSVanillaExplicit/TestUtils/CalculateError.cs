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
        public static double ModifiedMeanRelative(double[,] pVFDM, double[,] pVAnalytic)
        {
            double modifiedRelativeErrorSquareSum = 0;
            int tNum = pVAnalytic.GetLength(0);
            int xNum = pVFDM.GetLength(1);

            double epsilon = 1e-1;
            for (int i = 0; i < xNum; i++)
            {
                double modifiedRelativeError = (pVFDM[tNum - 1, i] - pVAnalytic[tNum - 1, i]) / (pVAnalytic[tNum - 1, i] + epsilon);
                modifiedRelativeErrorSquareSum += modifiedRelativeError * modifiedRelativeError;
            }
            return Math.Sqrt(modifiedRelativeErrorSquareSum) /xNum;
        }
    }
}