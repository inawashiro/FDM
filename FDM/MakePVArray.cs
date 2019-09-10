namespace FDM
{
    public class MakePVArray
    {
        public double[,] Analytic(Parameters parameters, Types.OptionType optionType)
        {
            var pVArray = new double[parameters.TNum, parameters.XNum];

            switch (optionType)
            {
                case Types.OptionType.Vanilla:
                    pVArray =
                        OptionVanilla.CalculatePVArray(
                            pVArray,
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
                        OptionBarrier.CalculatePVArray(
                            pVArray,
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
            var pVArray =  new double[parameters.TNum, parameters.XNum];

            switch (optionType)
            {
                case Types.OptionType.Vanilla:
                    OptionVanilla.SetInitialCondition(
                        pVArray,
                        parameters.BoundaryPrice,
                        parameters.Strike,
                        parameters.IsCall);

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
                            parameters.Strike,
                            parameters.Maturity,
                            parameters.DomesticRate,
                            parameters.ForeignRate,
                            parameters.Volatility,
                            parameters.IsCall);
                    break;

                case Types.OptionType.Barrier:
                    OptionBarrier.SetInitialCondition(
                        pVArray,
                        parameters.BoundaryPrice,
                        parameters.Strike,
                        parameters.Barrier,
                        parameters.IsCall);

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
                            parameters.Strike,
                            parameters.Maturity,
                            parameters.DomesticRate,
                            parameters.ForeignRate,
                            parameters.Volatility,
                            parameters.IsCall);
                    break;
            }
            return pVArray;
        }
    }
}
