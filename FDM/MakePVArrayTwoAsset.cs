//namespace FDM
//{
//    public class MakePVArrayTwoAsset
//    {
//        public double[,,] Analytic(Parameters parameters, Types.OptionType optionType)
//        {
//            var pVArray = default(double[,,]);

//            switch (optionType)
//            {
//                case Types.OptionType.Exchange:
//                    pVArray =
//                        OptionExchange.MakeAnalyticPVArray(
//                            new double[parameters.TNum, parameters.XNum[0], parameters.XNum[1]],
//                            parameters.BoundaryPrice,
//                            parameters.Maturity,
//                            parameters.ForeignRate,
//                            parameters.Volatility,
//                            parameters.Correlation);
//                    break;
//            }
//            return pVArray;
//        }

//        public double[,,] FDM(Parameters parameters, Types.OptionType optionType, Types.MethodType methodType) 
//        {
//            var pVArray = default(double[,,]);

//            switch (optionType)
//            {
//                case Types.OptionType.Vanilla:
//                    pVArray =
//                        OptionExchange.SetInitialCondition(
//                            new double[parameters.TNum, parameters.XNum[0], parameters.XNum[1]],
//                            parameters.BoundaryPrice);

//                    pVArray =
//                        OptionExchange.SetBoundaryCondition(
//                            pVArray,
//                            parameters.BoundaryPrice);

//                    pVArray =
//                        MethodADI.CalculatePVArray(
//                            methodType,
//                            pVArray,
//                            parameters.BoundaryPrice,
//                            parameters.Maturity,
//                            parameters.DomesticRate,
//                            parameters.ForeignRate,
//                            parameters.Volatility);
//                    break;
//            }
//            return pVArray;
//        }
//    }
//}
