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
// 27/11/14 | Maxwell Almeida            | Criãção da funcionalidade para pesquisa de vacinas


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
namespace C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9200_CtrlVacinas._9210_CtrlCadastral._9201_CadastroVacinas
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
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new String[] { "ID_VACINA" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NM_VACINA",
                HeaderText = "NOME"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_SIGLA_VACINA",
                HeaderText = "SIGLA"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_SITUACAO",
                HeaderText = "SITUAÇÃO"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {

            var res = (from tbs345 in TBS345_VACINA.RetornaTodosRegistros()
                       where (txtNome.Text != "" ? tbs345.NM_VACINA.Contains(txtNome.Text) : txtNome.Text == "")
                       && (txtSigla.Text != "" ? tbs345.CO_SIGLA_VACINA.Contains(txtSigla.Text) : txtSigla.Text == "")
                       && (ddlSituacao.SelectedValue != "0" ? tbs345.CO_SITUA_VACINA == ddlSituacao.SelectedValue : 0 == 0)
                       select new
                       {
                           tbs345.ID_VACINA,
                           tbs345.NM_VACINA,
                           tbs345.CO_SIGLA_VACINA,
                           DE_SITUACAO = (tbs345.CO_SITUA_VACINA == "A" ? "ATIVO" : tbs345.CO_SITUA_VACINA == "I" ? "INATIVO" : "SUSPENSO"),
                       }).OrderBy(w => w.NM_VACINA);

            CurrentPadraoBuscas.GridBusca.DataSource = (res.Count() > 0) ? res : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_VACINA"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        #endregion
    }
}