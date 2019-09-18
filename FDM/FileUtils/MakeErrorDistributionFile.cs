using System.Text;
using System.IO;

namespace FDM
{
    public static class MakeErrorDistributionFile
    {
        public static void Complete()
        {
            var file =
                new StreamWriter(
                    @"/Users/hiromichi/Desktop/official/job/advanced/data/Barrier/ErrorDistribution.csv",
                    false,
                    Encoding.UTF8);
            MergeIndexAndValue(file, Types.OptionType.Barrier);
        }

        private static void MergeIndexAndValue(
            StreamWriter file,
            Types.OptionType optionType)
        {
            WriteIndex(file, optionType);
            WriteError(file, optionType, Types.MethodType.Explicit);
            WriteError(file, optionType, Types.MethodType.Implicit);
            WriteError(file, optionType, Types.MethodType.CrankNicolson);
            file.Close();
        }

        private static void WriteIndex(StreamWriter file, Types.OptionType optionType)
        {
            var parameters = ParametersFactory.Original(optionType);
            var xArray = ParametersFactory.MakeXArray(parameters);
            int xNum = xArray.GetLength(0);

            file.Write(",");
            for (int j = 0; j < xNum; j++)
            {
                file.Write(xArray[j] + ",");
            }
            file.WriteLine();
        }

        private static void WriteError(StreamWriter file, Types.OptionType optionType, Types.MethodType methodType)
        {
            var parameters = ParametersFactory.Original(optionType);
            var xArray = ParametersFactory.MakeXArray(parameters);
            int xNum = xArray.GetLength(0);
            var errorArray = new double[xNum];

            file.Write(",");
            for (int j = 0; j < xNum; j++)
            {
                var makePVArray = new MakePVArray();
                var analyticArray =
                    makePVArray.AnalyticOneAsset(parameters, optionType);
                var fDMArray =
                    makePVArray.FDMOneAsset(parameters, optionType, methodType);
                
                errorArray[j] = CalculateError.AbsoluteArray(fDMArray, analyticArray)[j];

                file.Write(errorArray[j] + ",");
            }
            file.WriteLine();
        }
    }
}