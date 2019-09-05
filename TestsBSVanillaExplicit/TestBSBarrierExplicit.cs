using Xunit;

namespace FDM
{
    public class TestBSBarrierExplicit
    {
        [Fact]
        public void BSBarrierExplicitTest()
        {
            Parameters parameters = ParametersFactory.ForUnitTests(Types.OptionType.Barrier);

            double tol = 1e-3;
            var makePVArray = new MakePVArray();
            var analyticArray = makePVArray.Make(parameters, Types.OptionType.Barrier, Types.MethodType.Analytic);
            var fDMArray = makePVArray.Make(parameters, Types.OptionType.Barrier, Types.MethodType.Explicit);
            double error = CalculateError.MaxAbsoluteError(fDMArray, analyticArray);

            Assert.Equal(error, tol);
        }
    }
}