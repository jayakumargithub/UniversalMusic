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

        public static string GetSuffix(DateTime dt)
        {
            string output; 
            if (dt.Day % 10 == 1)
            {
                output = dt.Day + "st "; 
            }
            else if (dt.Day % 10 == 2)
            {
                output =  dt.Day + "nd "; 
            }
            else if (dt.Day % 10 == 3)
            {
                output =  dt.Day + "rd "; 
            }
            else
            {
                output =  dt.Day + "th "; 
            }
            var dateOutput = output + dt.Date.ToString("MMM yyyy");
            return dateOutput;
        }

        public static string GetSuffix(DateTime? dt)
        {
            if (dt.HasValue)
                return GetSuffix(dt.Value);
            else return string.Empty;
        }
    }
}
