using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Data;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using System.IO;
using System.Data.Objects;

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8260_Atendimento._8262_AtendimentoEvolucao
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
                var data = DateTime.Now;

                if (LoginAuxili.FLA_USR_DEMO)
                    data = LoginAuxili.DATA_INICIO_USU_DEMO;

                //txtDtRealizado.Text = DateTime.Now.ToString();
                IniPeriAG.Text = FimPeriAG.Text = data.ToString();
                txtIniAgenda.Text = data.AddDays(-5).ToString();
                txtFimAgenda.Text = data.AddDays(15).ToString();

                CarregaProfissionais();
                CarregaAgendamentos();

                grdItensAgend.DataSource = null;
                grdItensAgend.DataBind();
                //if (grdHistoricoAgenda.DataSource == null)
                //{
                //    grdHistoricoAgenda.DataSource = null;
                //    grdHistoricoAgenda.DataBind();
                //}
                //else
                //{

                //}

                //AuxiliCarregamentos.CarregaClassificacoesFuncionais(ddlClassFuncio, true, LoginAuxili.CO_EMP, AuxiliCarregamentos.ETiposClassificacoes.agendamento);

                //preCarregaGridAudios();
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

                if (string.IsNullOrEmpty(hidIdAgenda.Value))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar a agenda que deseja atender!");
                    grdPacientes.Focus();
                    return;
                }

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

                #region Grava o atendimento

                #region Trata sequencial
                //Trata para gerar um Código do Encaminhamento
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

                string CodigoAtendimento = string.Format("AT{0}{1}{2}", ano, mes, seqcon);
                #endregion


                //tbs390.DE_OBSER = (!string.IsNullOrEmpty(txtObservacao.Text) ? txtObservacao.Text : null);
                //tbs390.DE_CONSI = (!string.IsNullOrEmpty(txtConsidAtendim.Text) ? txtConsidAtendim.Text : null);
                //tbs390.DE_ACAO_REALI = (!string.IsNullOrEmpty(txtAcaoReali.Text) ? txtAcaoReali.Text : null);

                if (grdHistoricoAgenda.Rows.Count != 0)
                {
                    foreach (GridViewRow linha in grdHistoricoAgenda.Rows)
                    {
                        //CheckBox chk;
                        //chk = (((CheckBox)linha.FindControl("chkSelectHistAge")));

                        //if (chk.Checked)

                        if (((CheckBox)linha.FindControl("chkSelectHistAge")).Checked)
                        {
                            int idAgenda = int.Parse(((HiddenField)linha.FindControl("hidIdAgenda")).Value);
                            int hidIdAtendi = int.Parse(((HiddenField)linha.FindControl("hidIdAtendi")).Value);
                            string txtAcaoPlan = (((TextBox)linha.FindControl("txtAcaoPlan")).Text);
                            string txtAcaoReali = (((TextBox)linha.FindControl("txtAcaoReali")).Text);
                            if (string.IsNullOrEmpty(txtAcaoReali))
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "A ação realizada e obrigatório!");
                                return;
                            }
                            
                            TBS390_ATEND_AGEND tbs390 = new TBS390_ATEND_AGEND();

                            if (hidIdAtendi != 0 )
                            {
                                tbs390 = TBS390_ATEND_AGEND.RetornaPelaChavePrimaria(hidIdAtendi);
                                tbs390.NU_REGIS = CodigoAtendimento;
                                tbs390.TBS174_AGEND_HORAR = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(idAgenda);
                                tbs390.DE_ACAO_REALI = (!string.IsNullOrEmpty(txtAcaoReali) ? txtAcaoReali : null);
                                TBS390_ATEND_AGEND.SaveOrUpdate(tbs390);

                                TBS174_AGEND_HORAR tbs174ob = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(idAgenda); //int.Parse(((HiddenField)linha.FindControl("hidIdAgenda")).Value)

                                //tbs174ob.ID_AGEND_HORAR = tbs174ob.ID_AGEND_HORAR;
                                tbs174ob.CO_SITUA_AGEND_HORAR = "R";
                                tbs174ob.FL_SITUA_ACAO = "R";
                                tbs174ob.CO_COL_SITUA = LoginAuxili.CO_COL;
                                tbs174ob.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                                tbs174ob.DT_SITUA_AGEND_HORAR = DateTime.Now;
                                tbs174ob.DE_ACAO_PLAN = txtAcaoPlan;

                                TBS174_AGEND_HORAR.SaveOrUpdate(tbs174ob, true);
                            }
                            else
                            {
                                tbs390.NU_REGIS = CodigoAtendimento;
                                tbs390.TBS174_AGEND_HORAR = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(idAgenda);
                                tbs390.DE_ACAO_REALI = (!string.IsNullOrEmpty(txtAcaoReali) ? txtAcaoReali : null);

                                //Dados de quem realizou o atendimento                          
                                tbs390.DT_REALI = tbs390.TBS174_AGEND_HORAR.DT_AGEND_HORAR;
                                tbs390.CO_COL_ATEND = LoginAuxili.CO_COL;
                                tbs390.CO_EMP_ATEND = LoginAuxili.CO_EMP;
                                tbs390.CO_EMP_COL_ATEND = LoginAuxili.CO_COL;

                                //Dados de quem cadastrou o atendimento
                                tbs390.DT_CADAS = DateTime.Now;
                                tbs390.CO_COL_CADAS = LoginAuxili.CO_COL;
                                tbs390.CO_EMP_COL_CADAS = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
                                tbs390.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                                tbs390.IP_CADAS = Request.UserHostAddress;

                                //Dados da situação do atendimento
                                tbs390.CO_SITUA = "A";
                                tbs390.DT_SITUA = DateTime.Now;
                                tbs390.CO_COL_SITUA = LoginAuxili.CO_COL;
                                tbs390.CO_EMP_COL_SITUA = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
                                tbs390.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                                tbs390.IP_SITUA = Request.UserHostAddress;

                                TBS390_ATEND_AGEND.SaveOrUpdate(tbs390);


                                TBS174_AGEND_HORAR tbs174ob = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(idAgenda); //int.Parse(((HiddenField)linha.FindControl("hidIdAgenda")).Value)

                                //tbs174ob.ID_AGEND_HORAR = tbs174ob.ID_AGEND_HORAR;
                                tbs174ob.CO_SITUA_AGEND_HORAR = "R";
                                tbs174ob.FL_SITUA_ACAO = "R";
                                tbs174ob.CO_COL_SITUA = LoginAuxili.CO_COL;
                                tbs174ob.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                                tbs174ob.DT_SITUA_AGEND_HORAR = DateTime.Now;
                                tbs174ob.DE_ACAO_PLAN = txtAcaoPlan;

                                TBS174_AGEND_HORAR.SaveOrUpdate(tbs174ob, true);
                            }
                        }
                    }
                }



                #endregion

                //#region Atualiza a agenda de ação

                ////Atualizo apenas que a ação foi realizada
                //TBS174_AGEND_HORAR tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(hidIdAgenda.Value));

                //tbs174.FL_SITUA_ACAO = "R";
                //tbs174.CO_COL_SITUA = LoginAuxili.CO_COL;
                //tbs174.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                //tbs174.DT_SITUA_AGEND_HORAR = DateTime.Now;

                //TBS174_AGEND_HORAR.SaveOrUpdate(tbs174);

                #endregion

                //#region Atualiza a agenda de atendimento

                ////Aqui atualizo que a agenda foi atendida
                //foreach (GridViewRow i in grdPacientes.Rows)
                //{
                //    if (((CheckBox)i.FindControl("chkSelectPaciente")).Checked)
                //    {

                //    }
                //}

                //#endregion

               

                //#region Armazena as avaliações

                ////Percorre a grid de Questinários para realizar as persistências
                //foreach (GridViewRow li in grdQuestionario.Rows)
                //{
                //    string Quest = (((DropDownList)li.FindControl("ddlQuest")).SelectedValue);
                //    //Salva apenas se tiver sido selecionada uma das opções
                //    if (!string.IsNullOrEmpty(Quest))
                //    {
                //        TBS391_QUEST_PESQU_ATEND tbs391 = new TBS391_QUEST_PESQU_ATEND();
                //        tbs391.TBS390_ATEND_AGEND = tbs390;
                //        tbs391.TB201_AVAL_MASTER = TB201_AVAL_MASTER.RetornaPelaChavePrimaria(int.Parse(Quest));

                //        //Dados do cadastro
                //        tbs391.DT_CADAS = DateTime.Now;
                //        tbs391.CO_COL_CADAS = LoginAuxili.CO_COL;
                //        tbs391.CO_EMP_COL_CADAS = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                //        tbs391.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                //        tbs391.IP_CADAS = Request.UserHostAddress;

                //        //Dados da situação
                //        tbs391.CO_SITUA = "A";
                //        tbs391.DT_SITUA = DateTime.Now;
                //        tbs391.CO_COL_SITUA = LoginAuxili.CO_COL;
                //        tbs391.CO_EMP_COL_SITUA = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                //        tbs391.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                //        tbs391.IP_SITUA = Request.UserHostAddress;

                //        TBS391_QUEST_PESQU_ATEND.SaveOrUpdate(tbs391, true); // Salva o questionário
                //    }
                //}

                //#endregion

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

                //#region Áudios

                ////foreach (GridViewRow i in grdAnexoAudio.Rows)
                ////{
                ////    FileUpload flp = (((FileUpload)i.FindControl("fupAudios")));
                ////    string titulo = (((TextBox)i.FindControl("txtTituloAudio")).Text);
                ////    string observa = (((TextBox)i.FindControl("txtObserAudio")).Text);
                ////    #region Salva o arquivo

                ////    var tbs392 = new TBS392_ANEXO_ATEND();
                ////    tbs392.TBS390_ATEND_AGEND = tbs390;
                ////    tbs392.NM_ANEXO = flp.FileName;
                ////    tbs392.ANEXO = ColetaConteudoArquivo(flp.PostedFile.InputStream);
                ////    tbs392.EX_ANEXO = Path.GetExtension(flp.FileName);
                ////    tbs392.NM_TITULO = (!string.IsNullOrEmpty(titulo) ? titulo : null);
                ////    tbs392.DE_OBSER = (!string.IsNullOrEmpty(observa) ? observa : null);

                ////    //Dados do cadastro
                ////    tbs392.DT_CADAS = DateTime.Now;
                ////    tbs392.CO_COL_CADAS = LoginAuxili.CO_COL;
                ////    tbs392.CO_EMP_COL_CADAS = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                ////    tbs392.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                ////    tbs392.IP_CADAS = Request.UserHostAddress;

                ////    //Dados da situação
                ////    tbs392.CO_SITUA = "A";
                ////    tbs392.DT_SITUA = DateTime.Now;
                ////    tbs392.CO_COL_SITUA = LoginAuxili.CO_COL;
                ////    tbs392.CO_EMP_COL_SITUA = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                ////    tbs392.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                ////    tbs392.IP_SITUA = Request.UserHostAddress;

                ////    TBS392_ANEXO_ATEND.SaveOrUpdate(tbs392, true);

                ////    #endregion
                ////}

                //#endregion

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

            //DataRow linha;
            //foreach (GridViewRow li in grdQuestionario.Rows)
            //{
            //    linha = dtV.NewRow();
            //    linha["QUESTIONARIO"] = (((DropDownList)li.FindControl("ddlQuest")).SelectedValue);
            //    dtV.Rows.Add(linha);
            //}

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

            //DataRow linha;
            //foreach (GridViewRow li in grdQuestionario.Rows)
            //{
            //    linha = dtV.NewRow();
            //    linha["QUESTIONARIO"] = (((DropDownList)li.FindControl("ddlQuest")).SelectedValue);
            //    dtV.Rows.Add(linha);
            //}

            //linha = dtV.NewRow();
            //linha["QUESTIONARIO"] = "";
            //dtV.Rows.Add(linha);

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

            //grdQuestionario.DataSource = dtV;
            //grdQuestionario.DataBind();

            //int aux = 0;
            //foreach (GridViewRow li in grdQuestionario.Rows)
            //{
            //    DropDownList ddlQuest;
            //    ddlQuest = (((DropDownList)li.FindControl("ddlQuest")));

            //    string quest;

            //    //Coleta os valores do dtv da modal popup
            //    quest = dtV.Rows[aux]["QUESTIONARIO"].ToString();

            //    //Seta os valores nos campos da modal popup
            //    CarregaQuestionarios(ddlQuest);
            //    ddlQuest.SelectedValue = quest;
            //    aux++;
            //}
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

            //grdQuestionario.DataSource = dtV;
            //grdQuestionario.DataBind();

            //foreach (GridViewRow li in grdQuestionario.Rows)
            //{
            //    DropDownList ddlQuest;

            //    ddlQuest = (DropDownList)li.FindControl("ddlQuest");
            //    CarregaQuestionarios(ddlQuest);
            //}
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
        private void CarregaAgendaPlanejamento(int CO_ALU)
        {
            DateTime dtIni = (!string.IsNullOrEmpty(txtIniAgenda.Text) ? DateTime.Parse(txtIniAgenda.Text) : DateTime.Now);
            DateTime dtFim = (!string.IsNullOrEmpty(txtFimAgenda.Text) ? DateTime.Parse(txtFimAgenda.Text) : DateTime.Now);
            //string classif = ddlClassFuncio.SelectedValue;
            //string situa = ddlSituaProced.SelectedValue;

            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                       //join tbs390 in TBS390_ATEND_AGEND.RetornaTodosRegistros() on tbs174.ID_AGEND_HORAR equals tbs390.TBS174_AGEND_HORAR.ID_AGEND_HORAR
                       join tbs390 in TBS390_ATEND_AGEND.RetornaTodosRegistros() on tbs174.ID_AGEND_HORAR equals tbs390.TBS174_AGEND_HORAR.ID_AGEND_HORAR into late
                       from IIlaten in late.DefaultIfEmpty()
                       where tbs174.CO_ALU == CO_ALU
                       && ((EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) >= EntityFunctions.TruncateTime(dtIni))
                       && (EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) <= EntityFunctions.TruncateTime(dtFim)))
                       //&& (classif != "0" ? tb03.CO_CLASS_PROFI == classif : 0 == 0)
                       //&& (situa != "0" ? tbs174.CO_SITUA_AGEND_HORAR == situa : 0 == 0)
                       && (((LoginAuxili.CLASSIFICACAO_USU_LOGADO != "M") && (LoginAuxili.FLA_PROFESSOR == "S")) ? tbs174.CO_COL == LoginAuxili.CO_COL : 0 == 0)
                       select new saidaHistoricoAgenda
                       {
                           horaAgenda = tbs174.HR_AGEND_HORAR,
                           dtAgenda = tbs174.DT_AGEND_HORAR,
                           dtAgenda_Atend = (IIlaten != null ? IIlaten.DT_REALI : (DateTime?)null),
                           DE_ACAO = (IIlaten != null ? (!string.IsNullOrEmpty(IIlaten.DE_ACAO_REALI) ? IIlaten.DE_ACAO_REALI : " - ") : (!string.IsNullOrEmpty(tbs174.DE_ACAO_PLAN) ? tbs174.DE_ACAO_PLAN : " - ")),
                           CLASS_FUNCI_R = tb03.CO_CLASS_PROFI,
                           ID_AGENDA = tbs174.ID_AGEND_HORAR,
                           ID_ATEND_AGEND = IIlaten.ID_ATEND_AGEND == null ? 0 : IIlaten.ID_ATEND_AGEND == null ? 0 : IIlaten.ID_ATEND_AGEND,
                            txtAcaoReali = IIlaten.DE_ACAO_REALI,
                           PROFISSIONAL = tb03.NO_APEL_COL,
                           Situacao = tbs174.CO_SITUA_AGEND_HORAR,
                           agendaConfirm = tbs174.FL_CONF_AGEND,
                           agendaEncamin = tbs174.FL_AGEND_ENCAM,
                           faltaJustif = tbs174.FL_JUSTI_CANCE,
                           podeClicar = (tbs174.FL_SITUA_ACAO != null ? (tbs174.FL_SITUA_ACAO != "R" ? true : false) : true),
                       }).ToList();
            res = res.OrderBy(w => w.dtAgenda).ThenBy(w => w.hora).ToList();

            grdHistoricoAgenda.DataSource = res.DistinctBy(q => q.ID_AGENDA);
            grdHistoricoAgenda.DataBind();
            //grdPacientes.DataSource = res;
            //grdPacientes.DataBind();


        }

        public class saidaHistoricoAgenda
        {

            public string txtAcaoReali { get; set; }
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
            public int ID_ATEND_AGEND { get; set; }
            
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
            public string PROFISSIONAL { get; set; }
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

        /// <summary>
        /// Carrega a lista de agendamentos
        /// </summary>
        private void CarregaAgendamentos()
        {
            int idCOL_COL = !String.IsNullOrEmpty(ddlProficional.SelectedValue) ? Convert.ToInt32(ddlProficional.SelectedValue) : 0;

            DateTime dtIni = (!string.IsNullOrEmpty(IniPeriAG.Text) ? DateTime.Parse(IniPeriAG.Text) : DateTime.Now);
            DateTime dtFim = (!string.IsNullOrEmpty(FimPeriAG.Text) ? DateTime.Parse(FimPeriAG.Text) : DateTime.Now);

            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                       where (tbs174.DT_AGEND_HORAR >= dtIni && tbs174.DT_AGEND_HORAR <= dtFim)
                       && (ddlSituacao.SelectedValue != "0" ? tbs174.CO_SITUA_AGEND_HORAR == ddlSituacao.SelectedValue : 0 == 0)
                       && (((LoginAuxili.CLASSIFICACAO_USU_LOGADO != "M") && (LoginAuxili.FLA_PROFESSOR == "S")) ? tbs174.CO_COL == LoginAuxili.CO_COL : 0 == 0)
                       && (!string.IsNullOrEmpty(txtNomePacPesqAgendAtend.Text) ? tb07.NO_ALU.Contains(txtNomePacPesqAgendAtend.Text) : 0 == 0)
                       && (idCOL_COL != 0 ? tbs174.CO_COL == idCOL_COL : 0 == 0)
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
                           podeClicar = (tbs174.FL_AGEND_ENCAM == "S" && tbs174.CO_SITUA_AGEND_HORAR != "R" ? true : false),

                           Situacao = tbs174.CO_SITUA_AGEND_HORAR,
                           agendaConfirm = tbs174.FL_CONF_AGEND,
                           agendaEncamin = tbs174.FL_AGEND_ENCAM,
                           faltaJustif = tbs174.FL_JUSTI_CANCE,

                       }).ToList();

            res = res.OrderByDescending(w => w.DT).ThenBy(w => w.hora).ThenBy(w => w.PACIENTE_R).ToList();

            grdPacientes.DataSource = res;
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

        /// <summary>
        /// Carrega os dados do agendamento recebido como parâmetro
        /// </summary>
        /// <param name="ID_AGEND_HORAR"></param>
        private void CarregaDadosAgendamento(int ID_AGEND_HORAR)
        {
            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       where tbs174.ID_AGEND_HORAR == ID_AGEND_HORAR
                       select new
                       {
                           tbs174.DT_AGEND_HORAR,
                           tbs174.DE_ACAO_PLAN,
                       }).FirstOrDefault();

            //txtAcaoPlan.Text = res.DE_ACAO_PLAN;
            //txtDtPrevisao.Text = res.DT_AGEND_HORAR.ToString();
        }

        public class saidaItensAgenda
        {
            public string NO_PROCED { get; set; }
            public string NO_PRIORI { get; set; }
        }

        private void CarregaProfissionais()
        {
            AuxiliCarregamentos.CarregaProfissionaisSaude(ddlProficional, 0, true, "0", true);

            ddlProficional.Enabled = false;
            
            if (ddlProficional.Items.FindByValue(LoginAuxili.CO_COL.ToString()) != null)
                ddlProficional.SelectedValue = LoginAuxili.CO_COL.ToString();

            //Se o logado não for master, e for profissional de saúde, mostra e seleciona ele de padrão
            if ((LoginAuxili.CLASSIFICACAO_USU_LOGADO == "M") && (LoginAuxili.FLA_PROFESSOR == "S"))
                ddlProficional.Enabled = true;
        }

        //#endregion

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

        protected void imgSituacaoHistorico_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            if (grdHistoricoAgenda.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdHistoricoAgenda.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgSituacaoHistorico");
                    if (img.ClientID == atual.ClientID)
                    {
                        string caminho = img.ImageUrl;
                        int idAgenda = int.Parse(((HiddenField)linha.FindControl("hidIdAgenda")).Value);
                        CarregaGridLog(idAgenda); //Carrega o log do item clicado

                        foreach (GridViewRow i in grdPacientes.Rows)
                        {
                            if (((CheckBox)i.FindControl("chkSelectPaciente")).Checked)
                            {
                                //Atribui as informações da linha clicada aos campos correspondentes na modal
                                txtNomePaciMODLOG.Text = ((Label)i.FindControl("lblNomPaci")).Text;
                                txtSexoMODLOG.Text = i.Cells[3].Text;
                                txtIdadeMODLOG.Text = i.Cells[4].Text;
                            }
                        }

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

        protected void chkSelectPaciente_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            CheckBox chk;

            grdItensAgend.DataSource = grdHistoricoAgenda.DataSource = null;
            grdItensAgend.DataBind(); grdHistoricoAgenda.DataBind();

            foreach (GridViewRow li in grdPacientes.Rows)
            {
                chk = (((CheckBox)li.FindControl("chkSelectPaciente")));

                if (chk.ClientID == atual.ClientID)
                {
                    if (chk.Checked)
                    {
                        int idAgenda = int.Parse(((HiddenField)li.FindControl("hidIdAgenda")).Value);
                        int coAlu = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(idAgenda).CO_ALU.Value;
                        CarregaItensAgendamento(idAgenda);
                        CarregaAgendaPlanejamento(coAlu);

                        //Percorre a grid de histórico de atendimento, e ao achar a 
                        foreach (GridViewRow i in grdHistoricoAgenda.Rows)
                        {
                            string idAgendaHist = ((HiddenField)i.FindControl("hidIdAgenda")).Value;
                            if (idAgendaHist == idAgenda.ToString())
                            {
                                //Se a ação planejada para esta data já tiver sido executada, retorna mensagem
                                if (((CheckBox)i.FindControl("chkSelectHistAge")).Enabled == false)
                                {
                                    string dthr = i.Cells[4].Text;
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "A ação planejada para esta agenda foi realizada na data e hora: " + dthr + ". Favor escolher outra ação!");
                                    return;
                                }

                                CheckBox chkHistAg = (((CheckBox)i.FindControl("chkSelectHistAge")));
                                chkHistAg.Checked = true;

                                hidIdAgenda.Value = idAgendaHist.ToString();
                                CarregaDadosAgendamento(idAgenda);
                            }
                        }
                    }
                }
                else
                    chk.Checked = false;
            }
        }

        protected void chkSelectHistAge_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            CheckBox chk;

            foreach (GridViewRow li in grdHistoricoAgenda.Rows)
            {
                chk = (((CheckBox)li.FindControl("chkSelectHistAge")));

                if (chk.ClientID == atual.ClientID)
                {
                    if (chk.Checked)
                    {
                        int idAgenda = int.Parse(((HiddenField)li.FindControl("hidIdAgenda")).Value);

                        hidIdAgenda.Value = idAgenda.ToString();
                        CarregaDadosAgendamento(idAgenda);
                    }
                    //else
                    //txtDtPrevisao.Text = txtAcaoPlan.Text = hidIdAgenda.Value = "";
                }
                else
                    chk.Checked = false;
            }
        }

        protected void btnAddForm_OnClick(object sender, EventArgs e)
        {
            CriaNovaLinhaGridQuestionario();
        }

        protected void lnkAddImg_OnClick(object sender, EventArgs e)
        {
            CriaNovaLinhaGridQuestionario();
        }

        protected void imgExc_OnClick(object sender, EventArgs e)
        {
            //ImageButton atual = (ImageButton)sender;
            //ImageButton img;
            //int aux = 0;
            //if (grdQuestionario.Rows.Count != 0)
            //{
            //    foreach (GridViewRow linha in grdQuestionario.Rows)
            //    {
            //        img = (ImageButton)linha.FindControl("imgExc");

            //        if (img.ClientID == atual.ClientID)
            //            aux = linha.RowIndex;
            //    }
            //}
            //ExcluiItemGrid(aux);
        }

        protected void lnkFinalizar_OnClick(object sender, EventArgs e)
        {

        }

        protected void imgPesqAgendamentos_OnClick(object sender, EventArgs e)
        {
            CarregaAgendamentos();
        }

        protected void drpStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAgendamentos();
        }

        protected void ddlProficional_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAgendamentos();
        }

        #endregion

        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            Persistencias();
        }

        protected void ConsultaDemostrativo_Click(object sender, ImageClickEventArgs e)
        {
            CheckBox chk;
            foreach (GridViewRow li in grdPacientes.Rows)
            {
                chk = (((CheckBox)li.FindControl("chkSelectPaciente")));

                  if (chk.Checked == true)
                    {
                        int idAgenda = int.Parse(((HiddenField)li.FindControl("hidIdAgenda")).Value);
                        int coAlu = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(idAgenda).CO_ALU.Value;
                        //CarregaItensAgendamento(idAgenda);
                        CarregaAgendaPlanejamento(coAlu);

                        //Percorre a grid de histórico de atendimento, e ao achar a 
                        //foreach (GridViewRow i in grdHistoricoAgenda.Rows)
                        //{
                        //    string idAgendaHist = ((HiddenField)i.FindControl("hidIdAgenda")).Value;
                        //    if (idAgendaHist == idAgenda.ToString())
                        //    {
                        //        //Se a ação planejada para esta data já tiver sido executada, retorna mensagem
                        //        if (((CheckBox)i.FindControl("chkSelectHistAge")).Enabled == false)
                        //        {
                        //            string dthr = i.Cells[4].Text;
                        //            AuxiliPagina.EnvioMensagemErro(this.Page, "A ação planejada para esta agenda foi realizada na data e hora: " + dthr + ". Favor escolher outra ação!");
                        //            return;
                        //        }

                        //        CheckBox chkHistAg = (((CheckBox)i.FindControl("chkSelectHistAge")));
                        //        chkHistAg.Checked = true;

                        //        hidIdAgenda.Value = idAgendaHist.ToString();
                        //        CarregaDadosAgendamento(idAgenda);
                        //    }
                        //}
                    }
                    else
                    {
                        //grdItensAgend.DataSource = grdHistoricoAgenda.DataSource = null;
                        //grdItensAgend.DataBind(); grdHistoricoAgenda.DataBind();


                    }

            }
        }
    }
}