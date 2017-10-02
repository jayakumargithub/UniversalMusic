using System;
using System.Configuration;
using Autofac;
using RecklassRekkids.Process;

namespace RecklassRekkids
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = AutoFacContainer.BuildContainer();
           

            string partnerContractFilePath = null;
            string musicContractFilePath;
            bool isMusicContractFileValid = false;
            bool isPartnerContractFileValid = false; 
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
                    partnerContractFilePath = Console.ReadLine();
                    if (!System.IO.File.Exists(partnerContractFilePath))
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
            else // if use wants to use default file then pick up from fixed location
            {
                musicContractFilePath = ConfigurationManager.AppSettings["MusicContractFile"];
                partnerContractFilePath = ConfigurationManager.AppSettings["PartnerContractFile"];
            }
            bool canContinue = true;
            if (useDefaultFiles || (isMusicContractFileValid && isPartnerContractFileValid))
            { 

                while (true)
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

                    IUserInputProcess userInputProcess = new UserInputProcess(searchPharse);
                    if (userInputProcess.Errors.Count > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(string.Join("/r/n", userInputProcess.Errors.ToArray()));
                        canContinue = false;
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    if (canContinue)
                    {
                        var musicContractService = container.Resolve<IMusicContractService>();
                        var partnerContractService = container.Resolve<IPartnerContractService>();
                        var contractService = container.Resolve<IContractService>();
                        var musicContractTextReaderService = container.Resolve<ITextFileReaderService>();
                        var partnerContractTextReaderService = container.Resolve<ITextFileReaderService>();
                        var printService = container.Resolve<IPrintService>();
                        IRequestProcess requestProcess = new RequestProcess(userInputProcess, musicContractService,partnerContractService, contractService,musicContractTextReaderService,partnerContractTextReaderService);
                        var musicContractResults = requestProcess.Process(musicContractFilePath,partnerContractFilePath);
                        string data;
                        printService.Print(musicContractResults, out data); 
                    }
                    canContinue = true;
                }

            } 
        } 
    }
}

