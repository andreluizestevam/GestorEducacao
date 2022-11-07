//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE RECEITAS (CONTAS A RECEBER)
// OBJETIVO: POSIÇÃO DE TÍTULOS DE RECEITAS/RECURSOS - GERAL
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 10/06/2013| Victor Martins Machado     | Foi inserido nos filtros do relatório a combo de Responsável financeiro
//           |                            | do aluno, que só é apresentado quando é selecionado na combo de
//           |                            | 'Pesquisado por' o valor de 'Responsável'.
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
using C2BR.GestorEducacao.Reports.GSAUD._5000_CtrlFinanceira._5200_CtrlReceitas;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GSAUD.F5000_CtrlFinanceira.F5299_Relatorios
{
    public partial class RelatoPosicaoRecursosReceitas : System.Web.UI.Page
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

                CarregaResponsavel();
                CarregaPaciente();
                CarregaClientes();
                CarregaAgrupadores();

                liResponsavel.Visible = false;
            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            string strParametrosRelatorio;
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            string strP_NU_DOC, strINFOS, strP_IC_SIT_DOC, strUniContr, strPesqPor;
            int strP_CO_EMP, strP_CO_ALU, strP_CO_EMP_REF, strP_CO_AGRUP, strP_CO_CLIENTE, strP_CO_RESP;
            DateTime strP_DT_INI, strP_DT_FIM;

            //--------> Inicializa as variáveis
            strP_NU_DOC = null;
            strUniContr = null;
            strPesqPor = null;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strParametrosRelatorio = "";
            strP_CO_EMP = LoginAuxili.CO_EMP;
            strP_NU_DOC = txtNumDoc.Text;
            strP_CO_EMP_REF = int.Parse(ddlUnidade.SelectedValue);
            strUniContr = ddlUnidContrato.SelectedValue;
            strPesqPor = ddlPesqPor.SelectedValue;
            strP_CO_ALU = ddlTipoPessoa.SelectedValue == "A" ? int.Parse(ddlPacientes.SelectedValue) : 0;
            strP_CO_CLIENTE = ddlTipoPessoa.SelectedValue == "C" ? int.Parse(ddlClientes.SelectedValue) : 0;
            strP_IC_SIT_DOC = ddlStaDocumento.SelectedValue;
            strP_DT_INI = DateTime.Parse(txtDataPeriodoIni.Text);
            strP_DT_FIM = DateTime.Parse(txtDataPeriodoFim.Text);
            strP_CO_AGRUP = int.Parse(ddlAgrupador.SelectedValue);
            strP_CO_RESP = int.Parse(ddlResponsavel.SelectedValue);

            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            string siglaUnid = TB25_EMPRESA.RetornaPelaChavePrimaria(strP_CO_EMP_REF).sigla;
            string siglaUnidContr = strUniContr != "T" ? TB25_EMPRESA.RetornaPelaChavePrimaria(int.Parse(strUniContr)).sigla : "Todas";
            string descPesqu = ddlPesqPor.SelectedValue == "V" ? "Data Vencto" : ddlPesqPor.SelectedItem.ToString();
            
            strParametrosRelatorio = "( Unidade: " + siglaUnid + "- Unid. Contrato: " + siglaUnidContr + "- Buscar Por: " + descPesqu + " - Documentos: " + ddlStaDocumento.SelectedItem.ToString()
                + " - Período: " + DateTime.Parse(txtDataPeriodoIni.Text).ToString("dd/MM/yy") + " à " + DateTime.Parse(txtDataPeriodoFim.Text).ToString("dd/MM/yy") + " - Agrupador: " + ddlAgrupador.SelectedItem.ToString() + " )";

            //Parte responsável por coletar o nome da funcionalidade cadastrada no banco e mandar como parâmetro para o 
            //relatório usar no cabeçalho
            #region
            string NO_RELATORIO = "";
            var res = (from adm in ADMMODULO.RetornaTodosRegistros()
                       where adm.nomURLModulo == "GSAUD/5000_CtrlFinanceira/5299_Relatorios/RelatoPosicaoRecursosReceitas.aspx"
                       select new { adm.nomItemMenu }).FirstOrDefault();
            NO_RELATORIO = (res != null ? res.nomItemMenu : "");
            #endregion

            RptRelatoPosicaoRecursosReceitas fpcb = new RptRelatoPosicaoRecursosReceitas();
            lRetorno = fpcb.InitReport(strParametrosRelatorio, LoginAuxili.ORG_CODIGO_ORGAO, strP_CO_EMP, strP_CO_EMP_REF,
                strUniContr, strPesqPor, strP_CO_ALU, strP_CO_CLIENTE, strP_IC_SIT_DOC, strP_DT_INI, strP_DT_FIM, strP_NU_DOC, strP_CO_AGRUP, chkIncluiCancel.Checked, strINFOS, strP_CO_RESP, NO_RELATORIO);
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
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                     where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy(e => e.NO_FANTAS_EMP);

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();


            ddlUnidContrato.DataSource = from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                         where tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                         select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP };

            ddlUnidContrato.DataTextField = "NO_FANTAS_EMP";
            ddlUnidContrato.DataValueField = "CO_EMP";
            ddlUnidContrato.DataBind();

            ddlUnidContrato.Items.Insert(0, new ListItem("Todas", "T"));
        }

        /// <summary>
        /// Método que carrega o dropdown de responsáveis
        /// </summary>
        private void CarregaResponsavel()
        {
            AuxiliCarregamentos.CarregaResponsaveis(ddlResponsavel, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        /// <summary>
        /// Método que carrega o dropdown de Agrupadores
        /// </summary>
        private void CarregaAgrupadores()
        {
            ddlAgrupador.DataSource = (from tb315 in TB315_AGRUP_RECDESP.RetornaTodosRegistros()
                                       where tb315.TP_AGRUP_RECDESP == "R" || tb315.TP_AGRUP_RECDESP == "T"
                                       select new { tb315.ID_AGRUP_RECDESP, tb315.DE_SITU_AGRUP_RECDESP });

            ddlAgrupador.DataTextField = "DE_SITU_AGRUP_RECDESP";
            ddlAgrupador.DataValueField = "ID_AGRUP_RECDESP";
            ddlAgrupador.DataBind();

            ddlAgrupador.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Pacientes
        /// </summary>
        private void CarregaPaciente()
        {
            AuxiliCarregamentos.CarregaPacientes(ddlPacientes, LoginAuxili.CO_EMP, true);
        }

        /// <summary>
        /// Método que carrega o dropdown de Clientes
        /// </summary>
        private void CarregaClientes()
        {
            ddlClientes.DataSource = (from tb103 in TB103_CLIENTE.RetornaTodosRegistros()
                                      select new { tb103.CO_CLIENTE, tb103.NO_FAN_CLI });

            ddlClientes.DataTextField = "NO_FAN_CLI";
            ddlClientes.DataValueField = "CO_CLIENTE";
            ddlClientes.DataBind();

            ddlClientes.Items.Insert(0, new ListItem("Todos", "0"));
        }
        #endregion

        protected void ddlUnidade_SelectedIndexChanged1(object sender, EventArgs e)
        {
            CarregaPaciente();
        }

        protected void ddlTipoPessoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoPessoa.SelectedValue == "A")
            {
                liAluno.Visible = true;
                liCliente.Visible = false;
            }
            else
            {
                liAluno.Visible = false;
                liCliente.Visible = true;
            }
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

        protected void ddlPesqPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPesqPor.SelectedValue != "R")
                liResponsavel.Visible = false;
            else
                liResponsavel.Visible = true;

            ddlResponsavel.SelectedValue = "0";
            CarregaPaciente();
        }

        protected void ddlResponsavel_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaPaciente();
        }
    }
}
