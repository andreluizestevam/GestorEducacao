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
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1900_TabelasGenerCtrlAdmEscolar.F1910_CadastroGrupoCBO
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
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_CBO_GRUPO", "DE_CBO_GRUPO" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_CBO_GRUPO",
                HeaderText = "Descrição"
            });

            BoundField bf10 = new BoundField();
            bf10.DataField = "FL_SITUA";
            bf10.HeaderText = "Situação";
            bf10.ItemStyle.CssClass = "centro";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf10);
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            string situa = ddlSituacao.SelectedValue != "" ? ddlSituacao.SelectedValue : "";

            var resultado = (from tb316 in TB316_CBO_GRUPO.RetornaTodosRegistros()
                             where (txtDescricao.Text != "" ? tb316.DE_CBO_GRUPO.Contains(txtDescricao.Text) : 0 == 0)
                             && (situa != "" ? situa == tb316.FL_SITUA : 0 == 0)
                             select new
                             {
                                 tb316.CO_CBO_GRUPO,
                                 tb316.DE_CBO_GRUPO,
                                 tb316.FL_SITUA
                             });

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado.OrderBy(c => c.DE_CBO_GRUPO) : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>("ID", "CO_CBO_GRUPO"));
            queryStringKeys.Add(new KeyValuePair<string, string>("Descricao", "DE_CBO_GRUPO"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion
    }
}