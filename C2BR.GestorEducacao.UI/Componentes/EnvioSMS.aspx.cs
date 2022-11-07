//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
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
using Resources;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Linq.Expressions;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using System.Web.Security;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.Componentes
{
    public partial class EnvioSMS : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (LoginAuxili.CO_EMP == 0)
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "LogOut", "javascript:parent.LogOut();", true);
                    

//------------> Recebe a flag de envio de SMS do usuário logado
                string strFlaSMS = "S";

                var tb83 = TB83_PARAMETRO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

                if (tb83.FL_ENVIO_SMS != null)
                {
                    strFlaSMS = tb83.FL_ENVIO_SMS;
                }
                else
                {
                    var tb149 = TB149_PARAM_INSTI.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);

                    strFlaSMS = tb149.FL_ENVIO_SMS;
                }
                //ADMUSUARIO.RetornaPelaChavePrimaria(LoginAuxili.IDEADMUSUARIO).FLA_SMS;

                //if (AuxiliValidacao.IsConnected())
                    if (true)
                {
                    if (strFlaSMS == "S")
                    {
                        divLoadingSMS.Style.Add("display", "none");
                        divEnvioSMSContent.Style.Add("display", "block");
                        divErrorMessage.Style.Add("display", "none");
                        divSuccessoMessage.Style.Add("display", "none");
                        txtEmissor.Text = LoginAuxili.NOME_USU_LOGADO;
                        CarregaUnidade();
                        CarregaDestinatarios();
                        CarregaContato();
                    }
                    else
                    {
                        divSuccessoMessage.Style.Add("display", "none");
                        divErrorMessage.Style.Add("display", "block");
                        lblError.Text = "Funcionalidade não habilitada para esse usuário.";
                        lblErrorAviso.Text = "Clique no botão Fechar para voltar a tela inicial.";
                        divEnvioSMSContent.Style.Add("display", "none");
                        divLoadingSMS.Style.Add("display", "none");
                    }
                }
                else
                {
                    divSuccessoMessage.Style.Add("display", "none");
                    divErrorMessage.Style.Add("display", "block");
                    lblError.Text = "Internet não disponível para envio de SMS.";
                    lblErrorAviso.Text = "Clique no botão Fechar para voltar a tela inicial.";
                    divEnvioSMSContent.Style.Add("display", "none");
                    divLoadingSMS.Style.Add("display", "none");
                }               
            }
        }        
        #endregion      

        #region Métodos

        /// <summary>
        /// Carrega o dropdown de Unidades
        /// </summary>
        private void CarregaUnidade()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).OrderBy( e => e.NO_FANTAS_EMP );

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

        /// <summary>
        /// Carrega o dropdown de destinatarios de acordo com o tipo de contato
        /// </summary>
        private void CarregaDestinatarios()
        {
            txtNomeDestinatario.Text = "*";

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

        private void CarregaContato() {

            switch (LoginAuxili.CO_TIPO_UNID) { 
            
                case "PGS":
                    ddlTpContato.Items.Clear();
                    ddlTpContato.Items.Insert(0, new ListItem("Funcionário", "F"));
                    ddlTpContato.Items.Insert(1, new ListItem("Profissional de Saúde", "P"));
                    ddlTpContato.Items.Insert(2, new ListItem("Paciente", "A"));
                    ddlTpContato.Items.Insert(3, new ListItem("Responsável", "R"));
                    ddlTpContato.Items.Insert(4, new ListItem("Outros", "O"));
                    break;

                case "PGE":
                    ddlTpContato.Items.Clear();
                    ddlTpContato.Items.Insert(0, new ListItem("Funcionário", "F"));
                    ddlTpContato.Items.Insert(1, new ListItem("Professor", "P"));
                    ddlTpContato.Items.Insert(2, new ListItem("Aluno", "A"));
                    ddlTpContato.Items.Insert(3, new ListItem("Responsável", "R"));
                    ddlTpContato.Items.Insert(4, new ListItem("Outros", "O"));
                    break;

                default:
                    ddlTpContato.Items.Clear();
                    ddlTpContato.Items.Insert(0, new ListItem("Funcionário", "F"));
                    ddlTpContato.Items.Insert(1, new ListItem("Profissional de Serviço", "P"));
                    ddlTpContato.Items.Insert(2, new ListItem("Aluno/Paciente", "A"));
                    ddlTpContato.Items.Insert(3, new ListItem("Responsável", "R"));
                    ddlTpContato.Items.Insert(4, new ListItem("Outros", "O"));
                    break;
            
            }

        
        }

        /// <summary>
        /// Preenche o campo txtTelefone de acordo com o telefone do destinatário selecionado 
        /// </summary>
        private void PreencheTelefone()
        {
            if (ddlDestinatarios.Items.Count > 0)
            {
                int coEmp = int.Parse(ddlUnidade.SelectedValue);
                int idDestinatario = int.Parse(ddlDestinatarios.SelectedValue);

                string strTelefone = "";
                if (ddlTpContato.SelectedValue == "R")
                {
                    strTelefone = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                where tb108.CO_RESP == idDestinatario
                                select new { tb108.NU_TELE_CELU_RESP }).FirstOrDefault().NU_TELE_CELU_RESP;
                }
                else if (ddlTpContato.SelectedValue == "A")
                {
                    strTelefone = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                                where tb07.CO_EMP == coEmp && tb07.CO_ALU == idDestinatario
                                select new { tb07.NU_TELE_CELU_ALU }).FirstOrDefault().NU_TELE_CELU_ALU;
                }
                else
                {
                    strTelefone = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                                where tb03.CO_EMP == coEmp && tb03.CO_COL == idDestinatario
                                select new { tb03.NU_TELE_CELU_COL }).FirstOrDefault().NU_TELE_CELU_COL;
                }

                txtTelefone.Text = strTelefone;
            }
        }
        #endregion

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaDestinatarios();
        }


        protected void ddlTpContato_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTpContato.SelectedValue == "R" || ddlTpContato.SelectedValue == "O")
                ddlUnidade.Enabled = false;
            else
                ddlUnidade.Enabled = true;

            if (ddlTpContato.SelectedValue != "O")
            {
                liDestinatarios.Style.Add("display", "block");
                litxtDestinatarios.Style.Add("display", "none");
                txtTelefone.Enabled = false;
                CarregaDestinatarios();
            }
            else
            {
                liDestinatarios.Style.Add("display", "none");
                litxtDestinatarios.Style.Add("display", "block");
                txtTelefone.Enabled = true;
                txtTelefone.Text = "";
                txtNomeDestinatario.Text = "";
            }            
        }

        protected void ddlFuncionarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            PreencheTelefone();
        }        

