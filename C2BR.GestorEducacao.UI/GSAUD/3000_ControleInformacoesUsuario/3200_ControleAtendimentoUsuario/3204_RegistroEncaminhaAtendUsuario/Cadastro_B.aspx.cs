//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE DE PONTO DO COLABORADOR
// OBJETIVO: REGISTRO DE PLANTÃO
// DATA DE CRIAÇÃO: 20/05/2014
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//11/06/2014| MAXWELL ALMEIDA            | Criação da página para encaminhamento do Pré-Atendimento para o Atendimento propriamente dito.
//30/12/2014| MAXWELL ALMEIDA            | Inserção de regra para salvar o código do Pré-Atendimento no Direcionamento


using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Data;
using System.ServiceModel;
using System.IO;
using System.Reflection;
using Resources;
using WRAuxiliares = C2BR.GestorEducacao.DLLRelatorioWeb.Auxiliares;
using C2BR.GestorEducacao.DLLRelatorioWeb.Interfaces;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3010_CtrlPedagogicoSeries;
using C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2107_MatriculaAluno;
using System.Data.Objects;
using C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3400_CtrlAtendimentoUsuario;

namespace C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3204_RegistroEncaminhaAtendUsuario
{
    public partial class Cadastro_B : System.Web.UI.Page
    {
        #region Váriaveis

        int qtdLinhasGrid = 0;
        #endregion

        #region Eventos

        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtCPFResp.Text = "00000000000";
                txtDtNascResp.Text = "01/01/1900";
                txtNuIDResp.Text = "000000";
                txtOrgEmiss.Text = "SSP";

                //------------> Tamanho da imagem de Aluno
                upImagemAluno.ImagemLargura = 70;
                upImagemAluno.ImagemAltura = 85;
                upImagemAluno.MostraProcurar = false;

                CarregaConsultasAgendadas();
                carregaGridMedicosPlantonistas();
                CarregaClassificaProfi();

                AuxiliCarregamentos.CarregaUFs(ddlUF, false, LoginAuxili.CO_EMP);
                AuxiliCarregamentos.CarregaUFs(ddlUFOrgEmis, false, LoginAuxili.CO_EMP);
                //AuxiliCarregamentos.CarregaEspeciacialidades(ddlPesqEspec, LoginAuxili.CO_EMP, null, true);
                AuxiliCarregamentos.CarregaDepartamentos(ddlPesqLocal, LoginAuxili.CO_EMP, true);

                IniPeri.Text = DateTime.Now.ToString();
                FimPeri.Text = DateTime.Now.ToString();

                carregaCidade();
                carregaBairro();
                carregaEspec();
                carregaOperadoraPL();
                carregaPlano();
                CarregaClassRisco();
                CarregaDadosUnidLogada();
                VerificarNireAutomatico();
                CarregarFuncoesSimp();
            }

