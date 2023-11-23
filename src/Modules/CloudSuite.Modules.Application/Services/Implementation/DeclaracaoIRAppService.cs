using AutoMapper;
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
    public class DeclaracaoIRAppService : IDeclaracaoIRAppService
    {
        private readonly IDeclaracaoIRRepository _declaracaoIRRepository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;

        public DeclaracaoIRAppService(IDeclaracaoIRRepository declaracaoIRRepository, IMapper mapper, IMediatorHandler mediator)
        {
            _declaracaoIRRepository = declaracaoIRRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<DeclaracaoIRViewModel> GetByAlimony(decimal alimony)
        {
            return _mapper.Map<DeclaracaoIRViewModel>(await _declaracaoIRRepository.GetByAlimony(alimony));
        }

        public async Task<DeclaracaoIRViewModel> GetByCnpj(Cnpj cnpj)
        {
            return _mapper.Map<DeclaracaoIRViewModel>(await _declaracaoIRRepository.GetByCnpj(cnpj));
        }

        public async Task<DeclaracaoIRViewModel> GetByCpf(Cpf cpf)
        {
            return _mapper.Map<DeclaracaoIRViewModel>(await _declaracaoIRRepository.GetByCpf(cpf));
        }

        public async Task<DeclaracaoIRViewModel> GetByDeclaracaoNumero(string declaracaoNumero)
        {
            return _mapper.Map<DeclaracaoIRViewModel>(await _declaracaoIRRepository.GetByDeclaracaoNumero(declaracaoNumero));
        }

        public async Task<DeclaracaoIRViewModel> GetByPaidValueToBusiness(decimal paidValueToBusiness)
        {
            return _mapper.Map<DeclaracaoIRViewModel>(await _declaracaoIRRepository.GetByPaidValueToBusiness(paidValueToBusiness));
        }

        public async Task<DeclaracaoIRViewModel> GetByProfitsDividends(decimal profitsDividends)
        {
            return _mapper.Map<DeclaracaoIRViewModel>(await _declaracaoIRRepository.GetByProfitsDividends(profitsDividends));
        }

        public async Task<DeclaracaoIRViewModel> GetByTotalIncome(decimal totalIncome)
        {
            return _mapper.Map<DeclaracaoIRViewModel>(await _declaracaoIRRepository.GetByTotalIncome(totalIncome));
        }

        public async Task<DeclaracaoIRViewModel> Save(CreateDeclaracaoIRCommand createCommand)
        {
            await _declaracaoIRRepository.Add(createCommand.GetEntity());
        }
    }
}
