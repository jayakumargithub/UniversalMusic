using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecklassRekkids
{
    public class MusicContracts
    {
        public MusicContracts()
        {
            Usages = new List<string>();
        }
        public string Artist { get; set; }
        public string Title { get; set; }
        public List<string> Usages { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

    }

}
