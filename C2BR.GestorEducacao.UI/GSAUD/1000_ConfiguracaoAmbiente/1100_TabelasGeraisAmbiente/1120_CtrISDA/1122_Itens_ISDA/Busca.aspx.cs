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
// 03/03/14 | Maxwell Almeida            | Criação da funcionalidade para busca Itens de Plantões


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
namespace C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1120_CtrISDA._1122_Itens_ISDA
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
                CarregaTiposISDA();
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new String[] { "ID_ITEM_ISDA" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NM_ITEM_ISDA",
                HeaderText = "NOME ISDA"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_SIGLA_ITEM_ISDA",
                HeaderText = "SIGLA"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_TIPO",
                HeaderText = "TIPO ISDA"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "SITUACAO",
                HeaderText = "SITUACAO"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int tipo = ddlTipoISDA.SelectedValue != "" ? int.Parse(ddlTipoISDA.SelectedValue) : 0;

            var res = (from tbs336 in TBS336_ISDA_ITENS.RetornaTodosRegistros()
                       join tbs335 in TBS335_ISDA_TIPO.RetornaTodosRegistros() on tbs336.ID_TIPO_ISDA equals tbs335.ID_TIPO_ISDA into l1
                       from ls in l1.DefaultIfEmpty()
                       where ((txtNome.Text) != "" ? tbs336.NM_ITEM_ISDA.Contains(txtNome.Text) : txtNome.Text == "")
                          && ((txtSigla.Text) != "" ? tbs336.CO_SIGLA_ITEM_ISDA.Contains(txtSigla.Text) : txtSigla.Text == "")
                          && (tipo != 0 ? tbs336.ID_TIPO_ISDA == tipo : 0 == 0)
                          && (tbs336.CO_SITUA_ITEM_ISDA == ddlSituaTipo.SelectedValue)
                       select new
                       {
                           tbs336.ID_ITEM_ISDA,
                           tbs336.NM_ITEM_ISDA,
                           tbs336.CO_SIGLA_ITEM_ISDA,
                           NO_TIPO = ls.NM_TIPO_ISDA,
                           SITUACAO = tbs336.CO_SITUA_ITEM_ISDA == "A" ? "Ativo" : "Inativo",
                       }).OrderBy(w => w.NM_ITEM_ISDA);

            CurrentPadraoBuscas.GridBusca.DataSource = (res.Count() > 0) ? res : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_ITEM_ISDA"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        #endregion

        #region Carregamentos

        private void CarregaTiposISDA()
        {
            AuxiliCarregamentos.CarregaTiposISDA(ddlTipoISDA, true, true);
        }

        #endregion
    }
}