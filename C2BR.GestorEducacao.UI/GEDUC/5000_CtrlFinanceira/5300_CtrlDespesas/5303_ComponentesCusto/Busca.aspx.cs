//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE DESPESAS (CONTAS A PAGAR)
// OBJETIVO: CADASTRAMENTO GERAL DE TÍTULOS DE DESPESAS/COMPROMISSOS DE PAGAMENTOS
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
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5300_CtrlDespesas.F5303_ComponentesCusto
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
                CarregaUnidade();
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_ITEM_REFER_CUSTO", "NO_ITEM" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_ITEM",
                HeaderText = "Nome"
            });

            BoundField bf9 = new BoundField();
            bf9.DataField = "CO_REFER";
            bf9.HeaderText = "Código";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf9);

            BoundField bf10 = new BoundField();
            bf10.DataField = "FL_SITUA";
            bf10.HeaderText = "Situação";
            bf10.ItemStyle.CssClass = "centro";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf10);
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            string situa = ddlSituacao.SelectedValue != "" ? ddlSituacao.SelectedValue : "";
            int codUni = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            var resultado = (from tb429 in TB429_REFER_CUSTO.RetornaTodosRegistros()
                             where (codUni != 0 ? codUni == tb429.CO_EMP : 0 == 0)
                             && (txtNomeReduzido.Text != "" ? tb429.NO_ITEM.Contains(txtNomeReduzido.Text) : 0 == 0)
                             && (txtCodRefer.Text != "" ? tb429.CO_REFER.Contains(txtCodRefer.Text) : 0 == 0)
                             && (situa != "" ? situa == tb429.FL_SITUA : 0 == 0)
                             select new
                             {
                                 tb429.ID_ITEM_REFER_CUSTO,
                                 tb429.NO_ITEM,
                                 tb429.CO_REFER,
                                 tb429.FL_SITUA
                             });

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado.OrderBy(c => c.NO_ITEM) : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>("ID", "ID_ITEM_REFER_CUSTO"));
            queryStringKeys.Add(new KeyValuePair<string, string>("Nome", "NO_ITEM"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento

        //====> Método que carrega o DropDown de Unidades
        private void CarregaUnidade()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, false);
        }
        #endregion
    }
}