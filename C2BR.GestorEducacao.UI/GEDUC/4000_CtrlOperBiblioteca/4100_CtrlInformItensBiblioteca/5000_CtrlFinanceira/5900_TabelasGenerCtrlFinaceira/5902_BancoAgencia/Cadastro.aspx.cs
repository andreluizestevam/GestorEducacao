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
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 20/03/2013| André Nobre Vinagre        | Alteração da máscara do nosso número.
//           |                            | 
//           |                            | 

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5900_TabelasGenerCtrlFinaceira.F5902_BancoAgencia
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
        void CurrentPadraoCadastros_OnCarregaFormulario()
        {
            CarregaFormulario();

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                txtIDEBANCO.Enabled = false;
        }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int ocorTb29 = (from lTb29 in TB29_BANCO.RetornaTodosRegistros()
                            where lTb29.IDEBANCO == txtIDEBANCO.Text
                            select new { lTb29.IDEBANCO }).Count();

            if (ocorTb29 > 0 && QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                AuxiliPagina.EnvioMensagemErro(this, "Já existe esse Número de Banco cadastrado");
            else
            {
                TB29_BANCO tb29 = RetornaEntidade();

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                    tb29 = RetornaEntidade();
                else
                {
                    tb29 = new TB29_BANCO();
                    tb29.IDEBANCO = txtIDEBANCO.Text;
                }

                tb29.DESBANCO = txtDESBANCO.Text;
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                {
                    if (txtNossoNumero.Text != "" && tb29.CO_PROX_NOS_NUM != null)
                    {
                        if (Decimal.Parse(txtNossoNumero.Text) < Decimal.Parse(tb29.CO_PROX_NOS_NUM))
                        {
                            AuxiliPagina.EnvioMensagemErro(this, "Nosso número informado não pode ser cadastrado, pois é menor que o já cadastrado.");
                            return;
                        }
                        else
                            tb29.CO_PROX_NOS_NUM = txtNossoNumero.Text != "" ? txtNossoNumero.Text : null;
                    }
                    else
                        tb29.CO_PROX_NOS_NUM = txtNossoNumero.Text != "" ? txtNossoNumero.Text : null;
                }
                else
                    tb29.CO_PROX_NOS_NUM = txtNossoNumero.Text != "" ? txtNossoNumero.Text : null;

                CurrentPadraoCadastros.CurrentEntity = tb29;
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB29_BANCO tb29 = RetornaEntidade();

            if (tb29 != null)
            {
                txtIDEBANCO.Text = tb29.IDEBANCO;
                txtDESBANCO.Text = tb29.DESBANCO;
                txtNossoNumero.Text = tb29.CO_PROX_NOS_NUM != null ? tb29.CO_PROX_NOS_NUM : "";
            }   
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB29_BANCO</returns>
        private TB29_BANCO RetornaEntidade()
        {
            TB29_BANCO tb29 = TB29_BANCO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Id));
            return (tb29 == null) ? new TB29_BANCO() : tb29;
        }
        #endregion        
    }
}
