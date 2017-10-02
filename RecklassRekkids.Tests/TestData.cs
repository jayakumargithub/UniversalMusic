using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;

namespace RecklassRekkids.Tests
{
    public static class TestData
    {
        
         
        public static List<string> GetMusicContractData()
        {
            List<string> musicContactractsData = new List<string>
            {
                "Artist|Title|Usages|StartDate|EndDate",
                "Tinie Tempah|Frisky (Live from SoHo)|digital download, streaming|1st Feb 2012|",
                "Tinie Tempah|Miami 2 Ibiza|digital download|1st Feb 2012|",
                "Tinie Tempah|Till I'm Gone|digital download|1st Aug 2012|",
                "Monkey Claw|Black Mountain|digital download|1st Feb 2012|",
                "Monkey Claw|Iron Horse|digital download, streaming|1st June 2012|",
                "Monkey Claw|Motor Mouth|digital download, streaming|1st Mar 2011|",
                "Monkey Claw|Christmas Special|streaming|25st Dec 2012|31st Dec 2012|"
            };
            return musicContactractsData;
        }

        public static List<string> GetPartnerContractData()
        {
            List<string> partnerContractsData = new List<string>
            {
                "Partner|Usage",
                "ITunes|digital download",
                "YouTube|streaming"
            };
            return partnerContractsData;
        }


    }
}
