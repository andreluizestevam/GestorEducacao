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
using System.Data;
using System.Collections;

namespace C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3203_RegistroPreAtendimentoUsuario
{
    public partial class Cadastro_B : System.Web.UI.Page
    {
        int qtdLinhasGrid = 0;
        ProcedimentosClinicos proc = new ProcedimentosClinicos();

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
                //CarregarGridDirecionamentos("")
                preencheddlistunidades();
                carregaEspecialidades();
                carregaEncaminhamento();                
                carregaprofissionalatendimento();
                carregaProfissionalResponsavel();
                carregaClassificacaoRisco();
                CarregaTiposDores(ddlDor1);
                CarregaTiposDores(ddlDor2);
                CarregaTiposDores(ddlDor3);
                CarregaTiposDores(ddlDor4);
                txtDtDor.Text = DateTime.Now.Date.ToString();
                txtHrDor.Text = DateTime.Now.ToString("HH:mm");
                txtHoraPressArt.Text = DateTime.Now.ToString("HH:mm");
                txtHoraTemper.Text = DateTime.Now.ToString("HH:mm");
                txtHoraGlicem.Text = DateTime.Now.ToString("HH:mm");
                tbvalorbatimento.Text = DateTime.Now.ToString("HH:mm");
                tbvalorsaturacao.Text = DateTime.Now.ToString("HH:mm");


                C2BR.GestorEducacao.BusinessEntities.Auxiliar.SQLDirectAcess direc = new C2BR.GestorEducacao.BusinessEntities.Auxiliar.SQLDirectAcess();
                System.Data.DataTable dt = new System.Data.DataTable();
                dt = direc.retornacolunas("select ID_PROC_MEDI_PROCE, CO_PROC_MEDI, NM_PROC_MEDI from TBS356_PROC_MEDIC_PROCE where LEN(co_proc_medi) = 10 ORDER BY NM_PROC_MEDI");

