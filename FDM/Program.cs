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
        private static readonly int tNum = 20;
        private static readonly int xNum = 20;
        private static readonly double boundaryPV = 200;
        private static readonly double strike = 100;
        private static readonly double boundaryTime = 0.1;
        private static readonly double domesticRate = 0.1;
        private static readonly double foreignRate = 0.0;
        private static readonly double volatility = 0.3;
        private static readonly bool isCall = true;

        static void Main(string[] args)
        {
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
                    @"/Users/hiromichi/Desktop/official/job/advanced/data/BSVanillaExplicit.csv",
                    false,
                    Encoding.UTF8);

            var bSVanillaExplicitPV =
                BSVanillaExplicit.CalculatePVArray(
                    tNum,
                    xNum,
                    boundaryPV,
                    strike,
                    boundaryTime,
                    domesticRate,
                    foreignRate,
                    volatility,
                    isCall);

            FileUtils.CSVWriter(fileExplicit, bSVanillaExplicitPV, tIndex, xIndex);


            var fileAnalytic =
                new StreamWriter(
                    @"/Users/hiromichi/Desktop/official/job/advanced/data/BSVanillaAnalytic.csv",
                    false,
                    Encoding.UTF8);

            var bSVanillaAnalyticPV =
                BSVanillaAnalytic.Make2DArray(
                    tNum,
                    xNum,
                    boundaryPV,
                    strike,
                    boundaryTime,
                    domesticRate,
                    foreignRate,
                    volatility,
                    isCall);

            FileUtils.CSVWriter(fileAnalytic, bSVanillaAnalyticPV, tIndex, xIndex);
        }
    }
}