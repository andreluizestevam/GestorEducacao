//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL DE BIBLIOTECA
// SUBMÓDULO: DADOS CADASTRAIS DE ACERVO BIBLIOGRÁFICO
// OBJETIVO: CADASTRO DE ITENS DE ACERVO BIBLIOGRÁFICO
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
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F4000_CtrlOperBiblioteca.F4100_CtrlInformItensBiblioteca.F4101_CadastroItensAcervoBibli
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

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao) ||
                QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoExclusao))
                AuxiliPagina.RedirecionaParaPaginaMensagem("Favor, selecione uma obra.", Request.Url.AbsolutePath.ToLower().Replace("cadastro.aspx", "busca.aspx") + "&moduloNome=" + Request.QueryString["moduloNome"], C2BR.GestorEducacao.UI.RedirecionaMensagem.TipoMessagemRedirecionamento.Error);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string dataAtual = DateTime.Now.ToString("dd/MM/yyyy");
                txtDtCadastro.Text = txtDtSituacao.Text = dataAtual;
                txtResponsavel.Text = LoginAuxili.NOME_USU_LOGADO;
                CarregaUnidade();
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (Page.IsValid)
            {
                decimal decimalRetorno;
                int intRetorno;

                TB204_ACERVO_ITENS tb204 = RetornaEntidade();

                if (tb204 != null)
                {                    
                    tb204.VL_ACERVO_ITENS = decimal.TryParse(txtValor.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
                    tb204.CO_ESTADO_ACERVO_ITENS = ddlEstadoConservacao.SelectedValue;
                    tb204.NU_PAGINA_ACERVO_ITENS = int.TryParse(txtQtdPaginas.Text, out intRetorno) ? (int?)intRetorno : null;
                    tb204.DE_OBS_ACERVO_ITENS = txtObservacao.Text != "" ? txtObservacao.Text : null;
                    tb204.CO_SITU_ACERVO_ITENS = ddlSituacao.SelectedValue;
                    tb204.DT_SITU_ACERVO_ITENS = DateTime.Now;

                    CurrentPadraoCadastros.CurrentEntity = tb204;
                }
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB204_ACERVO_ITENS tb204 = RetornaEntidade();

            if (tb204 != null)
            {
                tb204.TB03_COLABORReference.Load();
                tb204.TB25_EMPRESAReference.Load();
                tb204.TB35_ACERVOReference.Load();
                tb204.TB35_ACERVO.TB34_AUTORReference.Load();
                tb204.TB35_ACERVO.TB33_EDITORAReference.Load();
                tb204.TB203_ACERVO_AQUISICAOReference.Load();

                ddlUnidadeBiblioteca.SelectedValue = tb204.TB25_EMPRESA.CO_EMP.ToString();
                txtNomeObra.Text = tb204.TB35_ACERVO.NO_ACERVO;
                txtAutor.Text = tb204.TB35_ACERVO.TB34_AUTOR.NO_AUTOR;
                txtEditora.Text = tb204.TB35_ACERVO.TB33_EDITORA.NO_EDITORA;
                txtIsbn.Text = tb204.CO_ISBN_ACER.ToString().Length == 13 ? tb204.CO_ISBN_ACER.ToString("000-00-000-0000-0") : tb204.CO_ISBN_ACER.ToString();
                txtNumItem.Text = tb204.CO_ACERVO_ITENS.ToString();
                txtCodBarras.Text = tb204.CO_CTRL_INTERNO;
                txtCodAquisicao.Text = tb204.TB203_ACERVO_AQUISICAO.TP_ACERVO_AQUISI.ToString() + "|" + tb204.CO_ACERVO_AQUISI.ToString("000000");
                txtValor.Text = tb204.VL_ACERVO_ITENS != null ? tb204.VL_ACERVO_ITENS.ToString() : "";
                ddlEstadoConservacao.SelectedValue = tb204.CO_ESTADO_ACERVO_ITENS;
                txtQtdPaginas.Text = tb204.NU_PAGINA_ACERVO_ITENS != null ? tb204.NU_PAGINA_ACERVO_ITENS.ToString() : "";
                txtObservacao.Text = tb204.DE_OBS_ACERVO_ITENS;
                ddlSituacao.SelectedValue = tb204.CO_SITU_ACERVO_ITENS;
                txtDtSituacao.Text = tb204.DT_SITU_ACERVO_ITENS.ToString("dd/MM/yyyy");
                txtDtCadastro.Text = tb204.DT_CADASTRO.ToString("dd/MM/yyyy");
                txtResponsavel.Text = tb204.TB03_COLABOR.NO_COL;
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB204_ACERVO_ITENS</returns>
        private TB204_ACERVO_ITENS RetornaEntidade()
        {
            int orgCodigoOrgao = QueryStringAuxili.RetornaQueryStringComoIntPelaChave("orgao");
            Decimal coIsbnAcer = Decimal.Parse(QueryStringAuxili.RetornaQueryStringPelaChave("isbn"));
            int coAcervoAquisi = QueryStringAuxili.RetornaQueryStringComoIntPelaChave("acervoAquisicao");
            int coAcervoItens = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);
            int coEmp = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoEmp);

            return TB204_ACERVO_ITENS.RetornaPelaChavePrimaria(orgCodigoOrgao, coIsbnAcer, coAcervoAquisi, coAcervoItens, coEmp);
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o DropDown de Unidades
        /// </summary>
        private void CarregaUnidade()
        {
            ddlUnidadeBiblioteca.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                               select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP });

            ddlUnidadeBiblioteca.DataTextField = "NO_FANTAS_EMP";
            ddlUnidadeBiblioteca.DataValueField = "CO_EMP";
            ddlUnidadeBiblioteca.DataBind();
        }
        #endregion
    }
}