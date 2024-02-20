using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.MultTenant.Fiscal.Domain.Enums
{
    public enum TipoTomadorServico
    {
        PFNI = 1,
        PessoaFisica = 2,
        JuridicaDoMunicipio = 3,
        JuridicaForaMunicipio = 4,
        JuridicaForaPais = 5,
        ProdutorRuralOuPolitico = 6
    }
}
