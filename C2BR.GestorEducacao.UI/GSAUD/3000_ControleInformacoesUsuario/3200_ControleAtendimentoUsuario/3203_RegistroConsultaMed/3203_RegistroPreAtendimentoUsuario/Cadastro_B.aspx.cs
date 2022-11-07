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
    public partial class Cadastro_B : System.Web.UI.Page
    {
        int qtdLinhasGrid = 0;

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
                IniPeri.Text = FimPeri.Text = DateTime.Now.ToString();
                CarregarGridDirecionamentos();
                carregaEspecialidades();
                carregaClassificacaoRisco();
                CarregaTiposDores(ddlDor1);
                CarregaTiposDores(ddlDor2);
                CarregaTiposDores(ddlDor3);
                CarregaTiposDores(ddlDor4);
                txtDtDor.Text = DateTime.Now.Date.ToString();
                txtHrDor.Text = DateTime.Now.ToString("HH:mm");
            }
        }

        /// <summary>
        /// Faz o Salvamento das informações do Pré-Atendimento na TBS194
        /// </summary>
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            bool erros = false;

            //----------------------------------------------------- Valida os Campos da Avaliação -----------------------------------------------------
            if (ddlClassRisco.SelectedValue == "")
            { AuxiliPagina.EnvioMensagemErro(this.Page, "A Classificação de Risco é Requerida"); erros = true; }

            if (ddlEspec.SelectedValue == "")
            { AuxiliPagina.EnvioMensagemErro(this.Page, "A Especialidade é Requerida"); erros = true; }

            //Recupera o ID do Pré-Atendimento da grid selecionada.
            int? idEncam = (HttpContext.Current.Session["VL_ENCAM_MEDIC_PRATB"] != null ? (int)HttpContext.Current.Session["VL_ENCAM_MEDIC_PRATB"] : (int?)null);

            //Verifica se algum encaminhamento foi selecionado
            if (!idEncam.HasValue)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar um Direcionamento!"); erros = true;
            }

            if (erros != true)
            {
                #region Dados do Direcionamento

                var resDirec = TBS195_ENCAM_MEDIC.RetornaPelaChavePrimaria(idEncam.Value);

                var dadosPac = TB07_ALUNO.RetornaPeloCoAlu(resDirec.CO_ALU);
                var dadosResp = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(resDirec.CO_RESP);

                #endregion

                TBS194_PRE_ATEND tbs194 = RetornaEntidade();

                tbs194.CO_RESP = resDirec.CO_RESP;
                tbs194.CO_ALU = resDirec.CO_ALU;
                tbs194.CO_SITUA_PRE_ATEND = "A";
                tbs194.DT_SITUA_PRE_ATEND = DateTime.Now;
                //tbs194.NU_SENHA_ATEND = txtSenha.Text;
                tbs194.NO_USU = dadosPac.NO_ALU;
                tbs194.CO_EMP = LoginAuxili.CO_EMP;
                tbs194.CO_SEXO_USU = dadosPac.CO_SEXO_ALU;
                tbs194.DT_NASC_USU = (dadosPac.DT_NASC_ALU.HasValue ? dadosPac.DT_NASC_ALU.Value : Convert.ToDateTime("01/01/1900"));
                tbs194.NU_PESO = (!string.IsNullOrEmpty(txtPeso.Text) ? decimal.Parse(txtPeso.Text) : (decimal?)null);
                tbs194.NU_ALTU = (!string.IsNullOrEmpty(txtAltura.Text) ? decimal.Parse(txtAltura.Text) : (decimal?)null);
                tbs194.NU_TEMP = (!string.IsNullOrEmpty(txtTemper.Text) ? decimal.Parse(txtTemper.Text) : (decimal?)null);
                tbs194.HR_TEMP = (!string.IsNullOrEmpty(txtHoraTemper.Text) ? txtHoraTemper.Text : null);
                tbs194.NU_PRES_ARTE = (!string.IsNullOrEmpty(txtPressArt.Text) ? txtPressArt.Text : null);
                tbs194.HR_PRES_ARTE = (!string.IsNullOrEmpty(txtHoraPressArt.Text) ? txtHoraPressArt.Text : null);
                tbs194.NU_GLICE = (!string.IsNullOrEmpty(txtGlicem.Text) ? int.Parse(txtGlicem.Text) : (int?)null);
                tbs194.HR_GLICE = (!string.IsNullOrEmpty(txtHoraGlicem.Text) ? txtHoraGlicem.Text : null);

        //================>Alterar o tipo desse nis no banco
                //tbs194.NU_NIS = (dadosPac.NU_NIS.HasValue ? Convert.ToInt64(dadosPac.NU_NIS.Value) : (int?)null);
                tbs194.NU_CPF_RESP = dadosResp.NU_CPF_RESP;
                tbs194.NU_CPF_USU = dadosPac.NU_CPF_ALU;
                tbs194.NO_RESP = dadosPac.NO_ALU;
                tbs194.NU_TEL_RESP = dadosResp.NU_TELE_RESI_RESP;
                tbs194.NU_CEL_RESP = dadosResp.NU_TELE_CELU_RESP;
                tbs194.NU_TEL_USU = dadosPac.NU_TELE_RESI_ALU;
                tbs194.NU_CEL_USU = dadosPac.NU_TELE_CELU_ALU;
                tbs194.CO_GRAU_PAREN = dadosPac.CO_GRAU_PAREN_RESP;
                tbs194.FL_DIABE = (chkDiabetes.Checked ? "S" : "N");
                tbs194.DE_DIABE = (chkDiabetes.Checked ? ddlDiabetes.SelectedValue : null);
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
                tbs194.CO_SEXO_RESP = dadosResp.CO_SEXO_RESP;
                tbs194.DT_NASC_RESP = dadosResp.DT_NASC_RESP;
                tbs194.FL_ATEND_MEDIC = "N";
                tbs194.TB250_OPERA = (resDirec.ID_OPER.HasValue ? (TB250_OPERA.RetornaPelaChavePrimaria(resDirec.ID_OPER.Value)) : null);
                tbs194.TBS337_TIPO_DORES = (!string.IsNullOrEmpty(ddlDor1.SelectedValue) ? TBS337_TIPO_DORES.RetornaPelaChavePrimaria(int.Parse(ddlDor1.SelectedValue)) : null);
                tbs194.TBS337_TIPO_DORES1 = (!string.IsNullOrEmpty(ddlDor2.SelectedValue) ? TBS337_TIPO_DORES.RetornaPelaChavePrimaria(int.Parse(ddlDor2.SelectedValue)) : null);
                tbs194.TBS337_TIPO_DORES2 = (!string.IsNullOrEmpty(ddlDor3.SelectedValue) ? TBS337_TIPO_DORES.RetornaPelaChavePrimaria(int.Parse(ddlDor3.SelectedValue)) : null);
                tbs194.TBS337_TIPO_DORES3 = (!string.IsNullOrEmpty(ddlDor4.SelectedValue) ? TBS337_TIPO_DORES.RetornaPelaChavePrimaria(int.Parse(ddlDor4.SelectedValue)) : null);

                //Trata Data e Hora da SENHA inserido no Cadastro para salvar em um campo só, como DateTime.
                #region e

                //DateTime dtSen = Convert.ToDateTime(txtDataSenha.Text);
                //DateTime hrSen = Convert.ToDateTime(txtHoraSenha.Text);

                //int hrs = int.Parse(txtHoraSenha.Text.Substring(0, 2));
                //int min = int.Parse(txtHoraSenha.Text.Substring(3, 2));

                //DateTime totDtHrSenha = dtSen.AddHours(hrs).AddMinutes(min);

                //DateTime totDtHrSenhaValid = totDtHrSenha;

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
                //tbs194.DT_SENHA_ATEND = totDtHrSenhaValid;

                TBS194_PRE_ATEND.SaveOrUpdate(tbs194);

                TBS195_ENCAM_MEDIC tbs195 = TBS195_ENCAM_MEDIC.RetornaPelaChavePrimaria(resDirec.ID_ENCAM_MEDIC);
                tbs195.ID_PRE_ATEND = tbs194.ID_PRE_ATEND;
                tbs195.CO_PRE_ATEND = tbs194.CO_PRE_ATEND;
                tbs195.CO_SITUA_ENCAM_MEDIC = "E";
                TBS195_ENCAM_MEDIC.SaveOrUpdate(tbs195, true);

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
        /// Carrega os direcionamentos ainda sem acolhimento
        /// </summary>
        private void CarregarGridDirecionamentos()
        {
            DateTime dtIni = (!string.IsNullOrEmpty(IniPeri.Text) ? DateTime.Parse(IniPeri.Text) : DateTime.Now);
            DateTime dtFim = (!string.IsNullOrEmpty(FimPeri.Text) ? DateTime.Parse(FimPeri.Text) : DateTime.Now);

            var res = (from tbs195 in TBS195_ENCAM_MEDIC.RetornaTodosRegistros()
                       join tb250 in TB250_OPERA.RetornaTodosRegistros() on tbs195.ID_OPER equals tb250.ID_OPER into l1
                       from loper in l1.DefaultIfEmpty()
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs195.CO_COL equals tb03.CO_COL
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs195.CO_ALU equals tb07.CO_ALU
                       join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tbs195.CO_ESPEC equals tb63.CO_ESPECIALIDADE
                       where tbs195.CO_SITUA_ENCAM_MEDIC == "A"
                       && tbs195.ID_PRE_ATEND == null
                       && (tbs195.DT_CADAS_ENCAM >= dtIni)

                       select new EncaminhamentosMedicos
                       {
                           CO_TIPO_RISCO = tbs195.NR_CLASS_RISCO,
                           NO_PAC_RECEB = tb07.NO_ALU,
                           ID_ENCAM_MEDIC = tbs195.ID_ENCAM_MEDIC,
                           NO_ESPEC = tb63.NO_ESPECIALIDADE,
                           CO_ESPEC = tb63.CO_ESPECIALIDADE,
                           CO_ENCAM_MEDIC = tbs195.CO_ENCAM_MEDIC,
                           dt_nascimento = tb07.DT_NASC_ALU,
                           ID_PRE_ATEND = tbs195.ID_PRE_ATEND,
                           CO_SEXO = tb07.CO_SEXO_ALU,
                           dataEncamMed = tbs195.DT_CADAS_ENCAM,
                           CO_ALU = tbs195.CO_ALU,
                           CO_RESP = tbs195.CO_RESP,
                           CO_COL = tbs195.CO_COL,
                           NM_OPER = loper.NOM_OPER,
                           ID_OPER = loper.ID_OPER,
                           ID_PLAN = tbs195.ID_PLAN,
                           NO_COL = tb03.NO_COL,
                       }).OrderBy(w => w.CO_TIPO_RISCO).ThenByDescending(w => w.dataEncamMed).ToList();

            //Faz uma lista com os 10 últimos acolhimentos e insere na lista anterior
            #region 10 últimos acolhimentos
            var resAntigos = (from tbs195 in TBS195_ENCAM_MEDIC.RetornaTodosRegistros()
                              join tb250 in TB250_OPERA.RetornaTodosRegistros() on tbs195.ID_OPER equals tb250.ID_OPER into l1
                              from loper in l1.DefaultIfEmpty()
                              join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs195.CO_COL equals tb03.CO_COL
                              join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs195.CO_ALU equals tb07.CO_ALU
                              join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tbs195.CO_ESPEC equals tb63.CO_ESPECIALIDADE
                              where tbs195.CO_SITUA_ENCAM_MEDIC == "A"
                              && (tbs195.DT_CADAS_ENCAM < dtIni)
                              && tbs195.ID_PRE_ATEND == null
                              select new EncaminhamentosMedicos
                              {
                                  CO_TIPO_RISCO = tbs195.NR_CLASS_RISCO,
                                  NO_PAC_RECEB = tb07.NO_ALU,
                                  ID_ENCAM_MEDIC = tbs195.ID_ENCAM_MEDIC,
                                  NO_ESPEC = tb63.NO_ESPECIALIDADE,
                                  CO_ESPEC = tb63.CO_ESPECIALIDADE,
                                  CO_ENCAM_MEDIC = tbs195.CO_ENCAM_MEDIC,
                                  dt_nascimento = tb07.DT_NASC_ALU,
                                  ID_PRE_ATEND = tbs195.ID_PRE_ATEND,
                                  CO_SEXO = tb07.CO_SEXO_ALU,
                                  dataEncamMed = tbs195.DT_CADAS_ENCAM,
                                  CO_ALU = tbs195.CO_ALU,
                                  CO_RESP = tbs195.CO_RESP,
                                  CO_COL = tbs195.CO_COL,
                                  NM_OPER = loper.NOM_OPER,
                                  ID_OPER = loper.ID_OPER,
                                  ID_PLAN = tbs195.ID_PLAN,
                                  ANTIGO = 1,
                                  NO_COL = tb03.NO_COL,
                              }).OrderByDescending(w => w.dataEncamMed).Take(10).ToList();

            foreach (var i in resAntigos) // passa por cada item da segunda lista e insere na primeira
            {
                res.Add(i);
            }

            //Reordena os itens
            res = res.OrderBy(w => w.CO_TIPO_RISCO).ThenByDescending(q => q.dataEncamMed).ToList();

            #endregion

            grdEncamMedic.DataSource = res;
            grdEncamMedic.DataBind();
        }


        public class EncaminhamentosMedicos
        {
            public int ANTIGO { get; set; }

            //Informações do Paciente
            public int CO_RESP { get; set; }
            public int CO_ALU { get; set; }
            public int? ID_OPER { get; set; }
            public int? ID_PLAN { get; set; }
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

            //Informações do médico
            public int? CO_COL { get; set; }
            public string NO_COL { get; set; }

            //Informações do pré-atendimento
            public int? ID_PRE_ATEND { get; set; }
            public string NU_SENHA
            {
                get
                {
                    //Resgata a informação de senha do pré-atendimento
                    string senha = " - ";
                    if (this.ID_PRE_ATEND.HasValue)
                    {
                        var res = (from tbs194 in TBS194_PRE_ATEND.RetornaTodosRegistros()
                                   where tbs194.ID_PRE_ATEND == this.ID_PRE_ATEND.Value
                                   select new { tbs194.NU_SENHA_ATEND }).FirstOrDefault();
                        senha = res != null ? res.NU_SENHA_ATEND : " - ";
                    }
                    return senha;
                }
            }
            public DateTime? dataPreAtend
            {
                get
                {
                    //Resgata a informação de data do pré-atendimento
                    DateTime? dt = (DateTime?)null;
                    if (ID_PRE_ATEND.HasValue)
                    {
                        dt = TBS194_PRE_ATEND.RetornaPelaChavePrimaria(this.ID_PRE_ATEND.Value).DT_PRE_ATEND;
                    }
                    return dt;
                }
            }
            public string dataPreAtendValid
            {
                get
                {
                    return (this.dataPreAtend.HasValue ? this.dataPreAtend.Value.ToString("dd/MM/yy") : "");
                }
            }
            public string horaPreAtendValid
            {
                get
                {
                    return (this.dataPreAtend.HasValue ? this.dataPreAtend.Value.ToString("HH:mm") : "");
                }
            }
            public string DTHRPreAtend
            {
                get
                {
                    return this.dataPreAtendValid + " - " + this.horaPreAtendValid;
                }
            }

            //Informações gerais do encaminhamento
            public int CO_TIPO_RISCO { get; set; }
            public string CLASS_RISCO
            {
                get
                {
                    string s = "";
                    switch (this.CO_TIPO_RISCO)
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
            public int ID_ENCAM_MEDIC { get; set; }
            public string NO_ESPEC { get; set; }
            public int CO_ESPEC { get; set; }
            public string CO_ENCAM_MEDIC { get; set; }
            public string NM_OPER { get; set; }

            //Trata data e hora do encaminhamento
            public DateTime? dataEncamMed { get; set; }
            public string dataEMValid
            {
                get
                {
                    return this.dataEncamMed.Value.ToString("dd/MM/yy");
                }
            }
            public string horaEMValid
            {
                get
                {
                    return this.dataEncamMed.Value.ToString("HH:mm");
                }
            }
            public string DTHREncamMed
            {
                get
                {
                    return this.dataEMValid + " - " + this.horaEMValid;
                }
            }

            //Trata as cores de acordo com a classificação de risco
            public string classCor
            {
                get
                {
                    switch (this.CO_TIPO_RISCO)
                    {
                        case 1:
                            return "Cor1";
                        case 2:
                            return "Cor2";
                        case 3:
                            return "Cor3";
                        case 4:
                            return "Cor4";
                        case 5:
                            return "Cor5";
                        default:
                            return "";
                    }
                }
            }
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
        /// Método responsável por colocar em session o id do encaminhamento selecionado
        /// </summary>
        /// <param name="ID_EMCAM_MEDIC"></param>
        private void GridSelecionada(int ID_EMCAM_MEDIC)
        {
            //Guarda a FLAG para saber se o Checkbox está sendo clicado para marcar ou desmarcar, neste caso, ele grava como para marcar.
            HttpContext.Current.Session.Add("FL_SELECT_GRID_PRATB", "S");

            //Guarda o Valor do Pré-Atendimento, para fins de posteriormente comparar este valor 
            HttpContext.Current.Session.Add("VL_ENCAM_MEDIC_PRATB", ID_EMCAM_MEDIC);
        }

        /// <summary>
        /// Exclui informações carregadas ao selecionar um item na grid
        /// </summary>
        private void GridDesselecionada()
        {
            //Guarda a FLAG para saber se o Checkbox está sendo clicado para marcar ou desmarcar, neste caso, ele grava como desmarcada.
            HttpContext.Current.Session.Add("FL_SELECT_GRID_PRATB", "N");
            HttpContext.Current.Session.Remove("VL_ENCAM_MEDIC_PRATB");
        }

        /// <summary>
        /// Devido ao método de reload na grid, ela perde a seleção do checkbox, este método coleta 
        /// </summary>
        private void selecionaGridPreAtend()
        {
            CheckBox chk;
            string idEncam;
            // Valida se a grid de atividades possui algum registro
            if (grdEncamMedic.Rows.Count != 0)
            {
                // Passa por todos os registros da grid de atividades
                foreach (GridViewRow linha in grdEncamMedic.Rows)
                {
                    idEncam = ((HiddenField)linha.Cells[0].FindControl("hidIdEncam")).Value;
                    int coPre = (int)HttpContext.Current.Session["VL_ENCAM_MEDIC_PRATB"];

                    if (int.Parse(idEncam) == coPre)
                    {
                        chk = (CheckBox)linha.Cells[0].FindControl("chkselectEn");
                        chk.Checked = true;
                        break;
                    }
                }
            }
        }

        #endregion

        #region Funções de Campo

        protected void imgPesqGrid_OnClick(object sender, EventArgs e)
        {
            CarregarGridDirecionamentos();
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //int coEspec = ddlPesqEspec.SelectedValue != "" ? int.Parse(ddlPesqEspec.SelectedValue) : 0;
            //int coDepto = ddlPesqLocal.SelectedValue != "" ? int.Parse(ddlPesqLocal.SelectedValue) : 0;

            CarregarGridDirecionamentos();

            //Verifica se a grid já possuia um registro selecionado antes, e chama um método responsável por selecioná-lo de novo no PRÉ-ATENDIMENTO
            if ((string)HttpContext.Current.Session["FL_SELECT_GRID_PRATB"] == "S")
            {
                selecionaGridPreAtend();
            }

            UpdatePanel1.Update();
            //updProfiPlantao.Update();
        }

        /// <summary>
        /// Evento chamado ao clicar em qualquer ponto de uma linha de informações sobre um pré-atendimento na Grid Superior
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdEncamMedic_SelectedIndexChanged(object sender, EventArgs e)
        {
            //--------> Define o código da Unidade Educacional Selecionada como a Unidade em uso
            if (grdEncamMedic.DataKeys[grdEncamMedic.SelectedIndex].Value != null)
            {
                // Passa por todos os registros da grid de Encaminhamentos
                foreach (GridViewRow linha in grdEncamMedic.Rows)
                {
                    CheckBox chk = (CheckBox)linha.Cells[0].FindControl("chkselectEn");
                    int idEncam = int.Parse((((HiddenField)linha.Cells[0].FindControl("hidIdEncam")).Value));

                    //Verifica se foi clicada uma linha diferente da selecionada, caso tenha sido, desmarca a linha
                    if (idEncam != Convert.ToInt32(grdEncamMedic.DataKeys[grdEncamMedic.SelectedIndex].Value))
                        chk.Checked = false;
                    //Se for a mesma linha então seleciona o checkbox correspondente
                    else
                    {
                        //Verifico se já está selecionado, caso já esteja eu recorro aos padrões de limpeza de dados
                        if (chk.Checked)
                        {
                            chk.Checked = false;
                            GridDesselecionada();
                        }
                        //Caso não esteja selecionado ainda, chamo os métodos responsáveis pelos carregamentos
                        else
                        {
                            chk.Checked = true;
                            GridSelecionada(idEncam);
                        }
                    }
                }
            }
        }

        protected void grdEncamMedic_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                e.Row.Attributes.Add("onClick", "javascript:__doPostBack('" + grdEncamMedic.UniqueID + "','Select$" + qtdLinhasGrid + "')");
                qtdLinhasGrid = qtdLinhasGrid + 1;

                //Se for registro antigo(um dos 10 antigos que são também apresentados), destaca ele em cor salmão
                if (((HiddenField)e.Row.Cells[0].FindControl("hidAntigos")).Value == "1")
                    e.Row.BackColor = System.Drawing.Color.AntiqueWhite;
            }
        }

        protected void chkselectEn_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            CheckBox chk;

            int coEncam;
            // Valida se a grid de atividades possui algum registro
            if (grdEncamMedic.Rows.Count != 0)
            {
                // Passa por todos os registros da grid de atividades
                foreach (GridViewRow linha in grdEncamMedic.Rows)
                {
                    chk = (CheckBox)linha.Cells[0].FindControl("chkselectEn");

                    // Desmarca todos os registros menos o que foi clicado
                    if (chk.ClientID != atual.ClientID)
                    {
                        chk.Checked = false;
                    }
                    else
                    {
                        if (chk.Checked)
                        {
                            coEncam = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidIdEncam")).Value);
                            GridSelecionada(coEncam);
                        }
                        else
                            GridDesselecionada();
                    }
                }
            }
        }

        #endregion
    }
}