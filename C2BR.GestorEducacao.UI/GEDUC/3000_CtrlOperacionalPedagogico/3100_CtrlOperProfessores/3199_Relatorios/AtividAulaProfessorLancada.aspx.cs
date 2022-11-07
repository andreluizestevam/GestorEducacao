//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE LETIVO DE AULAS E ATIVIDADES
// OBJETIVO: HISTÓRICO DAS ATIVIDADES LETIVAS EXECUTADAS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 09/05/2013| André Nobre Vinagre        | - Corrigida inconsistência encontrada na hora de gerar o
//           |                            | relatório para o cliente Barão do Rio Branco
//           |                            |

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
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3800_CtrlOperacionalProfessores;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3100_CtrlOperProfessores.F3199_Relatorios
{
    public partial class AtividAulaProfessorLancada : System.Web.UI.Page
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
                if (LoginAuxili.TIPO_USU.Equals("R")) {
                    lblTurma.Visible = false;
                    ddlTurma.Visible = false;
                    lblUnidade.Visible = false;
                    ddlUnidade.Visible = false;
                }
                CarregaUnidades();
                CarregaFuncionarios();
                CarregaAnos();
                CarregaModalidade();

                if (LoginAuxili.TIPO_USU.Equals("A"))
                {
                    var tb08 = TB08_MATRCUR.RetornaPeloAluno(LoginAuxili.CO_RESP);
                    tb08.TB44_MODULOReference.Load();

                    if (tb08.CO_EMP != 0 && ddlUnidade.Items.FindByValue(tb08.CO_EMP.ToString()) != null)
                        ddlUnidade.SelectedValue = tb08.CO_EMP.ToString();

                    if (!string.IsNullOrEmpty(tb08.CO_ANO_MES_MAT) && ddlAnoRefer.Items.FindByValue(tb08.CO_ANO_MES_MAT) != null)
                        ddlAnoRefer.SelectedValue = tb08.CO_ANO_MES_MAT;

                    if (tb08.TB44_MODULO.CO_MODU_CUR != 0 && ddlModalidade.Items.FindByValue(tb08.TB44_MODULO.CO_MODU_CUR.ToString()) != null)
                        ddlModalidade.SelectedValue = tb08.TB44_MODULO.CO_MODU_CUR.ToString();

                    CarregaSerieCurso();

                    if (tb08.CO_CUR != 0 && ddlSerieCurso.Items.FindByValue(tb08.CO_CUR.ToString()) != null)
                        ddlSerieCurso.SelectedValue = tb08.CO_CUR.ToString();

                    CarregaTurma();

                    if (tb08.CO_TUR != 0 && ddlTurma.Items.FindByValue(tb08.CO_TUR.ToString()) != null)
                        ddlTurma.SelectedValue = tb08.CO_TUR.ToString();

                    ddlUnidade.Enabled =
                    ddlAnoRefer.Enabled =
                    ddlModalidade.Enabled =
                    ddlSerieCurso.Enabled =
                    ddlTurma.Enabled = false;
                }
                else
                {
                    CarregaSerieCurso();
                    CarregaTurma();
                }

                ddlFuncionarios.Enabled = true;

                //ddlSerieCurso.Items.Insert(0, new ListItem("Todas", "0"));
                //ddlTurma.Items.Insert(0, new ListItem("Todas", "0"));
            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            #region desuso
            ////--------> Variáveis obrigatórias para gerar o Relatório
            //            string strIDSessao, strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, parametros;
            //            int lRetorno, coEmp, Unidade, Colaborador, anoRef, Modalidade, SerieCur, Turma;

            ////--------> Variáveis de parâmetro do Relatório
            //            string infos, strP_CO_EMP, strP_CO_COL, strP_CO_ANO_MES_MAT, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, deModalidde, noCur, noTurma;

            //            var varRelatorioWeb = new ChannelFactory<IRelatorioWeb>(new NetTcpBinding(), WRAuxiliares.URLRelatorioWeb);
            //            IRelatorioWeb lIRelatorioWeb;

            //            strIDSessao = Session.SessionID.ToString();
            //            strIdentFunc = WRAuxiliares.IdentFunc;
            //            strCaminhoRelatorioGerado = HttpRuntime.AppDomainAppPath + "TMP_Relatorios\\" + strIDSessao + "\\";
            //            strNomeRelatorio = WRAuxiliares.GeraNomeRelatorio("RelAtivRealizProfessor");

            ////--------> Criação da Pasta
            //            if (!Directory.Exists(@strCaminhoRelatorioGerado))
            //                Directory.CreateDirectory(@strCaminhoRelatorioGerado);


            ////--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            //strP_CO_EMP = ddlUnidade.SelectedValue;
            //strP_CO_COL = ddlFuncionarios.SelectedValue;
            //strP_CO_ANO_MES_MAT = ddlAnoRefer.SelectedValue;
            //strP_CO_MODU_CUR = ddlModalidade.SelectedValue;
            //strP_CO_CUR = ddlSerieCurso.SelectedValue;
            //strP_CO_TUR = ddlTurma.SelectedValue;  
            //lIRelatorioWeb = varRelatorioWeb.CreateChannel();
            #endregion

            string parametros, infos, deModalidde, noCur, noTurma, noCol, ano, noUnidade;
            int lRetorno, coEmp, Unidade, Colaborador, anoRef, Modalidade, SerieCur, Turma;

            coEmp = LoginAuxili.CO_EMP;
            Unidade = int.Parse(ddlUnidade.SelectedValue);
            Colaborador = int.Parse(ddlFuncionarios.SelectedValue);
            anoRef = int.Parse(ddlAnoRefer.SelectedValue);
            Modalidade = int.Parse(ddlModalidade.SelectedValue);
            SerieCur = int.Parse(ddlSerieCurso.SelectedValue);
            Turma = int.Parse(ddlTurma.SelectedValue);


            deModalidde = (Modalidade != 0 ? TB44_MODULO.RetornaPelaChavePrimaria(Modalidade).DE_MODU_CUR : "Todos");
            noCur = (SerieCur != 0 ? TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, Modalidade, SerieCur).NO_CUR : "Todos");
            noTurma = (Turma != 0 ? TB129_CADTURMAS.RetornaPelaChavePrimaria(Turma).NO_TURMA : "Todos");
            noUnidade = (Unidade != 0 ? TB25_EMPRESA.RetornaPelaChavePrimaria(Unidade).NO_FANTAS_EMP : "Todos");
            noCol = (Colaborador != 0 ? TB03_COLABOR.RetornaPelaChavePrimaria(Unidade, Colaborador).NO_COL : "Todos");
            ano = anoRef.ToString();


            parametros = "( Ano Referência: " + ano + " - Modalidade: " + deModalidde.ToUpper() + " - Série: " + noTurma.ToUpper() + " - Turma: " + noTurma.ToUpper() + " - Unidade: " + noUnidade.ToUpper() + " - Professor: " + noCol.ToUpper() + " )";

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptAtividAulaProfessorRealizada rtp = new RptAtividAulaProfessorRealizada();
            lRetorno = rtp.InitReport(parametros, coEmp, infos, Unidade, Colaborador, anoRef, Modalidade, SerieCur, Turma);
            Session["Report"] = rtp;
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
        }

        /// <summary>
        /// Método que carrega o dropdown de Funcionários
        /// </summary>
        private void CarregaFuncionarios()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serieCurso = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int coUnid = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            if (LoginAuxili.FLA_PROFESSOR == "S" && LoginAuxili.CLASSIFICACAO_USU_LOGADO != "M")
            {

                int ano = (ddlAnoRefer.SelectedValue != "" ? int.Parse(ddlAnoRefer.SelectedValue) : 0);
                var res = ddlFuncionarios.DataSource = (from tb03 in TB03_COLABOR.RetornaPelaEmpresa(coUnid)
                                                        join tbRM in TB_RESPON_MATERIA.RetornaTodosRegistros() on tb03.CO_COL equals tbRM.CO_COL_RESP
                                                        where (
                                     (modalidade != null ? tbRM.CO_MODU_CUR == modalidade : 0 == 0)
                                     && (serieCurso != 0 ? tbRM.CO_CUR == serieCurso : 0 == 0)
                                     && (turma != 0 ? tbRM.CO_TUR == turma : 0 == 0)
                                                            //&& (disciplina != 0 ? tbRM.CO_MAT == disciplina : 0 == 0)
                                     && (tb03.FLA_PROFESSOR == "S")
                                     && (ano != 0 ? tbRM.CO_ANO_REF == ano : 0 == 0)
                                     && tbRM.CO_COL_RESP == LoginAuxili.CO_COL
                                     )
                                                        select new { tb03.NO_COL, tb03.CO_COL }).Distinct().OrderBy(c => c.NO_COL);

                if (res != null)
                {
                    ddlFuncionarios.DataTextField = "NO_COL";
                    ddlFuncionarios.DataValueField = "CO_COL";
                    ddlFuncionarios.DataSource = res;
                    ddlFuncionarios.DataBind();
                }

               
            }
            else if(LoginAuxili.TIPO_USU.Equals("R")){
                 int ano = (ddlAnoRefer.SelectedValue != "" ? int.Parse(ddlAnoRefer.SelectedValue) : 0);
                var res = ddlFuncionarios.DataSource = (from tb03 in TB03_COLABOR.RetornaPelaEmpresa(coUnid)
                                                        join tbRM in TB_RESPON_MATERIA.RetornaTodosRegistros() on tb03.CO_COL equals tbRM.CO_COL_RESP
                                                        join tb48 in TB48_GRADE_ALUNO.RetornaTodosRegistros() on LoginAuxili.CO_EMP equals tb48.CO_EMP
                                                        join tb08 in TB08_MATRCUR.RetornaTodosRegistros() on LoginAuxili.CO_RESP equals tb08.TB108_RESPONSAVEL.CO_RESP
                                                        where (
                                                                 (modalidade != null ? tbRM.CO_MODU_CUR == modalidade : 0 == 0)
                                                                 &&(tb03.FLA_PROFESSOR == "S")
                                                                 && (ano != 0 ? tbRM.CO_ANO_REF == ano : 0 == 0)   
                                                                 && tb08.TB108_RESPONSAVEL.CO_RESP == LoginAuxili.CO_RESP
                                                                 && tb48.CO_ALU == tb08.CO_ALU
                                                                 && (modalidade != null ? tb48.CO_MODU_CUR == modalidade : 0 == 0) 
                                 
                                                              )
                                                        select new { tb03.NO_COL, tb03.CO_COL }).Distinct().OrderBy(c => c.NO_COL);

                if (res != null)
                {
                    ddlFuncionarios.DataTextField = "NO_COL";
                    ddlFuncionarios.DataValueField = "CO_COL";
                    ddlFuncionarios.DataSource = res;
                    ddlFuncionarios.DataBind();
                }
            }else
            {
                ddlFuncionarios.DataSource = (from tb03 in TB03_COLABOR.RetornaPelaEmpresa(coUnid)
                                              join tbRM in TB_RESPON_MATERIA.RetornaTodosRegistros() on tb03.CO_COL equals tbRM.CO_COL_RESP
                                              where (
                           (modalidade != 0 ? tbRM.CO_MODU_CUR == modalidade : 0 == 0)
                           && (serieCurso != 0 ? tbRM.CO_CUR == serieCurso : 0 == 0)
                           && (turma != 0 ? tbRM.CO_TUR == turma : 0 == 0)
                           && (tb03.FLA_PROFESSOR == "S")
                           )
                                              select new { tb03.NO_COL, tb03.CO_COL }).Distinct().OrderBy(c => c.NO_COL);

                ddlFuncionarios.DataTextField = "NO_COL";
                ddlFuncionarios.DataValueField = "CO_COL";
                ddlFuncionarios.DataBind();
            }
            if (ddlFuncionarios.Items.Count == 0)
            {
                ddlFuncionarios.Enabled = false;
                ddlFuncionarios.Items.Insert(0, new ListItem("Não Existem Professores Nestes Parâmetros", "0"));
            }
            else
            {
                ddlFuncionarios.Enabled = true;
            }
           
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
            if(LoginAuxili.TIPO_USU.Equals("R")){
                var res = (from tb44 in TB44_MODULO.RetornaTodosRegistros()
                           join tb08 in TB08_MATRCUR.RetornaTodosRegistros() on LoginAuxili.CO_RESP equals tb08.TB108_RESPONSAVEL.CO_RESP
                           join tb48 in TB48_GRADE_ALUNO.RetornaTodosRegistros() on LoginAuxili.CO_EMP equals tb48.CO_EMP
                           where tb44.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                           && tb08.TB108_RESPONSAVEL.CO_RESP == LoginAuxili.CO_RESP
                           && tb48.CO_MODU_CUR == tb44.CO_MODU_CUR
                           && tb48.CO_ALU == tb08.CO_ALU
                           select new
                           {
                               tb44.CO_MODU_CUR,
                               tb44.DE_MODU_CUR
                           }).Distinct();

                ddlModalidade.DataTextField = "DE_MODU_CUR";
                ddlModalidade.DataValueField = "CO_MODU_CUR";
                ddlModalidade.DataSource = res;
                ddlModalidade.DataBind();

                ddlModalidade.Items.Insert(0, new ListItem("Selecione", "0"));
            }
            else if (LoginAuxili.FLA_PROFESSOR != "S")
            {
                var res = (from tb44 in TB44_MODULO.RetornaTodosRegistros()
                           where tb44.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                           select new
                           {
                               tb44.CO_MODU_CUR,
                               tb44.DE_MODU_CUR
                           });

                ddlModalidade.DataTextField = "DE_MODU_CUR";
                ddlModalidade.DataValueField = "CO_MODU_CUR";
                ddlModalidade.DataSource = res;
                ddlModalidade.DataBind();

                ddlModalidade.Items.Insert(0, new ListItem("Todas", "0"));
            }
            else
            {
                int ano = (ddlAnoRefer.SelectedValue != "" ? int.Parse(ddlAnoRefer.SelectedValue) : 0);
                AuxiliCarregamentos.carregaModalidadesProfeResp(ddlModalidade, LoginAuxili.CO_COL, ano, true);
            }
            //ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where( m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            //ddlModalidade.DataTextField = "DE_MODU_CUR";
            //ddlModalidade.DataValueField = "CO_MODU_CUR";
            //ddlModalidade.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        /// <param name="coModuCur">Id da modalidade</param>
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            //   string anoGrade = ddlAnoRefer.SelectedValue;
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            //if (modalidade != 0)
            //{
            if (LoginAuxili.TIPO_USU.Equals("R"))
            {
                var res = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                           join tb08 in TB08_MATRCUR.RetornaTodosRegistros() on LoginAuxili.CO_RESP equals tb08.TB108_RESPONSAVEL.CO_RESP
                           join tb48 in TB48_GRADE_ALUNO.RetornaTodosRegistros() on LoginAuxili.CO_EMP equals tb48.CO_EMP
                           where tb01.CO_MODU_CUR == modalidade
                           && tb08.TB108_RESPONSAVEL.CO_RESP == LoginAuxili.CO_RESP
                           && tb48.CO_CUR == tb01.CO_CUR
                           && tb48.CO_ALU == tb08.CO_ALU
                           select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct();
                
                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataSource = res;
                ddlSerieCurso.DataBind();
            }
            else if (LoginAuxili.FLA_PROFESSOR != "S")
            {

                var res = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                           where tb01.CO_MODU_CUR == modalidade
                           select new { tb01.CO_CUR, tb01.NO_CUR });

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataSource = res;
                ddlSerieCurso.DataBind();
                ddlSerieCurso.Items.Insert(0, new ListItem("Todos", "0"));
            }
            else
            {
                int ano = (ddlAnoRefer.SelectedValue != "" ? int.Parse(ddlAnoRefer.SelectedValue) : 0);
                AuxiliCarregamentos.carregaSeriesCursosProfeResp(ddlSerieCurso, modalidade, LoginAuxili.CO_COL, ano, true);
            }
            //}
            //else
            //{
            //    ddlTurma.Items.Clear();
            //}

            #region desuso
            //int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            //string anoGrade = ddlAnoRefer.SelectedValue;

            //int modalidade;

            //if (!String.IsNullOrEmpty(coModuCur.ToString()))
            //    modalidade = Convert.ToInt32(coModuCur);
            //else
            //    modalidade = int.Parse(ddlModalidade.SelectedValue);

            //if (modalidade != 0)
            //{
            //    ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaTodosRegistros()
            //                                join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on tb01.CO_CUR equals tb43.CO_CUR
            //                                where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.CO_ANO_GRADE == anoGrade && tb01.CO_EMP == coEmp
            //                                select new { tb01.NO_CUR, tb01.CO_CUR }).Distinct().OrderBy( c => c.NO_CUR );

            //    ddlSerieCurso.DataTextField = "NO_CUR";
            //    ddlSerieCurso.DataValueField = "CO_CUR";
            //    ddlSerieCurso.DataBind();
            //}
            #endregion
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        /// <param name="coModuCur">Id da modalidade</param>
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            //if ((modalidade != 0) && (serie != 0))
            //{
            if (LoginAuxili.FLA_PROFESSOR != "S")
            {
                var res = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                           where tb06.CO_CUR == serie && tb06.CO_MODU_CUR == modalidade
                           select new { tb06.TB129_CADTURMAS.NO_TURMA, tb06.CO_TUR });

                ddlTurma.DataTextField = "NO_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataSource = res;
                ddlTurma.DataBind();
                ddlTurma.Items.Insert(0, new ListItem("Todas", "0"));
            }
            else
            {

              
                int ano = (ddlAnoRefer.SelectedValue != "" ? int.Parse(ddlAnoRefer.SelectedValue) : 0);
                AuxiliCarregamentos.CarregaTurmasProfeResp(ddlTurma, LoginAuxili.CO_EMP, modalidade, serie, LoginAuxili.CO_COL , ano, true);
                

            }
            //}
            //else
            //{
            //    ddlTurma.Items.Clear();
            //}

            #region desuso
            //int modalidade;
            //if (!String.IsNullOrEmpty(coModuCur.ToString()))
            //    modalidade = Convert.ToInt32(coModuCur);
            //else
            //    modalidade = int.Parse(ddlModalidade.SelectedValue);

            //int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            //if (serie != 0)
            //{
            //    ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
            //                           where tb06.CO_MODU_CUR == modalidade && tb06.CO_CUR == serie
            //                           select new { tb06.TB129_CADTURMAS.NO_TURMA, tb06.CO_TUR });

            //    ddlTurma.DataTextField = "NO_TURMA";
            //    ddlTurma.DataValueField = "CO_TUR";
            //    ddlTurma.DataBind();
            //}
            //else
            //    ddlTurma.Items.Clear();
            #endregion
        }
        #endregion

        public void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaFuncionarios();
        }

        protected void ddlAnoRefer_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaFuncionarios();
            CarregaModalidade();
            CarregaSerieCurso();
            CarregaTurma();
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaFuncionarios();
            CarregaSerieCurso();
        }

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAnos();
            CarregaFuncionarios();
            CarregaModalidade();
            CarregaSerieCurso();
            CarregaTurma();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaFuncionarios();
            CarregaTurma();
        }
    }
}
