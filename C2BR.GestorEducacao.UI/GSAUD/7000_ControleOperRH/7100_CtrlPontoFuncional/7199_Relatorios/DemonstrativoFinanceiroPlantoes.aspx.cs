//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: GESTÃO OPERACIONAL DE FREQUÊNCIA
// OBJETIVO: Emissão de demonstrativo financeiro de plantões
// DATA DE CRIAÇÃO: 06/10/2014
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
using C2BR.GestorEducacao.Reports.GSAUD._7000_ControleOperRH._7100_CtrlPontoFuncional._7130_CtrlPlantoes;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GSAUD._7000_ControleOperRH._7100_CtrlPontoFuncional._7199_Relatorios
{
    public partial class DemonstrativoFinanceiroPlantoes : System.Web.UI.Page
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
                CarregaUnidade(ddlUnidadeColab);
                CarregaUnidade(ddlUnidadePlantao);
                CarregaEspecialidades();
                CarregaColaboradores();
                CarregaDepartament();
                CarregaTiposContrato();
            }
        }

        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis de parâmetro do Relatório
            string infos, parametros, situacao;
            int lRetorno, coUnidFunc, coUnidPlant, coEspecPlant, coCol, coDepto, coOrdenacao, coTipoContrato;
            string noUnidadeFuncional, noUnidadePlantao, noEspecialidade, noColaborador, noPeriodo, noOrdenacao;

            //--------> Inserção de valores nas variáveis
            coUnidFunc = ddlUnidadeColab.SelectedValue != "" ? int.Parse(ddlUnidadeColab.SelectedValue) : 0;
            coUnidPlant = ddlUnidadePlantao.SelectedValue != "" ? int.Parse(ddlUnidadePlantao.SelectedValue) : 0;
            coEspecPlant = ddlEspecPlant.SelectedValue != "" ? int.Parse(ddlEspecPlant.SelectedValue) : 0;
            coCol = ddlNomeProfissional.SelectedValue != "" ? int.Parse(ddlNomeProfissional.SelectedValue) : 0;
            coDepto = ddlLocal.SelectedValue != "" ? int.Parse(ddlLocal.SelectedValue) : 0;
            coTipoContrato = ddlTipoContratoColab.SelectedValue != "" ? int.Parse(ddlTipoContratoColab.SelectedValue) : 0;
            situacao = ddlSituaFuncional.SelectedItem.Value;
            coOrdenacao = int.Parse( ddlClassificacao.SelectedValue);

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            noUnidadeFuncional = (coUnidFunc != 0 ? TB25_EMPRESA.RetornaPelaChavePrimaria(coUnidFunc).sigla : "Todos");
            noUnidadePlantao = (coUnidPlant != 0 ? TB25_EMPRESA.RetornaPelaChavePrimaria(coUnidPlant).sigla : "Todos");
            noEspecialidade = (coEspecPlant != 0 ? TB63_ESPECIALIDADE.RetornaTodosRegistros().Where(o => o.CO_ESPECIALIDADE == coEspecPlant).FirstOrDefault().NO_ESPECIALIDADE : "Todos");
            noColaborador = (coCol != 0 ? TB03_COLABOR.RetornaTodosRegistros().Where(o => o.CO_COL == coCol).FirstOrDefault().NO_COL : "Todos");
            noOrdenacao = ddlClassificacao.SelectedItem.Text;

            DateTime dtini = DateTime.Parse(txtIniPeri.Text);
            DateTime dtfim = DateTime.Parse(txtFimPeri.Text);

            parametros = "( Unid Func: " + noUnidadeFuncional.ToUpper() + " - Unid Plantão: "
                + noUnidadePlantao.ToUpper() + " - Especialidade: " + noEspecialidade.ToUpper() 
                + " - Colaborador: " + noColaborador + " - Tipo Contrato: " + ddlTipoContratoColab.SelectedItem.Text 
                + " - Relatório ordenado por " + noOrdenacao  + " - " + ddlTipoOrdem.SelectedItem.Text + " )";

            noPeriodo = " - Período: " + dtini.ToString("dd/MM/yy") + " à " + dtfim.ToString("dd/MM/yy");
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            //RptExtratoPlantaoColabor rpt = new RptExtratoPlantaoColabor();
            //lRetorno = rpt.InitReport(parametros, infos, LoginAuxili.CO_EMP, coUnidFunc, coUnidPlant, coEspecPlant, coCol, situacao, txtIniPeri.Text, txtFimPeri.Text);

            RptDemonFinanPlantoes rpt = new RptDemonFinanPlantoes();
            lRetorno = rpt.InitReport(parametros, infos, LoginAuxili.CO_EMP, coUnidFunc, coUnidPlant, coEspecPlant, coCol, situacao, txtIniPeri.Text, txtFimPeri.Text, coDepto, noPeriodo, coOrdenacao, coTipoContrato, ddlTipoOrdem.SelectedValue);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        /// <summary>
        /// Carrega as Unidades
        /// </summary>
        private void CarregaUnidade(DropDownList ddl)
        {
            AuxiliCarregamentos.CarregaUnidade(ddl, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        /// <summary>
        /// Carrega os Departamentos
        /// </summary>
        private void CarregaDepartament()
        {
            int coEmp = ddlUnidadePlantao.SelectedValue != "" ? int.Parse(ddlUnidadePlantao.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaDepartamentos(ddlLocal, coEmp, true);
        }

        /// <summary>
        /// Método que carrega o dropdown de Tipo de Contrato
        /// </summary>
        private void CarregaTiposContrato()
        {
            AuxiliCarregamentos.CarregaTiposContrato(ddlTipoContratoColab, true);
        }

        /// <summary>
        /// Carrega as Especiliadades
        /// </summary>
        private void CarregaEspecialidades()
        {
            int coEmp = ddlUnidadePlantao.SelectedValue != "" ? int.Parse(ddlUnidadePlantao.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaEspeciacialidades(ddlEspecPlant, coEmp, null, true);
        }

        /// <summary>
        /// Carrega os colaboradores de acordo com o informado
        /// </summary>
        private void CarregaColaboradores()
        {
            int CO_EMP = ddlUnidadeColab.SelectedValue != "" ? int.Parse(ddlUnidadeColab.SelectedValue) : 0;
            int tipoContr = ddlTipoContratoColab.SelectedValue != "" ? int.Parse(ddlTipoContratoColab.SelectedValue) : 0;
            string situ = ddlSituaFuncional.SelectedValue;

            var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                       where tb03.TB25_EMPRESA.CO_EMP == CO_EMP
                       && (situ != "0" ? tb03.CO_SITU_COL == situ : 0 == 0)
                       && (tipoContr != 0 ? tb03.CO_TPCON == tipoContr : 0 == 0)
                       select new { tb03.CO_COL, tb03.NO_COL }).OrderBy(o => o.NO_COL).ToList();

            ddlNomeProfissional.DataTextField = "NO_COL";
            ddlNomeProfissional.DataValueField = "CO_COL";
            ddlNomeProfissional.DataSource = res;
            ddlNomeProfissional.DataBind();

            ddlNomeProfissional.Items.Insert(0, new ListItem("Todos", "0"));
        }

        protected void ddlEspec_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaColaboradores();
        }

        protected void ddlUnidade_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaColaboradores();
        }

        protected void ddlSituaFuncional_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaColaboradores();
        }

        protected void ddlTipoContratoColab_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaColaboradores();
        }
    }
}
