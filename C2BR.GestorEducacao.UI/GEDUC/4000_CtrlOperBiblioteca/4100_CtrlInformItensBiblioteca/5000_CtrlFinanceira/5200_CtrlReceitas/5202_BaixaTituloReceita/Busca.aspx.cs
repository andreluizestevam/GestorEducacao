//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE RECEITAS (CONTAS A RECEBER)
// OBJETIVO: BAIXA DE TÍTULOS DE RECEITAS
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

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5200_CtrlReceitas.F5202_BaixaTituloReceita
{
    public partial class Busca : System.Web.UI.Page
    {
        public PadraoBuscas CurrentPadraoBuscas { get { return ((PadraoBuscas)this.Master); } }

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
            if (Page.IsPostBack)
                return;

            CarregaAluno();
        }
        
        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "NU_DOC", "NU_PAR", "CO_EMP", "DT_CAD_DOC" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NU_DOC",
                HeaderText = "N° Documento"
            });

            BoundField bf2 = new BoundField();
            bf2.DataField = "NU_PAR";
            bf2.HeaderText = "N° Par";
            bf2.ItemStyle.CssClass = "centro";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf2);

            BoundField bf11 = new BoundField();
            bf11.DataField = "DATA";
            bf11.HeaderText = "Dt Ref";
            bf11.DataFormatString = "{0:dd/MM/yy}";
            bf11.ItemStyle.CssClass = "centro";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf11);

            /*
            BoundField bf11 = new BoundField();
            bf11.DataField = "DT_EMISS_DOCTO";
            bf11.HeaderText = "Emissão";
            bf11.DataFormatString = "{0:dd/MM/yy}";
            bf11.ItemStyle.CssClass = "centro";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf11);

            BoundField bf3 = new BoundField();
            bf3.DataField = "DT_VEN_DOC";
            bf3.HeaderText = "Vencto";
            bf3.DataFormatString = "{0:dd/MM/yy}";
            bf3.ItemStyle.CssClass = "centro";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf3);
            */
            BoundField bf5 = new BoundField();
            bf5.DataField = "VR_PAR_DOC";
            bf5.HeaderText = "PAR";
            bf5.DataFormatString = "{0:N}";
            bf5.ItemStyle.CssClass = "direita";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf5);

            BoundField bf9 = new BoundField();
            bf9.DataField = "VALOR_CORRIGIDO";
            bf9.HeaderText = "Vl Corr";
            bf9.DataFormatString = "{0:N}";
            bf9.ItemStyle.CssClass = "direita";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf9);

            BoundField bf6 = new BoundField();
            bf6.DataField = "VR_PAG";
            bf6.HeaderText = "Vl Pag";
            bf6.DataFormatString = "{0:N}";
            bf6.ItemStyle.CssClass = "direita";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf6);

            BoundField bf7 = new BoundField();
            bf7.DataField = "IC_SIT_DOC";
            bf7.HeaderText = "Situação";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf7);

            BoundField bf10 = new BoundField();
            bf10.DataField = "sigla";
            bf10.HeaderText = "UNID CONT";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf10);

            BoundField bf8 = new BoundField();
            bf8.DataField = "CEDENTE";
            bf8.HeaderText = "Cedente";
            bf8.ItemStyle.CssClass = "direita";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf8);
        }
              
        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            string strTipoFonte = ddlTipoFonte.SelectedValue;
            string strTipoPeriodo = ddlTipoPeriodo.SelectedValue;
            string strSituacao = ddlSituacao.SelectedValue;
            int coNomeFonte = ddlNomeFonte.SelectedValue != "" ? int.Parse(ddlNomeFonte.SelectedValue) : 0;

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
                                 join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb47.CO_EMP_UNID_CONT equals tb25.CO_EMP into sr
                                 from x in sr.DefaultIfEmpty()
                                 where (tb47.NU_DOC == txtNumeroDocumento.Text || string.IsNullOrEmpty(txtNumeroDocumento.Text))
                                 && (dataVerifIni == null || (strTipoPeriodo == "V" && tb47.DT_VEN_DOC >= dataVerifIni) ||
                                 (strTipoPeriodo == "C" && tb47.DT_CAD_DOC >= dataVerifIni) || (strTipoPeriodo == "E" && tb47.DT_EMISS_DOCTO >= dataVerifIni))
                                 && (dataVerifFim == null || (strTipoPeriodo == "V" && tb47.DT_VEN_DOC <= dataVerifFim) ||
                                 (strTipoPeriodo == "C" && tb47.DT_CAD_DOC <= dataVerifFim) || (strTipoPeriodo == "E" && tb47.DT_EMISS_DOCTO <= dataVerifFim))
                                 && (strSituacao != "" ? tb47.IC_SIT_DOC == strSituacao : 0 == 0)
                                 && (tb47.TP_CLIENTE_DOC == "A" || tb47.TP_CLIENTE_DOC == "R") && tb47.CO_ALU == coNomeFonte && tb47.CO_EMP == LoginAuxili.CO_EMP && tb47.IC_SIT_DOC != "C"
                                 && tb47.IC_SIT_DOC != "Q"
                                 select new
                                 {
                                     tb47.NU_DOC, tb47.NU_PAR, tb47.DT_CAD_DOC, tb47.CO_EMP,
                                     tb47.VR_PAR_DOC, CEDENTE = tb47.NU_NIRE, tb47.VR_PAG,
                                     IC_SIT_DOC = tb47.IC_SIT_DOC.Equals("Q") ? "Quitado" : tb47.IC_SIT_DOC.Equals("P") ? "Parcialmente Quitado" : "Em Aberto",
                                     VALOR_CORRIGIDO = tb47.VR_PAR_DOC + tb47.MULTA + tb47.JUROS + tb47.VALOR_OUTRO - tb47.DESCTO - tb47.DESCTO_BOLSA,
                                     x.sigla,
                                     DATA = ddlTipoPeriodo.SelectedValue == "E" ? tb47.DT_EMISS_DOCTO : (ddlTipoPeriodo.SelectedValue == "C" ? tb47.DT_CAD_DOC : tb47.DT_VEN_DOC)
                                 });

                if (strTipoPeriodo == "C" && (dataVerifIni != null || dataVerifFim != null))
                    CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado.OrderByDescending( c => c.DATA ) : null;
                else
                    CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado.OrderByDescending(c => c.DATA) : null;
            }
            else if (strTipoFonte == "O" && ddlNomeFonte.SelectedValue != "")
            {
                var resultado = (from tb47 in VW47_CTA_RECEB.RetornaTodosRegistros()
                                 join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb47.CO_EMP_UNID_CONT equals tb25.CO_EMP into sr
                                 from x in sr.DefaultIfEmpty()
                                 where (txtNumeroDocumento.Text != "" ? tb47.NU_DOC == txtNumeroDocumento.Text : txtNumeroDocumento.Text == "")
                                && (dataVerifIni == null || (strTipoPeriodo == "V" && tb47.DT_VEN_DOC >= dataVerifIni) ||
                                (strTipoPeriodo == "C" && tb47.DT_CAD_DOC >= dataVerifIni) || (strTipoPeriodo == "E" && tb47.DT_EMISS_DOCTO >= dataVerifIni))
                                && (dataVerifFim == null || (strTipoPeriodo == "V" && tb47.DT_VEN_DOC <= dataVerifFim) ||
                                (strTipoPeriodo == "C" && tb47.DT_CAD_DOC <= dataVerifFim) || (strTipoPeriodo == "E" && tb47.DT_EMISS_DOCTO <= dataVerifFim))
                                && (strSituacao != "" ? tb47.IC_SIT_DOC == strSituacao : 0 == 0) && tb47.IC_SIT_DOC != "Q"
                                && tb47.TP_CLIENTE_DOC == "O" && tb47.CO_CLIENTE == coNomeFonte && tb47.CO_EMP == LoginAuxili.CO_EMP
                                select new
                                {
                                    tb47.NU_DOC, tb47.NU_PAR, tb47.VR_PAR_DOC, tb47.VR_PAG, tb47.DT_CAD_DOC, tb47.CO_EMP,
                                    CEDENTE = tb47.CO_CPFCGC_CLI.Length == 14 ? tb47.CO_CPFCGC_CLI.Insert(12, "-").Insert(8, "/").Insert(5, ".").Insert(2, ".") : tb47.CO_CPFCGC_CLI.Length == 11 ? tb47.CO_CPFCGC_CLI.Insert(9, "-").Insert(6, ".").Insert(3, ".") : tb47.CO_CPFCGC_CLI,
                                    IC_SIT_DOC = tb47.IC_SIT_DOC.Equals("Q") ? "Quitado" : tb47.IC_SIT_DOC.Equals("P") ? "Parcialmente Quitado" : "Em Aberto",
                                    VALOR_CORRIGIDO = tb47.VR_PAR_DOC + tb47.MULTA + tb47.JUROS + tb47.VALOR_OUTRO - tb47.DESCTO - tb47.DESCTO_BOLSA,
                                    x.sigla,
                                    DATA = ddlTipoPeriodo.SelectedValue == "E" ? tb47.DT_EMISS_DOCTO : (ddlTipoPeriodo.SelectedValue == "C" ? tb47.DT_CAD_DOC : tb47.DT_VEN_DOC)
                                });

                if (strTipoPeriodo == "C" && (dataVerifIni != null || dataVerifFim != null))
                    CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado.OrderByDescending( c => c.DATA ) : null;
                else
                    CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado.OrderByDescending( c => c.DATA ) : null;
            }
            else
            {
                var resultado = (from tb47 in VW47_CTA_RECEB.RetornaTodosRegistros()
                                 join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb47.CO_EMP_UNID_CONT equals tb25.CO_EMP into sr
                                 from x in sr.DefaultIfEmpty()
                                 where (txtNumeroDocumento.Text != "" ? tb47.NU_DOC == txtNumeroDocumento.Text : txtNumeroDocumento.Text == "")
                                && (dataVerifIni == null || (strTipoPeriodo == "V" && tb47.DT_VEN_DOC >= dataVerifIni) ||
                                (strTipoPeriodo == "C" && tb47.DT_CAD_DOC >= dataVerifIni) || (strTipoPeriodo == "E" && tb47.DT_EMISS_DOCTO >= dataVerifIni))
                                && (dataVerifFim == null || (strTipoPeriodo == "V" && tb47.DT_VEN_DOC <= dataVerifFim) ||
                                (strTipoPeriodo == "C" && tb47.DT_CAD_DOC <= dataVerifFim) || (strTipoPeriodo == "E" && tb47.DT_EMISS_DOCTO <= dataVerifFim))
                                && (strSituacao != "" ? tb47.IC_SIT_DOC == strSituacao : 0 == 0)
                                && tb47.CO_EMP == LoginAuxili.CO_EMP && tb47.IC_SIT_DOC != "C" && tb47.IC_SIT_DOC != "Q"
                                && tb47.TP_CLIENTE_DOC == ddlTipoFonte.SelectedValue
                                select new
                                {
                                    tb47.NU_DOC, tb47.NU_PAR, tb47.VR_PAR_DOC, tb47.VR_PAG, tb47.DT_CAD_DOC,
                                    CEDENTE = tb47.TP_CLIENTE_DOC == "A" ? tb47.NU_NIRE : 
                                    (tb47.CO_CPFCGC_CLI.Length == 14 ? tb47.CO_CPFCGC_CLI.Insert(12, "-").Insert(8, "/").Insert(5, ".").Insert(2, ".") : tb47.CO_CPFCGC_CLI.Length == 11 ? tb47.CO_CPFCGC_CLI.Insert(9, "-").Insert(6, ".").Insert(3, ".") : tb47.CO_CPFCGC_CLI),
                                    IC_SIT_DOC = tb47.IC_SIT_DOC.Equals("Q") ? "Quitado" : tb47.IC_SIT_DOC.Equals("P") ? "Parcialmente Quitado" : "Em Aberto",
                                    VALOR_CORRIGIDO = tb47.VR_PAR_DOC + tb47.MULTA + tb47.JUROS + tb47.VALOR_OUTRO - tb47.DESCTO - tb47.DESCTO_BOLSA, tb47.CO_EMP,
                                    x.sigla,
                                    DATA = ddlTipoPeriodo.SelectedValue == "E" ? tb47.DT_EMISS_DOCTO : (ddlTipoPeriodo.SelectedValue == "C" ? tb47.DT_CAD_DOC : tb47.DT_VEN_DOC)
                                });

                if (strTipoPeriodo == "C" && (dataVerifIni != null || dataVerifFim != null))
                    CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado.OrderByDescending(c => c.DATA) : null;
                else
                    CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado.OrderByDescending(c => c.DATA) : null;
            }
        }
        
        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>("doc", "NU_DOC"));
            queryStringKeys.Add(new KeyValuePair<string, string>("par", "NU_PAR"));
            queryStringKeys.Add(new KeyValuePair<string, string>("dtCadas", "DT_CAD_DOC"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento

//====> Método que carrega o DropDown Nome da Fonte com os Alunos
        private void CarregaAluno()
        {
            ddlNomeFonte.DataSource = (from tb07 in TB07_ALUNO.RetornaPelaUnid(LoginAuxili.CO_EMP)
                                       select new { tb07.CO_ALU, tb07.NO_ALU });

            ddlNomeFonte.DataTextField = "NO_ALU";
            ddlNomeFonte.DataValueField = "CO_ALU";
            ddlNomeFonte.DataBind();

            ddlNomeFonte.Items.Insert(0, new ListItem("Selecione", ""));
        }

//====> Método que carrega o DropDown Nome da Fonte com os Clientes
        private void CarregaFontesReceita()
        {
            ddlNomeFonte.DataSource = (from tb103 in TB103_CLIENTE.RetornaTodosRegistros()
                                       select new { tb103.CO_CLIENTE, tb103.NO_FAN_CLI }).OrderBy( c => c.NO_FAN_CLI );

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
        #endregion

        protected void ddlTipoFonte_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoFonte.SelectedValue == "A")
            {
                CarregaAluno();
                CarregaCodigoFonte(true);
            }
            else
            {
                CarregaFontesReceita();
                CarregaCodigoFonte(false);
            }
        }

        protected void ddlNomeFonte_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoFonte.SelectedValue == "A")
                CarregaCodigoFonte(true);
            else
                CarregaCodigoFonte(false);
        }
    }
}