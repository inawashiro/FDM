using Xunit;

namespace FDM
{
    public class TestBSBarrierImplicit
    {
        [Fact]
        public void BSBarrierImplicitTest()
        {
            var optionType = Types.OptionType.Barrier;
            var methodType = Types.MethodType.Implicit;

            TestError.Test(optionType, methodType);
        }
    }
}