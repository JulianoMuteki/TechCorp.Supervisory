using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ASAS.Domain.Comum
{
    public interface IObjetoDeValorComId<T>:IObjetoDeValor<T>
    {
        long Id { get; }
    }
}
