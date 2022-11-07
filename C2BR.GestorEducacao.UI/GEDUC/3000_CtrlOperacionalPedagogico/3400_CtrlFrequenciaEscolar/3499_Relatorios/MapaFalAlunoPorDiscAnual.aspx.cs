//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE DE FREQÜÊNCIA ESCOLAR DO ALUNO
// OBJETIVO: RELAÇÃO RESUMO DE FALTAS DE ALUNOS POR MATÉRIA NO ANO LETIVO
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
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3400_CtrlFrequenciaEscolar.F3499_Relatorios
{
    public partial class MapaFalAlunoPorDiscAnual : System.Web.UI.Page
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

                    CarregaSerieCurso();

                    if (tb08.CO_CUR != 0 && ddlSerieCurso.Items.FindByValue(tb08.CO_CUR.ToString()) != null)
                        ddlSerieCurso.SelectedValue = tb08.CO_CUR.ToString();

                    CarregaTurma();

                    if (tb08.CO_TUR != 0 && ddlTurma.Items.FindByValue(tb08.CO_TUR.ToString()) != null)
                        ddlTurma.SelectedValue = tb08.CO_TUR.ToString();

                    CarregaAluno();
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
                    CarregaSerieCurso();
                    CarregaTurma();
                    CarregaAluno();
                }

                VerificaMaterias();
            }
        }

//====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
//--------> Variáveis obrigatórias para gerar o Relatório
            string strIDSessao, strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, infos, strP_CO_ANO_REF;
            int lRetorno, coEmp, coUnid;

//--------> Variáveis de parâmetro do Relatório
            int strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_ALU, strP_CO_MAT;

            //var varRelatorioWeb = new ChannelFactory<IRelatorioWeb>(new NetTcpBinding(), WRAuxiliares.URLRelatorioWeb);
            //IRelatorioWeb lIRelatorioWeb;

            //strIDSessao = Session.SessionID.ToString();
            //strParametrosRelatorio;
            //strIdentFunc = WRAuxiliares.IdentFunc;
            //strCaminhoRelatorioGerado = HttpRuntime.AppDomainAppPath + "TMP_Relatorios\\" + strIDSessao + "\\";
            //strNomeRelatorio = WRAuxiliares.GeraNomeRelatorio("RelMapaAnualFaltas");

//--------> Criação da Pasta
            //if (!Directory.Exists(@strCaminhoRelatorioGerado))
            //    Directory.CreateDirectory(@strCaminhoRelatorioGerado);

//--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strParametrosRelatorio = "";

            coUnid = int.Parse(ddlUnidade.SelectedValue);
            strP_CO_MODU_CUR = int.Parse(ddlModalidade.SelectedValue);
            strP_CO_CUR = int.Parse(ddlSerieCurso.SelectedValue);
            strP_CO_TUR = int.Parse(ddlTurma.SelectedValue);
            strP_CO_ANO_REF = ddlAnoRefer.SelectedValue;
            strP_CO_ALU = int.Parse(ddlAluno.SelectedValue);
            strP_CO_MAT = ddlMateria.SelectedValue != "" ? int.Parse(ddlMateria.SelectedValue) : 0;
            coEmp = LoginAuxili.CO_EMP;
            strParametrosRelatorio += "( Ano Referência: " + ddlAnoRefer.SelectedItem.ToString();
            strParametrosRelatorio += " - Módulo: " + ddlModalidade.SelectedItem.ToString();
            strParametrosRelatorio += " - Série: " + ddlSerieCurso.SelectedItem.ToString();
            strParametrosRelatorio += " - Turma: " + ddlTurma.SelectedItem.ToString();

            if (ddlMateria.Visible == true && ddlMateria.Items.Count > 0)
                strParametrosRelatorio += " - Matéria: " + ddlMateria.SelectedItem.ToString();

            strParametrosRelatorio += ")";

            //lIRelatorioWeb = varRelatorioWeb.CreateChannel();

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptMapaFalAlunoPorDiscAnual rtp = new RptMapaFalAlunoPorDiscAnual();
            lRetorno = rtp.InitReport(strParametrosRelatorio,infos, coEmp,coUnid ,strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_ANO_REF, strP_CO_ALU, strP_CO_MAT);
            Session["Report"] = rtp;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            //lRetorno = lIRelatorioWeb.RelMapaAnualFaltas(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_ANO_REF, strP_CO_ALU, strP_CO_MAT);

            //string strURL = WRAuxiliares.RetornaCaminhoRelatorioPDF("/TMP_Relatorios/" + strIDSessao + "/") + strNomeRelatorio;
            //Session["URLRelatorio"] = strURL;
            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);

            //varRelatorioWeb.Close();
        }                     
        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaUnidades()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        /// <summary>
        /// Método que controla a visibilidade da matéria
        /// </summary>
        private void VerificaMaterias()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            if (serie != 0)
            {
                TB01_CURSO tb01 = TB01_CURSO.RetornaTodosRegistros().Where(w => w.CO_CUR == serie && w.CO_EMP == coEmp).FirstOrDefault();

                if (tb01 != null)
                {
                    if (tb01.CO_PARAM_FREQUE == "M")
                    {
                        liMateria.Visible = true;
                        CarregaMaterias();
                    }
                    else
                        liMateria.Visible = false;
                }
            }
            else
                liMateria.Visible = false;    
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        /// <param name="coModuCur">Id da modalidade</param>
        private void CarregaTurma()
        {
            int coEmp = int.Parse(ddlUnidade.SelectedValue);
            int modalidade = int.Parse(ddlModalidade.SelectedValue);
            int curso = int.Parse(ddlSerieCurso.SelectedValue);
            AuxiliCarregamentos.CarregaTurmas(ddlTurma, coEmp, modalidade, curso, true);
        }

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        /// <param name="codMod">Id da modalidade</param>
        private void CarregaAluno()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = int.Parse(ddlModalidade.SelectedValue);
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string ano = ddlAnoRefer.SelectedValue;

            AuxiliCarregamentos.CarregaAlunoMatriculado(ddlAluno, coEmp, modalidade, serie, turma, ano, true);
        }

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            AuxiliCarregamentos.CarregaAno(ddlAnoRefer, LoginAuxili.CO_EMP, true);
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        /// <param name="coModuCur">Id da modalidade</param>
        private void CarregaSerieCurso()
        {
            int modalidade = int.Parse(ddlModalidade.SelectedValue);
            AuxiliCarregamentos.carregaSeriesCursos(ddlSerieCurso, modalidade, LoginAuxili.CO_EMP, true);
        }

        /// <summary>
        /// Método que carrega o dropdown de Matérias
        /// </summary>
        private void CarregaMaterias()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            string anoGrade = ddlAnoRefer.SelectedValue;

            AuxiliCarregamentos.CarregaMateriasGradeCurso(ddlMateria, coEmp, modalidade, serie, anoGrade, true);
        }

        #endregion

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            VerificaMaterias();
            CarregaSerieCurso();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
            CarregaAluno();
            VerificaMaterias();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAluno();
        }

        protected void ddlUnidade_SelectedIndexChanged1(object sender, EventArgs e)
        {
            CarregaAnos();
            CarregaModalidades();
            CarregaSerieCurso();
            CarregaTurma();
            CarregaAluno();
            VerificaMaterias();
        }

        protected void ddlAnoRefer_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaModalidades();
            //CarregaSerieCurso();
            //CarregaTurma();
            //CarregaAluno();
            //VerificaMaterias();
        }
    }
}
