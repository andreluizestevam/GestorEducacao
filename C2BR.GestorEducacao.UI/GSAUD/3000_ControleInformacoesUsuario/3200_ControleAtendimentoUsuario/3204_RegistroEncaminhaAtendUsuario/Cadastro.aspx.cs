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

namespace C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3204_RegistroEncaminhaAtendUsuario
{
    public partial class Cadastro : System.Web.UI.Page
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
                //------------> Tamanho da imagem de Aluno
                upImagemAluno.ImagemLargura = 70;
                upImagemAluno.ImagemAltura = 85;
                upImagemAluno.MostraProcurar = false;

                carregaGridPreAtendimento();
                carregaGridMedicosPlantonistas();

                AuxiliCarregamentos.CarregaUFs(ddlUF, false, LoginAuxili.CO_EMP);
                AuxiliCarregamentos.CarregaUFs(ddlUFOrgEmis, false, LoginAuxili.CO_EMP);
                AuxiliCarregamentos.CarregaEspeciacialidades(ddlPesqEspec, LoginAuxili.CO_EMP, null, true);
                AuxiliCarregamentos.CarregaDepartamentos(ddlPesqLocal, LoginAuxili.CO_EMP, true);

                IniPeri.Text = DateTime.Now.ToString();
                FimPeri.Text = DateTime.Now.ToString();

                carregaCidade();
                carregaBairro();
                carregaEspec();
                carregaOperadoraPL();
                carregaPlano();
                CarregaClassRisco();

                chkPesqCPFUsu.Checked = true;
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
            int? coPreAt = (HttpContext.Current.Session["VL_PreAtend"] != null ? (int)HttpContext.Current.Session["VL_PreAtend"] : (int?)null);
            int cocolP = (HttpContext.Current.Session["coCol"] != null ? (int)HttpContext.Current.Session["coCol"] : 0);
            int coempP = (HttpContext.Current.Session["coEmp"] != null ? (int)HttpContext.Current.Session["coEmp"] : 0);

            bool erros = false;

            //----------------------------------------------------- Valida os campos do Responsável -----------------------------------------------------
            if (txtNomeResp.Text == "")
            { AuxiliPagina.EnvioMensagemErro(this.Page, "O Nome do Responsável é Requerido"); erros = true; }

            if (txtCPFResp.Text == "")
            { AuxiliPagina.EnvioMensagemErro(this.Page, "O CPF do Responsável é Requerido"); erros = true; }

            if (ddlSexResp.SelectedValue == "")
            { AuxiliPagina.EnvioMensagemErro(this.Page, "O Sexo do Responsável é Requerido"); erros = true; }

            if (txtDtNascResp.Text == "")
            { AuxiliPagina.EnvioMensagemErro(this.Page, "A Data de Nascimento do Responsável é Requerida"); erros = true; }

            if (txtTelCelResp.Text == "")
            { AuxiliPagina.EnvioMensagemErro(this.Page, "O Telefone do Responsável é Requerido"); erros = true; }

            if (txtNuIDResp.Text == "")
            { AuxiliPagina.EnvioMensagemErro(this.Page, "O Número da Identidade do Responsável é Requerido"); erros = true; }

            if (txtCEP.Text == "")
            { AuxiliPagina.EnvioMensagemErro(this.Page, "O CEP do Endereço do Responsável é Requerido"); erros = true; }

            if (ddlUF.Text == "")
            { AuxiliPagina.EnvioMensagemErro(this.Page, "O UF do Endereço do Responsável é Requerida"); erros = true; }

            if (ddlCidade.Text == "")
            { AuxiliPagina.EnvioMensagemErro(this.Page, "A Cidade do Responsável é Requerida"); erros = true; }

            if (ddlBairro.Text == "")
            { AuxiliPagina.EnvioMensagemErro(this.Page, "O Bairro do Responsável é Requerido"); erros = true; }

            if (txtLograEndResp.Text == "")
            { AuxiliPagina.EnvioMensagemErro(this.Page, "O Logradouro do Responsável é Requerido"); erros = true; }

            //----------------------------------------------------- Valida os campos do Aluno -----------------------------------------------------
            if (ddlSexoPaci.SelectedValue == "")
            { AuxiliPagina.EnvioMensagemErro(this.Page, "O Sexo do Paciente é Requerido"); erros = true; }

