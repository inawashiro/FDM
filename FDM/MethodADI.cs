//using MathNet.Numerics.LinearAlgebra;

//namespace FDM
//{
//    public class MethodADI
//    {
//        public static double[,] CalculatePVArray(
//            Types.MethodType methodType,
//            double[,,] pVArray,
//            double[] boundaryPrice,
//            double maturity,
//            double domesticRate,
//            double[] foreignRate,
//            double[] volatility)
//        {
//            int tNum = pVArray.GetLength(0);
//            int xNum = pVArray.GetLength(1);

//            var matrixExplicitX =
//                Matrix<double>.Build.DenseOfArray(
//                    MakeCoefficientMatrixTwoAsset.Explicit(
//                        methodType,
//                        Types.DifferentialDirection.X,
//                        pVArray,
//                        boundaryPrice,
//                        maturity,
//                        domesticRate,
//                        foreignRate,
//                        volatility));

//            var matrixImplicitX =
//                Matrix<double>.Build.DenseOfArray(
//                    MakeCoefficientMatrixTwoAsset.Implicit(
//                        methodType,
//                        Types.DifferentialDirection.X,
//                        pVArray,
//                        boundaryPrice,
//                        maturity,
//                        domesticRate,
//                        foreignRate,
//                        volatility));

//            var vector =
//                Vector<double>.Build.Dense(xNum, i => pVArray[0, i]);

//            for (int l = 1; l < tNum; l++)
//            {
//                switch (methodType)
//                {
//                    case Types.MethodType.ADI:
//                        vector = matrixImplicit.Solve(vector);
//                        vector = matrixExplicit * vector;
//                        break;
//                }

//                for (int i = 0; i < xNum; i++)
//                {
//                    pVArray[l, i] = vector[i];
//                }
//            }
//            return pVArray;
//        }
//    }
//}
