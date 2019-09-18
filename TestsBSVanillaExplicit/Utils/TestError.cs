using Xunit;

namespace FDM
{
    public class TestError
    {
        public static void Test(Types.OptionType optionType, Types.MethodType methodType)
        {
            double tol = 1e-3;

            var parameters = ParametersFactory.Original(optionType);
            var makePVArray = new MakePVArray();
            var analyticArray = makePVArray.AnalyticOneAsset(parameters, optionType);
            var fDMArray = makePVArray.FDMOneAsset(parameters, optionType, methodType);
            double error = CalculateError.MaxAbsolute(fDMArray, analyticArray);

            Assert.Equal(error, tol);
        }
    }
}