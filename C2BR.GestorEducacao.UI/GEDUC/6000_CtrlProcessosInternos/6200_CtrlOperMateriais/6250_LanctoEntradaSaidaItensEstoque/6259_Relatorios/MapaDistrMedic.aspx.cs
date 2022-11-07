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
//25/04/2014 |   Maxwell Almeida         |    Criação da Página.

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
using C2BR.GestorEducacao.Reports.GSAUD._6000_GestaoMedicamentos._6200_CtrlMovimento;

namespace C2BR.GestorEducacao.UI.GEDUC._6000_CtrlProcessosInternos._6200_CtrlOperMateriais._6250_LanctoEntradaSaidaItensEstoque._6259_Relatorios
{
    public partial class MapaDistrMedic : System.Web.UI.Page
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
                CarregaUnidades();
            }
        }

        #endregion

        #region Carregamentos

        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {

            string parametros;
            string infos;
            int coUnid, lRetorno;

            coUnid = ddlUnid.SelectedValue != "" ? int.Parse(ddlUnid.SelectedValue) : 0;

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            parametros = "( Unidade: " + ddlUnid.SelectedItem.Text + " - Período: " + IniPeri.Text + " à " + FimPeri.Text + " )";

            RptMapaDistriMedic fpcb = new RptMapaDistriMedic();
            lRetorno = fpcb.InitReport(parametros, infos, LoginAuxili.CO_EMP, coUnid, 0, IniPeri.Text, FimPeri.Text);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);


        }

        #endregion

        private void CarregaUnidades()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnid, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }
    }
}