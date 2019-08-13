using System;
namespace FDM
{
    public interface IOptionValueCalculator
    {
        double[,] CalculatePV(int spatialDivisionNum,
                              int temporalDivisionNum,
                              double boundaryOptionValue,
                              double strike,
                              double maturity,
                              double domesticRate,
                              double foreignRate,
                              double volatility,
                              bool isCall);
    }
}
