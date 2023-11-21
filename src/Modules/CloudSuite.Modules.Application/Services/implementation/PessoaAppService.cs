using AutoMapper;
using CloudSuite.Modules.Application.Handler;
using CloudSuite.Modules.Application.Services.Contract;
using CloudSuite.Modules.Application.ViewModels;
using CloudSuite.Modules.Domain.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CloudSuite.Modules.Application.Services.implementation
{
    public class PessoaAppService : IPessoaAppService
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public PessoaAppService(IPessoaRepository pessoaRepository, IMapper mapper, ILogger logger)
        {
            _pessoaRepository = pessoaRepository;
            _mapper = mapper;
            _logger = logger;

        }
        public async Task<PessoaViewModel> GetByName(string name)
        {
            return _mapper.Map<PessoaViewModel>(await _pessoaRepository.GetByName(name));
        }

        public async Task Save(CreatePessoaCommand commandCreate)
        {
            await _pessoaRepository.Add(commandCreate.GetEntity());
        }

        public Task Save(PessoaViewModel commandCreate)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
