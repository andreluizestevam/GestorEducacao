//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: GESTÃO DE DADOS DE ALUNOS
// OBJETIVO: RELAÇÃO DE ALUNOS - ANIVERSARIANTES
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//25/04/2014    Maxwell Almeida             Criação da página de Filtro de Relatórios e do Relatório em Si.

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
using C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8120_CtrlConsultas;
using System.Globalization;

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8199_Relatorios
{
    public partial class MapaMensalFaturColab : System.Web.UI.Page
    {
        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }

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
                CarregaUnidade();
                CarregaClassificacoesFuncionais();
                CarregaProfissional();

                try
                {
                    //Calcula o período do mês corrente
                    System.Globalization.Calendar c = new GregorianCalendar();
                    int DiasMes = c.GetDaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                    txtDtIni.Text = (DateTime.Now.AddDays(-DateTime.Now.Day + 1)).ToString(); //Coleta o primeiro dia do mês
                    txtDtFim.Text = (DateTime.Now.AddDays(Math.Abs(DateTime.Now.Day - DiasMes))).ToString(); //Coleta o último dia do mês
                }
                catch (Exception es)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Ops, ocorreu uma falha da Inteligência Artificial ao calcular o período do Mês. Não se preocupe, você pode parametrizar manualmente. Erro: " + es.Message);
                }
            }
        }

        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            int coEmp, Mes, lRetorno, CoUnidade, CoProfissional;
            string Periodo, infos, parametros, Titulo, CoClassFuncional;

            coEmp = LoginAuxili.CO_EMP;
            CoUnidade = int.Parse(ddlUnidade.SelectedValue);
            CoClassFuncional = ddlClassFuncional.SelectedValue;
            CoProfissional = Convert.ToInt32(ddlProfissional.SelectedValue);

            Periodo = string.Format("{0} à {1}", DateTime.Parse(txtDtIni.Text).ToString("dd/MM/yy"), DateTime.Parse(txtDtFim.Text).ToString("dd/MM/yy"));
            Titulo = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GSAUD/8000_GestaoAtendimento/8100_CtrlAgendaMedica/8199_Relatorios/MapaMensalAtendColab.aspx");
            parametros = "( Unidade: "
                        + ((!string.IsNullOrEmpty(ddlUnidade.SelectedValue)) && (ddlUnidade.SelectedValue != "0") ?
                        TB25_EMPRESA.RetornaPelaChavePrimaria(int.Parse(ddlUnidade.SelectedValue)).sigla.ToUpper() : "TODOS")
                + " - Classificação: " + ddlClassFuncional.SelectedItem.Text.ToUpper()
                + " - Profissional : " + ddlProfissional.SelectedItem
                + " - Período: " + Periodo + " )";
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptMapaFaturColab fpcb = new RptMapaFaturColab();
            lRetorno = fpcb.InitReport(parametros, infos, coEmp, CoUnidade, CoClassFuncional, CoProfissional, txtDtIni.Text, txtDtFim.Text, Titulo);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        #region Carregamentos

        protected void CarregaUnidade()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        protected void CarregaClassificacoesFuncionais()
        {
            AuxiliCarregamentos.CarregaClassificacoesFuncionais(ddlClassFuncional, true, LoginAuxili.CO_EMP, AuxiliCarregamentos.ETiposClassificacoes.agendamento);
        }

        protected void CarregaProfissional()
        {
            AuxiliCarregamentos.CarregaProfissionaisSaude(ddlProfissional, LoginAuxili.CO_EMP, true, ddlClassFuncional.SelectedValue, true);
        }

        protected void ddlClassFuncional_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CarregaProfissional();
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao carregar classificação funcional " + ex.Message);

            }
        }

        protected void ddlMes_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //Se for indefinido, o sistema calcula o período do mês selecionado
                if (ddlMes.SelectedValue != "0")
                {
                    txtDtIni.Enabled = txtDtFim.Enabled = false;

                    int Mes = int.Parse(ddlMes.SelectedValue);
                    System.Globalization.Calendar c = new GregorianCalendar();
                    int DiasMes = c.GetDaysInMonth(DateTime.Now.Year, Mes);

                    //Coleta o primeiro dia do mês selecionado
                    txtDtIni.Text = (new DateTime(DateTime.Now.Year, Mes, 01)).ToString();
                    //Coleta o último dia do mês selecionado
                    txtDtFim.Text = (new DateTime(DateTime.Now.Year, Mes, DiasMes)).ToString();
                }
                else //Se for indefinido, o sistema calcula o período do mês corrente
                {
                    txtDtIni.Enabled = txtDtFim.Enabled = true;

                    System.Globalization.Calendar c = new GregorianCalendar();
                    int DiasMes = c.GetDaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                    txtDtIni.Text = (DateTime.Now.AddDays(-DateTime.Now.Day + 1)).ToString(); //Coleta o primeiro dia do mês
                    txtDtFim.Text = (DateTime.Now.AddDays(Math.Abs(DateTime.Now.Day - DiasMes))).ToString(); //Coleta o último dia do mês
                }
            }
            catch (Exception es)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Ops, ocorreu uma falha da Inteligência Artificial ao calcular o período do Mês selecionado. Não se preocupe, você pode parametrizar manualmente. Erro: " + es.Message);
            }
        }

        #endregion
    }
}