//====> Ao clicar no botão de salvar o SMS é enviado
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            var admUsuario = ADMUSUARIO.RetornaPelaChavePrimaria(LoginAuxili.IDEADMUSUARIO);

            if (admUsuario.QT_SMS_MES_USR != null && admUsuario.QT_SMS_MAXIM_USR != null)
            {
                if (admUsuario.QT_SMS_MAXIM_USR <= admUsuario.QT_SMS_MES_USR)
                {
                    divSuccessoMessage.Style.Add("display", "none");
                    divErrorMessage.Style.Add("display", "block");
                    lblError.Text = "Saldo do envio de SMS para outras pessoas ultrapassado.";
                    lblErrorAviso.Text = "Clique no botão Fechar para voltar a tela inicial.";
                    divEnvioSMSContent.Style.Add("display", "none");
                    divLoadingSMS.Style.Add("display", "none");
                    return;
                }
            }

            if (!Page.IsValid)
                return;
            try
            {
                string desLogin = LoginAuxili.DESLOGIN.Trim().Length > 15 ? LoginAuxili.DESLOGIN.Trim().Substring(0, 15) : LoginAuxili.DESLOGIN.Trim();

                SMSAuxili.SMSRequestReturn sMSRequestReturn = SMSAuxili.EnvioSMS("=D Msg EscolaW", Extensoes.RemoveAcentuacoes(txtMsg.Text + "(" + desLogin + ")"),
                                            "55" + txtTelefone.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", ""),
                                            DateTime.Now.Ticks.ToString());

                TB249_MENSAG_SMS tb249 = new TB249_MENSAG_SMS();
                tb249.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_UNID_FUNC, LoginAuxili.CO_COL);

                tb249.CO_RECEPT = int.Parse(ddlDestinatarios.SelectedValue);

                if (ddlTpContato.SelectedValue != "R" && ddlTpContato.SelectedValue != "O")
                    tb249.CO_EMP_RECEPT = int.Parse(ddlUnidade.SelectedValue);

                tb249.NO_RECEPT_SMS = ddlTpContato.SelectedValue == "O" ? txtNomeDestinatario.Text : null;

                tb249.DT_ENVIO_MENSAG_SMS = DateTime.Now;
                tb249.DES_MENSAG_SMS = txtMsg.Text.Length > 110 ? txtMsg.Text.Substring(0, 110) : txtMsg.Text;

                if ((int)sMSRequestReturn == 0)
                {
                    if (ddlTpContato.SelectedValue == "O")
                    {
                        admUsuario.QT_SMS_TOTAL_USR = admUsuario.QT_SMS_TOTAL_USR != null ? admUsuario.QT_SMS_TOTAL_USR + 1 : 1;
                        admUsuario.QT_SMS_MES_USR = admUsuario.QT_SMS_MES_USR != null ? admUsuario.QT_SMS_MES_USR + 1 : 1;
                        ADMUSUARIO.SaveOrUpdate(admUsuario, false);
                    }
                    
                    tb249.FLA_SMS_SUCESS = "S";
                }
                else
                    tb249.FLA_SMS_SUCESS = "N";

                tb249.CO_TP_CONTAT_SMS = ddlTpContato.SelectedValue;

                if ((int)sMSRequestReturn == 13)
                    lblError.Text = "Número do destinatário está incompleto ou inválido.";
                else if ((int)sMSRequestReturn == 80)
                    lblError.Text = "Já foi enviada uma mensagem de sua conta com o mesmo identificador.";
                else if ((int)sMSRequestReturn == 900)
                    lblError.Text = "Erro de autenticação em account e/ou code.";
                else if ((int)sMSRequestReturn == 990)
                    lblError.Text = "Seu limite de segurança foi atingido. Contate nosso suporte para verificação/liberação.";
                else if ((int)sMSRequestReturn == 998)
                    lblError.Text = "Foi invocada uma operação inexistente.";
                else if ((int)sMSRequestReturn == 999)
                    lblError.Text = "Erro desconhecido. Contate nosso suporte.";


                tb249.ID_MENSAG_OPERAD = (int)sMSRequestReturn;
                tb249.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);

                if ((int)sMSRequestReturn == 0)
                    tb249.CO_STATUS = "E";
                else
                    tb249.CO_STATUS = "N";

                TB249_MENSAG_SMS.SaveOrUpdate(tb249, false);                

                if ((int)sMSRequestReturn != 0)
                {
                    divSuccessoMessage.Style.Add("display", "none");
                    divErrorMessage.Style.Add("display", "block");
                    lblErrorAviso.Text = "Clique no botão Fechar para voltar a tela inicial.";
                    divEnvioSMSContent.Style.Add("display", "none");
                    divLoadingSMS.Style.Add("display", "none");
                }
                else
                {
                    divSuccessoMessage.Style.Add("display", "block");
                    divErrorMessage.Style.Add("display", "none");
                    divEnvioSMSContent.Style.Add("display", "none");
                    divLoadingSMS.Style.Add("display", "none");
                    lblMsg.Text = "Mensagem enviada com sucesso.";
                    lblMsgAviso.Text = "Clique no botão Fechar para voltar a tela inicial.";
                    lblMsg.Visible = true;
                    lblMsgAviso.Visible = true;
                }                
            }
            catch (Exception)
            {
                divSuccessoMessage.Style.Add("display", "none");
                divErrorMessage.Style.Add("display", "block");
                lblError.Text = "Mensagem não foi enviada com sucesso.";
                lblErrorAviso.Text = "Clique no botão Fechar para voltar a tela inicial.";
                divEnvioSMSContent.Style.Add("display", "none");
                divLoadingSMS.Style.Add("display", "none");
            }

            GestorEntities.CurrentContext.SaveChanges();
        }
    }
}