using Xunit;

namespace FDM
{
    public class TestBSVanillaImplicit
    {
        [Fact]
        public void BSVanillaImplicitTest()
        {
            Parameters parameters = ParametersFactory.ForUnitTests(Types.OptionType.Vanilla);

            double tol = 1e-3;
            var makePVArray = new MakePVArray();
            var analyticArray = makePVArray.Make(parameters, Types.OptionType.Vanilla, Types.MethodType.Analytic);
            var fDMArray = makePVArray.Make(parameters, Types.OptionType.Vanilla, Types.MethodType.Implicit);
            double error = CalculateError.MaxAbsoluteError(fDMArray, analyticArray);

            Assert.Equal(error, tol);
        }
    }
}