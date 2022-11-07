//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: BAIRRO
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0500_SuporteOperacionalGE.F0506_CadastroExportacaoDados
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
            if (IsPostBack)
             return;
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TB309_CONTR_EXPOR_DADOS tb309 = RetornaEntidade();

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                if (ddlAtualCompl.SelectedValue == "S")
                {
                    var ocorAtuComp = (from lTb309 in TB309_CONTR_EXPOR_DADOS.RetornaTodosRegistros()
                                       where lTb309.FL_ATUAL_COMPL == "S"
                                       select lTb309).FirstOrDefault();

                    if (ocorAtuComp != null)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Só é permitido um único registro para Atualização Completa!.");
                        return;
                    }
                }
            }
            else
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                {
                    int idOcor = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);

                    var ocorAtuComp = (from lTb309 in TB309_CONTR_EXPOR_DADOS.RetornaTodosRegistros()
                                       where lTb309.FL_ATUAL_COMPL == "S" && lTb309.ID_EXPDADOS != idOcor
                                       select lTb309).FirstOrDefault();

                    if (ocorAtuComp != null)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Só é permitido um único registro para Atualização Completa!.");
                        return;
                    }
                }

            if (ddlAtualCompl.SelectedValue == "S")
            {
                tb309.DE_FUNCI_EXPDADOS = "Atualização completa Portal de Relacionamento";
                tb309.DE_MODULO_EXPDADOS = "Todos";
                tb309.DE_TABELA_DESTIN_EXPDADOS = "Todas";
                tb309.DE_TABELA_ORIGEM_EXPDADOS = "Todas";
                tb309.FL_ATUAL_COMPL = ddlAtualCompl.SelectedValue;
                tb309.CO_STATUS_EXPDADOS = ddlStatus.SelectedValue;
            }
            else
            {

                tb309.DE_FUNCI_EXPDADOS = txtFunciExpor.Text;
                tb309.DE_MODULO_EXPDADOS = txtModulExpor.Text;
                tb309.DE_TABELA_DESTIN_EXPDADOS = txtTabelDestino.Text;
                tb309.DE_TABELA_ORIGEM_EXPDADOS = txtTabelOrigem.Text;
                tb309.FL_ATUAL_COMPL = ddlAtualCompl.SelectedValue;
                tb309.CO_STATUS_EXPDADOS = ddlStatus.SelectedValue;
            }

            CurrentPadraoCadastros.CurrentEntity = tb309;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB309_CONTR_EXPOR_DADOS tb309 = RetornaEntidade();

            if (tb309 != null)
            {
                txtFunciExpor.Text = tb309.DE_FUNCI_EXPDADOS;
                txtModulExpor.Text = tb309.DE_MODULO_EXPDADOS;
                txtTabelOrigem.Text = tb309.DE_TABELA_ORIGEM_EXPDADOS;
                txtTabelDestino.Text = tb309.DE_TABELA_DESTIN_EXPDADOS;
                ddlAtualCompl.SelectedValue = tb309.FL_ATUAL_COMPL;
                ddlStatus.SelectedValue = tb309.CO_STATUS_EXPDADOS;

                if (ddlAtualCompl.SelectedValue == "S")
                {
                    txtFunciExpor.Enabled = txtModulExpor.Enabled = txtTabelOrigem.Enabled = 
                    txtTabelDestino.Enabled =  ddlAtualCompl.Enabled = false;
                }
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB309_CONTR_EXPOR_DADOS</returns>
        private TB309_CONTR_EXPOR_DADOS RetornaEntidade()
        {
            TB309_CONTR_EXPOR_DADOS tb309 = TB309_CONTR_EXPOR_DADOS.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb309 == null) ? new TB309_CONTR_EXPOR_DADOS() : tb309;
        }

        #endregion       
    }
}