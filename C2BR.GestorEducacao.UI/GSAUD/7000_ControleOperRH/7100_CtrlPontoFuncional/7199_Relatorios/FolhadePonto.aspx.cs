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
using C2BR.GestorEducacao.Reports.GSAUD._7000_ControleOperRH._7100_CtrlPontoFuncional;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GSAUD._7000_ControleOperRH._7100_CtrlPontoFuncional._7199_Relatorios
{
    public partial class FolhadePonto : System.Web.UI.Page
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
                CarregaDropDown();
                CarregaFuncionarios();
                CarregaMesAno();
            }
        }

        //====> Método que faz a chamada de outro método de acordo com o Tipo de presença
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis de parâmetro do Relatório
            int codEmp, codCol, codEmpRef, anoBase, mesRefer, lRetorno;
            string infos;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            codEmp = LoginAuxili.CO_EMP;
            codEmpRef = int.Parse(ddlUnidade.SelectedValue);
            codCol = int.Parse(ddlFuncionarios.SelectedValue);
            anoBase = int.Parse(txtAnoBase.Text);
            mesRefer = int.Parse(ddlMesRefer.Text);
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptFolhadePontoS rpt = new RptFolhadePontoS();
            lRetorno = rpt.InitReport(codEmp, codEmpRef, codCol, anoBase, mesRefer, ddlMesRefer.SelectedItem.ToString(), ddlTipoColaborador.SelectedValue, infos);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        /// 

        private void CarregaMesAno()
        {
            txtAnoBase.Text = DateTime.Now.Year.ToString();

            int mesCorr = DateTime.Now.Month;

            switch (mesCorr)
            {
                case 1:
                    ddlMesRefer.SelectedValue = "1";
                    break;

                case 2:
                    ddlMesRefer.SelectedValue = "2";
                    break;

                case 3:
                    ddlMesRefer.SelectedValue = "3";
                    break;

                case 4:
                    ddlMesRefer.SelectedValue = "4";
                    break;

                case 5:
                    ddlMesRefer.SelectedValue = "5";
                    break;

                case 6:
                    ddlMesRefer.SelectedValue = "6";
                    break;

                case 7:
                    ddlMesRefer.SelectedValue = "7";
                    break;

                case 8:
                    ddlMesRefer.SelectedValue = "8";
                    break;

                case 9:
                    ddlMesRefer.SelectedValue = "9";
                    break;

                case 10:
                    ddlMesRefer.SelectedValue = "10";
                    break;

                case 11:
                    ddlMesRefer.SelectedValue = "11";
                    break;

                case 12:
                    ddlMesRefer.SelectedValue = "12";
                    break;

            }

        }

        private void CarregaDropDown()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                     where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy(e => e.NO_FANTAS_EMP);

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

        /// <summary>
        /// Método que carrega o dropdown de Colaboradores
        /// </summary>
        private void CarregaFuncionarios()
        {

            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            string flaCol = ddlTipoColaborador.SelectedValue != "T" ? ddlTipoColaborador.SelectedValue : "";

            var lst =
                       from c in TB03_COLABOR.RetornaPeloCoUnid(coEmp)
                       //join m in TB286_MOVIM_TRANSF_FUNCI.RetornaTodosRegistros() on c.CO_COL equals m.TB03_COLABOR.CO_COL into l1
                       //from m in l1.DefaultIfEmpty()
                       //from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                       where c.CO_SITU_COL != "DEM"
                       //where m.CO_MOTIVO_AFAST != "DEM"
                       //where tb03.TIPO_SITU != "R"
                       select new
                       {
                           Nome = c.NO_COL + ((flaCol == "")
                             ? " - " + ((c.FLA_PROFESSOR == "N") ? "Funcionário" : "Professor") : ""),
                           c.CO_COL,
                           c.FLA_PROFESSOR

                       };

            //foreach (var dem in lst)
            //{
            //           coDem = TB03_COLABOR.RetornaTodosRegistros().Where(w => w.TB286_MOVIM_TRANSF_FUNCI
            //}

            if (flaCol != string.Empty)
                ddlFuncionarios.DataSource = lst.Where(x => x.FLA_PROFESSOR == flaCol).OrderBy(c => c.Nome);
            else
                ddlFuncionarios.DataSource = lst.OrderBy(c => c.Nome);

            ddlFuncionarios.DataTextField = "Nome";
            ddlFuncionarios.DataValueField = "CO_COL";
            ddlFuncionarios.DataBind();

            ddlFuncionarios.Items.Insert(0, new ListItem("Todos", "0"));


        }
        #endregion

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaFuncionarios();
        }

        protected void ddlTipoColaborador_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaFuncionarios();
        }
    }
}