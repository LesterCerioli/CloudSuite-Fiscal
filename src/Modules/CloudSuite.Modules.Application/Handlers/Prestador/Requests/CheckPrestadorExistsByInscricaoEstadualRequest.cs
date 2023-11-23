using CloudSuite.Modules.Application.Handlers.Prestador.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.Prestador.Requests
{
    public class CheckPrestadorExistsByInscricaoEstadualRequest : IRequest<CheckPrestadorExistsByInscricaoEstadualResponse>
    {
        
        public Guid Id { get; set; }

        public string? InscricaoEstadual { get; private set; }

        public CheckPrestadorExistsByInscricaoEstadualRequest(string? inscricaoEstadual)
        {
            Id = Guid.NewGuid();
            InscricaoEstadual = inscricaoEstadual;
        }

    }
}
