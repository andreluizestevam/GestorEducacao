//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: CONTROLE DE MENSAGENS SMS
// OBJETIVO: ENVIO DE SMS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0600_CtrlMensagensSMS.F0601_EnvioSMS
{
    public partial class CadEnvioSMS : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string flaSMS = ADMUSUARIO.RetornaPelaChavePrimaria(LoginAuxili.IDEADMUSUARIO).FLA_SMS;

                if (AuxiliValidacao.IsConnected())
                {
                    if (flaSMS == "S")
                    {
                        txtEmissor.Text = LoginAuxili.NOME_USU_LOGADO;
                        CarregaDropDown();
                        CarregaDestinatarios();
                    }
                    else
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Funcionalidade não habilitada para esse usuário.");
                        btnEnviarSMS.Enabled = false;
                    }
                }
                else
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Internet não disponível para envio de SMS.");
                    btnEnviarSMS.Enabled = false;
                }
            }
        }
        #endregion

        #region Carregamento
        
        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaDropDown()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).OrderBy( e => e.NO_FANTAS_EMP );

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

        /// <summary>
        /// Método que carrega o dropdown de Destinatários de acordo como o tipo de contato
        /// </summary>
        private void CarregaDestinatarios()
        {
            if (ddlTpContato.SelectedValue == "R")
                ddlDestinatarios.DataSource = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                               where tb108.NU_TELE_CELU_RESP != null
                                               select new { CONTATO = tb108.NO_RESP, CO_CONTATO = tb108.CO_RESP }).OrderBy( r => r.CONTATO );
            else if (ddlTpContato.SelectedValue == "A")
                ddlDestinatarios.DataSource = (from tb07 in TB07_ALUNO.RetornaPelaEmpresa(Convert.ToInt32(ddlUnidade.SelectedValue))
                                               where tb07.NU_TELE_CELU_ALU != null
                                               select new { CONTATO = tb07.NO_ALU, CO_CONTATO = tb07.CO_ALU }).OrderBy( a => a.CONTATO );
            else if (ddlTpContato.SelectedValue == "F")
                ddlDestinatarios.DataSource = (from tb03 in TB03_COLABOR.RetornaPelaEmpresa(Convert.ToInt32(ddlUnidade.SelectedValue))
                                               where tb03.NU_TELE_CELU_COL != null
                                               select new { CONTATO = tb03.NO_COL, CO_CONTATO = tb03.CO_COL }).OrderBy( c => c.CONTATO );
            else
                ddlDestinatarios.DataSource = (from tb03 in TB03_COLABOR.RetornaPelaEmpresa(Convert.ToInt32(ddlUnidade.SelectedValue))
                                               where tb03.FLA_PROFESSOR == "S" && tb03.NU_TELE_CELU_COL != null
                                               select new { CONTATO = tb03.NO_COL, CO_CONTATO = tb03.CO_COL }).OrderBy( c => c.CONTATO );

            ddlDestinatarios.DataTextField = "CONTATO";
            ddlDestinatarios.DataValueField = "CO_CONTATO";
            ddlDestinatarios.DataBind();

            ddlDestinatarios.Enabled = ddlDestinatarios.Items.Count > 0;

            PreencheTelefone();
        }

        /// <summary>
        /// Método que preenche o campo txtTelefone com o telefone do destinatário selecionado
        /// </summary>
        private void PreencheTelefone()
        {
            if (ddlDestinatarios.Items.Count > 0)
            {
                int idDestinatario = int.Parse(ddlDestinatarios.SelectedValue);

                string telefone = "";

                if (ddlTpContato.SelectedValue == "R")
                    telefone = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                where tb108.CO_RESP == idDestinatario
                                select new { tb108.NU_TELE_CELU_RESP }).FirstOrDefault().NU_TELE_CELU_RESP;
                else if (ddlTpContato.SelectedValue == "A")
                    telefone = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                                where tb07.CO_ALU == idDestinatario
                                select new { tb07.NU_TELE_CELU_ALU }).FirstOrDefault().NU_TELE_CELU_ALU;
                else
                    telefone = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                                where tb03.CO_COL == idDestinatario
                                select new { tb03.NU_TELE_CELU_COL }).FirstOrDefault().NU_TELE_CELU_COL;

                txtTelefone.Text = telefone;
            }
        }
        #endregion

//====> Método executado quando botão de salvar clicado
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtMsgInt.Text == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "*** Campo Mensagem Obrigatório ***");
                return;
            }

            if (txtMsgInt.Text.Count() > 110)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "*** Campo Mensagem não pode ser superior a 110 ***");
                return;
            }

            string msgErro = "";

            if (!Page.IsValid)
                return;
         
            SMSAuxili.SMSRequestReturn sMSRequestReturn = SMSAuxili.EnvioSMS("=D Msg EscolaW", Extensoes.RemoveAcentuacoes(txtMsgInt.Text + "(" + LoginAuxili.DESLOGIN + ")"),
                                        "55" + txtTelefone.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", ""),
                                        DateTime.Now.Ticks.ToString());

            TB249_MENSAG_SMS msgSMS = new TB249_MENSAG_SMS();
            msgSMS.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_UNID_FUNC, LoginAuxili.CO_COL);

            msgSMS.CO_RECEPT = int.Parse(ddlDestinatarios.SelectedValue);

            if (ddlTpContato.SelectedValue != "R")
                msgSMS.CO_EMP_RECEPT = int.Parse(ddlUnidade.SelectedValue);

            msgSMS.DT_ENVIO_MENSAG_SMS = DateTime.Now;
            msgSMS.DES_MENSAG_SMS = txtMsgInt.Text;

            if ((int)sMSRequestReturn == 0)
                msgSMS.FLA_SMS_SUCESS = "S";
            else
                msgSMS.FLA_SMS_SUCESS = "N";

            msgSMS.CO_TP_CONTAT_SMS = ddlTpContato.SelectedValue;

            if ((int)sMSRequestReturn == 13)
                msgErro = "Número do destinatário está incompleto ou inválido.";
            else if ((int)sMSRequestReturn == 80)
                msgErro = "Já foi enviada uma mensagem de sua conta com o mesmo identificador.";
            else if ((int)sMSRequestReturn == 900)
                msgErro = "Erro de autenticação em account e/ou code.";
            else if ((int)sMSRequestReturn == 990)
                msgErro = "Seu limite de segurança foi atingido. Contate nosso suporte para verificação/liberação.";
            else if ((int)sMSRequestReturn == 998)
                msgErro = "Foi invocada uma operação inexistente.";
            else if ((int)sMSRequestReturn == 999)
                msgErro = "Erro desconhecido. Contate nosso suporte.";


            msgSMS.ID_MENSAG_OPERAD = (int)sMSRequestReturn;
            msgSMS.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);

            if ((int)sMSRequestReturn == 0)
                msgSMS.CO_STATUS = "E";
            else
                msgSMS.CO_STATUS = "N";

            TB249_MENSAG_SMS.SaveOrUpdate(msgSMS, true);

            if ((int)sMSRequestReturn != 0)
                AuxiliPagina.RedirecionaParaPaginaErro(msgErro, Request.Url.AbsoluteUri);
            else
                AuxiliPagina.RedirecionaParaPaginaSucesso("Mensagem enviada com sucesso.", Request.Url.AbsoluteUri);         
        }        

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaDestinatarios();
        }

        protected void ddlTpContato_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTpContato.SelectedValue == "R")
                ddlUnidade.Enabled = false;
            else
                ddlUnidade.Enabled = true;

            CarregaDestinatarios();
        }

        protected void ddlFuncionarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            PreencheTelefone();
        }
    }
}
