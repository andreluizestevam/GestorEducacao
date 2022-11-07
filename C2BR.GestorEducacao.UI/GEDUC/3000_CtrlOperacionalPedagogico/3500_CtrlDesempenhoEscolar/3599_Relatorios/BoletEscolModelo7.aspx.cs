//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE DE AVALIAÇÃO ESCOLAR DO ALUNO
// OBJETIVO: BOLETIM DE DESEMPENHO ESCOLAR DO ALUNO - MODELO 3
// DATA DE CRIAÇÃO: 09/05/2013
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 07/05/2013| Victor Martins Machado     | Criação a partir do boletim Modelo 3.
//           |                            |
// ----------+----------------------------+-------------------------------------
// 12/08/2013| Victor Martins Machado     | Criada a validação do tipo de atividade de Recuperação do primeiro
//           |                            | semestre, sigla RECSE, para, se não existir, o mesmo ser criado.
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
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.DLLRelatorioWeb.Interfaces;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3500_CtrlDesempenhoEscolar;


namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3500_CtrlDesempenhoEscolar._3599_Relatorios
{
    public partial class BoletEscolModelo7 : System.Web.UI.Page
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
                CarregaModalidades();

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

                    CarregaAluno();
                    if (tb08.CO_ALU != 0 && ddlAlunos.Items.FindByValue(tb08.CO_ALU.ToString()) != null)
                        ddlAlunos.SelectedValue = tb08.CO_ALU.ToString();

                    ddlUnidade.Enabled =
                    ddlAnoRefer.Enabled =
                    ddlModalidade.Enabled =
                    ddlSerieCurso.Enabled =
                    ddlTurma.Enabled =
                    ddlAlunos.Enabled = false;
                }
                else
                {
                    CarregaSerieCurso(null);
                    CarregaTurma(null);
                    CarregaAluno();
                }
            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Verifica se existe o tipo de atividade com a sigla RECSE, se não existir, o mesmo é criado
            TB273_TIPO_ATIVIDADE tb273 = (from t in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros() where t.CO_SIGLA_ATIV == "RECSE" select t).FirstOrDefault();

            if (tb273 == null)
            {
                tb273 = new TB273_TIPO_ATIVIDADE();
                tb273.CO_CLASS_ATIV = "N";
                tb273.CO_PESO_ATIV = 1;
                tb273.CO_SIGLA_ATIV = "RECSE";
                tb273.CO_SITUA_ATIV = "A";
                tb273.DE_TIPO_ATIV = "Recuperação do 1° semestre";
                tb273.FL_LANCA_NOTA_ATIV = "S";
                tb273.NO_TIPO_ATIV = "Recuperação 1° Semestre";

                TB273_TIPO_ATIVIDADE.SaveOrUpdate(tb273, true);
            }

            //--------> Variáveis obrigatórias para gerar o Relatório
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            string strP_CO_EMP, strP_CO_ALU, strP_CO_ANO_REF, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, infos, strP_OBS, strP_PARAMETROS, strP_TURNO, strP_TITULO;
            Boolean boolP_CO_TOT, boolP_CO_IMG;

            //--------> Inicializa as variáveis
            strP_CO_EMP = null;
            strP_CO_MODU_CUR = null;
            strP_CO_CUR = null;
            strP_CO_TUR = null;
            strP_CO_ANO_REF = null;
            strP_CO_ALU = null;
            strP_OBS = null;
            boolP_CO_TOT = false;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = ddlUnidade.SelectedValue;
            strP_CO_MODU_CUR = ddlModalidade.SelectedValue;
            strP_CO_CUR = ddlSerieCurso.SelectedValue;
            strP_CO_TUR = ddlTurma.SelectedValue;
            strP_CO_ANO_REF = ddlAnoRefer.SelectedValue;
            strP_CO_ALU = ddlAlunos.SelectedValue;
            strP_OBS = txtObservacao.Text;
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            boolP_CO_TOT = cbLinhatotal.Checked;
            boolP_CO_IMG = cblImagem.Checked;
            strP_TURNO = "";
            strP_TITULO = txtTitulo.Text;

            switch (TB06_TURMAS.RetornaPelaChavePrimaria(int.Parse(strP_CO_EMP), int.Parse(strP_CO_MODU_CUR), int.Parse(strP_CO_CUR), int.Parse(strP_CO_TUR)).CO_PERI_TUR)
            {
                case "M":
                    strP_TURNO = "Matutino";
                    break;
                case "V":
                    strP_TURNO = "Vespertino";
                    break;
                case "N":
                    strP_TURNO = "Noturno";
                    break;
            }

            int coAlu = ddlAlunos.SelectedValue != "T" ? int.Parse(ddlAlunos.SelectedValue) : 0;

            strP_PARAMETROS = "Ano Letivo: " + strP_CO_ANO_REF.Trim() +
                              " - " + TB44_MODULO.RetornaPelaChavePrimaria(int.Parse(strP_CO_MODU_CUR)).DE_MODU_CUR +
                              " - " + TB01_CURSO.RetornaPelaChavePrimaria(int.Parse(strP_CO_EMP), int.Parse(strP_CO_MODU_CUR), int.Parse(strP_CO_CUR)).NO_CUR +
                              " - Turma " + TB129_CADTURMAS.RetornaPelaChavePrimaria(int.Parse(strP_CO_TUR)).NO_TURMA +
                              " - Turno " + strP_TURNO;

            RptBoletEscolMod7 rpt = new RptBoletEscolMod7();

            lRetorno = rpt.InitReport(strP_PARAMETROS, LoginAuxili.CO_EMP, infos, int.Parse(ddlUnidade.SelectedValue), strP_CO_ANO_REF, int.Parse(strP_CO_MODU_CUR), int.Parse(strP_CO_CUR), int.Parse(strP_CO_TUR), coAlu, strP_OBS, boolP_CO_TOT, boolP_CO_IMG, strP_TITULO);
            Session["Report"] = rpt;
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
        /// Método que carrega o dropdown de Turmas
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
                ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                       where tb06.CO_MODU_CUR == modalidade && tb06.CO_CUR == serie
                                       select new { tb06.TB129_CADTURMAS.NO_TURMA, tb06.CO_TUR });

                ddlTurma.DataTextField = "NO_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataBind();
            }
            else
                ddlTurma.Items.Clear();
        }

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            ddlAnoRefer.DataSource = (from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                                      where tb079.TB25_EMPRESA.CO_EMP == coEmp
                                      select new { tb079.CO_ANO_REF }).Distinct().OrderByDescending(h => h.CO_ANO_REF);

            ddlAnoRefer.DataTextField = "CO_ANO_REF";
            ddlAnoRefer.DataValueField = "CO_ANO_REF";
            ddlAnoRefer.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where(m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
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
                ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaTodosRegistros()
                                            join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on tb01.CO_CUR equals tb43.CO_CUR
                                            where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.CO_ANO_GRADE == anoGrade && tb01.CO_EMP == coEmp
                                            select new { tb01.NO_CUR, tb01.CO_CUR }).Distinct().OrderBy(c => c.NO_CUR);

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        private void CarregaAluno()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string anoMesMat = ddlAnoRefer.SelectedValue;

            ddlAlunos.Items.Clear();

            if (turma != 0)
            {
                ddlAlunos.DataSource = (from tb48 in TB48_GRADE_ALUNO.RetornaTodosRegistros()
                                        where tb48.TB44_MODULO.CO_MODU_CUR == modalidade && tb48.CO_CUR == serie
                                        && tb48.CO_ANO_MES_MAT == anoMesMat && tb48.CO_TUR == turma
                                        select new { tb48.TB07_ALUNO.CO_ALU, tb48.TB07_ALUNO.NO_ALU }).Distinct().OrderBy(g => g.NO_ALU);

                ddlAlunos.DataTextField = "NO_ALU";
                ddlAlunos.DataValueField = "CO_ALU";
                ddlAlunos.DataBind();
            }

            ddlAlunos.Items.Insert(0, new ListItem("Todos", "T"));
        }
        #endregion

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso(Convert.ToInt32(ddlModalidade.SelectedValue));
            CarregaTurma(Convert.ToInt32(ddlModalidade.SelectedValue));
            CarregaAluno();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma(Convert.ToInt32(ddlModalidade.SelectedValue));
            CarregaAluno();
        }

        protected void ddlUnidade_SelectedIndexChanged1(object sender, EventArgs e)
        {
            CarregaAnos();
            CarregaModalidades();
            CarregaSerieCurso(null);
            CarregaTurma(null);
            CarregaAluno();
        }

        protected void ddlAnoRefer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlModalidade.Items.Count > 0)
            {
                CarregaSerieCurso(Convert.ToInt32(ddlModalidade.SelectedValue));
                CarregaTurma(Convert.ToInt32(ddlModalidade.SelectedValue));
                CarregaAluno();
            }
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAluno();
        }
    }
}