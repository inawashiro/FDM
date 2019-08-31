﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace FDM
{
    public static class BSVanillaImplicit
    {
        private static void SetInitialCondition(
            double[,] pVArray,
            double boundaryPrice,
            double strike,
            bool isCall)
        {
            int xNum = pVArray.GetLength(1);

            for (int i = 0; i < xNum; i++)
            {
                double initialPV = i * boundaryPrice / xNum;

                int sign = isCall ? 1 : -1;

                pVArray[0, i] = Math.Max(sign * (initialPV - strike), 0);
            }
        }

        private static void SetBoundaryCondition(
            double[,] pVArray,
            double boundaryPrice,
            double strike,
            bool isCall)
        {
            int tNum = pVArray.GetLength(0);
            int xNum = pVArray.GetLength(1);

            for (int l = 1; l < tNum; l++)
            {
                pVArray[l, 0] = isCall ? 0 : strike;
                pVArray[l, xNum - 1] =
                    isCall ? Math.Max(((double)xNum - 1) / (double)xNum * boundaryPrice - strike, 0) : 0;
            }
        }

        public static double[,] CalculateCoefficientArray(
            double[,] pVArray,
            double boundaryPrice,
            double strike,
            double maturity,
            double domesticRate,
            double foreignRate,
            double volatility,
            bool isCall)
        {
            int tNum = pVArray.GetLength(0);
            int xNum = pVArray.GetLength(1);
            double dx = boundaryPrice / xNum;
            double dt = maturity / tNum;
            var coefficientArray = new double[xNum, xNum];

            coefficientArray[0, 0] = 1;
            coefficientArray[0, 1] = 0;
            for (int i = 1; i < xNum - 1; i++)
            {
                double initialPV = i * boundaryPrice / xNum;

                double a1 = (domesticRate - foreignRate) * initialPV;
                double b11 = 0.5 * volatility * volatility * initialPV * initialPV;
                double f = -domesticRate;

                double a = -b11 * dt / (dx * dx) + 0.5 * a1 * dt / dx;
                double b = 1 + f * dt + 2 * b11 * dt / (dx * dx);
                double c = -b11 * dt / (dx * dx) - 0.5 * a1 * dt / dx;

                coefficientArray[i, i - 1] = a;
                coefficientArray[i, i] = b;
                coefficientArray[i, i + 1] = c;
            }
            coefficientArray[xNum - 1, xNum - 2] = 0;
            coefficientArray[xNum - 1, xNum - 1] = 1;

            return coefficientArray;
        }

        public static double[,] CalculatePVArray(
            double[,] pVArray,
            double boundaryPrice,
            double strike,
            double maturity,
            double domesticRate,
            double foreignRate,
            double volatility,
            bool isCall)
        {
            int tNum = pVArray.GetLength(0);
            int xNum = pVArray.GetLength(1);

            SetInitialCondition(pVArray, boundaryPrice, strike, isCall);
            SetBoundaryCondition(pVArray, boundaryPrice, strike, isCall);

            var coefficientMatrix =
                Matrix<double>.Build.DenseOfArray(
                    CalculateCoefficientArray(
                        pVArray,
                        boundaryPrice,
                        strike,
                        maturity,
                        domesticRate,
                        foreignRate,
                        volatility,
                        isCall));
            var vector =
                Vector<double>.Build.Dense(xNum, i => pVArray[0, i]);
            for (int l = 1; l < tNum; l++)
            {
                vector = coefficientMatrix.Solve(vector);
                for (int i = 0; i < xNum; i++)
                {
                    pVArray[l, i] = vector[i];
                }
            }
            return pVArray;
        }
    }
}