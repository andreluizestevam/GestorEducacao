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
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3600_CtrlInformacoesAlunos;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3299_Relatorios
{
    public partial class CarteiraSaude : System.Web.UI.Page
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
                carregaPaciente();

            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório

            int lRetorno, coEmp, coModu, coCur, coTur, coAlu = 0;
            string infos, parametros, coAno, dtValidade = "";
            parametros = "";
            coAno = "2014";
            coModu = 0;
            coCur = 0;
            coTur = 0;
            dtValidade = "05/06/2015";

            coAlu = int.Parse(ddlPaciente.SelectedValue);
            coEmp = LoginAuxili.CO_EMP;

            //RptCarteiraEstudantil rpt = new RptCarteiraEstudantil();
            
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            //parametros = "(Ano: " + coAno + " - Unidade: " + noEmp + " - Modalidade: " + deModu + " - Série/Curso: " + noCur + " - Turma: " + noTur + " - Aluno: " + noAlu + ")";

            RptCarteiraEstudantil rpt = new RptCarteiraEstudantil();
            lRetorno = rpt.InitReport(infos, parametros, coAno, coEmp, coModu, coCur, coTur, coAlu, dtValidade);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        #endregion

        private void carregaPaciente()
        {
            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       select new { tb07.NO_ALU, tb07.CO_ALU });

            ddlPaciente.DataTextField = "NO_ALU";
            ddlPaciente.DataValueField = "CO_ALU";
            ddlPaciente.DataSource = res;
            ddlPaciente.DataBind();

            ddlPaciente.Items.Insert(0, new ListItem("Selecione", ""));
        }
    }
}