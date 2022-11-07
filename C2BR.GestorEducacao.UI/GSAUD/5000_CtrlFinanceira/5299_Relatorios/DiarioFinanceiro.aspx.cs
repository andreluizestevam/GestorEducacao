//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE RECEITAS (CONTAS A RECEBER)
// OBJETIVO: POSIÇÃO DE TÍTULOS DE RECEITAS/RECURSOS - GERAL
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
using System.Runtime.InteropServices;
using System.IO;
using System.ServiceModel;
using System.Configuration;
using Resources;
using WRAuxiliares = C2BR.GestorEducacao.DLLRelatorioWeb.Auxiliares;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.DLLRelatorioWeb.Interfaces;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports.GSAUD._5000_CtrlFinanceira._5200_CtrlReceitas;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GSAUD.F5000_CtrlFinanceira.F5299_Relatorios
{
    public partial class DiarioFinanceiro : System.Web.UI.Page
    {
        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //--------> Criação das instâncias utilizadas
            PadraoRelatoriosCorrente.OnAcaoGeraRelatorio += new PadraoRelatorios.OnAcaoGeraRelatorioHandler(PadraoRelatoriosCorrente_OnAcaoGeraRelatorio);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarUnidadeContrato();
                CarregaTipoDocumento();
            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            string strParametrosRelatorio;
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            string strINFOS, strP_ORIG_PAGTO;
            int strP_CO_EMP, strUniContr, strP_CO_TIPO_DOC, strP_CO_AGRUP, strP_CO_TipoPagto;
            DateTime strP_DT_INI, strP_DT_FIM;

            if (DateTime.TryParse(txtDataPeriodoIni.Text, out strP_DT_INI) && DateTime.TryParse(txtDataPeriodoFim.Text, out strP_DT_FIM))
            {
                if (strP_DT_INI > strP_DT_FIM)
                {
                    AuxiliPagina.EnvioMensagemErro( this, "A data final deve ser posterior a data inicial.");
                    return;
                }
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this, "Data em formato incorreto."); 
                return;
            }

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strParametrosRelatorio = "";
            strP_CO_EMP = LoginAuxili.CO_EMP;
            strUniContr = int.Parse(ddlUnidContrato.SelectedValue);
            strP_CO_TIPO_DOC = int.Parse(ddlTipoDoctos.SelectedValue);
            //strP_DT_INI = DateTime.Parse(txtDataPeriodoIni.Text);
            //strP_DT_FIM = DateTime.Parse(txtDataPeriodoFim.Text);
            strP_CO_AGRUP = int.Parse(ddlAgrupador.SelectedValue);
            strP_ORIG_PAGTO = ddlOrigPagto.SelectedValue;
            if (HabilitarTipoPag())
                strP_CO_TipoPagto = int.Parse(ddlTipoPagto.SelectedValue);
            else
                strP_CO_TipoPagto = -1;
            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            string siglaUnid = TB25_EMPRESA.RetornaPelaChavePrimaria(strP_CO_EMP).sigla;
            string siglaUnidContr = strUniContr != 0 ? TB25_EMPRESA.RetornaPelaChavePrimaria(strUniContr).sigla : "Todas";

            strParametrosRelatorio = "( Unid. Contrato: " + siglaUnidContr + " - Agrupador: " + ddlAgrupador.SelectedItem.ToString()
                + " - Tp Docto: " + ddlTipoDoctos.SelectedItem.ToString()
                + " - Período: " + DateTime.Parse(txtDataPeriodoIni.Text).ToString("dd/MM/yy") + " à " + DateTime.Parse(txtDataPeriodoFim.Text).ToString("dd/MM/yy")
                + (HabilitarTipoPag() ? (" - Tp Pagto: " + strP_CO_TipoPagto) : "") + " )";

            //Parte responsável por coletar o nome da funcionalidade cadastrada no banco e mandar como parâmetro para o 
            //relatório usar no cabeçalho
            #region
            string NO_RELATORIO = "";
            var res = (from adm in ADMMODULO.RetornaTodosRegistros()
                       where adm.nomURLModulo == "GSAUD/5000_CtrlFinanceira/5299_Relatorios/DiarioFinanceiro.aspx"
                       select new { adm.nomItemMenu }).FirstOrDefault();
            NO_RELATORIO = (res != null ? res.nomItemMenu : "");
            #endregion

            RptDiarioFinanceiro fpcb = new RptDiarioFinanceiro();
            lRetorno = fpcb.InitReport(strParametrosRelatorio, LoginAuxili.ORG_CODIGO_ORGAO, strP_CO_EMP, 
                strUniContr, strP_CO_TIPO_DOC, strP_DT_INI, strP_DT_FIM, 
                strP_CO_AGRUP, strP_ORIG_PAGTO, strP_CO_TipoPagto, strINFOS, NO_RELATORIO);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Carrega todos as unidades de contrato
        /// </summary>
        private void CarregarUnidadeContrato() {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidContrato, LoginAuxili.ORG_CODIGO_ORGAO, true);
            if (HabilitarTipoPag())
            {
                ddlTipoPagto.Items.Clear();
                HabilitarTipoPag(false);
            }
            ddlAgrupador.Items.Clear();
        }

        /// <summary>
        /// Método que carrega o dropdown de Tipos de Documento
        /// </summary>
        private void CarregaTipoDocumento()
        {
            ddlTipoDoctos.Items.Clear();
            ddlTipoDoctos.DataSource = (from tb086 in TB086_TIPO_DOC.RetornaTodosRegistros()
                                    select new { tb086.CO_TIPO_DOC, tb086.DES_TIPO_DOC });

            ddlTipoDoctos.DataTextField = "DES_TIPO_DOC";
            ddlTipoDoctos.DataValueField = "CO_TIPO_DOC";
            ddlTipoDoctos.DataBind();

            ddlTipoDoctos.Items.Insert(0, new ListItem("Todos", "-1"));
            ddlTipoDoctos.Items.Insert(0, new ListItem("Selecione", "0"));
            ddlTipoDoctos.SelectedValue = "0";
            ddlOrigPagto.Items.Clear();
            if (HabilitarTipoPag())
            {
                ddlTipoPagto.Items.Clear();
                HabilitarTipoPag(false);
            }
            ddlAgrupador.Items.Clear();
        }

        /// <summary>
        /// Carregar todas a origens de pagamento
        /// </summary>
        private void CarregaOrigPag() 
        {
            ddlOrigPagto.Items.Clear();
            ddlOrigPagto.Items.Insert(0, new ListItem("Selecione", "0"));
            ddlOrigPagto.Items.Insert(1, new ListItem("Todas", "-1"));
            ddlOrigPagto.Items.Insert(2, new ListItem("Caixa", "C"));
            ddlOrigPagto.Items.Insert(3, new ListItem("Banco", "B"));
            ddlOrigPagto.Items.Insert(4, new ListItem("Baixa CAR", "X"));
            ddlOrigPagto.SelectedValue = "0";
            if (HabilitarTipoPag())
            {
                ddlTipoPagto.Items.Clear();
                HabilitarTipoPag(false);
            }
            ddlAgrupador.Items.Clear();
        }

        /// <summary>
        /// Carrega os tipos de pagamento
        /// </summary>
        private void CarregaTipoPag() 
        {
            string origem = ddlOrigPagto.SelectedValue;
            HabilitarTipoPag(true);
            ddlTipoPagto.Items.Clear();
            if (origem != "0" && origem == "C")
            {
                ddlTipoPagto.DataSource = (from tp in TB118_TIPO_RECEB.RetornaTodosRegistros()
                                           select new { tp.DE_RECEBIMENTO, tp.CO_TIPO_REC}
                                               );
                ddlTipoPagto.DataTextField = "DE_RECEBIMENTO";
                ddlTipoPagto.DataValueField = "CO_TIPO_REC";
                ddlTipoPagto.DataBind();
                ddlTipoPagto.Items.Insert(0, new ListItem("Todos", "-1"));
                ddlTipoPagto.Items.Insert(0, new ListItem("Selecione", "0"));
                ddlTipoPagto.SelectedValue = "0";
                ddlAgrupador.Items.Clear();
            }
            else
            {
                HabilitarTipoPag(false);
                if (origem != "0")
                    CarregaAgrupadores();
            }
        }
        /// <summary>
        /// Método que carrega o dropdown de Agrupadores
        /// </summary>
        private void CarregaAgrupadores()
        {
            ddlAgrupador.Items.Clear();
            ddlAgrupador.DataSource = (from tb315 in TB315_AGRUP_RECDESP.RetornaTodosRegistros()
                                       where tb315.TP_AGRUP_RECDESP == "R" || tb315.TP_AGRUP_RECDESP == "T"
                                       select new { tb315.ID_AGRUP_RECDESP, tb315.DE_SITU_AGRUP_RECDESP });

            ddlAgrupador.DataTextField = "DE_SITU_AGRUP_RECDESP";
            ddlAgrupador.DataValueField = "ID_AGRUP_RECDESP";
            ddlAgrupador.DataBind();

            ddlAgrupador.Items.Insert(0, new ListItem("Todos", "-1"));
            ddlAgrupador.SelectedValue = "-1";
        }
        #endregion

        #region Selecionados

        protected void ddlTipoPagto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0")
                CarregaAgrupadores();
        }

        protected void ddlTipoDoctos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0")
                CarregaOrigPag();
        }

        protected void ddlOrigPagto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0")
            {
                CarregaTipoPag();
            }
        }
        #endregion

        #region Metodos diversos
        /// <summary>
        /// Habilitar e desabilita o campo tipo de pagamento
        /// </summary>
        /// <param name="ativar"></param>
        private Boolean HabilitarTipoPag(Boolean? ativar = null) 
        {
            if (ativar != null)
            {
                lbDdlTipoPagto.Visible = (Boolean)ativar;
                ddlTipoPagto.Visible = (Boolean)ativar;
            }
            return ddlTipoPagto.Visible;
        }
        #endregion

        protected void ddlAgrupador_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDataPeriodoIni.Text = txtDataPeriodoFim.Text = "";
        }
    }
}
