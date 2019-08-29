﻿using Xunit;
using FDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;

namespace FDM
{
    public class TestsBSBarrierExplicit
    {
        private static readonly Parameters parameters = ParametersFactory.DefaultParameters();
        
        [Fact]
        public void BSBarrierExplicitTest()
        {
            var isCall = true;
            double barrier = parameters.Strike * 1.1;
            IsLowerThanTolerance(isCall, barrier);

            isCall = false;
            barrier = parameters.Strike * 0.9;
            IsLowerThanTolerance(isCall, barrier);
        }

        public void IsLowerThanTolerance(bool isCall, double barrier)
        {
            double tol = 1e-1;

            var pVAnalytic =
                        BSBarrierAnalytic.Make2DArray(
                            new double[parameters.TNum, parameters.XNum],
                            parameters.BoundaryPrice,
                            parameters.Strike,
                            parameters.Maturity,
                            parameters.DomesticRate,
                            parameters.ForeignRate,
                            parameters.Volatility,
                            barrier,
                            isCall);

            var pVFDM =
                BSBarrierExplicit.CalculatePVArray(
                    new double[parameters.TNum, parameters.XNum],
                    parameters.BoundaryPrice,
                    parameters.Strike,
                    parameters.Maturity,
                    parameters.DomesticRate,
                    parameters.ForeignRate,
                    parameters.Volatility,
                    barrier,
                    isCall);

            double error = CalculateError.MaxAbsoluteError(pVFDM, pVAnalytic);
            Assert.True(error < tol);
        }
    }
}