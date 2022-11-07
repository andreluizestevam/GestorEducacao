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
    public partial class RelacEquipeCampanha : System.Web.UI.Page
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
                CarregaUnidade();
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

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            coUnid = (ddlUnid.SelectedValue != "" ? int.Parse(ddlUnid.SelectedValue) : 0);
            coTipo = ddlTipoCamp.SelectedValue;
            dataIni = txtDataIniCamp.Text;
            dataFim = txtDataFimCamp.Text;

            parametros = "( Unidade: " + ddlUnid.SelectedItem.Text.ToUpper() + " - Tipo: " + ddlTipoCamp.SelectedItem.Text.ToUpper() +
                " - Período: " + dataIni + " à " + dataFim + " )";

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptEquipeCampanha rpt = new RptEquipeCampanha();
            lRetorno = rpt.InitReport(parametros, infos, LoginAuxili.CO_EMP, coUnid, coTipo, dataIni, dataFim);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        /// <summary>
        /// Carrega as Unidades
        /// </summary>
        protected void CarregaUnidade()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnid, LoginAuxili.ORG_CODIGO_ORGAO, true, true, false);
        }

        private void carregaTipos()
        {
            AuxiliCarregamentos.CarregaTiposCampanhaSaude(ddlTipoCamp, true);
        }

        #endregion

        #region Funções de Campo

        #endregion
    }
}