//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: MATRÍCULA NOVA PARA ALUNO
// OBJETIVO: COMPROVANTE DE MATRÍCULA NOVA DE ALUNO
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
// 26/01/2015|  Maxwell Almeida          | Criação da funcionalidade para emissão da Ata de Resultados Finais

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
using C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8300_CtrlPreAtendimentos;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução


namespace C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3299_Relatorios
{
    public partial class ExtratPreAtendimentos : System.Web.UI.Page
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
                CarregaUnidades();
                CarregaEspecialidades();
                CarregaClasseRisco();
            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            string parametros, infos;
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            int coUnid = 0;
            parametros = "( Unidade: " + ddlUnidade.SelectedItem.Text + " - Exame: " + ddlUnidade.SelectedItem.Text + " -  Perìodo : " + txtDataInicial.Text + " a " + txtDataFinal.Text + ")";

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            coUnid = (ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0);
            int coClasseDeRisco = (ddlClasseDeRisco.SelectedValue != "" ? int.Parse(ddlClasseDeRisco.SelectedValue) : 0);
            int coEspecialidade = (ddlEspecialidade.SelectedValue != "" ? int.Parse(ddlEspecialidade.SelectedValue) : 0);
          
            string Sexo = ddlSexo.SelectedValue;
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            //Parte responsável por coletar o nome da funcionalidade cadastrada no banco e mandar como parâmetro para o 
            //relatório usar no cabeçalho
            #region
            string NO_RELATORIO = "";
            var res = (from adm in ADMMODULO.RetornaTodosRegistros()//
                       where adm.nomURLModulo == "GSAUD/3000_ControleInformacoesUsuario/3200_ControleAtendimentoUsuario/3299_Relatorios/ExtratPreAtendimentos.aspx"
                       select new { adm.nomItemMenu }).FirstOrDefault();
            NO_RELATORIO = (res != null ? res.nomItemMenu : "");
            #endregion
            
            RptExtratoPreAtendimentos rpt = new RptExtratoPreAtendimentos();
            
            lRetorno = rpt.InitReport(parametros, infos, LoginAuxili.CO_EMP, coUnid, coEspecialidade, coClasseDeRisco, Sexo, txtDataInicial.Text, txtDataFinal.Text, NO_RELATORIO.ToUpper());




            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

           AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }


        #endregion
        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown das Unidades
        /// </summary>
        private void CarregaUnidades()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        public void CarregaClasseRisco()
        {
         AuxiliCarregamentos.CarregaClassificacaoRisco(ddlClasseDeRisco, true);
        
        }
        
        public void CarregaEspecialidades()
        {
            int coEmp = (!string.IsNullOrEmpty(ddlEspecialidade.SelectedValue) ? int.Parse(ddlEspecialidade.SelectedValue) : 0);
            AuxiliCarregamentos.CarregaEspeciacialidades(ddlEspecialidade, coEmp, null, true);
        }

       

        #endregion




    }
}