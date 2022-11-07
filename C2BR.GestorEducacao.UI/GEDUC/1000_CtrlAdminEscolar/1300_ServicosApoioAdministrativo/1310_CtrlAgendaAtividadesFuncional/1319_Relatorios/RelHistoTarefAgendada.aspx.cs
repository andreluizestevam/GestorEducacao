//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: AGENDA DE ATIVIDADES PROFISSIONAIS
// OBJETIVO: EMISSÃO DO HISTÓRICO DE TAREFA AGENDADA
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using Resources;
using C2BR.GestorEducacao.UI.Library.Componentes;
using System.Runtime.InteropServices;
using System.IO;
using System.ServiceModel;
using System.Configuration;
using C2BR.GestorEducacao.DLLRelatorioWeb.Interfaces;
using WRAuxiliares = C2BR.GestorEducacao.DLLRelatorioWeb.Auxiliares;
using C2BR.GestorEducacao.Reports._1000_CtrlAdminEscolar._1300_ServicosApoioAdministrativo._1310_CtrlAgendaAtividadesFuncional;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1300_ServicosApoioAdministrativo.F1310_CtrlAgendaAtividadesFuncional.F1319_Relatorios
{
    public partial class RelHistoTarefAgendada : System.Web.UI.Page
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
                CarregaDropDown();
                if (ddlTipo.SelectedValue.ToString() == "R")
                    liFuncionarios.Visible = false;
                else
                {
                    liFuncionarios.Visible = true;
                    CarregaFuncionarios();
                }
            }
        }

//====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            // Vars. obrigatórias.
            string strParametrosRelatorio, strINFOS;
            int lRetorno;

            // Vars. de parametros de consulta
            int strP_CO_EMP, strP_CO_RESP, strP_CO_SOLIC;
            string strP_PRIOR;
            DateTime strP_DT_INI, strP_DT_FIM;

//--------> Inicializa as variáveis
            strP_PRIOR = null;

//--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = int.Parse(ddlUnidade.SelectedValue);
            strP_CO_RESP = Library.Auxiliares.LoginAuxili.CO_COL;
            strP_DT_INI = DateTime.Parse(txtDataPeriodoIni.Text);
            strP_DT_FIM = DateTime.Parse(txtDataPeriodoFim.Text);
            strP_PRIOR = ddlPrioridade.SelectedValue;
            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            if (ddlTipo.SelectedValue == "R")
            {
                strP_CO_SOLIC = Library.Auxiliares.LoginAuxili.CO_COL;
            }
            else
                strP_CO_SOLIC = int.Parse(ddlFuncionarios.SelectedValue);

            strParametrosRelatorio = "Unidade: " + ddlUnidade.SelectedItem.ToString() + " - Período: " + txtDataPeriodoIni.Text + " à " + txtDataPeriodoFim.Text +
                " - Prioridade: " + ddlPrioridade.SelectedItem.ToString();

            RptRelHistoTarefAgendada fpcb = new RptRelHistoTarefAgendada();
            lRetorno = fpcb.InitReport(strParametrosRelatorio, LoginAuxili.ORG_CODIGO_ORGAO, LoginAuxili.CO_EMP, strP_CO_EMP, strP_DT_INI, strP_DT_FIM, strP_CO_RESP, strP_PRIOR, strP_CO_SOLIC, strINFOS);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares e Prioridade
        /// </summary>
        private void CarregaDropDown()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                     where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy( e => e.NO_FANTAS_EMP );

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();

            ddlPrioridade.DataSource = TB140_PRIOR_TAREF_AGEND.RetornaTodosRegistros();

            ddlPrioridade.DataTextField = "DE_PRIOR_TAREF_AGEND";
            ddlPrioridade.DataValueField = "CO_PRIOR_TAREF_AGEND";
            ddlPrioridade.DataBind();

            ddlPrioridade.Items.Insert(0, new ListItem("Todos", "T"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Funcionários
        /// </summary>
        private void CarregaFuncionarios()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            ddlFuncionarios.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(coEmp)
                                          select new { tb03.NO_COL, tb03.CO_COL }).OrderBy( c => c.NO_COL );

            ddlFuncionarios.DataTextField = "NO_COL";
            ddlFuncionarios.DataValueField = "CO_COL";
            ddlFuncionarios.DataBind();

            ddlFuncionarios.Items.Insert(0, new ListItem("Todos", "0"));
        }
        #endregion

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipo.SelectedValue.ToString() == "R")
                liFuncionarios.Visible = false;
            else
                liFuncionarios.Visible = true;
        }

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipo.SelectedValue.ToString() == "E")
                CarregaFuncionarios();
        }
    }
}
