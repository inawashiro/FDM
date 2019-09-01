using System;

namespace FDM
{
    public static class ParametersFactory
    {
        public static Parameters DefaultParameters(Types.OptionType optionType)
        {
            // tNum,
            // xNum,
            // maturity,
            // boundaryPrice,
            // strike,
            // domesticRate,
            // foreignRate,
            // volatility

            var parameters =
                //optionType == Types.OptionType.Vanilla ?
                //new Parameters(10, 400, 0.1, 200, 100, 0, 2e-2, 0.1) :
                new Parameters(10, 800, 0.1, 200, 100, 0, 2e-2, 0.1);

            return parameters;
        }
    }
}