//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE LETIVO DE AULAS E ATIVIDADES
// OBJETIVO: PLANEJAMENTO LETIVO DE ATIVIDADES DE MATÉRIAS POR TURMA
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 12/04/2013| André Nobre Vinagre        | - Adicionado ao layout do relatório os campos objetivo
//           |                            | e metodologia abaixo da data
//           |                            |
// ----------+----------------------------+-------------------------------------
// 16/04/2013| André Nobre Vinagre        | - Corrigida inconsistencias na tela de parametros ao selecionar 
//           |                            | tipo de atividade e quando coloco um intervalo de data

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
    public partial class PlanoAulaPorSerieTurma : System.Web.UI.Page
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
                CarregaUnidades();
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

                    CarregaSerieCurso(tb08.TB44_MODULO.CO_MODU_CUR);

                    if (tb08.CO_CUR != 0 && ddlSerieCurso.Items.FindByValue(tb08.CO_CUR.ToString()) != null)
                        ddlSerieCurso.SelectedValue = tb08.CO_CUR.ToString();

                    CarregaTurma(tb08.TB44_MODULO.CO_MODU_CUR);

                    if (tb08.CO_TUR != 0 && ddlTurma.Items.FindByValue(tb08.CO_TUR.ToString()) != null)
                        ddlTurma.SelectedValue = tb08.CO_TUR.ToString();

                    CarregaProfessores(tb08.TB44_MODULO.CO_MODU_CUR);
                    CarregaMaterias(tb08.TB44_MODULO.CO_MODU_CUR);

                    ddlUnidade.Enabled =
                    ddlAnoRefer.Enabled =
                    ddlModalidade.Enabled =
                    ddlSerieCurso.Enabled =
                    ddlTurma.Enabled = false;
                }
                else
                {
                    CarregaSerieCurso(null);
                    CarregaTurma(null);
                    CarregaProfessores(null);
                    CarregaMaterias(null);

                    ddlSerieCurso.Items.Insert(0, new ListItem("Todos", "0"));
                    ddlTurma.Items.Insert(0, new ListItem("Todos", "0"));
                    ddlProfResp.Items.Insert(0, new ListItem("Todos", "0"));
                    ddlMateria.Items.Insert(0, new ListItem("Todos", "0"));
                }
            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            string strInfos, strParametrosRelatorio;
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            string strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_ID_MATERIA, strP_DT_INI, strP_DT_FIM, strP_TP_ATIV, strP_CO_COL, strP_SERIE, strP_TURMA;

            //--------> Inicializa as variáveis
            strP_CO_EMP = null;
            strP_CO_MODU_CUR = null;
            strP_CO_CUR = null;
            strP_CO_TUR = null;
            strP_ID_MATERIA = null;
            strP_DT_INI = null;
            strP_DT_FIM = null;
            strP_TP_ATIV = null;
            strP_CO_COL = null;
            strP_SERIE = null;
            strP_TURMA = null;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = ddlUnidade.SelectedValue;
            strP_CO_MODU_CUR = ddlModalidade.SelectedValue;
            strP_CO_CUR = ddlSerieCurso.SelectedValue;
            strP_CO_TUR = ddlTurma.SelectedValue;
            //int CO_TUR = int.Parse(strP_CO_TUR);
            strP_ID_MATERIA = ddlMateria.SelectedValue;
            strP_DT_INI = txtDataPeriodoIni.Text;
            strP_DT_FIM = txtDataPeriodoFim.Text;
            strP_TP_ATIV = ddlTpAtividade.SelectedValue;
            strP_CO_COL = ddlProfResp.SelectedValue;
            strP_SERIE = ddlSerieCurso.SelectedItem.ToString();
            string strP_CUR_TUR = ddlTurma.SelectedItem.ToString();
            strP_TURMA = ddlTurma.SelectedItem.Text;
            strInfos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            strParametrosRelatorio = "( Módulo: " + ddlModalidade.SelectedItem.ToString() + " - Série: " + ddlSerieCurso.SelectedItem.ToString() + " - Turma: " + ddlTurma.SelectedItem.ToString() +
            " - Período de: " + txtDataPeriodoIni.Text + " à " + txtDataPeriodoFim.Text + " - Atividades: " + ddlTpAtividade.SelectedItem + " )";

            RptPlanoAulaSerieTurma rtp = new RptPlanoAulaSerieTurma();
            lRetorno = rtp.InitReport(strParametrosRelatorio, LoginAuxili.CO_EMP, strInfos, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_ID_MATERIA, strP_DT_INI, strP_DT_FIM, strP_TP_ATIV, strP_CO_COL, strP_TURMA, strP_SERIE);
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
        /// Método que carrega o dropdown de Anos de Referência.
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
        /// Método que carrega o dropdown de Modalidades, verifica se o usuário logado é professor.
        /// </summary>
        private void CarregaModalidade()
        {
            if (LoginAuxili.FLA_PROFESSOR != "S")
            {
                ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where(m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

                ddlModalidade.DataTextField = "DE_MODU_CUR";
                ddlModalidade.DataValueField = "CO_MODU_CUR";
                ddlModalidade.DataBind();
                ddlModalidade.Items.Insert(0, new ListItem("Todos", "0"));
            }
            else
            {
                int ano = (ddlAnoRefer.SelectedValue != "" ? int.Parse(ddlAnoRefer.SelectedValue) : 0);
                AuxiliCarregamentos.carregaModalidadesProfeResp(ddlModalidade, LoginAuxili.CO_COL, ano, true);
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries, verifica se o usuário logado é professor.
        /// </summary>
        /// <param name="coModuCur">Id da modalidade</param>
        private void CarregaSerieCurso(int? coModuCur)
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            string anoGrade = ddlAnoRefer.SelectedValue;

            int modalidade;

            if (!String.IsNullOrEmpty(coModuCur.ToString()))
                modalidade = Convert.ToInt32(coModuCur);
            else
                modalidade = int.Parse(ddlModalidade.SelectedValue);

            if (modalidade != 0)
            {
                if (LoginAuxili.FLA_PROFESSOR != "S")
                {
                    ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaTodosRegistros()
                                                join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on tb01.CO_CUR equals tb43.CO_CUR
                                                where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.CO_ANO_GRADE == anoGrade && tb01.CO_EMP == coEmp
                                                select new { tb01.NO_CUR, tb01.CO_CUR }).Distinct().OrderBy(c => c.NO_CUR);

                    ddlSerieCurso.DataTextField = "NO_CUR";
                    ddlSerieCurso.DataValueField = "CO_CUR";
                    ddlSerieCurso.DataBind();
                    ddlSerieCurso.Items.Insert(0, new ListItem("Todos", "0"));
                }
                else
                {
                    int ano = (ddlAnoRefer.SelectedValue != "" ? int.Parse(ddlAnoRefer.SelectedValue) : 0);
                    AuxiliCarregamentos.carregaSeriesCursosProfeResp(ddlSerieCurso, modalidade, LoginAuxili.CO_COL, ano, true);
                }
            }
            else
            {
                ddlSerieCurso.Items.Insert(0, new ListItem("Todos", "0"));
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas, verifica se o usuário logado é professor.
        /// </summary>
        /// <param name="coModuCur">Id da modalidade</param>
        private void CarregaTurma(int? coModuCur)
        {
            int modalidade;
            if (!String.IsNullOrEmpty(coModuCur.ToString()))
                modalidade = Convert.ToInt32(coModuCur);
            else
                modalidade = int.Parse(ddlModalidade.SelectedValue);

            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            if (serie != 0)
            {
                if (LoginAuxili.FLA_PROFESSOR != "S")
                {
                    ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                           where tb06.CO_MODU_CUR == modalidade && tb06.CO_CUR == serie
                                           select new { tb06.TB129_CADTURMAS.NO_TURMA, tb06.CO_TUR });
                    ddlTurma.DataTextField = "NO_TURMA";
                    ddlTurma.DataValueField = "CO_TUR";
                    ddlTurma.DataBind();
                    ddlTurma.Items.Insert(0, new ListItem("Todos", "0"));
                }
                else
                {
                    int ano = (ddlAnoRefer.SelectedValue != "" ? int.Parse(ddlAnoRefer.SelectedValue) : 0);
                    ddlTurma.Items.Clear();
                    AuxiliCarregamentos.CarregaTurmasProfeResp(ddlTurma, LoginAuxili.CO_EMP, modalidade, serie, LoginAuxili.CO_COL, ano, true);
                }
            }
            else
            {
                ddlTurma.Items.Clear();
                ddlTurma.Items.Insert(0, new ListItem("Todos", "0"));
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Disciplinas, verifica se o usuário logado é professor.
        /// </summary>
        /// <param name="codMod">Id da modalidade</param>
        private void CarregaMaterias(int? codMod)
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int coAnoRefPla = ddlAnoRefer.SelectedValue != "" ? int.Parse(ddlAnoRefer.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int coCol = ddlProfResp.SelectedValue != "" ? int.Parse(ddlProfResp.SelectedValue) : 0;

            //ddlMateria.Items.Clear();

            if (ddlTurma.Items.Count > 0)
            {
                if (LoginAuxili.FLA_PROFESSOR != "S")
                {
                    ddlMateria.DataSource = (from tb17 in TB17_PLANO_AULA.RetornaTodosRegistros()
                                             where tb17.CO_MODU_CUR == modalidade && tb17.CO_CUR == serie && tb17.CO_EMP == coEmp
                                             && tb17.CO_ANO_REF_PLA == coAnoRefPla && tb17.CO_COL == coCol
                                             join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb17.CO_MAT equals tb02.CO_MAT
                                             join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                             select new { tb02.ID_MATERIA, tb107.NO_MATERIA }).Distinct().OrderBy(p => p.NO_MATERIA);

                    ddlMateria.DataTextField = "NO_MATERIA";
                    ddlMateria.DataValueField = "ID_MATERIA";
                    ddlMateria.DataBind();
                    ddlMateria.Items.Insert(0, new ListItem("Todos", "0"));
                }
                else
                {
                    int ano = (ddlAnoRefer.SelectedValue != "" ? int.Parse(ddlAnoRefer.SelectedValue) : 0);
                    AuxiliCarregamentos.CarregaMateriasProfeRespon(ddlMateria, LoginAuxili.CO_COL, modalidade, serie, ano, true);
                }
            }
            else
            {
                ddlMateria.Items.Insert(0, new ListItem("Todos", "0"));
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Professores, verifica se o usuário logado é professor.
        /// </summary>
        /// <param name="codMod">Id da modalidade</param>
        private void CarregaProfessores(int? codMod)
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int coAnoRefPla = ddlAnoRefer.SelectedValue != "" ? int.Parse(ddlAnoRefer.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int coUnid = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int materia = ddlMateria.SelectedValue != "" ? int.Parse(ddlMateria.SelectedValue) : 0;

            ddlProfResp.Items.Clear();

            if (serie != 0)
            {
                if (LoginAuxili.FLA_PROFESSOR == "S" && LoginAuxili.CLASSIFICACAO_USU_LOGADO != "M")
                {
                    int ano = (ddlAnoRefer.SelectedValue != "" ? int.Parse(ddlAnoRefer.SelectedValue) : 0);
                    var res = ddlProfResp.DataSource = (from tb03 in TB03_COLABOR.RetornaPelaEmpresa(coUnid)
                                                        join tbRM in TB_RESPON_MATERIA.RetornaTodosRegistros() on tb03.CO_COL equals tbRM.CO_COL_RESP
                                                        where (
                                     (modalidade != null ? tbRM.CO_MODU_CUR == modalidade : 0 == 0)
                                     && (serie != 0 ? tbRM.CO_CUR == serie : 0 == 0)
                                     && (turma != 0 ? tbRM.CO_TUR == turma : 0 == 0)
                                                            //&& (disciplina != 0 ? tbRM.CO_MAT == disciplina : 0 == 0)
                                     && (tb03.FLA_PROFESSOR == "S")
                                     && (ano != 0 ? tbRM.CO_ANO_REF == ano : 0 == 0)
                                     && tbRM.CO_COL_RESP == LoginAuxili.CO_COL
                                     )
                                                        select new { tb03.NO_COL, tb03.CO_COL }).Distinct().OrderBy(c => c.NO_COL);

                    if (res != null)
                    {
                        ddlProfResp.DataTextField = "NO_COL";
                        ddlProfResp.DataValueField = "CO_COL";
                        ddlProfResp.DataSource = res;
                        ddlProfResp.DataBind();
                    }
                }
                else
                {
                    ddlProfResp.DataSource = (from tb17 in TB17_PLANO_AULA.RetornaTodosRegistros()
                                              join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tb17.CO_COL equals tb03.CO_COL
                                              where tb17.CO_MODU_CUR == modalidade && tb17.CO_CUR == serie && tb17.CO_EMP == coEmp && tb17.CO_ANO_REF_PLA == coAnoRefPla
                                              select new { tb03.CO_COL, tb03.NO_COL }).Distinct().OrderBy(p => p.NO_COL);

                    ddlProfResp.DataTextField = "NO_COL";
                    ddlProfResp.DataValueField = "CO_COL";
                    ddlProfResp.DataBind();
                    ddlProfResp.Items.Insert(0, new ListItem("Todos", "0"));
                }
                if (ddlProfResp.Items.Count == 0)
                {
                    ddlProfResp.Enabled = false;
                    ddlProfResp.Items.Insert(0, new ListItem("Não Existem Professores Nestes Parâmetros", "0"));
                }
                else
                {
                    ddlProfResp.Enabled = true;
                }
            }
            else
            {
                ddlProfResp.Items.Insert(0, new ListItem("Todos", "0"));
            }
        }
        #endregion

        protected void ddlAnoRefer_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaModalidade();
            CarregaSerieCurso(int.Parse(ddlModalidade.SelectedValue));
            CarregaTurma(int.Parse(ddlModalidade.SelectedValue));
            CarregaProfessores(int.Parse(ddlModalidade.SelectedValue));
            CarregaMaterias(int.Parse(ddlModalidade.SelectedValue));
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso(int.Parse(ddlModalidade.SelectedValue));
            CarregaTurma(int.Parse(ddlModalidade.SelectedValue));
            CarregaProfessores(int.Parse(ddlModalidade.SelectedValue));
            CarregaMaterias(int.Parse(ddlModalidade.SelectedValue));
        }

        protected void ddlUnidade_SelectedIndexChanged1(object sender, EventArgs e)
        {
            CarregaAnos();
            CarregaModalidade();
            CarregaSerieCurso(null);
            CarregaTurma(null);
            CarregaProfessores(null);
            CarregaMaterias(null);
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma(int.Parse(ddlModalidade.SelectedValue));
            CarregaProfessores(int.Parse(ddlModalidade.SelectedValue));
            CarregaMaterias(int.Parse(ddlModalidade.SelectedValue));
        }

        protected void ddlProfResp_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaMaterias(int.Parse(ddlModalidade.SelectedValue));
        }
    }
}
