//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: Registrar prontuario   
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
// 03/09/2015 | Bruno max            | Criação da funcionalidade para Cadastro de Prontuario

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using System.Data;
using Resources;
using System.Data.Objects;
using System.IO;

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8270_Prontuario
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
            Page.Form.Enctype = "multipart/form-data";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //IniPeriAG.Text = FimPeriAG.Text = txtDtRealizado.Text = DateTime.Now.ToString();
                //txtIniAgenda.Text = DateTime.Now.AddDays(-5).ToString();
                //txtFimAgenda.Text = DateTime.Now.AddDays(15).ToString();

                //CarregaAgendamentos();
                CarregaProfissionais();
                grdItensAgend.DataSource = grdQuestionario.DataSource = null;
                grdItensAgend.DataBind(); grdQuestionario.DataBind();
                //CarregaProfissionais();

                //AuxiliCarregamentos.CarregaClassificacoesFuncionais(ddlClassFuncio, true, LoginAuxili.CO_EMP, AuxiliCarregamentos.ETiposClassificacoes.agendamento);

                //preCarregaGridAudios();

                CarregaAgendamentos();
            }
        }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            Persistencias();
        }

        #endregion

        #region Carregamentos

        /// <summary>
        /// Realiza as persistências pertinentes às informações de atendimento
        /// </summary>
        private void Persistencias()
        {
            try
            {
                #region Validações
                //int idAgenda = int.Parse(((HiddenField)linha.FindControl("hidIdAgenda")).Value);
                //if (string.IsNullOrEmpty(idAgenda))
                //{
                //    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar a agenda que deseja atender!");
                //    grdPacientes.Focus();
                //    return;
                //}

                //if (string.IsNullOrEmpty(txtDtRealizado.Text))
                //{
                //    AuxiliPagina.EnvioMensagemErro(this.Page, "A data de realização do atendimento é obrigatória!");
                //    txtDtRealizado.Focus();
                //    return;
                //}

                //if (string.IsNullOrEmpty(ddlProfiResp.SelectedValue))
                //{
                //    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o profissional que realizou o atendimento!");
                //    ddlProfiResp.Focus();
                //    return;
                //}

                #endregion

                #region Persistências
                CheckBox chk;

                foreach (GridViewRow li in grdPacientes.Rows)
                {
                    chk = (((CheckBox)li.FindControl("chkSelectPaciente")));


                    if (chk.Checked == true)
                    {
                        //string coAlu = Convert.ToString(((HiddenField)li.FindControl("hidCoAlu")).Value);
                        int idAgenda = int.Parse(((HiddenField)li.FindControl("hidIdAgenda")).Value);
                        int coAlu = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(idAgenda).CO_ALU.Value;
                        #region Grava o prontuario


                        TBS400_PRONT_MASTER tbs400;

                        if (!String.IsNullOrEmpty(hidIdAnamnese.Value))
                        {
                            tbs400 = TBS400_PRONT_MASTER.RetornaPelaChavePrimaria(int.Parse(hidIdAnamnese.Value));

                            //Dados de quem cadastrou o atendimento
                            tbs400.DT_CADAS = DateTime.Now;
                            tbs400.CO_COL_CADAS = LoginAuxili.CO_COL;
                            tbs400.CO_EMP_COL_CADAS = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
                            tbs400.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                            tbs400.IP_CADAS = Request.UserHostAddress;
                            tbs400.CO_COL = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_COL;
                        }
                        else
                            tbs400 = new TBS400_PRONT_MASTER();

                        tbs400.ANAMNSE = txtAnamnese.Text;
                        tbs400.NU_REGIS = "";//ddlReg.SelectedItem.ToString();
                        tbs400.CO_ALU = coAlu;

                        //Dados da situação do atendimento
                        tbs400.CO_SITUA = "A";
                        tbs400.DT_SITUA = DateTime.Now;
                        tbs400.CO_COL_SITUA = LoginAuxili.CO_COL;
                        tbs400.CO_EMP_COL_SITUA = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
                        tbs400.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                        tbs400.IP_SITUA = Request.UserHostAddress;

                        TBS400_PRONT_MASTER.SaveOrUpdate(tbs400);

                        #endregion

                        //Deleta os Questionários já associados à esta análise
                        var lstQuest = TBS401_PRONT_INTENS.RetornaTodosRegistros().Where(i => i.TBS400_PRONT_MASTER.ID_PRONT_MASTER == tbs400.ID_PRONT_MASTER).ToList();
                        foreach (var i in lstQuest)
                            TBS401_PRONT_INTENS.Delete(i, true);

                        ////Percorre a grid de Questinários para realizar as persistências
                        foreach (GridViewRow lii in grdQuestionario.Rows)
                        {
                            string ProgAtendimento = (((TextBox)lii.FindControl("txtProgAtendimento")).Text);
                            //Salva apenas se tiver sido selecionada uma das opções
                            if (!string.IsNullOrEmpty(ProgAtendimento))
                            {
                                TBS401_PRONT_INTENS tbs401 = new TBS401_PRONT_INTENS();

                                tbs401.DE_DESC = ProgAtendimento;
                                tbs401.TBS400_PRONT_MASTER = tbs400;

                                //Dados do cadastro
                                tbs401.DT_CADAS = DateTime.Now;
                                tbs401.CO_COL_CADAS = LoginAuxili.CO_COL;
                                tbs401.CO_EMP_COL_CADAS = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                                tbs401.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                                tbs401.IP_CADAS = Request.UserHostAddress;

                                //Dados da situação
                                tbs401.CO_SITUA = "A";
                                tbs401.DT_SITUA = DateTime.Now;
                                tbs401.CO_COL_SITUA = LoginAuxili.CO_COL;
                                tbs401.CO_EMP_COL_SITUA = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                                tbs401.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                                tbs401.IP_SITUA = Request.UserHostAddress;
                                
                                TBS401_PRONT_INTENS.SaveOrUpdate(tbs401, true); // Salva o questionário
                            }
                        }
                    }
                }

                #endregion

                //#region Imagens

                //HttpPostedFile postFile;
                //string imageName = string.Empty;
                //byte[] path;
                //string[] keys;
                //try
                //{
                //    string contetType = string.Empty;
                //    byte[] imgContent = null;
                //    string[] PhotoTitle;
                //    string PhotoTitlenme;
                //    HttpFileCollection files = HttpContext.Current.Request.Files;
                //    keys = files.AllKeys;
                //    for (int i = 0; i < files.Count; i++)
                //    {
                //        postFile = files[i];
                //        if (postFile.ContentLength > 0)
                //        {
                //            contetType = postFile.ContentType;
                //            path = ColetaConteudoArquivo(postFile.InputStream);
                //            imageName = Path.GetFileName(postFile.FileName);
                //            PhotoTitle = imageName.Split('.');
                //            PhotoTitlenme = PhotoTitle[0];

                //            #region Salva o arquivo

                //            var tbs392 = new TBS392_ANEXO_ATEND();
                //            tbs392.TBS390_ATEND_AGEND = tbs390;
                //            tbs392.NM_ANEXO = PhotoTitlenme;
                //            tbs392.ANEXO = path;
                //            tbs392.EX_ANEXO = Path.GetExtension(files[i].FileName);

                //            //Dados do cadastro
                //            tbs392.DT_CADAS = DateTime.Now;
                //            tbs392.CO_COL_CADAS = LoginAuxili.CO_COL;
                //            tbs392.CO_EMP_COL_CADAS = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                //            tbs392.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                //            tbs392.IP_CADAS = Request.UserHostAddress;

                //            //Dados da situação
                //            tbs392.CO_SITUA = "A";
                //            tbs392.DT_SITUA = DateTime.Now;
                //            tbs392.CO_COL_SITUA = LoginAuxili.CO_COL;
                //            tbs392.CO_EMP_COL_SITUA = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                //            tbs392.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                //            tbs392.IP_SITUA = Request.UserHostAddress;

                //            TBS392_ANEXO_ATEND.SaveOrUpdate(tbs392, true);

                //            #endregion
                //        }
                //    }
                //}
                //catch (Exception e)
                //{
                //    AuxiliPagina.EnvioMensagemErro(this.Page, "Ocorreu um erro ao salvar os anexos! Contacte o suporte");
                //    return;
                //}

                //#endregion

                #region Áudios

                //foreach (GridViewRow i in grdAnexoAudio.Rows)
                //{
                //    FileUpload flp = (((FileUpload)i.FindControl("fupAudios")));
                //    string titulo = (((TextBox)i.FindControl("txtTituloAudio")).Text);
                //    string observa = (((TextBox)i.FindControl("txtObserAudio")).Text);
                //    #region Salva o arquivo

                //    var tbs392 = new TBS392_ANEXO_ATEND();
                //    tbs392.TBS390_ATEND_AGEND = tbs390;
                //    tbs392.NM_ANEXO = flp.FileName;
                //    tbs392.ANEXO = ColetaConteudoArquivo(flp.PostedFile.InputStream);
                //    tbs392.EX_ANEXO = Path.GetExtension(flp.FileName);
                //    tbs392.NM_TITULO = (!string.IsNullOrEmpty(titulo) ? titulo : null);
                //    tbs392.DE_OBSER = (!string.IsNullOrEmpty(observa) ? observa : null);

                //    //Dados do cadastro
                //    tbs392.DT_CADAS = DateTime.Now;
                //    tbs392.CO_COL_CADAS = LoginAuxili.CO_COL;
                //    tbs392.CO_EMP_COL_CADAS = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                //    tbs392.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                //    tbs392.IP_CADAS = Request.UserHostAddress;

                //    //Dados da situação
                //    tbs392.CO_SITUA = "A";
                //    tbs392.DT_SITUA = DateTime.Now;
                //    tbs392.CO_COL_SITUA = LoginAuxili.CO_COL;
                //    tbs392.CO_EMP_COL_SITUA = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                //    tbs392.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                //    tbs392.IP_SITUA = Request.UserHostAddress;

                //    TBS392_ANEXO_ATEND.SaveOrUpdate(tbs392, true);

                //    #endregion
                //}

                #endregion

                AuxiliPagina.RedirecionaParaPaginaSucesso("Atendimento realizado com sucesso!", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
            }
            catch (Exception e)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Ocorreu um erro ao salvar! Entre em contato com o suporte! Erro: " + e.Message);
            }
        }

        /// <summary>
        /// Retorna o conteúdo do arquivo recebido como parâmetro
        /// </summary>
        /// <param name="inputstm"></param>
        /// <returns></returns>
        public byte[] ColetaConteudoArquivo(Stream inputstm)
        {
            Stream fs = inputstm;
            BinaryReader br = new BinaryReader(fs);
            Int32 Int = Convert.ToInt32(fs.Length);
            byte[] bytes = br.ReadBytes(Int);
            return bytes;
        }

        /// <summary>
        /// Carrega a Grid de Cheques da aba de Formas de Pagamento
        /// </summary>
        protected void carregaGridQuestionario(int idAnam)
        {
            DataTable dtV = CriarColunasGridquestionario();

            if (idAnam != 0)
            {
                var res = (from tbs401 in TBS401_PRONT_INTENS.RetornaTodosRegistros()
                           where tbs401.TBS400_PRONT_MASTER.ID_PRONT_MASTER == idAnam
                           select new
                           {
                               tbs401.ID_PRONT_ITENS,
                               tbs401.DE_DESC
                           }).ToList();

                foreach (var i in res)
                {
                    var linha = dtV.NewRow();
                    linha["IDITEM"] = i.ID_PRONT_ITENS;
                    linha["QUESTIONARIO"] = i.DE_DESC;
                    dtV.Rows.Add(linha);
                }
            }

            HttpContext.Current.Session.Add("GridSolic_AV", dtV);

            carregaGridNovaComContexto();
        }

        /// <summary>
        /// Exclui item da grid recebendo como parâmetro o index correspondente
        /// </summary>
        protected void ExcluiItemGrid(int Index)
        {
            DataTable dtV = CriarColunasGridquestionario();

            dtV.Rows.RemoveAt(Index); // Exclui o item de index correspondente
            Session["GridSolic_AV"] = dtV;

            carregaGridNovaComContexto();
        }

        /// <summary>
        /// Cria uma nova linha na forma de pagamento cheque
        /// </summary>
        protected void CriaNovaLinhaGridQuestionario()
        {
            DataTable dtV = CriarColunasGridquestionario();

            var linha = dtV.NewRow();
            linha["IDITEM"] = "";
            linha["QUESTIONARIO"] = "";
            dtV.Rows.Add(linha);

            Session["GridSolic_AV"] = dtV;

            carregaGridNovaComContexto();
        }

        private DataTable CriarColunasGridquestionario()
        {
            DataTable dtV = new DataTable();
            DataColumn dcATM;

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "IDITEM";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "QUESTIONARIO";
            dtV.Columns.Add(dcATM);

            DataRow linha;
            foreach (GridViewRow li in grdQuestionario.Rows)
            {
                linha = dtV.NewRow();
                linha["IDITEM"] = (((HiddenField)li.FindControl("hidIdItem")).Value);
                linha["QUESTIONARIO"] = (((TextBox)li.FindControl("txtProgAtendimento")).Text);
                dtV.Rows.Add(linha);
            }

            return dtV;
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
                ((HiddenField)li.FindControl("hidIdItem")).Value = dtV.Rows[aux]["IDITEM"].ToString();
                ((TextBox)li.FindControl("txtProgAtendimento")).Text = dtV.Rows[aux]["QUESTIONARIO"].ToString();
                aux++;
            }
        }

        /// <summary>
        /// Carrega a Grid de Cheques da aba de Formas de Pagamento
        /// </summary>
        //protected void preCarregaGridAudios()
        //{
        //    DataTable dtV = new DataTable();
        //    DataColumn dcATM;

        //    dcATM = new DataColumn();
        //    dcATM.DataType = System.Type.GetType("System.String");
        //    dcATM.ColumnName = "ARQUIVO";
        //    dtV.Columns.Add(dcATM);

        //    dcATM = new DataColumn();
        //    dcATM.DataType = System.Type.GetType("System.String");
        //    dcATM.ColumnName = "TITULO";
        //    dtV.Columns.Add(dcATM);

        //    dcATM = new DataColumn();
        //    dcATM.DataType = System.Type.GetType("System.String");
        //    dcATM.ColumnName = "OBSERVACAO";
        //    dtV.Columns.Add(dcATM);

        //    int i = 1;
        //    DataRow linha;
        //    while (i < 2)
        //    {
        //        linha = dtV.NewRow();
        //        linha["ARQUIVO"] = "";
        //        linha["TITULO"] = "";
        //        linha["OBSERVACAO"] = "";
        //        dtV.Rows.Add(linha);
        //        i++;
        //    }

        //    grdAnexoAudio.DataSource = dtV;
        //    grdAnexoAudio.DataBind();
        //}

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
        /// Responsável por carregar os profissionais, e selecionar e bloquear o logado caso não seja master
        /// </summary>
        //private void CarregaProfissionais()
        //{
        //    AuxiliCarregamentos.CarregaProfissionaisSaude(ddlProfiResp, 0, false, "0", true);

        //    //Se o logado não for master, e for profissional de saúde, mostra e seleciona ele de padrão
        //    if ((LoginAuxili.CLASSIFICACAO_USU_LOGADO != "M") && (LoginAuxili.FLA_PROFESSOR == "S"))
        //    {
        //        ddlProfiResp.Enabled = false;
        //        ddlProfiResp.SelectedValue = LoginAuxili.CO_COL.ToString();
        //    }
        //}

        /// <summary>
        /// Carrega a lista de agendamentos do paciente recebido como parâmetro 
        /// </summary>
        /// <param name="CO_ALU"></param>
        //private void CarregaAgendaPlanejamento(int CO_ALU)
        //{
        //    //DateTime dtIni = (!string.IsNullOrEmpty(txtIniAgenda.Text) ? DateTime.Parse(txtIniAgenda.Text) : DateTime.Now);
        //    //DateTime dtFim = (!string.IsNullOrEmpty(txtFimAgenda.Text) ? DateTime.Parse(txtFimAgenda.Text) : DateTime.Now);
        //    //string classif = ddlClassFuncio.SelectedValue;
        //    //string situa = ddlSituaProced.SelectedValue;

        //    var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
        //               join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
        //               join tbs390 in TBS390_ATEND_AGEND.RetornaTodosRegistros() on tbs174.ID_AGEND_HORAR equals tbs390.TBS174_AGEND_HORAR.ID_AGEND_HORAR into late
        //               from IIlaten in late.DefaultIfEmpty()
        //               where tbs174.CO_ALU == CO_ALU
        //               //&& ((EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) >= EntityFunctions.TruncateTime(dtIni))
        //               //&& (EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) <= EntityFunctions.TruncateTime(dtFim)))
        //               //&& (classif != "0" ? tb03.CO_CLASS_PROFI == classif : 0 == 0)
        //               //&& (situa != "0" ? tbs174.CO_SITUA_AGEND_HORAR == situa : 0 == 0)
        //               (((LoginAuxili.CLASSIFICACAO_USU_LOGADO != "M") && (LoginAuxili.FLA_PROFESSOR == "S")) ? tbs174.CO_COL == LoginAuxili.CO_COL : 0 == 0)
        //               select new saidaHistoricoAgenda
        //               {
        //                   horaAgenda = tbs174.HR_AGEND_HORAR,
        //                   dtAgenda = tbs174.DT_AGEND_HORAR,
        //                   dtAgenda_Atend = (IIlaten != null ? IIlaten.DT_REALI : (DateTime?)null),
        //                   DE_ACAO = (IIlaten != null ? (!string.IsNullOrEmpty(IIlaten.DE_ACAO_REALI) ? IIlaten.DE_ACAO_REALI : " - ") : (!string.IsNullOrEmpty(tbs174.DE_ACAO_PLAN) ? tbs174.DE_ACAO_PLAN : " - ")),
        //                   CLASS_FUNCI_R = tb03.CO_CLASS_PROFI,
        //                   ID_AGENDA = tbs174.ID_AGEND_HORAR,

        //                   Situacao = tbs174.CO_SITUA_AGEND_HORAR,
        //                   agendaConfirm = tbs174.FL_CONF_AGEND,
        //                   agendaEncamin = tbs174.FL_AGEND_ENCAM,
        //                   faltaJustif = tbs174.FL_JUSTI_CANCE,
        //                   podeClicar = (tbs174.FL_SITUA_ACAO != null ? (tbs174.FL_SITUA_ACAO != "R" ? true : false) : true),
        //               }).ToList();

        //    res = res.OrderBy(w => w.dtAgenda).ThenBy(w => w.hora).ToList();

        //    grdHistoricoAgenda.DataSource = res;
        //    grdHistoricoAgenda.DataBind();
        //}

        public class saidaHistoricoAgenda
        {
            public bool podeClicar { get; set; }
            public TimeSpan hora
            {
                get
                {
                    return TimeSpan.Parse((horaAgenda));
                }
            }
            public string horaAgenda { get; set; }
            public int ID_AGENDA { get; set; }
            public DateTime dtAgenda { get; set; }
            public string dtAgenda_V
            {
                get
                {
                    return this.dtAgenda.ToString("dd/MM/yy") + " - " + this.horaAgenda;
                }
            }
            public DateTime? dtAgenda_Atend { get; set; }
            public string dtAgenda_Atend_V
            {
                get
                {
                    return (this.dtAgenda_Atend.HasValue ? this.dtAgenda_Atend.Value.ToString("dd/MM/yy") + " - " + dtAgenda_Atend.Value.ToString("HH:mm") : " - ");
                }
            }
            public string DE_ACAO { get; set; }
            public string CLASS_FUNCI_R { get; set; }
            public string CLASS_FUNCI
            {
                get
                {
                    return AuxiliGeral.GetNomeClassificacaoFuncional(this.CLASS_FUNCI_R);
                }
            }

            //Trata a imagem
            public string Situacao { get; set; }
            public string agendaConfirm { get; set; }
            public string agendaEncamin { get; set; }
            public string faltaJustif { get; set; }
            public string imagem_URL
            {
                get
                {
                    //Trata as situações possíveis
                    if (this.Situacao == "A") //Se for agendado, pode estar confirmado, presente, ou encaminhado
                    {
                        if (this.agendaEncamin == "S")
                            return "/Library/IMG/PGS_SF_AgendaEncaminhada.png";
                        else if (this.agendaConfirm == "S")
                            return "/Library/IMG/PGS_SF_AgendaConfirmada.png";
                        else
                            return "/Library/IMG/PGS_SF_AgendaEmAberto.png";
                    }
                    else if (this.Situacao == "C") //Se for falta, pode ter sido justificada ou não
                    {
                        if (this.faltaJustif == "S")
                            return "/Library/IMG/PGS_SF_AgendaFaltaJustificada.png";
                        else
                            return "/Library/IMG/PGS_SF_AgendaFaltaNaoJustificada.png";
                    }
                    else if (this.Situacao == "R")
                        return "/Library/IMG/PGS_SF_AgendaRealizada.png";
                    else
                        return "/Library/IMG/Gestor_SemImagem.png";
                }
            }
            public string imagem_TIP
            {
                get
                {
                    switch (this.imagem_URL)
                    {
                        case "/Library/IMG/PGS_SF_AgendaConfirmada.png":
                            return "Paciente presente";
                        case "/Library/IMG/PGS_SF_AgendaEncaminhada.png":
                            return "Agendamento encaminhado para Atendimento";
                        case "/Library/IMG/elsePGS_SF_AgendaEmAberto.png":
                            return "Agendamento em aberto";
                        case "/Library/IMG/PGS_SF_AgendaFaltaJustificada.png":
                            return "Agendamento com Falta Justificada";
                        case "/Library/IMG/PGS_SF_AgendaFaltaNaoJustificada.png":
                            return "Agendamento com Falta Não Justificada";
                        case "/Library/IMG/PGS_SF_AgendaRealizada.png":
                            return "Agendamento com atendimento realizado";
                        default:
                            return " - ";
                    }
                }
            }
            public string imagem_URL_ACAO
            {
                get
                {
                    //Se puder clicar, então está em aberto, e mostra mão negativa, do contrário, mostra positiva
                    return (this.podeClicar ? "/Library/IMG/PGS_IC_Negativo.png" : "/Library/IMG/PGS_IC_Positivo.png");
                }
            }
        }

        private void CarregaProfissionais()
        {
            AuxiliCarregamentos.CarregaProfissionaisSaude(ddlProficional, 0, false, "0", true);

            //Se o logado não for master, e for profissional de saúde, mostra e seleciona ele de padrão
            if ((LoginAuxili.CLASSIFICACAO_USU_LOGADO == "M") && (LoginAuxili.FLA_PROFESSOR == "S"))
            {
                ddlProficional.SelectedValue = LoginAuxili.CO_COL.ToString();
            }
        }

        /// <summary>
        /// Carrega a lista de agendamentos
        /// </summary>
        private void CarregaAgendamentos()
        {
            int idCOL_COL = ddlProficional.SelectedValue == "" ? 0 : ddlProficional.SelectedValue == "0" ? 0 : Convert.ToInt32(ddlProficional.SelectedValue);
            if (idCOL_COL == 0)
                idCOL_COL = LoginAuxili.CO_COL;

            var data = DateTime.Now;

            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                       //Situações diferentes de cancelado e realizado
                       where tbs174.CO_SITUA_AGEND_HORAR != "R" && tbs174.CO_SITUA_AGEND_HORAR != "C"
                       && tbs174.CO_COL == idCOL_COL
                       && (!String.IsNullOrEmpty(drpStatus.SelectedValue) ? (drpStatus.SelectedValue == "A" ? tbs174.DT_AGEND_HORAR >= data : tbs174.DT_AGEND_HORAR <= data) : true)
                       select new saidaPacientes
                       {
                           AgendaHora = tbs174.HR_AGEND_HORAR,
                           CO_ALU = tbs174.CO_ALU,
                           DT = tbs174.DT_AGEND_HORAR,
                           HR = tbs174.HR_AGEND_HORAR,
                           ID_AGEND_HORAR = tbs174.ID_AGEND_HORAR,
                           PACIENTE_R = tb07.NO_ALU,
                           NU_NIRE = tb07.NU_NIRE,
                           DT_NASC_PAC = tb07.DT_NASC_ALU,
                           SX = tb07.CO_SEXO_ALU,
                           TP_DEF = tb07.TP_DEF,
                           podeClicar = (true),
                           //podeClicar = (tbs174.FL_AGEND_ENCAM == "S" && tbs174.CO_SITUA_AGEND_HORAR != "R" ? true : false),

                           Situacao = tbs174.CO_SITUA_AGEND_HORAR,
                           agendaConfirm = tbs174.FL_CONF_AGEND,
                           agendaEncamin = tbs174.FL_AGEND_ENCAM,
                           faltaJustif = tbs174.FL_JUSTI_CANCE,

                       }).OrderBy(w => w.PACIENTE_R).ToList();

            //res = res.OrderByDescending(w => w.DT).ThenBy(w => w.hora).ThenBy(w => w.PACIENTE_R).ToList();

            grdPacientes.DataSource = res.DistinctBy(w => w.PACIENTE_R);
            grdPacientes.DataBind();
        }

        public class saidaPacientes
        {
            public string AgendaHora { get; set; }
            public TimeSpan hora
            {
                get
                {
                    return TimeSpan.Parse((AgendaHora));
                }
            }
            public int? CO_ALU { get; set; }
            public bool podeClicar { get; set; }
            public int ID_AGEND_HORAR { get; set; }
            public DateTime DT { get; set; }
            public string HR { get; set; }
            public string DTHR
            {
                get
                {
                    return this.DT.ToString("dd/MM/yy") + " - " + this.HR;
                }
            }

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
                    return AuxiliFormatoExibicao.FormataDataNascimento(this.DT_NASC_PAC, AuxiliFormatoExibicao.ETipoDataNascimento.padraoSaudeCompleto);
                }
            }
            public string SX { get; set; }
            public string TP_DEF { get; set; }

            //Trata a imagem
            public string Situacao { get; set; }
            public string agendaConfirm { get; set; }
            public string agendaEncamin { get; set; }
            public string faltaJustif { get; set; }
            public string imagem_URL
            {
                get
                {
                    //Trata as situações possíveis
                    if (this.Situacao == "A") //Se for agendado, pode estar confirmado, presente, ou encaminhado
                    {
                        if (this.agendaEncamin == "S")
                            return "/Library/IMG/PGS_SF_AgendaEncaminhada.png";
                        else if (this.agendaConfirm == "S")
                            return "/Library/IMG/PGS_SF_AgendaConfirmada.png";
                        else
                            return "/Library/IMG/PGS_SF_AgendaEmAberto.png";
                    }
                    else if (this.Situacao == "C") //Se for falta, pode ter sido justificada ou não
                    {
                        if (this.faltaJustif == "S")
                            return "/Library/IMG/PGS_SF_AgendaFaltaJustificada.png";
                        else
                            return "/Library/IMG/PGS_SF_AgendaFaltaNaoJustificada.png";
                    }
                    else if (this.Situacao == "R")
                        return "/Library/IMG/PGS_SF_AgendaRealizada.png";
                    else
                        return "/Library/IMG/Gestor_SemImagem.png";
                }
            }
            public string imagem_TIP
            {
                get
                {
                    switch (this.imagem_URL)
                    {
                        case "/Library/IMG/PGS_SF_AgendaConfirmada.png":
                            return "Paciente presente";
                        case "/Library/IMG/PGS_SF_AgendaEncaminhada.png":
                            return "Agendamento encaminhado para Atendimento";
                        case "/Library/IMG/PGS_SF_AgendaEmAberto.png":
                            return "Agendamento em aberto";
                        case "/Library/IMG/PGS_SF_AgendaFaltaJustificada.png":
                            return "Agendamento com Falta Justificada";
                        case "/Library/IMG/PGS_SF_AgendaFaltaNaoJustificada.png":
                            return "Agendamento com Falta Não Justificada";
                        case "/Library/IMG/PGS_SF_AgendaRealizada.png":
                            return "Agendamento com atendimento realizado";
                        default:
                            return " - ";
                    }
                }
            }
        }

        /// <summary>
        /// Carrega os logs do agendamento recebido como parâmetro
        /// </summary>
        private void CarregaGridLog(int ID_AGEND_HORAR)
        {
            var res = (from tbs375 in TBS375_LOG_ALTER_STATUS_AGEND_HORAR.RetornaTodosRegistros()
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs375.CO_COL_CADAS equals tb03.CO_COL
                       where tbs375.TBS174_AGEND_HORAR.ID_AGEND_HORAR == ID_AGEND_HORAR
                       select new saidaLog
                       {
                           Data = tbs375.DT_CADAS,
                           NO_PROFI = tb03.CO_MAT_COL + " - " + tb03.NO_COL,
                           FL_TIPO = tbs375.FL_TIPO_LOG,
                           FL_CONFIR_AGEND = tbs375.FL_CONFIR_AGEND,
                           CO_SITUA_AGEND = tbs375.CO_SITUA_AGEND_HORAR,
                           FL_AGEND_ENCAM = tbs375.FL_AGEND_ENCAM,
                           FL_FALTA_JUSTIF = tbs375.FL_JUSTI,
                           OBS = tbs375.DE_OBSER,
                       }).ToList();

            //Coleta os dados de cadastro e inclui no log
            #region Coleta dados de Cadastro

            //Coleta os dados
            var dados = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                         join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL_SITUA equals tb03.CO_COL
                         where tbs174.ID_AGEND_HORAR == ID_AGEND_HORAR
                         select new
                         {
                             NO = tb03.CO_MAT_COL + " - " + tb03.NO_COL,
                             DT = tbs174.DT_SITUA_AGEND_HORAR,
                         }).FirstOrDefault();

            //Insere em novo objeto do tipo saidaLog
            saidaLog i = new saidaLog();
            i.Data = dados.DT;
            i.NO_PROFI = dados.NO;
            i.FL_TIPO = "A";

            res.Add(i); //Adiciona o novo item na lista
            res = res.OrderBy(w => w.Data).ThenBy(w => w.NO_PROFI).ToList(); //Ordena de acordo com a data e nome;

            #endregion

            #region Coleta dados de Efetivação

            //Coleta os dados
            var resAtend = (from tbs390 in TBS390_ATEND_AGEND.RetornaTodosRegistros()
                            join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs390.CO_COL_ATEND equals tb03.CO_COL
                            where tbs390.TBS174_AGEND_HORAR.ID_AGEND_HORAR == ID_AGEND_HORAR
                            select new
                            {
                                NO = tb03.CO_MAT_COL + " - " + tb03.NO_COL,
                                DT = tbs390.DT_REALI,
                            }).FirstOrDefault();

            if (resAtend != null)
            {
                //Insere em novo objeto do tipo saidaLog
                saidaLog at = new saidaLog();
                at.Data = dados.DT;
                at.NO_PROFI = dados.NO;
                at.FL_TIPO = "R";

                res.Add(at); //Adiciona o novo item na lista
                res = res.OrderBy(w => w.Data).ThenBy(w => w.NO_PROFI).ToList(); //Ordena de acordo com a data e nome;
            }

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
                        case "R":
                            return "Atendimento";
                        default:
                            return " - ";
                    }
                }
            }
            public string FL_CONFIR_AGEND { get; set; }
            public string CO_SITUA_AGEND { get; set; }
            public string FL_TIPO_AGENDA { get; set; }
            public string FL_TIPO_AGENDA_AVALI { get; set; }
            public string FL_AGEND_ENCAM { get; set; }
            public string FL_FALTA_JUSTIF { get; set; }
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
                        //Trata quando é de ENCAMINHAMENTO
                        case "E":
                            s = (this.FL_AGEND_ENCAM == "S" ? "Encaminhado para a Atendimento" : "Remoção de encaminhamento para Atendimento");
                            break;
                        //Esse é inserido na "Mão", se for CADASTRO, então verifica se foi cadastrado como lista de espera ou consulta
                        case "A":
                            s = "Inserção de registro de Agendamento";
                            break;
                        //Trata quando é ATENDIMENTO
                        case "R":
                            s = "Atendimento realizado";
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
                            if (this.CO_SITUA_AGEND == "C")
                            {
                                if (this.FL_FALTA_JUSTIF == "S")
                                    s = "/Library/IMG/PGS_SF_AgendaFaltaJustificada.png";
                                else
                                    s = "/Library/IMG/PGS_SF_AgendaFaltaNaoJustificada.png";
                            }
                            else
                                s = "/Library/IMG/PGS_ConsultaAtiva.png";
                            break;
                        //Trata quando é de ENCAMINHAMENTO
                        case "E":
                            s = "/Library/IMG/PGS_SF_AgendaEncaminhada.png";
                            break;
                        //Trata quando é de CADASTRO
                        case "A":
                            s = "/Library/IMG/PGN_IconeTelaCadastro2.png";
                            break;
                        //Trata quando é ATENDIMENTO
                        case "R":
                            s = "/Library/IMG/PGS_SF_AgendaRealizada.png";
                            break;
                        default:
                            s = "/Library/IMG/PGS_SF_AgendaEmAberto.png";
                            break;
                    }
                    return s;
                }
            }
            public string OBS { get; set; }
        }

        /// <summary>
        /// Carrega os itens de agendamento associados ao agendamento
        /// </summary>
        /// <param name="ID_AGEND_HORAR"></param>
        private void CarregaItensAgendamento(int ID_AGEND_HORAR)
        {
            var res = (from tbs389 in TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros()
                       where tbs389.TBS174_AGEND_HORAR.ID_AGEND_HORAR == ID_AGEND_HORAR
                       select new saidaItensAgenda
                       {
                           NO_PROCED = tbs389.TBS386_ITENS_PLANE_AVALI.TBS356_PROC_MEDIC_PROCE.CO_PROC_MEDI + " - " + tbs389.TBS386_ITENS_PLANE_AVALI.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
                           NO_PRIORI = " - ",
                       }).ToList();

            grdItensAgend.DataSource = res;
            grdItensAgend.DataBind();
        }

        private void CarregarAnamnese(int coAlu)
        {
            var res = TBS400_PRONT_MASTER.RetornaTodosRegistros().Where(a => a.CO_ALU == coAlu).FirstOrDefault();

            if (res != null)
            {
                hidIdAnamnese.Value = res.ID_PRONT_MASTER.ToString();
                txtAnamnese.Text = res.ANAMNSE;
                carregaGridQuestionario(res.ID_PRONT_MASTER);
            }
        }

        /*private void CarregaRegistro(int idAlu)
        {
            var res = (from tbs390 in TBS390_ATEND_AGEND.RetornaTodosRegistros()
                       where (tbs390.TBS174_AGEND_HORAR.CO_ALU == idAlu)
                       select new saida
                       {
                           id = tbs390.ID_ATEND_AGEND,
                           regis = tbs390.NU_REGIS,
                           data = tbs390.DT_REALI
                       }).ToList();

            ddlReg.DataTextField = "concat";
            ddlReg.DataValueField = "id";
            ddlReg.DataSource = res;
            ddlReg.DataBind();

            ddlReg.Items.Insert(0, new ListItem("Selecione", ""));
        }

        public class saida
        {
            public DateTime data { get; set; }
            public string regis { get; set; }
            public int id { get; set; }
            public string concat
            {
                get
                {
                    return this.regis.ToString() + " - " + this.data.ToString("dd/MM/yy");
                }
            }
        }*/


        /// <summary>
        /// Carrega os dados do agendamento recebido como parâmetro
        /// </summary>
        /// <param name="ID_AGEND_HORAR"></param>
        //private void CarregaDadosAgendamento(int ID_AGEND_HORAR)
        //{
        //    var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
        //               where tbs174.ID_AGEND_HORAR == ID_AGEND_HORAR
        //               select new
        //               {
        //                   tbs174.DT_AGEND_HORAR,
        //                   tbs174.DE_ACAO_PLAN,
        //               }).FirstOrDefault();

        //    txtAcaoPlan.Text = res.DE_ACAO_PLAN;
        //    txtDtPrevisao.Text = res.DT_AGEND_HORAR.ToString();
        //}

        public class saidaItensAgenda
        {
            public string NO_PROCED { get; set; }
            public string NO_PRIORI { get; set; }
        }

        #endregion

        #region Funções de Campo

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
                        int idAgenda = int.Parse(((HiddenField)linha.FindControl("hidIdAgenda")).Value);
                        CarregaGridLog(idAgenda); //Carrega o log do item clicado

                        //Atribui as informações da linha clicada aos campos correspondentes na modal
                        txtNomePaciMODLOG.Text = ((Label)linha.FindControl("lblNomPaci")).Text;
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

        //protected void imgSituacaoHistorico_OnClick(object sender, EventArgs e)
        //{
        //    ImageButton atual = (ImageButton)sender;
        //    ImageButton img;
        //    if (grdHistoricoAgenda.Rows.Count != 0)
        //    {
        //        foreach (GridViewRow linha in grdHistoricoAgenda.Rows)
        //        {
        //            img = (ImageButton)linha.FindControl("imgSituacaoHistorico");
        //            if (img.ClientID == atual.ClientID)
        //            {
        //                string caminho = img.ImageUrl;
        //                int idAgenda = int.Parse(((HiddenField)linha.FindControl("hidIdAgenda")).Value);
        //                CarregaGridLog(idAgenda); //Carrega o log do item clicado

        //                foreach (GridViewRow i in grdPacientes.Rows)
        //                {
        //                    if (((CheckBox)i.FindControl("chkSelectPaciente")).Checked)
        //                    {
        //                        //Atribui as informações da linha clicada aos campos correspondentes na modal
        //                        txtNomePaciMODLOG.Text = ((Label)i.FindControl("lblNomPaci")).Text;
        //                        txtSexoMODLOG.Text = i.Cells[3].Text;
        //                        txtIdadeMODLOG.Text = i.Cells[4].Text;
        //                    }
        //                }

        //                ScriptManager.RegisterStartupScript(
        //                    this.Page,
        //                    this.GetType(),
        //                    "Acao",
        //                    "AbreModalLog();",
        //                    true
        //                );
        //            }
        //        }
        //    }
        //}

        protected void chkSelectPaciente_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            CheckBox chk;

            foreach (GridViewRow li in grdPacientes.Rows)
            {
                chk = (((CheckBox)li.FindControl("chkSelectPaciente")));
                if (chk.Checked)
                {
                    if (chk.ClientID == atual.ClientID)
                    {
                        int idAgenda = int.Parse(((HiddenField)li.FindControl("hidIdAgenda")).Value);
                        int coAlu = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(idAgenda).CO_ALU.Value;

                        CarregaItensAgendamento(idAgenda);
                        //CarregaRegistro(coAlu);
                        CarregarAnamnese(coAlu);
                    }
                    else
                    {
                        chk.Checked = false;
                    }
                }
                else
                {
                    chk.Checked = false;
                }
            }
        }

        protected void drpStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAgendamentos();
        }

        protected void ddlProficional_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAgendamentos();
        }

        //protected void chkSelectHistAge_OnCheckedChanged(object sender, EventArgs e)
        //{
        //    CheckBox atual = (CheckBox)sender;
        //    CheckBox chk;

        //    //foreach (GridViewRow li in grdHistoricoAgenda.Rows)
        //    //{
        //    //    chk = (((CheckBox)li.FindControl("chkSelectHistAge")));

        //    //    if (chk.ClientID == atual.ClientID)
        //    //    {
        //    //        if (chk.Checked)
        //    //        {
        //    //            int idAgenda = int.Parse(((HiddenField)li.FindControl("hidIdAgenda")).Value);

        //    //            hidIdAgenda.Value = idAgenda.ToString();
        //    //            CarregaDadosAgendamento(idAgenda);
        //    //        }
        //    //        else
        //    //            txtDtPrevisao.Text = txtAcaoPlan.Text = hidIdAgenda.Value = "";
        //    //    }
        //    //    else
        //    //        chk.Checked = false;
        //    //}
        //}

        //protected void btnAddForm_OnClick(object sender, EventArgs e)
        //{
        //    CriaNovaLinhaGridQuestionario();
        //}

        protected void lnkAddImg_OnClick(object sender, EventArgs e)
        {
            CriaNovaLinhaGridQuestionario();
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

        protected void lnkFinalizar_OnClick(object sender, EventArgs e)
        {
            Persistencias();
        }

        protected void imgPesqAgendamentos_OnClick(object sender, EventArgs e)
        {
            CarregaAgendamentos();
        }
        protected void btnAddForm_OnClick(object sender, EventArgs e)
        {
            CriaNovaLinhaGridQuestionario();
        }

        #endregion
    }
}