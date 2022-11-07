//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE DE FREQÜÊNCIA ESCOLAR DO ALUNO
// OBJETIVO: DIÁRIO DE CLASSE - FALTAS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 16/06/2014| Maxwell Pereira Almeida    | Criação da funcionalidade para emissão do 
//           |                            | diário de classe com a diferença de que este
//           |                            | é emitido por bimestre, não por mês referência.

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
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar;
namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar._3499_Relatorios._3499_ModelosDiarios
{
    public partial class DiarioBimestralSupremoMod2 : System.Web.UI.Page
    {
        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }

        public DateTime dataInicio;
        public DateTime dataFim;
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
                CarregaAnos();
                CarregaModalidade();
                CarregaSerieCurso();
                CarregaTurma();
                CarregaMaterias();
                CarregaMeses();
            }
        }

        //====> Método que faz a chamada de outro método de acordo com o Tipo
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            TB01_CURSO tb01 = TB01_CURSO.RetornaPelaChavePrimaria(int.Parse(ddlUnidade.SelectedValue), int.Parse(ddlModalidade.SelectedValue),
                        int.Parse(ddlSerieCurso.SelectedValue));

            //--------> Variáveis obrigatórias para gerar o Relatório
            string strParametrosRelatorio, strINFOS, strP_BIMESTRE;
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            int strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_MAT, strP_MES;
            string strP_CO_ANO_REF, strP_PROF_RESP = "", strP_LAYOUT, strP_TURNO, strP_MESText, strProfessorCod, strProfessor, strMateria;
            DateTime strP_dataInicial, strP_dataFinal;
            bool chkDetal = chkVerDetSim.Checked ? true : false;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = int.Parse(ddlUnidade.SelectedValue);
            strP_CO_MODU_CUR = int.Parse(ddlModalidade.SelectedValue);
            strP_CO_CUR = int.Parse(ddlSerieCurso.SelectedValue);
            strP_CO_TUR = int.Parse(ddlTurma.SelectedValue);
            strP_CO_ANO_REF = ddlAnoRefer.SelectedValue;
            //strP_MES = int.Parse(ddlMesReferencia.SelectedValue);
            //strP_MESText = ddlMesReferencia.Text;
            strP_LAYOUT = ddlTipo.SelectedValue;
            strP_BIMESTRE = ddlBimestre.SelectedValue;
            //CarregaMeses();
            strP_dataInicial = dataInicio;
            strP_dataFinal = dataFim;


            strP_TURNO = (from tb06 in TB06_TURMAS.RetornaTodosRegistros() where tb06.CO_TUR == strP_CO_TUR select new { tb06.CO_PERI_TUR }).FirstOrDefault().CO_PERI_TUR;

            switch (strP_TURNO)
            {
                case "M":
                    strP_TURNO = "MATUTINO";
                    break;
                case "V":
                    strP_TURNO = "VESPERTINO";
                    break;
                case "N":
                    strP_TURNO = "NOTURNO";
                    break;
            }

            strParametrosRelatorio = "( " + ddlModalidade.SelectedItem.ToString() + " - " + ddlSerieCurso.SelectedItem.ToString() +
                " - Turma: " + TB129_CADTURMAS.RetornaPelaChavePrimaria(strP_CO_TUR).CO_SIGLA_TURMA + " - Turno: " + strP_TURNO + " - Ano Letivo: " + ddlAnoRefer.SelectedItem.ToString().Trim();

            if (tb01.CO_PARAM_FREQ_TIPO != "M")
            {
                strP_CO_MAT = 0;
                strMateria = "*****";
                strProfessorCod = "";
                strProfessor = "";


            }
            else
            {
                strP_CO_MAT = int.Parse(ddlMateria.SelectedValue);
                strMateria = ddlMateria.SelectedItem.ToString().ToUpper().ToUpper();

                int anoRefer = int.Parse(strP_CO_ANO_REF);
                var tbResponMat = (from tbRespMat in TB_RESPON_MATERIA.RetornaTodosRegistros()
                                   join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbRespMat.CO_COL_RESP equals tb03.CO_COL
                                   where (tbRespMat.CO_ANO_REF == anoRefer) && (tbRespMat.CO_MODU_CUR == strP_CO_MODU_CUR) && (tbRespMat.CO_CUR == strP_CO_CUR)
                                   && (tbRespMat.CO_TUR == strP_CO_TUR)
                                   && (tbRespMat.CO_MAT == strP_CO_MAT)
                                   select new
                                   {
                                       CO_MAT_COL = tb03.CO_MAT_COL.Insert(5, "-").Insert(2, "."),
                                       tb03.NO_COL
                                   }).FirstOrDefault();

                if (tbResponMat != null)
                {
                    strProfessorCod = tbResponMat.CO_MAT_COL;
                    strProfessor = tbResponMat.NO_COL.ToUpper();
                }
                else
                {
                    strProfessor = "*****";
                    strProfessorCod = "";
                }
            }
            DateTime dtIniBim, dtFimBim;

            DateTime.TryParse(hidDtIniBim.Value, out dtIniBim);
            DateTime.TryParse(hidDtFimBim.Value, out dtFimBim);

            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            //Se é diário do professor
            bool diarioProfessor = (ddlEmissao.SelectedValue == "P" ? true : false);
            //Código do professor
            int coProfe = ddlProfessor.SelectedValue != "" ? int.Parse(ddlProfessor.SelectedValue) : 0;

            //RptDiarioClasseBimestral rpt = new RptDiarioClasseBimestral();
            //lRetorno = rpt.InitReport(strParametrosRelatorio, LoginAuxili.ORG_CODIGO_ORGAO, LoginAuxili.CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strINFOS, strP_CO_ANO_REF, strP_BIMESTRE, strP_CO_MAT, strP_PROF_RESP, strP_LAYOUT, strP_dataInicial, strP_dataFinal, strProfessorCod, strProfessor, strMateria, dtIniBim, dtFimBim);

            #region Consulta utilizando o linq
            var ctx = GestorEntities.CurrentContext;
            var lst = (from mat in ctx.TB08_MATRCUR.AsQueryable()
                       where mat.CO_EMP == LoginAuxili.CO_EMP
                       && mat.CO_ANO_MES_MAT == strP_CO_ANO_REF
                       && mat.CO_CUR == strP_CO_CUR
                       && mat.CO_TUR == strP_CO_TUR
                       && mat.TB44_MODULO.CO_MODU_CUR == strP_CO_MODU_CUR
                       join fre in ctx.TB132_FREQ_ALU.AsQueryable() on mat.CO_ALU equals fre.TB07_ALUNO.CO_ALU into resultado
                       from fre in resultado.DefaultIfEmpty()
                       where
                       (fre.DT_FRE >= dtIniBim) && (fre.DT_FRE <= dtFimBim)
                       select new
                       {
                           homolocacao = fre.FL_HOMOL_FREQU
                       }
                         ).Distinct().OrderBy(o => o.homolocacao);
            //).ToList();

            #endregion
            // Caso ah alguma homologação por fazer  e não a  homologação feita 
            if (lst.Where(a => a.homolocacao == "N").Count() > 0 && lst.Where(b => b.homolocacao == "S").Count() == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Há frequências lançadas nesses parâmetros porém  não a homologação  ");

            }
            else // caso a  homologação feita 
            {
                RptDiarioClasseBimestral rpt = new RptDiarioClasseBimestral();
                lRetorno = rpt.InitReport(strParametrosRelatorio, LoginAuxili.ORG_CODIGO_ORGAO, LoginAuxili.CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strINFOS, strP_CO_ANO_REF, strP_BIMESTRE, strP_CO_MAT, strP_PROF_RESP, strP_LAYOUT, strP_dataInicial, strP_dataFinal, strProfessorCod, strProfessor, strMateria, dtIniBim, dtFimBim, chkDetal, diarioProfessor, coProfe, (chkImprimeMedias.Checked), (chkPresenP.Checked), (chkImpAssinSegPag.Checked), ETipoDiario.Completo, EModeloDiario.ModeloSupremoNovo);
                Session["Report"] = rpt;
                Session["URLRelatorio"] = "/GeducReportViewer.aspx";

                AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);


            }

        }

        //====> Processo de Geração do Relatório
        void PautaChamadaFrente()
        {
            TB01_CURSO tb01 = TB01_CURSO.RetornaPelaChavePrimaria(int.Parse(ddlUnidade.SelectedValue), int.Parse(ddlModalidade.SelectedValue),
            int.Parse(ddlSerieCurso.SelectedValue));

            //--------> Variáveis obrigatórias para gerar o Relatório
            string strParametrosRelatorio, strINFOS;
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            int strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_MAT, strP_MES;
            string strP_CO_ANO_REF, strP_BIMESTRE, strP_PROF_RESP = "";

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = int.Parse(ddlUnidade.SelectedValue);
            strP_CO_MODU_CUR = int.Parse(ddlModalidade.SelectedValue);
            strP_CO_CUR = int.Parse(ddlSerieCurso.SelectedValue);
            strP_CO_TUR = int.Parse(ddlTurma.SelectedValue);
            strP_CO_ANO_REF = ddlAnoRefer.SelectedValue;
            //strP_MES = int.Parse(ddlMesReferencia.SelectedValue);
            strP_BIMESTRE = ddlBimestre.SelectedValue;

            strParametrosRelatorio = "( " + ddlModalidade.SelectedItem.ToString() + " - " + ddlSerieCurso.SelectedItem.ToString() +
                " - Turma: " + TB129_CADTURMAS.RetornaPelaChavePrimaria(strP_CO_TUR).CO_SIGLA_TURMA + " - Ano Letivo: " + ddlAnoRefer.SelectedItem.ToString().Trim()
                + " - Bimestre -  " + strP_BIMESTRE;

            if (tb01.CO_PARAM_FREQ_TIPO != "M")
            {
                strP_CO_MAT = 0;
                strParametrosRelatorio = strParametrosRelatorio + " - Matéria: ***** )";
            }
            else
            {
                strP_CO_MAT = int.Parse(ddlMateria.SelectedValue);
                strParametrosRelatorio = strParametrosRelatorio + " - Matéria: " + ddlMateria.SelectedItem.ToString() + " )";

            }
            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptDiarioClasseFrenteBimestral2 rpt = new RptDiarioClasseFrenteBimestral2();
            //lRetorno = rpt.InitReport(strParametrosRelatorio, LoginAuxili.ORG_CODIGO_ORGAO, LoginAuxili.CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strINFOS, strP_CO_ANO_REF, strP_BIMESTRE, strP_CO_MAT, strP_PROF_RESP);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            //AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        //====> Processo de Geração do Relatório
        void PautaChamadaVerso()
        {
            TB01_CURSO tb01 = TB01_CURSO.RetornaPelaChavePrimaria(int.Parse(ddlUnidade.SelectedValue), int.Parse(ddlModalidade.SelectedValue),
                        int.Parse(ddlSerieCurso.SelectedValue));

            //--------> Variáveis obrigatórias para gerar o Relatório
            string strParametrosRelatorio, strINFOS, strP_BIMESTRE;
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            int strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_MAT, strP_MES;
            string strP_CO_ANO_REF, strP_PROF_RESP = "";

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = int.Parse(ddlUnidade.SelectedValue);
            strP_CO_MODU_CUR = int.Parse(ddlModalidade.SelectedValue);
            strP_CO_CUR = int.Parse(ddlSerieCurso.SelectedValue);
            strP_CO_TUR = int.Parse(ddlTurma.SelectedValue);
            strP_CO_ANO_REF = ddlAnoRefer.SelectedValue;
            //strP_MES = int.Parse(ddlMesReferencia.SelectedValue);
            strP_BIMESTRE = ddlBimestre.SelectedValue;

            strParametrosRelatorio = "( " + ddlModalidade.SelectedItem.ToString() + " - " + ddlSerieCurso.SelectedItem.ToString() +
                " - Turma: " + TB129_CADTURMAS.RetornaPelaChavePrimaria(strP_CO_TUR).CO_SIGLA_TURMA + " - Ano Letivo: " + ddlAnoRefer.SelectedItem.ToString().Trim();

            if (tb01.CO_PARAM_FREQ_TIPO != "M")
            {
                strP_CO_MAT = 0;
                strParametrosRelatorio = strParametrosRelatorio + " - Matéria: ***** )";
                strP_PROF_RESP = "( Professor Responsável: ***** )";
            }
            else
            {
                strP_CO_MAT = int.Parse(ddlMateria.SelectedValue);
                strParametrosRelatorio = strParametrosRelatorio + " - Matéria: " + ddlMateria.SelectedItem.ToString() + " )";

                int anoRefer = int.Parse(strP_CO_ANO_REF);
                var tbResponMat = (from tbRespMat in TB_RESPON_MATERIA.RetornaTodosRegistros()
                                   join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbRespMat.CO_COL_RESP equals tb03.CO_COL
                                   where (tbRespMat.CO_ANO_REF == anoRefer) && (tbRespMat.CO_MODU_CUR == strP_CO_MODU_CUR) && (tbRespMat.CO_CUR == strP_CO_CUR)
                                   && (tbRespMat.CO_TUR == strP_CO_TUR)
                                   && (tbRespMat.CO_MAT == strP_CO_MAT)
                                   select new
                                   {
                                       CO_MAT_COL = tb03.CO_MAT_COL.Insert(5, "-").Insert(2, "."),
                                       tb03.NO_COL
                                   }).FirstOrDefault();

                if (tbResponMat != null)
                {
                    strP_PROF_RESP = "( Professor Responsável: " + tbResponMat.CO_MAT_COL + " " + tbResponMat.NO_COL + " )";
                }
                else
                {
                    strP_PROF_RESP = "( Professor Responsável: ***** )";
                }
            }
            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            //RptDiarioClasse rpt = new RptDiarioClasse();
            //lRetorno = rpt.InitReport(strParametrosRelatorio, LoginAuxili.ORG_CODIGO_ORGAO, LoginAuxili.CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strINFOS, strP_CO_ANO_REF, strP_BIMESTRE, strP_CO_MAT, strP_PROF_RESP, strP_LAYOUT);
            //Session["Report"] = rpt;
            //Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            //AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
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
        }

        /// <summary>
        /// Carrega os professores
        /// </summary>
        private void CarregaFuncionarios()
        {
            int counid = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modali = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int disc = ddlMateria.SelectedValue != "" ? int.Parse(ddlMateria.SelectedValue) : 0;
            int ano = ddlAnoRefer.SelectedValue != "" ? int.Parse(ddlAnoRefer.SelectedValue) : 0;

            AuxiliCarregamentos.CarregaProfessoresRespMateria(ddlProfessor, counid, modali, serie, turma, disc, ano);
            ddlProfessor.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            if (coEmp != 0)
            {
                ddlAnoRefer.DataSource = (from tb48 in TB48_GRADE_ALUNO.RetornaTodosRegistros()
                                          where tb48.TB25_EMPRESA.CO_EMP == coEmp
                                          select new { tb48.CO_ANO_MES_MAT }).Distinct().OrderByDescending(g => g.CO_ANO_MES_MAT);

                ddlAnoRefer.DataTextField = "CO_ANO_MES_MAT";
                ddlAnoRefer.DataValueField = "CO_ANO_MES_MAT";
                ddlAnoRefer.DataBind();
            }
            else
                ddlAnoRefer.Items.Clear();
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidade()
        {
            if (LoginAuxili.FLA_PROFESSOR != "S")
                AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, false);
            else
            {
                int anoRef = (ddlAnoRefer.SelectedValue != "" ? int.Parse(ddlAnoRefer.SelectedValue) : 0);
                AuxiliCarregamentos.carregaModalidadesProfeResp(ddlModalidade, LoginAuxili.CO_COL, anoRef, false);
            }

            CarregaSerieCurso();
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        /// <param name="coModuCur">Id da modalidade</param>
        private void CarregaSerieCurso()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int anoGrade = (ddlAnoRefer.SelectedValue != "" ? int.Parse(ddlAnoRefer.SelectedValue) : 0);
            int modalidade = (ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0);
            if (LoginAuxili.FLA_PROFESSOR != "S")
                AuxiliCarregamentos.carregaSeriesCursos(ddlSerieCurso, modalidade, coEmp, false);
            else
                AuxiliCarregamentos.carregaSeriesCursosProfeResp(ddlSerieCurso, modalidade, LoginAuxili.CO_COL, anoGrade, false);

            CarregaTurma();
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        /// <param name="coModuCur">Id da modalidade</param>
        private void CarregaTurma()
        {
            int coEmp = (ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0);
            int modalidade = (ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0);
            int seria = (ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0);
            int ano = (ddlAnoRefer.SelectedValue != "" ? int.Parse(ddlAnoRefer.SelectedValue) : 0);
            if (LoginAuxili.FLA_PROFESSOR != "S")
                AuxiliCarregamentos.CarregaTurmas(ddlTurma, coEmp, modalidade, seria, false);
            else
                AuxiliCarregamentos.CarregaTurmasProfeResp(ddlTurma, coEmp, modalidade, seria, LoginAuxili.CO_COL, ano, false);

            CarregaMaterias();
        }

        /// <summary>
        /// Método que carrega o dropdown de Matérias
        /// </summary>
        /// <param name="codMod">Id da modalidade</param>
        private void CarregaMaterias(bool Relatorio = false)
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string anoGrade = ddlAnoRefer.SelectedValue;
            int anoInt = (ddlAnoRefer.SelectedValue != "" ? int.Parse(ddlAnoRefer.SelectedValue) : 0);

            if ((serie != 0) && (modalidade != 0) && (turma != 0) && (coEmp != 0))
            {
                string turmaUnica = TB06_TURMAS.RetornaPelaChavePrimaria(coEmp, modalidade, serie, turma).CO_FLAG_RESP_TURMA;
                string tipoFreq = TB01_CURSO.RetornaPelaChavePrimaria(int.Parse(ddlUnidade.SelectedValue), int.Parse(ddlModalidade.SelectedValue),
                        int.Parse(ddlSerieCurso.SelectedValue)).CO_PARAM_FREQ_TIPO;

                if (tipoFreq == "M")
                {
                    if (turmaUnica == "S")
                    {
                        liMateria.Visible = true;

                        if (LoginAuxili.FLA_PROFESSOR != "S")
                        {
                            ddlMateria.DataSource = (from tb02 in TB02_MATERIA.RetornaTodosRegistros()
                                                     join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                                     where tb107.NO_SIGLA_MATERIA == "MSR"
                                                     && tb02.CO_CUR == serie
                                                     select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy(m => m.NO_MATERIA);

                            ddlMateria.DataTextField = "NO_MATERIA";
                            ddlMateria.DataValueField = "CO_MAT";
                            ddlMateria.DataBind();
                        }
                        else
                        {
                            ddlMateria.DataSource = (from tb02 in TB02_MATERIA.RetornaTodosRegistros()
                                                     join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                                     join tbr in TB_RESPON_MATERIA.RetornaTodosRegistros() on tb02.CO_MAT equals tbr.CO_MAT
                                                     where tb107.NO_SIGLA_MATERIA == "MSR"
                                                     && tb02.CO_CUR == serie
                                                     && tbr.CO_COL_RESP == LoginAuxili.CO_COL
                                                     select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy(m => m.NO_MATERIA);

                            ddlMateria.DataTextField = "NO_MATERIA";
                            ddlMateria.DataValueField = "CO_MAT";
                            ddlMateria.DataBind();
                        }
                    }
                    else
                    {
                        if (LoginAuxili.FLA_PROFESSOR != "S")
                            AuxiliCarregamentos.CarregaMateriasGradeCurso(ddlMateria, coEmp, modalidade, serie, anoGrade, Relatorio, true, false);
                        else
                            AuxiliCarregamentos.CarregaMateriasProfeRespon(ddlMateria, LoginAuxili.CO_COL, modalidade, serie, anoInt, Relatorio, true);

                        //liMateria.Visible = true;
                        //ddlMateria.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                        //                         where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.CO_CUR == serie
                        //                         && tb43.CO_ANO_GRADE == anoGrade && tb43.CO_EMP == coEmp
                        //                         join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                        //                         join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                        //                         select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy(m => m.NO_MATERIA);

                        //ddlMateria.DataTextField = "NO_MATERIA";
                        //ddlMateria.DataValueField = "CO_MAT";
                        //ddlMateria.DataBind();

                    }
                }
                else
                {
                    liMateria.Visible = false;
                    ddlMateria.Items.Clear();
                }

            }
            else
            {
                if (Relatorio)
                    ddlMateria.Items.Insert(0, new ListItem("Todos", "0"));
                else
                    ddlMateria.Items.Insert(0, new ListItem("Selecione", ""));
            }
        }

        private void CarregaMeses()
        {
            var ctx = GestorEntities.CurrentContext;
            string bimestre = ddlBimestre.SelectedValue;
            int unidade = int.Parse(ddlUnidade.SelectedValue);


            #region Retorna as Datas dos Bimestres
            var datasBimestre = (from tb82 in ctx.TB82_DTCT_EMP
                                 where tb82.CO_EMP == unidade

                                 select new
                                 {
                                     // datas inicias dos bimestres
                                     dataInicialBimestre1 = tb82.DT_PERIO_INICI_BIM1,
                                     dataInicialBimestre2 = tb82.DT_PERIO_INICI_BIM2,
                                     dataInicialBimestre3 = tb82.DT_PERIO_INICI_BIM3,
                                     dataInicialBimestre4 = tb82.DT_PERIO_INICI_BIM4,

                                     // datas finais dos bimestres
                                     dataFinalBimestre1 = tb82.DT_PERIO_FINAL_BIM1,
                                     dataFinalBimestre2 = tb82.DT_PERIO_FINAL_BIM2,
                                     dataFinalBimestre3 = tb82.DT_PERIO_FINAL_BIM3,
                                     dataFinalBimestre4 = tb82.DT_PERIO_FINAL_BIM4,

                                 }).OrderBy(o => o.dataInicialBimestre1).FirstOrDefault();

            //ListItem item1 = new ListItem() { Value = "1", Text = "Janeiro" };
            //ListItem item2 = new ListItem() { Value = "2", Text = "Fevereiro" };
            //ListItem item3 = new ListItem() { Value = "3", Text = "Março" };
            //ListItem item4 = new ListItem() { Value = "4", Text = "Abril" };
            //ListItem item5 = new ListItem() { Value = "5", Text = "Maio" };
            //ListItem item6 = new ListItem() { Value = "6", Text = "Junho" };
            //ListItem item7 = new ListItem() { Value = "7", Text = "Julho" };
            //ListItem item8 = new ListItem() { Value = "8", Text = "Agosto" };
            //ListItem item9 = new ListItem() { Value = "9", Text = "Setembro" };
            //ListItem item10 = new ListItem() { Value = "10", Text = "Outubro" };
            //ListItem item11 = new ListItem() { Value = "11", Text = "Novembro" };
            //ListItem item12 = new ListItem() { Value = "12", Text = "Dezembro" };

            //List<ListItem> lstItems = new List<ListItem>();

            //lstItems.Add(item1);
            //lstItems.Add(item2);
            //lstItems.Add(item3);
            //lstItems.Add(item4);
            //lstItems.Add(item5);
            //lstItems.Add(item6);
            //lstItems.Add(item7);
            //lstItems.Add(item8);
            //lstItems.Add(item9);
            //lstItems.Add(item10);
            //lstItems.Add(item11);
            //lstItems.Add(item12);

            switch (bimestre)
            {
                case "B1":
                    // Limpa os meses
                    //ddlMesReferencia.Items.Clear();

                    // Pesquisa as datas de início e fim do bimestre
                    var mesInicio1 = datasBimestre.dataInicialBimestre1.Value.Month;
                    var mesFinal1 = datasBimestre.dataFinalBimestre1.Value.Month;

                    hidDtIniBim.Value = datasBimestre.dataInicialBimestre1 != null ? datasBimestre.dataInicialBimestre1.ToString() : "";
                    hidDtFimBim.Value = datasBimestre.dataFinalBimestre1 != null ? datasBimestre.dataFinalBimestre1.ToString() : "";

                    if (datasBimestre.dataInicialBimestre1 == null)
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Não existe data Inicial cadastrada para o Bimestre Selecionado");

                    if (datasBimestre.dataFinalBimestre1 == null)
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Não existe data Final cadastrada para o Bimestre Selecionado");

                    dataInicio = datasBimestre.dataInicialBimestre1.Value;
                    dataFim = datasBimestre.dataFinalBimestre1.Value;

                    // Percorre do mes início até o mes final adicionando no drop down
                    //for (int i = mesInicio1; i <= mesFinal1; i++)
                    //    ddlMesReferencia.Items.Add(lstItems.Find(f => f.Value == i.ToString()));
                    break;
                case "B2":
                    // Limpa os meses
                    //ddlMesReferencia.Items.Clear();

                    // Pesquisa as datas de início e fim do bimestre
                    var mesInicio2 = datasBimestre.dataInicialBimestre2.Value.Month;
                    var mesFinal2 = datasBimestre.dataFinalBimestre2.Value.Month;

                    hidDtIniBim.Value = datasBimestre.dataInicialBimestre2 != null ? datasBimestre.dataInicialBimestre2.ToString() : "";
                    hidDtFimBim.Value = datasBimestre.dataFinalBimestre2 != null ? datasBimestre.dataFinalBimestre2.ToString() : "";

                    if (datasBimestre.dataInicialBimestre2 == null)
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Não existe data Inicial cadastrada para o Bimestre Selecionado");

                    if (datasBimestre.dataFinalBimestre2 == null)
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Não existe data Final cadastrada para o Bimestre Selecionado");

                    dataInicio = datasBimestre.dataInicialBimestre2.Value;
                    dataFim = datasBimestre.dataFinalBimestre2.Value;

                    // Percorre do mes início até o mes final adicionando no drop down
                    //for (int i = mesInicio2; i <= mesFinal2; i++)
                    //    ddlMesReferencia.Items.Add(lstItems.Find(f => f.Value == i.ToString()));
                    break;
                case "B3":
                    // Limpa os meses
                    //ddlMesReferencia.Items.Clear();

                    // Pesquisa as datas de início e fim do bimestre
                    var mesInicio3 = datasBimestre.dataInicialBimestre3.Value.Month;
                    var mesFinal3 = datasBimestre.dataFinalBimestre3.Value.Month;

                    hidDtIniBim.Value = datasBimestre.dataInicialBimestre3 != null ? datasBimestre.dataInicialBimestre3.ToString() : "";
                    hidDtFimBim.Value = datasBimestre.dataFinalBimestre3 != null ? datasBimestre.dataFinalBimestre3.ToString() : "";

                    if (datasBimestre.dataInicialBimestre3 == null)
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Não existe data Inicial cadastrada para o Bimestre Selecionado");

                    if (datasBimestre.dataFinalBimestre3 == null)
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Não existe data Final cadastrada para o Bimestre Selecionado");

                    dataInicio = datasBimestre.dataInicialBimestre3.Value;
                    dataFim = datasBimestre.dataFinalBimestre3.Value;

                    // Percorre do mes início até o mes final adicionando no drop down
                    //for (int i = mesInicio3; i <= mesFinal3; i++)
                    //    ddlMesReferencia.Items.Add(lstItems.Find(f => f.Value == i.ToString()));
                    break;
                case "B4":
                    // Limpa os meses
                    //ddlMesReferencia.Items.Clear();

                    // Pesquisa as datas de início e fim do bimestre
                    var mesInicio4 = datasBimestre.dataInicialBimestre4.Value.Month;
                    var mesFinal4 = datasBimestre.dataFinalBimestre4.Value.Month;

                    hidDtIniBim.Value = datasBimestre.dataInicialBimestre4 != null ? datasBimestre.dataInicialBimestre4.ToString() : "";
                    hidDtFimBim.Value = datasBimestre.dataFinalBimestre4 != null ? datasBimestre.dataFinalBimestre4.ToString() : "";

                    if (datasBimestre.dataInicialBimestre4 == null)
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Não existe data Inicial cadastrada para o Bimestre Selecionado");

                    if (datasBimestre.dataFinalBimestre4 == null)
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Não existe data Final cadastrada para o Bimestre Selecionado");

                    dataInicio = datasBimestre.dataInicialBimestre4.Value;
                    dataFim = datasBimestre.dataFinalBimestre4.Value;

                    // Percorre do mes início até o mes final adicionando no drop down
                    //for (int i = mesInicio4; i <= mesFinal4; i++)
                    //    ddlMesReferencia.Items.Add(lstItems.Find(f => f.Value == i.ToString()));
                    break;
                default:
                    break;
            }

            #endregion

        }
        #endregion

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
        }

        protected void ddlUnidade_SelectedIndexChanged1(object sender, EventArgs e)
        {
            CarregaAnos();
            CarregaModalidade();
            CarregaSerieCurso();
            CarregaTurma();
            CarregaMaterias();
        }

        protected void ddlAnoRefer_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaModalidade();
            CarregaSerieCurso();
            CarregaTurma();
            CarregaMaterias();
        }

        protected void ddlEmissao_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaFuncionarios();
            if (ddlEmissao.SelectedValue == "D")
            {
                liProfessor.Visible = false;
                liMateria.Visible = true;
                CarregaMaterias();
            }
            else
            {
                liProfessor.Visible = true;
                liMateria.Visible = false;
                CarregaMaterias(true);
            }
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaMaterias();
            CarregaFuncionarios();
        }

        protected void ddlBimestre_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaMeses();
        }

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlTipo.SelectedValue == "F")
            {
                chkVerDetSim.Enabled = false;
                chkVerDetSim.Checked = false;
            }
            else
            {
                chkVerDetSim.Enabled = true;
                chkVerDetSim.Checked = false;
            }
        }
    }
}