//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// OBJETIVO: AGENDA DE ATIVIDADES
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Reflection;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.Componentes
{
    public partial class AgendaAtividades : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HFDataSelecCalendario.Value = DateTime.Now.ToShortDateString();
                BindAgendaAtividadesGrid(0);
            }
        }        
        #endregion

        #region Métodos

        private void BindAgendaAtividadesGrid(int indexNovaPagina)
        {
            DateTime dataCalendario;

            if (!DateTime.TryParse(HFDataSelecCalendario.Value, out dataCalendario))
                dataCalendario = DateTime.Now;

//--------> Carrega a grid de acordo com a data informada e com as situações definidas
            grdAgendaAtividades.DataSource = (from tb137 in TB137_TAREFAS_AGENDA.RetornaTodosRegistros()
                                             where(tb137.TB139_SITU_TAREF_AGEND.CO_SITU_TAREF_AGEND == "TC" || tb137.TB139_SITU_TAREF_AGEND.CO_SITU_TAREF_AGEND == "EA"
                                             || tb137.TB139_SITU_TAREF_AGEND.CO_SITU_TAREF_AGEND == "TA") && ( tb137.DT_COMPR_TAREF_AGEND >= dataCalendario )
                                             && (tb137.CO_COL == LoginAuxili.CO_COL) && (tb137.CO_EMP == LoginAuxili.CO_UNID_FUNC) 
                                             && (tb137.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO)
                                             select new
                                             {
                                                 tb137.CO_IDENT_TAREF, tb137.DT_COMPR_TAREF_AGEND, tb137.DT_LIMIT_TAREF_AGEND,
                                                 NM_RESUM_TAREF_AGEND = (tb137.DE_DETAL_TAREF_AGEND.Length > 26) ? tb137.DE_DETAL_TAREF_AGEND.Substring(0, 26) + "..." : tb137.DE_DETAL_TAREF_AGEND,
                                                 CO_PRIOR_TAREF_AGEND = tb137.TB140_PRIOR_TAREF_AGEND.DE_PRIOR_TAREF_AGEND.ToUpper(),
                                                 NomeColaborador = tb137.TB03_COLABOR1.NO_APEL_COL != null ? tb137.TB03_COLABOR1.CO_MAT_COL.Insert(5, "-").Insert(2, ".") + " - " + tb137.TB03_COLABOR1.NO_APEL_COL : tb137.TB03_COLABOR1.CO_MAT_COL.Insert(5, "-").Insert(2, ".") + " - *****",
                                                 CO_SITU_TAREF_AGEND = (tb137.DT_LIMIT_TAREF_AGEND.HasValue) ? ((DateTime.Now > tb137.DT_LIMIT_TAREF_AGEND.Value) ? "Atrasada" : tb137.TB139_SITU_TAREF_AGEND.DE_SITU_TAREF_AGEND) : null,
                                                 tb137.CO_COL, tb137.CO_EMP, tb137.ORG_CODIGO_ORGAO, tb137.CO_CHAVE_UNICA_TAREF
                                             }).OrderBy( t => t.DT_COMPR_TAREF_AGEND );

            grdAgendaAtividades.PageIndex = indexNovaPagina;
            grdAgendaAtividades.DataBind();            
        }                   
        #endregion

        protected void HFDataSelecCalendario_ValueChanged(object sender, EventArgs e)
        {
//--------> Quando data alterada atualiza a grid
            BindAgendaAtividadesGrid(0);
        }

        protected void grdAgendaAtividades_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("btnIncluir"))
            {
                Session[SessoesHttp.IdModuloCorrente] = e.CommandArgument;
                Session[SessoesHttp.ModuloIdAgenAtiv] = 294;
                Response.Redirect(String.Format("{0}?{1}={2}&ititle=+Cadastro+de+Tarefas+Agendadas.",
                                                    Request.Url.AbsolutePath,
                                                    QueryStrings.PaginaURLIFrame,
                                                    Server.UrlEncode("/GEDUC/1000_CtrlAdminEscolar/1300_ServicosApoioAdministrativo/1310_CtrlAgendaAtividadesFuncional/1311_RegistroAgendaAtividade/Busca.aspx&ititle=Cadastro+de+Tarefas+Agendadas")
                                                    ));
            }
            else if (e.CommandName.Equals("btnImprimir"))
            {
                Session[SessoesHttp.IdModuloCorrente] = e.CommandArgument;
                Response.Redirect("");
            }
            else if (e.CommandName.Equals("Select"))
            {
                int id = (int)((GridView)(e.CommandSource)).DataKeys[int.Parse(e.CommandArgument.ToString())].Values[0];
                int idTarefa = (int)((GridView)(e.CommandSource)).DataKeys[int.Parse(e.CommandArgument.ToString())].Values[1];

                TB137_TAREFAS_AGENDA tb137 = (from lTb137 in TB137_TAREFAS_AGENDA.RetornaTodosRegistros().Include(typeof(TB139_SITU_TAREF_AGEND).Name)
                                              where lTb137.CO_CHAVE_UNICA_TAREF == id
                                              && lTb137.CO_IDENT_TAREF == idTarefa
                                              select lTb137).FirstOrDefault();

                if (tb137 != null)
                    if (tb137.TB139_SITU_TAREF_AGEND.CO_SITU_TAREF_AGEND == "TC")
                    {
                        tb137.TB139_SITU_TAREF_AGEND = TB139_SITU_TAREF_AGEND.RetornaPelaChavePrimaria("TA");
                        TB137_TAREFAS_AGENDA.SaveOrUpdate(tb137);
                    }


                Session[SessoesHttp.IdModuloCorrente] = 295;
                Response.Redirect(String.Format("{0}?{1}={2}&ititle=+Cadastro+de+Tarefas+Agendadas.",
                                                    Request.Url.AbsolutePath,
                                                    QueryStrings.PaginaURLIFrame,
                                                    Server.UrlEncode("/GEDUC/1000_CtrlAdminEscolar/1300_ServicosApoioAdministrativo/1310_CtrlAgendaAtividadesFuncional/1311_RegistroAgendaAtividade/Cadastro.aspx?op=details&id=" + id + "&CoIdentTarefa=" + idTarefa)
                                                    ));
            }
        }

        protected void grdAgendaAtividades_RowDataBound(object sender, GridViewRowEventArgs e)
        {
//--------> Criação do link, para funcionalidade de "cadastro de tarefas agendadas", das linhas da GRID
            if (e.Row.DataItem != null)
            {
                string strCssClassRow = e.Row.Cells.GetCellValue("Status").Replace(" ", "").ToLower() + "Row";
                e.Row.CssClass = "agendaAtividadesGridRow " + strCssClassRow;

                DateTime dataLimite = DateTime.Parse(e.Row.Cells.GetCellValue("Limite"));
                if (DateTime.Now > dataLimite)
                    e.Row.CssClass += " atrasadaRow";

                string strTarefaURl = String.Format("/GEDUC/1000_CtrlAdminEscolar/1300_ServicosApoioAdministrativo/1310_CtrlAgendaAtividadesFuncional/1311_RegistroAgendaAtividade/Cadastro.aspx?op=edit&id=" + DataBinder.Eval(e.Row.DataItem, "CO_CHAVE_UNICA_TAREF") + "&CoIdentTarefa=" + DataBinder.Eval(e.Row.DataItem, "CO_IDENT_TAREF") + "&moduloNome=Cadastro%20de%20Tarefas%20Agendadas.?");

                e.Row.Attributes.Add("onclick", "javascript:openAsIframe('" + strTarefaURl + "')");
            }
        }

        protected void grdAgendaAtividades_PreRender(object sender, EventArgs e)
        {            
            GridViewRow gridViewRow = (GridViewRow)grdAgendaAtividades.BottomPagerRow;

            if ((gridViewRow != null) && (gridViewRow.Visible == false))
                gridViewRow.Visible = true;
        }

        protected void grdAgendaAtividades_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindAgendaAtividadesGrid(e.NewPageIndex);
        }
    }
}