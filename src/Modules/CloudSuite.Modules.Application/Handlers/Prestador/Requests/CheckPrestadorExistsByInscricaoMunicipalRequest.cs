using CloudSuite.Modules.Application.Handlers.Prestador.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.Prestador.Requests
{
    public class CheckPrestadorExistsByInscricaoMunicipalRequest : IRequest<CheckPrestadorExistsByInscricaoMunicipalResponse>
    {

        public Guid Id { get; private set; }

        public string? InscricaoMunicipal { get; private set; }

        public CheckPrestadorExistsByInscricaoMunicipalRequest(string? inscricaoMunicipal)
        {
            Id = Guid.NewGuid();
            InscricaoMunicipal = inscricaoMunicipal;
        }

    }
}
