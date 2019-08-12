using System;
using System.Collections.Generic;

namespace FDM
{
    public class BSCalculatorFactory
    {
        private int spatialDivisionNum;
        private int temporaryDivisionNum;
        private double maturity;
        private double maxValue;

        //properties
        public int SpatialDivisionNum
        {
            set { this.spatialDivisionNum = value; }
            get { return this.spatialDivisionNum; }
        }
        public int TemporaryDivisionNum
        {
            set { this.temporaryDivisionNum = value; }
            get { return this.temporaryDivisionNum; }
        }
        public double Maturity
        {
            set { this.maturity = value; }
            get { return this.maturity; }
        }
        public double MaxValue
        {
            set { this.maxValue = value; }
            get { return this.maxValue; }
        }

        //constructor
        public BSCalculatorFactory(int temporaryDivisionNum,
                                   int spatialDivisionNum,
                                   double maturity,
                                   double maxValue)
        {
            this.TemporaryDivisionNum = temporaryDivisionNum;
            this.SpatialDivisionNum = spatialDivisionNum;
            this.Maturity = maturity;
            this.MaxValue = maxValue;
        }
    }
}