            if (txtDtNascPaci.Text == "")
            { AuxiliPagina.EnvioMensagemErro(this.Page, "A Data de Nascimento do Paciente é Requerida"); erros = true; }

            if (txtNuNisPaci.Text == "")
            { AuxiliPagina.EnvioMensagemErro(this.Page, "O Número do NIS do Paciente é Requerido"); erros = true; }


            //----------------------------------------------------- Valida os Campos Gerais -----------------------------------------------------
            if (ddlClassRisco.SelectedValue == "")
            { AuxiliPagina.EnvioMensagemErro(this.Page, "A Classificação de Risco é Requerida"); erros = true; }

            if (ddlEspec.SelectedValue == "")
            { AuxiliPagina.EnvioMensagemErro(this.Page, "A Especialidade é Requerida"); erros = true; }

            if (cocolP == 0)
            { AuxiliPagina.EnvioMensagemErro(this.Page, "É Preciso selecionar um Médico para realizar o Encaminhamento"); erros = true; }

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
                if (string.IsNullOrEmpty(hidCoResp.Value))
                {
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
                if(string.IsNullOrEmpty(hidCoPac.Value))
                {
                    tb07 = new TB07_ALUNO();

                    #region Bloco foto
                    int codImagem = upImagemAluno.GravaImagem();
                    tb07.Image = C2BR.GestorEducacao.BusinessEntities.MSSQL.Image.RetornaPelaChavePrimaria(codImagem);
                    #endregion

                    tb07.NO_ALU = txtnompac.Text;
                    chkPesqCPFUsu.Checked = true;
                    tb07.NU_CPF_ALU = txtCpfPaci.Text.Replace(".", "").Replace("-", "").Trim();
                    tb07.NU_NIS = decimal.Parse(txtNuNisPaci.Text);
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
                tbs195.ID_PRE_ATEND = (coPreAt != 0 ? coPreAt : (int?)null);
                tbs195.CO_PRE_ATEND = (coPreAt != 0 ? TBS194_PRE_ATEND.RetornaPelaChavePrimaria(coPreAt.Value).CO_PRE_ATEND : null);
                tbs195.CO_ALU = tb07.CO_ALU;
                tbs195.CO_RESP = tb108.CO_RESP;
                tbs195.TP_RESP = (chkPaciEhResp.Checked ? "P" : "R");
                tbs195.CO_ESPEC = int.Parse(ddlEspec.SelectedValue);
                tbs195.NR_CLASS_RISCO = int.Parse(ddlClassRisco.SelectedValue);
                tbs195.ID_OPER = (ddlOperPlano.SelectedValue != "" ? int.Parse(ddlOperPlano.SelectedValue) : (int?)null);
                tbs195.ID_PLAN = (ddlPlano.SelectedValue != "" ? int.Parse(ddlPlano.SelectedValue) : (int?)null);
                tbs195.DT_VENC_CART_PLANO = (!string.IsNullOrEmpty(txtDtVenciPlan.Text)? txtDtVenciPlan.Text : null);
                tbs195.NU_CART_PLANO = (txtNumeroCartPla.Text != "" ? int.Parse(txtNumeroCartPla.Text) : (int?)null);
                tbs195.FL_PRE_ATEND = (chkEncaComPreAtend.Checked ? "S" : "N");
                tbs195.DT_ENCAM_MEDIC = DateTime.Now;
                tbs195.CO_COL_REAL_ENCAM = LoginAuxili.CO_COL;
                tbs195.CO_EMP_REAL_ENCAM = LoginAuxili.CO_EMP;
                tbs195.NR_IP_REAL_ENCAM = Request.UserHostAddress;
                tbs195.CO_SITUA_ENCAM_MEDIC = "A";
                tbs195.DT_SITUA_ENCAM_MEDIC = DateTime.Now;
                tbs195.DT_CADAS_ENCAM = DateTime.Now;
                TBS195_ENCAM_MEDIC.SaveOrUpdate(tbs195, true);

                //Limpa as Sessions usadas para guardar informações
                HttpContext.Current.Session.Remove("VL_PreAtend");
                HttpContext.Current.Session.Remove("coCol");
                HttpContext.Current.Session.Remove("coEmp");
                HttpContext.Current.Session.Remove("FL_Select_Grid");
                HttpContext.Current.Session.Remove("FL_Select_Grid_MEDIC");
                HttpContext.Current.Session.Remove("VL_MEDIC");

                //Altera a situação do Atendimento para Encaminhado, para que dessa forma, não apareça na Grid de Pré-Atendimentos.
                if (coPreAt.HasValue)
                {
                    TBS194_PRE_ATEND t4 = TBS194_PRE_ATEND.RetornaPelaChavePrimaria(coPreAt.Value);
                    if (t4 != null)
                    {
                        t4.CO_SITUA_PRE_ATEND = "E";
                        t4.DT_SITUA_PRE_ATEND = DateTime.Now;
                        TBS194_PRE_ATEND.SaveOrUpdate(t4);
                    }
                }
                #endregion

                AuxiliPagina.RedirecionaParaPaginaSucesso("Encaminhamento do Atendimento Realizado com sucesso.", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
            }
        }

        /// <summary>
        /// Carrega a Grid de Registros do Pré-Atendimento
        /// </summary>
        private void carregaGridPreAtendimento()
        {
            //DateTime dtIni = DateTime.Now.AddDays(-1);
            //DateTime dtAtual = DateTime.Now;
            DateTime dtIni = (!string.IsNullOrEmpty(IniPeri.Text) ? DateTime.Parse(IniPeri.Text) : DateTime.Now);
            DateTime dtFim = (!string.IsNullOrEmpty(FimPeri.Text) ? DateTime.Parse(FimPeri.Text) : DateTime.Now);

            var res = (from tbs194 in TBS194_PRE_ATEND.RetornaTodosRegistros()
                       join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tbs194.CO_ESPEC equals tb63.CO_ESPECIALIDADE
                       where tbs194.CO_SITUA_PRE_ATEND == "A"
                       && (tbs194.CO_EMP == LoginAuxili.CO_EMP)
                       && ((EntityFunctions.TruncateTime(tbs194.DT_PRE_ATEND) >= EntityFunctions.TruncateTime(dtIni))
                       && (EntityFunctions.TruncateTime(tbs194.DT_PRE_ATEND) <= EntityFunctions.TruncateTime(dtFim)))
                       select new PreAtendimento
                       {
                           CO_ESPEC = tbs194.CO_ESPEC,
                           ID_PRE_ATEND = tbs194.ID_PRE_ATEND,
                           NO_PAC = tbs194.NO_USU,
                           NO_RESP = tbs194.NO_RESP,
                           NO_ESPEC = tb63.NO_ESPECIALIDADE,
                           classRiscoValid = tbs194.CO_TIPO_RISCO,
                           dataPreAtend = tbs194.DT_PRE_ATEND,
                           NuPreAtendValid = tbs194.CO_PRE_ATEND,
                           SENHA = tbs194.NU_SENHA_ATEND,
                           CO_TIPO_RISCO = tbs194.CO_TIPO_RISCO,
                           CO_SEXO = tbs194.CO_SEXO_USU,
                           dt_nascimento = tbs194.DT_NASC_USU,

                       }).OrderBy(w => w.CO_TIPO_RISCO).ThenByDescending(q => q.dataPreAtend).ToList();

            //Faz uma lista com os 10 últimos acolhimentos e insere na lista anterior
            #region 10 últimos acolhimentos
            var resAntigos = (from tbs194 in TBS194_PRE_ATEND.RetornaTodosRegistros()
                       join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tbs194.CO_ESPEC equals tb63.CO_ESPECIALIDADE
                       where tbs194.CO_SITUA_PRE_ATEND == "A"
                       && (tbs194.CO_EMP == LoginAuxili.CO_EMP)
                       //Garante que vai pegar apenas aqueles fora do range das datas informadas
                       && (EntityFunctions.TruncateTime(tbs194.DT_PRE_ATEND) < EntityFunctions.TruncateTime(dtIni))
                       select new PreAtendimento
                       {
                           CO_ESPEC = tbs194.CO_ESPEC,
                           ID_PRE_ATEND = tbs194.ID_PRE_ATEND,
                           NO_PAC = tbs194.NO_USU,
                           NO_RESP = tbs194.NO_RESP,
                           NO_ESPEC = tb63.NO_ESPECIALIDADE,
                           classRiscoValid = tbs194.CO_TIPO_RISCO,
                           dataPreAtend = tbs194.DT_PRE_ATEND,
                           NuPreAtendValid = tbs194.CO_PRE_ATEND,
                           SENHA = tbs194.NU_SENHA_ATEND,
                           CO_TIPO_RISCO = tbs194.CO_TIPO_RISCO,
                           CO_SEXO = tbs194.CO_SEXO_USU,
                           dt_nascimento = tbs194.DT_NASC_USU,
                           ANTIGOS = 1,
                       }).OrderByDescending(q => q.dataPreAtend).Take(10).ToList();

            foreach (var i in resAntigos) // passa por cada item da segunda lista e insere na primeira
            {
                res.Add(i);
            }
            #endregion

            //Reordena os itens
            res = res.OrderBy(w => w.CO_TIPO_RISCO).ThenByDescending(q => q.dataPreAtend).ToList();

            grdAgendaPlantoes.DataSource = res;
            grdAgendaPlantoes.DataBind();

        }

        /// <summary>
        /// Método responsável por carregar os médicos plantonistas na grid correspondente
        /// </summary>
        /// <param name="CO_ESPEC"></param>
        private void carregaGridMedicosPlantonistas(int CO_ESPEC = 0, int CO_DEPTO = 0)
        {
            DateTime dtAtu = DateTime.Now;

            var res = (from tb159 in TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
                       join tb153 in TB153_TIPO_PLANT.RetornaTodosRegistros() on tb159.ID_TIPO_PLANT equals tb153.ID_TIPO_PLANT
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tb159.TB03_COLABOR.CO_COL equals tb03.CO_COL
                       join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tb159.CO_ESPEC_PLANT equals tb63.CO_ESPECIALIDADE
                       //where tb159.DT_INICIO_PREV >= dtAtu && tb159.DT_TERMIN_PREV <= dtAtu
                       where ((dtAtu >= tb159.DT_INICIO_PREV) && (dtAtu <= tb159.DT_TERMIN_PREV))
                       && (CO_ESPEC != 0 ? tb159.CO_ESPEC_PLANT == CO_ESPEC : 0 == 0)
                       && (CO_DEPTO != 0 ? tb153.TB14_DEPTO.CO_DEPTO == CO_DEPTO : 0 == 0)
                       && tb159.CO_EMP_AGEND_PLANT == LoginAuxili.CO_EMP
                       select new MedicosPlantonistas
                       {

                           NO_COL = tb03.NO_COL,
                           co_col = tb159.TB03_COLABOR.CO_COL,
                           co_emp_col_pla = tb03.CO_EMP,
                           //co_pla = tb159.CO_AGEND_PLANT_COLA,B
                           NO_ESPEC = tb63.NO_ESPECIALIDADE,
                           CO_ESPEC = tb159.CO_ESPEC_PLANT,
                       }).OrderBy(w => w.NO_COL).ToList();

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

        #region Classes de Saída

        /// <summary>
        /// Preenche a Grid de Pré-Atendimento com os registros previamente cadastrados na funcionalidade e processo de Cadastro de Pré-Atendimento.
        /// </summary>
        public class PreAtendimento
        {
            public int ANTIGOS { get; set; }

            public string LOCAL { get; set; }
            public int? CO_ESPEC { get; set; }
            public int ID_PRE_ATEND { get; set; }
            public string NO_PAC { get; set; }
            public string NO_ESPEC { get; set; }
            public int CO_TIPO_RISCO { get; set; }

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

            public string NO_RESP { get; set; }
            public string NO_RESP_V
            {
                get
                {
                    string[] nome;
                    //Coleta apenas o primeiro nome
                    try
                    {
                        nome = this.NO_RESP.Split(' ');
                        return nome[0];
                    }
                    catch { return null; }
                }
            }

            public DateTime dataPreAtend { get; set; }
            public string dataPAValid
            {
                get
                {
                    return this.dataPreAtend.ToString("dd/MM/yy");
                }
            }
            public string horaPAValid
            {
                get
                {
                    return this.dataPreAtend.ToString("hh:mm");
                }
            }

            public string DTHR
            {
                get
                {
                    return this.dataPAValid + " - " + this.horaPAValid;
                }
            }

            //Trata a Classificação de Risco, de Acordo com os valores, para apresentar o nome correspondente.
            public int classRiscoValid { get; set; }
            public string CLASS_RISCO
            {
                get
                {
                    string s = "";
                    switch (this.classRiscoValid)
                    {
                        case 1:
                            s = "EMERGÊNCIA";
                            break;

                        case 2:
                            s = "MUITO URGENTE";
                            break;

                        case 3:
                            s = "URGENTE";
                            break;

                        case 4:
                            s = "POUCO URGENTE";
                            break;

                        case 5:
                            s = "NÃO URGENTE";
                            break;
                    }

                    return s;
                }
            }
            public string SENHA { get; set; }

            public string NuPreAtendValid { get; set; }
            public string NU_PRE_ATEND
            {
                get
                {
                    return this.NuPreAtendValid.Insert(2, ".").Insert(6, ".").Insert(9, ".");
                }
            }
            public string SITU { get; set; }

            //Trata as cores de acordo com a classificação de risco
            public bool DIV_1
            {
                get
                {
                    return (this.CO_TIPO_RISCO == 1 ? true : false);
                }
            }
            public bool DIV_2
            {
                get
                {
                    return (this.CO_TIPO_RISCO == 2 ? true : false);
                }
            }
            public bool DIV_3
            {
                get
                {
                    return (this.CO_TIPO_RISCO == 3 ? true : false);
                }
            }
            public bool DIV_4
            {
                get
                {
                    return (this.CO_TIPO_RISCO == 4 ? true : false);
                }
            }
            public bool DIV_5
            {
                get
                {
                    return (this.CO_TIPO_RISCO == 5 ? true : false);
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
            public string NO_ESPEC { get; set; }
            public int? CO_ESPEC { get; set; }
            public string CO_TIPO_RISCO { get; set; }
        }

        #endregion

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
            var res = (from tb250 in TB250_OPERA.RetornaTodosRegistros()
                       select new { tb250.NOM_OPER, tb250.ID_OPER });

            ddlOperPlano.DataTextField = "NOM_OPER";
            ddlOperPlano.DataValueField = "ID_OPER";
            ddlOperPlano.DataSource = res;
            ddlOperPlano.DataBind();

            ddlOperPlano.Items.Insert(0, new ListItem("Nenhum", ""));
        }

        /// <summary>
        /// Carrega os Planos de saúde relacionados à Operadora selecionada no campo anterior, por exemplo, GoldenCross, Amil20, etc.
        /// </summary>
        private void carregaPlano()
        {
            int op = ddlOperPlano.SelectedValue != "" ? int.Parse(ddlOperPlano.SelectedValue) : 0;

            if (op != 0)
            {
                var res = (from tb251 in TB251_PLANO_OPERA.RetornaTodosRegistros()
                           where tb251.TB250_OPERA.ID_OPER == op
                           select new { tb251.NOM_PLAN, tb251.ID_PLAN });

                ddlPlano.DataTextField = "NOM_PLAN";
                ddlPlano.DataValueField = "ID_PLAN";
                ddlPlano.DataSource = res;
                ddlPlano.DataBind();

                ddlPlano.Items.Insert(0, new ListItem("Nenhum", ""));
            }
            else
            {
                ddlPlano.Items.Clear();
                ddlPlano.Items.Insert(0, new ListItem("Nenhum", ""));
            }
        }

        /// <summary>
        /// Carrega as Informações de Responsável e Paciente, de acordo com o registro que é clicado na Grid de Pré-Atendimentos.
        /// </summary>
        /// <param name="ID_PRE_ATEND"></param>
        private void CarregaResp(int ID_PRE_ATEND)
        {
            TBS194_PRE_ATEND tbs194 = TBS194_PRE_ATEND.RetornaPelaChavePrimaria(ID_PRE_ATEND);

            hidCoPac.Value = tbs194.CO_ALU.ToString();
            chkEncaComPreAtend.Checked = true;
            txtCPFResp.Text = tbs194.NU_CPF_RESP;
            txtNomeResp.Text = tbs194.NO_RESP;
            txtDtNascResp.Text = tbs194.DT_NASC_RESP.ToString();
            ddlSexResp.SelectedValue = tbs194.CO_SEXO_RESP;

            txtTelFixResp.Text = tbs194.NU_TEL_RESP;
            txtTelCelResp.Text = tbs194.NU_CEL_RESP;
            txtTelResPaci.Text = tbs194.NU_TEL_USU;
            txtTelCelPaci.Text = tbs194.NU_CEL_USU;

            chkPesqCPFUsu.Checked = true;
            txtCpfPaci.Text = tbs194.NU_CPF_USU;
            txtNuNisPaci.Text = tbs194.NU_NIS.ToString();
            txtDtNascPaci.Text = tbs194.DT_NASC_USU.ToString();
            ddlSexoPaci.SelectedValue = tbs194.CO_SEXO_USU;
            ddlGrParen.SelectedValue = tbs194.CO_GRAU_PAREN;
            txtnompac.Text = tbs194.NO_USU;

            upImagemAluno.ImagemLargura = 70;
            upImagemAluno.ImagemAltura = 85;
            upImagemAluno.MostraProcurar = false;

            tbs194.TB250_OPERAReference.Load();
            if (tbs194.TB250_OPERA != null)
            {
                ddlOperPlano.SelectedValue = tbs194.TB250_OPERA.ID_OPER.ToString();
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

            PesquisaCarregaResp(tbs194.CO_RESP);
            
            UpdatePanel2.Update();
        }

        /// <summary>
        /// Método que duplica as informações do responsável nos campos do paciente, quando o usuário clica em Paciente é o Responsável.
        /// </summary>
        private void carregaPaciehoResponsavel()
        {
            if (chkPaciEhResp.Checked)
            {
                chkPesqCPFUsu.Checked = true;
                chkPesqNuNisPac.Checked = false;
                txtCpfPaci.Text = txtCPFResp.Text;
                txtnompac.Text = txtNomeResp.Text;
                txtDtNascPaci.Text = txtDtNascResp.Text;
                ddlSexoPaci.SelectedValue = ddlSexResp.SelectedValue;
                txtTelCelPaci.Text = txtTelCelResp.Text;
                txtTelResPaci.Text = txtTelFixResp.Text;
                ddlGrParen.SelectedValue = "OU";
                txtEmailPaci.Text = txtEmailResp.Text;
                txtWhatsPaci.Text = txtNuWhatsResp.Text;

                txtEmailPaci.Enabled = false;
                chkPesqNuNisPac.Enabled = false;
                chkPesqCPFUsu.Enabled = false;
                txtCpfPaci.Enabled = false;
                txtnompac.Enabled = false;
                txtDtNascPaci.Enabled = false;
                ddlSexoPaci.Enabled = false;
                txtTelCelPaci.Enabled = false;
                txtTelCelPaci.Enabled = false;
                txtTelResPaci.Enabled = false;
                ddlGrParen.Enabled = false;
                txtWhatsPaci.Enabled = false;

                #region Verifica se já existe

                string cpf = txtCpfPaci.Text.Replace(".", "").Replace("-", "").Trim();

                var tb07 = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.NU_CPF_ALU == cpf).FirstOrDefault();

                if (tb07 != null)
                    hidCoPac.Value = tb07.CO_ALU.ToString();

                #endregion

            }
            else
            {
                chkPesqCPFUsu.Checked = false;
                chkPesqNuNisPac.Checked = false;
                txtCpfPaci.Text = "";
                txtnompac.Text = "";
                txtDtNascPaci.Text = "";
                ddlSexoPaci.SelectedValue = "";
                txtTelCelPaci.Text = "";
                txtTelResPaci.Text = "";
                ddlGrParen.SelectedValue = "";
                txtEmailPaci.Text = "";
                txtWhatsPaci.Text = "";

                chkPesqNuNisPac.Enabled = true;
                chkPesqCPFUsu.Enabled = true;
                txtCpfPaci.Enabled = true;
                txtnompac.Enabled = true;
                txtDtNascPaci.Enabled = true;
                ddlSexoPaci.Enabled = true;
                txtTelCelPaci.Enabled = true;
                txtTelCelPaci.Enabled = true;
                txtTelResPaci.Enabled = true;
                ddlGrParen.Enabled = true;
                txtEmailPaci.Enabled = true;
                txtWhatsPaci.Enabled = true;
                hidCoPac.Value = "";
            }
            UpdatePanel2.Update();
            ExecutaJavaScript();
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
                ddlCidade.SelectedValue = res.CO_CIDADE != null ? res.CO_CIDADE.ToString() : "";
                carregaBairro();
                ddlBairro.SelectedValue = res.CO_BAIRRO != null ? res.CO_BAIRRO.ToString() : "";
                txtLograEndResp.Text = res.DE_ENDE_RESP;
                txtEmailResp.Text = res.DES_EMAIL_RESP;
                txtTelCelResp.Text = res.NU_TELE_CELU_RESP;
                txtTelFixResp.Text = res.NU_TELE_RESI_RESP;
                txtNuWhatsResp.Text = res.NU_TELE_WHATS_RESP;
                txtTelComResp.Text = res.NU_TELE_COME_RESP;
                txtDeFaceResp.Text = res.NM_FACEBOOK_RESP;
                hidCoResp.Value = res.CO_RESP.ToString();
            }
            ExecutaJavaScript();

            UpdatePanel2.Update();
        }

        /// <summary>
        /// Pesquisa se já existe um Paciente com o CPF informado na tabela de Pacientes, se já existe ele preenche as informações do Paciente 
        /// </summary>
        private void PesquisaCarregaPaci()
        {
            if (chkPesqCPFUsu.Checked == true)
            {
                string cpfPaci = txtCpfPaci.Text.Replace(".", "").Replace("-", "");

                var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                           where tb07.NU_CPF_ALU == cpfPaci
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

                    res.ImageReference.Load();
                    upImagemAluno.ImagemLargura = 70;
                    upImagemAluno.ImagemAltura = 85;

                    if (res.Image != null)
                        upImagemAluno.CarregaImagem(res.Image.ImageId);
                    else
                        upImagemAluno.CarregaImagem(0);
                }
            }
            else
            {
                decimal nispaci = decimal.Parse(txtNuNisPaci.Text);

                var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                           where tb07.NU_NIS == nispaci
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

                    res.ImageReference.Load();
                    upImagemAluno.ImagemLargura = 70;
                    upImagemAluno.ImagemAltura = 85;

                    if (res.Image != null)
                        upImagemAluno.CarregaImagem(res.Image.ImageId);
                    else
                        upImagemAluno.CarregaImagem(0);
                }
            }
            //ExecutaJavaScript();
        }