            //ScriptManager.RegisterStartupScript(
            //        UpdatePanel2,
            //        this.GetType(),
            //        "Acao",
            //        "carregaCss();",
            //        true
            //    );
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //    --------> criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
        }

        /// <summary>
        /// Salva as informações nas tabelas cabíveis, TB108, TB07 e TBS195
        /// </summary>
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            SalvaEntidades();
        }

        #endregion

        #region Carregamentos

        /// <summary>
        /// Método responsável por salvar as entidades.
        /// </summary>
        private void SalvaEntidades()
        {
            //Recupera o ID do Pré-Atendimento da grid selecionada.
            int? idAgenda = (HttpContext.Current.Session["VL_Agenda_DMB"] != null ? (int)HttpContext.Current.Session["VL_Agenda_DMB"] : (int?)null);
            int cocolP = (HttpContext.Current.Session["coCol"] != null ? (int)HttpContext.Current.Session["coCol"] : 0);
            int coempP = (HttpContext.Current.Session["coEmp"] != null ? (int)HttpContext.Current.Session["coEmp"] : 0);
            VerificarNireAutomatico();
            bool erros = false;

            //----------------------------------------------------- Valida os campos do Responsável -----------------------------------------------------
            if (txtNomeResp.Text == "")
            //{ AuxiliPagina.EnvioMensagemErro(this.Page, "O Nome do Responsável é Requerido"); erros = true; }
            { AbreMensagemInfos("O Nome do Responsável é Requerido"); return; }

            if (txtCPFResp.Text == "")
            //{ AuxiliPagina.EnvioMensagemErro(this.Page, "O CPF do Responsável é Requerido"); erros = true; }
            { AbreMensagemInfos("O CPF do Responsável é Requerido"); return; }

            if (ddlSexResp.SelectedValue == "")
            //{ AuxiliPagina.EnvioMensagemErro(this.Page, "O Sexo do Responsável é Requerido"); erros = true; }
            { AbreMensagemInfos("O Sexo do Responsável é Requerido"); return; }

            if (txtDtNascResp.Text == "")
            //{ AuxiliPagina.EnvioMensagemErro(this.Page, "A Data de Nascimento do Responsável é Requerida"); erros = true; }
            { AbreMensagemInfos("A Data de Nascimento do Responsável é Requerida"); return; }

            if (txtNuIDResp.Text == "")
            //{ AuxiliPagina.EnvioMensagemErro(this.Page, "O Número da Identidade do Responsável é Requerido"); erros = true; }
            { AbreMensagemInfos("O Número da Identidade do Responsável é Requerido"); return; }

            if (txtCEP.Text == "")
            //{ AuxiliPagina.EnvioMensagemErro(this.Page, "O CEP do Endereço do Responsável é Requerido"); erros = true; }
            { AbreMensagemInfos("O CEP do Endereço do Responsável é Requerido"); return; }

            if (ddlUF.Text == "")
            //{ AuxiliPagina.EnvioMensagemErro(this.Page, "O UF do Endereço do Responsável é Requerida"); erros = true; }
            { AbreMensagemInfos("O UF do Endereço do Responsável é Requerida"); return; }

            if (ddlCidade.Text == "")
            //{ AuxiliPagina.EnvioMensagemErro(this.Page, "A Cidade do Responsável é Requerida"); erros = true; }
            { AbreMensagemInfos("A Cidade do Responsável é Requerida"); return; }

            if (ddlBairro.Text == "")
            //{ AuxiliPagina.EnvioMensagemErro(this.Page, "O Bairro do Responsável é Requerido"); erros = true; }
            { AbreMensagemInfos("O Bairro do Responsável é Requerido"); return; }

            if (txtLograEndResp.Text == "")
            //{ AuxiliPagina.EnvioMensagemErro(this.Page, "O Logradouro do Responsável é Requerido"); erros = true; }
            { AbreMensagemInfos("O Logradouro do Responsável é Requerido"); return; }

            //----------------------------------------------------- Valida os campos do Aluno -----------------------------------------------------
            if (ddlSexoPaci.SelectedValue == "")
            //{ AuxiliPagina.EnvioMensagemErro(this.Page, "O Sexo do Paciente é Requerido"); erros = true; }
            { AbreMensagemInfos("O Sexo do Paciente é Requerido"); return; }

            if (txtDtNascPaci.Text == "")
            //{ AuxiliPagina.EnvioMensagemErro(this.Page, "A Data de Nascimento do Paciente é Requerida"); erros = true; }
            { AbreMensagemInfos("A Data de Nascimento do Paciente é Requerida"); return; }

            /*29/04/2015 - Removida a obrigatoriedade, pois nem todos os pacientes possuem CNES*/
            //if (txtNuNisPaci.Text == "")
            ////{ AuxiliPagina.EnvioMensagemErro(this.Page, "O Número do NIS do Paciente é Requerido"); erros = true; }
            //{ AbreMensagemInfos("O Número do NIS do Paciente é Requerido"); return; }

            if (txtNuProntuario.Text == "")
            //{ AuxiliPagina.EnvioMensagemErro(this.Page, "O Número do PRONTUÁRIO do Paciente é Requerido"); erros = true; }
            { AbreMensagemInfos("O Número do PRONTUÁRIO do Paciente é Requerido"); return; }

            //----------------------------------------------------- Valida os Campos Gerais -----------------------------------------------------
            //if (ddlClassRisco.SelectedValue == "")
            ////{ AuxiliPagina.EnvioMensagemErro(this.Page, "A Classificação de Risco é Requerida"); erros = true; }
            //{ AbreMensagemInfos("A Classificação de Risco é Requerida"); return; }

            //if (ddlEspec.SelectedValue == "")
            ////{ AuxiliPagina.EnvioMensagemErro(this.Page, "A Especialidade é Requerida"); erros = true; }
            //{ AbreMensagemInfos("A Especialidade é Requerida"); return; }

            if (cocolP == 0)
            //{ AuxiliPagina.EnvioMensagemErro(this.Page, "É Preciso selecionar um Médico para realizar o Encaminhamento"); erros = true; }
            { AbreMensagemInfos("É Preciso selecionar um Médico para realizar o Encaminhamento"); return; }

            if (erros != true)
            {
                //Salva os dados do Responsável na tabela 108
                #region Salva Responsável na tb108

                //string nucpf = txtCPFResp.Text.Replace("-", "").Replace(".", "").Trim();
                //bool reExis = (from tb108li in TB108_RESPONSAVEL.RetornaTodosRegistros()
                //               where tb108li.NU_CPF_RESP == nucpf
                //               select tb108li).Any();

                TB108_RESPONSAVEL tb108;
                //Verifica se já não existe um responsável cadastrdado com esse CPF, caso não exista ele cadastra um novo com os dados informados
                //if (reExis == false)
                //{

                string cpfResp = txtCPFResp.Text.Replace(".", "").Replace("-", "").Trim();

                string cpfRespValido = cpfResp;

                if ((cpfResp == "00000000000") || (string.IsNullOrEmpty(cpfResp)))
                {
                    string cpfGerado = "";
                    cpfGerado = GerarNovoCPF(false);

                    //Enquanto existir, calcula um novo cpf
                    while (TB108_RESPONSAVEL.RetornaTodosRegistros().Where(w => w.NU_CPF_RESP == cpfGerado).Any())
                    {
                        cpfGerado = GerarNovoCPF(false);
                    }

                    cpfRespValido = cpfGerado;
                }

                if (string.IsNullOrEmpty(hidCoResp.Value))
                {
                    tb108 = new TB108_RESPONSAVEL();

                    tb108.CO_CEP_RESP = txtCEP.Text;
                    tb108.CO_ESTA_RESP = ddlUF.SelectedValue;
                    tb108.CO_BAIRRO = int.Parse(ddlBairro.SelectedValue);
                    tb108.CO_CIDADE = int.Parse(ddlCidade.SelectedValue);
                    tb108.DE_ENDE_RESP = txtLograEndResp.Text;

                    tb108.TBS366_FUNCAO_SIMPL = (!string.IsNullOrEmpty(ddlFuncao.SelectedValue) ? TBS366_FUNCAO_SIMPL.RetornaPelaChavePrimaria(int.Parse(ddlFuncao.SelectedValue)) : null);
                    tb108.NO_RESP = txtNomeResp.Text;
                    tb108.NU_CPF_RESP = cpfRespValido;
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
                }
                else
                {
                    //Busca em um campo na página, que é preenchido quando se pesquisa um responsável, o CO_RESP, usado pra instanciar um objeto da entidade do responsável em questão.
                    if (string.IsNullOrEmpty(hidCoResp.Value))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar um Responsável para dar continuidade no encaminhamento.");
                        return;
                    }

                    int coRe = int.Parse(hidCoResp.Value);
                    tb108 = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(coRe);
                }

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
                if (string.IsNullOrEmpty(hidCoPac.Value))
                {
                    tb07 = new TB07_ALUNO();

                    #region Bloco foto
                    int codImagem = upImagemAluno.GravaImagem();
                    tb07.Image = C2BR.GestorEducacao.BusinessEntities.MSSQL.Image.RetornaPelaChavePrimaria(codImagem);
                    #endregion

                    tb07.CO_ORIGEM_ALU = ddlOrigemPaci.SelectedValue;
                    tb07.NO_ALU = txtnompac.Text;
                    tb07.NU_NIRE = int.Parse(txtNuProntuario.Text);
                    tb07.NU_CPF_ALU = txtCpfPaci.Text.Replace(".", "").Replace("-", "").Trim();
                    tb07.NU_NIS = (!string.IsNullOrEmpty(txtNuNisPaci.Text) ? decimal.Parse(txtNuNisPaci.Text) : (decimal?)null);
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
                    tb07.TP_RACA = ddlEtniaAlu.SelectedValue != "" ? ddlEtniaAlu.SelectedValue : null;

                    //Endereço
                        tb07.CO_CEP_ALU = txtCEP.Text;
                        tb07.CO_ESTA_ALU = ddlUF.SelectedValue;
                        tb07.TB905_BAIRRO = TB905_BAIRRO.RetornaPelaChavePrimaria(int.Parse(ddlBairro.SelectedValue));
                        tb07.DE_ENDE_ALU = txtLograEndResp.Text;

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
                }
                else
                {
                    //if (string.IsNullOrEmpty(hidCoPac.Value))
                    //{
                    //    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar um Paciente para dar continuidade no encaminhamento.");
                    //    return;
                    //}

                    //Busca em um campo na página, que é preenchido quando se pesquisa um Paciente, o CO_ALU, usado pra instanciar um objeto da entidade do Paciente em questão.
                    int coPac = int.Parse(hidCoPac.Value);
                    tb07 = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.CO_ALU == coPac).FirstOrDefault();
                }

                #endregion

                //Os dados do Encaminhamento na tabela tbs195
                #region Salva na tbs195

                TBS195_ENCAM_MEDIC tbs195 = new TBS195_ENCAM_MEDIC();

                //Trata para gerar um Código do Encaminhamento
                var res2 = (from tbs195pesq in TBS195_ENCAM_MEDIC.RetornaTodosRegistros()
                            select new { tbs195pesq.CO_ENCAM_MEDIC }).OrderByDescending(w => w.CO_ENCAM_MEDIC).FirstOrDefault();

                string seq;
                int seq2;
                int seqConcat;
                string seqcon;
                string ano = DateTime.Now.Year.ToString().Substring(2, 2);
                string coUnid = LoginAuxili.CO_UNID.ToString();
                if (res2 == null)
                {
                    seq2 = 1;
                }
                else
                {
                    seq = res2.CO_ENCAM_MEDIC.Substring(7, 7);
                    seq2 = int.Parse(seq);
                }

                seqConcat = seq2 + 1;
                seqcon = seqConcat.ToString().PadLeft(7, '0');

                tbs195.CO_ENCAM_MEDIC = ano + coUnid.PadLeft(3, '0') + "EM" + seqcon;
                tbs195.DT_ENCAM_MEDIC = DateTime.Now;
                tbs195.CO_EMP_ENCAM_MEDIC = LoginAuxili.CO_EMP;
                tbs195.CO_DEPTO_ENCAM_MEDIC = 11;
                tbs195.CO_COL = cocolP;
                tbs195.CO_EMP_COL = coempP;
                //tbs195.ID_PRE_ATEND = (coPreAt != 0 ? coPreAt : (int?)null);
                //tbs195.CO_PRE_ATEND = (coPreAt != 0 ? TBS194_PRE_ATEND.RetornaPelaChavePrimaria(coPreAt.Value).CO_PRE_ATEND : null);
                tbs195.TBS174_AGEND_HORAR = (idAgenda.HasValue && idAgenda != 0 ? TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(idAgenda.Value) : null);
                tbs195.CO_ALU = tb07.CO_ALU;
                tbs195.CO_RESP = tb108.CO_RESP;
                tbs195.TP_RESP = (chkPaciEhResp.Checked ? "P" : "R");
                tbs195.CO_ESPEC = (!string.IsNullOrEmpty(ddlEspec.SelectedValue) ? int.Parse(ddlEspec.SelectedValue) : 0);
                tbs195.NR_CLASS_RISCO = 0;
                tbs195.ID_OPER = (ddlOperPlano.SelectedValue != "" ? int.Parse(ddlOperPlano.SelectedValue) : (int?)null);
                tbs195.ID_PLAN = (ddlPlano.SelectedValue != "" ? int.Parse(ddlPlano.SelectedValue) : (int?)null);
                tbs195.DT_VENC_CART_PLANO = (!string.IsNullOrEmpty(txtDtVenciPlan.Text) ? txtDtVenciPlan.Text : null);
                tbs195.NU_CART_PLANO = (txtNumeroCartPla.Text != "" ? int.Parse(txtNumeroCartPla.Text) : (int?)null);
                tbs195.FL_PRE_ATEND = (chkEncaComPreAtend.Checked ? "S" : "N");
                tbs195.DT_ENCAM_MEDIC = DateTime.Now;
                tbs195.CO_COL_REAL_ENCAM = LoginAuxili.CO_COL;
                tbs195.CO_EMP_REAL_ENCAM = LoginAuxili.CO_EMP;
                tbs195.NR_IP_REAL_ENCAM = Request.UserHostAddress;
                tbs195.CO_SITUA_ENCAM_MEDIC = "A";
                tbs195.DT_SITUA_ENCAM_MEDIC = DateTime.Now;
                tbs195.DT_CADAS_ENCAM = DateTime.Now;
                tbs195.FL_RESP_FINAN = (chkRespFinanc.Checked ? "S" : "N");
                tbs195.FL_TIPO_FLUXO = "B";

                TBS195_ENCAM_MEDIC.SaveOrUpdate(tbs195);

                hdIdEncam.Value = tbs195.ID_ENCAM_MEDIC.ToString();

                //Limpa as Sessions usadas para guardar informações
                HttpContext.Current.Session.Remove("VL_PreAtend");
                HttpContext.Current.Session.Remove("coCol");
                HttpContext.Current.Session.Remove("coEmp");
                HttpContext.Current.Session.Remove("FL_Select_Grid");
                HttpContext.Current.Session.Remove("FL_Select_Grid_MEDIC");
                HttpContext.Current.Session.Remove("VL_MEDIC");

                //Altera a situação do Atendimento para Encaminhado, para que dessa forma, não apareça na Grid de Pré-Atendimentos.
                if (idAgenda.HasValue)
                {
                    TBS174_AGEND_HORAR t4 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(idAgenda.Value);
                    if (t4 != null)
                    {
                        t4.CO_SITUA_AGEND_HORAR = "E";
                        t4.FL_CONF_AGEND = "S";
                        t4.DT_SITUA_AGEND_HORAR = DateTime.Now;
                        TBS174_AGEND_HORAR.SaveOrUpdate(t4);
                    }
                }
                #endregion
                AbreMensagemInfos("Direcionamento do Atendimento Realizado com sucesso!");
                //AuxiliPagina.RedirecionaParaPaginaSucesso("Encaminhamento do Atendimento Realizado com sucesso.", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
            }
        }

        /// <summary>
        /// Carrega a Grid de Registros de Consultas em aberto para o dia
        /// </summary>
        private void CarregaConsultasAgendadas()
        {
            //DateTime dtIni = DateTime.Now.AddDays(-1);
            //DateTime dtAtual = DateTime.Now;
            DateTime dtIni = (!string.IsNullOrEmpty(IniPeri.Text) ? DateTime.Parse(IniPeri.Text) : DateTime.Now);
            DateTime dtFim = (!string.IsNullOrEmpty(FimPeri.Text) ? DateTime.Parse(FimPeri.Text) : DateTime.Now);

            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                       where tbs174.CO_SITUA_AGEND_HORAR == "A"
                       && (tbs174.CO_EMP == LoginAuxili.CO_EMP)
                       && ((EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) >= EntityFunctions.TruncateTime(dtIni))
                       && (EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) <= EntityFunctions.TruncateTime(dtFim)))
                       select new Consultas
                       {
                           NO_COL = tb03.NO_COL,
                           CO_ALU = tb07.CO_ALU,
                           CO_COL = tbs174.CO_COL,
                           CO_AGEND_MEDIC = tbs174.ID_AGEND_HORAR,
                           CO_SEXO = tb07.CO_SEXO_ALU,
                           dt_nascimento = tb07.DT_NASC_ALU,
                           NO_PAC_RECEB = tb07.NO_ALU,
                           dt_Consul = tbs174.DT_AGEND_HORAR,
                           hr_Consul = tbs174.HR_AGEND_HORAR,
                           CO_SITU = tbs174.CO_SITUA_AGEND_HORAR,
                           FL_CONF = tbs174.FL_CONF_AGEND,
                           TP_CONSUL = tbs174.TP_CONSU,
                           CO_CLASS_PROFI = tb03.CO_CLASS_PROFI,
                           CO_ESPEC = tbs174.CO_ESPEC.HasValue ? tbs174.CO_ESPEC.Value : 0,

                       }).OrderByDescending(w => w.dt_Consul).ThenBy(y => y.hr_Consul).ToList();

            //Faz uma lista com as 10 últimas Consultas e insere na lista anterior
            #region 10 últimos acolhimentos
            var resAntigos = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                              join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                              join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                              join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tbs174.CO_ESPEC equals tb63.CO_ESPECIALIDADE
                              where tbs174.CO_SITUA_AGEND_HORAR == "A"
                              && (tbs174.CO_EMP == LoginAuxili.CO_EMP)
                                  //Garante que vai pegar apenas aqueles fora do range das datas informadas
                              && (EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) < EntityFunctions.TruncateTime(dtIni))
                              select new Consultas
                              {
                                  NO_COL = tb03.NO_COL,
                                  CO_ALU = tb07.CO_ALU,
                                  CO_COL = tbs174.CO_COL,
                                  CO_AGEND_MEDIC = tbs174.ID_AGEND_HORAR,
                                  CO_SEXO = tb07.CO_SEXO_ALU,
                                  dt_nascimento = tb07.DT_NASC_ALU,
                                  NO_PAC_RECEB = tb07.NO_ALU,
                                  dt_Consul = tbs174.DT_AGEND_HORAR,
                                  hr_Consul = tbs174.HR_AGEND_HORAR,
                                  CO_SITU = tbs174.CO_SITUA_AGEND_HORAR,
                                  FL_CONF = tbs174.FL_CONF_AGEND,
                                  TP_CONSUL = tbs174.TP_CONSU,
                                  CO_CLASS_PROFI = tb03.CO_CLASS_PROFI,
                                  CO_ESPEC = tbs174.CO_ESPEC.Value,
                                  ANTIGOS = 1,

                              }).OrderByDescending(w => w.dt_Consul).ToList();
            //}).OrderByDescending(w => w.dt_Consul).ThenBy(y => y.hr_Consul).Take(10).ToList();
            foreach (var i in resAntigos) // passa por cada item da segunda lista e insere na primeira
            {
                //res.Add(i);
            }
            #endregion

            //Reordena os itens
            res = res.OrderByDescending(w => w.dt_Consul).ThenBy(y => y.hr_Consul).ToList();

            grdAgendaPlantoes.DataSource = res;
            grdAgendaPlantoes.DataBind();

        }

        /// <summary>
        /// Método responsável por carregar os médicos plantonistas na grid correspondente
        /// </summary>
        /// <param name="CO_ESPEC"></param>
        private void carregaGridMedicosPlantonistas(string CLASS_PROFI = "0", int CO_DEPTO = 0)
        {
            DateTime dtAtu = DateTime.Now;

            //var res = (from tb159 in TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
            //           join tb153 in TB153_TIPO_PLANT.RetornaTodosRegistros() on tb159.ID_TIPO_PLANT equals tb153.ID_TIPO_PLANT
            //           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tb159.TB03_COLABOR.CO_COL equals tb03.CO_COL
            //           //where tb159.DT_INICIO_PREV >= dtAtu && tb159.DT_TERMIN_PREV <= dtAtu
            //           where ((dtAtu >= tb159.DT_INICIO_PREV) && (dtAtu <= tb159.DT_TERMIN_PREV))
            //           && (CLASS_PROFI != "0" ? tb03.CO_CLASS_PROFI == CLASS_PROFI : 0 == 0)
            //           && (CO_DEPTO != 0 ? tb153.TB14_DEPTO.CO_DEPTO == CO_DEPTO : 0 == 0)
            //           && tb159.CO_EMP_AGEND_PLANT == LoginAuxili.CO_EMP

            //Coleta quais são as classificações para as quais é possível efetuar direcionamentos parametrizadas na unidade
            var dadosEmp = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                            join tb83 in TB83_PARAMETRO.RetornaTodosRegistros() on tb25.CO_EMP equals tb83.CO_EMP
                            where tb25.CO_EMP == LoginAuxili.CO_EMP
                            select new
                            {
                                tb83.FL_PERM_DIREC_ENFER,
                                tb83.FL_PERM_DIREC_FISIO,
                                tb83.FL_PERM_DIREC_FONOA,
                                tb83.FL_PERM_DIREC_MEDIC,
                                tb83.FL_PERM_DIREC_ODONT,
                                tb83.FL_PERM_DIREC_ESTET,
                                tb83.FL_PERM_DIREC_NUTRI,
                                tb83.FL_PERM_DIREC_OUTRO,
                                tb83.FL_PERM_DIREC_PSICO,
                            }).FirstOrDefault();

            var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                       join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tb03.CO_DEPTO equals tb14.CO_DEPTO into l1
                       from ls in l1.DefaultIfEmpty()
                       where tb03.FLA_PROFESSOR == "S"
                       && tb03.CO_SITU_COL == "ATI"
                       //&& (dadosEmp.FL_PERM_DIREC_ENFER == "S" ? tb03.CO_CLASS_PROFI == "E" : 0 == 0)
                       //&& (dadosEmp.FL_PERM_DIREC_FISIO == "S" ? tb03.CO_CLASS_PROFI == "I" : 0 == 0)
                       //&& (dadosEmp.FL_PERM_DIREC_FONOA == "S" ? tb03.CO_CLASS_PROFI == "N" : 0 == 0)
                       //&& (dadosEmp.FL_PERM_DIREC_MEDIC == "S" ? tb03.CO_CLASS_PROFI == "M" : 0 == 0)
                       //&& (dadosEmp.FL_PERM_DIREC_ODONT == "S" ? tb03.CO_CLASS_PROFI == "D" : 0 == 0)
                       //&& (dadosEmp.FL_PERM_DIREC_OFTAL == "S" ? tb03.CO_CLASS_PROFI == "F" : 0 == 0)
                       //&& (dadosEmp.FL_PERM_DIREC_OUTRO == "S" ? tb03.CO_CLASS_PROFI == "O" : 0 == 0)
                       //&& (dadosEmp.FL_PERM_DIREC_PSICO == "S" ? tb03.CO_CLASS_PROFI == "P" : 0 == 0)
                       select new MedicosPlantonistas
                       {
                           NO_COL = tb03.NO_COL,
                           co_col = tb03.CO_COL,
                           co_emp_col_pla = tb03.CO_EMP,
                           CO_CLASS_PROFI = tb03.CO_CLASS_PROFI,
                           LOCAL = ls.NO_DEPTO,
                           CO_ESPEC = tb03.CO_ESPEC,
                       }).OrderBy(w => w.NO_COL).ToList();

            var lst = new List<MedicosPlantonistas>();

            if (dadosEmp != null)
            {
                #region Verifica os itens a serem excluídos
                if (res.Count > 0)
                {
                    int aux = 0;
                    foreach (var i in res)
                    {
                        switch (i.CO_CLASS_PROFI)
                        {
                            case "M":
                                if (dadosEmp.FL_PERM_DIREC_MEDIC != "S")
                                { lst.Add(i); }
                                break;
                            case "E":
                                if (dadosEmp.FL_PERM_DIREC_ENFER != "S")
                                { lst.Add(i); }
                                break;
                            case "I":
                                if (dadosEmp.FL_PERM_DIREC_FISIO != "S")
                                { lst.Add(i); }
                                break;
                            case "F":
                                if (dadosEmp.FL_PERM_DIREC_FONOA != "S")
                                { lst.Add(i); }
                                break;
                            case "D":
                                if (dadosEmp.FL_PERM_DIREC_ODONT != "S")
                                { lst.Add(i); }
                                break;
                            case "S":
                                if (dadosEmp.FL_PERM_DIREC_ESTET != "S")
                                { lst.Add(i); }
                                break;
                            case "N":
                                if (dadosEmp.FL_PERM_DIREC_NUTRI != "S")
                                { lst.Add(i); }
                                break;
                            case "O":
                                if (dadosEmp.FL_PERM_DIREC_OUTRO != "S")
                                { lst.Add(i); }
                                break;
                            case "P":
                                if (dadosEmp.FL_PERM_DIREC_PSICO != "S")
                                { lst.Add(i); }
                                break;
                            default:
                                { lst.Add(i); }
                                break;

                        }
                        aux++;
                    }
                }
                #endregion
            }

            res = res.Except(lst).ToList();

            if (res.Count > 0)
            {
                grdMedicosPlanto.DataSource = res;
                grdMedicosPlanto.DataBind();
            }
            else
            {
                grdMedicosPlanto.DataSource = null;
                grdMedicosPlanto.DataBind();
            }
        }

        /// <summary>
        /// Carrega as funções simplificadas
        /// </summary>
        private void CarregarFuncoesSimp()
        {
            AuxiliCarregamentos.CarregaFuncoesSimplificadas(ddlFuncao, false);
        }

        #region Classes de Saída

        /// <summary>
        /// Preenche a Grid de Consultas com os registros previamente cadastrados na funcionalidade e processo de registro de consulta.
        /// </summary>
        public class Consultas
        {
            public int ANTIGOS { get; set; }

            public string NO_COL { get; set; }
            public int CO_ALU { get; set; }
            public string NO_PAC
            {
                get
                {
                    return (this.NO_PAC_RECEB.Length > 25 ? this.NO_PAC_RECEB.Substring(0, 25) + "..." : this.NO_PAC_RECEB);
                }
            }
            public string NO_PAC_RECEB { get; set; }
            public DateTime? dt_nascimento { get; set; }
            public string NU_IDADE
            {
                get
                {
                    string idade = " - ";

                    //Calcula a idade do Paciente de acordo com a data de nascimento do mesmo.
                    if (this.dt_nascimento.HasValue)
                    {
                        int anos = DateTime.Now.Year - dt_nascimento.Value.Year;

                        if (DateTime.Now.Month < dt_nascimento.Value.Month || (DateTime.Now.Month == dt_nascimento.Value.Month && DateTime.Now.Day < dt_nascimento.Value.Day))
                            anos--;

                        idade = anos.ToString();
                    }
                    return idade;
                }
            }
            public string CO_SEXO { get; set; }

            public int? CO_COL { get; set; }
            public int CO_AGEND_MEDIC { get; set; }

            //Carrega informações gerais do agendamento
            public DateTime dt_Consul { get; set; }
            public string hr_Consul { get; set; }
            public string hora
            {
                get
                {
                    return this.dt_Consul.ToString("dd/MM/yy") + " - " + this.hr_Consul;
                }
            }
            public string CO_CLASS_PROFI { get; set; }
            public string NO_CLASS_PROFI
            {
                get
                {
                    return AuxiliGeral.GetNomeClassificacaoFuncional(CO_CLASS_PROFI, false);
                }
            }
            public int CO_ESPEC { get; set; }

            public string FL_CONF { get; set; }
            public bool FL_CONF_VALID
            {
                get
                {
                    return this.FL_CONF == "S" ? true : false;
                }
            }
            public string CO_SITU { get; set; }
            public string CO_SITU_VALID
            {
                get
                {
                    string situacao = "";
                    switch (this.CO_SITU)
                    {
                        case "A":
                            situacao = "Aberto";
                            break;
                        case "C":
                            situacao = "Cancelado";
                            break;
                        case "I":
                            situacao = "Inativo";
                            break;
                        case "S":
                            situacao = "Suspenso";
                            break;
                    }

                    return situacao;
                }
            }
            public string TP_CONSUL { get; set; }
            public string TP_CONSUL_VALID
            {
                get
                {
                    string tipo = "";
                    switch (this.TP_CONSUL)
                    {
                        case "N":
                            tipo = "Normal";
                            break;
                        case "R":
                            tipo = "Retorno";
                            break;
                        case "U":
                            tipo = "Urgência";
                            break;
                        default:
                            tipo = " - ";
                            break;
                    }
                    return tipo;
                }
            }
        }

        /// <summary>
        /// Classe de saída para a Grid de Médicos
        /// </summary>
        public class MedicosPlantonistas
        {
            public string NO_COL { get; set; }
            public int co_col { get; set; }
            public int co_emp_col_pla { get; set; }
            public string CO_CLASS_PROFI { get; set; }
            public string NO_CLASS_PROFI
            {
                get
                {
                    return AuxiliGeral.GetNomeClassificacaoFuncional(CO_CLASS_PROFI, false);
                }
            }
            public int? CO_ESPEC { get; set; }
            public string CO_TIPO_RISCO { get; set; }
            public string LOCAL { get; set; }
        }

        #endregion

        /// <summary>
        /// Carrega as classificações profissionais
        /// </summary>
        private void CarregaClassificaProfi()
        {
            AuxiliCarregamentos.CarregaClassificacoesFuncionais(ddlPesqClassProfi, true);
        }

        /// <summary>
        /// Carrega as Cidades relacionadas ao Bairro selecionado anteriormente.
        /// </summary>
        private void carregaCidade()
        {
            string uf = ddlUF.SelectedValue;
            AuxiliCarregamentos.CarregaCidades(ddlCidade, false, uf, LoginAuxili.CO_EMP, true, true);
        }

        /// <summary>
        /// Carrega os Bairros ligados à UF e Cidade selecionados anteriormente.
        /// </summary>
        private void carregaBairro()
        {
            string uf = ddlUF.SelectedValue;
            int cid = ddlCidade.SelectedValue != "" ? int.Parse(ddlCidade.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaBairros(ddlBairro, uf, cid, false, true, false, LoginAuxili.CO_EMP, true);
        }

        /// <summary>
        /// Carrega as informações da unidade logada em campos definidos
        /// </summary>
        private void CarregaDadosUnidLogada()
        {
            var res = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
            res.TB83_PARAMETROReference.Load();

            //Verifica se existe integração com o financeiro
            if (res.TB83_PARAMETRO != null)
                chkRespFinanc.Visible = res.TB83_PARAMETRO.FL_INTEG_FINAN == "S" ? true : false;

            txtCEP.Text = txtCEP_PADRAO.Text = res.CO_CEP_EMP;
            txtLograEndResp.Text = txtLograEndResp_PADRAO.Text = res.DE_END_EMP;
        }

        /// <summary>
        /// Carrega as Especialidades encontradas na tabela de Especialidades, tb63
        /// </summary>
        private void carregaEspec()
        {
            AuxiliCarregamentos.CarregaEspeciacialidades(ddlEspec, LoginAuxili.CO_EMP, null, false);
        }

        /// <summary>
        /// Carrega as Operadoras de Planos de Saúde, por exemplo, Amil, Bradesco, Sulamerica, etc.
        /// </summary>
        private void carregaOperadoraPL()
        {
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddlPlano, false, false);
            ddlOperPlano.Items.Insert(0, new ListItem("Nenhum", ""));
        }

        /// <summary>
        /// Carrega os Planos de saúde relacionados à Operadora selecionada no campo anterior, por exemplo, GoldenCross, Amil20, etc.
        /// </summary>
        private void carregaPlano()
        {
            AuxiliCarregamentos.CarregaPlanosSaude(ddlPlano, ddlOperPlano.SelectedValue, false, false);
            ddlPlano.Items.Insert(0, new ListItem("Nenhum", ""));
        }

        /// <summary>
        /// Gera um CPF válido
        /// </summary>
        /// <param name="ComPontos"></param>
        /// <returns></returns>
        private string GerarNovoCPF(bool ComPontos)
        {
            int soma = 0, resto = 0;
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            Random rnd = new Random();
            string semente = rnd.Next(100000000, 999999999).ToString();

            for (int i = 0; i < 9; i++)
                soma += int.Parse(semente[i].ToString()) * multiplicador1[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            semente = semente + resto;

            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(semente[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            semente = semente + resto;

            if (ComPontos)
                semente = string.Format("{0}.{1}.{2}-{3}", semente.Substring(0, 3), semente.Substring(3, 3), semente.Substring(6, 3), semente.Substring(9, 2));

            return semente;
        }

        /// <summary>
        /// Método responsável por receber os valores por parâmetros e inserir nos campos correspondentes
        /// </summary>
        /// <param name="cpf"></param>
        /// <param name="Nome"></param>
        /// <param name="sexo"></param>
        /// <param name="dtNasc"></param>
        /// <param name="RG"></param>
        /// <param name="ORGrg"></param>
        /// <param name="UFrg"></param>
        /// <param name="TelFixo"></param>
        /// <param name="TelCelu"></param>
        /// <param name="TelCome"></param>
        /// <param name="Whats"></param>
        /// <param name="Face"></param>
        /// <param name="CEP"></param>
        /// <param name="UF"></param>
        /// <param name="Cidade"></param>
        /// <param name="Bairro"></param>
        /// <param name="Logradouro"></param>
        /// <param name="Email"></param>
        private void CarregarDadosResponsavel(string cpf, string Nome, string sexo, DateTime dtNasc, string RG,
            string ORGrg, string UFrg, string TelFixo, string TelCelu, string TelCome, string Whats, string Face,
            string CEP, string UF, int Cidade, int? Bairro, string Logradouro, string Email)
        {
            txtCPFResp.Text = cpf;
            txtNomeResp.Text = Nome;
            ddlSexResp.SelectedValue = sexo;
            txtDtNascResp.Text = dtNasc.ToString();
            txtNuIDResp.Text = RG;
            txtOrgEmiss.Text = ORGrg;
            ddlUFOrgEmis.SelectedValue = UFrg;
            txtTelFixResp.Text = TelFixo;
            txtTelCelResp.Text = TelCelu;
            txtTelComResp.Text = TelCome;
            txtNuWhatsResp.Text = Whats;
            txtCEP.Text = CEP;
            ddlUF.SelectedValue = UF;
            carregaCidade();
            ddlCidade.SelectedValue = (Cidade != 0 ? Cidade.ToString() : "");
            carregaBairro();
            ddlBairro.SelectedValue = (Bairro != 0 && Cidade != 0 ? Bairro.ToString() : "");
            txtLograEndResp.Text = Logradouro;
            txtEmailResp.Text = Email;
        }

        /// <summary>
        /// Carrega as Informações de Responsável e Paciente, de acordo com o registro que é clicado na Grid de Pré-Atendimentos.
        /// </summary>
        /// <param name="ID_PRE_ATEND"></param>
        private void CarregaCampos(int ID_AGENDA)
        {
            var tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(ID_AGENDA);
            var tb07 = TB07_ALUNO.RetornaPeloCoAlu(tbs174.CO_ALU.Value);
            tb07.TB108_RESPONSAVELReference.Load();
            var tb108 = (tb07.TB108_RESPONSAVEL != null ? tb07.TB108_RESPONSAVEL : null);

            hidCoPac.Value = tbs174.CO_ALU.ToString();

            //Carrega essas informações apenas se o paciente da consulta tiver um responsável associado 
            if (tb108 != null)
            {
                tb108.TB904_CIDADEReference.Load();
                tb108.TB904_CIDADE1Reference.Load();

                txtCPFResp.Text = tb108.NU_CPF_RESP;
                txtNomeResp.Text = tb108.NO_RESP;
                txtDtNascResp.Text = tb108.DT_NASC_RESP.ToString();
                ddlSexResp.SelectedValue = tb108.CO_SEXO_RESP;
                txtTelFixResp.Text = tb108.NU_TELE_RESI_RESP;
                txtTelCelResp.Text = tb108.NU_TELE_CELU_RESP;

                CarregarDadosResponsavel(tb108.NU_CPF_RESP, tb108.NO_RESP, tb108.CO_SEXO_RESP, tb108.DT_NASC_RESP.Value, tb108.CO_RG_RESP
                           , tb108.CO_ORG_RG_RESP, tb108.CO_ESTA_RG_RESP, tb108.NU_TELE_RESI_RESP, tb108.NU_TELE_CELU_RESP,
                           tb108.NU_TELE_COME_RESP, tb108.NU_TELE_WHATS_RESP, tb108.NM_FACEBOOK_RESP, tb108.CO_CEP_RESP,
                           tb108.CO_ESTA_RESP, (tb108.CO_CIDADE.HasValue ? tb108.CO_CIDADE.Value : 0), tb108.CO_BAIRRO, tb108.DE_ENDE_RESP, "");
            }
            else
            {
            }

            //Carrega as informações do Paciente
            txtnompac.Text = tb07.NO_ALU;
            txtCpfPaci.Text = tb07.NU_CPF_ALU;
            txtNuNisPaci.Text = tb07.NU_NIS.ToString().PadLeft(7, '0');
            txtTelResPaci.Text = tb07.NU_TELE_RESI_ALU;
            txtTelCelPaci.Text = tb07.NU_TELE_CELU_ALU;
            //chkPesqCPFUsu.Checked = true;
            txtDtNascPaci.Text = tb07.DT_NASC_ALU.ToString();
            ddlSexoPaci.SelectedValue = tb07.CO_SEXO_ALU;
            ddlGrParen.SelectedValue = tb07.CO_GRAU_PAREN_RESP;

            upImagemAluno.ImagemLargura = 70;
            upImagemAluno.ImagemAltura = 85;
            upImagemAluno.MostraProcurar = false;

            tb07.TB250_OPERAReference.Load();
            if (tb07.TB250_OPERA != null)
            {
                if (ddlOperPlano.Items.Contains(new ListItem("", tb07.TB250_OPERA.ID_OPER.ToString())))
                    ddlOperPlano.SelectedValue = tb07.TB250_OPERA.ID_OPER.ToString();

                updInfoPlaSaude.Update();
            }
            else
                ddlOperPlano.SelectedValue = "";

            #region Instancia objeto da entidade para mostrar a foto correspondente

            string cpfPac = txtCpfPaci.Text.Replace("-", "").Replace(".", "").Trim();
            var realu = (from tb07li in TB07_ALUNO.RetornaTodosRegistros()
                         where tb07li.NU_CPF_ALU == cpfPac
                         select new { tb07li.CO_ALU }).FirstOrDefault();

            int? paExis = (realu != null ? realu.CO_ALU : (int?)null);

            Decimal nis = 0;
            if (!string.IsNullOrEmpty(txtNuNisPaci.Text))
            {
                nis = decimal.Parse(txtNuNisPaci.Text.Trim());
            }

            var realu2 = (from tb07ob in TB07_ALUNO.RetornaTodosRegistros()
                          where tb07ob.NU_NIS == nis
                          select new { tb07ob.CO_ALU }).FirstOrDefault();

            int? paExisNis = (realu2 != null ? realu2.CO_ALU : (int?)null);

            if ((!paExis.HasValue) && (!paExisNis.HasValue))
                upImagemAluno.CarregaImagem(0);
            else
            {
                int coPac = (paExis.HasValue ? paExis.Value : paExisNis.Value);
                var resupac = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.CO_ALU == coPac).FirstOrDefault();
            }

            #endregion

            //PesquisaCarregaResp(tbs194.CO_RESP);

            UpdatePanel2.Update();
        }

        /// <summary>
        /// Método que duplica as informações do responsável nos campos do paciente, quando o usuário clica em Paciente é o Responsável.
        /// </summary>
        private void carregaPaciehoResponsavel()
        {
            if (chkPaciEhResp.Checked)
            {
                //Se tiver cpf no paciente, carrega no responsável, se não tiver, deixa os 000...
                txtCPFResp.Text = (!string.IsNullOrEmpty(txtCpfPaci.Text) ? txtCpfPaci.Text : txtCPFResp.Text);
                txtNomeResp.Text = txtnompac.Text;
                txtDtNascResp.Text = txtDtNascPaci.Text;
                ddlSexResp.SelectedValue = ddlSexoPaci.SelectedValue;
                txtTelCelResp.Text = txtTelCelPaci.Text;
                txtTelFixResp.Text = txtTelResPaci.Text;
                ddlGrParen.SelectedValue = "OU";
                txtEmailResp.Text = txtEmailPaci.Text;
                txtNuWhatsResp.Text = txtWhatsPaci.Text;

                //txtEmailPaci.Enabled = false;
                //txtCPFMOD.Enabled = false;
                //txtnompac.Enabled = false;
                //txtDtNascPaci.Enabled = false;
                //ddlSexoPaci.Enabled = false;
                //txtTelCelPaci.Enabled = false;
                //txtTelCelPaci.Enabled = false;
                //txtTelResPaci.Enabled = false;
                //ddlGrParen.Enabled = false;
                //txtWhatsPaci.Enabled = false;

                #region Verifica se já existe

                string cpf = txtCpfPaci.Text.Replace(".", "").Replace("-", "").Trim();

                var tb07 = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.NU_CPF_ALU == cpf).FirstOrDefault();

                //if (tb07 != null)
                //    hidCoPac.Value = tb07.CO_ALU.ToString();

                #endregion

            }
            else
            {
                txtCPFResp.Text = "000.000.000-00";
                txtNomeResp.Text = "";
                txtDtNascResp.Text = "01/01/1900";
                ddlSexResp.SelectedValue = "";
                txtTelCelResp.Text = "";
                txtTelFixResp.Text = "";
                txtEmailResp.Text = "";
                txtNuWhatsResp.Text = "";

                //txtCPFMOD.Enabled = true;
                //txtnompac.Enabled = true;
                //txtDtNascPaci.Enabled = true;
                //ddlSexoPaci.Enabled = true;
                //txtTelCelPaci.Enabled = true;
                //txtTelCelPaci.Enabled = true;
                //txtTelResPaci.Enabled = true;
                //ddlGrParen.Enabled = true;
                //txtEmailPaci.Enabled = true;
                //txtWhatsPaci.Enabled = true;
                //hidCoPac.Value = "";
            }
            //updCadasUsuario.Update();
        }

        /// <summary>
        /// Carrega as opções padrões de Classificações de Risco
        /// </summary>
        private void CarregaClassRisco()
        {
            AuxiliCarregamentos.CarregaClassificacaoRisco(ddlClassRisco, false);
        }

        /// <summary>
        /// Pesquisa se já existe um responsável com o CPF informado na tabela de responsáveis, se já existe ele preenche as informações do responsável 
        /// </summary>
        private void PesquisaCarregaResp(int? co_resp)
        {
            string cpfResp = txtCPFResp.Text.Replace(".", "").Replace("-", "");

            var res = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                       where (co_resp.HasValue ? tb108.CO_RESP == co_resp : tb108.NU_CPF_RESP == cpfResp)
                       select tb108).FirstOrDefault();

            if (res != null)
            {
                txtNomeResp.Text = res.NO_RESP;
                txtCPFResp.Text = res.NU_CPF_RESP;
                txtNuIDResp.Text = res.CO_RG_RESP;
                txtOrgEmiss.Text = res.CO_ORG_RG_RESP;
                ddlUFOrgEmis.SelectedValue = res.CO_ESTA_RG_RESP;
                txtDtNascResp.Text = res.DT_NASC_RESP.ToString();
                ddlSexResp.SelectedValue = res.CO_SEXO_RESP;
                //txtCEP.Text = res.CO_CEP_RESP;
                //ddlUF.SelectedValue = res.CO_ESTA_RESP;
                //carregaCidade();
                //ddlCidade.SelectedValue = res.CO_CIDADE != null ? res.CO_CIDADE.ToString() : "";
                //carregaBairro();
                //ddlBairro.SelectedValue = res.CO_BAIRRO != null ? res.CO_BAIRRO.ToString() : "";
                //txtLograEndResp.Text = res.DE_ENDE_RESP;
                txtEmailResp.Text = res.DES_EMAIL_RESP;
                txtTelCelResp.Text = res.NU_TELE_CELU_RESP;
                txtTelFixResp.Text = res.NU_TELE_RESI_RESP;
                txtNuWhatsResp.Text = res.NU_TELE_WHATS_RESP;
                txtTelComResp.Text = res.NU_TELE_COME_RESP;
                txtDeFaceResp.Text = res.NM_FACEBOOK_RESP;
                hidCoResp.Value = res.CO_RESP.ToString();
                
                res.TBS366_FUNCAO_SIMPLReference.Load();
                if (res.TBS366_FUNCAO_SIMPL != null)
                    ddlFuncao.SelectedValue = res.TBS366_FUNCAO_SIMPL.ID_FUNCAO_SIMPL.ToString();
            }
            ExecutaJavaScript();

            UpdatePanel2.Update();
        }

        /// <summary>
        /// Pesquisa se já existe um Paciente com o CPF informado na tabela de Pacientes, se já existe ele preenche as informações do Paciente 
        /// </summary>
        private void PesquisaCarregaPaci()
        {
            string cpfPaci = (!string.IsNullOrEmpty(txtCpfPaci.Text) ? txtCpfPaci.Text.Replace(".", "").Replace("-", "") : string.Empty);
            decimal? nis = (!string.IsNullOrEmpty(txtNuNisPaci.Text) ? decimal.Parse(txtNuNisPaci.Text) : (decimal?)null);
            int? prontuario = (!string.IsNullOrEmpty(txtNuProntuario.Text) ? int.Parse(txtNuProntuario.Text) : (int?)null);

            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       where (rdbPesqCPF.Checked ? tb07.NU_CPF_ALU == cpfPaci : 0 == 0)
                       && (rdbPesqNIS.Checked && nis.HasValue ? tb07.NU_NIS == nis.Value : 0 == 0)
                       && (rdbPesqProntuario.Checked && prontuario.HasValue ? tb07.NU_NIRE == prontuario : 0 == 0)
                       select tb07).FirstOrDefault();

            if (res != null)
            {
                txtnompac.Text = res.NO_ALU;
                txtCpfPaci.Text = res.NU_CPF_ALU;
                txtNuNisPaci.Text = res.NU_NIS.ToString();
                txtDtNascPaci.Text = res.DT_NASC_ALU.ToString();
                ddlSexoPaci.SelectedValue = res.CO_SEXO_ALU;
                txtTelCelPaci.Text = res.NU_TELE_CELU_ALU;
                txtTelResPaci.Text = res.NU_TELE_RESI_ALU;
                ddlGrParen.SelectedValue = res.CO_GRAU_PAREN_RESP;
                txtEmailPaci.Text = res.NO_EMAIL_PAI;
                txtWhatsPaci.Text = res.NU_TELE_WHATS_ALU;
                hidCoPac.Value = res.CO_ALU.ToString();
                ddlEtniaAlu.SelectedValue = res.TP_RACA;

                txtLograEndResp.Text = txtLograEndResp_PADRAO.Text = res.DE_ENDE_ALU;
                ddlUF.SelectedValue = res.CO_ESTA_ALU;
                txtCEP.Text = txtCEP_PADRAO.Text = res.CO_CEP_ALU;
                res.TB905_BAIRROReference.Load();
                if (res.TB905_BAIRRO != null)
                {
                    carregaCidade();
                    res.TB905_BAIRRO.TB904_CIDADEReference.Load();
                    if (res.TB905_BAIRRO.TB904_CIDADE != null)
                    {
                        ListItem it1 = ddlCidade.Items.FindByValue(res.TB905_BAIRRO.TB904_CIDADE.CO_CIDADE.ToString());
                        if (it1 != null)
                            ddlCidade.SelectedValue = it1.Value;

                        carregaBairro();
                        ListItem it2 = ddlCidade.Items.FindByValue(res.TB905_BAIRRO.CO_BAIRRO.ToString());
                        if (it2 != null)
                            ddlCidade.SelectedValue = it2.Value;
                    }
                }

                res.ImageReference.Load();
                upImagemAluno.ImagemLargura = 70;
                upImagemAluno.ImagemAltura = 85;

                if (res.Image != null)
                    upImagemAluno.CarregaImagem(res.Image.ImageId);
                else
                    upImagemAluno.CarregaImagem(0);

                res.TB108_RESPONSAVELReference.Load();

                if (res.TB108_RESPONSAVEL != null)
                    PesquisaCarregaResp(res.TB108_RESPONSAVEL.CO_RESP);
            }
        }

        /// <summary>
        /// Devido ao método de reload na grid de Pré-Atendimento, ela perde a seleção do checkbox, este método coleta 
        /// </summary>
        private void selecionaGridPreAtend()
        {
            CheckBox chk;
            string idAgenda;
            // Valida se a grid de atividades possui algum registro
            if (grdAgendaPlantoes.Rows.Count != 0)
            {
                // Passa por todos os registros da grid de atividades
                foreach (GridViewRow linha in grdAgendaPlantoes.Rows)
                {
                    idAgenda = ((HiddenField)linha.Cells[0].FindControl("hidCoConsul")).Value;
                    int coPre = (int)HttpContext.Current.Session["VL_Agenda_DMB"];

                    if (int.Parse(idAgenda) == coPre)
                    {
                        chk = (CheckBox)linha.Cells[0].FindControl("chkselect");
                        chk.Checked = true;
                    }
                }
            }
        }

        /// <summary>
        /// Devido ao método de reload na grid de Médicos Plantonistas, ela perde a seleção do checkbox, este método coleta 
        /// </summary>
        private void selecionaGridMedicPlan()
        {
            CheckBox chk;
            // Valida se a grid de atividades possui algum registro
            if (grdMedicosPlanto.Rows.Count != 0)
            {
                // Passa por todos os registros da grid de atividades
                foreach (GridViewRow linha in grdMedicosPlanto.Rows)
                {
                    int coColPlantonista = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidcoCol")).Value);
                    int coCol = (int)HttpContext.Current.Session["VL_MEDIC"];

                    if (coColPlantonista == coCol)
                    {
                        chk = (CheckBox)linha.Cells[0].FindControl("chkselect2");
                        chk.Checked = true;
                    }
                }
            }
        }

        /// <summary>
        /// É o método usado para desmarcar a grid de pré-atendimento e limpar as variáveis de apoio referentes à ela
        /// </summary>
        private void DesmarcaPreAtendimento()
        {
            foreach (GridViewRow li in grdAgendaPlantoes.Rows)
            {
                CheckBox chk = (((CheckBox)li.Cells[0].FindControl("chkselect")));
                chk.Checked = false;
            }

            HttpContext.Current.Session.Remove("FL_Select_Grid_B");
            HttpContext.Current.Session.Remove("VL_Agenda_DMB");
            chkEncaComPreAtend.Checked = false;
            UpdatePanel1.Update();
        }

        /// <summary>
        /// Responsável por executar as funções padrões para quando um registro do Pré-Atendimento for desmarcado pelo usuário
        /// </summary>
        private void GridPreAtendimentoDesmarcada()
        {
            //Guarda a FLAG para saber se o Checkbox está sendo clicado para marcar ou desmarcar, neste caso, ele grava como desmarcada.
            HttpContext.Current.Session.Add("FL_Select_Grid", "N");
            HttpContext.Current.Session.Remove("VL_Agenda_DMB");
            LimpaCampos();
        }

        /// <summary>
        /// Responsável por executar as funções padrões para quando um registro do Pré-Atendimento for desmarcado pelo usuário
        /// </summary>
        private void GridMedicosPlantonistasDesmarcada()
        {
            HttpContext.Current.Session.Remove("CoCol");
            HttpContext.Current.Session.Remove("coEmp");
            ddlEspec.SelectedValue = "";

            //Guarda a FLAG para saber se o Checkbox está sendo clicado para marcar ou desmarcar, neste caso, ele grava como desmarcada.
            HttpContext.Current.Session.Add("FL_Select_Grid_MEDIC", "N");
            HttpContext.Current.Session.Remove("VL_MEDIC");
        }

        /// <summary>
        /// Métodos padrões à serem chamados quando uma linha da grid de pré-atendimento for selecionada
        /// </summary>
        private void GridPreAtendimentoSelecionada(int idAgenda, int coEspec, string CoClassProfi)
        {
            //Guarda a FLAG para saber se o Checkbox está sendo clicado para marcar ou desmarcar, neste caso, ele grava como para marcar.
            HttpContext.Current.Session.Add("FL_Select_Grid_B", "S");

            //Guarda o Valor do Pré-Atendimento, para fins de posteriormente comparar este valor 
            HttpContext.Current.Session.Add("VL_Agenda_DMB", idAgenda);

            CarregaCampos(idAgenda);
            UpdatePanel2.Update();
            //UpdatePanel3.Update();

            //Carrega as informações de pré-atendimento nos campos como facilitador
            if (ddlEspec.Items.Contains(new ListItem("", coEspec.ToString())))
                ddlEspec.SelectedValue = coEspec.ToString();

            //ddlPesqClassProfi.SelectedValue = CoClassProfi;
            int coDepto = ddlPesqLocal.SelectedValue != "" ? int.Parse(ddlPesqLocal.SelectedValue) : 0;

            carregaGridMedicosPlantonistas(CoClassProfi, coDepto);
            updProfiPlantao.Update();
            updInfosBottom.Update();
            updInfoPlaSaude.Update();
            ExecutaJavaScript();
            //ExecutaJavaScript();
        }

        /// <summary>
        /// Métodos padrões à serem chamados quando uma linha da grid de pré-atendimento for selecionada
        /// </summary>
        private void GridMedicosPlantonistasSelecionada(string coEspec, int coColPlantonista, int coEmpColPlantonista)
        {
            ddlEspec.SelectedValue = coEspec;
            updInfosBottom.Update();

            //Guarda a FLAG para saber se o Checkbox está sendo clicado para marcar ou desmarcar, neste caso, ele grava como para marcar.
            HttpContext.Current.Session.Add("FL_Select_Grid_MEDIC", "S");

            //Guarda o Valor do Médico Plantonista, para fins de posteriormente comparar este valor 
            HttpContext.Current.Session.Add("VL_MEDIC", coColPlantonista);

            HttpContext.Current.Session.Add("CoCol", coColPlantonista);
            HttpContext.Current.Session.Add("coEmp", coEmpColPlantonista);
        }

        /// <summary>
        /// Limpa as informações de todos os campos
        /// </summary>
        private void LimpaCampos()
        {
            txtCPFResp.Text = txtNomeResp.Text = txtNuIDResp.Text = txtOrgEmiss.Text = ddlUFOrgEmis.SelectedValue =
                txtDtNascResp.Text = ddlSexResp.SelectedValue = txtCEP.Text = ddlCidade.SelectedValue
                = ddlBairro.SelectedValue = txtLograEndResp.Text = txtEmailResp.Text = txtTelCelResp.Text = txtTelFixResp.Text
                = txtNuNisPaci.Text = txtCpfPaci.Text = txtDtNascPaci.Text = ddlSexoPaci.SelectedValue
                = txtTelResPaci.Text = txtTelCelPaci.Text = ddlGrParen.SelectedValue = txtEmailPaci.Text
                = ddlUF.SelectedValue = txtnompac.Text = txtNuWhatsResp.Text = txtWhatsPaci.Text = txtDeFaceResp.Text = "";

            UpdatePanel2.Update();
            ExecutaJavaScript();
        }

        /// <summary>
        /// Executa método javascript que corrige algumas regras faltantes
        /// </summary>
        private void ExecutaJavaScript()
        {
            ScriptManager.RegisterStartupScript(
                UpdatePanel2,
                this.GetType(),
                "Acao",
                "carregaPadroes();",
                true
            );
        }

        /// <summary>
        /// Abre mensagem com informações
        /// </summary>
        private void AbreMensagemInfos(string texto)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + texto + "');", true);
            //ScriptManager.RegisterClientScriptBlock(UpdatePanel2, GetType(), "newmsgE", "AbreMensagem('" + texto + "\');", true);
        }

        /// <summary>
        /// Verifica o nire
        /// </summary>
        private void VerificarNireAutomatico()
        {
            string strTipoNireAuto = "";
            //----------------> Variável que guarda informações da unidade selecionada pelo usuário logado
            var tb25 = (from iTb25 in TB25_EMPRESA.RetornaTodosRegistros()
                        where iTb25.CO_EMP == LoginAuxili.CO_EMP
                        select new { iTb25.TB83_PARAMETRO.FLA_GERA_NIRE_AUTO }).First();

            strTipoNireAuto = tb25.FLA_GERA_NIRE_AUTO != null ? tb25.FLA_GERA_NIRE_AUTO : "";

            if (strTipoNireAuto != "")
            {
                //----------------> Faz a verificação para saber se o NIRE é automático ou não
                if (strTipoNireAuto == "N")
                {
                }
                else
                {
                    ///-------------------> Adiciona no campo NU_NIRE o número do último código do aluno + 1
                    int? lastCoAlu = (from lTb07 in TB07_ALUNO.RetornaTodosRegistros()
                                      select lTb07.NU_NIRE == null ? new Nullable<int>() : lTb07.NU_NIRE).Max();
                    if (lastCoAlu != null)
                    {
                        txtNuProntuario.Text = (lastCoAlu.Value + 1).ToString();
                    }
                    else
                        txtNuProntuario.Text = "1";
                }
            }
        }

        #endregion

        #region Funções de Campo

        protected void imgPesqGrid_OnClick(object sender, EventArgs e)
        {
            CarregaConsultasAgendadas();
        }

        /// <summary>
        /// Evento necessário para que a grid "clicável" funcione
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdAgendaPlantoes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //--------> Criação do estilo e links das linhas da GRID
            if (e.Row.DataItem != null)
            {
                e.Row.Attributes.Add("onClick", "javascript:__doPostBack('" + grdAgendaPlantoes.UniqueID + "','Select$" + qtdLinhasGrid + "')");
                qtdLinhasGrid = qtdLinhasGrid + 1;

                //Se for registro antigo(um dos 10 antigos que são também apresentados), destaca ele em cor salmão
                if (((HiddenField)e.Row.Cells[0].FindControl("hidAntigos")).Value == "1")
                    e.Row.BackColor = System.Drawing.Color.AntiqueWhite;
            }
        }

        /// <summary>
        /// Evento necessário para que a grid "clicável" funcione
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdMedicosPlanto_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //--------> Criação do estilo e links das linhas da GRID
            if (e.Row.DataItem != null)
            {
                e.Row.Attributes.Add("onClick", "javascript:__doPostBack('" + grdMedicosPlanto.UniqueID + "','Select$" + qtdLinhasGrid + "')");
                qtdLinhasGrid = qtdLinhasGrid + 1;
            }
        }

        /// <summary>
        /// Evento chamado ao clicar em qualquer ponto de uma linha de informações sobre um pré-atendimento na Grid Superior
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdAgendaPlantoes_SelectedIndexChanged(object sender, EventArgs e)
        {
            //--------> Define o código da Unidade Educacional Selecionada como a Unidade em uso
            if (grdAgendaPlantoes.DataKeys[grdAgendaPlantoes.SelectedIndex].Value != null)
            {
                // Passa por todos os registros da grid de Pré-Atendimentos
                foreach (GridViewRow linha in grdAgendaPlantoes.Rows)
                {
                    CheckBox chk = (CheckBox)linha.Cells[0].FindControl("chkselect");
                    int idPreAtend = int.Parse((((HiddenField)linha.Cells[0].FindControl("hidCoConsul")).Value));

                    //Verifica se foi clicada uma linha diferente da selecionada, caso tenha sido, desmarca a linha
                    if (idPreAtend != Convert.ToInt32(grdAgendaPlantoes.DataKeys[grdAgendaPlantoes.SelectedIndex].Value))
                        chk.Checked = false;
                    //Se for a mesma linha então seleciona o checkbox correspondente
                    else
                    {
                        //Verifico se já está selecionado, caso já esteja eu recorro aos padrões de limpeza de dados
                        if (chk.Checked)
                        {
                            chk.Checked = false;
                            GridPreAtendimentoDesmarcada();
                        }
                        //Caso não esteja selecionado ainda, chamo os métodos responsáveis pelos carregamentos
                        else
                        {
                            chk.Checked = true;
                            int idAtend = Convert.ToInt32(grdAgendaPlantoes.DataKeys[grdAgendaPlantoes.SelectedIndex].Value);

                            int coPreAtend = idPreAtend;
                            int coEspec = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidCoEspec")).Value);
                            string ClassProfi = ((HiddenField)linha.Cells[0].FindControl("hidCoClassProfi")).Value;

                            GridPreAtendimentoSelecionada(coPreAtend, coEspec, ClassProfi);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Evento chamado ao clicar em qualquer ponto de uma linha de informações sobre um pré-atendimento na Grid Superior
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdMedicosPlanto_SelectedIndexChanged(object sender, EventArgs e)
        {
            //--------> Define o código da Unidade Educacional Selecionada como a Unidade em uso
            if (grdMedicosPlanto.DataKeys[grdMedicosPlanto.SelectedIndex].Value != null)
            {
                // Passa por todos os registros da grid de Pré-Atendimentos
                foreach (GridViewRow linha in grdMedicosPlanto.Rows)
                {
                    CheckBox chk = (CheckBox)linha.Cells[0].FindControl("chkselect2");
                    int coCol = int.Parse((((HiddenField)linha.Cells[0].FindControl("hidcoCol")).Value));

                    //Verifica se foi clicada uma linha diferente da selecionada, caso tenha sido, desmarca a linha
                    if (coCol != Convert.ToInt32(grdMedicosPlanto.DataKeys[grdMedicosPlanto.SelectedIndex].Value))
                        chk.Checked = false;
                    //Se for a mesma linha então seleciona o checkbox correspondente
                    else
                    {
                        //Verifico se já está selecionado, caso já esteja eu recorro aos padrões de limpeza de dados
                        if (chk.Checked)
                        {
                            chk.Checked = false;
                            GridMedicosPlantonistasDesmarcada();
                        }
                        //Caso não esteja selecionado ainda, chamo os métodos responsáveis pelos carregamentos
                        else
                        {
                            chk.Checked = true;
                            int idAtend = Convert.ToInt32(grdMedicosPlanto.DataKeys[grdMedicosPlanto.SelectedIndex].Value);

                            int coColPlantonista = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidcoCol")).Value);
                            int coEmpColPlantonista = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidcoEmpColPla")).Value);
                            string coEspec = ((HiddenField)linha.Cells[0].FindControl("hidCoEspec")).Value;

                            GridMedicosPlantonistasSelecionada(coEspec, coColPlantonista, coEmpColPlantonista);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// É executado a cada 10 segundos para atualizar a grid de Pré-Atendimentos automaticamente.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //int coEspec = ddlPesqEspec.SelectedValue != "" ? int.Parse(ddlPesqEspec.SelectedValue) : 0;
            string classProfi = ddlPesqClassProfi.SelectedValue;
            int coDepto = ddlPesqLocal.SelectedValue != "" ? int.Parse(ddlPesqLocal.SelectedValue) : 0;

            CarregaConsultasAgendadas();
            carregaGridMedicosPlantonistas(classProfi, coDepto);

            //Verifica se a grid já possuia um registro selecionado antes, e chama um método responsável por selecioná-lo de novo no PRÉ-ATENDIMENTO
            if ((string)HttpContext.Current.Session["FL_Select_Grid_B"] == "S")
            {
                selecionaGridPreAtend();
            }

            //Verifica se a grid já possuia um registro selecionado antes, e chama um método responsável por selecioná-lo de novo no MÉDICOS PLANTONISTAS
            if ((string)HttpContext.Current.Session["FL_Select_Grid_MEDIC"] == "S")
            {
                selecionaGridMedicPlan();
            }
            UpdatePanel1.Update();
            updProfiPlantao.Update();
        }

        /// <summary>
        /// Carrega as UFs no campo de UF's.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlUF_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaCidade();
            ExecutaJavaScript();
            ddlCidade.Focus();
        }

        /// <summary>
        /// Carrega as Cidades de acordo com as UF's selecionadas anteriormente.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCidade_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaBairro();
            ExecutaJavaScript();

            ddlBairro.Focus();
        }

        protected void ddlOperPlano_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaPlano();
            updInfoPlaSaude.Update();
            ddlPlano.Focus();
            //ExecutaJavaScript();
        }

        protected void chkPaciEhResp_OnCheckedChanged(object sender, EventArgs e)
        {
            carregaPaciehoResponsavel();
            DesmarcaPreAtendimento();

            if (chkPaciEhResp.Checked)
                chkPaciMoraCoResp.Checked = true;
        }

        protected void imgCpfResp_OnClick(object sender, EventArgs e)
        {
            PesquisaCarregaResp(null);
            //ExecutaJavaScript();
        }

        protected void imbPesqPaci_OnClick(object sender, EventArgs e)
        {
            #region Validações

            //Se não houver nenhum tipo de pesquisa marcado
            if ((!rdbPesqCPF.Checked) && (!rdbPesqNIS.Checked) && (!rdbPesqProntuario.Checked))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "É preciso selecionar ao menos uma das opções de pesquisa!");
                return;
            }

            //Apresenta erro, caso esteja selecionada busca por CPF mas nenhum tenha sido informado
            if ((rdbPesqCPF.Checked) && (string.IsNullOrEmpty(txtCpfPaci.Text)))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Você selecionou pesquisa por CPF mas não informou o número pelo qual pesquisar. Favor informar!");
                return;
            }

            //Apresenta erro, caso esteja selecionada busca por NIS mas nenhum tenha sido informado
            if ((rdbPesqNIS.Checked) && (string.IsNullOrEmpty(txtNuNisPaci.Text)))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Você selecionou pesquisa por NIS mas não informou o número pelo qual pesquisar. Favor informar!");
                return;
            }

            //Apresenta erro, caso esteja selecionada busca por PRONTUÁRIO mas nenhum tenha sido informado
            if ((rdbPesqProntuario.Checked) && (string.IsNullOrEmpty(txtNuProntuario.Text)))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Você selecionou pesquisa por Prontuário mas não informou o número pelo qual pesquisar. Favor informar!");
                return;
            }

            #endregion

            PesquisaCarregaPaci();
            DesmarcaPreAtendimento();
            //UpdatePanel2.Update();
            ExecutaJavaScript();
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
            UpdatePanel2.Update();
            ExecutaJavaScript();
        }

        protected void chkselect_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            CheckBox chk;

            int coPreAtend;
            int coEspec;
            // Valida se a grid de atividades possui algum registro
            if (grdAgendaPlantoes.Rows.Count != 0)
            {
                // Passa por todos os registros da grid de atividades
                foreach (GridViewRow linha in grdAgendaPlantoes.Rows)
                {
                    chk = (CheckBox)linha.Cells[0].FindControl("chkselect");

                    // Desmarca todos os registros menos o que foi clicado
                    if (chk.ClientID != atual.ClientID)
                    {
                        chk.Checked = false;
                    }
                    else
                    {
                        if (chk.Checked)
                        {
                            coPreAtend = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidCoConsul")).Value);
                            coEspec = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidCoEspec")).Value);
                            string ClassProfi = ((HiddenField)linha.Cells[0].FindControl("hidCoClassProfi")).Value;

                            GridPreAtendimentoSelecionada(coPreAtend, coEspec, ClassProfi);
                        }
                        else
                            GridPreAtendimentoDesmarcada();
                    }
                }
            }
        }

        protected void chkselect2_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            CheckBox chk;

            int coColPlantonista;
            int coEmpColPlantonista;

            // Valida se a grid de atividades possui algum registro
            if (grdMedicosPlanto.Rows.Count != 0)
            {
                // Passa por todos os registros da grid de atividades
                foreach (GridViewRow linha in grdMedicosPlanto.Rows)
                {
                    chk = (CheckBox)linha.Cells[0].FindControl("chkselect2");

                    // Desmarca todos os registros menos o que foi clicado
                    if (chk.ClientID != atual.ClientID)
                    {
                        chk.Checked = false;
                    }
                    else
                    {
                        if (chk.Checked)
                        {
                            coColPlantonista = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidcoCol")).Value);
                            coEmpColPlantonista = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidcoEmpColPla")).Value);
                            string coEspec = ((HiddenField)linha.Cells[0].FindControl("hidCoEspec")).Value;
                            GridMedicosPlantonistasSelecionada(coEspec, coColPlantonista, coEmpColPlantonista);
                        }
                        else
                            GridMedicosPlantonistasDesmarcada();
                    }
                }
            }
        }

        protected void txtCPFResp_OnTextChanged(object sender, EventArgs e)
        {
            //hidCoResp.Value = "";
            //UpdatePanel2.Update();
        }

        protected void imgPesqGridMedic_OnClick(object sender, EventArgs e)
        {
            //int coEspec = ddlPesqEspec.SelectedValue != "" ? int.Parse(ddlPesqEspec.SelectedValue) : 0;
            string classProfi = ddlPesqClassProfi.SelectedValue;
            int coDepto = ddlPesqLocal.SelectedValue != "" ? int.Parse(ddlPesqLocal.SelectedValue) : 0;
            carregaGridMedicosPlantonistas(classProfi, coDepto);
            updProfiPlantao.Update();
        }

        protected void lnkEfetAtendMed_OnClick(object sender, EventArgs e)
        {
            SalvaEntidades();
            if (!string.IsNullOrEmpty(hdIdEncam.Value))
            {
                lnkImpFichaAtendMed.Enabled = true;
                Session["IdEcam_DB"] = hdIdEncam.Value;
            }
        }

        protected void lnkImpFichaAtendMed_OnClick(object sender, EventArgs e)
        {
            string parametros = "";
            string infos = "";
            int lRetorno;
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            //string nome = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada();
            string idEmcam = Session["IdEcam_DB"].ToString();
            if (!string.IsNullOrEmpty(idEmcam))
            {
                RptFichaDirecionamento fpcb = new RptFichaDirecionamento();
                lRetorno = fpcb.InitReport(parametros, infos, LoginAuxili.CO_EMP, int.Parse(idEmcam), "Ficha de Direcionamento");
                Session["Report"] = fpcb;
                Session["URLRelatorio"] = "/GeducReportViewer.aspx";

                //AuxiliPagina.AbreNovaJanela(this, Session["URLRelatorio"].ToString());

                string strURL = String.Format("{0}", Session["URLRelatorio"].ToString());
                //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "newpageE", "window.open(\"" + strURL + "\");", true);
                //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "mensagem", "alert('Deveria ter aberto uma página');", true);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "newpageE", "customOpen('" + strURL + "\');", true);

                //----------------> Limpa a var de sessão com o url do relatório.
                Session.Remove("URLRelatorio");

                //----------------> Limpa a ref da url utilizada para carregar o relatório.
                PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                isreadonly.SetValue(this.Request.QueryString, false, null);
                isreadonly.SetValue(this.Request.QueryString, true, null);
            }
        }

        protected void lnkNovo_OnClick(object sender, EventArgs e)
        {
            AuxiliPagina.RedirecionaParaPaginaSucesso("Providenciando nova tela...", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
        }

        #endregion
    }
}