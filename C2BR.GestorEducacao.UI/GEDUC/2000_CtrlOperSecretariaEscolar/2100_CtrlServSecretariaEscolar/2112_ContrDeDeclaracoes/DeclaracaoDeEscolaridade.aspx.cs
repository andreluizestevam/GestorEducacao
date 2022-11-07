//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: ITENS SECRETARIA
// OBJETIVO: DECLARAÇÃO DE ESCOLARIDAD
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//29/03/2013|Caio Mendonça               | Criação, cópia do frequencia


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
using C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2101_ServSolicItensServicEscolar;

namespace C2BR.GestorEducacao.UI.GEDUC._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2112_ContrDeDeclaracoes
{
    public partial class DeclaracaoDeEscolaridade : System.Web.UI.Page
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
                txtUnidadeEscola.Text = LoginAuxili.NO_FANTAS_EMP;
                ddlModalidade.Enabled = true;
                ddlSerieCurso.Enabled = true;
                ddlTurma.Enabled = true;

                CarregaAnos();
                CarregaModalidade();
                CarregaSerieCurso();
                CarregaTurma();
                CarregaAluno();
            }
        }

        /// <summary>
        /// Processo de Geração do Relatório
        /// </summary>
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            int lRetorno;

            int strCO_EMP;
            string strCO_ALU_CAD;
            string strANO;
            int strMOD_CO_CUR;
            int strCO_CUR;
            int strTUR;

            strCO_ALU_CAD = ddlAlunos.SelectedValue;
            strANO = ddlAno.SelectedValue;
            strCO_EMP = LoginAuxili.CO_EMP;
            strMOD_CO_CUR = int.Parse(ddlModalidade.SelectedValue);
            strCO_CUR = int.Parse(ddlSerieCurso.SelectedValue);
            strTUR = int.Parse(ddlTurma.SelectedValue);

           

            var lst = (from tb009 in TB009_RTF_DOCTOS.RetornaTodosRegistros()
                           from tb010 in tb009.TB010_RTF_ARQUIVO.DefaultIfEmpty()
                       where tb009.TP_DOCUM == "DE" && tb009.CO_SIGLA_DOCUM == "DXXX008ESCOL"
                           select new
                           {
                               Pagina = tb010.NU_PAGINA,
                               Titulo = tb009.NM_TITUL_DOCUM,
                               Texto = tb010.AR_DADOS
                           }).OrderBy(x => x.Pagina);


            if (lst != null && lst.Where(x => x.Pagina == 1).Any())
            {


                RptDeclaracaoDeEscolaridade rpt = new RptDeclaracaoDeEscolaridade();
                lRetorno = rpt.InitReport(LoginAuxili.CO_EMP, strCO_ALU_CAD, strANO, strMOD_CO_CUR, strCO_CUR, strTUR);
                Session["Report"] = rpt;
                Session["URLRelatorio"] = "/GeducReportViewer.aspx";

                AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Arquivo não cadastrado na tabela de Arquivo RTF - Sigla DXXX008ESCOL, Tipo de documento Declaração");
                return;
            }
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Carrega os Anos encontrados na tabela de matrícula
        /// </summary>
        private void CarregaAnos()
        {
            AuxiliCarregamentos.CarregaAno(ddlAno, LoginAuxili.CO_EMP, true);
        }

        /// <summary>
        /// Carrega as modalidades
        /// </summary>
        private void CarregaModalidade()
        {
            AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        /// <summary>
        /// Carrega Série e Curso
        /// </summary>
        private void CarregaSerieCurso()
        {
            int modalidade = int.Parse(ddlModalidade.SelectedValue);
            AuxiliCarregamentos.carregaSeriesCursos(ddlSerieCurso, modalidade, LoginAuxili.CO_EMP, true);
        }

        /// <summary>
        /// Carrega as Turmas
        /// </summary>
        private void CarregaTurma()
        {
            int modalidade = int.Parse(ddlModalidade.SelectedValue);
            int serie = int.Parse(ddlSerieCurso.SelectedValue);
            AuxiliCarregamentos.CarregaTurmas(ddlTurma, LoginAuxili.CO_EMP, modalidade, serie, true);
        }

        /// <summary>
        /// Carrega os Alunos 
        /// </summary>
        private void CarregaAluno()
        {
            int modalidade = int.Parse(ddlModalidade.SelectedValue);
            int serie = int.Parse(ddlSerieCurso.SelectedValue);
            int turma = int.Parse(ddlTurma.SelectedValue);
            string ano = ddlAno.SelectedValue;
            ddlAlunos.Items.Clear();
            AuxiliCarregamentos.CarregaAlunoMatriculado(ddlAlunos, LoginAuxili.CO_EMP, modalidade, serie, turma, ano, true);
        }

        #endregion

        #region Eventos de componentes da página

        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaModalidade();
            CarregaAluno();
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
                CarregaSerieCurso();
                CarregaAluno();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
                CarregaTurma();
                CarregaAluno();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
                CarregaAluno();
        }

        #endregion

    }
   
}
