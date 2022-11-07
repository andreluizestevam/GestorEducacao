//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CADASTRO E INFORMAÇÕES DE ALUNOS
// OBJETIVO: RELAÇÃO DE INFORMAÇÕES GERAIS DO ALUNO
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
using C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2111_PreMatriculaAluno;
using C2BR.GestorEducacao.Reports.GSAUD;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2199_Relatorios
{
    public partial class FichaMatricAluno : System.Web.UI.Page
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
                txtUnidadeEscola.Text = LoginAuxili.NO_FANTAS_EMP;
                CarregaAnos();
                CarregaModalidade();
                CarregaSerieCurso();
                CarregaTurma();
                CarregaAluno();
                ddlModalidade.Enabled = false;
                ddlSerieCurso.Enabled = false;
                ddlTurma.Enabled = false;
            }
        }

        /// <summary>
        /// Processo de Geração do Relatório
        /// </summary>
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            string strP_CO_EMP;
            string strP_CO_ALU;
            string infos, parametros;
            String Situacao = ddlSituacao.SelectedValue;
            String Ano = ddlAno.SelectedValue;

            //--------> Inicializa as variáveis
            strP_CO_EMP = null;
            strP_CO_ALU = ddlAlunos.SelectedValue;

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            parametros = "";

            RptFichaMatricAluno rpt = new RptFichaMatricAluno();
            lRetorno = rpt.InitReport(parametros, LoginAuxili.CO_EMP, infos, strP_CO_EMP, strP_CO_ALU, Ano, Situacao);

            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);

        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        private void CarregaAluno()
        {
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            string Situacao = ddlSituacao.SelectedValue != "" ? ddlSituacao.SelectedValue : "0";

            string anoAtual = DateTime.Now.Year.ToString();

            ddlAlunos.Items.Clear();
            if (Situacao == "Todas")
            {
                ddlAlunos.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                        where tb08.CO_ANO_MES_MAT == anoAtual
                                        && tb08.CO_SIT_MAT == "A"
                                        select new { tb08.CO_ALU_CAD, tb08.TB07_ALUNO.NO_ALU }
                                        ).Distinct().OrderBy(m => m.NO_ALU);
                ddlAlunos.DataTextField = "NO_ALU";
                ddlAlunos.DataValueField = "CO_ALU_CAD";
                ddlAlunos.DataBind();
            }
            else if (Situacao == "NMat")
            {
                var resultado = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                 where tb08.CO_ANO_MES_MAT == anoAtual
                                 && tb08.CO_SIT_MAT == "A"
                                 select new { tb08.TB07_ALUNO.CO_ALU, tb08.TB07_ALUNO.NO_ALU }
                                );

                ddlAlunos.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                        where tb08.CO_ANO_MES_MAT == anoAtual
                                        && tb08.CO_SIT_MAT == "A"
                                        select new { tb08.CO_ALU_CAD, tb08.TB07_ALUNO.NO_ALU }
                                        ).Distinct().OrderBy(m => m.NO_ALU);
                ddlAlunos.DataTextField = "NO_ALU";
                ddlAlunos.DataValueField = "CO_ALU_CAD";
                ddlAlunos.DataBind();

            }
            else
            {
                string situacao = ddlSituacao.SelectedValue == "PMat" ? "R" : "A";
                ddlAlunos.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                        where (modalidade == 0 ? 0 == 0 : tb08.TB44_MODULO.CO_MODU_CUR == modalidade)
                                        && (serie == 0 ? 0 == 0 : tb08.CO_CUR == serie)
                                        && (turma == 0 ? 0 == 0 : tb08.CO_TUR == turma)
                                            //&& (situacao == "A" ? 0 == 0 : tb08.CO_SIT_MAT == situacao)
                                        && tb08.CO_SIT_MAT == "A"
                                        && tb08.CO_ANO_MES_MAT == anoAtual
                                        select new { tb08.CO_ALU_CAD, tb08.TB07_ALUNO.NO_ALU }).Distinct().OrderBy(m => m.NO_ALU);

                ddlAlunos.DataTextField = "NO_ALU";
                ddlAlunos.DataValueField = "CO_ALU_CAD";
                ddlAlunos.DataBind();

            }

        }

        private void CarregaAnos()
        {

            string ano = DateTime.Now.Year.ToString();


            ddlAno.DataTextField = ano;
            ddlAno.Items.Insert(0, new ListItem(ano, ano));
            ddlAno.DataBind();
        }

        private void CarregaModalidade()
        {
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where(m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();

            ddlModalidade.Items.Insert(0, new ListItem("Todas", ""));
        }

        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            if ((modalidade != 0) && (serie != 0))
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

            ddlTurma.Items.Insert(0, new ListItem("Todas", ""));
        }

        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            string anoGrade = ddlAno.SelectedValue;

            if (modalidade != 0)
            {
                var lst = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                           where tb01.CO_MODU_CUR == modalidade
                           join tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on tb01.CO_CUR equals tb43.CO_CUR
                           where tb43.CO_ANO_GRADE == anoGrade && tb43.TB44_MODULO.CO_MODU_CUR == modalidade
                           select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct().OrderBy(c => c.NO_CUR);

                ddlSerieCurso.DataSource = lst;
                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();
            }
            else
                ddlSerieCurso.Items.Clear();

            ddlSerieCurso.Items.Insert(0, new ListItem("Todas", ""));
        }
        #endregion

        #region Eventos de componentes

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            // CarregaAluno();
        }

        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
            CarregaAluno();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
            CarregaAluno();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAluno();

        }

        protected void ddlSituacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSituacao.SelectedValue == "Todas")
            {
                ddlModalidade.Enabled = false;
                ddlSerieCurso.Enabled = false;
                ddlTurma.Enabled = false;
            }
            else if (ddlSituacao.SelectedValue == "NMat")
            {
                ddlModalidade.Enabled = false;
                ddlSerieCurso.Enabled = false;
                ddlTurma.Enabled = false;
            }
            else
            {
                ddlModalidade.Enabled = true;
                ddlSerieCurso.Enabled = true;
                ddlTurma.Enabled = true;
            }

            CarregaAluno();
        }

        #endregion

    }
}
