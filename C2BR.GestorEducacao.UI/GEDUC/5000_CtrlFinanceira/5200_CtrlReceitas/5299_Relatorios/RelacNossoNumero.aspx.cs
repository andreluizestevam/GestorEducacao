//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: GESTÃO DE DADOS DE ALUNOS
// OBJETIVO: RELAÇÃO DE ALUNOS - ANIVERSARIANTES
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//25/04/2014    Maxwell Almeida             Criação da página e funcionalidade de emissão da relação de nossos números e os alunos relacionados à eles.

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
using C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5200_CtrlReceitas;

namespace C2BR.GestorEducacao.UI.GEDUC._5000_CtrlFinanceira._5200_CtrlReceitas._5299_Relatorios
{
    public partial class RelacNossoNumero : System.Web.UI.Page
    {
        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //--------> Criação das instâncias utilizadas
            PadraoRelatoriosCorrente.OnAcaoGeraRelatorio += new PadraoRelatorios.OnAcaoGeraRelatorioHandler(PadraoRelatoriosCorrente_OnAcaoGeraRelatorio);
        }

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                carregaUnidades();
                carregaModalidade();
                carregaSerieCurso();
                carregaTurma();
                carregaAno();
                carregaAluno();
            }
        }

        #endregion

        #region Carregamentos

        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {

//==========> Declara as variáveis necessárias para o correto funcionamento da página
            string parametros;
            string infos;
            int coEmp, coUnid, coModalidade, coCurso, coTurma, coAlu, lRetorno;
            string dataIni, dataFim, deModalidde, noCur, noTurma, noUnidade, noAlu, Periodo, txtnossNmer;
            DateTime? dtIni, dtFim;
            bool chkPesqNu = (chkPesqNossNum.Checked ? true : false);

            dtIni = (txtIniPeri.Text != "" ? DateTime.Parse(txtIniPeri.Text) : (DateTime?)null);
            dtFim = (TxtFimPeri.Text != "" ? DateTime.Parse(TxtFimPeri.Text) : (DateTime?)null);

//==========> Atribui as informações que o usuário inseriu na tela às funcionalidades criadas acima.
            coEmp = LoginAuxili.CO_EMP;
            coCurso = (ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0);
            coTurma = (ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0);
            coModalidade = (ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0);
            coUnid = (ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0);
            coAlu = (ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0);
            txtnossNmer = txtNuNossNu.Text;

//==========> Coleta os nomes e descrições para concatenação e preenchimento da linha superior de parâmetro do relatório
            deModalidde = (coModalidade != 0 ? TB44_MODULO.RetornaPelaChavePrimaria(coModalidade).DE_MODU_CUR : "Todos");
            noCur = (coCurso != 0 ? TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, coModalidade, coCurso).NO_CUR : "Todos");
            noTurma = (coTurma != 0 ? TB129_CADTURMAS.RetornaPelaChavePrimaria(coTurma).NO_TURMA : "Todos"); //TB06_TURMAS.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serieCurso, Turma).TB129_CADTURMAS.NO_TURMA : "Todos");
            noUnidade = (coUnid != 0 ? TB25_EMPRESA.RetornaPelaChavePrimaria(coUnid).NO_FANTAS_EMP : "Todos");
            noAlu = (coAlu != 0 ? TB07_ALUNO.RetornaPelaChavePrimaria(coAlu, coUnid).NO_ALU : "Todos");
            Periodo = txtIniPeri.Text + " à " + TxtFimPeri.Text;

//==========> Concate as informações inseridas na tela em uma variável só
            parametros = "( Unidade: " +   " - Modalidade: " + deModalidde.ToUpper() + " - Curso: " + noCur.ToUpper() + " - Turma: " + noTurma.ToUpper() + " - Aluno: " + noAlu.ToUpper() + " - Período: " + Periodo.ToUpper() + " )";
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptRelacNossoNumero fpcb = new RptRelacNossoNumero();
            lRetorno = fpcb.InitReport(parametros, infos, coEmp, coUnid, coModalidade, coCurso, coTurma, coAlu, dtIni.Value, dtFim.Value, chkPesqNu, txtnossNmer);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        /// <summary>
        /// Carrega os Anos das matrículas
        /// </summary>
        private void carregaAno()
        {
            AuxiliCarregamentos.CarregaAno(ddlAno, LoginAuxili.CO_EMP, false);
        }

        /// <summary>
        /// Carrega as unidades de acordo com o código da instituição logada.
        /// </summary>
        private void carregaUnidades()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        /// <summary>
        /// Carrega as Modalidades
        /// </summary>
        private void carregaModalidade()
        {
            AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        /// <summary>
        /// Carrega a Série e Curso de acordo com a modalidade informada.
        /// </summary>
        private void carregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int unidade = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            AuxiliCarregamentos.carregaSeriesCursos(ddlSerieCurso, modalidade, unidade, true);
        }

        /// <summary>
        /// Carrega as Turmas de acordo com série e curso.
        /// </summary>
        private void carregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int unidade = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaTurmas(ddlTurma, unidade, modalidade, serie, true);
        }

        /// <summary>
        /// Carrega os Alunos matriculados
        /// </summary>
        private void carregaAluno()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int unidade = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string ano = ddlAno.SelectedValue;
            AuxiliCarregamentos.CarregaAlunoMatriculado(ddlAluno, unidade, modalidade, serie, turma, ano, true);
        }

        #endregion

        #region Funções de Campo

        protected void ddlUnidade_SelectedIndexChanged1(object sender, EventArgs e)
        {
            carregaModalidade();
            carregaSerieCurso();
            carregaTurma();
        }

        protected void ddlModalidade_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaSerieCurso();
        }

        protected void ddlSerieCurso_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaTurma();
        }

        protected void ddlTurma_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaAluno();
        }

        protected void chkPesqNossNum_OnCheckedChanged(object sender, EventArgs e)
        {
            //Habilita o texto para pesquisa por Número do Nosso Número se o chk correspondente for clicado.
            txtNuNossNu.Enabled = chkPesqNossNum.Checked;

            if (chkPesqNossNum.Checked == true)
                ddlAno.Enabled = ddlUnidade.Enabled = ddlModalidade.Enabled = ddlSerieCurso.Enabled = ddlTurma.Enabled = ddlAluno.Enabled = false;
            else
                ddlAno.Enabled = ddlUnidade.Enabled = ddlModalidade.Enabled = ddlSerieCurso.Enabled = ddlTurma.Enabled = ddlAluno.Enabled = true;
        }

        #endregion
    }
}