//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: CONTROLE DE SÉRIES OU CURSOS
// OBJETIVO: EMISSÃO DA GRADE ANUAL CURRICULAR DA SÉRIE/CURSO
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
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3010_CtrlPedagogicoSeries;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3010_CtrlPedagogicoSeries.F3019_Relatorios
{
    public partial class GradeCurricSerie : System.Web.UI.Page
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
                CarregaModalidade();

                if (LoginAuxili.TIPO_USU.Equals("A"))
                {
                    var tb08 = TB08_MATRCUR.RetornaPeloAluno(LoginAuxili.CO_RESP);
                    tb08.TB44_MODULOReference.Load();

                    if (tb08.CO_EMP != 0 && ddlUnidade.Items.FindByValue(tb08.CO_EMP.ToString()) != null)
                        ddlUnidade.SelectedValue = tb08.CO_EMP.ToString();

                    if (tb08.TB44_MODULO.CO_MODU_CUR != 0 && ddlModalidade.Items.FindByValue(tb08.TB44_MODULO.CO_MODU_CUR.ToString()) != null)
                        ddlModalidade.SelectedValue = tb08.TB44_MODULO.CO_MODU_CUR.ToString();

                    CarregaSerieCurso();

                    if (tb08.CO_CUR != 0 && ddlSerieCurso.Items.FindByValue(tb08.CO_CUR.ToString()) != null)
                        ddlSerieCurso.SelectedValue = tb08.CO_CUR.ToString();

                    ddlUnidade.Enabled =
                    ddlModalidade.Enabled =
                    ddlSerieCurso.Enabled = false;
                }
                else
                    CarregaSerieCurso();
            }
        }

//====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
//--------> Variáveis obrigatórias para gerar o Relatório
            string strINFOS, strParametrosRelatorio, strCaminhoRelatorioGerado, strNomeRelatorio;
            int lRetorno;

//--------> Variáveis de parâmetro do Relatório
            int strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_ANO_INI, strP_CO_ANO_FIM;

//--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = int.Parse(ddlUnidade.SelectedValue);
            strP_CO_MODU_CUR = int.Parse(ddlModalidade.SelectedValue);
            strP_CO_CUR = int.Parse(ddlSerieCurso.SelectedValue);
            strP_CO_ANO_INI = int.Parse(txtAnoIni.Text);
            strP_CO_ANO_FIM = int.Parse(txtAnoFim.Text);
            strParametrosRelatorio = "";
            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptGradeCurricSerie rpt = new RptGradeCurricSerie();
            lRetorno = rpt.InitReport(strParametrosRelatorio, LoginAuxili.CO_EMP, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_ANO_INI,
                strP_CO_ANO_FIM, strINFOS);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }       
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares e Modalidades
        /// </summary>
        private void CarregaDropDown()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                     where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy(e => e.NO_FANTAS_EMP);

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();

            //ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where( m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            //ddlModalidade.DataTextField = "DE_MODU_CUR";
            //ddlModalidade.DataValueField = "CO_MODU_CUR";
            //ddlModalidade.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Coordenação, verifica se o usuário logado é professor.
        /// </summary>
        private void CarregaModalidade()
        {
            if (LoginAuxili.FLA_PROFESSOR != "S")
            {
                AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
            }
            else
            {
                int ano = DateTime.Now.Year;
                AuxiliCarregamentos.carregaModalidadesProfeResp(ddlModalidade, LoginAuxili.CO_COL, ano, true);
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries, verifica se o usuário logado é professor.
        /// </summary>
        private void CarregaSerieCurso()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            if (LoginAuxili.FLA_PROFESSOR != "S")
            {

                //ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(coEmp)
                //                            where tb01.CO_MODU_CUR == modalidade
                //                            select new { tb01.CO_SIGL_CUR, tb01.CO_CUR }).OrderBy(c => c.CO_SIGL_CUR);

                //ddlSerieCurso.DataTextField = "CO_SIGL_CUR";
                //ddlSerieCurso.DataValueField = "CO_CUR";
                //ddlSerieCurso.DataBind();

                AuxiliCarregamentos.carregaSeriesCursos(ddlSerieCurso, modalidade, coEmp, true);
            }
            else
            {
                int ano = DateTime.Now.Year;
                AuxiliCarregamentos.carregaSeriesCursosProfeResp(ddlSerieCurso, modalidade, LoginAuxili.CO_COL, ano, true);
            }
        }
        #endregion

        protected void ddlUnidade_SelectedIndexChanged1(object sender, EventArgs e)
        {
            CarregaSerieCurso();
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
        }
    }
}
