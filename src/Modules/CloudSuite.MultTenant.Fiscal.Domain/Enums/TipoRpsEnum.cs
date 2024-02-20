using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.MultTenant.Fiscal.Domain.Enums
{
    public enum TipoRpsEnum
    {
        RPS = 0,

        [Description("NFSe Conjugada")]
        NFConjugada = 1,

        Cupom = 2
    }
}
