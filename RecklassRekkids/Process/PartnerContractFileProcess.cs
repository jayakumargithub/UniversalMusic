using System;
using System.Collections.Generic;

namespace RecklassRekkids.Process
{
   public  class PartnerContractFileProcess : IContractProcess
    {
        private readonly List<string> _reader;
        readonly IFormatProvider _culture = new System.Globalization.CultureInfo("en-GB", true);

        public object ProcessedContract { get; set; }

        public PartnerContractFileProcess(List<string> reader)
        {
            ProcessedContract = new Dictionary<string, string>();
            _reader = reader;
            ProcessContract();
        }
        public void ProcessContract()
        {
            Dictionary<string, string> partnerContract = new Dictionary<string, string>();
            foreach(var s in _reader)
            {
                var splitValue = s.Split(new char[] { '|' });
                if (splitValue[0].ToString() == "Partner")
                    continue;
                partnerContract.Add(splitValue[0].ToString(), splitValue[1].ToString());
            }
            ProcessedContract = partnerContract;
        }
    }
}
