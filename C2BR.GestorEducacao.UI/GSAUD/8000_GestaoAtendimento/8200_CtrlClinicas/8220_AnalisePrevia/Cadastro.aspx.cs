//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE LETIVO DE AULAS E ATIVIDADES
// OBJETIVO: LANÇAMENTO DAS ATIVIDADES LETIVAS REALIZADAS PELO PROFESSOR
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 08/09/2017| Diogo Gomes                | Adaptação do método "CarregaSolicitacoes()" 
//           |                            | para obter os dados da tbs372
// ----------+----------------------------+-------------------------------------


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

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8220_AnalisePrevia
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
                IniPeri.Text = DateTime.Now.AddDays(-3).ToString();
                FimPeri.Text = DateTime.Now.ToString();
                AuxiliCarregamentos.CarregaClassificacoesFuncionais(ddlClassProfi, true);
                CarregaGrupos();
                CarregaSubGrupos();
                CarregaSolicitacoes();
                CarregaProfissionais();
            }
        }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            PersistirAnalisePrevia(EStatusAnalisePrevia.EmAnalise);
        }

        void CurrentPadraoCadastro_OnDelete(System.Data.Objects.DataClasses.EntityObject entity)
        {
            if (string.IsNullOrEmpty(hidIdItem.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o item de solicitação que deseja cancelar");
                grdSolicitacoes.Focus();
                return;
            }

            var tbs368 = TBS368_RECEP_SOLIC_ITENS.RetornaPelaChavePrimaria(int.Parse(hidIdItem.Value));
            tbs368.CO_SITUA = "C";
            tbs368.CO_COL_SITUA = LoginAuxili.CO_COL;
            tbs368.CO_EMP_COL_SITUA = TB03_COLABOR.RetornaTodosRegistros().Where(w => w.CO_COL == LoginAuxili.CO_COL).FirstOrDefault().CO_EMP;
            tbs368.CO_EMP_SITUA = LoginAuxili.CO_EMP;
            tbs368.DT_SITUA = DateTime.Now;
            tbs368.IP_SITUA = Request.UserHostAddress;
            TBS368_RECEP_SOLIC_ITENS.SaveOrUpdate(tbs368, true);
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Salva o item de análise prévia de acordo com o tipo recebido como parâmetro
        /// </summary>
        /// <param name="status"></param>
        private void PersistirAnalisePrevia(EStatusAnalisePrevia status)
        {
            if (string.IsNullOrEmpty(hidIdItem.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o item de solicitação para o qual deseja determinar o Profissional de Saúde");
                grdSolicitacoes.Focus();
                return;
            }

            ///Só precisa testar se não selecionou o colaborador, se estiver encaminhando
            if (status == EStatusAnalisePrevia.Encaminhado)
            {
                if (string.IsNullOrEmpty(hidCoCol.Value))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o Profissional de Saúde que deseja associar ao item de solicitação selecionado");
                    grdProfissionais.Focus();
                    return;
                }
            }

            string OBS = "";
            #region Coleta a Observacao

            foreach (GridViewRow gr in grdSolicitacoes.Rows)
            {
                if (((CheckBox)gr.Cells[0].FindControl("chkselectSolic")).Checked)
                {
                    OBS = (((TextBox)gr.Cells[9].FindControl("txtObservacao")).Text);
                    break;
                }
            }

            #endregion

            string st = status.ToString();
            string ste = status.GetValue();

            //var tbs368 = TBS368_RECEP_SOLIC_ITENS.RetornaPelaChavePrimaria(int.Parse(hidIdItem.Value));
            var tbs372 = TBS372_AGEND_AVALI.RetornaPelaChavePrimaria(int.Parse(hidIdItem.Value));
            //tbs368.CO_COL_OBJET_ANALI = (!string.IsNullOrEmpty(hidCoCol.Value) ? int.Parse(hidCoCol.Value) : (int?)null);
            //tbs368.CO_EMP_OBJET_ANALI = (!string.IsNullOrEmpty(hidCoEmpCol.Value) ? int.Parse(hidCoEmpCol.Value) : (int?)null);

            //tbs368.DT_ANALI_PREVI = DateTime.Now;
            tbs372.DE_OBSER_NECES = (!string.IsNullOrEmpty(OBS) ? OBS : null);
            tbs372.CO_SITUA = (status == EStatusAnalisePrevia.EmAberto ? "A" : status == EStatusAnalisePrevia.EmAnalise ?
                "N" : status == EStatusAnalisePrevia.Encaminhado ? "E" : status == EStatusAnalisePrevia.Cancelado ? "C" : "A");
            tbs372.CO_COL_SITUA = LoginAuxili.CO_COL;
            tbs372.CO_EMP_COL_SITUA = TB03_COLABOR.RetornaTodosRegistros().FirstOrDefault(w => w.CO_COL == LoginAuxili.CO_COL).CO_EMP;
            tbs372.CO_EMP_SITUA = LoginAuxili.CO_EMP;
            tbs372.DT_SITUA = DateTime.Now;
            tbs372.IP_SITUA = Request.UserHostAddress;
           
            TBS372_AGEND_AVALI.SaveOrUpdate(tbs372, true);

            HttpContext.Current.Session.Remove("FL_Select_Grid_ITSOLIC");
            HttpContext.Current.Session.Remove("VL_ITEM_SOLIC_SELEC");

            AuxiliPagina.RedirecionaParaPaginaSucesso("Análise salva com Sucesso!", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
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
            DateTime dtIni = (!string.IsNullOrEmpty(IniPeri.Text) ? DateTime.Parse(IniPeri.Text) : DateTime.Now);
            DateTime dtFim = (!string.IsNullOrEmpty(FimPeri.Text) ? DateTime.Parse(FimPeri.Text) : DateTime.Now);
        
            var res = (from tbs372 in TBS372_AGEND_AVALI.RetornaTodosRegistros()
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs372.CO_ALU equals tb07.CO_ALU
                       join tbs373 in TBS373_AGEND_AVALI_ITENS.RetornaTodosRegistros() on tbs372.ID_AGEND_AVALI equals tbs373.TBS372_AGEND_AVALI.ID_AGEND_AVALI
                       where
                           // tbs368.CO_SITUA != "C" //Não precisa ver cancelados
                           //&& tbs368.CO_SITUA != "E" //Não precisa ver encaminhados
                         
                   ((EntityFunctions.TruncateTime(tbs372.DT_AGEND) >= EntityFunctions.TruncateTime(dtIni))
                   && (EntityFunctions.TruncateTime(tbs372.DT_AGEND) <= EntityFunctions.TruncateTime(dtFim)))
                       && (ddlSituacao.SelectedValue != "0" ? tbs372.CO_SITUA == ddlSituacao.SelectedValue : 0 == 0)
                       select new saidaSolicitacoes
                       {
                           QTDE = tbs373.QT_SESSO,
                           PROCEDIMENTO = tbs373.TBS356_PROC_MEDIC_PROCE.CO_PROC_MEDI + " - " + tbs373.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
                           DT = tbs372.DT_CADAS,
                           ID_ITEM_SOLIC = tbs372.ID_AGEND_AVALI,
                           STATUS = (tbs372.CO_SITUA == "A" ? "Em Aberto" : tbs372.CO_SITUA == "E" ? "Encaminhado" :
                           tbs372.CO_SITUA == "N" ? "Em Análise" : " - "),
                           OBSERVACAO = tbs372.DE_OBSER_NECES,
                           //CO_COL_OBJET_ANALI = tbs368.CO_COL_OBJET_ANALI,

                           PACIENTE_R = tb07.NO_ALU,
                           NU_NIRE = tb07.NU_NIRE,
                           DT_NASC_PAC = tb07.DT_NASC_ALU,
                           SX = tb07.CO_SEXO_ALU,
                           TP_DEF = tb07.TP_DEF,
                           //NU_REGIS = tbs367.NU_REGIS_RECEP_SOLIC,
                       }).OrderBy(w => w.DT).ToList();

            grdSolicitacoes.DataSource = res;
            grdSolicitacoes.DataBind();
        }

        public class saidaSolicitacoes
        {
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
        /// Carrega os profissionais de saúde
        /// </summary>
        private void CarregaProfissionais()
        {
            string classProfi = (ddlClassProfi.SelectedValue);
            string Nome = txtNome.Text;

            var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                       join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tb03.CO_ESPEC equals tb63.CO_ESPECIALIDADE into l1
                       from lesp in l1.DefaultIfEmpty()
                       join tb20 in TB20_TIPOCON.RetornaTodosRegistros() on tb03.CO_TPCON equals tb20.CO_TPCON into l2
                       from lcontr in l2.DefaultIfEmpty()
                       join tb21 in TB21_TIPOCAL.RetornaTodosRegistros() on tb03.CO_TPCAL equals tb21.CO_TPCAL into l3
                       from ltpgto in l3.DefaultIfEmpty()
                       where tb03.FLA_PROFESSOR == "S"
                       && tb03.CO_SITU_COL == "ATI"
                       && tb03.CO_CLASS_PROFI != null // Só trás aqueles que tiverem uma classificação funcional
                       && (classProfi != "0" ? tb03.CO_CLASS_PROFI == classProfi : 0 == 0)
                       && (!string.IsNullOrEmpty(Nome) ? tb03.NO_COL.Contains(Nome) : 0 == 0)
                       select new saidaProfissionais
                       {
                           NO_COL = tb03.NO_COL,
                           MATRICULA = tb03.CO_MAT_COL,
                           CO_COL = tb03.CO_COL,
                           CO_CLASS_PROFI = tb03.CO_CLASS_PROFI,
                           CO_EMP = tb03.CO_EMP,
                           SX = tb03.CO_SEXO_COL,
                           DT_NASC = tb03.DT_NASC_COL,
                           NU_TEL = tb03.NU_TELE_CELU_COL,
                           EMAIL = tb03.CO_EMAI_COL,
                           NO_ESPEC = lesp != null ? lesp.NO_SIGLA_ESPECIALIDADE : " - ",
                           TP_CONTR = (lcontr != null ? lcontr.NO_TPCON : " - "),
                           NO_TPCAL = (ltpgto != null ? ltpgto.NO_TPCAL : " - "),
                           VL_SALAR_COL_R = tb03.VL_SALAR_COL,
                       }).OrderBy(w => w.NO_COL).ToList();

            grdProfissionais.DataSource = res;
            grdProfissionais.DataBind();
        }

        /// <summary>
        /// Seleciona o colaborador recebido como parâmetro na grid
        /// </summary>
        private void SelecionaColaborador(string col)
        {
            if (!string.IsNullOrEmpty(col))
            {
                foreach (GridViewRow li in grdProfissionais.Rows)
                {
                    string colAtuGrid = (((HiddenField)li.Cells[0].FindControl("hidCoCol")).Value);

                    if (col == colAtuGrid)
                    {
                        CheckBox chkCol = (((CheckBox)li.Cells[0].FindControl("chkselect")));
                        chkCol.Checked = true;
                    }
                }
            }
        }

        /// <summary>
        /// Devido ao método de reload na grid de Pré-Atendimento, ela perde a seleção do checkbox, este método coleta 
        /// </summary>
        private void selecionaGrid()
        {
            CheckBox chk;
            string idItem;
            // Valida se a grid de atividades possui algum registro
            if (grdSolicitacoes.Rows.Count != 0)
            {
                // Passa por todos os registros da grid de atividades
                foreach (GridViewRow linha in grdSolicitacoes.Rows)
                {
                    idItem = (((HiddenField)linha.Cells[0].FindControl("hidItemSolic")).Value);
                    int idI = (int)HttpContext.Current.Session["VL_ITEM_SOLIC_SELEC"];

                    if (int.Parse(idItem) == idI)
                    {
                        chk = (CheckBox)linha.Cells[0].FindControl("chkselect");
                        chk.Checked = true;
                    }
                }
            }
        }

        /// <summary>
        /// Devido ao método de reload na grid de Pré-Atendimento, ela perde a seleção do checkbox, este método coleta 
        /// </summary>
        private void DeselecionaGrid()
        {
            foreach (GridViewRow li in grdProfissionais.Rows)
            {
                CheckBox chkCol = (((CheckBox)li.Cells[0].FindControl("chkselect")));
                chkCol.Checked = false;
            }
        }

        public class saidaProfissionais
        {
            public int CO_EMP { get; set; }
            public int CO_COL { get; set; }
            public string NO_COL { get; set; }
            public string MATRICULA { get; set; }
            public string NO_COL_V
            {
                get
                {
                    return this.MATRICULA + " - " + this.NO_COL;
                }
            }
            public string CO_CLASS_PROFI { get; set; }
            public string CLASS_PROFI
            {
                get
                {
                    return (AuxiliGeral.GetNomeClassificacaoFuncional(this.CO_CLASS_PROFI));
                }
            }
            public string STATUS_R { get; set; }
            public string SX { get; set; }
            public DateTime DT_NASC { get; set; }
            public string ID
            {
                get
                {
                    return (AuxiliFormatoExibicao.FormataDataNascimento(this.DT_NASC, AuxiliFormatoExibicao.ETipoDataNascimento.padraoIdade));
                }
            }
            public string NU_TEL { get; set; }
            public string NU_TEL_V
            {
                get
                {
                    return AuxiliFormatoExibicao.PreparaTelefone(this.NU_TEL);
                }
            }
            public string EMAIL { get; set; }
            public string NO_ESPEC { get; set; }
            public string TP_CONTR { get; set; }
            public string NO_TPCAL { get; set; }
            public double? VL_SALAR_COL_R { get; set; }
            public string VL_SALAR_COL
            {
                get
                {
                    return (this.VL_SALAR_COL_R.HasValue ? this.VL_SALAR_COL_R.Value.ToString("N2") : " - ");
                }
            }
            public int QPE
            {
                get
                {
                    //var res = (from tbs368 in TBS368_RECEP_SOLIC_ITENS.RetornaTodosRegistros()
                    //           where tbs368.CO_COL_OBJET_ANALI == this.CO_COL
                    //           & tbs368.CO_SITUA == "E"
                    //           select new { tbs368.ID_RECEP_SOLIC_ITENS }).ToList();

                    //return res.Count;
                    return 0;
                }
            }
        }

        #endregion

        #region Funções de Campo

        protected void chkselect_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            CheckBox chk;

            // Valida se a grid de atividades possui algum registro
            if (grdProfissionais.Rows.Count != 0)
            {
                // Passa por todos os registros da grid de atividades
                foreach (GridViewRow linha in grdProfissionais.Rows)
                {
                    chk = (CheckBox)linha.Cells[0].FindControl("chkselect");

                    // Desmarca todos os registros menos o que foi clicado
                    if (chk.ClientID != atual.ClientID)
                        chk.Checked = false;
                    else
                    {
                        if (chk.Checked)
                        {
                            hidCoEmpCol.Value = (((HiddenField)linha.Cells[0].FindControl("hidCoEmp")).Value);
                            hidCoCol.Value = (((HiddenField)linha.Cells[0].FindControl("hidCoCol")).Value);
                        }
                        else
                        {
                            hidCoCol.Value = "";
                            hidCoEmpCol.Value = "";
                        }
                    }
                }
            }
        }

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

                            #region Seleciona o Colaborador se houver

                            string col = (((HiddenField)linha.Cells[0].FindControl("hidCoColObAnali")).Value);
                            SelecionaColaborador(col);

                            #endregion
                        }
                        else
                        {
                            DeselecionaGrid();
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

        protected void imgPesqProfis_OnClick(object sender, EventArgs e)
        {
            CarregaProfissionais();
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

        protected void imgInfoPaciente_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            if (grdSolicitacoes.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdSolicitacoes.Rows)
                {
                    img = (ImageButton)linha.Cells[1].FindControl("imgInfoPaciente");

                    if (img.ClientID == atual.ClientID)
                    {
                        ScriptManager.RegisterStartupScript(
                            this.Page,
                            this.GetType(),
                            "Acao",
                            "AbreModalInfoPaciente();",
                            true
                        );
                    }
                }
            }
        }

        protected void imgInfoPlano_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            if (grdSolicitacoes.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdSolicitacoes.Rows)
                {
                    img = (ImageButton)linha.Cells[5].FindControl("imgInfoPlano");

                    if (img.ClientID == atual.ClientID)
                    {
                        ScriptManager.RegisterStartupScript(
                            this.Page,
                            this.GetType(),
                            "Acao",
                            "AbreModalInfoPlano();",
                            true
                        );
                    }
                }
            }
        }

        #endregion
    }
}