using System;
using System.IO;
using NLog;

namespace ASAS.Log
{
    /// <summary>
    /// Classe FACADE para Log dos eventos internos do sistema
    /// </summary>
    public class Logger
    {
        private static Logger _instancia;

        public static Logger Instancia
        {
            get
            {
                if (_instancia != null)
                    return _instancia;
                else
                {
                    _instancia = new Logger();
                    return _instancia;
                }
            }
        }

        private NLog.Logger _logger;

        public Logger()
            : base()
        {

            string dir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string caminhoArquivo = Path.Combine(dir, "\\NLog.config");

            try
            {
                LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration(caminhoArquivo);
            }
            catch
            {
                ///caminho fisico do arquivo, atualmente configurado para a Locaweb
                LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration("C:\\NLog.config");
            }

            _logger = LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// Realiza o Log em Nível de Debug
        /// </summary>
        /// <param name="classe">Objeto instanciado da classe</param>
        /// <param name="nomeFuncao">Nome da Função que está realizando a chamada</param>
        /// <param name="mensagem">Mensagem para o Log</param>
        public void Debug(object classe, string nomeFuncao, string mensagem)
        {
            if (classe is String)
            {
                _logger.Log(LogLevel.Debug, "[" + classe + "." + nomeFuncao + "] " + mensagem);
            }
            else
            {

                _logger.Log(LogLevel.Debug, "[" + classe.GetType().Namespace + classe.GetType().Name + "." + nomeFuncao + "] " + mensagem);
            }
        }

        /// <summary>
        /// Realiza o Log em Nível de Trace
        /// </summary>
        /// <param name="classe">Objeto instanciado da classe</param>
        /// <param name="nomeFuncao">Nome da Função que está realizando a chamada</param>
        /// <param name="mensagem">Mensagem para o Log</param>
        public void Trace(object classe, string nomeFuncao, string mensagem)
        {
            if (classe is String)
            {
                _logger.Log(LogLevel.Trace, "[" + classe + "." + nomeFuncao + "] " + mensagem);
            }
            else
            {

                _logger.Log(LogLevel.Trace, "[" + classe.GetType().Namespace + classe.GetType().Name + "." + nomeFuncao + "] " + mensagem);
            }
        }

        /// <summary>
        /// Realiza o Log em Nível de Informação
        /// </summary>
        /// <param name="classe">Objeto instanciado da classe</param>
        /// <param name="nomeFuncao">Nome da Função que está realizando a chamada</param>
        /// <param name="mensagem">Mensagem para o Log</param>
        public void Info(object classe, string nomeFuncao, string mensagem)
        {
            if (classe is String)
            {
                _logger.Log(LogLevel.Info, "[" + classe + "." + nomeFuncao + "] " + mensagem);
            }
            else
            {
                _logger.Log(LogLevel.Info, "[" + classe.GetType().Namespace + classe.GetType().Name + "." + nomeFuncao + "] " + mensagem);
            }
        }

        /// <summary>
        /// Realiza o Log em Nível de Alerta
        /// </summary>
        /// <param name="classe">Objeto instanciado da classe</param>
        /// <param name="nomeFuncao">Nome da Função que está realizando a chamada</param>
        /// <param name="mensagem">Mensagem para o Log</param>
        public void Warn(object classe, string nomeFuncao, string mensagem)
        {
            if (classe is String)
            {
                _logger.Log(LogLevel.Warn, "[" + classe + "." + nomeFuncao + "] " + mensagem);
            }
            else
            {
                _logger.Log(LogLevel.Warn, "[" + classe.GetType().Namespace + classe.GetType().Name + "." + nomeFuncao + "] " + mensagem);
            }
        }

        /// <summary>
        /// Realiza o Log em Nível de Erro
        /// </summary>
        /// <param name="classe">Objeto instanciado da classe</param>
        /// <param name="nomeFuncao">Nome da Função que está realizando a chamada</param>
        /// <param name="ex">Objeto instanciado da Exceção gerada</param>
        /// <param name="mensagemAdicional">Mensagem Adicional, caso necessário</param>
        public void Error(object classe, string nomeFuncao, Exception ex, string mensagemAdicional)
        {
            try
            {
                string nomeClasse;
                if (classe is String)
                {
                    nomeClasse = (string)classe;
                }
                else
                {
                    nomeClasse = classe.GetType().Namespace + classe.GetType().Name;
                }


                if (ex.Message.Contains("inner"))
                {
                    ex = ex.InnerException;
                    if (ex.Message.Contains("inner"))
                    {
                        _logger.Log(LogLevel.Error, "[" + nomeClasse + "." + nomeFuncao + "] " + mensagemAdicional + " - Erro: " + ex.InnerException.Message);
                    }
                    else
                    {
                        _logger.Log(LogLevel.Error, "[" + nomeClasse + "." + nomeFuncao + "] " + mensagemAdicional + " - Erro: " + ex.Message);
                    }
                }
                else
                {
                    _logger.Log(LogLevel.Error, "[" + nomeClasse + "." + nomeFuncao + "] " + mensagemAdicional + " - Erro: " + ex.Message);
                }
            }
            catch { }
        }

        /// <summary>
        /// Realiza o Log em Nível de Erro Fatal
        /// </summary>
        /// <param name="classe">Objeto instanciado da classe</param>
        /// <param name="nomeFuncao">Nome da Função que está realizando a chamada</param>
        /// <param name="ex">Objeto instanciado da Exceção gerada</param>
        /// <param name="mensagemAdicional">Mensagem Adicional, caso necessário</param>
        public void Fatal(object classe, string nomeFuncao, Exception ex, string mensagemAdicional)
        {
            try
            {
                string nomeClasse;
                if (classe is String)
                {
                    nomeClasse = (string)classe;
                }
                else
                {
                    nomeClasse = classe.GetType().Namespace + classe.GetType().Name;
                }


                if (ex.Message.Contains("inner"))
                {
                    ex = ex.InnerException;
                    if (ex.Message.Contains("inner"))
                    {
                        _logger.Log(LogLevel.Error, "[" + nomeClasse + "." + nomeFuncao + "] " + mensagemAdicional + " - Erro: " + ex.InnerException.Message);
                    }
                    else
                    {
                        _logger.Log(LogLevel.Error, "[" + nomeClasse + "." + nomeFuncao + "] " + mensagemAdicional + " - Erro: " + ex.Message);
                    }
                }
                else
                {
                    _logger.Log(LogLevel.Fatal, "[" + nomeClasse + "." + nomeFuncao + "] " + mensagemAdicional + " - Erro: " + ex.Message);
                }
            }
            catch { }
        }

        /// <summary>
        /// Realiza o Log em Nível de Erro, sem Mensagem Adicional
        /// </summary>
        /// <param name="classe">Objeto instanciado da classe</param>
        /// <param name="nomeFuncao">Nome da Função que está realizando a chamada</param>
        /// <param name="ex">Objeto instanciado da Exceção gerada</param>
        public void Error(object classe, string nomeFuncao, Exception ex)
        {
            try
            {
                string nomeClasse;
                if (classe is String)
                {
                    nomeClasse = (string)classe;
                }
                else
                {
                    nomeClasse = classe.GetType().Namespace + classe.GetType().Name;
                }

                if (ex.Message.Contains("Inner"))
                {
                    _logger.Log(LogLevel.Error, "[" + nomeClasse + "." + nomeFuncao + "] " + "Exception: " + ex.InnerException.Message);
                }
                else
                {
                    _logger.Log(LogLevel.Error, "[" + nomeClasse + "." + nomeFuncao + "] " + "Exception: " + ex.Message);
                }
            }
            catch { }
        }
    }
}
