//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: ENCERRAMENTO ATIVIDADES DO PERÍODO LETIVO
// OBJETIVO: PROCESSO DE FINALIZAÇÃO DE MATRÍCULA (ANO LETIVO)
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA      |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// -----------+----------------------------+-------------------------------------
// 31/05/2013 | André Nobre Vinagre        | Corrigida a questão da atualização do titulo
//            |                            | na tb47, tb321 e tb045
//            |                            | 
// -----------+----------------------------+-------------------------------------
// 05/08/2013 | André Nobre Vinagre        | Corrigida inconsistencia no valor pago e na atualização
//            |                            | na tb47, tb321 e tb045
//            |                            | 

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.UI.GEDUC._5000_CtrlFinanceira._5200_CtrlReceitas._5204_LerArquivoRetorno;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GSAUD._5000_CtrlFinanceira._5200_CtrlReceitas._5205_BaixaTituloBoleto
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            divGrid.Visible = false;
        }

        #endregion

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            DateTime dtInicio = DateTime.ParseExact(txtDataPeriodoIni.Text, "dd/MM/yyyy", null);
            DateTime dtFim = DateTime.ParseExact(txtDataPeriodoFim.Text, "dd/MM/yyyy", null);

            string flag = ddlStatus.SelectedValue;


            var resultado = (from r in TB321_ARQ_RET_BOLETO.RetornaTodosRegistros()
                                    where r.DT_VENCIMENTO >= dtInicio
                                    && r.DT_VENCIMENTO <= dtFim
                                    && (flag != "T" ? r.FL_STATUS == flag : (r.FL_STATUS == "P" || r.FL_STATUS == "C" || r.FL_STATUS == "I" || r.FL_STATUS == "B"))
                                    select new
                                    {
                                        NossoNumero = r.NU_NOSSO_NUMERO,
                                        DtVencimento = r.DT_VENCIMENTO,
                                        Valor = r.VL_TITULO,
                                        DtPagamento = r.DT_CREDITO,
                                        Status = r.FL_STATUS == "P" ? "Pend. Baixa" : r.FL_STATUS == "C" ? "Cancelado" : r.FL_STATUS == "B" ? "Baixado" : "Inconsistente",
                                        Lote = r.NU_LOTE_ARQUI,
                                        chkSt = r.FL_STATUS == "B" ? false : true,
                                        NU_DOC = r.NU_DCTO_RECEB
                                    }).ToList();

            grdResumo.DataSource = from res in resultado
                                   select new
                                   {
                                       res.NossoNumero, res.DtPagamento, res.DtVencimento, res.Valor, res.Status,
                                       Lote = res.Lote.ToString().PadLeft(4,'0'),
                                       res.chkSt,
                                       NU_DOC = res.NU_DOC
                                   };

            grdResumo.DataBind();
            divGrid.Visible = liObser.Visible = liResumo.Visible = true;
        }

        protected void btnGerar_Click(object sender, EventArgs e)
        {
            //List<string> lstNossoNumero = new List<string>();
            List<string> lstNuDoc = new List<string>();

            // Varre toda a grid de Pesquisa
            foreach (GridViewRow linha in grdResumo.Rows)
            {
                // Verifica se linha está checada
                if (((CheckBox)linha.Cells[0].FindControl("chkSelect")).Checked)
                {
                    //string nossoNumero = linha.Cells[1].Text;
                    string nuDoc = ((HiddenField)linha.Cells[0].FindControl("hidNuDoc")).Value;

                    // Faz a adicão do item
                    //lstNossoNumero.Add(nossoNumero);
                    lstNuDoc.Add(nuDoc);
                }
            }

            if (lstNuDoc.Count == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Selecione os boletos a serem baixados.");
                return;
            }

            GestorEntities ctx = new GestorEntities();

            //var lstTit = from b in ctx.TB045_NOS_NUM
            //             join c in ctx.TBS47_CTA_RECEB on new { b.CO_EMP, b.NU_DOC, b.NU_PAR, b.DT_CAD_DOC } equals new { c.CO_EMP, c.NU_DOC, c.NU_PAR, c.DT_CAD_DOC }
            //             where lstNossoNumero.Contains(b.CO_NOS_NUM)
            //             //&& b.IC_SIT_DOC == "A"
            //             select c;

            var lstTit = from c in ctx.TBS47_CTA_RECEB
                         where lstNuDoc.Contains(c.NU_DOC)
                         //&& b.IC_SIT_DOC == "A"
                         select c;

            foreach (var tit in lstTit)
            {
                if (tit.IC_SIT_DOC == "A")
                {                 
                    var boleto = ctx.TB321_ARQ_RET_BOLETO.FirstOrDefault(x => x.NU_DCTO_RECEB == tit.NU_DOC);
                    if (boleto == null)
                        continue;

                    tit.IC_SIT_DOC = "Q";
                    tit.DT_REC_DOC = boleto.DT_CREDITO;
                    tit.DT_MOV_CAIXA = boleto.DT_CREDITO;
                    tit.VL_PAG = boleto.VL_PAGO;
                    tit.CO_COL_BAIXA = LoginAuxili.CO_COL;
                    tit.VL_JUR_PAG = boleto.VL_JUROS;
                    tit.VL_MUL_PAG = boleto.VL_MULTA;
                    tit.VL_DES_PAG = boleto.VL_DESCTO;
                    tit.FL_ORIGEM_PGTO = "B";
                    tit.FL_TIPO_RECEB = "B";

                    var nosNum = ctx.TB045_NOS_NUM.FirstOrDefault(x => x.CO_EMP == tit.CO_EMP && x.NU_DOC == tit.NU_DOC && x.NU_PAR == tit.NU_PAR && x.DT_CAD_DOC == tit.DT_CAD_DOC && lstNuDoc.Contains(x.NU_DOC));

                    if (nosNum != null)
                    {
                        //===> Altera o status do nossonúmero na tabela TB045
                        nosNum.FL_PGTO = "Q";
                        nosNum.DT_PGTO = boleto.DT_CREDITO;
                        nosNum.DT_REC = DateTime.Now;
                        nosNum.TB321_ARQ_RET_BOLETO = boleto;
                    }
                    

                    boleto.FL_STATUS = "B";
                }
                else if (tit.IC_SIT_DOC == "C")
                {
                    var boleto = ctx.TB321_ARQ_RET_BOLETO.FirstOrDefault(x => x.NU_DCTO_RECEB == tit.NU_DOC);                    
                    if (boleto == null)
                        continue;
                    boleto.FL_STATUS = "C";

                    var nosNum = ctx.TB045_NOS_NUM.FirstOrDefault(x => x.CO_EMP == tit.CO_EMP && x.NU_DOC == tit.NU_DOC && x.NU_PAR == tit.NU_PAR && x.DT_CAD_DOC == tit.DT_CAD_DOC);
                    if (nosNum != null)
                    {
                        //===> Altera o status do nossonúmero na tabela TB045
                        nosNum.FL_PGTO = "C";
                        nosNum.TB321_ARQ_RET_BOLETO = boleto;
                    }                    
                }
                else if (tit.IC_SIT_DOC == "Q" || tit.IC_SIT_DOC == "P")
                {
                    var boleto = ctx.TB321_ARQ_RET_BOLETO.FirstOrDefault(x => x.NU_DCTO_RECEB == tit.NU_DOC);                    
                    if (boleto == null)
                        continue;
                    boleto.FL_STATUS = "Q";

                    var nosNum = ctx.TB045_NOS_NUM.FirstOrDefault(x => x.CO_EMP == tit.CO_EMP && x.NU_DOC == tit.NU_DOC && x.NU_PAR == tit.NU_PAR && x.DT_CAD_DOC == tit.DT_CAD_DOC);

                    if (nosNum != null)
                    {
                        //===> Altera o status do nossonúmero na tabela TB045
                        nosNum.FL_PGTO = "Q";
                        nosNum.TB321_ARQ_RET_BOLETO = boleto;
                    }                    
                }
            }

            ctx.SaveChanges();

            AuxiliPagina.RedirecionaParaPaginaSucesso("Baixa de Títulos Efetuada com sucesso", Request.Url.AbsoluteUri);
        }

        protected void chkSelTodos_CheckChanged(object sender, EventArgs e)
        {
            CheckBox chkSelTodos = (CheckBox)sender;

            if (chkSelTodos.Checked)
            {
                selecionaTodos(true);
            }
            else
            {
                selecionaTodos(false);
            }
        }

        protected void selecionaTodos(bool st)
        {
            foreach (GridViewRow linha in grdResumo.Rows)
            {
                if (((CheckBox)linha.Cells[0].FindControl("chkSelect")).Enabled != false)
                {
                    ((CheckBox)linha.Cells[0].FindControl("chkSelect")).Checked = st;
                }
            }
        }
    }
}