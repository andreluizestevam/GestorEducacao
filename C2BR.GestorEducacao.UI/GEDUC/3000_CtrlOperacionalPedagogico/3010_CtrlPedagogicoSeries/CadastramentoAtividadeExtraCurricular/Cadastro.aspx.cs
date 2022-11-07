//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: CONTROLE DE SÉRIES OU CURSOS
// OBJETIVO: CADASTRAMENTO DE ATIVIDADES EXTRA CURRICULAR.
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3010_CtrlPedagogicoSeries.CadastramentoAtividadeExtraCurricular
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
            if (!IsPostBack)
            {
                CarregaAgrupador();
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                    CarregaFormulario();
                else
                    txtDtSituacao.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            decimal decimalRetorno;
            TB105_ATIVIDADES_EXTRAS tb105 = RetornaEntidade();

            if (tb105 == null)
                tb105 = new TB105_ATIVIDADES_EXTRAS();

            tb105.TB318_AGRUP_ATIVEXTRA = ddlGrupoAtiv.SelectedValue != "" ? TB318_AGRUP_ATIVEXTRA.RetornaPelaChavePrimaria(int.Parse(ddlGrupoAtiv.SelectedValue)) : null;
            tb105.DES_ATIV_EXTRA = txtDescricao.Text;
            tb105.SIGLA_ATIV_EXTRA = txtSigla.Text;
            tb105.VL_ATIV_EXTRA = decimal.TryParse(txtValor.Text, out decimalRetorno) ? (decimal?)decimalRetorno : decimal.Zero;
            tb105.VL_DESCTO_EXTRA = decimal.TryParse(txtValorDescto.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
            tb105.FL_TIPO_VL_DESCTO_EXTRA = chkValorDesctoPercentual.Checked ? "P" : "V";
            tb105.CO_SITUA_ATIV_EXTRA = ddlSituacao.SelectedValue;
            tb105.DT_SITUA_ATIV_EXTRA = DateTime.Now;

            CurrentPadraoCadastros.CurrentEntity = tb105;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB105_ATIVIDADES_EXTRAS tb105 = RetornaEntidade();

            if (tb105 != null)
            {
                tb105.TB318_AGRUP_ATIVEXTRAReference.Load();

                ddlGrupoAtiv.SelectedValue = tb105.TB318_AGRUP_ATIVEXTRA != null ? tb105.TB318_AGRUP_ATIVEXTRA.ID_AGRUP_ATIVEXTRA.ToString() : "";
                txtDescricao.Text = tb105.DES_ATIV_EXTRA;
                txtSigla.Text = tb105.SIGLA_ATIV_EXTRA;
                txtValor.Text = tb105.VL_ATIV_EXTRA != null ? tb105.VL_ATIV_EXTRA.Value.ToString("0.00") : "0";
                txtValorDescto.Text = tb105.VL_DESCTO_EXTRA.ToString();
                chkValorDesctoPercentual.Checked = tb105.FL_TIPO_VL_DESCTO_EXTRA == "P";
                ddlSituacao.SelectedValue = tb105.CO_SITUA_ATIV_EXTRA.ToString();
                txtDtSituacao.Text = tb105.DT_SITUA_ATIV_EXTRA.ToString("dd/MM/yyyy");
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB105_ATIVIDADES_EXTRAS</returns>
        private TB105_ATIVIDADES_EXTRAS RetornaEntidade()
        {
            return TB105_ATIVIDADES_EXTRAS.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Agrupador
        /// </summary>
        private void CarregaAgrupador()
        {
            ddlGrupoAtiv.DataSource = TB318_AGRUP_ATIVEXTRA.RetornaTodosRegistros().Where(c => c.CO_SITUA_AGRUP_ATIVEXTRA == "A" && c.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlGrupoAtiv.DataTextField = "DE_AGRUP_ATIVEXTRA";
            ddlGrupoAtiv.DataValueField = "ID_AGRUP_ATIVEXTRA";
            ddlGrupoAtiv.DataBind();

            ddlGrupoAtiv.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion
    }
}
