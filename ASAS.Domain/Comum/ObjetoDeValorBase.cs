using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASAS.Domain.Comum
{
    [Serializable]
    public class ObjetoDeValorBase<T> : IObjetoDeValor<T> where T : ObjetoDeValorBase<T>
    {
        public virtual string Valor { get; protected set; }

        protected ObjetoDeValorBase()
        {
            this.Valor = string.Empty;
        }

        public ObjetoDeValorBase(string valor)
        {
            this.Valor = valor;
        }

        public override string ToString()
        {
            return Valor;
        }

        public static bool operator ==(ObjetoDeValorBase<T> a, ObjetoDeValorBase<T> b)
        {
            try
            {
                return a.Valor.Equals(b.Valor);
            }
            catch
            {
                return false;
            }
        }

        public static bool operator !=(ObjetoDeValorBase<T> a, ObjetoDeValorBase<T> b)
        {
            try
            {
                return !a.Valor.Equals(b.Valor);
            }
            catch
            {
                return false;
            }
        }

        public virtual bool Equals(T x, T y)
        {
            if (x == null || y == null)
                return false;

            return x.Valor.Equals(y.Valor);
        }

        public virtual int GetHashCode(T obj)
        {
            if (obj == null)
                return -1;

            return obj.Valor.GetHashCode();
        }

        public override int GetHashCode()
        {
            return this.Valor.GetHashCode();
        }

        public virtual int GetHashCode(object obj)
        {
            return this.GetHashCode((T)obj);
        }

        public override bool Equals(object obj)
        {
            if (obj is T)
                return ((T)obj).Valor.Equals(this.Valor);

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


        /* Codigo de controle de igualdade copia do exemplo de DDD do Evans
        ///
        /// <summary>
        /// To be overridden in inheriting clesses for providing a collection of atomic values of
        /// this Value Object.
        /// </summary>
        /// <returns>Collection of atomic values.</returns>
        protected abstract IEnumerable<object> GetAtomicValues();

        /// <summary>
        /// Compares two Value Objects according to atomic values returned by <see cref="GetAtomicValues"/>.
        /// </summary>
        /// <param name="obj">Object to compare to.</param>
        /// <returns>True if objects are considered equal.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }
            ObjetoDeValorBase<T> other = (ObjetoDeValorBase<T>)obj;
            IEnumerator<object> thisValues = GetAtomicValues().GetEnumerator();
            IEnumerator<object> otherValues = other.GetAtomicValues().GetEnumerator();
            while (thisValues.MoveNext() && otherValues.MoveNext())
            {
                if (ReferenceEquals(thisValues.Current, null) ^ ReferenceEquals(otherValues.Current, null))
                {
                    return false;
                }
                if (thisValues.Current != null && !thisValues.Current.Equals(otherValues.Current))
                {
                    return false;
                }
            }
            return !thisValues.MoveNext() && !otherValues.MoveNext();
        }

        /// <summary>
        /// Helper function for implementing overloaded equality operator.
        /// </summary>
        /// <param name="left">Left-hand side object.</param>
        /// <param name="right">Right-hand side object.</param>
        /// <returns></returns>
        protected static bool EqualOperator(ObjetoDeValorBase<T> left, ObjetoDeValorBase<T> right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            {
                return false;
            }
            return ReferenceEquals(left, null) || left.Equals(right);
        }

        /// <summary>
        /// Helper function for implementing overloaded inequality operator.
        /// </summary>
        /// <param name="left">Left-hand side object.</param>
        /// <param name="right">Right-hand side object.</param>
        /// <returns></returns>
        protected static bool NotEqualOperator(ObjetoDeValorBase<T> left, ObjetoDeValorBase<T> right)
        {
            return !EqualOperator(left, right);
        }
        */

        public bool Equals(T other)
        {
            if (other is T)
                return ((T)other).Valor.Equals(this.Valor);

            return false;
        }
    }
}
