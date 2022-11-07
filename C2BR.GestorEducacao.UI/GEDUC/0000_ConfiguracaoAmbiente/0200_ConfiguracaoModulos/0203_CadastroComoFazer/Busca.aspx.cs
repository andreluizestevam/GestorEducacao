//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: CONFIGURAÇÃO DE MÓDULOS E FUNCIONALIDADES
// OBJETIVO: CADASTRO COMO FAZER
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
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0200_ConfiguracaoModulos.F0203_CadastroComoFazer
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
                CarregaFuncionalidade();
        }

        protected void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_PROXIPASSOS" };
            
            BoundField bfRealizado1 = new BoundField();
            bfRealizado1.DataField = "nomModulo";
            bfRealizado1.HeaderText = "Módulo Pai";
            bfRealizado1.ItemStyle.Width = 170;
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfRealizado1);
            
            BoundField bfRealizado2 = new BoundField();
            bfRealizado2.DataField = "NO_DESCRICAO";
            bfRealizado2.HeaderText = "Prox. Passos";
            bfRealizado2.ItemStyle.Width = 170;
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfRealizado2);

            BoundField bfRealizado3 = new BoundField();
            bfRealizado3.DataField = "DE_ITEM_REFER";
            bfRealizado3.HeaderText = "Item Refer";
            bfRealizado3.ItemStyle.Width = 150;
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfRealizado3);

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_ORDEM_MENU",
                HeaderText = "OM"
            });
          
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "LINK",
                HeaderText = "Link"                
            });
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "STATUS",
                HeaderText = "Status"
            });

            CurrentPadraoBuscas.GridBusca.PageSize = 10;
        }
        
        protected void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int idModulo = ddlFuncionalidadeCF.SelectedValue != "" ? int.Parse(ddlFuncionalidadeCF.SelectedValue) : 0;
     
            var resultado = (from comoFazer in TBPROX_PASSOS.RetornaTodosRegistros()
                             where (txtNomeFuncionalidadeCF.Text != "" ? comoFazer.NO_DESCRICAO.Contains(txtNomeFuncionalidadeCF.Text) : txtNomeFuncionalidadeCF.Text == "")
                             && (ddlStatusCF.SelectedValue != "T" ? comoFazer.CO_STATUS == ddlStatusCF.SelectedValue : ddlStatusCF.SelectedValue == "T")
                             && (idModulo != 0 ? (comoFazer.ADMMODULO.ideAdmModulo == idModulo) : idModulo == 0)
                             select new
                             {
                                comoFazer.CO_PROXIPASSOS, comoFazer.NO_DESCRICAO, comoFazer.DE_ITEM_REFER, 
                                LINK = comoFazer.CO_FLAG_LINK == "S" ? "Sim" : "Não", comoFazer.CO_ORDEM_MENU,
                                STATUS = comoFazer.CO_STATUS == "A" ? "Ativo" : "Inativo", comoFazer.ADMMODULO.nomModulo
                             }).OrderBy(p => p.nomModulo).ThenBy(p=>p.CO_ORDEM_MENU);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        protected void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_PROXIPASSOS"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

//====> Método que carrega o DropDown de Funcionalidades de Como Fazer
        private void CarregaFuncionalidade()
        {      
            ddlFuncionalidadeCF.DataSource = (from admModulo in ADMMODULO.RetornaTodosRegistrosStatus()
                                              where admModulo.nomURLModulo != null && admModulo.nomURLModulo != ""
                                              select new { admModulo.nomModulo, admModulo.ideAdmModulo }).OrderBy( a => a.nomModulo );

            ddlFuncionalidadeCF.DataTextField = "nomModulo";
            ddlFuncionalidadeCF.DataValueField = "ideAdmModulo";
            ddlFuncionalidadeCF.DataBind();

            ddlFuncionalidadeCF.Items.Insert(0, new ListItem("Todos", ""));
        }
        #endregion
    }
}