                //grdListarSIGTAP.DataSource = dt;
                //grdListarSIGTAP.DataBind();
                //Session["temp"] = grdListarSIGTAP.DataSource;
                ddlgrupoprocedimento = proc.DropGrupo(ddlgrupoprocedimento);
                ddlsubgrupoprocedimento.Items.Insert(0, new ListItem("Todos", "0"));

            }
        }
        protected void grdListarSIGTAP_SelectedIndexChanged(object sender, EventArgs e)
        {
            string teste = "";
        }
        protected void btnincluir_Click(object sender, EventArgs e)
        {
            //DataTable mDataTable = new DataTable();

            //DataColumn mDataColumn;
            //mDataColumn = new DataColumn();
            //mDataColumn.DataType = Type.GetType("System.String");
            //mDataColumn.ColumnName = "Codigo";
            //mDataTable.Columns.Add(mDataColumn);

            //mDataColumn = new DataColumn();
            //mDataColumn.DataType = Type.GetType("System.String");
            //mDataColumn.ColumnName = "Nome";
            //mDataTable.Columns.Add(mDataColumn);

            //DataRow linha;
            //foreach (GridViewRow linha2 in grdListarSIGTAP.Rows)
            //{
            //    if (((CheckBox)linha2.Cells[0].FindControl("chkselectEn")).Checked)
            //    {
            //        linha = mDataTable.NewRow();
            //        linha["Codigo"] = linha2.Cells[1].Text;
            //        linha["Nome"] = linha2.Cells[2].Text;
            //        mDataTable.Rows.Add(linha);
            //    }
            //}
            //Session["dtsigtab"] = mDataTable;
            //resDirec.CO_ALU
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

            if (ddlProfResp.SelectedValue == "")
            { AuxiliPagina.EnvioMensagemErro(this.Page, "O Profissional Responsável Triagem é Requerido"); erros = true; }
            
            //Recupera o ID do Pré-Atendimento da grid selecionada.
            int? idEncam = (HttpContext.Current.Session["VL_ENCAM_MEDIC_PRATB"] != null ? (int)HttpContext.Current.Session["VL_ENCAM_MEDIC_PRATB"] : (int?)null);

            //Verifica se algum encaminhamento foi selecionado
            if (!idEncam.HasValue)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar um Direcionamento!"); erros = true;
            }
            if (ddlEncamP.SelectedValue == "") {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar um encaminhamento de pasciênte!"); erros = true;
            }
            if (ddlprofatendimento.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar profissional do atendimento!"); erros = true;
            }
            if (erros != true)
            {
                #region Dados do Direcionamento

                int id_colaborador = int.Parse(ddlProfResp.SelectedValue);
                var resColaborador = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                                        where (tb03.CO_COL == id_colaborador)
                                        select new { tb03.CO_COL, tb03.CO_EMP }).FirstOrDefault();

                TBS174_AGEND_HORAR resDirec = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(idEncam.Value);
                //Atendimento clínico
                if (ddlEncamP.SelectedValue == "0") {

                    resDirec.CO_SITUA_AGEND_HORAR = "A";
                    resDirec.FL_AGEND_ENCAM = "S";
                    resDirec.FL_SITUA_TRIAGEM = "S";
                }//Recepção
                else
                {
                    resDirec.CO_SITUA_AGEND_HORAR = "A";
                    resDirec.FL_AGEND_ENCAM = "S";
                    resDirec.FL_SITUA_TRIAGEM = "S";
                }
              
                resDirec.CO_CLASS_RISCO = int.Parse(ddlClassRisco.SelectedValue);
                TBS174_AGEND_HORAR.SaveOrUpdate(resDirec);

                //resDirec.CO_SITUA_AGEND_HORAR = "E";
                //resDirec.FL_AGEND_ENCAM = "S";
                //resDirec.FL_SITUA_TRIAGEM = "S";
                //TBS174_AGEND_HORAR.SaveOrUpdate(resDirec);
                //var dadosPac = TB07_ALUNO.RetornaPeloCoAlu(resDirec.CO_ALU);
                //var dadosResp = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(resDirec.CO_RESP);

                #endregion

                TBS194_PRE_ATEND tbs194 = RetornaEntidade();

                tbs194.CO_EMP_FUNC = resColaborador.CO_EMP;
                tbs194.CO_COL_FUNC = resColaborador.CO_COL;

                //tbs194.CO_RESP = resDirec.CO_RESP;
                tbs194.CO_ALU = resDirec.CO_ALU;
                
                //Session["CO_ALU"] = resDirec.CO_ALU;

                tbs194.CO_SITUA_PRE_ATEND = "A";
                //tbs194.DT_SITUA_PRE_ATEND = DateTime.Now;
                //tbs194.NU_SENHA_ATEND = txtSenha.Text;
                tbs194.NO_USU = "dadosPac.NO_ALU";
                tbs194.CO_EMP = LoginAuxili.CO_EMP;
                //tbs194.CO_SEXO_USU = "dadosPac.CO_SEXO_ALU";
                tbs194.CO_SEXO_USU = "M";

                tbs194.DT_NASC_USU = DateTime.UtcNow; //(dadosPac.DT_NASC_ALU.HasValue ? dadosPac.DT_NASC_ALU.Value : Convert.ToDateTime("01/01/1900"));
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
                //tbs194.NU_CPF_RESP = dadosResp.NU_CPF_RESP;
                //tbs194.NU_CPF_USU = dadosPac.NU_CPF_ALU;
                //tbs194.NO_RESP = dadosPac.NO_ALU;
                //tbs194.NU_TEL_RESP = dadosResp.NU_TELE_RESI_RESP;
                //tbs194.NU_CEL_RESP = dadosResp.NU_TELE_CELU_RESP;
                //tbs194.NU_TEL_USU = dadosPac.NU_TELE_RESI_ALU;
                //tbs194.NU_CEL_USU = dadosPac.NU_TELE_CELU_ALU;
                //tbs194.CO_GRAU_PAREN = dadosPac.CO_GRAU_PAREN_RESP;
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
                tbs194.DT_PRE_ATEND = DateTime.UtcNow;
                //tbs194.CO_EMP_FUNC = LoginAuxili.CO_EMP;
                //tbs194.CO_COL_FUNC = LoginAuxili.CO_COL;
                tbs194.NR_IP_FUNC = Request.UserHostAddress;
                //tbs194.CO_SEXO_RESP = dadosResp.CO_SEXO_RESP;
                //tbs194.DT_NASC_RESP = dadosResp.DT_NASC_RESP;
                tbs194.FL_ATEND_MEDIC = "N";
                //tbs194.TB250_OPERA = (resDirec.ID_OPER.HasValue ? (TB250_OPERA.RetornaPelaChavePrimaria(resDirec.ID_OPER.Value)) : null);
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
                //tbs194.DT_DOR = totDtHrDorValid;
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
                    //seq = res.CO_PRE_ATEND.Substring(7, 7);
                    //seq2 = int.Parse(seq);
                }

                //seqConcat = seq2 + 1;
                //seqcon = seqConcat.ToString().PadLeft(7, '0');

                //tbs194.CO_PRE_ATEND = ano + coUnid.PadLeft(3, '0') + "PA" + seqcon;
                tbs194.CO_PRE_ATEND = resDirec.ID_AGEND_HORAR.ToString();
                
                //tbs194.DT_SENHA_ATEND = totDtHrSenhaValid;

                TBS194_PRE_ATEND.SaveOrUpdate(tbs194);
                TBS194_PRE_ATEND.GravaComplemento(tbbatimento.Text, tbvalorbatimento.Text, tbsaturacao.Text, tbvalorsaturacao.Text, ddlDiabetes.SelectedValue);

                try
                {
                    //André Grava telas criadas
                    C2BR.GestorEducacao.UI.Object.TBS478_ATEND_GESTANTE_BO BO = new C2BR.GestorEducacao.UI.Object.TBS478_ATEND_GESTANTE_BO();
                    C2BR.GestorEducacao.UI.Object.TBS478_ATEND_GESTANTE_BUSINESS insere = new C2BR.GestorEducacao.UI.Object.TBS478_ATEND_GESTANTE_BUSINESS();
                    BO = (C2BR.GestorEducacao.UI.Object.TBS478_ATEND_GESTANTE_BO)Session["TBS478_ATEND_GESTANTE_BO"];
                    if (BO is null){}
                    else
                    {
                        try { BO.CO_ALUNO = Convert.ToInt32(Session["CO_ALU"].ToString()); } catch { }
                        try { BO.COD_GESTANTE = Convert.ToInt16(resDirec.CO_ALU); } catch { }
                        try { BO.CO_PRE_ATEND = Convert.ToInt16(tbs194.CO_PRE_ATEND); } catch { }
                        insere.InsereTBS478(BO);
                    }
                    DataTable dt = new DataTable();
                    dt = (DataTable)Session["dtsigtab"];
                    if(dt.Rows.Count > 0)
                    {
                        for(int i = 0; i < dt.Rows.Count;i++)
                        {
                            proc.InsereProcedimentos(Session["CO_ALU"].ToString(), dt.Rows[i]["ID_PROCEDIMENTO"].ToString(), dt.Rows[i]["CO_ALUNO_ID_AGEND_HORAR"].ToString(), ddlprofatendimento.SelectedValue, ddlProfResp.SelectedValue);
                        }
                    }
                    Session["CO_ALU"] = null;
                    Session["IDADE"] = null;
                    Session["SEXO"] = null;
                }
                catch { }

                AuxiliPagina.RedirecionaParaPaginaSucesso("Pré-Atendimento Registrado com sucesso.", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
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

        private void CarregarGridDirecionamentos(string temp)
        {
            DateTime dtIni = (!string.IsNullOrEmpty(IniPeri.Text) ? DateTime.Parse(IniPeri.Text) : DateTime.Now);
            DateTime dtFim = (!string.IsNullOrEmpty(FimPeri.Text) ? DateTime.Parse(FimPeri.Text) : DateTime.Now);
            BusinessEntities.Auxiliar.SQLDirectAcess direct = new BusinessEntities.Auxiliar.SQLDirectAcess();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = direct.retornacolunas(" select 'dataEncamMed' =  A.DT_ENCAM,'DTHREncamMed' = A.dt_encam, 'A.ID_AGEND_HORA' = A.ID_AGEND_HORAR,'CO_TIPO_RISCO' = A.CO_CLASS_RISCO, 'NO_ESPEC' = B.DE_FUNC_COL, "+
                                       " 'CO_ESPEC' = B.CO_FUN, 'dt_nascimento' = C.DT_NASC_ALU, 'CO_SEXO' = C.CO_SEXO_ALU, 'dataEncamMed' = A.DT_ENCAM, " +
                                       " 'CO_COL' = A.CO_COL, 'NM_OPER' = C.NU_REGI_SUS_ALU, 'NO_COL' = B.NO_COL, A.DT_AGEND_HORAR, 'ID_AGEND_HORA' = A.ID_AGEND_HORAR, C.CO_RESP, c.ID_OPER, C.ID_PLAN, 'ANTIGO' = null, A.CO_ALU,'CO_ALUN' = CONCAT(C.CO_ALU,':',A.ID_AGEND_HORAR) " + 
                                       " from TBS174_AGEND_HORAR A  " +
                                       " inner join TB03_COLABOR B on b.CO_COL = a.CO_COL  " +
                                       " inner join TB07_ALUNO C on C.CO_ALU = A.CO_ALU  " +
                                       " where A.DT_ENCAM is not null and A.CO_CLASS_RISCO is null  " +
                                       " and DT_AGEND_HORAR >=  '" + dtIni + "'" +
                                       " and DT_AGEND_HORAR <=  '" + dtFim + "'" +
                                       " union all  " +
                                       " select 'dataEncamMed' =  A.DT_ENCAM,'DTHREncamMed' = A.dt_encam, 'A.ID_AGEND_HORA' = A.ID_AGEND_HORAR,'CO_TIPO_RISCO' = A.CO_CLASS_RISCO, 'NO_ESPEC' = B.DE_FUNC_COL, " +
                                       " 'CO_ESPEC' = B.CO_FUN, 'dt_nascimento' = C.DT_NASC_ALU, 'CO_SEXO' = C.CO_SEXO_ALU, 'dataEncamMed' = A.DT_ENCAM, " +
                                       " 'CO_COL' = A.CO_COL, 'NM_OPER' = C.NU_REGI_SUS_ALU, 'NO_COL' = B.NO_COL, A.DT_AGEND_HORAR, 'ID_AGEND_HORA' = A.ID_AGEND_HORAR, C.CO_RESP, c.ID_OPER, C.ID_PLAN, 'ANTIGO' = null, A.CO_ALU,'CO_ALUN' = CONCAT(C.CO_ALU,':',A.ID_AGEND_HORAR) " +
                                       " from TBS174_AGEND_HORAR A  " +
                                       " inner join TB03_COLABOR B on b.CO_COL = a.CO_COL  " +
                                       " inner join TB07_ALUNO C on C.CO_ALU = A.CO_ALU  " +
                                       " where A.DT_ENCAM is not null  " +
                                       " and A.CO_CLASS_RISCO is not null  " +
                                       " and DT_AGEND_HORAR >=  '" + dtIni + "'" +
                                       " and DT_AGEND_HORAR <=  '" + dtFim + "'" +
                                       " order by A.DT_AGEND_HORAR ");

            grdEncamMedic.DataSource = dt;
            grdEncamMedic.DataBind();
        }
        /// <summary>
        /// Carrega os direcionamentos ainda sem acolhimento
        /// </summary>
        private void CarregarGridDirecionamentos()
        {
            DateTime dtIni = (!string.IsNullOrEmpty(IniPeri.Text.Substring(0, 10)) ? DateTime.Parse(IniPeri.Text.Substring(0, 10)) : DateTime.Now);
            DateTime dtFim = (!string.IsNullOrEmpty(FimPeri.Text.Substring(0, 10)) ? DateTime.Parse(FimPeri.Text.Substring(0, 10)) : DateTime.Now);

            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       //join tb250 in TB250_OPERA.RetornaTodosRegistros() on tbs174.ID_OPER equals tb250.ID_OPER into l1
                       //from loper in l1.DefaultIfEmpty()
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                       //join tbs194 in TBS194_PRE_ATEND.RetornaTodosRegistros() on tbs174.CO_ALU equals tbs194.CO_ALU
                       //* join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tbs174.CO_ESPEC equals tb63.CO_ESPECIALIDADE
                       where tbs174.DT_ENCAM != null && tbs174.CO_CLASS_RISCO == null
                       && tbs174.DT_AGEND_HORAR >= dtIni
                       && tbs174.DT_AGEND_HORAR <= dtFim
                       //where tbs174.FL_AGEND_ENCAM == "T"
                       ////&& tbs174.ID_PRE_ATEND == null
                       ////&& (tbs174.DT_CADAS_ENCAM >= dtICIALIDADE.RetornaTodosRegistros() on tbs174.CO_ESPEC equals tb63.CO_ESPECIALIDADE
                       //where tbs174.DT_ENCAM != null  
                       //where tbs174.FL_AGEND_ENCAM == "T"
                       ////&& tbs174.ID_PRE_ATEND == null
                       ////&& (tbs174.DT_CADAS_ENCAM >= dtIni)

                       select new EncaminhamentosMedicos
                       {
                           CO_ALUNO_ID_AGEND_HORAR = tbs174.ID_AGEND_HORAR,
                           TP_AGEND_HORA = tbs174.TP_AGEND_HORAR,
                           CO_TIPO_RISCO = tbs174.CO_CLASS_RISCO != null? tbs174.CO_CLASS_RISCO:0,
                           __ALTURA = "1.50",
                           __PESO = "90",
                           __FICHA = "Paciênte ainda não fez Triagem " ,
                    
                           //CO_TIPO_RISCO = tbs194.CO_TIPO_RISCO != null? tbs194.CO_TIPO_RISCO:0,
                           NO_PAC_RECEB = tb07.NO_ALU,
                           //ID_ENCAM_MEDIC = tbs174.ID_ENCAM_MEDIC,;
                           ID_AGEND_HORA = tbs174.ID_AGEND_HORAR,
                           NO_ESPEC = tb03.DE_FUNC_COL,
                           CO_ESPEC = tb03.CO_FUN,
                           //CO_ENCAM_MEDIC = tbs194.CO_ENCAM_MEDIC,
                           dt_nascimento = tb07.DT_NASC_ALU,
                           //ID_PRE_ATEND = tbs194.ID_PRE_ATEND,
                     
                           CO_SEXO = tb07.CO_SEXO_ALU,
                           dataEncamMed = tbs174.DT_ENCAM,
                           CO_ALU = tb07.CO_ALU,
                           //CO_RESP = tbs174.CO_RESP,
                           CO_COL = tbs174.CO_COL,
                           NM_OPER = tb07.NU_REGI_SUS_ALU != null ? tb07.NU_REGI_SUS_ALU : "S/REG",
                           //ID_OPER = loper.ID_OPER,
                           //ID_PLAN = tbs174.ID_PLAN,
                           NO_COL = tb03.NO_COL,
                       }).OrderBy(w => w.ID_AGEND_HORA).ToList();
                        //OrderBy(w => w.CO_TIPO_RISCO).ThenByDescending(w => w.dataEncamMed).ToList();

            //Faz uma lista com os 10 últimos acolhimentos e insere na lista anterior
            #region 10 últimos acolhimentos

            var resAntigos = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                  //join tb250 in TB250_OPERA.RetornaTodosRegistros() on tbs174.ID_OPER equals tb250.ID_OPER into l1
                                  //from loper in l1.DefaultIfEmpty()
                              join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                              join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                              //join tbs194 in TBS194_PRE_ATEND.RetornaTodosRegistros() on tbs174.CO_ALU equals tbs194.CO_ALU
                              //* join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tbs174.CO_ESPEC equals tb63.CO_ESPECIALIDADE
                              where tbs174.DT_ENCAM != null && tbs174.CO_CLASS_RISCO != null
                              && tbs174.DT_AGEND_HORAR >= dtIni
                              && tbs174.DT_AGEND_HORAR <= dtFim
                              //where tbs174.FL_AGEND_ENCAM == "T"
                              ////&& tbs174.ID_PRE_ATEND == null
                              ////&& (tbs174.DT_CADAS_ENCAM >= dtICIALIDADE.RetornaTodosRegistros() on tbs174.CO_ESPEC equals tb63.CO_ESPECIALIDADE
                              //where tbs174.DT_ENCAM != null  
                              //where tbs174.FL_AGEND_ENCAM == "T"
                              ////&& tbs174.ID_PRE_ATEND == null
                              ////&& (tbs174.DT_CADAS_ENCAM >= dtIni)

                              select new EncaminhamentosMedicos
                              {
                                  CO_ALUNO_ID_AGEND_HORAR = tbs174.ID_AGEND_HORAR,
                                  TP_AGEND_HORA = tbs174.TP_AGEND_HORAR,
                                  CO_TIPO_RISCO = tbs174.CO_CLASS_RISCO != null ? tbs174.CO_CLASS_RISCO : 0,
                                  __ALTURA = "1.50",
                                  __PESO = "90",


                                  //CO_TIPO_RISCO = tbs194.CO_TIPO_RISCO != null? tbs194.CO_TIPO_RISCO:0,
                                  NO_PAC_RECEB = tb07.NO_ALU,
                                  //ID_ENCAM_MEDIC = tbs174.ID_ENCAM_MEDIC,;
                                  ID_AGEND_HORA = tbs174.ID_AGEND_HORAR,
                                  NO_ESPEC = tb03.DE_FUNC_COL,
                                  CO_ESPEC = tb03.CO_FUN,
                                  //CO_ENCAM_MEDIC = tbs194.CO_ENCAM_MEDIC,
                                  dt_nascimento = tb07.DT_NASC_ALU,
                                  //ID_PRE_ATEND = tbs194.ID_PRE_ATEND,

                                  CO_SEXO = tb07.CO_SEXO_ALU,
                                  dataEncamMed = tbs174.DT_ENCAM,
                                  CO_ALU = tb07.CO_ALU,
                                  //CO_RESP = tbs174.CO_RESP,
                                  CO_COL = tbs174.CO_COL,
                                  NM_OPER = tb07.NU_REGI_SUS_ALU != null ? tb07.NU_REGI_SUS_ALU : "S/REG",
                                  //ID_OPER = loper.ID_OPER,
                                  //ID_PLAN = tbs174.ID_PLAN,
                                  NO_COL = tb03.NO_COL,
                              }).OrderBy(w => w.ID_AGEND_HORA).ToList();
                              //OrderBy(w => w.CO_TIPO_RISCO).ThenByDescending(w => w.dataEncamMed).ToList();
            /* 
              var resAntigos = (from tbs195 in TBS195_ENCAM_MEDIC.RetornaTodosRegistros()
                                join tb250 in TB250_OPERA.RetornaTodosRegistros() on tbs195.ID_OPER equals tb250.ID_OPER into l1
                                from loper in l1.DefaultIfEmpty()
                                join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs195.CO_COL equals tb03.CO_COL
                                join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs195.CO_ALU equals tb07.CO_ALU
                                join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tbs195.CO_ESPEC equals tb63.CO_ESPECIALIDADE
                                where
                                tbs195.DT_CADAS_ENCAM != null &&
                                tbs195.CO_SITUA_ENCAM_MEDIC == "A"
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
              */
            foreach (var i in resAntigos) // passa por cada item da segunda lista e insere na primeira
            {
                string id_encam_str = i.ID_AGEND_HORA.ToString();
                var ret = GestorEntities.CurrentContext.TBS194_PRE_ATEND.FirstOrDefault(p => p.CO_PRE_ATEND == id_encam_str);
                if(ret != null){
                    i.__FICHA = i.NO_PAC_RECEB + "\n";
                    i.__FICHA = i.__FICHA + " Altura: " + ret.NU_ALTU.ToString() + " -- Peso : " + ret.NU_PESO.ToString() + "-- PA:" + ret.NU_PRES_ARTE + " -- Temp.: " + ret.NU_TEMP.ToString() + " -- Glicemia : " + ret.NU_GLICE.ToString() + "\n";
                    i.__FICHA = i.__FICHA + " Dores: " + ret.FL_SINTO_DORES + " -- Enjoo" + ret.FL_SINTO_ENJOO + " -- Vômito : " + ret.FL_SINTO_VOMIT + " -- Febre:" + ret.FL_SINTO_FEBRE + " -- Diabetes:" + ret.FL_DIABE + "\n ";
                    i.__FICHA = i.__FICHA + " Fumante : " + ret.FL_FUMAN + " -- Alcool:" + ret.FL_ALCOO + " -- Cirurgia:" + ret.DE_CIRUR + "Alergia:" + ret.DE_ALERG + "\n ";

                    i.__FICHA = i.__FICHA + " Medicação Uso Contínuo : " + ret.DE_MEDIC_USO_CONTI + "\n ";

                    i.__FICHA = i.__FICHA + " Medicação : " + ret.DE_MEDIC + "\n ";
                    i.__FICHA = i.__FICHA + " Sintomas : " + ret.DE_SINTO + "\n ";                    
                }

                res.Add(i);
            }
            
            //Reordena os itens
            //res = res.OrderBy(w => w.CO_TIPO_RISCO).ThenByDescending(q => q.dataEncamMed).ToList();

            #endregion

            grdEncamMedic.DataSource = res;
            grdEncamMedic.DataBind();
        }

        public string boolParaStr(bool fl){
            if (fl == null) {
                return "-";
            }

            if (fl)
            {
                return "Sim";
            }
            else {
                return "Não";
            }
        }

        public class EncaminhamentosMedicos
        {
            public string CO_ALUNO { get; set; }
            public int CO_ALUNO_ID_AGEND_HORAR { get; set; }
            public int ANTIGO { get; set; }
            public string  __ALTURA { get; set; }
            public string TP_AGEND_HORA { get; set; }
            public string __PESO { get; set; }
            public string __FICHA { get; set; }
            //Informações do Paciente
            public int CO_RESP { get; set; }
            public int CO_ALU { get; set; }
            public int? ID_OPER { get; set; }
            public int? ID_PLAN { get; set; }
            public string NO_PAC
            {
                get
                {
                    return this.NO_PAC_RECEB; //(this.NO_PAC_RECEB.Length > 25 ? this.NO_PAC_RECEB.Substring(0, 25) + "..." : this.NO_PAC_RECEB);
                }
            }
            public string NO_PAC_RECEB { get; set; }
            public DateTime? dt_nascimento { get; set; }
            public string NU_IDADE
            {
                get
                {
                    return AuxiliFormatoExibicao.FormataDataNascimento(
                        this.dt_nascimento, 
                        AuxiliFormatoExibicao.ETipoDataNascimento.padraoSaudeCompleto
                    );

                    //string idade = " - ";

                    ////Calcula a idade do Paciente de acordo com a data de nascimento do mesmo.
                    //if (this.dt_nascimento.HasValue)
                    //{
                    //    int anos = DateTime.Now.Year - dt_nascimento.Value.Year;

                    //    if (DateTime.Now.Month < dt_nascimento.Value.Month || (DateTime.Now.Month == dt_nascimento.Value.Month && DateTime.Now.Day < dt_nascimento.Value.Day))
                    //        anos--;

                    //    idade = anos.ToString();
                    //}
                    //return idade;
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
                        //dt = TBS194_PRE_ATEND.RetornaPelaChavePrimaria(this.ID_PRE_ATEND.Value).DT_PRE_ATEND;
                        dt = null;
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
            public int? ID_AGEND_HORA { get; set; }
            //Informações gerais do encaminhamento
            public int? CO_TIPO_RISCO { get; set; }
            public string CLASS_RISCO
            {
                get
                {
                    string s = "EM ABERTO";
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
            public string NM_OPER_FORMATADO {
                get
                {
                    string formatado = "";
                    int total = this.NM_OPER.Length;
                    int resto_divisao = total % 4;
                    formatado = this.NM_OPER.Substring(0, resto_divisao);
                    int iteracoes = (total / 4) - resto_divisao / 4;
                    int indice_inicial = resto_divisao;
                    while (iteracoes > 0) {
                        formatado += " " + this.NM_OPER.Substring(indice_inicial, 4);
                        indice_inicial += 4;
                        iteracoes--;
                    }
                    return formatado; //(this.NO_PAC_RECEB.Length > 25 ? this.NO_PAC_RECEB.Substring(0, 25) + "..." : this.NO_PAC_RECEB);
                }
            }
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

        private void carregaEncaminhamento()
        {
            ddlEncamP.Items.Insert(0, new ListItem("Atendimento clinico", "0"));
            ddlEncamP.Items.Insert(1, new ListItem("Finalizado na triagem", "1"));
            ddlEncamP.Items.Insert(2, new ListItem("Retornar a Recepção", "2"));
            ddlEncamP.Items.Insert(3, new ListItem("Serviço Ambulatorial", "3"));
            ddlEncamP.Items.Insert(4, new ListItem("Vacina", "4"));            
        }


        private void carregaprofissionalatendimento()
        {
            int temp = Convert.ToInt32(LoginAuxili.CO_UNID.ToString());
            var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                       where (tb03.FLA_PROFESSOR == "S") && (tb03.CO_SITU_COL == "ATI") && (tb03.CO_EMP == temp)
                       select new { tb03.CO_COL, tb03.CO_EMP, tb03.NO_COL }
                       ).OrderBy(w => w.NO_COL);

            ddlprofatendimento.DataTextField = "NO_COL";
            ddlprofatendimento.DataValueField = "CO_COL";
            ddlprofatendimento.DataSource = res;
            ddlprofatendimento.DataBind();
            ddlprofatendimento.Items.Insert(0, new ListItem("Selecione", ""));
            ddlprofatendimento.SelectedIndex = ddlprofatendimento.Items.IndexOf(ddlprofatendimento.Items.FindByText(LoginAuxili.NOME_USU_LOGADO));

        }
        private void carregaProfissionalResponsavel() {

            var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                       where (tb03.FLA_PROFESSOR == "S") &&  (tb03.CO_SITU_COL == "ATI")
                       select new { tb03.CO_COL, tb03.CO_EMP, tb03.NO_COL }
                       ).OrderBy(w => w.NO_COL);

            ddlProfResp.DataTextField = "NO_COL";
            ddlProfResp.DataValueField = "CO_COL";
            ddlProfResp.DataSource = res;
            ddlProfResp.DataBind();
            ddlProfResp.Items.Insert(0, new ListItem("Selecione", ""));
            ddlProfResp.SelectedIndex = ddlProfResp.Items.IndexOf(ddlProfResp.Items.FindByText(LoginAuxili.NOME_USU_LOGADO));
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

            HttpContext.Current.Session.Add("ID_AGEND_HORAR", ID_EMCAM_MEDIC);
            //this.CarregaDadosTriagem(ID_EMCAM_MEDIC);
        }
        public string ALTURA;

        private void CarregaDadosTriagem(int ID_EMCAM_MEDIC)
        {
            string id_encam_str = ID_EMCAM_MEDIC.ToString();
           
            var ret = GestorEntities.CurrentContext.TBS194_PRE_ATEND.FirstOrDefault(p => p.CO_PRE_ATEND == id_encam_str);
            txtAltura.Text = ret.NU_ALTU.ToString();
            ALTURA = ret.NU_ALTU.ToString();
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
        private void preencheddlistunidades()
        {
            var tb134 = (from lTb134 in TB134_USR_EMP.RetornaTodosRegistros()
                         where lTb134.ADMUSUARIO.ideAdmUsuario == LoginAuxili.IDEADMUSUARIO && lTb134.FLA_STATUS == "A"
                         && lTb134.TB25_EMPRESA.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                         select new
                         {
                             lTb134.TB25_EMPRESA.CO_EMP,
                             lTb134.TB25_EMPRESA.sigla,
                             lTb134.TB25_EMPRESA.NO_FANTAS_EMP,
                             NO_TIPOEMP = (lTb134.TB25_EMPRESA.CO_FLAG_ENSIN_SUPER == "S" ? "[Superior]" : "") +
                             (lTb134.TB25_EMPRESA.CO_FLAG_ENSIN_MEDIO == "S" ? " [Médio]" : "") +
                             (lTb134.TB25_EMPRESA.CO_FLAG_ENSIN_FUNDA == "S" ? " [Fundamental]" : "") +
                             (lTb134.TB25_EMPRESA.CO_FLAG_ENSIN_INFAN == "S" ? " [Infantil]" : "") +
                             (lTb134.TB25_EMPRESA.CO_FLAG_ENSIN_OUTRO == "S" ? " [Outros]" : ""),
                             PERFIL = lTb134.AdmPerfilAcesso.nomeTipoPerfilAcesso
                         }).OrderBy(u => u.NO_FANTAS_EMP);

            ddlescolheunidade.DataSource = tb134;
            ddlescolheunidade.DataTextField = "NO_FANTAS_EMP";
            ddlescolheunidade.DataValueField = "CO_EMP";
            ddlescolheunidade.DataBind();
            ddlescolheunidade.Items.Insert(0, new ListItem("Selecione outra unidade para visualizar", ""));
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
            //CarregarGridDirecionamentos("");
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //int coEspec = ddlPesqEspec.SelectedValue != "" ? int.Parse(ddlPesqEspec.SelectedValue) : 0;
            //int coDepto = ddlPesqLocal.SelectedValue != "" ? int.Parse(ddlPesqLocal.SelectedValue) : 0;

            //CarregarGridDirecionamentos();

            ////Verifica se a grid já possuia um registro selecionado antes, e chama um método responsável por selecioná-lo de novo no PRÉ-ATENDIMENTO
            //if ((string)HttpContext.Current.Session["FL_SELECT_GRID_PRATB"] == "S")
            //{
            //    selecionaGridPreAtend();
            //}

            //UpdatePanel1.Update();
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
                CheckBox chk;
                e.Row.Attributes.Add("onClick", "javascript:__doPostBack('" + grdEncamMedic.UniqueID + "','Select$" + qtdLinhasGrid + "')");
                qtdLinhasGrid = qtdLinhasGrid + 1;

                //Se for registro antigo(um dos 10 antigos que são também apresentados), destaca ele em cor salmão
                if (((HiddenField)e.Row.Cells[0].FindControl("hidAntigos")).Value == "1")
                    e.Row.BackColor = System.Drawing.Color.AntiqueWhite;

                if(ddlescolheunidade.SelectedValue != Convert.ToString(LoginAuxili.CO_EMP))
                    if (Session["temp"] != null)
                    {
                        chk = (CheckBox)e.Row.Cells[0].FindControl("chkselectEn");
                        chk.Enabled = !chk.Enabled;
                    }
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

        protected void ddlescolheunidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["temp"] = "OK";
            if (ddlescolheunidade.SelectedValue != "")
            {
                DateTime dtIni = (!string.IsNullOrEmpty(IniPeri.Text.Substring(0, 10)) ? DateTime.Parse(IniPeri.Text.Substring(0, 10)) : DateTime.Now);
                DateTime dtFim = (!string.IsNullOrEmpty(FimPeri.Text.Substring(0, 10)) ? DateTime.Parse(FimPeri.Text.Substring(0, 10)) : DateTime.Now);
                int temp = Convert.ToInt16(ddlescolheunidade.SelectedValue);

                var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                           where tbs174.DT_ENCAM != null && tbs174.CO_CLASS_RISCO == null
                           && tbs174.CO_EMP == temp
                           && tbs174.DT_AGEND_HORAR >= dtIni
                           && tbs174.DT_AGEND_HORAR <= dtFim
                           select new EncaminhamentosMedicos
                           {
                               TP_AGEND_HORA = tbs174.TP_AGEND_HORAR,
                               CO_TIPO_RISCO = tbs174.CO_CLASS_RISCO != null ? tbs174.CO_CLASS_RISCO : 0,
                               __ALTURA = "1.50",
                               __PESO = "90",
                               __FICHA = "Paciênte ainda não fez Triagem ",

                               NO_PAC_RECEB = tb07.NO_ALU,
                               ID_AGEND_HORA = tbs174.ID_AGEND_HORAR,
                               NO_ESPEC = tb03.DE_FUNC_COL,
                               CO_ESPEC = tb03.CO_FUN,
                               dt_nascimento = tb07.DT_NASC_ALU,
                               CO_SEXO = tb07.CO_SEXO_ALU,
                               dataEncamMed = tbs174.DT_ENCAM,
                               CO_COL = tbs174.CO_COL,
                               NM_OPER = tb07.NU_REGI_SUS_ALU != null ? tb07.NU_REGI_SUS_ALU : "S/REG",
                               NO_COL = tb03.NO_COL,
                           }).OrderBy(w => w.ID_AGEND_HORA).ToList();
                #region 10 últimos acolhimentos

                var resAntigos = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                  join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                                  join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                                  where tbs174.DT_ENCAM != null && tbs174.CO_CLASS_RISCO != null
                                  && tbs174.CO_EMP == temp
                                  && tbs174.DT_AGEND_HORAR >= dtIni
                                  && tbs174.DT_AGEND_HORAR <= dtFim

                                  select new EncaminhamentosMedicos
                                  {
                                      TP_AGEND_HORA = tbs174.TP_AGEND_HORAR,
                                      CO_TIPO_RISCO = tbs174.CO_CLASS_RISCO != null ? tbs174.CO_CLASS_RISCO : 0,
                                      __ALTURA = "1.50",
                                      __PESO = "90",
                                      NO_PAC_RECEB = tb07.NO_ALU,
                                      ID_AGEND_HORA = tbs174.ID_AGEND_HORAR,
                                      NO_ESPEC = tb03.DE_FUNC_COL,
                                      CO_ESPEC = tb03.CO_FUN,
                                      dt_nascimento = tb07.DT_NASC_ALU,
                                      CO_SEXO = tb07.CO_SEXO_ALU,
                                      dataEncamMed = tbs174.DT_ENCAM,
                                      CO_COL = tbs174.CO_COL,
                                      NM_OPER = tb07.NU_REGI_SUS_ALU != null ? tb07.NU_REGI_SUS_ALU : "S/REG",
                                      NO_COL = tb03.NO_COL,
                                  }).OrderBy(w => w.ID_AGEND_HORA).ToList();
                // ANDRÉ - COLOCAR NOVOS CAMPOS NESTE HINT
                foreach (var i in resAntigos) // passa por cada item da segunda lista e insere na primeira
                {
                    string id_encam_str = i.ID_AGEND_HORA.ToString();
                    var ret = GestorEntities.CurrentContext.TBS194_PRE_ATEND.FirstOrDefault(p => p.CO_PRE_ATEND == id_encam_str);
                    if (ret != null)
                    {
                        i.__FICHA = i.NO_PAC_RECEB + "\n";
                        i.__FICHA = i.__FICHA + " Altura: " + ret.NU_ALTU.ToString() + " -- Peso : " + ret.NU_PESO.ToString() + "-- PA:" + ret.NU_PRES_ARTE + " -- Temp.: " + ret.NU_TEMP.ToString() + " -- Glicemia : " + ret.NU_GLICE.ToString() + "\n";
                        i.__FICHA = i.__FICHA + " Dores: " + ret.FL_SINTO_DORES + " -- Enjoo" + ret.FL_SINTO_ENJOO + " -- Vômito : " + ret.FL_SINTO_VOMIT + " -- Febre:" + ret.FL_SINTO_FEBRE + " -- Diabetes:" + ret.FL_DIABE + "\n ";
                        i.__FICHA = i.__FICHA + " Fumante : " + ret.FL_FUMAN + " -- Alcool:" + ret.FL_ALCOO + " -- Cirurgia:" + ret.DE_CIRUR + "Alergia:" + ret.DE_ALERG + "\n ";

                        i.__FICHA = i.__FICHA + " Medicação Uso Contínuo : " + ret.DE_MEDIC_USO_CONTI + "\n ";

                        i.__FICHA = i.__FICHA + " Medicação : " + ret.DE_MEDIC + "\n ";
                        i.__FICHA = i.__FICHA + " Sintomas : " + ret.DE_SINTO + "\n ";
                    }

                    res.Add(i);
                }
                grdEncamMedic.DataSource = res;
                grdEncamMedic.DataBind();
                //Session["temp"] = grdEncamMedic.DataSource;
                #endregion
            }
        }
        protected void grdListarSIGTAP_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //grdListarSIGTAP.PageIndex = e.NewPageIndex;
            //grdListarSIGTAP.DataSource = Session["temp"];
            //grdListarSIGTAP.DataBind();
            //AbreModalPadrao("AbreModalInfosSigtap();");
        }

        protected void grdEncamMedic_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            foreach (GridViewRow linha2 in grdEncamMedic.Rows)
            {
                if (((CheckBox)linha2.Cells[0].FindControl("chkselectEn")).Checked)
                {
                    string temp = ((HiddenField)linha2.Cells[0].FindControl("hidCoAgen")).Value;
                    string temp2 = ((HiddenField)linha2.Cells[0].FindControl("hidCoAgen")).Value;
                    Session["CO_ALU"] = ((HiddenField)linha2.Cells[0].FindControl("hidCoAlu")).Value;
                    Session["CO_ALUNO_ID_AGEND_HORAR"] = ((Label)linha2.Cells[0].FindControl("lblgrid")).Text.Substring(((Label)linha2.Cells[0].FindControl("lblgrid")).Text.IndexOf(":") + 1, 2);
                    Session["IDADE"] = linha2.Cells[6].Text;
                    Session["SEXO"] = linha2.Cells[5].Text;
                    try { tbidadegestante.Text = linha2.Cells[6].Text; } catch { }
                }
            }
        }
        //TextBox x = ((TextBox)e.Row.Cells[i].FindControl("ctl" + tx.PadLeft(2,'0')))x.Enable = False;

        protected void ddlglicemia_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    C2BR.GestorEducacao.UI.Object.TBS478_ATEND_GESTANTE_BO BO = new C2BR.GestorEducacao.UI.Object.TBS478_ATEND_GESTANTE_BO();
            //    BO = (C2BR.GestorEducacao.UI.Object.TBS478_ATEND_GESTANTE_BO)Session["TBS478_ATEND_GESTANTE_BO"];
            //    BO.LEITURAGLICEMICA = ddlglicemia.SelectedValue;
            //    Session["TBS478_ATEND_GESTANTE_BO"] = BO;
            //}
            //catch { }
        }

        protected void txtAltura_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
                try { tbaltura.Text = txtAltura.Text; } catch { }
                //C2BR.GestorEducacao.UI.Object.TBS478_ATEND_GESTANTE_BO BO = new C2BR.GestorEducacao.UI.Object.TBS478_ATEND_GESTANTE_BO();
                //BO = (C2BR.GestorEducacao.UI.Object.TBS478_ATEND_GESTANTE_BO)Session["TBS478_ATEND_GESTANTE_BO"];
                //BO.AUTURA_RA = txtAltura.Text;
                //Session["TBS478_ATEND_GESTANTE_BO"] = BO;
            //}
            //catch { }
        }

        protected void txtPeso_TextChanged(object sender, EventArgs e)
        {
            try { tbpeso.Text = txtPeso.Text; } catch { }
            //try
            //{
            //    C2BR.GestorEducacao.UI.Object.TBS478_ATEND_GESTANTE_BO BO = new C2BR.GestorEducacao.UI.Object.TBS478_ATEND_GESTANTE_BO();
            //    BO = (C2BR.GestorEducacao.UI.Object.TBS478_ATEND_GESTANTE_BO)Session["TBS478_ATEND_GESTANTE_BO"];
            //    BO.PESO = txtPeso.Text;
            //    Session["TBS478_ATEND_GESTANTE_BO"] = BO;
            //}
            //catch { }
        }

        protected void txtPressArt_TextChanged(object sender, EventArgs e)
        {
            try { tbpa.Text = txtPressArt.Text; } catch { }
            //try
            //{
            //    C2BR.GestorEducacao.UI.Object.TBS478_ATEND_GESTANTE_BO BO = new C2BR.GestorEducacao.UI.Object.TBS478_ATEND_GESTANTE_BO();
            //    BO = (C2BR.GestorEducacao.UI.Object.TBS478_ATEND_GESTANTE_BO)Session["TBS478_ATEND_GESTANTE_BO"];
            //    BO.PA = txtPressArt.Text;
            //    Session["TBS478_ATEND_GESTANTE_BO"] = BO;
            //}
            //catch { }
        }

        protected void tbbatimento_TextChanged(object sender, EventArgs e)
        {
            try { tbbcbpm.Text = tbbatimento.Text; } catch { }
            //try
            //{
            //    C2BR.GestorEducacao.UI.Object.TBS478_ATEND_GESTANTE_BO BO = new C2BR.GestorEducacao.UI.Object.TBS478_ATEND_GESTANTE_BO();
            //    BO = (C2BR.GestorEducacao.UI.Object.TBS478_ATEND_GESTANTE_BO)Session["TBS478_ATEND_GESTANTE_BO"];
            //    BO.BCF = tbbatimento.Text;
            //    Session["TBS478_ATEND_GESTANTE_BO"] = BO;
            //}
            //catch { }
        }

        protected void tbsaturacao_TextChanged(object sender, EventArgs e)
        {
            try { tbsaturacao2.Text = tbsaturacao.Text; } catch { }
            //try
            //{
            //    C2BR.GestorEducacao.UI.Object.TBS478_ATEND_GESTANTE_BO BO = new C2BR.GestorEducacao.UI.Object.TBS478_ATEND_GESTANTE_BO();
            //    BO = (C2BR.GestorEducacao.UI.Object.TBS478_ATEND_GESTANTE_BO)Session["TBS478_ATEND_GESTANTE_BO"];
            //    BO.SATURACAO = tbbatimento.Text;
            //    Session["TBS478_ATEND_GESTANTE_BO"] = BO;
            //}
            //catch { }
        }

        protected void txtGlicem_TextChanged(object sender, EventArgs e)
        {
            try { tbglicemia.Text = txtGlicem.Text; } catch { }
            //try
            //{
            //    C2BR.GestorEducacao.UI.Object.TBS478_ATEND_GESTANTE_BO BO = new C2BR.GestorEducacao.UI.Object.TBS478_ATEND_GESTANTE_BO();
            //    BO = (C2BR.GestorEducacao.UI.Object.TBS478_ATEND_GESTANTE_BO)Session["TBS478_ATEND_GESTANTE_BO"];
            //    BO.GLICEMIA = txtGlicem.Text;
            //    Session["TBS478_ATEND_GESTANTE_BO"] = BO;
            //}
            //catch { }
        }
        protected void btn_GESTANTE_Click(object sender, EventArgs e)
        {
            if(Session["SEXO"] is null)
                AuxiliPagina.EnvioMensagemErro(this.Page, "É necessário selecionar um paciente, e este ser do sexo Feminino.");
            else if ((Session["CO_ALU"] == null) || (Session["SEXO"].ToString() == "M"))
                AuxiliPagina.EnvioMensagemErro(this.Page, "É necessário selecionar um paciente, e este ser do sexo Feminino.");
            else
                AbreModalPadrao("AbreModalInfosGestante();");
        }

        protected void btn_SIGTAP_Click(object sender, EventArgs e)
        {
            if (ddlprofatendimento.SelectedValue == "")
                AuxiliPagina.EnvioMensagemErro(this.Page, "É necessário selecionar um(a) profissional do Atendimento.");
            else if (Session["CO_ALU"] == null)
                AuxiliPagina.EnvioMensagemErro(this.Page, "É necessário selecionar um paciente.");
            else if (ddlProfResp.SelectedValue == "")
                AuxiliPagina.EnvioMensagemErro(this.Page, "É necessário selecionar um Profissional responsável pela triagem.");

            else
            {
                //for (int i = 0; i < 10; i++)
                  //  CriaNovaLinhaGridProced(Convert.ToInt32(Session["CO_ALUNO"]));
                AbreModalPadrao("AbreModalInfosSigtap();");
            }
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
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbdum.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "É necessário inserir a data da últrima mestruação!");
                AbreModalPadrao("AbreModalInfosGestante();");
            }
            if (string.IsNullOrWhiteSpace(tbdpp.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "É necessário inserir a Data Provável do Parto!");
                AbreModalPadrao("AbreModalInfosGestante();");
            }

            
            //string TEMP = 
            Object.TBS478_ATEND_GESTANTE_BO BO = new Object.TBS478_ATEND_GESTANTE_BO();
            //TBS478_ATEND_GESTANTE_BUSINESS insere = new TBS478_ATEND_GESTANTE_BUSINESS();

            if (Session["CO_ALU"] == null)
                AuxiliPagina.EnvioMensagemErro(this.Page, "É necessário selecionar uma paciente!");
            else
            {
                try { BO.AUTURA_RA = tbaltura.Text; } catch { }
                try { BO.AUTURA_RPN = tbautura.Text; } catch { }
                try { BO.BCF = tbbcf.Text; } catch { }
                try { BO.CO_ALUNO = Convert.ToInt32(Session["CO_ALU"]); } catch { }
                try { BO.COD_GESTANTE = 0; } catch { }
                try { BO.DPP = Convert.ToDateTime(tbdpp.Text); } catch { }
                try { BO.DADOS_REGISTRO = tbdataregistro.Text; } catch { }
                try { BO.DUM = Convert.ToDateTime(tbdum.Text); } catch { }
                try { BO.EDMA = ddledma.SelectedValue; } catch { }
                try { BO.ID_ATEND_GESTANTE = 0; } catch { }
                try { BO.IDADE_GESTANTE = tbidadegestante.Text; } catch { }
                try { BO.IMC = tbimc.Text; } catch { }
                try { BO.MF = tbmf.Text; } catch { }
                try { BO.OBS_ANTRO = tbobsantropometria.Text; } catch { }
                try { BO.OBS_COMPLEMENTO = tbobservacaocomplemento.Text; } catch { }
                try { BO.OBS_DUM = tbobsdum.Text; } catch { }
                try { BO.OBS_MF = tbobsmf.Text; } catch { }
                try { BO.PC = tbpc.Text; } catch { }
                try { BO.PESO = tbpeso.Text; } catch { }
                try { BO.PP = tbpp.Text; } catch { }
                try { BO.TIPO_REG = ddltiporegistro.SelectedValue; } catch { }
                try { BO.PA = tbpa.Text; } catch { }
                try { BO.SATURACAO = tbsaturacao.Text; } catch { }
                try { BO.GLICEMIA = tbglicemia.Text; } catch { }
                try { BO.LEITURAGLICEMICA = ddlleitura.SelectedValue; } catch { }
                Session["TBS478_ATEND_GESTANTE_BO"] = BO;
            }
            //insere.InsereTBS478(BO);
        }

        protected void imgCpfResp_Click(object sender, ImageClickEventArgs e)
        {
            C2BR.GestorEducacao.BusinessEntities.Auxiliar.SQLDirectAcess direc = new C2BR.GestorEducacao.BusinessEntities.Auxiliar.SQLDirectAcess();
            System.Data.DataTable dt = new System.Data.DataTable();
            //dt = direc.retornacolunas("select ID_PROC_MEDI_PROCE, CO_PROC_MEDI, NM_PROC_MEDI from TBS356_PROC_MEDIC_PROCE where LEN(co_proc_medi) = 10 and NM_PROC_MEDI like '%" + tbpesquisasigtab.Text + "%'");

            //grdListarSIGTAP.DataSource = dt;
            //grdListarSIGTAP.DataBind();
            //Session["temp"] = grdListarSIGTAP.DataSource;
            //AbreModalPadrao("AbreModalInfosSigtap();");
        }




        /*TELA DE PROCEDIMENTOS*/
        #region Código tela Procedimentos

        #region Montando a tela
        protected void lnkAddProcPla_OnClick(object sender, EventArgs e)
        {
            for(int i = 0; i < 10; i++)
                CriaNovaLinhaGridProced(Convert.ToInt32(Session["CO_ALU"]));

            AbreModalPadrao("AbreModalInfosSigtap();");
        }
        protected void CriaNovaLinhaGridProced(int paci = 0)
        {
            try
            {
                Session["GridSolic_PROC_PLA"] = null;

                DataTable dtV = CriarColunasELinhaGridProced();

                if (paci != 0)
                {
                    var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                               where (tb07.CO_ALU == paci)
                               select new CriaNovaLinhaGridProcedClass
                               {
                                   CO_ALU = tb07.CO_ALU,
                                   CONTRAT = tb07.TB250_OPERA.ID_OPER,
                                   PLANO = tb07.TB251_PLANO_OPERA.ID_PLAN,
                                   CART = tb07.NU_PLANO_SAUDE
                               }).FirstOrDefault();

                    DataRow linha = dtV.NewRow();
                    linha["CONTRAT"] = res.CONTRAT != null || res.CONTRAT != 0 ? res.CONTRAT.ToString() : "";
                    linha["PLANO"] = res.PLANO != null || res.PLANO != 0 ? res.PLANO.ToString() : "";
                    linha["CART"] = !String.IsNullOrEmpty(res.CART) ? res.CART : "";
                    linha["SOLIC"] = LoginAuxili.CO_COL.ToString();
                    linha["PROCED"] = "";
                    linha["CORT"] = false;
                    linha["UNIT"] = "";
                    linha["QTP"] = "";
                    linha["TOTAL"] = "";
                    linha["ID_ITENS_PLANE_AVALI"] = "";
                    dtV.Rows.Add(linha);
                }
                else
                {
                    DataRow linha = dtV.NewRow();
                    linha["CONTRAT"] = "";
                    linha["SOLIC"] = LoginAuxili.CO_COL.ToString();
                    linha["PLANO"] = "";
                    linha["CART"] = "";
                    linha["PROCED"] = "";
                    linha["CORT"] = false;
                    linha["UNIT"] = "";
                    linha["QTP"] = "";
                    linha["TOTAL"] = "";
                    linha["ID_ITENS_PLANE_AVALI"] = "";
                    dtV.Rows.Add(linha);
                }

                Session["GridSolic_PROC_PLA"] = dtV;
                carregaGridNovaComContextoProced();

            }
            catch (Exception) { }
        }
        protected void carregaGridNovaComContextoProced()
        {
            DataTable dtV = new DataTable();

            dtV = (DataTable)Session["GridSolic_PROC_PLA"];

            grdProcedimentos.DataSource = dtV;
            grdProcedimentos.DataBind();

            int aux = 0;
            foreach (GridViewRow li in grdProcedimentos.Rows)
            {
                DropDownList ddlContrat;
                DropDownList ddlPlan;
                DropDownList ddlProced;
                DropDownList ddlSolic;
                TextBox txtCart;
                TextBox valorUnit;
                TextBox valorTotal;
                TextBox txtQtp;
                CheckBox chkCort;
                TextBox idItemProced;
                ddlSolic = ((DropDownList)li.FindControl("ddlSolicProc"));
                ddlContrat = ((DropDownList)li.FindControl("ddlContratProc"));
                ddlPlan = ((DropDownList)li.FindControl("ddlPlanoProc"));
                ddlProced = ((DropDownList)li.FindControl("ddlProcMod"));
                txtCart = ((TextBox)li.FindControl("txtNrCartProc"));
                valorUnit = ((TextBox)li.FindControl("txtValorUnit"));
                valorTotal = ((TextBox)li.FindControl("txtValorTotalMod"));
                txtQtp = ((TextBox)li.FindControl("txtQTPMod"));
                chkCort = ((CheckBox)li.FindControl("chkCortProc"));
                idItemProced = ((TextBox)li.FindControl("txtCoItemProced"));

                string solic, contrat, plano, cart, proced, unit, qtp, total, cort, idItem;

                //Coleta os valores do dtv da modal popup
                solic = dtV.Rows[aux]["SOLIC"].ToString();
                contrat = dtV.Rows[aux]["CONTRAT"].ToString();
                plano = dtV.Rows[aux]["PLANO"].ToString();
                cart = dtV.Rows[aux]["CART"].ToString();
                proced = dtV.Rows[aux]["PROCED"].ToString();
                unit = dtV.Rows[aux]["UNIT"].ToString();
                qtp = dtV.Rows[aux]["QTP"].ToString();
                total = dtV.Rows[aux]["TOTAL"].ToString();
                cort = dtV.Rows[aux]["CORT"].ToString();
                idItem = dtV.Rows[aux]["ID_ITENS_PLANE_AVALI"].ToString();

                var opr = 0;

                //if (!String.IsNullOrEmpty(ddlPlanProcPlan.SelectedValue) && int.Parse(ddlPlanProcPlan.SelectedValue) != 0)
                //{
                //    var plan = TB251_PLANO_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlPlanProcPlan.SelectedValue));
                //    plan.TB250_OPERAReference.Load();
                //    opr = plan.TB250_OPERA != null ? plan.TB250_OPERA.ID_OPER : 0;
                //}

                //CarregarProcedimentos(ddlCodigoi, opr, "EX");

                var tbs174_tipoConsu = "G"; // TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(hidCoAgendProced.Value)).TP_CONSU;

                //lblTituloProcedMod.Text = (tbs174_tipoConsu.Equals("P") ? "PROCEDIMENTO" : tbs174_tipoConsu.Equals("N") ? "CONSULTA" : tbs174_tipoConsu.Equals("E") ? "EXAME" : tbs174_tipoConsu.Equals("V") ? "VACINA" : tbs174_tipoConsu.Equals("C") ? "CIRURGIA" : "OUTROS");
                CarregaOperadoras(ddlContrat, contrat);
                CarregarPlanosSaude(ddlPlan, ddlContrat);
                CarregaProcedimentos(ddlProced, ddlContrat, proced, tbs174_tipoConsu);
                AuxiliCarregamentos.CarregaProfissionaisSaude(ddlSolic, LoginAuxili.CO_EMP, false, "0");
                //SelecionaOperadoraPlanoPaciente();
                //ddlContrat.SelectedValue = contrat;
                ddlSolic.SelectedValue = solic;
                ddlPlan.SelectedValue = plano;
                CalcularPreencherValoresTabelaECalculado(ddlProced, ddlContrat, ddlPlan, valorUnit);
                txtCart.Text = cart;
                //valorTotal.Text = total;
                //txtQtp.Text = qtp;
                //chkCort.Checked = Convert.ToBoolean(cort);
                idItemProced.Text = idItem;
                aux++;
                //if (chkCort.Checked)
                //{
                //    valorUnit.Enabled = false;
                //    valorTotal.Enabled = false;
                //}

                //if (String.IsNullOrEmpty(cart))
                //{
                //    txtCart.Enabled = true;
                //}
                //else
                //{
                //    txtCart.Enabled = false;
                //}
            }
        }


        private DataTable CriarColunasELinhaGridProced()
        {
            DataTable dtV = new DataTable();
            DataColumn dcATM;

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "ID_ITENS_PLANE_AVALI";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "CONTRAT";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "SOLIC";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "PLANO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "CART";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "PROCED";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.Boolean");
            dcATM.ColumnName = "CORT";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "UNIT";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "QTP";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "TOTAL";
            dtV.Columns.Add(dcATM);

            DataRow linha;

            foreach (GridViewRow li in grdProcedimentos.Rows)
            {
                linha = dtV.NewRow();
                linha["CONTRAT"] = (((DropDownList)li.FindControl("ddlContratProc")).SelectedValue);
                linha["PLANO"] = (((DropDownList)li.FindControl("ddlPlanoProc")).SelectedValue);
                linha["SOLIC"] = (((DropDownList)li.FindControl("ddlSolicProc")).SelectedValue);
                linha["CART"] = (((TextBox)li.FindControl("txtNrCartProc")).Text);
                linha["PROCED"] = (((DropDownList)li.FindControl("ddlProcMod")).SelectedValue);
                //linha["CORT"] = (((CheckBox)li.FindControl("chkCortProc")).Checked);
                //linha["UNIT"] = (((TextBox)li.FindControl("txtValorUnit")).Text);
                //linha["QTP"] = (((TextBox)li.FindControl("txtQTPMod")).Text);
                //linha["TOTAL"] = (((TextBox)li.FindControl("txtValorTotalMod")).Text);
                linha["ID_ITENS_PLANE_AVALI"] = (((TextBox)li.FindControl("txtCoItemProced")).Text);
                dtV.Rows.Add(linha);
            }

            return dtV;
        }
        public class CriaNovaLinhaGridProcedClass
        {
            public int CO_ALU { get; set; }
            public int? CONTRAT { get; set; }
            public int? PLANO { get; set; }
            public string CART { get; set; }
        }

        protected void ExcluiItemGridProced(int Index)
        {
            DataTable dtV = CriarColunasELinhaGridProced();
            //try
            //{
            //    if (idItem != 0)
            //    {
            //        var tbs386 = TBS386_ITENS_PLANE_AVALI.RetornaPelaChavePrimaria(idItem);
            //        var tbs389 = TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros().Where(x => x.TBS386_ITENS_PLANE_AVALI.ID_ITENS_PLANE_AVALI == idItem).FirstOrDefault();

            //        TBS389_ASSOC_ITENS_PLANE_AGEND.Delete(tbs389, true);
            //        TBS386_ITENS_PLANE_AVALI.Delete(tbs386, true);
            //    }

            //}
            //catch (Exception) { }
            dtV.Rows.RemoveAt(Index); // Exclui o item de index correspondente
            Session["GridSolic_PROC_PLA"] = dtV;
            carregaGridNovaComContextoProced();
        }
        protected void LimparGridProced()
        {
            grdProcedimentos.DataSource = null;
            grdProcedimentos.DataBind();
        }
        #endregion
        #region CLIQUES
        protected void imgExcPla_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            int idItem = 0;
            int aux = 0;
            if (grdProcedimentos.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdProcedimentos.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgExcPla");
                    TextBox hidIdItem = ((TextBox)linha.FindControl("txtCoItemProced"));
                    idItem = (String.IsNullOrEmpty(hidIdItem.Text) ? 0 : int.Parse(hidIdItem.Text));
                    if (img.ClientID == atual.ClientID)
                        aux = linha.RowIndex;
                }
            }
            ExcluiItemGridProced(aux);
            LimparGridProced();
            carregaGridNovaComContextoProced();
            AbreModalPadrao("AbreModalInfosSigtap();");
        }

        protected void Qtp_OnTextChanged(object sender, EventArgs e)
        {
            //TextBox atual = (TextBox)sender;
            //TextBox txt;

            //foreach (GridViewRow li in grdProcedimentos.Rows)
            //{
            //    txt = (TextBox)li.FindControl("txtQTPMod");
            //    //Só marca os outros, se o registro estiver selecionado
            //    if (txt.ClientID == atual.ClientID)
            //    {
            //        TextBox txtValorUnit = (TextBox)li.FindControl("txtValorUnit");
            //        TextBox txtValorTotal = (TextBox)li.FindControl("txtValorTotalMod");

            //        decimal result = ((String.IsNullOrEmpty(txtValorUnit.Text) ? 0 : Decimal.Parse(txtValorUnit.Text)) * (String.IsNullOrEmpty(txt.Text) ? 0 : Decimal.Parse(txt.Text)));

            //        txtValorTotal.Text = result.ToString();
            //    }
            //}
            AbreModalPadrao("AbreModalInfosSigtap();");
        }
        protected void chkCortProc_OnChanged(object sender, EventArgs e)
        {
            //CheckBox atual = (CheckBox)sender;
            //CheckBox ck;
            //if (grdProcedimentos.Rows.Count != 0)
            //{
            //    foreach (GridViewRow li in grdProcedimentos.Rows)
            //    {
            //        TextBox valorUnit;
            //        TextBox valorTotal;
            //        TextBox txtQtp;
            //        valorUnit = ((TextBox)li.FindControl("txtValorUnit"));
            //        valorTotal = ((TextBox)li.FindControl("txtValorTotalMod"));
            //        txtQtp = ((TextBox)li.FindControl("txtQTPMod"));
            //        ck = ((CheckBox)li.FindControl("chkCortProc"));
            //        if (ck.ClientID == atual.ClientID)
            //        {
            //            if (ck.Checked)
            //            {
            //                valorUnit.Enabled = false;
            //                valorTotal.Enabled = false;
            //            }
            //        }
            //    }
            //}
            AbreModalPadrao("AbreModalInfosSigtap();");
        }


        protected void ddlProcedAgend_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList atual = (DropDownList)sender;
            DropDownList ddlOper, ddlPlan, ddlProc;

            int qntProced = 0;
            bool existeProcedimento = false; //Define se existe um procedimento igual já selecionado

            if (grdProcedimentos.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdProcedimentos.Rows)
                {
                    ddlProc = (DropDownList)linha.FindControl("ddlProcMod");

                    if (ddlProc.SelectedValue.Equals(atual.SelectedValue))
                    {
                        qntProced++;
                        if (qntProced > 1)
                        {
                            existeProcedimento = true;
                            ddlProc.Focus();
                            break;
                        }
                    }
                }
            }

            if (!existeProcedimento)
            {
                if (grdProcedimentos.Rows.Count != 0)
                {
                    foreach (GridViewRow linha in grdProcedimentos.Rows)
                    {
                        ddlOper = (DropDownList)linha.FindControl("ddlContratProc");
                        ddlPlan = (DropDownList)linha.FindControl("ddlPlanoProc");
                        ddlProc = (DropDownList)linha.FindControl("ddlProcMod");
                        TextBox txtValor = (TextBox)linha.FindControl("txtValorUnit");

                        //Carrega os planos de saúde da operadora quando encontra o objeto que invocou o postback
                        if (ddlProc.ClientID == atual.ClientID)
                            CalcularPreencherValoresTabelaECalculado(ddlProc, ddlOper, ddlPlan, txtValor);
                    }
                }
            }
            else
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "MSG: Este procedimento já foi listado.");
                atual.SelectedValue = null;
            }
            AbreModalPadrao("AbreModalInfosSigtap();");
        }

        protected void chkRetornaProced_OnCheckedChanged(object sender, EventArgs e)
        {
            //CheckBox atual = (CheckBox)sender;
            //if (atual.Checked)
            //{
            //    int coPaci;
            //    int coAgend;
            //    LimparGridProced();
            //    if (String.IsNullOrEmpty(hidCoPaciProced.Value) && String.IsNullOrEmpty(hidCoAgendProced.Value))
            //    {
            //        carregaGridProced();
            //    }
            //    else if (!String.IsNullOrEmpty(hidCoPaciProced.Value) && !String.IsNullOrEmpty(hidCoAgendProced.Value))
            //    {
            //        coPaci = int.Parse(hidCoPaciProced.Value);
            //        coAgend = int.Parse(hidCoAgendProced.Value);

            //        carregaGridProced(coAgend, coPaci, true);
            //    }
            //    else if (!String.IsNullOrEmpty(hidCoPaciProced.Value) && String.IsNullOrEmpty(hidCoAgendProced.Value))
            //    {
            //        coPaci = int.Parse(hidCoPaciProced.Value);

            //        carregaGridProced(0, coPaci, true);
            //    }
            //}
            //else
            //{
            //    int coPaci;
            //    int coAgend;
            //    LimparGridProced();
            //    if (String.IsNullOrEmpty(hidCoPaciProced.Value) && String.IsNullOrEmpty(hidCoAgendProced.Value))
            //    {
            //        carregaGridProced();
            //    }
            //    else if (!String.IsNullOrEmpty(hidCoPaciProced.Value) && !String.IsNullOrEmpty(hidCoAgendProced.Value))
            //    {
            //        coPaci = int.Parse(hidCoPaciProced.Value);
            //        coAgend = int.Parse(hidCoAgendProced.Value);

            //        carregaGridProced(coAgend, coPaci, true);
            //    }
            //    else if (!String.IsNullOrEmpty(hidCoPaciProced.Value) && String.IsNullOrEmpty(hidCoAgendProced.Value))
            //    {
            //        coPaci = int.Parse(hidCoPaciProced.Value);

            //        carregaGridProced(0, coPaci, true);
            //    }
            //}
            AbreModalPadrao("AbreModalInfosSigtap();");
        }
        protected void ddlOpers_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList atual = (DropDownList)sender;
            DropDownList ddl;

            foreach (GridViewRow li in grdProcedimentos.Rows)
            {
                ddl = (DropDownList)li.FindControl("ddlContratProc");
                //Só marca os outros, se o registro estiver selecionado
                if (ddl.ClientID == atual.ClientID)
                {
                    DropDownList ddlPlan = (DropDownList)li.FindControl("ddlPlanoProc");
                    DropDownList ddlProc = (DropDownList)li.FindControl("ddlProcMod");
                    var tbs174_tipoConsu = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(hidCoAgendProced.Value)).TP_CONSU;

                    CarregarPlanosSaude(ddlPlan, ddl);
                    CarregaProcedimentos(ddlProc, ddl, null, tbs174_tipoConsu);
                }
            }
            AbreModalPadrao("AbreModalInfosSigtap();");
        }
        #endregion
        #region Procedimentos

        private void CarregaOperadoras(DropDownList ddl, string selec)
        {
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddl, false, false, true);
            ddl.SelectedValue = selec;
        }
        private void CarregarPlanosSaude(DropDownList ddlPlan, DropDownList ddlOper)
        {
            AuxiliCarregamentos.CarregaPlanosSaude(ddlPlan, ddlOper, false, false, true);
        }
        private void CarregaProcedimentos(DropDownList ddl, DropDownList ddlOper, string selec = null, string tipo = null)
        {
            //André
            int idOper = (!string.IsNullOrEmpty(ddlOper.SelectedValue) ? int.Parse(ddlOper.SelectedValue) : 0);
            string tipoV = (String.IsNullOrEmpty(tipo) ? null : tipo.Equals("P") ? "PR" : tipo.Equals("N") ? "CO" : tipo.Equals("R") ? "CO" : tipo.Equals("E") ? "EX" : tipo.Equals("V") ? "VA" : tipo.Equals("C") ? "CI" : tipo.Equals("O") ? "OU" : null);
            var res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                       where (tbs356.CO_SITU_PROC_MEDI == "A")
                       //&& (idOper != 0 ? tbs356.TB250_OPERA.ID_OPER == idOper : tbs356.TB250_OPERA == null)
                       //&& (String.IsNullOrEmpty(tipoV) ? 0 == 0 : tbs356.CO_TIPO_PROC_MEDI == tipoV)
                       select new { tbs356.ID_PROC_MEDI_PROCE, CO_PROC_MEDI = tbs356.CO_PROC_MEDI + " - " + tbs356.NM_PROC_MEDI }).OrderBy(w => w.CO_PROC_MEDI).ToList();

            /*
                var resColaborador = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                                        where (tb03.CO_COL == id_colaborador)
                                        select new { tb03.CO_COL, tb03.CO_EMP }).FirstOrDefault();             
             */

            if (res != null)
            {
                ddl.DataTextField = "CO_PROC_MEDI";
                ddl.DataValueField = "ID_PROC_MEDI_PROCE";
                ddl.DataSource = res;
                ddl.DataBind();
            }

            ddl.Items.Insert(0, new ListItem(".'. Selecione .'.", ""));

            if (!string.IsNullOrEmpty(selec) && ddl.Items.FindByValue(selec) != null)
                ddl.SelectedValue = selec;
        }
        private void CalcularPreencherValoresTabelaECalculado(DropDownList ddlProcConsul, DropDownList ddlOperPlano, DropDownList ddlPlano, TextBox txtValor)
        {
            int idProc = (!string.IsNullOrEmpty(ddlProcConsul.SelectedValue) ? int.Parse(ddlProcConsul.SelectedValue) : 0);
            int idOper = (!string.IsNullOrEmpty(ddlOperPlano.SelectedValue) ? int.Parse(ddlOperPlano.SelectedValue) : 0);
            int idPlan = (!string.IsNullOrEmpty(ddlPlano.SelectedValue) ? int.Parse(ddlPlano.SelectedValue) : 0);

            //Apenas se tiver sido escolhido algum procedimento
            if (idProc != 0)
            {
                int? procAgrupador = (int?)null; // Se for procedimento de alguma operadora, verifica qual o id do procedimento agrupador do mesmo
                if (!string.IsNullOrEmpty(ddlOperPlano.SelectedValue))
                    procAgrupador = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(idProc).ID_AGRUP_PROC_MEDI_PROCE;

                AuxiliCalculos.ValoresProcedimentosMedicos ob = AuxiliCalculos.RetornaValoresProcedimentosMedicos((idProc), idOper, idPlan);
                txtValor.Text = ob.VL_CALCULADO.ToString("N2"); // Insere o valor Calculado
            }
        }
        private TBS370_PLANE_AVALI RecuperaPlanejamento(int? ID_PLANE_AVALI, int CO_ALU)
        {
            if (ID_PLANE_AVALI.HasValue)
                return TBS370_PLANE_AVALI.RetornaPelaChavePrimaria(ID_PLANE_AVALI.Value);
            else // Já que não tem ainda, cria um novo planejamento e retorna um objeto do mesmo no método
            {
                TBS370_PLANE_AVALI tbs370 = new TBS370_PLANE_AVALI();
                tbs370.CO_ALU = CO_ALU;

                //Dados do cadastro
                tbs370.DT_CADAS = DateTime.Now;
                tbs370.CO_COL_CADAS = LoginAuxili.CO_COL;
                tbs370.CO_EMP_COL_CADAS = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                tbs370.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                tbs370.IP_CADAS = Request.UserHostAddress;

                //Dados da situação
                tbs370.CO_SITUA = "A";
                tbs370.DT_SITUA = DateTime.Now;
                tbs370.CO_COL_SITUA = LoginAuxili.CO_COL;
                tbs370.CO_EMP_COL_SITUA = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                tbs370.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                tbs370.IP_SITUA = Request.UserHostAddress;
                TBS370_PLANE_AVALI.SaveOrUpdate(tbs370, true);

                return tbs370;
            }
        }
        private int RecuperaUltimoNrAcao(int ID_PLANE_AVALI)
        {
            var res = (from tbs389 in TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros()
                       where tbs389.TBS386_ITENS_PLANE_AVALI.TBS370_PLANE_AVALI.ID_PLANE_AVALI == ID_PLANE_AVALI
                       select new { tbs389.TBS386_ITENS_PLANE_AVALI.NR_ACAO }).OrderByDescending(w => w.NR_ACAO).FirstOrDefault();

            /*
             *Retorna o último número de ação encontrado (para a agenda e procedimento recebidos como parâmetro) + 1.
             *Se não houver, retorna o número 1
             */
            return (res != null ? res.NR_ACAO + 1 : 1);
        }
        #endregion
        #region gravar registro
        protected void lnkConfirmarProced_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (Session["CO_ALU"] is null)
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Selecione um paciente antes de atribuir procedimentos");
                    return;
                }

                int coPaci = int.Parse(Session["CO_ALU"].ToString());
                int coAgend = int.Parse(hidCoAgendProced.Value);

                int qntItensSelecionados = 0;
                //Inclui registro de item de planejamento na tabela TBS386_ITENS_PLANE_AVALI
                #region Inclui o Item de Planjamento


                if (grdProcedimentos.Rows.Count != 0)
                {
                    foreach (GridViewRow li in grdProcedimentos.Rows)
                    {
                        DropDownList ddlContrat;
                        DropDownList ddlSolic;
                        DropDownList ddlPlan;
                        DropDownList ddlProced;
                        TextBox txtCart;
                        //TextBox txtIdItem;
                        //TextBox valorUnit;
                        //TextBox valorTotal;
                        //TextBox txtQtp;
                        CheckBox chkCort;
                        ddlSolic = ((DropDownList)li.FindControl("ddlSolicProc"));
                        ddlContrat = ((DropDownList)li.FindControl("ddlContratProc"));
                        ddlPlan = ((DropDownList)li.FindControl("ddlPlanoProc"));
                        ddlProced = ((DropDownList)li.FindControl("ddlProcMod"));
                        txtCart = ((TextBox)li.FindControl("txtNrCartProc"));
                        //valorUnit = ((TextBox)li.FindControl("txtValorUnit"));
                        //valorTotal = ((TextBox)li.FindControl("txtValorTotalMod"));
                        //txtQtp = ((TextBox)li.FindControl("txtQTPMod"));
                        //txtIdItem = ((TextBox)li.FindControl("txtCoItemProced"));
                        chkCort = ((CheckBox)li.FindControl("chkCortProc"));

                        if (string.IsNullOrEmpty(ddlProced.SelectedValue))
                        {
                            AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Selecione um procedimento");
                            ddlProced.Focus();
                            AbreModalPadrao("AbreModalProcedHorar();");
                            return;
                        }
                        //if (string.IsNullOrEmpty(txtQtp.Text))
                        //{
                        //    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Informe a quantidade de procedimentos");
                        //    txtQtp.Focus();
                        //    AbreModalPadrao("AbreModalProcedHorar();");
                        //    return;
                        //}
                        if (coPaci == 0 || coPaci <= 0)
                        {
                            AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Para confirmar, é necessário selecionar um paciente");
                            AbreModalPadrao("AbreModalProcedHorar();");
                            return;
                        }

                        TBS174_AGEND_HORAR agend = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(coAgend);

                       // if ((String.IsNullOrEmpty(txtIdItem.Text)))
                        //{
                            agend.TB250_OPERA = TB250_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlContrat.SelectedValue));
                            agend.TB251_PLANO_OPERA = TB251_PLANO_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlPlan.SelectedValue));
                            agend.TBS356_PROC_MEDIC_PROCE = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlProced.SelectedValue));
                            agend.TBS370_PLANE_AVALI = RecuperaPlanejamento((agend.TBS370_PLANE_AVALI != null ? agend.TBS370_PLANE_AVALI.ID_PLANE_AVALI : (int?)null), coPaci);
                            agend.FL_CORTESIA = (chkCort.Checked ? "S" : "N");
                            agend.VL_CONSUL = 0; // (!string.IsNullOrEmpty(valorUnit.Text) ? decimal.Parse(valorUnit.Text) : (decimal?)null);
                            TBS174_AGEND_HORAR.SaveOrUpdate(agend, true);
                        //}
                        //Se tem item de planjamento, trás objeto da entidade dele, se não, cria novo objeto da entidade
                        TBS386_ITENS_PLANE_AVALI tbs386;
                        tbs386 = new TBS386_ITENS_PLANE_AVALI();
                        //if (String.IsNullOrEmpty(txtIdItem.Text) || int.Parse(txtIdItem.Text) <= 0)
                        //{
                        //    tbs386 = new TBS386_ITENS_PLANE_AVALI();
                        //    //Dados do cadastro
                        //    tbs386.DT_CADAS = DateTime.Now;
                        //    tbs386.CO_COL_CADAS = LoginAuxili.CO_COL;
                        //    tbs386.CO_EMP_COL_CADAS = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                        //    tbs386.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                        //    tbs386.IP_CADAS = Request.UserHostAddress;
                        //}
                        //else
                        //{
                        //    tbs386 = TBS386_ITENS_PLANE_AVALI.RetornaPelaChavePrimaria(int.Parse(txtIdItem.Text));
                        //}
                        //Dados da situação
                        tbs386.CO_SITUA = "A";
                        tbs386.DT_SITUA = DateTime.Now;
                        tbs386.CO_COL_SITUA = LoginAuxili.CO_COL;
                        tbs386.CO_EMP_COL_SITUA = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                        tbs386.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                        tbs386.IP_SITUA = Request.UserHostAddress;
                        tbs386.DE_RESUM_ACAO = null;

                        //Dados básicos do item de planejamento
                        tbs386.TBS370_PLANE_AVALI = RecuperaPlanejamento((agend.TBS370_PLANE_AVALI != null ? agend.TBS370_PLANE_AVALI.ID_PLANE_AVALI : (int?)null), coPaci);
                        tbs386.TBS356_PROC_MEDIC_PROCE = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlProced.SelectedValue));
                        tbs386.NR_ACAO = RecuperaUltimoNrAcao(tbs386.TBS370_PLANE_AVALI.ID_PLANE_AVALI);
                        tbs386.QT_SESSO = TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaPelaAgendaEProcedimento(coAgend, TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlProced.SelectedValue)).ID_PROC_MEDI_PROCE).Count; //Conta quantos itens existem na lista para este mesmo e agenda
                        tbs386.DT_INICI = agend.DT_AGEND_HORAR; //Verifica qual a primeira data na lista
                        tbs386.DT_FINAL = agend.DT_AGEND_HORAR; //Verifica qual a última data na lista
                        tbs386.FL_AGEND_FEITA_PLANE = "N";
                        tbs386.QT_PROCED = 1; // int.Parse(txtQtp.Text);
                        tbs386.ID_OPER = String.IsNullOrEmpty(ddlContrat.Text) ? null : (int?)int.Parse(ddlContrat.Text);
                        tbs386.ID_PLAN = String.IsNullOrEmpty(ddlPlan.Text) ? null : (int?)int.Parse(ddlPlan.Text);
                        tbs386.FL_CORTESIA = (chkCort.Checked ? "S" : "N");

                        tbs386.VL_PROCED = 0; //(!string.IsNullOrEmpty(valorUnit.Text) ? decimal.Parse(valorUnit.Text) : (decimal?)null);

                        //Data prevista é a data do agendamento associado
                        tbs386.DT_AGEND = agend.DT_AGEND_HORAR;

                        TBS386_ITENS_PLANE_AVALI.SaveOrUpdate(tbs386, true);

                        //Associa o item criado ao agendamento em contexto na tabela TBS389_ASSOC_ITENS_PLANE_AGEND
                        #region Associa o Item ao Agendamento
                        TBS389_ASSOC_ITENS_PLANE_AGEND tbs389 = TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros().Where(x => x.TBS386_ITENS_PLANE_AVALI.ID_ITENS_PLANE_AVALI == tbs386.ID_ITENS_PLANE_AVALI).FirstOrDefault();
                        if (tbs389 == null)
                        {
                            tbs389 = new TBS389_ASSOC_ITENS_PLANE_AGEND();

                            tbs389.TBS174_AGEND_HORAR = agend;
                            tbs389.TBS386_ITENS_PLANE_AVALI = tbs386;
                        }
                        TBS389_ASSOC_ITENS_PLANE_AGEND.SaveOrUpdate(tbs389, true);

                        if (txtCart.Enabled == true && !String.IsNullOrEmpty(txtCart.Text))
                        {
                            var tb07 = TB07_ALUNO.RetornaPeloCoAlu(coPaci);
                            tb07.NU_CARTAO_SAUDE = txtCart.Text;
                            TB07_ALUNO.SaveOrUpdate(tb07, true);
                        }

                        #endregion
                    }
                }
                #endregion

                //LimparGridHorarios();
                //CarregaGridHorariosAlter();
                //carregaGridNovaComContextoProced();
                //AbreModalPadrao("AbreModalProcedHorar();");
            }
            catch (Exception) { }
        }

        #endregion

        #endregion
        /*FIM TELA DE PROCEDIMENTOS*/

        /*TELA DE PROCEDIMENTOS 2*/
        #region Segunda tela para carregar Procedimentos
        protected void ddlgrupoprocedimento_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlsubgrupoprocedimento = proc.DropSubGrupo(ddlsubgrupoprocedimento, Convert.ToInt32(ddlgrupoprocedimento.SelectedValue));
            AbreModalPadrao("AbreModalInfosSigtap();");
        }

        protected void imgPesqProcedimentos_Click(object sender, ImageClickEventArgs e)
        {
            grdListarSIGTAP.DataSource = proc.PreencheGrigProcedimento(Convert.ToInt32(ddlgrupoprocedimento.SelectedValue), Convert.ToInt32(ddlsubgrupoprocedimento.SelectedValue), tbtextolivreprocedimento.Text);
            grdListarSIGTAP.DataBind();
            Session["temp"] = grdListarSIGTAP.DataSource;
            AbreModalPadrao("AbreModalInfosSigtap();");
        }

        #region necessário para quando paginar o chkbox continua checado
        // colocar no gridview a chave DataKeyNames, o autoincremento da tabela que esta carregando o grid
        private void RememberOldValues()
        {
            ArrayList categoryIDList = new ArrayList();
            int index = -1;
            foreach (GridViewRow row in grdListarSIGTAP.Rows)
            {
                try
                {
                    index = (int)grdListarSIGTAP.DataKeys[row.RowIndex].Value;
                    //index = row.RowIndex;
                    bool result = ((CheckBox)row.FindControl("chkselectEn")).Checked;

                    // Check in the Session
                    if (Session["CHECKED_ITEMS"] != null)
                        categoryIDList = (ArrayList)Session["CHECKED_ITEMS"];
                    if (result)
                    {
                        if (!categoryIDList.Contains(index))
                            categoryIDList.Add(index);
                    }
                    else
                        categoryIDList.Remove(index);
                }
                catch { }
            }
            if (categoryIDList != null && categoryIDList.Count > 0)
                Session["CHECKED_ITEMS"] = categoryIDList;
        }
        private void RePopulateValues()
        {
            ArrayList categoryIDList = (ArrayList)Session["CHECKED_ITEMS"];
            if (categoryIDList != null && categoryIDList.Count > 0)
            {
                foreach (GridViewRow row in grdListarSIGTAP.Rows)
                {
                    //int index = row.RowIndex;
                    int index = (int)grdListarSIGTAP.DataKeys[row.RowIndex].Value;
                    if (categoryIDList.Contains(index))
                    {
                        CheckBox myCheckBox = (CheckBox)row.FindControl("chkselectEn");
                        myCheckBox.Checked = true;
                    }
                }
            }
        }
        #endregion
        protected void grdListarSIGTAP_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            RememberOldValues();
            grdListarSIGTAP.PageIndex = e.NewPageIndex;
            grdListarSIGTAP.DataSource = Session["temp"];
            grdListarSIGTAP.DataBind();
            RePopulateValues();
            AbreModalPadrao("AbreModalInfosSigtap();");
        }

        protected void btnincluir_Click1(object sender, EventArgs e)
        {
            DataTable mDataTable = new DataTable();

            DataColumn mDataColumn;
            mDataColumn = new DataColumn();
            mDataColumn.DataType = Type.GetType("System.String");
            mDataColumn.ColumnName = "ID_PROCEDIMENTO";
            mDataTable.Columns.Add(mDataColumn);

            mDataColumn = new DataColumn();
            mDataColumn.DataType = Type.GetType("System.String");
            mDataColumn.ColumnName = "CO_ALUNO";
            mDataTable.Columns.Add(mDataColumn);

            mDataColumn = new DataColumn();
            mDataColumn.DataType = Type.GetType("System.String");
            mDataColumn.ColumnName = "CO_ALUNO_ID_AGEND_HORAR";
            mDataTable.Columns.Add(mDataColumn);

            DataRow linha;

            foreach (GridViewRow linha2 in grdListarSIGTAP.Rows)
            {
                if (((CheckBox)linha2.Cells[0].FindControl("chkselectEn")).Checked)
                {
                    linha = mDataTable.NewRow();
                    linha["ID_PROCEDIMENTO"] = linha2.Cells[1].Text;
                    linha["CO_ALUNO"] = Session["CO_ALU"].ToString();
                    linha["CO_ALUNO_ID_AGEND_HORAR"] = Session["CO_ALUNO_ID_AGEND_HORAR"].ToString();

                    mDataTable.Rows.Add(linha);                    
                }
            }
            
            Session["dtsigtab"] = mDataTable;
        }

        #endregion
        /*FIM TELA DE PROCEDIMENTOS 2*/


    }
}