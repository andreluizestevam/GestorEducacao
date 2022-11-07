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

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8199_Relatorios
{
    public partial class MapaMensalFreqPaciente : System.Web.UI.Page
    {
        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //--------> Criação das instâncias utilizadas
            PadraoRelatoriosCorrente.OnAcaoGeraRelatorio += new PadraoRelatorios.OnAcaoGeraRelatorioHandler(PadraoRelatoriosCorrente_OnAcaoGeraRelatorio);
        }
        DateTime Data;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaUnidade();
                CarregaClassificacoesFuncionais();
                CarregaProfissional();
                ddlMes.SelectedValue = DateTime.Now.Month.ToString();
                txtAno.Text = DateTime.Now.Year.ToString();
            }
        }

        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {

            if (string.IsNullOrEmpty(txtAno.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o  ano!");
                return;
            }

            int coEmp, Mes, Ano, lRetorno, CoUnidade, CoProfissional;
            string Periodo, infos, parametros, Titulo, CoClassFuncional;

            coEmp = LoginAuxili.CO_EMP;
            CoUnidade = int.Parse(ddlUnidade.SelectedValue);
            CoClassFuncional = ddlClassFuncional.SelectedValue;
            CoProfissional = Convert.ToInt32(ddlProfissional.SelectedValue);
            Mes = Convert.ToInt32(ddlMes.SelectedValue);
            Ano = Convert.ToInt32(txtAno.Text);

            Periodo = ddlMes.SelectedItem.Text.ToUpper() + " / " + Ano;
            Titulo = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GSAUD/8000_GestaoAtendimento/8100_CtrlAgendaMedica/8199_Relatorios/MapaMensalFreqPaciente.aspx");
            parametros = "( Unidade: "
                + ((!string.IsNullOrEmpty(ddlUnidade.SelectedValue)) && (ddlUnidade.SelectedValue != "0") ?
                TB25_EMPRESA.RetornaPelaChavePrimaria(int.Parse(ddlUnidade.SelectedValue)).sigla.ToUpper() : "TODOS")
                + " - Classificação: " + ddlClassFuncional.SelectedItem.Text.ToUpper()
                + " - Profissional : " + ddlProfissional.SelectedItem
                + " - Período: " + Periodo + " )";
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptMapaFreqPacientes fpcb = new RptMapaFreqPacientes();
            lRetorno = fpcb.InitReport(parametros, infos, coEmp, CoUnidade, CoClassFuncional, CoProfissional, Mes, Ano, Titulo);
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
            AuxiliCarregamentos.CarregaProfissionaisSaude(ddlProfissional, LoginAuxili.CO_EMP, false, ddlClassFuncional.SelectedValue, true);
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


        #endregion
    }
}