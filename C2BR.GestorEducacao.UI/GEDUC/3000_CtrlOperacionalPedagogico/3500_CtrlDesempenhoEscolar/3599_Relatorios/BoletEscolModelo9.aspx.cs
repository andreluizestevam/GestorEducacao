//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE DE AVALIAÇÃO ESCOLAR DO ALUNO
// OBJETIVO: BOLETIM DE DESEMPENHO ESCOLAR DO ALUNO - MODELO 9
// DATA DE CRIAÇÃO: 10/09/2014
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 10/09/2014| Maxwell Almeida            | Criação a partir do boletim Modelo 8, com a diferença de mostrar as notas de recuperação de todos os bimestres.
//           |                            |
// ----------+----------------------------+-------------------------------------

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
    public partial class BoletEscolModelo9 : System.Web.UI.Page
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
                CarregaSerieCurso();
                CarregaTurma();
                CarregaAluno();

                //Traz o título do boletim informado no cadastro da unidade
                string titu = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP).DE_TITUL_BOLET;
                txtTitulo.Text = (!string.IsNullOrEmpty(titu) ? titu : "FICHA DE RENDIMENTO ESCOLAR");
            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            string strP_CO_EMP, strP_CO_ALU, strP_CO_ANO_REF, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, infos, strP_OBS, strP_PARAMETROS, strP_TURNO, strP_TITULO;
            Boolean boolP_CO_TOT, boolP_CO_IMG;
            int totFal = 0;

            //--------> Inicializa as variáveis
            strP_CO_EMP = null;
            strP_CO_MODU_CUR = null;
            strP_CO_CUR = null;
            strP_CO_TUR = null;
            strP_CO_ANO_REF = null;
            strP_CO_ALU = null;
            strP_OBS = null;
            boolP_CO_TOT = false;
            bool chk2B, chk3B, chk4B, chk1B;
            chk1B = (chk1Bim.Checked ? true : false);
            chk2B = (chk2Bim.Checked ? true : false);
            chk3B = (chk3Bim.Checked ? true : false);
            chk4B = (chk4Bim.Checked ? true : false);

            if (txtTotFal.Text != "")
            {
                if (!int.TryParse(txtTotFal.Text, out totFal))
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Total de faltas inválido.");
                    return;
                }
            }

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

            RptBoletEscolMod9 rpt = new RptBoletEscolMod9();

            bool ImpProto = (chkImpProtocol.Checked ? true : false);
            lRetorno = rpt.InitReport(strP_PARAMETROS, LoginAuxili.CO_EMP, infos, int.Parse(ddlUnidade.SelectedValue), strP_CO_ANO_REF, int.Parse(strP_CO_MODU_CUR), int.Parse(strP_CO_CUR), int.Parse(strP_CO_TUR), coAlu, strP_OBS, boolP_CO_TOT, boolP_CO_IMG, strP_TITULO, chkImpAvalAlu.Checked, chkMostraTotFal.Checked, totFal, chk1B, chk2B, chk3B, chk4B, ImpProto);
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
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, false);
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
            AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, false);
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        /// <param name="coModuCur">Id da modalidade</param>
        private void CarregaSerieCurso()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            string anoGrade = ddlAnoRefer.SelectedValue;
            AuxiliCarregamentos.carregaSeriesGradeCurso(ddlSerieCurso, modalidade, anoGrade, coEmp, false);
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        /// <param name="coModuCur">Id da modalidade</param>
        private void CarregaTurma()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaTurmas(ddlTurma, coEmp, modalidade, serie, false);
        }

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        private void CarregaAluno()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            string anoMesMat = ddlAnoRefer.SelectedValue;
            AuxiliCarregamentos.CarregaAlunoMatriculado(ddlAlunos, coEmp, modalidade, serie, turma, anoMesMat, true, true);
        }

        /// <summary>
        /// Método responsável por percorrer uma lista criada apartir das informações preenchidas nos campos da página verificando quais os bimestres que houveram avaliações e já inteligentemente seleciona o campo do bimestre correspondente
        /// </summary>
        private void VerificaAvaliacoesAlunos()
        {
            int modu = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int curso = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int aluno = ddlAlunos.SelectedValue != "T" ? int.Parse(ddlAlunos.SelectedValue) : 0;
            string ano = ddlAnoRefer.SelectedValue;

            var res = (from tb126 in TB126_HIST_ALUNO_AVAL.RetornaTodosRegistros()
                       where (modu != 0 ? tb126.CO_MODU_CUR == modu : 0 == 0)
                       && (curso != 0 ? tb126.CO_CUR == curso : 0 == 0)
                       && (turma != 0 ? tb126.CO_TUR == turma : 0 == 0)
                       && (aluno != 0 ? tb126.CO_ALU == aluno : 0 == 0)
                       && (ano != "0" ? tb126.CO_ANO_REF == ano : 0 == 0)
                       select new { tb126.CO_BIMESTRE }).Distinct().ToList();

            chk1Bim.Checked = chk2Bim.Checked = chk3Bim.Checked = chk4Bim.Checked = false;

            //Percorre a lista acima verificando quais os bimestres que houveram avaliações e já inteligentemente seleciona o campo do bimestre correspondente
            foreach (var li in res)
            {
                switch (li.CO_BIMESTRE)
                {
                    case "B1":
                        chk1Bim.Checked = true;
                        break;
                    case "B2":
                        chk2Bim.Checked = true;
                        break;
                    case "B3":
                        chk3Bim.Checked = true;
                        break;
                    case "B4":
                        chk4Bim.Checked = true;
                        break;
                }
            }

        }

        #endregion

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            VerificaAvaliacoesAlunos();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
            VerificaAvaliacoesAlunos();
        }

        protected void ddlUnidade_SelectedIndexChanged1(object sender, EventArgs e)
        {
            CarregaAnos();
            CarregaModalidades();
            CarregaSerieCurso();
            CarregaTurma();
            CarregaAluno();
            VerificaAvaliacoesAlunos();
        }

        protected void ddlAnoRefer_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
            CarregaAluno();
            VerificaAvaliacoesAlunos();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAluno();
            VerificaAvaliacoesAlunos();
        }

        protected void chkImpAvalAlu_CheckedChanged(object sender, EventArgs e)
        {
            if (chkImpAvalAlu.Checked)
            {
                txtObservacao.Enabled = false;
                //Habilita as caixas de selecionar os bimestres referentes à avaliação
                chk1Bim.Enabled = chk2Bim.Enabled = chk3Bim.Enabled = chk4Bim.Enabled = true;
                VerificaAvaliacoesAlunos();
            }
            else
            {
                txtObservacao.Enabled = true;
                //Desabilita as caixas de selecionar os bimestres referentes à avaliação
                chk1Bim.Checked = chk1Bim.Enabled = chk2Bim.Checked = chk2Bim.Enabled = chk3Bim.Checked = chk3Bim.Enabled = chk4Bim.Checked = chk4Bim.Enabled = false;
            }
        }

        protected void chkMostraTotFal_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMostraTotFal.Checked)
            {
                txtTotFal.Enabled = true;
            }
            else
            {
                txtTotFal.Enabled = false;
                txtTotFal.Text = "";
            }
        }

        protected void ddlAlunos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAlunos.SelectedValue == "T")
            {
                chkMostraTotFal.Checked = false;
                chkMostraTotFal.Enabled = false;

                txtTotFal.Text = "";
                txtTotFal.Enabled = false;
            }
            else
            {
                chkMostraTotFal.Checked = false;
                chkMostraTotFal.Enabled = cbLinhatotal.Checked ? true : false;

                txtTotFal.Text = "";
                txtTotFal.Enabled = false;
            }
            VerificaAvaliacoesAlunos();
        }

        protected void cbLinhatotal_CheckedChanged(object sender, EventArgs e)
        {
            string coAlu = ddlAlunos.SelectedValue;

            if (cbLinhatotal.Checked)
            {
                chkMostraTotFal.Checked = false;
                chkMostraTotFal.Enabled = coAlu != "T" ? true : false;
            }
            else
            {
                chkMostraTotFal.Checked = false;
                chkMostraTotFal.Enabled = false;

                txtTotFal.Text = "";
                txtTotFal.Enabled = false;
            }
        }
    }
}