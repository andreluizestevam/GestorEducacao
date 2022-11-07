//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE RECEITAS (CONTAS A RECEBER)
// OBJETIVO: HISTÓRICO FINANCEIRO DE ALUNOS - SÉRIE/TURMA
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//    DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// 02/04/2014 |  Julio Gleisson            | Copia do Relatorio HistorFinanceAlunos do GEDUC                          
// ---------+----------------------------+-------------------------------------

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
using C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3200_ExtratoEntrega;

namespace C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3299_Relatorios
{
    public partial class Extrato_Entrega : System.Web.UI.Page
    {
        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }

        //#region Eventos

        //protected override void OnPreInit(EventArgs e)
        //{
        //    base.OnPreInit(e);

        //    ///Criação das instâncias utilizadas
        //    PadraoRelatoriosCorrente.OnAcaoGeraRelatorio += new PadraoRelatorios.OnAcaoGeraRelatorioHandler(PadraoRelatoriosCorrente_OnAcaoGeraRelatorio);
        //}

        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        CarregaUnidades();
        //        CarregaAnos();
        //        CarregaModalidades();
        //        CarregaSerieCurso();
        //        CarregaTurma();
        //        CarregaAlunos();
        //        CarregaResponsaveis();
        //        CarregaAgrupadores();
        //        CarregaTipoDocumento();
        //    }
        //}

        ///// <summary>
        ///// Processo de Geração do Relatório
        ///// </summary>
        //void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        //{
        //    ///Variáveis obrigatórias para gerar o Relatório
        //    string strParametrosRelatorio, strINFOS;
        //    int lRetorno;

        //    ///Variáveis de parâmetro do Relatório
        //    int strP_CO_EMP, strP_CO_EMP_REF, strP_CO_EMP_UNID_CONT, intP_ID_REGIAO, intP_ID_AREA, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_AGRUP, strP_CO_ALU, strP_CO_RESP;
        //    string strP_CO_ANO_REF, strP_IC_SIT_DOC, strP_CO_ANO_MES_MAT;

        //    ///Inicializa as variáveis       
        //    strP_CO_ANO_REF = null;
        //    strP_IC_SIT_DOC = null;

        //    ///Lança nas variáveis dos parâmetros os valores escolhidos no formulário
        //    intP_ID_REGIAO = int.Parse(ddlRegiao.SelectedValue);
        //    intP_ID_AREA = int.Parse(ddlArea.SelectedValue);
        //    strP_CO_EMP_REF = int.Parse(ddlUnidade.SelectedValue);
        //    strP_CO_EMP_UNID_CONT = int.Parse(ddlUnidadeContrato.SelectedValue);
        //    strP_CO_CUR = int.Parse(ddlRegiao.SelectedValue);
        //    strP_CO_TUR = int.Parse(ddlArea.SelectedValue);
        //    strP_CO_ANO_REF = ddlAnoRefer.SelectedValue;
        //    strP_CO_ALU = int.Parse(ddlAluno.SelectedValue);
        //    strP_IC_SIT_DOC = ddlStaDocumento.SelectedValue;
        //    strP_CO_AGRUP = int.Parse(ddlAgrupador.SelectedValue);
        //    strP_CO_RESP = int.Parse(ddlResponsavel.SelectedValue);
        //    strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

        //    RptExtratoEntrega fpcb = new RptExtratoEntrega();
        
        //    #region Verifica campos
        //    if (chkPorAluno.Checked)
        //    {
        //        strP_CO_MODU_CUR = strP_CO_CUR = strP_CO_TUR = strP_CO_RESP = -1;
        //        fpcb.Titulo = "HISTÓRICO FINANCEIRO DE ALUNOS";
        //    }
        //    else if (chkPorResponsavel.Checked)
        //    {
        //        strP_CO_MODU_CUR = strP_CO_CUR = strP_CO_TUR = -1;
        //        strP_CO_ALU = -1;
        //        fpcb.Titulo = "HISTÓRICO FINANCEIRO DE ALUNOS - POR RESPONSÁVEL";
        //    }
        //    else
        //    {
        //        strP_CO_ALU = strP_CO_RESP = -1;
        //        fpcb.Titulo = "HISTÓRICO FINANCEIRO DE ALUNOS - POR SÉRIE/TURMA";
        //    }
        //    #endregion

