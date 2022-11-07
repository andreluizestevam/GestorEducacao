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
namespace C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9300_CtrlExames._9308_ControleSubGruposExameFisico
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

            var tbs432 = TBS432_EXAME_FISIC_SUB_GRUPO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));

            if (tbs432 == null)
            {
                tbs432 = new TBS432_EXAME_FISIC_SUB_GRUPO();

                tbs432.TBS431_GRUPO_EXAME_FISIC = TBS431_GRUPO_EXAME_FISIC.RetornaPelaChavePrimaria(int.Parse(ddlGrupo.SelectedValue));
                tbs432.NO_SUB_GRUPO_FISIC = txtNomeSubgrupo.Text;
                tbs432.DE_SUB_GRUPO_FISIC = txtDesc.Text;
                tbs432.FL_SITUA_SUB_GRUPO_FISIC = ddlStatus.SelectedValue;
                tbs432.DT_CADASTRO = DateTime.Now;
                tbs432.CO_COL_CAD = LoginAuxili.CO_COL;
                tbs432.CO_EMP_CAD = LoginAuxili.CO_EMP;
                tbs432.IP_CADASTRO = LoginAuxili.IP_USU;
                tbs432.DT_SITUA = DateTime.Now;
                tbs432.CO_COL_SITUA = LoginAuxili.CO_COL;
                tbs432.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                tbs432.IP_SITUA = LoginAuxili.IP_USU;
            }
            else
            {
                tbs432.TBS431_GRUPO_EXAME_FISICReference.Load();

                tbs432.TBS431_GRUPO_EXAME_FISIC = TBS431_GRUPO_EXAME_FISIC.RetornaPelaChavePrimaria(int.Parse(ddlGrupo.SelectedValue));
                tbs432.NO_SUB_GRUPO_FISIC = txtNomeSubgrupo.Text;
                tbs432.DE_SUB_GRUPO_FISIC = txtDesc.Text;
                tbs432.FL_SITUA_SUB_GRUPO_FISIC = ddlStatus.SelectedValue;
                tbs432.DT_SITUA = DateTime.Now;
                tbs432.CO_COL_SITUA = LoginAuxili.CO_COL;
                tbs432.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                tbs432.IP_SITUA = LoginAuxili.IP_USU;
            }

            CurrentPadraoCadastros.CurrentEntity = tbs432;
        }
        #endregion

        #region Carregamento

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            var tbs432 = TBS432_EXAME_FISIC_SUB_GRUPO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));

            if (tbs432 != null)
            {
                tbs432.TBS431_GRUPO_EXAME_FISICReference.Load();

                CarregaGrupos();
                ddlGrupo.SelectedValue = tbs432.TBS431_GRUPO_EXAME_FISIC.ID_GRUPO_FISIC.ToString();

                txtNomeSubgrupo.Text = tbs432.NO_SUB_GRUPO_FISIC;
                txtDesc.Text = tbs432.DE_SUB_GRUPO_FISIC;
                ddlStatus.SelectedValue = tbs432.FL_SITUA_SUB_GRUPO_FISIC;
            }
        }
        private void CarregaGrupos()
        {
            ddlGrupo.Items.Clear();

            ddlGrupo.DataSource = (from tbs431 in TBS431_GRUPO_EXAME_FISIC.RetornaTodosRegistros()
                                   select new
                                   {
                                       tbs431.ID_GRUPO_FISIC,
                                       tbs431.NO_GRUPO_FISIC
                                   }).OrderBy(t => t.NO_GRUPO_FISIC);

            ddlGrupo.DataTextField = "NO_GRUPO_FISIC";
            ddlGrupo.DataValueField = "ID_GRUPO_FISIC";
            ddlGrupo.DataBind();

            ddlGrupo.Items.Insert(0, new ListItem("Selecione", ""));
        }

        #endregion

        #region Métodos

        #endregion
    }
}
