//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: GESTÃO DE DADOS DE ALUNOS
// OBJETIVO: RELAÇÃO DE MENSALIDADES DE ALUNOS POR TURMA
// DATA DE CRIAÇÃO: 18/02/2014
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//   DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
//  18/02/14 | Vinicius Reis              | Criação do relatório de mensalidades
//           |                            | de alunos por turma

using System;
using System.Collections.Generic;
using System.Linq;
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
using C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5200_CtrlReceitas;

namespace C2BR.GestorEducacao.UI.GEDUC._5000_CtrlFinanceira._5200_CtrlReceitas._5299_Relatorios
{
    public partial class RelacaoMensalidadesAlunosTurma : System.Web.UI.Page
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
                CarregaDropDown();
                CarregaAnos();
                CarregaModalidades();
                CarregaSerieCurso();
                CarregaTurma();
                CarregaHistorico();
                CarregaTipoSolicitacao();
            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            int strP_CO_EMP_UNID_CONT, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_TIPO_BOLSA, strP_CO_HISTORICO, strP_CO_TIPO_SOLI;
            string infos, parametros, strP_CO_ANO_MES_MAT, strP_CO_GRUPO_BOLSA;

            //--------> Inicializa as variáveis
            strP_CO_ANO_MES_MAT = null;
            strP_CO_GRUPO_BOLSA = null;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP_UNID_CONT = int.Parse(ddlUnidade.SelectedValue);
            strP_CO_MODU_CUR = int.Parse(ddlModalidade.SelectedValue);
            strP_CO_CUR = int.Parse(ddlSerieCurso.SelectedValue);
            strP_CO_TUR = int.Parse(ddlTurma.SelectedValue);
            strP_CO_HISTORICO = int.Parse(ddlHistorico.SelectedValue);
            strP_CO_TIPO_SOLI = int.Parse(ddlTipoSolic.SelectedValue);
            strP_CO_ANO_MES_MAT = ddlAnoRefer.SelectedValue;

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            parametros = "( Unid. Contr: " + ddlUnidade.SelectedItem.ToString() + " - Modal: " + ddlModalidade.SelectedItem.ToString() + " - Série: " + ddlSerieCurso.SelectedItem.ToString() + " - Turma: " +
                ddlTurma.SelectedItem.ToString() + " - Ano Refer.: " + ddlAnoRefer.SelectedItem.ToString().Trim() + " )";

            RptRelacaoAlunosMensalidades rpt = new RptRelacaoAlunosMensalidades();
            lRetorno = rpt.InitReport(parametros, LoginAuxili.CO_EMP, infos, strP_CO_EMP_UNID_CONT, strP_CO_ANO_MES_MAT, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_HISTORICO, strP_CO_TIPO_SOLI);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares e Bolsas
        /// </summary>
        private void CarregaDropDown()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     where tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy(e => e.NO_FANTAS_EMP);

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.Items.Insert(0, new ListItem("Todas", "0"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        private void CarregaTurma()
        {
            ddlTurma.Items.Clear();
            int modalidade = int.Parse(ddlModalidade.SelectedValue);
            int serie = int.Parse(ddlSerieCurso.SelectedValue);

            if (serie != 0)
            {
                ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                       where tb06.CO_MODU_CUR == modalidade && tb06.CO_CUR == serie
                                       select new { tb06.TB129_CADTURMAS.NO_TURMA, tb06.CO_TUR });

                ddlTurma.DataTextField = "NO_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataBind();
            }
            else
                ddlTurma.Items.Clear();

            ddlTurma.Items.Insert(0, new ListItem("Todas", "0"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            ddlAnoRefer.DataSource = (from tb48 in TB48_GRADE_ALUNO.RetornaTodosRegistros()
                                      where tb48.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                      select new { tb48.CO_ANO_MES_MAT }).Distinct().OrderBy(g => g.CO_ANO_MES_MAT);

            ddlAnoRefer.DataTextField = "CO_ANO_MES_MAT";
            ddlAnoRefer.DataValueField = "CO_ANO_MES_MAT";
            ddlAnoRefer.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            ddlModalidade.Items.Clear();
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where(m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();

            ddlModalidade.Items.Insert(0, new ListItem("Todas", "0"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {
            ddlSerieCurso.Items.Clear();
            string anoGrade = ddlAnoRefer.SelectedValue;
            int modalidade = int.Parse(ddlModalidade.SelectedValue);

            if (modalidade != 0)
            {
                ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaTodosRegistros()
                                            join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on tb01.CO_CUR equals tb43.CO_CUR
                                            where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.CO_ANO_GRADE == anoGrade && tb01.CO_EMP == LoginAuxili.CO_EMP
                                            select new { tb01.NO_CUR, tb01.CO_CUR }).Distinct().OrderBy(c => c.NO_CUR);

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();
            }

            ddlSerieCurso.Items.Insert(0, new ListItem("Todas", "0"));
        }
        /// <summary>
        /// Método que carrega o dropdown de históricos
        /// </summary>
        private void CarregaHistorico()
        {
            ddlHistorico.DataSource = (from tb39 in TB39_HISTORICO.RetornaTodosRegistros()
                                       where tb39.FLA_TIPO_HISTORICO == "C"
                                       select new { tb39.CO_HISTORICO, tb39.DE_HISTORICO });

            ddlHistorico.DataTextField = "DE_HISTORICO";
            ddlHistorico.DataValueField = "CO_HISTORICO";
            ddlHistorico.DataBind();

            ddlHistorico.Items.Insert(0, new ListItem("Todos", "0"));
            ddlHistorico.Items.Insert(0, new ListItem("Selecione", "-1"));
        }
        /// <summary>
        /// Método que carrega o dropdown de tipo de solicitação
        /// </summary>
        private void CarregaTipoSolicitacao()
        {
            ddlTipoSolic.DataSource = (from tb66 in TB66_TIPO_SOLIC.RetornaTodosRegistros()
                                       where tb66.CO_SITU_SOLI == "A"
                                       select new { tb66.CO_TIPO_SOLI, tb66.NO_TIPO_SOLI });

            ddlTipoSolic.DataTextField = "NO_TIPO_SOLI";
            ddlTipoSolic.DataValueField = "CO_TIPO_SOLI";
            ddlTipoSolic.DataBind();

            ddlTipoSolic.Items.Insert(0, new ListItem("Todos", "0"));
            ddlTipoSolic.Items.Insert(0, new ListItem("Selecione", "-1"));
        }
        #endregion

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
        }

        protected void ddlAnoRefer_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
        }

        protected void ckbHistorico_CheckedChanged(object sender, EventArgs e)
        {
            ckbTipoSolicitacao.Checked = !ckbHistorico.Checked;
            ddlHistorico.Enabled = ckbHistorico.Checked;
            ddlTipoSolic.Enabled = !ckbHistorico.Checked;
            ddlTipoSolic.ClearSelection();
        }

        protected void ckbTipoSolicitacao_CheckedChanged(object sender, EventArgs e)
        {
            ckbHistorico.Checked = !ckbTipoSolicitacao.Checked;
            ddlTipoSolic.Enabled = ckbTipoSolicitacao.Checked;
            ddlHistorico.Enabled = !ckbTipoSolicitacao.Checked;
            ddlHistorico.ClearSelection();
        }
    }
}