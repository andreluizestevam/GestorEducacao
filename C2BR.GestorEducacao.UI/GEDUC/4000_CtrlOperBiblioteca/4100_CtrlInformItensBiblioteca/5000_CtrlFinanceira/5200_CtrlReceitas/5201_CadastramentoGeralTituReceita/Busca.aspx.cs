 //=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE RECEITAS (CONTAS A RECEBER)
// OBJETIVO: CADASTRAMENTO GERAL DE TÍTULOS DE RECEITAS
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
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using Resources;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5200_CtrlReceitas.F5201_CadastramentoGeralTituReceita
{
    public partial class Busca : System.Web.UI.Page
    {
        public PadraoBuscas CurrentPadraoBuscas { get { return ((PadraoBuscas)this.Master); } }
        private Dictionary<string, string> statusF = AuxiliBaseApoio.chave(statusFinanceiro.ResourceManager);

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            CurrentPadraoBuscas.OnAcaoBuscaDefineGridView += new PadraoBuscas.OnAcaoBuscaDefineGridViewHandler(CurrentPadraoBuscas_OnAcaoBuscaDefineGridView);
            CurrentPadraoBuscas.OnDefineColunasGridView += new PadraoBuscas.OnDefineColunasGridViewHandler(CurrentPadraoBuscas_OnDefineColunasGridView);
            CurrentPadraoBuscas.OnDefineQueryStringIds += new PadraoBuscas.OnDefineQueryStringIdsHandler(CurrentPadraoBuscas_OnDefineQueryStringIds);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CarregaUnidadeContrato();
                CarregaHistoricos();
                CarregaStatusFinanceiro();
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "NU_DOC", "NU_PAR" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NU_DOC",
                HeaderText = "N° Documento"
            });

            BoundField bf2 = new BoundField();
            bf2.DataField = "NU_PAR";
            bf2.HeaderText = "Parc";
            bf2.ItemStyle.CssClass = "centro";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf2);

            BoundField bf9 = new BoundField();
            bf9.DataField = "DE_HISTORICO";
            bf9.HeaderText = "Histórico";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf9);

            BoundField bf12 = new BoundField();
            bf12.DataField = "TFR";
            bf12.HeaderText = "TFR";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf12);

            BoundField bf11 = new BoundField();
            bf11.DataField = "CEDENTE";
            bf11.HeaderText = "Código";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf11);

            BoundField bf3 = new BoundField();
            bf3.DataField = "DT_VEN_DOC";
            bf3.HeaderText = "Vencto";
            bf3.DataFormatString = "{0:dd/MM/yyyy}";
            bf3.ItemStyle.CssClass = "centro";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf3);

            BoundField bf4 = new BoundField();
            bf4.DataField = "DT_REC_DOC";
            bf4.HeaderText = "Pagto";
            bf4.DataFormatString = "{0:dd/MM/yyyy}";
            bf4.ItemStyle.CssClass = "centro";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf4);

            BoundField bf5 = new BoundField();
            bf5.DataField = "VR_PAR_DOC";
            bf5.HeaderText = "R$ Parc";
            bf5.DataFormatString = "{0:N}";
            bf5.ItemStyle.CssClass = "direita";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf5);

            BoundField bf6 = new BoundField();
            bf6.DataField = "VR_PAG";
            bf6.HeaderText = "R$ Pago";
            bf6.DataFormatString = "{0:N}";
            bf6.ItemStyle.CssClass = "direita";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf6);

            BoundField bf7 = new BoundField();
            bf7.DataField = "IC_SIT_DOC";
            bf7.HeaderText = "Status";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf7);
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            string strTipoFonte = ddlTipoFonte.SelectedValue;
            string strTipoPeriodo = ddlTipoPeriodo.SelectedValue;
            string strSituacao = ddlSituacao.SelectedValue;
            int coNomeFonte = ddlNomeFonte.SelectedValue != "" ? int.Parse(ddlNomeFonte.SelectedValue) : 0;
            int coUnidContr = ddlUnidadeContrato.SelectedValue != "" ? int.Parse(ddlUnidadeContrato.SelectedValue) : 0;
            int coHisto = int.Parse(ddlHistorico.SelectedValue);


            DateTime dataInicio = DateTime.MinValue;
            DateTime dataFim = DateTime.MinValue;
            DateTime? dataVerifIni = null;
            DateTime? dataVerifFim = null;

            DateTime.TryParse(txtPeriodoDe.Text, out dataInicio);
            DateTime.TryParse(txtPeriodoAte.Text, out dataFim);

            dataVerifIni = dataInicio == DateTime.MinValue ? null : (DateTime?)dataInicio;
            dataVerifFim = dataFim == DateTime.MinValue ? null : (DateTime?)dataFim;

            if (strTipoFonte == "A" && ddlNomeFonte.SelectedValue != "")
            {
                var resultado = (from tb47 in VW47_CTA_RECEB.RetornaTodosRegistros()
                                 join tb39 in TB39_HISTORICO.RetornaTodosRegistros() on tb47.CO_HISTORICO equals tb39.CO_HISTORICO into sr
                                 from x in sr.DefaultIfEmpty()
                                 where ((txtNumeroDocumento.Text != "" ? tb47.NU_DOC == txtNumeroDocumento.Text : txtNumeroDocumento.Text == "")
                                 && (dataVerifIni == null || (strTipoPeriodo == "V" && tb47.DT_VEN_DOC >= dataVerifIni) ||
                                 (strTipoPeriodo == "C" && tb47.DT_CAD_DOC >= dataVerifIni) || (strTipoPeriodo == "E" && tb47.DT_EMISS_DOCTO >= dataVerifIni))
                                 && (dataVerifFim == null || (strTipoPeriodo == "V" && tb47.DT_VEN_DOC <= dataVerifFim) ||
                                 (strTipoPeriodo == "C" && tb47.DT_CAD_DOC <= dataVerifFim) || (strTipoPeriodo == "E" && tb47.DT_EMISS_DOCTO <= dataVerifFim))
                                 && tb47.TP_CLIENTE_DOC == "A"  && tb47.CO_EMP == LoginAuxili.CO_EMP
                                 && (coUnidContr != 0 ? tb47.CO_EMP_UNID_CONT == coUnidContr : coUnidContr == 0)
                                 && (coHisto != 0 ? tb47.CO_HISTORICO == coHisto : coHisto == 0)) 
                                 && tb47.CO_ALU == coNomeFonte
                                 && (strSituacao != "-1" ? tb47.IC_SIT_DOC == strSituacao : 0 == 0)
                                 select new listaTítulos
                                 {
                                     NU_DOC = tb47.NU_DOC,
                                     NU_PAR = tb47.NU_PAR,
                                     DT_CAD_DOC = tb47.DT_CAD_DOC,
                                     DT_VEN_DOC = tb47.DT_VEN_DOC,
                                     DT_REC_DOC = tb47.DT_REC_DOC,
                                     VR_PAR_DOC = tb47.VR_PAR_DOC,
                                     VR_PAG = tb47.VR_PAG,
                                     DT_EMISS_DOCTO = tb47.DT_EMISS_DOCTO,
                                     CEDENTE = tb47.NU_NIRE,
                                     CO_CON_RECFIX = tb47.CO_CON_RECFIX,
                                     CO_ADITI_RECFIX = tb47.CO_ADITI_RECFIX,
                                     IC_SIT_DOC = tb47.IC_SIT_DOC,
                                     TFR = "Aluno",
                                     DE_HISTORICO = x.DE_HISTORICO
                                 });

                if (strTipoPeriodo == "V" && (dataVerifIni != null || dataVerifFim != null))
                    if (ddlTipoOrdem.SelectedValue == "D")
                        CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado.OrderByDescending(c => c.DT_VEN_DOC) : null;
                    else
                        CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado.OrderBy(c => c.DT_VEN_DOC) : null;
                else if (strTipoPeriodo == "E" && (dataVerifIni != null || dataVerifFim != null))
                    if (ddlTipoOrdem.SelectedValue == "D")
                        CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado.OrderByDescending(c => c.DT_EMISS_DOCTO) : null;
                    else
                        CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado.OrderBy(c => c.DT_EMISS_DOCTO) : null;
                else
                    if (ddlTipoOrdem.SelectedValue == "D")
                        CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado.OrderByDescending(c => c.DT_CAD_DOC) : null;
                    else
                        CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado.OrderBy(c => c.DT_CAD_DOC) : null;
            }
            else if (strTipoFonte == "O" && ddlNomeFonte.SelectedValue != "")
            {
                var resultado = (from tb47 in VW47_CTA_RECEB.RetornaTodosRegistros()
                                 join tb39 in TB39_HISTORICO.RetornaTodosRegistros() on tb47.CO_HISTORICO equals tb39.CO_HISTORICO into sr
                                 from x in sr.DefaultIfEmpty()
                                 where ((txtNumeroDocumento.Text != "" ? tb47.NU_DOC == txtNumeroDocumento.Text : txtNumeroDocumento.Text == "")
                                 && (dataVerifIni == null || (strTipoPeriodo == "V" && tb47.DT_VEN_DOC >= dataVerifIni) ||
                                 (strTipoPeriodo == "C" && tb47.DT_CAD_DOC >= dataVerifIni) || (strTipoPeriodo == "E" && tb47.DT_EMISS_DOCTO >= dataVerifIni))
                                 && (dataVerifFim == null || (strTipoPeriodo == "V" && tb47.DT_VEN_DOC <= dataVerifFim) ||
                                 (strTipoPeriodo == "C" && tb47.DT_CAD_DOC <= dataVerifFim) || (strTipoPeriodo == "E" && tb47.DT_EMISS_DOCTO <= dataVerifFim))
                                 && tb47.TP_CLIENTE_DOC == "O" && tb47.CO_EMP == LoginAuxili.CO_EMP
                                 && (coUnidContr != 0 ? tb47.CO_EMP_UNID_CONT == coUnidContr : coUnidContr == 0)
                                 && (coHisto != 0 ? tb47.CO_HISTORICO == coHisto : coHisto == 0)) && tb47.CO_CLIENTE == coNomeFonte 
                                 && (strSituacao != "-1" ? tb47.IC_SIT_DOC == strSituacao : 0 == 0)
                                 select new listaTítulos
                                 {
                                     NU_DOC = tb47.NU_DOC,
                                     NU_PAR = tb47.NU_PAR,
                                     DT_CAD_DOC = tb47.DT_CAD_DOC,
                                     DT_VEN_DOC = tb47.DT_VEN_DOC,
                                     DT_REC_DOC = tb47.DT_REC_DOC,
                                     VR_PAR_DOC = tb47.VR_PAR_DOC,
                                     IC_SIT_DOC = tb47.IC_SIT_DOC,
                                     VR_PAG = tb47.VR_PAG,
                                     DT_EMISS_DOCTO = tb47.DT_EMISS_DOCTO,
                                     CO_CON_RECFIX = tb47.CO_CON_RECFIX,
                                     CO_ADITI_RECFIX = tb47.CO_ADITI_RECFIX,
                                     DE_HISTORICO = x.DE_HISTORICO,
                                     CEDENTE = (tb47.CO_CPFCGC_CLI.Length == 14 ? tb47.CO_CPFCGC_CLI.Insert(12, "-").Insert(8, "/").Insert(5, ".").Insert(2, ".") : tb47.CO_CPFCGC_CLI.Length == 11 ? tb47.CO_CPFCGC_CLI.Insert(9, "-").Insert(6, ".").Insert(3, ".") : tb47.CO_CPFCGC_CLI),
                                     TFR = "Não Aluno"
                                 });

                if (strTipoPeriodo == "V" && (dataVerifIni != null || dataVerifFim != null))
                    if (ddlTipoOrdem.SelectedValue == "D")
                        CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado.OrderByDescending(c => c.DT_VEN_DOC) : null;
                    else
                        CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado.OrderBy(c => c.DT_VEN_DOC) : null;
                else if (strTipoPeriodo == "E" && (dataVerifIni != null || dataVerifFim != null))
                    if (ddlTipoOrdem.SelectedValue == "D")
                        CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado.OrderByDescending(c => c.DT_EMISS_DOCTO) : null;
                    else
                        CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado.OrderBy(c => c.DT_EMISS_DOCTO) : null;
                else
                    if (ddlTipoOrdem.SelectedValue == "D")
                        CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado.OrderByDescending(c => c.DT_CAD_DOC) : null;
                    else
                        CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado.OrderBy(c => c.DT_CAD_DOC) : null;
            }
            else
            {
                var resultado = (from tb47 in VW47_CTA_RECEB.RetornaTodosRegistros()
                                 join tb39 in TB39_HISTORICO.RetornaTodosRegistros() on tb47.CO_HISTORICO equals tb39.CO_HISTORICO into sr
                                 from x in sr.DefaultIfEmpty()
                                 where (txtNumeroDocumento.Text != "" ? tb47.NU_DOC == txtNumeroDocumento.Text : txtNumeroDocumento.Text == "")
                                 && (dataVerifIni == null || (strTipoPeriodo == "V" 
                                 && tb47.DT_VEN_DOC >= dataVerifIni) ||
                                 (strTipoPeriodo == "C" 
                                 && tb47.DT_CAD_DOC >= dataVerifIni) || (strTipoPeriodo == "E" 
                                 && tb47.DT_EMISS_DOCTO >= dataVerifIni))
                                 && (dataVerifFim == null || (strTipoPeriodo == "V" 
                                 && tb47.DT_VEN_DOC <= dataVerifFim) ||
                                 (strTipoPeriodo == "C" 
                                 && tb47.DT_CAD_DOC <= dataVerifFim) || (strTipoPeriodo == "E" 
                                 && tb47.DT_EMISS_DOCTO <= dataVerifFim))
                                 && tb47.CO_EMP == LoginAuxili.CO_EMP 
                                 && (coUnidContr != 0 ? tb47.CO_EMP_UNID_CONT == coUnidContr : coUnidContr == 0)
                                 && (ddlTipoFonte.SelectedValue != "T" ? tb47.TP_CLIENTE_DOC == ddlTipoFonte.SelectedValue : ddlTipoFonte.SelectedValue == "T")
                                 && (coHisto != 0 ? tb47.CO_HISTORICO == coHisto : 0 == 0)
                                 && (strSituacao != "-1" ? tb47.IC_SIT_DOC == strSituacao : 0 == 0)
                                 select new listaTítulos
                                 {
                                     NU_DOC = tb47.NU_DOC,
                                     NU_PAR = tb47.NU_PAR,
                                     DT_CAD_DOC = tb47.DT_CAD_DOC,
                                     DT_VEN_DOC = tb47.DT_VEN_DOC,
                                     DT_REC_DOC = tb47.DT_REC_DOC,
                                     VR_PAR_DOC = tb47.VR_PAR_DOC,
                                     IC_SIT_DOC = tb47.IC_SIT_DOC,
                                     VR_PAG = tb47.VR_PAG,
                                     DT_EMISS_DOCTO = tb47.DT_EMISS_DOCTO,
                                     CO_CON_RECFIX = tb47.CO_CON_RECFIX,
                                     CO_ADITI_RECFIX = tb47.CO_ADITI_RECFIX,
                                     CEDENTE = (tb47.TP_CLIENTE_DOC == "A" ? tb47.NU_NIRE :
                                         (tb47.CO_CPFCGC_CLI.Length == 14 ? tb47.CO_CPFCGC_CLI.Insert(12, "-").Insert(8, "/").Insert(5, ".").Insert(2, ".") : tb47.CO_CPFCGC_CLI.Length == 11 ? tb47.CO_CPFCGC_CLI.Insert(9, "-").Insert(6, ".").Insert(3, ".") : tb47.CO_CPFCGC_CLI)),
                                     TFR = (tb47.TP_CLIENTE_DOC == "A" ? "Aluno" : "Não Aluno"),
                                     DE_HISTORICO = x.DE_HISTORICO
                                 });

                if (strTipoPeriodo == "V" && (dataVerifIni != null || dataVerifFim != null))
                {
                    if (ddlTipoOrdem.SelectedValue == "D")
                        CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado.OrderByDescending(c => c.DT_VEN_DOC) : null;
                    else
                        CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado.OrderBy(c => c.DT_VEN_DOC) : null;
                }
                else if (strTipoPeriodo == "E" && (dataVerifIni != null || dataVerifFim != null))
                    if (ddlTipoOrdem.SelectedValue == "D")
                        CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado.OrderByDescending(c => c.DT_EMISS_DOCTO) : null;
                    else
                        CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado.OrderBy(c => c.DT_EMISS_DOCTO) : null;
                else
                    if (ddlTipoOrdem.SelectedValue == "D")
                        CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado.OrderByDescending(c => c.DT_CAD_DOC) : null;
                    else
                        CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado.OrderBy(c => c.DT_CAD_DOC) : null;
            }
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>("doc", "NU_DOC"));
            queryStringKeys.Add(new KeyValuePair<string, string>("par", "NU_PAR"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento

        //====> Método que carrega o DropDown Nome da Fonte com os Alunos
        private void CarregaAluno()
        {
            ddlNomeFonte.DataSource = (from tb07 in TB07_ALUNO.RetornaPelaUnid(LoginAuxili.CO_EMP)
                                       select new { tb07.CO_ALU, tb07.NO_ALU }).OrderBy( a => a.NO_ALU );

            ddlNomeFonte.DataTextField = "NO_ALU";
            ddlNomeFonte.DataValueField = "CO_ALU";
            ddlNomeFonte.DataBind();

            ddlNomeFonte.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Carrega os status financeiro dos títulos
        /// </summary>
        private void CarregaStatusFinanceiro()
        {
            ddlSituacao.Items.Clear();
            ddlSituacao.Items.AddRange(AuxiliBaseApoio.BaseApoioDDL(statusFinanceiro.ResourceManager, todos: true));
        }

        //====> Método que carrega o DropDown de Unidades de Contrato
        private void CarregaUnidadeContrato()
        {
            ddlUnidadeContrato.DataSource = from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                            where tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                            select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP };

            ddlUnidadeContrato.DataTextField = "NO_FANTAS_EMP";
            ddlUnidadeContrato.DataValueField = "CO_EMP";
            ddlUnidadeContrato.DataBind();

            ddlUnidadeContrato.Items.Insert(0, new ListItem("Todas", ""));
        }

        //====> Método que carrega o DropDown Nome da Fontes com os Clientes
        private void CarregaFontesReceita()
        {
            ddlNomeFonte.DataSource = (from tb103 in TB103_CLIENTE.RetornaTodosRegistros()
                                       select new { tb103.CO_CLIENTE, tb103.NO_FAN_CLI }).OrderBy(c => c.NO_FAN_CLI);

            ddlNomeFonte.DataTextField = "NO_FAN_CLI";
            ddlNomeFonte.DataValueField = "CO_CLIENTE";
            ddlNomeFonte.DataBind();

            ddlNomeFonte.Items.Insert(0, new ListItem("Selecione", ""));
        }

        //====> Método que preenche informação de acordo com o Nome da Fonte selecionado
        private void CarregaCodigoFonte(bool flagAluno)
        {
            int coNomeFonte = ddlNomeFonte.SelectedValue != "" ? int.Parse(ddlNomeFonte.SelectedValue) : 0;

            txtCodigoFonte.Text = "";

            if (coNomeFonte == 0)
                return;

            if (flagAluno)
            {
                var tb07 = (from iTb07 in TB07_ALUNO.RetornaTodosRegistros()
                            where iTb07.CO_ALU == coNomeFonte
                            select new { iTb07.NU_NIRE });

                if (tb07.Count() > 0)
                {
                    txtCodigoFonte.Text = tb07.First().NU_NIRE.ToString();
                }
            }
            else
            {
                TB103_CLIENTE tb103 = TB103_CLIENTE.RetornaPelaChavePrimaria(coNomeFonte);
                if (tb103 != null)
                {
                    txtCodigoFonte.Text = tb103.TP_CLIENTE == "F" && tb103.CO_CPFCGC_CLI.Length == 11 ? tb103.CO_CPFCGC_CLI.Insert(9, "-").Insert(6, ".").Insert(3, ".") :
                    (tb103.TP_CLIENTE == "J" && tb103.CO_CPFCGC_CLI.Length == 14 ? tb103.CO_CPFCGC_CLI.Insert(12, "-").Insert(8, "/").Insert(5, ".").Insert(2, ".") : tb103.CO_CPFCGC_CLI);
                }

            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Histórico
        /// </summary>
        private void CarregaHistoricos()
        {
            ddlHistorico.DataSource = (from tb39 in TB39_HISTORICO.RetornaTodosRegistros()
                                       where tb39.FLA_TIPO_HISTORICO == "C"
                                       select new { tb39.CO_HISTORICO, tb39.DE_HISTORICO });

            ddlHistorico.DataTextField = "DE_HISTORICO";
            ddlHistorico.DataValueField = "CO_HISTORICO";
            ddlHistorico.DataBind();

            ddlHistorico.Items.Insert(0, new ListItem("Todos", "0"));
        }
        #endregion

        #region Eventos dos componentes

        protected void ddlTipoFonte_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoFonte.SelectedValue == "A")
            {
                CarregaAluno();
                CarregaCodigoFonte(true);
            }
            else if (ddlTipoFonte.SelectedValue == "O")
            {
                CarregaFontesReceita();
                CarregaCodigoFonte(false);
            }
            else
                ddlNomeFonte.Items.Clear();
        }

        protected void ddlNomeFonte_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCodigoFonte.Text = "";

            if (ddlTipoFonte.SelectedValue == "A")
                CarregaCodigoFonte(true);
            else if (ddlTipoFonte.SelectedValue == "O")
                CarregaCodigoFonte(false);
        }

        #endregion

        #region Classe
        /// <summary>
        /// Classe para listagem de títulos para carregamento da grid
        /// </summary>
        private class listaTítulos
        {
            private Dictionary<string, string> statusFi = AuxiliBaseApoio.chave(statusFinanceiro.ResourceManager, true);
            public string NU_DOC { get; set; }
            public int NU_PAR { get; set; }
            public DateTime DT_CAD_DOC { get; set; }
            public DateTime DT_VEN_DOC { get; set; }
            public DateTime? DT_REC_DOC { get; set; }
            public decimal VR_PAR_DOC { get; set; }
            private string codigoIC { get; set; }
            public string IC_SIT_DOC
            {
                get
                {
                    if (statusFi.Where(f => f.Key == this.codigoIC).Count() > 0)
                        return statusFi[this.codigoIC];
                    else
                        return "Não definido";
                }
                set 
                {
                    this.codigoIC = value;
                }
            }
            public decimal? VR_PAG { get; set; }
            public DateTime DT_EMISS_DOCTO { get; set; }
            public string CO_CON_RECFIX { get; set; }
            public int? CO_ADITI_RECFIX { get; set; }
            public string CEDENTE { get; set; }
            public string TFR { get; set; }
            private string descHistorico { get; set; }
            public string DE_HISTORICO { 
                get 
                {
                    return (this.descHistorico != null ? (this.descHistorico.Length > 60 ? (this.descHistorico.Substring(0, 60) + "...") : this.descHistorico) : "");
                }
                set 
                {
                    this.descHistorico = value; 
                }
            }
        }
        #endregion
    }
}