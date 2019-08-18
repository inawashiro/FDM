using System;
using Xunit;
using FDM;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;

namespace FDM.Tests
{
    public class BSVanillaPVTests
    {
        private static readonly int tNum = 10;
        private static readonly int xNum = 10;
        private static readonly double boundaryPV = 200;
        private static readonly double strike = 100;
        private static readonly double boundaryTime = 0.2;
        private static readonly double domesticRate = 0.1;
        private static readonly double foreignRate = 0.0;
        private static readonly double volatility = 0.3;

        [Fact]
        public void BSVanillaTest()
        {
            double tol = 1e-3;
            var isCall = true;

            double squareAbsoluteErrorSum = 0;
            double squareAnalyticSum = 0;
            double relativeErrorTotal = 0;

            for (int l = 1; l < tNum; l++)
            {
                double maturity = l * boundaryTime / tNum;

                for (int i = 1; i < xNum - 1; i++)
                {
                    double initialPV = i * boundaryPV / xNum;

                    var pVAnalytic =
                        BSVanillaAnalytic.Make2DArray(
                            xNum,
                            tNum,
                            boundaryPV,
                            strike,
                            boundaryTime,
                            domesticRate,
                            foreignRate,
                            volatility,
                            isCall);

                    var pVFDM =
                        BSVanillaExplicit.CalculatePVArray(
                            xNum,
                            tNum,
                            boundaryPV,
                            strike,
                            boundaryTime,
                            domesticRate,
                            foreignRate,
                            volatility,
                            isCall);

                    squareAbsoluteErrorSum += (pVAnalytic[l, i] - pVFDM[l, i]) * (pVAnalytic[l, i] - pVFDM[l, i]);
                    squareAnalyticSum += pVAnalytic[l, i] * pVAnalytic[l, i];
                }
            }
            relativeErrorTotal = squareAbsoluteErrorSum / squareAnalyticSum;
            Assert.True(relativeErrorTotal < tol ? true : false);
        }
    }
}