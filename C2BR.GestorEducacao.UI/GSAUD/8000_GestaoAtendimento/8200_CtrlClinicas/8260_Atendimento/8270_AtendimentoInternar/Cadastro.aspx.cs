//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PGS - Portal Gestor Saúde
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: Atendimento
// SUBMÓDULO: Atendimento Internar
// DATA DE CRIAÇÃO: 12/01/2017
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//12/01/2017| Samira Lira                | Criação da página para atendimento no processo de internação hospitalar


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

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8260_Atendimento._8270_AtendimentoInternar
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

                AuxiliCarregamentos.CarregaUFs(ddlUF, false, LoginAuxili.CO_EMP);
                AuxiliCarregamentos.CarregaUFs(ddlUFOrgEmis, false, LoginAuxili.CO_EMP);

                IniPeri.Text = DateTime.Now.Date.ToString();
                FimPeri.Text = DateTime.Now.Date.AddDays(1).ToString();

                carregaGridPacientesInternacao();

                carregaCidade();
                carregaBairro();
                carregaOperadoraPL();
                carregaPlano();
                carregarCaraterInter();
                carregarTipoInter();
                carregarRegimeInter();
                carregarTipoAcomodacaoInter();
                carregarTipoDoencaInter();
                carregarIndicacaoAcidenteInter();

                chkPesqCPFUsu.Checked = true;
            }
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
            SalvarEntidades();
        }

        #endregion

        #region Carregamentos

        /// <summary>
        /// Método responsável por salvar as entidades.
        /// </summary>
        private void SalvarEntidades()
        {
            try
            {
                //Recupera o ID do Pré-Atendimento da grid selecionada.
                int idAtendInter = (HttpContext.Current.Session["VL_IdAtendInter"]) != null ? (int)HttpContext.Current.Session["VL_IdAtendInter"] : 0;
                int idAtendAgend = (HttpContext.Current.Session["VL_IdAtendAgend"]) != null ? (int)HttpContext.Current.Session["VL_IdAtendAgend"] : 0;
                int coAlu = (HttpContext.Current.Session["VL_CO_ALU"]) != null ? (int)HttpContext.Current.Session["VL_CO_ALU"] : 0;
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

                //if (txtNuNisPaci.Text == "")
                //{ AuxiliPagina.EnvioMensagemErro(this.Page, "O Número do NIS do Paciente é Requerido"); erros = true; }

                //if (txtCpfPaci.Text == "")
                //{ AuxiliPagina.EnvioMensagemErro(this.Page, "O Número do CPF do Paciente é Requerido"); erros = true; }

                if (string.IsNullOrEmpty(drpClassRiscoInternar.SelectedValue))
                { AuxiliPagina.EnvioMensagemErro(this.Page, "A Prioridade da Internação é Requerida"); erros = true; }

                if (string.IsNullOrEmpty(ddlCaraterInternar.SelectedValue))
                { AuxiliPagina.EnvioMensagemErro(this.Page, "O Caráter da Internação é Requerida"); erros = true; }

                if (string.IsNullOrEmpty(ddlTipoInternar.SelectedValue))
                { AuxiliPagina.EnvioMensagemErro(this.Page, "O Tipo da Internação é Requerida"); erros = true; }

                if (string.IsNullOrEmpty(ddlRegimeInternar.SelectedValue))
                { AuxiliPagina.EnvioMensagemErro(this.Page, "O Regime da Internação é Requerida"); erros = true; }

                if (string.IsNullOrEmpty(txtDS.Text))
                { AuxiliPagina.EnvioMensagemErro(this.Page, "A quantidade de Diárias Solicitadas (DS) é Requerida"); erros = true; }

                if (string.IsNullOrEmpty(drpTipoDoenca.SelectedValue))
                { AuxiliPagina.EnvioMensagemErro(this.Page, "O Tipo de Doença é Requerido"); erros = true; }

                if (string.IsNullOrEmpty(txtTDRP.Text))
                { AuxiliPagina.EnvioMensagemErro(this.Page, "O tempo de doença referida pelo paciente (TDRP) é Requerido"); erros = true; }

                if (string.IsNullOrEmpty(drpTDRP.SelectedValue))
                { AuxiliPagina.EnvioMensagemErro(this.Page, "O tipo de tempo de doença referida pelo paciente (TDRP) é Requerido"); erros = true; }

                if (string.IsNullOrEmpty(drpIndicacaoAcidente.SelectedValue))
                { AuxiliPagina.EnvioMensagemErro(this.Page, "A Indicaçõa de Acidente é Requerida"); erros = true; }

                if (erros != true)
                {

                    //Salva os dados do Responsável na tabela 108
                    #region Salva Responsável na tb108

                    TB108_RESPONSAVEL tb108;
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
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar um Responsável para dar continuidade no registro.");
                            return;
                        }

                        int coRe = int.Parse(hidCoResp.Value);
                        tb108 = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(coRe);
                    }

                    #endregion

                    //Salva os dados do Usuário em um registro na tb07
                    #region Salva o Usuário na TB07

                    TB07_ALUNO tb07;
                    var _tb07 = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.CO_ALU == coAlu).FirstOrDefault();
                    tb07 = _tb07 != null ? _tb07 : new TB07_ALUNO();

                    #region Bloco foto
                    int codImagem = upImagemAluno.GravaImagem();
                    tb07.Image = C2BR.GestorEducacao.BusinessEntities.MSSQL.Image.RetornaPelaChavePrimaria(codImagem);
                    #endregion

                    tb07.NO_ALU = txtnompac.Text;
                    chkPesqCPFUsu.Checked = true;
                    tb07.NU_CPF_ALU = txtCpfPaci.Text.Replace(".", "").Replace("-", "").Trim();
                    tb07.NU_NIS = !string.IsNullOrEmpty(txtNuNisPaci.Text) ? decimal.Parse(txtNuNisPaci.Text) : (decimal?)null;
                    tb07.DT_NASC_ALU = DateTime.Parse(txtDtNascPaci.Text);
                    tb07.CO_SEXO_ALU = ddlSexoPaci.SelectedValue;
                    tb07.NU_TELE_CELU_ALU = txtTelCelPaci.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                    tb07.NU_TELE_RESI_ALU = txtTelResPaci.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                    tb07.NU_TELE_WHATS_ALU = txtNuWhatsResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                    tb07.CO_GRAU_PAREN_RESP = ddlGrParen.SelectedValue;
                    tb07.NO_EMAIL_PAI = txtEmailPaci.Text;
                    tb07.TB25_EMPRESA1 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                    tb07.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                    tb07.TB108_RESPONSAVEL = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(tb108.CO_RESP);
                    tb07.FL_LIST_ESP = "N";

                    if (chkPaciMoraCoResp.Checked)
                    {
                        tb07.CO_CEP_ALU = txtCEP.Text;
                        tb07.CO_ESTA_ALU = ddlUF.SelectedValue;
                        tb07.TB905_BAIRRO = TB905_BAIRRO.RetornaPelaChavePrimaria(int.Parse(ddlBairro.SelectedValue));
                        tb07.DE_ENDE_ALU = txtLograEndResp.Text;
                    }

                    //H = internação (o paciente passa a situação de internado e não aparece mais neste registro e nem no atendimento)
                    tb07.CO_SITU_ALU = "H";
                    tb07.TP_DEF = "N";

                    #region trata para criação do nire

                    if (_tb07 == null)
                    {
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

                        tb07.NU_NIRE = nirTot;
                    }

                    #endregion

                    TB07_ALUNO.SaveOrUpdate(tb07, true);

                    #endregion

                    #region Salvar registro de internação

                    TBS451_INTER_REGIST tbs451 = new TBS451_INTER_REGIST();

                    tbs451.CO_COL_INTER =  LoginAuxili.CO_COL;
                    tbs451.CO_EMP_INTER = LoginAuxili.CO_EMP;
                    tbs451.DT_INTER = DateTime.Parse(txtDataProvavelAH.Text);
                    tbs451.HR_INTER = TimeSpan.Parse(DateTime.Now.ToShortTimeString());
                    tbs451.CO_PRIOR_INTER = drpClassRiscoInternar.SelectedValue;
                    tbs451.TBS442_TIPO_CARAT = TBS442_TIPO_CARAT.RetornaPelaChavePrimaria(int.Parse(ddlCaraterInternar.SelectedValue));
                    tbs451.TBS443_TIPO_INTER = TBS443_TIPO_INTER.RetornaPelaChavePrimaria(int.Parse(ddlTipoInternar.SelectedValue));
                    tbs451.TBS444_TIPO_REGIM = TBS444_TIPO_REGIM.RetornaPelaChavePrimaria(int.Parse(ddlRegimeInternar.SelectedValue));
                    tbs451.QT_INDIC_DIAS_INTER = int.Parse(txtDS.Text);
                    tbs451.DE_OBSER_INTER = txtIndicacaoClinica.Text;
                    tbs451.TB14_DEPTO = !string.IsNullOrEmpty(ddlTipoAcomodacao.SelectedValue) ? TB14_DEPTO.RetornaPelaChavePrimaria(int.Parse(ddlTipoAcomodacao.SelectedValue)) : null;
                    tbs451.TBS448_ATEND_INTER_HOSPI = TBS448_ATEND_INTER_HOSPI.RetornaPelaChavePrimaria(idAtendInter);
                    tbs451.TBS446_TIPO_DOENCA = TBS446_TIPO_DOENCA.RetornaPelaChavePrimaria(int.Parse(drpTipoDoenca.SelectedValue));
                    tbs451.QT_TEMPO_DOENC_PACIE = int.Parse(txtTDRP.Text);
                    tbs451.TP_TEMPO_DOENC_PACIE = drpTDRP.SelectedValue;
                    tbs451.TBS445_TIPO_INDIC_ACIDE = TBS445_TIPO_INDIC_ACIDE.RetornaPelaChavePrimaria(int.Parse(drpIndicacaoAcidente.SelectedValue));
                    tbs451.CO_SITUA_INTER = "A";
                        tbs451.DT_SITUA_INTER = DateTime.Now;
                        tbs451.CO_EMP_USUAR_LOGAD = LoginAuxili.CO_EMP;
                        var res2 = (from tbs390pesq in TBS390_ATEND_AGEND.RetornaTodosRegistros()
                                    select new { tbs390pesq.NU_REGIS }).OrderByDescending(w => w.NU_REGIS).FirstOrDefault();

                        string seq;
                        int seq2;
                        int seqConcat;
                        string seqcon;
                        string ano = DateTime.Now.Year.ToString().Substring(2, 2);
                        string mes = DateTime.Now.Month.ToString().PadLeft(2, '0');

                        if (res2 == null)
                            seq2 = 1;
                        else
                        {
                            seq = res2.NU_REGIS.Substring(6, 6);
                            seq2 = int.Parse(seq);
                        }

                        seqConcat = seq2 + 1;
                        seqcon = seqConcat.ToString().PadLeft(6, '0');

                        string CodigoAtendimento = string.Format("IN{0}{1}{2}", ano, mes, seqcon);
                        tbs451.NU_REGIS = CodigoAtendimento;
                    TBS451_INTER_REGIST.SaveOrUpdate(tbs451, true);

                    var tbs452 = TBS452_INTER_PROCE.RetornaTodosRegistros().Where(x => x.TBS390_ATEND_AGEND.ID_ATEND_AGEND == idAtendAgend).ToList();
                    foreach (var t in tbs452)
                    {
                        t.TBS451_INTER_REGIST = TBS451_INTER_REGIST.RetornaPelaChavePrimaria(tbs451.ID_INTER_REGIS);
                        TBS452_INTER_PROCE.SaveOrUpdate(t, true);
                    }

                    var tbs450 = TBS450_ATEND_INTER_PROCE_MEDIC.RetornaTodosRegistros().Where(x => x.TBS448_ATEND_INTER_HOSPI.ID_ATEND_INTER == idAtendInter).ToList();
                    foreach (var t in tbs450)
                    {
                        var _tbs452 = new TBS452_INTER_PROCE();
                        _tbs452.CO_TIPO_REGIS_PROCE = t.CO_TIPO_REGIS_PROCE;
                        _tbs452.DE_OBSER_PROCE_INTER = t.DE_OBSER_PROCE_INTER;
                        _tbs452.FABRI_OPM = t.FABRI_OPM;
                        _tbs452.ID_INTER_PROCE = t.ID_ATEND_INTER_PROCE;
                        _tbs452.QT_PROCE_INTER = t.QT_PROCE_INTER;
                        t.TBS356_PROC_MEDIC_PROCEReference.Load();
                        _tbs452.TBS356_PROC_MEDIC_PROCE = t.TBS356_PROC_MEDIC_PROCE;
                        t.TBS448_ATEND_INTER_HOSPIReference.Load();
                        t.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGENDReference.Load();
                        _tbs452.TBS390_ATEND_AGEND = t.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND;
                        _tbs452.TBS451_INTER_REGIST = TBS451_INTER_REGIST.RetornaPelaChavePrimaria(tbs451.ID_INTER_REGIS);
                        TBS452_INTER_PROCE.SaveOrUpdate(_tbs452, true);
                    }

                    var tbs453 = TBS453_INTER_CID.RetornaTodosRegistros().Where(x => x.TBS390_ATEND_AGEND.ID_ATEND_AGEND == idAtendAgend).ToList();
                    foreach (var t in tbs453)
                    {
                        t.TBS451_INTER_REGIST = TBS451_INTER_REGIST.RetornaPelaChavePrimaria(tbs451.ID_INTER_REGIS);
                        TBS453_INTER_CID.SaveOrUpdate(t, true);
                    }

                    var tbs449 = TBS449_ATEND_INTER_CID.RetornaTodosRegistros().Where(x => x.TBS448_ATEND_INTER_HOSPI.ID_ATEND_INTER == idAtendInter).ToList();
                    foreach (var t in tbs449)
                    {
                        var _tbs449 = new TBS453_INTER_CID();
                        _tbs449.DE_OBSER_CID_INTER = t.DE_OBSER_CID_INTER;
                        _tbs449.ID_INTER_CID = t.ID_ATEND_INTER_CID;
                        _tbs449.IS_CID_PRINC = t.IS_CID_PRINC;
                        _tbs449.IS_ITEM_APLIC = t.IS_ITEM_APLIC;
                        t.TB117_CODIGO_INTERNACIONAL_DOENCAReference.Load();
                        _tbs449.TB117_CODIGO_INTERNACIONAL_DOENCA = t.TB117_CODIGO_INTERNACIONAL_DOENCA;
                        t.TBS434_PROTO_CIDReference.Load();
                        _tbs449.TBS434_PROTO_CID = t.TBS434_PROTO_CID;
                        t.TBS436_ITEM_PROTO_CIDReference.Load();
                        _tbs449.TBS436_ITEM_PROTO_CID = t.TBS436_ITEM_PROTO_CID;
                        t.TBS448_ATEND_INTER_HOSPIReference.Load();
                        t.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGENDReference.Load();
                        _tbs449.TBS390_ATEND_AGEND = t.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND;
                        _tbs449.TBS451_INTER_REGIST = TBS451_INTER_REGIST.RetornaPelaChavePrimaria(tbs451.ID_INTER_REGIS);
                        TBS453_INTER_CID.SaveOrUpdate(_tbs449, true);
                    }

                    #endregion

                    AuxiliPagina.RedirecionaParaPaginaSucesso("Registro de internação efetuado com sucesso.", Request.Url.AbsoluteUri);
                }
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Não foi possível concluir a operação, por favor tente novamente ou entre em contato com o suporte. Erro: " + ex.Message);
                return;
            }
        }

        ///// <summary>
        ///// Carrega a Grid de Registros do Pré-Atendimento
        ///// </summary>
        private void carregaGridPacientesInternacao()
        {
            //string nomePac = txtNomePacPesq.Text;
            DateTime dtIni = (!string.IsNullOrEmpty(IniPeri.Text) ? DateTime.Parse(IniPeri.Text) : DateTime.Now);
            DateTime dtFim = (!string.IsNullOrEmpty(FimPeri.Text) ? DateTime.Parse(FimPeri.Text) : DateTime.Now);

            var res = TBS448_ATEND_INTER_HOSPI.RetornaTodosRegistros()
                        .Join(TBS390_ATEND_AGEND.RetornaTodosRegistros(), a => a.TBS390_ATEND_AGEND.ID_ATEND_AGEND, b => b.ID_ATEND_AGEND, (a, b) => new { a, b })
                        .Join(TB03_COLABOR.RetornaTodosRegistros(), c => c.a.CO_COL_SOLIC_INTER, d => d.CO_COL, (c, d) => new { c, d })
                        .Join(TB63_ESPECIALIDADE.RetornaTodosRegistros(), e => e.d.CO_ESPEC, f => f.CO_ESPECIALIDADE, (e, f) => new { e, f })
                        .Where(x => (x.e.c.a.DT_SOLIC_INTER >= dtIni) && (x.e.c.a.DT_SOLIC_INTER <= dtFim)                                
                                && x.e.c.b.TB07_ALUNO.CO_SITU_ALU.Equals("E"))
                        .Select(w => new AtendimentoInternar
                        {
                            ID_ATEND_INTER = w.e.c.a.ID_ATEND_INTER,
                            ID_ATEND_AGEND = w.e.c.b.ID_ATEND_AGEND,
                            NOME_PACIENTE = w.e.c.b.TB07_ALUNO.NO_ALU,
                            NU_REGIS_INTER = w.e.c.a.NU_REGIS_INTER,
                            dtProvavel = w.e.c.a.DT_PREVI_INTER,
                            dtSolic = w.e.c.a.DT_SOLIC_INTER,
                            SEXO = w.e.c.b.TB07_ALUNO.CO_SEXO_ALU,
                            prioridade = w.e.c.a.CO_PRIOR_INTER,
                            NOME_RESP = w.e.c.b.TB07_ALUNO.TB108_RESPONSAVEL.NO_APELIDO_RESP,
                            NOME_COL_SOL = w.e.d.NO_APEL_COL,
                            ESPE_COL_SOL = w.f.NO_ESPECIALIDADE,
                            TELE_COL_SOL = w.e.d.NU_TELE_CELU_COL
                        });

            grdPacientesInternacao.DataSource = res;
            grdPacientesInternacao.DataBind();

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

        private void carregarCaraterInter()
        {
            var tbs442 = TBS442_TIPO_CARAT.RetornaTodosRegistros()
                            .Select(x => new
                            {
                                x.ID_CARAT,
                                x.NO_CARAT
                            }).OrderBy(x => x.NO_CARAT);

            ddlCaraterInternar.DataSource = tbs442;
            ddlCaraterInternar.DataTextField = "NO_CARAT";
            ddlCaraterInternar.DataValueField = "ID_CARAT";
            ddlCaraterInternar.DataBind();
            ddlCaraterInternar.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void carregarTipoInter()
        {
            var tbs443 = TBS443_TIPO_INTER.RetornaTodosRegistros()
                            .Select(x => new
                            {
                                x.ID_TP_INTER,
                                x.NO_TP_INTER
                            }).OrderBy(x => x.NO_TP_INTER);

            ddlTipoInternar.DataSource = tbs443;
            ddlTipoInternar.DataTextField = "NO_TP_INTER";
            ddlTipoInternar.DataValueField = "ID_TP_INTER";
            ddlTipoInternar.DataBind();
            ddlTipoInternar.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void carregarRegimeInter()
        {
            var tbs444 = TBS444_TIPO_REGIM.RetornaTodosRegistros()
                            .Select(x => new
                            {
                                x.ID_TP_REGIM,
                                x.NO_TP_REGIM
                            }).OrderBy(x => x.NO_TP_REGIM);

            ddlRegimeInternar.DataSource = tbs444;
            ddlRegimeInternar.DataTextField = "NO_TP_REGIM";
            ddlRegimeInternar.DataValueField = "ID_TP_REGIM";
            ddlRegimeInternar.DataBind();
            ddlRegimeInternar.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void carregarTipoAcomodacaoInter()
        {
            ddlTipoAcomodacao.DataSource = TB14_DEPTO.RetornaTodosRegistros().Where(x => x.FL_INTER.Equals("S") || x.FL_UTI.Equals("S")).OrderBy(x => x.NO_DEPTO);
            ddlTipoAcomodacao.DataTextField = "NO_DEPTO";
            ddlTipoAcomodacao.DataValueField = "CO_DEPTO";
            ddlTipoAcomodacao.DataBind();
            ddlTipoAcomodacao.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void carregarTipoDoencaInter()
        {
            drpTipoDoenca.DataSource = TBS446_TIPO_DOENCA.RetornaTodosRegistros().OrderBy(x => x.NO_TP_DOENCA);
            drpTipoDoenca.DataTextField = "NO_TP_DOENCA";
            drpTipoDoenca.DataValueField = "ID_TP_DOENCA";
            drpTipoDoenca.DataBind();
            drpTipoDoenca.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void carregarIndicacaoAcidenteInter()
        {
            drpIndicacaoAcidente.DataSource = TBS445_TIPO_INDIC_ACIDE.RetornaTodosRegistros().OrderBy(x => x.NO_TP_ACIDE);
            drpIndicacaoAcidente.DataTextField = "NO_TP_ACIDE";
            drpIndicacaoAcidente.DataValueField = "ID_TP_ACIDE";
            drpIndicacaoAcidente.DataBind();
            drpIndicacaoAcidente.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Carrega as Informações de Responsável e Paciente, de acordo com o registro que é clicado na Grid de Pré-Atendimentos.
        /// </summary>
        /// <param name="ID_PRE_ATEND"></param>
        private void CarregaResp(int ID_ATEND_AGEND)
        {
            var tbs390 = TBS390_ATEND_AGEND.RetornaPelaChavePrimaria(ID_ATEND_AGEND);
            tbs390.TB07_ALUNOReference.Load();
            tbs390.TB07_ALUNO.TB108_RESPONSAVELReference.Load();
            HttpContext.Current.Session.Add("VL_CO_ALU", tbs390.TB07_ALUNO.CO_ALU);
            HttpContext.Current.Session.Add("VL_IdAtendAgend", ID_ATEND_AGEND);
            hidCoPac.Value = tbs390.TB07_ALUNO.CO_ALU.ToString();
            //chkEncaComPreAtend.Checked = true;
            txtCPFResp.Text = tbs390.TB07_ALUNO.TB108_RESPONSAVEL.NU_CPF_RESP;
            txtNomeResp.Text = tbs390.TB07_ALUNO.TB108_RESPONSAVEL.NO_RESP;
            txtDtNascResp.Text = tbs390.TB07_ALUNO.TB108_RESPONSAVEL.DT_NASC_RESP.ToString();
            ddlSexResp.SelectedValue = tbs390.TB07_ALUNO.TB108_RESPONSAVEL.CO_SEXO_RESP;

            txtTelFixResp.Text = tbs390.TB07_ALUNO.TB108_RESPONSAVEL.NU_TELE_RESI_RESP;
            txtTelCelResp.Text = tbs390.TB07_ALUNO.TB108_RESPONSAVEL.NU_TELE_CELU_RESP;
            txtTelResPaci.Text = tbs390.TB07_ALUNO.NU_TELE_RESI_ALU;
            txtTelCelPaci.Text = tbs390.TB07_ALUNO.NU_TELE_CELU_ALU;

            chkPesqCPFUsu.Checked = true;
            txtCpfPaci.Text = tbs390.TB07_ALUNO.NU_CPF_ALU;
            txtNuNisPaci.Text = tbs390.TB07_ALUNO.NU_NIS.ToString();
            txtDtNascPaci.Text = tbs390.TB07_ALUNO.DT_NASC_ALU.ToString();
            ddlSexoPaci.SelectedValue = tbs390.TB07_ALUNO.CO_SEXO_ALU;
            ddlGrParen.SelectedValue = tbs390.TB07_ALUNO.CO_GRAU_PAREN_RESP;
            txtnompac.Text = tbs390.TB07_ALUNO.NO_ALU;

            upImagemAluno.ImagemLargura = 70;
            upImagemAluno.ImagemAltura = 85;
            upImagemAluno.MostraProcurar = false;

            tbs390.TB07_ALUNO.TB108_RESPONSAVEL.TB250_OPERAReference.Load();
            if (tbs390.TB07_ALUNO.TB108_RESPONSAVEL.TB250_OPERA != null)
            {
                ddlOperPlano.SelectedValue = tbs390.TB07_ALUNO.TB108_RESPONSAVEL.TB250_OPERA.ID_OPER.ToString();
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

            PesquisaCarregaResp(tbs390.TB07_ALUNO.TB108_RESPONSAVEL.CO_RESP);

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

        ///// <summary>
        ///// Pesquisa se já existe um responsável com o CPF informado na tabela de responsáveis, se já existe ele preenche as informações do responsável 
        ///// </summary>
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
            //ExecutaJavaScript();

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
            ExecutaJavaScript();
        }

        /// <summary>
        /// Devido ao método de reload na grid de Pré-Atendimento, ela perde a seleção do checkbox, este método coleta 
        /// </summary>
        private void selecionaGridPreAtend()
        {
            CheckBox chk;
            string coPreAtend;
            // Valida se a grid de atividades possui algum registro
            if (grdPacientesInternacao.Rows.Count != 0)
            {
                // Passa por todos os registros da grid de atividades
                foreach (GridViewRow linha in grdPacientesInternacao.Rows)
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
        /// É o método usado para desmarcar a grid de pré-atendimento e limpar as variáveis de apoio referentes à ela
        /// </summary>
        private void DesmarcaPacienteInternacao()
        {
            foreach (GridViewRow li in grdPacientesInternacao.Rows)
            {
                CheckBox chk = (((CheckBox)li.Cells[0].FindControl("chkselect")));
                chk.Checked = false;
            }

            HttpContext.Current.Session.Remove("FL_Select_Grid");
            HttpContext.Current.Session.Remove("VL_IdAtendAgend");
            HttpContext.Current.Session.Remove("VL_CO_ALU");
            //chkEncaComPreAtend.Checked = false;
            UpdatePanel1.Update();
        }

        /// <summary>
        /// Responsável por executar as funções padrões para quando um registro do Pré-Atendimento for desmarcado pelo usuário
        /// </summary>
        private void GridPacienteInternacaoDesmarcada()
        {
            //Guarda a FLAG para saber se o Checkbox está sendo clicado para marcar ou desmarcar, neste caso, ele grava como desmarcada.
            HttpContext.Current.Session.Add("FL_Select_Grid", "N");
            HttpContext.Current.Session.Remove("VL_IdAtendAgend");
            HttpContext.Current.Session.Remove("VL_CO_ALU");
            LimpaCampos();
        }

        ///// <summary>
        ///// Métodos padrões à serem chamados quando uma linha da grid de pré-atendimento for selecionada
        ///// </summary>
        private void GridPacienteInternacaoSelecionada(int idAtendAgend)
        {
            updInfoPlaSaude.Update();
            HttpContext.Current.Session.Remove("FL_Select_Grid");
            HttpContext.Current.Session.Remove("VL_IdAtendAgend");
            HttpContext.Current.Session.Remove("VL_CO_ALU");

            CarregaResp(idAtendAgend);
            UpdatePanel2.Update();
            ExecutaJavaScript();
        }

        private void carregarDadosInternacao(int idAtendInter, int idAtendAgend)
        {
            HttpContext.Current.Session.Add("VL_IdAtendInter", idAtendInter);

            var tbs448 = TBS448_ATEND_INTER_HOSPI.RetornaTodosRegistros()
                           .Where(x => x.ID_ATEND_INTER == idAtendInter)
                           .Select(w => new EncaminhamentoInternar
                           {
                               //dados Internacao
                               nuRegistroInter = w.NU_REGIS_INTER,
                               coPrioridadeInter = w.CO_PRIOR_INTER,
                               caraterInter = w.TBS442_TIPO_CARAT.ID_CARAT,
                               tipoInter = w.TBS443_TIPO_INTER.ID_TP_INTER,
                               regimeInter = w.TBS444_TIPO_REGIM.ID_TP_REGIM,
                               diariasSolicitadas = w.QT_INDIC_DIAS_INTER,
                               indicacaoClinica = w.DE_OBSER_INTER,
                               dtInter = w.DT_PREVI_INTER,
                               tipoAcomodacao = w.TB14_DEPTO.CO_DEPTO,

                               //Hipóstese diagnóstica
                               tipoDoenca = w.TBS446_TIPO_DOENCA.ID_TP_DOENCA,
                               qtdeTempoDoenca = w.QT_TEMPO_DOENC_PACIE,
                               tipoTempoDoenca = w.TP_TEMPO_DOENC_PACIE,
                               tipoAcidente = w.TBS445_TIPO_INDIC_ACIDE.ID_TP_ACIDE

                           }).FirstOrDefault();

            lblNRegistro.Text = tbs448.nuRegistroInter;
            txtDS.Text = tbs448.diariasSolicitadas.ToString();
            txtIndicacaoClinica.Text = tbs448.indicacaoClinica;
            txtDataProvavelAH.Text = tbs448.dataAH;
            txtTDRP.Text = tbs448.qtdeTempoDoenca.ToString();

            drpClassRiscoInternar.SelectedValue = tbs448.coPrioridadeInter.ToString();
            ddlCaraterInternar.SelectedValue = tbs448.caraterInter.ToString();
            ddlTipoInternar.SelectedValue = tbs448.tipoInter.ToString();
            ddlRegimeInternar.SelectedValue = tbs448.regimeInter.ToString();
            ddlTipoAcomodacao.SelectedValue = tbs448.tipoAcomodacao.ToString();
            drpTipoDoenca.SelectedValue = tbs448.tipoDoenca.ToString();
            drpTDRP.SelectedValue = tbs448.tipoTempoDoenca;
            drpIndicacaoAcidente.SelectedValue = tbs448.tipoAcidente.ToString();

            #region CID

            var tbs449 = TBS449_ATEND_INTER_CID.RetornaTodosRegistros()
                                                .Where(x => x.TBS448_ATEND_INTER_HOSPI.ID_ATEND_INTER == idAtendInter).OrderBy(x => x.TB117_CODIGO_INTERNACIONAL_DOENCA.CO_CID); ;

            List<CIDInternar> listCIDInter = new List<CIDInternar>();
            foreach (var item in tbs449)
            {
                item.TB117_CODIGO_INTERNACIONAL_DOENCAReference.Load();
                var cid = new CIDInternar();
                cid.idCID = item.TB117_CODIGO_INTERNACIONAL_DOENCA.IDE_CID;
                cid.idInterCID = item.ID_ATEND_INTER_CID;
                cid.coCID = item.TB117_CODIGO_INTERNACIONAL_DOENCA.CO_CID;
                cid.nomeCID = item.TB117_CODIGO_INTERNACIONAL_DOENCA.NO_CID;
                cid.descCID = item.TB117_CODIGO_INTERNACIONAL_DOENCA.DE_CID;
                cid.cidPrincipal = item.IS_CID_PRINC;
                var tbs434 = TBS434_PROTO_CID.RetornaTodosRegistros().Where(x => x.TB117_CODIGO_INTERNACIONAL_DOENCA.IDE_CID == cid.idCID).FirstOrDefault();
                cid.existeProtocolo = (tbs434 != null && tbs434.ID_PROTO_CID > 0) ? true : false;
                listCIDInter.Add(cid);
            }

            var tbs453 = TBS453_INTER_CID.RetornaTodosRegistros()
                                                   .Where(x => x.TBS390_ATEND_AGEND.ID_ATEND_AGEND == idAtendAgend).OrderBy(x => x.TB117_CODIGO_INTERNACIONAL_DOENCA.CO_CID); ;

            foreach (var item in tbs453)
            {
                item.TB117_CODIGO_INTERNACIONAL_DOENCAReference.Load();
                var cid = new CIDInternar();
                cid.idCID = item.TB117_CODIGO_INTERNACIONAL_DOENCA.IDE_CID;
                cid.idInterCID = item.ID_INTER_CID;
                cid.coCID = item.TB117_CODIGO_INTERNACIONAL_DOENCA.CO_CID;
                cid.nomeCID = item.TB117_CODIGO_INTERNACIONAL_DOENCA.NO_CID;
                cid.descCID = item.TB117_CODIGO_INTERNACIONAL_DOENCA.DE_CID;
                cid.cidPrincipal = item.IS_CID_PRINC;
                var tbs434 = TBS434_PROTO_CID.RetornaTodosRegistros().Where(x => x.TB117_CODIGO_INTERNACIONAL_DOENCA.IDE_CID == cid.idCID).FirstOrDefault();
                cid.existeProtocolo = (tbs434 != null && tbs434.ID_PROTO_CID > 0) ? true : false;
                listCIDInter.Add(cid);
            }

            grdCIDInternar.DataSource = listCIDInter.DistinctBy(x => x.coCID);
            grdCIDInternar.DataBind();

            #endregion

            #region Procedimentos

            var tbs356 = TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros().Where(x => x.CO_TIPO_PROC_MEDI.Equals("IN") || x.CO_TIPO_PROC_MEDI.Equals("OP")).Select(x => new { x.NM_PROC_MEDI, x.ID_PROC_MEDI_PROCE, x.CO_TIPO_PROC_MEDI }).OrderBy(x => x.NM_PROC_MEDI);

            ddlTipoProcedimentoInternar.DataSource = tbs356.Where(x => x.CO_TIPO_PROC_MEDI.Equals("IN"));
            ddlTipoProcedimentoInternar.DataTextField = "NM_PROC_MEDI";
            ddlTipoProcedimentoInternar.DataValueField = "ID_PROC_MEDI_PROCE";
            ddlTipoProcedimentoInternar.DataBind();
            ddlTipoProcedimentoInternar.Items.Insert(0, new ListItem("Selecione", ""));

            ddlOPMInternar.DataSource = tbs356.Where(x => x.CO_TIPO_PROC_MEDI.Equals("OP"));
            ddlOPMInternar.DataTextField = "NM_PROC_MEDI";
            ddlOPMInternar.DataValueField = "ID_PROC_MEDI_PROCE";
            ddlOPMInternar.DataBind();
            ddlOPMInternar.Items.Insert(0, new ListItem("Selecione", ""));

            var tbs450 = TBS450_ATEND_INTER_PROCE_MEDIC.RetornaTodosRegistros()
                                                    .Where(x => x.TBS448_ATEND_INTER_HOSPI.ID_ATEND_INTER == idAtendInter && (x.TBS356_PROC_MEDIC_PROCE.CO_TIPO_PROC_MEDI.Equals("OP") || x.TBS356_PROC_MEDIC_PROCE.CO_TIPO_PROC_MEDI.Equals("IN")))
                                                    .Select(x => new
                                                    {
                                                        idProcedimentoOPM = x.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE,
                                                        idInterProcOPM = x.ID_ATEND_INTER_PROCE,
                                                        tipoProcedimentoOPM = x.TBS356_PROC_MEDIC_PROCE.TBS354_PROC_MEDIC_GRUPO.NM_PROC_MEDIC_GRUPO + " - " + x.TBS356_PROC_MEDIC_PROCE.TBS355_PROC_MEDIC_SGRUP.NM_PROC_MEDIC_SGRUP,
                                                        nomeProcedimentoOPM = x.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
                                                        codigoProcedimentoOPM = x.TBS356_PROC_MEDIC_PROCE.CO_PROC_MEDI,
                                                        qtdProcedimentoOPM = x.QT_PROCE_INTER,
                                                        vlUnitarioProcedimentoOPM = x.VL_UNITA_PROCE_INTER,
                                                        vlTotalProcedimentoOPM = x.VL_TOTAL_PROCE_INTER,
                                                        fabricanteOPM = x.FABRI_OPM,
                                                        tipo = x.TBS356_PROC_MEDIC_PROCE.CO_TIPO_PROC_MEDI
                                                    });

            var tbs452 = TBS452_INTER_PROCE.RetornaTodosRegistros()
                                                        .Where(x => x.TBS390_ATEND_AGEND.ID_ATEND_AGEND == idAtendAgend && (x.TBS356_PROC_MEDIC_PROCE.CO_TIPO_PROC_MEDI.Equals("OP") || x.TBS356_PROC_MEDIC_PROCE.CO_TIPO_PROC_MEDI.Equals("IN")))
                                                        .Select(x => new
                                                        {
                                                            idProcedimentoOPM = x.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE,
                                                            idInterProcOPM = x.ID_INTER_PROCE,
                                                            tipoProcedimentoOPM = x.TBS356_PROC_MEDIC_PROCE.TBS354_PROC_MEDIC_GRUPO.NM_PROC_MEDIC_GRUPO + " - " + x.TBS356_PROC_MEDIC_PROCE.TBS355_PROC_MEDIC_SGRUP.NM_PROC_MEDIC_SGRUP,
                                                            nomeProcedimentoOPM = x.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
                                                            codigoProcedimentoOPM = x.TBS356_PROC_MEDIC_PROCE.CO_PROC_MEDI,
                                                            qtdProcedimentoOPM = x.QT_PROCE_INTER,
                                                            vlUnitarioProcedimentoOPM = x.VL_UNITA_PROCE_INTER,
                                                            vlTotalProcedimentoOPM = x.VL_TOTAL_PROCE_INTER,
                                                            fabricanteOPM = x.FABRI_OPM,
                                                            tipo = x.TBS356_PROC_MEDIC_PROCE.CO_TIPO_PROC_MEDI
                                                        });

            List<ProcedimentoInternar> procList = new List<ProcedimentoInternar>();
            foreach (var t in tbs450)
            {
                var p = new ProcedimentoInternar();
                p.codigoProcedimentoOPM = t.codigoProcedimentoOPM;
                p.fabricanteOPM = t.fabricanteOPM;
                p.idInterProcOPM = t.idInterProcOPM;
                p.idProcedimentoOPM = t.idProcedimentoOPM;
                p.nomeProcedimentoOPM = t.nomeProcedimentoOPM;
                p.qtdProcedimentoOPM = t.qtdProcedimentoOPM;
                p.tipo = t.tipo;
                p.tipoProcedimentoOPM = t.tipoProcedimentoOPM;
                p.vlTotalProcedimentoOPM = t.vlTotalProcedimentoOPM;
                p.vlUnitarioProcedimentoOPM = t.vlUnitarioProcedimentoOPM;

                procList.Add(p);
            }

            foreach (var t in tbs452)
            {
                var p = new ProcedimentoInternar();
                p.codigoProcedimentoOPM = t.codigoProcedimentoOPM;
                p.fabricanteOPM = t.fabricanteOPM;
                p.idInterProcOPM = t.idInterProcOPM;
                p.idProcedimentoOPM = t.idProcedimentoOPM;
                p.nomeProcedimentoOPM = t.nomeProcedimentoOPM;
                p.qtdProcedimentoOPM = t.qtdProcedimentoOPM;
                p.tipo = t.tipo;
                p.tipoProcedimentoOPM = t.tipoProcedimentoOPM;
                p.vlTotalProcedimentoOPM = t.vlTotalProcedimentoOPM;
                p.vlUnitarioProcedimentoOPM = t.vlUnitarioProcedimentoOPM;

                procList.Add(p);
            }


            grdProcedimentoInternar.DataSource = procList.Where(x => x.tipo.Equals("IN")).DistinctBy(x => x.idProcedimentoOPM);
            grdProcedimentoInternar.DataBind();

            grdOPM.DataSource = procList.Where(x => x.tipo.Equals("OP")).DistinctBy(x => x.idProcedimentoOPM);
            grdOPM.DataBind();

            #endregion

            UpdatePanel3.Update();
            UpdatePanel5.Update();
            UpdatePanel6.Update();
            UpdatePanel7.Update();
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
                = ddlUF.SelectedValue = txtnompac.Text = txtNuWhatsResp.Text = txtWhatsPaci.Text = txtDeFaceResp.Text = lblNRegistro.Text = drpClassRiscoInternar.SelectedValue
                = ddlCaraterInternar.SelectedValue = ddlTipoInternar.SelectedValue = ddlRegimeInternar.SelectedValue = txtDS.Text = txtIndicacaoClinica.Text = ddlTipoAcomodacao.SelectedValue
                = txtDataProvavelAH.Text = drpTipoDoenca.SelectedValue = txtTDRP.Text = drpTDRP.SelectedValue = drpIndicacaoAcidente.SelectedValue = drpDefCid.SelectedValue
                = ddlTipoProcedimentoInternar.SelectedValue = ddlOPMInternar.SelectedValue = "";

            grdCIDInternar.DataSource = null;
            grdCIDInternar.DataBind();

            grdProcedimentoInternar.DataSource = null;
            grdProcedimentoInternar.DataBind();

            grdOPM.DataSource = null;
            grdOPM.DataBind();

            UpdatePanel2.Update();
            updInfoPlaSaude.Update();
            UpdatePanel3.Update();
            UpdatePanel5.Update();
            UpdatePanel6.Update();
            UpdatePanel7.Update();
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

        private void AbreModalPadrao(string funcao)
        {
            ScriptManager.RegisterStartupScript(
                this.Page,
                this.GetType(),
                "Acao",
                funcao,
                true
            );
        }

        //#endregion

        //#region Funções de Campo

        #endregion

        #region Funções de Campo
        protected void imgPesqGrid_OnClick(object sender, EventArgs e)
        {
            carregaGridPacientesInternacao();
        }

        /// <summary>
        /// É executado a cada 10 segundos para atualizar a grid de PacientesInternacao automaticamente.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void Timer1_Tick(object sender, EventArgs e)
        //{
        //    carregaGridPacientesInternacao();

        //    //Verifica se a grid já possuia um registro selecionado antes, e chama um método responsável por selecioná-lo de novo no PRÉ-ATENDIMENTO
        //    if ((string)HttpContext.Current.Session["FL_Select_Grid"] == "S")
        //    {
        //        //selecionaGridPreAtend();
        //    }
        //    UpdatePanel1.Update();
        //}

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
            ExecutaJavaScript();
        }

        protected void chkPaciEhResp_OnCheckedChanged(object sender, EventArgs e)
        {
            carregaPaciehoResponsavel();
            DesmarcaPacienteInternacao();

            if (chkPaciEhResp.Checked)
                chkPaciMoraCoResp.Checked = true;
        }

        protected void imgCpfResp_OnClick(object sender, EventArgs e)
        {
            PesquisaCarregaResp(null);
            ExecutaJavaScript();
        }

        protected void imbPesqPaci_OnClick(object sender, EventArgs e)
        {
            PesquisaCarregaPaci();
            DesmarcaPacienteInternacao();
            UpdatePanel2.Update();
            UpdatePanel3.Update();
            UpdatePanel5.Update();
            UpdatePanel6.Update();
            UpdatePanel7.Update();
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
            UpdatePanel3.Update();
            UpdatePanel5.Update();
            UpdatePanel6.Update();
            UpdatePanel7.Update();
            ExecutaJavaScript();
        }

        protected void chkPesqCPFUsu_OnCheckedChanged(object sender, EventArgs e)
        {
            hidCoPac.Value = "";

            if (chkPesqCPFUsu.Checked == true)
                chkPesqNuNisPac.Checked = false;

            ExecutaJavaScript();
        }

        /// <summary>
        /// Desmarca todos os checkbox com exceção do que foi clicado por último, para garantir que apenas um seja clicado( DA GRID DE ENCAMINHAMENTO INTERNAÇÃO).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chkselect_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            CheckBox chk;

            // Valida se a grid de atividades possui algum registro
            if (grdPacientesInternacao.Rows.Count != 0)
            {
                // Passa por todos os registros da grid de atividades
                foreach (GridViewRow linha in grdPacientesInternacao.Rows)
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
                            int idAtendAgend = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidIdAtendAgend")).Value);
                            int idAtendInter = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidIdAtendInter")).Value);

                            GridPacienteInternacaoSelecionada(idAtendAgend);
                            carregarDadosInternacao(idAtendInter, idAtendAgend);
                        }
                        else
                        {
                            GridPacienteInternacaoDesmarcada();
                        }
                    }
                }
            }
        }

        protected void txtCPFResp_OnTextChanged(object sender, EventArgs e)
        {
            hidCoResp.Value = "";
            UpdatePanel2.Update();
        }

        protected void txtCpfPaci_OnTextChanged(object sender, EventArgs e)
        {
            hidCoPac.Value = "";
            UpdatePanel2.Update();
        }

        protected void chkPesqNuNisPac_OnCheckedChanged(object sender, EventArgs e)
        {
            if (chkPesqNuNisPac.Checked)
                chkPesqCPFUsu.Checked = false;

            UpdatePanel2.Update();
        }

        #region Procedimento

        protected void ddlTipoProcedimentoInternar_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int idRegisInter = (HttpContext.Current.Session["VL_IdAtendAgend"] != null) ? (int)HttpContext.Current.Session["VL_IdAtendAgend"] : 0;

            if (idRegisInter > 0)
            {
                var idProcedimento = !string.IsNullOrEmpty(ddlTipoProcedimentoInternar.SelectedValue) ? int.Parse(ddlTipoProcedimentoInternar.SelectedValue) : 0;

                List<ProcedimentoInternar> procInter = new List<ProcedimentoInternar>();
                foreach (GridViewRow row in grdProcedimentoInternar.Rows)
                {
                    var proc = new ProcedimentoInternar();
                    proc.idProcedimentoOPM = !string.IsNullOrEmpty(row.Cells[0].Text) ? int.Parse(row.Cells[0].Text) : (int?)null;
                    proc.tipoProcedimentoOPM = row.Cells[1].Text;
                    proc.nomeProcedimentoOPM = row.Cells[2].Text;
                    proc.codigoProcedimentoOPM = row.Cells[3].Text;
                    proc.idInterProcOPM = !string.IsNullOrEmpty(((HiddenField)row.Cells[4].FindControl("hidIdInterProcedimento")).Value) ? int.Parse(((HiddenField)row.Cells[4].FindControl("hidIdInterProcedimento")).Value) : (int?)null;
                    proc.qtdProcedimentoOPM = !string.IsNullOrEmpty(((TextBox)row.Cells[4].FindControl("qtdProcedimento")).Text) ? int.Parse(((TextBox)row.Cells[4].FindControl("qtdProcedimento")).Text) : (decimal?)null;
                    string vlUnit = !string.IsNullOrEmpty(((TextBox)row.Cells[5].FindControl("vlUnitarioProcedimentoInternar")).Text) ? (((TextBox)row.Cells[5].FindControl("vlUnitarioProcedimentoInternar")).Text) : "";
                    proc.vlUnitarioProcedimentoOPM = !string.IsNullOrEmpty(vlUnit) ? decimal.Parse(vlUnit) : (decimal?)null;
                    string vlTotal = !string.IsNullOrEmpty(((TextBox)row.Cells[6].FindControl("vlTotalProcedimentoInternar")).Text) ? (((TextBox)row.Cells[6].FindControl("vlTotalProcedimentoInternar")).Text) : "";
                    proc.vlTotalProcedimentoOPM = !string.IsNullOrEmpty(vlTotal) ? decimal.Parse(vlTotal) : (decimal?)null;
                    procInter.Add(proc);
                }

                var tbs356 = TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                                 .Join(TBS355_PROC_MEDIC_SGRUP.RetornaTodosRegistros(), x => x.TBS355_PROC_MEDIC_SGRUP.ID_PROC_MEDIC_SGRUP, y => y.ID_PROC_MEDIC_SGRUP, (x, y) => new { x, y })
                                 .Join(TBS354_PROC_MEDIC_GRUPO.RetornaTodosRegistros(), z => z.x.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO, w => w.ID_PROC_MEDIC_GRUPO, (z, w) => new { z, w })
                                 .Where(i => i.z.x.ID_PROC_MEDI_PROCE == idProcedimento)
                                 .Select(a => new
                                 {
                                     a.z.x.ID_PROC_MEDI_PROCE,
                                     nmProcedimento = a.w.NM_PROC_MEDIC_GRUPO + " - " + a.z.y.NM_PROC_MEDIC_SGRUP,
                                     a.z.x.NM_PROC_MEDI,
                                     a.z.x.CO_PROC_MEDI,
                                 }).FirstOrDefault();

                var tbs353 = TBS353_VALOR_PROC_MEDIC_PROCE.RetornaTodosRegistros().Where(x => x.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == tbs356.ID_PROC_MEDI_PROCE)
                            .Select(w => new
                            {
                                w.VL_BASE
                            }).FirstOrDefault();

                if (tbs356 != null)
                {
                    var procTb = new ProcedimentoInternar();
                    procTb.idProcedimentoOPM = tbs356.ID_PROC_MEDI_PROCE;
                    procTb.tipoProcedimentoOPM = tbs356.nmProcedimento;
                    procTb.nomeProcedimentoOPM = tbs356.NM_PROC_MEDI;
                    procTb.codigoProcedimentoOPM = tbs356.CO_PROC_MEDI;
                    if (tbs353 != null)
                        procTb.vlUnitarioProcedimentoOPM = tbs353.VL_BASE;
                    procInter.Add(procTb);
                }

                grdProcedimentoInternar.DataSource = procInter.DistinctBy(x => x.idProcedimentoOPM);
                grdProcedimentoInternar.DataBind();
            }
            else
            {
                AbreModalPadrao("closeModalProcedimento()");
                AuxiliPagina.EnvioMensagemErro(this.Page, "Para inserir um novo procedimento no registro de internação, é necessário salvá-lo antes.");
            }
        }

        protected void btnDelProcedimentoInternar_OnClick(object sender, EventArgs e)
        {
            int idAtendAgend = (HttpContext.Current.Session["VL_IdAtendAgend"] != null) ? (int)HttpContext.Current.Session["VL_IdAtendAgend"] : 0;

            if (idAtendAgend > 0)
            {
                ImageButton clickedButton = (ImageButton)sender;
                GridViewRow linha = (GridViewRow)clickedButton.Parent.Parent;
                int index = linha.RowIndex;
                List<ProcedimentoInternar> procInter = new List<ProcedimentoInternar>();

                foreach (GridViewRow row in grdProcedimentoInternar.Rows)
                {
                    if (row.RowIndex != index)
                    {
                        var proc = new ProcedimentoInternar();
                        proc.idProcedimentoOPM = !string.IsNullOrEmpty(row.Cells[0].Text) ? int.Parse(row.Cells[0].Text) : (int?)null;
                        proc.tipoProcedimentoOPM = row.Cells[1].Text;
                        proc.nomeProcedimentoOPM = row.Cells[2].Text;
                        proc.codigoProcedimentoOPM = row.Cells[3].Text;
                        proc.idInterProcOPM = !string.IsNullOrEmpty(((HiddenField)row.Cells[4].FindControl("hidIdInterProcedimento")).Value) ? int.Parse(((HiddenField)row.Cells[4].FindControl("hidIdInterProcedimento")).Value) : (int?)null;
                        proc.qtdProcedimentoOPM = !string.IsNullOrEmpty(((TextBox)row.Cells[4].FindControl("qtdProcedimento")).Text) ? decimal.Parse(((TextBox)row.Cells[4].FindControl("qtdProcedimento")).Text) : (decimal?)null;
                        string vlUnit = !string.IsNullOrEmpty(((TextBox)row.Cells[5].FindControl("vlUnitarioProcedimentoInternar")).Text) ? (((TextBox)row.Cells[5].FindControl("vlUnitarioProcedimentoInternar")).Text) : "";
                        proc.vlUnitarioProcedimentoOPM = !string.IsNullOrEmpty(vlUnit) ? decimal.Parse(vlUnit) : (decimal?)null;
                        string vlTotal = !string.IsNullOrEmpty(((TextBox)row.Cells[6].FindControl("vlTotalProcedimentoInternar")).Text) ? (((TextBox)row.Cells[6].FindControl("vlTotalProcedimentoInternar")).Text) : "";
                        proc.vlTotalProcedimentoOPM = !string.IsNullOrEmpty(vlTotal) ? decimal.Parse(vlTotal) : (decimal?)null;
                        procInter.Add(proc);
                    }
                    else
                    {
                        HiddenField hidId = ((HiddenField)row.Cells[5].FindControl("hidIdInterProcedimento"));
                        int idInterProc = !string.IsNullOrEmpty(hidId.Value) ? int.Parse(hidId.Value) : 0;
                        if (idInterProc > 0)
                        {
                            try
                            {
                                var tbs450 = TBS452_INTER_PROCE.RetornaTodosRegistros().Where(x => x.ID_INTER_PROCE == idInterProc).Select(x => new { x.ID_INTER_PROCE }).ToList();
                                for (int i = 0; i < tbs450.Count; i++)
                                {
                                    var res = TBS452_INTER_PROCE.RetornaPelaChavePrimaria(tbs450[i].ID_INTER_PROCE);
                                    TBS452_INTER_PROCE.Delete(res, true);
                                }
                            }
                            catch (Exception ex)
                            {
                                AbreModalPadrao("closeModalProcedimento()");
                                AuxiliPagina.EnvioMensagemErro(this.Page, "(MSG) Não foi possível excluir o Procedimento do registro, por favor tente novamente ou entre em contato com o suporte. Erro: " + ex.Message);
                            }
                        }
                    }
                }
                grdProcedimentoInternar.DataSource = procInter;
                grdProcedimentoInternar.DataBind();
            }
            else
            {
                AbreModalPadrao("closeModalProcedimento()");
                AuxiliPagina.EnvioMensagemErro(this.Page, "(MSG) Para excluir o procedimento, é necessário salvar o registro de internação antes.");
            }
        }

        protected void btnSalvarProcedimento_OnClick(object sender, EventArgs e)
        {
            int idAtendAgend = (HttpContext.Current.Session["VL_IdAtendAgend"] != null) ? (int)HttpContext.Current.Session["VL_IdAtendAgend"] : 0;

            try
            {
                if (idAtendAgend > 0)
                {
                    int idAtendInter = (HttpContext.Current.Session["VL_IdAtendInter"] != null) ? (int)HttpContext.Current.Session["VL_IdAtendInter"] : 0;

                    foreach (GridViewRow row in grdProcedimentoInternar.Rows)
                    {
                        var proc = new ProcedimentoInternar();
                        int idProcedimentoOPM = !string.IsNullOrEmpty(row.Cells[0].Text) ? int.Parse(row.Cells[0].Text) : 0;
                        int idRegisInterProcedimento = !string.IsNullOrEmpty(((HiddenField)row.Cells[4].FindControl("hidIdInterProcedimento")).Value) ? int.Parse(((HiddenField)row.Cells[4].FindControl("hidIdInterProcedimento")).Value) : 0;
                        var res = TBS452_INTER_PROCE.RetornaTodosRegistros().Where(x => x.ID_INTER_PROCE == idRegisInterProcedimento && x.TBS390_ATEND_AGEND.ID_ATEND_AGEND == idAtendAgend).FirstOrDefault();
                        var tbs452 = res != null ? res : new TBS452_INTER_PROCE();

                        //oriundo da funcionalidade de internação
                        tbs452.CO_TIPO_REGIS_PROCE = "IN";
                        tbs452.TBS356_PROC_MEDIC_PROCE = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(idProcedimentoOPM);
                        tbs452.QT_PROCE_INTER = decimal.Parse(((TextBox)row.Cells[4].FindControl("qtdProcedimento")).Text);
                        string vlUnit = (((TextBox)row.Cells[5].FindControl("vlUnitarioProcedimentoInternar")).Text);
                        tbs452.VL_UNITA_PROCE_INTER = !string.IsNullOrEmpty(vlUnit) ? decimal.Parse(vlUnit) : (decimal?)null;
                        decimal vlTotal = tbs452.QT_PROCE_INTER * tbs452.QT_PROCE_INTER * (tbs452.VL_UNITA_PROCE_INTER != null ? (decimal)tbs452.VL_UNITA_PROCE_INTER : 0);
                        tbs452.VL_TOTAL_PROCE_INTER = vlTotal;
                        tbs452.TBS390_ATEND_AGEND = TBS390_ATEND_AGEND.RetornaPelaChavePrimaria(idAtendAgend);

                        TBS452_INTER_PROCE.SaveOrUpdate(tbs452, true);
                    }

                    GridPacienteInternacaoSelecionada(idAtendAgend);
                    carregarDadosInternacao(idAtendInter, idAtendAgend);
                    AbreModalPadrao("closeModalProcedimentoSucesso()");
                }
                else
                {
                    throw new ArgumentException("(MSG) Para incluir o procedimento, é necessário salvar o registro de internação antes.");
                }
            }
            catch (Exception ex)
            {
                AbreModalPadrao("closeModalProcedimento()");
                AuxiliPagina.EnvioMensagemErro(this.Page, ex.Message);
                return;
            }
        }

        #endregion

        #region OPM

        protected void ddlOPMInternar_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int idRegisInter = (HttpContext.Current.Session["VL_IdAtendAgend"] != null) ? (int)HttpContext.Current.Session["VL_IdAtendAgend"] : 0;

            if (idRegisInter > 0)
            {
                var idOPM = !string.IsNullOrEmpty(ddlOPMInternar.SelectedValue) ? int.Parse(ddlOPMInternar.SelectedValue) : 0;

                List<ProcedimentoInternar> procInter = new List<ProcedimentoInternar>();
                foreach (GridViewRow row in grdOPM.Rows)
                {
                    var proc = new ProcedimentoInternar();
                    proc.idProcedimentoOPM = !string.IsNullOrEmpty(row.Cells[0].Text) ? int.Parse(row.Cells[0].Text) : (int?)null;
                    proc.tipoProcedimentoOPM = row.Cells[1].Text;
                    proc.nomeProcedimentoOPM = row.Cells[2].Text;
                    proc.codigoProcedimentoOPM = row.Cells[3].Text;
                    proc.idInterProcOPM = !string.IsNullOrEmpty(((HiddenField)row.Cells[4].FindControl("hidIdInterOPM")).Value) ? int.Parse(((HiddenField)row.Cells[4].FindControl("hidIdInterOPM")).Value) : (int?)null;
                    proc.qtdProcedimentoOPM = !string.IsNullOrEmpty(((TextBox)row.Cells[4].FindControl("qtdOPM")).Text) ? int.Parse(((TextBox)row.Cells[4].FindControl("qtdOPM")).Text) : (decimal?)null;
                    proc.fabricanteOPM = ((TextBox)row.Cells[5].FindControl("fabricanteOPM")).Text;
                    string vlUnit = !string.IsNullOrEmpty(((TextBox)row.Cells[6].FindControl("VlUnitarioOPM")).Text) ? (((TextBox)row.Cells[6].FindControl("VlUnitarioOPM")).Text) : "";
                    proc.vlUnitarioProcedimentoOPM = !string.IsNullOrEmpty(vlUnit) ? decimal.Parse(vlUnit) : (decimal?)null;
                    string vlTotal = !string.IsNullOrEmpty(((TextBox)row.Cells[7].FindControl("qtdVlTotalOPM")).Text) ? (((TextBox)row.Cells[7].FindControl("qtdVlTotalOPM")).Text) : "";
                    proc.vlTotalProcedimentoOPM = !string.IsNullOrEmpty(vlTotal) ? decimal.Parse(vlTotal) : (decimal?)null;
                    procInter.Add(proc);
                }

                var tbs356 = TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                                   .Join(TBS355_PROC_MEDIC_SGRUP.RetornaTodosRegistros(), x => x.TBS355_PROC_MEDIC_SGRUP.ID_PROC_MEDIC_SGRUP, y => y.ID_PROC_MEDIC_SGRUP, (x, y) => new { x, y })
                                   .Join(TBS354_PROC_MEDIC_GRUPO.RetornaTodosRegistros(), z => z.x.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO, w => w.ID_PROC_MEDIC_GRUPO, (z, w) => new { z, w })
                                   .Where(i => i.z.x.ID_PROC_MEDI_PROCE == idOPM)
                                   .Select(a => new
                                   {
                                       a.z.x.ID_PROC_MEDI_PROCE,
                                       nmProcedimento = a.w.NM_PROC_MEDIC_GRUPO + " - " + a.z.y.NM_PROC_MEDIC_SGRUP,
                                       a.z.x.NM_PROC_MEDI,
                                       a.z.x.CO_PROC_MEDI,
                                   }).FirstOrDefault();

                if (tbs356 != null)
                {
                    var tbs353 = TBS353_VALOR_PROC_MEDIC_PROCE.RetornaTodosRegistros().Where(x => x.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == tbs356.ID_PROC_MEDI_PROCE)
                            .Select(w => new
                            {
                                w.VL_BASE
                            }).FirstOrDefault();

                    var procTb = new ProcedimentoInternar();
                    procTb.idProcedimentoOPM = tbs356.ID_PROC_MEDI_PROCE;
                    procTb.tipoProcedimentoOPM = tbs356.nmProcedimento;
                    procTb.nomeProcedimentoOPM = tbs356.NM_PROC_MEDI;
                    procTb.codigoProcedimentoOPM = tbs356.CO_PROC_MEDI;
                    if (tbs353 != null)
                        procTb.vlUnitarioProcedimentoOPM = tbs353.VL_BASE;
                    procInter.Add(procTb);
                }

                grdOPM.DataSource = procInter.DistinctBy(x => x.idProcedimentoOPM);
                grdOPM.DataBind();
            }
            else
            {
                AbreModalPadrao("closeModalOPM()");
                AuxiliPagina.EnvioMensagemErro(this.Page, "Para inserir um novo OPM no registro de internação, é necessário salvá-lo antes.");
            }
        }

        protected void btnDelOPMInternar_OnClick(object sender, EventArgs e)
        {
            int idAtendAgend = (HttpContext.Current.Session["VL_IdAtendAgend"] != null) ? (int)HttpContext.Current.Session["VL_IdAtendAgend"] : 0;

            if (idAtendAgend > 0)
            {
                ImageButton clickedButton = (ImageButton)sender;
                GridViewRow linha = (GridViewRow)clickedButton.Parent.Parent;
                int index = linha.RowIndex;

                List<ProcedimentoInternar> procInter = new List<ProcedimentoInternar>();
                foreach (GridViewRow row in grdOPM.Rows)
                {
                    if (row.RowIndex != index)
                    {
                        var proc = new ProcedimentoInternar();
                        proc.idProcedimentoOPM = !string.IsNullOrEmpty(row.Cells[0].Text) ? int.Parse(row.Cells[0].Text) : (int?)null;
                        proc.tipoProcedimentoOPM = row.Cells[1].Text;
                        proc.nomeProcedimentoOPM = row.Cells[2].Text;
                        proc.codigoProcedimentoOPM = row.Cells[3].Text;
                        proc.idInterProcOPM = !string.IsNullOrEmpty(((HiddenField)row.Cells[4].FindControl("hidIdInterOPM")).Value) ? int.Parse(((HiddenField)row.Cells[4].FindControl("hidIdInterOPM")).Value) : (int?)null;
                        proc.qtdProcedimentoOPM = !string.IsNullOrEmpty(((TextBox)row.Cells[4].FindControl("qtdOPM")).Text) ? decimal.Parse(((TextBox)row.Cells[4].FindControl("qtdOPM")).Text) : (decimal?)null;
                        proc.fabricanteOPM = ((TextBox)row.Cells[5].FindControl("fabricanteOPM")).Text;
                        string vlUnit = !string.IsNullOrEmpty(((TextBox)row.Cells[6].FindControl("VlUnitarioOPM")).Text) ? (((TextBox)row.Cells[6].FindControl("VlUnitarioOPM")).Text) : "";
                        proc.vlUnitarioProcedimentoOPM = !string.IsNullOrEmpty(vlUnit) ? decimal.Parse(vlUnit) : (decimal?)null;
                        string vlTotal = !string.IsNullOrEmpty(((TextBox)row.Cells[7].FindControl("qtdVlTotalOPM")).Text) ? (((TextBox)row.Cells[7].FindControl("qtdVlTotalOPM")).Text) : "";
                        proc.vlTotalProcedimentoOPM = !string.IsNullOrEmpty(vlTotal) ? decimal.Parse(vlTotal) : (decimal?)null;
                        procInter.Add(proc);
                    }
                    else
                    {
                        HiddenField hidId = ((HiddenField)row.Cells[5].FindControl("hidIdInterOPM"));
                        int idInterProc = !string.IsNullOrEmpty(hidId.Value) ? int.Parse(hidId.Value) : 0;
                        if (idInterProc > 0)
                        {
                            try
                            {
                                var tbs450 = TBS452_INTER_PROCE.RetornaTodosRegistros().Where(x => x.ID_INTER_PROCE == idInterProc).Select(x => new { x.ID_INTER_PROCE }).ToList();
                                for (int i = 0; i < tbs450.Count; i++)
                                {
                                    var res = TBS452_INTER_PROCE.RetornaPelaChavePrimaria(tbs450[i].ID_INTER_PROCE);
                                    TBS452_INTER_PROCE.Delete(res, true);
                                }
                            }
                            catch (Exception ex)
                            {
                                AbreModalPadrao("closeModalOPM()");
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Não foi possível excluir o OPM do registro, por favor tente novamente ou entre em contato com o suporte. Erro: " + ex.Message);
                            }
                        }
                    }
                }
                grdOPM.DataSource = procInter;
                grdOPM.DataBind();
            }
            else
            {
                AbreModalPadrao("closeModalOPM()");
                AuxiliPagina.EnvioMensagemErro(this.Page, "(MSG) Para excluir o OPM, é necessário salvar o registro de internação antes.");
            }
        }

        protected void btnSalvarOPM_OnClick(object sender, EventArgs e)
        {
            int idAtendAgend = (HttpContext.Current.Session["VL_IdAtendAgend"] != null) ? (int)HttpContext.Current.Session["VL_IdAtendAgend"] : 0;
            try
            {
                if (idAtendAgend > 0)
                {
                    int idAtendInter = (HttpContext.Current.Session["VL_IdAtendInter"] != null) ? (int)HttpContext.Current.Session["VL_IdAtendInter"] : 0;

                    foreach (GridViewRow row in grdOPM.Rows)
                    {
                        var proc = new ProcedimentoInternar();
                        int idProcedimentoOPM = !string.IsNullOrEmpty(row.Cells[0].Text) ? int.Parse(row.Cells[0].Text) : 0;
                        int idRegisInterProcedimento = !string.IsNullOrEmpty(((HiddenField)row.Cells[4].FindControl("hidIdInterOPM")).Value) ? int.Parse(((HiddenField)row.Cells[4].FindControl("hidIdInterOPM")).Value) : 0;
                        var res = TBS452_INTER_PROCE.RetornaTodosRegistros().Where(x => x.ID_INTER_PROCE == idRegisInterProcedimento && x.TBS390_ATEND_AGEND.ID_ATEND_AGEND == idAtendAgend).FirstOrDefault();
                        var tbs452 = res != null ? res : new TBS452_INTER_PROCE();

                        //oriundo da funcionalidade de internação
                        tbs452.CO_TIPO_REGIS_PROCE = "IN";
                        tbs452.TBS356_PROC_MEDIC_PROCE = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(idProcedimentoOPM);
                        tbs452.FABRI_OPM = ((TextBox)row.Cells[5].FindControl("fabricanteOPM")).Text;
                        tbs452.QT_PROCE_INTER = decimal.Parse(((TextBox)row.Cells[4].FindControl("qtdOPM")).Text);
                        string vlUnit = (((TextBox)row.Cells[6].FindControl("VlUnitarioOPM")).Text);
                        tbs452.VL_UNITA_PROCE_INTER = !string.IsNullOrEmpty(vlUnit) ? decimal.Parse(vlUnit) : (decimal?)null;
                        decimal vlTotal = tbs452.QT_PROCE_INTER * (tbs452.VL_UNITA_PROCE_INTER != null ?  (decimal)tbs452.VL_UNITA_PROCE_INTER : 0);
                        tbs452.VL_TOTAL_PROCE_INTER = vlTotal;
                        tbs452.TBS390_ATEND_AGEND = TBS390_ATEND_AGEND.RetornaPelaChavePrimaria(idAtendAgend);

                        TBS452_INTER_PROCE.SaveOrUpdate(tbs452, true);
                    }

                    GridPacienteInternacaoSelecionada(idAtendAgend);
                    carregarDadosInternacao(idAtendInter, idAtendAgend);
                    AbreModalPadrao("closeModalOPMSucesso()");
                }
                else
                {
                    throw new ArgumentException("(MSG) Para incluir o OPM, é necessário salvar o registro de internação antes.");
                }
            }
            catch (Exception ex)
            {
                AbreModalPadrao("closeModalOPM()");
                AuxiliPagina.EnvioMensagemErro(this.Page, ex.Message);
                return;
            }
        }

        #endregion

        #region CID

        protected void imgbPesqCID_OnClick(object sender, EventArgs e)
        {
            OcultarPesquisa(true);

            string nomeCID = txtDefCid.Text;

            var res = TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaTodosRegistros()
                .Where(x => x.NO_CID.Contains(nomeCID) || x.CO_CID.Contains(nomeCID))
                .Select(x => new { x.NO_CID, x.IDE_CID }).OrderBy(x => x.NO_CID);
            drpDefCid.DataSource = res;
            drpDefCid.DataTextField = "NO_CID";
            drpDefCid.DataValueField = "IDE_CID";
            drpDefCid.DataBind();

            drpDefCid.Items.Insert(0, new ListItem("Selecione", ""));

            AbreModalPadrao("AbreModalCID();");
        }

        protected void imgbVoltarPesq_OnClick(object sender, EventArgs e)
        {
            OcultarPesquisa(false);
        }

        private void OcultarPesquisa(bool ocultar)
        {
            txtDefCid.Visible =
            imgbPesqPacNome.Visible = !ocultar;
            drpDefCid.Visible =
            imgbVoltarPesq.Visible = ocultar;
            AbreModalPadrao("AbreModalCID();");
        }

        protected void drpDefCid_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int idCID = !string.IsNullOrEmpty(drpDefCid.SelectedValue) ? int.Parse(drpDefCid.SelectedValue) : 0;

            carregarGrdProtocoloCID(idCID);
        }

        private void carregarGrdProtocoloCID(int id)
        {
            try
            {
                int idRegisInter = (HttpContext.Current.Session["VL_IdAtendAgend"] != null) ? (int)HttpContext.Current.Session["VL_IdAtendAgend"] : 0;

                if (idRegisInter <= 0)
                {
                    throw new ArgumentException("Nenhum encaminhamento de internação foi selecionado.");
                }

                if (id > 0)
                {
                    List<int> listaidCID = new List<int>();
                    List<CIDInternar> listaCID = new List<CIDInternar>();

                    foreach (GridViewRow row in grdCIDInternar.Rows)
                    {
                        int coCID = int.Parse(((HiddenField)row.Cells[0].FindControl("hidIdCIDInternar")).Value);
                        listaidCID.Add(coCID);
                    }

                    listaidCID.Add(id);

                    foreach (var item in listaidCID)
                    {
                        int countProtocolo = TBS434_PROTO_CID.RetornaTodosRegistros().Where(x => x.TB117_CODIGO_INTERNACIONAL_DOENCA.IDE_CID == item).Count();

                        bool existeProtocolo = countProtocolo > 0 ? true : false;

                        var res = TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaTodosRegistros()
                                                    .Join(TBS453_INTER_CID.RetornaTodosRegistros(), y => y.IDE_CID, x => x.TB117_CODIGO_INTERNACIONAL_DOENCA.IDE_CID, (y, x) => new { y, x })
                                                    .Where(w => w.y.IDE_CID == item && w.x.TBS451_INTER_REGIST.ID_INTER_REGIS == id)
                                                    .Select(w => new CIDInternar { idInterCID = w.x.ID_INTER_CID, cidPrincipal = w.x.IS_CID_PRINC, coCID = w.y.CO_CID, idCID = w.y.IDE_CID, nomeCID = w.y.NO_CID, existeProtocolo = existeProtocolo }).FirstOrDefault();
                        if (res != null)
                        {
                            listaCID.Add(res);
                        }
                        else
                        {
                            var query = TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaTodosRegistros().Where(x => x.IDE_CID == item)
                                .Select(w => new CIDInternar { cidPrincipal = "N", coCID = w.CO_CID, idCID = w.IDE_CID, nomeCID = w.NO_CID, existeProtocolo = existeProtocolo }).FirstOrDefault();
                            listaCID.Add(query);
                        }
                    }
                    grdCIDInternar.DataSource = listaCID.DistinctBy(x => x.idCID);
                    grdCIDInternar.DataBind();
                    repProtocolosCID.DataSource = null;
                    repProtocolosCID.DataBind();
                }
            }
            catch (Exception ex)
            {
                AbreModalPadrao("closeModalCID()");
                AuxiliPagina.EnvioMensagemErro(this.Page, "Não foi possível concluir a operação. Erro: " + ex.Message);
                return;
            }
        }

        protected void btnDelCIDInternar_OnClick(object sender, EventArgs e)
        {
            int idRegisInter = (HttpContext.Current.Session["VL_IdAtendAgend"] != null) ? (int)HttpContext.Current.Session["VL_IdAtendAgend"] : 0;

            if (idRegisInter > 0)
            {
                ImageButton clickedButton = (ImageButton)sender;
                GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
                int index = row.RowIndex;

                List<CIDInternar> listaCID = new List<CIDInternar>();

                foreach (GridViewRow item in grdCIDInternar.Rows)
                {
                    if (item.RowIndex != index)
                    {
                        CIDInternar itemCID = new CIDInternar();
                        HiddenField hidIdInter = ((HiddenField)item.Cells[1].FindControl("hidIdInterCID"));
                        itemCID.idInterCID = !string.IsNullOrEmpty(hidIdInter.Value) ? int.Parse(hidIdInter.Value) : 0;
                        HiddenField hidIdCID = ((HiddenField)item.Cells[1].FindControl("hidIdCIDInternar"));
                        itemCID.idCID = !string.IsNullOrEmpty(hidIdCID.Value) ? int.Parse(hidIdCID.Value) : 0;
                        itemCID.nomeCID = ((Label)item.Cells[1].FindControl("lblCIDInternar")).Text;
                        itemCID.cidPrincipal = ((CheckBox)item.Cells[0].FindControl("chcCIDPrincipal")).Checked ? "S" : "N";
                        string existeProt = ((HiddenField)item.Cells[2].FindControl("hidCIDExisteProtocolo")).Value;
                        itemCID.existeProtocolo = !string.IsNullOrEmpty(existeProt) && existeProt.Equals("True") ? true : false;

                        listaCID.Add(itemCID);
                    }
                    else
                    {
                        HiddenField hidId = ((HiddenField)item.Cells[0].FindControl("hidIdInterCID"));
                        int idInterCID = !string.IsNullOrEmpty(hidId.Value) ? int.Parse(hidId.Value) : 0;
                        if (idInterCID > 0)
                        {
                            try
                            {
                                var tbs453 = TBS453_INTER_CID.RetornaTodosRegistros().Where(x => x.ID_INTER_CID == idInterCID).Select(x => new { x.ID_INTER_CID }).ToList();
                                for (int i = 0; i < tbs453.Count; i++)
                                {
                                    var res = TBS453_INTER_CID.RetornaPelaChavePrimaria(tbs453[i].ID_INTER_CID);
                                    TBS453_INTER_CID.Delete(res, true);
                                }
                            }
                            catch (Exception ex)
                            {
                                AbreModalPadrao("closeModalCID()");
                                AuxiliPagina.EnvioMensagemErro(this.Page, "(MSG) Não foi possível excluir o CID do registro, por favor tente novamente ou entre em contato com o suporte. Erro: " + ex.Message);
                            }
                        }
                    }

                }
                grdCIDInternar.DataSource = listaCID;
                grdCIDInternar.DataBind();
                repProtocolosCID.DataSource = null;
                repProtocolosCID.DataBind();
            }
            else
            {
                AbreModalPadrao("closeModalCID()");
                AuxiliPagina.EnvioMensagemErro(this.Page, "(MSG) Não foi possível exluir a CID, por favor selecione um encaminhamento de internação.");
                return;
            }
        }

        protected void btnObsProtCIDInternar_OnClick(object sender, EventArgs e)
        {
            int idRegisInter = (HttpContext.Current.Session["VL_IdAtendAgend"] != null) ? (int)HttpContext.Current.Session["VL_IdAtendAgend"] : 0;

            if (idRegisInter > 0)
            {
                ImageButton clickedButton = (ImageButton)sender;
                GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
                int index = row.RowIndex;
                int idCID = 0;
                string nomeCID = "";

                foreach (GridViewRow item in grdCIDInternar.Rows)
                {
                    if (item.RowIndex == index)
                    {
                        HiddenField hidIdInter = ((HiddenField)item.Cells[1].FindControl("hidIdInterCID"));
                        int idTbs449 = !string.IsNullOrEmpty(hidIdInter.Value) ? int.Parse(hidIdInter.Value) : 0;
                        HiddenField hidIdCID = ((HiddenField)item.Cells[1].FindControl("hidIdCIDInternar"));
                        idCID = !string.IsNullOrEmpty(hidIdCID.Value) ? int.Parse(hidIdCID.Value) : 0;
                        nomeCID = ((Label)item.Cells[0].FindControl("lblCIDInternar")).Text;
                    }
                }

                lblTitProtocolo.Controls.Add(new Literal() { Text = "<span>Protocolos CID: " + nomeCID + "</span>" });

                var tbs434 = TBS434_PROTO_CID.RetornaTodosRegistros()
                                .Where(x => x.TB117_CODIGO_INTERNACIONAL_DOENCA.IDE_CID == idCID)
                                .Select(x => new CIDProtocolo
                                {
                                    idProtocolo = x.ID_PROTO_CID,
                                    nomeProtocolo = x.NO_PROTO_CID
                                });

                repProtocolosCID.DataSource = tbs434;
                repProtocolosCID.DataBind();
            }
            else
            {
                AbreModalPadrao("closeModalCID()");
                AuxiliPagina.EnvioMensagemErro(this.Page, "(MSG) Por favor, salve o registro de internação antes de prosseguir com a operação.");
            }
        }

        protected void repProtocolosCID_OnItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater childRepeater = (Repeater)args.Item.FindControl("repItenProtocoloCID");
                var Obj = (CIDProtocolo)args.Item.DataItem;
                int idAtendInter = (HttpContext.Current.Session["VL_PreAtend"] != null ? (int)HttpContext.Current.Session["VL_PreAtend"] : 0);
                int idRegisInter = (HttpContext.Current.Session["VL_IdAtendAgend"] != null) ? (int)HttpContext.Current.Session["VL_IdAtendAgend"] : 0;

                if (idRegisInter <= 0)
                {
                    var tbs449 = TBS448_ATEND_INTER_HOSPI.RetornaPelaChavePrimaria(idAtendInter);
                    tbs449.TBS390_ATEND_AGENDReference.Load();
                    var res = TBS441_ATEND_HOSPIT_ITEN_PROTO_CID.RetornaTodosRegistros()
                                 .Join(TBS436_ITEM_PROTO_CID.RetornaTodosRegistros(), a => a.TBS436_ITEM_PROTO_CID.ID_ITEM_PROTO, b => b.ID_ITEM_PROTO, (a, b) => new { a, b })
                                 .Where(x => x.b.TBS434_PROTO_CID.ID_PROTO_CID == Obj.idProtocolo && x.a.TBS390_ATEND_AGEND.ID_ATEND_AGEND == tbs449.TBS390_ATEND_AGEND.ID_ATEND_AGEND)
                                 .Select(w => new CIDItensProtocolo
                                 {
                                     idItenProtocolo = w.b.ID_ITEM_PROTO,
                                     itemAplicado = w.a.FL_APLIC,
                                     nomeItemProtocolo = w.b.NO_ITEM_PROTO,
                                     obsItemCID = w.a.OBS_ITEM
                                 }).OrderBy(x => x.nomeItemProtocolo);

                    if (res.Count() > 0)
                    {
                        childRepeater.DataSource = res;
                    }
                    else
                    {
                        childRepeater.DataSource = TBS436_ITEM_PROTO_CID.RetornaTodosRegistros()
                                                   .Where(x => x.TBS434_PROTO_CID.ID_PROTO_CID == Obj.idProtocolo)
                                                   .Select(w => new CIDItensProtocolo
                                                   {
                                                       idItenProtocolo = w.ID_ITEM_PROTO,
                                                       itemAplicado = "S",
                                                       nomeItemProtocolo = w.NO_ITEM_PROTO,
                                                       obsItemCID = ""
                                                   }).OrderBy(x => x.nomeItemProtocolo);
                    }
                }
                else
                {
                    var res = TBS453_INTER_CID.RetornaTodosRegistros()
                                                 .Join(TBS436_ITEM_PROTO_CID.RetornaTodosRegistros(), a => a.TBS436_ITEM_PROTO_CID.ID_ITEM_PROTO, b => b.ID_ITEM_PROTO, (a, b) => new { a, b })
                                                 .Where(x => x.b.TBS434_PROTO_CID.ID_PROTO_CID == Obj.idProtocolo && x.a.TBS451_INTER_REGIST.ID_INTER_REGIS == idRegisInter)
                                                 .Select(w => new CIDItensProtocolo
                                                 {
                                                     regisInterItemCID = w.a.ID_INTER_CID,
                                                     idItenProtocolo = w.b.ID_ITEM_PROTO,
                                                     itemAplicado = w.a.IS_ITEM_APLIC,
                                                     nomeItemProtocolo = w.b.NO_ITEM_PROTO,
                                                     obsItemCID = w.a.DE_OBSER_CID_INTER
                                                 }).OrderBy(x => x.nomeItemProtocolo);

                    if (res.Count() > 0)
                    {
                        childRepeater.DataSource = res;
                    }
                    else
                    {
                        childRepeater.DataSource = TBS436_ITEM_PROTO_CID.RetornaTodosRegistros()
                                                    .Where(x => x.TBS434_PROTO_CID.ID_PROTO_CID == Obj.idProtocolo)
                                                    .Select(w => new CIDItensProtocolo
                                                     {
                                                         idItenProtocolo = w.ID_ITEM_PROTO,
                                                         itemAplicado = "S",
                                                         nomeItemProtocolo = w.NO_ITEM_PROTO,
                                                         obsItemCID = ""
                                                     }).OrderBy(x => x.nomeItemProtocolo);
                    }
                }
                childRepeater.DataBind();
            }
        }

        protected void btnSalvarCID_OnClick(object sender, EventArgs e)
        {
            int idAtendAgend = (HttpContext.Current.Session["VL_IdAtendAgend"] != null) ? (int)HttpContext.Current.Session["VL_IdAtendAgend"] : 0;
            int IAtendInter = (HttpContext.Current.Session["VL_IAtendInter"] != null) ? (int)HttpContext.Current.Session["VL_IAtendInter"] : 0;

            try
            {
                if (idAtendAgend > 0)
                {
                    foreach (GridViewRow row in grdCIDInternar.Rows)
                    {
                        bool chcPrincipal = ((CheckBox)row.Cells[0].FindControl("chcCIDPrincipal")).Checked;
                        HiddenField hidIdInter = ((HiddenField)row.Cells[1].FindControl("hidIdInterCID"));
                        int idTbs449 = !string.IsNullOrEmpty(hidIdInter.Value) ? int.Parse(hidIdInter.Value) : 0;
                        HiddenField hidIdCID = ((HiddenField)row.Cells[1].FindControl("hidIdCIDInternar"));
                        int idCID = !string.IsNullOrEmpty(hidIdCID.Value) ? int.Parse(hidIdCID.Value) : 0;
                        var tbs434 = TBS434_PROTO_CID.RetornaTodosRegistros().Where(x => x.TB117_CODIGO_INTERNACIONAL_DOENCA.IDE_CID == idCID).ToList();
                        if (tbs434.Count > 0)
                        {
                            foreach (var prot in tbs434)
                            {
                                var tbs436 = TBS436_ITEM_PROTO_CID.RetornaTodosRegistros().Where(x => x.TBS434_PROTO_CID.ID_PROTO_CID == prot.ID_PROTO_CID).ToList();

                                foreach (RepeaterItem item in repProtocolosCID.Items)
                                {
                                    HiddenField hidProt = ((HiddenField)item.FindControl("hidIdProtocoloCID"));
                                    int idProt = !string.IsNullOrEmpty(hidProt.Value) ? int.Parse(hidProt.Value) : 0;
                                    Repeater rptr = (Repeater)item.FindControl("repItenProtocoloCID");
                                    foreach (RepeaterItem linha in rptr.Items)
                                    {
                                        HiddenField hidItemProt = ((HiddenField)linha.FindControl("hidIdItemProtocoloCID"));
                                        int idItemProt = !string.IsNullOrEmpty(hidItemProt.Value) ? int.Parse(hidItemProt.Value) : 0;
                                        if (tbs436.Any(x => x.ID_ITEM_PROTO == idItemProt))
                                        {
                                            int idRegisInterCID = !string.IsNullOrEmpty(((HiddenField)linha.FindControl("hidRegisInterItemCID")).Value) ? int.Parse(((HiddenField)linha.FindControl("hidRegisInterItemCID")).Value) : 0;
                                            var itemCID = TBS453_INTER_CID.RetornaPelaChavePrimaria(idRegisInterCID);
                                            var tsb453 = itemCID != null ? itemCID : new TBS453_INTER_CID();
                                            tsb453.IS_CID_PRINC = chcPrincipal ? "S" : "N";
                                            tsb453.TB117_CODIGO_INTERNACIONAL_DOENCA = TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaPelaChavePrimaria(idCID);
                                            tsb453.TBS434_PROTO_CID = TBS434_PROTO_CID.RetornaPelaChavePrimaria(idProt);
                                            tsb453.TBS436_ITEM_PROTO_CID = TBS436_ITEM_PROTO_CID.RetornaPelaChavePrimaria(idItemProt);
                                            tsb453.TBS390_ATEND_AGEND = TBS390_ATEND_AGEND.RetornaPelaChavePrimaria(idAtendAgend);
                                            tsb453.IS_ITEM_APLIC = ((CheckBox)linha.FindControl("chkIdItenProtocoloCID")).Checked ? "S" : "N";
                                            tsb453.DE_OBSER_CID_INTER = ((TextBox)linha.FindControl("txtItensProtocoloCID")).Text;
                                            TBS453_INTER_CID.SaveOrUpdate(tsb453, true);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            var res = TBS453_INTER_CID.RetornaTodosRegistros().Where(x => x.TBS390_ATEND_AGEND.ID_ATEND_AGEND == idAtendAgend && x.TB117_CODIGO_INTERNACIONAL_DOENCA.IDE_CID == idCID).FirstOrDefault();
                            var tsb453 = res != null ? res : new TBS453_INTER_CID();
                            tsb453.IS_CID_PRINC = chcPrincipal ? "S" : "N";
                            tsb453.TB117_CODIGO_INTERNACIONAL_DOENCA = TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaPelaChavePrimaria(idCID);
                            tsb453.TBS390_ATEND_AGEND = TBS390_ATEND_AGEND.RetornaPelaChavePrimaria(idAtendAgend);
                            TBS453_INTER_CID.SaveOrUpdate(tsb453, true);
                        }
                    }
                    GridPacienteInternacaoSelecionada(idAtendAgend);
                    carregarDadosInternacao(IAtendInter, idAtendAgend);
                    AbreModalPadrao("closeModalCIDSucesso()");
                }
                else
                {
                    AbreModalPadrao("closeModalCID()");
                    throw new ArgumentException("Por favor, salve o registro de internção atual para concluir o registro.");
                }
            }
            catch (Exception ex)
            {
                AbreModalPadrao("closeModalCID()");
                AuxiliPagina.EnvioMensagemErro(this.Page, "Não foi possível concluir a operação. Erro: " + ex.Message);
            }

        }

        #endregion

        #endregion

        #region Classes de Saída

        public class AtendimentoInternar
        {
            public int ID_ATEND_INTER { get; set; }
            public int ID_ATEND_AGEND { get; set; }
            public string NOME_PACIENTE { get; set; }
            public DateTime? dtProvavel { get; set; }
            public string DT_PROVAVEL_INTER { get { return this.dtProvavel.Value.ToShortDateString(); } }
            public DateTime dtSolic { get; set; }
            public string DT_SOLIC_INTER { get { return this.dtSolic.ToShortDateString(); } }
            public string NU_REGIS_INTER { get; set; }
            public string SEXO { get; set; }
            public string prioridade { get; set; }
            public string CO_PRIOR_RISCO
            {
                get
                {
                    switch (this.prioridade)
                    {//(U) Urgente; A (Alta); M (Média); N (Normal); B (Baixa) X (Nenhuma)
                        case "N":
                            return "Normal";
                        case "M":
                            return "Média";
                        case "A":
                            return "Alta";
                        case "B":
                            return "Baixa";
                        case "U":
                            return "Urgente";
                        default:
                            return "Nenhuma";
                    }
                }
            }
            public string NOME_RESP { get; set; }
            public string NOME_COL_SOL { get; set; }
            public string ESPE_COL_SOL { get; set; }
            public string TELE_COL_SOL { get; set; }
        }

        public class EncaminhamentoInternar
        {
            public string nuRegistroInter { get; set; }
            public string coPrioridadeInter { get; set; }
            public int caraterInter { get; set; }
            public int tipoInter { get; set; }
            public int regimeInter { get; set; }
            public decimal diariasSolicitadas { get; set; }
            public string indicacaoClinica { get; set; }
            public int tipoAcomodacao { get; set; }
            public DateTime? dtInter { get; set; }
            public string dataAH { get { return this.dtInter.HasValue ? this.dtInter.Value.ToShortDateString() : ""; } }
            public int tipoDoenca { get; set; }
            public decimal qtdeTempoDoenca { get; set; }
            public string tipoTempoDoenca { get; set; }
            public int tipoAcidente { get; set; }
            public int idInter { get; set; }
        }

        public class CIDInternar
        {
            public int? idCID { get; set; }
            public int? idInterCID { get; set; }
            public string coCID { get; set; }
            public string nomeCID { get; set; }
            public string descCID { get; set; }
            public string cidPrincipal { get; set; }
            public bool isCIDPrincipal { get { return this.cidPrincipal.Equals("S") ? true : false; } }
            public bool existeProtocolo { get; set; }
        }

        public class CIDProtocolo
        {
            public int? idProtocolo { get; set; }
            public string nomeProtocolo { get; set; }
        }

        public class CIDItensProtocolo
        {
            public int? regisInterItemCID { get; set; }
            public int? idItenProtocolo { get; set; }
            public string nomeItemProtocolo { get; set; }
            public string obsItemCID { get; set; }
            public string itemAplicado { get; set; }
            public bool flAplicado
            {
                get { return !string.IsNullOrEmpty(this.itemAplicado) && this.itemAplicado.Equals("N") ? false : true; }
            }
            public bool flEnabled
            {
                get { return this.flAplicado ? false : true; }
            }
        }

        public class ProcedimentoInternar
        {
            public int? idProcedimentoOPM { get; set; }
            public int? idItemProcOPM { get; set; }
            public string tipoProcedimentoOPM { get; set; }
            public string nomeProcedimentoOPM { get; set; }
            public string codigoProcedimentoOPM { get; set; }
            public decimal? qtdProcedimentoOPM { get; set; }
            public decimal? vlUnitarioProcedimentoOPM { get; set; }
            public decimal? vlTotalProcedimentoOPM { get; set; }
            public string fabricanteOPM { get; set; }
            public string tipo { get; set; }

            public int? idInterProcOPM { get; set; }
        }

        #endregion
    }
}