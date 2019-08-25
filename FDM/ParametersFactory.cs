using System;
namespace FDM
{
    public class ParametersFactory : Parameters
    {
        Parameters parameters = new Parameters();
        
        public ParametersFactory()
        {
            int tNum = parameters.TNum;
        }
    }
}
