using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using RecklassRekkids.Process;

namespace RecklassRekkids
{
    public static class AutoFacContainer
    {
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ContractService>().As<IContractService>();
            builder.RegisterType<UserInputProcess>().As<IUserInputProcess>();
            builder.RegisterType<TextFileReaderService>().As<ITextFileReaderService>();
            builder.RegisterType<MusicContractService>().As<IMusicContractService>();
            builder.RegisterType<PartnerContractService>().As<IPartnerContractService>();
            builder.RegisterType<PrintService>().As<IPrintService>();
            return builder.Build();
        }
    }
}
