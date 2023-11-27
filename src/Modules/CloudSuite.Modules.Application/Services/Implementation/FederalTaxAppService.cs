using AutoMapper;
using CloudSuite.Modules.Application.Handlers.FederalTax;
using CloudSuite.Modules.Application.Services.Contracts;
using CloudSuite.Modules.Application.ViewModels;
using CloudSuite.Modules.Domain.Contracts;
using NetDevPack.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Services.Implementation
{
    public class FederalTaxAppService : IFederalTaxAppService
    {
        IFederalTaxRepository _federalTaxRepository;
        IMapper _mapper;
        IMediatorHandler _mediator;

        public async Task<FederalTaxViewModel> GetByVCOFINS(decimal vCOFINS)
        {
            return _mapper.Map<FederalTaxViewModel>(await _federalTaxRepository.GetByVCOFINS(vCOFINS));
        }

        public async Task<FederalTaxViewModel> GetByVCSLL(decimal vCSLL)
        {
            return _mapper.Map<FederalTaxViewModel>(await _federalTaxRepository.GetByVCSLL(vCSLL));
        }

        public async Task<FederalTaxViewModel> GetByVINSS(decimal vINSS)
        {
            return _mapper.Map<FederalTaxViewModel>(await _federalTaxRepository.GetByVINSS(vINSS));
        }

        public async Task<FederalTaxViewModel> GetByVIR(decimal vIR)
        {
            return _mapper.Map<FederalTaxViewModel>( await _federalTaxRepository.GetByVIR(vIR));
        }

        public async Task<FederalTaxViewModel> GetByVPIS(decimal vPIS)
        {
            return _mapper.Map<FederalTaxViewModel>( await _federalTaxRepository.GetByVPIS(vPIS));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task Save(CreateFederalTaxCommand createCommand)
        {
            await _federalTaxRepository.Add(createCommand.GetEntity());
        }
    }
}
