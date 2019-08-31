using Xunit;
using FDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;

namespace FDM
{
    public class TestsBSVanillaImplicit
    {
        private static readonly Parameters parameters = ParametersFactory.DefaultParameters(Types.OptionType.Vanilla);

        [Fact]
        public void BSVanillaImplicitTest()
        {
            var isCall = true;
            IsLowerThanTolerance(isCall);

            isCall = false;
            IsLowerThanTolerance(isCall);
        }

        public void IsLowerThanTolerance(bool isCall)
        {
            double tol = 1e-1;

            var pVAnalytic =
                        BSVanillaAnalytic.Make2DArray(
                            new double[parameters.TNum, parameters.XNum],
                            parameters.BoundaryPrice,
                            parameters.Strike,
                            parameters.Maturity,
                            parameters.DomesticRate,
                            parameters.ForeignRate,
                            parameters.Volatility,
                            isCall);

            var pVFDM =
                BSVanillaImplicit.CalculatePVArray(
                    new double[parameters.TNum, parameters.XNum],
                    parameters.BoundaryPrice,
                    parameters.Strike,
                    parameters.Maturity,
                    parameters.DomesticRate,
                    parameters.ForeignRate,
                    parameters.Volatility,
                    isCall);

            double error = CalculateError.MaxAbsoluteError(pVFDM, pVAnalytic);
            Assert.True(error < tol);
        }
    }
}