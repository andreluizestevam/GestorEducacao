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
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1900_TabelasGenerCtrlAdmEscolar.F1910_CadastroGrupoCBO
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
            TB316_CBO_GRUPO tb316 = RetornaEntidade();

            if (tb316 != null)
            {
                txtDescricao.Text = tb316.DE_CBO_GRUPO;
                ddlSituacao.SelectedValue = tb316.FL_SITUA;
            }
        }

        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            TB316_CBO_GRUPO tb316 = RetornaEntidade();

            try
            {
                TB316_CBO_GRUPO.Delete(tb316, true);
                AuxiliPagina.RedirecionaParaPaginaSucesso("Item excluído com sucesso.", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
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
                TB316_CBO_GRUPO tb316 = RetornaEntidade();

                if(tb316.CO_CBO_GRUPO == null)
                {
                    var result = (from tb316r in TB316_CBO_GRUPO.RetornaTodosRegistros()
                                  orderby tb316r.CO_CBO_GRUPO descending
                                  select new
                                  {tb316r.CO_CBO_GRUPO}).FirstOrDefault();

                    tb316.CO_CBO_GRUPO = Convert.ToString(Convert.ToInt32(result.CO_CBO_GRUPO)+1);
                }

                tb316.DE_CBO_GRUPO = txtDescricao.Text;
                tb316.FL_SITUA = ddlSituacao.SelectedValue;

                TB316_CBO_GRUPO.SaveOrUpdate(tb316, true);

                if (tb316 != null)
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
        private TB316_CBO_GRUPO RetornaEntidade()
        {
            TB316_CBO_GRUPO tb316 = TB316_CBO_GRUPO.RetornaPelaChavePrimaria(Convert.ToString(QueryStringAuxili.RetornaQueryStringComoIntPelaChave("ID")));
            return (tb316 != null ? tb316 : new TB316_CBO_GRUPO());
        }
        #endregion

    }
}