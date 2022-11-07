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
// 27/11/14 | Maxwell Almeida            | Criação da funcionalidade para busca Grupos de Procedimentos Médicos


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

namespace C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9110_CtrlProcedimentosMedicos._9111_Grupo
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
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new String[] { "ID_PROC_MEDIC_GRUPO" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NM_AREA_GRUPO",
                HeaderText = "ÁREA ABRANGÊNCIA"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NM_PROC_MEDIC_GRUPO",
                HeaderText = "GRUPO"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_SITUACAO",
                HeaderText = "SITUAÇÃO"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {

            var res = (from tbs354 in TBS354_PROC_MEDIC_GRUPO.RetornaTodosRegistros()
                       where (txtNoGrupo.Text != "" ? tbs354.NM_PROC_MEDIC_GRUPO.Contains(txtNoGrupo.Text) : txtNoGrupo.Text == "")
                       && (ddlSituacao.SelectedValue != "0" ? tbs354.FL_SITUA_PROC_MEDIC_GRUPO == ddlSituacao.SelectedValue : 0 == 0)
                       select new 
                       {
                           tbs354.ID_PROC_MEDIC_GRUPO,
                           tbs354.NM_PROC_MEDIC_GRUPO,
                           NM_AREA_GRUPO = (tbs354.NM_AREA_GRUPO == "T" ? "(T) Terapia Ocupacional"
                                            : (tbs354.NM_AREA_GRUPO == "O" ? "(O) Outros"
                                            : (tbs354.NM_AREA_GRUPO == "N" ? "(N) Nutrição"
                                            : (tbs354.NM_AREA_GRUPO == "S" ? "(S) Estética"
                                            : (tbs354.NM_AREA_GRUPO == "D" ? "(D) Odontologia"
                                            : (tbs354.NM_AREA_GRUPO == "P" ? "(P) Psicologia"
                                            : (tbs354.NM_AREA_GRUPO == "M" ? "(M) Médica"
                                            : (tbs354.NM_AREA_GRUPO == "F" ? "(F) Fonoaudiologia"
                                            : (tbs354.NM_AREA_GRUPO == "I" ? "(I) Fisioterapia"
                                            : (tbs354.NM_AREA_GRUPO == "E" ? "(E) Enfermagem" 
                                            : " - ")))))))))),
                           DE_SITUACAO = ( tbs354.FL_SITUA_PROC_MEDIC_GRUPO == "A" ? "ATIVO" : tbs354.FL_SITUA_PROC_MEDIC_GRUPO == "I" ? "INATIVO" : "SUSPENSO"),
                       }).OrderBy(w => w.NM_PROC_MEDIC_GRUPO);

            CurrentPadraoBuscas.GridBusca.DataSource = (res.Count() > 0) ? res : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_PROC_MEDIC_GRUPO"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        #endregion
    }
}
