//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: GESTÃO OPERACIONAL DE RH
// OBJETIVO: REGISTRO DO PONTO DO COLABORADOR
// DATA DE CRIAÇÃO: 07/05/2014
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//------------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 11/06/2014| Maxwell Almeida            | Criação da Funcionalidade para cadastro de Usuário de Campanha

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library;
using System.Data.Objects;
using C2BR.GestorEducacao.UI.App_Masters;

namespace C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9100_CtrlCampanhasSaude._9110_Registros._9113_UsuarioCampanha
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //--------> Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            base.OnPreInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaUF();
                CarregaBairros("", 0);
                CarregaCidades("");
            }
        }

        /// <summary>
        /// Faz o Salvamento das informações do Pré-Atendimento na TBS194
        /// </summary>
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            bool erros = false;

            //----------------------------------------------------- Valida os campos do Responsável -----------------------------------------------------
            if (txtNomeResp.Text == "")
            { AuxiliPagina.EnvioMensagemErro(this.Page, "O Nome do Responsável é Requerido"); erros = true; }

            if (txtCPFRespInfo.Text == "")
            { AuxiliPagina.EnvioMensagemErro(this.Page, "O CPF do Responsável é Requerido"); erros = true; }

            if (ddlSexoResp.SelectedValue == "")
            { AuxiliPagina.EnvioMensagemErro(this.Page, "O Sexo do Responsável é Requerido"); erros = true; }

            if (txtDtNascResp.Text == "")
            { AuxiliPagina.EnvioMensagemErro(this.Page, "A Data de Nascimento do Responsável é Requerida"); erros = true; }

            if (txtTelCelResp.Text == "")
            { AuxiliPagina.EnvioMensagemErro(this.Page, "O Telefone do Responsável é Requerido"); erros = true; }

            //----------------------------------------------------- Valida os campos do Aluno -----------------------------------------------------
            //if(txtCPFPaci.Text == "")
            //    { AuxiliPagina.EnvioMensagemErro(this.Page, "O CPF do Paciente é Requerido"); erros = true; }

            if (txtNomePaciente.Text == "")
            { AuxiliPagina.EnvioMensagemErro(this.Page, "O Nome do Paciente é Requerido"); erros = true; }

            if (ddlSexoUsu.SelectedValue == "")
            { AuxiliPagina.EnvioMensagemErro(this.Page, "O Sexo do Paciente é Requerido"); erros = true; }

            if (txtDtNascUsu.Text == "")
            { AuxiliPagina.EnvioMensagemErro(this.Page, "A Data de Nascimento do Paciente é Requerida"); erros = true; }

            if (erros != true)
            {
                #region Persistência dos dados do Responsável
                TB108_RESPONSAVEL tb108 = RetornaEntidadeResp(hidCoResp);

                tb108.NO_RESP = txtNomeResp.Text;
                tb108.NU_CPF_RESP = txtCPFRespInfo.Text.Replace("-", "").Replace(".", "").Trim();
                tb108.CO_RG_RESP = txtRGResp.Text;
                tb108.DT_NASC_RESP = (!string.IsNullOrEmpty(txtDtNascResp.Text) ? DateTime.Parse(txtDtNascResp.Text) : (DateTime?)null);
                tb108.NU_TELE_CELU_RESP = txtTelCelResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb108.NU_TELE_RESI_RESP = txtTelResResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb108.CO_SEXO_RESP = ddlSexoResp.SelectedValue;
                tb108.CO_CEP_RESP = txtCepResp.Text;
                tb108.CO_ESTA_RESP = ddlUfResp.SelectedValue;
                tb108.CO_CIDADE = (!string.IsNullOrEmpty(ddlCidadeResp.SelectedValue) ? int.Parse(ddlCidadeResp.SelectedValue) : (int?)null);
                tb108.CO_BAIRRO = (!string.IsNullOrEmpty(ddlBairroUsu.SelectedValue) ? int.Parse(ddlBairroUsu.SelectedValue) : (int?)null);
                tb108.DE_ENDE_RESP = txtLogradouroResp.Text;
                tb108.DES_EMAIL_RESP = txtEmailResp.Text;
                tb108.CO_ORIGEM_RESP = "NN";

                //Atribui valores vazios para os campos not null da tabela de Responsável.
                tb108.FL_NEGAT_CHEQUE = "V";
                tb108.FL_NEGAT_SERASA = "V";
                tb108.FL_NEGAT_SPC = "V";
                tb108.CO_INST = 0;
                tb108.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);

                tb108 = TB108_RESPONSAVEL.SaveOrUpdate(tb108);
                #endregion

                #region Persistência dos Dados do Aluno

                TB07_ALUNO tb07 = new TB07_ALUNO();

                tb07.NO_ALU = txtNomePaciente.Text;
                tb07.NU_CPF_ALU = (!string.IsNullOrEmpty(txtCPFUsuaInfo.Text) ? txtCPFUsuaInfo.Text.Replace(".", "").Replace("-", "").Trim() : null);
                tb07.CO_RG_ALU = txtRGUsu.Text;
                tb07.NU_NIS = (!string.IsNullOrEmpty(txtNisUsu.Text) ? decimal.Parse(txtNisUsu.Text) : (decimal?)null);
                tb07.DT_NASC_ALU = (!string.IsNullOrEmpty(txtDtNascUsu.Text) ? DateTime.Parse(txtDtNascUsu.Text) : (DateTime?)null);
                tb07.CO_SEXO_ALU = ddlSexoUsu.SelectedValue;
                tb07.NU_TELE_CELU_ALU = (!string.IsNullOrEmpty(txtTelCelUsu.Text) ? txtTelCelUsu.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "") : null);
                tb07.NU_TELE_RESI_ALU = (!string.IsNullOrEmpty(txtTelResUsu.Text) ? txtTelResUsu.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "") : null);
                tb07.NO_EMAIL_PAI = txtEmailUsu.Text;
                tb07.CO_EMP = LoginAuxili.CO_EMP;
                tb07.TB25_EMPRESA1 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                tb07.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                tb07.TB108_RESPONSAVEL = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(tb108.CO_RESP);
                tb07.FL_USUAR_CAD_UNICO = "N";
                tb07.CO_CEP_ALU = this.txtCepUsu.Text.Replace("-", "");
                tb07.CO_ESTA_ALU = this.ddlUFUsu.SelectedValue != "" ? this.ddlUFUsu.SelectedValue : null;
                tb07.DE_ENDE_ALU = this.txtLograUsu.Text;
                tb07.TB905_BAIRRO = (!string.IsNullOrEmpty(ddlBairroUsu.SelectedValue) ? TB905_BAIRRO.RetornaPelaChavePrimaria(int.Parse(ddlBairroUsu.SelectedValue)) : null);
                tb07.DT_CADA_ALU = DateTime.Now;

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

                #endregion

                AuxiliPagina.RedirecionaParaPaginaSucesso("Usuário de Campanha Registrado com sucesso.", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
            }
        }
        #endregion

        #region Carregamentos

        /// <summary>
        /// Método que verifica se é cadastro de um novo responsável ou busca de um já existente
        /// </summary>
        /// <param name="hid"></param>
        /// <returns></returns>
        private TB108_RESPONSAVEL RetornaEntidadeResp(HiddenField hid)
        {
            if (!string.IsNullOrEmpty(hid.Value))
            {
                return TB108_RESPONSAVEL.RetornaPelaChavePrimaria(int.Parse(hid.Value));
            }
            else
            {
                return new TB108_RESPONSAVEL();
            }
        }

        /// <summary>
        /// Atribui as Informações inseridas nos campos de Responsável, ao campo de Paciente
        /// </summary>
        private void carregaRespPaci()
        {
            if (chkRespPasci.Checked == true)
            {
                txtCPFUsuaInfo.Text = txtCPFRespInfo.Text;
                txtRGUsu.Text = txtRGResp.Text;
                txtNomePaciente.Text = txtNomeResp.Text;
                ddlSexoUsu.SelectedValue = ddlSexoResp.SelectedValue;
                txtDtNascUsu.Text = txtDtNascResp.Text;
                txtTelCelUsu.Text = txtTelCelResp.Text;
                txtTelResUsu.Text = txtTelResResp.Text;
                txtEmailUsu.Text = txtEmailResp.Text;
                ddlUFUsu.SelectedValue = ddlUfResp.SelectedValue;
                txtCepUsu.Text = txtCepResp.Text;
                AuxiliCarregamentos.CarregaCidades(ddlCidadeUsu, false, ddlUFUsu.SelectedValue, LoginAuxili.CO_EMP, true, true);
                ddlCidadeUsu.SelectedValue = ddlCidadeResp.SelectedValue;
                AuxiliCarregamentos.CarregaBairros(ddlBairroUsu, ddlUFUsu.SelectedValue, (ddlCidadeUsu.SelectedValue != "" ? int.Parse(ddlCidadeUsu.SelectedValue) : 0),false,true);
                ddlBairroUsu.SelectedValue = ddlBairroResp.SelectedValue;
                txtLograUsu.Text = txtLogradouroResp.Text;

                CalculaIdadeUsu();
            }
            else
            {
                txtCPFUsuaInfo.Text = txtRGUsu.Text = txtNomePaciente.Text = ddlSexoUsu.SelectedValue = txtDtNascUsu.Text 
                = txtTelCelUsu.Text = txtTelResUsu.Text = txtIdadePaci.Text = txtEmailUsu.Text = ddlUFUsu.SelectedValue
                = txtCepUsu.Text = ddlCidadeUsu.SelectedValue = ddlBairroUsu.SelectedValue = txtLograUsu.Text = "";
            }
        }

        /// <summary>
        /// Carrega todas as UFs
        /// </summary>
        private void CarregaUF()
        {
            AuxiliCarregamentos.CarregaUFs(ddlUfResp, false);
            AuxiliCarregamentos.CarregaUFs(ddlUFUsu, false);
        }

        /// <summary>
        /// Carrega as Cidades
        /// </summary>
        private void CarregaCidades(string COUF)
        {
            AuxiliCarregamentos.CarregaCidades(ddlCidadeResp, false, COUF, LoginAuxili.CO_EMP, true, true);
            AuxiliCarregamentos.CarregaCidades(ddlCidadeUsu, false, COUF, LoginAuxili.CO_EMP, true, true);
        }

        /// <summary>
        /// Carrega os Bairros
        /// </summary>
        private void CarregaBairros(string COUF, int CO_CIDADE)
        {
            AuxiliCarregamentos.CarregaBairros(ddlBairroResp, COUF, CO_CIDADE, false);
            AuxiliCarregamentos.CarregaBairros(ddlBairroUsu, COUF, CO_CIDADE, false);
        }

        /// <summary>
        /// Calcula a Idade do Paciente de acordo com a data de nascimento inserida no campo DT Nascimento.
        /// </summary>
        private void CalculaIdadeUsu()
        {
            DateTime dtNasci = DateTime.Parse(txtDtNascUsu.Text);
            int anos = DateTime.Now.Year - dtNasci.Year;

            if (DateTime.Now.Month < dtNasci.Month || (DateTime.Now.Month == dtNasci.Month && DateTime.Now.Day < dtNasci.Day))
                anos--;

            string idade = anos.ToString();

            txtIdadePaci.Text = idade;
        }

        /// <summary>
        /// Pesquisa se existe responsável cadastrado com o CPF informado no campo CPF do Responsável.
        /// </summary>
        private void carregaRespCPF()
        {
            string cpfResp = txtCPFRespPesq.Text.Replace(".", "").Replace("-", "");

            var res = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                       where tb108.NU_CPF_RESP == cpfResp
                       select tb108).FirstOrDefault();

            if (res != null)
            {
                txtCPFRespInfo.Text = res.NU_CPF_RESP;
                txtRGResp.Text = res.CO_RG_RESP;
                txtNomeResp.Text = res.NO_RESP;
                ddlSexoResp.SelectedValue = res.CO_SEXO_RESP;
                txtDtNascResp.Text = res.DT_NASC_RESP.ToString();
                txtTelCelResp.Text = res.NU_TELE_CELU_RESP;
                txtTelResResp.Text = res.NU_TELE_RESI_RESP;
                txtEmailResp.Text = res.DES_EMAIL_RESP;
                txtCepResp.Text = res.CO_CEP_RESP;
                
                res.TB74_UFReference.Load();
                //ddlUfResp.SelectedValue = res.TB74_UF.CODUF;
                if(res.TB74_UF != null)
                    ddlUfResp.SelectedValue = res.TB74_UF.CODUF;

                res.TB904_CIDADEReference.Load();
                //ddlCidadeResp.SelectedValue = res.TB904_CIDADE.CO_CIDADE.ToString();

                ListItem liCR = ddlCidadeResp.Items.FindByValue(res.CO_CIDADE.ToString());
                if (liCR != null)
                {
                    ddlCidadeResp.SelectedValue = res.CO_CIDADE.ToString();
                }
                else
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "A Cidade no cadastro do responsável não condiz com a UF no cadastro do mesmo, favor informar a correta.");
                    ddlCidadeResp.Focus();
                }

                //Verifica se existe na lista de bairros, um bairros de acordo com a Cidade no cadastro do responsável, caso não exista, apresenta uma mensagem de erro tratada.
                ListItem liBR = ddlBairroResp.Items.FindByValue(res.CO_BAIRRO.ToString());
                if (liBR != null)
                {
                    ddlBairroResp.SelectedValue = res.CO_BAIRRO.ToString();
                }
                else
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "O Bairro no cadastro do responsável não condiz com a Cidade no cadastro do mesmo, favor informar o correto.");
                    ddlBairroResp.Focus();
                }

                txtLogradouroResp.Text = res.DE_ENDE_RESP;
            }
        }

        #endregion

        #region Funções de Campo

        protected void chkRespPasci_OnCheckedChanged(object sender, EventArgs e)
        {
            carregaRespPaci();
        }

        protected void txtDtNasc_OnTextChanged(object sender, EventArgs e)
        {
            CalculaIdadeUsu();
        }

        protected void imgCpfResp_OnClick(object sender, EventArgs e)
        {
            carregaRespCPF();
        }

        protected void ddlUfResp_SelectedIndexChanged(object sender, EventArgs e)
        {
            AuxiliCarregamentos.CarregaCidades(ddlCidadeResp, false, ddlUfResp.SelectedValue, LoginAuxili.CO_EMP);
        }

        protected void ddlCidadeResp_SelectedIndexChanged(object sender, EventArgs e)
        {
            int coCidade = (!string.IsNullOrEmpty(ddlCidadeResp.SelectedValue) ? int.Parse(ddlCidadeResp.SelectedValue) : 0);
            AuxiliCarregamentos.CarregaBairros(ddlBairroResp, ddlUfResp.SelectedValue, coCidade, false);
        }

        protected void ddlUFUsu_SelectedIndexChanged(object sender, EventArgs e)
        {
            AuxiliCarregamentos.CarregaCidades(ddlCidadeUsu, false, ddlUFUsu.SelectedValue, LoginAuxili.CO_EMP);
        }

        protected void ddlCidadeUsu_SelectedIndexChanged(object sender, EventArgs e)
        {
            int coCidade = (!string.IsNullOrEmpty(ddlCidadeUsu.SelectedValue) ? int.Parse(ddlCidadeUsu.SelectedValue) : 0);
            AuxiliCarregamentos.CarregaBairros(ddlBairroUsu, ddlUFUsu.SelectedValue, coCidade, false);
        }
        
        protected void btnPesqCEPR_Click(object sender, ImageClickEventArgs e)
        {
            if (txtCepResp.Text.Replace("-", "") != "")
            {
                int numCep = int.Parse(txtCepResp.Text.Replace("-", ""));

                TB235_CEP tb235 = TB235_CEP.RetornaTodosRegistros().Where(c => c.CO_CEP == numCep).FirstOrDefault();

                if (tb235 != null)
                {
                    txtLogradouroResp.Text = tb235.NO_ENDER_CEP;
                    tb235.TB905_BAIRROReference.Load();
                    ddlUfResp.SelectedValue = tb235.TB905_BAIRRO.CO_UF;
                    AuxiliCarregamentos.CarregaCidades(ddlCidadeResp, false, ddlUfResp.SelectedValue, LoginAuxili.CO_EMP, true, true);
                    ddlCidadeResp.SelectedValue = tb235.TB905_BAIRRO.CO_CIDADE.ToString();
                    AuxiliCarregamentos.CarregaBairros(ddlBairroResp, ddlUfResp.SelectedValue, tb235.TB905_BAIRRO.CO_CIDADE, false);
                    ddlBairroResp.SelectedValue = tb235.TB905_BAIRRO.CO_BAIRRO.ToString();
                }
                else
                {
                    txtLogradouroResp.Text = "";
                    ddlBairroResp.SelectedValue = "";
                    ddlCidadeResp.SelectedValue = "";
                    ddlUfResp.SelectedValue = "";
                }
            }
        }

        protected void imgPesqUsu_OnClick(object sender, ImageClickEventArgs e)
        {
            if (txtCepUsu.Text.Replace("-", "") != "")
            {
                int numCep = int.Parse(txtCepUsu.Text.Replace("-", ""));

                TB235_CEP tb235 = TB235_CEP.RetornaTodosRegistros().Where(c => c.CO_CEP == numCep).FirstOrDefault();

                if (tb235 != null)
                {
                    txtLograUsu.Text = tb235.NO_ENDER_CEP;
                    tb235.TB905_BAIRROReference.Load();
                    ddlUFUsu.SelectedValue = tb235.TB905_BAIRRO.CO_UF;
                    AuxiliCarregamentos.CarregaCidades(ddlCidadeUsu, false, ddlUFUsu.SelectedValue, LoginAuxili.CO_EMP, true, true);
                    ddlCidadeResp.SelectedValue = tb235.TB905_BAIRRO.CO_CIDADE.ToString();
                    AuxiliCarregamentos.CarregaBairros(ddlBairroUsu, ddlUFUsu.SelectedValue, tb235.TB905_BAIRRO.CO_CIDADE, false);
                    ddlBairroUsu.SelectedValue = tb235.TB905_BAIRRO.CO_BAIRRO.ToString();
                }
                else
                {
                    txtLograUsu.Text = "";
                    ddlBairroUsu.SelectedValue = "";
                    ddlCidadeResp.SelectedValue = "";
                    ddlUFUsu.SelectedValue = "";
                }
            }
        }

        #endregion
    }
}