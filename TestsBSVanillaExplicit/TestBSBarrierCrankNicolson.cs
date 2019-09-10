using Xunit;

namespace FDM
{
    public class TestBSBarrierCrankNicolson
    {
        [Fact]
        public void BSBarrierCrankNicolsonTest()
        {
            var optionType = Types.OptionType.Barrier;
            var methodType = Types.MethodType.CrankNicolson;

            TestError.Test(optionType, methodType);
        }
    }
}