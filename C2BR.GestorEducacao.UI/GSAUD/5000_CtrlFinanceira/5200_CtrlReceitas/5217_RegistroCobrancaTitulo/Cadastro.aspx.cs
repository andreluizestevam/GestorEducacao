//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE RECEITAS (CONTAS A RECEBER)
// OBJETIVO: CADASTRAMENTO GERAL DE TÍTULOS DE RECEITAS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using Resources;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GSAUD.F5000_CtrlFinanceira.F5200_CtrlReceitas.F5217_RegistroCobrancaTitulo
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return ((PadraoCadastros)this.Master); } }
        private Dictionary<string, string> tipoAgrupador = AuxiliBaseApoio.chave(tipoAgrupadorFinanceiro.ResourceManager);
        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string nomeEmp = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP).NO_FANTAS_EMP;
                txtNomeEmp.Text = nomeEmp;

                CarregaAgrupador();
                CarregaUnidadeContrato();
                OcultarPesquisa(false);
            }
        }

        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega o DropDown Nome da Fonte com os Alunos
        /// </summary>
        private void CarregaResponsavel()
        {
            var res = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                       where tb108.NO_RESP.Contains(txtNomeRespPesq.Text)
                       select new
                       {
                           tb108.NO_RESP,
                           tb108.CO_RESP,
                       }).ToList();

            if (res != null)
            {
                ddlResponsavel.DataTextField = "NO_RESP";
                ddlResponsavel.DataValueField = "CO_RESP";
                ddlResponsavel.DataSource = res;
                ddlResponsavel.DataBind();
            }

            ddlResponsavel.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o DropDown Nome da Fonte com os Alunos
        /// </summary>
        private void CarregaAlunos()
        {
            int coResp = (ddlResponsavel.SelectedValue != "" ? int.Parse(ddlResponsavel.SelectedValue) : 0);
            AuxiliCarregamentos.CarregaAlunoXResponsavel(ddlAluno, coResp, false);
        }

        /// <summary>
        /// Método que carrega o DropDown de Unidades de Contrato
        /// </summary>
        private void CarregaAgrupador()
        {
            ddlAgrupador.Items.Clear();
            ddlAgrupador.Items.AddRange(AuxiliBaseApoio.AgrupadoresDDL(tipoAgrupador[tipoAgrupadorFinanceiro.R], todos:true));

        }

        /// <summary>
        /// Método que carrega o DropDown de Unidades de Contrato
        /// </summary>
        private void CarregaUnidadeContrato()
        {
            ddlUnidadeContrato.Items.Clear();
            ddlUnidadeContrato.Items.AddRange(AuxiliBaseApoio.UnidadesDDL(LoginAuxili.IDEADMUSUARIO, todos: true));
        }

        private void CarregarUsuariosCobranca(DropDownList drp)
        {
            drp.DataSource = (from admUsuario in ADMUSUARIO.RetornaTodosRegistros()
                              join tb03 in TB03_COLABOR.RetornaTodosRegistros() on admUsuario.CodUsuario equals tb03.CO_COL
                              where admUsuario.CO_EMP == tb03.CO_EMP && admUsuario.FL_MANUT_COBRANCA == "S"
                              && admUsuario.CodInstituicao == LoginAuxili.ORG_CODIGO_ORGAO
                              select new { tb03.CO_COL, tb03.NO_COL }).OrderBy(a => a.NO_COL);

            drp.DataTextField = "NO_COL";
            drp.DataValueField = "CO_COL";
            drp.DataBind();

            drp.Items.Insert(0, new ListItem("Selecione", ""));
        }

        #endregion

        #region Eventos de componentes da pagina

        protected void imgbPesqRespNome_OnClick(object sender, EventArgs e)
        {
            CarregaResponsavel();
            OcultarPesquisa(true);
        }

        protected void imgbVoltarPesq_OnClick(object sender, EventArgs e)
        {
            ddlAluno.Items.Clear();
            OcultarPesquisa(false);
        }

        private void OcultarPesquisa(bool ocultar)
        {
            txtNomeRespPesq.Visible =
            imgbPesqRespNome.Visible = !ocultar;
            ddlResponsavel.Visible =
            imgbVoltarPesq.Visible = ocultar;
        }

        protected void ddlResponsavel_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAlunos();
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (ddlResponsavel.SelectedValue == "" || ddlResponsavel.SelectedValue == "0")
            {
                AuxiliPagina.EnvioMensagemErro(this, "Selecione o responsável.");
                return;
            }
            if (ddlAluno.SelectedValue == "" || ddlAluno.SelectedValue == "0")
            {
                AuxiliPagina.EnvioMensagemErro(this, "Selecione o aluno.");
                return;
            }

            if (ADMUSUARIO.RetornaPelaUnidColabor(LoginAuxili.CO_EMP, LoginAuxili.CO_COL).FL_MANUT_COBRANCA != "S")
                AuxiliPagina.EnvioMensagemErro(this, "O usuário logado não esta credenciado como 'Usuário Cobrança'. As informações disponiveis são apenas para efeito de pesquisa.");

            int agrupador = int.Parse(ddlAgrupador.SelectedValue);
            int undContrato = int.Parse(ddlUnidadeContrato.SelectedValue);
            int coAlu = int.Parse(ddlAluno.SelectedValue);

            DateTime? dtInicio = (!string.IsNullOrEmpty(txtDataPeriodoIni.Text) ? DateTime.ParseExact(txtDataPeriodoIni.Text, "dd/MM/yyyy", null) : (DateTime?)null);
            DateTime? dtFim = (!string.IsNullOrEmpty(txtDataPeriodoFim.Text) ? DateTime.ParseExact(txtDataPeriodoFim.Text, "dd/MM/yyyy", null) : (DateTime?)null);

            var lst = from tbs47 in TBS47_CTA_RECEB.RetornaTodosRegistros()
                      //Se não tiver sido informada data de início ou final, traz todas para cada caso respectivamente
                      where (dtInicio.HasValue ? tbs47.DT_VEN_DOC >= dtInicio.Value : 0 == 0) && (dtFim.HasValue ? tbs47.DT_VEN_DOC <= dtFim.Value : 0 == 0)
                      && (tbs47.IC_SIT_DOC == "A" || tbs47.IC_SIT_DOC == "R") 
                      && tbs47.CO_ALU == coAlu
                      && (agrupador != -1 ? tbs47.CO_AGRUP_RECDESP == agrupador : 0 == 0)
                      && (undContrato != -1 ? tbs47.CO_EMP_UNID_CONT == undContrato : 0 == 0)
                      && (tbs47.IC_SIT_DOC != "Q")
                      select new Dummy
                      {
                          Checked = false,
                          NU_DOC = tbs47.NU_DOC,
                          NU_PAR = tbs47.NU_PAR,
                          VL_PAR_DOC = tbs47.VL_PAR_DOC,
                          DT_VEN_DOC = tbs47.DT_VEN_DOC,
                          DE_HISTORICO = tbs47.TB39_HISTORICO.DE_HISTORICO,
                          DT_COBRAN = tbs47.DT_COBRAN,
                          VL_COBRAN = tbs47.VL_COBRAN,
                          CO_COL_COBRAN = tbs47.TB03_COLABOR != null ? tbs47.TB03_COLABOR.CO_COL : (int?)null
                      };

            this.grdResumo.DataSource = lst.ToList();
            this.grdResumo.DataBind();

            divGrid.Visible = liResumo.Visible = true;
        }

        protected void grdResumo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var hidColCobran = (HiddenField)e.Row.FindControl("hidColCobranca");
                var drpColCobran = (DropDownList)e.Row.FindControl("drpColCobranca");

                CarregarUsuariosCobranca(drpColCobran);
                if (!String.IsNullOrEmpty(hidColCobran.Value) && drpColCobran.Items.FindByValue(hidColCobran.Value) != null)
                    drpColCobran.SelectedValue = hidColCobran.Value;
            }
        }

        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            HabilitaCamposGride();
        }

        /// <summary>
        /// Método que habilita campos da gride
        /// </summary>
        protected void HabilitaCamposGride()
        {
            foreach (GridViewRow linha in grdResumo.Rows)
            {
                var txtData = (TextBox)linha.FindControl("txtData");
                var txtValor = (TextBox)linha.FindControl("txtValorNovo");
                var drpColCobran = (DropDownList)linha.FindControl("drpColCobranca");

                //------------> Faz a verificação dos itens marcados na Grid de Títulos
                if (((CheckBox)linha.FindControl("chkSelect")).Checked)
                {
                    if (String.IsNullOrEmpty(txtData.Text))
                        txtData.Text = ((TextBox)linha.FindControl("txtDataAtual")).Text;
                    if (String.IsNullOrEmpty(txtValor.Text))
                        txtValor.Text = ((TextBox)linha.FindControl("txtValorAtual")).Text;
                    if (String.IsNullOrEmpty(drpColCobran.SelectedValue) && drpColCobran.Items.FindByValue(LoginAuxili.CO_COL.ToString()) != null)
                        drpColCobran.SelectedValue = LoginAuxili.CO_COL.ToString();

                    txtData.Enabled =
                    txtValor.Enabled =
                    drpColCobran.Enabled = true;
                }
                else
                    txtData.Enabled =
                    txtValor.Enabled =
                    drpColCobran.Enabled = false;
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (ADMUSUARIO.RetornaPelaUnidColabor(LoginAuxili.CO_EMP, LoginAuxili.CO_COL).FL_MANUT_COBRANCA != "S")
            {
                AuxiliPagina.EnvioMensagemErro(this, "O usuário logado não esta credenciado como 'Usuário Cobrança'. As informações disponiveis são apenas para efeito de pesquisa.");
                return;
            }

            bool editado = false;

            ///Varre toda a grid de Pesquisa
            foreach (GridViewRow linha in grdResumo.Rows)
            {
                ///Verifica se linha está checada
                if (((CheckBox)linha.FindControl("chkSelect")).Checked)
                {
                    var novaData = ((TextBox)linha.FindControl("txtData")).Text;
                    var novoValor = ((TextBox)linha.FindControl("txtValorNovo")).Text;
                    var drpColCobran = (DropDownList)linha.FindControl("drpColCobranca");

                    string NuDoc = linha.Cells[1].Text;
                    int NuPar = int.Parse(linha.Cells[2].Text);

                    var novaDt = !String.IsNullOrEmpty(novaData) ? DateTime.Parse(novaData) : (DateTime?)null;
                    var novoVR = (!string.IsNullOrEmpty(novoValor) ? decimal.Parse(novoValor) : (decimal?)null);

                    ///Faz a adicão do item
                    var tit = TBS47_CTA_RECEB.RetornaPrimeiroRegistroPeloCoEmpNuDocNuPar(LoginAuxili.CO_EMP, NuDoc, NuPar);
                    
                    if (novaDt.HasValue)
                        tit.DT_COBRAN = novaDt;

                    if (novoVR.HasValue)
                        tit.VL_COBRAN = novoVR;
                    
                    if (!String.IsNullOrEmpty(drpColCobran.SelectedValue))
                        tit.TB03_COLABOR = TB03_COLABOR.RetornaPeloCoCol(int.Parse(drpColCobran.SelectedValue));

                    if (GestorEntities.SaveOrUpdate(tit) < 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Erro ao salvar um dos títulos.");
                        return;
                    }

                    editado = true;

                    if (tit.CO_ALU.HasValue)
                        TB07_ALUNO.AtualizarSituacaoFinanceira(tit.CO_ALU.Value);
                }
            }

            if (!editado)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Selecione ao menos um título para alterar a data ou valor.");
                return;
            }

            AuxiliPagina.RedirecionaParaPaginaSucesso("Datas/Valor Alterados com Sucesso!", "/GSAUD/5000_CtrlFinanceira/5200_CtrlReceitas/5217_RegistroCobrancaTitulo/Cadastro.aspx?moduloId=981&moduloNome=Alteração+de+Datas+de+Vencimento+de+Títulos+de+Receitas");
        }

        #endregion
    }

    public class Dummy
    {
        public string NU_DOC { get; set; }
        public int NU_PAR { get; set; }
        public decimal VL_PAR_DOC { get; set; }
        public DateTime DT_VEN_DOC { get; set; }
        public string DE_HISTORICO { get; set; }
        public bool Checked { get; set; }
        public DateTime? DT_COBRAN { get; set; }
        public decimal? VL_COBRAN { get; set; }
        public int? CO_COL_COBRAN { get; set; }
    }
}