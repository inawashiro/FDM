using MathNet.Numerics.LinearAlgebra;

namespace FDM
{
    public class MakeCoefficientMatrixTwoAsset
    {
        public static Matrix<double> Explicit(
            Types.MethodType methodType,
            Types.DifferentialDirection differentialDirection,
            double[,,] pVArray,
            double[] boundaryPrice,
            double maturity,
            double domesticRate,
            double[] foreignRate,
            double[] volatility)
        {
            int tNum = pVArray.GetLength(0);
            int xNum = pVArray.GetLength(1);
            double dx = boundaryPrice[0] / xNum;
            double dt = maturity / tNum;
            var coefficientArray = new double[xNum, xNum];
            double theta = DefineTheta.Define(methodType);

            int bit = differentialDirection == Types.DifferentialDirection.X ? 0 : 1;
            double a1 = domesticRate - foreignRate[bit] - 0.5 * volatility[bit] * volatility[bit];
            double b11 = 0.5 * volatility[bit] * volatility[bit];
            double f = -domesticRate;

            coefficientArray[0, 0] = 1;
            coefficientArray[bit, 1 - bit] = 0;
            for (int i = 1; i < xNum - 1; i++)
            {
                double a = (1 - theta) * dt * (b11 / (dx * dx) - 0.5 * a1 / dx);
                double b = 1 + (1 - theta) * dt * (f - 2 * b11 / (dx * dx));
                double c = (1 - theta) * dt * (b11 / (dx * dx) + 0.5 * a1 / dx);

                coefficientArray[i - bit, i + bit - 1] = a;
                coefficientArray[i, i] = b;
                coefficientArray[i + bit, i - bit + 1] = c;
            }
            coefficientArray[xNum - 1 - bit, xNum - 2 + bit] = 0;
            coefficientArray[xNum - 1, xNum - 1] = 1;

            var matrix = Matrix<double>.Build.Dense(xNum, xNum, (i, j) => coefficientArray[i, j]);

            return matrix;
        }

        public static Matrix<double> Implicit(
            Types.MethodType methodType,
            Types.DifferentialDirection differentialDirection,
            double[,,] pVArray,
            double[] boundaryPrice,
            double maturity,
            double domesticRate,
            double[] foreignRate,
            double[] volatility)
        {
            int tNum = pVArray.GetLength(0);
            int xNum = pVArray.GetLength(1);
            double dx = boundaryPrice[0] / xNum;
            double dt = maturity / tNum;
            var coefficientArray = new double[xNum, xNum];
            double theta = DefineTheta.Define(methodType);

            int bit = differentialDirection == Types.DifferentialDirection.X ? 0 : 1;
            double a1 = domesticRate - foreignRate[bit] - 0.5 * volatility[bit] * volatility[bit];
            double b11 = 0.5 * volatility[bit] * volatility[bit];
            double f = -domesticRate;

            coefficientArray[0, 0] = 1;
            coefficientArray[bit, 1 - bit] = 0;
            for (int i = 1; i < xNum - 1; i++)
            {
                double a = theta * dt * (-b11 / (dx * dx) + 0.5 * a1 / dx);
                double b = 1 + theta * dt * (f + 2 * b11 / (dx * dx));
                double c = theta * dt * (-b11 / (dx * dx) - 0.5 * a1 / dx);

                coefficientArray[i - bit, i + bit - 1] = a;
                coefficientArray[i, i] = b;
                coefficientArray[i + bit, i - bit + 1] = c;
            }
            coefficientArray[xNum - 1 - bit, xNum - 2 + bit] = 0;
            coefficientArray[xNum - 1, xNum - 1] = 1;

            var matrix = Matrix<double>.Build.Dense(xNum, xNum, (i, j) => coefficientArray[i, j]);

            return matrix;
        }

        //public static double[,] FirstOrderDerivativeCoefficientArray(
        //    double[] volatility,
        //    double[,] correlation)
        //{
        //    var array = new double[2, 2];
        //    for (int i = 0; i < 2; i++)
        //    {
        //        for (int j = 0; j < 2; j++)
        //        {
        //            array[i, j] = 0.5 * correlation[i, j] * volatility[i] * volatility[j];
        //        }
        //    }
        //    return array;
        //}

        //public static double[,] SecondOrderDerivativeCoefficientArray(
        //    double[] volatility,
        //    double[,] correlation)
        //{
        //    var array = new double[2, 2];
        //    for (int i = 0; i < 2; i++)
        //    {
        //        for (int j = 0; j < 2; j++)
        //        {
        //            array[i, j] = 0.5 * correlation[i, j] * volatility[i] * volatility[j];
        //        }
        //    }
        //    return array;
        //}
    }
}

