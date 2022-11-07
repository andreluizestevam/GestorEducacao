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
using C2BR.GestorEducacao.Reports.GSAUD._5000_CtrlFinanceira._5200_CtrlReceitas;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução GSAUD.5000_CtrlFinanceira.5299_Relatorios
namespace C2BR.GestorEducacao.UI.GSAUD._5000_CtrlFinanceira._5299_Relatorios
{
    public partial class HistorFinancPaciente : System.Web.UI.Page
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
                CarregaPaciente();
                CarregaResponsaveis();
                CarregaAgrupadores();
                CarregaTipoDocumento();
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
            int strP_CO_EMP, strP_CO_EMP_REF, strP_CO_EMP_UNID_CONT, strP_CO_AGRUP, strP_CO_ALU, strP_CO_RESP;
            string strP_IC_SIT_DOC;

            ///Inicializa as variáveis     
            strP_IC_SIT_DOC = null;

            ///Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = LoginAuxili.CO_EMP;
            strP_CO_EMP_REF = int.Parse(ddlUnidade.SelectedValue);
            strP_CO_EMP_UNID_CONT = int.Parse(ddlUnidadeContrato.SelectedValue);
            string tipo = chkPorPaciente.Checked == true ? "Paciente" : "Responsável";
            //strP_CO_A NO_REF = (LoginAuxili.CO_TIPO_UNID == "PGE" ? ddlAnoRefer.SelectedValue : "0");
             strP_CO_ALU = int.Parse(ddlPaciente.SelectedValue); 
            strP_IC_SIT_DOC = ddlStaDocumento.SelectedValue;
            strP_CO_AGRUP = int.Parse(ddlAgrupador.SelectedValue);
            strP_CO_RESP = int.Parse(ddlResponsavel.SelectedValue);
            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            string nome  =     AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GSAUD/5000_CtrlFinanceira/5299_Relatorios/HistorFinancPaciente.aspx");
            RptHistorFinancPaciente fpcb = new RptHistorFinancPaciente();

            fpcb.Titulo = "HISTÓRICO FINANCEIRO DE PACIENTES";
            
            if (chkPorPaciente.Checked)
                strP_CO_RESP = 0;
            else if (chkPorResponsavel.Checked)
            {
                strP_CO_ALU = 0;
                fpcb.Titulo = "HISTÓRICO FINANCEIRO DE PACIENTES - POR RESPONSÁVEL";
            }
            else
                strP_CO_ALU = strP_CO_RESP = 0;

            string paramEscolas = "Unidade : " + ddlUnidade.SelectedItem.Text + "- Unidade de Contrato :" + ddlUnidadeContrato.SelectedItem.Text + " Pesquisa Por :" + tipo + " ";
            strParametrosRelatorio = "("  + paramEscolas + "Status: " + ddlStaDocumento.SelectedItem.ToString() + " - Agrupador: " + ddlAgrupador.SelectedItem.ToString() + " )";

            lRetorno = fpcb.InitReport(strParametrosRelatorio, LoginAuxili.ORG_CODIGO_ORGAO, strP_CO_EMP, strP_CO_EMP_REF, strP_CO_EMP_UNID_CONT,strP_CO_ALU ,strP_CO_RESP ,strP_IC_SIT_DOC , strP_CO_AGRUP, chkIncluiCancel.Checked, strINFOS, nome);
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
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
            AuxiliCarregamentos.CarregaUnidade(ddlUnidadeContrato, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaPaciente()
        {
            AuxiliCarregamentos.CarregaPacientes(ddlPaciente,LoginAuxili.CO_EMP, true);
        }

        /// <summary>
        /// Método que carrega o dropdown de Agrupadores
        /// </summary>
        private void CarregaAgrupadores()
        {
            ddlAgrupador.Items.Clear();
            ddlAgrupador.Items.AddRange(AuxiliBaseApoio.AgrupadoresDDL("R", todos: true));
            ddlAgrupador.SelectedValue = "-1";
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
            ddlStaDocumento.Items.AddRange(AuxiliBaseApoio.BaseApoioDDL(statusFinanceiro.ResourceManager, todos: true));
            ddlStaDocumento.SelectedValue = "-1";
        }
        #endregion

        #region Eventos de componentes da pagina

        protected void ddlUnidade_SelectedIndexChanged1(object sender, EventArgs e)
        {
            CarregaPaciente();
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

        protected void chkPorPaciente_CheckedChanged(object sender, EventArgs e)
        {
            chkPorResponsavel.Checked = false;
            liPaciente.Visible = true;
            liResponsavel.Visible = false;
           
        }

        protected void chkPorResponsavel_CheckedChanged(object sender, EventArgs e)
        {
            chkPorPaciente.Checked = false;
            liPaciente.Visible = false;
            liResponsavel.Visible = true;
            
        }

        #endregion
    }
}
