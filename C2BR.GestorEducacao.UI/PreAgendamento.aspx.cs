using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.UI
{
    public partial class PreAgendamento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                txtData.Text = DateTime.Now.ToShortDateString();
                txtHora.Text = string.Format("{0}:{1}", DateTime.Now.Hour.ToString("D2"), DateTime.Now.Minute);

                txtDtNascResp.Text = "01/01/1900";

                CarregaUFs();
                CarregaCidade();
                CarregaBairro();
                CarregaDeficiencias();
                CarregaContratacao();
                CarregaPlanosSaude();
                CarregaUnidade();
                CarregaTiposAgendamento();
                CarregaTiposConsulta();
            }
        }

        #region Carregamentos

        /// <summary>
        /// Carrega Todas as UF's recebendo o objeto do DropDownList onde será carregada a Lista.
        /// </summary>
        /// <param name="ddlUF"></param>
        /// <param name="Relatorio"></param>
        public void CarregaUFs()
        {
            var res = TB74_UF.RetornaTodosRegistros();

            if (res != null)
            {
                ddlUF.DataTextField = "CODUF";
                ddlUF.DataValueField = "CODUF";
                ddlUF.DataSource = res;
                ddlUF.DataBind();
                ddlUF.SelectByText("DF");
            }
        }

        /// <summary>
        /// Carrega as Cidades relacionadas ao Bairro selecionado anteriormente.
        /// </summary>
        private void CarregaCidade()
        {
            string uf = ddlUF.SelectedValue;

            var res = (from tb904 in TB904_CIDADE.RetornaTodosRegistros()
                       where tb904.CO_UF == uf
                       select new { tb904.CO_CIDADE, tb904.NO_CIDADE }).ToList();

            ddlCidade.Items.Clear();

            if (res.Count > 0)
            {
                ddlCidade.DataTextField = "NO_CIDADE";
                ddlCidade.DataValueField = "CO_CIDADE";
                ddlCidade.DataSource = res;
                ddlCidade.DataBind();
                ddlCidade.SelectByText("Brasília");
            }
        }

        /// <summary>
        /// Carrega os Bairros ligados à UF e Cidade selecionados anteriormente.
        /// </summary>
        private void CarregaBairro()
        {
            string uf = ddlUF.SelectedValue;
            int cid = ddlCidade.SelectedValue != "" ? int.Parse(ddlCidade.SelectedValue) : 0;

            var res = (from tb905 in TB905_BAIRRO.RetornaTodosRegistros()
                       where tb905.CO_CIDADE == cid
                       && tb905.CO_UF == uf
                       select new { tb905.NO_BAIRRO, tb905.CO_BAIRRO }).OrderBy(w => w.NO_BAIRRO).ToList();

            ddlBairro.Items.Clear();

            if (res.Count > 0)
            {
                ddlBairro.DataTextField = "NO_BAIRRO";
                ddlBairro.DataValueField = "CO_BAIRRO";
                ddlBairro.DataSource = res;
                ddlBairro.DataBind();
            }
        }

        /// <summary>
        /// Carregamento das deficiências
        /// </summary>
        public void CarregaDeficiencias()
        {
            var res = (from tbs387 in TBS387_DEFIC.RetornaTodosRegistros()
                       where tbs387.CO_SITUA == "A"
                       select new
                       {
                           tbs387.NM_DEFIC,
                           tbs387.ID_DEFIC,
                       }).OrderBy(w => w.NM_DEFIC).ToList();

            ddlDeficiencia.DataTextField = "NM_DEFIC";
            ddlDeficiencia.DataValueField = "ID_DEFIC";
            ddlDeficiencia.DataSource = res;
            ddlDeficiencia.DataBind();
            ddlDeficiencia.SelectByText("(Nenhuma)");

            //ddlDeficiencia.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Carrega todas as formas de contratação
        /// </summary>
        public void CarregaContratacao()
        {
            var res = (from tb250 in TB250_OPERA.RetornaTodosRegistros()
                       where tb250.FL_SITU_OPER == "A"
                       select new { tb250.ID_OPER, tb250.NOM_OPER })
                       .OrderBy(w => w.NOM_OPER).ToList();

            if (res != null)
            {
                ddlOperadora.DataTextField = "NOM_OPER";
                ddlOperadora.DataValueField = "ID_OPER";
                ddlOperadora.DataSource = res;
                ddlOperadora.DataBind();
                ddlOperadora.SelectByText("PARTICULAR");
            }

            //ddlOperadora.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Carrega todos os planos de saúde de uma determinada operadora
        /// </summary>
        public void CarregaPlanosSaude()
        {
            int idOper = (!string.IsNullOrEmpty(ddlOperadora.SelectedValue) ? int.Parse(ddlOperadora.SelectedValue) : 0);

            var res = (from tb251 in TB251_PLANO_OPERA.RetornaTodosRegistros()
                       where tb251.TB250_OPERA.ID_OPER == idOper
                       && tb251.FL_SITU_PLAN == "A"
                       select new {tb251.NOM_PLAN, tb251.ID_PLAN });

            if (res != null)
            {
                ddlPlano.DataTextField = "NOM_PLAN";
                ddlPlano.DataValueField = "ID_PLAN";
                ddlPlano.DataSource = res;
                ddlPlano.DataBind();
            }
        }

        /// <summary>
        /// Carrega Todas as Unidades
        /// </summary>
        public void CarregaUnidade()
        {
            var res = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                       where tb25.CO_SIT_EMP == "A"
                       select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy(e => e.NO_FANTAS_EMP);
            
            if (res != null)
            {
                ddlUnidade.DataTextField = "NO_FANTAS_EMP";
                ddlUnidade.DataValueField = "CO_EMP";
                ddlUnidade.DataSource = res;
                ddlUnidade.DataBind();
            }

            ddlUnidade.Items.Insert(0, new ListItem("Todas", "0"));
        }

        /// <summary>
        /// Carrega todos os tipos de agendamentos
        /// </summary>
        public void CarregaTiposAgendamento()
        {
            var CO_EMP = !String.IsNullOrEmpty(ddlUnidade.SelectedValue) ? int.Parse(ddlUnidade.SelectedValue) : 0;

            ddlEspecialidade.Items.Clear();

            ddlEspecialidade.Items.Insert(0, new ListItem("Outros", "OU"));
            ddlEspecialidade.Items.Insert(0, new ListItem("Terapia Ocupacional", "TO"));
            ddlEspecialidade.Items.Insert(0, new ListItem("Nutrição", "NT"));
            ddlEspecialidade.Items.Insert(0, new ListItem("Estética", "ES"));
            ddlEspecialidade.Items.Insert(0, new ListItem("Psicologia", "PI"));
            ddlEspecialidade.Items.Insert(0, new ListItem("Fonoaudiologia", "FO"));
            ddlEspecialidade.Items.Insert(0, new ListItem("Fisioterapia", "FI"));
            ddlEspecialidade.Items.Insert(0, new ListItem("Enfermaria", "EN"));
            ddlEspecialidade.Items.Insert(0, new ListItem("Atendimento Odontológico", "AO"));
            ddlEspecialidade.Items.Insert(0, new ListItem("Atendimento Médico", "AM"));

            if (CO_EMP != 0)
            {
                var res = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                           join tb83 in TB83_PARAMETRO.RetornaTodosRegistros() on tb25.TB83_PARAMETRO.CO_EMP equals tb83.CO_EMP
                           where tb25.CO_EMP == CO_EMP
                           select new
                           {
                               tb83.FL_PERM_AGEND_ENFER,
                               tb83.FL_PERM_AGEND_FISIO,
                               tb83.FL_PERM_AGEND_FONOA,
                               tb83.FL_PERM_AGEND_MEDIC,
                               tb83.FL_PERM_AGEND_ODONT,
                               tb83.FL_PERM_AGEND_OUTRO,
                               tb83.FL_PERM_AGEND_PSICO,
                               tb83.FL_PERM_AGEND_TERAP_OCUPA,
                               tb83.FL_PERM_AGEND_ESTET,
                               tb83.FL_PERM_AGEND_NUTRI
                           }).FirstOrDefault();

                if (res.FL_PERM_AGEND_MEDIC != "S")
                    ddlEspecialidade.Items.Remove(ddlEspecialidade.Items.FindByValue("AM"));

                if (res.FL_PERM_AGEND_ENFER != "S")
                    ddlEspecialidade.Items.Remove(ddlEspecialidade.Items.FindByValue("EN"));

                if (res.FL_PERM_AGEND_FISIO != "S")
                    ddlEspecialidade.Items.Remove(ddlEspecialidade.Items.FindByValue("FI"));

                if (res.FL_PERM_AGEND_FONOA != "S")
                    ddlEspecialidade.Items.Remove(ddlEspecialidade.Items.FindByValue("FO"));

                if (res.FL_PERM_AGEND_ODONT != "S")
                    ddlEspecialidade.Items.Remove(ddlEspecialidade.Items.FindByValue("AO"));

                if (res.FL_PERM_AGEND_ESTET != "S")
                    ddlEspecialidade.Items.Remove(ddlEspecialidade.Items.FindByValue("ES"));

                if (res.FL_PERM_AGEND_NUTRI != "S")
                    ddlEspecialidade.Items.Remove(ddlEspecialidade.Items.FindByValue("NT"));

                if (res.FL_PERM_AGEND_OUTRO != "S")
                    ddlEspecialidade.Items.Remove(ddlEspecialidade.Items.FindByValue("OU"));

                if (res.FL_PERM_AGEND_PSICO != "S")
                    ddlEspecialidade.Items.Remove(ddlEspecialidade.Items.FindByValue("PI"));

                if (res.FL_PERM_AGEND_TERAP_OCUPA != "S")
                    ddlEspecialidade.Items.Remove(ddlEspecialidade.Items.FindByValue("TO"));
            }

            ddlEspecialidade.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Carrega os tipos de consulta
        /// </summary>
        public void CarregaTiposConsulta()
        {
            ddlTipoAg.Items.Insert(0, new ListItem("Normal", "N"));
            ddlTipoAg.Items.Insert(1, new ListItem("Retorno", "R"));
            ddlTipoAg.Items.Insert(2, new ListItem("Avaliação", "A"));
            //ddlTipoAg.Items.Insert(3, new ListItem("Selecione", ""));
        }

        #endregion

        #region Eventos de Componentes

        protected void ddlUF_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCidade();
            ddlCidade.Focus();
        }

        protected void ddlCidade_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaBairro();
            ddlBairro.Focus();
        }

        protected void ddlOperadora_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaPlanosSaude();
        }

        protected void ddlUnidade_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTiposAgendamento();
        }

        protected void imgPesqAgendaAtendimento_OnClick(object sender, EventArgs e)
        {
            string cpfResp = txtCPFResp.Text.Replace(".", "").Replace("-", "");

            if (!String.IsNullOrEmpty(cpfResp)) {
                var res = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                           where tb108.NU_CPF_RESP == cpfResp
                           select new
                           {
                               tb108.NO_RESP,
                               tb108.NU_CPF_RESP,
                               tb108.DT_NASC_RESP,
                               tb108.CO_SEXO_RESP,
                               tb108.CO_CEP_RESP,
                               tb108.CO_ESTA_RESP,
                               tb108.CO_CIDADE,
                               tb108.CO_BAIRRO,
                               tb108.DES_EMAIL_RESP,
                               tb108.NU_TELE_CELU_RESP,
                               tb108.NU_TELE_RESI_RESP,
                               tb108.NU_TELE_WHATS_RESP,
                               tb108.DE_GRAU_PAREN,
                               tb108.DE_ENDE_RESP
                           }).FirstOrDefault();

                if (res != null)
                {
                    txtNomeResp.Text = res.NO_RESP;
                    txtCPFResp.Text = res.NU_CPF_RESP;
                    ddlSexoResp.SelectedValue = res.CO_SEXO_RESP;
                    txtDtNascResp.Text = res.DT_NASC_RESP.ToString();
                    txtTelCelResp.Text = res.NU_TELE_CELU_RESP;
                    txtTelResResp.Text = res.NU_TELE_RESI_RESP;
                    txtTelWhsResp.Text = res.NU_TELE_WHATS_RESP;
                    txtEmailResp.Text = res.DES_EMAIL_RESP;
                    ddlGrauParent.SelectedValue = res.DE_GRAU_PAREN;
                    txtCEP.Text = res.CO_CEP_RESP;
                    ddlUF.SelectedValue = res.CO_ESTA_RESP;
                    CarregaCidade();
                    ddlCidade.SelectedValue = res.CO_CIDADE != null ? res.CO_CIDADE.ToString() : "";
                    CarregaBairro();
                    ddlBairro.SelectedValue = res.CO_BAIRRO != null ? res.CO_BAIRRO.ToString() : "";
                    txtLogradouro.Text = res.DE_ENDE_RESP;
                }
            }
        }

        protected void imgPesqCEP_OnClick(object sender, EventArgs e)
        {
            if (txtCEP.Text.Replace("-", "") != "")
            {
                int numCep = int.Parse(txtCEP.Text.Replace("-", ""));

                TB235_CEP tb235 = TB235_CEP.RetornaTodosRegistros().Where(c => c.CO_CEP == numCep).FirstOrDefault();

                if (tb235 != null)
                {
                    txtCEP.Text = numCep.ToString();
                    tb235.TB905_BAIRROReference.Load();
                    ddlUF.SelectedValue = tb235.TB905_BAIRRO.CO_UF;
                    CarregaCidade();
                    ddlCidade.SelectedValue = tb235.TB905_BAIRRO.CO_CIDADE.ToString();
                    CarregaBairro();
                    ddlBairro.SelectedValue = tb235.TB905_BAIRRO.CO_BAIRRO.ToString();
                    txtLogradouro.Text = tb235.NO_ENDER_CEP;
                }
                else
                {
                    AuxiliPagina.EnvioMensagemSucesso(this.Page, "CEP não encontrado.");
                }
            }
            else 
            {
                AuxiliPagina.EnvioMensagemSucesso(this.Page, "Campo CEP está em branco");
            }
        }

        protected void imgPesqPacCPF_OnClick(object sender, EventArgs e)
        {
            string cpfPac = txtCPFPac.Text.Replace(".", "").Replace("-", "");

            if (!String.IsNullOrEmpty(cpfPac))
            {
                var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                           where tb07.NU_CPF_ALU == cpfPac
                           select new
                           {
                               tb07.NU_CPF_ALU,
                               tb07.NO_ALU,
                               tb07.CO_SEXO_ALU,
                               tb07.DT_NASC_ALU,
                               CO_OPER = (tb07.TB250_OPERA != null ? tb07.TB250_OPERA.CO_OPER : ""),
                               ID_PLAN = (tb07.TB251_PLANO_OPERA != null ? tb07.TB251_PLANO_OPERA.ID_PLAN : (int?)null),
                               tb07.NU_PLANO_SAUDE,
                               tb07.DT_VENC_PLAN,
                               ID_DEFIC = (tb07.TBS387_DEFIC != null ? tb07.TBS387_DEFIC.ID_DEFIC : (int?)null)
                           }).FirstOrDefault();

                if (res != null)
                {
                    txtCPFPac.Text = res.NU_CPF_ALU;
                    txtNomePac.Text = res.NO_ALU;
                    ddlSexoPac.SelectedValue = res.CO_SEXO_ALU;
                    txtDtNascPac.Text = res.DT_NASC_ALU.HasValue ? res.DT_NASC_ALU.Value.ToShortDateString() : "";
                    if (String.IsNullOrEmpty(res.CO_OPER) && ddlOperadora.Items.FindByValue(res.CO_OPER) != null)
                        ddlOperadora.SelectedValue = res.CO_OPER;
                    CarregaPlanosSaude();
                    if (res.ID_PLAN.HasValue && ddlPlano.Items.FindByValue(res.ID_PLAN.ToString()) != null)
                        ddlPlano.SelectedValue = res.ID_PLAN.ToString();
                    txtNumeroPlano.Text = res.NU_PLANO_SAUDE;
                    txtDtVencPlan.Text = res.DT_VENC_PLAN.HasValue ? res.DT_VENC_PLAN.Value.ToShortDateString() : "";
                    ddlDeficiencia.SelectedValue = res.ID_DEFIC.ToString();

                }
            }
        }

        protected void lnkbSalvar_OnClick(object sender, EventArgs e)
        {
            if (!AuxiliValidacao.ValidaCpf(txtCPFResp.Text))
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "CPF do responsável invalido!");
                txtCPFResp.Focus();
                return;
            }

            if (!String.IsNullOrEmpty(txtCPFPac.Text) && !AuxiliValidacao.ValidaCpf(txtCPFPac.Text))
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "CPF do paciente invalido!");
                txtCPFPac.Focus();
                return;
            }

            if (chkEnviarEmail.Checked) 
            {
                EnvioEmail.EnviaEMail("teste pré-agendamento", txtEmailResp.Text, "tayguara.acioli@c2br.com.br", "Pré-Agendamento", false);
            }

            var tbs409 = new TBS409_PRE_AGEND();

            var coEmp = ddlUnidade.SelectedValue != "0" ? int.Parse(ddlUnidade.SelectedValue) : TB25_EMPRESA.RetornaTodosRegistros().Where(em => em.FL_UNID_MATRIZ == "S").FirstOrDefault().CO_EMP;

            //Agendamento
            tbs409.DT_AGEND = DateTime.Parse(txtData.Text);
            tbs409.HR_AGEND = txtHora.Text;
            tbs409.TP_AGEND = ddlEspecialidade.SelectedValue;
            tbs409.TP_CONSU = ddlTipoAg.SelectedValue;
            tbs409.CO_EMP = coEmp;
            tbs409.FL_CONFIR_SMS = chkEnviarSMS.Checked ? "S" : "N";
            tbs409.DE_SINTOMAS = txtSintomas.Text;
            tbs409.DT_CADAS = DateTime.Now;
            tbs409.CO_SITU = "A";

            //Paciente
            if (chkPaciEhResp.Checked)
            {
                tbs409.NO_PAC = txtNomeResp.Text.ToUpper();
                tbs409.DT_NASC_PAC = DateTime.Parse(txtDtNascResp.Text);
                tbs409.NU_CPF_PAC = txtCPFResp.Text.Replace("-", "").Replace(".", "");
                tbs409.CO_SEXO_PAC = ddlSexoResp.SelectedValue;
            }
            else
            {
                tbs409.NO_PAC = txtNomePac.Text.ToUpper();
                tbs409.DT_NASC_PAC = DateTime.Parse(txtDtNascPac.Text);
                tbs409.NU_CPF_PAC = txtCPFPac.Text.Replace("-", "").Replace(".", "");
                tbs409.CO_SEXO_PAC = ddlSexoPac.SelectedValue;
                tbs409.TBS387_DEFIC = (!string.IsNullOrEmpty(ddlDeficiencia.SelectedValue) ? TBS387_DEFIC.RetornaPelaChavePrimaria(int.Parse(ddlDeficiencia.SelectedValue)) : null);
            }
            //Responsável
            tbs409.NO_RESP = txtNomeResp.Text.ToUpper();
            tbs409.DT_NASC_RESP = DateTime.Parse(txtDtNascResp.Text);
            tbs409.NU_CPF_RESP = txtCPFResp.Text.Replace("-", "").Replace(".", "");
            tbs409.CO_SEXO_RESP = ddlSexoResp.SelectedValue;
            tbs409.NU_TELE_CELU_RESP = txtTelCelResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            tbs409.NU_TELE_RESI_RESP = txtTelResResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            tbs409.NU_TELE_WHATS_RESP = txtTelWhsResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            tbs409.DE_EMAIL_RESP = txtEmailResp.Text;
            tbs409.CO_GRAU_PAREN_RESP = ddlGrauParent.SelectedValue;

            //Endereço
            tbs409.CO_ENDE_CEP = txtCEP.Text;
            tbs409.CO_ENDE_ESTADO = ddlUF.SelectedValue;
            tbs409.CO_ENDE_CIDADE = int.Parse(ddlCidade.SelectedValue);
            tbs409.CO_ENDE_BAIRRO = (!String.IsNullOrEmpty(ddlBairro.SelectedValue) ? int.Parse(ddlBairro.SelectedValue) : (int?)null);
            tbs409.DE_ENDE = txtLogradouro.Text;

            //Contratação
            tbs409.TB250_OPERA = (!string.IsNullOrEmpty(ddlOperadora.SelectedValue) ? TB250_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlOperadora.SelectedValue)) : null);
            tbs409.TB251_PLANO_OPERA = (!string.IsNullOrEmpty(ddlPlano.SelectedValue) ? TB251_PLANO_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlPlano.SelectedValue)) : null);
            tbs409.DT_VENC_PLAN = !String.IsNullOrEmpty(txtDtVencPlan.Text) ? DateTime.Parse(txtDtVencPlan.Text) : (DateTime?)null;
            tbs409.NU_PLAN_SAUD = txtNumeroPlano.Text;

            #region Trata sequencial
            //Trata para gerar um Código do Encaminhamento
            var res2 = (from tbs409pesq in TBS409_PRE_AGEND.RetornaTodosRegistros()
                        select new { tbs409pesq.NU_REGIS_PRE_AGEND }).OrderByDescending(w => w.NU_REGIS_PRE_AGEND).FirstOrDefault();

            string seq;
            int seq2 = 0;
            int seqConcat;
            string seqcon;
            string ano = DateTime.Now.Year.ToString().Substring(2, 2);
            string mes = DateTime.Now.Month.ToString().PadLeft(2, '0');

            if (res2 != null && !String.IsNullOrEmpty(res2.NU_REGIS_PRE_AGEND))
            {
                seq = res2.NU_REGIS_PRE_AGEND.Substring(6, 6);
                seq2 = int.Parse(seq);
            }

            seqConcat = seq2 + 1;
            seqcon = seqConcat.ToString().PadLeft(6, '0');

            string CodigoAtendimento = string.Format("PA{0}{1}{2}", ano, mes, seqcon);
            #endregion

            tbs409.NU_REGIS_PRE_AGEND = CodigoAtendimento;

            TBS409_PRE_AGEND.SaveOrUpdate(tbs409, true);

            lblCodigoAtendimento.InnerText = CodigoAtendimento;

            //ScriptManager.RegisterStartupScript(this, GetType(), "IdUnicoParaSucesso", "Sucesso();", true);

            AuxiliPagina.EnvioMensagemSucesso(this.Page, "Seu número de Pré-Agendamento é: " + CodigoAtendimento);
        }

        #endregion
    }
}