//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: PESQUISAS/ENQUETES DE SATISFAÇÃO
// OBJETIVO: CADASTRAMENTO DE PESQUISAS INSITUCIONAIS
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
using System.Data.Sql;
using Resources;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9300_CtrlExames._9304_ControleSubGrupos
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
            CurrentPadraoCadastros.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentPadraoCadastros_OnCarregaFormulario);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaOperadoras();
                CarregaProcedimentos();
                CarregaGrupos();
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {

            if (String.IsNullOrEmpty(txtNomeSubgrupo.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "É necessário informar o nome do Subgrupo.");
                txtNomeSubgrupo.Focus();
                return;
            }
            if (String.IsNullOrEmpty(ddlGrupo.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "É necessário informar o Grupo do Subgrupo.");
                ddlGrupo.Focus();
                return;
            }

            int coTipoAval = int.Parse(ddlGrupo.SelectedValue);

            var tbs413 = TBS413_EXAME_SUBGR.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));

            if (tbs413 == null)
            {
                tbs413 = new TBS413_EXAME_SUBGR();
            }

            tbs413.TBS412_EXAME_GRUPO = TBS412_EXAME_GRUPO.RetornaPelaChavePrimaria(coTipoAval);
            tbs413.CO_SUBGR = txtCodSubgrupo.Text;
            tbs413.NO_SUBGR_EXAME = txtNomeSubgrupo.Text;
            tbs413.FL_SITUA_SUBGR = ddlStatus.SelectedValue;
            tbs413.NU_ORDEM = !String.IsNullOrEmpty(txtNumOrdem.Text) ? int.Parse(txtNumOrdem.Text) : (int?)null;

            CurrentPadraoCadastros.CurrentEntity = tbs413;
        }        
        #endregion

        #region Carregamento

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            var tbs413 = TBS413_EXAME_SUBGR.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));

            if (tbs413 != null)
            {
                tbs413.TBS412_EXAME_GRUPOReference.Load();
                tbs413.TBS412_EXAME_GRUPO.TBS356_PROC_MEDIC_PROCEReference.Load();
                tbs413.TBS412_EXAME_GRUPO.TBS356_PROC_MEDIC_PROCE.TB250_OPERAReference.Load();

                CarregaOperadoras();
                ddlOperadora.SelectedValue = tbs413.TBS412_EXAME_GRUPO.TBS356_PROC_MEDIC_PROCE.TB250_OPERA.ID_OPER.ToString();
                ddlOperadora.Enabled = false;

                CarregaProcedimentos();
                ddlProcedimento.SelectedValue = tbs413.TBS412_EXAME_GRUPO.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE.ToString();
                ddlProcedimento.Enabled = false;

                CarregaGrupos();
                ddlGrupo.SelectedValue = tbs413.TBS412_EXAME_GRUPO.ID_GRUPO.ToString();
                ddlGrupo.Enabled = false;

                txtCodSubgrupo.Text = tbs413.CO_SUBGR;
                txtNomeSubgrupo.Text = tbs413.NO_SUBGR_EXAME;
                ddlStatus.SelectedValue = tbs413.FL_SITUA_SUBGR.ToString();
                txtNumOrdem.Text = tbs413.NU_ORDEM.HasValue ? tbs413.NU_ORDEM.Value.ToString() : "";
            }
        }

        public void CarregaOperadoras()
        {
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddlOperadora, false);
        }

        public void CarregaProcedimentos()
        {
            var idOper = !String.IsNullOrEmpty(ddlOperadora.SelectedValue) ? int.Parse(ddlOperadora.SelectedValue) : 0;

            AuxiliCarregamentos.CarregaProcedimentosMedicos(ddlProcedimento, idOper, false);
        }

        private void CarregaGrupos()
        {
            ddlGrupo.Items.Clear();

            var idProc = !String.IsNullOrEmpty(ddlProcedimento.SelectedValue) ? int.Parse(ddlProcedimento.SelectedValue) : 0;

            ddlGrupo.DataSource = (from tbs412 in TBS412_EXAME_GRUPO.RetornaTodosRegistros()
                                   where tbs412.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == idProc
                                   select new
                                   {
                                       tbs412.ID_GRUPO,
                                       tbs412.NO_GRUPO_EXAME
                                   }).OrderBy(t => t.NO_GRUPO_EXAME);

            ddlGrupo.DataTextField = "NO_GRUPO_EXAME";
            ddlGrupo.DataValueField = "ID_GRUPO";
            ddlGrupo.DataBind();

            ddlGrupo.Items.Insert(0, new ListItem("Selecione", ""));
        }

        #endregion

        #region Métodos

        protected void ddlOperadora_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaProcedimentos();
            CarregaGrupos();
        }

        protected void ddlProcedimento_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGrupos();
        }

        #endregion
    }
}
