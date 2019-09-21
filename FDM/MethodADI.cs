using MathNet.Numerics.LinearAlgebra;

namespace FDM
{
    public class MethodADI
    {
        public static double[,,] CalculatePVArray(
            Types.MethodType methodType,
            double[,,] pVArray,
            double[] boundaryPrice,
            double maturity,
            double domesticRate,
            double[] foreignRate,
            double[] volatility)
        {
            int tNum = pVArray.GetLength(0);
            var xNum = new int[] { pVArray.GetLength(1), pVArray.GetLength(1) };

            var matrixExplicitX =
                MakeCoefficientMatrixTwoAsset.Explicit(
                        methodType,
                        Types.DifferentialDirection.X,
                        pVArray,
                        boundaryPrice,
                        maturity,
                        domesticRate,
                        foreignRate,
                        volatility);

            var matrixExplicitY =
                MakeCoefficientMatrixTwoAsset.Explicit(
                        methodType,
                        Types.DifferentialDirection.Y,
                        pVArray,
                        boundaryPrice,
                        maturity,
                        domesticRate,
                        foreignRate,
                        volatility);

            var matrixImplicitX =
                MakeCoefficientMatrixTwoAsset.Implicit(
                        methodType,
                        Types.DifferentialDirection.X,
                        pVArray,
                        boundaryPrice,
                        maturity,
                        domesticRate,
                        foreignRate,
                        volatility);

            var matrixImplicitY =
                MakeCoefficientMatrixTwoAsset.Implicit(
                        methodType,
                        Types.DifferentialDirection.Y,
                        pVArray,
                        boundaryPrice,
                        maturity,
                        domesticRate,
                        foreignRate,
                        volatility);

            var matrix =
                Matrix<double>.Build.Dense(xNum[0], xNum[1], (i, j) => pVArray[0, i, j]);

            for (int l = 1; l < tNum; l++)
            {
                switch (methodType)
                {
                    case Types.MethodType.ADI:
                        matrix = matrixExplicitY * matrix;
                        matrix = matrixImplicitX.Solve(matrix);
                        matrix = matrixExplicitX * matrix;
                        matrix = matrixImplicitY.Solve(matrix);
                        break;
                }

                for (int i = 0; i < xNum[0]; i++)
                {
                    for (int j = 0; j < xNum[1]; j++)
                    {
                        pVArray[l, i, j] = matrix[i, j];
                    }
                }
            }
            return pVArray;
        }
    }
}
