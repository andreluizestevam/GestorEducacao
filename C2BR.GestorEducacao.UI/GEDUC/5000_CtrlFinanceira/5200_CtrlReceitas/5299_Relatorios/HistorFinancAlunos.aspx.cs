//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE RECEITAS (CONTAS A RECEBER)
// OBJETIVO: HISTÓRICO FINANCEIRO DE ALUNOS - SÉRIE/TURMA
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
using C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5200_CtrlReceitas;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5200_CtrlReceitas.F5299_Relatorios
{
    public partial class HistorFinancAlunos : System.Web.UI.Page
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
                CarregaAlunos();
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
            ///Variáveis obrigatórias para gerar o Relatório
            string strParametrosRelatorio, strINFOS;
            int lRetorno;

            ///Variáveis de parâmetro do Relatório
            int strP_CO_EMP, strP_CO_EMP_REF, strP_CO_EMP_UNID_CONT, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_AGRUP, strP_CO_ALU, strP_CO_RESP;
            string strP_CO_ANO_REF, strP_IC_SIT_DOC;

            ///Inicializa as variáveis       
            strP_CO_ANO_REF = null;
            strP_IC_SIT_DOC = null;

            ///Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = LoginAuxili.CO_EMP;
            strP_CO_EMP_REF = int.Parse(ddlUnidade.SelectedValue);
            strP_CO_EMP_UNID_CONT = int.Parse(ddlUnidadeContrato.SelectedValue);            
            strP_CO_MODU_CUR = int.Parse(ddlModalidade.SelectedValue);
            strP_CO_CUR = int.Parse(ddlSerieCurso.SelectedValue);
            strP_CO_TUR = int.Parse(ddlTurma.SelectedValue);
            strP_CO_ANO_REF = (LoginAuxili.CO_TIPO_UNID == "PGE" ? ddlAnoRefer.SelectedValue : "0");
            strP_CO_ALU = int.Parse(ddlAluno.SelectedValue);
            strP_IC_SIT_DOC = ddlStaDocumento.SelectedValue;
            strP_CO_AGRUP = int.Parse(ddlAgrupador.SelectedValue);
            strP_CO_RESP = int.Parse(ddlResponsavel.SelectedValue);
            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptHistorFinancAlunos fpcb = new RptHistorFinancAlunos();

            #region Verifica campos
            if (chkPorAluno.Checked)
            {
                strP_CO_MODU_CUR = strP_CO_CUR = strP_CO_TUR = strP_CO_RESP = 0;
                fpcb.Titulo = "HISTÓRICO FINANCEIRO DE ALUNOS";
            }
            else if (chkPorResponsavel.Checked)
            {
                strP_CO_MODU_CUR = strP_CO_CUR = strP_CO_TUR = 0;
                strP_CO_ALU = 0;
                fpcb.Titulo = "HISTÓRICO FINANCEIRO DE ALUNOS - POR RESPONSÁVEL";
            }
            else
            {
                strP_CO_ALU = strP_CO_RESP = 0;
                fpcb.Titulo = "HISTÓRICO FINANCEIRO DE ALUNOS - POR SÉRIE/TURMA";
            }
            #endregion

            string paramEscolas = (LoginAuxili.CO_TIPO_UNID == "PGE" ? "Ano: " + ddlAnoRefer.SelectedItem.ToString().Trim() + " - Modalidade: "
                + ddlModalidade.SelectedItem.ToString() + " - Série: " + ddlSerieCurso.SelectedItem.ToString()
                + " - Turma: " + ddlTurma.SelectedItem.ToString() + " - " : "");

            strParametrosRelatorio = "( " + paramEscolas + "Status: " + ddlStaDocumento.SelectedItem.ToString() + " - Agrupador: " + ddlAgrupador.SelectedItem.ToString() + " )";
            
            lRetorno = fpcb.InitReport(strParametrosRelatorio, LoginAuxili.ORG_CODIGO_ORGAO, strP_CO_EMP, strP_CO_EMP_REF, strP_CO_EMP_UNID_CONT, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_ANO_REF, strP_CO_ALU, strP_CO_RESP, strP_IC_SIT_DOC, strP_CO_AGRUP, chkIncluiCancel.Checked, strINFOS);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
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
                    lblAluPaci.Text = "Paciente";
                    chkPorAluno.Text = "Por Paciente";

                    liItensEscola.Visible =
                    liAnoRefer.Visible = 
                    chkPorSerieTurma.Visible = false;
                    break;
                case "PGE":
                default:
                    lblAluPaci.Text = "Aluno(a)";
                    chkPorAluno.Text = "Por Aluno";

                    liItensEscola.Visible =
                    liAnoRefer.Visible =
                    chkPorSerieTurma.Visible = true;
                    break;
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaUnidades()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
            AuxiliCarregamentos.CarregaUnidade(ddlUnidadeContrato, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        private void CarregaTurma()
        {
            int modalidade = (ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0);
            int coEmp = (ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0);
            int serie = (ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0);
            AuxiliCarregamentos.CarregaTurmas(ddlTurma, coEmp, modalidade, serie, true);
        }

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            int coEmp = (ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0);
            AuxiliCarregamentos.CarregaAno(ddlAnoRefer, coEmp, true);
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
            int modalidade = (ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0);
            int coEmp = (ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0);
            AuxiliCarregamentos.carregaSeriesCursos(ddlSerieCurso, modalidade, coEmp, true);
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
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        private void CarregaAlunos()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                                   where tb07.CO_EMP == coEmp
                                   select new { tb07.NO_ALU, tb07.CO_ALU }).Distinct().OrderBy(g => g.NO_ALU);

            ddlAluno.DataTextField = "NO_ALU";
            ddlAluno.DataValueField = "CO_ALU";
            ddlAluno.DataSource = res;
            ddlAluno.DataBind();

            ddlAluno.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        private void CarregaResponsaveis()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaResponsaveis(ddlResponsavel, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        /// <summary>
        /// Carrega todos os status de títulos financeiros
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
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
        }

        protected void ddlUnidade_SelectedIndexChanged1(object sender, EventArgs e)
        {
            CarregaAnos();
            CarregaModalidades();
            CarregaSerieCurso();
            CarregaTurma();
            CarregaAlunos();
        }

        protected void ddlAnoRefer_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
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

        protected void chkPorSerieTurma_CheckedChanged(object sender, EventArgs e)
        {
            chkPorAluno.Checked = chkPorResponsavel.Checked = false;

            liModalidade.Visible = liSerie.Visible = liTurma.Visible = true;
            liAluno.Visible = liResponsavel.Visible = false;
        }

        protected void chkPorAluno_CheckedChanged(object sender, EventArgs e)
        {
            chkPorSerieTurma.Checked = chkPorResponsavel.Checked = false;

            liModalidade.Visible = liSerie.Visible = liTurma.Visible = liResponsavel.Visible = false;
            liAluno.Visible = true;
        }

        protected void chkPorResponsavel_CheckedChanged(object sender, EventArgs e)
        {
            chkPorAluno.Checked = chkPorSerieTurma.Checked = false;

            liModalidade.Visible = liSerie.Visible = liTurma.Visible = liAluno.Visible = false;
            liResponsavel.Visible = true;
        }

        #endregion
    }
}
