using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3070_CtrlEmailSaude._3071_EnvioDeEmailSaude
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
            if (IsPostBack) return;

            Page.Form.Attributes.Add("enctype", "multipart/form-data");

            liTodos.Visible = false;
            divGridC.Visible = false;
            invisibleTodos();
            //txtEmissor.Text = LoginAuxili.NOME_USU_LOGADO;
            CarregaEmissor();
            txtDataDeEnvio.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }


        #endregion

        #region Carregamentos

        private void CarregaEmissor()
        {

            ddlEmissor.DataSource = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                                     join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tb03.CO_DEPTO equals tb14.CO_DEPTO
                                     select new { tb03.NO_COL, tb03.CO_COL });
            ddlEmissor.DataTextField = "NO_COL";
            ddlEmissor.DataValueField = "CO_COL";
            ddlEmissor.DataBind();

            ddlEmissor.Items.Insert(0, new ListItem(LoginAuxili.NOME_USU_LOGADO, LoginAuxili.CO_COL.ToString()));

        }

        private void CarregaGrid()
        {
            if (rblTipoEmail.SelectedValue == "P")
            {
                var resultado = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                                 select new Usuario
                                 {
                                     CPF = tb07.NU_CPF_ALU,
                                     NOME = tb07.NO_ALU,
                                     APELIDO = tb07.NO_APE_ALU,
                                     SITUACAO = (tb07.CO_SITU_ALU == "A" ? "Em Atendimento" : tb07.CO_SITU_ALU == "V" ? "Em Análise" :
                                                 tb07.CO_SITU_ALU == "E" ? "Alta(Normal)" : tb07.CO_SITU_ALU == "D" ? "Alta(Desistência)" :
                                                 tb07.CO_SITU_ALU == "I" ? "Inativo" : "-"),
                                     TELEFONE = tb07.NU_TELE_CELU_ALU ?? tb07.NU_TELE_RESI_ALU,
                                     EMAIL = tb07.NO_WEB_ALU,
                                     CO_EMP = tb07.TB25_EMPRESA.CO_EMP,
                                     CO_COD = tb07.CO_ALU,
                                     CO_COD_ALU = tb07.TB108_RESPONSAVEL.CO_RESP,
                                     tipoUsu = "P"
                                 });

                divGridC.Visible = true;
                grdColaborador.DataSource = resultado.Count() > 0 ? resultado.OrderBy(p => p.NOME) : null;
                grdColaborador.DataBind();

                liTodos.Visible = true;
                visibleTodos();
            }

            if (rblTipoEmail.SelectedValue == "R")
            {
                var resultado = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                 join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb108.CO_RESP equals tb07.TB108_RESPONSAVEL.CO_RESP
                                 select new Usuario
                                 {
                                     CPF = tb108.NU_CPF_RESP,
                                     NOME = tb108.NO_RESP,
                                     APELIDO = tb108.NO_APELIDO_RESP,
                                     SITUACAO = (tb108.CO_SITU_RESP == "A" ? "Ativo" : "-"),
                                     TELEFONE = tb108.NU_TELE_CELU_RESP ?? tb108.NU_TELE_RESI_RESP,
                                     EMAIL = tb108.DES_EMAIL_RESP,
                                     CO_EMP = LoginAuxili.CO_EMP,
                                     CO_COD = tb108.CO_RESP,
                                     CO_COD_ALU = tb07.CO_ALU,
                                     tipoUsu = "R"
                                 });

                divGridC.Visible = true;
                grdColaborador.DataSource = resultado.Count() > 0 ? resultado.OrderBy(p => p.NOME) : null;
                grdColaborador.DataBind();

                liTodos.Visible = true;
                visibleTodos();
            }
        }

        protected void rblTipoEmail_SelectedChanged(object sender, EventArgs e)
        {
            CarregaGrid();
            foreach (GridViewRow row in grdColaborador.Rows)
            {
                System.Web.UI.WebControls.TextBox cb = (System.Web.UI.WebControls.TextBox)row.FindControl("txtEmail");
                if (cb.Text != "")
                {
                    cb.Enabled = false;
                }
                else
                {
                    cb.Enabled = true;
                }
            }

        }

        protected void chcSelecionarTodos_OnCheckedChanged(object sender, EventArgs e)
        {

            if (chcSelecionarTodos.Checked)
            {
                ToggleCheckState(true);
            }
            else
            {
                ToggleCheckState(false);
            }


        }

        private void ToggleCheckState(bool checkState)
        {
            foreach (GridViewRow row in grdColaborador.Rows)
            {
                System.Web.UI.WebControls.CheckBox cb = (System.Web.UI.WebControls.CheckBox)FindControl("chcSelecionarEmail");

                if (cb != null)
                {
                    cb.Checked = checkState;
                }
            }

        }

        /// <summary>
        /// 

        protected void invisibleTodos()
        {
            //liUnidade.Visible = false;
            liAssunto.Visible = false;
            liEmissor.Visible = false;
            liMensagem.Visible = false;
            liDtEnvio.Visible = false;
            liBtnEnviar.Visible = false;
            liAnexo.Visible = false;
        }

        protected void visibleTodos()
        {
            //liUnidade.Visible = true;
            liAssunto.Visible = true;
            liEmissor.Visible = true;
            liMensagem.Visible = true;
            liDtEnvio.Visible = true;
            liBtnEnviar.Visible = true;
            liAnexo.Visible = true;
        }

        #endregion

        #region Enviar

        protected void btnEnviarEmail_Click(object sender, EventArgs e)
        {
            try
            {
                var resultado = (from x in TB25_EMPRESA.RetornaTodosRegistros()
                                 where x.CO_EMP == LoginAuxili.CO_EMP
                                 select new { x.NO_EMAIL, x.SE_EMAIL }).FirstOrDefault();

                string email = resultado.NO_EMAIL;
                string senha = resultado.SE_EMAIL;

                if (String.IsNullOrEmpty(email) || String.IsNullOrEmpty(senha))
                {
                    throw new ArgumentException("Não foi possível localizar o Email e/ou a Senha do remetente, por favor cadastre uma conta de email válida do GMAIL com a senha corretamente.");
                }


                using (System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential(email, senha);

                    List<string> emailResponsavel = new List<string>();

                    foreach (GridViewRow linha in grdColaborador.Rows)
                    {
                        System.Web.UI.WebControls.CheckBox cb = (System.Web.UI.WebControls.CheckBox)linha.FindControl("chcSelecionarEmail");
                        if (cb != null && cb.Checked)
                        {
                            string listaEmail = ((System.Web.UI.WebControls.TextBox)linha.Cells[6].FindControl("txtEmail")).Text;
                            emailResponsavel.Add(listaEmail);
                        }
                    }

                    bool anexo = false;
                    int i = 0;
                    using (System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage())
                    {
                        mail.From = new System.Net.Mail.MailAddress(email);

                        foreach (var item in emailResponsavel)
                        {
                            if (!string.IsNullOrWhiteSpace(item))
                            {
                                mail.To.Add(new System.Net.Mail.MailAddress(item));
                                i++;
                            }
                        }

                        mail.Subject = txtAssunto.Text;
                        mail.Body = txtMensagem.Text != "" ? txtMensagem.Text : "1";

                        if (FileUploadControl.HasFile)
                        {
                            MemoryStream MS = new MemoryStream(FileUploadControl.FileBytes);
                            mail.Attachments.Add(new System.Net.Mail.Attachment(MS, FileUploadControl.FileName));

                            anexo = true;
                        }

                        if (i == 0)
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Não foi encontrado nenhum endereço de email");
                            return;
                        }
                        if (mail.Body == "1")
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao enviar email, o campo Mensagem é obrigatório");
                            return;
                        }
                        else
                        {
                            smtp.Send(mail);
                            SalvarEmail(anexo);
                            AuxiliPagina.RedirecionaParaPaginaSucesso("Email(s) enviado(s) com sucesso.", Request.Url.AbsoluteUri);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, ex.Message);
            }            
        }

        private void SalvarEmail(bool exiteAnexo)
        {
            try
            {
                TB418_CONTR_EMAIL tb418 = new TB418_CONTR_EMAIL();

                string ipDeEnvio = "";

                if (Page.IsValid)
                {
                    var host = Dns.GetHostEntry(Dns.GetHostName());

                    foreach (var ip in host.AddressList)
                    {
                        if (ip.AddressFamily == AddressFamily.InterNetwork)
                        {
                            ipDeEnvio = ip.ToString();
                        }
                    }

                    tb418.IP_ENVIO = ipDeEnvio;
                    /*Privisoriamente todos com estato de enviado
                        E = enviado
                        A = aberto
                        C = cancelado
                     */
                    tb418.CO_SITUA_EMAIL = "E";
                    tb418.DE_ASSUN = txtAssunto.Text;
                    tb418.DE_MENSA = txtMensagem.Text;
                    /*Provisoriamente data de cadastro igual a data de envio*/
                    tb418.DT_CADAS_EMAIL = Convert.ToDateTime(txtDataDeEnvio.Text);
                    tb418.DT_ENVIO_EMAIL = DateTime.Now;
                    tb418.FL_ANEXO_EMAIL = exiteAnexo ? "S" : "N";
                    /* Tipos de Usuário de Email 
                     *  E = Educação
                     *  S = Saúde
                     *  C = Colaborador/Empresa
                     *  P = Parceiro
                     */
                    tb418.TP_USUAR_EMAIL = "S";
                    tb418.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, LoginAuxili.CO_COL);

                    TB418_CONTR_EMAIL.SaveOrUpdate(tb418, true);

                    if (tb418.ID_EMAIL > 0)
                    {
                        if (rblTipoEmail.SelectedValue == "P")
                        {
                            TB424_EMAIL_USUAR_GSAUD tb424 = new TB424_EMAIL_USUAR_GSAUD();
                            foreach (GridViewRow linha in grdColaborador.Rows)
                            {
                                CheckBox cb = (CheckBox)linha.FindControl("chcSelecionarEmail");
                                if (cb != null && cb.Checked)
                                {
                                    string coCol = ((HiddenField)linha.Cells[2].FindControl("hdCoCol")).Value;
                                    string coColAlu = ((HiddenField)linha.Cells[2].FindControl("hdCoCol2")).Value;
                                    string coEmp = ((HiddenField)linha.Cells[2].FindControl("hdCoEmp")).Value;

                                    tb424.TB07_ALUNO = TB07_ALUNO.RetornaPeloCoAlu(int.Parse(coCol));
                                    tb424.TB108_RESPONSAVEL = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(int.Parse(coColAlu));
                                    tb424.TB418_CONTR_EMAIL = TB418_CONTR_EMAIL.RetornaPelaChavePrimaria(tb418.ID_EMAIL);

                                    TB424_EMAIL_USUAR_GSAUD.SaveOrUpdate(tb424, true);
                                }
                            }
                        }

                        if (rblTipoEmail.SelectedValue == "R")
                        {
                            Usuario usu = new Usuario();
                            TB424_EMAIL_USUAR_GSAUD tb424 = new TB424_EMAIL_USUAR_GSAUD();
                            foreach (GridViewRow linha in grdColaborador.Rows)
                            {
                                CheckBox cb = (CheckBox)linha.FindControl("chcSelecionarEmail");
                                if (cb != null && cb.Checked)
                                {
                                    string coCol = ((HiddenField)linha.Cells[2].FindControl("hdCoCol")).Value;
                                    string coColAlu = ((HiddenField)linha.Cells[2].FindControl("hdCoCol2")).Value;
                                    string coEmp = ((HiddenField)linha.Cells[2].FindControl("hdCoEmp")).Value;

                                    tb424.TB07_ALUNO = TB07_ALUNO.RetornaPeloCoAlu(int.Parse(coColAlu));
                                    tb424.TB108_RESPONSAVEL = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(int.Parse(coCol));
                                    tb424.TB418_CONTR_EMAIL = TB418_CONTR_EMAIL.RetornaPelaChavePrimaria(tb418.ID_EMAIL);

                                    TB424_EMAIL_USUAR_GSAUD.SaveOrUpdate(tb424, true);
                                }
                            }
                        }
                    }
                    if (exiteAnexo)
                    {
                        TB420_ANEXO_EMAIL tb420 = new TB420_ANEXO_EMAIL();

                        tb420.TB418_CONTR_EMAIL = TB418_CONTR_EMAIL.RetornaPelaChavePrimaria(tb418.ID_EMAIL);
                        byte[] bytes;
                        bytes = FileUploadControl.FileBytes;
                        FileUploadControl.PostedFile.InputStream.Read(bytes, 0, bytes.Length);
                        tb420.DOCUMENTO = bytes;
                        TB420_ANEXO_EMAIL.SaveOrUpdate(tb420, true);
                    }
                }
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Não foi possível salvar os dados do envio do email para geração de relatórios."); 
            }
           
        }


        #endregion



        #region Lista ddos Alunos
        public class Usuario
        {
            public string tipoUsu { get; set; }
            public string CPF { get; set; }
            public string NOME { get; set; }
            public string APELIDO { get; set; }
            public string SITUACAO { get; set; }
            public string SITUACAO_V
            {                
                get
                {
                    return SITUACAO;
                }
            }
            public string TELEFONE { get; set; }
            public string EMAIL { get; set; }
            public int ?CO_COD_ALU { get; set; }
            public int CO_COD { get; set; }
            public int CO_EMP { get; set; }
            //public string DescCPF { get { return this.CPF.ToString(); } }
            //public string DescTel { get { return this.TELEFONE.ToString(); } }
        }

        #endregion

    }
}