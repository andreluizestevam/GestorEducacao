//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: **********************
// SUBMÓDULO: *******************
// OBJETIVO: ********************
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
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5900_TabelasGenerCtrlFinaceira.F5907_CarteiraBanco
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

        private void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                CarregaBancos();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "IDEBANCO", "CO_CARTEIRA" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DESBANCO",
                HeaderText = "Banco"
            });

            BoundField bfCarteira = new BoundField();
            bfCarteira.DataField = "CO_CARTEIRA";
            bfCarteira.HeaderText = "Carteira";
            bfCarteira.ItemStyle.CssClass = "codCol";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfCarteira);

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_CARTEIRA",
                HeaderText = "Descrição"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            string srtIdeBanco = ddlBanco.SelectedValue;

            var resultado = (from tb226 in TB226_CARTEIRA_BANCO.RetornaTodosRegistros()
                             where (srtIdeBanco != "" ? tb226.TB29_BANCO.IDEBANCO == srtIdeBanco : srtIdeBanco == "")
                             select new
                             {
                                 tb226.TB29_BANCO.IDEBANCO, tb226.TB29_BANCO.DESBANCO, tb226.CO_CARTEIRA, tb226.DE_CARTEIRA
                             }).OrderBy( c => c.DESBANCO ).ThenBy( c => c.CO_CARTEIRA );

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>("ideBanco", "IDEBANCO"));
            queryStringKeys.Add(new KeyValuePair<string, string>("coCarteira", "CO_CARTEIRA"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

//====> Método que carrega o DropDown de Bancos
        private void CarregaBancos()
        {
            ddlBanco.DataSource = (from tb29 in TB29_BANCO.RetornaTodosRegistros().AsEnumerable()
                                   select new { tb29.IDEBANCO, DESBANCO = string.Format("{0} - {1}", tb29.IDEBANCO, tb29.DESBANCO) }).OrderBy( b => b.DESBANCO );

            ddlBanco.DataValueField = "IDEBANCO";
            ddlBanco.DataTextField = "DESBANCO";
            ddlBanco.DataBind();

            ddlBanco.Items.Insert(0, new ListItem("Todos", ""));
        }
        #endregion
    }
}
