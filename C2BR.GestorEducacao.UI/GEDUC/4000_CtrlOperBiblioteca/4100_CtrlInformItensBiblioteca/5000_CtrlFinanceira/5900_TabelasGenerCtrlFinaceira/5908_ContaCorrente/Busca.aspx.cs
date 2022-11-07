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
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5900_TabelasGenerCtrlFinaceira.F5908_ContaCorrente
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
                CarregaBancos();
                CarregaAgencias();
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "IDEBANCO", "CO_AGENCIA", "CO_CONTA" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DESBANCO",
                HeaderText = "Banco"
            });

            BoundField bfAgencia = new BoundField();
            bfAgencia.DataField = "CO_AGENCIA_DIG";
            bfAgencia.HeaderText = "Agência";
            bfAgencia.ItemStyle.CssClass = "codCol";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfAgencia);

            BoundField bfConta = new BoundField();
            bfConta.DataField = "CO_CONTA_DIG";
            bfConta.HeaderText = "Conta";
            bfConta.ItemStyle.CssClass = "codCol";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfConta);
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coAgencia = ddlAgencia.SelectedValue != "" ? int.Parse(ddlAgencia.SelectedValue) : 0;
            string srtIdeBanco = ddlBanco.SelectedValue;

            var resultado = (from tb224 in TB224_CONTA_CORRENTE.RetornaTodosRegistros().Include(typeof(TB000_INSTITUICAO).Name).AsEnumerable()
                             join tb30 in TB30_AGENCIA.RetornaTodosRegistros().Include(typeof(TB29_BANCO).Name).AsEnumerable() on tb224.TB30_AGENCIA.CO_AGENCIA equals tb30.CO_AGENCIA
                             where (srtIdeBanco != "" ? tb224.IDEBANCO == srtIdeBanco : srtIdeBanco == "")
                             && (coAgencia != 0 ? tb224.CO_AGENCIA.Equals(coAgencia) : coAgencia == 0)
                             && (tb224.TB000_INSTITUICAO.ORG_CODIGO_ORGAO.Equals(LoginAuxili.ORG_CODIGO_ORGAO))
                             && tb30.IDEBANCO == tb224.IDEBANCO
                             select new
                             {
                                 IDEBANCO = tb30.TB29_BANCO.IDEBANCO,
                                 DESBANCO = tb30.TB29_BANCO.DESBANCO,
                                 CO_AGENCIA = tb224.CO_AGENCIA, CO_CONTA = tb224.CO_CONTA,
                                 CO_AGENCIA_DIG = string.Format("{0}-{1}", tb224.CO_AGENCIA, tb30.DI_AGENCIA.Trim()),
                                 CO_CONTA_DIG = string.Format("{0}-{1}", tb224.CO_CONTA.Trim(), tb224.CO_DIG_CONTA.Trim())
                             }).OrderBy( c => c.DESBANCO ).ThenBy( c => c.CO_AGENCIA ).ThenBy( c => c.CO_CONTA ).ToList();

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>("ideBanco", "IDEBANCO"));
            queryStringKeys.Add(new KeyValuePair<string, string>("coAgencia", "CO_AGENCIA"));
            queryStringKeys.Add(new KeyValuePair<string, string>("coConta", "CO_CONTA"));

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

//====> Método que carrega o DropDown de Agências
        private void CarregaAgencias()
        {
            string srtIdeBanco = ddlBanco.SelectedValue;

            ddlAgencia.DataSource = (from tb30 in TB30_AGENCIA.RetornaTodosRegistros().AsEnumerable()
                                     where (srtIdeBanco != "" ? tb30.IDEBANCO == srtIdeBanco : srtIdeBanco == "")
                                    select new
                                    {
                                        tb30.CO_AGENCIA,
                                        DESCRICAO = string.IsNullOrEmpty(ddlBanco.SelectedValue) ?
                                                    string.Format("({0}) {1} - {2}", tb30.IDEBANCO, tb30.CO_AGENCIA, tb30.NO_AGENCIA) :
                                                    string.Format("{0} - {1}", tb30.CO_AGENCIA, tb30.NO_AGENCIA)
                                    }).OrderBy( a => a.DESCRICAO );

            ddlAgencia.DataValueField = "CO_AGENCIA";
            ddlAgencia.DataTextField = "DESCRICAO";
            ddlAgencia.DataBind();

            ddlAgencia.Items.Insert(0, new ListItem("Todas", ""));
        }
        #endregion

        protected void ddlBanco_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAgencias();
        }
    }
}
