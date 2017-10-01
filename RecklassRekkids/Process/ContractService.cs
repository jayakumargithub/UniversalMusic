using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecklassRekkids.Process
{
   public  class ContractService
    {
       public List<MusicContracts> Process(List<MusicContracts> musicContract, string usage, DateTime startDate)
       {
            var partnerMusic = (from a in musicContract
                                where a.Usages.Contains(usage)
                                      && a.StartDate <= startDate
                                select a).OrderBy(x => x.Artist).ToList();
           return partnerMusic;
       }
    }
}
