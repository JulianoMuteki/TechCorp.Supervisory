using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ASAS.Domain.Comum
{
    public interface IObjetoDeValor<T>:IEqualityComparer<T>,IEqualityComparer,IEquatable<T>
    {
    }
}
