//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE OPERACIONAL DE REGIÕES DE ENSINO
// OBJETIVO: CADASTRAMENTO DE TIPOS DE AVALIAÇÕES DE DESEMPENHO DE REGIÕES DE ENSINO ESCOLAR
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3800_CtrlEncerramentoLetivo.F3810_CtrlRegioesEnsino.F3813_CadastramentoTpAvalDesempRegEnsino
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
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                    CarregaFormulario();
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TB_TIPO_AVAL_INST tbTpAvalInst = RetornaEntidade();

            if (tbTpAvalInst == null)
                tbTpAvalInst = new TB_TIPO_AVAL_INST();

            tbTpAvalInst.NO_TP_AVAL_INST = txtNAvaliacao.Text;
            tbTpAvalInst.CO_SIGLA_TP_AVAL_INST = txtSigla.Text.ToUpper();
            tbTpAvalInst.NO_ASPECT_AVAL1 = txtAsp1.Text;
            tbTpAvalInst.NO_ASPECT_AVAL2 = txtAsp2.Text;
            tbTpAvalInst.NO_ASPECT_AVAL3 = txtAsp3.Text;
            tbTpAvalInst.NO_ASPECT_AVAL4 = txtAsp4.Text;
            tbTpAvalInst.NO_ASPECT_AVAL5 = txtAsp5.Text;
            tbTpAvalInst.NO_ASPECT_AVAL6 = txtAsp6.Text;

            CurrentPadraoCadastros.CurrentEntity = tbTpAvalInst;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB_TIPO_AVAL_INST tbTpAvalInst = RetornaEntidade();

            if (tbTpAvalInst != null)
            {
                txtNAvaliacao.Text = tbTpAvalInst.NO_TP_AVAL_INST;
                txtSigla.Text = tbTpAvalInst.CO_SIGLA_TP_AVAL_INST;                
                txtAsp1.Text = tbTpAvalInst.NO_ASPECT_AVAL1;
                txtAsp2.Text = tbTpAvalInst.NO_ASPECT_AVAL2;
                txtAsp3.Text = tbTpAvalInst.NO_ASPECT_AVAL3;
                txtAsp4.Text = tbTpAvalInst.NO_ASPECT_AVAL4;
                txtAsp5.Text = tbTpAvalInst.NO_ASPECT_AVAL5;
                txtAsp6.Text = tbTpAvalInst.NO_ASPECT_AVAL6;
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB_TIPO_AVAL_INST</returns>
        private TB_TIPO_AVAL_INST RetornaEntidade()
        {
            return TB_TIPO_AVAL_INST.RetornaPeloCoTpAvalInst(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
        }
        #endregion
    }
}
