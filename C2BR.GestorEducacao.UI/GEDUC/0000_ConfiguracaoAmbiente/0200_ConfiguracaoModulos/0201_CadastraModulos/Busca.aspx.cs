//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: CONFIGURAÇÃO DE MÓDULOS E FUNCIONALIDADES
// OBJETIVO: CADASTRA MÓDULOS/FUNCIONALIDADES
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
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0200_ConfiguracaoModulos.F0201_CadastraModulos
{
    public partial class Busca : System.Web.UI.Page
    {
        public PadraoBuscas CurrentPadraoBuscas {get { return ((PadraoBuscas)this.Master); } }

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
                CarregaModulos();
        }

        protected void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ideAdmModulo", "ideModuloPai" };            

            BoundField bf1 = new BoundField();
            bf1.DataField = "nomModulo";
            bf1.HeaderText = "Módulo Pai";
            bf1.ItemStyle.Width = 270;
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf1);

            BoundField bf2 = new BoundField();
            bf2.DataField = "nomDescricao";
            bf2.HeaderText = "Descrição";
            bf2.ItemStyle.Width = 280;
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf2);

            BoundField bf3 = new BoundField();
            bf3.DataField = "flaStatus";
            bf3.HeaderText = "Status";
            bf3.ItemStyle.Width = 40;
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf3);

            CurrentPadraoBuscas.GridBusca.PageSize = 10;
        }

        protected void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int idModulo = ddlModulo.SelectedValue != "" ? int.Parse(ddlModulo.SelectedValue) : 0;
            
            var resultado = (from admModulo in ADMMODULO.RetornaTodosRegistrosStatus()
                             where (idModulo != 0 ? idModulo != -1 ? (admModulo.ADMMODULO2.ideAdmModulo == idModulo) : admModulo.ADMMODULO2 == null : idModulo == 0)
                            && (txtNomeModulo.Text != "" ? admModulo.nomModulo.Contains(txtNomeModulo.Text) : txtNomeModulo.Text == "")
                            && (ddlStatusMod.SelectedValue != "" ? admModulo.flaStatus == ddlStatusMod.SelectedValue : ddlStatusMod.SelectedValue == "")
                            && (admModulo.flaTipoItemSubMenu == "ATM" || admModulo.flaTipoItemSubMenu == "LST")
                            select new
                            {
                                admModulo.ideAdmModulo, ideModuloPai = (admModulo.ADMMODULO2 == null ? 0 : admModulo.ADMMODULO2.ideAdmModulo),
                                admModulo.nomModulo, admModulo.nomDescricao, flaStatus = (admModulo.flaStatus == "A" ? "Ativo" : (admModulo.flaStatus == "I" ? "Inativo" : "Cancelado"))
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

//====> Método que carrega o DropDown de Módulos
        private void CarregaModulos()
        {
            ddlModulo.DataSource = (from admModulo in ADMMODULO.RetornaTodosRegistrosStatus()
                                    where admModulo.flaTipoItemSubMenu == "ATM"
                                    select new { nomModulo = admModulo.nomModulo, ideAdmModulo = admModulo.ideAdmModulo }).OrderBy( a => a.nomModulo );

            ddlModulo.DataTextField = "nomModulo";
            ddlModulo.DataValueField = "ideAdmModulo";
            ddlModulo.DataBind();

            ddlModulo.Items.Insert(0, new ListItem("Todos", ""));
            ddlModulo.Items.Insert(1, new ListItem("Nenhum", "-1"));
        }

        #endregion
    }
}