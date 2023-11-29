using AutoMapper;
using CloudSuite.Modules.Application.Handlers.CancelOrder;
using CloudSuite.Modules.Application.Services.Contracts;
using CloudSuite.Modules.Application.ViewModels;
using CloudSuite.Modules.Common.ValueObjects;
using CloudSuite.Modules.Domain.Contracts;
using NetDevPack.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Services.Implementation
{
    public class CancelOrderAppService : ICancelOrderAppService
    {
        private readonly ICancelOrderRepository _cancelOrderRepository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;


        public CancelOrderAppService(
            ICancelOrderRepository cancelOrderRepository,
            IMediatorHandler mediator,
            IMapper mapper)
        {
            _cancelOrderRepository = cancelOrderRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<CancelOrderViewModel> GetbyCnpj(Cnpj cnpj)
        {
            return _mapper.Map<CancelOrderViewModel>(await _cancelOrderRepository.GetbyCnpj(cnpj));
        }

        public async Task<CancelOrderViewModel> GetByRequestDate(DateTimeOffset requestDate)
        {
            return _mapper.Map<CancelOrderViewModel>(await _cancelOrderRepository.GetByRequestDate(requestDate));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task Save(CreateCancelOrderCommand commandCreate)
        {
            await _cancelOrderRepository.Add(commandCreate.GetEntity());
        }
    }
}
