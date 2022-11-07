//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: GESTÃO DE DADOS DE ALUNOS
// OBJETIVO: RELAÇÃO DE ALUNOS POR SÉRIE/TURMA
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
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
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3600_CtrlInformacoesAlunos;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3600_CtrlInformacoesAlunos._3699_Relatorios
{
    public partial class CarteiraEstudantil : System.Web.UI.Page
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
                CarregaAnos();
                CarregaUnidade();
                CarregaModalidade();
                CarregaSerie();
                CarregaTurma();
                CarregaAluno();
            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório

            int lRetorno, coEmp, coModu, coCur, coTur, coAlu = 0, coEmpLog;
            string infos, parametros, coAno, deModu, noEmp, noCur, noTur, noAlu, dtValidade = "";
            bool ckFoto, ckAno, ckModal, ckCurso, ckTurma;

            coAno = ddlAno.SelectedValue;
            coEmp = int.Parse(ddlUnidade.SelectedValue);
            coEmpLog = LoginAuxili.CO_EMP;
            coModu = int.Parse(ddlModalidade.SelectedValue);
            coCur = int.Parse(ddlSerie.SelectedValue);
            coTur = int.Parse(ddlTurma.SelectedValue);
            coAlu = int.Parse(ddlAluno.SelectedValue);
            dtValidade = txtValidade.Text;

            ckFoto = (chkFoto.Checked ? true : false);
            ckAno = (chkAnoLetivo.Checked ? true : false);
            ckModal = (chkModalidade.Checked ? true : false);
            ckCurso = (chkCurso.Checked ? true : false);
            ckTurma = (chkTurma.Checked ? true : false);

            noEmp = (coEmp != 0 ? TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp).NO_FANTAS_EMP : "TODOS");
            deModu = (coModu != 0 ? TB44_MODULO.RetornaPelaChavePrimaria(coModu).CO_SIGLA_MODU_CUR : "TODOS");
            noCur = (coCur != 0 && coModu != 0 && coEmp != 0 ? TB01_CURSO.RetornaPelaChavePrimaria(coEmp, coModu, coCur).CO_SIGL_CUR: "TODOS");
            noTur = (coTur != 0 ? TB129_CADTURMAS.RetornaPelaChavePrimaria(coTur).CO_SIGLA_TURMA : "TODOS");
            noAlu = (coAlu != 0 && coEmp != 0 ? TB07_ALUNO.RetornaPelaChavePrimaria(coAlu, coEmp).NO_ALU : "TODOS");

            RptCarteiraEstudantil rpt = new RptCarteiraEstudantil();

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            parametros = "(Ano: " + coAno + " - Unidade: " + noEmp + " - Modalidade: " + deModu + " - Curso: " + noCur + " - Turma: " + noTur + " - Aluno: " + noAlu + ")";

            lRetorno = rpt.InitReport(infos, parametros, coAno, coEmp, coEmpLog, coModu, coCur, coTur, coAlu, dtValidade, ckFoto, ckAno, ckModal, ckCurso, ckTurma);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }
        #endregion

        #region Classes Gerais

        public class ComboAnos
        {
            public string coAno { get; set; }
            public string ano
            {
                get
                {
                    return this.coAno.Trim();
                }
            }
        }

        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega os anos, que possuem matrícula, na combo ddlAno
        /// </summary>
        protected void CarregaAnos()
        {
            AuxiliCarregamentos.CarregaAno(ddlAno, LoginAuxili.CO_EMP, true);
            //var res = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
            //           select new ComboAnos
            //           {
            //               coAno = tb08.CO_ANO_MES_MAT
            //           }).Distinct().OrderByDescending(o => o.coAno);

            //ddlAno.DataTextField = "ano";
            //ddlAno.DataValueField = "ano";

            //ddlAno.DataSource = res;
            //ddlAno.DataBind();
        }

        /// <summary>
        /// Método que carrega as unidades cadastradas na combo ddlUnidade
        /// </summary>
        protected void CarregaUnidade()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
            //var res = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
            //           select new
            //           {
            //               tb25.CO_EMP,
            //               tb25.NO_FANTAS_EMP
            //           });

            //ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            //ddlUnidade.DataValueField = "CO_EMP";

            //ddlUnidade.DataSource = res;
            //ddlUnidade.DataBind();

            //ddlUnidade.Items.Insert(0, new ListItem("Todos", "0"));

            //// Seleciona, por padrão, a unidade logada
            //if (ddlUnidade.SelectedValue == "0")
            //{
            //    ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
            //}
        }

        /// <summary>
        /// Método que carrega as modalidades cadastradas na combo ddlModalidade
        /// </summary>
        protected void CarregaModalidade()
        {
            int coEmp = int.Parse(ddlUnidade.SelectedValue);
            AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
            //string sigEmp = "";

            //var res = (from tb44 in TB44_MODULO.RetornaTodosRegistros()
            //           select new
            //           {
            //               tb44.CO_MODU_CUR,
            //               tb44.DE_MODU_CUR
            //           });

            //ddlModalidade.DataTextField = "DE_MODU_CUR";
            //ddlModalidade.DataValueField = "CO_MODU_CUR";

            //ddlModalidade.DataSource = res;
            //ddlModalidade.DataBind();

            //ddlModalidade.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Método que carrega as séries cadastradas, para a modalidade selecionada, na combo ddlSerie
        /// </summary>
        protected void CarregaSerie()
        {
            int coModu = int.Parse(ddlModalidade.SelectedValue);
            int coEmp = int.Parse(ddlUnidade.SelectedValue);
            AuxiliCarregamentos.carregaSeriesCursos(ddlSerie, coModu, coEmp, true);
            //var res = (from tb01 in TB01_CURSO.RetornaTodosRegistros()
            //           where (coModu != 0 ? tb01.CO_MODU_CUR == coModu : 0 == 0)
            //           select new
            //           {
            //               tb01.CO_CUR,
            //               tb01.NO_CUR
            //           });

            //ddlSerie.DataTextField = "NO_CUR";
            //ddlSerie.DataValueField = "CO_CUR";

            //ddlSerie.DataSource = res;
            //ddlSerie.DataBind();

            //ddlSerie.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Método que carrega as turmas cadastradas, para a série selecionada, na combo ddlTurma
        /// </summary>
        protected void CarregaTurma()
        {
            int coEmp = int.Parse(ddlUnidade.SelectedValue);
            int coModu = int.Parse(ddlModalidade.SelectedValue);
            int coCur = int.Parse(ddlSerie.SelectedValue);
            AuxiliCarregamentos.CarregaTurmas(ddlTurma, coEmp, coModu, coCur, true);

            //var res = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
            //           join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tb06.CO_TUR equals tb129.CO_TUR
            //           where (coModu != 0 ? tb06.CO_MODU_CUR == coModu : 0 == 0)
            //           && (coCur != 0 ? tb06.CO_CUR == coCur : 0 == 0)
            //           select new
            //           {
            //               tb06.CO_TUR,
            //               tb129.NO_TURMA
            //           });

            //ddlTurma.DataTextField = "NO_TURMA";
            //ddlTurma.DataValueField = "CO_TUR";

            //ddlTurma.DataSource = res;
            //ddlTurma.DataBind();

            //ddlTurma.Items.Insert(0, new ListItem("Todos", "0"));
        }
        
        /// <summary>
        /// Método que carrega os alunos matriculados, na turma selecionada, na combo ddlAluno
        /// </summary>
        protected void CarregaAluno()
        {
            string coAno = ddlAno.SelectedValue;
            int coModu = int.Parse(ddlModalidade.SelectedValue);
            int coCur = int.Parse(ddlSerie.SelectedValue);
            int coTur = int.Parse(ddlTurma.SelectedValue);
            int coEmp = int.Parse(ddlUnidade.SelectedValue);
            AuxiliCarregamentos.CarregaAlunoMatriculado(ddlAluno, coEmp, coModu, coCur, coTur, coAno, true);
            //var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
            //           join tb08 in TB08_MATRCUR.RetornaTodosRegistros() on tb07.CO_ALU equals tb08.CO_ALU
            //           where (coModu != 0 ? tb08.TB44_MODULO.CO_MODU_CUR == coModu : 0 == 0)
            //           && (coCur != 0 ? tb08.CO_CUR == coCur : 0 == 0)
            //           && (coTur != 0 ? tb08.CO_TUR == coTur : 0 == 0)
            //           && tb08.CO_ANO_MES_MAT == coAno
            //           select new
            //           {
            //               tb07.NO_ALU,
            //               tb07.CO_ALU
            //           }).Distinct().OrderBy(o => o.NO_ALU);

            //ddlAluno.DataTextField = "NO_ALU";
            //ddlAluno.DataValueField = "CO_ALU";

            //ddlAluno.DataSource = res;
            //ddlAluno.DataBind();

            //ddlAluno.Items.Insert(0, new ListItem("Todos", "0"));
        }

        #endregion

        #region Funções de Campo

        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaUnidade();
            CarregaModalidade();
            CarregaSerie();
            CarregaTurma();
            CarregaAluno();
        }

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaModalidade();
            CarregaSerie();
            CarregaTurma();
            CarregaAluno();
        }

        protected void lblModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerie();
        }

        protected void ddlSerie_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAluno();
        }

        #endregion
    }
}