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
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5900_TabelasGenerCtrlFinaceira.F5911_LocalCobranca
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
                CarregaTipoEmpresa();
                CarregaUfs();
                CarregaCidades();
                CarregaBairros();

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoExclusao) || QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                    CarregaFormulario();
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int coTipoEmp = ddlTipoEmpresa.SelectedValue != "" ? int.Parse(ddlTipoEmpresa.SelectedValue) : 0;
            int coBairro = ddlBairro.SelectedValue != "" ? int.Parse(ddlBairro.SelectedValue) : 0;

            TB101_LOCALCOBRANCA tb101 = RetornaEntidade();

            if (tb101 == null)
                tb101 = new TB101_LOCALCOBRANCA();

            tb101.CO_CEP_COB = txtCep.Text.Replace("-", "");
            tb101.CO_CPFCGC_COB = txtCpfCnpj.Text.Replace(".", "").Replace("-", "").Replace("/", "");
            tb101.CO_FAX_COB = txtFax.Text.Replace("-", "").Replace("(", "").Replace(")", "");
            tb101.CO_INS_EST_COB = txtInscricaoEstadual.Text;
            tb101.CO_INS_MUN_COB = txtInscricaoMunicipal.Text;
            tb101.CO_SIT_COB = rblSituacao.SelectedValue;
            tb101.CO_TEL1_COB = txtTelefone1.Text.Replace("-", "").Replace("(", "").Replace(")", "");
            tb101.CO_TEL2_COB = txtTelefone2.Text.Replace("-", "").Replace("(", "").Replace(")", "");
            tb101.CO_UF_COB = ddlUf.SelectedValue;
            tb101.DE_COM_COB = txtComplemento.Text;
            tb101.DE_EMAIL_COB = txtEmail.Text;
            tb101.DE_END_COB = txtEndereco.Text;
            tb101.DE_OBS_COB = txtObservacao.Text;
            tb101.DE_RAZSOC_COB = txtRazaoSocial.Text;
            tb101.DE_WEB_COB = txtHomePage.Text;
            tb101.DT_CAD_COB = DateTime.Parse(txtDataInclusao.Text);
            tb101.DT_SIT_COB = DateTime.Parse(txtDataSituacao.Text);
            tb101.NO_FAN_COB = txtNomeFantasia.Text;
            tb101.TB24_TPEMPRESA = TB24_TPEMPRESA.RetornaPelaChavePrimaria(coTipoEmp);
            tb101.TB905_BAIRRO = TB905_BAIRRO.RetornaPelaChavePrimaria(coBairro);

            CurrentPadraoCadastros.CurrentEntity = tb101;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB101_LOCALCOBRANCA tb101 = RetornaEntidade();

            if (tb101 != null)
            {
                txtCep.Text = tb101.CO_CEP_COB;
                txtCpfCnpj.Text = tb101.CO_CPFCGC_COB;
                txtFax.Text = tb101.CO_FAX_COB;
                txtInscricaoEstadual.Text = tb101.CO_INS_EST_COB;
                txtInscricaoMunicipal.Text = tb101.CO_INS_MUN_COB;
                rblSituacao.SelectedValue = tb101.CO_SIT_COB;
                txtTelefone1.Text = tb101.CO_TEL1_COB;
                txtTelefone2.Text = tb101.CO_TEL2_COB;
                ddlUf.SelectedValue = tb101.CO_UF_COB;
                CarregaCidades();
                ddlCidade.SelectedValue = tb101.TB905_BAIRRO.CO_CIDADE.ToString();
                txtComplemento.Text = tb101.DE_COM_COB;
                txtEmail.Text = tb101.DE_EMAIL_COB;
                txtEndereco.Text = tb101.DE_END_COB;
                txtObservacao.Text = tb101.DE_OBS_COB;
                txtRazaoSocial.Text = tb101.DE_RAZSOC_COB;
                txtHomePage.Text = tb101.DE_WEB_COB;
                txtDataInclusao.Text = tb101.DT_CAD_COB.ToString("dd/MM/yyyy");
                txtDataSituacao.Text = tb101.DT_SIT_COB.ToString("dd/MM/yyyy");
                txtNomeFantasia.Text = tb101.NO_FAN_COB;
                ddlTipoEmpresa.SelectedValue = tb101.TB24_TPEMPRESA.CO_TIPOEMP.ToString();
                CarregaBairros();
                ddlBairro.SelectedValue = tb101.TB905_BAIRRO.CO_BAIRRO.ToString();
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB101_LOCALCOBRANCA</returns>
        private TB101_LOCALCOBRANCA RetornaEntidade()
        {
            return TB101_LOCALCOBRANCA.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Tipo de Empresa
        /// </summary>
        private void CarregaTipoEmpresa()
        {
            ddlTipoEmpresa.DataSource = TB24_TPEMPRESA.RetornaTodosRegistros();

            ddlTipoEmpresa.DataTextField = "NO_TIPOEMP";
            ddlTipoEmpresa.DataValueField = "CO_TIPOEMP";
            ddlTipoEmpresa.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de UFs
        /// </summary>
        private void CarregaUfs()
        {
            ddlUf.DataSource = TB74_UF.RetornaTodosRegistros();

            ddlUf.DataTextField = "CODUF";
            ddlUf.DataValueField = "CODUF";
            ddlUf.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Cidades
        /// </summary>
        private void CarregaCidades()
        {
            ddlCidade.DataSource = TB904_CIDADE.RetornaPeloUF(ddlUf.SelectedValue);

            ddlCidade.DataTextField = "NO_CIDADE";
            ddlCidade.DataValueField = "CO_CIDADE";
            ddlCidade.DataBind();

            ddlCidade.Enabled = ddlCidade.Items.Count > 0;
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
        }
        #endregion

        #region Validadores

        protected void cvCpfCnpj_ServerValidate(object source, ServerValidateEventArgs e)
        {
            string strCpfCnpj = e.Value.Replace(".", "").Replace("-", "").Replace("/", "");

            if (strCpfCnpj.Length == 11)
                e.IsValid = AuxiliValidacao.ValidaCpf(strCpfCnpj);
            else if (strCpfCnpj.Length == 14)
                e.IsValid = AuxiliValidacao.ValidaCnpj(strCpfCnpj);
            else
                e.IsValid = false;
        }
        #endregion

        protected void ddlUf_SelectedIndexChanged(object sender, EventArgs e)
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
