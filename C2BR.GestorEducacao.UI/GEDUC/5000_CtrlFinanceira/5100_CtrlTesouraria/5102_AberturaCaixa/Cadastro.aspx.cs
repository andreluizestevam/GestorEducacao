//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE TESOURARIA (CAIXA)
// OBJETIVO: REGISTRO DE ABERTURA DE CAIXA
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
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5100_CtrlTesouraria.F5102_AberturaCaixa
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string dataAtual = DateTime.Now.ToString("dd/MM/yyyy");
                CompareValidatorDataAtual.ValueToCompare = txtDataAbertura.Text = txtDataMovimento.Text = dataAtual;
                txtHoraAberCai.Text = DateTime.Now.ToString("HH:mm");
                CarregaCaixas();
                CarregaFuncionarios();
            }
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Caixas
        /// </summary>
        private void CarregaCaixas()
        {
            ddlNomeCaixa.DataSource = TB113_PARAM_CAIXA.RetornaTodosRegistros().Where(p => (p.CO_FLAG_USO_CAIXA == "F") && (p.CO_SITU_CAIXA.Equals("A"))
                                                                                      && (p.CO_EMP == LoginAuxili.CO_EMP));

            ddlNomeCaixa.DataTextField = "DE_CAIXA";
            ddlNomeCaixa.DataValueField = "CO_CAIXA";

            ddlNomeCaixa.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Funcionários de Caixa
        /// </summary>
        private void CarregaFuncionarios()
        {
            ddlFuncCaixa.DataSource = (from admUsuario in ADMUSUARIO.RetornaTodosRegistros()
                                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on admUsuario.CodUsuario equals tb03.CO_COL
                                       where admUsuario.CO_EMP == tb03.CO_EMP && admUsuario.FLA_MANUT_CAIXA == "S"
                                       && (admUsuario.TipoUsuario == "F" || admUsuario.TipoUsuario == "S")
                                       && admUsuario.CodInstituicao == LoginAuxili.ORG_CODIGO_ORGAO
                                       select new { tb03.CO_COL, tb03.NO_COL }).OrderBy(a => a.NO_COL);

            ddlFuncCaixa.DataTextField = "NO_COL";
            ddlFuncCaixa.DataValueField = "CO_COL";
            ddlFuncCaixa.DataBind();
        }
        #endregion

        protected void chkPerDescto_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPerDescto.Checked)
                txtPorcDescto.Enabled = true;
            else
            {
                txtPorcDescto.Enabled = false;
                txtPorcDescto.Text = "";
            }
        }

        protected void chkAboMulta_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAboMulta.Checked)
                txtPorcAboMulta.Enabled = true;
            else
            {
                txtPorcAboMulta.Enabled = false;
                txtPorcAboMulta.Text = "";
            }
        }

        protected void chkAboCorrecao_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAboCorrecao.Checked)
                txtPorcAboCorrecao.Enabled = true;
            else
            {
                txtPorcAboCorrecao.Enabled = false;
                txtPorcAboCorrecao.Text = "";
            }
        }

        protected void btnAberCaixa_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if ((chkAboCorrecao.Checked) && (txtPorcAboCorrecao.Text == ""))
                {
                    AuxiliPagina.EnvioMensagemErro(this, "% Abona Correção deve ser informado.");
                    return;
                }

                if ((chkAboMulta.Checked) && (txtPorcAboMulta.Text == ""))
                {
                    AuxiliPagina.EnvioMensagemErro(this, "% Abona Multa deve ser informado.");
                    return;
                }

                if ((chkPerDescto.Checked) && (chkPerDescto.Text == ""))
                {
                    AuxiliPagina.EnvioMensagemErro(this, "% Abona Desconto deve ser informado.");
                    return;
                }

                if (txtPorcAboCorrecao.Text != "")
                {
                    if (decimal.Parse(txtPorcAboCorrecao.Text) > 100)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "% Abona Correção não pode ser maior que 100%.");
                        return;
                    }
                }

                if (txtPorcDescto.Text != "")
                {
                    if (decimal.Parse(txtPorcDescto.Text) > 100)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "% Abona Desconto não pode ser maior que 100%.");
                        return;
                    }
                }

                if (txtPorcAboMulta.Text != "")
                {
                    if (decimal.Parse(txtPorcAboMulta.Text) > 100)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "% Abona Multa não pode ser maior que 100%.");
                        return;
                    }
                }

                int coCaixa = ddlNomeCaixa.SelectedValue != "" ? int.Parse(ddlNomeCaixa.SelectedValue) : 0;
                int coCol = ddlFuncCaixa.SelectedValue != "" ? int.Parse(ddlFuncCaixa.SelectedValue) : 0;
                DateTime dtMov = DateTime.Parse(txtDataMovimento.Text + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second);
                

                TB295_CAIXA tb295 = TB295_CAIXA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, coCaixa, coCol, dtMov);

                if (tb295 == null)
                {
                    if (string.IsNullOrWhiteSpace(txtValor.Text))
                    {
                        txtValor.Text = "0";
                    }
                    if (decimal.Parse(txtValor.Text) < 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Valor de abertura do caixa deve maior ou igual a 0.");
                        return;
                    }

                    tb295 = new TB295_CAIXA();

                    tb295.CO_CAIXA = coCaixa;
                    tb295.TB03_COLABOR = TB03_COLABOR.RetornaPeloCoCol(coCol);
                    tb295.DT_MOVIMENTO = dtMov;
                    tb295.DT_ABERTURA_CAIXA = DateTime.Now;
                    tb295.HR_ABERTURA_CAIXA = DateTime.Now.ToString("HH:mm");
                    tb295.VR_ABERTURA_CAIXA = decimal.Parse(txtValor.Text);
                    tb295.CO_USUARIO_ABERT = LoginAuxili.IDEADMUSUARIO;
                    tb295.VR_PERCENTUAL_ABONO_CORRECAO = txtPorcAboCorrecao.Text != "" ? (decimal?)decimal.Parse(txtPorcAboCorrecao.Text) : null;
                    tb295.VR_PERCENTUAL_ABONO_DESCONTO = txtPorcDescto.Text != "" ? (decimal?)decimal.Parse(txtPorcDescto.Text) : null;
                    tb295.VR_PERCENTUAL_ABONO_MULTA = txtPorcAboMulta.Text != "" ? (decimal?)decimal.Parse(txtPorcAboMulta.Text) : null;
                }
                else
                {
                    tb295.DT_FECHAMENTO_CAIXA = null;
                }

                if (GestorEntities.SaveOrUpdate(tb295) <= 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Erro ao abrir caixa.");
                    return;
                }

                TB113_PARAM_CAIXA tb113 = TB113_PARAM_CAIXA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, coCaixa);

                if (tb113 != null)
                {
                    tb113.CO_FLAG_USO_CAIXA = "A";
                    GestorEntities.SaveOrUpdate(tb113);
                }

                AuxiliPagina.RedirecionaParaPaginaSucesso("Caixa Aberto com sucesso", Request.Url.AbsoluteUri);
            }
        }
    }
}