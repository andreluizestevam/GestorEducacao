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
namespace C2BR.GestorEducacao.UI.GSAUD.F5000_CtrlFinanceira.F5200_CtrlReceitas.F5201_CadastramentoGeralTituReceita
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

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_ALU",
                HeaderText = "Paciente"
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
            string strTipoPeriodo = ddlTipoPeriodo.SelectedValue;
            string strSituacao = ddlSituacao.SelectedValue;
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

            var resultado = (from tb47 in TBS47_CTA_RECEB.RetornaTodosRegistros()
                             join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb47.CO_ALU equals tb07.CO_ALU
                             join tb39 in TB39_HISTORICO.RetornaTodosRegistros() on tb47.TB39_HISTORICO.CO_HISTORICO equals tb39.CO_HISTORICO into sr
                             from x in sr.DefaultIfEmpty()
                             where ((txtNumeroDocumento.Text != "" ? tb47.NU_DOC == txtNumeroDocumento.Text : txtNumeroDocumento.Text == "")
                                 && (dataVerifIni == null || (strTipoPeriodo == "V" && tb47.DT_VEN_DOC >= dataVerifIni) ||
                                 (strTipoPeriodo == "C" && tb47.DT_CAD_DOC >= dataVerifIni) || (strTipoPeriodo == "E" && tb47.DT_EMISS_DOCTO >= dataVerifIni))
                                 && (dataVerifFim == null || (strTipoPeriodo == "V" && tb47.DT_VEN_DOC <= dataVerifFim) ||
                                 (strTipoPeriodo == "C" && tb47.DT_CAD_DOC <= dataVerifFim) || (strTipoPeriodo == "E" && tb47.DT_EMISS_DOCTO <= dataVerifFim))
                                 && tb47.CO_EMP == LoginAuxili.CO_EMP
                                 && (coUnidContr != 0 ? tb47.CO_EMP_UNID_CONT == coUnidContr : coUnidContr == 0)
                                 && (coHisto != 0 ? x.CO_HISTORICO == coHisto : coHisto == 0))
                                 && (tb07.NO_ALU.Contains(txtNomePacPesq.Text))
                                 && (strSituacao != "-1" ? tb47.IC_SIT_DOC == strSituacao : 0 == 0)
                             select new listaTítulos
                             {
                                 NO_ALU = tb07.NO_ALU.Substring(0, 10) + "...",
                                 NU_DOC = tb47.NU_DOC,
                                 NU_PAR = tb47.NU_PAR,
                                 DT_CAD_DOC = tb47.DT_CAD_DOC,
                                 DT_VEN_DOC = tb47.DT_VEN_DOC,
                                 DT_REC_DOC = tb47.DT_REC_DOC,
                                 VR_PAR_DOC = tb47.VL_PAR_DOC,
                                 IC_SIT_DOC = tb47.IC_SIT_DOC,
                                 VR_PAG = tb47.VL_PAG,
                                 DT_EMISS_DOCTO = tb47.DT_EMISS_DOCTO,
                                 CO_CON_RECFIX = tb47.CO_CON_RECFIX,
                                 CO_ADITI_RECFIX = tb47.CO_ADITI_RECFIX,
                                 DE_HISTORICO = x.DE_HISTORICO
                             });

            if (resultado.Count() > 0)
            {
                if (strTipoPeriodo == "V" && (dataVerifIni != null || dataVerifFim != null))
                    if (ddlTipoOrdem.SelectedValue == "D")
                        resultado = resultado.OrderByDescending(c => c.DT_VEN_DOC);
                    else
                        resultado = resultado.OrderBy(c => c.DT_VEN_DOC);
                else if (strTipoPeriodo == "E" && (dataVerifIni != null || dataVerifFim != null))
                    if (ddlTipoOrdem.SelectedValue == "D")
                        resultado = resultado.OrderByDescending(c => c.DT_EMISS_DOCTO);
                    else
                        resultado = resultado.OrderBy(c => c.DT_EMISS_DOCTO);
                else
                    if (ddlTipoOrdem.SelectedValue == "D")
                        resultado = resultado.OrderByDescending(c => c.DT_CAD_DOC);
                    else
                        resultado = resultado.OrderBy(c => c.DT_CAD_DOC);

                CurrentPadraoBuscas.GridBusca.DataSource = resultado;
            }
            else
                CurrentPadraoBuscas.GridBusca.DataSource = null;
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
            public int NIRE { get; set; }
            public string NO_ALU { get; set; }
            private string descHistorico { get; set; }
            public string DE_HISTORICO
            {
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