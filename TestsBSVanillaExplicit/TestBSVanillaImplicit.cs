using Xunit;

namespace FDM
{
    public class TestBSVanillaImplicit
    {
        [Fact]
        public void BSVanillaImplicitTest()
        {
            var optionType = Types.OptionType.Vanilla;
            var methodType = Types.MethodType.Implicit;

            TestError.Test(optionType, methodType);
        }
    }
}