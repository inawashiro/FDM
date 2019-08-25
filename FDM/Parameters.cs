using System;

namespace FDM
{
    public class Parameters
    {
        private int tNum;
        private int xNum;
        private double boundaryPV;
        private double strike;
        private double boundaryTime;
        private double domesticRate;
        private double foreignRate;
        private double volatility;

        public Parameters()
        {
            this.tNum = TNum;
            this.xNum = XNum;
            this.boundaryPV = BoundaryPV;
            this.strike = Strike;
            this.boundaryTime = BoundaryTime;
            this.domesticRate = DomesticRate;
            this.foreignRate = ForeignRate;
            this.volatility = Volatility;
        }
        
        public static int TNum { get { return 20; } }
        public static int XNum { get { return 40; } }
        public static double BoundaryPV { get { return 400; } }
        public static double Strike { get { return 100; } }
        public static double BoundaryTime { get { return 0.2; } }
        public static double DomesticRate { get { return -1.5e-3; } }
        public static double ForeignRate { get { return 2e-2; } }
        public static double Volatility { get { return 0.3; } }
    }
}
