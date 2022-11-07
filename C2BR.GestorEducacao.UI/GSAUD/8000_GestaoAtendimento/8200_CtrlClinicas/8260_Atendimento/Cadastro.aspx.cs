//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: Registrar atendimento das consultas da tbs174, devidamente cruzando informações com os planejamentos   
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR  |   O.S.  | DESCRIÇÃO RESUMIDA
// ---------+-----------------------+---------+--------------------------------
// 10/12/14 | Maxwell Almeida       |         | Criação da funcionalidade para Cadastro de Operadoras
// ---------+-----------------------+---------+--------------------------------
// 27/04/16 | Filipe Rodrigues      | FSP0035 | Alteração na exibição da lista de profissionais para não aparecer caso não seja master

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
using C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8200_CtrlExames;
using C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3400_CtrlAtendimentoUsuario;

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8260_Atendimento
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

                IniPeriAG.Text = FimPeriAG.Text = txtDtRealizado.Text = data.ToString();
                txtIniAgenda.Text = data.AddDays(-5).ToString();
                txtFimAgenda.Text = data.AddDays(15).ToString();

                CarregaAgendamentos();

                CarregaProfissionais();

                AuxiliCarregamentos.CarregaClassificacoesFuncionais(ddlClassFuncio, true, LoginAuxili.CO_EMP, AuxiliCarregamentos.ETiposClassificacoes.agendamento);

                preCarregaGridAudios();
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

                if (!string.IsNullOrEmpty(hidIdAtendimento.Value))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Atendimento já finalizado!");
                    return;
                }

                if (string.IsNullOrEmpty(hidIdAgenda.Value))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar a agenda que deseja atender!");
                    grdPacientes.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtDtRealizado.Text))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "A data de realização do atendimento é obrigatória!");
                    txtDtRealizado.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(ddlProfiResp.SelectedValue))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o profissional que realizou o atendimento!");
                    ddlProfiResp.Focus();
                    return;
                }

                #endregion

                #region Persistências

                ExecutarFuncaoPadrao("PararCronometro();");

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
                var tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(hidIdAgenda.Value));
                var tb07 = TB07_ALUNO.RetornaPeloCoAlu(tbs174.CO_ALU.Value);

                TBS390_ATEND_AGEND tbs390 = new TBS390_ATEND_AGEND();
                tbs390.TBS174_AGEND_HORAR = tbs174;
                tbs390.TB07_ALUNO = tb07;
                tbs390.NU_REGIS = CodigoAtendimento;
                tbs390.DE_OBSER = (!string.IsNullOrEmpty(txtObservacao.Text) ? txtObservacao.Text : null);
                tbs390.DE_CONSI = (!string.IsNullOrEmpty(txtConsidAtendim.Text) ? txtConsidAtendim.Text : null);
                tbs390.DE_ACAO_REALI = (!string.IsNullOrEmpty(txtAcaoReali.Text) ? txtAcaoReali.Text : null);

                //Dados de quem realizou o atendimento
                tbs390.DT_REALI = DateTime.Parse(txtDtRealizado.Text).Add(TimeSpan.Parse(DateTime.Now.ToShortTimeString()));
                tbs390.CO_COL_ATEND = int.Parse(ddlProfiResp.SelectedValue);
                tbs390.CO_EMP_ATEND = LoginAuxili.CO_EMP;
                tbs390.CO_EMP_COL_ATEND = TB03_COLABOR.RetornaPeloCoCol(int.Parse(ddlProfiResp.SelectedValue)).CO_COL;

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

                if (LoginAuxili.CO_DEPTO != 0)
                    tbs390.TB14_DEPTO = TB14_DEPTO.RetornaPelaChavePrimaria(LoginAuxili.CO_DEPTO);

                TBS390_ATEND_AGEND.SaveOrUpdate(tbs390);
                hidIdAtendimento.Value = tbs390.ID_ATEND_AGEND.ToString();
                #endregion

                #region Atualiza a agenda de ação

                //Atualizo apenas que a ação foi realizada

                tbs174.DE_ACAO_PLAN = txtAcaoPlan.Text;
                tbs174.FL_SITUA_ACAO = "R";
                tbs174.CO_COL_SITUA = LoginAuxili.CO_COL;
                tbs174.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                tbs174.DT_SITUA_AGEND_HORAR = DateTime.Now;

                TBS174_AGEND_HORAR.SaveOrUpdate(tbs174, true);

                #endregion

                #region Atualiza a agenda de atendimento
                var atend = 0;
                //Aqui atualizo que a agenda foi atendida
                foreach (GridViewRow i in grdPacientes.Rows)
                {
                    if (((CheckBox)i.FindControl("chkSelectPaciente")).Checked)
                    {
                        atend = int.Parse(((HiddenField)i.FindControl("hidIdAgenda")).Value);
                        TBS174_AGEND_HORAR tbs174ob = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(atend);

                        tbs174ob.CO_SITUA_AGEND_HORAR = "R";
                        tbs174ob.CO_COL_SITUA = LoginAuxili.CO_COL;
                        tbs174ob.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                        tbs174ob.DT_SITUA_AGEND_HORAR = DateTime.Now;

                        if (!String.IsNullOrEmpty(hidHoras.Value) && !String.IsNullOrEmpty(hidMinutos.Value))
                            tbs174ob.HR_DURACAO_ATEND = int.Parse(hidHoras.Value).ToString("D2") + ":" + int.Parse(hidMinutos.Value).ToString("D2");

                        TBS174_AGEND_HORAR.SaveOrUpdate(tbs174ob, true);
                    }
                }

                #endregion
                
                #region Armazena as avaliações

                //Percorre a grid de Questinários para realizar as persistências
                foreach (GridViewRow li in grdQuestionario.Rows)
                {
                    string Quest = (((DropDownList)li.FindControl("ddlQuest")).SelectedValue);
                    //Salva apenas se tiver sido selecionada uma das opções
                    if (!string.IsNullOrEmpty(Quest))
                    {
                        TBS391_QUEST_PESQU_ATEND tbs391 = new TBS391_QUEST_PESQU_ATEND();
                        tbs391.TBS390_ATEND_AGEND = tbs390;
                        tbs391.TB201_AVAL_MASTER = TB201_AVAL_MASTER.RetornaPelaChavePrimaria(int.Parse(Quest));

                        //Dados do cadastro
                        tbs391.DT_CADAS = DateTime.Now;
                        tbs391.CO_COL_CADAS = LoginAuxili.CO_COL;
                        tbs391.CO_EMP_COL_CADAS = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                        tbs391.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                        tbs391.IP_CADAS = Request.UserHostAddress;

                        //Dados da situação
                        tbs391.CO_SITUA = "A";
                        tbs391.DT_SITUA = DateTime.Now;
                        tbs391.CO_COL_SITUA = LoginAuxili.CO_COL;
                        tbs391.CO_EMP_COL_SITUA = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                        tbs391.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                        tbs391.IP_SITUA = Request.UserHostAddress;

                        TBS391_QUEST_PESQU_ATEND.SaveOrUpdate(tbs391, true); // Salva o questionário
                    }
                }

                #endregion

                #endregion

                #region Imagens

                HttpPostedFile postFile;
                string imageName = string.Empty;
                byte[] path;
                string[] keys;
                try
                {
                    string contetType = string.Empty;
                    byte[] imgContent = null;
                    string[] PhotoTitle;
                    string PhotoTitlenme;
                    HttpFileCollection files = HttpContext.Current.Request.Files;
                    keys = files.AllKeys;
                    for (int i = 0; i < files.Count; i++)
                    {
                        postFile = files[i];
                        if (postFile.ContentLength > 0)
                        {
                            contetType = postFile.ContentType;
                            path = ColetaConteudoArquivo(postFile.InputStream);
                            imageName = Path.GetFileName(postFile.FileName);
                            PhotoTitle = imageName.Split('.');
                            PhotoTitlenme = PhotoTitle[0];

                            #region Salva o arquivo

                            var tbs392 = new TBS392_ANEXO_ATEND();
                            tbs392.TBS390_ATEND_AGEND = tbs390;
                            tbs392.NM_ANEXO = PhotoTitlenme;
                            tbs392.ANEXO = path;
                            tbs392.EX_ANEXO = Path.GetExtension(files[i].FileName);

                            //Dados do cadastro
                            tbs392.DT_CADAS = DateTime.Now;
                            tbs392.CO_COL_CADAS = LoginAuxili.CO_COL;
                            tbs392.CO_EMP_COL_CADAS = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                            tbs392.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                            tbs392.IP_CADAS = Request.UserHostAddress;

                            //Dados da situação
                            tbs392.CO_SITUA = "A";
                            tbs392.DT_SITUA = DateTime.Now;
                            tbs392.CO_COL_SITUA = LoginAuxili.CO_COL;
                            tbs392.CO_EMP_COL_SITUA = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                            tbs392.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                            tbs392.IP_SITUA = Request.UserHostAddress;

                            TBS392_ANEXO_ATEND.SaveOrUpdate(tbs392, true);

                            #endregion
                        }
                    }
                }
                catch (Exception e)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Ocorreu um erro ao salvar os anexos! Contacte o suporte");
                    return;
                }

                #endregion

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

                RecarregarGrids(atend, tbs174.CO_ALU.Value);

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
        protected void carregaGridQuestionario(int idAtendAgend)
        {
            DataTable dtV = CriarColunasELinhasGridQuestionario();

            if (idAtendAgend != 0)
            {
                var res = (from tbs391 in TBS391_QUEST_PESQU_ATEND.RetornaTodosRegistros()
                           where tbs391.TBS390_ATEND_AGEND.ID_ATEND_AGEND == idAtendAgend
                           select new
                           {
                               tbs391.TB201_AVAL_MASTER.NU_AVAL_MASTER
                           }).ToList();

                foreach (var i in res)
                {
                    var linha = dtV.NewRow();
                    linha["QUESTIONARIO"] = i.NU_AVAL_MASTER;
                    dtV.Rows.Add(linha);
                }
            }

            HttpContext.Current.Session.Add("GridSolic_AV", dtV);

            carregaGridNovaComContexto();
        }

        /// <summary>
        /// Exclui item da grid recebendo como parâmetro o index correspondente
        /// </summary>
        protected void ExcluiItemGridQuestionario(int Index)
        {
            DataTable dtV = CriarColunasELinhasGridQuestionario();

            dtV.Rows.RemoveAt(Index); // Exclui o item de index correspondente
            Session["GridSolic_AV"] = dtV;

            carregaGridNovaComContexto();
        }

        /// <summary>
        /// Cria uma nova linha na forma de pagamento cheque
        /// </summary>
        protected void CriaNovaLinhaGridQuestionario()
        {
            DataTable dtV = CriarColunasELinhasGridQuestionario();

            var linha = dtV.NewRow();
            linha["QUESTIONARIO"] = "";
            dtV.Rows.Add(linha);

            Session["GridSolic_AV"] = dtV;

            carregaGridNovaComContexto();
        }

        private DataTable CriarColunasELinhasGridQuestionario()
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
        protected void preCarregaGridAudios()
        {
            DataTable dtV = new DataTable();
            DataColumn dcATM;

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "ARQUIVO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "TITULO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "OBSERVACAO";
            dtV.Columns.Add(dcATM);

            int i = 1;
            DataRow linha;
            while (i < 2)
            {
                linha = dtV.NewRow();
                linha["ARQUIVO"] = "";
                linha["TITULO"] = "";
                linha["OBSERVACAO"] = "";
                dtV.Rows.Add(linha);
                i++;
            }

            grdAnexoAudio.DataSource = dtV;
            grdAnexoAudio.DataBind();
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
        /// Responsável por carregar os profissionais, e selecionar e bloquear o logado caso não seja master
        /// </summary>
        private void CarregaProfissionais()
        {
            AuxiliCarregamentos.CarregaProfissionaisSaude(ddlProfiResp, 0, false, "0", true);

            //Se o logado não for master, e for profissional de saúde, mostra e seleciona ele de padrão
            if ((LoginAuxili.CLASSIFICACAO_USU_LOGADO != "M"))
                ddlProfiResp.Enabled = false;

            if ((LoginAuxili.FLA_PROFESSOR == "S"))
                ddlProfiResp.SelectedValue = LoginAuxili.CO_COL.ToString();
        }

        /// <summary>
        /// Carrega a lista de agendamentos do paciente recebido como parâmetro 
        /// </summary>
        /// <param name="CO_ALU"></param>
        private void CarregaAgendaPlanejamento(int CO_ALU)
        {
            DateTime dtIni = (!string.IsNullOrEmpty(txtIniAgenda.Text) ? DateTime.Parse(txtIniAgenda.Text) : DateTime.Now);
            DateTime dtFim = (!string.IsNullOrEmpty(txtFimAgenda.Text) ? DateTime.Parse(txtFimAgenda.Text) : DateTime.Now);
            string classif = ddlClassFuncio.SelectedValue;
            string situa = ddlSituaProced.SelectedValue;

            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                       join tbs390 in TBS390_ATEND_AGEND.RetornaTodosRegistros() on tbs174.ID_AGEND_HORAR equals tbs390.TBS174_AGEND_HORAR.ID_AGEND_HORAR into late
                       from IIlaten in late.DefaultIfEmpty()
                       where tbs174.CO_ALU == CO_ALU
                       && ((EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) >= EntityFunctions.TruncateTime(dtIni))
                       && (EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) <= EntityFunctions.TruncateTime(dtFim)))
                       && (classif != "0" ? tb03.CO_CLASS_PROFI == classif : 0 == 0)
                       && (situa != "0" ? tbs174.CO_SITUA_AGEND_HORAR == situa : 0 == 0)
                       && (((LoginAuxili.CLASSIFICACAO_USU_LOGADO != "M") && (LoginAuxili.FLA_PROFESSOR == "S")) ? tbs174.CO_COL == LoginAuxili.CO_COL : 0 == 0)
                       && (!String.IsNullOrEmpty(tbs174.FL_JUSTI_CANCE) ? tbs174.FL_JUSTI_CANCE != "M" : "" == "")
                       select new saidaHistoricoAgenda
                       {
                           horaAgenda = tbs174.HR_AGEND_HORAR,
                           dtAgenda = tbs174.DT_AGEND_HORAR,
                           dtAgenda_Atend = (IIlaten != null ? IIlaten.DT_REALI : (DateTime?)null),
                           DE_ACAO = (IIlaten != null ? (!string.IsNullOrEmpty(IIlaten.DE_ACAO_REALI) ? IIlaten.DE_ACAO_REALI : " - ") : (!string.IsNullOrEmpty(tbs174.DE_ACAO_PLAN) ? tbs174.DE_ACAO_PLAN : " - ")),
                           CLASS_FUNCI_R = tb03.CO_CLASS_PROFI,
                           ID_AGENDA = tbs174.ID_AGEND_HORAR,

                           Situacao = tbs174.CO_SITUA_AGEND_HORAR,
                           agendaConfirm = tbs174.FL_CONF_AGEND,
                           agendaEncamin = tbs174.FL_AGEND_ENCAM,
                           faltaJustif = tbs174.FL_JUSTI_CANCE,
                           podeClicar = true
                           //podeClicar = (tbs174.FL_SITUA_ACAO != null ? (tbs174.FL_SITUA_ACAO != "R" ? true : false) : true),
                       }).ToList();

            res = res.OrderBy(w => w.dtAgenda).ThenBy(w => w.hora).ToList();

            grdHistoricoAgenda.DataSource = res;
            grdHistoricoAgenda.DataBind();
        }

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
                    return AuxiliFormatoExibicao.RetornarUrlImagemAgend(Situacao, agendaEncamin, agendaConfirm, faltaJustif);
                }
            }
            public string imagem_TIP
            {
                get
                {
                    return AuxiliFormatoExibicao.RetornarToolTipImagemAgend(this.imagem_URL);
                }
            }
            public string imagem_URL_ACAO
            {
                get
                {
                    //Se puder clicar, então está em aberto, e mostra mão negativa, do contrário, mostra positiva
                    return (!this.dtAgenda_Atend.HasValue ? "/Library/IMG/PGS_IC_Negativo.png" : "/Library/IMG/PGS_IC_Positivo.png");
                }
            }
        }

        /// <summary>
        /// Carrega a lista de agendamentos
        /// </summary>
        private void CarregaAgendamentos()
        {
            DateTime dtIni = (!string.IsNullOrEmpty(IniPeriAG.Text) ? DateTime.Parse(IniPeriAG.Text) : DateTime.Now);
            DateTime dtFim = (!string.IsNullOrEmpty(FimPeriAG.Text) ? DateTime.Parse(FimPeriAG.Text) : DateTime.Now);

            txtIniAgenda.Text = dtIni.AddDays(-5).ToString();
            txtFimAgenda.Text = dtFim.AddDays(15).ToString();

            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                       where ((EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) >= EntityFunctions.TruncateTime(dtIni))
                       && (EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) <= EntityFunctions.TruncateTime(dtFim)))
                       && (ddlSituacao.SelectedValue != "0" ? tbs174.CO_SITUA_AGEND_HORAR == ddlSituacao.SelectedValue : 0 == 0)
                       && (((LoginAuxili.CLASSIFICACAO_USU_LOGADO != "M") && (LoginAuxili.FLA_PROFESSOR == "S")) ? tbs174.CO_COL == LoginAuxili.CO_COL : 0 == 0)
                       && (!string.IsNullOrEmpty(txtNomePacPesqAgendAtend.Text) ? tb07.NO_ALU.Contains(txtNomePacPesqAgendAtend.Text) : 0 == 0)
                       && (!String.IsNullOrEmpty(tbs174.FL_JUSTI_CANCE) ? tbs174.FL_JUSTI_CANCE != "M" : "" == "")
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
                           podeClicar = true,
                           //podeClicar = (tbs174.FL_AGEND_ENCAM == "S" && tbs174.CO_SITUA_AGEND_HORAR != "R" ? true : false),

                           Situacao = tbs174.CO_SITUA_AGEND_HORAR,
                           agendaConfirm = tbs174.FL_CONF_AGEND,
                           agendaEncamin = tbs174.FL_AGEND_ENCAM,
                           faltaJustif = tbs174.FL_JUSTI_CANCE,

                           Cortesia = tbs174.FL_CORTESIA,
                           Contratacao = tbs174.TB250_OPERA != null ? tbs174.TB250_OPERA.NM_SIGLA_OPER : "",
                           ContratParticular = tbs174.TB250_OPERA != null ? (tbs174.TB250_OPERA.FL_INSTI_OPERA != null && tbs174.TB250_OPERA.FL_INSTI_OPERA == "S") : false
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
                    return AuxiliFormatoExibicao.RetornarUrlImagemAgend(Situacao, agendaEncamin, agendaConfirm, faltaJustif);
                }
            }
            public string imagem_TIP
            {
                get
                {
                    return AuxiliFormatoExibicao.RetornarToolTipImagemAgend(this.imagem_URL);
                }
            }

            public string Cortesia { get; set; }
            public string Contratacao { get; set; }
            public bool ContratParticular { get; set; }

            public string tpContr_URL
            {
                get
                {
                    return AuxiliFormatoExibicao.RetornarURLAgendContrat(Contratacao, Cortesia, ContratParticular);
                }
            }

            public string tpContr_TIP
            {
                get
                {
                    var txt = AuxiliFormatoExibicao.RetornarTextoAgendContrat(Contratacao, Cortesia, ContratParticular);
                    var tip = AuxiliFormatoExibicao.RetornarToolTipAgendContrat(Contratacao, Cortesia, ContratParticular);

                    return txt + " - " + tip;
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

            txtAcaoPlan.Text = res.DE_ACAO_PLAN;
            txtDtPrevisao.Text = res.DT_AGEND_HORAR.ToString();

            hidIdAtendimento.Value =
            txtConsidAtendim.Text =
            txtObservacao.Text =
            txtAcaoReali.Text = "";

            if (grdQuestionario.Rows.Count != 0)
            {
                grdQuestionario.DataSource = null;
                grdQuestionario.DataBind();
            }

            var Atend = TBS390_ATEND_AGEND.RetornaTodosRegistros().Where(a => a.TBS174_AGEND_HORAR.ID_AGEND_HORAR == ID_AGEND_HORAR).FirstOrDefault();

            if (Atend != null)
            {
                hidIdAtendimento.Value = Atend.ID_ATEND_AGEND.ToString();
                txtConsidAtendim.Text = Atend.DE_CONSI;

                txtObservacao.Text = Atend.DE_OBSER;
                txtAcaoReali.Text = Atend.DE_ACAO_REALI;
                if (ddlProfiResp.Items.FindByValue(Atend.CO_COL_ATEND.ToString()) != null)
                    ddlProfiResp.SelectedValue = Atend.CO_COL_ATEND.ToString();

                carregaGridQuestionario(Atend.ID_ATEND_AGEND);
            }
        }

        public class saidaItensAgenda
        {
            public string NO_PROCED { get; set; }
            public string NO_PRIORI { get; set; }
        }

        private void CarregarPacientesGuia()
        {
            if (String.IsNullOrEmpty(txtDtGuia.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Informe a data de comparecimento!");
                return;
            }

            DateTime dtAtdo = DateTime.Parse(txtDtGuia.Text);

            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                       where tbs174.DT_AGEND_HORAR == dtAtdo
                       && (LoginAuxili.CLASSIFICACAO_USU_LOGADO != "M" && LoginAuxili.FLA_PROFESSOR == "S" ? tbs174.CO_COL == LoginAuxili.CO_COL : 0 == 0)
                       select new { tb07.NO_ALU, tb07.CO_ALU }).OrderBy(w => w.NO_ALU).DistinctBy(p => p.CO_ALU).ToList();

            if (res != null && res.Count > 0)
            {
                drpPacienteGuia.DataTextField = "NO_ALU";
                drpPacienteGuia.DataValueField = "CO_ALU";
                drpPacienteGuia.DataSource = res;
                drpPacienteGuia.DataBind();
            }

            drpPacienteGuia.Items.Insert(0, new ListItem("EM BRANCO", "0"));

            AbreModalPadrao("AbreModalGuiaPlano();");
        }

        private void CarregarPacientesDisponiveisAtestado()
        {
            if (String.IsNullOrEmpty(txtDtAtestado.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Informe a data de comparecimento!");
                return;
            }

            DateTime dtAtdo = DateTime.Parse(txtDtAtestado.Text);

            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                       where tbs174.DT_AGEND_HORAR == dtAtdo
                       && tbs174.CO_SITUA_AGEND_HORAR == "R"
                       && (LoginAuxili.CLASSIFICACAO_USU_LOGADO != "M" && LoginAuxili.FLA_PROFESSOR == "S" ? tbs174.CO_COL == LoginAuxili.CO_COL : 0 == 0)
                       select new PacientesAtestado
                       {
                           hr_Consul = tbs174.HR_AGEND_HORAR,

                           NO_PAC_ = tb07.NO_ALU,
                           RG_PAC = !String.IsNullOrEmpty(tb07.CO_RG_ALU) ? tb07.CO_RG_ALU + " - " + (!String.IsNullOrEmpty(tb07.CO_ORG_RG_ALU) ? tb07.CO_ORG_RG_ALU + "/" + tb07.CO_ESTA_RG_ALU : "") : " - ",
                           NO_RESP_ = tb07.TB108_RESPONSAVEL.NO_RESP,
                           NO_PAC_RECEB = !String.IsNullOrEmpty(tb07.NO_APE_ALU) ? tb07.NO_APE_ALU : tb07.NO_ALU,
                           NU_NIRE = tb07.NU_NIRE,
                           CO_ALU = tb07.CO_ALU,
                           CO_RESP = (tb07.TB108_RESPONSAVEL != null ? tb07.TB108_RESPONSAVEL.CO_RESP : (int?)null),

                           FL_PAI_RESP = tb07.FL_PAI_RESP_ATEND,
                           FL_MAE_RESP = tb07.FL_MAE_RESP_ATEND,
                           NO_PAI = tb07.NO_PAI_ALU,
                           NO_MAE = tb07.NO_MAE_ALU
                       }).OrderByDescending(w => w.hr_Consul).ThenBy(w => w.NO_PAC_).ToList();

            grdPacAtestado.DataSource = res;
            grdPacAtestado.DataBind();

            AbreModalPadrao("AbreModalAtestado();");
        }

        public class PacientesAtestado
        {
            public string hr_Consul { get; set; }
            public int CO_ALU { get; set; }
            public int? CO_RESP { get; set; }
            public string RG_PAC { get; set; }
            public string NO_PAC_ { get; set; }
            public string NO_PAC
            {
                get
                {
                    return (this.NU_NIRE.ToString().PadLeft(7, '0') + " - "
                        + (this.NO_PAC_RECEB.Length > 43 ? this.NO_PAC_RECEB.Substring(0, 43) + "..." : this.NO_PAC_RECEB));
                }
            }
            public int NU_NIRE { get; set; }
            public string NO_PAC_RECEB { get; set; }

            //Insumo para tratar o nome do responsável dinamicamente
            public string FL_PAI_RESP { get; set; }
            public string FL_MAE_RESP { get; set; }
            public string NO_PAI { get; set; }
            public string NO_MAE { get; set; }
            public string NO_RESP_ { get; set; }
            public string NO_RESP_IMP { get { return AuxiliFormatoExibicao.TrataNomeResponsavel(this.FL_PAI_RESP, this.FL_MAE_RESP, this.NO_PAI, this.NO_MAE, this.NO_RESP_, false); } }
            public string NO_RESP
            {
                get
                {
                    var nmResp = AuxiliFormatoExibicao.TrataNomeResponsavel(this.FL_PAI_RESP, this.FL_MAE_RESP, this.NO_PAI, this.NO_MAE, this.NO_RESP_);

                    if (nmResp == null)
                        return " - ";

                    nmResp = (nmResp.Length > 28 ? nmResp.Substring(0, 28) + "..." : nmResp);

                    return nmResp;
                }
            }
        }

        private void RecarregarGrids(int ID_AGEND_HORAR, int CO_ALU)
        {
            CarregaAgendamentos();

            foreach (GridViewRow li in grdPacientes.Rows)
            {
                int idAgenda = int.Parse(((HiddenField)li.FindControl("hidIdAgenda")).Value);

                if (idAgenda == ID_AGEND_HORAR)
                    ((CheckBox)li.FindControl("chkSelectPaciente")).Checked = true;
            }

            CarregaAgendaPlanejamento(CO_ALU);

            foreach (GridViewRow i in grdHistoricoAgenda.Rows)
            {
                string idAgendaHist = ((HiddenField)i.FindControl("hidIdAgenda")).Value;

                if (!String.IsNullOrEmpty(hidIdAgenda.Value) && idAgendaHist == hidIdAgenda.Value)
                    ((CheckBox)i.FindControl("chkSelectHistAge")).Checked = true;
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

        private void ExecutarFuncaoPadrao(string funcao)
        {
            ScriptManager.RegisterStartupScript(
                this.Page,
                this.GetType(),
                "Funcao",
                funcao,
                true
            );
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

                        AbreModalPadrao("AbreModalLog();");
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

                        AbreModalPadrao("AbreModalLog();");
                    }
                }
            }
        }

        protected void chkSelectPaciente_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            CheckBox chk;

            foreach (GridViewRow li in grdPacientes.Rows)
            {
                chk = (CheckBox)li.FindControl("chkSelectPaciente");

                if (chk.ClientID == atual.ClientID)
                {
                    int idAgenda = int.Parse(((HiddenField)li.FindControl("hidIdAgenda")).Value);
                    var tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(idAgenda);

                    if (tbs174.CO_SITUA_AGEND_HORAR == "A" && !String.IsNullOrEmpty(tbs174.FL_AGEND_ENCAM) && tbs174.FL_AGEND_ENCAM != "N")
                    {
                        if ((atual.Checked && tbs174.FL_AGEND_ENCAM == "S") || (!atual.Checked && tbs174.FL_AGEND_ENCAM == "A"))
                            hidAgendSelec.Value = idAgenda.ToString();
                        else
                            hidAgendSelec.Value = "";

                        if (tbs174.FL_AGEND_ENCAM == "S")
                            lblConfEncam.Text = "Deseja encaminhar o paciente para atendimento?";
                        else if (tbs174.FL_AGEND_ENCAM == "A")
                            lblConfEncam.Text = "Deseja retornar a situação do paciente para encaminhado?";
                    }
                    else
                        hidAgendSelec.Value = "";

                    if (!(chk.Checked && tbs174.CO_SITUA_AGEND_HORAR == "A" && tbs174.FL_AGEND_ENCAM == "A"))
                        ExecutarFuncaoPadrao("ZerarCronometro();");

                    if (chk.Checked)
                    {
                        CarregaItensAgendamento(idAgenda);
                        CarregaAgendaPlanejamento(tbs174.CO_ALU.Value);

                        //Percorre a grid de histórico de atendimento, e ao achar a 
                        foreach (GridViewRow i in grdHistoricoAgenda.Rows)
                        {
                            string idAgendaHist = ((HiddenField)i.FindControl("hidIdAgenda")).Value;
                            if (idAgendaHist == idAgenda.ToString())
                            {
                                CheckBox chkHistAg = (((CheckBox)i.FindControl("chkSelectHistAge")));
                                chkHistAg.Checked = true;

                                hidIdAgenda.Value = idAgendaHist.ToString();
                                CarregaDadosAgendamento(idAgenda);
                            }
                        }
                    }
                    else
                    {
                        grdItensAgend.DataSource = grdHistoricoAgenda.DataSource = null;
                        grdItensAgend.DataBind(); grdHistoricoAgenda.DataBind();

                        txtDtPrevisao.Text = txtAcaoPlan.Text = hidIdAgenda.Value = "";
                    }
                }
                else
                    chk.Checked = false;
            }

            if (!String.IsNullOrEmpty(hidAgendSelec.Value))
                AbreModalPadrao("AbreModalEncamAtend();");
        }

        protected void lnkbAtendSim_OnClick(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(hidAgendSelec.Value))
            {
                var tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(hidAgendSelec.Value));

                if (tbs174.FL_AGEND_ENCAM == "S")
                {
                    tbs174.FL_AGEND_ENCAM = "A";

                    tbs174.DT_ATEND = DateTime.Now;
                    tbs174.CO_COL_ATEND = LoginAuxili.CO_COL;
                    tbs174.CO_EMP_COL_ATEND = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
                    tbs174.CO_EMP_ATEND = LoginAuxili.CO_EMP;
                    tbs174.IP_ATEND = Request.UserHostAddress;
                }
                else if (tbs174.FL_AGEND_ENCAM == "A")
                {
                    tbs174.FL_AGEND_ENCAM = "S";

                    tbs174.DT_ATEND = (DateTime?)null;
                    tbs174.CO_COL_ATEND =
                    tbs174.CO_EMP_COL_ATEND =
                    tbs174.CO_EMP_ATEND = (int?)null;
                    tbs174.IP_ATEND = null;
                }

                TBS174_AGEND_HORAR.SaveOrUpdate(tbs174);

                RecarregarGrids(tbs174.ID_AGEND_HORAR, tbs174.CO_ALU.Value);

                if (tbs174.FL_AGEND_ENCAM == "A")
                    ExecutarFuncaoPadrao("IniciarCronometro();");
                else
                    ExecutarFuncaoPadrao("ZerarCronometro();");
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
                    else
                        txtDtPrevisao.Text = txtAcaoPlan.Text = hidIdAgenda.Value = "";
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
            ExcluiItemGridQuestionario(aux);
        }

        protected void imgPesqAgendamentos_OnClick(object sender, EventArgs e)
        {
            grdItensAgend.DataSource = grdQuestionario.DataSource = grdHistoricoAgenda.DataSource = null;
            grdItensAgend.DataBind(); grdQuestionario.DataBind(); grdHistoricoAgenda.DataBind();
            txtConsidAtendim.Text = "";
            CarregaAgendamentos();
            ExecutarFuncaoPadrao("ZerarCronometro();");
        }

        protected void lnkFinalizar_OnClick(object sender, EventArgs e)
        {
            Persistencias();
        }

        protected void lnkFicha_OnClick(object sender, EventArgs e)
        {
            List<int> listAlus = new List<int>();
            
            if (grdPacientes.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdPacientes.Rows)
                {
                    string coSitua = (((HiddenField)linha.FindControl("hidSituacao")).Value);

                    if (coSitua == "R")
                    {
                        int coAlu = int.Parse(((HiddenField)linha.FindControl("hidCoAlu")).Value);

                        listAlus.Add(coAlu);
                    }
                }
            }

            if (listAlus.Count > 0)
            {
                var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                           where listAlus.Contains(tb07.CO_ALU)
                           select new { tb07.NO_ALU, tb07.CO_ALU }).OrderBy(w => w.NO_ALU).DistinctBy(p => p.CO_ALU).ToList();

                if (res != null)
                {
                    drpPacienteFicha.DataTextField = "NO_ALU";
                    drpPacienteFicha.DataValueField = "CO_ALU";
                    drpPacienteFicha.DataSource = res;
                    drpPacienteFicha.DataBind();
                }

                AbreModalPadrao("AbreModalFichaAtendimento();");
            }
            else
                AuxiliPagina.EnvioMensagemErro(this.Page, "Deve existir pelo menos um paciente finalizado!");
        }

        protected void lnkbImprimirFicha_Click(object sender, EventArgs e)
        {
            int paciente = int.Parse(drpPacienteFicha.SelectedValue);

            string infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptFichaAtend rpt = new RptFichaAtend();
            var retorno = rpt.InitReport("FICHA DE ATENDIMENTO", infos, LoginAuxili.CO_EMP, paciente, 0, txtObsFicha.Text, txtQxsFicha.Text);

            GerarRelatorioPadrão(rpt, retorno);
        }

        protected void lnkGuia_OnClick(object sender, EventArgs e)
        {
            var data = DateTime.Now;

            if (LoginAuxili.FLA_USR_DEMO)
                data = LoginAuxili.DATA_INICIO_USU_DEMO;

            txtDtGuia.Text = data.ToShortDateString();
            txtObsGuia.Text = "";
            txtObsGuia.Attributes.Add("MaxLength", "180");
            drpOperGuia.Items.Clear();
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(drpOperGuia, false, false, false, true, false);
            drpOperGuia.Items.Insert(0, new ListItem("PADRÃO", "0"));

            CarregarPacientesGuia();
        }

        protected void txtDtGuia_OnTextChanged(object sender, EventArgs e)
        {
            CarregarPacientesGuia();
        }

        protected void lnkbImprimirGuia_OnClick(object sender, EventArgs e)
        {
            int paciente = int.Parse(drpPacienteGuia.SelectedValue);

            RptGuiaAtend rpt = new RptGuiaAtend();
            var retorno = rpt.InitReport(paciente, txtObsGuia.Text, drpOperGuia.SelectedValue);

            GerarRelatorioPadrão(rpt, retorno);
        }

        protected void lnkAtestado_OnClick(object sender, EventArgs e)
        {
            var data = DateTime.Now;

            if (LoginAuxili.FLA_USR_DEMO)
                data = LoginAuxili.DATA_INICIO_USU_DEMO;

            txtDtAtestado.Text = data.ToShortDateString();
            CarregarPacientesDisponiveisAtestado();

            AbreModalPadrao("AbreModalAtestado();");
        }

        protected void rbtPaciente_OnCheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdoAtual = (RadioButton)sender;

            foreach (GridViewRow li in grdPacAtestado.Rows)
            {
                RadioButton rdo = (((RadioButton)li.FindControl("rbtPaciente")));

                if (rdo.ClientID != rdoAtual.ClientID)
                    rdo.Checked = false;
            }

            AbreModalPadrao("AbreModalAtestado();");
        }

        protected void txtDtAtestado_OnTextChanged(object sender, EventArgs e)
        {
            CarregarPacientesDisponiveisAtestado();
        }

        protected void lnkbGerarAtestado_Click(object sender, EventArgs e)
        {
            if (grdPacAtestado.Rows.Count == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Não existem pacientes para a emissão neste período!");
                return;
            }

            int ck = 0;

            foreach (GridViewRow li in grdPacAtestado.Rows)
            {
                RadioButton rdo = (((RadioButton)li.FindControl("rbtPaciente")));

                if (rdo.Checked)
                {
                    var nmPac = ((HiddenField)li.FindControl("hidNmPac")).Value;

                    var nmResp = ((HiddenField)li.FindControl("hidNmResp")).Value;

                    var infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
                    RptDclComparecimento rpt = new RptDclComparecimento();
                    var retorno = rpt.InitReport("Declaração de Comparecimento", infos, LoginAuxili.CO_EMP, nmPac, nmResp, this.drpPrdComparecimento.SelectedItem.Text, txtDtAtestado.Text, LoginAuxili.CO_COL);

                    GerarRelatorioPadrão(rpt, retorno);

                    ck++;
                }
            }

            if (ck == 0)
                AuxiliPagina.EnvioMensagemErro(this.Page, "Deve ser selecionado pelo menos um paciente!");
        }

        private void GerarRelatorioPadrão(DevExpress.XtraReports.UI.XtraReport rpt, int lRetorno)
        {
            if (lRetorno == 0)
                AuxiliPagina.EnvioMensagemErro(this.Page, "Erro na geração do Relatório! Tente novamente.");
            else if (lRetorno < 0)
                AuxiliPagina.EnvioMensagemErro(this.Page, "Não existem dados para a impressão do formulário solicitado.");
            else
            {
                Session["Report"] = rpt;
                Session["URLRelatorio"] = "/GeducReportViewer.aspx";

                AuxiliPagina.AbreNovaJanela(this, Session["URLRelatorio"].ToString());
                //----------------> Limpa a var de sessão com o url do relatório.
                Session.Remove("URLRelatorio");

                //----------------> Limpa a ref da url utilizada para carregar o relatório.
                System.Reflection.PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                isreadonly.SetValue(this.Request.QueryString, false, null);
                isreadonly.SetValue(this.Request.QueryString, true, null);
            }
        }

        #endregion
    }
}