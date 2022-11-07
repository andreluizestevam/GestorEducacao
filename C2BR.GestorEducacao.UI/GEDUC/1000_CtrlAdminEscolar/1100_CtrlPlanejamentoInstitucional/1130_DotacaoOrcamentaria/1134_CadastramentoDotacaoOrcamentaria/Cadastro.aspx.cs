//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: PLANEJAMENTO ESCOLAR (FINANCEIRO)
// OBJETIVO: CADASTRAMENTO DE GRUPO DE CONTAS CONTÁBIL
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1130_DotacaoOrcamentaria.F1134_CadastramentoDotacaoOrcamentaria
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
            if (Page.IsPostBack) return;

            CarregaGrupo();
            CarregaSubGrupo();
            CarregaOrdenador();
            CarregaOrigemFinanceira();

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                txtDtSituacao.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (LoginAuxili.CO_COL < 0)
	        {
		        AuxiliPagina.EnvioMensagemErro(this.Page, "Usuário Logado não está cadastrado como funcionário.");
                return;
	        }

            int coAnoRefer = int.Parse(txtAnoRefer.Text);
            int idSubGrupo = int.Parse(ddlGrupo.SelectedValue);
            int coDotacOrcam = int.Parse(txtNumDotacOrcam.Text);
            int idOrigeFinan = int.Parse(ddlOrigeFinan.Text);            

            TB305_DOTAC_ORCAM tb305 = RetornaEntidade();

            if (tb305 == null)
            {
//------------> Guarda se existe ocorrência para o ano, subgrupo e número de orçamento informado
                int ocorDotacOrcam = (from iTb305 in TB305_DOTAC_ORCAM.RetornaTodosRegistros()
                                      where iTb305.CO_ANO_REF == coAnoRefer && iTb305.TB261_SUBGRUPO.ID_SUBGRUPO == idSubGrupo
                                      && iTb305.CO_DOTAC_ORCAM == coDotacOrcam
                                      select iTb305).Count();

                if (ocorDotacOrcam > 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Dotação Orçamentária já cadastrada no sistema.");
                    return;
                }

                tb305 = new TB305_DOTAC_ORCAM();
                tb305.DT_CADAS_DOTAC_ORCAM = DateTime.Now;
            }
            else if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
            {
                int idDotacOrcam = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);
