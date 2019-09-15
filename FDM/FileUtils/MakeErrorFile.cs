using System.Text;
using System.IO;

namespace FDM
{
    public static class MakeErrorFile
    {
        public static void Complete()
        {
            var file =
                new StreamWriter(
                    @"/Users/hiromichi/Desktop/official/job/advanced/data/BS/Vanilla/Error.csv",
                    false,
                    Encoding.UTF8);
            MergeIndexAndValue(file, Types.OptionType.Vanilla);

            file =
                new StreamWriter(
                    @"/Users/hiromichi/Desktop/official/job/advanced/data/BS/Barrier/Error.csv",
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
            var xNumArray = ParametersFactory.MakeXNumArray();
            int xNumNum = xNumArray.GetLength(0);

            file.Write(",");
            for (int j = 0; j < xNumNum; j++)
            {
                file.Write(xNumArray[j] + ",");
            }
            file.WriteLine();
        }

        private static void WriteError(StreamWriter file, Types.OptionType optionType, Types.MethodType methodType)
        {
            var parameters = ParametersFactory.ForVerification(optionType);
            var xNumArray = ParametersFactory.MakeXNumArray();
            int xNumNum = xNumArray.GetLength(0);

            file.Write(",");
            for (int j = 0; j < xNumNum; j++)
            {
                var makePVArray = new MakePVArray();
                var analyticArray =
                    makePVArray.Analytic(parameters[j], optionType);
                var fDMArray =
                    makePVArray.FDM(parameters[j], optionType, methodType);
                double error = CalculateError.MaxAbsoluteError(fDMArray, analyticArray);

                file.Write(error + ",");
            }
            file.WriteLine();
        }
    }
}