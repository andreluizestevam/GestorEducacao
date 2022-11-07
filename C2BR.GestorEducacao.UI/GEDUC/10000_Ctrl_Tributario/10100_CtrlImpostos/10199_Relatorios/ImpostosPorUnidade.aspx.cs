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
//25/04/2014    Maxwell Almeida             Criação da página de Filtro de Relatórios.

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
using C2BR.GestorEducacao.Reports._10000_CtrlTributario._10100_CtrlImpostos;

namespace C2BR.GestorEducacao.UI.GEDUC._10000_Ctrl_Tributario._10100_CtrlImpostos._10199_Relatorios
{
    public partial class ImpostosPorUnidade : System.Web.UI.Page
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
                carregaModalidade();
            }
        }

        #endregion

        #region Carregamentos

        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            if (ddlTpPesq.SelectedValue == "M")
                porModalidade();

            else
                porUnidade();
        }
        void porUnidade()
        {
            string parametros = "";
            string infos;
            int coEmp, lRetorno, unidade, modalidade;
            string dataIni, dataFim, deModalidde, deUnidade, noTurma, noAluno, noBeneficiario, noMat, Periodo, anogr;


            coEmp = LoginAuxili.CO_EMP;
            unidade = int.Parse(ddlUnidade.SelectedValue);
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            modalidade = int.Parse(ddlModalidade.SelectedValue);
            dataIni = IniPeri.Text;
            dataFim = FimPeri.Text;
            Periodo = dataIni + " à " + dataFim;
            
            deModalidde = (modalidade != 0 ? TB44_MODULO.RetornaPelaChavePrimaria(modalidade).DE_MODU_CUR : "Todos");
            deUnidade = (unidade != 0 ? TB25_EMPRESA.RetornaPelaChavePrimaria(unidade).NO_FANTAS_EMP : "Todos");

            parametros = "( Modalidade: " + deModalidde.ToUpper() + " - Unidade: " + deUnidade.ToUpper() + " - Período: " + Periodo.ToUpper() + " )";

            RptImpostosPorUnidadecs fpcb = new RptImpostosPorUnidadecs();
            lRetorno = fpcb.InitReport(parametros, infos, coEmp, unidade);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        void porModalidade()
        {
            string parametros = "";
            string infos;
            int coEmp, lRetorno, unidade, modalidade;
            string dataIni, dataFim, deModalidde, deUnidade, noTurma, noAluno, noBeneficiario, noMat, Periodo, anogr;


            coEmp = LoginAuxili.CO_EMP;
            unidade = int.Parse(ddlUnidade.SelectedValue);
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            modalidade = int.Parse(ddlModalidade.SelectedValue);
            dataIni = IniPeri.Text;
            dataFim = FimPeri.Text;
            Periodo = dataIni + " à " + dataFim;

            deModalidde = (modalidade != 0 ? TB44_MODULO.RetornaPelaChavePrimaria(modalidade).DE_MODU_CUR : "Todos");
            deUnidade = (unidade != 0 ? TB25_EMPRESA.RetornaPelaChavePrimaria(unidade).NO_FANTAS_EMP : "Todos");

            parametros = "( Modalidade: " + deModalidde.ToUpper() + " - Unidade: " + deUnidade.ToUpper() + " - Período: " + Periodo.ToUpper() + " )";

            RptImpostosPorModal fpcb = new RptImpostosPorModal();
            lRetorno = fpcb.InitReport(parametros, infos, coEmp, unidade);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
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
        private void carregaModalidade()
        {
            var res = (from tb44 in TB44_MODULO.RetornaTodosRegistros()
                       select new { tb44.CO_MODU_CUR, tb44.DE_MODU_CUR });

            ddlModalidade.DataTextField = "";
            ddlModalidade.DataValueField = "";
            ddlModalidade.DataSource = res;
            ddlModalidade.DataBind();

            ddlModalidade.Items.Insert(0, new ListItem("Todos", "0"));
        }

        #endregion
    }
}