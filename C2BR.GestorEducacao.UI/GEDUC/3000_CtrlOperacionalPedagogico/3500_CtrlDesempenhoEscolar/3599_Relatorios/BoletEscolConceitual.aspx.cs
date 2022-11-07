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
// 13/05/2013| André Nobre Vinagre        |Criada a funcionalidade
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
    public partial class BoletEscolConceitual : System.Web.UI.Page
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
                CarregaSerieCurso(null);
                CarregaTurma(null);
                CarregaAluno();
                VerificaAvaliacoesAlunos();
            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            string strP_CO_EMP, strP_CO_ALU, strP_CO_ANO_REF, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, infos, strP_OBS;

            int totFal = 0;

            //--------> Inicializa as variáveis
            strP_CO_EMP = null;
            strP_CO_MODU_CUR = null;
            strP_CO_CUR = null;
            strP_CO_TUR = null;
            strP_CO_ANO_REF = null;
            strP_CO_ALU = null;
            strP_OBS = null;
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

            int coAlu = ddlAlunos.SelectedValue != "T" ? int.Parse(ddlAlunos.SelectedValue) : 0;

            RptBoletEscolConceitual rpt = new RptBoletEscolConceitual();

            lRetorno = rpt.InitReport("", LoginAuxili.CO_EMP, infos, int.Parse(ddlUnidade.SelectedValue), strP_CO_ANO_REF, int.Parse(strP_CO_MODU_CUR), int.Parse(strP_CO_CUR), int.Parse(strP_CO_TUR), coAlu, strP_OBS, chkMostraAvalGeral.Checked, chkMostraTotFal.Checked, totFal, chk1B, chk2B, chk3B, chk4B);
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

        protected void chkMostraAvalGeral_OnCheckedChanged(object sender, EventArgs e)
        {
            //Verifica se está marcado ou não para mostrar a avaliação, caso esteja marcado, habilita a opção de o usuário escolher os bimestres que desejar.
            if (chkMostraAvalGeral.Checked == true)
            {
                chk1Bim.Enabled = chk2Bim.Enabled = chk3Bim.Enabled = chk4Bim.Enabled = true;
                VerificaAvaliacoesAlunos();
            }
            else
                chk1Bim.Enabled = chk1Bim.Checked = chk2Bim.Enabled = chk2Bim.Checked = chk3Bim.Enabled = chk3Bim.Checked = chk4Bim.Enabled = chk4Bim.Checked = false;
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso(Convert.ToInt32(ddlModalidade.SelectedValue));
            CarregaTurma(Convert.ToInt32(ddlModalidade.SelectedValue));
            CarregaAluno();
            VerificaAvaliacoesAlunos();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma(Convert.ToInt32(ddlModalidade.SelectedValue));
            CarregaAluno();
            VerificaAvaliacoesAlunos();
        }

        protected void ddlUnidade_SelectedIndexChanged1(object sender, EventArgs e)
        {
            CarregaAnos();
            CarregaModalidades();
            CarregaSerieCurso(null);
            CarregaTurma(null);
            CarregaAluno();
            VerificaAvaliacoesAlunos();
        }

        protected void ddlAnoRefer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlModalidade.Items.Count > 0)
            {
                CarregaSerieCurso(Convert.ToInt32(ddlModalidade.SelectedValue));
                CarregaTurma(Convert.ToInt32(ddlModalidade.SelectedValue));
                CarregaAluno();
                VerificaAvaliacoesAlunos();
            }
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAluno();
            VerificaAvaliacoesAlunos();
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
            }
        }

        protected void ddlAlunos_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            VerificaAvaliacoesAlunos();
        }
    }
}