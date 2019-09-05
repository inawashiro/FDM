using MathNet.Numerics.LinearAlgebra;

namespace FDM
{
    public class NewMethodTheta
    {
        public static double[,] CalculatePVArray(
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

            var matrixExplicit =
                Matrix<double>.Build.DenseOfArray(
                    CalculateCoefficientMatrix.Explicit(
                        methodType,
                        pVArray,
                        boundaryPrice,
                        strike,
                        maturity,
                        domesticRate,
                        foreignRate,
                        volatility,
                        isCall));
            var matrixImplicit =
                Matrix<double>.Build.DenseOfArray(
                    CalculateCoefficientMatrix.Implicit(
                        methodType,
                        pVArray,
                        boundaryPrice,
                        strike,
                        maturity,
                        domesticRate,
                        foreignRate,
                        volatility,
                        isCall));
            var vector =
                Vector<double>.Build.Dense(xNum, i => pVArray[0, i]);

            for (int l = 1; l < tNum; l++)
            {
                switch (methodType)
                {
                    case Types.MethodType.Explicit:
                        vector = matrixExplicit * vector;
                        break;

                    case Types.MethodType.Implicit:
                        vector = matrixImplicit.Solve(vector);
                        break;

                    case Types.MethodType.CrankNicolson:
                        vector = matrixExplicit * vector;
                        vector = matrixImplicit.Solve(vector);
                        break;
                }
                
                for (int i = 0; i < xNum; i++)
                {
                    pVArray[l, i] = vector[i];
                }
            }
            return pVArray;
        }
    }
}
