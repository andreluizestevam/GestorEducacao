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

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8230_AvalTecnicaPlanejamento
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaProfissionais();
                CarregaGrupos();
                CarregaSubGrupos();
                CarregaSolicitacoes();
                CarregaQuestionarios();

                grdHistorico.DataSource = null;
                grdHistorico.DataBind();
            }
        }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (string.IsNullOrEmpty(hidIdItem.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o item de solicitação para o qual deseja associar as fichas e o planejamento");
                grdSolicitacoes.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtDtInicio.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a data de início da Ação");
                txtDtInicio.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtDtPrevTerm.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a data de Término prevista para a Ação");
                txtDtPrevTerm.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtQtSessoes.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a quantidade de sessões para a Ação");
                txtQtSessoes.Focus();
                return;
            }

            var tbs368 = TBS368_RECEP_SOLIC_ITENS.RetornaPelaChavePrimaria(int.Parse(hidIdItem.Value));

            tbs368.CO_SITUA = "F";
            tbs368.CO_COL_SITUA = LoginAuxili.CO_COL;
            tbs368.CO_EMP_COL_SITUA = TB03_COLABOR.RetornaTodosRegistros().Where(w => w.CO_COL == LoginAuxili.CO_COL).FirstOrDefault().CO_EMP;
            tbs368.CO_EMP_SITUA = LoginAuxili.CO_EMP;
            tbs368.DT_SITUA = DateTime.Now;
            tbs368.IP_SITUA = Request.UserHostAddress;

            //Salva a ação para o procedimento em questão
            var tbs370 = new TBS370_PLANO_ACAO_PROCE();
            tbs370.TBS368_RECEP_SOLIC_ITENS1 = TBS368_RECEP_SOLIC_ITENS.RetornaPelaChavePrimaria(tbs368.ID_RECEP_SOLIC_ITENS);
            tbs370.DT_INICIO = DateTime.Parse(txtDtInicio.Text);
            tbs370.DT_PREV_TERM = DateTime.Parse(txtDtPrevTerm.Text);
            tbs370.QT_SESSO = int.Parse(txtQtSessoes.Text);
            tbs370.DE_ACAO = txtAcao.Text;

            //======================> Dados do Cadastro
            tbs370.DT_CADAS = DateTime.Now;
            tbs370.CO_EMP_CADAS = LoginAuxili.CO_EMP;
            tbs370.CO_COL_CADAS = LoginAuxili.CO_COL;
            tbs370.CO_EMP_COL_CADAS = TB03_COLABOR.RetornaTodosRegistros().Where(w => w.CO_COL == LoginAuxili.CO_COL).FirstOrDefault().CO_EMP;
            tbs370.IP_CADAS = Request.UserHostAddress;

            //======================> Dados da Situação
            tbs370.CO_SITUA = "A";
            tbs370.CO_EMP_SITUA = LoginAuxili.CO_EMP;
            tbs370.CO_COL_SITUA = LoginAuxili.CO_COL;
            tbs370.CO_EMP_COL_SITUA = TB03_COLABOR.RetornaTodosRegistros().Where(w => w.CO_COL == LoginAuxili.CO_COL).FirstOrDefault().CO_EMP;
            tbs370.IP_SITUA = Request.UserHostAddress;
            tbs370.DT_SITUA = DateTime.Now;

            TBS370_PLANO_ACAO_PROCE.SaveOrUpdate(tbs370, true);

            //Atualiza o item com o id do plano de ação
            tbs368.TBS370_PLANO_ACAO_PROCE = tbs370;
            TBS368_RECEP_SOLIC_ITENS.SaveOrUpdate(tbs368, true);

            #region Persiste as Avaliações

            foreach (GridViewRow r in grdQuestionario.Rows)
            {

            }

            #endregion

            AuxiliPagina.RedirecionaParaPaginaSucesso("Plano de ação e análise técnica associados com sucesso ao item de procedimento!", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Exclui item da grid recebendo como parâmetro o index correspondente
        /// </summary>
        protected void ExcluiItemGrid(int Index)
        {
            DataTable dtV = new DataTable();
            DataColumn dcATM;

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "NO_TITULO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "NO_TIPO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "ID_AVALIACAO";
            dtV.Columns.Add(dcATM);

            DataRow linha;
            foreach (GridViewRow li in grdQuestionario.Rows)
            {
                linha = dtV.NewRow();
                linha["NO_TITULO"] = li.Cells[0].Text;
                linha["NO_TIPO"] = li.Cells[1].Text;
                linha["ID_AVALIACAO"] = ((HiddenField)li.Cells[3].FindControl("hidIdAvaliacao")).Value;
                dtV.Rows.Add(linha);
            }

            dtV.Rows.RemoveAt(Index); // Exclui o item de index correspondente
            HttpContext.Current.Session.Add("GridAvalTecnica", dtV);

            carregaGridNovaComContexto();
        }

        /// <summary>
        /// Cria uma nova linha na forma de pagamento cheque
        /// </summary>
        protected void CriaNovaLinhaGridSolicitacoes()
        {
            DataTable dtV = new DataTable();
            DataColumn dcATM;

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "NO_TITULO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "NO_TIPO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "ID_AVALIACAO";
            dtV.Columns.Add(dcATM);

            DataRow linha;
            foreach (GridViewRow li in grdSolicitacoes.Rows)
            {
                linha = dtV.NewRow();
                linha["NO_TITULO"] = li.Cells[0].Text;
                linha["NO_TIPO"] = li.Cells[1].Text;
                linha["ID_AVALIACAO"] = ((HiddenField)li.Cells[3].FindControl("hidIdAvaliacao")).Value;
                dtV.Rows.Add(linha);
            }

            int idAval = int.Parse(ddlAvaliacoes.SelectedValue);
            var res = (from tb201 in TB201_AVAL_MASTER.RetornaTodosRegistros()
                       join tb73 in TB73_TIPO_AVAL.RetornaTodosRegistros() on tb201.CO_TIPO_AVAL equals tb73.CO_TIPO_AVAL
                       where tb201.NU_AVAL_MASTER == idAval
                       select new
                       {
                           tb201.NM_TITU_AVAL,
                           tb73.NO_TIPO_AVAL,
                       }).FirstOrDefault();

            linha = dtV.NewRow();
            linha["NO_TITULO"] = res.NM_TITU_AVAL;
            linha["NO_TIPO"] = res.NO_TIPO_AVAL;
            linha["ID_AVALIACAO"] = idAval;
            dtV.Rows.Add(linha);

            HttpContext.Current.Session.Add("GridAvalTecnica", dtV);

            carregaGridNovaComContexto();
        }

        /// <summary>
        /// Carrega o a DataTable em Session com as informações anteriores e a nova linha.
        /// </summary>
        protected void carregaGridNovaComContexto()
        {
            DataTable dtV = new DataTable();

            dtV = (DataTable)Session["GridAvalTecnica"];

            grdQuestionario.DataSource = dtV;
            grdQuestionario.DataBind();
        }

        /// <summary>
        /// Carrega os questionários disponíveis
        /// </summary>
        private void CarregaQuestionarios()
        {
            string coClassCol = "0";
            if (!string.IsNullOrEmpty(ddlProfiSaude.SelectedValue) && ddlProfiSaude.SelectedValue != "0")
            {
                int col = (int.Parse(ddlProfiSaude.SelectedValue));
                coClassCol = TB03_COLABOR.RetornaTodosRegistros().Where(w => w.CO_COL == col).FirstOrDefault().CO_CLASS_PROFI;
            }

            var res = (from tb201 in TB201_AVAL_MASTER.RetornaTodosRegistros()
                       join tb73 in TB73_TIPO_AVAL.RetornaTodosRegistros() on tb201.CO_TIPO_AVAL equals tb73.CO_TIPO_AVAL
                       where (coClassCol != "0" ? tb73.FL_CLASS_PROFI == coClassCol : 0 == 0)
                       && tb201.STATUS == "A"
                       && tb73.CO_ESTI_AVAL == "A"
                       select new
                       {
                           nome = tb201.NM_TITU_AVAL + " - " + tb73.NO_TIPO_AVAL,
                           id = tb201.NU_AVAL_MASTER
                       }).OrderBy(w => w.nome).ToList();

            ddlAvaliacoes.DataTextField = "nome";
            ddlAvaliacoes.DataValueField = "id";
            ddlAvaliacoes.DataSource = res;
            ddlAvaliacoes.DataBind();
        }

        /// <summary>
        /// Carrega os profissionais de saúde
        /// </summary>
        private void CarregaProfissionais()
        {
            AuxiliCarregamentos.carregaProfessores(ddlProfiSaude, LoginAuxili.CO_EMP, true, true);

            if ((LoginAuxili.FLA_PROFESSOR == "S"))
            {
                ListItem it = ddlProfiSaude.Items.FindByValue(LoginAuxili.CO_COL.ToString());
                if (it != null) // Garante que só vai selecionar se houver o colaborador que se deseja selecionar
                {
                    ddlProfiSaude.SelectedValue = LoginAuxili.CO_COL.ToString();

                    if (LoginAuxili.CLASSIFICACAO_USU_LOGADO != "M") // Se não for Master, então bloqueia a seleção do profissional
                        ddlProfiSaude.Enabled = false;
                }
                else
                {
                    ddlProfiSaude.Items.Insert(0, new ListItem("Profissional não associado à unidade logada."));

                    if (LoginAuxili.CLASSIFICACAO_USU_LOGADO != "M") // Se não for Master, então bloqueia a seleção do profissional
                        ddlProfiSaude.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Carrega os subgrupos de procedimentos
        /// </summary>
        private void CarregaSubGrupos()
        {
            AuxiliCarregamentos.CarregaSubGruposProcedimentosMedicos(ddlSubGrupo, ddlGrupoProc, true);
        }

        /// <summary>
        /// Carrega os grupos de procedimentos
        /// </summary>
        private void CarregaGrupos()
        {
            AuxiliCarregamentos.CarregaGruposProcedimentosMedicos(ddlGrupoProc, true);
        }

        /// <summary>
        /// Carrega as solicitacoes em aberto 
        /// </summary>
        private void CarregaSolicitacoes()
        {
            int grupo = (!string.IsNullOrEmpty(ddlGrupoProc.SelectedValue) ? int.Parse(ddlGrupoProc.SelectedValue) : 0);
            int subGr = (!string.IsNullOrEmpty(ddlSubGrupo.SelectedValue) ? int.Parse(ddlSubGrupo.SelectedValue) : 0);
            int coCol = (!string.IsNullOrEmpty(ddlProfiSaude.SelectedValue) ? int.Parse(ddlProfiSaude.SelectedValue) : 0);

            var res = (from tbs368 in TBS368_RECEP_SOLIC_ITENS.RetornaTodosRegistros()
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs368.TBS367_RECEP_SOLIC.CO_ALU equals tb07.CO_ALU
                       where tbs368.CO_SITUA == "E"
                           //Só filtra pelo colaborador, caso a recepção tenha sido encaminhada para alguém realizar a
                           //análise e direcionar para algum profissional
                       && (tbs368.TBS367_RECEP_SOLIC.CO_COL_ANALI != (int?)null ? (coCol != 0 ? tbs368.CO_COL_OBJET_ANALI == coCol : 0 == 0) : 0 == 0)
                       && (grupo != 0 ? tbs368.TBS356_PROC_MEDIC_PROCE.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO == grupo : 0 == 0)
                       && (subGr != 0 ? tbs368.TBS356_PROC_MEDIC_PROCE.TBS355_PROC_MEDIC_SGRUP.ID_PROC_MEDIC_SGRUP == subGr : 0 == 0)
                       select new saidaSolicitacoes
                       {
                           OPERADORA = (tbs368.TB250_OPERA != null ? tbs368.TB250_OPERA.NOM_OPER : " - "),
                           NU_GUIA = (!string.IsNullOrEmpty(tbs368.NU_GUIA) ? tbs368.NU_GUIA : " - "),
                           QTDE_R = tbs368.NU_QTDE_SESSO,
                           PROCEDIMENTO = tbs368.TBS356_PROC_MEDIC_PROCE.CO_PROC_MEDI + " - " + tbs368.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
                           DT = tbs368.DT_CADAS,
                           ID_ITEM_SOLIC = tbs368.ID_RECEP_SOLIC_ITENS,
                           NO_PACIENTE = tb07.NO_ALU,
                           NU_NIRE = tb07.NU_NIRE,
                           DE_OBSER = tbs368.DE_OBSER,
                           NU_REGIS_R = tbs368.NU_REGIS_RECEP_SOLIC,
                           DT_REGIS = tbs368.DT_CADAS,
                       }).OrderBy(w => w.DT).ToList();

            grdSolicitacoes.DataSource = res;
            grdSolicitacoes.DataBind();
        }

        public class saidaSolicitacoes
        {
            public string NU_REGIS_R { get; set; }
            public DateTime DT_REGIS { get; set; }
            public string NU_REGIS
            {
                get
                {
                    return (this.DT_REGIS.ToString("dd/MM/yy") + " - " + this.NU_REGIS_R);
                }
            }
            public string OPERADORA { get; set; }
            public string NU_GUIA { get; set; }
            public int? QTDE_R { get; set; }
            public string QTDE
            {
                get
                {
                    return (this.QTDE_R.HasValue ? this.QTDE_R.ToString().PadLeft(2, '0') : " - ");
                }
            }
            public string PROCEDIMENTO { get; set; }
            public DateTime DT { get; set; }
            public int ID_ITEM_SOLIC { get; set; }
            public string NO_PACIENTE { get; set; }
            public int NU_NIRE { get; set; }
            public string PACIENTE
            {
                get
                {
                    return (this.NU_NIRE.ToString().PadLeft(7, '0') + " - " + this.NO_PACIENTE);
                }
            }
            public string DE_OBSER { get; set; }
        }

        #endregion

        #region Funções de Campo

        protected void chkselectSolic_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            CheckBox chk;

            // Valida se a grid de atividades possui algum registro
            if (grdSolicitacoes.Rows.Count != 0)
            {
                // Passa por todos os registros da grid de atividades
                foreach (GridViewRow linha in grdSolicitacoes.Rows)
                {
                    chk = (CheckBox)linha.Cells[0].FindControl("chkselectSolic");

                    // Desmarca todos os registros menos o que foi clicado
                    if (chk.ClientID != atual.ClientID)
                        chk.Checked = false;
                    else
                    {
                        if (chk.Checked)
                        {
                            hidIdItem.Value = (((HiddenField)linha.Cells[0].FindControl("hidItemSolic")).Value);

                            //Guarda a FLAG para saber se o Checkbox está sendo clicado para marcar ou desmarcar, neste caso, ele grava como para marcar.
                            HttpContext.Current.Session.Add("FL_Select_Grid_ITSOLIC", "S");

                            //Guarda o Valor do Pré-Atendimento, para fins de posteriormente comparar este valor 
                            HttpContext.Current.Session.Add("VL_ITEM_SOLIC_SELEC", hidIdItem.Value);
                        }
                        else
                        {
                            hidIdItem.Value = "";
                            HttpContext.Current.Session.Remove("FL_Select_Grid_ITSOLIC");
                            HttpContext.Current.Session.Remove("VL_ITEM_SOLIC_SELEC");
                        }
                    }
                }
            }
        }

        protected void ddlGrupoProc_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubGrupos();
        }

        protected void imgPesqProcedimentos_OnClick(object sender, EventArgs e)
        {
            CarregaSolicitacoes();
        }

        protected void btnAddForm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlAvaliacoes.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar a Avaliação que deseja associar!");
                return;
            }

            string avalddl = ddlAvaliacoes.SelectedValue;
            foreach (GridViewRow i in grdQuestionario.Rows)
            {
                if (((HiddenField)i.Cells[3].FindControl("hidIdAvaliacao")).Value == avalddl)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "A Avaliação que está tentando adicionar, já está presente na grade!");
                    return;
                }
            }

            CriaNovaLinhaGridSolicitacoes();
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
                    img = (ImageButton)linha.Cells[3].FindControl("imgExc");

                    if (img.ClientID == atual.ClientID)
                        aux = linha.RowIndex;
                }
            }
            ExcluiItemGrid(aux);
        }

        #endregion
    }
}