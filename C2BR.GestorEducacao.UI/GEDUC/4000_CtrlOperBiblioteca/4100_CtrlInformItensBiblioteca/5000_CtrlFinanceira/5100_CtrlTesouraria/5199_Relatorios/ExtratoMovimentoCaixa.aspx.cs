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
using C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5500_GeraFluxoCaixa;
using C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5100_CtrlTesouraria;

namespace C2BR.GestorEducacao.UI.GEDUC._5000_CtrlFinanceira._5100_CtrlTesouraria._5199_Relatorios
{
    public partial class ExtratoMovimentoCaixa : System.Web.UI.Page
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
                CarregaUnidades();
                CarregaCaixas();
                ListaBeneficiarios("C", "A");
            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório

            string strParametrosRelatorio, strINFOS, Operacao;
            DateTime dtInicio, dtFim;
            int codEmp, codEmpRef, codUndContrato, codCaixa, codResponsavel;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strParametrosRelatorio = "";
            codEmp = LoginAuxili.CO_EMP;
            codEmpRef = int.Parse(ddlUnidade.SelectedValue);
            codUndContrato = int.Parse(ddlUnidade.SelectedValue);
            codResponsavel = int.Parse(ddlResponsavel.SelectedValue);
            codCaixa = int.Parse(ddlCaixa.SelectedValue);
            dtInicio = DateTime.ParseExact(txtDtInicio.Text, "dd/MM/yyyy", null);
            dtFim = DateTime.ParseExact(txtDtFim.Text, "dd/MM/yyyy", null);
            Operacao = ddlOperacao.SelectedValue;
            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            TB25_EMPRESA emp = (codUndContrato != 0) ? TB25_EMPRESA.RetornaPelaChavePrimaria(codUndContrato) : null;  
            TB113_PARAM_CAIXA caixa = (codCaixa != 0) ? TB113_PARAM_CAIXA.RetornaPelaChavePrimaria(codEmp, codCaixa) : null;
            TB108_RESPONSAVEL resp = (codResponsavel != 0) ? TB108_RESPONSAVEL.RetornaPelaChavePrimaria(codResponsavel) : null; 
            string oper = (Operacao == "T") ? "Todas" : Operacao == "C" ? "PAG" : "REC";

            strParametrosRelatorio = "(Caixa: " + (caixa == null ? "Todos" : caixa.CO_SIGLA_CAIXA)
                                     + " - Operacao: " + oper.ToString()
                                     + " - Responsavel: " + (resp == null ? "Todos" : codResponsavel.ToString()
                                     + " Período: " + dtInicio.ToString("dd/MM/yy") + " à " + dtFim.ToString("dd/MM/yy") + ")");

           //strParametrosRelatorio += "(Caixa: " + (caixa == null ? "Todos" : caixa.CO_SIGLA_CAIXA);
           //strParametrosRelatorio += " - Operacao: " + oper;
           //strParametrosRelatorio += " - Responsavel: " + resp == null ? "Todos" : ddlResponsavel.SelectedValue;
           //strParametrosRelatorio += " - Período: " + dtInicio.ToString("dd/MM/yy") + " à " + dtFim.ToString("dd/MM/yy") + ")";                    
            
            RptExtratoMovimentoCaixa rpt = new RptExtratoMovimentoCaixa();
            lRetorno = rpt.InitReport(strParametrosRelatorio, codEmp, codEmpRef, codUndContrato, codCaixa, 0, Operacao, codResponsavel, dtInicio, dtFim, strINFOS);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaUnidades()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                     where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy(e => e.NO_FANTAS_EMP);

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();

            ddlUnidContrato.DataSource = from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                         where tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                         select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP };

            ddlUnidContrato.DataTextField = "NO_FANTAS_EMP";
            ddlUnidContrato.DataValueField = "CO_EMP";
            ddlUnidContrato.DataBind();

            ddlUnidContrato.Items.Insert(0, new ListItem("Todas", "0"));
        }
        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaCaixas()
        {
            ddlCaixa.DataSource = (from tb113 in TB113_PARAM_CAIXA.RetornaTodosRegistros()
                                   where tb113.CO_FLAG_USO_CAIXA == "A"
                                   select new { tb113.DE_CAIXA, tb113.CO_CAIXA }).OrderBy(e => e.DE_CAIXA);

            ddlCaixa.DataTextField = "DE_CAIXA";
            ddlCaixa.DataValueField = "CO_CAIXA";
            ddlCaixa.DataBind();

            ddlCaixa.Items.Insert(0, new ListItem("Todos", "0"));
        }

        protected void ListaBeneficiarios(string strTipoMovi, string strTipoBenef)
        {
            if (strTipoMovi == "C")
            {
                if (strTipoBenef == "A")
                {
                    ddlResponsavel.DataSource = (from iTb07 in TB07_ALUNO.RetornaTodosRegistros()
                                           where iTb07.TB108_RESPONSAVEL.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO &&
                                           iTb07.CO_EMP == LoginAuxili.CO_EMP
                                           select new { iTb07.TB108_RESPONSAVEL.CO_RESP, iTb07.TB108_RESPONSAVEL.NO_RESP }
                                           ).DistinctBy(x => x.NO_RESP).OrderBy(p => p.NO_RESP);

                    ddlResponsavel.DataTextField = "NO_RESP";
                    ddlResponsavel.DataValueField = "CO_RESP";
                    ddlResponsavel.DataBind();
                    ddlResponsavel.Items.Insert(0, new ListItem("Todos", "0"));
                }
                else
                {
                    ddlResponsavel.DataSource = TB103_CLIENTE.RetornaTodosRegistros();

                    ddlResponsavel.DataTextField = "NO_FAN_CLI";
                    ddlResponsavel.DataValueField = "CO_CLIENTE";                    
                    ddlResponsavel.DataBind();
                    ddlResponsavel.Items.Insert(0, new ListItem("Todos", "0"));
                }
            }
            else
            {
                ddlResponsavel.DataSource = TB41_FORNEC.RetornaTodosRegistros();

                ddlResponsavel.DataTextField = "NO_FAN_FOR";
                ddlResponsavel.DataValueField = "CO_FORN";
                ddlResponsavel.DataBind();
                ddlResponsavel.Items.Insert(0, new ListItem("Todos", "0"));
            }
        }

        #endregion
    }
}
