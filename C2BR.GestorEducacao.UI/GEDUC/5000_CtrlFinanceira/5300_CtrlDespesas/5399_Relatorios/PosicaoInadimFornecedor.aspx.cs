//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE RECEITAS (CONTAS A RECEBER)
// OBJETIVO: POSIÇÃO DE INADIMPLÊNCIA DE TÍTULOS DE RECEITAS DE ALUNOS 
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
using C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5300_CtrlDespesas;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5200_CtrlReceitas.F5299_Relatorios
{
    public partial class PosicaoInadimFornecedor : System.Web.UI.Page
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
                CarregaAgrupadores();
            }
        }

        //====> Método que faz a chamada de outro método de acordo com o Tipo
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            if (ddlTpInadimplencia.SelectedValue.ToString() == "F")
                RelInadimplenciaPorForn();
            else
                RelInadimplencia();
        }

        //====> Processo de Geração do Relatório
        private void RelInadimplenciaPorForn()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            string strParametrosRelatorio, strINFOS;
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            int strP_CO_EMP, strP_CO_EMP_REF, strP_CO_AGRUP;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP_REF = LoginAuxili.CO_EMP;
            strP_CO_EMP = int.Parse(ddlUnidade.SelectedValue);
            strP_CO_AGRUP = int.Parse(ddlAgrupador.SelectedValue);
            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            strParametrosRelatorio = "(Unidade: " + ddlUnidade.SelectedItem.ToString();
            strParametrosRelatorio += " - Agrupador: " + ddlAgrupador.SelectedItem.Text + ")";

            RptPosicInadimPorFornecedor fpcb = new RptPosicInadimPorFornecedor();
            lRetorno = fpcb.InitReport(strParametrosRelatorio, LoginAuxili.ORG_CODIGO_ORGAO, strP_CO_EMP, strP_CO_EMP_REF, strP_CO_AGRUP, strINFOS);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        //====> Processo de Geração do Relatório
        private void RelInadimplencia()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            string strParametrosRelatorio, strINFOS;
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            int strP_CO_EMP, strP_CO_EMP_REF, strP_CO_FORN, strP_CO_AGRUP;

            //--------> Inicializa as variáveis
            strP_CO_EMP = LoginAuxili.CO_EMP;
            strP_CO_EMP_REF = int.Parse(ddlUnidade.SelectedValue);
            strP_CO_FORN = int.Parse(ddlResponsavel.SelectedValue);
            strP_CO_AGRUP = int.Parse(ddlAgrupador.SelectedValue);
            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            var tb41 = (from iTb41 in TB41_FORNEC.RetornaTodosRegistros()
                        where iTb41.CO_FORN == strP_CO_FORN
                        select new
                        {
                            iTb41.NO_FAN_FOR,
                            iTb41.CO_CPFCGC_FORN
                        }).First();

            string id = string.Empty;
            if (tb41.CO_CPFCGC_FORN.Length == 11) //CPF
                id = tb41.CO_CPFCGC_FORN.Insert(3, ".").Insert(7, ".").Insert(11, "-");
            else // CNPJ
                id = tb41.CO_CPFCGC_FORN.Insert(2, ".").Insert(6, ".").Insert(10, "/").Insert(15, "-");

            strParametrosRelatorio = "(Unidade: " + ddlUnidade.SelectedItem.Text;
            strParametrosRelatorio += " - Fornecedor: " + tb41.NO_FAN_FOR + (id.Length == 14 ? " CPF: " : " CNPJ: ") + id;
            strParametrosRelatorio += " - Agrupador: " + ddlAgrupador.SelectedItem.Text + ")";

            RptPosicInadimFicha fpcb = new RptPosicInadimFicha();
            lRetorno = fpcb.InitReport(strParametrosRelatorio, LoginAuxili.ORG_CODIGO_ORGAO, strP_CO_EMP, strP_CO_EMP_REF, strP_CO_FORN, strP_CO_AGRUP, strINFOS);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método que controla a visibilidade dos campos
        /// </summary>
        private void ControlaVisibilidade()
        {
            if (ddlTpInadimplencia.SelectedValue == "I")
            {
                liResponsavel.Visible = true;

                ddlResponsavel.DataSource = (from tb108 in TB41_FORNEC.RetornaTodosRegistros()
                                             select new { tb108.CO_FORN, tb108.NO_FAN_FOR });

                ddlResponsavel.DataTextField = "NO_FAN_FOR";
                ddlResponsavel.DataValueField = "CO_FORN";
                ddlResponsavel.DataBind();
            }
            else
                liResponsavel.Visible = false;
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

            ControlaVisibilidade();
        }

        /// <summary>
        /// Método que carrega o dropdown de Agrupadores
        /// </summary>
        private void CarregaAgrupadores()
        {
            ddlAgrupador.DataSource = (from tb315 in TB315_AGRUP_RECDESP.RetornaTodosRegistros()
                                       where tb315.TP_AGRUP_RECDESP == "D" || tb315.TP_AGRUP_RECDESP == "T"
                                       select new { tb315.ID_AGRUP_RECDESP, tb315.DE_SITU_AGRUP_RECDESP });

            ddlAgrupador.DataTextField = "DE_SITU_AGRUP_RECDESP";
            ddlAgrupador.DataValueField = "ID_AGRUP_RECDESP";
            ddlAgrupador.DataBind();

            ddlAgrupador.Items.Insert(0, new ListItem("Todos", "0"));
        }
        #endregion

        protected void ddlTpInadimplencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            ControlaVisibilidade();
        }
    }
}
