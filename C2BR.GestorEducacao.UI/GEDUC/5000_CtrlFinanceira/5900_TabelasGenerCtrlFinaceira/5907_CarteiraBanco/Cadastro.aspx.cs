//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: **********************
// SUBMÓDULO: *******************
// OBJETIVO: ********************
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5900_TabelasGenerCtrlFinaceira.F5907_CarteiraBanco
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos
        
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

//--------> Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
            CurrentPadraoCadastros.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentPadraoCadastros_OnCarregaFormulario);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                CarregaBancos();
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (Page.IsValid)
            {
                string strIdeBanco = ddlBanco.SelectedValue;
                TB226_CARTEIRA_BANCO tb226 = RetornaEntidade();

                if (tb226 == null)
                    tb226 = new TB226_CARTEIRA_BANCO();

                TB226_CARTEIRA_BANCO ocorTb226 = (from lTb226 in TB226_CARTEIRA_BANCO.RetornaTodosRegistros()
                                                  where lTb226.TB29_BANCO.IDEBANCO == strIdeBanco
                                                  && lTb226.CO_CARTEIRA == txtCodigoCarteira.Text
                                                  select lTb226).FirstOrDefault();

                if (ocorTb226 != null && QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Carteira informada já existe para esse Banco.");
                    return;
                }

                tb226.TB29_BANCO = TB29_BANCO.RetornaPelaChavePrimaria(strIdeBanco);
                tb226.CO_CARTEIRA = txtCodigoCarteira.Text.Trim();
                tb226.DE_CARTEIRA = txtDescricao.Text.Trim();

                CurrentPadraoCadastros.CurrentEntity = tb226;
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB226_CARTEIRA_BANCO tb226 = RetornaEntidade();

            if (tb226 != null)
            {
                tb226.TB29_BANCOReference.Load();
                ddlBanco.SelectedValue = tb226.TB29_BANCO.IDEBANCO;
                txtCodigoCarteira.Text = tb226.CO_CARTEIRA;
                txtDescricao.Text = tb226.DE_CARTEIRA;
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB226_CARTEIRA_BANCO</returns>
        private TB226_CARTEIRA_BANCO RetornaEntidade()
        {
            return TB226_CARTEIRA_BANCO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringPelaChave("ideBanco"), QueryStringAuxili.RetornaQueryStringPelaChave("coCarteira"));
        }        
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Bancos
        /// </summary>
        private void CarregaBancos()
        {
            ddlBanco.DataSource = (from tb29 in TB29_BANCO.RetornaTodosRegistros().AsEnumerable()
                                   select new { tb29.IDEBANCO, DESBANCO = string.Format("{0} - {1}", tb29.IDEBANCO, tb29.DESBANCO) }).OrderBy(b => b.DESBANCO);

            ddlBanco.DataValueField = "IDEBANCO";
            ddlBanco.DataTextField = "DESBANCO";
            ddlBanco.DataBind();
        }
        #endregion
    }
}
