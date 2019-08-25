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
        private static readonly int tNum = Parameters.TNum;
        private static readonly int xNum = Parameters.XNum;
        private static readonly double boundaryPV = Parameters.BoundaryPV;
        private static readonly double strike = Parameters.Strike;
        private static readonly double boundaryTime = Parameters.BoundaryTime;
        private static readonly double domesticRate = Parameters.DomesticRate;
        private static readonly double foreignRate = Parameters.ForeignRate;
        private static readonly double volatility = Parameters.Volatility;

        static void Main(string[] args)
        {
            double barrier = strike + 50;
            bool isCall = true;

            var tIndex = new double[tNum];
            var xIndex = new double[xNum];

            for (int l = 0; l < tNum; l++)
            {
                tIndex[l] = l * boundaryTime / tNum;
            }
            for (int i = 0; i < xNum; i++)
            {
                xIndex[i] = i * boundaryPV / xNum;
            }

            var fileExplicit =
                new StreamWriter(
                    @"S:\GR6795\GR6795_41002\90_個人\行員\猪苗代\training\advanced\data\BSBarrier\Explicit.csv",
                    false,
                    Encoding.UTF8);

            var pVArray = new double[tNum, xNum];

            var bSBarrierExplicitPV =
                BSBarrierExplicit.CalculatePVArray(
                    pVArray,
                    boundaryPV,
                    strike,
                    barrier,
                    boundaryTime,
                    domesticRate,
                    foreignRate,
                    volatility,
                    isCall);

            CSVWriter.Write2D(fileExplicit, bSBarrierExplicitPV, tIndex, xIndex);


            var fileAnalytic =
                new StreamWriter(
                    @"S:\GR6795\GR6795_41002\90_個人\行員\猪苗代\training\advanced\data\BSBarrier\Analytic.csv",
                    false,
                    Encoding.UTF8);

            pVArray = new double[tNum, xNum];

            var bSBarrierAnalyticPV =
                BSBarrierAnalytic.Make2DArray(
                    pVArray,
                    boundaryPV,
                    strike,
                    barrier,
                    boundaryTime,
                    domesticRate,
                    foreignRate,
                    volatility,
                    isCall);

            CSVWriter.Write2D(fileAnalytic, bSBarrierAnalyticPV, tIndex, xIndex);
        }
    }
}