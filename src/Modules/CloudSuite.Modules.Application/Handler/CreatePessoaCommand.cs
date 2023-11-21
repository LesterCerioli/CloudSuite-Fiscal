using CloudSuite.Modules.Application.Handler.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PessoaEntity = CloudSuite.Modules.Domain.Models.Pessoa;

namespace CloudSuite.Modules.Application.Handler
{
    public class CreatePessoaCommand : IRequest<CreatePessoaResponse>
    {
        public Guid Id { get; private set; }

        public string? Nome { get; private set; }

        public string? Idade { get; private set; }


        public PessoaEntity GetEntity()
        {
            return new PessoaEntity(
            this.Id,
            this.Nome,
            this.Idade
                );
        }
    }
}
