//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE DESPESAS (CONTAS A PAGAR)
// OBJETIVO: BAIXA DE TÍTULOS DE DESPESAS/COMPROMISSOS DE PAGAMENTOS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web.UI.WebControls;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5300_CtrlDespesas.F5303_ComponentesCusto
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //--------> Criação das instâncias utilizadas
            //CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
            CurrentPadraoCadastros.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentPadraoCadastros_OnCarregaFormulario);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaUnidade();

            }
        }

        protected void btnNewSearch_Click(object sender, EventArgs e)
        {
            AuxiliPagina.RedirecionaParaPaginaBusca();
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo para carregar Formulário
        void CarregaFormulario()
        {
            TB429_REFER_CUSTO tb429 = RetornaEntidade();

            if (tb429 != null)
            {
                ddlUnidade.SelectedValue = Convert.ToString(tb429.CO_EMP);
                txtDescricao.Text = tb429.DE_ITEM;
                txtCodRefer.Text = tb429.CO_REFER;
                txtNome.Text = tb429.NO_ITEM;
                txtOrdImpre.Text = Convert.ToString(tb429.ORD_IMPRE);
                txtDataCad.Text = Convert.ToString(tb429.DT_CADAS);
                txtDataSitu.Text = Convert.ToString(tb429.DT_SITUA);
                ddlSituacao.SelectedValue = tb429.FL_SITUA;
                ddlGrupo.SelectedValue = Convert.ToString(tb429.NU_GRUPO);
            }
        }

        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            TB429_REFER_CUSTO tb429 = RetornaEntidade();

            try
            {
                TB429_REFER_CUSTO.Delete(tb429, true);
                AuxiliPagina.RedirecionaParaPaginaSucesso("Item excluído com sucesso.", Request.Url.AbsoluteUri);
            }
            catch (Exception)
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Não foi possível excluir o Parceiro. Por favor tente novamente ou entre em contato com o suporte.");
                return;
            }
        }

        //====> Processo de Inclusão e/ou Alteração
        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            try
            {
                TB429_REFER_CUSTO tb429 = RetornaEntidade();

                tb429.CO_EMP = int.Parse(ddlUnidade.SelectedValue);
                tb429.CO_ORG = LoginAuxili.ORG_CODIGO_ORGAO;
                tb429.DE_ITEM = txtDescricao.Text;
                tb429.CO_REFER = txtCodRefer.Text;
                tb429.NO_ITEM = txtNome.Text;
                tb429.ORD_IMPRE = int.Parse(txtOrdImpre.Text);
                tb429.DT_CADAS = DateTime.Parse(txtDataCad.Text);
                tb429.DT_SITUA = DateTime.Parse(txtDataSitu.Text);
                tb429.FL_SITUA = ddlSituacao.SelectedValue;
                tb429.NU_GRUPO = int.Parse(ddlGrupo.SelectedValue);

                TB429_REFER_CUSTO.SaveOrUpdate(tb429, true);

                if (tb429 != null)
                {
                    AuxiliPagina.RedirecionaParaPaginaSucesso("Registro adicionado com sucesso.", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
                }
            }

            catch (Exception)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Não foi possível salvar ou alterar o cadastro de parceiros, por favor, tente novamente ou entre em contato com o suporte.");
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB38_CTA_PAGAR</returns>
        private TB429_REFER_CUSTO RetornaEntidade()
        {
            TB429_REFER_CUSTO tb429 = TB429_REFER_CUSTO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave("ID"));
            return (tb429 != null ? tb429 : new TB429_REFER_CUSTO());
        }
        #endregion

        #region Carrega DropDown

        public void CarregaUnidade()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, false);
        }
        #endregion

    }
}