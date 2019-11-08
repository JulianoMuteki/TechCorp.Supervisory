using ASAS.Log;
using ASAS.Services;
using Ninject;
using Ninject.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAS.Infra.ServiceFactory
{
    public class ServiceFactory : IFactory
    {
        private static IFactory fInstancia;

        // A Fábrica é Singleton.
        public static IFactory Instancia
        {
            get
            {
                if (fInstancia == null)
                    fInstancia = new ServiceFactory();

                return fInstancia;
            }
        }

        private StandardKernel Kernel { get; set; }
        public Ninject.Modules.NinjectModule Service { get; set; }


        public ServiceFactory()
        {
            Logger.Instancia.Debug(this, "New()", "Iniciando Fabrica de IoC [REPOSITORIOS]");
            // Nesse ponto definimos qual módulo de persistência
            //será utilizado.
            this.Service = new ServiceModel();

            Logger.Instancia.Debug(this, "New()", "Instanciando Kernel do Ninject");
            this.Kernel = new StandardKernel(Service);
        }


        #region [ Métodos ]

        public T Obter<T>()
        {
            //Logger.Instancia.Trace(this, "Obter<T>", " Obtendo tipo de " + typeof(T).Name);
            foreach (var item in typeof(T).GetInterfaces())
            {
                if (item.Name.Contains("IRepositorio"))
                {
                    var ex = new Exception("REPOSITORIO NAO PODE SER ACESSADO VIA O METODO OBTER<T>. UTILIZE O METODO OBTERREPOSITORIO<T>");
                    // Logger.Instancia.Fatal(this, "Obter<T>", ex, string.Empty);
                    throw ex;
                }
                else if (item.Name.Contains("IServico"))
                {
                    var ex = new Exception("SERVICO NAO PODE SER ACESSADO VIA O METODO OBTER<T>. UTILIZE O METODO OBTERSERVICO<T>");
                    //Logger.Instancia.Fatal(this, "Obter<T>", ex, string.Empty);
                    throw ex;
                }
            }
            return this.Kernel.Get<T>();
        }

        public T ObterRepositorio<T>(object contexto)
        {
            Logger.Instancia.Trace(this, "ObterRepositorio<T>", " Obtendo repositorio de " + typeof(T).Name);

            bool flagEhRepositorio = false;

            foreach (var item in typeof(T).GetInterfaces())
            {
                if (item.Name.Contains("IRepository"))
                {
                    flagEhRepositorio = true;
                    break;
                }
            }

            if (flagEhRepositorio)
                return this.Kernel.Get<T>(new ConstructorArgument("context", contexto));
            else
            {
                var ex = new Exception("TIPO DE OBJETO SOLICITADO NAO EH UM REPOSITORIO");
                Logger.Instancia.Fatal(this, "ObterRepositorio<T>", ex, string.Empty);
                throw ex;
            }
        }

        #endregion
    }
}
