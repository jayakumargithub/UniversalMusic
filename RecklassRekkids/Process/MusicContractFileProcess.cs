using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace RecklassRekkids.Process
{
   public  class MusicContractFileProcess : IContractProcess
    {
       private readonly List<string> _reader;
       readonly IFormatProvider _culture = new CultureInfo("en-GB", true);
        public object ProcessedContract { get; set; }

        public MusicContractFileProcess(List<string> reader)
       {
           _reader = reader;
            ProcessContract();
       }

       public void ProcessContract()
        {
            List<MusicContracts> musicContractList = new List<MusicContracts>();

            foreach (var s in _reader)
            {
                var splitValue = s.Split(new char[] { '|' });
                if (splitValue[0].ToString() == "Artist")
                    continue;
                MusicContracts m = new MusicContracts();
                m.Artist = splitValue[0].ToString();
                m.Title = splitValue[1].ToString();
                if (splitValue[2].ToString().Trim().Split(',').Length > 1)
                {
                    m.Usages.AddRange(splitValue[2].ToString().Split(',').ToList());                    
                }
                else
                {
                    m.Usages.Add(splitValue[2].ToString());
                }
                m.StartDate = DateTime.Parse(CommonUtility.RemoveDaySuffix(splitValue[3].ToString()), _culture, DateTimeStyles.AssumeLocal);
                m.EndDate = !string.IsNullOrEmpty(splitValue[4].ToString())
                               ? DateTime.Parse(CommonUtility.RemoveDaySuffix(splitValue[4].ToString()), _culture, DateTimeStyles.AssumeLocal)
                               : DateTime.MaxValue;

                musicContractList.Add(m);
            }


             ProcessedContract =  musicContractList;
        }
    }
}
