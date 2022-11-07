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
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 21/03/2013| André Nobre Vinagre        | Correção - Tratamento de data que estava vindo vazia
//           |                            | no filtro e ocasionando erro grave.
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
using C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5500_GeraFluxoCaixa;
using C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5100_CtrlTesouraria;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5100_CtrlTesouraria.F5199_Relatorios
{
    public partial class MovimentoCaixa : System.Web.UI.Page
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
                CarregaCaixa();
                CarregaDataMovto();
                CarregaDataPagto();
            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            string strParametrosRelatorio, strINFOS, situCaixa;
            DateTime dtMovto;
            DateTime? dtPagto;
            int codEmp, codCaixa;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strParametrosRelatorio = "";
            codEmp = LoginAuxili.CO_EMP;
            codCaixa = int.Parse(ddlCaixa.SelectedValue);
            dtMovto = DateTime.Parse(ddlDataMovto.SelectedValue);
            dtPagto = ddlDataPagto.SelectedValue != "" ? (DateTime?)DateTime.Parse(ddlDataMovto.SelectedValue) : null;
            situCaixa = ddlSituCaixa.SelectedValue;
            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);


            strParametrosRelatorio = "(Movimento do dia: " + ddlDataMovto.SelectedValue + " - Caixa: " + TB113_PARAM_CAIXA.RetornaPeloCoCaixa(int.Parse(ddlCaixa.SelectedValue)).DE_CAIXA
                    + " - Situação: " + ddlSituCaixa.SelectedItem.ToString() + " - Data Pagamento: " + ddlDataPagto.SelectedValue + ")";

            // Acrescenta os horários/datas escolhidas
            List<DateTime> datas = new List<DateTime>();
            foreach (ListItem check in checkLista.Items)
            {
                if (check.Selected)
                {
                    datas.Add(Convert.ToDateTime(check.Value.ToString()));
                }
            }

            if (datas.Count == 0)
            {
                datas.Add(dtMovto);
            }
            
            Boolean alt = chkAlter.Checked;
            RptMovimentoCaixa rpt = new RptMovimentoCaixa();
            lRetorno = rpt.InitReport(strParametrosRelatorio, codEmp, dtMovto, dtPagto, codCaixa, situCaixa, strINFOS, datas,alt);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaCaixa()
        {
            string situCaixa = (ddlSituCaixa.SelectedValue != "" ? ddlSituCaixa.SelectedValue : "T");
            ddlCaixa.DataSource = (from tb295 in TB295_CAIXA.RetornaTodosRegistros()
                                   join tb113 in TB113_PARAM_CAIXA.RetornaTodosRegistros() on tb295.CO_CAIXA equals tb113.CO_CAIXA
                                    where (situCaixa != "T" ? tb113.CO_FLAG_USO_CAIXA == situCaixa : 0 == 0)
                                    select new
                                    {
                                        tb113.CO_CAIXA,
                                        CAIXA = tb113.DE_CAIXA + " - " + tb295.TB03_COLABOR.NO_COL
                                    }).ToList().Distinct();

            ddlCaixa.DataTextField = "CAIXA";
            ddlCaixa.DataValueField = "CO_CAIXA";
            ddlCaixa.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Data de Movimento
        /// </summary>
        private void CarregaDataMovto()
        {
            int coCaixa = ddlCaixa.SelectedValue != "" ? int.Parse(ddlCaixa.SelectedValue) : 0;

            var vTb295 = (from tb295 in TB295_CAIXA.RetornaTodosRegistros()
                          where tb295.CO_CAIXA == coCaixa
                          && (ddlSituCaixa.Text != "T" ? (ddlSituCaixa.SelectedValue == "A" ? tb295.DT_FECHAMENTO_CAIXA == null : tb295.DT_FECHAMENTO_CAIXA != null) : ddlSituCaixa.SelectedValue == "T")
                          select new { tb295.DT_MOVIMENTO }).ToList().Distinct();

            ddlDataMovto.DataSource = (from iTb295 in vTb295
                                       select new { DT_MOVTO_CAIXA = iTb295.DT_MOVIMENTO.ToString("dd/MM/yyyy") }).Distinct();

            ddlDataMovto.DataTextField = "DT_MOVTO_CAIXA";
            ddlDataMovto.DataValueField = "DT_MOVTO_CAIXA";
            ddlDataMovto.DataBind();

            CarregaHorarios();
        }

        /// <summary>
        /// Método que carrega o dropdown de Data de Pagamento
        /// </summary>
        private void CarregaDataPagto()
        {
            int coCaixa = ddlCaixa.SelectedValue != "" ? int.Parse(ddlCaixa.SelectedValue) : 0;
            DateTime dataMovto = ddlDataMovto.SelectedValue != "" ? DateTime.Parse(ddlDataMovto.SelectedValue) : DateTime.Parse("01/01/1950");                

            var vTb296 = (from tb296 in TB296_CAIXA_MOVIMENTO.RetornaTodosRegistros()
                          where tb296.CO_CAIXA == coCaixa
                          && tb296.DT_MOVIMENTO.Year == dataMovto.Year && tb296.DT_MOVIMENTO.Month == dataMovto.Month
                          && tb296.DT_MOVIMENTO.Day == dataMovto.Day
                          select new { tb296.DT_PAGTO_DOC }).ToList().Distinct();

            ddlDataPagto.DataSource = (from iTb296 in vTb296
                                       select new { DT_PAGTO_DOC = iTb296.DT_PAGTO_DOC.Value.ToString("dd/MM/yyyy") }).Distinct();

            ddlDataPagto.DataTextField = "DT_PAGTO_DOC";
            ddlDataPagto.DataValueField = "DT_PAGTO_DOC";
            ddlDataPagto.DataBind();

            ddlDataPagto.Items.Insert(0, new ListItem("Todas", ""));
        }

      /*  private void CarregaHorarioAbertura()
        {
            int coCaixa = ddlCaixa.SelectedValue != "" ? int.Parse(ddlCaixa.SelectedValue) : 0;
            DateTime dataMovto = ddlDataMovto.SelectedValue != "" ? DateTime.Parse(ddlDataMovto.SelectedValue) : DateTime.Parse("01/01/1950");
            ddlHorarioAbertura.DataSource = (from tb295 in TB295_CAIXA.RetornaTodosRegistros()
                          where tb295.CO_CAIXA == coCaixa
                          && tb295.DT_ABERTURA_CAIXA.Year == dataMovto.Year && tb295.DT_ABERTURA_CAIXA.Month == dataMovto.Month
                          && tb295.DT_ABERTURA_CAIXA.Day == dataMovto.Day
                          select new { tb295.HR_ABERTURA_CAIXA }).ToList().Distinct();
            ddlHorarioAbertura.DataBind();
        }
        */
        #endregion

        protected void ddlCaixa_SelectedIndexChanged(object sender, EventArgs e)
        {

            CarregaDataMovto();
            CarregaDataPagto();
        }

        protected void ddlSituCaixa_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaDataMovto();
            CarregaDataPagto();
        }

        protected void ddlDataMovto_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaDataPagto();
            CarregaHorarios();
            //CarregaHorarioAbertura();
        }

        protected void CarregaHorarios()
        {
            if (ddlDataMovto.SelectedValue != "")
            {
                DateTime dtMovto = Convert.ToDateTime(ddlDataMovto.SelectedValue);

                int coCaixa = ddlCaixa.SelectedValue != "" ? int.Parse(ddlCaixa.SelectedValue) : 0;

                var vTb295 = (from tb295 in TB295_CAIXA.RetornaTodosRegistros()
                              where tb295.CO_CAIXA == coCaixa
                              && (ddlSituCaixa.Text != "T" ? (ddlSituCaixa.SelectedValue == "A" ? tb295.DT_FECHAMENTO_CAIXA == null : tb295.DT_FECHAMENTO_CAIXA != null) : ddlSituCaixa.SelectedValue == "T")
                              && (tb295.DT_MOVIMENTO.Year == dtMovto.Year && tb295.DT_MOVIMENTO.Month == dtMovto.Month
                                    && tb295.DT_MOVIMENTO.Day == dtMovto.Day)
                              select new
                              {
                                  tb295.DT_MOVIMENTO
                              }
                              ).ToList().Distinct();

                checkLista.DataSource = (from iTb295 in vTb295
                                         select new
                                         {
                                             horario = (iTb295.DT_MOVIMENTO.Hour < 10 ? "0" + iTb295.DT_MOVIMENTO.Hour.ToString() : iTb295.DT_MOVIMENTO.Hour.ToString()) + ":" + (iTb295.DT_MOVIMENTO.Minute < 10 ? "0" + iTb295.DT_MOVIMENTO.Minute.ToString() : iTb295.DT_MOVIMENTO.Minute.ToString()) + ":" + (iTb295.DT_MOVIMENTO.Second < 10 ? "0" + iTb295.DT_MOVIMENTO.Second.ToString() : iTb295.DT_MOVIMENTO.Second.ToString()),
                                             data = iTb295.DT_MOVIMENTO
                                         }
                                         ).Distinct();
                checkLista.DataBind();
                liCheck.Visible = true;
            }           
        }
    }
}
