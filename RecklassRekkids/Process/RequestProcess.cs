using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecklassRekkids.Process
{
    public interface IRequestProcess
    {
        List<MusicContracts> Process(string musicContractFilePath, string partnerContractFilePath);
    }

    public class RequestProcess : IRequestProcess
    {
        private readonly IPartnerContractService _partnerService;
        private readonly IMusicContractService _musicService;
        private readonly IUserInputProcess _input;
        private readonly IContractService _contractService; 
        private readonly ITextFileReaderService _musicFileReaderService;
        private readonly ITextFileReaderService _partnerFileReaderService;

        public RequestProcess(IUserInputProcess input, IMusicContractService musicService,
            IPartnerContractService partnerService, IContractService contractService, ITextFileReaderService musicFileReaderService, ITextFileReaderService partnerFileReaderService)
        {
            _musicService = musicService;
            _partnerService = partnerService;
            _contractService = contractService; 
            _musicFileReaderService = musicFileReaderService;
            _partnerFileReaderService = partnerFileReaderService;
            _input = input;
        }

       public List<MusicContracts> Process(string musicContractFilePath, string partnerContractFilePath)
        {
            var musicContractList = _musicFileReaderService.ReadTextFile(musicContractFilePath);
            var partnerContractList = _partnerFileReaderService.ReadTextFile(partnerContractFilePath);

            var musicContractsAll = _musicService.ProcessContract(musicContractList);
            var partnerContractAll = _partnerService.ProcessContract(partnerContractList);

            var partnerUsage = partnerContractAll.SingleOrDefault(x => x.Key == _input.UserSerachCriteria.Partner.ToLower()).Value;

            var musicContracts = _contractService.Process(musicContractsAll, partnerUsage, _input.UserSerachCriteria.SearchDate);
            musicContracts.ForEach(x => x.Usage = partnerUsage);
            return musicContracts;
        }

    }
}
