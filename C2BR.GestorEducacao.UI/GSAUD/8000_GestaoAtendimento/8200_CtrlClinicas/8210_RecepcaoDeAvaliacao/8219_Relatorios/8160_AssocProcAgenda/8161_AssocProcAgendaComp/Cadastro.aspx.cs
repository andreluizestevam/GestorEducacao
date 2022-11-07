//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CADASTRO E INFORMAÇÕES DE ALUNOS
// OBJETIVO: Registro de Atendimento do Usuário
// DATA DE CRIAÇÃO: 08/03/2014
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------

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
using System.Globalization;
using System.Reflection;
using Resources;
using WRAuxiliares = C2BR.GestorEducacao.DLLRelatorioWeb.Auxiliares;
using C2BR.GestorEducacao.DLLRelatorioWeb.Interfaces;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Data.Objects;
using System.Transactions;

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8160_AssocProcAgenda._8161_AssocProcAgendaComp
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            ///Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                var data = DateTime.Now;

                if (LoginAuxili.FLA_USR_DEMO)
                    data = LoginAuxili.DATA_INICIO_USU_DEMO;

                Session["arrayItensSelec"] = null;

                grdHistorPaciente.DataSource = grdHorario.DataSource = null;
                grdHistorPaciente.DataBind(); grdHorario.DataBind();
                btnExcProcs.OnClientClick = "alert('É necessario selecionar pelo menos um procedimento!'); return false;";

                CarregaPacientes();
                txtDtIniHistoUsuar.Text = data.AddDays(-5).ToString();
                txtDtFimHistoUsuar.Text = data.AddDays(30).ToString();

                AuxiliCarregamentos.CarregaUFs(ddlUFOrgEmis, false, LoginAuxili.CO_EMP, true);
                AuxiliCarregamentos.CarregaUFs(ddlUF, false, LoginAuxili.CO_EMP);
                AuxiliCarregamentos.CarregaProfissionaisSaude(ddlProfi, 0, true, "0", true);
                ddlUFOrgEmis.Items.Insert(0, new ListItem("", ""));
                carregaCidade();
                carregaBairro();

                CarregaOperadoras(ddlOperadora, "");
                CarregarPlanosSaude(ddlPlano, ddlOperadora);

                CarregarGrupos(ddlGrupoPr, false, false, true);
                CarregarSubGrupos(ddlSubGrupoPr, ddlGrupoPr);
                CarregaProcedimentos(ddlProcedimento, ddlOperadora, ddlGrupoPr, ddlSubGrupoPr, null, true);
            }
        }

        protected void imgbPesqPacNome_OnClick(object sender, EventArgs e)
        {
            
            int profissional = ddlProfi.SelectedValue != "" ? int.Parse(ddlProfi.SelectedValue) : 0;

            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       join tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros() on tb07.CO_ALU equals tbs174.CO_ALU
                       where tb07.NO_ALU.Contains(txtNomePacPesq.Text)
                           && (profissional != 0 ? tbs174.CO_COL == profissional : 0 == 0)
                       select new { tb07.NO_ALU, tb07.CO_ALU }).OrderBy(w => w.NO_ALU).DistinctBy(p => p.CO_ALU).ToList();

            if (res != null)
            {
                ddlNomeUsu.DataTextField = "NO_ALU";
                ddlNomeUsu.DataValueField = "CO_ALU";
                ddlNomeUsu.DataSource = res;
                ddlNomeUsu.DataBind();
            }

            ddlNomeUsu.Items.Insert(0, new ListItem("Todos", "0"));

            OcultarPesquisa(true);

        }

        protected void imgbVoltarPesq_OnClick(object sender, EventArgs e)
        {
            OcultarPesquisa(false);
        }

        private void OcultarPesquisa(bool ocultar)
        {
            txtNomePacPesq.Visible =
            imgbPesqPacNome.Visible = !ocultar;
            ddlNomeUsu.Visible =
            imgbVoltarPesq.Visible = ocultar;
        }

        #endregion

        #region Carregamento

        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            //Lista onde serão armazenados os itens que se deseja salvar, para poder ser percorrida depois
            List<saidaClassHorarios> lstItensAgenda = new List<saidaClassHorarios>();

            //Prepara lista dos procedimentos que serão salvos
            #region Lista Salvar

            foreach (GridViewRow li in grdHorario.Rows)
            {
                //Apenas nos itens selecionados
                if (((CheckBox)li.FindControl("ckSelectHr")).Checked)
                {
                    DropDownList proced = (((DropDownList)li.FindControl("ddlProcedimento")));
                    TextBox deAcao = (((TextBox)li.FindControl("txtDesAcao")));
                    string situa = (((HiddenField)li.FindControl("hidSituaItemPlanej")).Value);

                    #region Validações

                    //Se foi selecionado o procedimento
                    if (string.IsNullOrEmpty(proced.SelectedValue))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "O Procedimento precisa ser selecionado!");
                        proced.Focus();
                        return;
                    }

                    #endregion

                    //Adiciona mais um item na lista que será persistida
                    saidaClassHorarios ob = new saidaClassHorarios();
                    ob.DE_RESUM_ACAO = deAcao.Text;
                    ob.tbs356 = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(proced.SelectedValue));
                    ob.situa = situa;
                    lstItensAgenda.Add(ob);
                }
            }

            #endregion

            //Percorre lista dos itens selecionados da agenda, para incluir/alterar os dados de procedimentos associados
            #region Itens Agenda Selecionados

            foreach (GridViewRow i in grdHistorPaciente.Rows)
            {
                if (((CheckBox)i.FindControl("chkSelectHist")).Checked)
                {
                    int idAgend = int.Parse(((HiddenField)i.FindControl("hidIdAgend")).Value);
                    //Cria objeto de entidade da agenda que será armazenado em outras tabelas posteriormente
                    var tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(idAgend);
                    tbs174.TBS370_PLANE_AVALIReference.Load();

                    //Percorre todos os itens presentes na grid de associação
                    foreach (var itls in lstItensAgenda)
                    {
                        var res = (from tbs389 in TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros()
                                   where tbs389.TBS174_AGEND_HORAR.ID_AGEND_HORAR == idAgend
                                && tbs389.TBS386_ITENS_PLANE_AVALI.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == itls.tbs356.ID_PROC_MEDI_PROCE
                                   select tbs389).ToList();

                        //Se não houver este procedimento, cria no item de planejamento e associa à esta agenda
                        if (res.Count == 0)
                        {
                            //Apenas se estiver passando por um item de planejamento com status em aberto
                            if (itls.situa == "A")
                            {
                                //Inclui registro de item de planejamento na tabela TBS386_ITENS_PLANE_AVALI
                                #region Inclui o Item de Planjamento

                                //Se tem item de planjamento, trás objeto da entidade dele, se não, cria novo objeto da entidade
                                TBS386_ITENS_PLANE_AVALI tbs386 = new TBS386_ITENS_PLANE_AVALI();

                                //Dados da situação
                                tbs386.CO_SITUA = "A";
                                tbs386.DT_SITUA = DateTime.Now;
                                tbs386.CO_COL_SITUA = LoginAuxili.CO_COL;
                                tbs386.CO_EMP_COL_SITUA = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                                tbs386.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                                tbs386.IP_SITUA = Request.UserHostAddress;
                                tbs386.DE_RESUM_ACAO = (!string.IsNullOrEmpty(itls.DE_RESUM_ACAO) ? itls.DE_RESUM_ACAO : null);

                                //Dados básicos do item de planejamento
                                tbs386.TBS370_PLANE_AVALI = RecuperaPlanejamento((tbs174.TBS370_PLANE_AVALI != null ? tbs174.TBS370_PLANE_AVALI.ID_PLANE_AVALI : (int?)null), tbs174.CO_ALU.Value);
                                tbs386.TBS356_PROC_MEDIC_PROCE = itls.tbs356;
                                tbs386.NR_ACAO = RecuperaUltimoNrAcao(tbs386.TBS370_PLANE_AVALI.ID_PLANE_AVALI);
                                tbs386.QT_SESSO = TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaPelaAgendaEProcedimento(idAgend, itls.tbs356.ID_PROC_MEDI_PROCE).Count; //Conta quantos itens existem na lista para este mesmo e agenda
                                tbs386.DT_INICI = tbs174.DT_AGEND_HORAR; //Verifica qual a primeira data na lista
                                tbs386.DT_FINAL = tbs174.DT_AGEND_HORAR; //Verifica qual a última data na lista
                                tbs386.FL_AGEND_FEITA_PLANE = "N";
                                
                                var vl_proc = TBS353_VALOR_PROC_MEDIC_PROCE.RetornaPeloProcedimento(itls.tbs356.ID_PROC_MEDI_PROCE);
                                
                                if (vl_proc != null)
                                {
                                    tbs386.VL_PROCED = vl_proc.VL_BASE;
                                }

                                //Dados do cadastro
                                tbs386.DT_CADAS = DateTime.Now;
                                tbs386.CO_COL_CADAS = LoginAuxili.CO_COL;
                                tbs386.CO_EMP_COL_CADAS = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                                tbs386.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                                tbs386.IP_CADAS = Request.UserHostAddress;

                                //Data prevista é a data do agendamento associado
                                tbs386.DT_AGEND = tbs174.DT_AGEND_HORAR;

                                TBS386_ITENS_PLANE_AVALI.SaveOrUpdate(tbs386);

                                #endregion

                                //Associa o item criado ao agendamento em contexto na tabela TBS389_ASSOC_ITENS_PLANE_AGEND
                                #region Associa o Item ao Agendamento

                                TBS389_ASSOC_ITENS_PLANE_AGEND tbs389 = new TBS389_ASSOC_ITENS_PLANE_AGEND();
                                tbs389.TBS174_AGEND_HORAR = tbs174;
                                tbs389.TBS386_ITENS_PLANE_AVALI = tbs386;
                                TBS389_ASSOC_ITENS_PLANE_AGEND.SaveOrUpdate(tbs389, true);

                                tbs174.TBS370_PLANE_AVALI = tbs386.TBS370_PLANE_AVALI;
                                TBS174_AGEND_HORAR.SaveOrUpdate(tbs174, true);

                                #endregion
                            }
                        }
                        else
                        {
                            foreach (var rsAssoc in res)
                            {
                                rsAssoc.TBS174_AGEND_HORARReference.Load();
                                rsAssoc.TBS386_ITENS_PLANE_AVALIReference.Load();
                                /*Percorre a lista de associações deste procedimento à esta agenda
                                 *verificando e apenas alterando a descrição da ação daqueles que estiverem com
                                 *situação "Em Aberto".
                                 */
                                #region Verifica e Edita

                                //Realiza esse bloco apenas se o item não estiver realizado
                                if (rsAssoc.TBS386_ITENS_PLANE_AVALI.CO_SITUA != "R")
                                {
                                    var ob386 = TBS386_ITENS_PLANE_AVALI.RetornaPelaChavePrimaria(rsAssoc.TBS386_ITENS_PLANE_AVALI.ID_ITENS_PLANE_AVALI);
                                    if (rsAssoc.TBS386_ITENS_PLANE_AVALI.CO_SITUA == "A")
                                        ob386.DE_RESUM_ACAO = itls.DE_RESUM_ACAO;

                                    ob386.CO_SITUA = itls.situa;
                                    TBS386_ITENS_PLANE_AVALI.SaveOrUpdate(ob386, true);
                                }

                                #endregion
                            }
                        }
                    }
                }
            }

            #endregion

            Session["arrayItensSelec"] = null;
            AuxiliPagina.RedirecionaParaPaginaSucesso("Operação realizada com Sucesso!", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
        }

        public class saidaClassHorarios
        {
            public TBS356_PROC_MEDIC_PROCE tbs356 { get; set; }
            public string DE_RESUM_ACAO { get; set; }
            public string situa { get; set; }
        }

        /// <summary>
        /// Retorna um objeto do planejamento de determinad paciente/agenda recebidos como parâmetro
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Retorna o último número de ação encontrado (para o planejamento recebido como parâmetro) + 1
        /// </summary>
        /// <param name="CO_ALU"></param>
        /// <param name="ID_PROC"></param>
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

        /// <summary>
        /// Exclui item da grid recebendo como parâmetro o index correspondente
        /// </summary>
        protected void ExcluiItemGridDEFIN(List<int> Indexs)
        {
            DataTable dtV = CriarColunasELinhasGridDEFIN();

            foreach (var i in Indexs)
                dtV.Rows.RemoveAt(i); // Exclui os itens de index correspondente

            Session["GridSolic_PR_AGEND"] = dtV;

            carregaGridNovaComContextoDEFIN();
        }

        /// <summary>
        /// Cria uma nova linha na forma de pagamento cheque
        /// </summary>
        protected void CriaNovaLinhaGridDEFIN()
        {
            DataTable dtV = CriarColunasELinhasGridDEFIN();

            DataRow linha = dtV.NewRow();
            linha["hora"] = " - ";
            linha["MARCADO"] = "0";
            linha["ID_ITEM_PLAN"] = "";
            linha["hidSituaItemPlanej"] = "A";
            linha["ID_GRUPO"] = "";
            linha["ID_SUB_GRUPO"] = "";
            linha["ID_PROCED"] = "";
            linha["NM_PROCED"] = "";
            linha["NR_ACAO_V"] = "";
            linha["DE_ACAO"] = "";
            dtV.Rows.Add(linha);

            Session["GridSolic_PR_AGEND"] = dtV;

            carregaGridNovaComContextoDEFIN();
        }

        private DataTable CriarColunasELinhasGridDEFIN()
        {
            DataTable dtV = new DataTable();
            DataColumn dcATM;

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "ID_ITEM_PLAN";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "MARCADO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "hidSituaItemPlanej";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "hora";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "ID_GRUPO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "ID_SUB_GRUPO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "ID_PROCED";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "NM_PROCED";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "NR_ACAO_V";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "DE_ACAO";
            dtV.Columns.Add(dcATM);

            DataRow linha;
            foreach (GridViewRow li in grdHorario.Rows)
            {
                linha = dtV.NewRow();
                linha["hora"] = li.Cells[1].Text;
                linha["ID_ITEM_PLAN"] = (((HiddenField)li.FindControl("hidIdItemPlane")).Value);
                linha["MARCADO"] = ((((CheckBox)li.FindControl("ckSelectHr")).Checked) ? "1" : "0");
                linha["hidSituaItemPlanej"] = (((HiddenField)li.FindControl("hidSituaItemPlanej")).Value);
                linha["ID_GRUPO"] = (((HiddenField)li.FindControl("hidIdGrupo")).Value);
                linha["ID_SUB_GRUPO"] = (((HiddenField)li.FindControl("hidIdSubGrupo")).Value);
                linha["ID_PROCED"] = (((HiddenField)li.FindControl("hidIdProced")).Value);
                linha["NM_PROCED"] = (((TextBox)li.FindControl("txtDesProced")).Text);
                linha["NR_ACAO_V"] = (((TextBox)li.FindControl("txtNuAcao")).Text);
                linha["DE_ACAO"] = (((TextBox)li.FindControl("txtDesAcao")).Text);
                dtV.Rows.Add(linha);
            }
            return dtV;
        }

        /// <summary>
        /// Carrega o a DataTable em Session com as informações anteriores e a nova linha.
        /// </summary>
        protected void carregaGridNovaComContextoDEFIN()
        {
            DataTable dtV = new DataTable();

            dtV = (DataTable)Session["GridSolic_PR_AGEND"];

            grdHorario.DataSource = dtV;
            grdHorario.DataBind();

            int aux = 0;
            foreach (GridViewRow li in grdHorario.Rows)
            {
                //Declara
                DropDownList ddlGrupo, ddlSubGrupo, ddlProced;
                TextBox txtNmProced, txtNrAcao, txtDeAcao;
                HiddenField hidIdGrupo, hidIdSubGrupo, hidIdProced, hidIdItemPlane, hidSituaItemPlanej;
                bool marcado;

                //Instancia
                CheckBox chk = (((CheckBox)li.FindControl("ckSelectHr")));
                hidIdItemPlane = (((HiddenField)li.FindControl("hidIdItemPlane")));
                hidSituaItemPlanej = (((HiddenField)li.FindControl("hidSituaItemPlanej")));
                hidIdGrupo = (((HiddenField)li.FindControl("hidIdGrupo")));
                hidIdSubGrupo = (((HiddenField)li.FindControl("hidIdSubGrupo")));
                hidIdProced = (((HiddenField)li.FindControl("hidIdProced")));
                ddlGrupo = (((DropDownList)li.FindControl("ddlGrupo")));
                ddlSubGrupo = (((DropDownList)li.FindControl("ddlSubGrupo")));
                ddlProced = (((DropDownList)li.FindControl("ddlProcedimento")));
                txtNmProced = (((TextBox)li.FindControl("txtDesProced")));
                txtNrAcao = (((TextBox)li.FindControl("txtNuAcao")));
                txtDeAcao = (((TextBox)li.FindControl("txtDesAcao")));

                string idItemPlan, idGrupo, idSubGrp, idProced, nmProc, nrAcao, deAcao, situaItem;

                //Coleta os valores do dtv da modal popup
                idItemPlan = dtV.Rows[aux]["ID_ITEM_PLAN"].ToString();
                situaItem = dtV.Rows[aux]["hidSituaItemPlanej"].ToString();
                li.Cells[1].Text = dtV.Rows[aux]["hora"].ToString();
                idGrupo = dtV.Rows[aux]["ID_GRUPO"].ToString();
                idSubGrp = dtV.Rows[aux]["ID_SUB_GRUPO"].ToString();
                idProced = dtV.Rows[aux]["ID_PROCED"].ToString();
                nmProc = dtV.Rows[aux]["NM_PROCED"].ToString();
                nrAcao = dtV.Rows[aux]["NR_ACAO_V"].ToString();
                deAcao = dtV.Rows[aux]["DE_ACAO"].ToString();
                marcado = (dtV.Rows[aux]["MARCADO"].ToString() == "1" ? true : false);

                //Seta os valores
                CarregarGrupos(ddlGrupo, false, true, false);
                ddlGrupo.SelectedValue = idGrupo;
                CarregarSubGrupos(ddlSubGrupo, ddlGrupo, false, true, false);
                ddlSubGrupo.SelectedValue = idSubGrp;
                CarregaProcedimentos(ddlProced, ddlOperadora, ddlGrupo, ddlSubGrupo, idProced);

                hidIdItemPlane.Value = idItemPlan;
                hidSituaItemPlanej.Value = situaItem;
                hidIdGrupo.Value = idGrupo;
                hidIdSubGrupo.Value = idSubGrp;
                hidIdProced.Value = idProced;
                txtNmProced.Text = nmProc;
                txtNrAcao.Text = nrAcao;
                txtDeAcao.Text = deAcao;

                aux++;

                //Se estiver como marcado
                if (marcado)
                    chk.Checked = ddlGrupo.Enabled = ddlSubGrupo.Enabled = ddlProced.Enabled = txtDeAcao.Enabled = true;
            }
        }

        /// <summary>
        /// Carrega a grid de horarios de acordo com o profissional de saúde recebido como parâmetro
        /// </summary>
        private void CarregaGridHorario()
        {
            //Se não houver nenhum item selecionado, então carrega a grid com tudo nulo
            if (Session["arrayItensSelec"] == null)
            {
                grdHorario.DataSource = null;
                grdHorario.DataBind();
                btnExcProcs.OnClientClick = "alert('É necessario selecionar pelo menos um procedimento!'); return false;";
            }
            else
            {
                List<int> itens = (List<int>)Session["arrayItensSelec"];

                var res = (from tbs386 in TBS386_ITENS_PLANE_AVALI.RetornaTodosRegistros()
                           join tbs389 in TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros() on tbs386.ID_ITENS_PLANE_AVALI equals tbs389.TBS386_ITENS_PLANE_AVALI.ID_ITENS_PLANE_AVALI
                           where itens.Contains(tbs389.TBS174_AGEND_HORAR.ID_AGEND_HORAR)
                           && tbs386.CO_SITUA != "C"
                           select new HorarioSaida
                           {
                               ID_ITEM_PLAN = tbs386.ID_ITENS_PLANE_AVALI,
                               dt = tbs389.TBS174_AGEND_HORAR.DT_AGEND_HORAR,
                               hr = tbs389.TBS174_AGEND_HORAR.HR_AGEND_HORAR,
                               ID_PROC = tbs386.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE,
                               ID_GRUPO = tbs386.TBS356_PROC_MEDIC_PROCE.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO,
                               ID_SUB_GRUPO = tbs386.TBS356_PROC_MEDIC_PROCE.TBS355_PROC_MEDIC_SGRUP.ID_PROC_MEDIC_SGRUP,
                               ID_PROCED = tbs386.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE,
                               NR_ACAO = (itens.Count == 1 ? tbs386.NR_ACAO : 000),  //Se for de um registro só, coloca o número da ação, se for de mais de um, coloca 000 como padrão
                               DE_ACAO = tbs386.DE_RESUM_ACAO,
                               NM_PROCED = tbs386.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
                               hidSituaItemPlanej = (itens.Count == 1 ? tbs386.CO_SITUA : "A"), //Se for de um registro só, coloca a situação, se for de mais de um, coloca A como padrão
                           }).OrderBy(w => w.dt).ToList();

                btnExcProcs.OnClientClick = "alert('É necessario selecionar pelo menos um procedimento!'); return false;";

                grdHorario.DataSource = res;
                grdHorario.DataBind();
            }
        }

        /// <summary>
        /// Carrega os grupos de procedimentos
        /// </summary>
        /// <param name="ddl"></param>
        private void CarregarGrupos(DropDownList ddl, bool Relatorio = false, bool mostraPadrao = false, bool insereVazio = true)
        {
            AuxiliCarregamentos.CarregaGruposProcedimentosMedicos(ddl, Relatorio, mostraPadrao, insereVazio);
        }

        /// <summary>
        /// Carrega os subgrupos de procedimentos
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="ddlGrupo"></param>
        private void CarregarSubGrupos(DropDownList ddl, DropDownList ddlGrupo, bool Relatorio = false, bool mostraPadrao = false, bool insereVazio = true)
        {
            AuxiliCarregamentos.CarregaSubGruposProcedimentosMedicos(ddl, ddlGrupo, Relatorio, mostraPadrao, insereVazio);
        }

        /// <summary>
        /// Calcula os valores de tabela e calculado de acordo com desconto concedido à determinada operadora e plano de saúde informados no cadastro na página
        /// </summary>
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

                AuxiliCalculos.ValoresProcedimentosMedicos ob = AuxiliCalculos.RetornaValoresProcedimentosMedicos((procAgrupador.HasValue ? procAgrupador.Value : idProc), idOper, idPlan);
                txtValor.Text = ob.VL_CALCULADO.ToString("N2"); // Insere o valor Calculado
            }
        }

        /// <summary>
        /// Grava na tabela de financeiro de procedimentos os devidos dados
        /// </summary>
        private void GravaFinanceiroProcedimentos(TBS356_PROC_MEDIC_PROCE tbs356, int CO_ALU, int CO_RESP, int ID_PLAN, int ID_OPER, int ID_AGEND_HORAR, int CO_COL)
        {
            var re = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                      where tb03.CO_COL == CO_COL
                      select new { tb03.CO_EMP }).FirstOrDefault();

            TBS357_PROC_MEDIC_FINAN tbs357 = new TBS357_PROC_MEDIC_FINAN();

            //Recebe objeto com o valor corrente do procedimento para determinado plano de saúde (Quando esta for a situação)
            AuxiliCalculos.ValoresProcedimentosMedicos valPrc = AuxiliCalculos.RetornaValoresProcedimentosMedicos(tbs356.ID_PROC_MEDI_PROCE, ID_OPER, ID_PLAN);

            tbs357.TBS174_AGEND_HORAR = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(ID_AGEND_HORAR) ;
            tbs357.TB250_OPERA = (ID_OPER != 0 ? TB250_OPERA.RetornaPelaChavePrimaria(ID_OPER) : null);
            tbs357.CO_COL_INCLU_LANC = LoginAuxili.CO_COL;
            tbs357.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
            tbs357.IP_INCLU_LANC = Request.UserHostAddress;
            tbs357.CO_COL_PROFI_ATEND = CO_COL;
            tbs357.TB25_EMPRESA1 = TB25_EMPRESA.RetornaPelaChavePrimaria(re.CO_EMP);
            tbs357.FL_SITUA = "A";
            tbs357.CO_COL_SITUA = LoginAuxili.CO_COL;
            tbs357.CO_EMP_SITUA = LoginAuxili.CO_EMP;
            tbs357.DT_SITUA = DateTime.Now;
            tbs357.IP_SITUA = Request.UserHostAddress;
            tbs357.DT_INCLU_LANC = DateTime.Now;
            tbs357.ID_ITEM = tbs356.ID_PROC_MEDI_PROCE;
            tbs357.NM_ITEM = (tbs356.NM_PROC_MEDI.Length > 100 ? tbs356.NM_PROC_MEDI.Substring(0, 100) : tbs356.NM_PROC_MEDI);
            tbs357.CO_TIPO_ITEM = "PCM";
            tbs357.CO_ORIGEM = "C"; //Determina que a origem desse registro financeiro é uma consulta
            tbs357.CO_ALU = CO_ALU;
            tbs357.CO_RESP = CO_RESP;
            //tbs357.DT_EVENT = DateTime.Now;

            //Questão de valores
            tbs357.VL_CUSTO_PROC = valPrc.VL_CUSTO;
            tbs357.VL_RESTI = valPrc.VL_RESTI;
            tbs357.VL_BASE = valPrc.VL_BASE;
            tbs357.VL_PROCE_LIQUI = valPrc.VL_CALCULADO;
            tbs357.VL_DSCTO = valPrc.VL_DESCONTO;
            tbs357.TBS353_VALOR_PROC_MEDIC_PROCE = (valPrc.ID_VALOR_PROC_MEDIC_PROCE != 0 ? TBS353_VALOR_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(valPrc.ID_VALOR_PROC_MEDIC_PROCE) : null);
            tbs357.TBS361_CONDI_PLANO_SAUDE = (valPrc.ID_CONDI_PLANO_SAUDE != 0 ? TBS361_CONDI_PLANO_SAUDE.RetornaPelaChavePrimaria(valPrc.ID_CONDI_PLANO_SAUDE) : null);

            TBS357_PROC_MEDIC_FINAN.SaveOrUpdate(tbs357, true);
        }

        /// <summary>
        /// Responsável por carregar os pacientes de acordo com o cpf concedido
        /// </summary>
        private void PesquisaPaciente()
        {
            //Verifica se o usuário optou por pesquisar por CPF ou por NIRE
            if (chkPesqCpf.Checked)
            {
                string cpf = (txtCPFPaci.Text != "" ? txtCPFPaci.Text.Replace(".", "").Replace("-", "").Trim() : "");

                //Valida se o usuário digitou ou não o CPF
                if (txtCPFPaci.Text == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o CPF para pesquisa");
                    return;
                }

                var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                           where tb07.NU_CPF_ALU == cpf
                           select new { tb07.CO_ALU, tb07.NU_NIS }).FirstOrDefault();

                ddlNomeUsu.SelectedValue = res.CO_ALU.ToString();
                txtNisUsu.Text = res.NU_NIS.ToString();
            }
            else if (chkPesqNire.Checked)
            {
                //Valida se o usuário deixou o campo em branco.
                if (txtNirePaci.Text == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o NIRE para pesquisa");
                    return;
                }

                int nire = int.Parse(txtNirePaci.Text.Trim());

                var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                           where tb07.NU_NIRE == nire
                           select new { tb07.CO_ALU, tb07.NU_NIS }).FirstOrDefault();

                ddlNomeUsu.SelectedValue = res.CO_ALU.ToString();
                txtNisUsu.Text = res.NU_NIS.ToString();
            }
        }

        /// <summary>
        /// Método responsável por enviar SMS caso a opção correspondente tenha sido selecionada
        /// </summary>
        private void EnviaSMS(bool NovoAgendamento, string hora, DateTime data, int CO_COL, int CO_ESPEC, int CO_EMP, string NU_CELULAR, string NO_ALU, int CO_ALU)
        {
            //***IMPORTANTE*** - O limite máximo de caracteres de acordo com a ZENVIA que é quem presta o serviço de envio,
            //é de 140 caracteres para NEXTEL e 150 para DEMAIS OPERADORAS
            TB03_COLABOR tb03 = TB03_COLABOR.RetornaTodosRegistros().Where(w => w.CO_COL == CO_COL).FirstOrDefault();
            string noEspec = TB63_ESPECIALIDADE.RetornaTodosRegistros().Where(w => w.CO_ESPECIALIDADE == CO_ESPEC).FirstOrDefault().NO_ESPECIALIDADE;
            string noEmp = TB25_EMPRESA.RetornaPelaChavePrimaria(CO_EMP).NO_FANTAS_EMP;

            noEspec = noEspec.Length > 23 ? noEspec.Substring(0, 23) : noEspec;
            bool masc = tb03.CO_SEXO_COL == "M" ? true : false;
            string noCol = tb03.NO_COL.Length > 40 ? tb03.NO_COL.Substring(0, 40) : tb03.NO_COL;
            string texto = "";
            if (NovoAgendamento)
                texto = "Consulta na especialidade " + noEspec + " com " + (masc ? "o Dr" : "a Dra ") + noCol + " agendada para o dia " + data.ToString("dd/MM") + ", às " + hora;
            else
                texto = "Consulta na especialidade " + noEspec + " com " + (masc ? "o Dr" : "a Dra ") + noCol + " reagendada para o dia " + data.ToString("dd/MM") + ", às " + hora;

            //Envia a mensagem apenas se o número do celular for diferente de nulo
            if (NU_CELULAR != null)
            {
                var admUsuario = ADMUSUARIO.RetornaPelaChavePrimaria(LoginAuxili.IDEADMUSUARIO);
                string retorno = "";

                if (admUsuario.QT_SMS_MES_USR != null && admUsuario.QT_SMS_MAXIM_USR != null)
                {
                    if (admUsuario.QT_SMS_MAXIM_USR <= admUsuario.QT_SMS_MES_USR)
                    {
                        retorno = "Saldo do envio de SMS para outras pessoas ultrapassado.";
                        return;
                    }
                }

                if (!Page.IsValid)
                    return;
                try
                {
                    //Salva na tabela de mensagens enviadas, as informações pertinentes
                    TB249_MENSAG_SMS tb249 = new TB249_MENSAG_SMS();
                    tb249.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_UNID_FUNC, LoginAuxili.CO_COL);
                    tb249.CO_RECEPT = CO_ALU;
                    tb249.CO_EMP_RECEPT = CO_EMP;
                    tb249.NO_RECEPT_SMS = NO_ALU != "" ? NO_ALU : NO_ALU;
                    tb249.DT_ENVIO_MENSAG_SMS = DateTime.Now;
                    tb249.DES_MENSAG_SMS = texto.Length > 150 ? texto.Substring(0, 150) : texto;
                    tb249.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);

                    string desLogin = LoginAuxili.DESLOGIN.Trim().Length > 15 ? LoginAuxili.DESLOGIN.Trim().Substring(0, 15) : LoginAuxili.DESLOGIN.Trim();

                    SMSAuxili.SMSRequestReturn sMSRequestReturn = SMSAuxili.EnvioSMS(desLogin, Extensoes.RemoveAcentuacoes(texto + "(" + desLogin + ")"),
                                                "55" + NU_CELULAR.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", ""),
                                                DateTime.Now.Ticks.ToString());

                    if ((int)sMSRequestReturn == 0)
                    {
                        admUsuario.QT_SMS_TOTAL_USR = admUsuario.QT_SMS_TOTAL_USR != null ? admUsuario.QT_SMS_TOTAL_USR + 1 : 1;
                        admUsuario.QT_SMS_MES_USR = admUsuario.QT_SMS_MES_USR != null ? admUsuario.QT_SMS_MES_USR + 1 : 1;
                        ADMUSUARIO.SaveOrUpdate(admUsuario, false);

                        tb249.FLA_SMS_SUCESS = "S";
                    }
                    else
                        tb249.FLA_SMS_SUCESS = "N";

                    tb249.CO_TP_CONTAT_SMS = "A";

                    if ((int)sMSRequestReturn == 13)
                        retorno = "Número do destinatário está incompleto ou inválido.";
                    else if ((int)sMSRequestReturn == 80)
                        retorno = "Já foi enviada uma mensagem de sua conta com o mesmo identificador.";
                    else if ((int)sMSRequestReturn == 900)
                        retorno = "Erro de autenticação em account e/ou code.";
                    else if ((int)sMSRequestReturn == 990)
                        retorno = "Seu limite de segurança foi atingido. Contate nosso suporte para verificação/liberação.";
                    else if ((int)sMSRequestReturn == 998)
                        retorno = "Foi invocada uma operação inexistente.";
                    else if ((int)sMSRequestReturn == 999)
                        retorno = "Erro desconhecido. Contate nosso suporte.";


                    tb249.ID_MENSAG_OPERAD = (int)sMSRequestReturn;

                    if ((int)sMSRequestReturn == 0)
                        tb249.CO_STATUS = "E";
                    else
                        tb249.CO_STATUS = "N";

                    TB249_MENSAG_SMS.SaveOrUpdate(tb249, false);
                }
                catch (Exception)
                {
                    retorno = "Mensagem não foi enviada com sucesso.";
                }
                //GestorEntities.CurrentContext.SaveChanges();
            }
        }

        /// <summary>
        /// Carrega as unidades de acordo com a Instituição logada.
        /// </summary>
        /// <param name="ddl"></param>
        private void CarregaUnidades(DropDownList ddl)
        {
            //AuxiliCarregamentos.CarregaUnidade(ddl, LoginAuxili.ORG_CODIGO_ORGAO, true);

            //Carrega apenas as unidades que possuem algum colaborador com FLAG de Profissional de Saúde
            var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                       join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb03.TB25_EMPRESA.CO_EMP equals tb25.CO_EMP
                       where tb03.FLA_PROFESSOR == "S"
                       select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy(w => w.NO_FANTAS_EMP).ToList();

            ddl.DataTextField = "NO_FANTAS_EMP";
            ddl.DataValueField = "CO_EMP";
            ddl.DataSource = res;
            ddl.DataBind();

            if (res.Count() > 0)
                ddl.Items.Insert(0, new ListItem("Todos", "0"));
            else
                ddl.Items.Insert(0, new ListItem("Sem Unidades com Plantonistas", ""));
        }

        /// <summary>
        /// Carrega os pacientes
        /// </summary>
        private void CarregaPacientes()
        {
            AuxiliCarregamentos.CarregaPacientes(ddlNomeUsu, LoginAuxili.CO_EMP, false);
        }

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
        /// Carrega o histórico de agendamentos do paciente recebido como parâmetro;
        /// </summary>
        /// <param name="CO_ALU"></param>
        private void CarregaGridHistoricoPaciente(int CO_ALU)
        {
            CarregaDatasIniFimPaciente(CO_ALU);
            DateTime dtIni = (!string.IsNullOrEmpty(txtDtIniHistoUsuar.Text) ? DateTime.Parse(txtDtIniHistoUsuar.Text) : DateTime.Now);
            DateTime dtFim = (!string.IsNullOrEmpty(txtDtFimHistoUsuar.Text) ? DateTime.Parse(txtDtFimHistoUsuar.Text) : DateTime.Now);
            int coCol = (!string.IsNullOrEmpty(ddlProfi.SelectedValue) ? int.Parse(ddlProfi.SelectedValue) : 0);

            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       //where tbs174.FL_JUSTI_CANCE != "M"
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                       where tbs174.CO_ALU == CO_ALU
                       && (EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) >= EntityFunctions.TruncateTime(dtIni)
                       && EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) <= EntityFunctions.TruncateTime(dtFim))
                       && (coCol != 0 ? tbs174.CO_COL == coCol : 0 == 0)
                       && (!String.IsNullOrEmpty(tbs174.FL_JUSTI_CANCE) ? tbs174.FL_JUSTI_CANCE != "M" : ""=="")
                       //&& (!String.IsNullOrEmpty(tbs174.FL_JUSTI_CANCE) ? tbs174.FL_JUSTI_CANCE != "M" && tbs174.FL_JUSTI_CANCE != "C" && tbs174.FL_JUSTI_CANCE != "P" : "" == "")
                       select new HorarioHistoricoPaciente
                       {
                           ID_AGEND_HORAR = tbs174.ID_AGEND_HORAR,
                           OPER = tbs174.TB250_OPERA != null ? tbs174.TB250_OPERA.NOM_OPER : " - ",
                           STATUS = tbs174.CO_SITUA_AGEND_HORAR,
                           TP_PROCED = tbs174.CO_TIPO_PROC_MEDI,
                           DT = tbs174.DT_AGEND_HORAR,
                           HR = tbs174.HR_AGEND_HORAR,
                           NO_PROFI = tb03.CO_MAT_COL + " - " + tb03.NO_COL,
                           UNID_COL = tb03.TB25_EMPRESA.sigla,
                           TEL_COL = tb03.NU_TELE_CELU_COL,
                           CO_CLASS = tb03.CO_CLASS_PROFI,
                           FL_CONFIR = tbs174.FL_CONF_AGEND, 
                           FL_CANCE_JUSTIF = tbs174.FL_JUSTI_CANCE,
                           FL_AGEND_ENCAM = tbs174.FL_AGEND_ENCAM,
                           //Só permite clicar quando a agenda estiver em aberto e presença diferente de sim

                           /* MAXWELL ALMEIDA
                            * 25/06/2015 - A PEDIDO DO CORDOVA, ESTE BLOCO FOI COMENTADO PARA SER REATIVADO POSTERIORMENTE
                            * 
                            */
                           //permiteClicar = (tbs174.CO_SITUA_AGEND_HORAR == "A" && tbs174.FL_CONF_AGEND != "S" ? true : false),
                           permiteClicar = true,
                       }).OrderBy(w => w.DT).ToList();

            var lst = new List<HorarioHistoricoPaciente>();

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

            grdHistorPaciente.DataSource = resNew;
            grdHistorPaciente.DataBind();
        }

        /// <summary>
        /// Carrega os procedimentos da instituição e seleciona o recebido como parâmetro
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="selec"></param>
        private void CarregaProcedimentos(DropDownList ddl, DropDownList ddlOper, DropDownList ddlGrupo, DropDownList ddlSubGrupo, string selec = null, bool insereVazio = false)
        {
            int idOper = (!string.IsNullOrEmpty(ddlOper.SelectedValue) ? int.Parse(ddlOper.SelectedValue) : 0);
            int grupo = (!string.IsNullOrEmpty(ddlGrupo.SelectedValue) ? int.Parse(ddlGrupo.SelectedValue) : 0);
            int Subgrupo = (!string.IsNullOrEmpty(ddlSubGrupo.SelectedValue) ? int.Parse(ddlSubGrupo.SelectedValue) : 0);
            var res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                       where tbs356.CO_SITU_PROC_MEDI == "A"
                       && (idOper != 0 ? tbs356.TB250_OPERA.ID_OPER == idOper : tbs356.TB250_OPERA == null)
                       && (grupo != 0 ? tbs356.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO == grupo : 0 == 0)
                       && (Subgrupo != 0 ? tbs356.TBS355_PROC_MEDIC_SGRUP.ID_PROC_MEDIC_SGRUP == Subgrupo : 0 == 0)
                       select new { tbs356.ID_PROC_MEDI_PROCE, CO_PROC_MEDI = tbs356.CO_PROC_MEDI + " - " + tbs356.NM_PROC_MEDI }).OrderBy(w => w.CO_PROC_MEDI).ToList();

            if (res != null)
            {
                ddl.Items.Clear();
                ddl.SelectedIndex = -1;
                ddl.SelectedValue = null;
                ddl.ClearSelection();

                ddl.DataTextField = "CO_PROC_MEDI";
                ddl.DataValueField = "ID_PROC_MEDI_PROCE";
                ddl.DataSource = res;
                ddl.DataBind();
            }

            if (insereVazio)
                ddl.Items.Insert(0, new ListItem("", ""));
            else
                ddl.Items.Insert(0, new ListItem("Selecione", ""));

            if (!string.IsNullOrEmpty(selec))
                ddl.SelectedValue = selec;
        }

        /// <summary>
        /// Sobrecarga do método que carrega as operadoras de plano de saúde já selecionando o valor recebido como parâmetro
        /// </summary>
        private void CarregaOperadoras(DropDownList ddl, string selec)
        {
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddl, false, false, true);

            if (!string.IsNullOrEmpty(selec))
                ddl.SelectedValue = selec;
        }

        /// <summary>
        /// Carrega os planos de saúde da operadora recebida como parâmetro
        /// </summary>
        /// <param name="ddlPlan"></param>
        /// <param name="ddlOper"></param>
        private void CarregarPlanosSaude(DropDownList ddlPlan, DropDownList ddlOper)
        {
            AuxiliCarregamentos.CarregaPlanosSaude(ddlPlan, ddlOper, false, false, true);
        }

        /// <summary>
        /// Executa método javascript que mostra a Modal com o histórico de ocorrências
        /// </summary>
        private void abreModalInfosCadastrais()
        {
            ScriptManager.RegisterStartupScript(
                this.Page,
                this.GetType(),
                "Acao",
                "AbreModalInfosCadas();",
                true
            );
        }

        /// <summary>
        /// Executa método javascript que mostra a Modal com o Registro de Informações Financeiras
        /// </summary>
        private void abreModalInfosFinanceiras()
        {
            ScriptManager.RegisterStartupScript(
                this.Page,
                this.GetType(),
                "Acao",
                "AbreModalInfosFinan();",
                true
            );

            //UpdFinanceiro.Update();
        }

        /// <summary>
        /// Carrega as datas de início e fim de consultas de um determinado profissional recebido como parâmetro
        /// </summary>
        private void CarregaDatasIniFim(int CO_COL)
        {
            //txtDtIniResCons.Text = DateTime.Now.AddMonths(-2).ToString();
            //txtDtFimResCons.Text = DateTime.Now.AddDays(3).ToString();
        }

        /// <summary>
        /// Carrega a primeira data de início e final para o paciente recebido como parâmetro
        /// </summary>
        /// <param name="CO_COL"></param>
        private void CarregaDatasIniFimPaciente(int CO_ALU)
        {
            //txtDtIniHistoUsuar.Text = DateTime.Now.AddMonths(-2).ToString();
            //txtDtFimHistoUsuar.Text = DateTime.Now.ToString();
            //var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
            //           where tbs174.CO_ALU == CO_ALU
            //           select new
            //           {
            //               tbs174.DT_AGEND_HORAR,
            //           }).ToList();

            ////Seta a primeira e última data de consultas do colaborador recebido como parâmetro
            //if (res.Count > 0)
            //{
            //    txtDtIniHistoUsuar.Text = (res != null ? res.FirstOrDefault().DT_AGEND_HORAR.ToString() : DateTime.Now.ToString());
            //    txtDtFimHistoUsuar.Text = (res != null ? res.LastOrDefault().DT_AGEND_HORAR.ToString() : DateTime.Now.ToString());
            //}
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
            //updCadasUsuario.Update();
        }

        /// <summary>
        /// Método que duplica as informações do responsável nos campos do paciente, quando o usuário clica em Paciente é o Responsável.
        /// </summary>
        private void carregaPaciehoResponsavel()
        {
            if (chkPaciEhResp.Checked)
            {
                txtCPFMOD.Text = txtCPFResp.Text;
                txtnompac.Text = txtNomeResp.Text;
                txtDtNascPaci.Text = txtDtNascResp.Text;
                ddlSexoPaci.SelectedValue = ddlSexResp.SelectedValue;
                txtTelCelPaci.Text = txtTelCelResp.Text;
                txtTelResPaci.Text = txtTelFixResp.Text;
                ddlGrParen.SelectedValue = "OU";
                txtEmailPaci.Text = txtEmailResp.Text;
                txtWhatsPaci.Text = txtNuWhatsResp.Text;

                txtEmailPaci.Enabled = false;
                txtCPFMOD.Enabled = false;
                txtnompac.Enabled = false;
                txtDtNascPaci.Enabled = false;
                ddlSexoPaci.Enabled = false;
                txtTelCelPaci.Enabled = false;
                txtTelCelPaci.Enabled = false;
                txtTelResPaci.Enabled = false;
                ddlGrParen.Enabled = false;
                txtWhatsPaci.Enabled = false;

                #region Verifica se já existe

                string cpf = txtCPFMOD.Text.Replace(".", "").Replace("-", "").Trim();

                var tb07 = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.NU_CPF_ALU == cpf).FirstOrDefault();

                if (tb07 != null)
                    hidCoPac.Value = tb07.CO_ALU.ToString();

                #endregion

            }
            else
            {
                txtCPFMOD.Text = "";
                txtnompac.Text = "";
                txtDtNascPaci.Text = "";
                ddlSexoPaci.SelectedValue = "";
                txtTelCelPaci.Text = "";
                txtTelResPaci.Text = "";
                ddlGrParen.SelectedValue = "";
                txtEmailPaci.Text = "";
                txtWhatsPaci.Text = "";

                txtCPFMOD.Enabled = true;
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
            //updCadasUsuario.Update();
        }

        /// <summary>
        /// Torna a primeira letra maiúscula
        /// </summary>
        private static string PrimeiraLetraMaiuscula(string tex)
        {
            if (string.IsNullOrEmpty(tex))
                return string.Empty;

            return tex.Substring(0, 1).ToUpper() + tex.Substring(1);
        }

        public class HorarioHistoricoPaciente
        {
            public int ID_AGEND_HORAR { get; set; }
            public string OPER { get; set; }
            public string TP_PROCED { get; set; }
            public string TP_PROCED_V
            {
                get
                {
                    switch (this.TP_PROCED)
                    {
                        case "CO":
                            return "Consulta";
                        case "EX":
                            return "Exame";
                        case "SS":
                            return "Serv.Saúde";
                        case "PR":
                            return "Procedimento";
                        case "SA":
                            return "Serv. Ambulatorial";
                        case "OU":
                            return "Outros";
                        default:
                            return " - ";
                    }
                }
            }
            public string PROCED
            {
                get
                {
                    //Quantidade de procedimentos associados à essa agenda
                    //var lst = TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros().Where(w => w.TBS174_AGEND_HORAR.ID_AGEND_HORAR == this.ID_AGEND_HORAR).ToList();
                    //if(lst.Count > 0)
                    //    return lst.DistinctBy(w => w.TBS386_ITENS_PLANE_AVALI.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE).Count().ToString("00");
                    //else
                    //    return "00";
                    return " - ";
                }
            }
            public string STATUS { get; set; }
            public DateTime DT { get; set; }
            public string HR { get; set; }
            public string DT_HORAR
            {
                get
                {
                    string diaSemana = this.DT.ToString("ddd", new CultureInfo("pt-BR"));
                    return this.DT.ToShortDateString() + " - " + this.HR + " " + diaSemana;
                }
            }
            public int? NR_ACAO_R { get; set; }
            public string NR_ACAO
            {
                get
                {
                    return (this.NR_ACAO_R.HasValue ? this.NR_ACAO_R.Value.ToString("00") : " - ");
                }
            }
            public string NO_PROFI { get; set; }
            public string UNID_COL { get; set; }
            public string CO_CLASS { get; set; }
            public string CO_CLASS_V
            {
                get
                {
                    return (AuxiliGeral.GetNomeClassificacaoFuncional(this.CO_CLASS));
                }
            }
            public string TEL_COL { get; set; }
            public string TEL_COL_V
            {
                get
                {
                    return AuxiliFormatoExibicao.PreparaTelefone(this.TEL_COL);
                }
            }
            public bool permiteClicar { get; set; }

            public string FL_CONFIR { get; set; }
            public string FL_CANCE_JUSTIF { get; set; }
            public string FL_AGEND_ENCAM { get; set; }
            public string IMG_URL
            {
                get
                {
                    return AuxiliFormatoExibicao.RetornarUrlImagemAgend(this.STATUS, this.FL_AGEND_ENCAM, this.FL_CONFIR, this.FL_CANCE_JUSTIF);
                }
            }
            public string IMG_TIP
            {
                get
                {
                    return AuxiliFormatoExibicao.RetornarToolTipImagemAgend(this.IMG_URL);
                }
            }
        }

        public class HorarioSaida
        {
            //Carrega informações gerais do agendamento
            public DateTime dt { get; set; }
            public string hr { get; set; }
            public TimeSpan hrC
            {
                get
                {
                    //DateTime d = DateTime.Parse(hr);
                    return TimeSpan.Parse((hr + ":00"));
                }
            }
            public string hora
            {
                get
                {
                    string diaSemana = this.dt.ToString("ddd", new CultureInfo("pt-BR"));
                    return this.dt.ToShortDateString() + " - " + this.hr + " " + diaSemana;
                }
            }
            public int CO_AGEND { get; set; }
            public int? CO_COL { get; set; }
            public string NM_PROCED { get; set; }
            public string hidSituaItemPlanej { get; set; }

            //Dados da Grid
            public int? ID_GRUPO { get; set; }
            public int? ID_SUB_GRUPO { get; set; }
            public int? ID_PROCED { get; set; }
            public int? NR_ACAO { get; set; }
            public string NR_ACAO_V
            {
                get
                {
                    return (this.NR_ACAO.HasValue ? this.NR_ACAO.Value.ToString("00") : "");
                }
            }
            public string DE_ACAO { get; set; }

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

            public int? CO_OPER { get; set; }
            public int? CO_PLAN { get; set; }
            public int? ID_PROC { get; set; }

            public int? ID_ITEM_PLAN { get; set; }
        }

        public enum ETipoClique
        {
            marcar,
            desmarcar,
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idAgend"></param>
        private void AlterarAgendasClicadas(int idAgend, ETipoClique etpc)
        {
            //Se houver na sessão, resgata, se não, cria nova
            List<int> list = (Session["arrayItensSelec"] != null ? (List<int>)Session["arrayItensSelec"] : new List<int>());

            //Verifica se o parâmetro foi marcado ou não, e inclui ou exclui de acordo
            if (etpc == ETipoClique.marcar)
                list.Add(idAgend);
            else
                list.Remove(idAgend); // Remove o item desmarcado

            Session["arrayItensSelec"] = list; // Atualiza a sessão com a lista com os valores alterados
        }

        #endregion

        #region Eventos de componentes

        protected void grdHorario_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            //--------> Criação do estilo e links das linhas da GRID
            if (e.Row.DataItem != null)
            {
                DropDownList ddlGrupo, ddlSubGrupo, ddlProced;
                ddlGrupo = (((DropDownList)e.Row.FindControl("ddlGrupo")));
                ddlSubGrupo = (((DropDownList)e.Row.FindControl("ddlSubGrupo")));
                ddlProced = (((DropDownList)e.Row.FindControl("ddlProcedimento")));

                string idGrupo, idSubGrupo, idProced, situa;
                idGrupo = (((HiddenField)e.Row.FindControl("hidIdGrupo")).Value);
                idSubGrupo = (((HiddenField)e.Row.FindControl("hidIdSubGrupo")).Value);
                idProced = (((HiddenField)e.Row.FindControl("hidIdProced")).Value);
                situa = (((HiddenField)e.Row.FindControl("hidSituaItemPlanej")).Value);

                //Carrega e seleciona grupo se houver
                CarregarGrupos(ddlGrupo, false, true, false);
                if (!string.IsNullOrEmpty(idGrupo))
                    ddlGrupo.SelectedValue = idGrupo;

                //Carrega e seleciona subgrupo se houver
                CarregarSubGrupos(ddlSubGrupo, ddlGrupo, false, true, false);
                if (!string.IsNullOrEmpty(idSubGrupo))
                    ddlSubGrupo.SelectedValue = idSubGrupo;

                //Carrega e seleciona procedimento se houver
                CarregaProcedimentos(ddlProced, ddlOperadora, ddlGrupo, ddlSubGrupo, idProced);

                List<int> itens = (List<int>)Session["arrayItensSelec"];
                if (itens != null) //Apenas se itens for diferente de nulo
                {
                    if (itens.Count == 1) //Apenas se um item estiver selecionado
                    {
                        if (situa == "C") //Apenas se estiver cancelado
                            e.Row.BackColor = System.Drawing.Color.AntiqueWhite;
                    }
                }
            }
        }

        protected void imgCpfResp_OnClick(object sender, EventArgs e)
        {
            PesquisaPaciente();
            //PesquisaCarregaResp(null);
        }

        protected void chkPesqNire_OnCheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                txtNirePaci.Enabled = true;
                chkPesqCpf.Checked = txtCPFPaci.Enabled = false;
                txtCPFPaci.Text = "";
            }
            else
            {
                txtNirePaci.Enabled = false;
                txtNirePaci.Text = "";
            }
        }

        protected void chkPesqCpf_OnCheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                txtCPFPaci.Enabled = true;
                chkPesqNire.Checked = txtNirePaci.Enabled = false;
                txtNirePaci.Text = "";
            }
            else
            {
                txtNirePaci.Enabled = false;
                txtNirePaci.Text = "";
            }
        }

        protected void imgCadPac_OnClick(object sender, EventArgs e)
        {
            divResp.Visible = true;
            divSuccessoMessage.Visible = false;
            //updCadasUsuario.Update();
            abreModalInfosCadastrais();
        }

        protected void lnkSalvarPaciente_OnClick(object sender, EventArgs e)
        {
            TB07_ALUNO tb07 = new TB07_ALUNO();

            //tb07.NO_ALU = txtNomeAluMod.Text;
            //tb07.NU_CPF_ALU = txtCpfAluMod.Text.Replace(".", "").Replace("-", "").Trim();
            //tb07.NU_NIS = (!string.IsNullOrEmpty(txtNisAluMod.Text) ? decimal.Parse(txtNisAluMod.Text) : (decimal?)null);
            //tb07.DT_NASC_ALU = DateTime.Parse(txtDataNascimentoAluMod.Text);
            //tb07.CO_SEXO_ALU = ddlSexoAluMod.SelectedValue;
            ////tb07.NU_TELE_CELU_ALU = txtTelCelUsu.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            ////tb07.NU_TELE_RESI_ALU = txtTelResUsu.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            ////tb07.CO_GRAU_PAREN_RESP = ddlGrauParen.SelectedValue;
            //tb07.CO_EMP = LoginAuxili.CO_EMP;
            //tb07.TB25_EMPRESA1 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
            //tb07.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
            ////tb07.TB108_RESPONSAVEL = (tb108 != null ? tb108 : null);

            ////Salva os valores para os campos not null da tabela de Usuário
            //tb07.CO_SITU_ALU = "A";
            //tb07.TP_DEF = "N";

            //#region trata para criação do nire

            //var resNire = (from tb07pesq in TB07_ALUNO.RetornaTodosRegistros()
            //               select new { tb07pesq.NU_NIRE }).OrderByDescending(w => w.NU_NIRE).FirstOrDefault();

            //int nir = 0;
            //if (resNire == null)
            //{
            //    nir = 1;
            //}
            //else
            //{
            //    nir = resNire.NU_NIRE;
            //}

            //int nirTot = nir + 1;

            //#endregion
            //tb07.NU_NIRE = nirTot;

            TB07_ALUNO.SaveOrUpdate(tb07, true);
        }

        protected void chkPaciEhResp_OnCheckedChanged(object sender, EventArgs e)
        {
            carregaPaciehoResponsavel();

            if (chkPaciEhResp.Checked)
                chkPaciMoraCoResp.Checked = true;

            abreModalInfosCadastrais();
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
            abreModalInfosCadastrais();
            //updCadasUsuario.Update();
        }

        protected void ddlUF_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaCidade();
            ddlCidade.Focus();
            abreModalInfosCadastrais();
        }

        protected void ddlCidade_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaBairro();
            ddlBairro.Focus();
            abreModalInfosCadastrais();
        }

        protected void lnkSalvar_OnClick(object sender, EventArgs e)
        {
            abreModalInfosCadastrais();
            if (string.IsNullOrEmpty(txtNuNisPaci.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "O Nis do Paciente é requerido");
                txtNuNisPaci.Focus();
                //updCadasUsuario.Update();
                return;
            }

            //Salva os dados do Responsável na tabela 108
            #region Salva Responsável na tb108

            TB108_RESPONSAVEL tb108;
            //Verifica se já não existe um responsável cadastrdado com esse CPF, caso não exista ele cadastra um novo com os dados informados
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
            if (string.IsNullOrEmpty(hidCoPac.Value))
            {
                tb07 = new TB07_ALUNO();

                #region Bloco foto
                int codImagem = upImagemAluno.GravaImagem();
                tb07.Image = C2BR.GestorEducacao.BusinessEntities.MSSQL.Image.RetornaPelaChavePrimaria(codImagem);
                #endregion

                tb07.NO_ALU = txtnompac.Text;
                tb07.NU_CPF_ALU = txtCPFMOD.Text.Replace(".", "").Replace("-", "").Trim();
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

            divResp.Visible = false;
            divSuccessoMessage.Visible = true;
            lblMsg.Text = "Usuário salvo com êxito!";
            lblMsgAviso.Text = "Clique no botão Fechar para voltar a tela inicial.";
            lblMsg.Visible = true;
            lblMsgAviso.Visible = true;

            CarregaPacientes();
            ddlNomeUsu.SelectedValue = tb07.CO_ALU.ToString();
            //updTopo.Update();

            #endregion
        }

        protected void ddlNomeUsu_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlNomeUsu.SelectedValue))
            {
                CarregaGridHistoricoPaciente(int.Parse(ddlNomeUsu.SelectedValue));

                var res = TB07_ALUNO.RetornaPeloCoAlu(int.Parse(ddlNomeUsu.SelectedValue));
                res.TB250_OPERAReference.Load();
                res.TB251_PLANO_OPERAReference.Load();

                //Carrega a operadora se houver
                CarregaOperadoras(ddlOperadora, "");
                if (res.TB250_OPERA != null)
                {
                    ddlOperadora.SelectedValue = res.TB250_OPERA.ID_OPER.ToString();
                }

                //Carrega o plano se houver
                CarregarPlanosSaude(ddlPlano, ddlOperadora);
                if (res.TB251_PLANO_OPERA != null)
                {
                    ddlPlano.SelectedValue = res.TB251_PLANO_OPERA.ID_PLAN.ToString();
                }

                //Recarrega os procedimentos de acordo com a operadora
                CarregaProcedimentos(ddlProcedimento, ddlOperadora, ddlGrupoPr, ddlSubGrupoPr, null, true);

                
                btnExcProcs.OnClientClick = "alert('É necessario selecionar pelo menos um procedimento!'); return false;";
                grdHorario.DataSource = Session["arrayItensSelec"] = null;
                grdHorario.DataBind();
            }
        }

        protected void imgPesqHistPaciente_OnClick(object sender, EventArgs e)
        {
            Session["arrayItensSelec"] = null;
            CarregaGridHistoricoPaciente(ddlNomeUsu.SelectedValue != "" ? int.Parse(ddlNomeUsu.SelectedValue) : 0);
        }

        protected void ddlGrupoPr_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarSubGrupos(ddlSubGrupoPr, ddlGrupoPr);

            //Cada item da grade de horários selecionado
            foreach (GridViewRow i in grdHorario.Rows)
            {
                CheckBox chk = (((CheckBox)i.FindControl("ckSelectHr")));
                if (chk.Checked) // Apenas nos marcados
                {
                    HiddenField hdIdItemPlan = (((HiddenField)i.FindControl("hidIdItemPlane")));

                    //Se não houver item de planejamento, replica o grupo selecionado no master
                    if (string.IsNullOrEmpty(hdIdItemPlan.Value))
                    {
                        //Seleciona o mesmo grupo selecionado no campo de cima
                        DropDownList ddlGrupo = (((DropDownList)i.FindControl("ddlGrupo")));
                        DropDownList ddlSubGrupo = (((DropDownList)i.FindControl("ddlSubGrupo")));
                        ddlGrupo.SelectedValue = ddlGrupoPr.SelectedValue;

                        CarregarSubGrupos(ddlSubGrupo, ddlGrupoPr, false, true, false);
                    }
                }
            }
        }

        protected void ddlSubGrupoPr_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaProcedimentos(ddlProcedimento, ddlOperadora, ddlGrupoPr, ddlSubGrupoPr, null, true);

            //Cada item da grade de horários selecionado
            foreach (GridViewRow i in grdHorario.Rows)
            {
                CheckBox chk = (((CheckBox)i.FindControl("ckSelectHr")));
                if (chk.Checked) // Apenas nos marcados
                {
                    HiddenField hdIdItemPlan = (((HiddenField)i.FindControl("hidIdItemPlane")));

                    //Se não houver item de planejamento, replica o subgrupo selecionado no master
                    if (string.IsNullOrEmpty(hdIdItemPlan.Value))
                    {
                        //Seleciona o mesmo subgrupo selecionado no campo de cima
                        DropDownList ddlGrupo = (((DropDownList)i.FindControl("ddlGrupo")));
                        DropDownList ddlSubGrupo = (((DropDownList)i.FindControl("ddlSubGrupo")));
                        DropDownList ddlProced = (((DropDownList)i.FindControl("ddlProcedimento")));
                        ddlSubGrupo.SelectedValue = ddlSubGrupoPr.SelectedValue;

                        CarregaProcedimentos(ddlProced, ddlOperadora, ddlSubGrupo, ddlGrupoPr, null, false);
                    }
                }
            }
        }

        protected void ddlProcedimento_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string nomeProced = "";
            //Coloca o nome do procedimento no campo
            if (!string.IsNullOrEmpty(ddlProcedimento.SelectedValue))
                txtDesProcedimento.Text = nomeProced = (TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlProcedimento.SelectedValue)).NM_PROC_MEDI);

            //Cada item da grade de horários selecionado
            foreach (GridViewRow i in grdHorario.Rows)
            {
                CheckBox chk = (((CheckBox)i.FindControl("ckSelectHr")));
                if (chk.Checked) // Apenas nos marcados
                {
                    HiddenField hdIdItemPlan = (((HiddenField)i.FindControl("hidIdItemPlane")));

                    //Se não houver item de planejamento, replica o procedimento selecionado no master
                    if (string.IsNullOrEmpty(hdIdItemPlan.Value))
                    {
                        //Seleciona o mesmo procedimento selecionado no campo de cima
                        DropDownList ddlProced = (((DropDownList)i.FindControl("ddlProcedimento")));
                        TextBox txtDesProced = (((TextBox)i.FindControl("txtDesProced")));

                        if (ddlProced.Items.Contains(new ListItem("", ddlProcedimento.SelectedValue)))
                            ddlProced.SelectedValue = ddlProcedimento.SelectedValue;

                        TextBox txtNrAcao = (TextBox)i.FindControl("txtNuAcao");
                        txtDesProced.Text = nomeProced;

                        //Calcula qual o próximo número para ação
                        #region Nº AÇÃO

                        //Insere no campo do número da ação, o próximo número da ação encontrado
                        //if (!string.IsNullOrEmpty(ddlProced.SelectedValue))
                        //    txtNrAcao.Text = RecuperaUltimoNrAcao(int.Parse(ddlNomeUsu.SelectedValue), int.Parse(ddlProced.SelectedValue)).ToString("00");

                        #endregion
                    }
                }
            }
        }

        protected void txtDeAcao_OnTextChanged(object sender, EventArgs e)
        {
            //Cada item da grade de horários selecionado
            foreach (GridViewRow i in grdHorario.Rows)
            {
                CheckBox chk = ((CheckBox)i.FindControl("ckSelectHr"));
                if (chk.Checked) // Apenas nos marcados
                {
                    //Seleciona o mesmo procedimento selecionado no campo de cima
                    TextBox txtDescrAcao = ((TextBox)i.FindControl("txtDesAcao"));
                    txtDescrAcao.Text = txtDeAcao.Text;
                }
            }
        }

        protected void ddlOperadora_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarPlanosSaude(ddlPlano, ddlOperadora);
            CarregaProcedimentos(ddlProcedimento, ddlOperadora, ddlGrupoPr, ddlSubGrupoPr, null, true);
        }

        protected void chkReplicar_OnCheckedChanged(object sender, EventArgs e)
        {
            //Marca ou desmarca de acordo com o selecionado
            foreach (GridViewRow i in grdHorario.Rows)
            {
                CheckBox chk = (((CheckBox)i.FindControl("ckSelectHr")));
                chk.Checked = chkReplicar.Checked;

                //Instancia os objetos
                DropDownList ddlGrupo = (((DropDownList)i.FindControl("ddlGrupo")));
                DropDownList ddlSubGrupo = (((DropDownList)i.FindControl("ddlSubGrupo")));
                DropDownList ddlProced = (((DropDownList)i.FindControl("ddlProcedimento")));
                TextBox txtDescrAcao = (((TextBox)i.FindControl("txtDesAcao")));
                TextBox txtDesProc = (((TextBox)i.FindControl("txtDesProced")));
                TextBox txtNrAcaoitem = (TextBox)i.FindControl("txtNuAcao");

                if (chk.Checked) //Se estiver marcando, replica os dados encontrados
                {
                    //Replica apenas se houver algo selecionado
                    if (!string.IsNullOrEmpty(ddlGrupoPr.SelectedValue))
                    {
                        ddlGrupo.SelectedValue = ddlGrupoPr.SelectedValue;
                        CarregarSubGrupos(ddlSubGrupo, ddlGrupoPr, false, true, false);
                    }

                    if (!string.IsNullOrEmpty(ddlSubGrupoPr.SelectedValue))
                    {
                        ddlSubGrupo.SelectedValue = ddlSubGrupoPr.SelectedValue;
                        CarregaProcedimentos(ddlProced, ddlOperadora, ddlGrupo, ddlSubGrupo, null, false);
                    }

                    if (!string.IsNullOrEmpty(ddlProcedimento.SelectedValue))
                        ddlProced.SelectedValue = ddlProcedimento.SelectedValue;

                    if (!string.IsNullOrEmpty(txtDesProcedimento.Text))
                        txtDesProc.Text = txtDesProcedimento.Text;

                    if (!string.IsNullOrEmpty(txtDeAcao.Text))
                        txtDescrAcao.Text = txtDeAcao.Text;

                    //Libera os campos
                    ddlGrupo.Enabled = ddlSubGrupo.Enabled = ddlProced.Enabled = txtDescrAcao.Enabled = true;
                    btnExcProcs.OnClientClick = "return confirm('Tem certeza de que deseja EXCLUIR o(s) item(ns) de planejamento selecionado(s) ?')";
                }
                else //Se estiver desmarcando, limpa os dados
                {
                    //Bloqueia os campos
                    ddlGrupo.Enabled = ddlSubGrupo.Enabled = ddlProced.Enabled = txtDescrAcao.Enabled = false;
                }
            }

            ddlGrupoPr.SelectedValue = ddlSubGrupoPr.SelectedValue = ddlProcedimento.SelectedValue
                = txtDesProcedimento.Text = txtNrAcao.Text = txtDeAcao.Text = "";
        }

        protected void ckSelectHr_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;

            foreach (GridViewRow l in grdHorario.Rows)
            {
                CheckBox chk = ((CheckBox)l.FindControl("ckSelectHr"));

                //Instancia os objetos
                DropDownList ddlGrupo = (((DropDownList)l.FindControl("ddlGrupo")));
                DropDownList ddlSubGrupo = (((DropDownList)l.FindControl("ddlSubGrupo")));
                DropDownList ddlProced = (((DropDownList)l.FindControl("ddlProcedimento")));
                TextBox txtDescrAcao = (((TextBox)l.FindControl("txtDesAcao")));
                TextBox txtNuAc = (((TextBox)l.FindControl("txtNuAcao")));
                TextBox txtDesProc = (((TextBox)l.FindControl("txtDesProced")));

                if (chk.ClientID == atual.ClientID)
                {
                    //Se estiver marcando, coloca os dados de acordo com os acima
                    if (chk.Checked == true)
                    {
                        //Replica apenas se houver algo selecionado
                        if (!string.IsNullOrEmpty(ddlGrupoPr.SelectedValue))
                        {
                            ddlGrupo.SelectedValue = ddlGrupoPr.SelectedValue;
                            CarregarSubGrupos(ddlSubGrupo, ddlGrupoPr, false, true, false);
                        }

                        if (!string.IsNullOrEmpty(ddlSubGrupoPr.SelectedValue))
                        {
                            ddlSubGrupo.SelectedValue = ddlSubGrupoPr.SelectedValue;
                            CarregaProcedimentos(ddlProced, ddlOperadora, ddlGrupo, ddlSubGrupo, null, false);
                        }

                        if (!string.IsNullOrEmpty(ddlProcedimento.SelectedValue))
                            ddlProced.SelectedValue = ddlProcedimento.SelectedValue;

                        if (!string.IsNullOrEmpty(txtDesProcedimento.Text))
                            txtDesProc.Text = txtDesProcedimento.Text;

                        if (!string.IsNullOrEmpty(txtDeAcao.Text))
                            txtDescrAcao.Text = txtDeAcao.Text;

                        //Libera os campos
                        ddlGrupo.Enabled = ddlSubGrupo.Enabled = ddlProced.Enabled = txtDescrAcao.Enabled = true;
                        btnExcProcs.OnClientClick = "return confirm('Tem certeza de que deseja EXCLUIR o(s) item(ns) de planejamento selecionado(s) ?')";
                    }
                    else
                    {
                        //Bloqueia os campos
                        ddlGrupo.Enabled = ddlSubGrupo.Enabled = ddlProced.Enabled = txtDescrAcao.Enabled = false;
                    }
                }
            }
        }

        protected void ddlGrupo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList atual = (DropDownList)sender;
            DropDownList ddl, ddlSubGrupo;

            if (grdHorario.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdHorario.Rows)
                {
                    ddl = (DropDownList)linha.FindControl("ddlGrupo");
                    ddlSubGrupo = (DropDownList)linha.FindControl("ddlSubGrupo");

                    //Atualiza o hidden do grupo
                    HiddenField hidGrupo = (((HiddenField)linha.FindControl("hidIdGrupo")));
                    hidGrupo.Value = ddl.SelectedValue;

                    //Atribui à session o id da central de regulação clicada para ser usada no popup que será aberto
                    if (ddl.ClientID == atual.ClientID)
                        CarregarSubGrupos(ddlSubGrupo, ddl, false, true, false);
                }
            }
        }
        
        protected void ddlSubGrupo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList atual = (DropDownList)sender;
            DropDownList ddl;

            if (grdHorario.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdHorario.Rows)
                {
                    ddl = (DropDownList)linha.FindControl("ddlSubGrupo");
                    DropDownList ddlGrupo = (DropDownList)linha.FindControl("ddlGrupo");
                    DropDownList ddlProced = (DropDownList)linha.FindControl("ddlProcedimento");

                    //Atualiza o hidden do subgrupo
                    HiddenField hidSubGrupo = (((HiddenField)linha.FindControl("hidIdSubGrupo")));
                    hidSubGrupo.Value = ddl.SelectedValue;

                    //Atribui à session o id da central de regulação clicada para ser usada no popup que será aberto
                    if (ddl.ClientID == atual.ClientID)
                        CarregaProcedimentos(ddlProced, ddlOperadora, ddlGrupo, ddl, null);
                }
            }
        }

        protected void ddlProcedimentoGr_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList atual = (DropDownList)sender;
            DropDownList ddl;

            if (grdHorario.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdHorario.Rows)
                {
                    ddl = (DropDownList)linha.FindControl("ddlProcedimento");
                    DropDownList ddlGrupo = (DropDownList)linha.FindControl("ddlGrupo");
                    DropDownList ddlSubGrupo = (DropDownList)linha.FindControl("ddlSubGrupo");
                    TextBox txtNrAcao = (TextBox)linha.FindControl("txtNuAcao");
                    TextBox txtDesProced = (TextBox)linha.FindControl("txtDesProced");

                    //Atualiza o hidden do procedimento
                    HiddenField hidProced = (((HiddenField)linha.FindControl("hidIdProced")));
                    hidProced.Value = ddl.SelectedValue;

                    //Atribui à session o id da central de regulação clicada para ser usada no popup que será aberto
                    if (ddl.ClientID == atual.ClientID)
                    {
                        if (!string.IsNullOrEmpty(ddl.SelectedValue))
                        {
                            txtDesProced.Text = (TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddl.SelectedValue)).NM_PROC_MEDI);

                            //Insere no campo do número da ação, o próximo número da ação encontrado
                            //txtNrAcao.Text = RecuperaUltimoNrAcao(int.Parse(ddlNomeUsu.SelectedValue), int.Parse(ddl.SelectedValue)).ToString("00");
                        }
                        else
                            txtNrAcao.Text = txtDesProced.Text = ""; // Limpa os dois campos caso esteja deselecionando o procedimento
                    }
                }
            }
        }

        protected void imgExc_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            if (grdHorario.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdHorario.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgExc");

                    if (img.ClientID == atual.ClientID)
                    {
                        var hidIdPlan = ((HiddenField)linha.FindControl("hidIdItemPlane")).Value;
                        if (!String.IsNullOrEmpty(hidIdPlan))
                        {
                            int idPlan = int.Parse(hidIdPlan);

                            var lstAssociacoes = TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros().Where(w => w.TBS386_ITENS_PLANE_AVALI.ID_ITENS_PLANE_AVALI == idPlan).ToList();
                            foreach (var ilsl in lstAssociacoes)
                            {
                                ilsl.TBS174_AGEND_HORARReference.Load();

                                if (ilsl.TBS174_AGEND_HORAR.CO_SITUA_AGEND_HORAR != "A")
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "Não é possivel excluir os procedimentos desse agendamento devido a sua situação!");
                                    return;
                                }

                                TBS389_ASSOC_ITENS_PLANE_AGEND.Delete(ilsl, true);

                                //logs de planejamentos associados
                                var lstPlanAssociados = TBS386_ITENS_PLANE_AVALI.RetornaTodosRegistros().Where(w => w.ID_ITENS_PLANE_AVALI == ilsl.ID_ASSOC_ITENS_PLANE_AGEND).ToList();
                                foreach (var pla in lstPlanAssociados)
                                    TBS386_ITENS_PLANE_AVALI.Delete(pla, true);
                            }
                        }

                        ExcluiItemGridDEFIN(new List<int>() { linha.RowIndex });

                        AuxiliPagina.EnvioAvisoGeralSistema(this.Page, string.Format("Item de Planejamento do procedimento {0} excluido com êxito!", (((TextBox)linha.FindControl("txtDesProced")).Text)));
                    }
                }
            }
        }

        protected void btnExcProcs_OnClick(object sender, EventArgs e)
        {
            if (grdHorario.Rows.Count != 0)
            {
                List<int> indices = new List<int>();

                foreach (GridViewRow linha in grdHorario.Rows)
                {
                    CheckBox chk = (((CheckBox)linha.FindControl("ckSelectHr")));

                    if (chk.Checked)
                    {
                        var hidIdPlan = ((HiddenField)linha.FindControl("hidIdItemPlane")).Value;
                        if (!String.IsNullOrEmpty(hidIdPlan))
                        {
                            int idPlan = int.Parse(hidIdPlan);

                            var lstAssociacoes = TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros().Where(w => w.TBS386_ITENS_PLANE_AVALI.ID_ITENS_PLANE_AVALI == idPlan).ToList();
                            foreach (var ilsl in lstAssociacoes)
                            {
                                ilsl.TBS174_AGEND_HORARReference.Load();

                                if (ilsl.TBS174_AGEND_HORAR.CO_SITUA_AGEND_HORAR != "A")
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "Não é possivel excluir os procedimentos desse agendamento devido a sua situação!");
                                    return;
                                }

                                TBS389_ASSOC_ITENS_PLANE_AGEND.Delete(ilsl, true);

                                //logs de planejamentos associados
                                var lstPlanAssociados = TBS386_ITENS_PLANE_AVALI.RetornaTodosRegistros().Where(w => w.ID_ITENS_PLANE_AVALI == ilsl.ID_ASSOC_ITENS_PLANE_AGEND).ToList();
                                foreach (var pla in lstPlanAssociados)
                                    TBS386_ITENS_PLANE_AVALI.Delete(pla, true);
                            }
                        }

                        indices.Add(linha.RowIndex);
                    }
                }

                indices.Reverse();

                ExcluiItemGridDEFIN(indices);

                AuxiliPagina.EnvioAvisoGeralSistema(this.Page, "Item(ns) de Planejamento excluido(s) com êxito!");
            }
        }
        
        protected void chkSelectHist_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;

            foreach (GridViewRow l in grdHistorPaciente.Rows)
            {
                CheckBox chk = ((CheckBox)l.FindControl("chkSelectHist"));
                int idAgend = int.Parse(((HiddenField)l.FindControl("hidIdAgend")).Value);

                if (chk.ClientID == atual.ClientID)
                {
                    if (chk.Checked)
                        AlterarAgendasClicadas(idAgend, ETipoClique.marcar);
                    else
                        AlterarAgendasClicadas(idAgend, ETipoClique.desmarcar);
                }
            }

            CarregaGridHorario();
        }

        protected void ChkTodos_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkMarca = ((CheckBox)grdHistorPaciente.HeaderRow.FindControl("chkMarcaTodosItens"));

            foreach (GridViewRow l in grdHistorPaciente.Rows)
            {
                CheckBox ckItem = ((CheckBox)l.FindControl("chkSelectHist"));

                //Apenas marca/desmarca se estiver habilitado
                if (ckItem.Enabled)
                    ckItem.Checked = chkMarca.Checked; //Altera seleção de acordo com "Master"

                int idAgend = int.Parse(((HiddenField)l.FindControl("hidIdAgend")).Value);

                if (chkMarca.Checked)
                    AlterarAgendasClicadas(idAgend, ETipoClique.marcar);
                else
                    AlterarAgendasClicadas(idAgend, ETipoClique.desmarcar);
            }

            CarregaGridHorario();
        }

        protected void btnMaisItens_OnClick(object sender, EventArgs e)
        {
            List<int> itens = (List<int>)Session["arrayItensSelec"];
            if (itens == null) //Apenas se itens for igual a nulo
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Não é possível inserir procedimentos sem uma ou mais datas selecionada(s)");
                return;
            }

            CriaNovaLinhaGridDEFIN();
        }

        #endregion
    }
}