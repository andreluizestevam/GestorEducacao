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
namespace C2BR.GestorEducacao.UI.GEDUC.F4000_CtrlOperBiblioteca.F4100_CtrlInformItensBiblioteca.F4103_AtualiDadosItensAcervo
{
    public partial class Busca : System.Web.UI.Page
    {
        public PadraoBuscas CurrentPadraoBuscas { get { return ((PadraoBuscas)this.Master); } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            CurrentPadraoBuscas.OnAcaoBuscaDefineGridView += new PadraoBuscas.OnAcaoBuscaDefineGridViewHandler(CurrentPadraoBuscas_OnAcaoBuscaDefineGridView);
            CurrentPadraoBuscas.OnDefineColunasGridView += new PadraoBuscas.OnDefineColunasGridViewHandler(CurrentPadraoBuscas_OnDefineColunasGridView);
            CurrentPadraoBuscas.OnDefineQueryStringIds += new PadraoBuscas.OnDefineQueryStringIdsHandler(CurrentPadraoBuscas_OnDefineQueryStringIds);
            CurrentPadraoBuscas.OnGridRowDataBound += new PadraoBuscas.OnGridRowDataBoundHandler(CurrentPadraoBuscas_RowDataBound);
        }

        protected void Page_Load()
        {
            if (IsPostBack) return;

            CarregaClassificacoes();
            CarregaAutores();
            CarregaAreasConhecimento();
            CarregaEditoras();
        }        

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_ISBN_ACER", "ORG_CODIGO_ORGAO" };
            
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_ISBN_ACER",
                HeaderText = "ISBN"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_AUTOR",
                HeaderText = "Autor"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_ACERVO",
                HeaderText = "Livro"
            });           
            
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_CLAS_ACER",
                HeaderText = "Classificação"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coClasAcer = ddlClassificacao.SelectedValue != "" ? int.Parse(ddlClassificacao.SelectedValue) : 0;
            int coAreaCon = ddlAreaConhecimento.SelectedValue != "" ? int.Parse(ddlAreaConhecimento.SelectedValue) : 0;
            int coAutor = ddlAutor.SelectedValue != "" ? int.Parse(ddlAutor.SelectedValue) : 0;
            int coEditora = ddlEditora.SelectedValue != "" ? int.Parse(ddlEditora.SelectedValue) : 0;

            var resultado = (from tb35 in TB35_ACERVO.RetornaPelaInstituicao(LoginAuxili.ORG_CODIGO_ORGAO)
                            where (txtLivro.Text != "" ? tb35.NO_ACERVO.Contains(txtLivro.Text) : txtLivro.Text == "")
                            && (coAreaCon != 0 ? tb35.TB31_AREA_CONHEC.CO_AREACON == coAreaCon : coAreaCon == 0)
                            && (coClasAcer != 0 ? tb35.TB32_CLASSIF_ACER.CO_CLAS_ACER == coClasAcer : coClasAcer == 0)
                            && (coAutor != 0 ? tb35.TB34_AUTOR.CO_AUTOR == coAutor : coAutor == 0)
                            && (coEditora != 0 ? tb35.TB33_EDITORA.CO_EDITORA == coEditora : coEditora == 0)
                            select new 
                            {
                                tb35.CO_ISBN_ACER, tb35.NO_ACERVO, tb35.TB34_AUTOR.NO_AUTOR,
                                tb35.TB32_CLASSIF_ACER.NO_CLAS_ACER, tb35.ORG_CODIGO_ORGAO, tb35.TB31_AREA_CONHEC.NO_AREACON
                            }).OrderBy( a => a.NO_AUTOR ).ThenBy( a => a.NO_ACERVO );

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                decimal isbn = decimal.Parse(e.Row.Cells[0].Text);
                e.Row.Cells[0].Text = isbn.ToString("000-00-0000-000-0");
            }
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>( QueryStrings.Id, "CO_ISBN_ACER"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

//====> Método que carrega o DropDown de Classificações de Acervo
        private void CarregaClassificacoes()
        {
            int coAreaCon = ddlAreaConhecimento.SelectedValue != "" ? int.Parse(ddlAreaConhecimento.SelectedValue) : 0;

            ddlClassificacao.Items.Clear();

            if (coAreaCon != 0)
            {
                ddlClassificacao.DataSource = (from tb32 in TB32_CLASSIF_ACER.RetornaPelaAreaConhecimento(coAreaCon)
                                               select new { tb32.CO_CLAS_ACER, tb32.NO_CLAS_ACER });

                ddlClassificacao.DataTextField = "NO_CLAS_ACER";
                ddlClassificacao.DataValueField = "CO_CLAS_ACER";
                ddlClassificacao.DataBind();
            }

            ddlClassificacao.Items.Insert(0, new ListItem("Todas", ""));
        }

//====> Método que carrega o DropDown de Autores
        private void CarregaAutores()
        {
            ddlAutor.DataSource = (from tb34 in TB34_AUTOR.RetornaTodosRegistros()
                                   select new { tb34.CO_AUTOR, tb34.NO_AUTOR });

            ddlAutor.DataTextField = "NO_AUTOR";
            ddlAutor.DataValueField = "CO_AUTOR";
            ddlAutor.DataBind();

            ddlAutor.Items.Insert(0, new ListItem("Todos", ""));
        }

//====> Método que carrega o DropDown de Áreas de Conhecimento
        private void CarregaAreasConhecimento()
        {
            ddlAreaConhecimento.DataSource = (from tb31 in TB31_AREA_CONHEC.RetornaTodosRegistros()
                                              where tb31.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                              select new { tb31.CO_AREACON, tb31.NO_AREACON });

            ddlAreaConhecimento.DataTextField = "NO_AREACON";
            ddlAreaConhecimento.DataValueField = "CO_AREACON";
            ddlAreaConhecimento.DataBind();

            ddlAreaConhecimento.Items.Insert(0, new ListItem("Todas", ""));
        }

//====> Método que carrega o DropDown de Editoras
        private void CarregaEditoras()
        {
            ddlEditora.DataSource = (from tb33 in TB33_EDITORA.RetornaTodosRegistros()
                                     select new { tb33.NO_EDITORA, tb33.CO_EDITORA });

            ddlEditora.DataTextField = "NO_EDITORA";
            ddlEditora.DataValueField = "CO_EDITORA";
            ddlEditora.DataBind();

            ddlEditora.Items.Insert(0, new ListItem("Todas", ""));
        }
        #endregion

        protected void ddlAreaConhecimento_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaClassificacoes();
        }
    }
}
