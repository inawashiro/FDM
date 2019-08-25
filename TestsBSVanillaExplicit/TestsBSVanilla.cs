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
    public class TestsBSVanilla
    {
        private static readonly int tNum = Parameters.TNum;
        private static readonly int xNum = Parameters.XNum;
        private static readonly double boundaryPV = Parameters.BoundaryPV;
        private static readonly double strike = Parameters.Strike;
        private static readonly double boundaryTime = Parameters.BoundaryTime;
        private static readonly double domesticRate = Parameters.DomesticRate;
        private static readonly double foreignRate = Parameters.ForeignRate;
        private static readonly double volatility = Parameters.Volatility;

        [Fact]
        public void BSVanillaTest()
        {
            var isCall = true;
            IsLowerThanTolerance(isCall);

            isCall = false;
            IsLowerThanTolerance(isCall);
        }

        public void IsLowerThanTolerance(bool isCall)
        {
            double tol = 1e-2;

            var pVAnalytic =
                        BSVanillaAnalytic.Make2DArray(
                            new double[tNum, xNum],
                            boundaryPV,
                            strike,
                            boundaryTime,
                            domesticRate,
                            foreignRate,
                            volatility,
                            isCall);

            var pVFDM =
                BSVanillaExplicit.CalculatePVArray(
                    new double[tNum, xNum],
                    boundaryPV,
                    strike,
                    boundaryTime,
                    domesticRate,
                    foreignRate,
                    volatility,
                    isCall);

            double error = CalculateError.ModifiedMeanRelative(pVFDM, pVAnalytic);
            Assert.True(error < tol);
        }
    }
}