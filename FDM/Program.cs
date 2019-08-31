using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FDM
{
    class Program
    {
        private static readonly Parameters parameters = ParametersFactory.DefaultParameters(Types.OptionType.Barrier);

        static void Main(string[] args)
        {
            double barrier = parameters.Strike * 1.1;
            bool isCall = true;

            var tIndex = new double[parameters.TNum];
            var xIndex = new double[parameters.XNum];

            for (int l = 0; l < parameters.TNum; l++)
            {
                tIndex[l] = l * parameters.Maturity / parameters.TNum;
            }
            for (int i = 0; i < parameters.XNum; i++)
            {
                xIndex[i] = i * parameters.BoundaryPrice / parameters.XNum;
            }

            var fileExplicit =
                new StreamWriter(
                    @"/Users/hiromichi/Desktop/official/job/advanced/data/BS/Barrier/Explicit.csv",
                    false,
                    Encoding.UTF8);

            var bSBarrierExplicitPV =
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

            CSVWriter.Write2D(fileExplicit, bSBarrierExplicitPV, tIndex, xIndex);


            var fileImplicit =
                new StreamWriter(
                    @"/Users/hiromichi/Desktop/official/job/advanced/data/BS/Barrier/Implicit.csv",
                    false,
                    Encoding.UTF8);

            var bSBarrierImplicitPV =
                BSBarrierImplicit.CalculatePVArray(
                    new double[parameters.TNum, parameters.XNum],
                    parameters.BoundaryPrice,
                    parameters.Strike,
                    parameters.Maturity,
                    parameters.DomesticRate,
                    parameters.ForeignRate,
                    parameters.Volatility,
                    barrier,
                    isCall);

            CSVWriter.Write2D(fileImplicit, bSBarrierImplicitPV, tIndex, xIndex);


            var fileAnalytic =
                new StreamWriter(
                    @"/Users/hiromichi/Desktop/official/job/advanced/data/BS/Barrier/Analytic.csv",
                    false,
                    Encoding.UTF8);

            var bSBarrierAnalyticPV =
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

            CSVWriter.Write2D(fileAnalytic, bSBarrierAnalyticPV, tIndex, xIndex);
        }
    }
}