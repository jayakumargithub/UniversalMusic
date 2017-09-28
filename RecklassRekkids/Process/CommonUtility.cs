using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecklassRekkids.Process
{
    public static class CommonUtility
    {
        public static string RemoveDaySuffix(string date)
        {
            string dateout = string.Empty;
            var splitDate = date.Split(' ');

            if (splitDate[0].Contains("st") || splitDate[0].Contains("nd") || splitDate[0].Contains("rd") || splitDate[0].Contains("th"))
            {
                dateout = splitDate[0].Length == 3 ? splitDate[0].Remove(1, 2) : splitDate[0].Remove(2, 2);
            }
            return dateout + ' ' + splitDate[1] + ' ' + splitDate[2];
        }
    }
}
