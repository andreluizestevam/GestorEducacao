//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: GESTÃO OPERACIONAL DE FREQUÊNCIA
// OBJETIVO: *****
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using Resources;
using C2BR.GestorEducacao.UI.Library.Componentes;
using System.Runtime.InteropServices;
using System.IO;
using System.ServiceModel;
using System.Configuration;
using C2BR.GestorEducacao.DLLRelatorioWeb.Interfaces;
using WRAuxiliares = C2BR.GestorEducacao.DLLRelatorioWeb.Auxiliares;
using C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8100_CtrlAtendimetoMedico;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3299_Relatorios
{
    public partial class DemonAtendMedic1 : System.Web.UI.Page
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
                CarregaUnidade();
                CarregaEspecialidades(LoginAuxili.CO_EMP);
            }
        }

        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis de parâmetro do Relatório
            string infos, parametros, periodo;
            int lRetorno, coUnid, coEspec, Ordenacao;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            coUnid = (ddlUnid.SelectedValue != "" ? int.Parse(ddlUnid.SelectedValue) : 0);
            coEspec = (ddlEspec.SelectedValue != "" ? int.Parse(ddlEspec.SelectedValue) : 0);
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            parametros = "( Unidade: " + ddlUnid.SelectedItem.Text.ToUpper() + " - Especialidade: " + ddlEspec.SelectedItem.Text.ToUpper()
                 + " - Relatório ordenado por " + ddlClassificacao.SelectedItem.Text + " - " + ddlTipoOrdem.SelectedItem.Text + " )";

            Ordenacao = int.Parse(ddlClassificacao.SelectedValue);
            periodo = txtIniPeri.Text + " à " + txtFimPeri.Text;

            RptDemonAtendMedic rpt = new RptDemonAtendMedic();
            lRetorno = rpt.InitReport(parametros, infos, LoginAuxili.CO_EMP, coUnid, coEspec, txtIniPeri.Text, txtFimPeri.Text, Ordenacao, periodo, ddlTipoOrdem.SelectedValue, (chkComAtendimento.Checked ? true : false), (chkGraficos.Checked ? true : false), (chkComAtendimento.Checked ? true : false));
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        /// <summary>
        /// Carrega as unidades
        /// </summary>
        private void CarregaUnidade()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnid, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        /// <summary>
        /// Carrega as especialidades
        /// </summary>
        private void CarregaEspecialidades(int coEmp)
        {
            AuxiliCarregamentos.CarregaEspeciacialidades(ddlEspec, coEmp, null, true);
        }

        protected void ddlUnid_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int coEmp = ddlUnid.SelectedValue != "" ? int.Parse(ddlUnid.SelectedValue) : 0;
            CarregaEspecialidades(coEmp);
        }
    }
}