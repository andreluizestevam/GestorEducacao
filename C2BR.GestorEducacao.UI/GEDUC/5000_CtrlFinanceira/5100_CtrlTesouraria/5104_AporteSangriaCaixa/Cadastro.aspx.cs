//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE TESOURARIA (CAIXA)
// OBJETIVO: REGISTRO DE APORTE OU SANGRIA DE CAIXA
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
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5100_CtrlTesouraria.F5104_AporteSangriaCaixa
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                hdCodCola.Value = txtTotalRec.Text = "0";
                CarregaCaixas();
                MontaGridFormPagamento();
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método que carrega o dropdown de Caixas
        /// </summary>
        private void CarregaCaixas()
        {
            ddlNomeCaixa.DataSource = TB113_PARAM_CAIXA.RetornaTodosRegistros().Where(p => (p.CO_FLAG_USO_CAIXA == "A") && (p.CO_SITU_CAIXA == "A") 
                                                                                            && (p.CO_EMP == LoginAuxili.CO_EMP));

            ddlNomeCaixa.DataTextField = "DE_CAIXA";
            ddlNomeCaixa.DataValueField = "CO_CAIXA";
            ddlNomeCaixa.DataBind();

            int coCaixa = ddlNomeCaixa.SelectedValue != "" ? int.Parse(ddlNomeCaixa.SelectedValue) : 0;

            if (coCaixa != 0)
            {
                ddlFuncCaixa.Enabled = txtSenhaApoSan.Enabled = grdFormPag.Enabled = true;
                CarregaFuncionarios();

                var varTb295 = (from tb295 in TB295_CAIXA.RetornaTodosRegistros()
                                where tb295.TB03_COLABOR.CO_EMP == LoginAuxili.CO_EMP && tb295.CO_CAIXA == coCaixa && tb295.DT_FECHAMENTO_CAIXA == null
                                select new { tb295.TB03_COLABOR.NO_COL, tb295.TB03_COLABOR.CO_COL }).FirstOrDefault();

                if (varTb295 != null)
                {
                    txtOperCaixa.Text = varTb295.NO_COL;
                    hdCodCola.Value = varTb295.CO_COL.ToString();
                }
            }
            else
            {
                ddlFuncCaixa.Items.Clear();
                ddlFuncCaixa.Enabled = txtSenhaApoSan.Enabled = txtSenhaOpeCai.Enabled = grdFormPag.Enabled = false;
                MontaGridFormPagamento();                
                txtOperCaixa.Text = txtSenhaApoSan.Text = txtSenhaOpeCai.Text = "";
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Funcionários
        /// </summary>
        private void CarregaFuncionarios()
        {
            ddlFuncCaixa.DataSource = (from admUsuario in ADMUSUARIO.RetornaTodosRegistros()
                                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on admUsuario.CodUsuario equals tb03.CO_COL
                                       where admUsuario.CO_EMP == tb03.CO_EMP && admUsuario.FLA_MANUT_CAIXA == "S"
                                       && (admUsuario.TipoUsuario == "F" || admUsuario.TipoUsuario == "S") && admUsuario.CO_EMP == LoginAuxili.CO_EMP
                                       select new { admUsuario.ideAdmUsuario, tb03.NO_COL }).OrderBy( a => a.NO_COL );

            ddlFuncCaixa.DataTextField = "NO_COL";
            ddlFuncCaixa.DataValueField = "ideAdmUsuario";
            ddlFuncCaixa.DataBind();
        }

        /// <summary>
        /// Método que monta a grid de Forma de Pagamento
        /// </summary>
        protected void MontaGridFormPagamento()
        {
            grdFormPag.DataKeyNames = new string[] { "CO_TIPO_REC" };

            grdFormPag.DataSource = from tb118 in TB118_TIPO_RECEB.RetornaTodosRegistros()
                                    select new { tb118.CO_TIPO_REC, tb118.DE_SIG_RECEB, tb118.DE_RECEBIMENTO };
            grdFormPag.DataBind();
        }
        #endregion

        protected void btnAporte_Click(object sender, EventArgs e)
        {
            decimal dcmTotalValor = 0;
            int idUsuario = int.Parse(ddlFuncCaixa.SelectedValue);
            string desSenha = LoginAuxili.GerarMD5(txtSenhaApoSan.Text);

            var usuario = ADMUSUARIO.RetornaTodosRegistros().Where(p => p.ideAdmUsuario == idUsuario && p.desSenha == desSenha).FirstOrDefault();

            if (usuario == null)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Senha de Usuário Responsável incorreta. Tente novamente.");
                return;
            }

            int codOpeCai = int.Parse(hdCodCola.Value);
            desSenha = LoginAuxili.GerarMD5(txtSenhaOpeCai.Text);

            var usuaOpeCai = ADMUSUARIO.RetornaTodosRegistros().Where(p => p.CodUsuario == codOpeCai && p.CO_EMP == LoginAuxili.CO_EMP && 
                                                                           p.desSenha == desSenha).FirstOrDefault();

            if (usuaOpeCai == null)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Senha do Operador do Caixa incorreta. Tente novamente.");
                return;
            }

            int coCaixa = ddlNomeCaixa.SelectedValue != "" ? int.Parse(ddlNomeCaixa.SelectedValue) : 0;

            var varTb295 = (from tb295 in TB295_CAIXA.RetornaTodosRegistros()
                            where tb295.TB03_COLABOR.CO_EMP == LoginAuxili.CO_EMP && tb295.CO_CAIXA == coCaixa && tb295.DT_FECHAMENTO_CAIXA == null
                            select tb295).FirstOrDefault();

            if (varTb295 != null)
            {
                foreach (GridViewRow linha in grdFormPag.Rows)
                {
                    if (((TextBox)linha.Cells[2].FindControl("txtValorFP")).Text != "")
                    {
                        if (Decimal.Parse(((TextBox)linha.Cells[2].FindControl("txtValorFP")).Text) != 0)
                        {
                            TB297_OPER_CAIXA operCaixa = new TB297_OPER_CAIXA();

                            varTb295.TB03_COLABORReference.Load();

                            operCaixa.TB295_CAIXA = TB295_CAIXA.RetornaPelaChavePrimaria(varTb295.TB03_COLABOR.CO_EMP, varTb295.CO_CAIXA, varTb295.TB03_COLABOR.CO_COL, varTb295.DT_MOVIMENTO);
                            operCaixa.CO_USUARIO = int.Parse(ddlFuncCaixa.SelectedValue);
                            operCaixa.FLA_TIPO = ddlTpMov.SelectedValue;
                            operCaixa.VALOR = Decimal.Parse(((TextBox)linha.Cells[2].FindControl("txtValorFP")).Text);
                            int coTpRec = int.Parse(((HiddenField)linha.Cells[2].FindControl("hdCO_TIPO_REC")).Value);
                            operCaixa.TB118_TIPO_RECEB = TB118_TIPO_RECEB.RetornaPelaChavePrimaria(coTpRec);
                            dcmTotalValor = dcmTotalValor + Decimal.Parse(((TextBox)linha.Cells[2].FindControl("txtValorFP")).Text);
                            operCaixa.DT_CADASTRO = DateTime.Now;
                            operCaixa.HR_CADASTRO = DateTime.Now.ToString("HH:mm");

                            TB297_OPER_CAIXA.SaveOrUpdate(operCaixa, true);
                        }
                    }
                }

                if (ddlTpMov.SelectedValue == "A")
                    varTb295.VR_APORTE_CAIXA = varTb295.VR_APORTE_CAIXA != null ? varTb295.VR_APORTE_CAIXA + dcmTotalValor : dcmTotalValor;
                else
                    varTb295.VR_SANGRIA_CAIXA = varTb295.VR_SANGRIA_CAIXA != null ? varTb295.VR_SANGRIA_CAIXA + dcmTotalValor : dcmTotalValor;

                TB295_CAIXA.SaveOrUpdate(varTb295, true);
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this, "Caixa não foi encontrado.");
                return;
            }

            AuxiliPagina.RedirecionaParaPaginaSucesso("Operação Realizada com sucesso", Request.Url.AbsoluteUri);
        }

        protected void ddlNomeCaixa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlNomeCaixa.Items.Count > 0)
            {
                CarregaFuncionarios();
                ddlFuncCaixa.Enabled = txtSenhaApoSan.Enabled = grdFormPag.Enabled = true;
                txtSenhaApoSan.Text = txtOperCaixa.Text = "";                

                int coCaixa = ddlNomeCaixa.SelectedValue != "" ? int.Parse(ddlNomeCaixa.SelectedValue) : 0;

                var varTb295 = (from tb295 in TB295_CAIXA.RetornaTodosRegistros()
                                where tb295.TB03_COLABOR.CO_EMP == LoginAuxili.CO_EMP && tb295.CO_CAIXA == coCaixa && tb295.DT_FECHAMENTO_CAIXA == null
                                select new { tb295.TB03_COLABOR.NO_COL, tb295.TB03_COLABOR.CO_COL }).FirstOrDefault();

                if (varTb295 != null)
                {
                    txtOperCaixa.Text = varTb295.NO_COL;
                    hdCodCola.Value = varTb295.CO_COL.ToString();
                }
                else
                {
                    txtOperCaixa.Text = "";
                    hdCodCola.Value = "0";
                }

            }
            else
            {
                ddlFuncCaixa.Items.Clear();
                ddlFuncCaixa.Enabled = grdFormPag.Enabled = txtSenhaApoSan.Enabled = false;
                txtSenhaApoSan.Text = txtOperCaixa.Text = "";
                MontaGridFormPagamento();                
            }
        }

        protected void txtValorFP_TextChanged(object sender, EventArgs e)
        {
            decimal dcmValorTotal = 0;

            foreach (GridViewRow linha in grdFormPag.Rows)
            {
                if (((TextBox)linha.Cells[2].FindControl("txtValorFP")).Text != "")
                {
                    if (Decimal.Parse(((TextBox)linha.Cells[2].FindControl("txtValorFP")).Text) != 0)
                        dcmValorTotal = dcmValorTotal + Decimal.Parse(((TextBox)linha.Cells[2].FindControl("txtValorFP")).Text);
                }
            }

            txtTotalRec.Text = dcmValorTotal.ToString();
        }
    }
}