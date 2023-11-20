using CloudSuite.Modules.Application.Handlers.Prestador.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.Prestador.Requests
{
    public class CheckPrestadorExistsByNomeFantasiaRequest : IRequest<CheckPrestadorExistsByNomeFantasiaResponse>
    {

        public Guid Id { get; private set; }

        public string NomeFantasia { get; set; }

        public CheckPrestadorExistsByNomeFantasiaRequest(Guid id, string nomeFantasia)
        {
            Id = Guid.NewGuid();
            NomeFantasia = nomeFantasia;
        }
    }
}
