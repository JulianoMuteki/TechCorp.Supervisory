using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ASAS.Domain.Comum
{
    public interface IEntidade:IEqualityComparer<IEntidade>,IEqualityComparer 
    {
        DateTime DataHoraUltimaAlteracao { get; }
        long Id { get; }
    }
}
