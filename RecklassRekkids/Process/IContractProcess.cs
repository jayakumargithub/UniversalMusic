using System.Collections.Generic;

namespace RecklassRekkids.Process
{
    public interface IContractProcess
    {
        void ProcessContract();
       object ProcessedContract { get; set; }
        
    }

    public interface IParterContractProcess
    {
        void ProcessContract();
    }
}
