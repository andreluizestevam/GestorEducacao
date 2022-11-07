//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE RECEITAS (CONTAS A RECEBER)
// OBJETIVO: POSIÇÃO DE TÍTULOS DE RECEITAS/RECURSOS - GERAL
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
using C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5200_CtrlReceitas;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5200_CtrlReceitas.F5299_Relatorios
{
    public partial class DiarioFinanceiro : System.Web.UI.Page
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
                CarregarUnidadeContrato();
            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            string strParametrosRelatorio;
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            string strINFOS, strP_CO_ANO_MES_MAT, strP_ORIG_PAGTO;
            int strP_CO_EMP, strP_CO_TUR, strP_CO_MODU_CUR, strP_CO_CUR, strUniContr,
                strP_CO_TIPO_DOC, strP_CO_EMP_REF, strP_CO_AGRUP, strP_CO_TipoPagto;
            DateTime strP_DT_INI, strP_DT_FIM;

            if (DateTime.TryParse(txtDataPeriodoIni.Text, out strP_DT_INI) && DateTime.TryParse(txtDataPeriodoFim.Text, out strP_DT_FIM))
            {
                if (strP_DT_INI > strP_DT_FIM)
                {
                    AuxiliPagina.EnvioMensagemErro( this, "A data final deve ser posterior a data inicial.");
                    return;
                }
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this, "Data em formato incorreto."); 
                return;
            }
            

            //--------> Inicializa as variáveis
            strP_CO_ANO_MES_MAT = null;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strParametrosRelatorio = "";
            strP_CO_EMP = LoginAuxili.CO_EMP;
            strP_CO_EMP_REF = LoginAuxili.CO_EMP;
            strUniContr = int.Parse(ddlUnidContrato.SelectedValue);
            strP_CO_ANO_MES_MAT = DateTime.Now.Year.ToString();
            strP_CO_MODU_CUR = int.Parse(ddlModalidade.SelectedValue);
            strP_CO_CUR = int.Parse(ddlSerieCurso.SelectedValue);
            strP_CO_TUR = int.Parse(ddlTurma.SelectedValue);
            strP_CO_TIPO_DOC = int.Parse(ddlTipoDoctos.SelectedValue);
            //strP_DT_INI = DateTime.Parse(txtDataPeriodoIni.Text);
            //strP_DT_FIM = DateTime.Parse(txtDataPeriodoFim.Text);
            strP_CO_AGRUP = int.Parse(ddlAgrupador.SelectedValue);
            strP_ORIG_PAGTO = ddlOrigPagto.SelectedValue;
            if (HabilitarTipoPag())
                strP_CO_TipoPagto = int.Parse(ddlTipoPagto.SelectedValue);
            else
                strP_CO_TipoPagto = -1;
            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            string siglaUnid = TB25_EMPRESA.RetornaPelaChavePrimaria(strP_CO_EMP_REF).sigla;
            string siglaUnidContr = strUniContr != -1 ? TB25_EMPRESA.RetornaPelaChavePrimaria(strUniContr).sigla : "Todas";

            strParametrosRelatorio = "( Unid. Contrato: " + siglaUnidContr + " - Ano Ref.: " + strP_CO_ANO_MES_MAT
                + " - Mod.: " + ddlModalidade.SelectedItem.ToString() + " - Série: " + ddlSerieCurso.SelectedItem.ToString() +
                " - Turma: " + ddlTurma.SelectedItem.ToString() + " - Agrupador: " + ddlAgrupador.SelectedItem.ToString()
                + " - Tp Docto: " + ddlTipoDoctos.SelectedItem.ToString()
                + " - Período: " + DateTime.Parse(txtDataPeriodoIni.Text).ToString("dd/MM/yy") + " à " + DateTime.Parse(txtDataPeriodoFim.Text).ToString("dd/MM/yy")
                + (HabilitarTipoPag() ? (" - Tp Pagto: " + strP_CO_TipoPagto) : "") + " )";
             
            RptDiarioFinanceiro fpcb = new RptDiarioFinanceiro();
            lRetorno = fpcb.InitReport(strParametrosRelatorio, LoginAuxili.ORG_CODIGO_ORGAO, strP_CO_EMP, strP_CO_EMP_REF,
                strUniContr, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_ANO_MES_MAT, strP_CO_TIPO_DOC, strP_DT_INI, strP_DT_FIM, strP_CO_AGRUP, strP_ORIG_PAGTO, strP_CO_TipoPagto, strINFOS);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Carrega todos as unidades de contrato
        /// </summary>
        private void CarregarUnidadeContrato() {
            ddlUnidContrato.Items.Clear();
            ddlUnidContrato.DataSource = from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                         where tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                         select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP };

            ddlUnidContrato.DataTextField = "NO_FANTAS_EMP";
            ddlUnidContrato.DataValueField = "CO_EMP";
            ddlUnidContrato.DataBind();

            ddlUnidContrato.Items.Insert(0, new ListItem("Todas", "-1"));
            ddlUnidContrato.Items.Insert(0, new ListItem("Selecione", "0"));
            ddlUnidContrato.SelectedValue = "0";

            ddlModalidade.Items.Clear();
            ddlSerieCurso.Items.Clear();
            ddlTurma.Items.Clear();
            ddlTipoDoctos.Items.Clear();
            ddlOrigPagto.Items.Clear();
            if (HabilitarTipoPag())
            {
                ddlTipoPagto.Items.Clear();
                HabilitarTipoPag(false);
            }
            ddlAgrupador.Items.Clear();
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

            ddlModalidade.Items.Insert(0, new ListItem("Todos", "-1"));
            ddlModalidade.Items.Insert(0, new ListItem("Selecione", "0"));
            ddlModalidade.SelectedValue = "0";
            ddlSerieCurso.Items.Clear();
            ddlTurma.Items.Clear();
            ddlTipoDoctos.Items.Clear();
            ddlOrigPagto.Items.Clear();
            if (HabilitarTipoPag())
            {
                ddlTipoPagto.Items.Clear();
                HabilitarTipoPag(false);
            }
            ddlAgrupador.Items.Clear();

        }
        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {
            int coEmp = LoginAuxili.CO_EMP;
            int modalidade = ddlModalidade.SelectedValue != "0" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            string anoGrade = DateTime.Now.Year.ToString();

            ddlSerieCurso.Items.Clear();
            if (modalidade != 0 && anoGrade != "0")
            {
                ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaTodosRegistros()
                                            join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on tb01.CO_CUR equals tb43.CO_CUR
                                            where (modalidade != -1 ? tb43.TB44_MODULO.CO_MODU_CUR == modalidade : 0==0)
                                            && (anoGrade != "-1" ? tb43.CO_ANO_GRADE == anoGrade : 0 == 0) 
                                            && tb01.CO_EMP == coEmp
                                            select new { tb01.NO_CUR, tb01.CO_CUR }).Distinct().OrderBy(c => c.NO_CUR);

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();
            }
            ddlSerieCurso.Items.Insert(0, new ListItem("Todas", "-1"));
            ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", "0"));
            ddlSerieCurso.SelectedValue = "0";
            ddlTurma.Items.Clear();
            ddlTipoDoctos.Items.Clear();
            ddlOrigPagto.Items.Clear();
            if (HabilitarTipoPag())
            {
                ddlTipoPagto.Items.Clear();
                HabilitarTipoPag(false);
            }
            ddlAgrupador.Items.Clear();
        }
        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "0" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "0" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            ddlTurma.Items.Clear();

            if ((modalidade != 0) && (serie != 0))
            {
                ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                       where (modalidade != -1 ? tb06.CO_MODU_CUR == modalidade : 0==0)
                                       && (serie != -1 ? tb06.CO_CUR == serie : 0==0)
                                       select new { tb06.TB129_CADTURMAS.NO_TURMA, tb06.CO_TUR });

                ddlTurma.DataTextField = "NO_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataBind();
            }

            ddlTurma.Items.Insert(0, new ListItem("Todas", "-1"));
            ddlTurma.Items.Insert(0, new ListItem("Selecione", "0"));
            ddlTurma.SelectedValue = "0";
            ddlTipoDoctos.Items.Clear();
            ddlOrigPagto.Items.Clear();
            if (HabilitarTipoPag())
            {
                ddlTipoPagto.Items.Clear();
                HabilitarTipoPag(false);
            }
            ddlAgrupador.Items.Clear();
        }
        /// <summary>
        /// Método que carrega o dropdown de Tipos de Documento
        /// </summary>
        private void CarregaTipoDocumento()
        {
            ddlTipoDoctos.Items.Clear();
            ddlTipoDoctos.DataSource = (from tb086 in TB086_TIPO_DOC.RetornaTodosRegistros()
                                    select new { tb086.CO_TIPO_DOC, tb086.DES_TIPO_DOC });

            ddlTipoDoctos.DataTextField = "DES_TIPO_DOC";
            ddlTipoDoctos.DataValueField = "CO_TIPO_DOC";
            ddlTipoDoctos.DataBind();

            ddlTipoDoctos.Items.Insert(0, new ListItem("Todos", "-1"));
            ddlTipoDoctos.Items.Insert(0, new ListItem("Selecione", "0"));
            ddlTipoDoctos.SelectedValue = "0";
            ddlOrigPagto.Items.Clear();
            if (HabilitarTipoPag())
            {
                ddlTipoPagto.Items.Clear();
                HabilitarTipoPag(false);
            }
            ddlAgrupador.Items.Clear();
        }
        /// <summary>
        /// Carregar todas a origens de pagamento
        /// </summary>
        private void CarregaOrigPag() 
        {
            ddlOrigPagto.Items.Clear();
            ddlOrigPagto.Items.Insert(0, new ListItem("Selecione", "0"));
            ddlOrigPagto.Items.Insert(1, new ListItem("Todas", "-1"));
            ddlOrigPagto.Items.Insert(2, new ListItem("Caixa", "C"));
            ddlOrigPagto.Items.Insert(3, new ListItem("Banco", "B"));
            ddlOrigPagto.Items.Insert(4, new ListItem("Baixa CAR", "X"));
            ddlOrigPagto.SelectedValue = "0";
            if (HabilitarTipoPag())
            {
                ddlTipoPagto.Items.Clear();
                HabilitarTipoPag(false);
            }
            ddlAgrupador.Items.Clear();
        }
        /// <summary>
        /// Carrega os tipos de pagamento
        /// </summary>
        private void CarregaTipoPag() 
        {
            string origem = ddlOrigPagto.SelectedValue;
            HabilitarTipoPag(true);
            ddlTipoPagto.Items.Clear();
            if (origem != "0" && origem == "C")
            {
                ddlTipoPagto.DataSource = (from tp in TB118_TIPO_RECEB.RetornaTodosRegistros()
                                           select new { tp.DE_RECEBIMENTO, tp.CO_TIPO_REC}
                                               );
                ddlTipoPagto.DataTextField = "DE_RECEBIMENTO";
                ddlTipoPagto.DataValueField = "CO_TIPO_REC";
                ddlTipoPagto.DataBind();
                ddlTipoPagto.Items.Insert(0, new ListItem("Todos", "-1"));
                ddlTipoPagto.Items.Insert(0, new ListItem("Selecione", "0"));
                ddlTipoPagto.SelectedValue = "0";
                ddlAgrupador.Items.Clear();
            }
            else
            {
                HabilitarTipoPag(false);
                if (origem != "0")
                    CarregaAgrupadores();
            }
        }
        /// <summary>
        /// Método que carrega o dropdown de Agrupadores
        /// </summary>
        private void CarregaAgrupadores()
        {
            ddlAgrupador.Items.Clear();
            ddlAgrupador.DataSource = (from tb315 in TB315_AGRUP_RECDESP.RetornaTodosRegistros()
                                       where tb315.TP_AGRUP_RECDESP == "R" || tb315.TP_AGRUP_RECDESP == "T"
                                       select new { tb315.ID_AGRUP_RECDESP, tb315.DE_SITU_AGRUP_RECDESP });

            ddlAgrupador.DataTextField = "DE_SITU_AGRUP_RECDESP";
            ddlAgrupador.DataValueField = "ID_AGRUP_RECDESP";
            ddlAgrupador.DataBind();

            ddlAgrupador.Items.Insert(0, new ListItem("Todos", "-1"));
            ddlAgrupador.SelectedValue = "-1";
        }
        #endregion

        #region Selecionados
        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0")
            {
                if (((DropDownList)sender).SelectedValue == "-1")
                {
                    CarregaSerieCurso();
                    ddlSerieCurso.SelectedValue = "-1";
                    ddlSerieCurso.Enabled = false;
                    CarregaTurma();
                    ddlTurma.SelectedValue = "-1";
                    ddlTurma.Enabled = false;
                    CarregaTipoDocumento();
                }
                else
                {
                    ddlSerieCurso.Enabled = true;
                    ddlTurma.Enabled = true;
                    CarregaSerieCurso();
                }
            }
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0")
                CarregaTurma();
        }

        protected void ddlTipoPagto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0")
                CarregaAgrupadores();
        }

        protected void ddlUnidContrato_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0")
                CarregaModalidades();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0")
                CarregaTipoDocumento();
        }

        protected void ddlTipoDoctos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0")
                CarregaOrigPag();
        }

        protected void ddlOrigPagto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0")
            {
                CarregaTipoPag();
            }
        }
        #endregion

        #region Metodos diversos
        /// <summary>
        /// Habilitar e desabilita o campo tipo de pagamento
        /// </summary>
        /// <param name="ativar"></param>
        private Boolean HabilitarTipoPag(Boolean? ativar = null) 
        {
            if (ativar != null)
            {
                lbDdlTipoPagto.Visible = (Boolean)ativar;
                ddlTipoPagto.Visible = (Boolean)ativar;
            }
            return ddlTipoPagto.Visible;
        }
        #endregion

        protected void ddlAgrupador_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDataPeriodoIni.Text = txtDataPeriodoFim.Text = "";
        }
    }
}