        //    strParametrosRelatorio = "( Ano: " + ddlAnoRefer.SelectedItem.ToString().Trim() + " - Modalidade: " + ddlModalidade.SelectedItem.ToString() + " - Regiao: " + ddlRegiao.SelectedItem.ToString() + " - Area: " + ddlArea.SelectedItem.ToString() +
        //    " - Status: " + ddlStaDocumento.SelectedItem.ToString() + " - Agrupador: " + ddlAgrupador.SelectedItem.ToString() + " )";

        //    lRetorno = fpcb.InitReport(strParametrosRelatorio, strP_CO_EMP_REF, intP_ID_REGIAO, intP_ID_AREA, strP_CO_ALU, strP_CO_RESP, chkIncluiCancel.Checked, strINFOS);
        //    Session["Report"] = fpcb;
        //    Session["URLRelatorio"] = "/GeducReportViewer.aspx";

        //    AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        //}
        //#endregion

        //#region Carregamento DropDown

        ///// <summary>
        ///// Método que carrega o dropdown de Unidades Escolares
        ///// </summary>
        //private void CarregaUnidades()
        //{
        //    ddlUnidade.Items.Clear();
        //    ddlUnidade.Items.AddRange(AuxiliBaseApoio.UnidadesDDL(LoginAuxili.IDEADMUSUARIO));
        //    if (ddlUnidade.Items.FindByValue(LoginAuxili.CO_EMP.ToString()) != null)
        //        ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();

        //    ddlUnidadeContrato.Items.Clear();
        //    ddlUnidadeContrato.Items.AddRange(AuxiliBaseApoio.UnidadesDDL(LoginAuxili.IDEADMUSUARIO, todos: true));
        //    ddlUnidadeContrato.SelectedValue = "-1";
        //}

        ///// <summary>
        ///// Método que carrega o dropdown de Turmas
        ///// </summary>
        //private void CarregaTurma()
        //{
        //    ddlArea.Items.Clear();
        //    ddlArea.Items.AddRange(AuxiliBaseApoio.TurmasDDL(ddlUnidade.SelectedValue, ddlModalidade.SelectedValue, ddlArea.SelectedValue, ddlAnoRefer.SelectedValue, todos: true));
        //    ddlArea.SelectedValue = "-1";
        //}

        ///// <summary>
        ///// Método que carrega o dropdown de Anos
        ///// </summary>
        //private void CarregaAnos()
        //{
        //    ddlAnoRefer.Items.Clear();
        //    ddlAnoRefer.Items.AddRange(AuxiliBaseApoio.AnosDDL(ddlUnidade.SelectedValue, todos: true));
        //    ddlAnoRefer.SelectedValue = "-1";
        //}

        ///// <summary>
        ///// Método que carrega o dropdown de Modalidades
        ///// </summary>
        //private void CarregaModalidades()
        //{
        //    ddlModalidade.Items.Clear();
        //    ddlModalidade.Items.AddRange(AuxiliBaseApoio.ModulosDDL(LoginAuxili.ORG_CODIGO_ORGAO, todos: true));
        //    ddlModalidade.SelectedValue = "-1";
        //}

        //    /// <summary>
        ///// Método que carrega o dropdown de Modalidades
        ///// </summary>
        //private void CarregaRegiao()
        //{
        //    var res = (from tb906 in TB906_REGIAO.RetornaTodosRegistros()
        //               select new
        //               {
        //                   tb906.NM_REGIAO,
        //                   tb906.ID_REGIAO
        //               }).OrderBy(o => o.NM_REGIAO);

        //    ddlRegiao.DataTextField = "NM_REGIAO";
        //    ddlRegiao.DataValueField = "ID_REGIAO";

        //    ddlRegiao.DataSource = res;
        //    ddlRegiao.DataBind();

        //    ddlRegiao.Items.Insert(0, new ListItem("Selecione", ""));
        //}

        //private void CarregaArea()
        //{
        //    var res = (from tb89 in TB89_UNIDADES.RetornaTodosRegistros()
        //               where 
        //                   tb89.ID_REGIAO
        //                   tb906.ID_REGIAO
        //               }).OrderBy(o => o.NM_REGIAO);

        //    ddlRegiao.DataTextField = "NM_REGIAO";
        //    ddlRegiao.DataValueField = "ID_REGIAO";

        //    ddlRegiao.DataSource = res;
        //    ddlRegiao.DataBind();

        //    ddlRegiao.Items.Insert(0, new ListItem("Selecione", ""));
        //}

