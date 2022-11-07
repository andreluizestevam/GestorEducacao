//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE TESOURARIA (CAIXA)
// OBJETIVO: CADASTRAMENTO DE CAIXAS
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
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5900_TabelasGenerCtrlFinaceira.F5913_Agrup_RecDesp
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

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_AGRUP_RECDESP" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_SITU_AGRUP_RECDESP",
                HeaderText = "Nome Agrupador"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_AGRUP_RECDESP",
                HeaderText = "Sigla Agrupador"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "TP_AGRUP_RECDESP",
                HeaderText = "Tipo Agrupador"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_SITU_AGRUP_RECDESP",
                HeaderText = "Situação Agrupador"
            });

            BoundField bf1 = new BoundField();
            bf1.DataField = "DT_SITU_AGRUP_RECDESP";
            bf1.HeaderText = "Cadastro";
            bf1.DataFormatString = "{0:dd/MM/yyyy}";
            bf1.ItemStyle.CssClass = "centro";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf1);
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            var resultado = (from tb315 in TB315_AGRUP_RECDESP.RetornaTodosRegistros()
                             where (txtNomeAgrupador.Text != "" ? tb315.DE_SITU_AGRUP_RECDESP.Contains(txtNomeAgrupador.Text) : txtNomeAgrupador.Text == "")
                            && (ddlTpAgrupador.SelectedValue != "T" ? (tb315.TP_AGRUP_RECDESP.Equals(ddlTpAgrupador.SelectedValue)) : ddlTpAgrupador.SelectedValue == "T")                                 
                            select new
                            {
                                tb315.ID_AGRUP_RECDESP, tb315.DE_SITU_AGRUP_RECDESP, tb315.CO_AGRUP_RECDESP, tb315.DT_SITU_AGRUP_RECDESP,
                                TP_AGRUP_RECDESP = tb315.TP_AGRUP_RECDESP == "T" ? "Todos" : tb315.TP_AGRUP_RECDESP == "R" ? "Receitas" : "Despesas",
                                CO_SITU_AGRUP_RECDESP = tb315.CO_SITU_AGRUP_RECDESP == "A" ? "Ativo" : "Inativo"
                            }).OrderBy(d => d.DE_SITU_AGRUP_RECDESP);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_AGRUP_RECDESP"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion
    }
}