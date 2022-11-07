//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: TABELAS DE APOIO OPERACIONAL PEDAGÓGICO
// OBJETIVO: CADASTRAMENTO DE TIPOS DE ATIVIDADE
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//------------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 23/04/2013| Victor Martins Machado     | Foram criados os campos Sigla, Classificação,
//           |                            | Peso, Lança Nota? e Situação do tipo de atividade.
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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3090_TabelasGeraisCtrlPedagogico.F3091_TipoAtividade
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
            TB273_TIPO_ATIVIDADE tb273 = RetornaEntidade();

            tb273.NO_TIPO_ATIV = txtNome.Text;
            tb273.DE_TIPO_ATIV = txtDescricao.Text;
            tb273.CO_SIGLA_ATIV = txtSigla.Text;
            tb273.CO_PESO_ATIV = int.Parse(txtPeso.Text);
            tb273.CO_SITUA_ATIV = ddlSituacao.SelectedValue;
            tb273.CO_CLASS_ATIV = ddlClass.SelectedValue;
            tb273.FL_LANCA_NOTA_ATIV = ddlLancaNota.SelectedValue;
            tb273.CO_TIPO_ENSINO = drpTipoEnsino.SelectedValue;

            CurrentPadraoCadastros.CurrentEntity = tb273;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB273_TIPO_ATIVIDADE tb273 = RetornaEntidade();

            if (tb273 != null)
            {
                txtNome.Text = tb273.NO_TIPO_ATIV;
                txtDescricao.Text = tb273.DE_TIPO_ATIV;
                txtSigla.Text = tb273.CO_SIGLA_ATIV;
                txtPeso.Text = tb273.CO_PESO_ATIV.ToString();
                ddlSituacao.SelectedValue = tb273.CO_SITUA_ATIV;
                ddlClass.SelectedValue = tb273.CO_CLASS_ATIV;
                ddlLancaNota.SelectedValue = tb273.FL_LANCA_NOTA_ATIV;
                drpTipoEnsino.SelectedValue = tb273.CO_TIPO_ENSINO;
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB273_TIPO_ATIVIDADE</returns>
        private TB273_TIPO_ATIVIDADE RetornaEntidade()
        {
            TB273_TIPO_ATIVIDADE tb273 = TB273_TIPO_ATIVIDADE.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb273 == null) ? new TB273_TIPO_ATIVIDADE() : tb273;
        }
        #endregion        

        protected void txtPeso_TextChanged(object sender, EventArgs e)
        {
            string v = txtPeso.Text;
            int i;
            if (!int.TryParse(v, out i))
            {
                AuxiliPagina.EnvioMensagemErro(this, "O peso do tipo de atividade deve conter somente números");
                txtPeso.Text = "";
                return;
            }
        }
    }
}