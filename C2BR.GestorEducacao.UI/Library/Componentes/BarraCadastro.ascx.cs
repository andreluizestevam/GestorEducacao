//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Objects.DataClasses;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.App_Masters;
using Resources;
using System.Web.UI.HtmlControls;
using System.Data;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.Library.Componentes
{
    public partial class BarraCadastro : System.Web.UI.UserControl
    {
        #region Propriedades

        public EntityObject Entity { get; set; }

        public LinkButton BtnSave
        {
            get;
            set;
        }

        public LinkButton BtnEdit
        {
            get;
            set;
        }

        public LinkButton BtnCancel
        {
            get;
            set;
        }

        public LinkButton BtnNewSearch
        {
            get { return this.btnNewSearch; }
            set { this.btnNewSearch = value; }
        }

        public string BtnEditText
        {
            get;
            set;
        }

        public string BtnEditImage
        {
            get;
            set;
        }

        public string BtnSaveText
        {
            get;
            set;
        }

        public string BtnSaveImage
        {
            get;
            set;
        }

        public string BtnNewSearchText
        {
            get;
            set;
        }

        public string BtnNewSearchImage
        {
            get;
            set;
        }

        public string AcaoSolicitadaClique = string.Empty;

        public string botaoDelete = string.Empty;

        public string botaoSave = string.Empty;

        public string botaoNewSearch = string.Empty;

        RegistroLog registroLog = new RegistroLog();
        #endregion

        #region Eventos

        #region Eventos (Declaração)

        public delegate void OnOverrideMessageHandler();
        public event OnOverrideMessageHandler OnOverrideMessage;

        public delegate void OnActionHandler();
        public event OnActionHandler OnAction;

        public delegate void OnNewSearchHandler();
        public event OnNewSearchHandler OnNewSearch;

        public delegate void OnDeleteHandler(EntityObject entity);
        public event OnDeleteHandler OnDelete;

        public delegate void OnLoadHandler();
        public event OnLoadHandler OnLoaded;

        #endregion

        #region Eventos da Página

        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            botaoDelete = btnDelete.ID;
            botaoNewSearch = btnNewSearch.ID;
            botaoSave = btnSave.ID;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoExclusao))
                    CurrentPadraoCadastros.DefineMensagem(MensagensAjuda.MessagemCampoObrigatorio, MensagensAjuda.MessagemEdicao);
                else if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                    CurrentPadraoCadastros.DefineMensagem(MensagensAjuda.MessagemCampoObrigatorio, MensagensAjuda.MessagemEdicao);

                if (OnOverrideMessage != null)
                    OnOverrideMessage();
            }
            if (OnLoaded != null)
                OnLoaded();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.AcaoSolicitadaClique = ((LinkButton)sender).ID;
            int idUsuario = ADMUSUARIO.RetornaPelaChavePrimaria(LoginAuxili.IDEADMUSUARIO).ideAdmUsuario;

            //if (idUsuario != 27)
            //{
            //    //AuxiliPagina.RedirecionaParaPaginaErro("Usuário não possui permissão para executar tal ação.", Request.Url.AbsoluteUri);
            //    AuxiliPagina.RedirecionaParaPaginaErro("Versão demonstrativa - Comando desabilitado.", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
            //}

            if (LoginAuxili.ORG_CODIGO_ORGAO == 0)
                Page.ClientScript.RegisterStartupScript(this.GetType(), "LogOut", "javascript:parent.LogOut();", true);

            if (OnAction != null)
                OnAction();

            if (Entity != null && Entity.EntityState != EntityState.Unchanged)
            {
                if (Page.IsValid)
                {
                    string message = "Registro editado com sucesso!";

                    if (Entity.EntityState == System.Data.EntityState.Detached ||
                        Entity.EntityState == System.Data.EntityState.Added)
                    {
                        message = "Registro adicionado com sucesso!";
                    }

                    if (GestorEntities.SaveOrUpdate(Entity) > 0)
                    {
                        registroLog.RegistroLOG(Entity, RegistroLog.ACAO_GRAVAR);

                        AuxiliPagina.RedirecionaParaPaginaSucesso(message, AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
                    }
                    else
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao salvar registro.", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
                }
            }
            else if (Entity != null && Entity.EntityState == EntityState.Unchanged)
            {
                AuxiliPagina.RedirecionaParaPaginaSucesso("Operação realizada com sucesso!", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
            }
        }

        protected void btnNewSearch_Click(object sender, EventArgs e)
        {
            this.AcaoSolicitadaClique = ((LinkButton)sender).ID;
            if (OnNewSearch != null)
                OnNewSearch();
            else
                AuxiliPagina.RedirecionaParaPaginaBusca();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            this.AcaoSolicitadaClique = ((LinkButton)sender).ID;
            //int idUsuario = ADMUSUARIO.RetornaPelaChavePrimaria(LoginAuxili.IDEADMUSUARIO).ideAdmUsuario;

            //if (idUsuario != 27)
            //{
            //    AuxiliPagina.RedirecionaParaPaginaErro("Versão demonstrativa - Comando desabilitado.", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
            //    //AuxiliPagina.RedirecionaParaPaginaErro("Usuário não possui permissão para executar tal ação.", Request.Url.AbsoluteUri);
            //}
            
            if (OnAction != null)
                OnAction();

            if (Entity != null)
            {
                if (Page.IsValid)
                {
                    try
                    {
                        if (OnDelete != null)
                            OnDelete(Entity);
                        else
                            GestorEntities.Delete(Entity);

                        registroLog.RegistroLOG(Entity, RegistroLog.ACAO_DELETE);
                        AuxiliPagina.RedirecionaParaPaginaSucesso("Registro excluído com sucesso!", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
                    }
                    catch (UpdateException)
                    {
                        //GestorEntities.CurrentContext = new GestorEntities();
                        AuxiliPagina.EnvioMensagemErro(Page, "O registro atual faz referência à outros registros e não pode ser excluído.");
                    }
                }
            }
        }

        #endregion

        #region metodos
        /// <summary>
        /// Habilita e desabilita os itens
        /// </summary>
        /// <param name="nomeBotao">Nome do item a ser usado</param>
        /// <param name="tipo">tipo true para habilitar e false para desabilitar</param>
        public void HabilitarBotoes(string nomeBotao, bool tipo)
        {
            if (btnDelete.ID == nomeBotao)
                btnDelete.Enabled = tipo;
            else if (btnSave.ID == nomeBotao)
                btnSave.Enabled = tipo;
            else if (btnNewSearch.ID == nomeBotao)
                btnNewSearch.Enabled = tipo;
        }
        #endregion
        #endregion
    }
}