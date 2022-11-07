//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL DE BIBLIOTECA
// SUBMÓDULO: DADOS CADASTRAIS DE ACERVO BIBLIOGRÁFICO
// OBJETIVO: ATUALIZAÇÃO DE DADOS DE ITENS DE ACERVO BIBLIOGRÁFICOS
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
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F4000_CtrlOperBiblioteca.F4100_CtrlInformItensBiblioteca.F4103_AtualiDadosItensAcervo
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
            if (IsPostBack) return;
            CarregaAreasConhecimento();
            CarregaClassificacoes();
            CarregaAutores();
            CarregaEditoras();
            txtDataSituacao.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int coClasAcer = ddlClassificacao.SelectedValue != "" ? int.Parse(ddlClassificacao.SelectedValue) : 0;
            int coAreaCon = ddlAreaConhecimento.SelectedValue != "" ? int.Parse(ddlAreaConhecimento.SelectedValue) : 0;
            int coAutor = ddlAutor.SelectedValue != "" ? int.Parse(ddlAutor.SelectedValue) : 0;
            int coEditora = ddlEditora.SelectedValue != "" ? int.Parse(ddlEditora.SelectedValue) : 0;

            TB35_ACERVO tb35 = RetornaEntidade();

            int intRetorno = 0;
            decimal decimalRetorno = 0;

            if (tb35 == null)
            {
                tb35 = new TB35_ACERVO();

                tb35.ORG_CODIGO_ORGAO = LoginAuxili.ORG_CODIGO_ORGAO;
                tb35.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
                tb35.CO_ISBN_ACER = decimal.TryParse(txtIsbn.Text.Replace("-", ""), out decimalRetorno) ? decimalRetorno : 0;
                tb35.DT_CADASTRO = DateTime.Now;
            }

            tb35.TB31_AREA_CONHEC = TB31_AREA_CONHEC.RetornaPelaChavePrimaria(coAreaCon);
            tb35.TB32_CLASSIF_ACER = TB32_CLASSIF_ACER.RetornaPelaChavePrimaria(coClasAcer);
            tb35.TB33_EDITORA = TB33_EDITORA.RetornaPelaChavePrimaria(coEditora);
            tb35.TB34_AUTOR = TB34_AUTOR.RetornaPelaChavePrimaria(coAutor);
            tb35.NO_ACERVO = txtNomeObra.Text.Length > 50 ? txtNomeObra.Text.Substring(0, 50) : txtNomeObra.Text;
            tb35.NU_PAG_LIVRO = int.TryParse(txtNumeroPaginas.Text, out intRetorno) ? intRetorno : 0;
            tb35.DES_SINOPSE = txtSinopse.Text.Length > 250 ? txtSinopse.Text.Substring(0, 250) : txtSinopse.Text;
            tb35.CO_SITU = ddlSituacao.SelectedValue;            
            tb35.DT_SIT_ACER = DateTime.Parse(txtDataSituacao.Text);
            
            CurrentPadraoCadastros.CurrentEntity = tb35;
        }        
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB35_ACERVO tb35 = RetornaEntidade();

            if (tb35 != null)
            {
                tb35.TB31_AREA_CONHECReference.Load();
                tb35.TB32_CLASSIF_ACERReference.Load();
                tb35.TB33_EDITORAReference.Load();
                tb35.TB34_AUTORReference.Load();

                txtDataSituacao.Text = tb35.DT_SIT_ACER.ToString("dd/MM/yyyy");
                txtIsbn.Text = tb35.CO_ISBN_ACER.ToString("0000000000000");
                txtNomeObra.Text = tb35.NO_ACERVO;
                txtNumeroPaginas.Text = tb35.NU_PAG_LIVRO.ToString();
                txtSinopse.Text = tb35.DES_SINOPSE;
                ddlAreaConhecimento.SelectedValue = tb35.TB31_AREA_CONHEC.CO_AREACON.ToString();
                CarregaClassificacoes();
                ddlClassificacao.SelectedValue = tb35.TB32_CLASSIF_ACER.CO_CLAS_ACER.ToString();
                ddlAutor.SelectedValue = tb35.TB34_AUTOR.CO_AUTOR.ToString();
                ddlEditora.SelectedValue = tb35.TB33_EDITORA.CO_EDITORA.ToString();
                ddlSituacao.SelectedValue = tb35.CO_SITU;
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB35_ACERVO</returns>
        private TB35_ACERVO RetornaEntidade()
        {
            decimal coIsbnAcer;
            coIsbnAcer = QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Id) != null ? decimal.Parse(QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Id)) : 0;
            return TB35_ACERVO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO, coIsbnAcer);
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Classificações de Acervo
        /// </summary>
        private void CarregaClassificacoes()
        {
            int coAreaCon = ddlAreaConhecimento.SelectedValue != "" ? int.Parse(ddlAreaConhecimento.SelectedValue) : 0;

            ddlClassificacao.Items.Clear();

            if (coAreaCon != 0)
            {
                ddlClassificacao.DataSource = (from tb32 in TB32_CLASSIF_ACER.RetornaTodosRegistros()
                                               where tb32.TB31_AREA_CONHEC.CO_AREACON == coAreaCon                                               
                                               select new { tb32.CO_CLAS_ACER, tb32.NO_CLAS_ACER });

                ddlClassificacao.DataTextField = "NO_CLAS_ACER";
                ddlClassificacao.DataValueField = "CO_CLAS_ACER";
                ddlClassificacao.DataBind();
            }

            ddlClassificacao.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Autores
        /// </summary>
        private void CarregaAutores()
        {
            ddlAutor.DataSource = (from tb34 in TB34_AUTOR.RetornaTodosRegistros()
                                   where tb34.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                   select new { tb34.CO_AUTOR, tb34.NO_AUTOR });

            ddlAutor.DataTextField = "NO_AUTOR";
            ddlAutor.DataValueField = "CO_AUTOR";
            ddlAutor.DataBind();

            ddlAutor.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Áreas de Conhecimento
        /// </summary>
        private void CarregaAreasConhecimento()
        {
            ddlAreaConhecimento.DataSource = (from tb31 in TB31_AREA_CONHEC.RetornaTodosRegistros()
                                              where tb31.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                              select new { tb31.CO_AREACON, tb31.NO_AREACON });

            ddlAreaConhecimento.DataTextField = "NO_AREACON";
            ddlAreaConhecimento.DataValueField = "CO_AREACON";
            ddlAreaConhecimento.DataBind();

            ddlAreaConhecimento.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Editoras
        /// </summary>
        private void CarregaEditoras()
        {
            ddlEditora.DataSource = (from tb33 in TB33_EDITORA.RetornaTodosRegistros()
                                     where tb33.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                     select new { tb33.NO_EDITORA, tb33.CO_EDITORA });

            ddlEditora.DataTextField = "NO_EDITORA";
            ddlEditora.DataValueField = "CO_EDITORA";
            ddlEditora.DataBind();

            ddlEditora.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion

        protected void ddlAreaConhecimento_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaClassificacoes();
        }        
    }
}
