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
namespace C2BR.GestorEducacao.UI.GEDUC.F4000_CtrlOperBiblioteca.F4100_CtrlInformItensBiblioteca.F4101_CadastroItensAcervoBibli
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
            if (!IsPostBack)
            {
                CarregaUnidade();
                CarregaAreasConhecimento();
                CarregaClassificao();
                CarregarObras();
            }
        }        

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ORG_CODIGO_ORGAO", "CO_ISBN_ACER", "CO_ACERVO_AQUISI", "CO_ACERVO_ITENS", "CO_EMP" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_ISBN_ACER",
                HeaderText = "ISBN"
            });

            BoundField numControle = new BoundField();
            numControle.DataField = "CO_CTRL_INTERNO";
            numControle.HeaderText = "Cód. Interno";
            numControle.ItemStyle.CssClass = "codCol";
            CurrentPadraoBuscas.GridBusca.Columns.Add(numControle);

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_ACERVO",
                HeaderText = "Nome Obra"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "SIGLA",
                HeaderText = "Unidade (Sigla)"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_SITU_ACERVO_ITENS",
                HeaderText = "Situação"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int coAreaCon = ddlAreaConhecimento.SelectedValue != "" ? int.Parse(ddlAreaConhecimento.SelectedValue) : 0;
            int coClasAcer = ddlClassificacao.SelectedValue != "" ? int.Parse(ddlClassificacao.SelectedValue) : 0;
            decimal coISBNAcer = ddlObra.SelectedValue != "" ? decimal.Parse(ddlObra.SelectedValue) : 0;
            
            var resultado = (from tb204 in TB204_ACERVO_ITENS.RetornaTodosRegistros()
                             where (coEmp != 0 ? tb204.TB25_EMPRESA.CO_EMP == coEmp : coEmp == 0)
                             && (coAreaCon != 0 ? tb204.TB35_ACERVO.TB31_AREA_CONHEC.CO_AREACON == coAreaCon : coAreaCon == 0)
                             && (coClasAcer != 0 ? tb204.TB35_ACERVO.TB32_CLASSIF_ACER.CO_CLAS_ACER == coClasAcer : coClasAcer == 0)
                             && (coISBNAcer != 0 ? tb204.CO_ISBN_ACER == coISBNAcer : coISBNAcer == 0)
                             select new
                             {
                                 tb204.ORG_CODIGO_ORGAO, tb204.CO_CTRL_INTERNO, tb204.TB25_EMPRESA.CO_EMP, tb204.TB25_EMPRESA.NO_FANTAS_EMP,
                                 tb204.TB25_EMPRESA.sigla, tb204.CO_ISBN_ACER, tb204.CO_ACERVO_ITENS, tb204.TB203_ACERVO_AQUISICAO.CO_ACERVO_AQUISI,
                                 tb204.TB35_ACERVO.NO_ACERVO, tb204.TB203_ACERVO_AQUISICAO.DT_CADASTRO,
                                 CO_SITU_ACERVO_ITENS = ((tb204.CO_SITU_ACERVO_ITENS == "M") ? "Em Manutenção" : ((tb204.CO_SITU_ACERVO_ITENS == "I") ? "Inativo" : ((tb204.CO_SITU_ACERVO_ITENS == "R") ? "Reserva Técnica" : ((tb204.CO_SITU_ACERVO_ITENS == "E" ? "Emprestado" : ((tb204.CO_SITU_ACERVO_ITENS == "D") ? "Disponível" : ""))))))
                             }).OrderBy( a => a.sigla ).OrderByDescending( a => a.DT_CADASTRO ).ToList();

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
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_ACERVO_ITENS"));
            queryStringKeys.Add(new KeyValuePair<string, string>("orgao", "ORG_CODIGO_ORGAO"));
            queryStringKeys.Add(new KeyValuePair<string, string>("isbn", "CO_ISBN_ACER"));
            queryStringKeys.Add(new KeyValuePair<string, string>("acervoAquisicao", "CO_ACERVO_AQUISI"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoEmp, "CO_EMP"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

//====> Método que carrega o DropDown de Unidades
        private void CarregaUnidade()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                     where tb134.ADMUSUARIO.ideAdmUsuario == LoginAuxili.IDEADMUSUARIO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).OrderBy( e => e.NO_FANTAS_EMP );

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.Items.Insert(0, new ListItem("Todas", ""));

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

//====> Método que carrega o DropDown de Areas de Conhececimento
        private void CarregaAreasConhecimento()
        {
            ddlAreaConhecimento.DataSource = (from tb31 in TB31_AREA_CONHEC.RetornaTodosRegistros()
                                              where tb31.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                              select new { tb31.CO_AREACON, tb31.NO_AREACON }).OrderBy( a => a.NO_AREACON );

            ddlAreaConhecimento.DataTextField = "NO_AREACON";
            ddlAreaConhecimento.DataValueField = "CO_AREACON";
            ddlAreaConhecimento.DataBind();

            ddlAreaConhecimento.Items.Insert(0, new ListItem("Todas", ""));
        }

//====> Método que carrega o DropDown de Classificação de Acervo
        private void CarregaClassificao()
        {
            int coAreaCon = ddlAreaConhecimento.SelectedValue != "" ? int.Parse(ddlAreaConhecimento.SelectedValue) : 0;

            ddlClassificacao.DataSource = TB32_CLASSIF_ACER.RetornaPelaAreaConhecimento(coAreaCon);

            ddlClassificacao.DataTextField = "NO_CLAS_ACER";
            ddlClassificacao.DataValueField = "CO_CLAS_ACER";
            ddlClassificacao.DataBind();

            ddlClassificacao.Items.Insert(0, new ListItem("Todas", ""));
        }

//====> Método que carrega o DropDown de Obras do Acervo
        private void CarregarObras()
        {
            int coAreaCon = ddlAreaConhecimento.SelectedValue != "" ? int.Parse(ddlAreaConhecimento.SelectedValue) : 0;
            int coClasAcer = ddlClassificacao.SelectedValue != "" ? int.Parse(ddlClassificacao.SelectedValue) : 0;

            ddlObra.DataSource = (from tb35 in TB35_ACERVO.RetornaTodosRegistros()
                                  where tb35.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                  && (coAreaCon != 0 ? tb35.TB31_AREA_CONHEC.CO_AREACON == coAreaCon : coAreaCon == 0)
                                  && (coClasAcer != 0 ? tb35.TB32_CLASSIF_ACER.CO_CLAS_ACER == coClasAcer : coClasAcer == 0)
                                  select new { tb35.CO_ISBN_ACER, tb35.NO_ACERVO }).OrderBy( a => a.NO_ACERVO );

            ddlObra.DataTextField = "NO_ACERVO";
            ddlObra.DataValueField = "CO_ISBN_ACER";
            ddlObra.DataBind();

            ddlObra.Items.Insert(0, new ListItem("Todas", ""));
        }
        #endregion        

        protected void ddlAreaConhecimento_SelectedIndexChanged(object sender, EventArgs e)
        {            
            CarregaClassificao();
            CarregarObras();
        }

        protected void ddlClassificacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarObras();
        }
    }
}
