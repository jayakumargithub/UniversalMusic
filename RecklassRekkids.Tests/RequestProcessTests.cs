using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Moq;
using NUnit.Framework;
using RecklassRekkids.Process;

namespace RecklassRekkids.Tests
{
    [TestFixture]
    class RequestProcessTests
    {

        private IMusicContractService _musicContractService;
        private IPartnerContractService _partnerContractService;
        private IContractService _contractService;
        private Mock<ITextFileReaderService> _musicContractFileReaderService;
        private Mock<ITextFileReaderService> _partnerContrFileReaderService;
        private IPrintService _printService;
        private RequestProcess _sut;


        [OneTimeSetUp]
        public void SetUp()
        {
            _musicContractService = new MusicContractService();
            _partnerContractService = new PartnerContractService();
            _contractService = new ContractService();
            _musicContractFileReaderService = new Mock<ITextFileReaderService>();
            _partnerContrFileReaderService = new Mock<ITextFileReaderService>();
            _printService = new PrintService();
            _musicContractFileReaderService.Setup(x => x.ReadTextFile(It.IsAny<string>()))
                .Returns(TestData.GetMusicContractData);
            _partnerContrFileReaderService.Setup(x => x.ReadTextFile(It.IsAny<string>()))
                .Returns(TestData.GetPartnerContractData);
            _musicContractService.ProcessContract(TestData.GetMusicContractData());
            _partnerContractService.ProcessContract(TestData.GetPartnerContractData());

        }

        [Test]
        public void returns_4_streaming_music_contract_for_youtube()
        {
            StringBuilder expected = new StringBuilder();
            expected.Append("Monkey Claw|Iron Horse|streaming|1st Jun 2012|");
            expected.Append(System.Environment.NewLine);
            expected.Append("Monkey Claw|Motor Mouth|streaming|1st Mar 2011|");
            expected.Append(System.Environment.NewLine);
            expected.Append("Monkey Claw|Christmas Special|streaming|25th Dec 2012|31st Dec 2012");
            expected.Append(System.Environment.NewLine);
            expected.Append("Tinie Tempah|Frisky (Live from SoHo)|streaming|1st Feb 2012|");
            expected.Append(System.Environment.NewLine);

            IUserInputProcess input = new UserInputProcess("YouTube 27th Dec 2012");
            _sut = new RequestProcess(input, _musicContractService, _partnerContractService, _contractService,
                _musicContractFileReaderService.Object, _partnerContrFileReaderService.Object);
            var actual = _sut.Process(It.IsAny<string>(), It.IsAny<string>());
            string actualOutput;
            _printService.Print(actual, out actualOutput);

            StringAssert.AreEqualIgnoringCase(expected.ToString(), actualOutput);


        }

        [Test]
        public void return_4_digitaldownload_music_contract_for_itunes()
        {
            StringBuilder expected = new StringBuilder();
            expected.Append("Monkey Claw|Black Mountain|digital download|1st Feb 2012|");
            expected.Append(System.Environment.NewLine);
            expected.Append("Monkey Claw|Motor Mouth|digital download|1st Mar 2011|");
            expected.Append(System.Environment.NewLine);
            expected.Append("Tinie Tempah|Frisky (Live from SoHo)|digital download|1st Feb 2012|");
            expected.Append(System.Environment.NewLine);
            expected.Append("Tinie Tempah|Miami 2 Ibiza|digital download|1st Feb 2012|");
            expected.Append(System.Environment.NewLine);

            IUserInputProcess input = new UserInputProcess("ITunes 1st March 2012");

            _sut = new RequestProcess(input, _musicContractService, _partnerContractService, _contractService,
                _musicContractFileReaderService.Object, _partnerContrFileReaderService.Object);
            var actual = _sut.Process(It.IsAny<string>(), It.IsAny<string>());
            string actualOutput;
            _printService.Print(actual, out actualOutput);

            StringAssert.AreEqualIgnoringCase(expected.ToString(), actualOutput);

        }

        [Test]
        public void return_2_streaming_music_contract_for_youtube()
        {
            StringBuilder expected = new StringBuilder();
            expected.Append("Monkey Claw|Motor Mouth|streaming|1st Mar 2011|");
            expected.Append(System.Environment.NewLine);
            expected.Append("Tinie Tempah|Frisky (Live from SoHo)|streaming|1st Feb 2012|");
            expected.Append(System.Environment.NewLine);
            IUserInputProcess input = new UserInputProcess("Youtube 1st April 2012");

            _sut = new RequestProcess(input, _musicContractService, _partnerContractService, _contractService,
                _musicContractFileReaderService.Object, _partnerContrFileReaderService.Object);
            var actual = _sut.Process(It.IsAny<string>(), It.IsAny<string>());
            string actualOutput;
            _printService.Print(actual, out actualOutput);

            StringAssert.AreEqualIgnoringCase(expected.ToString(), actualOutput);
        }
    }
}
