//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE TESOURARIA (CAIXA)
// OBJETIVO: FECHAMENTO DE CAIXA
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
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5100_CtrlTesouraria.F5105_FechamentoCaixa
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
            if (Page.IsPostBack)
                return;

            CarregaFuncionarios();
            CarregaCaixas();
            CarregaDatas();
            CarregaRespAberCaixa();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_EMP", "CO_CAIXA", "DT_MOVIMENTO", "CO_COLABOR_CAIXA" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_CAIXA",
                HeaderText = "Caixa"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "COLAB_CAIXA",
                HeaderText = "Colabor Caixa"
            }); 

            BoundField bf1 = new BoundField();
            bf1.DataField = "DT_MOVIMENTO";
            bf1.HeaderText = "Data Movto";
            bf1.DataFormatString = "{0:dd/MM/yyyy}";
            bf1.ItemStyle.CssClass = "centro";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf1);
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coCaixa = ddlNomeCaixa.SelectedValue != "T" ? int.Parse(ddlNomeCaixa.SelectedValue) : 0;
            int coRespAbeCaixa = ddlRespAberCaixa.SelectedValue != "T" ? int.Parse(ddlRespAberCaixa.SelectedValue) : 0;
            int coColCaixa = ddlFuncCaixa.SelectedValue != "T" ? int.Parse(ddlFuncCaixa.SelectedValue) : 0;
            DateTime? dataMovto = ddlDtMovto.SelectedValue != "T" ? (DateTime?)DateTime.Parse(ddlDtMovto.SelectedValue) : null;

            var resultado = (from tb295 in TB295_CAIXA.RetornaTodosRegistros()
                            join tb113 in TB113_PARAM_CAIXA.RetornaTodosRegistros()
                            on tb295.CO_CAIXA equals tb113.CO_CAIXA
                            where (tb295.TB03_COLABOR.CO_EMP == LoginAuxili.CO_EMP)
                            && (coCaixa != 0 ? (tb295.CO_CAIXA == coCaixa) : coCaixa == 0)
                            && (tb295.DT_FECHAMENTO_CAIXA == null) && (tb295.TB03_COLABOR.CO_EMP == tb113.TB25_EMPRESA.CO_EMP)
                            && (coRespAbeCaixa != 0 ? (tb295.CO_USUARIO_ABERT == coRespAbeCaixa) : coRespAbeCaixa == 0)
                            && (coColCaixa != 0 ? (tb295.TB03_COLABOR.CO_COL == coColCaixa) : coColCaixa == 0)
                            && (dataMovto != null ? tb295.DT_MOVIMENTO == dataMovto : dataMovto == null)
                            select new
                            {
                                tb113.DE_CAIXA, tb295.CO_EMP, tb295.CO_CAIXA, tb295.DT_MOVIMENTO, CO_COLABOR_CAIXA = tb295.TB03_COLABOR.CO_COL,
                                COLAB_CAIXA = tb295.TB03_COLABOR.CO_MAT_COL.Insert(5, "-").Insert(2, "-") + " " + tb295.TB03_COLABOR.NO_COL
                            }).OrderBy(d => d.DE_CAIXA);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoEmp, "CO_EMP"));
            queryStringKeys.Add(new KeyValuePair<string, string>("coCaixa", "CO_CAIXA"));
            queryStringKeys.Add(new KeyValuePair<string, string>("dtMov", "DT_MOVIMENTO"));
            queryStringKeys.Add(new KeyValuePair<string, string>("coColCaixa", "CO_COLABOR_CAIXA"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

//====> Método que carrega o DropDown de Caixas
        private void CarregaCaixas()
        {
            ddlNomeCaixa.Items.Clear();

            ddlNomeCaixa.DataSource = TB113_PARAM_CAIXA.RetornaTodosRegistros().Where(p => (p.CO_FLAG_USO_CAIXA == "A") && (p.CO_SITU_CAIXA == "A") 
                                                                                            && (p.CO_EMP == LoginAuxili.CO_EMP));

            ddlNomeCaixa.DataTextField = "DE_CAIXA";
            ddlNomeCaixa.DataValueField = "CO_CAIXA";
            ddlNomeCaixa.DataBind();

            ddlNomeCaixa.Items.Insert(0,new ListItem("Todas","T"));
        }

//====> Método que carrega o DropDown de Responsáveis pela abertura do Caixa
        private void CarregaRespAberCaixa()
        {
            ddlRespAberCaixa.Items.Clear();

            ddlRespAberCaixa.DataSource = (from admUsuario in ADMUSUARIO.RetornaTodosRegistros()
                                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on admUsuario.CodUsuario equals tb03.CO_COL
                                           where admUsuario.CO_EMP == tb03.CO_EMP && admUsuario.FLA_MANUT_CAIXA == "S"
                                           && (admUsuario.TipoUsuario == "F" || admUsuario.TipoUsuario == "S") && admUsuario.CO_EMP == LoginAuxili.CO_EMP
                                           select new { admUsuario.ideAdmUsuario, tb03.NO_COL }).OrderBy( a => a.NO_COL );

            ddlRespAberCaixa.DataTextField = "NO_COL";
            ddlRespAberCaixa.DataValueField = "ideAdmUsuario";
            ddlRespAberCaixa.DataBind();

            ddlRespAberCaixa.Items.Insert(0, new ListItem("Todas", "T"));            
        }

//====> Método que carrega o DropDown de Funcionários do Caixa
        private void CarregaFuncionarios()
        {
            ddlFuncCaixa.Items.Clear();

            ddlFuncCaixa.DataSource = (from admUsuario in ADMUSUARIO.RetornaTodosRegistros()
                                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on admUsuario.CodUsuario equals tb03.CO_COL
                                       where admUsuario.CO_EMP == tb03.CO_EMP && admUsuario.FLA_MANUT_CAIXA == "S"
                                       && (admUsuario.TipoUsuario == "F" || admUsuario.TipoUsuario == "S")
                                       select new { tb03.CO_COL, tb03.NO_COL }).OrderBy( a => a.NO_COL );

            ddlFuncCaixa.DataTextField = "NO_COL";
            ddlFuncCaixa.DataValueField = "CO_COL";
            ddlFuncCaixa.DataBind();

            ddlFuncCaixa.Items.Insert(0, new ListItem("Todas", "T"));
        }

//====> Método que carrega o DropDown de Datas
        private void CarregaDatas()
        {
            ddlDtMovto.Items.Clear();

            var dtMovtos = (from tb295 in TB295_CAIXA.RetornaTodosRegistros()
                            where tb295.TB03_COLABOR.CO_EMP.Equals(LoginAuxili.CO_EMP) && tb295.DT_FECHAMENTO_CAIXA == null
                            select new { tb295.DT_MOVIMENTO }).Distinct().OrderBy( c => c.DT_MOVIMENTO );

            foreach (var data in dtMovtos)
	        {
                ddlDtMovto.Items.Add(new ListItem(data.DT_MOVIMENTO.ToString("dd/MM/yyyy"), data.DT_MOVIMENTO.ToString("dd/MM/yyyy")));
	        }            

            ddlDtMovto.Items.Insert(0, new ListItem("Todas", "T"));
        }
        #endregion
    }
}