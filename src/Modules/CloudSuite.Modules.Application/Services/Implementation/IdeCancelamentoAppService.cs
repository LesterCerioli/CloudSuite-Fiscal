using AutoMapper;
using CloudSuite.Modules.Application.Handlers.IdeCancelamento;
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

        public IdeCancelamentoAppService(IIdeCancelamentoRepository ideCancelamentoRepository, IMapper mapper, IMediatorHandler mediator)
        {
            _ideCancelamentoRepository = ideCancelamentoRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<IdeCancelamentoViewModel> GetByCancelReason(string cancelReason)
        {
            return _mapper.Map<IdeCancelamentoViewModel>( await _ideCancelamentoRepository.GetByCancelReason(cancelReason));
        }

        public async Task<IdeCancelamentoViewModel> GetByTimeDate(DateTimeOffset timeDate)
        {
            return _mapper.Map<IdeCancelamentoViewModel>( await _ideCancelamentoRepository.GetByTimeDate(timeDate));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task Save(CreateIdeCancelamentoCommand createCommand)
        {
            await _ideCancelamentoRepository.Add(createCommand.GetEntity());
        }
    }
}
