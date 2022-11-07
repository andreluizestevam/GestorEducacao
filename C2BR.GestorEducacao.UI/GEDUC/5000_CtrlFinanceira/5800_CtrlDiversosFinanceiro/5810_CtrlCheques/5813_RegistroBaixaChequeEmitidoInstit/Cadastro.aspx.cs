//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE GERAL DE CHEQUES
// OBJETIVO: REGISTRO DE BAIXA DE CHEQUE EMITIDO OU RECEBIDO PELA INSTITUIÇÃO
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5800_CtrlDiversosFinanceiro.F5810_CtrlCheques.F5813_RegistroBaixaChequeEmitidoInstit
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos
        
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

//--------> Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            CarregaBancos();
            CarregaAgencias();
            divGrid.Visible = liTotal.Visible = false;
        }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            tb158_cheques tb158;
            int intQtdeAlter = 0;

//--------> Varre toda a grid de Cheques
            foreach (GridViewRow linha in grdCheques.Rows)
            {
                if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                {
                    intQtdeAlter++;

                    tb158 = RetornaEntidade(Convert.ToInt32(grdCheques.DataKeys[linha.RowIndex].Values[0]), Convert.ToInt32(grdCheques.DataKeys[linha.RowIndex].Values[1]));

                    tb158.ic_sit = "Q";
                    tb158.dt_sit = DateTime.Now;
                    tb158.observacao = ((TextBox)linha.Cells[4].FindControl("txtObs")).Text != "" ? ((TextBox)linha.Cells[6].FindControl("txtObs")).Text : "";

                    tb158_cheques.SaveOrUpdate(tb158, true);
                }
            }

            if (intQtdeAlter > 0)
                AuxiliPagina.RedirecionaParaPaginaSucesso("Registro Salvo com Sucesso", Request.Url.AbsoluteUri);
            else
                AuxiliPagina.EnvioMensagemErro(this, "Selecione um cheque para baixa.");

        }        
        #endregion

        #region Métodos

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <param name="coCheque">Id do cheque</param>
        /// <param name="codOrgao">Id da instituição</param>
        /// <returns>Entidade tb158_cheques</returns>
        private tb158_cheques RetornaEntidade(int coCheque, int codOrgao)
        {
            return tb158_cheques.RetornaTodosRegistros().Where(p => p.co_cheque == coCheque && p.ORG_CODIGO_ORGAO == codOrgao).FirstOrDefault();
        }     
        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega o dropdown de Agências
        /// </summary>
        private void CarregaAgencias()
        {
            string ideBanco = ddlBanco.SelectedValue != "" ? ddlBanco.SelectedValue : "0";

            ddlAgencia.DataSource = (from tb30 in TB30_AGENCIA.RetornaTodosRegistros()
                                     where tb30.IDEBANCO == ideBanco
                                     select new { tb30.CO_AGENCIA }).OrderBy(a => a.CO_AGENCIA);

            ddlAgencia.DataTextField = "CO_AGENCIA";
            ddlAgencia.DataValueField = "CO_AGENCIA";
            ddlAgencia.DataBind();

            ddlAgencia.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Bancos
        /// </summary>
        private void CarregaBancos()
        {
            ddlBanco.DataSource = TB29_BANCO.RetornaTodosRegistros();

            ddlBanco.DataTextField = "IDEBANCO";
            ddlBanco.DataValueField = "IDEBANCO";
            ddlBanco.DataBind();

            ddlBanco.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que preenche a grid de Cheques
        /// </summary>
        private void CarregaGrid()
        {
            string ideBanco = ddlBanco.SelectedValue != "" ? ddlBanco.SelectedValue : "0";
            int coAgencia = ddlAgencia.SelectedValue != "" ? int.Parse(ddlAgencia.SelectedValue) : 0;

            if (ideBanco == "0" || coAgencia == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Banco e agência devem ser selecionados");
                grdCheques.DataBind();
                return;
            }

            var listaCheques = (from tb158 in tb158_cheques.RetornaTodosRegistros()
                                where tb158.TB29_BANCO.IDEBANCO == ideBanco 
                                && tb158.co_agencia == coAgencia 
                                && tb158.ic_sit == "A"
                                && tb158.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                select new
                                {
                                    tb158.co_cheque, 
                                    tb158.TB000_INSTITUICAO.ORG_CODIGO_ORGAO, 
                                    tb158.nu_conta, 
                                    tb158.nu_cheque,
                                    tb158.ic_sit, 
                                    tb158.valor, 
                                    tb158.observacao, 
                                    tb158.dt_vencimento, 
                                    tb158.nu_doc,
                                    tb158.dt_sit
                                }).ToList().OrderBy( c => c.dt_vencimento );

            divGrid.Visible = true;

            if (listaCheques.Count() > 0)
            {
                // Habilita o botão de salvar
                liTotal.Visible = true;
            }
            else
            {
                grdCheques.DataBind();
                txtTotCheques.Text = "";
                liTotal.Visible = false;
                return;
            }

            grdCheques.DataKeyNames = new string[] { "CO_CHEQUE", "ORG_CODIGO_ORGAO" };

            grdCheques.DataSource = listaCheques;
            grdCheques.DataBind();

            txtTotCheques.Text = grdCheques.Rows.Count.ToString();
        }   
        #endregion

        protected void ddlAgencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGrid();
        }

        protected void grdCheques_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            double doubleValorTotal = 0;

            foreach (GridViewRow linha in grdCheques.Rows)
            {
                if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                    ((TextBox)linha.Cells[6].FindControl("txtObs")).Enabled = true;
                else
                    ((TextBox)linha.Cells[6].FindControl("txtObs")).Enabled = false;

                doubleValorTotal = doubleValorTotal + double.Parse(linha.Cells[4].Text);
            }
            txtTotValor.Text = String.Format("{0:0.00}", doubleValorTotal);
        }
        
        protected void ddlBanco_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAgencias();
        }

        protected void ckSelect_CheckedChanged(object sender, EventArgs e)
        {
//--------> Varre toda a grid de Cheques
            foreach (GridViewRow linha in grdCheques.Rows)
            {

                if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                    ((TextBox)linha.Cells[6].FindControl("txtObs")).Enabled = true;
                else
                    ((TextBox)linha.Cells[6].FindControl("txtObs")).Enabled = false;                
            }
        }        
    }
}