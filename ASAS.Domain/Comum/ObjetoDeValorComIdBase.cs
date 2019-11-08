using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ASAS.Log;

namespace ASAS.Domain.Comum
{
        [Serializable]
    public abstract class ObjetoDeValorComIdBase<T> : IObjetoDeValorComId<T> where T : IObjetoDeValorComId<T>
    {
        public virtual long Id { get; protected set; }
        public virtual DateTime DataHoraUltimaAlteracao { get; protected set; }

        protected void AtualizarDataHoraUltimaAlteracao()
        {
            this.DataHoraUltimaAlteracao = DateTime.Now;
        }

        /// <summary>
        /// <b>NÃO UTILIZAR!</b>
        /// Necessario por causa do NHibernate
        /// </summary>

        protected ObjetoDeValorComIdBase()
        {
            this.Id = 0L;
        }

        public ObjetoDeValorComIdBase(long id)
        {
            this.Id = id;
        }

        public virtual new int GetHashCode()
        {
            return this.GetHashCode(this);
        }

        public virtual bool Equals(T other)
        {
            if (other is T)
                return ((T)other).Id.Equals(this.Id);

            return false;
        }

        public new virtual bool Equals(object x, object y)
        {
            if (x == null && y == null)
            {
                return false;
            }
            if (x is T && y is T)
            {
                return this.Equals((T)x, (T)y);
            }

            return (x.Equals(y));
        }

        public virtual int GetHashCode(object obj)
        {
            if (obj is T)
                return this.GetHashCode((T)obj);
            else
                throw new ArgumentNullException();
        }
        

        #region IEqualityComparer<IEntidade> Members

        public virtual bool Equals(T x, T y)
        {
            if (x == null || y == null)
            {
                return false;
            }
            if (x is T && y is T)
            {
                return ((T)x).Id == ((T)y).Id;
            }

            return (x.Equals(y));
        }

        public virtual int GetHashCode(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("Valores não instanciados");
            }
            else
            {
                if (((T)obj).Id == 0L)
                    throw new ArgumentException("GUID não deve ser Vazio (Zero)");
                else
                    return ((T)obj).Id.GetHashCode();
            }
        }

        #endregion
    }
}
