//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: CONFIGURAÇÃO DE MÓDULOS E FUNCIONALIDADES
// OBJETIVO: CADASTRO DE BOLETIM
// DATA DE CRIAÇÃO: 02/05/2013
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//------------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 02/05/2013| Victor Martins Machado     | Criação da funcionalidade
//           |                            | 

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
namespace C2BR.GestorEducacao.UI.GEDUC._0000_ConfiguracaoAmbiente._0200_ConfiguracaoModulos._0204_CadastroBoletim
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

        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            ADMBOLETIM admBol = RetornaEntidade();

            admBol.NO_BOL = txtNome.Text;
            admBol.CLA_BOL = txtClasse.Text;
            admBol.FL_STAT = ddlStatus.SelectedValue;
            admBol.DE_BOL = txtDescricao.Text;

            if (admBol.DT_CAD_BOL.ToString() == "")
            {
                admBol.DT_CAD_BOL = DateTime.Now;
            }

            CurrentPadraoCadastros.CurrentEntity = admBol;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            var admBol = RetornaEntidade();

            if (admBol != null)
            {
                txtNome.Text = admBol.NO_BOL;
                txtClasse.Text = admBol.CLA_BOL;
                txtDescricao.Text = admBol.DE_BOL;
                ddlStatus.SelectedValue = admBol.FL_STAT;
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade ADMMODULO</returns>
        private ADMBOLETIM RetornaEntidade()
        {
            ADMBOLETIM admBol = ADMBOLETIM.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (admBol == null) ? new ADMBOLETIM() : admBol;
        }

        #endregion
    }
}