        ///// <summary>
        ///// Método que carrega o dropdown de Séries
        ///// </summary>
        //private void CarregaSerieCurso()
        //{
        //    ddlRegiao.Items.Clear();
        //    ddlRegiao.Items.AddRange(AuxiliBaseApoio.SeriesDDL(ddlUnidade.SelectedValue, ddlModalidade.SelectedValue, ddlAnoRefer.SelectedValue, todos: true));
        //    ddlRegiao.SelectedValue = "-1";
        //}

        ///// <summary>
        ///// Método que carrega o dropdown de Agrupadores
        ///// </summary>
        //private void CarregaAgrupadores()
        //{
        //    ddlAgrupador.Items.Clear();
        //    ddlAgrupador.Items.AddRange(AuxiliBaseApoio.AgrupadoresDDL("R", todos: true));
        //    ddlAgrupador.SelectedValue = "-1";
        //}

        ///// <summary>
        ///// Método que carrega o dropdown de Alunos
        ///// </summary>
        //private void CarregaAlunos()
        //{
        //    int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

        //    ddlAluno.DataSource = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
        //                           where tb07.CO_EMP == coEmp
        //                           select new { tb07.NO_ALU, tb07.CO_ALU }).Distinct().OrderBy(g => g.NO_ALU);

        //    ddlAluno.DataTextField = "NO_ALU";
        //    ddlAluno.DataValueField = "CO_ALU";
        //    ddlAluno.DataBind();

        //    ddlAluno.Items.Insert(0, new ListItem("Todos", "-1"));
        //    ddlAluno.SelectedValue = "-1";
        //}

        ///// <summary>
        ///// Método que carrega o dropdown de Alunos
        ///// </summary>
        //private void CarregaResponsaveis()
        //{
        //    ddlResponsavel.Items.Clear();
        //    ddlResponsavel.Items.AddRange(AuxiliBaseApoio.ResponsaveisDDL(LoginAuxili.ORG_CODIGO_ORGAO, todos: true));
        //    ddlResponsavel.SelectedValue = "-1";
        //}

        ///// <summary>
        ///// Carrega todos os status de títulos financeiros
        ///// </summary>
        //private void CarregaTipoDocumento()
        //{
        //    ddlStaDocumento.Items.Clear();
        //    ddlStaDocumento.Items.AddRange(AuxiliBaseApoio.BaseApoioDDL(statusFinanceiro.ResourceManager, todos: true));
        //    ddlStaDocumento.SelectedValue = "-1";
        //}
        //#endregion

        //#region Eventos de componentes da pagina

        //protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    CarregaSerieCurso();
        //    CarregaTurma();
        //}


        //protected void ddlRegiao_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Carregar();
        //    CarregaTurma();
        //}


        //protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    CarregaTurma();
        //}

        //protected void ddlUnidade_SelectedIndexChanged1(object sender, EventArgs e)
        //{
        //    CarregaAnos();
        //    CarregaModalidades();
        //    CarregaSerieCurso();
        //    CarregaTurma();
        //    CarregaAlunos();
        //}

        //protected void ddlAnoRefer_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    CarregaSerieCurso();
        //    CarregaTurma();
        //}

        //protected void ddlStaDocumento_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlStaDocumento.SelectedValue == "T")
        //    {
        //        chkIncluiCancel.Visible = true;
        //    }
        //    else
        //    {
        //        chkIncluiCancel.Visible = false;
        //        chkIncluiCancel.Checked = false;
        //    }
        //}

        //protected void chkPorSerieTurma_CheckedChanged(object sender, EventArgs e)
        //{
        //    chkPorAluno.Checked = chkPorResponsavel.Checked = false;

        //    liModalidade.Visible = liRegiao.Visible = liArea.Visible = true;
        //    liAluno.Visible = liResponsavel.Visible = false;
        //}

        //protected void chkPorAluno_CheckedChanged(object sender, EventArgs e)
        //{
        //    chkPorSerieTurma.Checked = chkPorResponsavel.Checked = false;

        //    liModalidade.Visible = liRegiao.Visible = liArea.Visible = liResponsavel.Visible = false;
        //    liAluno.Visible = true;
        //}

        //protected void chkPorResponsavel_CheckedChanged(object sender, EventArgs e)
        //{
        //    chkPorAluno.Checked = chkPorSerieTurma.Checked = false;

        //    liModalidade.Visible = liRegiao.Visible = liArea.Visible = liAluno.Visible = false;
        //    liResponsavel.Visible = true;
        //}

        //#endregion
    }
}