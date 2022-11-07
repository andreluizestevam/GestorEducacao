//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: ITENS SECRETARIA
// OBJETIVO: DECLARAÇÃO DE TRANSFERENCIA
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//29/03/2013|Caio Mendonça               | colocar validação da existencia do rtf




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
using C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2101_ServSolicItensServicEscolar;
namespace C2BR.GestorEducacao.UI.GEDUC._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2112_ContrDeDeclaracoes
{
    public partial class DeclaracaoComparecimentoInsef : System.Web.UI.Page
    {
        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }
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
                //CarregaUnidades();
                CarregaModalidade();
                CarregaSerieCurso();
                CarregaTurma();
                
                ddlModalidade.Enabled = true;
                ddlSerieCurso.Enabled = true;
                ddlTurma.Enabled = true;
                CarregaAnos();
                CarregaAluno();
            }
            
        }
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            int lRetorno;


            string strCO_ALU_CAD;

            strCO_ALU_CAD = ddlAlunos.SelectedValue;

            var lst = (from tb009 in TB009_RTF_DOCTOS.RetornaTodosRegistros()
                           from tb010 in tb009.TB010_RTF_ARQUIVO.DefaultIfEmpty()
                           where tb009.TP_DOCUM == "DE" && tb009.CO_SIGLA_DOCUM == "DINS001COMPA"
                           //b009.ID_DOCUM && tb009.TP_DOCUM == "DE" && tb009.CO_SITUS_DOCUM == "A"
                           //where tb009.ID_DOCUM == 6 
                           //&& tb009.TP_DOCUM == "CC" && tb009.CO_SITUS_DOCUM == "A"
                           //tb009.ID_DOCUM == dados.AlunoContratoCodigo //
                           //&& tb009.TB25_EMPRESA.CO_EMP == codEmp
                           select new
                           {
                               Pagina = tb010.NU_PAGINA,
                               Titulo = tb009.NM_TITUL_DOCUM,
                               Texto = tb010.AR_DADOS
                           }).OrderBy(x => x.Pagina);

            string DataTemp = txtDataTemp.Text;
            string Periodo = ddlPeriodo.SelectedValue;
            string Descricao = txtDescricao.Text;

            if (lst != null && lst.Where(x => x.Pagina == 1).Any())
            {
                RptDeclaracaoComparecimentoInsef rpt = new RptDeclaracaoComparecimentoInsef();
                lRetorno = rpt.InitReport(LoginAuxili.CO_EMP, strCO_ALU_CAD, DataTemp, Periodo, Descricao);
                Session["Report"] = rpt;
                Session["URLRelatorio"] = "/GeducReportViewer.aspx";

                AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Arquivo não cadastrado na tabela de Arquivo RTF - Sigla DINS001COMPA, Tipo de documento Declaração");
                return;
            }
        }

       #region Carregamento DropDown

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
                ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                            where tb01.CO_MODU_CUR == modalidade
                                            join tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on tb01.CO_CUR equals tb43.CO_CUR
                                            where tb43.CO_ANO_GRADE == anoGrade && tb43.TB44_MODULO.CO_MODU_CUR == modalidade
                                            select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct().OrderBy(c => c.NO_CUR);

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();
            }
            else
                ddlSerieCurso.Items.Clear();

            ddlSerieCurso.Items.Insert(0, new ListItem("Todas", ""));
        }

        private void CarregaAnos()
        {
            //int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

           ddlAno.DataSource = (from tb48 in TB48_GRADE_ALUNO.RetornaTodosRegistros()
                                select new { tb48.CO_ANO_MES_MAT }).Distinct().OrderByDescending(g => g.CO_ANO_MES_MAT);

            ddlAno.DataTextField = "CO_ANO_MES_MAT";
            ddlAno.DataValueField = "CO_ANO_MES_MAT";
            ddlAno.DataBind();
            

            //ddlModalidade.Items.Insert(0, new ListItem("Todas", ""));

        }

        private void CarregaAluno()
        {
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            //int unidade = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            //string coAnoMesMat = dd.SelectedValue != "" ? ddlAno.SelectedValue : "0";
            //string Situacao = ddlSituacao.SelectedValue != "" ? ddlSituacao.SelectedValue : "0";
            string Ano = ddlAno.SelectedValue;
            ddlAlunos.Items.Clear();


            if (modalidade == 0 && (serie == 0 && turma == 0))
            {
                ddlAlunos.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                        where tb08.CO_ANO_MES_MAT == Ano
                                        select new { tb08.TB07_ALUNO.CO_ALU, tb08.TB07_ALUNO.NO_ALU }).Distinct().OrderBy(m => m.NO_ALU);

                ddlAlunos.DataTextField = "NO_ALU";
                ddlAlunos.DataValueField = "CO_ALU";
                ddlAlunos.DataBind();
            }
            else if (modalidade != 0 && (serie == 0 && turma == 0))
            {
                ddlAlunos.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                        where (tb08.TB44_MODULO.CO_MODU_CUR == modalidade && tb08.CO_ANO_MES_MAT == Ano)
                                        select new { tb08.TB07_ALUNO.CO_ALU, tb08.TB07_ALUNO.NO_ALU }).Distinct().OrderBy(m => m.NO_ALU);

                ddlAlunos.DataTextField = "NO_ALU";
                ddlAlunos.DataValueField = "CO_ALU";
                ddlAlunos.DataBind();
            }
            else if (serie != 0 && turma == 0)
            {
                ddlAlunos.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                        where (tb08.CO_CUR == serie &&  tb08.CO_ANO_MES_MAT == Ano)
                                        select new { tb08.TB07_ALUNO.CO_ALU, tb08.TB07_ALUNO.NO_ALU }).Distinct().OrderBy(m => m.NO_ALU);

                ddlAlunos.DataTextField = "NO_ALU";
                ddlAlunos.DataValueField = "CO_ALU";
                ddlAlunos.DataBind();
            }
            else if (turma != 0)
            {
                ddlAlunos.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                        where (tb08.CO_TUR == turma && tb08.CO_ANO_MES_MAT == Ano)
                                        select new { tb08.TB07_ALUNO.CO_ALU, tb08.TB07_ALUNO.NO_ALU }).Distinct().OrderBy(m => m.NO_ALU);

                ddlAlunos.DataTextField = "NO_ALU";
                ddlAlunos.DataValueField = "CO_ALU";
                ddlAlunos.DataBind();
            }

        }

        /*private void CarregaUnidades()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                     where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy(e => e.NO_FANTAS_EMP);

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";

            ddlUnidade.DataBind();
            ddlUnidade.Items.Insert(0, new ListItem("Todas", ""));
            //ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
            CarregaAnos();
        }*/

        #endregion

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            // CarregaAluno();
            CarregaAnos();
        }

        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAluno();
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
    }
}