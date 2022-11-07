//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: PESQUISAS/ENQUETES DE SATISFAÇÃO
// OBJETIVO: CADASTRAMENTO DE PESQUISAS INSITUCIONAIS
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
namespace C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9300_CtrlExames._9308_ControleSubGruposExameFisico
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
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_SUB_GRUPO_FISIC" };

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
                DataField = "FL_SITUA_SUBGR",
                HeaderText = "Situação"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int idGrupo = !String.IsNullOrEmpty(ddlGrupo.SelectedValue) ? int.Parse(ddlGrupo.SelectedValue) : 0;

            var res = (from tbs432 in TBS432_EXAME_FISIC_SUB_GRUPO.RetornaTodosRegistros()
                       where (!String.IsNullOrEmpty(txtSubgrupo.Text) ? tbs432.NO_SUB_GRUPO_FISIC.Contains(txtSubgrupo.Text) : true)
                       && (idGrupo != 0 ? tbs432.TBS431_GRUPO_EXAME_FISIC.ID_GRUPO_FISIC == idGrupo : true)
                       && (ddlSitu.SelectedValue.Equals("") ? true : tbs432.FL_SITUA_SUB_GRUPO_FISIC == ddlSitu.SelectedValue)
                       select new
                       {
                           tbs432.ID_SUB_GRUPO_FISIC,
                           tbs432.NO_SUB_GRUPO_FISIC,
                           tbs432.TBS431_GRUPO_EXAME_FISIC.NO_GRUPO_FISIC,
                           FL_SITUA_SUBGR = tbs432.FL_SITUA_SUB_GRUPO_FISIC == "A" ? "Ativo" : "Inativo",
                       }).OrderBy(x => x.NO_GRUPO_FISIC).ThenBy(y => y.NO_SUB_GRUPO_FISIC);

            CurrentPadraoBuscas.GridBusca.DataSource = res;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_SUB_GRUPO_FISIC"));

            
            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

        private void CarregaGrupos()
        {
            ddlGrupo.Items.Clear();

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

        #endregion

        #region Métodos

        #endregion
    }
}
