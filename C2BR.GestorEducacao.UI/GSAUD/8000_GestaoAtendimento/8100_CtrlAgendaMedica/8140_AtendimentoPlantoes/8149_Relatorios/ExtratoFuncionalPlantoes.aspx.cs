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
//25/04/2014    Maxwell Almeida             Criação da página de Filtro de Relatórios e do Relatório em Si, com a possibilidade de emissão do Extrato Detalhado ou Resumido.

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
using C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3300_Consultas;

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8140_AtendimentoPlantoes._8149_Relatorios
{
    public partial class ExtratoFuncionalPlantoes : System.Web.UI.Page
    {
        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //--------> Criação das instâncias utilizadas
            PadraoRelatoriosCorrente.OnAcaoGeraRelatorio += new PadraoRelatorios.OnAcaoGeraRelatorioHandler(PadraoRelatoriosCorrente_OnAcaoGeraRelatorio);
        }

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                carregaUnidades();
                carregaMedicos();
            }
        }

        #endregion

        #region Carregamentos

        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {

            string parametros;
            string infos;
            string noMedic = "";
            string noSitu = "";
            string noStatus = "";
            int coEmp, lRetorno, unidade, deptoLocal, Espec, Medico, materia, anoRef;
            string dataIni, dataFim, Status, situacao, noUnidade, noDeptoLocal, noEspec, noMedico, Periodo;


            coEmp = LoginAuxili.CO_EMP;
            unidade = int.Parse(ddlUnidade.SelectedValue);
            deptoLocal = int.Parse(ddlDeptLocal.SelectedValue);
            Espec = int.Parse(ddlEspec.SelectedValue);
            Medico = int.Parse(ddlMedico.SelectedValue);
            Status = ddlStatus.SelectedValue;
            situacao = ddlSituacao.SelectedValue;
            dataIni = IniPeri.Text;
            dataFim = FimPeri.Text;

            if (Medico != 0)
            {
                TB03_COLABOR t03 = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                                    where (tb03.CO_COL == Medico)
                                    select tb03).FirstOrDefault();

                noMedic = t03.NO_COL;
            }

            noUnidade = (unidade != 0 ? TB25_EMPRESA.RetornaPelaChavePrimaria(unidade).NO_FANTAS_EMP : "Todos");
            noDeptoLocal = (deptoLocal != 0 ? TB14_DEPTO.RetornaPelaChavePrimaria(deptoLocal).NO_DEPTO : "Todos");
            //noEspec = (Espec != 0 ? TB63_ESPECIALIDADE.RetornaPelaChavePrimaria(unidade,).NO_ESPECIALIDADE : "Todos");
            noMedico = (Medico != 0 ? noMedic : "Todos");
            Periodo = dataIni + " à " + dataFim;

            switch (situacao)
            {
                case "0":
                    noStatus = "Todos";
                    break;

                case "A":
                    noStatus = "Em Aberto";
                    break;

                case "C":
                    noStatus = "Cancelado";
                    break;

                case "R":
                    noStatus = "Realizado";
                    break;
            }

            switch (Status)
            {
                case "0":
                    noSitu = "Todos";
                    break;

                case "C":
                    noSitu = "Confirmado";
                    break;

                case "N":
                    noSitu = "Não Confirmado";
                    break;

                case "I":
                    noSitu = "Indisponível";
                    break;
            }


            parametros = "( Unidade: " + noUnidade.ToUpper() + " - Departamento: " + noDeptoLocal.ToUpper() + " - Médico: " + noMedico.ToUpper() + "" + " - Período: " + Periodo.ToUpper() + " - Status: " + noStatus + " - Situação: " + noSitu + " )";
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            //RptExtratoConsultas fpcb = new RptExtratoConsultas();
            //lRetorno = fpcb.InitReport(parametros, infos, coEmp, unidade, deptoLocal, Espec, Medico, Status, situacao, dataIni, dataFim);
            //Session["Report"] = fpcb;
            //Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            //AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        private void carregaUnidades()
        {
            var res = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                       select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP });

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataSource = res;
            ddlUnidade.DataBind();

            ddlUnidade.Items.Insert(0, new ListItem("Todos", "0"));
        }

        private void carregaMedicos()
        {
            int unid = int.Parse(ddlUnidade.SelectedValue);

            var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                       where unid != 0 ? tb03.CO_EMP == unid : 0 == 0
                       select new { tb03.NO_COL, tb03.CO_COL, tb03.CO_EMP });

            ddlMedico.DataTextField = "NO_COL";
            ddlMedico.DataValueField = "CO_COL";
            ddlMedico.DataSource = res;
            ddlMedico.DataBind();

            ddlMedico.Items.Insert(0, new ListItem("Todos", "0"));
        }

        #endregion

        #region Funções de Campo

        protected void ddlUnidade_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaMedicos();
        }
        #endregion
    }
}
