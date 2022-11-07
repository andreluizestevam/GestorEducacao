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
//15/05/2014| Maxwell Almeida            | Criação da página 
//          |                            | 

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
using C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8110_CampanhasSaude;
namespace C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9100_CtrlCampanhasSaude._9199_Relatorios
{
    public partial class DemonResulCampanhas : System.Web.UI.Page
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
                carregaTipos();
            }
        }

        #endregion

        #region Carregamentos

        //====> Método que faz a chamada de outro método de acordo com o ddlTipoPesq
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis de parâmetro do Relatório
            string infos, parametros, coTipo, dataIni, dataFim;
            int lRetorno, coUnid;
            bool ComValor = chkComValor.Checked ? true : false;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            coTipo = ddlTipoCamp.SelectedValue;
            dataIni = txtDataIniCamp.Text;
            dataFim = txtDataFimCamp.Text;

            parametros = "( Tipo: " + ddlTipoCamp.SelectedItem.Text.ToUpper() +
                " - Período: " + dataIni + " à " + dataFim + " - Classificado por " + ddlClassPor.SelectedItem.Text
                + (ComValor ? " - Com valores" : " - Sem Valores") + " - Ordenado por " + ddlClassificacao.SelectedItem.Text
                + " - " + ddlTipoOrdem.SelectedItem.Text + " )";

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptDemonResulCampanha rpt = new RptDemonResulCampanha();
            lRetorno = rpt.InitReport(parametros, infos, LoginAuxili.CO_EMP, ddlClassPor.SelectedValue, ddlTipoCamp.SelectedValue, dataIni, dataFim, ComValor, (chkGraficos.Checked ? true : false), int.Parse(ddlClassificacao.SelectedValue), ddlTipoOrdem.SelectedValue, (chkRelatorio.Checked ? true : false));
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        /// <summary>
        /// Carrega os Tipos de Campanhas de Saúde
        /// </summary>
        private void carregaTipos()
        {
            AuxiliCarregamentos.CarregaTiposCampanhaSaude(ddlTipoCamp, true);
        }

        #endregion

        #region Funções de Campo

        protected void ddlClassPor_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlClassPor.SelectedValue)
            {
                case "U":
                    ddlClassificacao.Items.Clear();
                    ddlClassificacao.Items.Insert(0, new ListItem("VTP - Valor Total do Plantão", "10"));
                    ddlClassificacao.Items.Insert(0, new ListItem("MHP - Valor Médio da Hora", "9"));
                    ddlClassificacao.Items.Insert(0, new ListItem("QTPI - Plantões Inconsistentes", "8"));
                    ddlClassificacao.Items.Insert(0, new ListItem("QTEP - Especialidades de Plantão", "7"));
                    ddlClassificacao.Items.Insert(0, new ListItem("QTTP - Total de Plantões", "6"));
                    ddlClassificacao.Items.Insert(0, new ListItem("QTHT - Horas de Plantão", "5"));
                    ddlClassificacao.Items.Insert(0, new ListItem("QTUP - Unidades de Plantão", "4"));
                    ddlClassificacao.Items.Insert(0, new ListItem("Unidade", "3"));
                    ddlClassificacao.Items.Insert(0, new ListItem("Tipo de Campanha", "2"));
                    ddlClassificacao.Items.Insert(0, new ListItem("Campanha", "1"));
                    break;
                case "C":
                    ddlClassificacao.Items.Clear();
                    ddlClassificacao.Items.Insert(0, new ListItem("VTP - Valor Total do Plantão", "10"));
                    ddlClassificacao.Items.Insert(0, new ListItem("MHP - Valor Médio da Hora", "9"));
                    ddlClassificacao.Items.Insert(0, new ListItem("QTPI - Plantões Inconsistentes", "8"));
                    ddlClassificacao.Items.Insert(0, new ListItem("QTEP - Especialidades de Plantão", "7"));
                    ddlClassificacao.Items.Insert(0, new ListItem("QTTP - Total de Plantões", "6"));
                    ddlClassificacao.Items.Insert(0, new ListItem("QTHT - Horas de Plantão", "5"));
                    ddlClassificacao.Items.Insert(0, new ListItem("QTUP - Unidades de Plantão", "4"));
                    ddlClassificacao.Items.Insert(0, new ListItem("Tipo de Campanha", "2"));
                    ddlClassificacao.Items.Insert(0, new ListItem("Campanha", "1"));
                    break;
                case "B":
                    ddlClassificacao.Items.Clear();
                    ddlClassificacao.Items.Insert(0, new ListItem("VTP - Valor Total do Plantão", "10"));
                    ddlClassificacao.Items.Insert(0, new ListItem("MHP - Valor Médio da Hora", "9"));
                    ddlClassificacao.Items.Insert(0, new ListItem("QTPI - Plantões Inconsistentes", "8"));
                    ddlClassificacao.Items.Insert(0, new ListItem("QTEP - Especialidades de Plantão", "7"));
                    ddlClassificacao.Items.Insert(0, new ListItem("QTTP - Total de Plantões", "6"));
                    ddlClassificacao.Items.Insert(0, new ListItem("QTHT - Horas de Plantão", "5"));
                    ddlClassificacao.Items.Insert(0, new ListItem("QTUP - Unidades de Plantão", "4"));
                    ddlClassificacao.Items.Insert(0, new ListItem("Cidade/Bairro", "3"));
                    ddlClassificacao.Items.Insert(0, new ListItem("Tipo de Campanha", "2"));
                    ddlClassificacao.Items.Insert(0, new ListItem("Campanha", "1"));
                    break;
            }
        }

        #endregion
    }
}