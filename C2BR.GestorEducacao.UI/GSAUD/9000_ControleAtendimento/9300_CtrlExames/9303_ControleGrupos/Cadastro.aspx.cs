//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: PESQUISAS/ENQUETES DE SATISFAÇÃO
// OBJETIVO: CADASTRAMENTO DE GRUPOS DE PESQUISAS INSTITUCIONAIS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.App_Masters;
using System.Data.Sql;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9300_CtrlExames._9303_ControleGrupos
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
            if (!Page.IsPostBack)
            {
                CarregaProcedimentos();
                CarregaOperadora();

                txtObs.Attributes.Add("maxlength", "300");
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (String.IsNullOrEmpty(ddlProced.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page,"Por Favor, selecione um procedimento para ser associado ao grupo.");
                ddlProced.Focus();
                return;
            }

            if (String.IsNullOrEmpty(txtNomeGrupo.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Por Favor, informe o nome do grupo.");
                txtNomeGrupo.Focus();
                return;
            }

            var tbs412 = TBS412_EXAME_GRUPO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));

            if (tbs412 == null)
            {
                tbs412 = new TBS412_EXAME_GRUPO();
            }

            tbs412.TBS356_PROC_MEDIC_PROCE = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlProced.SelectedValue));
            tbs412.CO_GRUPO = txtCodGrupo.Text;
            tbs412.NO_GRUPO_EXAME = txtNomeGrupo.Text;
            tbs412.FL_SITUA_EXAME = ddlStatus.SelectedValue;
            tbs412.DE_METOD_EXAME = txtMetodoProced.Text;
            tbs412.DE_MATER_EXAME = txtMaterialProced.Text;
            tbs412.DE_GRUPO_EXAME = txtObjetivo.Text;
            tbs412.DE_OBSER_EXAME = txtObs.Text;
            tbs412.NU_ORDEM = !String.IsNullOrEmpty(txtNumOrdem.Text) ? int.Parse(txtNumOrdem.Text) : (int?)null;

            CurrentPadraoCadastros.CurrentEntity = tbs412;
        }
        #endregion

        #region Métodos

        public void CarregaProcedimentos()
        {
            AuxiliCarregamentos.CarregaProcedimentosMedicos(ddlProced, ddlOper,false);
        }

        public void CarregaOperadora()
        {
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddlOper, false);
        }

        protected void ddlOper_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaProcedimentos();
        }

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            var tbs412 = TBS412_EXAME_GRUPO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));

            if (tbs412 != null)
            {
                tbs412.TBS356_PROC_MEDIC_PROCEReference.Load();
                tbs412.TBS356_PROC_MEDIC_PROCE.TB250_OPERAReference.Load();

                ddlOper.SelectedValue = tbs412.TBS356_PROC_MEDIC_PROCE.TB250_OPERA.ID_OPER.ToString();
                ddlOper.Enabled = false;

                CarregaProcedimentos();
                
                ddlProced.SelectedValue = tbs412.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE.ToString();
                ddlProced.Enabled = false;

                txtCodGrupo.Text = tbs412.CO_GRUPO;
                txtNomeGrupo.Text = tbs412.NO_GRUPO_EXAME;
                ddlStatus.SelectedValue = tbs412.FL_SITUA_EXAME;
                txtMetodoProced.Text = tbs412.DE_METOD_EXAME;
                txtMaterialProced.Text = tbs412.DE_MATER_EXAME;
                txtObjetivo.Text = tbs412.DE_GRUPO_EXAME;
                txtObs.Text = tbs412.DE_OBSER_EXAME;
                txtNumOrdem.Text = tbs412.NU_ORDEM.HasValue ? tbs412.NU_ORDEM.Value.ToString() : "";
            }
        }
        #endregion
    }
}
