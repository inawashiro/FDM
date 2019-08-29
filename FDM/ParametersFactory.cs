using System;

namespace FDM
{
    public static class ParametersFactory
    {
        public static Parameters DefaultParameters()
        {
            //tNum,
            //xNum,
            //maturity,
            //boundaryPrice,
            //strike,
            //domesticRate,
            //foreignRate,
            //volatility

            var parameters = new Parameters(20, 400, 0.1, 400, 100, 0, 2e-2, 0.1);

            return parameters;
        }
    }
}