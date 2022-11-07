//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: TABELAS DE APOIO OPERACIONAL SECRETARIA
// OBJETIVO: TIPOS DE BOLSA ESCOLAR
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2900_TabelasGenerCtrlOperSecretaria.F2903_TipoBolsaEscolar
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos
        
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            ///Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
            CurrentPadraoCadastros.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentPadraoCadastros_OnCarregaFormulario);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CarregaAgrupador();

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                txtDtCadas.Text = txtDtSituacao.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

        /// <summary>
        /// Chamada do método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        /// <summary>
        /// Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        /// </summary>
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (txtValorDescto.Text != "")
            {
                if ((Decimal.Parse(txtValorDescto.Text) > 100) && (chkValorDesctoPercentual.Checked) )
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Valor percentual não pode ser maior que 100.");
                    return;
                }
            }

            if (txtDtInicioBolsa.Text != "")
            {
                if (txtDtFinalBolsa.Text == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Data Fim deve ser informada.");
                    return;
                }
                else
                {
                    if (DateTime.Parse(txtDtInicioBolsa.Text) > DateTime.Parse(txtDtFinalBolsa.Text))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Data Início deve ser menor que Data Fim.");
                        return;
                    }
                }                
            }

            int coTipoBolsa = ddlGrupoBolsa.SelectedValue != "" ? int.Parse(ddlGrupoBolsa.SelectedValue) : 0;            

            TB148_TIPO_BOLSA tb148 = RetornaEntidade();

            tb148.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
            tb148.NO_TIPO_BOLSA = txtNome.Text;
            tb148.DE_TIPO_BOLSA = txtDescricao.Text != "" ? txtDescricao.Text : null;                   
            tb148.TP_GRUPO_BOLSA = ddlTipo.SelectedValue;
            tb148.VL_TIPO_BOLSA = txtValorDescto.Text != "" ? (decimal?)decimal.Parse(txtValorDescto.Text) : null;
            tb148.FL_TIPO_VALOR_BOLSA = chkValorDesctoPercentual.Checked ? "P" : "V";

            if (ddlGrupoBolsa.SelectedValue != "")
                tb148.TB317_AGRUP_BOLSA = TB317_AGRUP_BOLSA.RetornaPelaChavePrimaria(int.Parse(ddlGrupoBolsa.SelectedValue));

            tb148.DT_INICI_TIPO_BOLSA = txtDtInicioBolsa.Text != "" ? (DateTime?)DateTime.Parse(txtDtInicioBolsa.Text) : null;
            tb148.DT_FIM_TIPO_BOLSA = txtDtFinalBolsa.Text != "" ? (DateTime?)DateTime.Parse(txtDtFinalBolsa.Text) : null;
            tb148.DT_SITUA_TIPO_BOLSA = DateTime.Now;
            tb148.CO_SITUA_TIPO_BOLSA = ddlSituacao.SelectedValue;
            tb148.NOM_USUARIO = LoginAuxili.NOME_USU_LOGADO;
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                tb148.DT_CADAS_TIPO_BOLSA = DateTime.Now;

            CurrentPadraoCadastros.CurrentEntity = tb148;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB148_TIPO_BOLSA tb148 = RetornaEntidade();

            if (tb148 != null)
            {
                tb148.TB317_AGRUP_BOLSAReference.Load();

                ddlTipo.SelectedValue = tb148.TP_GRUPO_BOLSA;
                txtNome.Text = tb148.NO_TIPO_BOLSA;
                txtDescricao.Text = tb148.DE_TIPO_BOLSA;
                ddlGrupoBolsa.SelectedValue = tb148.TB317_AGRUP_BOLSA != null ? tb148.TB317_AGRUP_BOLSA.ID_AGRUP_BOLSA.ToString() : "";
                chkValorDesctoPercentual.Checked = tb148.FL_TIPO_VALOR_BOLSA == "P";
                txtValorDescto.Text = tb148.VL_TIPO_BOLSA != null ? String.Format("{0:N}", tb148.VL_TIPO_BOLSA) : "";
                txtDtInicioBolsa.Text = tb148.DT_INICI_TIPO_BOLSA != null ? tb148.DT_INICI_TIPO_BOLSA.Value.ToString("dd/MM/yyyy") : "";
                txtDtFinalBolsa.Text = tb148.DT_FIM_TIPO_BOLSA != null ? tb148.DT_FIM_TIPO_BOLSA.Value.ToString("dd/MM/yyyy") : "";
                txtDtCadas.Text = tb148.DT_CADAS_TIPO_BOLSA.ToString("dd/MM/yyyy");
                txtDtSituacao.Text = tb148.DT_SITUA_TIPO_BOLSA.ToString("dd/MM/yyyy");
                ddlSituacao.SelectedValue = tb148.CO_SITUA_TIPO_BOLSA;
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB148_TIPO_BOLSA</returns>
        private TB148_TIPO_BOLSA RetornaEntidade()
        {
            TB148_TIPO_BOLSA tb148 = TB148_TIPO_BOLSA.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb148 == null) ? new TB148_TIPO_BOLSA() : tb148;
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Grupos
        /// </summary>
        private void CarregaAgrupador()
        {
            ddlGrupoBolsa.DataSource = TB317_AGRUP_BOLSA.RetornaTodosRegistros().Where( c => c.CO_SITUA_AGRUP_BOLSA == "A" && c.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlGrupoBolsa.DataTextField = "NO_AGRUP_BOLSA";
            ddlGrupoBolsa.DataValueField = "ID_AGRUP_BOLSA";
            ddlGrupoBolsa.DataBind();

            ddlGrupoBolsa.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion
    }
}