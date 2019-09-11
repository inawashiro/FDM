using System;

namespace FDM
{
    public static class ParametersFactory
    {
        public static Parameters ForUnitTests(Types.OptionType optionType)
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
                new Parameters(10, 200, 0.1, Math.Log(200), 100, 0, 2e-2, 0.1, true) :
                new Parameters(10, 200, 0.1, Math.Log(200), 100, 0, 2e-2, 0.1, 120, true);

            return parameters;
        }

        public static int[] MakeXNumArray()
        {
            int xNumNum = 6;
            var xNumArray = new int[6];
            for (int j = 0; j < xNumNum; j++)
            {
                xNumArray[j] = 16 * (int)Math.Pow(2, j);
            }
            return xNumArray;
        }

        public static Parameters[] ForConvergenceTests(Types.OptionType optionType)
        {
            var xNumArray = MakeXNumArray();
            int xNumNum = xNumArray.GetLength(0);
            var parameters = new Parameters[xNumNum];

            for (int j = 0; j < xNumNum; j++)
            {
                parameters[j] =
                    optionType == Types.OptionType.Vanilla ?
                        new Parameters(10, xNumArray[j], 0.1, 200, 100, 0, 2e-2, 0.1, true) :
                        new Parameters(10, xNumArray[j], 0.1, 200, 100, 0, 2e-2, 0.1, 120, true);

            }
            return parameters;
        }
    }
}