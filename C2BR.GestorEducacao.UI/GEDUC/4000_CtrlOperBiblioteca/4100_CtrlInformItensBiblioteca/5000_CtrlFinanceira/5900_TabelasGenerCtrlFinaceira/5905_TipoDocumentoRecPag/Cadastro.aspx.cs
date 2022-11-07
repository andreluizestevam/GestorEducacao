﻿//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: **********************
// SUBMÓDULO: *******************
// OBJETIVO: ********************
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
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5900_TabelasGenerCtrlFinaceira.F5905_TipoDocumentoRecPag
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
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoExclusao) || QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                    CarregaFormulario();
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TB086_TIPO_DOC tb086 = RetornaEntidade();

            if (tb086 == null)
                tb086 = new TB086_TIPO_DOC();

            tb086.DES_TIPO_DOC = txtDescricao.Text;
            tb086.SIG_TIPO_DOC = txtSigla.Text.Trim().ToUpper();

            CurrentPadraoCadastros.CurrentEntity = tb086;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB086_TIPO_DOC tb086 = RetornaEntidade();

            if (tb086 != null)
            {
                txtDescricao.Text = tb086.DES_TIPO_DOC.ToString();
                txtSigla.Text = tb086.SIG_TIPO_DOC.ToString(); 
            }                      
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB086_TIPO_DOC</returns>
        private TB086_TIPO_DOC RetornaEntidade()
        {
            return TB086_TIPO_DOC.RetornaPeloCoTipoDoc(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
        }
        #endregion
    }
}
