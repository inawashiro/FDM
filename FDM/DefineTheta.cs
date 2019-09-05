using System;
namespace FDM
{
    public class DefineTheta
    {
        public static double Define(Types.MethodType methodType)
        {
            double theta = 0;

            switch (methodType)
            {
                case Types.MethodType.Explicit:
                    theta = 0;
                    break;

                case Types.MethodType.Implicit:
                    theta = 1;
                    break;

                case Types.MethodType.CrankNicolson:
                    theta = 0.5;
                    break;
            }
            return theta;
        }
    }
}
