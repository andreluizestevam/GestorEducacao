//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: GESTÃO DE DADOS DE ALUNOS
// OBJETIVO: RELAÇÃO DE ALUNOS - SITUAÇÃO DE MATRÍCULA
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3600_CtrlInformacoesAlunos.F3699_Relatorios
{
    public partial class RelacaoAlunoSituacaoMatricula : System.Web.UI.Page
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
                liModalidade.Visible = liSerie.Visible = liTurma.Visible = true;
                CarregaAnos();
                CarregaModalidades();
                CarregaSerieCurso();
                CarregaTurma();
            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            int coEmp, coUndCont, coMod, coCur, coTur, coOrdem;
            DateTime dtInicio, dtFim;
            string sitMat, infos, parametros;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            coEmp = LoginAuxili.CO_EMP;
            coUndCont = ddlUnidade.SelectedValue == "T" ? 0 : int.Parse(ddlUnidade.SelectedValue);
            coMod = ddlModalidade.SelectedValue == "T" ? 0 : int.Parse(ddlModalidade.SelectedValue);
            coCur = ddlSerieCurso.SelectedValue == "T" ? 0 : int.Parse(ddlSerieCurso.SelectedValue);
            coTur = ddlTurma.SelectedValue == "T" ? 0 : int.Parse(ddlTurma.SelectedValue);
            coOrdem = int.Parse(ddlOrdemImpressao.SelectedValue);
            sitMat = ddlSituacao.SelectedValue;
            dtInicio = DateTime.ParseExact(txtDtInicio.Text, "dd/MM/yyyy", null);
            dtFim = DateTime.ParseExact(txtDataFim.Text, "dd/MM/yyyy", null);
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            parametros = "(Unidade de Contrato: " + ddlUnidade.SelectedItem.ToString() +
                         " - Período: " + txtDtInicio.Text + " à " + txtDataFim.Text +
                         " - Ano Refer. : " + ddlAnoRefer.SelectedItem.ToString() +
                         " - Módulo: " + ddlModalidade.SelectedItem.ToString() +
                         " - Série: " + ddlSerieCurso.SelectedItem.ToString() +
                         " - Turma: " + ddlTurma.SelectedItem.ToString() +
                         " - Situação: " + ddlSituacao.SelectedItem.ToString() +
                         " - Ordenado Por: " + ddlOrdemImpressao.SelectedItem.ToString() + ")";

            RptAlunosSituMatricula rpt = new RptAlunosSituMatricula();
            lRetorno = rpt.InitReport(parametros, coEmp, coUndCont, ddlAnoRefer.SelectedValue, coMod, coCur, dtInicio, dtFim, coTur, sitMat, coOrdem, infos);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            ddlAnoRefer.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                      where tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                      select new { tb08.CO_ANO_MES_MAT }).Distinct().OrderBy(g => g.CO_ANO_MES_MAT);

            ddlAnoRefer.DataTextField = "CO_ANO_MES_MAT";
            ddlAnoRefer.DataValueField = "CO_ANO_MES_MAT";
            ddlAnoRefer.DataBind();

            ddlAnoRefer.Items.Insert(0, new ListItem("Todos", "T"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaUnidades()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                     where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy(e => e.NO_FANTAS_EMP);

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.Items.Insert(0, new ListItem("Todas", "T"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "T" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "T" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            if (serie != 0)
            {
                ddlTurma.Enabled = true;

                ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                       where tb06.CO_MODU_CUR == modalidade && tb06.CO_CUR == serie
                                       select new { tb06.TB129_CADTURMAS.NO_TURMA, tb06.CO_TUR });

                ddlTurma.DataTextField = "NO_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataBind();

                ddlTurma.Items.Insert(0, new ListItem("Todos", "T"));
            }
            else
            {
                ddlTurma.Items.Clear();
                ddlTurma.Items.Insert(0, new ListItem("Todos", "T"));
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where(m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();

            ddlModalidade.Items.Insert(0, new ListItem("Todos", "T"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {
            int coEmp = ddlUnidade.SelectedValue != "T" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "T" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            ddlSerieCurso.Items.Clear();

            if (modalidade != 0)
            {
                ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaTodosRegistros()
                                            join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on tb01.CO_CUR equals tb43.CO_CUR
                                            where tb43.TB44_MODULO.CO_MODU_CUR == modalidade
                                            && (coEmp != 0 ? tb01.CO_EMP == coEmp : coEmp == 0)
                                            select new { tb01.NO_CUR, tb01.CO_CUR }).Distinct().OrderBy(c => c.NO_CUR);

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();
            }

            ddlSerieCurso.Items.Insert(0, new ListItem("Todos", "T"));
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
    }
}