//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE GERAL DE CHEQUES
// OBJETIVO: CADASTRAMENTO DE CHEQUES EMITIDOS OU RECEBIDOS PELA INSTITUIÇÃO
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
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5800_CtrlDiversosFinanceiro.F5810_CtrlCheques.F5811_CadastramentoChequeEmitidoInstit
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

            CarregaBancos();
        }        

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ORG_CODIGO_ORGAO", "CO_CHEQUE" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "IDEBANCO",
                HeaderText = "Banco"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_AGENCIA",
                HeaderText = "Agência"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NU_CONTA",
                HeaderText = "Conta"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NU_CHEQUE",
                HeaderText = "Cheque"
            });

            BoundField bf2 = new BoundField();
            bf2.DataField = "VALOR";
            bf2.HeaderText = "Valor";
            bf2.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf2);

            BoundField bf1 = new BoundField();
            bf1.DataField = "DT_VENCIMENTO";
            bf1.HeaderText = "Vencto";
            bf1.DataFormatString = "{0:dd/MM/yyyy}";
            bf1.ItemStyle.CssClass = "centro";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf1);
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            string strIdeBanco = ddlBanco.SelectedValue;
            int coAgencia = ddlAgencia.SelectedValue != "" ? int.Parse(ddlAgencia.SelectedValue) : 0;

            var resultado = (from tb158 in tb158_cheques.RetornaTodosRegistros()
                             where (strIdeBanco != "" ? tb158.TB29_BANCO.IDEBANCO == strIdeBanco : strIdeBanco == "")
                             && (coAgencia != 0 ? tb158.co_agencia == coAgencia : coAgencia == 0)          
                             && tb158.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                             select new
                             {
                                 tb158.ORG_CODIGO_ORGAO, tb158.co_cheque, tb158.TB29_BANCO.IDEBANCO, tb158.co_agencia,
                                 tb158.nu_conta, tb158.nu_cheque, tb158.valor, tb158.dt_vencimento
                             }).OrderBy(d => d.nu_cheque).ThenBy(p => p.IDEBANCO);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>("codOrgao", "ORG_CODIGO_ORGAO"));
            queryStringKeys.Add(new KeyValuePair<string, string>("coCheque", "CO_CHEQUE"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

//====> Método que carrega o DropDown de Bancos
        protected void CarregaBancos()
        {
            ddlBanco.DataSource = (from tb29 in TB29_BANCO.RetornaTodosRegistros()
                                   select new { tb29.IDEBANCO });

            ddlBanco.DataTextField = "IDEBANCO";
            ddlBanco.DataValueField = "IDEBANCO";
            ddlBanco.DataBind();

            ddlBanco.Items.Insert(0, new ListItem("Selecione", ""));

            CarregaAgencia();
        }

//====> Método que carrega o DropDown de Agencias
        protected void CarregaAgencia()
        {
            string strIdeBanco = ddlBanco.SelectedValue != "" ? ddlBanco.SelectedValue : "0";

            ddlAgencia.DataSource = (from tb30 in TB30_AGENCIA.RetornaTodosRegistros()
                                     where tb30.IDEBANCO == strIdeBanco
                                     select new { tb30.CO_AGENCIA });

            ddlAgencia.DataTextField = "CO_AGENCIA";
            ddlAgencia.DataValueField = "CO_AGENCIA";
            ddlAgencia.DataBind();

            ddlAgencia.Items.Insert(0, new ListItem("Selecione", ""));       
        }
        #endregion

        protected void ddlBanco_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAgencia();
        }
    }
}