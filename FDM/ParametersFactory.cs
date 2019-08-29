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
                optionType == Types.OptionType.Vanilla ?
                new Parameters(20, 100, 1.0, 200, 100, 0, 2e-2, 0.1) :
                new Parameters(20, 200, 1.0, 200, 100, 0, 2e-2, 0.1);

            return parameters;
        }
    }
}