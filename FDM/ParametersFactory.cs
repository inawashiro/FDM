using System;

namespace FDM
{
    public static class ParametersFactory
    {
        public static Parameters Original(Types.OptionType optionType)
        {
            // tNum,
            // xNum,
            // maturity,
            // boundaryPrice,
            // strike,
            // domesticRate,
            // foreignRate,
            // volatility
            // barrier
            // call/put

            var parameters =
                optionType == Types.OptionType.Vanilla ?
                new Parameters(10, 400, 0.2, Math.Log(200), 100, 0, 2e-2, 0.1, true) :
                new Parameters(10, 400, 0.1, Math.Log(200), 100, 0, 2e-2, 0.1, 110, true);

            return parameters;
        }

        public static int[] MakeXNumArray()
        {
            int xNumNum = 5;
            var xNumArray = new int[xNumNum];
            for (int j = 0; j < xNumNum; j++)
            {
                xNumArray[j] = 25 * (int)Math.Pow(2, j);
            }
            return xNumArray;
        }

        public static Parameters[] ForVerification(Types.OptionType optionType)
        {
            var xNumArray = MakeXNumArray();
            int xNumNum = xNumArray.GetLength(0);
            var parameters = Original(optionType);
            var parametersArray = new Parameters[xNumNum];

            for (int j = 0; j < xNumNum; j++)
            {
                parametersArray[j] =
                    optionType == Types.OptionType.Vanilla ?
                        new Parameters(
                            parameters.TNum,
                            xNumArray[j],
                            parameters.Maturity,
                            parameters.BoundaryPrice,
                            parameters.Strike,
                            parameters.DomesticRate,
                            parameters.ForeignRate,
                            parameters.Volatility,
                            parameters.IsCall) :
                        new Parameters(
                            parameters.TNum,
                            xNumArray[j],
                            parameters.Maturity,
                            parameters.BoundaryPrice,
                            parameters.Strike,
                            parameters.DomesticRate,
                            parameters.ForeignRate,
                            parameters.Volatility,
                            parameters.Barrier,
                            parameters.IsCall);
            }
            return parametersArray;
        }
    }
}