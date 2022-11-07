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
namespace C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9300_CtrlExames._9305_ControleItensAvaliacao
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
                CarregaOperadoras();
                CarregaProcedimentos();
                CarregaGrupos();
                CarregaSubgrupos();
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_ITENS_AVALI" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NM_SIGLA_OPER",
                HeaderText = " Contratação"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NM_PROC_MEDI",
                HeaderText = " Procedimento"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_GRUPO_EXAME",
                HeaderText = "Grupo"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_SUBGR_EXAME",
                HeaderText = "Subgrupo"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_ITEM_AVALI",
                HeaderText = "Itens de Avaliação"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
               DataField = "FL_SITUA_ITEM",
               HeaderText = "Status"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            var idOper = !String.IsNullOrEmpty(ddlOperadora.SelectedValue) ? int.Parse(ddlOperadora.SelectedValue) : 0;
            var idProc = !String.IsNullOrEmpty(ddlProcedimento.SelectedValue) ? int.Parse(ddlProcedimento.SelectedValue) : 0;
            var idGrupo = !String.IsNullOrEmpty(ddlGrupo.SelectedValue) ? int.Parse(ddlGrupo.SelectedValue) : 0;
            var idSubgrupo = !String.IsNullOrEmpty(ddlSubgrupo.SelectedValue) ? int.Parse(ddlSubgrupo.SelectedValue) : 0;

            var res = (from tbs414 in TBS414_EXAME_ITENS_AVALI.RetornaTodosRegistros()
                       where (!String.IsNullOrEmpty(txtItem.Text) ? tbs414.NO_ITEM_AVALI.Contains(txtItem.Text) : true)
                       && (idOper != 0 ? tbs414.TBS413_EXAME_SUBGR.TBS412_EXAME_GRUPO.TBS356_PROC_MEDIC_PROCE.TB250_OPERA.ID_OPER == idOper : true)
                       && (idProc != 0 ? tbs414.TBS413_EXAME_SUBGR.TBS412_EXAME_GRUPO.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == idProc : true)
                       && (idGrupo != 0 ? tbs414.TBS413_EXAME_SUBGR.TBS412_EXAME_GRUPO.ID_GRUPO == idGrupo : true)
                       && (idSubgrupo != 0 ? tbs414.TBS413_EXAME_SUBGR.ID_SUBGRUPO == idSubgrupo : true)
                       select new
                       {
                           tbs414.ID_ITENS_AVALI,
                           tbs414.NO_ITEM_AVALI,
                           tbs414.TBS413_EXAME_SUBGR.NO_SUBGR_EXAME,
                           tbs414.TBS413_EXAME_SUBGR.TBS412_EXAME_GRUPO.NO_GRUPO_EXAME,
                           tbs414.TBS413_EXAME_SUBGR.TBS412_EXAME_GRUPO.TBS356_PROC_MEDIC_PROCE.TB250_OPERA.NM_SIGLA_OPER,
                           tbs414.TBS413_EXAME_SUBGR.TBS412_EXAME_GRUPO.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
                           FL_SITUA_ITEM = tbs414.FL_SITUA_ITEM == "A" ? "Ativo" : "Inativo",
                           tbs414.NU_ORDEM
                       }).OrderBy(x => x.NO_GRUPO_EXAME).ThenBy(x => x.NO_SUBGR_EXAME).ThenBy(x => x.NU_ORDEM).ThenBy(y => y.NO_ITEM_AVALI);

            CurrentPadraoBuscas.GridBusca.DataSource = res;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>("item", "ID_ITENS_AVALI"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        #endregion

        #region Carregamento DropDown

        public void CarregaOperadoras()
        {
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddlOperadora, true);
        }

        public void CarregaProcedimentos()
        {
            var idOper = !String.IsNullOrEmpty(ddlOperadora.SelectedValue) ? int.Parse(ddlOperadora.SelectedValue) : 0;

            AuxiliCarregamentos.CarregaProcedimentosMedicos(ddlProcedimento, idOper, true);
        }

        private void CarregaGrupos()
        {
            var idOper = !String.IsNullOrEmpty(ddlOperadora.SelectedValue) ? int.Parse(ddlOperadora.SelectedValue) : 0;
            var idProc = !String.IsNullOrEmpty(ddlProcedimento.SelectedValue) ? int.Parse(ddlProcedimento.SelectedValue) : 0;

            ddlGrupo.DataSource = (from tbs412 in TBS412_EXAME_GRUPO.RetornaTodosRegistros()
                                   where (idOper != 0 ? tbs412.TBS356_PROC_MEDIC_PROCE.TB250_OPERA.ID_OPER == idOper : true)
                                   && (idProc != 0 ? tbs412.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == idProc : true)
                                   select new
                                   {
                                       tbs412.ID_GRUPO,
                                       tbs412.NO_GRUPO_EXAME
                                   }).OrderBy(t => t.NO_GRUPO_EXAME);

            ddlGrupo.DataTextField = "NO_GRUPO_EXAME";
            ddlGrupo.DataValueField = "ID_GRUPO";
            ddlGrupo.DataBind();

            ddlGrupo.Items.Insert(0, new ListItem("Todos", "0"));
        }

        private void CarregaSubgrupos()
        {
            var idOper = !String.IsNullOrEmpty(ddlOperadora.SelectedValue) ? int.Parse(ddlOperadora.SelectedValue) : 0;
            var idProc = !String.IsNullOrEmpty(ddlProcedimento.SelectedValue) ? int.Parse(ddlProcedimento.SelectedValue) : 0;
            var idGrupo = !String.IsNullOrEmpty(ddlGrupo.SelectedValue) ? int.Parse(ddlGrupo.SelectedValue) : 0;

            ddlSubgrupo.DataSource = (from tbs413 in TBS413_EXAME_SUBGR.RetornaTodosRegistros()
                                   where (idOper != 0 ? tbs413.TBS412_EXAME_GRUPO.TBS356_PROC_MEDIC_PROCE.TB250_OPERA.ID_OPER == idOper : true)
                                   && (idProc != 0 ? tbs413.TBS412_EXAME_GRUPO.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == idProc : true)
                                   && (idGrupo != 0 ? tbs413.TBS412_EXAME_GRUPO.ID_GRUPO == idGrupo : true)
                                   select new
                                   {
                                       tbs413.ID_SUBGRUPO,
                                       tbs413.NO_SUBGR_EXAME
                                   }).OrderBy(t => t.NO_SUBGR_EXAME);

            ddlSubgrupo.DataTextField = "NO_SUBGR_EXAME";
            ddlSubgrupo.DataValueField = "ID_SUBGRUPO";
            ddlSubgrupo.DataBind();

            ddlSubgrupo.Items.Insert(0, new ListItem("Todos", "0"));
        }

        #endregion

        #region Métodos

        protected void ddlOperadora_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaProcedimentos();
            CarregaGrupos();
            CarregaSubgrupos();
        }

        protected void ddlProcedimento_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGrupos();
            CarregaSubgrupos();
        }

        protected void ddlGrupo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubgrupos();
        }

        #endregion
    }
}
