//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: PESQUISAS/ENQUETES DE SATISFAÇÃO
// OBJETIVO: CADASTRAMENTO DE QUESTÕES PARA PESQUISAS INSITUCIONAIS
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
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9300_CtrlExames._9309_ControleItensExameFisico
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
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaGrupos();
                CarregaSubgrupos();
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_ITEM_EXAME_FISIC" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_GRUPO_FISIC",
                HeaderText = "Grupo"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_SUB_GRUPO_FISIC",
                HeaderText = "Subgrupo"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_ITEM_EXAME_FISIC",
                HeaderText = "Itens de Avaliação"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "FL_SITUA_ITEM",
                HeaderText = "Situação"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            var idGrupo = !String.IsNullOrEmpty(ddlGrupo.SelectedValue) ? int.Parse(ddlGrupo.SelectedValue) : 0;
            var idSubgrupo = !String.IsNullOrEmpty(ddlSubgrupo.SelectedValue) ? int.Parse(ddlSubgrupo.SelectedValue) : 0;

            var res = (from tbs433 in TBS433_EXAME_FISIC_ITEM.RetornaTodosRegistros()
                       where (!String.IsNullOrEmpty(txtItem.Text) ? tbs433.NO_ITEM_EXAME_FISIC.Contains(txtItem.Text) : true)
                       && (idGrupo != 0 ? tbs433.TBS432_EXAME_FISIC_SUB_GRUPO.TBS431_GRUPO_EXAME_FISIC.ID_GRUPO_FISIC == idGrupo : true)
                       && (idSubgrupo != 0 ? tbs433.TBS432_EXAME_FISIC_SUB_GRUPO.ID_SUB_GRUPO_FISIC == idSubgrupo : true)
                       && (ddlSitu.SelectedValue.Equals("") ? true : tbs433.FL_SITUA_ITEM_EXAME_FISIC == ddlSitu.SelectedValue)
                       select new
                       {
                           tbs433.ID_ITEM_EXAME_FISIC,
                           tbs433.NO_ITEM_EXAME_FISIC,
                           tbs433.TBS432_EXAME_FISIC_SUB_GRUPO.NO_SUB_GRUPO_FISIC,
                           tbs433.TBS432_EXAME_FISIC_SUB_GRUPO.TBS431_GRUPO_EXAME_FISIC.NO_GRUPO_FISIC,
                           FL_SITUA_ITEM = tbs433.FL_SITUA_ITEM_EXAME_FISIC == "A" ? "Ativo" : "Inativo",
                       }).OrderBy(x => x.NO_GRUPO_FISIC).ThenBy(x => x.NO_SUB_GRUPO_FISIC).ThenBy(y => y.NO_ITEM_EXAME_FISIC);

            CurrentPadraoBuscas.GridBusca.DataSource = res;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>("item", "ID_ITEM_EXAME_FISIC"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        #endregion

        #region Carregamento DropDown

        private void CarregaGrupos()
        {
            ddlGrupo.DataSource = (from tbs431 in TBS431_GRUPO_EXAME_FISIC.RetornaTodosRegistros()
                                   select new
                                   {
                                       tbs431.ID_GRUPO_FISIC,
                                       tbs431.NO_GRUPO_FISIC
                                   }).OrderBy(t => t.NO_GRUPO_FISIC);

            ddlGrupo.DataTextField = "NO_GRUPO_FISIC";
            ddlGrupo.DataValueField = "ID_GRUPO_FISIC";
            ddlGrupo.DataBind();

            ddlGrupo.Items.Insert(0, new ListItem("Todos", "0"));
        }

        private void CarregaSubgrupos()
        {
            var idGrupo = !String.IsNullOrEmpty(ddlGrupo.SelectedValue) ? int.Parse(ddlGrupo.SelectedValue) : 0;

            ddlSubgrupo.DataSource = (from tbs432 in TBS432_EXAME_FISIC_SUB_GRUPO.RetornaTodosRegistros()
                                      where (idGrupo != 0 ? tbs432.TBS431_GRUPO_EXAME_FISIC.ID_GRUPO_FISIC == idGrupo : true)
                                      select new
                                      {
                                          tbs432.ID_SUB_GRUPO_FISIC,
                                          tbs432.NO_SUB_GRUPO_FISIC
                                      }).OrderBy(t => t.NO_SUB_GRUPO_FISIC);

            ddlSubgrupo.DataTextField = "NO_SUB_GRUPO_FISIC";
            ddlSubgrupo.DataValueField = "ID_SUB_GRUPO_FISIC";
            ddlSubgrupo.DataBind();

            ddlSubgrupo.Items.Insert(0, new ListItem("Todos", "0"));
        }

        #endregion

        #region Métodos

        protected void ddlGrupo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubgrupos();
        }

        #endregion
    }
}
