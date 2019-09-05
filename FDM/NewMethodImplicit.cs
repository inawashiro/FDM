using MathNet.Numerics.LinearAlgebra;

namespace FDM
{
    public class NewMethodImplicit
    {
        public static double[,] CalculateCoefficientArray(
            Types.MethodType methodType,
            double[,] pVArray,
            double boundaryPrice,
            double strike,
            double maturity,
            double domesticRate,
            double foreignRate,
            double volatility,
            bool isCall)
        {
            int tNum = pVArray.GetLength(0);
            int xNum = pVArray.GetLength(1);
            double dx = boundaryPrice / xNum;
            double dt = maturity / tNum;
            var coefficientArray = new double[xNum, xNum];
            double theta = DefineTheta.Define(methodType);

            coefficientArray[0, 0] = 1;
            coefficientArray[0, 1] = 0;
            for (int i = 1; i < xNum - 1; i++)
            {
                double initialPV = i * boundaryPrice / xNum;

                double a1 = (domesticRate - foreignRate) * initialPV;
                double b11 = 0.5 * volatility * volatility * initialPV * initialPV;
                double f = -domesticRate;

                double a = theta * dt * (-b11 / (dx * dx) + 0.5 * a1/ dx);
                double b = 1 + theta * dt * (f + 2 * b11 / (dx * dx));
                double c = theta * dt * (-b11 / (dx * dx) - 0.5 * a1 / dx);

                coefficientArray[i, i - 1] = a;
                coefficientArray[i, i] = b;
                coefficientArray[i, i + 1] = c;
            }
            coefficientArray[xNum - 1, xNum - 2] = 0;
            coefficientArray[xNum - 1, xNum - 1] = 1;

            return coefficientArray;
        }
    }
}
