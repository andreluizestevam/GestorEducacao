//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: CIDADE
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
// 27/11/14 | Maxwell Almeida            | Criação da funcionalidade para busca SubGrupos de Procedimentos Médicos


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

namespace C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9110_CtrlProcedimentosMedicos._9112_SubGrupo
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
                CarregaGrupos();
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new String[] { "ID_PROC_MEDIC_SGRUP" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NM_PROC_MEDIC_GRUPO",
                HeaderText = "GRUPO"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NM_PROC_MEDIC_SGRUP",
                HeaderText = "SUBGRUPO"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_SITUACAO",
                HeaderText = "SITUAÇÃO"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coGrupo = ddlGrupo.SelectedValue != "" ? int.Parse(ddlGrupo.SelectedValue) : 0;
            var res = (from tbs355 in TBS355_PROC_MEDIC_SGRUP.RetornaTodosRegistros()
                       join tbs354 in TBS354_PROC_MEDIC_GRUPO.RetornaTodosRegistros() on tbs355.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO equals tbs354.ID_PROC_MEDIC_GRUPO
                       where (txtNoSubGrupo.Text != "" ? tbs355.NM_PROC_MEDIC_SGRUP.Contains(txtNoSubGrupo.Text) : txtNoSubGrupo.Text == "")
                       && (ddlSituacao.SelectedValue != "0" ? tbs354.FL_SITUA_PROC_MEDIC_GRUPO == ddlSituacao.SelectedValue : 0 == 0)
                       && (coGrupo != 0 ? tbs354.ID_PROC_MEDIC_GRUPO == coGrupo : 0 == 0)
                       select new
                       {
                           tbs355.ID_PROC_MEDIC_SGRUP,
                           tbs355.NM_PROC_MEDIC_SGRUP,
                           tbs354.NM_PROC_MEDIC_GRUPO,
                           DE_SITUACAO = (tbs355.FL_SITUA_PROC_MEDIC_GRUP == "A" ? "ATIVO" : tbs355.FL_SITUA_PROC_MEDIC_GRUP == "I" ? "INATIVO" : "SUSPENSO"),
                       }).OrderBy(w => w.NM_PROC_MEDIC_GRUPO);

            CurrentPadraoBuscas.GridBusca.DataSource = (res.Count() > 0) ? res : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_PROC_MEDIC_SGRUP"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        #endregion

        #region Carregamentos

        /// <summary>
        /// Carrega os Grupos
        /// </summary>
        private void CarregaGrupos()
        {
            AuxiliCarregamentos.CarregaGruposProcedimentosMedicos(ddlGrupo, true);
        }

        #endregion
    }
}
