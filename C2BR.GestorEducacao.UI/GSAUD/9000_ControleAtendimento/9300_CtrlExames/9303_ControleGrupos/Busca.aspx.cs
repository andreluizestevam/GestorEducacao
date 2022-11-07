//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: PESQUISAS/ENQUETES DE SATISFAÇÃO
// OBJETIVO: CADASTRAMENTO DE GRUPOS DE PESQUISAS INSTITUCIONAIS
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
using Resources;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9300_CtrlExames._9303_ControleGrupos
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
            if (!Page.IsPostBack)
            {
                CarregaOperadoras();
                CarregaProcedimentos();
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_GRUPO" };

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
                DataField = "NO_GRUPO",
                HeaderText = " Grupo"
            });

             CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "FL_SITUA_EXAME",
                HeaderText = "Status"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            var idOper = !String.IsNullOrEmpty(ddlOperadora.SelectedValue) ? int.Parse(ddlOperadora.SelectedValue) : 0;
            var idProc = !String.IsNullOrEmpty(ddlProcedimento.SelectedValue) ? int.Parse(ddlProcedimento.SelectedValue) : 0;

            var resultado = (from tbs412 in TBS412_EXAME_GRUPO.RetornaTodosRegistros()
                             where (!String.IsNullOrEmpty(txtGrupo.Text) ? tbs412.NO_GRUPO_EXAME.Contains(txtGrupo.Text) : true)
                             && (!String.IsNullOrEmpty(txtCodGrupo.Text) ? tbs412.CO_GRUPO.Contains(txtCodGrupo.Text) : true)
                             && (idOper != 0 ? tbs412.TBS356_PROC_MEDIC_PROCE.TB250_OPERA.ID_OPER == idOper : true)
                             && (idProc != 0 ? tbs412.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == idProc : true)
                             select new 
                             {
                                 tbs412.ID_GRUPO,
                                 tbs412.NO_GRUPO_EXAME,
                                 NO_GRUPO = !String.IsNullOrEmpty(tbs412.CO_GRUPO) ? tbs412.CO_GRUPO + " - " + tbs412.NO_GRUPO_EXAME : tbs412.NO_GRUPO_EXAME,
                                 tbs412.TBS356_PROC_MEDIC_PROCE.TB250_OPERA.NM_SIGLA_OPER,
                                 tbs412.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
                                 FL_SITUA_EXAME = tbs412.FL_SITUA_EXAME == "A" ? "Ativo" : "Inativo",
                                 tbs412.NU_ORDEM
                             }).OrderBy(t => t.NU_ORDEM).ThenBy(t => t.NO_GRUPO_EXAME);

            CurrentPadraoBuscas.GridBusca.DataSource = resultado ;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_GRUPO"));
            
            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        #endregion

        #region Carregamento

        public void CarregaOperadoras()
        {
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddlOperadora, true);
        }

        public void CarregaProcedimentos()
        {
            var idOper = !String.IsNullOrEmpty(ddlOperadora.SelectedValue) ? int.Parse(ddlOperadora.SelectedValue) : 0;

            AuxiliCarregamentos.CarregaProcedimentosMedicos(ddlProcedimento, idOper, true);
        }

        #endregion

        #region Métodos

        protected void ddlOperadora_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaProcedimentos();
        }

        #endregion
    }
}
