//=============================================================================
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
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5900_TabelasGenerCtrlFinaceira.F5901_FormaPagto
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
            TB22_TIPOPAG tb22 = RetornaEntidade();

            if (!QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                tb22 = RetornaEntidade();
            else
                tb22 = new TB22_TIPOPAG();

            tb22.NO_TIPO_PAGA = txtDescricao.Text;
            tb22.NU_PARC_PAGA = int.Parse(txtNParcela.Text);
            tb22.QT_DIAS_PARC = int.Parse(txtQtdDias.Text);
            tb22.PE_CORR_PARC = decimal.Parse(txtCorrecaoParcela.Text);
            tb22.CO_INDIENT_PARC = ddlReceberValorEntrada.SelectedValue;

            CurrentPadraoCadastros.CurrentEntity = tb22;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB22_TIPOPAG tb22 = RetornaEntidade();

            if (tb22 != null)
            {
                txtDescricao.Text = tb22.NO_TIPO_PAGA;
                txtNParcela.Text = tb22.NU_PARC_PAGA.ToString();
                txtQtdDias.Text = tb22.QT_DIAS_PARC.ToString();
                txtCorrecaoParcela.Text = tb22.PE_CORR_PARC != null ? tb22.PE_CORR_PARC.Value.ToString("N2") : "";
                ddlReceberValorEntrada.SelectedValue = tb22.CO_INDIENT_PARC.ToString();                
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB22_TIPOPAG</returns>
        private TB22_TIPOPAG RetornaEntidade()
        {
            return TB22_TIPOPAG.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
        }
        #endregion
    }
}
