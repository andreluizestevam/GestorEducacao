﻿//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL DE ESTOQUE
// SUBMÓDULO: DADOS CADASTRAIS DE ITENS DE ESTOQUE
// OBJETIVO: CADASTRO DE TIPOS DE MOVIMENTAÇÃO DE ITENS DE ESTOQUE
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
namespace C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6200_CtrlOperMateriais.F6260_CtrlInformacoesItensEstoque.F6264_CadastroTipoMovtoItemEstoque
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
            TB93_TIPO_MOVIMENTO tb93 = RetornaEntidade();

            tb93.DE_TIPO_MOV = txtDescricao.Text;
            tb93.DT_ALT_REGISTRO = DateTime.Now;
            tb93.FLA_TP_MOV = ddlTipoMovimento.SelectedValue;

            string strNomeUsuario = LoginAuxili.NOME_USU_LOGADO;

            tb93.NOM_USUARIO = strNomeUsuario.ToString().Substring(0, (strNomeUsuario.Length > 30 ? 29 : strNomeUsuario.Length));

            CurrentPadraoCadastros.CurrentEntity = tb93;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB93_TIPO_MOVIMENTO tb93 = RetornaEntidade();

            if (tb93 != null)
            {
                txtDescricao.Text = tb93.DE_TIPO_MOV;
                ddlTipoMovimento.SelectedValue = tb93.FLA_TP_MOV;
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB93_TIPO_MOVIMENTO</returns>
        private TB93_TIPO_MOVIMENTO RetornaEntidade()
        {
            TB93_TIPO_MOVIMENTO tb93 = TB93_TIPO_MOVIMENTO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb93 == null) ? new TB93_TIPO_MOVIMENTO() : tb93;
        }
        #endregion
    }
}
