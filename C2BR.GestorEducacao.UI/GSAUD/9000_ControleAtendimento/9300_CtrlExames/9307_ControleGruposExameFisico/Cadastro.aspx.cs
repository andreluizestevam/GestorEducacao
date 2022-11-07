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
namespace C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9300_CtrlExames._9307_ControleGruposExameFisico
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
                CarregaEspecialidade();
            }
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (String.IsNullOrEmpty(ddlEspec.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Por Favor, selecione uma especialidade para ser associada ao grupo.");
                ddlEspec.Focus();
                return;
            }

            if (String.IsNullOrEmpty(txtNomeGrupo.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Por Favor, informe o nome do grupo.");
                txtNomeGrupo.Focus();
                return;
            }

            var tbs431 = TBS431_GRUPO_EXAME_FISIC.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));

            if (tbs431 == null)
            {
                tbs431 = new TBS431_GRUPO_EXAME_FISIC();

                tbs431.CO_ESPECIALIDADE = int.Parse(ddlEspec.SelectedValue);
                tbs431.NO_GRUPO_FISIC = txtNomeGrupo.Text;
                tbs431.DE_GRUPO_FISIC = txtDesc.Text;
                tbs431.FL_SITUA_GRUPO_FISIC = ddlSitu.SelectedValue;
                tbs431.CO_COL_CAD = LoginAuxili.CO_COL;
                tbs431.CO_EMP_CAD = LoginAuxili.CO_EMP;
                tbs431.CO_COL_SITUA = LoginAuxili.CO_COL;
                tbs431.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                tbs431.DT_CADASTRO = DateTime.Now;
                tbs431.DT_SITUA = DateTime.Now;
                tbs431.IP_CADASTRO = LoginAuxili.IP_USU;
                tbs431.IP_SITUA = LoginAuxili.IP_USU;
            }
            else
            {
                tbs431.CO_ESPECIALIDADE = int.Parse(ddlEspec.SelectedValue);
                tbs431.NO_GRUPO_FISIC = txtNomeGrupo.Text;
                tbs431.DE_GRUPO_FISIC = txtDesc.Text;
                tbs431.FL_SITUA_GRUPO_FISIC = ddlSitu.SelectedValue;
                tbs431.CO_COL_SITUA = LoginAuxili.CO_COL;
                tbs431.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                tbs431.DT_SITUA = DateTime.Now;
                tbs431.IP_SITUA = LoginAuxili.IP_USU;
            }

            CurrentPadraoCadastros.CurrentEntity = tbs431;
        }
        #endregion

        #region Métodos

        public void CarregaEspecialidade()
        {
            AuxiliCarregamentos.CarregaEspeciacialidades(ddlEspec, LoginAuxili.CO_EMP, 0, false);
        }

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            var tbs431 = TBS431_GRUPO_EXAME_FISIC.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));

            if (tbs431 != null)
            {

                ddlEspec.SelectedValue = tbs431.CO_ESPECIALIDADE.ToString();
                txtNomeGrupo.Text = tbs431.NO_GRUPO_FISIC;
                txtDesc.Text = String.IsNullOrEmpty(tbs431.DE_GRUPO_FISIC) ? "" : tbs431.DE_GRUPO_FISIC;
                ddlSitu.SelectedValue = tbs431.FL_SITUA_GRUPO_FISIC;
            }
        }
        #endregion
    }
}
