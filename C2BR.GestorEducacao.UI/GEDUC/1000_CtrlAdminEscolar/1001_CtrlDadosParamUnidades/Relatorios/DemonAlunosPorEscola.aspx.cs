//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: DADOS/PARAMETRIZAÇÃO UNIDADES DE ENSINO
// OBJETIVO: EMISSÃO DO DEMONSTRATIVO DE ALUNOS POR ESCOLA
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
using C2BR.GestorEducacao.Reports._1000_CtrlAdminEscolar._1001_CtrlDadosParamUnidades;
using DevExpress.XtraReports.UI;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1001_CtrlDadosParamUnidades.Relatorios
{
    public partial class DemonAlunosPorEscola : System.Web.UI.Page
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
                CarregaDropDown();
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            string codSituMat, infos, anoRef;
            int codEmp, codOrg;

            lRetorno = 0;
            codEmp = LoginAuxili.CO_EMP;
            codOrg = LoginAuxili.ORG_CODIGO_ORGAO;
            anoRef = ddlAnoRefer.SelectedValue;
            codSituMat = ddlSituMatricula.SelectedValue;
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            XtraReport rpt = null;

            if (ddlTipoEnsino.SelectedValue == "I")
            {
                rpt = new RptDemQtdAlunosEnsinoInfantil();
                lRetorno = (rpt as RptDemQtdAlunosEnsinoInfantil).InitReport(codEmp, codOrg, anoRef, codSituMat, infos);
            }
            else if (ddlTipoEnsino.SelectedValue == "F")
            {
                rpt = new RptDemQtdAlunosEnsinoFundamental();
                lRetorno = (rpt as RptDemQtdAlunosEnsinoFundamental).InitReport(codEmp, codOrg, anoRef, codSituMat, infos);
            }
            else if (ddlTipoEnsino.SelectedValue == "M")
            {
                rpt = new RptDemQtdAlunosEnsinoMedio();
                lRetorno = (rpt as RptDemQtdAlunosEnsinoMedio).InitReport(codEmp, codOrg, anoRef, codSituMat, infos);
            }
            else if (ddlTipoEnsino.SelectedValue == "O")
            {
                rpt = new RptDemQtdAlunosOutros();
                lRetorno = (rpt as RptDemQtdAlunosOutros).InitReport(codEmp, codOrg, anoRef, codSituMat, infos);
            }

            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";
            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Tipos de Unidade e Ano de Referencia
        /// </summary>
        private void CarregaDropDown()
        {
            var res = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                       where tb08.TB25_EMPRESA.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                       select tb08.CO_ANO_MES_MAT);

            ddlAnoRefer.DataSource = res.DistinctBy(x => x).Select(x => new { Ano = int.Parse(x) })
                .OrderByDescending(x => x.Ano).ToList();

            ddlAnoRefer.DataTextField = "Ano";
            ddlAnoRefer.DataValueField = "Ano";
            ddlAnoRefer.DataBind();
            ddlAnoRefer.SelectedIndex = 0;
        }
        #endregion
    }
}
