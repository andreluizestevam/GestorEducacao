//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE TESOURARIA (CAIXA)
// OBJETIVO: REGISTRO DE RECEBIMENTO OU PAGAMENTO DE COMPROMISSOS FINANCEIROS
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
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5100_CtrlTesouraria.F5103_RecebPagamCompromisso
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                CarregaCaixas();
        }
        #endregion

        #region Carregamento DropDown

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

            if (ddlNomeCaixa.Items.Count > 0)
            {
                txtSenha.Enabled = true;
                int coCaixa = ddlNomeCaixa.SelectedValue != "" ? int.Parse(ddlNomeCaixa.SelectedValue) : 0;

                var varTb295 = (from tb295 in TB295_CAIXA.RetornaTodosRegistros()
                                join tb113 in TB113_PARAM_CAIXA.RetornaTodosRegistros() on tb295.CO_CAIXA equals tb113.CO_CAIXA
                                where tb295.CO_EMP.Equals(LoginAuxili.CO_EMP) && tb295.CO_CAIXA == coCaixa && tb295.DT_FECHAMENTO_CAIXA == null
                                && tb295.TB03_COLABOR.CO_EMP == LoginAuxili.CO_EMP && tb113.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                select new
                                {
                                    tb295.DT_ABERTURA_CAIXA, tb295.DT_FECHAMENTO_CAIXA, tb295.VR_ABERTURA_CAIXA, tb295.DT_MOVIMENTO, tb295.HR_ABERTURA_CAIXA,
                                    tb295.TB03_COLABOR.NO_COL, tb295.VR_PERCENTUAL_ABONO_CORRECAO, tb295.VR_PERCENTUAL_ABONO_DESCONTO,
                                    tb295.VR_PERCENTUAL_ABONO_MULTA, tb113.CO_SIGLA_CAIXA
                                }).FirstOrDefault();

                if (varTb295 != null)
                {
                    txtDataMovimento.Text = varTb295.DT_MOVIMENTO.ToString("dd/MM/yyyy");
                    txtValor.Text = varTb295.VR_ABERTURA_CAIXA.ToString();
                    txtDataAbertura.Text = varTb295.DT_ABERTURA_CAIXA.ToString("dd/MM/yyyy");
                    txtHoraAberCai.Text = varTb295.HR_ABERTURA_CAIXA;
                    txtRespAbert.Text = varTb295.NO_COL != null ? varTb295.NO_COL : "";
                    txtPorcAboCorrecao.Text = varTb295.VR_PERCENTUAL_ABONO_CORRECAO != null ? varTb295.VR_PERCENTUAL_ABONO_CORRECAO.ToString() : "";
                    txtPorcAboMulta.Text = varTb295.VR_PERCENTUAL_ABONO_MULTA != null ? varTb295.VR_PERCENTUAL_ABONO_MULTA.ToString() : "";
                    txtPorcDescto.Text = varTb295.VR_PERCENTUAL_ABONO_DESCONTO != null ? varTb295.VR_PERCENTUAL_ABONO_DESCONTO.ToString() : "";
                    txtSiglaCaixa.Text = varTb295.CO_SIGLA_CAIXA != null ? varTb295.CO_SIGLA_CAIXA : "";
                }
                else
                {
                    txtDataMovimento.Text = txtValor.Text = txtDataAbertura.Text = txtHoraAberCai.Text = txtRespAbert.Text =
                    txtPorcAboCorrecao.Text = txtPorcAboMulta.Text = txtPorcDescto.Text = txtSiglaCaixa.Text = "";
                }
            }
            else
            {
                txtSenha.Enabled = false;
                txtDataMovimento.Text = txtValor.Text = txtDataAbertura.Text = txtHoraAberCai.Text = txtRespAbert.Text =
                txtPorcAboCorrecao.Text = txtPorcAboMulta.Text = txtPorcDescto.Text = txtSiglaCaixa.Text = "";
            }
        }
        #endregion

        protected void btnAbreCaixa_Click(object sender, EventArgs e)
        {
            int coCaixa = ddlNomeCaixa.SelectedValue != "" ? int.Parse(ddlNomeCaixa.SelectedValue) : 0;
            string strSenhaMD5 = LoginAuxili.GerarMD5(txtSenha.Text);

            var varTb295 = (from tb295 in TB295_CAIXA.RetornaTodosRegistros()
                            join admUsuario in ADMUSUARIO.RetornaTodosRegistros()
                            on tb295.TB03_COLABOR.CO_COL equals admUsuario.CodUsuario
                            where tb295.TB03_COLABOR.CO_EMP == LoginAuxili.CO_EMP && tb295.CO_CAIXA == coCaixa
                            && tb295.DT_FECHAMENTO_CAIXA == null && admUsuario.desSenha == strSenhaMD5
                            select new { tb295.DT_ABERTURA_CAIXA }).FirstOrDefault();

            if (varTb295 != null)
            {
                this.Session[SessoesHttp.CodigoCaixa] = coCaixa;
                AuxiliPagina.RedirecionaParaPaginaSucesso("Autenticação do Caixa Efetuado com sucesso", "/GEDUC/5000_CtrlFinanceira/5100_CtrlTesouraria/5103_RecebPagamCompromisso/MovimentacaoCaixa.aspx?moduloId=692&moduloNome=Registro+de+Recebimento+ou+Pagamento+de+Compromissos+Financeiros");
            }
            else
                AuxiliPagina.EnvioMensagemErro(this, "Senha incorreta.");
        }

        protected void ddlNomeCaixa_SelectedIndexChanged(object sender, EventArgs e)
        {
            int coCaixa = ddlNomeCaixa.SelectedValue != "" ? int.Parse(ddlNomeCaixa.SelectedValue) : 0;

            if (coCaixa != 0)
            {
                txtSenha.Enabled = true;

                var varTb295 = (from tb295 in TB295_CAIXA.RetornaTodosRegistros()
                                join tb113 in TB113_PARAM_CAIXA.RetornaTodosRegistros() on tb295.CO_CAIXA equals tb113.CO_CAIXA
                                where tb295.CO_EMP.Equals(LoginAuxili.CO_EMP) && tb295.CO_CAIXA == coCaixa && tb295.DT_FECHAMENTO_CAIXA == null
                                && tb295.TB03_COLABOR.CO_EMP == LoginAuxili.CO_EMP && tb113.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                select new
                                {
                                    tb295.DT_ABERTURA_CAIXA, tb295.DT_FECHAMENTO_CAIXA, tb295.VR_ABERTURA_CAIXA, tb295.DT_MOVIMENTO,
                                    tb295.HR_ABERTURA_CAIXA, tb295.TB03_COLABOR.NO_COL, tb295.VR_PERCENTUAL_ABONO_CORRECAO, tb295.VR_PERCENTUAL_ABONO_DESCONTO,
                                    tb295.VR_PERCENTUAL_ABONO_MULTA, tb113.CO_SIGLA_CAIXA
                                }).FirstOrDefault();

                if (varTb295 != null)
                {
                    txtDataMovimento.Text = varTb295.DT_MOVIMENTO.ToString("dd/MM/yyyy");
                    txtValor.Text = varTb295.VR_ABERTURA_CAIXA.ToString();
                    txtDataAbertura.Text = varTb295.DT_ABERTURA_CAIXA.ToString("dd/MM/yyyy");
                    txtHoraAberCai.Text = varTb295.HR_ABERTURA_CAIXA;
                    txtRespAbert.Text = varTb295.NO_COL != null ? varTb295.NO_COL : "";
                    txtPorcAboCorrecao.Text = varTb295.VR_PERCENTUAL_ABONO_CORRECAO != null ? varTb295.VR_PERCENTUAL_ABONO_CORRECAO.ToString() : "";
                    txtPorcAboMulta.Text = varTb295.VR_PERCENTUAL_ABONO_DESCONTO != null ? varTb295.VR_PERCENTUAL_ABONO_DESCONTO.ToString() : "";
                    txtPorcDescto.Text = varTb295.VR_PERCENTUAL_ABONO_MULTA != null ? varTb295.VR_PERCENTUAL_ABONO_MULTA.ToString() : "";
                    txtSiglaCaixa.Text = varTb295.CO_SIGLA_CAIXA != null ? varTb295.CO_SIGLA_CAIXA : "";
                }
                else
                {
                    txtDataMovimento.Text = txtValor.Text = txtDataAbertura.Text = txtHoraAberCai.Text = txtRespAbert.Text =
                    txtPorcAboCorrecao.Text = txtPorcAboMulta.Text = txtPorcDescto.Text = txtSiglaCaixa.Text = "";
                }
            }
            else
            {
                txtSenha.Enabled = false;
                txtDataMovimento.Text = txtValor.Text = txtDataAbertura.Text = txtHoraAberCai.Text = txtRespAbert.Text =
                txtPorcAboCorrecao.Text = txtPorcAboMulta.Text = txtPorcDescto.Text = txtSiglaCaixa.Text = "";
            }
        }
    }
}