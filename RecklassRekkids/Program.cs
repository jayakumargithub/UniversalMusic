using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using RecklassRekkids.Process;

namespace RecklassRekkids
{
    class Program
    {
        static void Main(string[] args)
        {
            string partnerFilePath = string.Empty;

           
           
            bool isMusicContractFileValid = false;
            bool isPartnerContractFileValid = false;
            string musicContractFilePath;
            bool useDefaultFiles = false;

            Console.WriteLine("You want to use default contacts? type 'Y' or 'N' ");
            var userInput = Console.ReadLine();
            if(userInput.ToLower() == "y")
            {
                useDefaultFiles = true;
            }

            if (!useDefaultFiles)
            {
                Console.WriteLine("Please Enter Music Contract Text File Path");
               musicContractFilePath = Console.ReadLine();
                if (!System.IO.File.Exists(musicContractFilePath))
                {
                    Console.WriteLine("Music contract file path is not valid!."); 
                }
                else
                {
                    isMusicContractFileValid = true;
                }
                if (isMusicContractFileValid)
                {
                    Console.WriteLine("Please Enter Partner Contract Text File Path");
                    partnerFilePath = Console.ReadLine();
                    if (!System.IO.File.Exists(partnerFilePath))
                    {
                        Console.WriteLine("Partner contract file path is not valid!");
                    }
                    else
                    {
                        isPartnerContractFileValid = true;
                    }
                } 
            }
            else
            {
                musicContractFilePath = ConfigurationManager.AppSettings["MusicContractFile"];
                partnerFilePath = ConfigurationManager.AppSettings["PartnerContractFile"];
            }

            if (!useDefaultFiles && isMusicContractFileValid && isPartnerContractFileValid)
            {
                TextFileReader musicContractReader = new TextFileReader(musicContractFilePath);
                var musicContractsText = musicContractReader.ContractString;

                IContractProcess musicContractProcess = new MusicContractFileProcess(musicContractsText);
                var musicContracList = musicContractProcess.ProcessedContract;

                TextFileReader partnerContractReader = new TextFileReader(partnerFilePath);
                var partnerContractText = partnerContractReader.ContractString;

                IContractProcess partnerContractProcess = new PartnerContractFileProcess(partnerContractText);
                var partnerContactList = partnerContractProcess.ProcessedContract as Dictionary<string, string>;

            }
            Console.ReadLine();
        }
    }
}
