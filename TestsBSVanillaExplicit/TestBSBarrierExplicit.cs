using Xunit;

namespace FDM
{
    public class TestBSBarrierExplicit
    {
        [Fact]
        public void BSBarrierExplicitTest()
        {
            var optionType = Types.OptionType.Barrier;
            var methodType = Types.MethodType.Explicit;

            TestError.Test(optionType, methodType);
        }
    }
}