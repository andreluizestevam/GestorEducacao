using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;
using System.Data;
using C2BR.GestorEducacao.UI.App_Masters;
using System.Data.Objects;
using System.Globalization;
using System.Drawing;

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8220_AnalisePrevia._8222_AnalisePreviaCompl
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }
        private static Dictionary<string, string> tipoDef = AuxiliBaseApoio.chave(tipoDeficienciaAluno.ResourceManager, true);

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //--------> Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IniPeriAG.Text = DateTime.Now.AddDays(-3).ToString();
                FimPeriAG.Text = DateTime.Now.ToString();
                CarregaAgendamentosAvaliacao();
                CarregaSolicitacoes(0);

                carregaGridQuestionario();
                carregaGridCID();

                grdProfissionais.DataSource = null;
                grdProfissionais.DataBind();
            }
        }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            //Verifica se foi selecionado um agendamento
            if (string.IsNullOrEmpty(hidIdAvaliacao.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o paciente da recepção para salvar a Avaliação!");
                return;
            }

            //Rotinas de persistências da entidade
            TBS381_AVALI_RECEP tbs381 = TBS381_AVALI_RECEP.RetornaPelaChavePrimaria(int.Parse(hidIdAvaliacao.Value));
            tbs381.DE_CONSI_AVALI = (!string.IsNullOrEmpty(txtConsidAvaliador.Text) ? txtConsidAvaliador.Text : null);

            //Dados do cadastro
            tbs381.DT_CADAS = DateTime.Now;
            tbs381.CO_COL_CADAS = LoginAuxili.CO_COL;
            tbs381.CO_EMP_COL_CADAS = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
            tbs381.CO_EMP_CADAS = LoginAuxili.CO_EMP;
            tbs381.IP_CADAS = Request.UserHostAddress;

            //Dados da situação
            tbs381.CO_SITUA = "A";
            tbs381.DT_SITUA = DateTime.Now;
            tbs381.CO_COL_SITUA = LoginAuxili.CO_COL;
            tbs381.CO_EMP_COL_SITUA = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
            tbs381.CO_EMP_SITUA = LoginAuxili.CO_EMP;
            tbs381.IP_SITUA = Request.UserHostAddress;

            TBS381_AVALI_RECEP.SaveOrUpdate(tbs381, true);

            #region Salva os Questionários

            //Deleta os Questionários já associados à esta análise
            var lstQuest = TBS371_PESQU_AVALI_PROCE_SOLIC.RetornaPeloIDAvaliacao(tbs381.ID_AVALI_RECEP);
            foreach (var i in lstQuest)
                TBS371_PESQU_AVALI_PROCE_SOLIC.Delete(i, true);

            //Percorre a grid de Questinários para realizar as persistências
            foreach (GridViewRow li in grdQuestionario.Rows)
            {
                string Quest = (((DropDownList)li.FindControl("ddlQuest")).SelectedValue);
                //Salva apenas se tiver sido selecionada uma das opções
                if (!string.IsNullOrEmpty(Quest))
                {
                    TBS371_PESQU_AVALI_PROCE_SOLIC tbs371 = new TBS371_PESQU_AVALI_PROCE_SOLIC();
                    tbs371.TBS381_AVALI_RECEP = tbs381;
                    tbs371.TB201_AVAL_MASTER = TB201_AVAL_MASTER.RetornaPelaChavePrimaria(int.Parse(Quest));
                    tbs371.FL_TIPO_QUEST = "A";

                    //Dados do cadastro
                    tbs371.DT_CADAS = DateTime.Now;
                    tbs371.CO_COL_CADAS = LoginAuxili.CO_COL;
                    tbs371.CO_EMP_COL_CADAS = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                    tbs371.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                    tbs371.IP_CADAS = Request.UserHostAddress;

                    //Dados da situação
                    tbs371.CO_SITUA = "A";
                    tbs371.DT_SITUA = DateTime.Now;
                    tbs371.CO_COL_SITUA = LoginAuxili.CO_COL;
                    tbs371.CO_EMP_COL_SITUA = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                    tbs371.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                    tbs371.IP_SITUA = Request.UserHostAddress;

                    TBS371_PESQU_AVALI_PROCE_SOLIC.SaveOrUpdate(tbs371, true); // Salva o questionário
                }
            }

            #endregion

            #region Salva os CID's

            //Deleta os cids já associados à esta análise
            var lstCids = TBS385_CID_ANALI_PREVI.RetornaPeloIDAvaliacao(tbs381.ID_AVALI_RECEP);
            foreach (var i in lstCids)
                TBS385_CID_ANALI_PREVI.Delete(i, true);

            //Percorre a grid de CID's para realizar as persistências
            foreach (GridViewRow li in grdCids.Rows)
            {
                string cid = (((DropDownList)li.FindControl("ddlCID")).SelectedValue);
                //Salva apenas se tiver sido selecionada uma das opções
                if (!string.IsNullOrEmpty(cid))
                {
                    TBS385_CID_ANALI_PREVI tbs385 = new TBS385_CID_ANALI_PREVI();
                    tbs385.TBS381_AVALI_RECEP = tbs381;
                    tbs385.TB117_CODIGO_INTERNACIONAL_DOENCA = TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaPelaChavePrimaria(int.Parse(cid));

                    //Dados do cadastro
                    tbs385.DT_CADAS = DateTime.Now;
                    tbs385.CO_COL_CADAS = LoginAuxili.CO_COL;
                    tbs385.CO_EMP_COL_CADAS = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                    tbs385.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                    tbs385.IP_CADAS = Request.UserHostAddress;

                    //Dados da situação
                    tbs385.CO_SITUA = "A";
                    tbs385.DT_SITUA = DateTime.Now;
                    tbs385.CO_COL_SITUA = LoginAuxili.CO_COL;
                    tbs385.CO_EMP_COL_SITUA = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                    tbs385.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                    tbs385.IP_SITUA = Request.UserHostAddress;

                    TBS385_CID_ANALI_PREVI.SaveOrUpdate(tbs385, true);
                }
            }

            #endregion

            #region Salva as definições que ainda não tiverem sido feitas

            //foreach (GridViewRow li in grdProfissionais.Rows)
            //{
            //    //Salva apenas se ainda não tiver sido salvo
            //    if(string.IsNullOrEmpty(((HiddenField)li.FindControl("hidIdItemAval")).Value))
            //    {
            //        //Coleta os objetos e vaores
            //        DropDownList ddlcoCol, ddlidProc, ddlClassFuncio;
            //        TextBox qtSessoes, dtInicio, dtFinal;
            //        ddlClassFuncio = (((DropDownList)li.FindControl("ddlClassFuncional")));
            //        ddlcoCol = (((DropDownList)li.FindControl("ddlProfissional")));
            //        ddlidProc = (((DropDownList)li.FindControl("ddlProced")));
            //        qtSessoes = (((TextBox)li.FindControl("txtQSP")));
            //        dtInicio = (((TextBox)li.FindControl("txtDataInicio")));
            //        dtFinal = (((TextBox)li.FindControl("txtDataTermino")));

            //        #region Validações de Campos

            //        //Se não tiver sido informado o colaborador
            //        if (string.IsNullOrEmpty(ddlcoCol.SelectedValue))
            //        {
            //            AuxiliPagina.EnvioMensagemErro(this.Page, "É preciso informar o Profissional para gravar a associação!");
            //            ddlcoCol.Focus();
            //            return;
            //        }

            //        //Se não tiver sido informado o procedimento
            //        if (string.IsNullOrEmpty(ddlidProc.SelectedValue))
            //        {
            //            AuxiliPagina.EnvioMensagemErro(this.Page, "É preciso informar o Procedimento para gravar a associação!");
            //            ddlidProc.Focus();
            //            return;
            //        }

            //        //Se não tiver sido informada a Quantidade de Sessões
            //        if (string.IsNullOrEmpty(qtSessoes.Text))
            //        {
            //            AuxiliPagina.EnvioMensagemErro(this.Page, "É preciso informar a Quantidade de Sessões para gravar a associação!");
            //            qtSessoes.Focus();
            //            return;
            //        }

            //        //Se não tiver sido informada a Data de Início
            //        if (string.IsNullOrEmpty(dtInicio.Text))
            //        {
            //            AuxiliPagina.EnvioMensagemErro(this.Page, "É preciso informar a Data de Início para gravar a associação!");
            //            dtInicio.Focus();
            //            return;
            //        }

            //        //Se não tiver sido informada a Data de Término
            //        if (string.IsNullOrEmpty(dtFinal.Text))
            //        {
            //            AuxiliPagina.EnvioMensagemErro(this.Page, "É preciso informar a Data de Término para gravar a associação!");
            //            dtFinal.Focus();
            //            return;
            //        }

            //        //Verifica se a quantidade de sessões informadas é maior que a permitida para o procedimento
            //        int? qtMaxSess = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlidProc.SelectedValue)).QT_SESSO_AUTOR;
            //        int qtSessoesInf = int.Parse(qtSessoes.Text);
            //        if (qtMaxSess.HasValue)
            //        {
            //            if (qtSessoesInf > qtMaxSess)
            //            {
            //                AuxiliPagina.EnvioMensagemErro(this.Page,
            //                    "A Quantidade de sessões informadas pra o procedimento "
            //                    + ddlidProc.SelectedItem.Text + " excede a máxima permitida, que é "
            //                    + qtMaxSess.Value.ToString().PadLeft(2, '0'));
            //                qtSessoes.Focus();
            //                return;
            //            }
            //        }

            //        #endregion

            //        TBS378_ASSOC_ITENS_AVALI_PROFI tbs378;
            //                tbs378 = new TBS378_ASSOC_ITENS_AVALI_PROFI();

            //            tbs378.TBS381_AVALI_RECEP = TBS381_AVALI_RECEP.RetornaPelaChavePrimaria(int.Parse(hidIdAvaliacao.Value));
            //            tbs378.CO_COL = int.Parse(ddlcoCol.SelectedValue);
            //            tbs378.TBS356_PROC_MEDIC_PROCE = (TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlidProc.SelectedValue)));
            //            tbs378.QT_SESSO = int.Parse(qtSessoes.Text);
            //            tbs378.DT_INICI = DateTime.Parse(dtInicio.Text);
            //            tbs378.DT_FINAL = DateTime.Parse(dtFinal.Text);

            //            //Dados do cadastro
            //            tbs378.DT_CADAS = DateTime.Now;
            //            tbs378.CO_COL_CADAS = LoginAuxili.CO_COL;
            //            tbs378.CO_EMP_COL_CADAS = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
            //            tbs378.CO_EMP_CADAS = LoginAuxili.CO_EMP;
            //            tbs378.IP_CADAS = Request.UserHostAddress;

            //            //Dados da situação
            //            tbs378.CO_SITUA = "A";
            //            tbs378.DT_SITUA = DateTime.Now;
            //            tbs378.CO_COL_SITUA = LoginAuxili.CO_COL;
            //            tbs378.CO_EMP_COL_SITUA = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
            //            tbs378.CO_EMP_SITUA = LoginAuxili.CO_EMP;
            //            tbs378.IP_SITUA = Request.UserHostAddress;
            //    }
            //}

            #endregion

            AuxiliPagina.RedirecionaParaPaginaSucesso("Análise Prévia registrada com êxito!", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
        }

        void CurrentPadraoCadastro_OnDelete(System.Data.Objects.DataClasses.EntityObject entity)
        {
            //if (string.IsNullOrEmpty(hidIdItem.Value))
            //{
            //    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o item de solicitação que deseja cancelar");
            //    grds.Focus();
            //    return;
            //}

            //var tbs368 = TBS368_RECEP_SOLIC_ITENS.RetornaPelaChavePrimaria(int.Parse(hidIdItem.Value));
            //tbs368.CO_SITUA = "C";
            //tbs368.CO_COL_SITUA = LoginAuxili.CO_COL;
            //tbs368.CO_EMP_COL_SITUA = TB03_COLABOR.RetornaTodosRegistros().Where(w => w.CO_COL == LoginAuxili.CO_COL).FirstOrDefault().CO_EMP;
            //tbs368.CO_EMP_SITUA = LoginAuxili.CO_EMP;
            //tbs368.DT_SITUA = DateTime.Now;
            //tbs368.IP_SITUA = Request.UserHostAddress;
            //TBS368_RECEP_SOLIC_ITENS.SaveOrUpdate(tbs368, true);
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método responsável por salvar um registro de planejamento para esta avaliação
        /// </summary>
        private TBS370_PLANE_AVALI SalvaPlanejamento(int ID_AGEND_HORAR, int CO_ALU, int ID_AVALI_RECEP)
        {
            bool jaTem = (TBS174_AGEND_HORAR.RetornaTodosRegistros().Where(w => w.ID_AGEND_HORAR == ID_AGEND_HORAR && w.TBS370_PLANE_AVALI != null).Any());

            //Se já tiver, resgata, se não tiver, cria
            TBS370_PLANE_AVALI tbs370;
            if (jaTem)
            {
                var res = (TBS174_AGEND_HORAR.RetornaTodosRegistros().Where(w => w.ID_AGEND_HORAR == ID_AGEND_HORAR).FirstOrDefault());
                res.TBS370_PLANE_AVALIReference.Load();
                tbs370 = res.TBS370_PLANE_AVALI;
            }
            else
            {
                //Cria um planejamento para a Avaliação recebida como parâmetro
                tbs370 = new TBS370_PLANE_AVALI();
                tbs370.TBS381_AVALI_RECEP = (TBS381_AVALI_RECEP.RetornaPelaChavePrimaria(ID_AVALI_RECEP));
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
            }
            return tbs370;
        }

        /// <summary>
        /// Método responsável por salvar os agendamentos selecionados
        /// </summary>
        protected bool SalvaAgendamentos(int ID_ASSOC_ITENS_AVALI_PROFI, string CLASS_FUNCI, int CO_ALU, int ID_PROC, int QSP, DateTime dtInicial, DateTime dtFinal, bool procFaturm, bool procAtendm, string idOper, string idPlan)
        {
            try //Trata erros desconhecidos
            {
                bool temAgendamento = false;
                int auxNR = 0;
                //Para cada item de agenda selecionado
                foreach (GridViewRow li in grdAgenda.Rows)
                {
                    //Apenas se estiver selecionado
                    if (((CheckBox)li.FindControl("chkSelectAgend")).Checked)
                    {
                        auxNR++;
                        int idAgenda = int.Parse(((HiddenField)li.FindControl("hidIdAgenda")).Value);

                        //Retorna o id do planejamento desta AGENDA
                        TBS370_PLANE_AVALI tbs370obPl = SalvaPlanejamento(idAgenda, CO_ALU, int.Parse(hidIdAvaliacao.Value));

                        TBS356_PROC_MEDIC_PROCE tbs356 = (TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(ID_PROC));

                        //Instancia um objeto da agenda para realizar as persistências
                        TBS174_AGEND_HORAR tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(idAgenda);
                        bool novoAgendamento = (!tbs174.CO_ALU.HasValue);
                        //Para cada item de agenda, é gravado um item de planejamento
                        #region Item de Planejamento

                        TBS386_ITENS_PLANE_AVALI tbs386 = new TBS386_ITENS_PLANE_AVALI();
                        //Dados básicos do item de planejamento
                        tbs386.TBS370_PLANE_AVALI = tbs370obPl;
                        tbs386.TBS356_PROC_MEDIC_PROCE = tbs356;
                        tbs386.TBS378_ASSOC_ITENS_AVALI_PROFI = (TBS378_ASSOC_ITENS_AVALI_PROFI.RetornaPelaChavePrimaria(ID_ASSOC_ITENS_AVALI_PROFI));
                        tbs386.NR_ACAO = auxNR;
                        tbs386.QT_SESSO = QSP;
                        tbs386.DT_INICI = dtInicial;
                        tbs386.DT_FINAL = dtFinal;
                        //O Resumo fica sendo neste caso o nome do procedimento
                        tbs386.DE_RESUM_ACAO = (tbs356.NM_PROC_MEDI.Length > 60 ? tbs356.NM_PROC_MEDI.Substring(0, 60) : tbs356.NM_PROC_MEDI);
                        tbs386.FL_AGEND_FEITA_PLANE = "N";

                        //Define se é para atendimento e faturamento
                        tbs386.FL_PROCE_ATEND = (procAtendm ? "S" : "N");
                        tbs386.FL_PROCE_FATUR = (procFaturm ? "S" : "N");

                        //Dados do cadastro
                        tbs386.DT_CADAS = DateTime.Now;
                        tbs386.CO_COL_CADAS = LoginAuxili.CO_COL;
                        tbs386.CO_EMP_COL_CADAS = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                        tbs386.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                        tbs386.IP_CADAS = Request.UserHostAddress;

                        //Dados da situação
                        tbs386.CO_SITUA = "A";
                        tbs386.DT_SITUA = DateTime.Now;
                        tbs386.CO_COL_SITUA = LoginAuxili.CO_COL;
                        tbs386.CO_EMP_COL_SITUA = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                        tbs386.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                        tbs386.IP_SITUA = Request.UserHostAddress;

                        //Data prevista é a data do agendamento associado
                        tbs386.DT_AGEND = tbs174.DT_AGEND_HORAR;
                        TBS386_ITENS_PLANE_AVALI.SaveOrUpdate(tbs386);

                        #endregion

                        //Associa o item criado ao agendamento em contexto na tabela TBS389_ASSOC_ITENS_PLANE_AGEND
                        #region Associa o Item ao Agendamento

                        TBS389_ASSOC_ITENS_PLANE_AGEND tbs389 = new TBS389_ASSOC_ITENS_PLANE_AGEND();
                        tbs389.TBS174_AGEND_HORAR = tbs174;
                        tbs389.TBS386_ITENS_PLANE_AVALI = tbs386;
                        TBS389_ASSOC_ITENS_PLANE_AGEND.SaveOrUpdate(tbs389);

                        #endregion

                        //Grava estes campos apenas se for um novo agendamento
                        #region Novo Agendamento

                        if (novoAgendamento)
                        {
                            //Agendamento associado ao item de avaliação
                            tbs174.TBS370_PLANE_AVALI = tbs386.TBS370_PLANE_AVALI;
                            tbs174.TP_AGEND_HORAR = CLASS_FUNCI;
                            tbs174.CO_ALU = CO_ALU;
                            tbs174.CO_EMP_ALU = (TB07_ALUNO.RetornaPeloCoAlu(CO_ALU).CO_EMP);
                            tbs174.TP_CONSU = "N"; // normal de padrão
                            tbs174.FL_CONF_AGEND = "N";
                            tbs174.TBS356_PROC_MEDIC_PROCE = tbs356;
                            tbs174.TB250_OPERA = (!string.IsNullOrEmpty(idOper) ? TB250_OPERA.RetornaPelaChavePrimaria(int.Parse(idOper)) : null);
                            tbs174.TB251_PLANO_OPERA = (!string.IsNullOrEmpty(idPlan) ? TB251_PLANO_OPERA.RetornaPelaChavePrimaria(int.Parse(idPlan)) : null);

                            #region Gera Código da Consulta

                            string coUnid = LoginAuxili.CO_UNID.ToString();
                            int coEmp = LoginAuxili.CO_EMP;
                            string ano = DateTime.Now.Year.ToString().Substring(2, 2);

                            var res = (from tbs174pesq in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                       where tbs174pesq.CO_EMP == coEmp && tbs174pesq.NU_REGIS_CONSUL != null
                                       select new { tbs174pesq.NU_REGIS_CONSUL }).OrderByDescending(w => w.NU_REGIS_CONSUL).FirstOrDefault();

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
                                seq = res.NU_REGIS_CONSUL.Substring(7, 7);
                                seq2 = int.Parse(seq);
                            }

                            seqConcat = seq2 + 1;
                            seqcon = seqConcat.ToString().PadLeft(7, '0');

                            tbs174.NU_REGIS_CONSUL = ano + coUnid.PadLeft(3, '0') + "CO" + seqcon;

                            #endregion
                        }

                        #endregion

                        TBS174_AGEND_HORAR.SaveOrUpdate(tbs174, true);
                        temAgendamento = true;
                    }
                }

                if (auxNR > 0)
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "alerta", string.Format("alert('Foi(ram) agendado(s) {0} para o item de avaliação em questão!');", auxNR), true);

                return temAgendamento;
            }
            catch (Exception e)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Ops, ocorreu um erro desconhecido na tentativa de realizar o Agendamento. Caso persista, contate o nosso suporte.");
            }
            return false;
        }

        /// <summary>
        /// Retorna a quantidade de agendamentos(já salvos ou selecionados na grid de agendamento)
        /// </summary>
        /// <param name="ID_ASSOC_ITENS_AVALI_PROFI"></param>
        /// <returns></returns>
        protected int RetornaQuantidadeAgendamentosItem(int ID_ASSOC_ITENS_AVALI_PROFI)
        {
            int cont = 0;
            //Para cada item de agenda selecionado
            foreach (GridViewRow li in grdAgenda.Rows)
            {
                //Apenas se estiver selecionado
                if (((CheckBox)li.FindControl("chkSelectAgend")).Checked)
                    cont++;
            }

            cont += TBS174_AGEND_HORAR.RetornaTodosRegistros().Where(w => w.TBS378_ASSOC_ITENS_AVALI_PROFI.ID_ASSOC_ITENS_AVALI_PROFI == ID_ASSOC_ITENS_AVALI_PROFI).Count();
            return cont;
        }

        /// <summary>
        /// Exclui item da grid recebendo como parâmetro o index correspondente
        /// </summary>
        protected void ExcluiItemGrid(int Index)
        {
            DataTable dtV = new DataTable();
            DataColumn dcATM;

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "QUESTIONARIO";
            dtV.Columns.Add(dcATM);

            DataRow linha;
            foreach (GridViewRow li in grdQuestionario.Rows)
            {
                linha = dtV.NewRow();
                linha["QUESTIONARIO"] = (((DropDownList)li.FindControl("ddlQuest")).SelectedValue);
                dtV.Rows.Add(linha);
            }

            dtV.Rows.RemoveAt(Index); // Exclui o item de index correspondente
            Session["GridSolic_AV"] = dtV;

            carregaGridNovaComContexto();
        }

        /// <summary>
        /// Cria uma nova linha na forma de pagamento cheque
        /// </summary>
        protected void CriaNovaLinhaGridQuestionario()
        {
            DataTable dtV = new DataTable();
            DataColumn dcATM;

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "QUESTIONARIO";
            dtV.Columns.Add(dcATM);

            DataRow linha;
            foreach (GridViewRow li in grdQuestionario.Rows)
            {
                linha = dtV.NewRow();
                linha["QUESTIONARIO"] = (((DropDownList)li.FindControl("ddlQuest")).SelectedValue);
                dtV.Rows.Add(linha);
            }

            linha = dtV.NewRow();
            linha["QUESTIONARIO"] = "";
            dtV.Rows.Add(linha);

            Session["GridSolic_AV"] = dtV;

            carregaGridNovaComContexto();
        }

        /// <summary>
        /// Carrega o a DataTable em Session com as informações anteriores e a nova linha.
        /// </summary>
        protected void carregaGridNovaComContexto()
        {
            DataTable dtV = new DataTable();

            dtV = (DataTable)Session["GridSolic_AV"];

            grdQuestionario.DataSource = dtV;
            grdQuestionario.DataBind();

            int aux = 0;
            foreach (GridViewRow li in grdQuestionario.Rows)
            {
                DropDownList ddlQuest;
                ddlQuest = (((DropDownList)li.FindControl("ddlQuest")));

                string quest;

                //Coleta os valores do dtv da modal popup
                quest = dtV.Rows[aux]["QUESTIONARIO"].ToString();

                //Seta os valores nos campos da modal popup
                CarregaQuestionarios(ddlQuest);
                ddlQuest.SelectedValue = quest;
                aux++;
            }
        }

        /// <summary>
        /// Carrega a Grid de Cheques da aba de Formas de Pagamento
        /// </summary>
        protected void carregaGridQuestionario()
        {
            DataTable dtV = new DataTable();
            DataColumn dcATM;

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "QUESTIONARIO";
            dtV.Columns.Add(dcATM);

            int i = 1;
            DataRow linha;
            while (i < 1)
            {
                linha = dtV.NewRow();
                linha["QUESTIONARIO"] = "";
                dtV.Rows.Add(linha);
                i++;
            }

            HttpContext.Current.Session.Add("GridSolic_AV", dtV);

            grdQuestionario.DataSource = dtV;
            grdQuestionario.DataBind();

            foreach (GridViewRow li in grdQuestionario.Rows)
            {
                DropDownList ddlQuest;

                ddlQuest = (DropDownList)li.FindControl("ddlQuest");
                CarregaQuestionarios(ddlQuest);
            }
        }

        /// <summary>
        /// Carrega os questionários disponíveis
        /// </summary>
        private void CarregaQuestionarios(DropDownList ddl)
        {
            var res = (from tb201 in TB201_AVAL_MASTER.RetornaTodosRegistros()
                       where tb201.STATUS == "A"
                       select new
                       {
                           nome = tb201.NM_TITU_AVAL,
                           id = tb201.NU_AVAL_MASTER
                       }).OrderBy(w => w.nome).ToList();

            ddl.Items.Clear();
            ddl.SelectedIndex = -1;
            ddl.SelectedValue = null;
            ddl.ClearSelection();

            ddl.DataTextField = "nome";
            ddl.DataValueField = "id";
            ddl.DataSource = res;
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Exclui item da grid recebendo como parâmetro o index correspondente
        /// </summary>
        protected void ExcluiItemGridCID(int Index)
        {
            DataTable dtV = new DataTable();
            DataColumn dcATM;

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "CID";
            dtV.Columns.Add(dcATM);

            DataRow linha;
            foreach (GridViewRow li in grdCids.Rows)
            {
                linha = dtV.NewRow();
                linha["CID"] = (((DropDownList)li.FindControl("ddlCID")).SelectedValue);
                dtV.Rows.Add(linha);
            }

            dtV.Rows.RemoveAt(Index); // Exclui o item de index correspondente
            Session["GridSolic_AV_CID"] = dtV;

            carregaGridNovaComContextoCID();
        }

        /// <summary>
        /// Cria uma nova linha na forma de pagamento cheque
        /// </summary>
        protected void CriaNovaLinhaGridCID()
        {
            DataTable dtV = new DataTable();
            DataColumn dcATM;

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "CID";
            dtV.Columns.Add(dcATM);

            DataRow linha;
            foreach (GridViewRow li in grdCids.Rows)
            {
                linha = dtV.NewRow();
                linha["CID"] = (((DropDownList)li.FindControl("ddlCID")).SelectedValue);
                dtV.Rows.Add(linha);
            }

            linha = dtV.NewRow();
            linha["CID"] = "";
            dtV.Rows.Add(linha);

            Session["GridSolic_AV_CID"] = dtV;

            carregaGridNovaComContextoCID();
        }

        /// <summary>
        /// Carrega o a DataTable em Session com as informações anteriores e a nova linha.
        /// </summary>
        protected void carregaGridNovaComContextoCID()
        {
            DataTable dtV = new DataTable();

            dtV = (DataTable)Session["GridSolic_AV_CID"];

            grdCids.DataSource = dtV;
            grdCids.DataBind();

            int aux = 0;
            foreach (GridViewRow li in grdCids.Rows)
            {
                DropDownList ddlcid;
                ddlcid = (((DropDownList)li.FindControl("ddlCID")));

                string cid;

                //Coleta os valores do dtv da modal popup
                cid = dtV.Rows[aux]["CID"].ToString();

                CarregaCID(ddlcid);
                //Seta os valores nos campos da modal popup
                ddlcid.SelectedValue = cid;
                aux++;
            }
        }

        /// <summary>
        /// Carrega a Grid de Cheques da aba de Formas de Pagamento
        /// </summary>
        protected void carregaGridCID()
        {
            DataTable dtV = new DataTable();
            DataColumn dcATM;

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "CID";
            dtV.Columns.Add(dcATM);

            int i = 1;
            DataRow linha;
            while (i < 1)
            {
                linha = dtV.NewRow();
                linha["CID"] = "";
                dtV.Rows.Add(linha);
                i++;
            }

            HttpContext.Current.Session.Add("GridSolic_AV_CID", dtV);

            grdCids.DataSource = dtV;
            grdCids.DataBind();

            foreach (GridViewRow li in grdCids.Rows)
            {
                DropDownList ddlCid;

                ddlCid = (DropDownList)li.FindControl("ddlCID");
                CarregaCID(ddlCid);
            }
        }

        /// <summary>
        /// Carrega os CIDS
        /// </summary>
        /// <param name="ddl"></param>
        private void CarregaCID(DropDownList ddl)
        {
            AuxiliCarregamentos.CarregaCID(ddl, false, true);
        }

        /// <summary>
        /// Exclui item da grid recebendo como parâmetro o index correspondente
        /// </summary>
        protected void ExcluiItemGridDEFIN(int Index, string idItem)
        {
            //Apenas permite seguir com a exclusão, se não houver nenhum agendamento ou item de planejamento em aberto para 
            //este item de avaliação
            #region Exclui da Tabela

            //Exclui da tabela caso já tenha sido salvo
            if (!string.IsNullOrEmpty(idItem))
            {
                int iitem = int.Parse(idItem);
                int qtAgnd = TBS174_AGEND_HORAR.RetornaTodosRegistros().Where(w => w.TBS378_ASSOC_ITENS_AVALI_PROFI.ID_ASSOC_ITENS_AVALI_PROFI == iitem && w.CO_SITUA_AGEND_HORAR != "A").Count();
                int qtplan = TBS386_ITENS_PLANE_AVALI.RetornaTodosRegistros().Where(w => w.TBS378_ASSOC_ITENS_AVALI_PROFI.ID_ASSOC_ITENS_AVALI_PROFI == iitem && w.CO_SITUA != "A").Count();

                //Se houver alguma agenda para o item em questão
                if (qtAgnd > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "alerta", string.Format("alert('Não é possível excluir este item de avaliação, pois existem {0} agendamentos referentes à ele com situação diferente de Em Aberto');", qtAgnd), true);
                    return;
                }

                //Se houver algum item de planejamento para o item em questão
                if (qtplan > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "alerta", string.Format("alert('Não é possível excluir este item de avaliação, pois existem {0} itens de planejamento referentes à ele com situação diferente de Em Aberto');", qtplan), true);
                    return;
                }

                LimparDadosItemAvaliacao(iitem);
            }

            #endregion

            DataTable dtV = new DataTable();
            DataColumn dcATM;

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "IDITEMAVAL";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "OPER";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "PLAN";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "CLASSFUNC";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "PROFI";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "PROCED";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "QSP";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "INICIO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "TERMINO";
            dtV.Columns.Add(dcATM);

            DataRow linha;
            foreach (GridViewRow li in grdProfissionais.Rows)
            {
                linha = dtV.NewRow();
                linha["IDITEMAVAL"] = (((HiddenField)li.FindControl("hidIdItemAval")).Value);
                linha["OPER"] = (((HiddenField)li.FindControl("hidIdOper")).Value);
                linha["PLAN"] = (((HiddenField)li.FindControl("hidIdPlan")).Value);
                linha["CLASSFUNC"] = (((DropDownList)li.FindControl("ddlClassFuncional")).SelectedValue);
                linha["PROFI"] = (((DropDownList)li.FindControl("ddlProfissional")).SelectedValue);
                linha["PROCED"] = (((DropDownList)li.FindControl("ddlProced")).SelectedValue);
                linha["QSP"] = (((TextBox)li.FindControl("txtQSP")).Text);
                linha["INICIO"] = (((TextBox)li.FindControl("txtDataInicio")).Text);
                linha["TERMINO"] = (((TextBox)li.FindControl("txtDataTermino")).Text);
                dtV.Rows.Add(linha);
            }

            dtV.Rows.RemoveAt(Index); // Exclui o item de index correspondente
            Session["GridSolic_AV_DEF_PROFI"] = dtV;

            carregaGridNovaComContextoDEFIN();
        }

        /// <summary>
        /// Cria uma nova linha na forma de pagamento cheque
        /// </summary>
        protected void CriaNovaLinhaGridDEFIN()
        {
            DataTable dtV = new DataTable();
            DataColumn dcATM;

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "IDITEMAVAL";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "OPER";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "PLAN";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "CLASSFUNC";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "PROFI";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "PROCED";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "QSP";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "INICIO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "TERMINO";
            dtV.Columns.Add(dcATM);

            DataRow linha;
            foreach (GridViewRow li in grdProfissionais.Rows)
            {
                linha = dtV.NewRow();
                linha["IDITEMAVAL"] = (((HiddenField)li.FindControl("hidIdItemAval")).Value);
                linha["OPER"] = (((HiddenField)li.FindControl("hidIdOper")).Value);
                linha["PLAN"] = (((HiddenField)li.FindControl("hidIdPlan")).Value);
                linha["CLASSFUNC"] = (((DropDownList)li.FindControl("ddlClassFuncional")).SelectedValue);
                linha["PROFI"] = (((DropDownList)li.FindControl("ddlProfissional")).SelectedValue);
                linha["PROCED"] = (((DropDownList)li.FindControl("ddlProced")).SelectedValue);
                linha["QSP"] = (((TextBox)li.FindControl("txtQSP")).Text);
                linha["INICIO"] = (((TextBox)li.FindControl("txtDataInicio")).Text);
                linha["TERMINO"] = (((TextBox)li.FindControl("txtDataTermino")).Text);
                dtV.Rows.Add(linha);
            }

            #region Dados do plano de saúde do paciente

            string oper = "";
            string plan = "";
            if (!string.IsNullOrEmpty(hidCoAlu.Value))
            {
                int coAl = int.Parse(hidCoAlu.Value);
                var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                           where tb07.CO_ALU == coAl
                           select new
                           {
                               oper = (tb07.TB250_OPERA != null ? tb07.TB250_OPERA.ID_OPER : 0),
                               plan = (tb07.TB251_PLANO_OPERA != null ? tb07.TB251_PLANO_OPERA.ID_PLAN : 0),
                           }).FirstOrDefault();

                if (res != null)
                {
                    oper = (res.oper != 0 ? res.oper.ToString() : "");
                    plan = (res.plan != 0 ? res.plan.ToString() : "");
                }
            }

            #endregion

            linha = dtV.NewRow();
            linha["IDITEMAVAL"] = "";
            linha["OPER"] = oper;
            linha["PLAN"] = plan;
            linha["CLASSFUNC"] = "";
            linha["PROFI"] = "";
            linha["PROCED"] = "";
            linha["QSP"] = "";
            linha["INICIO"] = "";
            linha["TERMINO"] = "";
            dtV.Rows.Add(linha);

            Session["GridSolic_AV_DEF_PROFI"] = dtV;

            carregaGridNovaComContextoDEFIN();
        }

        /// <summary>
        /// Carrega o a DataTable em Session com as informações anteriores e a nova linha.
        /// </summary>
        protected void carregaGridNovaComContextoDEFIN()
        {
            DataTable dtV = new DataTable();

            dtV = (DataTable)Session["GridSolic_AV_DEF_PROFI"];

            grdProfissionais.DataSource = dtV;
            grdProfissionais.DataBind();

            int aux = 0;
            foreach (GridViewRow li in grdProfissionais.Rows)
            {
                DropDownList ddlClass, ddlProfi, ddlProced;
                TextBox txtQsp, txtDtInicio, txtDtFinal;
                HiddenField hidIdItemAval, hidIdOper, hidIdPlan;
                hidIdItemAval = (((HiddenField)li.FindControl("hidIdItemAval")));
                hidIdOper = (((HiddenField)li.FindControl("hidIdOper")));
                hidIdPlan = (((HiddenField)li.FindControl("hidIdPlan")));
                ddlClass = (((DropDownList)li.FindControl("ddlClassFuncional")));
                ddlProfi = (((DropDownList)li.FindControl("ddlProfissional")));
                ddlProced = (((DropDownList)li.FindControl("ddlProced")));
                txtQsp = (((TextBox)li.FindControl("txtQSP")));
                txtDtInicio = (((TextBox)li.FindControl("txtDataInicio")));
                txtDtFinal = (((TextBox)li.FindControl("txtDataTermino")));

                string idItemAval, idOper, idPlan, classif, profi, proced, qsp, inicio, final;

                //Coleta os valores do dtv da modal popup
                classif = dtV.Rows[aux]["CLASSFUNC"].ToString();
                profi = dtV.Rows[aux]["PROFI"].ToString();
                proced = dtV.Rows[aux]["PROCED"].ToString();
                qsp = dtV.Rows[aux]["QSP"].ToString();
                inicio = dtV.Rows[aux]["INICIO"].ToString();
                final = dtV.Rows[aux]["TERMINO"].ToString();
                idItemAval = dtV.Rows[aux]["IDITEMAVAL"].ToString();
                idOper = dtV.Rows[aux]["OPER"].ToString();
                idPlan = dtV.Rows[aux]["PLAN"].ToString();

                CarregaClassProfi(ddlClass);
                CarregaProfissional(ddlProfi, classif);
                CarregaProcedimentos(ddlProced, (!string.IsNullOrEmpty(idOper) ? int.Parse(idOper) : 0));

                //Seta os valores
                hidIdItemAval.Value = idItemAval;
                hidIdOper.Value = idOper;
                hidIdPlan.Value = idPlan;
                ddlClass.SelectedValue = classif;
                ddlProfi.SelectedValue = profi;
                ddlProced.SelectedValue = proced;
                txtQsp.Text = qsp;
                txtDtInicio.Text = inicio;
                txtDtFinal.Text = final;

                //Libera o checkbox caso essa linha já tenha sido salva
                if (!string.IsNullOrEmpty(hidIdItemAval.Value))
                    (((CheckBox)li.FindControl("chkselectDef")).Enabled) = true;

                aux++;
            }
        }

        /// <summary>
        /// Carrega a Grid de Cheques da aba de Formas de Pagamento
        /// </summary>
        protected void carregaGridDEFIN()
        {
            DataTable dtV = new DataTable();
            DataColumn dcATM;

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "CLASSFUNC";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "OPER";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "PLAN";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "PROFI";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "PROCED";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "QSP";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "INICIO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "TERMINO";
            dtV.Columns.Add(dcATM);

            int i = 1;
            DataRow linha;
            while (i < 1)
            {
                linha = dtV.NewRow();
                linha["CLASSFUNC"] = "";
                linha["OPER"] = "";
                linha["PLAN"] = "";
                linha["PROFI"] = "";
                linha["PROCED"] = "";
                linha["QSP"] = "";
                linha["INICIO"] = "";
                linha["TERMINO"] = "";
                dtV.Rows.Add(linha);
                i++;
            }

            HttpContext.Current.Session.Add("GridSolic_AV_DEF_PROFI", dtV);

            grdProfissionais.DataSource = dtV;
            grdProfissionais.DataBind();

            foreach (GridViewRow li in grdProfissionais.Rows)
            {
                DropDownList ddlClass, ddlProfi, ddlProced;
                ddlClass = (((DropDownList)li.FindControl("ddlCID")));
                ddlProfi = (((DropDownList)li.FindControl("ddlProfissional")));
                ddlProced = (((DropDownList)li.FindControl("ddlProced")));
                string idOper = (((HiddenField)li.FindControl("hidIdOper")).Value);
                CarregaClassProfi(ddlClass);
                CarregaProfissional(ddlProfi, ddlClass.SelectedValue);
                CarregaProcedimentos(ddlProced, (!string.IsNullOrEmpty(idOper) ? int.Parse(idOper) : 0));
            }
        }

        private void LimparDadosItemAvaliacao(int ID_ASSOC_ITENS_AVALI_PROFI)
        {
            LimparDoAgendamento(ID_ASSOC_ITENS_AVALI_PROFI);
            LimparDoPlanejamento(ID_ASSOC_ITENS_AVALI_PROFI);

            TBS378_ASSOC_ITENS_AVALI_PROFI.Delete(TBS378_ASSOC_ITENS_AVALI_PROFI.RetornaPelaChavePrimaria(ID_ASSOC_ITENS_AVALI_PROFI), true);
        }

        /// <summary>
        /// Exclui o item de avaliação e as suas referências
        /// </summary>
        /// <param name="idItem"></param>
        protected void LimparDoAgendamento(int ID_ASSOC_ITENS_AVALI_PROFI)
        {
            //Todos os agendamentos feitos para este item de avaliação
            var resAgenAtend = (from tbs389 in TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros()
                                where tbs389.TBS386_ITENS_PLANE_AVALI.TBS378_ASSOC_ITENS_AVALI_PROFI.ID_ASSOC_ITENS_AVALI_PROFI == ID_ASSOC_ITENS_AVALI_PROFI
                                && tbs389.TBS386_ITENS_PLANE_AVALI.CO_SITUA == "A"
                                select tbs389).ToList();

            //Deleta todas as associações de procedimentos feitas com esse item de avaliação
            foreach (var i in resAgenAtend)
            {
                TBS389_ASSOC_ITENS_PLANE_AGEND.Delete(i, true);
            }
        }

        /// <summary>
        /// Exclui os itens de planejamentos gerados para determinado item de avaliação
        /// </summary>
        /// <param name="ID_ASSOC_ITENS_AVALI_PROFI"></param>
        protected void LimparDoPlanejamento(int ID_ASSOC_ITENS_AVALI_PROFI)
        {
            var lstPlan = TBS386_ITENS_PLANE_AVALI.RetornaTodosRegistros().Where(w => w.TBS378_ASSOC_ITENS_AVALI_PROFI.ID_ASSOC_ITENS_AVALI_PROFI == ID_ASSOC_ITENS_AVALI_PROFI).ToList();
            foreach (var ilse in lstPlan)
                TBS386_ITENS_PLANE_AVALI.Delete(ilse, true);
        }

        /// <summary>
        /// Carrega as classificações profissionais
        /// </summary>
        /// <param name="ddl"></param>
        protected void CarregaClassProfi(DropDownList ddl)
        {
            AuxiliCarregamentos.CarregaClassificacoesFuncionais(ddl, true, LoginAuxili.CO_EMP, AuxiliCarregamentos.ETiposClassificacoes.agendamento);
        }

        /// <summary>
        /// Carrega os profissionais
        /// </summary>
        /// <param name="ddl"></param>
        protected void CarregaProfissional(DropDownList ddl, string classProfi)
        {
            AuxiliCarregamentos.CarregaProfissionaisSaude(ddl, LoginAuxili.CO_EMP, false, (!string.IsNullOrEmpty(classProfi) ? classProfi : "0"), true);
        }

        /// <summary>
        /// Carrega os procedimentos
        /// </summary>
        /// <param name="ddl"></param>
        protected void CarregaProcedimentos(DropDownList ddl, int idOper = 0)
        {
            AuxiliCarregamentos.CarregaProcedimentosMedicos(ddl, idOper, false);
        }

        /// <summary>
        /// Abre a modal com informações de plano de saúde
        /// </summary>
        private void AbreModalInfosPlanos()
        {
            ScriptManager.RegisterStartupScript(
                this.Page,
                this.GetType(),
                "Acao",
                "AbreModalInfoPlano();",
                true
            );
        }

        /// <summary>
        /// Abre a modal com informações de plano de saúde
        /// </summary>
        private void AbreModalObservacao()
        {
            ScriptManager.RegisterStartupScript(
                this.Page,
                this.GetType(),
                "Acao",
                "AbreModalObservacao();",
                true
            );
        }

        /// <summary>
        /// Carrega as associações de profissionais e procedimentos da avaliação recebida como parâmetro
        /// </summary>
        /// <param name="ID_AVALI_RECEP"></param>
        private void CarregaGridItensAvaliacao(int ID_AVALI_RECEP)
        {
            var res = (from tbs378 in TBS378_ASSOC_ITENS_AVALI_PROFI.RetornaTodosRegistros()
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs378.CO_COL equals tb03.CO_COL
                       where tbs378.TBS381_AVALI_RECEP.ID_AVALI_RECEP == ID_AVALI_RECEP
                       select new
                       {
                           IDITEMAVAL = tbs378.ID_ASSOC_ITENS_AVALI_PROFI,
                           CLASSFUNC = tb03.CO_CLASS_PROFI,
                           PROFI = tb03.CO_COL,
                           PROCED = tbs378.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE,
                           QSP = tbs378.QT_SESSO,
                           INICIO = tbs378.DT_INICI,
                           TERMINO = tbs378.DT_FINAL,
                           OPER = (tbs378.TB250_OPERA != null ? tbs378.TB250_OPERA.ID_OPER : (int?)null),
                           PLAN = (tbs378.TB251_PLANO_OPERA != null ? tbs378.TB251_PLANO_OPERA.ID_PLAN : (int?)null),
                       }).ToList();

            grdProfissionais.DataSource = res;
            grdProfissionais.DataBind();

            //Percorre a grid e insere os valores armazenados nos campos
            foreach (GridViewRow i in grdProfissionais.Rows)
            {
                string hidClass, hidProfi, hidProced, hidQsp, hidInicio, hidFinal, idOper;
                DropDownList ddlClass, ddlProfi, ddlProced;
                TextBox txtQsp, txtInicio, txtFinal;

                //Coleta os valores
                idOper = (((HiddenField)i.FindControl("hidIdOper")).Value);
                hidClass = (((HiddenField)i.FindControl("hidCoClassP")).Value);
                hidProfi = (((HiddenField)i.FindControl("hidCoCol")).Value);
                hidProced = (((HiddenField)i.FindControl("hidIdProc")).Value);
                hidQsp = (((HiddenField)i.FindControl("hidQSP")).Value);
                hidInicio = (((HiddenField)i.FindControl("hidDtInicio")).Value);
                hidFinal = (((HiddenField)i.FindControl("hidDtFinal")).Value);

                //Coleta os objetos
                ddlClass = ((DropDownList)i.FindControl("ddlClassFuncional"));
                ddlProfi = ((DropDownList)i.FindControl("ddlProfissional"));
                ddlProced = ((DropDownList)i.FindControl("ddlProced"));
                txtQsp = (((TextBox)i.FindControl("txtQSP")));
                txtInicio = (((TextBox)i.FindControl("txtDataInicio")));
                txtFinal = (((TextBox)i.FindControl("txtDataTermino")));

                //Seta os valores nos objetos
                CarregaClassProfi(ddlClass);
                ddlClass.SelectedValue = hidClass;
                CarregaProfissional(ddlProfi, ddlClass.SelectedValue);
                ddlProfi.SelectedValue = hidProfi;
                CarregaProcedimentos(ddlProced, (!string.IsNullOrEmpty(idOper) ? int.Parse(idOper) : 0));
                ddlProced.SelectedValue = hidProced;
                txtQsp.Text = hidQsp;
                txtInicio.Text = hidInicio;
                txtFinal.Text = hidFinal;

                (((CheckBox)i.FindControl("chkselectDef")).Enabled) = true;
            }
        }

        /// <summary>
        /// Carrega os questionários de determinada avaliação recebida como parâmetro na grid de questionários
        /// </summary>
        /// <param name="ID_AVALI_RECEP"></param>
        private void CarregaGridQuestionariosBD(int ID_AVALI_RECEP)
        {
            var res = (from tbs371 in TBS371_PESQU_AVALI_PROCE_SOLIC.RetornaTodosRegistros()
                       where tbs371.TBS381_AVALI_RECEP.ID_AVALI_RECEP == ID_AVALI_RECEP
                       select new
                       {
                           QUESTIONARIO = tbs371.TB201_AVAL_MASTER.NU_AVAL_MASTER,
                       }).ToList();

            grdQuestionario.DataSource = res;
            grdQuestionario.DataBind();

            foreach (GridViewRow i in grdQuestionario.Rows)
            {
                //Coleta os objetos
                DropDownList ddlQuest = ((DropDownList)i.FindControl("ddlQuest"));
                CarregaQuestionarios(ddlQuest);
                ddlQuest.SelectedValue = (((HiddenField)i.FindControl("hidNuAval")).Value);
            }
        }

        /// <summary>
        /// Carrega os CID's de determinada avaliação recebida como parâmetro na grid de cids
        /// </summary>
        /// <param name="ID_AVALI_RECEP"></param>
        private void CarregaGridCIDsBD(int ID_AVALI_RECEP)
        {
            var res = (from tbs385 in TBS385_CID_ANALI_PREVI.RetornaTodosRegistros()
                       where tbs385.TBS381_AVALI_RECEP.ID_AVALI_RECEP == ID_AVALI_RECEP
                       select new
                       {
                           CID = tbs385.TB117_CODIGO_INTERNACIONAL_DOENCA.IDE_CID,
                       }).ToList();

            grdCids.DataSource = res;
            grdCids.DataBind();

            foreach (GridViewRow i in grdCids.Rows)
            {
                //Coleta os objetos
                DropDownList ddlcid = ((DropDownList)i.FindControl("ddlCID"));
                CarregaCID(ddlcid);
                ddlcid.SelectedValue = (((HiddenField)i.FindControl("hidIdCID")).Value);
            }
        }

        /// <summary>
        /// Carrega os logs do agendamento recebido como parâmetro
        /// </summary>
        private void CarregaGridLog(int ID_AGEND_AVALI)
        {
            var res = (from tbs380 in TBS380_LOG_ALTER_STATUS_AGEND_AVALI.RetornaTodosRegistros()
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs380.CO_COL_CADAS equals tb03.CO_COL
                       where tbs380.TBS372_AGEND_AVALI.ID_AGEND_AVALI == ID_AGEND_AVALI
                       select new saidaLog
                       {
                           Data = tbs380.DT_CADAS,
                           NO_PROFI = tb03.CO_MAT_COL + " - " + tb03.NO_COL,
                           FL_TIPO = tbs380.FL_TIPO_LOG,
                           FL_CONFIR_AGEND = tbs380.FL_CONFIR_AGEND,
                           CO_SITUA_AGEND = tbs380.CO_SITUA_AGEND,
                           FL_TIPO_AGENDA = tbs380.FL_TIPO_AGENDA,
                           OBS = tbs380.DE_OBSER,
                       }).ToList();

            //Coleta os dados de cadastro e inclui no log
            #region Coleta dados de Cadastro

            //Coleta os dados
            var dados = (from tbs372 in TBS372_AGEND_AVALI.RetornaTodosRegistros()
                         join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs372.CO_COL_CADAS equals tb03.CO_COL
                         where tbs372.ID_AGEND_AVALI == ID_AGEND_AVALI
                         select new
                         {
                             NO = tb03.CO_MAT_COL + " - " + tb03.NO_COL,
                             DT = tbs372.DT_CADAS,
                             TP = tbs372.FL_TIPO_AGENDA,
                         }).FirstOrDefault();

            //Insere em novo objeto do tipo saidaLog
            saidaLog i = new saidaLog();
            i.Data = dados.DT;
            i.NO_PROFI = dados.NO;
            i.FL_TIPO = "A";
            i.FL_TIPO_AGENDA_AVALI = dados.TP;

            res.Add(i); //Adiciona o novo item na lista
            res = res.OrderBy(w => w.Data).ThenBy(w => w.NO_PROFI).ToList(); //Ordena de acordo com a data e nome;

            #endregion

            grdLogAgendamento.DataSource = res;
            grdLogAgendamento.DataBind();
        }

        public class saidaLog
        {
            public DateTime Data { get; set; }
            public string Data_V
            {
                get
                {
                    return this.Data.ToString("dd/MM/yy") + " - " + this.Data.ToString("HH:mm");
                }
            }
            public string NO_PROFI { get; set; }
            public string NO_PROFI_V
            {
                get
                {
                    if (!string.IsNullOrEmpty(this.NO_PROFI))
                        return (this.NO_PROFI.Length > 42 ? this.NO_PROFI.Substring(0, 42) + "..." : this.NO_PROFI);
                    else
                        return " - ";
                }
            }
            public string FL_TIPO { get; set; }
            public string NO_TIPO
            {
                get
                {
                    switch (this.FL_TIPO)
                    {
                        case "P":
                            return "Presença";
                        case "C":
                            return "Cancelamento";
                        case "T":
                            return "Tipo Agenda";
                        case "E":
                            return "Encaminhamento";
                        case "A":
                            return "Cadastro";
                        default:
                            return " - ";
                    }
                }
            }
            public string FL_CONFIR_AGEND { get; set; }
            public string CO_SITUA_AGEND { get; set; }
            public string FL_TIPO_AGENDA { get; set; }
            public string FL_TIPO_AGENDA_AVALI { get; set; }
            public string DE_TIPO
            {
                get
                {
                    string s;
                    //Trata de acordo com o tipo
                    switch (this.FL_TIPO)
                    {
                        //Trata quando é PRESENÇA
                        case "P":
                            s = (this.FL_CONFIR_AGEND == "S" ? "Alterado para Presente" : "Alterado para Ausente");
                            break;
                        //Trata quando é CANCELAMENTO
                        case "C":
                            s = (this.CO_SITUA_AGEND == "C" ? "Alterado para Cancelado" : "Alterado para Agendado");
                            break;
                        //Trata de quando é alteração de TIPO
                        case "T":
                            s = (this.FL_TIPO_AGENDA == "C" ? "Alterado para Consulta de Avaliação" : "Alterado para Lista de Espera");
                            break;
                        //Trata quando é de ENCAMINHAMENTO
                        case "E":
                            s = "Encaminhado para a Consulta";
                            break;
                        //Esse é inserido na "Mão", se for CADASTRO, então verifica se foi cadastrado como lista de espera ou consulta
                        case "A":
                            s = "Inserção de registro" + (this.FL_TIPO_AGENDA_AVALI == "C"
                                ? " de Agendamento de Consulta de Avaliação" : " de Lista de Espera");
                            break;
                        default:
                            s = " - ";
                            break;
                    }
                    return s;
                }
            }
            public string CAMINHO_IMAGEM
            {
                get
                {
                    string s;
                    //Trata de acordo com o tipo
                    switch (this.FL_TIPO)
                    {
                        //Trata quando é PRESENÇA
                        case "P":
                            s = (this.FL_CONFIR_AGEND == "S" ? "/Library/IMG/PGS_PacienteChegou.ico" : "/Library/IMG/PGS_PacienteNaoChegou.ico");
                            break;
                        //Trata quando é CANCELAMENTO
                        case "C":
                            s = (this.CO_SITUA_AGEND == "C" ? "/Library/IMG/PGS_ConsultaCancelada.png" : "/Library/IMG/PGS_ConsultaAtiva.png");
                            break;
                        //Trata de quando é alteração de TIPO
                        case "T":
                            s = (this.FL_TIPO_AGENDA == "C" ? "/Library/IMG/Gestor_ServicosAgendaAtividades.png" : "/Library/IMG/Gestor_ImgDocto.png");
                            break;
                        //Trata quando é de ENCAMINHAMENTO
                        case "E":
                            s = "/Library/IMG/PGS_IC_EncaminharIn.png";
                            break;
                        //Trata quando é de CADASTRO
                        case "A":
                            s = "/Library/IMG/PGN_IconeTelaCadastro2.png";
                            break;
                        default:
                            s = "/Library/IMG/Gestor_SemImagem.png";
                            break;
                    }
                    return s;
                }
            }
            public string OBS { get; set; }
        }

        /// <summary>
        /// Mostra mensagem de erro em javascript
        /// </summary>
        private void MostraMsgErro(string msg)
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "alerta", "customOpen(' + msg + ');", true);
        }

        /// <summary>
        /// Carrega as Operadoras
        /// </summary>
        private void CarregaOperadoras(DropDownList ddl)
        {
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddl, false, true, false, true);
        }

        /// <summary>
        /// Carrega os Planos de saúde de acordo com a operadora
        /// </summary>
        private void CarregaPlanos()
        {
            AuxiliCarregamentos.CarregaPlanosSaude(ddlPlano, ddlOperadora, false, true, false, true);
        }

        /// <summary>
        /// Salva o item de análise prévia de acordo com o tipo recebido como parâmetro
        /// </summary>
        /// <param name="status"></param>
        private void PersistirAnalisePrevia(EStatusAnalisePrevia status)
        {
            //if (string.IsNullOrEmpty(hidIdItem.Value))
            //{
            //    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o item de solicitação para o qual deseja determinar o Profissional de Saúde");
            //    grdSolicitacoes.Focus();
            //    return;
            //}

            /////Só precisa testar se não selecionou o colaborador, se estiver encaminhando
            //if (status == EStatusAnalisePrevia.Encaminhado)
            //{
            //    if (string.IsNullOrEmpty(hidCoCol.Value))
            //    {
            //        AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o Profissional de Saúde que deseja associar ao item de solicitação selecionado");
            //        grdProfissionais.Focus();
            //        return;
            //    }
            //}

            //string OBS = "";
            //#region Coleta a Observacao

            //foreach (GridViewRow gr in grdSolicitacoes.Rows)
            //{
            //    if (((CheckBox)gr.Cells[0].FindControl("chkselectSolic")).Checked)
            //    {
            //        OBS = (((TextBox)gr.Cells[9].FindControl("txtObservacao")).Text);
            //        break;
            //    }
            //}

            //#endregion

            //string st = status.ToString();
            //string ste = status.GetValue();

            //var tbs368 = TBS368_RECEP_SOLIC_ITENS.RetornaPelaChavePrimaria(int.Parse(hidIdItem.Value));
            //tbs368.CO_COL_OBJET_ANALI = (!string.IsNullOrEmpty(hidCoCol.Value) ? int.Parse(hidCoCol.Value) : (int?)null);
            //tbs368.CO_EMP_OBJET_ANALI = (!string.IsNullOrEmpty(hidCoEmpCol.Value) ? int.Parse(hidCoEmpCol.Value) : (int?)null);

            //tbs368.DT_ANALI_PREVI = DateTime.Now;
            //tbs368.DE_OBSER = (!string.IsNullOrEmpty(OBS) ? OBS : null);
            //tbs368.CO_SITUA = (status == EStatusAnalisePrevia.EmAberto ? "A" : status == EStatusAnalisePrevia.EmAnalise ?
            //    "N" : status == EStatusAnalisePrevia.Encaminhado ? "E" : status == EStatusAnalisePrevia.Cancelado ? "C" : "A");
            //tbs368.CO_COL_SITUA = LoginAuxili.CO_COL;
            //tbs368.CO_EMP_COL_SITUA = TB03_COLABOR.RetornaTodosRegistros().Where(w => w.CO_COL == LoginAuxili.CO_COL).FirstOrDefault().CO_EMP;
            //tbs368.CO_EMP_SITUA = LoginAuxili.CO_EMP;
            //tbs368.DT_SITUA = DateTime.Now;
            //tbs368.IP_SITUA = Request.UserHostAddress;
            //TBS368_RECEP_SOLIC_ITENS.SaveOrUpdate(tbs368, true);

            //HttpContext.Current.Session.Remove("FL_Select_Grid_ITSOLIC");
            //HttpContext.Current.Session.Remove("VL_ITEM_SOLIC_SELEC");

            //AuxiliPagina.RedirecionaParaPaginaSucesso("Análise salva com Sucesso!", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
        }

        /// <summary>
        /// Carrega as agendas encontradas de acordo com os parâmetros informados
        /// </summary>
        /// <param name="CO_COL"></param>
        /// <param name="dtInicio"></param>
        /// <param name="dtFinal"></param>
        private void CarregaAgendamentos(int CO_COL, int CO_ALU_SELEC, DateTime dtInicio, DateTime dtFinal)
        {
            TimeSpan? hrInicio = txtHrIni.Text != "" ? TimeSpan.Parse(txtHrIni.Text) : (TimeSpan?)null;
            TimeSpan? hrFim = txtHrFim.Text != "" ? TimeSpan.Parse(txtHrFim.Text) : (TimeSpan?)null;

            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU into l1
                       from lalu in l1.DefaultIfEmpty()
                       where tbs174.CO_COL == CO_COL
                       && EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) >= EntityFunctions.TruncateTime(dtInicio)
                       && EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) <= EntityFunctions.TruncateTime(dtFinal)
                       select new saidaAgendamentos
                       {
                           ID_AGEND_HORAR = tbs174.ID_AGEND_HORAR,
                           DT = tbs174.DT_AGEND_HORAR,
                           HR = tbs174.HR_AGEND_HORAR,
                           NO_ALU = lalu.NO_APE_ALU,
                           CO_ALU_SELEC = CO_ALU_SELEC,
                           CO_ALU = tbs174.CO_ALU,
                       }).OrderBy(w => w.DT).ThenBy(w => w.NO_ALU).ToList();

            var lst = new List<saidaAgendamentos>();

            //Exclui da lista caso o dia da semana correspondente não esteja selecionado
            #region Verifica os itens a serem excluídos
            if (res.Count > 0)
            {
                int aux = 0;
                foreach (var i in res)
                {
                    int dia = (int)i.DT.DayOfWeek;

                    switch (dia)
                    {
                        case 0:
                            if (!chkDom.Checked)
                            { lst.Add(i); }
                            break;
                        case 1:
                            if (!chkSeg.Checked)
                            { lst.Add(i); }
                            break;
                        case 2:
                            if (!chkTer.Checked)
                            { lst.Add(i); }
                            break;
                        case 3:
                            if (!chkQua.Checked)
                            { lst.Add(i); }
                            break;
                        case 4:
                            if (!chkQui.Checked)
                            { lst.Add(i); }
                            break;
                        case 5:
                            if (!chkSex.Checked)
                            { lst.Add(i); }
                            break;
                        case 6:
                            if (!chkSab.Checked)
                            { lst.Add(i); }
                            break;
                    }
                    aux++;
                }
            }
            #endregion

            var resNew = res.Except(lst).ToList();

            //Se tiver horario de inicio, filtra
            if (hrInicio != null)
                resNew = resNew.Where(a => a.hrC >= hrInicio).ToList();

            //Se tiver horario de termino, filtra
            if (hrFim != null)
                resNew = resNew.Where(a => a.hrC <= hrFim).ToList();

            resNew = resNew.OrderBy(w => w.DT).ThenBy(w => w.hrC).ToList();
            grdAgenda.DataSource = resNew;
            grdAgenda.DataBind();
        }

        public class saidaAgendamentos
        {
            //Código do paciente selecionado na grid superior de agendamentos de avaliação
            public int CO_ALU_SELEC { get; set; }
            public int? CO_ALU { get; set; }
            public DateTime DT { get; set; }
            public string HR { get; set; }
            public string DTHR
            {
                get
                {
                    string diaSemana = this.DT.ToString("ddd", new CultureInfo("pt-BR"));
                    return this.DT.ToString("dd/MM/yy") + " - " + this.HR + " " + diaSemana;
                }
            }
            public TimeSpan hrC
            {
                get
                {
                    //DateTime d = DateTime.Parse(hr);
                    return TimeSpan.Parse((HR + ":00"));
                }
            }
            public string NO_ALU { get; set; }
            public string NO_ALU_V
            {
                get
                {
                    if (!string.IsNullOrEmpty(NO_ALU))
                        return (this.NO_ALU.Length > 16 ? this.NO_ALU.Substring(0, 16) + "..." : this.NO_ALU);
                    else
                        return " - ";
                }
            }
            public int ID_AGEND_HORAR { get; set; }
            public bool CHK_HABILITA
            {
                get
                {
                    //Se o paciente dessa agenda em questão for diferente do selecionado na grid superior, não habilita
                    if ((this.CO_ALU.HasValue) && (this.CO_ALU != this.CO_ALU_SELEC))
                        return false;
                    else //Se for o mesmo, pode habilitar pois pode haver mais de uma agenda para a mesma agenda se for o mesmo paciente
                        return true;
                }
            }
        }

        /// <summary>
        /// Carrega as solicitacoes em aberto 
        /// </summary>
        private void CarregaSolicitacoes(int ID_RECEP_SOLIC)
        {
            DateTime dtIni = (!string.IsNullOrEmpty(IniPeriAG.Text) ? DateTime.Parse(IniPeriAG.Text) : DateTime.Now);
            DateTime dtFim = (!string.IsNullOrEmpty(FimPeriAG.Text) ? DateTime.Parse(FimPeriAG.Text) : DateTime.Now);

            //var res = (from tbs368 in TBS368_RECEP_SOLIC_ITENS.RetornaTodosRegistros()
            //           where// tbs368.CO_SITUA != "C" //Não precisa ver cancelados
            //               //&& tbs368.CO_SITUA != "E" //Não precisa ver encaminhados
            //           tbs368.TBS367_RECEP_SOLIC.ID_RECEP_SOLIC == ID_RECEP_SOLIC
            //           && (ddlSituacao.SelectedValue != "0" ? tbs368.CO_SITUA == ddlSituacao.SelectedValue : 0 == 0)
            //           select new saidaSolicitacoes
            //           {
            //               ID_OPER = tbs368.TB250_OPERA.ID_OPER,
            //               ID_PLAN = tbs368.TB251_PLANO_OPERA.ID_PLAN,
            //               ID_CATEG_PLANO_SAUDE = tbs368.TB367_CATEG_PLANO_SAUDE.ID_CATEG_PLANO_SAUDE,
            //               NU_PLAN_SAUDE = tbs368.NU_PLAN_SAUDE,
            //               NU_GUIA = tbs368.NU_GUIA,
            //               NU_AUTOR = tbs368.NU_AUTOR,

            //               NU_REGIS = tbs368.TBS367_RECEP_SOLIC.NU_REGIS_RECEP_SOLIC,
            //               ID_RECEP_SOLIC_ITENS = tbs368.ID_RECEP_SOLIC_ITENS,
            //               QTDE = tbs368.NU_QTDE_SESSO,
            //               PROCEDIMENTO = tbs368.TBS356_PROC_MEDIC_PROCE.CO_PROC_MEDI + " - " + tbs368.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
            //               DT = tbs368.DT_CADAS,
            //               ID_ITEM_SOLIC = tbs368.ID_RECEP_SOLIC_ITENS,
            //               STATUS = (tbs368.CO_SITUA == "A" ? "Em Aberto" : tbs368.CO_SITUA == "E" ? "Encaminhado" :
            //               tbs368.CO_SITUA == "N" ? "Em Análise" : " - "),
            //               OBSERVACAO = tbs368.DE_OBSER,
            //           }).OrderBy(w => w.DT).ToList();

            var res = TBS373_AGEND_AVALI_ITENS.RetornaTodosRegistros();
                        //.Where(x => x.tb);

            grdItensRecep.DataSource = null;
            grdItensRecep.DataBind();
        }

        public class saidaSolicitacoes
        {
            public int? ID_OPER { get; set; }
            public int? ID_PLAN { get; set; }
            public int? ID_CATEG_PLANO_SAUDE { get; set; }
            public int? NU_PLAN_SAUDE { get; set; }
            public string NU_GUIA { get; set; }
            public string NU_AUTOR { get; set; }

            public int ID_RECEP_SOLIC_ITENS { get; set; }
            public int CO_ALU { get; set; }
            public string OPERADORA { get; set; }
            public int? QTDE { get; set; }
            public string QTDE_V
            {
                get
                {
                    return (this.QTDE.HasValue ? this.QTDE.Value.ToString().PadLeft(2, '0') : " - ");
                }
            }
            public string PROCEDIMENTO { get; set; }
            public DateTime DT { get; set; }
            public int ID_ITEM_SOLIC { get; set; }
            public string STATUS { get; set; }
            public string OBSERVACAO { get; set; }
            public int? CO_COL_OBJET_ANALI { get; set; }
            public string NU_REGIS { get; set; }

            //Dados do Paciente
            public string PACIENTE_R { get; set; }
            public int NU_NIRE { get; set; }
            public string PACIENTE
            {
                get
                {
                    return (this.NU_NIRE.ToString().PadLeft(7, '0') + " - " + this.PACIENTE_R);
                }
            }
            public DateTime? DT_NASC_PAC { get; set; }
            public string IDADE
            {
                get
                {
                    return AuxiliFormatoExibicao.FormataDataNascimento(this.DT_NASC_PAC, AuxiliFormatoExibicao.ETipoDataNascimento.padraoIdade);
                }
            }
            public string SX { get; set; }
            public string siglaDef
            {
                set
                {
                    if (value.Trim() != "")
                        this.TP_DEF = tipoDef[value];
                    else
                        this.TP_DEF = "";
                }
            }
            public string TP_DEF { get; set; }
        }

        /// <summary>
        /// Carrega as solicitacoes em aberto 
        /// </summary>
        private void CarregaAgendamentosAvaliacao()
        {
            DateTime dtIni = (!string.IsNullOrEmpty(IniPeriAG.Text) ? DateTime.Parse(IniPeriAG.Text) : DateTime.Now);
            DateTime dtFim = (!string.IsNullOrEmpty(FimPeriAG.Text) ? DateTime.Parse(FimPeriAG.Text) : DateTime.Now);

            var res = (from tbs372 in TBS372_AGEND_AVALI.RetornaTodosRegistros()
                       join tbs460 in TBS460_AGEND_AVALI_PROFI.RetornaTodosRegistros() on tbs372.ID_AGEND_AVALI equals tbs460.TBS372_AGEND_AVALI.ID_AGEND_AVALI
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs372.CO_ALU equals tb07.CO_ALU
                       join tbs367 in TBS367_RECEP_SOLIC.RetornaTodosRegistros() on tbs372.ID_AGEND_AVALI equals tbs367.TBS372_AGEND_AVALI.ID_AGEND_AVALI into l1
                       from ls in l1.DefaultIfEmpty()
                       where// tbs368.CO_SITUA != "C" //Não precisa ver cancelados
                           //&& tbs368.CO_SITUA != "E" //Não precisa ver encaminhados
                       ((EntityFunctions.TruncateTime(tbs372.DT_AGEND.Value) >= EntityFunctions.TruncateTime(dtIni))
                       && (EntityFunctions.TruncateTime(tbs372.DT_AGEND.Value) <= EntityFunctions.TruncateTime(dtFim)))
                       && tbs372.FL_TIPO_AGENDA == "C"
                       && (ddlSituacao.SelectedValue != "0" ? tbs372.CO_SITUA == ddlSituacao.SelectedValue : 0 == 0)
                       && tbs460.CO_COL_AVALI == LoginAuxili.CO_COL
                       select new saidaPacientes
                       {
                           DT = tbs372.DT_AGEND.Value,
                           NU_REGIS = (ls != null ? ls.NU_REGIS_RECEP_SOLIC : " - "),
                           ID_AGEND_AVALI = tbs372.ID_AGEND_AVALI,
                           PACIENTE_R = tb07.NO_ALU,
                           NU_NIRE = tb07.NU_NIRE,
                           DT_NASC_PAC = tb07.DT_NASC_ALU,
                           SX = tb07.CO_SEXO_ALU,
                           TP_DEF = tb07.TP_DEF,
                       }).OrderBy(w => w.DT).ThenBy(w => w.PACIENTE_R).ToList();

            grdPacientes.DataSource = res;
            grdPacientes.DataBind();
        }

        public class saidaPacientes
        {
            public int ID_AGEND_AVALI { get; set; }
            public string OPERADORA { get; set; }
            public DateTime DT { get; set; }

            public string NU_REGIS { get; set; }

            //Dados do Paciente
            public string PACIENTE_R { get; set; }
            public int NU_NIRE { get; set; }
            public string PACIENTE
            {
                get
                {
                    //return (this.NU_NIRE.ToString().PadLeft(7, '0') + " - " + this.PACIENTE_R);
                    return this.PACIENTE_R;
                }
            }
            public DateTime? DT_NASC_PAC { get; set; }
            public string IDADE
            {
                get
                {
                    return AuxiliFormatoExibicao.FormataDataNascimento(this.DT_NASC_PAC, AuxiliFormatoExibicao.ETipoDataNascimento.padraoSaudeCompleto);
                }
            }
            public string SX { get; set; }
            public string siglaDef
            {
                set
                {
                    if (value.Trim() != "")
                        this.TP_DEF = tipoDef[value];
                    else
                        this.TP_DEF = "";
                }
            }
            public string TP_DEF { get; set; }
        }

        /// <summary>
        /// Devido ao método de reload na grid de Pré-Atendimento, ela perde a seleção do checkbox, este método coleta 
        /// </summary>
        private void selecionaGrid()
        {
            //CheckBox chk;
            //string idItem;
            //// Valida se a grid de atividades possui algum registro
            //if (grdSolicitacoes.Rows.Count != 0)
            //{
            //    // Passa por todos os registros da grid de atividades
            //    foreach (GridViewRow linha in grdSolicitacoes.Rows)
            //    {
            //        idItem = (((HiddenField)linha.Cells[0].FindControl("hidItemSolic")).Value);
            //        int idI = (int)HttpContext.Current.Session["VL_ITEM_SOLIC_SELEC"];

            //        if (int.Parse(idItem) == idI)
            //        {
            //            chk = (CheckBox)linha.Cells[0].FindControl("chkselect");
            //            chk.Checked = true;
            //        }
            //    }
            //}
        }

        #endregion

        #region Funções de Campo

        protected void grdPacientes_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                ImageButton img = (((ImageButton)e.Row.FindControl("imgSituacao")));
                CheckBox chk = (((CheckBox)e.Row.FindControl("chkSelectPaciente")));
                int idAgenda = int.Parse(((HiddenField)e.Row.FindControl("hidIdAgendaAval")).Value);

                var resAgend = (from tbs372 in TBS372_AGEND_AVALI.RetornaTodosRegistros()
                                where tbs372.ID_AGEND_AVALI == idAgenda
                                select new
                                {
                                    tbs372.FL_CONFIR_AGEND,
                                    tbs372.CO_SITUA,
                                }).FirstOrDefault();

                if (resAgend != null)
                {
                    if (resAgend.CO_SITUA == "C") //Se estiver cancelado
                    {
                        img.ImageUrl = "/Library/IMG/PGS_ConsultaCancelada.png";
                    }
                    else if (resAgend.FL_CONFIR_AGEND == "S") //Se estiver confirmada presença
                    {
                        img.ImageUrl = "/Library/IMG/PGS_PacienteChegou.ico";
                    }
                }

                //Libera caso já tenha havido a recepção
                if (TBS367_RECEP_SOLIC.RetornaTodosRegistros().Where(w => w.TBS372_AGEND_AVALI.ID_AGEND_AVALI == idAgenda).Any())
                    chk.Enabled = true;
            }
        }

        protected void grdAgenda_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                CheckBox chk = (((CheckBox)e.Row.FindControl("chkSelectAgend")));

                if (chk.Enabled)
                    e.Row.BackColor = Color.FromArgb(144, 238, 144);
            }
        }

        protected void imgInfoObser_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            if (grdItensRecep.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdItensRecep.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgInfoObser");

                    if (img.ClientID == atual.ClientID)
                    {
                        txtObser.Text = (((TextBox)linha.FindControl("txtObservacao")).Text);
                        AbreModalObservacao();
                    }
                }
            }
        }

        protected void ddlOperadora_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaPlanos();
            AbreModalInfosPlanos();
        }

        protected void lnkConfirmarPlano_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hidIndexGridRefer.Value))
            {
                //Percorre a grid de solicitações, guardando as informações preenchidas na modal
                foreach (GridViewRow li in grdProfissionais.Rows)
                {
                    if ((int.Parse(hidIndexGridRefer.Value)) == li.RowIndex)
                    {
                        HiddenField idOper = (((HiddenField)li.FindControl("hidIdOper")));
                        HiddenField idPlan = (((HiddenField)li.FindControl("hidIdPlan")));

                        idOper.Value = ddlOperadora.SelectedValue;
                        idPlan.Value = ddlPlano.SelectedValue;

                        //Somente se tiver sido informada uma operadora
                        if (!string.IsNullOrEmpty(idOper.Value))
                        {
                            DropDownList ddlProc;
                            ddlProc = (DropDownList)li.FindControl("ddlProced");
                            CarregaProcedimentos(ddlProc, int.Parse(idOper.Value));
                        }
                    }
                }
                AuxiliPagina.EnvioAvisoGeralSistema(this.Page, "Informações do plano armazenadas com sucesso!");
            }
        }

        protected void chkSelectPaciente_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            CheckBox chk;

            // Valida se a grid de atividades possui algum registro
            if (grdPacientes.Rows.Count != 0)
            {
                // Passa por todos os registros da grid de atividades
                foreach (GridViewRow linha in grdPacientes.Rows)
                {
                    chk = (CheckBox)linha.FindControl("chkSelectPaciente");

                    // Desmarca todos os registros menos o que foi clicado
                    if (chk.ClientID != atual.ClientID)
                        chk.Checked = false;
                    else
                    {
                        if (chk.Checked)
                        {
                            int idAgendAval = int.Parse(((HiddenField)linha.FindControl("hidIdAgendaAval")).Value);

                            var res = TBS372_AGEND_AVALI.RetornaTodosRegistros()
                                      .Where(x => x.ID_AGEND_AVALI == idAgendAval).FirstOrDefault();

                            //Se houve recepção, então houve um cadastro prévio do registro de avaliação 
                            if (res != null)
                            {
                                txtNecessidade.Text = res.DE_OBSER_NECES;

                                //Dados da avaliação
                                var resAvali = TBS373_AGEND_AVALI_ITENS.RetornaTodosRegistros()
                                                .Where(x => x.TBS372_AGEND_AVALI.ID_AGEND_AVALI == res.ID_AGEND_AVALI)
                                                .Select(x => new
                                                {
                                                    x.ID_AGEND_AVALI_ITENS,
                                                    x.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE
                                                }).ToList();

                                if (resAvali != null)
                                {
                                    //hidIdAvaliacao.Value = resAvali.ID_AVALI_RECEP.ToString();
                                    //txtConsidAvaliador.Text = resAvali.DE_CONSI_AVALI;
                                    //CarregaGridItensAvaliacao(resAvali.ID_AVALI_RECEP);
                                    //CarregaGridQuestionariosBD(resAvali.ID_AVALI_RECEP);
                                    //CarregaGridCIDsBD(resAvali.ID_AVALI_RECEP);
                                }

                                //CarregaSolicitacoes(res.ID_RECEP_SOLIC);
                                carregaGridCID();
                                hidCoAlu.Value = res.CO_ALU.ToString();
                            }

                            btnMaisDefinicoes.Enabled = true; // habilita para usar a grid de profissionais
                        }
                        else
                        {
                            hidIdAvaliacao.Value = txtNecessidade.Text = txtConsidAvaliador.Text = hidCoAlu.Value = "";

                            //Limpa grid de Itens de Solicitação
                            grdItensRecep.DataSource = null;
                            grdItensRecep.DataBind();

                            //Limpa grid de questionários
                            grdQuestionario.DataSource = null;
                            grdQuestionario.DataBind();

                            //Limpa grid de CID's
                            grdCids.DataSource = null;
                            grdCids.DataBind();

                            //Limpa grid de Profissionais
                            grdProfissionais.DataSource = null;
                            grdProfissionais.DataBind();

                            //Limpa grid de Agenda
                            grdAgenda.DataSource = null;
                            grdAgenda.DataBind();

                            btnMaisDefinicoes.Enabled = false; // Desabilita para usar a grid de profissionais
                        }
                    }
                }
            }
        }

        protected void imgPesqAgendamentos_OnClick(object sender, EventArgs e)
        {
            CarregaAgendamentosAvaliacao();
        }

        protected void lnkEmAnalise_OnClick(object sender, EventArgs e)
        {
            PersistirAnalisePrevia(EStatusAnalisePrevia.EmAnalise);
        }

        protected void lnkEncaminhar_OnClick(object sender, EventArgs e)
        {
            PersistirAnalisePrevia(EStatusAnalisePrevia.Encaminhado);
        }

        protected void lnkCancelar_OnClick(object sender, EventArgs e)
        {
            PersistirAnalisePrevia(EStatusAnalisePrevia.Cancelado);
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //CarregaSolicitacoes();

            ////Verifica se a grid já possuia um registro selecionado antes, e chama um método responsável por selecioná-lo de novo no PRÉ-ATENDIMENTO
            //if ((string)HttpContext.Current.Session["FL_Select_Grid_B"] == "S")
            //{
            //    selecionaGrid();
            //}
            //updItens.Update();
        }

        protected void imgInfoPlano_OnClick(object sender, EventArgs e)
        {
            //ImageButton atual = (ImageButton)sender;
            //ImageButton img;
            //if (grdSolicitacoes.Rows.Count != 0)
            //{
            //    foreach (GridViewRow linha in grdSolicitacoes.Rows)
            //    {
            //        img = (ImageButton)linha.Cells[5].FindControl("imgInfoPlano");

            //        if (img.ClientID == atual.ClientID)
            //        {
            //            ScriptManager.RegisterStartupScript(
            //                this.Page,
            //                this.GetType(),
            //                "Acao",
            //                "AbreModalInfoPlano();",
            //                true
            //            );
            //        }
            //    }
            //}
        }

        protected void imgSituacao_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            if (grdPacientes.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdPacientes.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgSituacao");
                    if (img.ClientID == atual.ClientID)
                    {
                        string caminho = img.ImageUrl;
                        int idAgenda = int.Parse(((HiddenField)linha.FindControl("hidIdAgendaAval")).Value);
                        CarregaGridLog(idAgenda); //Carrega o log do item clicado

                        //Atribui as informações da linha clicada aos campos correspondentes na modal
                        txtNomePaciMODLOG.Text = ((Label)linha.FindControl("lblNomPaci")).Text;
                        txtNuRegistroMODLOG.Text = linha.Cells[2].Text;
                        txtSexoMODLOG.Text = linha.Cells[3].Text;
                        txtIdadeMODLOG.Text = linha.Cells[4].Text;

                        ScriptManager.RegisterStartupScript(
                            this.Page,
                            this.GetType(),
                            "Acao",
                            "AbreModalLog();",
                            true
                        );
                    }
                }
            }
        }

        protected void btnAddForm_OnClick(object sender, EventArgs e)
        {
            CriaNovaLinhaGridQuestionario();
        }

        protected void lnkAdicionarCID_OnClick(object sender, EventArgs e)
        {
            CriaNovaLinhaGridCID();
        }

        protected void imgExc_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            int aux = 0;
            if (grdQuestionario.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdQuestionario.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgExc");

                    if (img.ClientID == atual.ClientID)
                        aux = linha.RowIndex;
                }
            }
            ExcluiItemGrid(aux);
        }

        protected void imgExcCID_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            int aux = 0;
            if (grdCids.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdCids.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgExc");

                    if (img.ClientID == atual.ClientID)
                        aux = linha.RowIndex;
                }
            }
            ExcluiItemGridCID(aux);
        }

        protected void imgExcDef_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            int aux = 0;
            string idItem = "";
            if (grdProfissionais.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdProfissionais.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgExcDef");
                    idItem = (((HiddenField)linha.FindControl("hidIdItemAval")).Value);

                    if (img.ClientID == atual.ClientID)
                        aux = linha.RowIndex;
                }
            }
            ExcluiItemGridDEFIN(aux, idItem);
        }

        protected void btnMaisDefinicoes_OnClick(object sender, EventArgs e)
        {
            CriaNovaLinhaGridDEFIN();
        }

        protected void imgGravar_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            if (grdProfissionais.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdProfissionais.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgGravar");

                    if (img.ClientID == atual.ClientID)
                    {
                        //Coleta os objetos e valores
                        DropDownList ddlcoCol, ddlidProc, ddlClassFuncio;
                        TextBox qtSessoes, dtInicio, dtFinal;
                        ddlClassFuncio = (((DropDownList)linha.FindControl("ddlClassFuncional")));
                        ddlcoCol = (((DropDownList)linha.FindControl("ddlProfissional")));
                        ddlidProc = (((DropDownList)linha.FindControl("ddlProced")));
                        qtSessoes = (((TextBox)linha.FindControl("txtQSP")));
                        dtInicio = (((TextBox)linha.FindControl("txtDataInicio")));
                        dtFinal = (((TextBox)linha.FindControl("txtDataTermino")));
                        HiddenField hdIdItemAval = (((HiddenField)linha.FindControl("hidIdItemAval")));
                        CheckBox chk = ((CheckBox)linha.FindControl("chkselectDef"));
                        CheckBox chkPF = ((CheckBox)linha.FindControl("chkProcedFatur"));
                        CheckBox chkPA = ((CheckBox)linha.FindControl("chkProcedAtend"));
                        string idOper, idPlan;
                        idOper = (((HiddenField)linha.FindControl("hidIdOper")).Value);
                        idPlan = (((HiddenField)linha.FindControl("hidIdPlan")).Value);

                        #region Validações de Campos

                        //Se não tiver sido informado o colaborador
                        if (string.IsNullOrEmpty(ddlcoCol.SelectedValue))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "É preciso informar o Profissional para gravar a associação!");
                            ddlcoCol.Focus();
                            return;
                        }

                        //Se não tiver sido informado o procedimento
                        if (string.IsNullOrEmpty(ddlidProc.SelectedValue))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "É preciso informar o Procedimento para gravar a associação!");
                            ddlidProc.Focus();
                            return;
                        }

                        //Se não tiver sido informada a Quantidade de Sessões
                        if (string.IsNullOrEmpty(qtSessoes.Text))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "É preciso informar a Quantidade de Sessões para gravar a associação!");
                            qtSessoes.Focus();
                            return;
                        }

                        //Se não tiver sido informada a Data de Início
                        if (string.IsNullOrEmpty(dtInicio.Text))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "É preciso informar a Data de Início para gravar a associação!");
                            dtInicio.Focus();
                            return;
                        }

                        //Se não tiver sido informada a Data de Término
                        if (string.IsNullOrEmpty(dtFinal.Text))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "É preciso informar a Data de Término para gravar a associação!");
                            dtFinal.Focus();
                            return;
                        }

                        //Verifica se a quantidade de sessões informadas é maior que a permitida para o procedimento
                        int? qtMaxSess = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlidProc.SelectedValue)).QT_SESSO_AUTOR;
                        int qtSessoesInf = int.Parse(qtSessoes.Text);
                        if (qtMaxSess.HasValue)
                        {
                            if (qtSessoesInf > qtMaxSess)
                            {
                                //AuxiliPagina.EnvioAvisoGeralSistema(this.Page, "A Quantidade de sessões informadas pra o procedimento '" + ddlidProc.SelectedItem.Text + "' excede a máxima permitida: " + qtMaxSess.Value.ToString().PadLeft(2, '0'));
                                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alerta", "alert('A Quantidade de sessões informadas pra o procedimento " + ddlidProc.SelectedItem.Text + " excede a máxima permitida: " + qtMaxSess.Value.ToString().PadLeft(2, '0') + "');", true);
                                qtSessoes.Focus();
                                return;
                            }
                        }

                        #endregion

                        //Essa parte viabiliza tanto a inclusão quanto a edição
                        TBS378_ASSOC_ITENS_AVALI_PROFI tbs378;
                        if (string.IsNullOrEmpty(hdIdItemAval.Value))
                            tbs378 = new TBS378_ASSOC_ITENS_AVALI_PROFI();
                        else
                            tbs378 = TBS378_ASSOC_ITENS_AVALI_PROFI.RetornaPelaChavePrimaria(int.Parse(hdIdItemAval.Value));

                        tbs378.TBS381_AVALI_RECEP = TBS381_AVALI_RECEP.RetornaPelaChavePrimaria(int.Parse(hidIdAvaliacao.Value));
                        tbs378.TB250_OPERA = (!string.IsNullOrEmpty(idOper) ? TB250_OPERA.RetornaPelaChavePrimaria(int.Parse(idOper)) : null);
                        tbs378.TB251_PLANO_OPERA = (!string.IsNullOrEmpty(idPlan) ? TB251_PLANO_OPERA.RetornaPelaChavePrimaria(int.Parse(idPlan)) : null);
                        tbs378.CO_COL = int.Parse(ddlcoCol.SelectedValue);
                        tbs378.TBS356_PROC_MEDIC_PROCE = (TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlidProc.SelectedValue)));
                        tbs378.QT_SESSO = int.Parse(qtSessoes.Text);
                        tbs378.DT_INICI = DateTime.Parse(dtInicio.Text);
                        tbs378.DT_FINAL = DateTime.Parse(dtFinal.Text);
                        tbs378.FL_PROCE_FATUR = (chkPF.Checked ? "S" : "N");
                        tbs378.FL_PROCE_ATEND = (chkPA.Checked ? "S" : "N");
                        //Dados do cadastro são inseridos apenas no momento do cadastro de fato
                        if (string.IsNullOrEmpty(hdIdItemAval.Value))
                        {
                            tbs378.DT_CADAS = DateTime.Now;
                            tbs378.CO_COL_CADAS = LoginAuxili.CO_COL;
                            tbs378.CO_EMP_COL_CADAS = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                            tbs378.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                            tbs378.IP_CADAS = Request.UserHostAddress;
                        }

                        //Dados da situação
                        tbs378.CO_SITUA = "A";
                        tbs378.DT_SITUA = DateTime.Now;
                        tbs378.CO_COL_SITUA = LoginAuxili.CO_COL;
                        tbs378.CO_EMP_COL_SITUA = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                        tbs378.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                        tbs378.IP_SITUA = Request.UserHostAddress;

                        //Parte responsável por validações e persistências de agendamentos
                        #region Agendamentos

                        //Valida se a quantidade de agendamentos feitos excede a quantidade de sessões informada
                        int qtagrealizado = RetornaQuantidadeAgendamentosItem(tbs378.ID_ASSOC_ITENS_AVALI_PROFI);
                        if (qtagrealizado > qtSessoesInf)
                        {
                            //AuxiliPagina.EnvioAvisoGeralSistema(this.Page, "A Quantidade de agendamentos já realizados somados a quantidade de agendamentos selecionados " + qtagrealizado.ToString("00") + " excede a quantidade de sessões informadas para o procedimento '" + ddlidProc.SelectedItem.Text + "'!");
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "alerta", "alert('A Quantidade de agendamentos já realizados somados a quantidade de agendamentos selecionados: " + qtagrealizado.ToString("00") + " excede a quantidade de sessões informadas para o procedimento " + ddlidProc.SelectedItem.Text + " que é(são) " + qtSessoesInf + "');", true);
                            qtSessoes.Focus();
                            return;
                        }

                        var recepAvali = TBS381_AVALI_RECEP.RetornaPelaChavePrimaria(int.Parse(hidIdAvaliacao.Value));
                        recepAvali.TBS367_RECEP_SOLICReference.Load();
                        int coAlu = recepAvali.TBS367_RECEP_SOLIC.CO_ALU;

                        TBS378_ASSOC_ITENS_AVALI_PROFI.SaveOrUpdate(tbs378);

                        bool teveAgenda = SalvaAgendamentos(tbs378.ID_ASSOC_ITENS_AVALI_PROFI, ddlClassFuncio.SelectedValue, coAlu, int.Parse(ddlidProc.SelectedValue), qtSessoesInf, DateTime.Parse(dtInicio.Text), DateTime.Parse(dtFinal.Text), chkPF.Checked, chkPA.Checked, idOper, idPlan);

                        //Recarrega a grid de agendamentos
                        if (teveAgenda)
                            CarregaAgendamentos(tbs378.CO_COL, coAlu, tbs378.DT_INICI, tbs378.DT_FINAL);

                        #endregion

                        //Verifica se foi feito algum agendamento neste item de avaliação e grava a flag correspondente
                        tbs378.FL_AGEND_FEITA_AVALI = (teveAgenda ? "S" : "N");
                        TBS378_ASSOC_ITENS_AVALI_PROFI.SaveOrUpdate(tbs378);

                        chk.Enabled = true;
                        hdIdItemAval.Value = tbs378.ID_ASSOC_ITENS_AVALI_PROFI.ToString();
                    }
                }
            }
        }

        protected void ddlClassFuncional_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList atual = (DropDownList)sender;
            DropDownList ddl;

            if (grdProfissionais.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdProfissionais.Rows)
                {
                    ddl = (DropDownList)linha.FindControl("ddlClassFuncional");
                    DropDownList ddlProfi = (DropDownList)linha.FindControl("ddlProfissional");

                    //Carrega os profissionais de saúde de acordo com a classificação informada quando encontra o ddl que invocou o postback
                    if (ddl.ClientID == atual.ClientID)
                        CarregaProfissional(ddlProfi, ddl.SelectedValue);
                }
            }
        }

        protected void chkselectDef_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            CheckBox chk;

            // Valida se a grid de atividades possui algum registro
            if (grdProfissionais.Rows.Count != 0)
            {
                // Passa por todos os registros da grid de atividades
                foreach (GridViewRow linha in grdProfissionais.Rows)
                {
                    chk = (CheckBox)linha.FindControl("chkselectDef");

                    // Desmarca todos os registros menos o que foi clicado
                    if (chk.ClientID != atual.ClientID)
                        chk.Checked = false;
                    else
                    {
                        if (chk.Checked)
                        {
                            string coCol, dtInicio, dtFinal;
                            int coAlu = (!string.IsNullOrEmpty(hidCoAlu.Value) ? int.Parse(hidCoAlu.Value) : 0);
                            hidCoCol.Value = coCol = (((DropDownList)linha.FindControl("ddlProfissional")).SelectedValue);
                            hidDtInicio.Value = dtInicio = (((TextBox)linha.FindControl("txtDataInicio")).Text);
                            hidDtFinal.Value = dtFinal = (((TextBox)linha.FindControl("txtDataTermino")).Text);
                            CarregaAgendamentos(int.Parse(coCol), coAlu, DateTime.Parse(dtInicio), DateTime.Parse(dtFinal));
                        }
                        else
                        {
                            hidCoCol.Value = hidDtInicio.Value = hidDtFinal.Value = "";
                        }
                    }
                }
            }
        }

        protected void imgbAgenda_OnClick(object sender, EventArgs e)
        {
            ADMMODULO admModuloMatr = (from admMod in ADMMODULO.RetornaTodosRegistros()
                                       where admMod.nomURLModulo.Contains("8120_RegistroConsulMedMod2/Cadastro.aspx")
                                       select admMod).FirstOrDefault();

            if (admModuloMatr != null)
            {
                this.Session[SessoesHttp.URLOrigem] = HttpContext.Current.Request.Url.PathAndQuery;

                HttpContext.Current.Response.Redirect("/" + String.Format("{0}?moduloNome={1}", admModuloMatr.nomURLModulo, HttpContext.Current.Server.UrlEncode(admModuloMatr.nomModulo)));
            }
        }

        protected void imgPesqAgenda_OnClick(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(hidCoCol.Value) || (string.IsNullOrEmpty(hidDtInicio.Value)) || (string.IsNullOrEmpty(hidDtFinal.Value))))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "É preciso selecionar o profissional e informar o período de referência para pesquisar em sua Agenda.");
                return;
            }

            if (string.IsNullOrEmpty(hidCoAlu.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "É preciso selecionar o paciente para qual será feita a avaliação");
                return;
            }

            CarregaAgendamentos(int.Parse(hidCoCol.Value), int.Parse(hidCoAlu.Value), DateTime.Parse(hidDtInicio.Value), DateTime.Parse(hidDtFinal.Value));
        }

        protected void imgINF_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            CarregaOperadoras(ddlOperadora);
            CarregaPlanos();
            hidIndexGridRefer.Value = "";

            if (grdProfissionais.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdProfissionais.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgINF");

                    if (img.ClientID == atual.ClientID)
                    {
                        string idOper = (((HiddenField)linha.FindControl("hidIdOper")).Value);
                        string idPlan = (((HiddenField)linha.FindControl("hidIdPlan")).Value);

                        #region Seta as informações na modal

                        //Se tiver operadora
                        if (!string.IsNullOrEmpty(idOper))
                        {
                            ddlOperadora.SelectedValue = idOper;
                            CarregaPlanos();

                            //Se tiver plano
                            if (!string.IsNullOrEmpty(idPlan))
                                ddlPlano.SelectedValue = idPlan;
                        }

                        hidIndexGridRefer.Value = linha.RowIndex.ToString();
                        AbreModalInfosPlanos();
                        #endregion
                    }
                }
            }
        }

        #endregion
    }
}