//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE RECEITAS E DESPESAS FIXAS
// OBJETIVO: CADASTRAMENTO DE DADOS CADASTRAIS DE ORIGENS DE RECEITAS FIXAS EXTERNAS
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
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5400_CtrlReceitaDespesaFixa.F5401_CadastramentoDadoOrigReceitaFixaExt
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

            if (Page.IsPostBack)
                return;

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                string dataAtual = DateTime.Now.ToString("dd/MM/yyyy");
                txtDtCadastro.Text = txtDtStatus.Text = dataAtual;
            }

            CarregaTiposCategoria();
            formularioEndereco.DdlUf.Enabled = formularioEndereco.DdlCidade.Enabled = 
            formularioEndereco.DdlBairro.Enabled = formularioEndereco.TxtLogradouro.Enabled = true;
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TB103_CLIENTE tb103 = RetornaEntidade();
            TB104_CONT_CLIENTE tb104 = RetornaEntidade2();

            if (tb103 == null)
                tb103 = new TB103_CLIENTE();

            if (QueryStringAuxili.OperacaoCorrenteQueryString == QueryStrings.OperacaoInsercao
                || QueryStringAuxili.OperacaoCorrenteQueryString == QueryStrings.OperacaoAlteracao)
            {
                tb103.CO_CLASS_EMP = ddlTipoCategoria.SelectedValue;
                tb103.TP_CLIENTE = ddlTipo.SelectedValue;
                tb103.CO_CPFCGC_CLI = txtCNPJ.Text.Replace("-", "").Replace(".", "").Replace("/", "");
                tb103.CO_INS_EST_CLI = txtInscEstadual.Text != "" ? txtInscEstadual.Text : null;
                tb103.DE_RAZSOC_CLI = txtRazaoSocial.Text;
                tb103.NO_SIGLA_CLIEN = txtSigla.Text;
                tb103.NO_FAN_CLI = txtNome.Text;
                tb103.CO_TEL1_CLI = txtTelefone.Text != "" ? txtTelefone.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
                tb103.CO_TEL2_CLI = txtTelefone2.Text != "" ? txtTelefone2.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
                tb103.CO_FAX_CLI = txtFax.Text != "" ? txtFax.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
                tb103.DE_EMAIL_CLI = txtEmail.Text != "" ? txtEmail.Text : null;
                tb103.DE_WEB_CLI = txtWebSite.Text != "" ? txtWebSite.Text : null;
                tb103.NU_END_CLI = formularioEndereco.TxtNumero.Text != "" ? (int?)int.Parse(formularioEndereco.TxtNumero.Text) : null;
                tb103.CO_CEP_CLI = formularioEndereco.TxtCep.Text.Replace("-", "");
                tb103.DE_END_CLI = formularioEndereco.TxtLogradouro.Text != "" ? formularioEndereco.TxtLogradouro.Text : null;
                tb103.DE_COM_CLI = formularioEndereco.TxtComplemento.Text != "" ? formularioEndereco.TxtComplemento.Text : null;
                tb103.CO_UF_CLI = formularioEndereco.DdlUf.SelectedValue;
                tb103.TB905_BAIRRO = TB905_BAIRRO.RetornaPelaChavePrimaria(int.Parse(formularioEndereco.DdlBairro.SelectedValue));
                tb103.TB905_BAIRRO.TB904_CIDADE = TB904_CIDADE.RetornaPelaChavePrimaria(int.Parse(formularioEndereco.DdlCidade.SelectedValue));
                tb103.NO_CONTAT_CLIEN = txtNomeContato.Text;
                tb103.DE_CARGO_CONTAT = txtCargoContato.Text;
                tb103.DE_OBS_CLI = txtObservacao.Text != "" ? txtObservacao.Text : null;
                tb103.CO_SIT_CLI = ddlStatus.SelectedValue;
                tb103.DT_SIT_CLI = DateTime.Now;
                tb103.DT_CAD_CLI = DateTime.Parse(txtDtCadastro.Text);

                if (tb103.DT_SIT_CLI < tb103.DT_CAD_CLI)
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Data da situação deve ser posterior ou igual a data de cadastro");

                if (QueryStringAuxili.OperacaoCorrenteQueryString == QueryStrings.OperacaoInsercao)
                    tb103.DT_ASSIN_CONTRA = tb103.DT_SIT_CLI;

                if (GestorEntities.SaveOrUpdate(tb103) <= 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Erro ao salvar item.");
                    return;
                }

                if (tb104 == null)
                {
                    tb104 = new TB104_CONT_CLIENTE();
                    tb104.CO_CLIENTE = tb103.CO_CLIENTE;
                    tb104.TB103_CLIENTE = TB103_CLIENTE.RetornaPelaChavePrimaria(tb104.CO_CLIENTE);
                }

                tb104.CO_TEL_CELU_CONTCLI = txtTelCelularContato.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "");
                tb104.CO_TEL_FIXO_CONTCLI = txtTelFixoContato.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "");
                tb104.DE_EMAIL_CONTCLI = txtEmailContato.Text;
                tb104.NO_CON_CLI = tb103.NO_CONTAT_CLIEN;
                tb104.NO_FUNC_CONTCLI = tb103.DE_CARGO_CONTAT;

                CurrentPadraoCadastros.CurrentEntity = tb104;
            }
            else if (QueryStringAuxili.OperacaoCorrenteQueryString == QueryStrings.OperacaoExclusao)
            {
                if (tb104 != null && GestorEntities.Delete(tb104) <= 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Erro ao excluir registro.");
                    return;
                }

                CurrentPadraoCadastros.CurrentEntity = tb103;
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB103_CLIENTE tb103 = RetornaEntidade();
            TB104_CONT_CLIENTE tb104 = RetornaEntidade2();

            if (tb103 != null)
            {
                tb103.TB905_BAIRROReference.Load();
                tb103.TB905_BAIRRO.TB904_CIDADEReference.Load();

                txtSigla.Text = tb103.NO_SIGLA_CLIEN;
                txtNome.Text = tb103.NO_FAN_CLI;
                txtRazaoSocial.Text = tb103.DE_RAZSOC_CLI;
                ddlTipoCategoria.SelectedValue = tb103.CO_CLASS_EMP;
                ddlTipo.SelectedValue = tb103.TP_CLIENTE;

                if (ddlTipo.SelectedValue == "J" && tb103.CO_CPFCGC_CLI.Length >= 14)
                    txtCNPJ.Text = tb103.CO_CPFCGC_CLI.Insert(12, "-").Insert(8, "/").Insert(5, ".").Insert(2, ".");
                else if (ddlTipo.SelectedValue == "F" && tb103.CO_CPFCGC_CLI.Length >= 11)
                    txtCNPJ.Text = tb103.CO_CPFCGC_CLI.Insert(9, "-").Insert(6, ".").Insert(3, ".");
                else
                    txtCNPJ.Text = tb103.CO_CPFCGC_CLI;

                txtInscEstadual.Text = tb103.CO_INS_EST_CLI;

                formularioEndereco.TxtNumero.Text = tb103.NU_END_CLI.ToString();
                formularioEndereco.TxtCep.Text = tb103.CO_CEP_CLI;
                formularioEndereco.TxtLogradouro.Text = tb103.DE_END_CLI;
                formularioEndereco.TxtComplemento.Text = tb103.DE_COM_CLI;
                formularioEndereco.CarregaUfs();
                formularioEndereco.DdlUf.SelectedValue = tb103.CO_UF_CLI;
                formularioEndereco.CarregaCidades();
                formularioEndereco.DdlCidade.SelectedValue = tb103.TB905_BAIRRO.TB904_CIDADE.CO_CIDADE.ToString();
                formularioEndereco.CarregaBairros();
                formularioEndereco.DdlBairro.SelectedValue = tb103.TB905_BAIRRO.CO_BAIRRO.ToString();

                txtTelefone.Text = tb103.CO_TEL1_CLI;
                txtTelefone2.Text = tb103.CO_TEL2_CLI;
                txtFax.Text = tb103.CO_FAX_CLI;
                txtWebSite.Text = tb103.DE_WEB_CLI;
                txtEmail.Text = tb103.DE_EMAIL_CLI;

                txtObservacao.Text = tb103.DE_OBS_CLI;

                ddlStatus.SelectedValue = tb103.CO_SIT_CLI;
                txtDtStatus.Text = tb103.DT_SIT_CLI.ToString("dd/MM/yyyy");
                txtDtCadastro.Text = tb103.DT_CAD_CLI.ToString("dd/MM/yyyy");

                if (tb104 != null)
                {
                    txtCargoContato.Text = tb104.NO_FUNC_CONTCLI;
                    txtNomeContato.Text = tb104.NO_CON_CLI;
                    txtTelCelularContato.Text = tb104.CO_TEL_CELU_CONTCLI;
                    txtTelFixoContato.Text = tb104.CO_TEL_FIXO_CONTCLI;
                    txtEmailContato.Text = tb104.DE_EMAIL_CONTCLI;
                }
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB103_CLIENTE</returns>
        private TB103_CLIENTE RetornaEntidade()
        {
            return TB103_CLIENTE.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB104_CONT_CLIENTE</returns>
        private TB104_CONT_CLIENTE RetornaEntidade2()
        {
            return TB104_CONT_CLIENTE.RetornaPeloCliente(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
        }        
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Tipos de Categoria
        /// </summary>
        private void CarregaTiposCategoria()
        {
            ddlTipoCategoria.Items.Clear();
            ddlTipoCategoria.Items.AddRange(AuxiliBaseApoio.BaseApoioDDL(tipoClassificacaoCliente.ResourceManager));
        }

        #endregion
    }
}