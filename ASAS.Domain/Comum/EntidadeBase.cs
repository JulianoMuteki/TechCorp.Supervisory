using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using ASAS.Log;

namespace ASAS.Domain.Comum
{
    [Serializable]
    public abstract class EntidadeBase : IEntidade
    {
        public virtual long Id { get; protected set; }
        public virtual DateTime DataHoraUltimaAlteracao { get; protected set; }

        protected EntidadeBase()
        {
            this.Id = 0;
            this.DataHoraUltimaAlteracao = DateTime.Now;
        }

        public new virtual int GetHashCode()
        {
            Logger.Instancia.Warn(this, "GetHashCode()", "Verificando...");
            if(this.Id != 0L)
                return this.Id.GetHashCode();
            else
                throw new ArgumentNullException("Valores não instanciados");
        }

        protected virtual void AtualizarDataHoraUltimaAlteracao()
        {
            this.DataHoraUltimaAlteracao = DateTime.Now;
        }

        public new virtual bool Equals(object x, object y)
        {
            Logger.Instancia.Warn(this, "Equals(obj,obj)", "Verificando...");
            if (x == null && y == null)
            {
                return false;
            }
            if (x is IEntidade && y is IEntidade)
            {
                return this.Equals(x, y);
            }

            return (x.Equals(y));
        }

        public virtual int GetHashCode(object obj)
        {
            Logger.Instancia.Warn(this, "GetHashCode(obj)", "Verificando...");
            if (obj is IEntidade)
                return this.GetHashCode((IEntidade)obj);
            else
                throw new NotImplementedException();
        }

        #region IEqualityComparer<IEntidade> Members

        public virtual bool Equals(IEntidade x, IEntidade y)
        {
            Logger.Instancia.Warn(this, "Equals(entidade,entidade)", "Verificando...");
            if (x == null && y == null)
            {
                return false;
            }
            if (x is IEntidade && y is IEntidade)
            {
                return ((IEntidade)x).Id == ((IEntidade)y).Id;
            }

            return (x.Equals(y));
        }

        public virtual int GetHashCode(IEntidade obj)
        {
            Logger.Instancia.Warn(this, "GetHashCode(entidade)", "Verificando...");
            if (!(obj != null || obj is IEntidade || obj.Id == 0L))
            {
                throw new NotImplementedException();
            }
            else
            {
                return obj.Id.GetHashCode();
            }
        }

        #endregion
    }
}
