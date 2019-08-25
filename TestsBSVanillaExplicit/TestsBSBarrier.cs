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
    public class TestsBSBarrier
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
        public void BSBarrierTest()
        {
            var isCall = true;
            double barrier = strike * 1.8;
            IsLowerThanTolerance(isCall, barrier);

            isCall = false;
            barrier = strike * 0.2;
            IsLowerThanTolerance(isCall, barrier);
        }

        public void IsLowerThanTolerance(bool isCall, double barrier)
        {
            double tol = 1e-1;

            var pVAnalytic =
                        BSBarrierAnalytic.Make2DArray(
                            new double[tNum, xNum],
                            boundaryPV,
                            strike,
                            barrier,
                            boundaryTime,
                            domesticRate,
                            foreignRate,
                            volatility,
                            isCall);

            var pVFDM =
                BSBarrierExplicit.CalculatePVArray(
                    new double[tNum, xNum],
                    boundaryPV,
                    strike,
                    barrier,
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