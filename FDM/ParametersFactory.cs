using System;

namespace FDM
{
    public static class ParametersFactory
    {
        public static Parameters Original(Types.OptionType optionType)
        {
            var parameters = default(Parameters);

            switch (optionType)
            {
                case Types.OptionType.Vanilla:
                    parameters =
                        new Parameters(
                            10,                             // tNum
                            new int[] { 400 },              // xNum
                            0.4,                            // maturity
                            new double[] { Math.Log(200) }, // boundaryPrice
                            100,                            // strike
                            0,                              // domesticRate
                            new double[] { 2e-2 },          // foreignRate
                            new double[] { 0.2 },           // volatility
                            true                            // isCall
                            );
                    break;

                case Types.OptionType.Barrier:
                    parameters =
                        new Parameters(
                            10,                             // tNum
                            new int[] { 400 },              // xNum
                            0.4,                            // maturity
                            new double[] { Math.Log(200) }, // boundaryPrice
                            100,                            // strike
                            0,                              // domesticRate
                            new double[] { 2e-2 },          // foreignRate
                            new double[] { 0.2 },           // volatility
                            110,                            // barrier
                            true                            // isCall
                            );
                    break;
            }
            return parameters;
        }

        public static double[] MakeXArray(Parameters parameters)
        {
            var xNum = parameters.XNum;
            var boundaryPrice = parameters.BoundaryPrice;
            var dx = new double[] { boundaryPrice[0] / xNum[0] };
            var xArray = new double[xNum[0]];

            for (int j = 0; j < xNum[0]; j++)
            {
                xArray[j] = Math.Exp(j * dx[0]);
            }
            return xArray;
        }

        public static int[] MakeXNumArray()
        {
            int xNumNum = 7;
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
                switch (optionType)
                {
                    case Types.OptionType.Vanilla:
                        parametersArray[j] =
                            new Parameters(
                                parameters.TNum,
                                new int[] { xNumArray[j] },
                                parameters.Maturity,
                                parameters.BoundaryPrice,
                                parameters.Strike,
                                parameters.DomesticRate,
                                parameters.ForeignRate,
                                parameters.Volatility,
                                parameters.IsCall);
                        break;

                    case Types.OptionType.Barrier:
                        parametersArray[j] =
                            new Parameters(
                                parameters.TNum,
                                new int[] { xNumArray[j] },
                                parameters.Maturity,
                                parameters.BoundaryPrice,
                                parameters.Strike,
                                parameters.DomesticRate,
                                parameters.ForeignRate,
                                parameters.Volatility,
                                parameters.Barrier,
                                parameters.IsCall);
                        break;

                    case Types.OptionType.Exchange:
                        parametersArray[j] =
                            new Parameters(
                                parameters.TNum,
                                new int[] { xNumArray[j] },
                                parameters.Maturity,
                                parameters.BoundaryPrice,
                                parameters.DomesticRate,
                                parameters.ForeignRate,
                                parameters.Volatility,
                                parameters.Correlation);
                        break;
                }
            }
            return parametersArray;
        }
    }
}