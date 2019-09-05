using System;

namespace FDM
{
    public static class Types
    {
        public enum OptionType
        {
            Vanilla,
            Barrier
        }

        public enum MethodType
        {
            Analytic,
            Explicit,
            Implicit,
            CrankNicolson
        }
    }
}
