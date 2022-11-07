//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE RECEITAS (CONTAS A RECEBER)
// OBJETIVO: POSIÇÃO DE TÍTULOS DE RECEITAS - DE ALUNOS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//11/06/2012|   Thales Pinho Andrade     |add uma if que direnciona RTP conrespondete se é ordenado Por aluno Ou Por Responsavel
//          |                            |Antes de Gerar o Relatorio RptRelacaoTituloAlunosRecursReceitas OU RptRelacaoTituloAlunosRecursReceitasMo2
//----------+----------------------------+-------------------------------------
//
//
//
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
using C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5200_CtrlReceitas;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5200_CtrlReceitas.F5299_Relatorios
{
    public partial class RelacaoTituloAlunosRecursReceitas : System.Web.UI.Page
    {
        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            ///Criação das instâncias utilizadas
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
                CarregaResponsaveis();
                CarregaAgrupadores();
                CarregaTipoDocumento();

                VerificaTipoUnid();
            }
        }

        /// <summary>
        /// Processo de Geração do Relatório
        /// </summary>
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            if (DateTime.Parse(txtDtVenctoFim.Text).Subtract(DateTime.Parse(txtDtVenctoIni.Text)).TotalDays > 366)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Intervalo de período máximo permitido é de 12 meses.");
                return;
            }

            ///Variáveis obrigatórias para gerar o Relatório
            string strParametrosRelatorio, strINFOS;
            int lRetorno;

            ///Variáveis de parâmetro do Relatório
            string strP_IC_SIT_DOC, strP_NU_DOC, strP_DT_INI, strP_DT_FIM, strP_DT_VEN_INI, strP_DT_VEN_FIM, strP_CO_ANO_MES_MAT;

            int strP_Ordem, strP_CO_EMP, strP_CO_EMP_REF, strP_CO_UNID_CONTR, strP_CO_ALU, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_AGRUP, strP_CO_RESP;

            ///Inicializa as variáveis
            strP_Ordem = 0;
            strP_CO_ANO_MES_MAT = null;
            strP_IC_SIT_DOC = null;
            strP_NU_DOC = null;
            strP_DT_INI = "";
            strP_DT_FIM = "";
            strP_DT_VEN_INI = "";
            strP_DT_VEN_FIM = "";


            ///Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strParametrosRelatorio = "";
            strP_CO_EMP = LoginAuxili.CO_EMP;
            strP_NU_DOC = txtNumDoc.Text;
            strP_CO_EMP_REF = int.Parse(ddlUnidade.SelectedValue);
            strP_CO_UNID_CONTR = int.Parse(ddlUnidContrato.SelectedValue);
            strP_CO_MODU_CUR = int.Parse(ddlModalidade.SelectedValue);
            strP_CO_CUR = int.Parse(ddlSerieCurso.SelectedValue);
            strP_CO_TUR = int.Parse(ddlTurma.SelectedValue);
            strP_CO_ANO_MES_MAT = ddlAnoRefer.SelectedValue;
            strP_CO_ALU = int.Parse(ddlAlunos.SelectedValue);
            strP_IC_SIT_DOC = ddlStaDocumento.SelectedValue;
            strP_DT_INI = txtDataPeriodoIni.Text;
            strP_DT_FIM = txtDataPeriodoFim.Text;
            strP_DT_VEN_INI = txtDtVenctoIni.Text;
            strP_DT_VEN_FIM = txtDtVenctoFim.Text;
            strP_CO_AGRUP = int.Parse(ddlAgrupador.SelectedValue);
            strP_CO_RESP = int.Parse(ddlResponsavel.SelectedValue);
            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);


            if (chkPorAluno.Checked)
            {
                strP_Ordem = 1;
                strP_CO_RESP = -1;

            }
            else if (chkPorResponsavel.Checked)
            {
                strP_Ordem = -1;
                strP_CO_MODU_CUR = strP_CO_CUR = strP_CO_TUR = -1;
                strP_CO_ALU = -1;
            }
            // Se Checked for igual a Por Aluno = 1 ele se relaciona com o RptRelacaoTituloAlunosRecursReceitas Ordem por Aluno
            if (strP_Ordem == 1)
            {
                RptRelacaoTituloAlunosRecursReceitas fpcb = new RptRelacaoTituloAlunosRecursReceitas();
                string siglaUnid = TB25_EMPRESA.RetornaPelaChavePrimaria(strP_CO_EMP_REF).sigla;
                string siglaUnidContr = strP_CO_UNID_CONTR != -1 ? TB25_EMPRESA.RetornaPelaChavePrimaria(strP_CO_UNID_CONTR).sigla : "Todas";
                string paramEscolas = (LoginAuxili.CO_TIPO_UNID == "PGE" ? " - Ano Referência: " + ddlAnoRefer.SelectedItem.ToString()
                    + " - Modalidade: " + ddlModalidade.SelectedItem.ToString() + " - Série: " + ddlSerieCurso.SelectedItem.ToString() +
                    " - Turma: " + ddlTurma.SelectedItem.ToString() : "");

                strParametrosRelatorio = "(Unidade: " + siglaUnid + " - Unidade Contrato: " + siglaUnidContr + paramEscolas + " - Documentos: " + ddlStaDocumento.SelectedItem.ToString() +
                    " - Agrupador: " + ddlAgrupador.SelectedItem.ToString() + ")";


                lRetorno = fpcb.InitReport(strParametrosRelatorio, strP_Ordem, LoginAuxili.ORG_CODIGO_ORGAO, strP_CO_EMP, strP_CO_EMP_REF, strP_CO_UNID_CONTR, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_RESP, strP_CO_ANO_MES_MAT, strP_CO_ALU, strP_IC_SIT_DOC, strP_DT_VEN_INI, strP_DT_VEN_FIM, strP_DT_INI, strP_DT_FIM, strP_NU_DOC, strP_CO_AGRUP, chkIncluiCancel.Checked, strINFOS);
                Session["Report"] = fpcb;
                Session["URLRelatorio"] = "/GeducReportViewer.aspx";

                AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
            }
            // Se Checked for igual a Por Aluno = 0 ele se relaciona com o RptRelacaoTituloAlunosRecursReceitasMod2 Ordem por Responsavel
            else if (strP_Ordem == 0)
            {
                RptRelacaoTituloAlunosRecursReceitasMod2 fpcb = new RptRelacaoTituloAlunosRecursReceitasMod2();
                string siglaUnid = TB25_EMPRESA.RetornaPelaChavePrimaria(strP_CO_EMP_REF).sigla;
                string siglaUnidContr = strP_CO_UNID_CONTR != -1 ? TB25_EMPRESA.RetornaPelaChavePrimaria(strP_CO_UNID_CONTR).sigla : "Todas";
                string paramEscola = (LoginAuxili.CO_TIPO_UNID == "PGE" ? " - Ano Referência: " + ddlAnoRefer.SelectedItem.ToString()
                    + " - Modalidade: " + ddlModalidade.SelectedItem.ToString() + " - Série: " + ddlSerieCurso.SelectedItem.ToString() +
                    " - Turma: " + ddlTurma.SelectedItem.ToString() : "");

                strParametrosRelatorio = "(Unidade: " + siglaUnid + " - Unidade Contrato: " + siglaUnidContr + paramEscola + " - Documentos: " + ddlStaDocumento.SelectedItem.ToString() +
                    " - Agrupador: " + ddlAgrupador.SelectedItem.ToString() + ")";


                lRetorno = fpcb.InitReport(strParametrosRelatorio, strP_Ordem, LoginAuxili.ORG_CODIGO_ORGAO, strP_CO_EMP, strP_CO_EMP_REF, strP_CO_UNID_CONTR, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_RESP, strP_CO_ANO_MES_MAT, strP_CO_ALU, strP_IC_SIT_DOC, strP_DT_VEN_INI, strP_DT_VEN_FIM, strP_DT_INI, strP_DT_FIM, strP_NU_DOC, strP_CO_AGRUP, chkIncluiCancel.Checked, strINFOS);
                Session["Report"] = fpcb;
                Session["URLRelatorio"] = "/GeducReportViewer.aspx";

                AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);

            }
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Verifica o tipo da unidade e faz as devidas alterações
        /// </summary>
        private void VerificaTipoUnid()
        {
            switch (LoginAuxili.CO_TIPO_UNID)
            {
                case "PGS":
                    lblNomeAluPac.Text = "Paciente";
                    liItensEscola.Visible =
                    liAnoRefer.Visible = false;
                    chkPorAluno.Text = "Por Paciente";
                    break;
                case "PGE":
                default:
                    lblNomeAluPac.Text = "Aluno(a)";
                    liItensEscola.Visible =
                    liAnoRefer.Visible = true;
                    chkPorAluno.Text = "Por Aluno";
                    break;
            }
        }

        /// <summary>
        /// Metado que carrega o Dropdonw de Responsavel
        /// </summary>
        private void CarregaResponsaveis()
        {
            ddlResponsavel.Items.Clear();
            ddlResponsavel.Items.AddRange(AuxiliBaseApoio.ResponsaveisDDL(LoginAuxili.ORG_CODIGO_ORGAO, todos: true));
            ddlResponsavel.SelectedValue = "-1";
        }

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaUnidades()
        {
            ddlUnidade.Items.Clear();
            ddlUnidade.Items.AddRange(AuxiliBaseApoio.UnidadesDDL(LoginAuxili.IDEADMUSUARIO));
            if(ddlUnidade.Items.FindByValue(LoginAuxili.CO_EMP.ToString()) != null)
                ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();

            ddlUnidContrato.Items.Clear();
            ddlUnidContrato.Items.AddRange(AuxiliBaseApoio.UnidadesDDL(LoginAuxili.ORG_CODIGO_ORGAO, todos:true));
            ddlUnidContrato.SelectedValue = "-1";
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        private void CarregaTurma()
        {
            int modalide = (!string.IsNullOrEmpty(ddlModalidade.SelectedValue) ? int.Parse(ddlModalidade.SelectedValue) : 0);
            int curso = (!string.IsNullOrEmpty(ddlSerieCurso.SelectedValue) ? int.Parse(ddlSerieCurso.SelectedValue) : 0);
            int coEmp = (!string.IsNullOrEmpty(ddlUnidade.SelectedValue) ? int.Parse(ddlUnidade.SelectedValue) : 0);
            AuxiliCarregamentos.CarregaTurmas(ddlTurma, coEmp, modalide, curso, true);
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
        private void CarregaSerieCurso()
        {
                int modalide = (!string.IsNullOrEmpty(ddlModalidade.SelectedValue) ? int.Parse(ddlModalidade.SelectedValue) : 0);
                int coEmp = (!string.IsNullOrEmpty(ddlUnidade.SelectedValue) ? int.Parse(ddlUnidade.SelectedValue) : 0);
                AuxiliCarregamentos.carregaSeriesCursos(ddlSerieCurso, modalide, coEmp, true);
        }

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        private void CarregaAluno()
        {
            if (LoginAuxili.CO_TIPO_UNID == "PGS")
            {
                int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

                ddlAlunos.DataSource = (from tb07 in TB07_ALUNO.RetornaPelaUnid(coEmp)
                                        select new { tb07.CO_ALU, tb07.NO_ALU });

                ddlAlunos.DataTextField = "NO_ALU";
                ddlAlunos.DataValueField = "CO_ALU";
                ddlAlunos.DataBind();

                ddlAlunos.Items.Insert(0, new ListItem("Todos", "-1"));
            }
            else
            {
                int modalide = (!string.IsNullOrEmpty(ddlModalidade.SelectedValue) ? int.Parse(ddlModalidade.SelectedValue) : 0);
                int curso = (!string.IsNullOrEmpty(ddlSerieCurso.SelectedValue) ? int.Parse(ddlSerieCurso.SelectedValue) : 0);
                int turma = (!string.IsNullOrEmpty(ddlTurma.SelectedValue) ? int.Parse(ddlTurma.SelectedValue) : 0);
                int coEmp = (!string.IsNullOrEmpty(ddlUnidade.SelectedValue) ? int.Parse(ddlUnidade.SelectedValue) : 0);
                AuxiliCarregamentos.CarregaAlunoMatriculado(ddlAlunos, coEmp, modalide, curso, turma, ddlAnoRefer.SelectedValue, true); 
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Agrupadores
        /// </summary>
        private void CarregaAgrupadores()
        {
            ddlAgrupador.Items.Clear();
            ddlAgrupador.Items.AddRange(AuxiliBaseApoio.AgrupadoresDDL("R", todos:true));
            ddlAgrupador.SelectedValue = "-1";
        }

        /// <summary>
        /// Carrega o tipo de status financeiro dos títulos
        /// </summary>
        private void CarregaTipoDocumento()
        {
            ddlStaDocumento.Items.Clear();
            ddlStaDocumento.Items.AddRange(AuxiliBaseApoio.BaseApoioDDL(statusFinanceiro.ResourceManager, todos:true));
            ddlStaDocumento.SelectedValue = "-1";
        }
        #endregion

        #region Eventos de componentes da pagina
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

        protected void ddlUnidade_SelectedIndexChanged1(object sender, EventArgs e)
        {
            CarregaAnos();
            CarregaModalidades();
            CarregaSerieCurso();
            CarregaTurma();
            CarregaAluno();
        }

        protected void ddlAnoRefer_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaModalidades();
            CarregaAluno();
        }

        protected void ddlStaDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlStaDocumento.SelectedValue == "T")
            {
                chkIncluiCancel.Visible = true;
            }
            else
            {
                chkIncluiCancel.Visible = false;
                chkIncluiCancel.Checked = false;
            }
        }

        protected void chkPorAluno_CheckedChanged1(object sender, EventArgs e)
        {
            chkPorResponsavel.Checked = false;
            liModalidade.Visible = liSerie.Visible = liTurma.Visible = liAluno.Visible = true;
            liAluno.Visible = true;
            liResponsavel.Visible = false;

        }

        protected void chkPorResponsavel_CheckedChanged1(object sender, EventArgs e)
        {
            chkPorAluno.Checked = false;
            liModalidade.Visible = liSerie.Visible = liTurma.Visible = liAluno.Visible = false;
            liResponsavel.Visible = true;
        }

        protected void ddlTurma_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAluno();
        }

        #endregion

    }
}
