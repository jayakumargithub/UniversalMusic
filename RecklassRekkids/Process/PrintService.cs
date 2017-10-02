using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecklassRekkids.Process
{
    public interface IPrintService
    {
        void Print(List<MusicContracts> musicContracts, out string output);
    }

    public class PrintService : IPrintService
    {
       public void Print(List<MusicContracts> musicContracts,out string output)
       {
           StringBuilder data = new StringBuilder();
            if (musicContracts.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("----Here are  the results-----");
                Console.WriteLine();

                foreach (var a in musicContracts)
                {
                     data.Append( a.Artist + "|" + a.Title + "|" + a.Usage + "|" +
                               CommonUtility.GetSuffix(a.StartDate.Date) + "|" +
                               CommonUtility.GetSuffix(a.EndDate?.Date));
                    data.Append(System.Environment.NewLine);
                }
                Console.WriteLine(data.ToString());
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                data.Append("No Results found!.");
                Console.WriteLine(data.ToString());
            }

           output = data.ToString();
       }
    }
}
