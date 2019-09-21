namespace FDM
{
    public class MakePVArrayTwoAsset
    {
        public double[,] Analytic(Parameters parameters, Types.OptionType optionType)
        {
            var pVArray = default(double[,]);

            switch (optionType)
            {
                case Types.OptionType.Vanilla:
                    pVArray =
                        OptionVanilla.MakeAnalyticPVArray(
                            new double[parameters.TNum, parameters.XNum[0]],
                            parameters.BoundaryPrice,
                            parameters.Strike,
                            parameters.Maturity,
                            parameters.DomesticRate,
                            parameters.ForeignRate,
                            parameters.Volatility,
                            parameters.IsCall);
                    break;

                case Types.OptionType.Barrier:
                    pVArray =
                        OptionBarrier.MakeAnalyticPVArray(
                            new double[parameters.TNum, parameters.XNum[0]],
                            parameters.BoundaryPrice,
                            parameters.Strike,
                            parameters.Maturity,
                            parameters.DomesticRate,
                            parameters.ForeignRate,
                            parameters.Volatility,
                            parameters.Barrier,
                            parameters.IsCall);
                    break;
            }
            return pVArray;
        }

        public double[,] FDM(Parameters parameters, Types.OptionType optionType, Types.MethodType methodType) 
        {
            var pVArray = default(double[,]);

            switch (optionType)
            {
                case Types.OptionType.Vanilla:
                    pVArray =
                        OptionVanilla.SetInitialCondition(
                            new double[parameters.TNum, parameters.XNum[0]],
                            parameters.BoundaryPrice,
                            parameters.Strike,
                            parameters.IsCall);

                    pVArray =
                        OptionVanilla.SetBoundaryCondition(
                            pVArray,
                            parameters.BoundaryPrice,
                            parameters.Strike,
                            parameters.IsCall);

                    pVArray =
                        MethodTheta.CalculatePVArray(
                            methodType,
                            pVArray,
                            parameters.BoundaryPrice,
                            parameters.Maturity,
                            parameters.DomesticRate,
                            parameters.ForeignRate,
                            parameters.Volatility);
                    break;

                case Types.OptionType.Barrier:
                    pVArray =
                        OptionBarrier.SetInitialCondition(
                            new double[parameters.TNum, parameters.XNum[0]],
                            parameters.BoundaryPrice,
                            parameters.Strike,
                            parameters.Barrier,
                            parameters.IsCall);

                    pVArray =
                        OptionBarrier.SetBoundaryCondition(pVArray);

                    pVArray =
                        MethodTheta.CalculatePVArray(
                            methodType,
                            pVArray,
                            parameters.BoundaryPrice,
                            parameters.Maturity,
                            parameters.DomesticRate,
                            parameters.ForeignRate,
                            parameters.Volatility);
                    break;
            }
            return pVArray;
        }
    }
}
