//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE DE FREQÜÊNCIA ESCOLAR DO ALUNO
// OBJETIVO: RELAÇÃO DE FREQUÊNCIA DO ALUNO AS ATIVIDADES LETIVAS PLANEJADAS/EXECUTADAS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA      |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// -----------+----------------------------+-------------------------------------
// 16/03/2013 | André Nobre Vinagre        | Migração do relatório para .NET
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
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3400_CtrlFrequenciaEscolar.F3499_Relatorios
{
    public partial class HistoFrequeAluno : System.Web.UI.Page
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

                    CarregaAluno(tb08.TB44_MODULO.CO_MODU_CUR);
                    if (tb08.CO_ALU != 0 && ddlAluno.Items.FindByValue(tb08.CO_ALU.ToString()) != null)
                        ddlAluno.SelectedValue = tb08.CO_ALU.ToString();

                    ddlUnidade.Enabled =
                    ddlAnoRefer.Enabled =
                    ddlModalidade.Enabled =
                    ddlSerieCurso.Enabled =
                    ddlTurma.Enabled =
                    ddlAluno.Enabled = false;
                }
                else
                {
                    CarregaSerieCurso(null);
                    CarregaTurma(null);
                    CarregaAluno(null);
                }
            }
        }

//====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            if (DateTime.Parse(txtDataPeriodoFim.Text) > DateTime.Now)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Data Final da Frequência não pode ser superior a data atual.");
                return;
            }

            //--------> Variáveis obrigatórias para gerar o Relatório
            string strParametrosRelatorio, strINFOS;
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            int strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_ALU, strP_CO_ANO_REF;
            string strP_DT_INI, strP_DT_FIM, strP_NO_ALU = "";

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = int.Parse(ddlUnidade.SelectedValue);
            strP_CO_MODU_CUR = int.Parse(ddlModalidade.SelectedValue);
            strP_CO_CUR = int.Parse(ddlSerieCurso.SelectedValue);
            strP_CO_TUR = int.Parse(ddlTurma.SelectedValue);
            strP_CO_ANO_REF = int.Parse(ddlAnoRefer.SelectedValue.Trim());
            strP_CO_ALU = int.Parse(ddlAluno.SelectedValue);
            strP_NO_ALU = ddlAluno.SelectedItem.ToString();

            strP_DT_INI = txtDataPeriodoIni.Text;
            strP_DT_FIM = txtDataPeriodoFim.Text;            

            strParametrosRelatorio = "( Modalidade: " + ddlModalidade.SelectedItem.ToString() + " Série: " + ddlSerieCurso.SelectedItem.ToString() +
                " - Turma: " + TB129_CADTURMAS.RetornaPelaChavePrimaria(strP_CO_TUR).CO_SIGLA_TURMA + " - Ano Letivo: " + ddlAnoRefer.SelectedItem.ToString().Trim()
                + " - Aluno :  " + strP_NO_ALU + " )";

            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptHistoFrequeAluno rpt = new RptHistoFrequeAluno();
            lRetorno = rpt.InitReport(strParametrosRelatorio, LoginAuxili.CO_EMP, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_ANO_REF, strP_CO_TUR, strP_CO_ALU, strP_DT_INI, strP_DT_FIM, strINFOS);
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
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy( e => e.NO_FANTAS_EMP );

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
                if (LoginAuxili.TIPO_USU.Equals("R"))
                {
                    ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                           join tb48 in TB48_GRADE_ALUNO.RetornaTodosRegistros() on tb06.CO_MODU_CUR equals tb48.CO_MODU_CUR
                                           join tb08 in TB08_MATRCUR.RetornaTodosRegistros() on LoginAuxili.CO_RESP equals tb08.TB108_RESPONSAVEL.CO_RESP
                                           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb08.CO_ALU equals tb07.CO_ALU
                                           where tb06.CO_MODU_CUR == modalidade
                                           && tb06.CO_CUR == serie
                                           && tb48.CO_ALU == tb08.CO_ALU
                                           && tb07.CO_TUR == tb06.CO_TUR
                                           select new { tb06.TB129_CADTURMAS.NO_TURMA, tb06.CO_TUR }).Distinct();
                }
                else
                {
                    ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                           where tb06.CO_MODU_CUR == modalidade && tb06.CO_CUR == serie
                                           select new { tb06.TB129_CADTURMAS.NO_TURMA, tb06.CO_TUR });
                }

                ddlTurma.DataTextField = "NO_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataBind();
            }
            else
                ddlTurma.Items.Clear();
        }

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        /// <param name="codMod">Id da modalidade</param>
        private void CarregaAluno(int? codMod)
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string anoMesMat = ddlAnoRefer.SelectedValue.ToString();

            if ((serie != 0) && (turma != 0))
            {
                int modalidade;
                if (!String.IsNullOrEmpty(codMod.ToString()))
                    modalidade = Convert.ToInt32(codMod);
                else
                    modalidade = int.Parse(ddlModalidade.SelectedValue);
                if (LoginAuxili.TIPO_USU.Equals("R"))
                {
                    ddlAluno.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                           join tb48 in TB48_GRADE_ALUNO.RetornaTodosRegistros() on tb08.CO_ALU equals tb48.CO_ALU
                                           where tb08.TB25_EMPRESA.CO_EMP == coEmp
                                           && tb08.TB44_MODULO.CO_MODU_CUR == modalidade
                                           && tb08.CO_CUR == serie
                                           && tb08.CO_ANO_MES_MAT == anoMesMat
                                           && tb08.CO_TUR == turma
                                           && tb08.TB108_RESPONSAVEL.CO_RESP == LoginAuxili.CO_RESP
                                           && tb08.CO_ALU == tb48.CO_ALU
                                           select new { tb08.TB07_ALUNO.NO_ALU, tb08.TB07_ALUNO.CO_ALU }).Distinct().OrderBy(m => m.NO_ALU);
                }
                else
                {
                    ddlAluno.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                           where tb08.TB25_EMPRESA.CO_EMP == coEmp && tb08.TB44_MODULO.CO_MODU_CUR == modalidade && tb08.CO_CUR == serie
                                           && tb08.CO_ANO_MES_MAT == anoMesMat && tb08.CO_TUR == turma
                                           select new { tb08.TB07_ALUNO.NO_ALU, tb08.TB07_ALUNO.CO_ALU }).Distinct().OrderBy(m => m.NO_ALU);
                }

                ddlAluno.DataTextField = "NO_ALU";
                ddlAluno.DataValueField = "CO_ALU";
                ddlAluno.DataBind();

                ddlAluno.Items.Insert(0, new ListItem("Todos", "0"));
            }
            else
                ddlAluno.Items.Clear();
        }

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            if (LoginAuxili.TIPO_USU.Equals("R"))
            {
                int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

                ddlAnoRefer.DataSource = (from tb48 in TB48_GRADE_ALUNO.RetornaTodosRegistros()
                                          join tb08 in TB08_MATRCUR.RetornaTodosRegistros() on LoginAuxili.CO_RESP equals tb08.TB108_RESPONSAVEL.CO_RESP
                                          where tb48.TB25_EMPRESA.CO_EMP == coEmp
                                          && tb48.CO_ALU == tb08.CO_ALU
                                          select new { tb48.CO_ANO_MES_MAT }).Distinct().OrderByDescending(g => g.CO_ANO_MES_MAT);
            }
            else
            {
                int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

                ddlAnoRefer.DataSource = (from tb48 in TB48_GRADE_ALUNO.RetornaTodosRegistros()
                                          where tb48.TB25_EMPRESA.CO_EMP == coEmp
                                          select new { tb48.CO_ANO_MES_MAT }).Distinct().OrderByDescending(g => g.CO_ANO_MES_MAT);
            }

            ddlAnoRefer.DataTextField = "CO_ANO_MES_MAT";
            ddlAnoRefer.DataValueField = "CO_ANO_MES_MAT";
            ddlAnoRefer.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            if (LoginAuxili.TIPO_USU.Equals("R"))
            {
                ddlModalidade.DataSource = (from tb44 in TB44_MODULO.RetornaTodosRegistros()
                                            join tb48 in TB48_GRADE_ALUNO.RetornaTodosRegistros() on tb44.CO_MODU_CUR equals tb48.CO_MODU_CUR
                                            join tb08 in TB08_MATRCUR.RetornaTodosRegistros() on LoginAuxili.CO_RESP equals tb08.TB108_RESPONSAVEL.CO_RESP
                                            where tb48.CO_ALU == tb08.CO_ALU
                                            && tb44.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                            select new { tb44.DE_MODU_CUR, tb44.CO_MODU_CUR }
                                            ).Distinct();
            }
            else
            {
                ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where(m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);
            }
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
            if (LoginAuxili.TIPO_USU.Equals("R"))
            {
                ddlSerieCurso.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(coEmp)
                                            join tb01 in TB01_CURSO.RetornaPelaEmpresa(coEmp) on tb43.CO_CUR equals tb01.CO_CUR
                                            join tb48 in TB48_GRADE_ALUNO.RetornaTodosRegistros() on tb01.CO_CUR equals tb48.CO_CUR
                                            join tb08 in TB08_MATRCUR.RetornaTodosRegistros() on LoginAuxili.CO_RESP equals tb08.TB108_RESPONSAVEL.CO_RESP
                                            where tb43.TB44_MODULO.CO_MODU_CUR == modalidade
                                            && tb43.TB44_MODULO.CO_MODU_CUR == tb01.CO_MODU_CUR
                                            && tb43.CO_ANO_GRADE == anoGrade
                                            && tb48.CO_ALU == tb08.CO_ALU
                                            select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct().OrderBy(g => g.NO_CUR);
            }
            else
            {
                ddlSerieCurso.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(coEmp)
                                            join tb01 in TB01_CURSO.RetornaPelaEmpresa(coEmp) on tb43.CO_CUR equals tb01.CO_CUR
                                            where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.TB44_MODULO.CO_MODU_CUR == tb01.CO_MODU_CUR
                                            && tb43.CO_ANO_GRADE == anoGrade
                                            select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct().OrderBy(g => g.NO_CUR);
            }

            ddlSerieCurso.DataTextField = "NO_CUR";
            ddlSerieCurso.DataValueField = "CO_CUR";
            ddlSerieCurso.DataBind();
        }
        #endregion

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso(int.Parse(ddlModalidade.SelectedValue));
            CarregaTurma(int.Parse(ddlModalidade.SelectedValue));
            CarregaAluno(int.Parse(ddlModalidade.SelectedValue));
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma(int.Parse(ddlModalidade.SelectedValue));
            CarregaAluno(int.Parse(ddlModalidade.SelectedValue));
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAluno(int.Parse(ddlModalidade.SelectedValue));
        }

        protected void ddlUnidade_SelectedIndexChanged1(object sender, EventArgs e)
        {
            CarregaAnos();
            CarregaModalidades();
            CarregaSerieCurso(null);
            CarregaTurma(null);
            CarregaAluno(null);
        }

        protected void ddlAnoRefer_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso(int.Parse(ddlModalidade.SelectedValue));
            CarregaTurma(int.Parse(ddlModalidade.SelectedValue));
            CarregaAluno(int.Parse(ddlModalidade.SelectedValue));
        } 
    }
}