//------------> Guarda se existe ocorrência para o ano, subgrupo e número de orçamento informado; e diferente do ID informado pela QueryString
                int ocorDotacOrcam = (from iTb305 in TB305_DOTAC_ORCAM.RetornaTodosRegistros()
                                      where iTb305.CO_ANO_REF == coAnoRefer && iTb305.TB261_SUBGRUPO.ID_SUBGRUPO == idSubGrupo
                                      && iTb305.CO_DOTAC_ORCAM == coDotacOrcam && iTb305.ID_DOTAC_ORCAM != idDotacOrcam
                                      select iTb305).Count();

                if (ocorDotacOrcam > 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Dotação Orçamentária já cadastrada no sistema.");
                    return;
                }
            }

            tb305.CO_ANO_REF = coAnoRefer;
            tb305.TB261_SUBGRUPO = TB261_SUBGRUPO.RetornaPelaChavePrimaria(idSubGrupo);
            tb305.CO_DOTAC_ORCAM = coDotacOrcam;
            tb305.NO_TITUL_DOTAC_ORCAM = txtTitulDotacOrcam.Text;
            tb305.DE_DOTAC_ORCAM = txtDescricaoDotacOrcam.Text != "" ? txtDescricaoDotacOrcam.Text : null;
            tb305.SIGLA_DOTAC_ORCAM = txtSiglaDotacOrcam.Text.ToUpper();
            tb305.TB03_COLABOR = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL);
            tb305.TB304_ORIGE_FINAN = TB304_ORIGE_FINAN.RetornaPelaChavePrimaria(idOrigeFinan);
            tb305.VL_PLANE_DOTAC_ORCAM = decimal.Parse(txtValorPlanej.Text);
            tb305.DT_SITUA_DOTAC_ORCAM = DateTime.Now;
            tb305.CO_SITUA_DOTAC_ORCAM = ddlSituacao.SelectedValue;

            CurrentPadraoCadastros.CurrentEntity = tb305;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB305_DOTAC_ORCAM tb305 = RetornaEntidade();

            if (tb305 != null)
            {                
                tb305.TB261_SUBGRUPOReference.Load();
                tb305.TB261_SUBGRUPO.TB260_GRUPOReference.Load();
                tb305.TB304_ORIGE_FINANReference.Load();
                tb305.TB03_COLABORReference.Load();

                txtAnoRefer.Text = tb305.CO_ANO_REF.ToString();
                ddlGrupo.SelectedValue = tb305.TB261_SUBGRUPO.TB260_GRUPO.ID_GRUPO.ToString();
                CarregaSubGrupo();
                ddlGrupo.SelectedValue = tb305.TB261_SUBGRUPO.ID_SUBGRUPO.ToString();
                txtNumDotacOrcam.Text = tb305.CO_DOTAC_ORCAM.ToString();
                txtTitulDotacOrcam.Text = tb305.NO_TITUL_DOTAC_ORCAM;
                txtDescricaoDotacOrcam.Text = tb305.DE_DOTAC_ORCAM != null ? tb305.DE_DOTAC_ORCAM.ToString() : "";
                txtSiglaDotacOrcam.Text = tb305.SIGLA_DOTAC_ORCAM;
                ddlOrdenador.SelectedValue = tb305.TB03_COLABOR.CO_COL.ToString();
                ddlOrigeFinan.SelectedValue = tb305.TB304_ORIGE_FINAN.ID_ORIGE_FINAN.ToString();
                txtValorPlanej.Text = tb305.VL_PLANE_DOTAC_ORCAM.ToString("N2");
                txtValorExecu.Text = tb305.VL_EXECU_DOTAC_ORCAM != null ? tb305.VL_EXECU_DOTAC_ORCAM.Value.ToString("N2") : "";
                txtValorTrans.Text = tb305.VL_TRANS_DOTAC_ORCAM != null ? tb305.VL_TRANS_DOTAC_ORCAM.Value.ToString("N2") : "";
                txtDtUltimExecu.Text = tb305.DT_ULTI_EXECU_DOTAC_ORCAM != null ? tb305.DT_ULTI_EXECU_DOTAC_ORCAM.Value.ToString("dd/MM/yyyy") : "";
                txtDtUltimTrans.Text = tb305.DT_ULTI_TRANS_DOTAC_ORCAM != null ? tb305.DT_ULTI_TRANS_DOTAC_ORCAM.Value.ToString("dd/MM/yyyy") : "";
                ddlSituacao.SelectedValue = tb305.CO_SITUA_DOTAC_ORCAM;
                txtDtSituacao.Text = tb305.DT_SITUA_DOTAC_ORCAM.ToString("dd/MM/yyyy");
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB305_DOTAC_ORCAM</returns>
        private TB305_DOTAC_ORCAM RetornaEntidade()
        {
            return TB305_DOTAC_ORCAM.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Grupo
        /// </summary>
        private void CarregaGrupo()
        {
            ddlGrupo.DataSource = (from tb260 in TB260_GRUPO.RetornaTodosRegistros()
                                   where tb260.TP_GRUPO == "D"
                                   select new { tb260.NOM_GRUPO, tb260.ID_GRUPO });

            ddlGrupo.DataTextField = "NOM_GRUPO";
            ddlGrupo.DataValueField = "ID_GRUPO";
            ddlGrupo.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de SubGrupo
        /// </summary>
        private void CarregaSubGrupo()
        {
            int idGrupo = ddlGrupo.SelectedValue != "" ? int.Parse(ddlGrupo.SelectedValue) : 0;

            ddlSubGrupo.DataSource = (from tb261 in TB261_SUBGRUPO.RetornaTodosRegistros()
                                      select new { tb261.NOM_SUBGRUPO, tb261.ID_SUBGRUPO });

            ddlSubGrupo.DataTextField = "NOM_SUBGRUPO";
            ddlSubGrupo.DataValueField = "ID_SUBGRUPO";
            ddlSubGrupo.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Ordenador
        /// </summary>
        private void CarregaOrdenador()
        {
            ddlOrdenador.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(LoginAuxili.CO_EMP)
                                      select new { tb03.NO_COL, tb03.CO_COL });

            ddlOrdenador.DataTextField = "NO_COL";
            ddlOrdenador.DataValueField = "CO_COL";
            ddlOrdenador.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Origem Financeira
        /// </summary>
        private void CarregaOrigemFinanceira()
        {
            ddlOrigeFinan.DataSource = (from tb304 in TB304_ORIGE_FINAN.RetornaTodosRegistros()
                                        select new { tb304.DE_ORIGE_FINAN, tb304.ID_ORIGE_FINAN });

            ddlOrigeFinan.DataTextField = "DE_ORIGE_FINAN";
            ddlOrigeFinan.DataValueField = "ID_ORIGE_FINAN";
            ddlOrigeFinan.DataBind();
        }
        #endregion

        protected void ddlGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubGrupo();
        }
    }
}