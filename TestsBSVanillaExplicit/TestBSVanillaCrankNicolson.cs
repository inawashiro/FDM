using Xunit;

namespace FDM
{
    public class TestBSVanillaCrankNicolson
    {
        [Fact]
        public void BSVanillaCrankNicolsonTest()
        {
            var optionType = Types.OptionType.Vanilla;
            var methodType = Types.MethodType.CrankNicolson;

            TestError.Test(optionType, methodType);
        }
    }
}