using Xunit;

namespace FDM
{
    public class TestError
    {
        [Fact]
        public static void Test(Types.OptionType optionType, Types.MethodType methodType)
        {
            double tol = 1e-3;

            var parameters = ParametersFactory.ForUnitTests(optionType);
            var makePVArray = new MakePVArray();
            var analyticArray = makePVArray.Analytic(parameters, optionType);
            var fDMArray = makePVArray.FDM(parameters, optionType, methodType);
            double error = CalculateError.MaxAbsoluteError(fDMArray, analyticArray);

            Assert.Equal(error, tol);
        }
    }
}