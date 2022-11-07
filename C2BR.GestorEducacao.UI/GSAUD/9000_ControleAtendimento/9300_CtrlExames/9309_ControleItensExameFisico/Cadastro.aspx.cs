//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PGS - Portal Gestor Saúde
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: --
// SUBMÓDULO: --
// OBJETIVO: --
// DATA DE CRIAÇÃO: 17/08/2016
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
namespace C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9300_CtrlExames._9309_ControleItensExameFisico
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
                CarregaSubgrupos();
            }
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (String.IsNullOrEmpty(ddlSubgrupo.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "É necessário informar o subgrupo do item.");
                ddlSubgrupo.Focus();
                return;
            }

            if (String.IsNullOrEmpty(txtItem.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "É necessário informar o nome do item.");
                txtItem.Focus();
                return;
            }

            var tbs433 = TBS433_EXAME_FISIC_ITEM.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave("item"));

            if (tbs433 == null)
            {
                tbs433 = new TBS433_EXAME_FISIC_ITEM();

                tbs433.NO_ITEM_EXAME_FISIC = txtItem.Text;
                tbs433.DE_ITEM_EXAME_FISIC = txtDesc.Text;
                tbs433.FL_SITUA_ITEM_EXAME_FISIC = ddlStatus.SelectedValue;
                tbs433.TBS432_EXAME_FISIC_SUB_GRUPO = TBS432_EXAME_FISIC_SUB_GRUPO.RetornaPelaChavePrimaria(int.Parse(ddlSubgrupo.SelectedValue));
                tbs433.DT_CADASTRO = DateTime.Now;
                tbs433.IP_CADASTRO = LoginAuxili.IP_USU;
                tbs433.CO_COL_CAD = LoginAuxili.CO_COL;
                tbs433.CO_EMP_CAD = LoginAuxili.CO_EMP;
                tbs433.DT_SITUA = DateTime.Now;
                tbs433.IP_SITUA = LoginAuxili.IP_USU;
                tbs433.CO_COL_SITUA = LoginAuxili.CO_COL;
                tbs433.CO_EMP_SITUA = LoginAuxili.CO_EMP;
            }
            else
            {
                tbs433.TBS432_EXAME_FISIC_SUB_GRUPOReference.Load();
                tbs433.TBS432_EXAME_FISIC_SUB_GRUPO.TBS431_GRUPO_EXAME_FISICReference.Load();

                tbs433.FL_SITUA_ITEM_EXAME_FISIC = ddlStatus.SelectedValue;
                tbs433.NO_ITEM_EXAME_FISIC = txtItem.Text;
                tbs433.DE_ITEM_EXAME_FISIC = txtDesc.Text;
                tbs433.TBS432_EXAME_FISIC_SUB_GRUPO = TBS432_EXAME_FISIC_SUB_GRUPO.RetornaPelaChavePrimaria(int.Parse(ddlSubgrupo.SelectedValue));                
                tbs433.DT_SITUA = DateTime.Now;
                tbs433.IP_SITUA = LoginAuxili.IP_USU;
                tbs433.CO_COL_SITUA = LoginAuxili.CO_COL;
                tbs433.CO_EMP_SITUA = LoginAuxili.CO_EMP;
            }

            CurrentPadraoCadastros.CurrentEntity = tbs433;
        }

        #endregion

        #region Carregamento

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            var tbs433 = TBS433_EXAME_FISIC_ITEM.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave("item"));

            if (tbs433 != null)
            {
                tbs433.TBS432_EXAME_FISIC_SUB_GRUPOReference.Load();
                tbs433.TBS432_EXAME_FISIC_SUB_GRUPO.TBS431_GRUPO_EXAME_FISICReference.Load();

                CarregaGrupos();
                CarregaSubgrupos();

                ddlGrupo.SelectedValue = tbs433.TBS432_EXAME_FISIC_SUB_GRUPO.TBS431_GRUPO_EXAME_FISIC.ID_GRUPO_FISIC.ToString();                
                ddlSubgrupo.SelectedValue = tbs433.TBS432_EXAME_FISIC_SUB_GRUPO.ID_SUB_GRUPO_FISIC.ToString();

                txtItem.Text = tbs433.NO_ITEM_EXAME_FISIC;
                ddlStatus.SelectedValue = tbs433.FL_SITUA_ITEM_EXAME_FISIC;
                txtDesc.Text = String.IsNullOrEmpty(tbs433.DE_ITEM_EXAME_FISIC) ? "" : tbs433.DE_ITEM_EXAME_FISIC;
            }
        }

        private void CarregaGrupos()
        {
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

        private void CarregaSubgrupos()
        {
            var idGrupo = !String.IsNullOrEmpty(ddlGrupo.SelectedValue) ? int.Parse(ddlGrupo.SelectedValue) : 0;

            ddlSubgrupo.DataSource = (from tbs432 in TBS432_EXAME_FISIC_SUB_GRUPO.RetornaTodosRegistros()
                                      where tbs432.TBS431_GRUPO_EXAME_FISIC.ID_GRUPO_FISIC == idGrupo
                                      select new
                                      {
                                          tbs432.ID_SUB_GRUPO_FISIC,
                                          tbs432.NO_SUB_GRUPO_FISIC
                                      }).OrderBy(t => t.NO_SUB_GRUPO_FISIC);

            ddlSubgrupo.DataTextField = "NO_SUB_GRUPO_FISIC";
            ddlSubgrupo.DataValueField = "ID_SUB_GRUPO_FISIC";
            ddlSubgrupo.DataBind();

            ddlSubgrupo.Items.Insert(0, new ListItem("Selecione", ""));
        }

        #endregion

        #region Métodos Componentes

        protected void ddlGrupo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubgrupos();
        }

        #endregion
    }
}
