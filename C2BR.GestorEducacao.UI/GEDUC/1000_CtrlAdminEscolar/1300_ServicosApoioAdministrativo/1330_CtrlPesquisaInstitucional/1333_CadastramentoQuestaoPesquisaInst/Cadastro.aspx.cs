//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: PESQUISAS/ENQUETES DE SATISFAÇÃO
// OBJETIVO: CADASTRAMENTO DE QUESTÕES PARA PESQUISAS INSITUCIONAIS
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
namespace C2BR.GestorEducacao.UI.GEDUC.F1300_ServicosApoioAdministrativo.F1330_CtrlPesquisaInstitucional.F1333_CadastramentoQuestaoPesquisaInst
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
            if (ddlTipAva.SelectedValue != "" && Session["codTipo"].ToString() != "")
                Session["codTipo"] = ddlTipAva.SelectedValue;

            if (!IsPostBack)
            {
                CarregaTipoAvaliacao();
                CarregaTitAvaliacao();

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                    li1.Visible = ddlTipAva.Enabled = ddlTitQuestao.Enabled = barraTitulo.Visible = grdBusca.Visible = false;
            }

            if (Session["codTipo"] == null)
            {
                ddlTipAva.SelectedIndex = 0;
                Session["codTipo"] = ddlTipAva.SelectedValue;
            }
            else if (Session["codTipo"].ToString() == "")
            {
                CarregaTipoAvaliacao();
                ddlTipAva.SelectedValue = (from tb73 in TB73_TIPO_AVAL.RetornaTodosRegistros()
                                           select new { tb73.CO_TIPO_AVAL }).Max( t => t.CO_TIPO_AVAL ).ToString();
                CarregaTitAvaliacao();
                Session["codTipo"] = ddlTipAva.SelectedValue;
                grdBusca.DataSource = null;
                grdBusca.DataBind();
            }
            else if (Session["codTipo"].ToString() != "")
            {
                int codTipo = int.Parse(Session["codTipo"].ToString());

                if (codTipo > 0)
                {
                    ddlTipAva.SelectedValue = codTipo.ToString();

                    if (Session["codTit"] != null)
                    {
                        CarregaTitAvaliacao();
                        int codigoTitQuestAval;

                        List<TB72_TIT_QUES_AVAL> lstTb72 = (from tb72 in TB72_TIT_QUES_AVAL.RetornaTodosRegistros()
                                                            where tb72.CO_TIPO_AVAL.Equals(codTipo)
                                                            select tb72).ToList();

                        if (lstTb72.Count > 0)
                        {
                            codigoTitQuestAval = (from ltb72 in lstTb72
                                                  where ltb72.CO_TIPO_AVAL.Equals(codTipo)
                                                  select ltb72).Max(p => p.CO_TITU_AVAL);

                            ddlTitQuestao.SelectedValue = codigoTitQuestAval.ToString();
                        }

                        Session["codTit"] = null;
                    }
                    Session["codTipo"] = ddlTipAva.SelectedValue;
                }

                CarregaGrid();
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int qtdGrd = grdBusca.Rows.Count;

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
            {
                TB71_QUEST_AVAL tb71 = RetornaEntidade();

                if (tb71 == null)
                    tb71 = new TB71_QUEST_AVAL();

                tb71.DE_QUES_AVAL = txtQuestao.Text;

                CurrentPadraoCadastros.CurrentEntity = tb71;
            }
            else
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao) && qtdGrd > 0)
                    AuxiliPagina.RedirecionaParaPaginaSucesso("Registro Adicionado com sucesso!", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB71_QUEST_AVAL tb71 = RetornaEntidade();

            if (tb71 != null)
            {
                ddlTipAva.SelectedValue = tb71.CO_TIPO_AVAL.ToString();                                
                CarregaTitAvaliacao();
                ddlTitQuestao.SelectedValue = tb71.CO_TITU_AVAL.ToString();
                txtQuestao.Text = tb71.DE_QUES_AVAL;
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB71_QUEST_AVAL</returns>
        private TB71_QUEST_AVAL RetornaEntidade()
        {
            return TB71_QUEST_AVAL.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave("titAvaliacao"), 
                QueryStringAuxili.RetornaQueryStringComoIntPelaChave("tipoAvaliacao"), QueryStringAuxili.RetornaQueryStringComoIntPelaChave("nuQuest"));
        }
        #endregion

        #region Carregamento
        
        /// <summary>
        /// Método que carrega o dropdown de Tipo de Avaliação
        /// </summary>
        private void CarregaTipoAvaliacao()
        {
            ddlTipAva.DataSource = (from tb73 in TB73_TIPO_AVAL.RetornaTodosRegistros()
                                    select new { tb73.NO_TIPO_AVAL, tb73.CO_TIPO_AVAL });

            ddlTipAva.DataTextField = "NO_TIPO_AVAL";
            ddlTipAva.DataValueField = "CO_TIPO_AVAL";
            ddlTipAva.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Titulo de Avaliação
        /// </summary>
        private void CarregaTitAvaliacao()
        {
            int coTipoAval = ddlTipAva.SelectedValue != "" ? int.Parse(ddlTipAva.SelectedValue) : 0 ;

            var tipoAva =

            ddlTitQuestao.DataSource = (from tb72 in TB72_TIT_QUES_AVAL.RetornaTodosRegistros()
                                        where tb72.CO_TIPO_AVAL == coTipoAval
                                        select new { tb72.NO_TITU_AVAL, tb72.CO_TITU_AVAL });

            ddlTitQuestao.DataTextField = "NO_TITU_AVAL";
            ddlTitQuestao.DataValueField = "CO_TITU_AVAL";
            ddlTitQuestao.DataBind();

            ddlTitQuestao.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega a grid de Questões
        /// </summary>
        private void CarregaGrid()
        {
            int coTipoAval = ddlTipAva.SelectedValue != "" ? int.Parse(ddlTipAva.SelectedValue) : 0;
            int coTituAval = ddlTitQuestao.SelectedValue != "" ? int.Parse(ddlTitQuestao.SelectedValue) : 0;

            var questoes = from tb71 in TB71_QUEST_AVAL.RetornaTodosRegistros()
                           where tb71.CO_TIPO_AVAL == coTipoAval && tb71.CO_TITU_AVAL == coTituAval
                           select new { tb71.NU_QUES_AVAL, tb71.DE_QUES_AVAL };

            if (coTituAval > 0 && questoes.Count() > 0)
            {
                grdBusca.DataSource = questoes;
                grdBusca.DataBind();
            }
            else
            {
                grdBusca.Dispose();
                grdBusca.DataBind();
            }

            barraTitulo.Visible = true;
        }        
        #endregion

        protected void ddlTipAva_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTitAvaliacao();
            grdBusca.Dispose();
        }

        protected void ddlTitQuestao_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGrid();
        }

        protected void imgAdd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                int codTitulo = 0;
                int codTipoAva = Convert.ToInt32(ddlTipAva.SelectedValue);
                codTitulo = Convert.ToInt32(ddlTitQuestao.SelectedValue);

                TB71_QUEST_AVAL tb71 = RetornaEntidade();

                if (tb71 == null)
                    tb71 = new TB71_QUEST_AVAL();

                tb71.CO_TITU_AVAL = codTitulo;
                tb71.DE_QUES_AVAL = txtQuestao.Text;
                tb71.TB72_TIT_QUES_AVAL = TB72_TIT_QUES_AVAL.RetornaPelaChavePrimaria(codTitulo);

                int result = TB71_QUEST_AVAL.SaveOrUpdate(tb71).NU_QUES_AVAL;

                CarregaGrid();
            }
        }
    }
}
