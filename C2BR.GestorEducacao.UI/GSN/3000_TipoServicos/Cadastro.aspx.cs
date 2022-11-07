//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: TIPOS DE UNIDADES DA FEDERAÇÃO
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections;
using System.Configuration;
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

namespace C2BR.GestorEducacao.UI.GSN._3000_TipoServicos
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
            if (Page.IsPostBack) return;

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                txtSigla.Enabled = true;
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TSN017_TIPO_SERVICO TSN017;

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                TSN017 = new TSN017_TIPO_SERVICO();
                TSN017.DE_SIGLA = txtSigla.Text;
                TSN017.DE_SERVICO = txtServico.Text;
                TSN017.CO_TIPO = txtTipo.Text;
                TSN017.CO_SITUACAO = ddlStatus.SelectedValue;
            }
            else
                TSN017 = RetornaEntidade();

            TSN017.DE_SIGLA = txtSigla.Text;

            CurrentPadraoCadastros.CurrentEntity = TSN017;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TSN017_TIPO_SERVICO TSN017 = RetornaEntidade();

            if (TSN017 != null)
            {
                txtSigla.Text = TSN017.DE_SIGLA;
                txtServico.Text = TSN017.DE_SERVICO;
                txtTipo.Text = TSN017.CO_TIPO;
                ddlStatus.SelectedValue = TSN017.CO_SITUACAO;
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TSN017_TIPO_SERVICO</returns>
        private TSN017_TIPO_SERVICO RetornaEntidade()
        {
            TSN017_TIPO_SERVICO TSN017 = TSN017_TIPO_SERVICO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Id));
            return (TSN017 == null) ? new TSN017_TIPO_SERVICO() : TSN017;
        }
        #endregion        
    }
}