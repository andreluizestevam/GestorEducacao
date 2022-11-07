//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: GESTÃO DE DADOS DE ALUNOS
// OBJETIVO: RELAÇÃO DE ALUNOS POR SÉRIE/TURMA
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
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
using C2BR.GestorEducacao.Reports._GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario._3199_Relatorios
{
    public partial class CarteiraIdentificacaoBeneficiario : System.Web.UI.Page
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
                CarregaUnidade();
                CarregaAluno();
            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório

            int lRetorno, coEmp, coAlu = 0, coEmpLog;
            string infos, parametros, noEmp, noAlu, dtValidade = "";
            coEmp = int.Parse(ddlUnidade.SelectedValue);
            coEmpLog = LoginAuxili.CO_EMP;          
            coAlu = int.Parse(ddlAluno.SelectedValue);
            dtValidade = txtValidade.Text;

            noEmp = (coEmp != 0 ? TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp).NO_FANTAS_EMP : "TODOS");
            noAlu = (coAlu != 0 && coEmp != 0 ? TB07_ALUNO.RetornaPelaChavePrimaria(coAlu, coEmp).NO_ALU : "TODOS");

            RptCarteiraIdentificacaoBeneficiario rpt = new RptCarteiraIdentificacaoBeneficiario();

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            parametros = "(Unidade: " + noEmp + " - Beneficiário: " + noAlu + ")";

            lRetorno = rpt.InitReport(infos, parametros, coEmp, coEmpLog, coAlu, dtValidade);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }
        #endregion

        #region Carregamento  
        /// <summary>
        /// Método que carrega as unidades cadastradas na combo ddlUnidade
        /// </summary>
        protected void CarregaUnidade()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, true);         
        }      
        /// <summary>
        /// Método que carrega os alunos matriculados, na turma selecionada, na combo ddlAluno
        /// </summary>
        protected void CarregaAluno()
        {        
            int coEmp = int.Parse(ddlUnidade.SelectedValue);
            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       where (coEmp != 0 ? tb07.CO_EMP == coEmp : 0 == 0)            
                       select new
                       {
                           tb07.NO_ALU,
                           tb07.CO_ALU
                       }).Distinct().OrderBy(o => o.NO_ALU);

            if (res != null)
            {
                ddlAluno.DataTextField = "NO_ALU";
                ddlAluno.DataValueField = "CO_ALU";
                ddlAluno.DataSource = res;
                ddlAluno.DataBind();
            }       
                ddlAluno.Items.Insert(0, new ListItem("Todos", "0"));
        }

        #endregion

        #region Funções de Campo   

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAluno();
        }
        #endregion
    }
}