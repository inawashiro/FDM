using Xunit;

namespace FDM
{
    public class TestBSBarrierImplicit
    {
        [Fact]
        public void BSBarrierImplicitTest()
        {
            Parameters parameters = ParametersFactory.ForUnitTests(Types.OptionType.Barrier);

            double tol = 1e-3;
            var makePVArray = new MakePVArray();
            var analyticArray = makePVArray.Make(parameters, Types.OptionType.Barrier, Types.MethodType.Analytic);
            var fDMArray = makePVArray.Make(parameters, Types.OptionType.Barrier, Types.MethodType.Implicit);
            double error = CalculateError.MaxAbsoluteError(fDMArray, analyticArray);

            Assert.Equal(error, tol);
        }
    }
}