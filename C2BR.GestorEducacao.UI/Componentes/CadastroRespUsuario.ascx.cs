using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.UI.Componentes
{
    public partial class CadastroRespUsuario : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                carregaBairro();
                carregaCidade();
                AuxiliCarregamentos.CarregaUFs(ddlUFOrgEmis, false, null, false);
                AuxiliCarregamentos.CarregaUFs(ddlUF, false, LoginAuxili.CO_EMP);
                ddlUFOrgEmis.Items.Insert(0, new ListItem("", ""));
            }
        }


        #region Cadastro Usuário de Saúde

        /// <summary>
        /// Pesquisa se já existe um responsável com o CPF informado na tabela de responsáveis, se já existe ele preenche as informações do responsável 
        /// </summary>
        private void PesquisaCarregaResp(int? co_resp)
        {
            string cpfResp = txtCPFResp.Text.Replace(".", "").Replace("-", "");

            var res = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                       where (co_resp.HasValue ? tb108.CO_RESP == co_resp : tb108.NU_CPF_RESP == cpfResp)
                       select new
                       {
                           tb108.NO_RESP,
                           tb108.CO_RG_RESP,
                           tb108.CO_ORG_RG_RESP,
                           tb108.NU_CPF_RESP,
                           tb108.CO_ESTA_RG_RESP,
                           tb108.DT_NASC_RESP,
                           tb108.CO_SEXO_RESP,
                           tb108.CO_CEP_RESP,
                           tb108.CO_ESTA_RESP,
                           tb108.CO_CIDADE,
                           tb108.CO_BAIRRO,
                           tb108.DE_ENDE_RESP,
                           tb108.DES_EMAIL_RESP,
                           tb108.NU_TELE_CELU_RESP,
                           tb108.NU_TELE_RESI_RESP,
                           tb108.CO_ORIGEM_RESP,
                           tb108.CO_RESP,
                           tb108.NU_TELE_WHATS_RESP,
                           tb108.NM_FACEBOOK_RESP,
                           tb108.NU_TELE_COME_RESP,
                       }).FirstOrDefault();

            if (res != null)
            {
                txtNomeResp.Text = res.NO_RESP;
                txtCPFResp.Text = res.NU_CPF_RESP;
                txtNuIDResp.Text = res.CO_RG_RESP;
                txtOrgEmiss.Text = res.CO_ORG_RG_RESP;
                ddlUFOrgEmis.SelectedValue = res.CO_ESTA_RG_RESP;
                txtDtNascResp.Text = res.DT_NASC_RESP.ToString();
                ddlSexResp.SelectedValue = res.CO_SEXO_RESP;
                txtCEP.Text = res.CO_CEP_RESP;
                ddlUF.SelectedValue = res.CO_ESTA_RESP;
                carregaCidade();
                ListItem itcidade = ddlCidade.Items.FindByValue(res.CO_CIDADE.ToString());
                if (itcidade != null)
                    ddlCidade.SelectedValue = res.CO_CIDADE != null ? res.CO_CIDADE.ToString() : "";

                carregaBairro();
                ListItem itBairro = ddlBairro.Items.FindByValue(res.CO_BAIRRO.ToString());
                if (itBairro != null)
                    ddlBairro.SelectedValue = res.CO_BAIRRO != null ? res.CO_BAIRRO.ToString() : "";

                txtLograEndResp.Text = res.DE_ENDE_RESP;
                txtEmailResp.Text = res.DES_EMAIL_RESP;
                txtTelCelResp.Text = res.NU_TELE_CELU_RESP;
                txtTelFixResp.Text = res.NU_TELE_RESI_RESP;
                txtNuWhatsResp.Text = res.NU_TELE_WHATS_RESP;
                txtTelComResp.Text = res.NU_TELE_COME_RESP;
                txtDeFaceResp.Text = res.NM_FACEBOOK_RESP;
                //hidCoResp.Value = res.CO_RESP.ToString();
            }
            //updCadasUsuario.Update();
        }

        /// <summary>
        /// Carrega as Cidades relacionadas ao Bairro selecionado anteriormente.
        /// </summary>
        private void carregaCidade()
        {
            string uf = ddlUF.SelectedValue;
            AuxiliCarregamentos.CarregaCidades(ddlCidade, false, uf, LoginAuxili.CO_EMP, true, true);
            //if (uf != "")
            //{
            //    var res = (from tb904 in TB904_CIDADE.RetornaTodosRegistros()
            //               where tb904.CO_UF == uf
            //               select new { tb904.NO_CIDADE, tb904.CO_CIDADE });

            //    ddlCidade.DataTextField = "NO_CIDADE";
            //    ddlCidade.DataValueField = "CO_CIDADE";
            //    ddlCidade.DataSource = res;
            //    ddlCidade.DataBind();

            //    ddlCidade.Items.Insert(0, new ListItem("Selecione", ""));
            //}
            //else
            //{
            //    ddlCidade.Items.Clear();
            //    ddlCidade.Items.Insert(0, new ListItem("Selecione", ""));
            //}
        }

        /// <summary>
        /// Carrega os Bairros ligados à UF e Cidade selecionados anteriormente.
        /// </summary>
        private void carregaBairro()
        {
            string uf = ddlUF.SelectedValue;
            int cid = ddlCidade.SelectedValue != "" ? int.Parse(ddlCidade.SelectedValue) : 0;

            if ((uf != "") && (cid != 0))
            {
                var res = (from tb905 in TB905_BAIRRO.RetornaTodosRegistros()
                           where tb905.CO_CIDADE == cid
                           && (tb905.CO_UF == uf)
                           select new { tb905.NO_BAIRRO, tb905.CO_BAIRRO });

                ddlBairro.DataTextField = "NO_BAIRRO";
                ddlBairro.DataValueField = "CO_BAIRRO";
                ddlBairro.DataSource = res;
                ddlBairro.DataBind();

                ddlBairro.Items.Insert(0, new ListItem("Selecione", ""));
            }
            else
            {
                ddlBairro.Items.Clear();
                ddlBairro.Items.Insert(0, new ListItem("Selecione", ""));
            }
        }

        /// <summary>
        /// Método que duplica as informações do responsável nos campos do paciente, quando o usuário clica em Paciente é o Responsável.
        /// </summary>
        private void carregaPaciehoResponsavel()
        {
            if (chkPaciEhResp.Checked)
            {
                txtCPFMOD.Text = txtCPFResp.Text;
                txtnompac.Text = txtNomeResp.Text;
                txtDtNascPaci.Text = txtDtNascResp.Text;
                ddlSexoPaci.SelectedValue = ddlSexResp.SelectedValue;
                txtTelCelPaci.Text = txtTelCelResp.Text;
                txtTelResPaci.Text = txtTelFixResp.Text;
                ddlGrParen.SelectedValue = "OU";
                txtEmailPaci.Text = txtEmailResp.Text;
                txtWhatsPaci.Text = txtNuWhatsResp.Text;

                txtEmailPaci.Enabled = false;
                txtCPFMOD.Enabled = false;
                txtnompac.Enabled = false;
                txtDtNascPaci.Enabled = false;
                ddlSexoPaci.Enabled = false;
                txtTelCelPaci.Enabled = false;
                txtTelCelPaci.Enabled = false;
                txtTelResPaci.Enabled = false;
                ddlGrParen.Enabled = false;
                txtWhatsPaci.Enabled = false;

                #region Verifica se já existe

                string cpf = txtCPFMOD.Text.Replace(".", "").Replace("-", "").Trim();

                var tb07 = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.NU_CPF_ALU == cpf).FirstOrDefault();

                //if (tb07 != null)
                //    hidCoPac.Value = tb07.CO_ALU.ToString();

                #endregion

            }
            else
            {
                txtCPFMOD.Text = "";
                txtnompac.Text = "";
                txtDtNascPaci.Text = "";
                ddlSexoPaci.SelectedValue = "";
                txtTelCelPaci.Text = "";
                txtTelResPaci.Text = "";
                ddlGrParen.SelectedValue = "";
                txtEmailPaci.Text = "";
                txtWhatsPaci.Text = "";

                txtCPFMOD.Enabled = true;
                txtnompac.Enabled = true;
                txtDtNascPaci.Enabled = true;
                ddlSexoPaci.Enabled = true;
                txtTelCelPaci.Enabled = true;
                txtTelCelPaci.Enabled = true;
                txtTelResPaci.Enabled = true;
                ddlGrParen.Enabled = true;
                txtEmailPaci.Enabled = true;
                txtWhatsPaci.Enabled = true;
                //hidCoPac.Value = "";
            }
            //updCadasUsuario.Update();
        }

        protected void imgPesqCEP_OnClick(object sender, EventArgs e)
        {
            if (txtCEP.Text.Replace("-", "") != "")
            {
                int numCep = int.Parse(txtCEP.Text.Replace("-", ""));

                TB235_CEP tb235 = TB235_CEP.RetornaTodosRegistros().Where(c => c.CO_CEP == numCep).FirstOrDefault();

                if (tb235 != null)
                {
                    txtLograEndResp.Text = tb235.NO_ENDER_CEP;
                    tb235.TB905_BAIRROReference.Load();
                    ddlUF.SelectedValue = tb235.TB905_BAIRRO.CO_UF;
                    carregaCidade();
                    ddlCidade.SelectedValue = tb235.TB905_BAIRRO.CO_CIDADE.ToString();
                    carregaBairro();
                    ddlBairro.SelectedValue = tb235.TB905_BAIRRO.CO_BAIRRO.ToString();
                }
                else
                {
                    txtLograEndResp.Text = "";
                    ddlBairro.SelectedValue = "";
                    ddlCidade.SelectedValue = "";
                    ddlUF.SelectedValue = "";
                }
            }
        }

        protected void ddlUF_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaCidade();
            ddlCidade.Focus();
        }

        protected void ddlCidade_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaBairro();
            ddlBairro.Focus();
        }

        protected void chkPaciEhResp_OnCheckedChanged(object sender, EventArgs e)
        {
            carregaPaciehoResponsavel();

            if (chkPaciEhResp.Checked)
                chkPaciMoraCoResp.Checked = true;

        }

        protected void imgCadPac_OnClick(object sender, EventArgs e)
        {
            divResp.Visible = true;
            divSuccessoMessage.Visible = false;
            //updCadasUsuario.Update();
        }

        protected void imgCpfResp_OnClick(object sender, EventArgs e)
        {
            PesquisaCarregaResp(null);
        }

        protected void lnkSalvar_OnClick(object sender, EventArgs e)
        {

            #region Valida informações do Responsável

            if (string.IsNullOrEmpty(txtCPFResp.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "CPF do responsável é obrigatório!");
                txtCPFResp.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtNomeResp.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "O Nome do responsável é obrigatório!");
                txtNomeResp.Focus();
                return;
            }

            if (string.IsNullOrEmpty(ddlSexResp.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "O Sexo do responsável é obrigatório!");
                ddlSexResp.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtDtNascResp.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "A Data de Nascimento do responsável é obrigatória!");
                txtDtNascResp.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtTelCelResp.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "O Telfone fixo do responsável é obrigatório!");
                txtTelCelResp.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtCEP.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "O CEP do responsável é obrigatório!");
                txtCEP.Focus();
                return;
            }

            if (string.IsNullOrEmpty(ddlUF.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "A UF do endereço do responsável é obrigatória!");
                ddlUF.Focus();
                return;
            }

            if (string.IsNullOrEmpty(ddlCidade.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "A UF do endereço do responsável é obrigatória!");
                ddlCidade.Focus();
                return;
            }

            if (string.IsNullOrEmpty(ddlBairro.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "O Bairro do endereço do responsável é obrigatório!");
                ddlBairro.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtLograEndResp.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "O Logradouro do endereço do responsável é obrigatório!");
                txtLograEndResp.Focus();
                return;
            }

            #endregion

            #region Valida campos do Paciente

            if (string.IsNullOrEmpty(txtNuNisPaci.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "O Nis do Paciente é obrigatório!");
                txtNuNisPaci.Focus();
                //updCadasUsuario.Update();
                return;
            }

            if (string.IsNullOrEmpty(txtnompac.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "O Nome do Paciente é obrigatório!");
                txtnompac.Focus();
                return;
            }

            if (string.IsNullOrEmpty(ddlSexoPaci.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "O Sexo do Paciente é obrigatório!");
                ddlSexoPaci.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtDtNascPaci.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "A Data de Nascimento do Paciente é obrigatória!");
                txtDtNascPaci.Focus();
                return;
            }

            #endregion

            //Salva os dados do Responsável na tabela 108
            #region Salva Responsável na tb108

            TB108_RESPONSAVEL tb108;
            //Verifica se já não existe um responsável cadastrdado com esse CPF, caso não exista ele cadastra um novo com os dados informados
            //if (string.IsNullOrEmpty(hidCoResp.Value))
            //{
                tb108 = new TB108_RESPONSAVEL();

                tb108.NO_RESP = txtNomeResp.Text;
                tb108.NU_CPF_RESP = txtCPFResp.Text.Replace("-", "").Replace(".", "").Trim();
                tb108.CO_RG_RESP = txtNuIDResp.Text;
                tb108.CO_ORG_RG_RESP = txtOrgEmiss.Text;
                tb108.CO_ESTA_RG_RESP = ddlUFOrgEmis.SelectedValue;
                tb108.DT_NASC_RESP = DateTime.Parse(txtDtNascResp.Text);
                tb108.CO_SEXO_RESP = ddlSexResp.SelectedValue;
                tb108.CO_CEP_RESP = txtCEP.Text;
                tb108.CO_ESTA_RESP = ddlUF.SelectedValue;
                tb108.CO_CIDADE = int.Parse(ddlCidade.SelectedValue);
                tb108.CO_BAIRRO = int.Parse(ddlBairro.SelectedValue);
                tb108.DE_ENDE_RESP = txtLograEndResp.Text;
                tb108.DES_EMAIL_RESP = txtEmailResp.Text;
                tb108.NU_TELE_CELU_RESP = txtTelCelResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb108.NU_TELE_RESI_RESP = txtTelFixResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb108.NU_TELE_WHATS_RESP = txtNuWhatsResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb108.NU_TELE_COME_RESP = txtTelComResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb108.CO_ORIGEM_RESP = "NN";
                tb108.CO_SITU_RESP = "A";

                //Atribui valores vazios para os campos not null da tabela de Responsável.
                tb108.FL_NEGAT_CHEQUE = "V";
                tb108.FL_NEGAT_SERASA = "V";
                tb108.FL_NEGAT_SPC = "V";
                tb108.CO_INST = 0;
                tb108.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);

                tb108 = TB108_RESPONSAVEL.SaveOrUpdate(tb108);
            //}
            //else
            //{
            //    //Busca em um campo na página, que é preenchido quando se pesquisa um responsável, o CO_RESP, usado pra instanciar um objeto da entidade do responsável em questão.
            //    if (string.IsNullOrEmpty(hidCoResp.Value))
            //    {
            //        AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar um Responsável para dar continuidade no encaminhamento.");
            //        return;
            //    }

            //    int coRe = int.Parse(hidCoResp.Value);
            //    tb108 = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(coRe);
            //    hidCoResp.Value = coRe.ToString();
            //}

            #endregion

            //Salva os dados do Usuário em um registro na tb07
            #region Salva o Usuário na TB07

            ////Verifica antes se já existe o paciente algum paciente com o mesmo CPF e NIS informados nos campos, caso não exista, cria um novo
            //string cpfPac = txtCpfPaci.Text.Replace("-", "").Replace(".", "").Trim();
            //var realu = (from tb07li in TB07_ALUNO.RetornaTodosRegistros()
            //             where tb07li.NU_CPF_ALU == cpfPac
            //             select new { tb07li.CO_ALU }).FirstOrDefault();

            //int? paExis = (realu != null ? realu.CO_ALU : (int?)null);

            //Decimal nis = 0;
            //if (!string.IsNullOrEmpty(txtNuNisPaci.Text))
            //{
            //    nis = decimal.Parse(txtNuNisPaci.Text.Trim());
            //}

            //var realu2 = (from tb07ob in TB07_ALUNO.RetornaTodosRegistros()
            //              where tb07ob.NU_NIS == nis
            //              select new { tb07ob.CO_ALU }).FirstOrDefault();

            //int? paExisNis = (realu2 != null ? realu2.CO_ALU : (int?)null);

            TB07_ALUNO tb07;
            //if ((!paExis.HasValue) && (!paExisNis.HasValue))
            //{
            //if (string.IsNullOrEmpty(hidCoPac.Value))
            //{
            tb07 = new TB07_ALUNO();

            #region Bloco foto
            //int codImagem = upima.GravaImagem();
            //tb07.Image = C2BR.GestorEducacao.BusinessEntities.MSSQL.Image.RetornaPelaChavePrimaria(codImagem);
            #endregion

            string cpfPac = txtCPFMOD.Text.Replace(".", "").Replace("-", "").Trim();
            decimal nis = decimal.Parse(txtNuNisPaci.Text);

            //Verifica se tem, se houver instancia um objeto para alteração dos dados informados
            if (TB07_ALUNO.RetornaTodosRegistros().Where(w => w.NU_CPF_ALU == cpfPac).Any() && (!string.IsNullOrEmpty(cpfPac)))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Já existe um paciente cadastrado com o CPF informado!");
                return;
            }
            //tb07 = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.NU_CPF_ALU == cpfPac).FirstOrDefault();
            else if (TB07_ALUNO.RetornaTodosRegistros().Where(w => w.NU_NIS == nis).Any() && (!string.IsNullOrEmpty(txtNuNisPaci.Text)))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Já existe um paciente cadastrado com o NIS/Nº PRONTUÁRIO informado!");
            }

            //tb07 = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.NU_NIS == nis).FirstOrDefault();

            tb07.NO_ALU = txtnompac.Text.ToUpper();
            tb07.NU_CPF_ALU = cpfPac;
            tb07.NU_NIS = nis;
            tb07.DT_NASC_ALU = DateTime.Parse(txtDtNascPaci.Text);
            tb07.CO_SEXO_ALU = ddlSexoPaci.SelectedValue;
            tb07.NU_TELE_CELU_ALU = txtTelCelPaci.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            tb07.NU_TELE_RESI_ALU = txtTelResPaci.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            tb07.NU_TELE_WHATS_ALU = txtNuWhatsResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            tb07.CO_GRAU_PAREN_RESP = ddlGrParen.SelectedValue;
            tb07.NO_EMAIL_PAI = txtEmailPaci.Text;
            tb07.CO_EMP = LoginAuxili.CO_EMP;
            tb07.TB25_EMPRESA1 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
            tb07.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
            tb07.TB108_RESPONSAVEL = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(tb108.CO_RESP);

            if (chkPaciMoraCoResp.Checked)
            {
                tb07.CO_CEP_ALU = txtCEP.Text;
                tb07.CO_ESTA_ALU = ddlUF.SelectedValue;
                tb07.TB905_BAIRRO = TB905_BAIRRO.RetornaPelaChavePrimaria(int.Parse(ddlBairro.SelectedValue));
                tb07.DE_ENDE_ALU = txtLograEndResp.Text;
            }

            //Salva os valores para os campos not null da tabela de Usuário
            tb07.CO_SITU_ALU = "A";
            tb07.TP_DEF = "N";

            #region trata para criação do nire

            var res = (from tb07pesq in TB07_ALUNO.RetornaTodosRegistros()
                       select new { tb07pesq.NU_NIRE }).OrderByDescending(w => w.NU_NIRE).FirstOrDefault();

            int nir = 0;
            if (res == null)
            {
                nir = 1;
            }
            else
            {
                nir = res.NU_NIRE;
            }

            int nirTot = nir + 1;

            #endregion
            tb07.NU_NIRE = nirTot;

            tb07 = TB07_ALUNO.SaveOrUpdate(tb07);
            //}
            //else
            //{
            //    //if (string.IsNullOrEmpty(hidCoPac.Value))
            //    //{
            //    //    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar um Paciente para dar continuidade no encaminhamento.");
            //    //    return;
            //    //}

            //    //Busca em um campo na página, que é preenchido quando se pesquisa um Paciente, o CO_ALU, usado pra instanciar um objeto da entidade do Paciente em questão.
            //    int coPac = int.Parse(hidCoPac.Value);
            //    tb07 = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.CO_ALU == coPac).FirstOrDefault();
            //}

            divResp.Visible = false;
            divSuccessoMessage.Visible = true;
            lblMsg.Text = "Usuário salvo com êxito!";
            lblMsgAviso.Text = "Clique no botão Fechar para voltar a tela inicial.";
            lblMsg.Visible = true;
            lblMsgAviso.Visible = true;

            #endregion
        }

        #endregion
    }
}