using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FDM
{
    class CSVWriter
    {
        public static void Write2D(StreamWriter file, double[,] data, double[] tIndex, double[] xIndex)
        {
            int tNum = data.GetLength(0);
            int xNum = data.GetLength(1);

            file.Write(",");
            for (int i = 0; i < xNum; i++)
            {
                file.Write(xIndex[i] + ",");
            }
            file.WriteLine();
            for (int l = 0; l < tNum; l++)
            {
                file.Write(tIndex[l] + ",");

                for (int i = 0; i < xNum; i++)
                {
                    file.Write(data[l, i] + ",");
                }
                file.WriteLine();
            }
            file.Close();
        }
    }
}