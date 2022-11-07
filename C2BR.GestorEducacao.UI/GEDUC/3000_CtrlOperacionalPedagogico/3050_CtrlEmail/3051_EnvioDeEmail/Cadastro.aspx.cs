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

namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3050_CtrlEmail._3051_EnvioDeEmail
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

            CarregaAnos();
            CarregaModalidades();
            divGrid.Visible = false;
            invisibleTodos();
            //txtEmissor.Text = LoginAuxili.NOME_USU_LOGADO;
            CarregaEmissor();
            txtDataDeEnvio.Text = DateTime.Now.ToString();
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

        private void CarregaAnos()
        {
            ddlAno.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                 select new { tb43.CO_ANO_GRADE }).Distinct().OrderByDescending(g => g.CO_ANO_GRADE);

            ddlAno.DataTextField = "CO_ANO_GRADE";
            ddlAno.DataValueField = "CO_ANO_GRADE";
            ddlAno.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where(m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();

            ddlModalidade.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            string anoGrade = ddlAno.SelectedValue;

            if (modalidade != 0)
            {
                ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                            where tb01.CO_MODU_CUR == modalidade
                                            join tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on tb01.CO_CUR equals tb43.CO_CUR
                                            where tb43.CO_ANO_GRADE == anoGrade && tb43.TB44_MODULO.CO_MODU_CUR == tb01.CO_MODU_CUR
                                            select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct().OrderBy(c => c.NO_CUR);

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();
            }
            else
                ddlSerieCurso.Items.Clear();

            ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            if ((modalidade != 0) && (serie != 0))
            {
                ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                       where tb06.CO_MODU_CUR == modalidade && tb06.CO_CUR == serie
                                       select new { tb06.TB129_CADTURMAS.NO_TURMA, tb06.CO_TUR });

                ddlTurma.DataTextField = "NO_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataBind();
            }
            else
                ddlTurma.Items.Clear();

            ddlTurma.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void CarregaGrid()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string anoMesMat = ddlAno.SelectedValue;

            divGrid.Visible = true;

            var resultado = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                             where tb08.CO_ANO_MES_MAT == ddlAno.SelectedValue
                             && tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                             && tb08.TB44_MODULO.CO_MODU_CUR == modalidade
                             && tb08.CO_CUR == serie
                             && tb08.CO_TUR == turma
                             && tb08.CO_SIT_MAT == "A"
                             select new Aluno
                             {
                                 CO_ALU = tb08.TB07_ALUNO.CO_ALU,
                                 NO_ALU = tb08.TB07_ALUNO.NO_ALU,
                                 NU_NIRE = tb08.TB07_ALUNO.NU_NIRE,
                                 NO_RESP = tb08.TB07_ALUNO.TB108_RESPONSAVEL.NO_RESP,
                                 CO_RESP = tb08.TB108_RESPONSAVEL.CO_RESP,
                                 EM_RESP = tb08.TB07_ALUNO.TB108_RESPONSAVEL.DES_EMAIL_RESP
                             });

            divGrid.Visible = true;
            grdAluno.DataSource = resultado.Count() > 0 ? resultado.OrderBy(p => p.NO_ALU) : null;
            grdAluno.DataBind();

            liTodos.Visible = true;
            visibleTodos();
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            liBtnPesquisar.Visible = false;
            liTodos.Visible = false;
            divGrid.Visible = false;
            invisibleTodos();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
            liBtnPesquisar.Visible = false;
            liTodos.Visible = false;
            divGrid.Visible = false;
            invisibleTodos();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            liBtnPesquisar.Visible = true;
            liTodos.Visible = false;
            divGrid.Visible = false;
            invisibleTodos();
        }

        protected void btnPesqGride_Click(object sender, EventArgs e)
        {
            CarregaGrid();
            foreach (GridViewRow row in grdAluno.Rows)
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
            foreach (GridViewRow row in grdAluno.Rows)
            {
                System.Web.UI.WebControls.CheckBox cb = (System.Web.UI.WebControls.CheckBox)row.FindControl("chcSelecionarEmail");

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

                    foreach (GridViewRow linha in grdAluno.Rows)
                    {
                        System.Web.UI.WebControls.CheckBox cb = (System.Web.UI.WebControls.CheckBox)linha.FindControl("chcSelecionarEmail");
                        if (cb != null && cb.Checked)
                        {
                            string listaEmail = ((System.Web.UI.WebControls.TextBox)linha.Cells[4].FindControl("txtEmail")).Text;
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
                    tb418.TP_USUAR_EMAIL = "E";
                    tb418.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, LoginAuxili.CO_COL);

                    TB418_CONTR_EMAIL.SaveOrUpdate(tb418, true);

                    if (tb418.ID_EMAIL > 0)
                    {
                        TB419_EMAIL_USUAR_GEDUC tb419 = new TB419_EMAIL_USUAR_GEDUC();

                        foreach (GridViewRow linha in grdAluno.Rows)
                        {
                            CheckBox cb = (CheckBox)linha.FindControl("chcSelecionarEmail");
                            if (cb != null && cb.Checked)
                            {
                                string coALu = ((HiddenField)linha.Cells[2].FindControl("hdCoAlu")).Value;
                                string coResp = ((HiddenField)linha.Cells[3].FindControl("hdCoRes")).Value;
                                int responsavel = int.Parse(coResp);
                                int modulo = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;

                                tb419.TB07_ALUNO = TB07_ALUNO.RetornaPelaChavePrimaria(int.Parse(coALu), LoginAuxili.CO_EMP);
                                tb419.TB108_RESPONSAVEL = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(responsavel);
                                /*Provisioriamente o colaborador é o mesmo que o usuário*/
                                //tb419.ID_USUAR_EMAIL = LoginAuxili.CO_COL;
                                tb419.CO_CURSO = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
                                tb419.CO_TURMA = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
                                tb419.TB44_MODULO = TB44_MODULO.RetornaPelaChavePrimaria(modulo);
                                tb419.TB07_ALUNO.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(tb419.TB07_ALUNO.CO_EMP);
                                tb419.TB418_CONTR_EMAIL = TB418_CONTR_EMAIL.RetornaPelaChavePrimaria(tb418.ID_EMAIL);

                                TB419_EMAIL_USUAR_GEDUC.SaveOrUpdate(tb419, true);
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
        public class Aluno
        {
            public string NO_ALU { get; set; }
            public string NO_RESP { get; set; }
            public int CO_RESP { get; set; }
            public string EM_RESP { get; set; }
            public int NU_NIRE { get; set; }
            public int CO_ALU { get; set; }
            public string DescNU_NIRE { get { return this.NU_NIRE.ToString().PadLeft(7, '0'); } }
        }

        public class Unidade
        {
            public int? CO_EMP { get; set; }
            public int? CO_UNID { get; set; }
            public int CO_COL { get; set; }
            public string NO_EMP { get; set; }
        }
        #endregion

    }
}