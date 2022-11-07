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
// 11/06/2014| Maxwell Almeida            | Criação da FUncionalidade para cadastro do Pré-Atendimento do Usuário 

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

namespace C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3203_RegistroPreAtendimentoUsuario
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
                carregaEspecialidades();
                carregaClassificacaoRisco();
                CarregaOperadora();
                CarregaTiposDores(ddlDor1);
                CarregaTiposDores(ddlDor2);
                CarregaTiposDores(ddlDor3);
                CarregaTiposDores(ddlDor4);

                txtDataSenha.Text = txtDtDor.Text = DateTime.Now.Date.ToString();
                txtHoraSenha.Text = txtHrDor.Text = DateTime.Now.ToString("HH:mm");
            }
        }

        /// <summary>
        /// Faz o Salvamento das informações do Pré-Atendimento na TBS194
        /// </summary>
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            bool erros = false;

            //----------------------------------------------------- Valida os campos do Responsável -----------------------------------------------------
                if(txtNomeResp.Text == "")
                    { AuxiliPagina.EnvioMensagemErro(this.Page, "O Nome do Responsável é Requerido"); erros = true; }

                if(txtCPFResp.Text == "")
                    { AuxiliPagina.EnvioMensagemErro(this.Page, "O CPF do Responsável é Requerido"); erros = true; }

                if(ddlSexoResp.SelectedValue == "")
                    { AuxiliPagina.EnvioMensagemErro(this.Page, "O Sexo do Responsável é Requerido"); erros = true; }

                if(txtDtNascResp.Text == "")
                    { AuxiliPagina.EnvioMensagemErro(this.Page, "A Data de Nascimento do Responsável é Requerida"); erros = true; }
                
                if(txtTelCelResp.Text == "")
                    { AuxiliPagina.EnvioMensagemErro(this.Page, "O Telefone do Responsável é Requerido"); erros = true; }

                //----------------------------------------------------- Valida os campos do Aluno -----------------------------------------------------
                //if(txtCPFPaci.Text == "")
                //    { AuxiliPagina.EnvioMensagemErro(this.Page, "O CPF do Paciente é Requerido"); erros = true; }

                if(txtNomePaciente.Text == "")
                    { AuxiliPagina.EnvioMensagemErro(this.Page, "O Nome do Paciente é Requerido"); erros = true; }

                if(ddlSxPac.SelectedValue == "")
                    { AuxiliPagina.EnvioMensagemErro(this.Page, "O Sexo do Paciente é Requerido"); erros = true; }

                if(txtDtNasc.Text == "")
                    { AuxiliPagina.EnvioMensagemErro(this.Page, "A Data de Nascimento do Paciente é Requerida"); erros = true; }

                //----------------------------------------------------- Valida os Campos da Avaliação -----------------------------------------------------
                if(ddlClassRisco.SelectedValue == "")
                    { AuxiliPagina.EnvioMensagemErro(this.Page, "A Classificação de Risco é Requerida"); erros = true; }

                if(ddlEspec.SelectedValue == "")
                    { AuxiliPagina.EnvioMensagemErro(this.Page, "A Especialidade é Requerida"); erros = true; }

                //int vlTpFu;
                //bool eInt = int.TryParse(txtTempoFumante.Text, out vlTpFu);
                //if(eInt)
                //{ AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o tempo de fumante em anos Inteiros"); erros = true; }

                //int vltpBe;
                //bool eIntB = int.TryParse(txtTempoBebidas.Text, out vltpBe);
                //if(eIntB)
                //{ AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o tempo de bebida em anos Inteiros"); erros = true; }
            
            if(erros != true)
            {
                #region Salva Responsável

                TB108_RESPONSAVEL tb108;
                //Verifica se já não existe um responsável cadastrdado com esse CPF, caso não exista ele cadastra um novo com os dados informados
                if (string.IsNullOrEmpty(hidCoResp.Value))
                {
                    tb108 = new TB108_RESPONSAVEL();

                    tb108.NO_RESP = txtNomeResp.Text;
                    tb108.NU_CPF_RESP = txtCPFResp.Text.Replace("-", "").Replace(".", "").Trim();
                    tb108.DT_NASC_RESP = DateTime.Parse(txtDtNascResp.Text);
                    tb108.CO_SEXO_RESP = ddlSexoResp.SelectedValue;
                    tb108.NU_TELE_CELU_RESP = txtTelCelResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                    tb108.NU_TELE_RESI_RESP = txtTelResResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                    tb108.CO_ORIGEM_RESP = "NN";
                    tb108.CO_SITU_RESP = "A";

                    //Atribui valores vazios para os campos not null da tabela de Responsável.
                    tb108.FL_NEGAT_CHEQUE = "V";
                    tb108.FL_NEGAT_SERASA = "V";
                    tb108.FL_NEGAT_SPC = "V";
                    tb108.CO_INST = 0;
                    tb108.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);

                    tb108 = TB108_RESPONSAVEL.SaveOrUpdate(tb108);
                }
                else
                {
                    int coRe = int.Parse(hidCoResp.Value);
                    tb108 = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(coRe);
                }

                #endregion

                //Salva os dados do Usuário em um registro na tb07
                #region Salva o Usuário na TB07

                TB07_ALUNO tb07;
                if (string.IsNullOrEmpty(hidCoPaci.Value))
                {
                    tb07 = new TB07_ALUNO();

                    tb07.NO_ALU = txtNomePaciente.Text;
                    tb07.NU_CPF_ALU = txtCPFPaci.Text.Replace(".", "").Replace("-", "").Trim();
                    tb07.NU_NIS = (!string.IsNullOrEmpty(txtNisPaci.Text) ? decimal.Parse(txtNisPaci.Text) : (decimal?)null);
                    tb07.DT_NASC_ALU = DateTime.Parse(txtDtNasc.Text);
                    tb07.CO_SEXO_ALU = ddlSxPac.SelectedValue;
                    tb07.NU_TELE_CELU_ALU = txtTelCelUsu.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                    tb07.NU_TELE_RESI_ALU = txtTelResUsu.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                    tb07.CO_GRAU_PAREN_RESP = ddlGrauParen.SelectedValue;
                    tb07.CO_EMP = LoginAuxili.CO_EMP;
                    tb07.TB25_EMPRESA1 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                    tb07.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                    tb07.TB108_RESPONSAVEL = (tb108 != null ? tb108 : null);

                    //Salva os valores para os campos not null da tabela de Usuário
                    tb07.CO_SITU_ALU = "A";
                    tb07.TP_DEF = "N";

                    #region trata para criação do nire

                    var resNire = (from tb07pesq in TB07_ALUNO.RetornaTodosRegistros()
                               select new { tb07pesq.NU_NIRE }).OrderByDescending(w => w.NU_NIRE).FirstOrDefault();

                    int nir = 0;
                    if (resNire == null)
                    {
                        nir = 1;
                    }
                    else
                    {
                        nir = resNire.NU_NIRE;
                    }

                    int nirTot = nir + 1;

                    #endregion
                    tb07.NU_NIRE = nirTot;

                    tb07 = TB07_ALUNO.SaveOrUpdate(tb07);
                }
                else
                {
                    int coPac = int.Parse(hidCoPaci.Value);
                    tb07 = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.CO_ALU == coPac).FirstOrDefault();
                }

                #endregion

                TBS194_PRE_ATEND tbs194 = RetornaEntidade();

                tbs194.TB250_OPERA = (!string.IsNullOrEmpty(ddlOperadora.SelectedValue) ? (TB250_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlOperadora.SelectedValue))) : null);
                tbs194.CO_RESP = tb108.CO_RESP;
                tbs194.CO_ALU = tb07.CO_ALU;
                tbs194.CO_SITUA_PRE_ATEND = "A";
                tbs194.DT_SITUA_PRE_ATEND = DateTime.Now;
                tbs194.NU_SENHA_ATEND = txtSenha.Text;
                tbs194.NO_USU = txtNomePaciente.Text;
                tbs194.CO_EMP = LoginAuxili.CO_EMP;
                tbs194.CO_SEXO_USU = ddlSxPac.SelectedValue;
                tbs194.DT_NASC_USU = DateTime.Parse(txtDtNasc.Text);
                tbs194.NU_PESO = (!string.IsNullOrEmpty(txtPeso.Text) ? decimal.Parse(txtPeso.Text) : (decimal?)null);
                tbs194.NU_ALTU = (!string.IsNullOrEmpty(txtAltura.Text) ? decimal.Parse(txtAltura.Text) : (decimal?)null);
                tbs194.NU_TEMP = (!string.IsNullOrEmpty(txtTemper.Text) ? decimal.Parse(txtTemper.Text) : (decimal?)null);
                tbs194.HR_TEMP = (!string.IsNullOrEmpty(txtHoraTemper.Text) ? txtHoraTemper.Text : null);
                tbs194.NU_PRES_ARTE = (!string.IsNullOrEmpty(txtPressArt.Text) ? txtPressArt.Text : null);
                tbs194.HR_PRES_ARTE = (!string.IsNullOrEmpty(txtHoraPressArt.Text) ? txtHoraPressArt.Text : null);
                tbs194.NU_GLICE = (!string.IsNullOrEmpty(txtGlicem.Text) ? int.Parse(txtGlicem.Text) : (int?)null);
                tbs194.HR_GLICE = (!string.IsNullOrEmpty(txtHoraGlicem.Text) ? txtHoraGlicem.Text : null);
                tbs194.NU_NIS = (!string.IsNullOrEmpty(txtNisPaci.Text) ? Convert.ToInt32(txtNisPaci.Text) : (int?)null);
                tbs194.NU_CPF_RESP = txtCPFResp.Text.Replace(".", "").Replace("-", "");
                tbs194.NU_CPF_USU = txtCPFPaci.Text.Replace(".", "").Replace("-", "");
                tbs194.NO_RESP = txtNomeResp.Text;
                tbs194.NU_TEL_RESP = txtTelResResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tbs194.NU_CEL_RESP = txtTelCelResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tbs194.NU_TEL_USU = txtTelResUsu.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tbs194.NU_CEL_USU = txtTelCelUsu.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tbs194.CO_GRAU_PAREN = ddlGrauParen.SelectedValue; //////////////////
                tbs194.FL_DIABE = (chkDiabetes.Checked ? "S" : "N");
                tbs194.DE_DIABE = (chkDiabetes.Checked ? ddlDiabetes.SelectedValue : null );
                tbs194.FL_HIPER_TENSO = (chkHibert.Checked ? "S" : "N");
                tbs194.DE_HIPER_TENSO = (chkHibert.Checked ? (!string.IsNullOrEmpty(txtHiper.Text) ? txtHiper.Text : null) : null);
                tbs194.FL_MARCA_PASSO = (chkMarcPass.Checked ? "S" : "N");
                tbs194.DE_MARCA_PASSO = (chkMarcPass.Checked ? (!string.IsNullOrEmpty(txtMarcPass.Text) ? txtMarcPass.Text : null) : null);
                tbs194.FL_VALVU = (chkValvulas.Checked ? "S" : "N");
                tbs194.DE_VALVU = (chkValvulas.Checked ? (!string.IsNullOrEmpty(txtValvula.Text) ? txtValvula.Text : null) : null);
                tbs194.FL_CIRUR = (chkCiru.Checked ? "S" : "N");
                tbs194.DE_CIRUR = (chkCiru.Checked ? (!string.IsNullOrEmpty(txtCiru.Text) ? txtCiru.Text : null) : null);
                tbs194.FL_ALERG = (chkAlergia.Checked ? "S" : "N");
                tbs194.DE_ALERG = (chkAlergia.Checked ? (!string.IsNullOrEmpty(txtAlergia.Text) ? txtAlergia.Text : null) : null);
                tbs194.FL_FUMAN = ddlFumante.SelectedValue;
                tbs194.NU_TEMPO_FUMAN = (!string.IsNullOrEmpty(txtTempoFumante.Text) ? int.Parse(txtTempoFumante.Text) : (int?)null);
                tbs194.FL_ALCOO = ddlAlcool.SelectedValue;
                tbs194.NU_TEMPO_ALCOO = (!string.IsNullOrEmpty(txtTempoBebidas.Text) ? int.Parse(txtTempoBebidas.Text) : (int?)null);
                tbs194.DE_MEDIC_USO_CONTI = (!string.IsNullOrEmpty(txtMedicContinuo.Text) ? txtMedicContinuo.Text : null);
                tbs194.DE_MEDIC = (!string.IsNullOrEmpty(txtMedicacaoAdmin.Text) ? txtMedicacaoAdmin.Text : null);
                tbs194.DE_SINTO = (!string.IsNullOrEmpty(txtSintomas.Text) ? txtSintomas.Text : null);
                tbs194.DE_OBSER = (!string.IsNullOrEmpty(txtObserPreAtend.Text) ? txtObserPreAtend.Text : null);
                tbs194.FL_SINTO_DORES = ddlDores.SelectedValue;
                tbs194.FL_SINTO_ENJOO = ddlEnjoos.SelectedValue;
                tbs194.FL_SINTO_VOMIT = ddlVomitos.SelectedValue;
                tbs194.FL_SINTO_FEBRE = ddlFebre.SelectedValue;
                tbs194.CO_TIPO_RISCO = int.Parse(ddlClassRisco.SelectedValue);
                tbs194.CO_ESPEC = int.Parse(ddlEspec.SelectedValue);
                tbs194.DT_PRE_ATEND = DateTime.Now;
                tbs194.CO_EMP_FUNC = LoginAuxili.CO_EMP;
                tbs194.CO_COL_FUNC = LoginAuxili.CO_COL;
                tbs194.NR_IP_FUNC = Request.UserHostAddress;
                tbs194.CO_SEXO_RESP = ddlSexoResp.SelectedValue;
                tbs194.DT_NASC_RESP = DateTime.Parse(txtDtNascResp.Text);
                tbs194.FL_ATEND_MEDIC = "N";
                tbs194.TB250_OPERA = (!string.IsNullOrEmpty(ddlOperadora.SelectedValue) ? TB250_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlOperadora.SelectedValue)) : null);
                tbs194.TBS337_TIPO_DORES = (!string.IsNullOrEmpty(ddlDor1.SelectedValue) ? TBS337_TIPO_DORES.RetornaPelaChavePrimaria(int.Parse(ddlDor1.SelectedValue)) : null);
                tbs194.TBS337_TIPO_DORES1 = (!string.IsNullOrEmpty(ddlDor2.SelectedValue) ? TBS337_TIPO_DORES.RetornaPelaChavePrimaria(int.Parse(ddlDor2.SelectedValue)) : null);
                tbs194.TBS337_TIPO_DORES2 = (!string.IsNullOrEmpty(ddlDor3.SelectedValue) ? TBS337_TIPO_DORES.RetornaPelaChavePrimaria(int.Parse(ddlDor3.SelectedValue)) : null);
                tbs194.TBS337_TIPO_DORES3 = (!string.IsNullOrEmpty(ddlDor4.SelectedValue) ? TBS337_TIPO_DORES.RetornaPelaChavePrimaria(int.Parse(ddlDor4.SelectedValue)) : null);

                //Trata Data e Hora da SENHA inserido no Cadastro para salvar em um campo só, como DateTime.
                #region e

                DateTime dtSen = Convert.ToDateTime(txtDataSenha.Text);
                DateTime hrSen = Convert.ToDateTime(txtHoraSenha.Text);

                int hrs = int.Parse(txtHoraSenha.Text.Substring(0, 2));
                int min = int.Parse(txtHoraSenha.Text.Substring(3, 2));

                DateTime totDtHrSenha = dtSen.AddHours(hrs).AddMinutes(min);

                DateTime totDtHrSenhaValid = totDtHrSenha;

                #endregion

                //Trata Data e Hora da DOR inserido no Cadastro para salvar em um campo só, como DateTime.
                #region e

                DateTime dtDor = Convert.ToDateTime(txtDtDor.Text);
                DateTime hrDor = Convert.ToDateTime(txtHrDor.Text);

                int hrsDor = int.Parse(txtHrDor.Text.Substring(0, 2));
                int minDor = int.Parse(txtHrDor.Text.Substring(3, 2));

                DateTime totDtHrDor = dtDor.AddHours(hrsDor).AddMinutes(minDor);

                DateTime totDtHrDorValid = totDtHrDor;
                tbs194.DT_DOR = totDtHrDorValid;
                tbs194.HR_DOR = txtHrDor.Text;

                #endregion


                //Trata e concatena o Código do Pré-Atendimento (Verifica qual o ultimo número do Pré-Atendimento cadastrado no banco, e acrescenta + 1 no registro atual, 
                //caso não haja registro ainda no banco ele inicia uma contagem do 1).

                string coUnid = LoginAuxili.CO_UNID.ToString();
                int coEmp = LoginAuxili.CO_EMP;
                string ano = DateTime.Now.Year.ToString().Substring(2, 2);

                var res = (from tbs194pesq in TBS194_PRE_ATEND.RetornaTodosRegistros()
                           where tbs194pesq.CO_EMP == coEmp
                           select new { tbs194pesq.CO_PRE_ATEND }).OrderByDescending(w => w.CO_PRE_ATEND).FirstOrDefault();

                string seq;
                int seq2;
                int seqConcat;
                string seqcon;
                if (res == null)
                {
                    seq2 = 1;
                }
                else
                {
                    seq = res.CO_PRE_ATEND.Substring(7, 7);
                    seq2 = int.Parse(seq);
                }

                seqConcat = seq2 + 1;
                seqcon = seqConcat.ToString().PadLeft(7, '0');

                tbs194.CO_PRE_ATEND = ano + coUnid.PadLeft(3, '0') + "PA" + seqcon;
                tbs194.DT_SENHA_ATEND = totDtHrSenhaValid;

                TBS194_PRE_ATEND.SaveOrUpdate(tbs194, true);

                AuxiliPagina.RedirecionaParaPaginaSucesso("Pré-Atendimento Registrado com sucesso.", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());

                //CurrentPadraoCadastros.CurrentEntity = tbs194;
            }
        }
        #endregion

        #region Carregamentos

        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TBS194_PRE_ATEND</returns>
        private TBS194_PRE_ATEND RetornaEntidade()
        {
            TBS194_PRE_ATEND tbs194 = TBS194_PRE_ATEND.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tbs194 == null) ? new TBS194_PRE_ATEND() : tbs194;
        }

        /// <summary>
        /// Carrega as Especialidades no campo de especialidade.
        /// </summary>
        private void carregaEspecialidades()
        {
            var res = (from tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros()
                       select new { tb63.CO_ESPECIALIDADE, tb63.NO_ESPECIALIDADE }).OrderBy(w => w.NO_ESPECIALIDADE);

            ddlEspec.DataTextField = "NO_ESPECIALIDADE";
            ddlEspec.DataValueField = "CO_ESPECIALIDADE";
            ddlEspec.DataSource = res;
            ddlEspec.DataBind();

            ddlEspec.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Carrega as classificações de risco
        /// </summary>
        private void carregaClassificacaoRisco()
        {
            AuxiliCarregamentos.CarregaClassificacaoRisco(ddlClassRisco, false);
        }

        /// <summary>
        /// Carrega os tipos de dores
        /// </summary>
        /// <param name="ddl"></param>
        private void CarregaTiposDores(DropDownList ddl)
        {
            AuxiliCarregamentos.CarregaTiposDores(ddl, false, false, true);
        }

        /// <summary>
        /// Atribui as Informações inseridas nos campos de Responsável, ao campo de Paciente
        /// </summary>
        private void carregaRespPaci()
        {
            if (chkRespPasci.Checked == true)
            {
                txtCPFPaci.Text = txtCPFResp.Text;
                txtNomePaciente.Text = txtNomeResp.Text;
                ddlSxPac.SelectedValue = ddlSexoResp.SelectedValue;
                txtDtNasc.Text = txtDtNascResp.Text;
                txtTelCelUsu.Text = txtTelCelResp.Text;
                txtTelResUsu.Text = txtTelResResp.Text;

                txtCPFPaci.Enabled = false;
                txtNomePaciente.Enabled = false;
                ddlSxPac.Enabled = false;
                txtTelCelUsu.Enabled = false;
                txtTelResUsu.Enabled = false;
                txtDtNasc.Enabled = false;

                CalculaIdadeUsu();

                #region Verifica se já existe

                string cpf = txtCPFPaci.Text.Replace(".", "").Replace("-", "").Trim();

                var tb07 = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.NU_CPF_ALU == cpf).FirstOrDefault();

                if (tb07 != null)
                    hidCoPaci.Value = tb07.CO_ALU.ToString();

                #endregion
            }
            else
            {
                txtCPFPaci.Text = "";
                txtNomePaciente.Text = "";
                ddlSxPac.SelectedValue = "";
                txtDtNasc.Text = "";
                txtTelCelUsu.Text = "";
                txtTelResUsu.Text = "";
                txtIdadePaci.Text = "";

                txtCPFPaci.Enabled = true;
                txtNomePaciente.Enabled = true;
                ddlSxPac.Enabled = true;
                txtTelCelUsu.Enabled = true;
                txtTelResUsu.Enabled = true;
                txtDtNasc.Enabled = true;
                hidCoPaci.Value = "";
            }
        }

        /// <summary>
        /// Calcula a Idade do Paciente de acordo com a data de nascimento inserida no campo DT Nascimento.
        /// </summary>
        private void CalculaIdadeUsu()
        {
            DateTime dtNasci = DateTime.Parse(txtDtNasc.Text);
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
            string cpfResp = txtCPFResp.Text.Replace(".", "").Replace("-", "");

            var res = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                       where tb108.NU_CPF_RESP == cpfResp
                       select new { 
                                        tb108.NO_RESP,
                                        tb108.DT_NASC_RESP,
                                        tb108.NU_TELE_CELU_RESP,
                                        tb108.NU_TELE_RESI_RESP,
                                        tb108.CO_SEXO_RESP,
                                        tb108.CO_RESP,
                                  }).FirstOrDefault();

            if (res != null)
            {
                txtNomeResp.Text = res.NO_RESP;
                txtDtNascResp.Text = res.DT_NASC_RESP.ToString();
                txtTelCelResp.Text = res.NU_TELE_CELU_RESP;
                txtTelResResp.Text = res.NU_TELE_RESI_RESP;
                ddlSexoResp.SelectedValue = res.CO_SEXO_RESP;
                hidCoResp.Value = res.CO_RESP.ToString();
            }
        }

        /// <summary>
        /// Carrega as Operadoras
        /// </summary>
        private void CarregaOperadora()
        {
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddlOperadora, false);
        }

        /// <summary>
        /// Pesquisa se existe Paciente cadastrado de acordo com o parâmetro informado, podendo pesquisar por CPF e NIS do Paciente.
        /// </summary>
        private void carregaPaciCPF()
        {
            if (chkPesqCPFPaci.Checked == true)
            {
                string cpfPaci = txtCPFPaci.Text.Replace(".", "").Replace("-", "").Trim();

                if (string.IsNullOrEmpty(cpfPaci))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o CPF do Paciente o qual será pesquisado.");
                    return;
                }

                var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                           where tb07.NU_CPF_ALU == cpfPaci
                           select new
                           {
                               tb07.CO_ALU,
                               tb07.NO_ALU,
                               tb07.CO_SEXO_ALU,
                               tb07.DT_NASC_ALU,
                               tb07.NU_TELE_CELU_ALU,
                               tb07.NU_TELE_RESI_ALU

                           }).FirstOrDefault();

                if (res != null)
                {
                    txtNomePaciente.Text = res.NO_ALU;
                    ddlSxPac.SelectedValue = res.CO_SEXO_ALU;
                    txtDtNasc.Text = res.DT_NASC_ALU.ToString();
                    txtTelCelUsu.Text = res.NU_TELE_CELU_ALU;
                    txtTelResUsu.Text = res.NU_TELE_CELU_ALU;
                    hidCoPaci.Value = res.CO_ALU.ToString();

                    CalculaIdadeUsu();
                }
            }
            else if(chkPesqNisPaci.Checked == true)
            {
                string nispaci = txtNisPaci.Text.Trim();

                if (string.IsNullOrEmpty(nispaci))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o NIS o qual será pesquisado.");
                    return;
                }
                
                int nisPaciI = int.Parse(nispaci);

                var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                           where tb07.NU_NIS == nisPaciI
                           select new
                           {
                               tb07.CO_ALU,
                               tb07.NO_ALU,
                               tb07.CO_SEXO_ALU,
                               tb07.DT_NASC_ALU,
                               tb07.NU_TELE_CELU_ALU,
                               tb07.NU_TELE_RESI_ALU

                           }).FirstOrDefault();

                if (res != null)
                {
                    txtNomePaciente.Text = res.NO_ALU;
                    ddlSxPac.SelectedValue = res.CO_SEXO_ALU;
                    txtDtNasc.Text = res.DT_NASC_ALU.ToString();
                    txtTelCelUsu.Text = res.NU_TELE_CELU_ALU;
                    txtTelResUsu.Text = res.NU_TELE_CELU_ALU;
                    hidCoPaci.Value = res.CO_ALU.ToString();

                    CalculaIdadeUsu();
                }
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

        protected void imgCPFPaci_OnClick(object sender, EventArgs e)
        {
            carregaPaciCPF();
        }

        protected void chkPesqNisPaci_OnCheckedChanged(object sender, EventArgs e)
        {
            if (chkPesqNisPaci.Checked == true)
            {
                txtNisPaci.Enabled = true;
                chkPesqCPFPaci.Checked = txtCPFPaci.Enabled = false;
                txtCPFPaci.Text = "";

            }
            else
            {
                txtCPFPaci.Enabled = false;
                txtCPFPaci.Text = "";
            }
        }

        protected void chkPesqCPFPaci_OnCheckedChanged(object sender, EventArgs e)
        {
            if (chkPesqCPFPaci.Checked == true)
            {
                txtCPFPaci.Enabled = true;
                chkPesqNisPaci.Checked = txtNisPaci.Enabled = false;
                txtNisPaci.Text = "";
            }
            else
            {
                txtNisPaci.Enabled = false;
                txtNisPaci.Text = "";
            }
        }

        protected void txtCPFResp_OnTextChanged(object sender, EventArgs e)
        {
            hidCoResp.Value = "";
        }

        protected void txtCPFPaci_OnTextChanged(object sender, EventArgs e)
        {
            if (chkPesqCPFPaci.Checked)
                hidCoPaci.Value = "";
        }

        #endregion

        #region Métodos

        public enum EObjetoLogAgenda
        {
            paraAtendimento,
            paraAvaliacao
        }

        /// <summary>
        /// Salva o log de alteração de status de agenda
        /// </summary>
        /// <param name="CO_TIPO_ALTERACAO"></param>
        private void SalvaLogAlteracaoStatusAgenda(int idAgenda, string CO_TIPO_ALTERACAO, bool flagSim, EObjetoLogAgenda etipo)
        {
            //Se for para atendimento
            if (etipo == EObjetoLogAgenda.paraAtendimento)
            {
                #region Para Atendimento

                TBS375_LOG_ALTER_STATUS_AGEND_HORAR tbs375 = new TBS375_LOG_ALTER_STATUS_AGEND_HORAR();

                tbs375.TBS174_AGEND_HORAR = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(idAgenda);
                tbs375.FL_TIPO_LOG = CO_TIPO_ALTERACAO;
                tbs375.CO_COL_CADAS = LoginAuxili.CO_COL;
                tbs375.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                tbs375.DT_CADAS = DateTime.Now;
                tbs375.IP_CADAS = Request.UserHostAddress;
                //Se for triagem, salva no log para diferenciar do encaminhamento para triagem. TA.06/07/2016
                tbs375.DE_OBSER = (CO_TIPO_ALTERACAO == "E" ? "ENCAMINHADO" : (CO_TIPO_ALTERACAO == "T" ? "TRIAGEM" : (CO_TIPO_ALTERACAO == "P" ? "ATENDIMENTO" : null)));
                //Se for de presença, verifica se o parâmetro recebido é como presente ou não
                tbs375.FL_CONFIR_AGEND = (CO_TIPO_ALTERACAO == "P" ? (flagSim ? "S" : "N") : null);
                //Se for de encaminhamento, verifica se o parâmetro recebido é como sim ou não
                tbs375.FL_AGEND_ENCAM = (CO_TIPO_ALTERACAO == "E" ? (flagSim ? "S" : "N") : (CO_TIPO_ALTERACAO == "T" ? (flagSim ? "T" : "N") : null));

                TBS375_LOG_ALTER_STATUS_AGEND_HORAR.SaveOrUpdate(tbs375, true);

                if ((CO_TIPO_ALTERACAO == "P") || (CO_TIPO_ALTERACAO == "E" || CO_TIPO_ALTERACAO == "T"))
                {
                    TBS174_AGEND_HORAR tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(idAgenda);
                    tbs174.FL_CONF_AGEND = (flagSim ? "S" : "N"); //Salva este apenas se a alteração for para presença
                    tbs174.FL_AGEND_ENCAM = (CO_TIPO_ALTERACAO == "E" ? (flagSim ? "S" : "N") : (CO_TIPO_ALTERACAO == "T" ? (flagSim ? "T" : "N") : null)); //Salva este apenas se a alteração for para encaminhamento

                    //Realiza esses processos apenas se a alteração no registro for do tipo de PRESENÇA
                    #region Se for uma alteração da Presença

                    if (CO_TIPO_ALTERACAO == "P")
                    {
                        if (flagSim) //Se for presença SIM, grava as informações pertinentes
                        {
                            tbs174.DT_PRESE = DateTime.Now;
                            tbs174.CO_COL_PRESE = LoginAuxili.CO_COL;
                            tbs174.CO_EMP_COL_PRESE = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
                            tbs174.CO_EMP_PRESE = LoginAuxili.CO_EMP;
                            tbs174.IP_PRESE = Request.UserHostAddress;
                        }
                        else //Se for presença NÃO, grava os campos de presença como NULL
                        {
                            tbs174.DT_PRESE = (DateTime?)null;
                            tbs174.CO_COL_PRESE =
                            tbs174.CO_EMP_COL_PRESE =
                            tbs174.CO_EMP_PRESE = (int?)null;
                            tbs174.IP_PRESE = null;
                        }
                    }

                    #endregion

                    //Realiza esses processos apenas se a alteração no registro for do tipo de ENCAMINHAMENTO
                    #region Se for uma alteração da Encaminhamento

                    if (CO_TIPO_ALTERACAO == "E" || CO_TIPO_ALTERACAO == "T")
                    {
                        if (flagSim) //Se for presença SIM, grava as informações pertinentes
                        {
                            tbs174.DT_ENCAM = DateTime.Now;
                            tbs174.CO_COL_ENCAM = LoginAuxili.CO_COL;
                            tbs174.CO_EMP_COL_ENCAM = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
                            tbs174.CO_EMP_ENCAM = LoginAuxili.CO_EMP;
                            tbs174.IP_ENCAM = Request.UserHostAddress;
                        }
                        else //Se for presença NÃO, grava os campos de presença como NULL
                        {
                            tbs174.DT_ENCAM = (DateTime?)null;
                            tbs174.CO_COL_ENCAM =
                            tbs174.CO_EMP_COL_ENCAM =
                            tbs174.CO_EMP_ENCAM = (int?)null;
                            tbs174.IP_ENCAM = null;
                        }
                    }

                    #endregion

                    TBS174_AGEND_HORAR.SaveOrUpdate(tbs174, true);
                }

                #endregion
            }
            else if (etipo == EObjetoLogAgenda.paraAvaliacao) //Se for para avaliação //TA.06/07/2016
            {
                #region Para Avaliação

                TBS380_LOG_ALTER_STATUS_AGEND_AVALI tbs380 = new TBS380_LOG_ALTER_STATUS_AGEND_AVALI();

                tbs380.TBS372_AGEND_AVALI = TBS372_AGEND_AVALI.RetornaPelaChavePrimaria(idAgenda);
                tbs380.FL_TIPO_LOG = CO_TIPO_ALTERACAO;
                tbs380.CO_COL_CADAS = LoginAuxili.CO_COL;
                tbs380.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                tbs380.DT_CADAS = DateTime.Now;
                tbs380.IP_CADAS = Request.UserHostAddress;
                //Se for de presença, verifica se o parâmetro recebido é como presente ou não
                tbs380.FL_CONFIR_AGEND = (CO_TIPO_ALTERACAO == "P" || CO_TIPO_ALTERACAO == "R" ? (flagSim ? "S" : "N") : null);

                TBS380_LOG_ALTER_STATUS_AGEND_AVALI.SaveOrUpdate(tbs380, true);

                if (CO_TIPO_ALTERACAO == "P" || CO_TIPO_ALTERACAO == "R")
                {
                    TBS372_AGEND_AVALI tbs372 = TBS372_AGEND_AVALI.RetornaPelaChavePrimaria(idAgenda);
                    tbs372.FL_CONFIR_AGEND = (flagSim ? "S" : "N");

                    //Realiza esses processos apenas se a alteração no registro for do tipo de PRESENÇA
                    #region Se for uma alteração da Presença

                    if (flagSim) //Se for presença SIM, grava as informações pertinentes
                    {
                        if (CO_TIPO_ALTERACAO == "P")
                        {
                            tbs372.DT_PRESE = DateTime.Now;
                            tbs372.CO_COL_PRESE = LoginAuxili.CO_COL;
                            tbs372.CO_EMP_COL_PRESE = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
                            tbs372.CO_EMP_PRESE = LoginAuxili.CO_EMP;
                            tbs372.IP_PRESE = Request.UserHostAddress;
                        }
                        else
                        {
                            tbs372.CO_SITUA = "R";
                            tbs372.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                            tbs372.CO_COL_SITUA = LoginAuxili.CO_COL;
                            tbs372.CO_EMP_COL_SITUA = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
                            tbs372.IP_SITUA = Request.UserHostAddress;
                            tbs372.DT_SITUA = DateTime.Now;

                            tbs372.DT_ENCAM = DateTime.Now;
                            tbs372.CO_COL_ENCAM = LoginAuxili.CO_COL;
                            tbs372.CO_EMP_COL_ENCAM = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
                            tbs372.CO_EMP_ENCAM = LoginAuxili.CO_EMP;
                            tbs372.IP_ENCAM = Request.UserHostAddress;
                        }
                    }
                    else //Se for presença NÃO, grava os campos de presença como NULL
                    {
                        if (CO_TIPO_ALTERACAO == "P")
                        {
                            tbs372.DT_PRESE = (DateTime?)null;
                            tbs372.CO_COL_PRESE =
                            tbs372.CO_EMP_COL_PRESE =
                            tbs372.CO_EMP_PRESE = (int?)null;
                            tbs372.IP_PRESE = null;
                        }
                        else
                        {
                            tbs372.CO_SITUA = "A";
                            tbs372.DT_SITUA = DateTime.Now;
                            tbs372.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                            tbs372.CO_COL_SITUA = LoginAuxili.CO_COL;
                            tbs372.CO_EMP_COL_SITUA = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
                            tbs372.IP_SITUA = Request.UserHostAddress;

                            tbs372.DT_ENCAM = (DateTime?)null;
                            tbs372.CO_COL_ENCAM =
                            tbs372.CO_EMP_COL_ENCAM =
                            tbs372.CO_EMP_ENCAM = (int?)null;
                            tbs372.IP_ENCAM = null;
                        }
                    }

                    #endregion

                    TBS372_AGEND_AVALI.SaveOrUpdate(tbs372, true);
                }

                #endregion
            }
        }

        #endregion
    }
}