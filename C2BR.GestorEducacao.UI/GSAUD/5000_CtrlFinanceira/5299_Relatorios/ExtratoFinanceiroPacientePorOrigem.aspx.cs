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
    public partial class ExtratoFinanceiroPacientePorOrigem : System.Web.UI.Page
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
                CarregaPaciente();
                CarregaOrigem();
                CarregaProfissional();
                CarregaSituacao();
            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            string strParametrosRelatorio;
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            string strINFOS, strRap, strOrigem, strSitua;
            int strP_CO_EMP, strUniContr, strPac, strProf, strOper;
            DateTime strP_DT_INI, strP_DT_FIM;

            if (DateTime.TryParse(txtDataPeriodoIni.Text, out strP_DT_INI) && DateTime.TryParse(txtDataPeriodoFim.Text, out strP_DT_FIM))
            {
                if (strP_DT_INI > strP_DT_FIM)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "A data final deve ser posterior a data inicial.");
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
            strRap = ddlRap.SelectedValue;
            strPac = int.Parse(ddlPaciente.SelectedValue);
            strOrigem = ddlOrigem.SelectedValue;
            strProf = int.Parse(ddlProfissional.SelectedValue);
            strOper = int.Parse(ddlOperadora.SelectedValue);
            strSitua = ddlSituacao.SelectedValue;

            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            string siglaUnid = TB25_EMPRESA.RetornaPelaChavePrimaria(strP_CO_EMP).sigla;
            string siglaUnidContr = strUniContr != 0 ? TB25_EMPRESA.RetornaPelaChavePrimaria(strUniContr).sigla : "Todas";

            strParametrosRelatorio = "( Unid. Contrato: " + siglaUnidContr + " - RAP: " + ddlRap.SelectedItem.ToString()
                + " - Origem: " + ddlOrigem.SelectedItem.ToString()
                + " - Profissional: " + ddlProfissional.SelectedItem.ToString()
                + " - Operadora: " + ddlOperadora.SelectedItem.ToString()
                + " - Situação: " + ddlOperadora.SelectedItem.ToString()
                + " - Período: " + DateTime.Parse(txtDataPeriodoIni.Text).ToString("dd/MM/yy") + " à " + DateTime.Parse(txtDataPeriodoFim.Text).ToString("dd/MM/yy")
                + " )";

            //Parte responsável por coletar o nome da funcionalidade cadastrada no banco e mandar como parâmetro para o 
            //relatório usar no cabeçalho
            #region
            string NO_RELATORIO = "";
            var res = (from adm in ADMMODULO.RetornaTodosRegistros()
                       where adm.nomURLModulo == "GSAUD/5000_CtrlFinanceira/5299_Relatorios/ExtratoFinanceiroPaciente.aspx"
                       select new { adm.nomItemMenu }).FirstOrDefault();
            NO_RELATORIO = (res != null ? res.nomItemMenu : "");
            #endregion

            RptExtratoFinanceiroPacientePorOrigem rpt = new RptExtratoFinanceiroPacientePorOrigem();
            lRetorno = rpt.InitReport(strParametrosRelatorio, strUniContr, strP_CO_EMP, strPac, strRap, strOrigem,
                strProf, strOper, strSitua, strP_DT_INI, strP_DT_FIM, strINFOS, NO_RELATORIO);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Carrega todos as unidades de contrato
        /// </summary>
        private void CarregarUnidadeContrato()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidContrato, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        /// <summary>
        /// Carrega todos os pacientes
        /// </summary>
        private void CarregaPaciente()
        {
            AuxiliCarregamentos.CarregaPacientes(ddlPaciente, int.Parse(ddlUnidContrato.SelectedValue), false);
        }

        /// <summary>
        /// Carrega rap
        /// </summary>
        private void CarregaRap()
        {
            int co_alu = ddlPaciente.SelectedValue != "" ? int.Parse(ddlPaciente.SelectedValue) : 0;
            int co_emp = int.Parse(ddlUnidContrato.SelectedValue);
            DateTime dtRap = DateTime.Parse("01/01/1900");

            var rap174 = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                          where tbs174.CO_EMP == co_emp
                          && (co_alu != 0 ? co_alu == tbs174.CO_ALU : 0 == 0)
                          select new RAP
                          {
                              IdRap = tbs174.ID_AGEND_HORAR,
                              DataRap = tbs174.DT_AGEND_HORAR,
                              HoraRap = tbs174.HR_AGEND_HORAR,
                              TipoRap = "CO",
                              NuRap = tbs174.NU_REGIS_CONSUL
                          }).Distinct().OrderByDescending(x => x.DataRap);

            ddlRap.DataSource = rap174;
            ddlRap.DataTextField = "deRap";
            ddlRap.DataValueField = "NuRap";
            ddlRap.DataBind();
            ddlRap.Items.Insert(0, new ListItem("Todos", ""));
        }

        /// <summary>
        /// Carrega Origem
        /// </summary>
        private void CarregaOrigem()
        {
            AuxiliCarregamentos.CarregaTiposProcedimentosMedicos(ddlOrigem, true);
        }

        /// <summary>
        /// Carrega todos os Profissionais
        /// </summary>
        private void CarregaProfissional()
        {
            AuxiliCarregamentos.CarregaProfissionaisSaude(ddlProfissional, int.Parse(ddlUnidContrato.SelectedValue), true, "0");
        }

        /// <summary>
        /// Carrega todos as Operadoras
        /// </summary>
        private void CarregaOperadora()
        {
            int coPaci = ddlPaciente.SelectedValue != "" ? int.Parse(ddlPaciente.SelectedValue) : 0;
            var res = (from tb250 in TB250_OPERA.RetornaTodosRegistros()
                       join tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros() on tb250.CO_OPER equals tbs174.TB250_OPERA.CO_OPER
                       where tb250.FL_SITU_OPER == "A"
                       && (coPaci != 0 ? coPaci == tbs174.CO_ALU : 0 == 0)
                       select new { ID_OPER = tb250.ID_OPER, NOM_OPER = tb250.NOM_OPER, NM_SIGLA_OPER = tb250.NM_SIGLA_OPER })
                       .OrderBy(w => w.NOM_OPER).Distinct().ToList();

            if (res != null)
            {
                ddlOperadora.DataTextField = "NOM_OPER";
                ddlOperadora.DataValueField = "ID_OPER";
                ddlOperadora.DataSource = res;
                ddlOperadora.DataBind();
            }
            ddlOperadora.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Carrega Situações para o relatório
        /// </summary>
        private void CarregaSituacao()
        {
            ddlSituacao.Items.Insert(0, new ListItem("Agendado", "A"));
            ddlSituacao.Items.Insert(0, new ListItem("Cancelado", "C"));
            ddlSituacao.Items.Insert(0, new ListItem("Realizado", "R"));
            ddlSituacao.Items.Insert(0, new ListItem("Todos", "0"));
        }

        #endregion

        #region Selecionados

        protected void ddlPaciente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0")
            {
                CarregaRap();
                CarregaOperadora();
            }
        }

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0")
            {
                CarregaPaciente();
            }
        }

        #endregion

        #region Metodos diversos

        #endregion

        #region classes

        public class RAP
        {
            public int IdRap { get; set; }
            public DateTime DataRap { get; set; }
            public string HoraRap { get; set; }
            public string TipoRap { get; set; }
            public string NuRap { get; set; }

            public string DeRap
            {
                get { return DataRap.ToShortDateString() + " - " + HoraRap + " - " + TipoRap + " - " + NuRap; }
            }
        }

        #endregion
    }
}
