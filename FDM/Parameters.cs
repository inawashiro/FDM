using System;

namespace FDM
{
    public class Parameters
    {
        // For vanilla option
        public Parameters(
            int tNum,
            int xNum,
            double maturity,
            double boundaryPrice,
            double strike,
            double domesticRate,
            double foreignRate,
            double volatility,
            bool isCall)
        {
            TNum = tNum;
            XNum = xNum;
            Maturity = maturity;
            BoundaryPrice = boundaryPrice;
            Strike = strike;
            DomesticRate = domesticRate;
            ForeignRate = foreignRate;
            Volatility = volatility;
            IsCall = isCall;
        }

        // For barrier option
        public Parameters(
            int tNum,
            int xNum,
            double maturity,
            double boundaryPrice,
            double strike,
            double domesticRate,
            double foreignRate,
            double volatility,
            double barrier,
            bool isCall)
        {
            TNum = tNum;
            XNum = xNum;
            Maturity = maturity;
            BoundaryPrice = boundaryPrice;
            Strike = strike;
            DomesticRate = domesticRate;
            ForeignRate = foreignRate;
            Volatility = volatility;
            Barrier = barrier;
            IsCall = isCall;
        }



        public int TNum { set; get; }
        public int XNum { set; get; }
        public double Maturity { set; get; }
        public double BoundaryPrice { set; get; }
        public double Strike { set; get; }
        public double DomesticRate { set; get; }
        public double ForeignRate { set; get; }
        public double Volatility { set; get; }
        public double Barrier { set; get; }
        public bool IsCall { set; get; }
    }
}
