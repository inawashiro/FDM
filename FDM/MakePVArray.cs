namespace FDM
{
    public class MakePVArray
    {
        public double[,] Make(Parameters parameters, Types.OptionType optionType, Types.MethodType methodType) 
        {
            var pVArray = new double[parameters.TNum, parameters.XNum];

            if (optionType == Types.OptionType.Vanilla)
            {
                NewOptionVanilla.SetInitialCondition(
                    pVArray,
                    parameters.BoundaryPrice,
                    parameters.Strike,
                    parameters.IsCall);

                NewOptionVanilla.SetBoundaryCondition(
                    pVArray,
                    parameters.BoundaryPrice,
                    parameters.Strike,
                    parameters.IsCall);
            }
            else
            {
                NewOptionBarrier.SetInitialCondition(
                    pVArray,
                    parameters.BoundaryPrice,
                    parameters.Strike,
                    parameters.Barrier,
                    parameters.IsCall);

                NewOptionBarrier.SetBoundaryCondition(
                    pVArray,
                    parameters.BoundaryPrice,
                    parameters.Strike,
                    parameters.IsCall);
            }

            if (methodType == Types.MethodType.Analytic)
            {
                pVArray =
                    NewMethodAnalytic.CalculatePVArray(
                        optionType,
                        pVArray,
                        parameters.BoundaryPrice,
                        parameters.Strike,
                        parameters.Maturity,
                        parameters.DomesticRate,
                        parameters.ForeignRate,
                        parameters.Volatility,
                        parameters.Barrier,
                        parameters.IsCall);
            }
            else
            {
                pVArray =
                    NewMethodTheta.CalculatePVArray(
                        methodType,
                        pVArray,
                        parameters.BoundaryPrice,
                        parameters.Strike,
                        parameters.Maturity,
                        parameters.DomesticRate,
                        parameters.ForeignRate,
                        parameters.Volatility,
                        parameters.IsCall);
            }
            return pVArray;
        }
    }
}
