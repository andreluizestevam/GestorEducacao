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
namespace C2BR.GestorEducacao.UI.GEDUC._5000_CtrlFinanceira._5200_CtrlReceitas._5299_Relatorios
{
    public partial class PrevReceitasPorDataVencimento : System.Web.UI.Page
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
                CarregaModalidades();
                CarregaCursos();
                CarregaTurma();
                CarregaTipoDocumento();
                VerificaTipoUnid();
            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            string strParametrosRelatorio, strINFOS;
            int lRetorno;


            //--------> Variáveis de parâmetro do Relatório
            int codEmp, coModu, coCur, coTur;
            DateTime strDT_INI, strDT_FIM;

            strINFOS = null;
            strDT_INI = DateTime.Parse(txtDataPeriodoIni.Text);
            strDT_FIM = DateTime.Parse(txtDataPeriodoFim.Text);

            string strP_IC_SIT_DOC = ddlStaDocumento.SelectedValue;
            codEmp = int.Parse( ddlUnidade.SelectedValue);
            coModu = (!string.IsNullOrEmpty(ddlModalidade.SelectedValue) ? int.Parse(ddlModalidade.SelectedValue) : 0);
            coCur = (!string.IsNullOrEmpty(ddlCurso.SelectedValue) ? int.Parse(ddlCurso.SelectedValue) : 0);
            coTur = (!string.IsNullOrEmpty(ddlTurma.SelectedValue) ? int.Parse(ddlTurma.SelectedValue) : 0);

            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            strParametrosRelatorio = "( Unidade: " + ddlUnidade.SelectedItem.Text + " - Modalidade: " + ddlModalidade.SelectedItem.Text 
                + " - Curso: " + ddlCurso.SelectedItem.Text +  " - Turma: " + ddlTurma.SelectedItem.Text 
                + " - Documentos: " + ddlStaDocumento.SelectedItem.ToString()
                + " - Período: " + txtDataPeriodoIni.Text + " à " + txtDataPeriodoFim.Text + " )";

            //Parte responsável por coletar o nome da funcionalidade cadastrada no banco e mandar como parâmetro para o 
            //relatório usar no cabeçalho
            #region
            string NO_RELATORIO = "";
            var res = (from adm in ADMMODULO.RetornaTodosRegistros()
                       where adm.nomURLModulo == "GEDUC/5000_CtrlFinanceira/5200_CtrlReceitas/5299_Relatorios/PrevReceitasPorDataVencimento.aspx"
                       select new { adm.nomItemMenu }).FirstOrDefault();
            NO_RELATORIO = (res != null ? res.nomItemMenu : "");
            #endregion

            RptPrevPorDtVencimento fpcb = new RptPrevPorDtVencimento();
            lRetorno = fpcb.InitReport(strParametrosRelatorio, LoginAuxili.CO_EMP, codEmp, strDT_INI, strDT_FIM, strINFOS, coModu, coCur, coTur, NO_RELATORIO, strP_IC_SIT_DOC, chkIncluiCancel.Checked);
            Session["Report"] = fpcb;
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
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, true, false);
        }

        /// <summary>
        /// Carrega as modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        /// <summary>
        /// Carrega os Cursos
        /// </summary>
        private void CarregaCursos()
        {
            int modalidade = (!string.IsNullOrEmpty(ddlModalidade.SelectedValue) ? int.Parse(ddlModalidade.SelectedValue) : 0);
            int coEmp = (!string.IsNullOrEmpty(ddlUnidade.SelectedValue) ? int.Parse(ddlUnidade.SelectedValue) : 0 );
            AuxiliCarregamentos.carregaSeriesCursos(ddlCurso, modalidade, coEmp, true); 
        }

        /// <summary>
        /// Carrega turmas
        /// </summary>
        private void CarregaTurma()
        {
            int modalidade = (!string.IsNullOrEmpty(ddlModalidade.SelectedValue) ? int.Parse(ddlModalidade.SelectedValue) : 0);
            int curso = (!string.IsNullOrEmpty(ddlCurso.SelectedValue) ? int.Parse(ddlCurso.SelectedValue) : 0);
            int coEmp = (!string.IsNullOrEmpty(ddlUnidade.SelectedValue) ? int.Parse(ddlUnidade.SelectedValue) : 0);
            AuxiliCarregamentos.CarregaTurmas(ddlTurma, coEmp, modalidade, curso, true);
        }

        /// <summary>
        /// Verifica o tipo da unidade e faz as devidas alterações
        /// </summary>
        private void VerificaTipoUnid()
        {
            switch (LoginAuxili.CO_TIPO_UNID)
            {
                case "PGS":
                    divItensEscola.Visible = false;
                    break;
                case "PGE":
                default:
                    divItensEscola.Visible = true;
                    break;
            }
        }

        /// <summary>
        /// Carrega o tipo de status financeiro dos títulos
        /// </summary>
        private void CarregaTipoDocumento()
        {
            ddlStaDocumento.Items.Clear();
            ddlStaDocumento.Items.AddRange(AuxiliBaseApoio.BaseApoioDDL(statusFinanceiro.ResourceManager, todos: true));
            ddlStaDocumento.SelectedValue = "-1";
        }
        
        #endregion

        #region Funções de Campo

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

        protected void ddlModalidade_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCursos();
        }

        protected void ddlCurso_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
        }

        #endregion
    }
}