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
//    DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// 02/04/2014 |  Julio Gleisson            | Copia do Relatorio HistorFinanceAlunos do GEDUC                          
// ---------+----------------------------+-------------------------------------

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
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3030_CtrlMonitoria._3031_RptAgendaMonitoria;

namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3030_CtrlMonitoria._3099_ExtratoMonitoria
{
    public partial class RelacaoDeMonitores : System.Web.UI.Page
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
            if (! IsPostBack)
            {
                this.CarregaUnidade();
                this.CarregaEspecialidade();
            }
           
        }

        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis de parâmetro do Relatório
            int codEmp;
            int codEmpRef;
            string infos, parametros;
            int lRetorno;
            int cod_Espc;
            string no_unidade;
         
            


            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            codEmp = LoginAuxili.CO_EMP;
            codEmpRef = int.Parse(ddlUnidade.SelectedValue);
            cod_Espc = int.Parse(ddlEspecialidade.SelectedValue);
            
            
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

           


            no_unidade = (codEmpRef != 0 ? no_unidade = TB25_EMPRESA.RetornaPelaChavePrimaria(codEmpRef).NO_FANTAS_EMP : no_unidade = "Todas");

            parametros = "( Unidade: " + no_unidade.ToUpper() + " - Especialidade: " + ddlEspecialidade.SelectedItem.Text.ToUpper() + " )";


            RptRelacaoDeMonitores rpt = new RptRelacaoDeMonitores();
            lRetorno = rpt.InitReport(codEmp, codEmpRef, infos, parametros, cod_Espc);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        void CarregaUnidade()
        {
            var res = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                       select new
                       {
                           tb25.CO_EMP,
                           tb25.NO_FANTAS_EMP
                       });

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";

            ddlUnidade.DataSource = res;
            ddlUnidade.DataBind();

            ddlUnidade.Items.Insert(0, new ListItem("Todos", "0"));
        }
        void CarregaEspecialidade()
        {
            var res = (from tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros()
                       select new
                       {
                           tb63.CO_ESPECIALIDADE,
                           tb63.NO_ESPECIALIDADE

                       });

            ddlEspecialidade.DataValueField = "CO_ESPECIALIDADE";
            ddlEspecialidade.DataTextField = "NO_ESPECIALIDADE";

            ddlEspecialidade.DataSource = res;
            ddlEspecialidade.DataBind();

            ddlEspecialidade.Items.Insert(0, new ListItem("Todos", "0"));
        }


    }
}