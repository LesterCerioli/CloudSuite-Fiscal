using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.MultTenant.Fiscal.Domain.Enums
{
    public enum TipoTributacaoEnum
    {
        ForaMunIsento,
        Imune,
        ForaMunImune,
        Suspensa,
        ForaMunSuspensa,
        ExpServicos,
        DepositoEmJuizo,
        NaoIncide,
        TributavelFixo
    }
}
