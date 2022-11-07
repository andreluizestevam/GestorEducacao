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
    public partial class QuadroPlantonistas : System.Web.UI.Page
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
                CarregaEspecialidades();
                CarregaDepartamentos();
                CarregaColaboradores();
                CarregaTiposContrato();
            }
            else
            {
                if (ddlColaborador.SelectedValue != "0")
                {
                    ddlClassificacao.Enabled = false;
                    ddlClassificacao.SelectedValue = "1";
                }
                else
                    ddlClassificacao.Enabled = true;
            }
        }

        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis de parâmetro do Relatório
            string infos, parametros, situacao;
            int lRetorno, coUnid, coDept, coEspec, coFun, coCol, classificacao, coTipoContrato;
            string noUnidade, noDepartamento, noEspecialidade, noFuncao, noColaborador, noSituacao, noClass, noPeriodo;

            coUnid = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            coDept = ddlDept.SelectedValue != "" ? int.Parse(ddlDept.SelectedValue) : 0;
            coEspec = ddlEspec.SelectedValue != "" ? int.Parse(ddlEspec.SelectedValue) : 0;
            coCol = ddlColaborador.SelectedValue != "" ? int.Parse(ddlColaborador.SelectedValue) : 0;
            coTipoContrato = ddlTipoContratoColab.SelectedValue != "" ? int.Parse(ddlTipoContratoColab.SelectedValue) : 0;
            classificacao = int.Parse(ddlClassificacao.SelectedValue);
            situacao = ddlSituacao.SelectedItem.Value;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            noUnidade = (coUnid != 0 ? TB25_EMPRESA.RetornaPelaChavePrimaria(coUnid).sigla : "Todos");
            noDepartamento = (coDept != 0 ? TB14_DEPTO.RetornaPelaChavePrimaria(coDept).CO_SIGLA_DEPTO : "Todos");
            noEspecialidade = (coEspec != 0 ? TB63_ESPECIALIDADE.RetornaTodosRegistros().Where(o=>o.CO_ESPECIALIDADE == coEspec).FirstOrDefault().NO_ESPECIALIDADE : "Todos");
            noColaborador = (coCol != 0 ? TB03_COLABOR.RetornaTodosRegistros().Where(o=>o.CO_COL == coCol).FirstOrDefault().NO_COL : "Todos");
            noClass = ddlClassificacao.SelectedItem.Text;

            switch(situacao)
            {
                case "A":
                    noSituacao = "Aberto";
                    break;
                case "C":
                    noSituacao = "Cancelado";
                    break;
                case "R":
                    noSituacao = "Realizado";
                    break;
                case "P":
                    noSituacao = "Planejado";
                    break;
                default:
                    noSituacao = "Todos";
                    break;
            }

            DateTime dtini = DateTime.Parse(IniPeri.Text);
            DateTime dtfim = DateTime.Parse(FimPeri.Text);

            parametros = "( Unid: " + noUnidade.ToUpper() + " - Depto: " + noDepartamento.ToUpper() + " - Especialidade: " 
                + noEspecialidade.ToUpper() + " - Situação: " + noSituacao.ToUpper() 
                 + " - Tipo Contrato: " + ddlTipoContratoColab.SelectedItem.Text + " - Relatório ordenado por " + noClass + " )";

            noPeriodo = dtini.ToString("dd/MM/yy") + " à " + dtfim.ToString("dd/MM/yy");

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptQuadroPlantonistas rpt = new RptQuadroPlantonistas();
            lRetorno = rpt.InitReport(parametros, infos, LoginAuxili.CO_EMP, coUnid, coDept, coEspec, coCol, situacao, classificacao, IniPeri.Text, FimPeri.Text, noPeriodo, coTipoContrato); 
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        /// <summary>
        /// Carrega as Unidades
        /// </summary>
        private void CarregaUnidade()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        /// <summary>
        /// Carrega os departamentos
        /// </summary>
        private void CarregaDepartamentos()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaDepartamentos(ddlDept, coEmp, true);
        }

        /// <summary>
        /// Carrega as Especiliadades
        /// </summary>
        private void CarregaEspecialidades()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaEspeciacialidades(ddlEspec, coEmp, null, true);
        }

        /// <summary>
        /// Método que carrega o dropdown de Tipo de Contrato
        /// </summary>
        private void CarregaTiposContrato()
        {
            AuxiliCarregamentos.CarregaTiposContrato(ddlTipoContratoColab, true);
        }

        /// <summary>
        /// Carrega os colaboradores de acordo com o informado
        /// </summary>
        private void CarregaColaboradores()
        {
            int CO_EMP = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int CO_ESPEC = ddlEspec.SelectedValue != "" ? int.Parse(ddlEspec.SelectedValue) : 0;
            int tpCon = ddlTipoContratoColab.SelectedValue != "" ? int.Parse(ddlTipoContratoColab.SelectedValue) : 0;

            var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                       where ( CO_EMP != 0 ? tb03.TB25_EMPRESA.CO_EMP == CO_EMP : 0 == 0)
                       && (CO_ESPEC != 0 ? tb03.CO_ESPEC == CO_ESPEC : 0 == 0)
                       && (tpCon != 0 ? tb03.CO_TPCON == tpCon : 0 == 0)
                       select new { tb03.CO_COL, tb03.NO_COL }).OrderBy(o=>o.NO_COL).ToList();

            ddlColaborador.DataTextField = "NO_COL";
            ddlColaborador.DataValueField = "CO_COL";
            ddlColaborador.DataSource = res;
            ddlColaborador.DataBind();

            ddlColaborador.Items.Insert(0, new ListItem("Todos", "0"));
        }

        protected void ddlEspec_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaColaboradores();
        }

        protected void ddlUnidade_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaColaboradores();
        }

        protected void ddlTipoContratoColab_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaColaboradores();
        }
    }
}