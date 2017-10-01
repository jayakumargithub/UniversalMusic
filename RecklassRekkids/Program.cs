using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Text.RegularExpressions;
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

            Console.WriteLine("You want to use default contarct file? type 'Y' or 'N' ");
            var userInput = Console.ReadLine();
            if (userInput != null && userInput.ToLower() == "y")
            {
                useDefaultFiles = true;
            }

            if (!useDefaultFiles)
            {
                Console.WriteLine("Please Enter Music Contract Text File Path");
                Console.WriteLine();
                musicContractFilePath = Console.ReadLine();
                Console.WriteLine();
                if (!System.IO.File.Exists(musicContractFilePath))
                {
                    Console.WriteLine("Music contract file path is not valid!.");
                    Console.WriteLine();
                }
                else
                {
                    isMusicContractFileValid = true;
                }
                if (isMusicContractFileValid)
                {
                    Console.WriteLine("Please Enter Partner Contract Text File Path");
                    Console.WriteLine();
                    partnerFilePath = Console.ReadLine();
                    if (!System.IO.File.Exists(partnerFilePath))
                    {
                        Console.WriteLine("Partner contract file path is not valid!");
                        Console.WriteLine();
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

            bool canContinue = true;
            if (useDefaultFiles || (isMusicContractFileValid && isPartnerContractFileValid))
            {
                UserSerachCriteria searchCriteria = new UserSerachCriteria();

                while (canContinue)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    var msg = "Please enter search criteria. or Enter 'q' to  Quit.";
                    if (msg.ToLower() == "q")
                    {
                        System.Environment.Exit(0);
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                    Console.WriteLine(msg);
                    Console.WriteLine();

                    var searchPharse = Console.ReadLine();
                    if (searchPharse != null && searchPharse.ToLower() == "q")
                    {
                        System.Environment.Exit(0);
                    }
                    while (string.IsNullOrEmpty(searchPharse))
                    {
                        Console.WriteLine();
                        Console.WriteLine(msg);

                        Console.WriteLine();
                        searchPharse = Console.ReadLine();
                    }

                    Console.WriteLine();

                    //Reading the Partner contract text file
                    TextFileReader partnerContractReader = new TextFileReader(partnerFilePath);
                    var partnerContractText = partnerContractReader.ContractString;

                    //Building the parter contract model
                    IContractProcess partnerContractProcess = new PartnerContractFileProcess(partnerContractText);
                    var partnerContactList = partnerContractProcess.ProcessedContract as Dictionary<string, string>;

                    UserInputProcess userInputProcess = new UserInputProcess(searchPharse);
                    if (userInputProcess.Errors.Count > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(string.Join("/r/n", userInputProcess.Errors.ToArray()));
                        canContinue = userInputProcess.Errors.Count == 0;
                    }

                    if (canContinue)
                    {


                        //Reading the music contract text file
                        TextFileReader musicContractReader = new TextFileReader(musicContractFilePath);
                        var musicContractsText = musicContractReader.ContractString;

                        //Building the music contract model 
                        IContractProcess musicContractProcess = new MusicContractFileProcess(musicContractsText);
                        var musicContracList = musicContractProcess.ProcessedContract as List<MusicContracts>;

                        if (partnerContactList != null)
                        {
                            var partnerUsage =
                                partnerContactList.SingleOrDefault(x => x.Key == searchCriteria.Partner).Value;

                            ContractService contractService = new ContractService();
                            var musicContracts = contractService.Process(musicContracList, partnerUsage,
                                searchCriteria.SearchDate);

                            if (musicContracts.Count > 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("----Here are  the results-----");
                                Console.WriteLine();

                                foreach (var a in musicContracts)
                                {
                                    Console.WriteLine(a.Artist + "|" + a.Title + "|" + partnerUsage + "|" +
                                                      CommonUtility.GetSuffix(a.StartDate.Date) + "|" +
                                                      CommonUtility.GetSuffix(a.EndDate?.Date));
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("No Results found!.");
                            }
                        }

                    }
                    //}
                    canContinue = true;
                }
            }

            Console.ReadLine();
        }
    }
}
