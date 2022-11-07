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
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5200_CtrlReceitas.F5206_AlterarDataTituReceita
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

                CarregaResponsavel();
                CarregaAgrupador();
                CarregaUnidadeContrato();
            }
        }

        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega o DropDown Nome da Fonte com os Alunos
        /// </summary>
        private void CarregaResponsavel()
        {
            ddlResponsavel.Items.Clear();
            ddlResponsavel.Items.AddRange(AuxiliBaseApoio.ResponsaveisDDL(LoginAuxili.ORG_CODIGO_ORGAO, true));
        }

        /// <summary>
        /// Método que carrega o DropDown Nome da Fonte com os Alunos
        /// </summary>
        private void CarregaAlunos()
        {
            ddlAluno.Items.Clear();
            ddlAluno.Items.AddRange(AuxiliBaseApoio.AlunosDDL(ddlResponsavel.SelectedValue, selecione: true, codigoAluno: true));
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

        #endregion

        #region Eventos de componentes da pagina

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
            int agrupador = int.Parse(ddlAgrupador.SelectedValue);
            int undContrato = int.Parse(ddlUnidadeContrato.SelectedValue);
            int coAlu = int.Parse(ddlAluno.SelectedValue);

            DateTime dtInicio = DateTime.ParseExact(txtDataPeriodoIni.Text, "dd/MM/yyyy", null);
            DateTime dtFim = DateTime.ParseExact(txtDataPeriodoFim.Text, "dd/MM/yyyy", null);

            var lst = from tb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                      where tb47.DT_VEN_DOC >= dtInicio && tb47.DT_VEN_DOC <= dtFim
                      && (tb47.IC_SIT_DOC == "A" || tb47.IC_SIT_DOC == "R") 
                      && tb47.CO_ALU == coAlu
                      && (agrupador != -1 ? tb47.CO_AGRUP_RECDESP == agrupador : 0 == 0)
                      && (undContrato != -1 ? tb47.CO_EMP_UNID_CONT == undContrato : 0 == 0)
                      select new Dummy
                      {
                          Checked = false,
                          NU_DOC = tb47.NU_DOC,
                          NU_PAR = tb47.NU_PAR,
                          VR_PAR_DOC = tb47.VR_PAR_DOC,
                          DT_VEN_DOC = tb47.DT_VEN_DOC,
                          DE_HISTORICO = tb47.TB39_HISTORICO.DE_HISTORICO
                      };

            this.grdResumo.DataSource = lst.ToList();
            this.grdResumo.DataBind();

            divGrid.Visible = liResumo.Visible = true;
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
                //------------> Faz a verificação dos itens marcados na Grid de Títulos
                if (((CheckBox)linha.Cells[0].FindControl("chkSelect")).Checked)
                {
                    ((TextBox)linha.Cells[5].FindControl("txtData")).Enabled = true;

                    if (((TextBox)linha.Cells[5].FindControl("txtData")).Text == "")
                    {
                        ((TextBox)linha.Cells[5].FindControl("txtData")).Text = ((TextBox)linha.Cells[5].FindControl("txtDataAtual")).Text;
                    }
                }
                else
                {
                    ((TextBox)linha.Cells[5].FindControl("txtData")).Enabled = false;
                    ((TextBox)linha.Cells[5].FindControl("txtData")).Text = "";
                }
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            bool editado = false;

            ///Varre toda a grid de Pesquisa
            foreach (GridViewRow linha in grdResumo.Rows)
            {
                ///Verifica se linha está checada
                if (((CheckBox)linha.Cells[0].FindControl("chkSelect")).Checked)
                {
                    string novaData = ((TextBox)linha.Cells[5].FindControl("txtData")).Text;
                    string NuDoc = linha.Cells[1].Text;
                    int NuPar = int.Parse(linha.Cells[2].Text);

                    if (string.IsNullOrWhiteSpace(novaData))
                        continue;

                    DateTime novaDt = DateTime.ParseExact(novaData, "dd/MM/yyyy", null);

                    ///Faz a adicão do item
                    var tit = TB47_CTA_RECEB.RetornaPrimeiroRegistroPeloCoEmpNuDocNuPar(LoginAuxili.CO_EMP, NuDoc, NuPar);
                    tit.DT_VEN_DOC = novaDt;

                    if (GestorEntities.SaveOrUpdate(tit) < 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Erro ao salvar um dos títulos.");
                        return;
                    }

                    editado = true;
                }
            }

            if (!editado)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Selecione ao menos um título para alterar a data.");
                return;
            }

            AuxiliPagina.RedirecionaParaPaginaSucesso("Datas Alteradas com Sucesso!", "/GEDUC/5000_CtrlFinanceira/5200_CtrlReceitas/5206_AlterarDataTituReceita/Cadastro.aspx?moduloId=981&moduloNome=Alteração+de+Datas+de+Vencimento+de+Títulos+de+Receitas");
        }

        #endregion
    }

    public class Dummy
    {
        public string NU_DOC { get; set; }
        public int NU_PAR { get; set; }
        public decimal VR_PAR_DOC { get; set; }
        public DateTime DT_VEN_DOC { get; set; }
        public string DE_HISTORICO { get; set; }
        public bool Checked { get; set; }
        public DateTime? NovaData { get; set; }
    }
}