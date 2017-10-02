using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace RecklassRekkids.Process
{
    public interface IMusicContractService
    { 
        List<MusicContracts> ProcessContract(List<string> reader);
    }

    public  class MusicContractService : IMusicContractService
    {
       
       readonly IFormatProvider _culture = new CultureInfo("en-GB", true);
         
 
        public List<MusicContracts> ProcessContract(List<string> reader)
        {
            List<MusicContracts> musicContractList = new List<MusicContracts>();

            foreach (var s in reader)
            {
                var splitValue = s.Split('|');
                if (splitValue[0] == "Artist")
                    continue;
                MusicContracts m = new MusicContracts();
                m.Artist = splitValue[0];
                m.Title = splitValue[1];
                if (splitValue[2].Trim().Split(',').Length > 1)
                {
                    m.Usages.AddRange(splitValue[2].Split(',').Select(p => p.Trim()).ToList());                    
                }
                else
                {
                    m.Usages.Add(splitValue[2]);
                }
                m.StartDate = DateTime.Parse(CommonUtility.RemoveDaySuffix(splitValue[3]), _culture, DateTimeStyles.AssumeLocal);
                m.EndDate = !string.IsNullOrEmpty(splitValue[4])
                               ? DateTime.Parse(CommonUtility.RemoveDaySuffix(splitValue[4]), _culture, DateTimeStyles.AssumeLocal)
                               :(DateTime?) null;

                musicContractList.Add(m);
            }


             return musicContractList;
        }
    }
}
