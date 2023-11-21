using CloudSuite.Modules.Application.Handler.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handler.Requests
{
    public  class CheckPessoaExistsByNameRequest : IRequest<CheckPessoaExistsByNameResponse>
    {
        public CheckPessoaExistsByNameRequest(Guid id, string? nome)
        {
            Id = id;
            Nome = nome;
        }

        public Guid Id { get; private set; }

        public string? Nome { get; private set; }
    }
}