        /// <summary>
        /// Devido ao método de reload na grid de Pré-Atendimento, ela perde a seleção do checkbox, este método coleta 
        /// </summary>
        private void selecionaGridPreAtend()
        {
            CheckBox chk;
            string coPreAtend;
            // Valida se a grid de atividades possui algum registro
            if (grdAgendaPlantoes.Rows.Count != 0)
            {
                // Passa por todos os registros da grid de atividades
                foreach (GridViewRow linha in grdAgendaPlantoes.Rows)
                {
                    coPreAtend = ((HiddenField)linha.Cells[0].FindControl("hidCoPreAtend")).Value;
                    int coPre = (int)HttpContext.Current.Session["VL_PreAtend"];

                    if (int.Parse(coPreAtend) == coPre)
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

            HttpContext.Current.Session.Remove("FL_Select_Grid");
            HttpContext.Current.Session.Remove("VL_PreAtend");
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
            HttpContext.Current.Session.Remove("VL_PreAtend");
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
        private void GridPreAtendimentoSelecionada(string co_tipo_risco, int coPreAtend, int coEspec)
        {
            //Atribui a mesma classificação de risco inserida no pré-atendimento, o usuário altera conforme interesse.
            ddlClassRisco.SelectedValue = co_tipo_risco;
            //switch (co_tipo_risco)
            //{
            //    case "1":
            //        sty.InnerText = ".divClassPri {background-color: Red;}";
            //        break;
            //    case "2":
            //        sty.InnerText = ".divClassPri {background-color: Orange;}";
            //        break;
            //    case "3":
            //        sty.InnerText = ".divClassPri {background-color: Yellow;}";
            //        break;
            //    case "4":
            //        sty.InnerText = ".divClassPri {background-color: Green;}";
            //        break;
            //    case "5":
            //        sty.InnerText = ".divClassPri {background-color: Blue;}";
            //        break;
            //}
            //Guarda a FLAG para saber se o Checkbox está sendo clicado para marcar ou desmarcar, neste caso, ele grava como para marcar.
            HttpContext.Current.Session.Add("FL_Select_Grid", "S");

            //Guarda o Valor do Pré-Atendimento, para fins de posteriormente comparar este valor 
            HttpContext.Current.Session.Add("VL_PreAtend", coPreAtend);

            CarregaResp(coPreAtend);
            UpdatePanel2.Update();
            //UpdatePanel3.Update();

            //Carrega as informações de pré-atendimento nos campos como facilitador
            ddlPesqEspec.SelectedValue = ddlEspec.SelectedValue = coEspec.ToString();
            int coDepto = ddlPesqLocal.SelectedValue != "" ? int.Parse(ddlPesqLocal.SelectedValue) : 0;

            carregaGridMedicosPlantonistas(coEspec, coDepto);
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
            //ddlEspec.SelectedValue = coEspec;
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

        #endregion

        #region Funções de Campo

        protected void imgPesqGrid_OnClick(object sender, EventArgs e)
        {
            carregaGridPreAtendimento();
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
                    int idPreAtend = int.Parse((((HiddenField)linha.Cells[0].FindControl("hidCoPreAtend")).Value));

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

                            int coPreAtend = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidCoPreAtend")).Value);
                            int coEspec = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidCoEspec")).Value);
                            string co_tipo_risco = ((HiddenField)linha.Cells[0].FindControl("hidCoRisco")).Value;

                            GridPreAtendimentoSelecionada(co_tipo_risco, coPreAtend, coEspec);
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
            int coEspec = ddlPesqEspec.SelectedValue != "" ? int.Parse(ddlPesqEspec.SelectedValue) : 0;
            int coDepto = ddlPesqLocal.SelectedValue != "" ? int.Parse(ddlPesqLocal.SelectedValue) : 0;

            carregaGridPreAtendimento();
            carregaGridMedicosPlantonistas(coEspec, coDepto);

            //Verifica se a grid já possuia um registro selecionado antes, e chama um método responsável por selecioná-lo de novo no PRÉ-ATENDIMENTO
            if ((string)HttpContext.Current.Session["FL_Select_Grid"] == "S")
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

            if(chkPaciEhResp.Checked)
                chkPaciMoraCoResp.Checked = true;
        }

        protected void imgCpfResp_OnClick(object sender, EventArgs e)
        {
            PesquisaCarregaResp(null);
            //ExecutaJavaScript();
        }

        protected void imbPesqPaci_OnClick(object sender, EventArgs e)
        {
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

        protected void chkPesqCPFUsu_OnCheckedChanged(object sender, EventArgs e)
        {
            hidCoPac.Value = "";

            if (chkPesqCPFUsu.Checked == true)
                chkPesqNuNisPac.Checked = false;

            //ExecutaJavaScript();
        }

        /// <summary>
        /// Desmarca todos os checkbox com exceção do que foi clicado por último, para garantir que apenas um seja clicado( DA GRID DE PRÉ-ATENDIMENTOS).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                            coPreAtend = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidCoPreAtend")).Value);
                            coEspec = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidCoEspec")).Value);
                            string co_tipo_risco = ((HiddenField)linha.Cells[0].FindControl("hidCoRisco")).Value;

                            GridPreAtendimentoSelecionada(co_tipo_risco, coPreAtend, coEspec);
                        }
                        else
                            GridPreAtendimentoDesmarcada();
                    }
                }
            }
        }

        /// <summary>
        /// Desmarca todos os checkbox com exceção do que foi clicado por último, para garantir que apenas um seja clicado (DA GRID DE MÉDICOS DE PLANTÃO).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        protected void txtCpfPaci_OnTextChanged(object sender, EventArgs e)
        {
            //hidCoPac.Value = "";
            //UpdatePanel2.Update();
        }

        protected void chkPesqNuNisPac_OnCheckedChanged(object sender, EventArgs e)
        {
            if (chkPesqNuNisPac.Checked)
                chkPesqCPFUsu.Checked = false;

            UpdatePanel2.Update();
        }

        protected void imgPesqGridMedic_OnClick(object sender, EventArgs e)
        {
            int coEspec = ddlPesqEspec.SelectedValue != "" ? int.Parse(ddlPesqEspec.SelectedValue) : 0;
            int coDepto = ddlPesqLocal.SelectedValue != "" ? int.Parse(ddlPesqLocal.SelectedValue) : 0;
            carregaGridMedicosPlantonistas(coEspec, coDepto);
            updProfiPlantao.Update();
        }

        #endregion
    }
}