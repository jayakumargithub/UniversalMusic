using System;
using System.Collections.Generic;

namespace RecklassRekkids.Process
{
    public interface IPartnerContractService
    {
       
    Dictionary<string, string> ProcessContract(List<string> reader);
    }

    public  class PartnerContractService : IPartnerContractService
    {
       

        public Dictionary<string, string> ProcessContract(List<string> reader)
        {
            Dictionary<string, string> partnerContract = new Dictionary<string, string>();
            foreach(var s in reader)
            {
                var splitValue = s.Split(new char[] { '|' });
                if (splitValue[0].ToString() == "Partner") //ignore the first line of text file
                    continue;
                partnerContract.Add(splitValue[0].ToLower(), splitValue[1].ToLower());
            }
           return partnerContract;
        }
    }
}
