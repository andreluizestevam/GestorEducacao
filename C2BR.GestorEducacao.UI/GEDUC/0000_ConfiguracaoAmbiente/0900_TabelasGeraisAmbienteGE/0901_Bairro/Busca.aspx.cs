//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: BAIRRO
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
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0900_TabelasGeraisAmbienteGE.F0901_Bairro
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
                CarregaUF();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_BAIRRO"};

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_UF",
                HeaderText = "UF"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_CIDADE",
                HeaderText = "Cidade"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_BAIRRO",
                HeaderText = "Bairro"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coCidade = ddlCidadeCB.SelectedValue != "" ? int.Parse(ddlCidadeCB.SelectedValue) : 0;

            var resultado = (from tb905 in TB905_BAIRRO.RetornaTodosRegistros()
                             where (txtDescricaoCB.Text != "" ? tb905.NO_BAIRRO.Contains(txtDescricaoCB.Text) : txtDescricaoCB.Text == "")
                               && (ddlUfCB.SelectedValue != "" ? tb905.CO_UF == ddlUfCB.SelectedValue : ddlUfCB.SelectedValue == "")
                               && (coCidade != 0 ? tb905.CO_CIDADE == coCidade : coCidade == 0)
                               select new 
                               {
                                   tb905.CO_UF, tb905.TB904_CIDADE.NO_CIDADE, tb905.CO_BAIRRO, tb905.NO_BAIRRO
                               }).OrderBy( b => b.CO_UF ).ThenBy( b => b.NO_CIDADE ).ThenBy( b => b.NO_BAIRRO );

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_BAIRRO"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        #endregion

        #region Carregamento DropDown

//====> Método que carrega o DropDown de UFs
        private void CarregaUF()
        {
            ddlUfCB.DataSource = TB74_UF.RetornaTodosRegistros().OrderBy( u => u.CODUF );

            ddlUfCB.DataTextField = "CODUF";
            ddlUfCB.DataValueField = "CODUF";
            ddlUfCB.DataBind();

            ddlUfCB.Items.Insert(0, new ListItem("", ""));
        }

//====> Método que carrega o DropDown de Cidades
        private void CarregaCidades()
        {
            ddlCidadeCB.DataSource = TB904_CIDADE.RetornaPeloUF(ddlUfCB.SelectedValue).OrderBy( c => c.NO_CIDADE );

            ddlCidadeCB.DataTextField = "NO_CIDADE";
            ddlCidadeCB.DataValueField = "CO_CIDADE";
            ddlCidadeCB.DataBind();

            ddlCidadeCB.Items.Insert(0, new ListItem("", ""));
        }        
        #endregion

        protected void ddlUfCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCidades();
        }
    }
}