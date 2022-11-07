//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: DADOS OPERACIONAIS DE CONTRATO
// OBJETIVO: DEPARTAMENTO INSTITUCIONAL.
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//------------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
//09/12/2016 | Alex Ribeiro               | Criada a funcionalidade

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

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0100_DadosOperContrato.F0105_TipoDepartamentoInstitucional
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
            if(String.IsNullOrEmpty(ddlClass.SelectedValue)){
                AuxiliPagina.EnvioMensagemErro(this.Page,"Selecione uma classificação para o tipo de Departamento/Local");
                ddlClass.Focus();
                return;
            }

            TB174_DEPTO_TIPO tb147 = RetornaEntidade();

            if (tb147 == null)
            {
                tb147 = new TB174_DEPTO_TIPO();

                tb147.NO_DEPTO_TIPO = txtNome.Text;
                tb147.DE_DEPTO_TIPO = txtDescricao.Text;
                tb147.CO_SITU_TIPO = ddlSitua.SelectedValue;
                tb147.CO_CLASS_TIPO_LOCAL = ddlClass.SelectedValue;
                tb147.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, LoginAuxili.CO_COL);
                tb147.DT_TIPO_CAD = DateTime.Now;
                tb147.CO_COL_SITU = LoginAuxili.CO_COL;
                tb147.CO_EMP_SITU = LoginAuxili.CO_EMP;
                tb147.DT_TIPO_SITU = DateTime.Now;
            }
            else
            {
                tb147.TB03_COLABORReference.Load();

                tb147.NO_DEPTO_TIPO = txtNome.Text;
                tb147.DE_DEPTO_TIPO = txtDescricao.Text;
                tb147.CO_SITU_TIPO = ddlSitua.SelectedValue;
                tb147.CO_CLASS_TIPO_LOCAL = ddlClass.SelectedValue;
                tb147.CO_COL_SITU = LoginAuxili.CO_COL;
                tb147.CO_EMP_SITU = LoginAuxili.CO_EMP;
                tb147.DT_TIPO_SITU = DateTime.Now;
            }           

            CurrentPadraoCadastros.CurrentEntity = tb147;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB174_DEPTO_TIPO tb174 = RetornaEntidade();

            if (tb174 != null)
            {
                txtNome.Text = tb174.NO_DEPTO_TIPO;
                txtDescricao.Text = tb174.DE_DEPTO_TIPO;
                ddlSitua.SelectedValue = tb174.CO_SITU_TIPO;
                ddlClass.SelectedValue = tb174.CO_CLASS_TIPO_LOCAL;
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB14_DEPTO</returns>
        private TB174_DEPTO_TIPO RetornaEntidade()
        {
            return TB174_DEPTO_TIPO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
        }        
        #endregion
    }
}
