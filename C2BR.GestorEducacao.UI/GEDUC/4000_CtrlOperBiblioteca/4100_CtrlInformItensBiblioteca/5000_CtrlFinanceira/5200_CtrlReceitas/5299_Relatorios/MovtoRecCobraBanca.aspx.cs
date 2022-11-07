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
using C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5200_CtrlReceitas;

namespace C2BR.GestorEducacao.UI.GEDUC._5000_CtrlFinanceira._5200_CtrlReceitas._5299_Relatorios
{
    public partial class MovtoRecCobraBanca : System.Web.UI.Page
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
            if (!Page.IsPostBack)
            {
                CarregaBancos();
            }
        }

        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //Mensagem para data inválida
            string mensagemDatas = "O período deve ser informado, por favor informe a data de ínicio e uma data posterior para o fim do período. ";
            //--------> Variáveis obrigatórias para gerar o Relatório
            string strParametrosRelatorio;
            int lRetorno;
            //--------> Variáveis de parâmetro do Relatório
            string strINFOS, strP_IDEBANCO, strP_CO_CONTA, strContrato, strP_STATUS;
            int intP_CO_AGENCIA;
            DateTime dtInicio, dtFim;
            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strParametrosRelatorio = "";
            strP_IDEBANCO = ddlBanco.SelectedValue;
            intP_CO_AGENCIA = int.Parse(ddlAgencia.SelectedValue);
            strP_CO_CONTA = ddlConta.SelectedValue;
            strContrato = ddlContrato.SelectedValue;
            strP_STATUS = ddlStaDocumento.SelectedValue;
            if (txtDtVenctoIni.Text == "" || txtDtVenctoFim.Text == "" || !DateTime.TryParse(txtDtVenctoIni.Text, out dtInicio) || !DateTime.TryParse(txtDtVenctoFim.Text, out dtFim))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, mensagemDatas);
                return;
            }
            if (dtFim < dtInicio)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, mensagemDatas);
                return;
            }
            if(strP_STATUS == "0")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Selecione uma opção de status.");
                return;
            }

            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            strParametrosRelatorio = "( Banco: " + ddlBanco.SelectedItem.ToString() + " - Agc.: " + ddlAgencia.SelectedItem.ToString() +
                " - Conta: " + ddlConta.SelectedItem.ToString() +
                " - Contrato: " + ddlContrato.SelectedItem.ToString() +
                " - Periodo Vecto: " + (txtDtVenctoIni.Text != "" ? DateTime.Parse(txtDtVenctoIni.Text).ToString("dd/MM/yy") : "***") +
                " à " + (txtDtVenctoFim.Text != "" ? DateTime.Parse(txtDtVenctoFim.Text).ToString("dd/MM/yy") : "***") +
                " - Status: " + ddlStaDocumento.SelectedItem.ToString();

            RptMovtoRecDaCobraBanca fpcb = new RptMovtoRecDaCobraBanca();
            lRetorno = fpcb.InitReport(strParametrosRelatorio, LoginAuxili.ORG_CODIGO_ORGAO, LoginAuxili.CO_EMP, strP_IDEBANCO, intP_CO_AGENCIA, strP_CO_CONTA, strP_STATUS, strContrato, dtInicio, dtFim, strINFOS);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        #endregion
        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Bancos
        /// </summary>
        private void CarregaBancos()
        {
            ddlBanco.Items.Clear();
            ddlBanco.DataSource = (from tb29 in TB29_BANCO.RetornaTodosRegistros().AsEnumerable()
                                   select new { tb29.IDEBANCO, DESBANCO = string.Format("{0} - {1}", tb29.IDEBANCO, tb29.DESBANCO) }).OrderBy(b => b.DESBANCO);

            ddlBanco.DataValueField = "IDEBANCO";
            ddlBanco.DataTextField = "DESBANCO";
            ddlBanco.DataBind();

            ddlBanco.Items.Insert(0, new ListItem("Todos", "-1"));
            ddlBanco.Items.Insert(0, new ListItem("Selecione", "0"));
            ddlBanco.SelectedValue = "0";

            ddlAgencia.Items.Clear();
            ddlConta.Items.Clear();
            ddlContrato.Items.Clear();
            ddlStaDocumento.Items.Clear();
        }
        /// <summary>
        /// Método que carrega o dropdown de Agências
        /// </summary>
        private void CarregaAgencias(string coBanco = "")
        {
            if (coBanco != "")
            {
                ddlAgencia.Items.Clear();
                ddlAgencia.DataSource = (from tb30 in TB30_AGENCIA.RetornaTodosRegistros().AsEnumerable()
                                         where coBanco == "-1" ? 0==0 : tb30.IDEBANCO == coBanco
                                         select new
                                         {
                                             tb30.CO_AGENCIA,
                                             DESCRICAO = string.IsNullOrEmpty(ddlBanco.SelectedValue) ?
                                                     string.Format("({0}) {1} - {2}", tb30.IDEBANCO, tb30.CO_AGENCIA, tb30.NO_AGENCIA) :
                                                     string.Format("{0} - {1}", tb30.CO_AGENCIA, tb30.NO_AGENCIA)
                                         }).OrderBy(a => a.DESCRICAO);

                ddlAgencia.DataValueField = "CO_AGENCIA";
                ddlAgencia.DataTextField = "DESCRICAO";
                ddlAgencia.DataBind();

                ddlAgencia.Items.Insert(0, new ListItem("Todos", "-1"));
                ddlAgencia.Items.Insert(0, new ListItem("Selecione", "0"));
                ddlAgencia.SelectedValue = "0";

                ddlConta.Items.Clear();
                ddlContrato.Items.Clear();
                ddlStaDocumento.Items.Clear();
            }
            else
                ddlAgencia.Items.Clear();
        }
        /// <summary>
        /// Método que carrega o dropdown de Contas
        /// </summary>
        private void CarregaContas(int? coAgencia, string coBanco = "")
        {
            if (coAgencia != null
                && coBanco != "")
            {
                ddlConta.Items.Clear();
                ddlConta.DataSource = (from tb224 in TB224_CONTA_CORRENTE.RetornaTodosRegistros()
                                       where coBanco=="-1" ? 0==0 : tb224.TB30_AGENCIA.IDEBANCO == coBanco 
                                       && coAgencia==-1 ? 0==0 : tb224.TB30_AGENCIA.CO_AGENCIA == coAgencia
                                       && tb224.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                       select new { tb224.CO_CONTA }).OrderBy(a => a.CO_CONTA);

                ddlConta.DataValueField = "CO_CONTA";
                ddlConta.DataTextField = "CO_CONTA";
                ddlConta.DataBind();

                ddlConta.Items.Insert(0, new ListItem("Todos", "-1"));
                ddlConta.Items.Insert(0, new ListItem("Selecione", "0"));
                ddlConta.SelectedValue = "0";

                ddlContrato.Items.Clear();
                ddlStaDocumento.Items.Clear();
            }
            else
                ddlConta.Items.Clear();
        }
        /// <summary>
        /// Busca os contratos relacionados a banco, agencia e conta informados
        /// </summary>
        /// <param name="coBanco"></param>
        /// <param name="coAgencia"></param>
        /// <param name="coConta"></param>
        private void CarregarContratos(int? coBanco, int? coAgencia, string coConta) {
            if (coBanco != null
                && coAgencia != null
                && coConta != null
                )
            {
                string strBanco = string.Format("{0:000}", coBanco);
                ddlContrato.Items.Clear();
                ddlContrato.DataSource = (from bb in TB227_DADOS_BOLETO_BANCARIO.RetornaTodosRegistros()
                                          where 
                                          strBanco == "-1" ? 0 == 0 :  bb.TB224_CONTA_CORRENTE.IDEBANCO == strBanco
                                          && coAgencia == -1 ? 0 == 0 : bb.TB224_CONTA_CORRENTE.CO_AGENCIA == coAgencia
                                          && coConta == "-1" ? 0 == 0 : bb.TB224_CONTA_CORRENTE.CO_CONTA == coConta
                                          select new { 
                                                bb.NU_CONVENIO
                                              }
                                            );
                ddlContrato.DataTextField = "NU_CONVENIO";
                ddlContrato.DataValueField = "NU_CONVENIO";
                ddlContrato.DataBind();
                ddlContrato.Items.Insert(0, new ListItem("Todos", "-1"));
                ddlContrato.Items.Insert(0, new ListItem("Selecione", "0"));
                ddlContrato.SelectedValue = "0";

                ddlStaDocumento.Items.Clear();
            }
            else
                ddlContrato.Items.Clear();
        }
        /// <summary>
        /// Atualiza a lista de opções de situação
        /// </summary>
        private void CarregarSituacao() {
            ddlStaDocumento.Items.Clear();
            ddlStaDocumento.Items.Insert(0, new ListItem("Já Pago", "Q"));
            ddlStaDocumento.Items.Insert(0, new ListItem("Cancelado", "C"));
            ddlStaDocumento.Items.Insert(0, new ListItem("Mov Duplicado", "D"));
            ddlStaDocumento.Items.Insert(0, new ListItem("Inconsistente", "I"));
            ddlStaDocumento.Items.Insert(0, new ListItem("Baixado", "B"));
            ddlStaDocumento.Items.Insert(0, new ListItem("Pendente Baixa", "P"));
            ddlStaDocumento.Items.Insert(0, new ListItem("Todos", "-1"));
            ddlStaDocumento.Items.Insert(0, new ListItem("Selecione", "0"));
            ddlStaDocumento.DataBind();
            ddlStaDocumento.SelectedValue = "0";
        }
        #endregion

        #region Changed

        protected void ddlBanco_SelectedIndexChanged(object sender, EventArgs e)
        {
            string coBanco = ((DropDownList)sender).SelectedValue;
            if (coBanco != "0")
            {
                if (coBanco == "-1")
                {
                    CarregaAgencias(coBanco);
                    ddlAgencia.SelectedValue = "-1";
                    CarregaContas(-1, "-1");
                    ddlConta.SelectedValue = "-1";
                    CarregarContratos(-1, -1, "-1");
                    ddlContrato.SelectedValue = "-1";
                    ddlAgencia.Enabled = false;
                    ddlConta.Enabled = false;
                    ddlContrato.Enabled = false;
                    CarregarSituacao();
                }
                else
                {
                    ddlAgencia.Enabled = true;
                    ddlConta.Enabled = true;
                    ddlContrato.Enabled = true;
                    CarregaAgencias(coBanco);
                }
            }
        }

        protected void ddlAgencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            string coBanco = ddlBanco.SelectedValue;
            int coAgencia = int.Parse(((DropDownList)sender).SelectedValue);
            if (coAgencia != 0)
                CarregaContas(coAgencia, coBanco);
        }

        protected void ddlConta_SelectedIndexChanged(object sender, EventArgs e)
        {
            int coBanco = int.Parse(ddlBanco.SelectedValue);
            int coAgencia = int.Parse(ddlAgencia.SelectedValue);
            string coConta = ((DropDownList)sender).SelectedValue;
            if (coConta != "0")
                CarregarContratos(coBanco, coAgencia, coConta);
        }

        protected void ddlContrato_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlContrato.SelectedValue != "0")
                CarregarSituacao();
        }

        #endregion
    }
}