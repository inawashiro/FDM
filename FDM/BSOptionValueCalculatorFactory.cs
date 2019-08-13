using System;
using System.Collections.Generic;

namespace FDM
{
    public class BSOptionValueCalculatorFactory
    {
        private int spatialDivisionNum;
        private int temporalDivisionNum;
        private double maturity;
        private double maxOptionValue;
        private double strike;
        private double interestRate;
        private double volatility;

        //properties
        public int SpatialDivisionNum
        {
            set { this.spatialDivisionNum = value; }
            get { return this.spatialDivisionNum; }
        }
        public int TemporalDivisionNum
        {
            set { this.temporalDivisionNum = value; }
            get { return this.temporalDivisionNum; }
        }
        public double Maturity
        {
            set { this.maturity = value; }
            get { return this.maturity; }
        }
        public double MaxOptionValue
        {
            set { this.maxOptionValue = value; }
            get { return this.maxOptionValue; }
        }
        public double Strike
        {
            set { this.strike = value; }
            get { return this.strike; }
        }
        public double InterestRate
        {
            set { this.interestRate = value; }
            get { return this.interestRate; }
        }
        public double Volatility
        {
            set { this.volatility = value; }
            get { return this.volatility; }
        }

        //constructor
        public BSOptionValueCalculatorFactory(int temporalDivisionNum,
                                              int spatialDivisionNum,
                                              double maturity,
                                              double maxOptionValue,
                                              double strike,
                                              double interestRate,
                                              double volatility)
        {
            this.TemporalDivisionNum = temporalDivisionNum;
            this.SpatialDivisionNum = spatialDivisionNum;
            this.Maturity = maturity;
            this.MaxOptionValue = maxOptionValue;
            this.strike = strike;
            this.interestRate = interestRate;
            this.volatility = volatility;
        }

        public List<List<double>> Test()
        {
            double dx = maxOptionValue / spatialDivisionNum;
            double dt = maturity / temporalDivisionNum;
            var OptionValues = new List<List<double>>();

            for (int l = 0; l < temporalDivisionNum; l++)
            {
                //Boundary Conditions
                OptionValues[0][l - 1] = 0;
                OptionValues[spatialDivisionNum - 1][l - 1] = 0;

                for (int i = 0; i < spatialDivisionNum; i++)
                {
                    double initialOptionValue = maxOptionValue * i / spatialDivisionNum;
                    //Initial Condition
                    OptionValues[i][0] = Math.Max(initialOptionValue - strike, 0);

                    double a1 = interestRate * initialOptionValue;
                    double b11 = 0.5 * volatility * volatility * initialOptionValue * initialOptionValue;

                    double a = b11 * dt / (dx * dx) - 0.5 * a1 * dt / dx;
                    double b = 1 + interestRate * dt - b11 * 2.0 * dt / (dx * dx);
                    double c = b11 * dt / (dx * dx) + 0.5 * a1 * dt / dx;
                    OptionValues[i][l + 1] = a * OptionValues[i - 1][l]
                                             + b * OptionValues[i][l]
                                             + c * OptionValues[i + 1][l];
                }
            }
            return OptionValues;
        }
    }
}
