//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL DE ESTOQUE
// SUBMÓDULO: CONTROLE OPERACIONAL DE ITENS DE ESTOQUE
// OBJETIVO: CADASTRO DE FORNECEDORES DE PRODUTOS E SERVIÇOS
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
namespace C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6200_CtrlOperMateriais.F6230_CtrlOperacionalItensEstoque.F6234_CadastroFornecedorProdServ
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
            CompareValidatorDataAtual.ValueToCompare = DateTime.Now.ToShortDateString();

            if (!Page.IsPostBack)
            {
                CarregaTipoUnidade();
                formularioEndereco.DdlUf.Enabled = formularioEndereco.DdlCidade.Enabled =
                formularioEndereco.DdlBairro.Enabled = formularioEndereco.TxtLogradouro.Enabled = true;

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {
                    string dataAtual = DateTime.Now.ToString("dd/MM/yyyy");
                    txtDtCadastro.Text = txtDtStatus.Text = dataAtual;
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
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {
                    string strCNPJ = txtCNPJ.Text.Replace("-", "").Replace(".", "").Replace("/", "");

                    var ocor = (from iTb41 in TB41_FORNEC.RetornaTodosRegistros()
                                where iTb41.CO_CPFCGC_FORN == strCNPJ && iTb41.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                select new { iTb41.CO_FORN }).FirstOrDefault();

                    if (ocor != null)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "CNPJ já cadastrado.");
                        return;
                    }
                }
                else if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                {
                    string strCNPJ = txtCNPJ.Text.Replace("-", "").Replace(".", "").Replace("/", "");
                    int coForn = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);

                    var ocor = (from iTb41 in TB41_FORNEC.RetornaTodosRegistros()
                                where iTb41.CO_CPFCGC_FORN == strCNPJ && iTb41.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                && iTb41.CO_FORN != coForn
                                select new { iTb41.CO_FORN }).FirstOrDefault();

                    if (ocor != null)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "CNPJ já cadastrado.");
                        return;
                    }
                }

                TB41_FORNEC tb41 = RetornaEntidade();                

                if (tb41 == null)
                    tb41 = new TB41_FORNEC();
                
                tb41.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
                tb41.TB24_TPEMPRESA = ddlTipoCategoria.SelectedValue != "" ? TB24_TPEMPRESA.RetornaPelaChavePrimaria(int.Parse(ddlTipoCategoria.SelectedValue)) : null;
                tb41.TP_FORN = ddlTipo.SelectedValue;
                tb41.NO_FAN_FOR = txtNome.Text;
                tb41.NO_SIGLA_FORN = txtSigla.Text != "" ? txtSigla.Text : null;
                tb41.DE_RAZSOC_FORN = txtRazaoSocial.Text;                
                tb41.CO_CPFCGC_FORN = txtCNPJ.Text.Replace("-", "").Replace(".", "").Replace("/", "");
                tb41.CO_INS_EST_FORN = txtInscEstadual.Text != "" ? txtInscEstadual.Text : null;
                tb41.CO_CEP_FORN = formularioEndereco.TxtCep.Text.Replace("-", "");
                tb41.DE_END_FORN = formularioEndereco.TxtLogradouro.Text != "" ? formularioEndereco.TxtLogradouro.Text : null;
                tb41.NU_END_FORN = formularioEndereco.TxtNumero.Text != "" ? (int?)int.Parse(formularioEndereco.TxtNumero.Text) : null;
                tb41.DE_COM_FORN = formularioEndereco.TxtComplemento.Text != "" ? formularioEndereco.TxtComplemento.Text : null;
                tb41.TB905_BAIRRO = TB905_BAIRRO.RetornaPelaChavePrimaria(int.Parse(formularioEndereco.DdlBairro.SelectedValue));
                tb41.TB905_BAIRRO.TB904_CIDADE = TB904_CIDADE.RetornaPelaChavePrimaria(int.Parse(formularioEndereco.DdlCidade.SelectedValue));
                tb41.CO_UF_FORN = formularioEndereco.DdlUf.SelectedValue;
                tb41.CO_TEL1_FORN = txtTelefone.Text != "" ? txtTelefone.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
                tb41.CO_TEL2_FORN = txtTelefone2.Text != "" ? txtTelefone2.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
                tb41.DE_EMAIL_CLI = txtEmail.Text != "" ? txtEmail.Text : null;
                tb41.CO_FAX_FORN = txtFax.Text != "" ? txtFax.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
                tb41.NO_CONTAT_FORN = txtNomeContato.Text != "" ? txtNomeContato.Text : null;
                tb41.DE_CARGO_CONTAT = txtCargoContato.Text != "" ? txtCargoContato.Text : null;
                tb41.CO_TEL_CELU_CONT_FORN = txtTelCelularContato.Text != "" ? txtTelCelularContato.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
                tb41.CO_TEL_FIXO_CONT_FORN = txtTelFixoContato.Text != "" ? txtTelFixoContato.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
                tb41.DE_EMAIL_CONT_FORN = txtEmailContato.Text != "" ? txtEmailContato.Text : null;                
                tb41.DE_WEB_FORN = txtWebSite.Text != "" ? txtWebSite.Text : null;
                tb41.DE_OBS_FORN = txtObservacao.Text != "" ? txtObservacao.Text : null;
                tb41.CO_SIT_FORN = ddlStatus.SelectedValue;
                tb41.DT_SIT_FORN = DateTime.Parse(txtDtStatus.Text);
                tb41.DT_CAD_FORN = DateTime.Parse(txtDtCadastro.Text);                

                CurrentPadraoCadastros.CurrentEntity = tb41;
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB41_FORNEC tb41 = RetornaEntidade();

            if (tb41 != null)
            {
                tb41.TB24_TPEMPRESAReference.Load();
                tb41.TB905_BAIRROReference.Load();
                tb41.TB905_BAIRRO.TB904_CIDADEReference.Load();

                ddlTipo.SelectedValue = tb41.TP_FORN;
                txtNome.Text = tb41.NO_FAN_FOR;
                txtRazaoSocial.Text = tb41.DE_RAZSOC_FORN;
                ddlTipoCategoria.SelectedValue = tb41.TB24_TPEMPRESA != null ? tb41.TB24_TPEMPRESA.CO_TIPOEMP.ToString() : "";
                txtCNPJ.Text = tb41.CO_CPFCGC_FORN;
                txtSigla.Text = tb41.NO_SIGLA_FORN;
                txtInscEstadual.Text = tb41.CO_INS_EST_FORN;
                formularioEndereco.TxtCep.Text = tb41.CO_CEP_FORN;
                formularioEndereco.TxtLogradouro.Text = tb41.DE_END_FORN;                
                formularioEndereco.TxtComplemento.Text = tb41.DE_COM_FORN;
                formularioEndereco.CarregaUfs();
                formularioEndereco.DdlUf.SelectedValue = tb41.CO_UF_FORN;
                formularioEndereco.CarregaCidades();
                formularioEndereco.DdlCidade.SelectedValue = tb41.TB905_BAIRRO.TB904_CIDADE.CO_CIDADE.ToString();
                formularioEndereco.CarregaBairros();
                formularioEndereco.DdlBairro.SelectedValue = tb41.TB905_BAIRRO.CO_BAIRRO.ToString();
                formularioEndereco.TxtNumero.Text = tb41.NU_END_FORN.ToString();
                txtTelefone.Text = tb41.CO_TEL1_FORN;
                txtTelefone2.Text = tb41.CO_TEL2_FORN;
                txtFax.Text = tb41.CO_FAX_FORN;
                txtWebSite.Text = tb41.DE_WEB_FORN;
                txtEmail.Text = tb41.DE_EMAIL_CLI;
                txtObservacao.Text = tb41.DE_OBS_FORN;
                ddlStatus.SelectedValue = tb41.CO_SIT_FORN;
                txtDtStatus.Text = tb41.DT_SIT_FORN.ToString("dd/MM/yyyy");
                txtDtCadastro.Text = tb41.DT_CAD_FORN.ToString("dd/MM/yyyy");

                txtNomeContato.Text = tb41.NO_CONTAT_FORN;
                txtCargoContato.Text = tb41.DE_CARGO_CONTAT;
                txtTelCelularContato.Text = tb41.CO_TEL_CELU_CONT_FORN;
                txtTelFixoContato.Text = tb41.CO_TEL_FIXO_CONT_FORN;
                txtEmailContato.Text = tb41.DE_EMAIL_CONT_FORN;
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB41_FORNEC</returns>
        private TB41_FORNEC RetornaEntidade()
        {
            return TB41_FORNEC.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Tipo de Unidade
        /// </summary>
        private void CarregaTipoUnidade()
        {
            ddlTipoCategoria.DataSource = TB24_TPEMPRESA.RetornaTodosRegistros().Where(t => t.CL_CLAS_EMP.Equals("F"));

            ddlTipoCategoria.DataTextField = "NO_TIPOEMP";
            ddlTipoCategoria.DataValueField = "CO_TIPOEMP";
            ddlTipoCategoria.DataBind();

            ddlTipoCategoria.Items.Insert(0, "");
        }
        #endregion       
    }
}