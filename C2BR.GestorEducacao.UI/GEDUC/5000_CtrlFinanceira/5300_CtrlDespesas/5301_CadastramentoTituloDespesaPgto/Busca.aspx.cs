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
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5300_CtrlDespesas.F5301_CadastramentoTituloDespesaPgto
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

            CarregaFornecedores();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "NU_DOC", "NU_PAR" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NU_DOC",
                HeaderText = "N° Documento"
            });

            BoundField bf2 = new BoundField();
            bf2.DataField = "NU_PAR";
            bf2.HeaderText = "Parc";
            bf2.ItemStyle.CssClass = "centro";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf2);

            BoundField bf9 = new BoundField();
            bf9.DataField = "CO_CON_DESFIX";
            bf9.HeaderText = "Contrato";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf9);

            BoundField bf10 = new BoundField();
            bf10.DataField = "CO_ADITI_DESFIX";
            bf10.HeaderText = "Adit";
            bf10.ItemStyle.CssClass = "centro";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf10);

            BoundField bf1 = new BoundField();
            bf1.DataField = "DT_CAD_DOC";
            bf1.HeaderText = "Cadastro";
            bf1.DataFormatString = "{0:dd/MM/yyyy}";
            bf1.ItemStyle.CssClass = "centro";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf1);

            BoundField bf11 = new BoundField();
            bf11.DataField = "DT_EMISS_DOCTO";
            bf11.HeaderText = "Emissão";
            bf11.DataFormatString = "{0:dd/MM/yyyy}";
            bf11.ItemStyle.CssClass = "centro";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf11);

            BoundField bf3 = new BoundField();
            bf3.DataField = "DT_VEN_DOC";
            bf3.HeaderText = "Vencto";
            bf3.DataFormatString = "{0:dd/MM/yyyy}";
            bf3.ItemStyle.CssClass = "centro";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf3);

            BoundField bf4 = new BoundField();
            bf4.DataField = "DT_REC_DOC";
            bf4.HeaderText = "Recebto";
            bf4.DataFormatString = "{0:dd/MM/yyyy}";
            bf4.ItemStyle.CssClass = "centro";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf4);

            BoundField bf5 = new BoundField();
            bf5.DataField = "VR_PAR_DOC";
            bf5.HeaderText = "R$ Parc";
            bf5.DataFormatString = "{0:N}";
            bf5.ItemStyle.CssClass = "direita";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf5);

            BoundField bf6 = new BoundField();
            bf6.DataField = "VR_PAG";
            bf6.HeaderText = "R$ Pago";
            bf6.DataFormatString = "{0:N}";
            bf6.ItemStyle.CssClass = "direita";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf6);

            BoundField bf7 = new BoundField();
            bf7.DataField = "IC_SIT_DOC";
            bf7.HeaderText = "Status";
            bf7.ItemStyle.CssClass = "centro";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf7);
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            string strTipoPeriodo = ddlTipoPeriodo.SelectedValue;
            string strSituacao = ddlSituacao.SelectedValue;
            int coForn = ddlNomeFornecedor.SelectedValue != "" ? int.Parse(ddlNomeFornecedor.SelectedValue) : 0;

            DateTime dataInicio = DateTime.MinValue;
            DateTime dataFim = DateTime.MinValue;
            DateTime? dataVerifIni = null;
            DateTime? dataVerifFim = null;

            DateTime.TryParse(txtPeriodoDe.Text, out dataInicio);
            DateTime.TryParse(txtPeriodoAte.Text, out dataFim);

            dataVerifIni = dataInicio == DateTime.MinValue ? null : (DateTime?)dataInicio;
            dataVerifFim = dataFim == DateTime.MinValue ? null : (DateTime?)dataFim;

            var resultado = (from tb38 in TB38_CTA_PAGAR.RetornaTodosRegistros()
                            where (txtNumeroDocumento.Text != "" ? tb38.NU_DOC == txtNumeroDocumento.Text : txtNumeroDocumento.Text == "")
                            && (dataVerifIni == null || (strTipoPeriodo == "V" && tb38.DT_VEN_DOC >= dataVerifIni) ||
                            (strTipoPeriodo == "C" && tb38.DT_CAD_DOC >= dataVerifIni) || (strTipoPeriodo == "E" && tb38.DT_EMISS_DOCTO >= dataVerifIni))
                            && (dataVerifFim == null || (strTipoPeriodo == "V" && tb38.DT_VEN_DOC <= dataVerifFim) ||
                            (strTipoPeriodo == "C" && tb38.DT_CAD_DOC <= dataVerifFim) || (strTipoPeriodo == "E" && tb38.DT_EMISS_DOCTO <= dataVerifFim))
                            && (strSituacao != "" ? tb38.IC_SIT_DOC == strSituacao : strSituacao == "")
                            && (coForn != 0 ? tb38.TB41_FORNEC.CO_FORN == coForn : coForn == 0) && tb38.CO_EMP == LoginAuxili.CO_EMP
                            select new
                            {
                                tb38.NU_DOC, tb38.NU_PAR, tb38.DT_CAD_DOC, tb38.DT_VEN_DOC, tb38.DT_REC_DOC, tb38.VR_PAR_DOC,
                                IC_SIT_DOC = tb38.IC_SIT_DOC == "A" ? "Em Aberto" : (tb38.IC_SIT_DOC == "Q" ? "Quitado" : (tb38.IC_SIT_DOC == "P" ? "Parcialmente Quitado" : "Cancelado")),                                  
                                tb38.DT_EMISS_DOCTO, tb38.CO_CON_DESFIX, tb38.CO_ADITI_DESFIX, tb38.VR_PAG
                                //CO_CPFCGC_FORN = (tb38.TB41_FORNEC.TP_FORN == "F" && tb38.TB41_FORNEC.CO_CPFCGC_FORN.Length >= 11) ? tb38.TB41_FORNEC.CO_CPFCGC_FORN.Insert(9, "-").Insert(6, ".").Insert(3, ".") :
                                //((tb38.TB41_FORNEC.TP_FORN == "J" && tb38.TB41_FORNEC.CO_CPFCGC_FORN.Length >= 14) ? tb38.TB41_FORNEC.CO_CPFCGC_FORN.Insert(12, "-").Insert(8, "/").Insert(5, ".").Insert(2, ".") : tb38.TB41_FORNEC.CO_CPFCGC_FORN)
                            });

            if (strTipoPeriodo == "V" && (dataVerifIni != null || dataVerifFim != null))
                CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado.OrderByDescending( c => c.DT_VEN_DOC ) : null;
            else if (strTipoPeriodo == "E" && (dataVerifIni != null || dataVerifFim != null))
                CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado.OrderByDescending( c => c.DT_EMISS_DOCTO ) : null;
            else
                CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado.OrderByDescending( c => c.DT_CAD_DOC ) : null;
        }
        
        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>("doc", "NU_DOC"));
            queryStringKeys.Add(new KeyValuePair<string, string>("par", "NU_PAR"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento

//====> Método que carrega o DropDown de Fornecedores
        private void CarregaFornecedores()
        {
            ddlNomeFornecedor.DataSource = (from tb41 in TB41_FORNEC.RetornaTodosRegistros()
                                            where tb41.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                            select new { tb41.CO_FORN, tb41.NO_FAN_FOR }).OrderBy( f => f.NO_FAN_FOR );

            ddlNomeFornecedor.DataTextField = "NO_FAN_FOR";
            ddlNomeFornecedor.DataValueField = "CO_FORN";
            ddlNomeFornecedor.DataBind();

            ddlNomeFornecedor.Items.Insert(0, new ListItem("Selecione", ""));
        }

//====> Método que preenche informação de acordo com o Nome do Fornecedor selecionado
        private void CarregaCodigoFornecedor()
        {
            int coForn = ddlNomeFornecedor.SelectedValue != "" ? int.Parse(ddlNomeFornecedor.SelectedValue) : 0;

            txtCodigoFornecedor.Text = "";

            if (coForn == 0)   
                return; 

            TB41_FORNEC tb41 = TB41_FORNEC.RetornaPelaChavePrimaria(coForn);

            if (tb41 != null)
            {
                txtCodigoFornecedor.Text = (tb41.TP_FORN == "F" && tb41.CO_CPFCGC_FORN.Length >= 11) ? tb41.CO_CPFCGC_FORN.Insert(9, "-").Insert(6, ".").Insert(3, ".") :
                                ((tb41.TP_FORN == "J" && tb41.CO_CPFCGC_FORN.Length >= 14) ? tb41.CO_CPFCGC_FORN.Insert(12, "-").Insert(8, "/").Insert(5, ".").Insert(2, ".") : tb41.CO_CPFCGC_FORN);
            }
            else
                txtCodigoFornecedor.Text = "";                 
        }
        #endregion

        protected void ddlNomeFornecedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCodigoFornecedor();
        }
    }
}