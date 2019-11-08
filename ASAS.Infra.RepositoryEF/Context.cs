using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using ASAS.Log;
using ASAS.Domain.Comum;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Infrastructure;
using ASAS.Infra.RepositoryEF.ModelMap;

namespace ASAS.Infra.RepositoryEF
{
    public class Context : DbContext, IGerenciadorDeBD
    {
        public DbSet<ASAS.Domain.Model.Reles.ReleReport> Reles { get; set; }

        public Context()
            : base("DB_ASAS")
        {
           
            //TODO: asas automated systems and supervisory
            Database.SetInitializer(new DBInitializer());
            Database.Initialize(true);
        }

        public string RetornarStringConexao()
        {
            try
            {
                return "Servidor: " + this.Database.Connection.DataSource + "\r\nDatabase: " + this.Database.Connection.Database;
            }
            catch
            {
                return "String de Conexão com BD Não Encontrada";
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
           // modelBuilder.Conventions.Remove<IncludeMetadataConvention>();

            //mapeando entidades
            modelBuilder.Configurations.Add(new ReleReportMap());
        }
       

        public void CriarBancoDeDados(bool apagarExistente)
        {
            Logger.Instancia.Fatal(this, "CriarBancoDeDados", new Exception("Criando Banco de Dados"), "Criando Banco de Dados - NAO EH ERRO");
            if (this.Database.Exists() && apagarExistente)
            {
                Logger.Instancia.Fatal(this, "CriarBancoDeDados", new Exception("Excluindo Banco de Dados"), "Excluindo Banco de Dados - NAO EH ERRO");
                this.Database.Delete();
                Logger.Instancia.Fatal(this, "CriarBancoDeDados", new Exception("Criando Banco de Dados"), "Criando Banco de Dados - NAO EH ERRO");
            }

            Database.SetInitializer(new DBInitializer());
            Database.Initialize(true);
        }

    }

}
