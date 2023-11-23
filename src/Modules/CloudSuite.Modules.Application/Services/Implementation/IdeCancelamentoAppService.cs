using AutoMapper;
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
    public class IdeCancelamentoAppService : IIdeCancelamentoAppService
    {
        private readonly IIdeCancelamentoRepository _ideCancelamentoRepository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;
        public async Task<IdeCancelamentoViewModel> GetByCancelReason(string cancelReason)
        {
            return _mapper.Map<IdeCancelamentoViewModel>( await _ideCancelamentoRepository.GetByCancelReason(cancelRaaeason));
        }

        public async Task<IdeCancelamentoViewModel> GetByTimeDate(DateTimeOffset timeDate)
        {
            return _mapper.Map<IdeCancelamentoViewModel>( await _ideCancelamentoRepository.GetByTimeDate(timeDate));
        }

        public async Task<IdeCancelamentoViewModel> Save(CreateIdeCancelamento createCommand)
        {
            return await _ideCancelamentoRepository.Add(createCommand.GetEntity());
        }
    }
}
