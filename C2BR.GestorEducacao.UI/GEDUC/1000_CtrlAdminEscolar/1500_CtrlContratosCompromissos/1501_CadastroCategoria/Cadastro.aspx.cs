//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: CONTROLE DE CONTRATOS DE COMPROMISSOS
// OBJETIVO: CADASTRAMENTO DE TIPOS DE CATEGORIAS DE CONTRATOS DE COMPROMISSOS
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
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1500_CtrlContratosCompromissos.F1501_CadastroCategoria
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

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TB313_CATEG_CONTR tb313 = RetornaEntidade();

            tb313.CO_CATEG_CONTR = txtCodigo.Text;
            tb313.NM_CATEG_CONTR = txtNome.Text;
            tb313.CO_SITUACAO = ddlStatus.SelectedValue;
            tb313.DT_SITUACAO = DateTime.Now;

            CurrentPadraoCadastros.CurrentEntity = tb313;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB313_CATEG_CONTR tb313 = RetornaEntidade();

            if (tb313 != null)
            {
                txtCodigo.Text = tb313.CO_CATEG_CONTR;
                txtNome.Text = tb313.NM_CATEG_CONTR;
                ddlStatus.SelectedValue = tb313.CO_SITUACAO;
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB313_CATEG_CONTR</returns>
        private TB313_CATEG_CONTR RetornaEntidade()
        {
            TB313_CATEG_CONTR tb313 = TB313_CATEG_CONTR.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb313 == null) ? new TB313_CATEG_CONTR() : tb313;
        }
        #endregion        
    }
}