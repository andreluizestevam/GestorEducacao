//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: GESTÃO DE MOVIMENTAÇÃO FUNCIONAL
// OBJETIVO: CADASTRAMENTO DE INSTITUIÇÃO DE TRANSFERÊNCIA
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
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1230_CrtlMovimentacaoFuncional.F1232_InstituicaoTransferencia
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
            {
                CarregaUF();
                CarregaCidades();
                CarregaBairros();

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {
                }
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (Page.IsValid)
            {
                TB285_INSTIT_TRANSF tb285 = RetornaEntidade();

                if (tb285 == null)
                    tb285 = new TB285_INSTIT_TRANSF();

                tb285.NO_INSTIT_TRANSF = txtNome.Text;
                tb285.CO_SIGLA_INSTIT_TRANSF = txtSigla.Text;
                tb285.CO_CPFCGC_INSTIT_TRANSF = txtCNPJ.Text.Replace("-", "").Replace(".", "").Replace("/", "");
                tb285.CO_CEP_INSTIT_TRANSF = txtCEP.Text.Replace("-", "");
                tb285.DE_END_INSTIT_TRANSF = txtLogradouro.Text != "" ? txtLogradouro.Text : null;
                tb285.NU_END_INSTIT_TRANSF = txtNumero.Text != "" ? int.Parse(txtNumero.Text) : (int?)null;
                tb285.DE_COM_ENDE_INSTIT_TRANSF = txtComplemento.Text != "" ? txtComplemento.Text : null;
                tb285.TB905_BAIRRO = TB905_BAIRRO.RetornaPelaChavePrimaria(int.Parse(ddlBairro.SelectedValue));
                tb285.TB905_BAIRRO.TB904_CIDADE = TB904_CIDADE.RetornaPelaChavePrimaria(int.Parse(ddlCidade.SelectedValue));
                tb285.CO_UF_INSTIT_TRANSF = ddlUF.SelectedValue;
                tb285.CO_TEL1_INSTIT_TRANSF = txtTelefone.Text != "" ? txtTelefone.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
                tb285.CO_TEL2_INSTIT_TRANSF = txtTelefone2.Text != "" ? txtTelefone2.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
                tb285.CO_FAX_INSTIT_TRANSF = txtFax.Text != "" ? txtFax.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;

                CurrentPadraoCadastros.CurrentEntity = tb285;
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB285_INSTIT_TRANSF tb285 = RetornaEntidade();

            if (tb285 != null)
            {
                tb285.TB905_BAIRROReference.Load();
                tb285.TB905_BAIRRO.TB904_CIDADEReference.Load();

                txtNome.Text = tb285.NO_INSTIT_TRANSF;
                txtSigla.Text = tb285.CO_SIGLA_INSTIT_TRANSF;
                txtCNPJ.Text = tb285.CO_CPFCGC_INSTIT_TRANSF;
                txtCEP.Text = tb285.CO_CEP_INSTIT_TRANSF;
                txtLogradouro.Text = tb285.DE_END_INSTIT_TRANSF;
                txtNumero.Text = tb285.NU_END_INSTIT_TRANSF != null ? tb285.NU_END_INSTIT_TRANSF.ToString() : "";
                txtComplemento.Text = tb285.DE_COM_ENDE_INSTIT_TRANSF;
                ddlUF.SelectedValue = tb285.CO_UF_INSTIT_TRANSF;
                CarregaCidades();
                ddlCidade.SelectedValue = tb285.TB905_BAIRRO.TB904_CIDADE.CO_CIDADE.ToString();
                CarregaBairros();
                ddlBairro.SelectedValue = tb285.TB905_BAIRRO.CO_BAIRRO.ToString();
                txtTelefone.Text = tb285.CO_TEL1_INSTIT_TRANSF;
                txtTelefone2.Text = tb285.CO_TEL2_INSTIT_TRANSF;
                txtFax.Text = tb285.CO_FAX_INSTIT_TRANSF;
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB285_INSTIT_TRANSF</returns>
        private TB285_INSTIT_TRANSF RetornaEntidade()
        {
            return TB285_INSTIT_TRANSF.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de UF
        /// </summary>
        private void CarregaUF()
        {
            ddlUF.DataSource = TB74_UF.RetornaTodosRegistros();

            ddlUF.DataTextField = "CODUF";
            ddlUF.DataValueField = "CODUF";
            ddlUF.DataBind();

            ddlUF.SelectedValue = LoginAuxili.CO_UF_INSTITUICAO;
            ddlUF.Items.Insert(0, "");
        }

        /// <summary>
        /// Método que carrega o dropdown de Cidades
        /// </summary>
        private void CarregaCidades()
        {
            ddlCidade.DataSource = TB904_CIDADE.RetornaPeloUF(ddlUF.SelectedValue);

            ddlCidade.DataTextField = "NO_CIDADE";
            ddlCidade.DataValueField = "CO_CIDADE";
            ddlCidade.DataBind();

            ddlCidade.Enabled = ddlCidade.Items.Count > 0;
            ddlCidade.Items.Insert(0, "");
        }

        /// <summary>
        /// Método que carrega o dropdown de Bairros
        /// </summary>
        private void CarregaBairros()
        {
            int coCidade = ddlCidade.SelectedValue != "" ? int.Parse(ddlCidade.SelectedValue) : 0;

            if (coCidade == 0)
            {
                ddlBairro.Enabled = false;
                ddlBairro.Items.Clear();

                return;
            }

            ddlBairro.DataSource = TB905_BAIRRO.RetornaPelaCidade(coCidade);

            ddlBairro.DataTextField = "NO_BAIRRO";
            ddlBairro.DataValueField = "CO_BAIRRO";
            ddlBairro.DataBind();

            ddlBairro.Enabled = ddlBairro.Items.Count > 0;
            ddlBairro.Items.Insert(0, "");
        }
        #endregion

        protected void ddlUF_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCidades();
            CarregaBairros();
        }
        
        protected void ddlCidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaBairros();
        }        
    }
}