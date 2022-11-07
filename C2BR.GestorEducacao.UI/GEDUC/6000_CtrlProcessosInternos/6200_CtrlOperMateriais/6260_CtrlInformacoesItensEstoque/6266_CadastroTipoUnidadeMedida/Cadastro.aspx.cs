//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL DE ESTOQUE
// SUBMÓDULO: DADOS CADASTRAIS DE ITENS DE ESTOQUE
// OBJETIVO: CADASTRO DO TIPO UNIDADES DE MEDIDA
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
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6200_CtrlOperMateriais.F6260_CtrlInformacoesItensEstoque.F6266_CadastroTipoUnidadeMedida
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
//------------> Se for edição deve desabilitar o campo txtSigla -> Sigla do Tipo de Unidade de Medida 
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                    txtSigla.Enabled = false;
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TB89_UNIDADES tb89 = RetornaEntidade();

            tb89.NO_UNID_ITEM = txtDescricao.Text;
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                tb89.SG_UNIDADE = txtSigla.Text.ToUpper();
            tb89.FL_CATEG_UNID = ddlCatUnidade.SelectedValue;            
            tb89.DT_ALT_REGISTRO = DateTime.Now;

            string strNomeUsuario = LoginAuxili.NOME_USU_LOGADO;

            tb89.NOM_USUARIO = strNomeUsuario.ToString().Substring(0, (strNomeUsuario.Length > 30 ? 29 : strNomeUsuario.Length));

            CurrentPadraoCadastros.CurrentEntity = tb89;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB89_UNIDADES tb89 = RetornaEntidade();

            if (tb89 != null)
            {                
                txtDescricao.Text = tb89.NO_UNID_ITEM;
                txtSigla.Text = tb89.SG_UNIDADE.ToString();
                ddlCatUnidade.SelectedValue = tb89.FL_CATEG_UNID;
            }                        
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB89_UNIDADES</returns>
        private TB89_UNIDADES RetornaEntidade()
        {
            TB89_UNIDADES tb89 = TB89_UNIDADES.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb89 == null) ? new TB89_UNIDADES() : tb89;
        }
        #endregion
    }
}
