using Xunit;

namespace FDM
{
    public class TestBSVanillaExplicit
    {
        [Fact]
        public void BSVanillaExplicitTest()
        {
            var optionType = Types.OptionType.Vanilla;
            var methodType = Types.MethodType.Explicit;

            TestError.Test(optionType, methodType);
        }
    }
}