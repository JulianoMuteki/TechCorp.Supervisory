using System;

namespace ASAS.Domain.Comum
{
    public class NaoLocalizadoException : Exception
    {
        public NaoLocalizadoException(object classe, long id, string funcao)
            : base("Nao Pode Localizar Id: " + id)
        {
            Log.Logger.Instancia.Debug(classe, funcao, this.Message);
        }
        public NaoLocalizadoException(object classe, Guid guid, string funcao)
            : base("Nao Pode Localizar Guid: " + guid.ToString())
        {
            Log.Logger.Instancia.Debug(classe, funcao, this.Message);
        }
        public NaoLocalizadoException(object classe, DateTime data, string funcao)
            : base("Nao Pode Localizar Data: " + data.ToString())
        {
            Log.Logger.Instancia.Debug(classe, funcao, this.Message);
        }
        public NaoLocalizadoException(object classe, string valor, string funcao)
            : base("Nao Pode Localizar: " + valor)
        {
            Log.Logger.Instancia.Debug(classe, funcao, this.Message);
        }
    }

    public class NaoPodeSalvarException<T> : Exception
    {
        public NaoPodeSalvarException(T repositorio, long id, string funcao)
            : base("Nao Pode Salvar Id: " + id)
        {
            Log.Logger.Instancia.Debug(repositorio, funcao, this.Message);
        }
    }

    public class AcessoNaoPermitidoException<T> : Exception
    {
        public AcessoNaoPermitidoException(T repositorio, string login, string funcao)
            : base("Acesso não permitido: " + login)
        {
            Log.Logger.Instancia.Debug(repositorio, funcao, this.Message);
        }

        public AcessoNaoPermitidoException(object classe, string numeroPedido, string lote, string codigoSistema, string funcao)
            : base("Acesso não permitido: " + numeroPedido + " " + lote + " " + " " + codigoSistema)
        {
            Log.Logger.Instancia.Debug(classe, funcao, this.Message);
        }
    }

    public class MatriculaJaCadastradoException<T> : Exception
    {
        public MatriculaJaCadastradoException(T classe, string login, string funcao)
            : base("Login Ja Cadastrado: " + login)
        {
            Log.Logger.Instancia.Debug(classe, funcao, this.Message);
        }
    }

    public class TipoPerfilUsuarioInvalidoException<T> : Exception
    {
        public TipoPerfilUsuarioInvalidoException(T classe, string tipo, string funcao)
            : base("Tipo Invalido: " + tipo)
        {
            Log.Logger.Instancia.Debug(classe, funcao, this.Message);
        }
    }

    public class CaracterSeparadorInvalidoException<T> : Exception
    {
        public CaracterSeparadorInvalidoException(T classe, string tipo, string funcao)
            : base("Caracter Invalido: " + tipo)
        {
            Log.Logger.Instancia.Debug(classe, funcao, this.Message);
        }
    }

    public class PedidoDeVendaNaoLiberadoException<T> : Exception
    {
        public PedidoDeVendaNaoLiberadoException(T classe, string numeroPedido, string funcao)
            : base("Pedido de Venda Nao Liberado: " + numeroPedido)
        {
            Log.Logger.Instancia.Debug(classe, funcao, this.Message);
        }
    }

    public class PedidoDeVendaNaoLocalizadoException<T> : Exception
    {
        public PedidoDeVendaNaoLocalizadoException(T classe, string numeroPedido, string funcao)
            : base("Pedido de Venda Nao Localizado: " + numeroPedido)
        {
            Log.Logger.Instancia.Debug(classe, funcao, this.Message);
        }
    }

    public class AtualizarItemPedidoVendaException<T> : Exception
    {
        public AtualizarItemPedidoVendaException(T classe, string dadosItemPedido, string funcao)
            : base("Item do Pedido de Venda Eh Diferente do Parametro Passado: " + dadosItemPedido)
        {
            Log.Logger.Instancia.Debug(classe, funcao, this.Message);
        }
    }

    public class NaoPodeAlterarStatusPedidoVendaException<T> : Exception
    {
        public NaoPodeAlterarStatusPedidoVendaException(T classe, string status, string funcao)
            : base("Nao e possivel alterar o status do PEDIDO, pois esta " + status)
        {
            Log.Logger.Instancia.Debug(classe, funcao, this.Message);
        }
    }

    public class SerializandoObjetoParaXMLException<T> : Exception
    {
        public SerializandoObjetoParaXMLException(T classe, string funcao)
            : base("Erro serializando o objeto " + classe.GetType().Name + " para XML")
        {
            Log.Logger.Instancia.Debug(classe, funcao, this.Message);
        }
    }

    public class DeserializandoObjetoParaXMLException<T> : Exception
    {
        public DeserializandoObjetoParaXMLException(T classe, string funcao)
            : base("Erro deserializando o objeto " + classe.GetType().Name + " para XML")
        {
            Log.Logger.Instancia.Debug(classe, funcao, this.Message);
        }
    }
}
