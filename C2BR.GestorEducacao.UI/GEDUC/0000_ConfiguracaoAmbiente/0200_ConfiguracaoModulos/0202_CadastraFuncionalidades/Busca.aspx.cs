//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: CONFIGURAÇÃO DE MÓDULOS E FUNCIONALIDADES
// OBJETIVO: CADASTRA FUNCIONALIDADES
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
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0200_ConfiguracaoModulos.F0202_CadastraFuncionalidades
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
                CarregaFuncionalidades();
        }

        protected void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ideAdmModulo" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "nomModulo",
                HeaderText = "Funcionalidade",
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "flaStatus",
                HeaderText = "Status"
            });
        }
        
        protected void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int idModulo = ddlFuncionalidade.SelectedValue != "" ? int.Parse(ddlFuncionalidade.SelectedValue) : 0;

            var resultado = (from admModulo in ADMMODULO.RetornaTodosRegistrosStatus()
                             where (idModulo != 0 ? admModulo.ADMMODULO2.ideAdmModulo == idModulo : idModulo == 0)
                            && (txtNomeFuncionalidade.Text != "" ? admModulo.nomModulo.Contains(txtNomeFuncionalidade.Text) : txtNomeFuncionalidade.Text == "")
                            && (ddlStatusFunc.SelectedValue != "" ? admModulo.flaStatus == ddlStatusFunc.SelectedValue : ddlStatusFunc.SelectedValue == "")
                            && (admModulo.flaTipoItemSubMenu == null)
                            select new
                            {
                                admModulo.ideAdmModulo, admModulo.nomModulo,
                                flaStatus = (admModulo.flaStatus == "A" ? "Ativo" : (admModulo.flaStatus == "I" ? "Inativo" : "Cancelado"))
                            }).OrderBy( a => a.nomModulo );

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        protected void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {             
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ideAdmModulo"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

//====> Método que carrega o DropDown de Funcionalidades
        private void CarregaFuncionalidades()
        {
            ddlFuncionalidade.DataSource = (from admModulo in ADMMODULO.RetornaTodosRegistrosStatus()
                                            where (admModulo.flaTipoItemSubMenu == "LST" || admModulo.flaTipoItemSubMenu == "ATM")
                                            && admModulo.ADMMODULO2 != null
                                            select new { admModulo.nomModulo, admModulo.ideAdmModulo }).OrderBy( a => a.nomModulo );

            ddlFuncionalidade.DataTextField = "nomModulo";
            ddlFuncionalidade.DataValueField = "ideAdmModulo";
            ddlFuncionalidade.DataBind();

            ddlFuncionalidade.Items.Insert(0, new ListItem("Todos", ""));
        }
        #endregion
    }
}
