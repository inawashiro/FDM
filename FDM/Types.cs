using System;

namespace FDM
{
    public static class Types
    {
        public enum OptionType
        {
            Vanilla,
            Barrier,
            Exchange
        }

        public enum MethodType
        {
            Explicit,
            Implicit,
            CrankNicolson,
            ADI
        }

        public enum DifferentialDirection
        {
            X,
            Y
        }
    }
}
