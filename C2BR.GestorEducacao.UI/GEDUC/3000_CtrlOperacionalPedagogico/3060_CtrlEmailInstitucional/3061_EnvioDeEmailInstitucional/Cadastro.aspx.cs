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

namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3060_CtrlEmailInstitucional._3061_EnvioDeEmailInstitucional
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
            divGridP.Visible = false;
            invisibleTodos();
            carregaDestinatarioTipo();
            CarregaEmissor();
            txtDataDeEnvio.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }


        #endregion

        #region Carregamentos

        private void carregaDestinatarioTipo() {
            rblTipoEmail.Items.Clear();
            rblTipoEmail.Items.Add(new ListItem("Selecione", ""));
            rblTipoEmail.Items.Add(new ListItem("Colaborador", "C"));
            rblTipoEmail.Items.Add(new ListItem("Parceiro", "P"));
        }

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

            switch (rblTipoEmail.SelectedValue)
            {
                case "C":
                    var resultado = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                                     join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tb03.CO_DEPTO equals tb14.CO_DEPTO
                                     select new Colaborador
                                     {
                                         CPF_C = tb03.NU_CPF_COL,
                                         NOME_C = tb03.NO_COL,
                                         FUNCAO_C = tb03.TB128_FUNCA_FUNCI.NO_FUNCA_FUNCI,
                                         DEPART_C = tb14.NO_DEPTO,
                                         TELEFONE_C = tb03.NU_TELE_CELU_COL,
                                         EMAIL_C = tb03.CO_EMAI_COL,
                                         CO_COL = tb03.CO_COL,
                                         CO_EMP = tb03.CO_EMP
                                     });

                    divGridP.Visible = false;
                    divGridC.Visible = true;
                    grdColaborador.DataSource = resultado.Count() > 0 ? resultado.OrderBy(p => p.NOME_C) : null;
                    grdColaborador.DataBind();

                    liTodos.Visible = true;
                    visibleTodos();

                    break;
                case "P":
                    var res = (from tb421 in TB421_PARCEIROS.RetornaTodosRegistros()
                               select new Parceiro
                               {
                                   TIPO_P = tb421.TP_PARCE,
                                   COD_P = tb421.CO_CPFCGC_PARCE,
                                   NOME_P = tb421.DE_RAZSOC_PARCE,
                                   RESP_P = tb421.NM_RESPO_PARCE != null ? tb421.NM_RESPO_PARCE : "",
                                   TELEFONE_P = tb421.CO_TEL1_PARCE != null ? tb421.CO_TEL1_PARCE : "",
                                   EMAIL_P = tb421.DE_EMAIL_PARCE != null ? tb421.DE_EMAIL_PARCE : "",
                                   CO_PARCE = tb421.CO_PARCE
                               });

                    divGridC.Visible = false;
                    divGridP.Visible = true;
                    grdParceiro.DataSource = res.Count() > 0 ? res.OrderBy(p => p.NOME_P) : null;
                    grdParceiro.DataBind();

                    liTodos.Visible = true;
                    visibleTodos();

                    break;
            }

        }

        protected void rblTipoEmail_SelectedChanged(object sender, EventArgs e)
        {
            CarregaGrid();
            if (rblTipoEmail.SelectedValue == "C")
            {
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
            if (rblTipoEmail.SelectedValue == "P")
            {
                foreach (GridViewRow row in grdParceiro.Rows)
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
            if (rblTipoEmail.SelectedValue == "C")
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

            if (rblTipoEmail.SelectedValue == "P")
            {
                foreach (GridViewRow row in grdParceiro.Rows)
                {
                    System.Web.UI.WebControls.CheckBox cb = (System.Web.UI.WebControls.CheckBox)row.FindControl("chcSelecionarEmail");

                    if (cb != null)
                    {
                        cb.Checked = checkState;
                    }
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

                    if (rblTipoEmail.SelectedValue == "C")
                    {
                        foreach (GridViewRow linha in grdColaborador.Rows)
                        {
                            System.Web.UI.WebControls.CheckBox cb = (System.Web.UI.WebControls.CheckBox)linha.FindControl("chcSelecionarEmail");
                            if (cb != null && cb.Checked)
                            {
                                string listaEmail = ((System.Web.UI.WebControls.TextBox)linha.Cells[6].FindControl("txtEmail")).Text;
                                emailResponsavel.Add(listaEmail);
                            }
                        }
                    }

                    if (rblTipoEmail.SelectedValue == "P")
                    {
                        foreach (GridViewRow linha in grdParceiro.Rows)
                        {
                            System.Web.UI.WebControls.CheckBox cb = (System.Web.UI.WebControls.CheckBox)linha.FindControl("chcSelecionarEmail");
                            if (cb != null && cb.Checked)
                            {
                                string listaEmail = ((System.Web.UI.WebControls.TextBox)linha.Cells[6].FindControl("txtEmail")).Text;
                                emailResponsavel.Add(listaEmail);
                            }
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
                           throw new ArgumentException("Não foi encontrado nenhum endereço de email");
                        }
                        if (mail.Body == "1")
                        {
                            throw new ArgumentException("Erro ao enviar email, o campo Mensagem é obrigatório");
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
                    tb418.TP_USUAR_EMAIL = rblTipoEmail.SelectedValue;
                    tb418.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, LoginAuxili.CO_COL);

                    TB418_CONTR_EMAIL.SaveOrUpdate(tb418, true);

                    if (tb418.ID_EMAIL > 0)
                    {
                        if (rblTipoEmail.SelectedValue == "C")
                        {
                            TB423_EMAIL_USUAR_GEMPR tb423 = new TB423_EMAIL_USUAR_GEMPR();
                            foreach (GridViewRow linha in grdColaborador.Rows)
                            {
                                CheckBox cb = (CheckBox)linha.FindControl("chcSelecionarEmail");
                                if (cb != null && cb.Checked)
                                {
                                    string coCol = ((HiddenField)linha.Cells[2].FindControl("hdCoCol")).Value;
                                    string coEmp = ((HiddenField)linha.Cells[2].FindControl("hdCoEmp")).Value;

                                    tb423.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(int.Parse(coEmp), int.Parse(coCol));
                                    tb423.TB418_CONTR_EMAIL = TB418_CONTR_EMAIL.RetornaPelaChavePrimaria(tb418.ID_EMAIL);

                                    TB423_EMAIL_USUAR_GEMPR.SaveOrUpdate(tb423, true);
                                }
                            }
                        }

                        if (rblTipoEmail.SelectedValue == "P")
                        {
                            TB425_EMAIL_USUAR_GPARC tb425 = new TB425_EMAIL_USUAR_GPARC();
                            foreach (GridViewRow linha in grdParceiro.Rows)
                            {
                                CheckBox cb = (CheckBox)linha.FindControl("chcSelecionarEmail");
                                if (cb != null && cb.Checked)
                                {
                                    string coParse = ((HiddenField)linha.Cells[3].FindControl("hdCoParce")).Value;

                                    tb425.TB421_PARCEIROS = TB421_PARCEIROS.RetornaPelaChavePrimaria(int.Parse(coParse));
                                    tb425.TB421_PARCEIROS.CO_ORG_PARCE = TB421_PARCEIROS.RetornaPelaChavePrimaria(int.Parse(coParse)).CO_ORG_PARCE;
                                    tb425.TB418_CONTR_EMAIL = TB418_CONTR_EMAIL.RetornaPelaChavePrimaria(tb418.ID_EMAIL);

                                    TB425_EMAIL_USUAR_GPARC.SaveOrUpdate(tb425, true);
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
        public class Colaborador
        {
            public string CPF_C { get; set; }
            public string NOME_C { get; set; }
            public string FUNCAO_C { get; set; }
            public string DEPART_C { get; set; }
            public string TELEFONE_C { get; set; }
            public string EMAIL_C { get; set; }
            public int CO_COL { get; set; }
            public int CO_EMP { get; set; }
        }

        public class Parceiro
        {
            public string TIPO_P { get; set; }
            public string COD_P { get; set; }
            public string NOME_P { get; set; }
            public string RESP_P { get; set; }
            public string TELEFONE_P { get; set; }
            public string EMAIL_P { get; set; }
            public int CO_PARCE { get; set; }
            public string DESC_TIPO { get { return TIPO_P.Equals("F") ? "Passoa Física" : TIPO_P.Equals("J") ? "Pessoa Jurídica" : "Outros"; } }
        }

        #endregion

    }
}