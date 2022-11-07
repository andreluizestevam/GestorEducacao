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
    public partial class ExtratoPantao : System.Web.UI.Page
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
                CarregaSituacao();
                CarregaEspec();
                CarregaDepartamentos();
            }
        }

        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis de parâmetro do Relatório
            int codEmp;
            int codEmpRef;
            string infos, parametros, situacao;
            int lRetorno, coEspec, coDepto, ordenacao;
            string no_unidade, noEspec, noDepto, noOrdenacao;
            string no_situacao;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            codEmp = LoginAuxili.CO_EMP;
            codEmpRef = (ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0);
            coEspec = (ddlEspec.SelectedValue != "" ? int.Parse(ddlEspec.SelectedValue) : 0);
            coDepto = (ddlLocal.SelectedValue != "" ? int.Parse(ddlLocal.SelectedValue) : 0);
            situacao = ddlSituacao.SelectedValue;
            ordenacao = int.Parse(ddlOrdenacao.SelectedValue);
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            no_unidade = ( codEmpRef != 0 ? no_unidade =  TB25_EMPRESA.RetornaPelaChavePrimaria(codEmpRef).sigla : no_unidade = "Todos");
            no_situacao = ( situacao != "A"? no_situacao = "Inativo" : no_situacao = "Ativo");
            noEspec = (coEspec != 0 ? TB63_ESPECIALIDADE.RetornaTodosRegistros().Where(u=>u.CO_ESPECIALIDADE == coEspec).FirstOrDefault().NO_SIGLA_ESPECIALIDADE : "Todos");
            noDepto = (coDepto != 0 ? TB14_DEPTO.RetornaPelaChavePrimaria(coDepto).CO_SIGLA_DEPTO : "Todos");
            noOrdenacao = ddlOrdenacao.SelectedItem.Text;

            parametros = "( Unidade: " + no_unidade.ToUpper() + " - Local: " + noDepto.ToUpper() + " - Especialidade: " + noEspec.ToUpper()
                + " - Situação: " + no_situacao.ToUpper() + " - Relatório ordenado por " + noOrdenacao + " )";

            RptExtratoPlantao rpt = new RptExtratoPlantao();
            lRetorno = rpt.InitReport(parametros, infos, codEmp, codEmpRef, situacao, coDepto, coEspec, ordenacao);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        /// <summary>
        /// Carrega as unidades
        /// </summary>
        private void CarregaUnidade()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        /// <summary>
        /// Carrega as situações
        /// </summary>
        private void CarregaSituacao()
        {
            AuxiliCarregamentos.CarregaSituacaoTipoPlantao(ddlSituacao, true);
        }

        /// <summary>
        /// Carrega as especialidades
        /// </summary>
        private void CarregaEspec()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaEspeciacialidades(ddlEspec, coEmp, null, true);
        }

        /// <summary>
        /// Carrega os Departamentos
        /// </summary>
        private void CarregaDepartamentos()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaDepartamentos(ddlLocal, coEmp, true);
        }
    }
}