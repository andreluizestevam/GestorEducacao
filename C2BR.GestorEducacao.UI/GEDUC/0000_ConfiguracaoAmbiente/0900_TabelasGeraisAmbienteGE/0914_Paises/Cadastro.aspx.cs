//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: CADASTRAMENTO DE PAÍSES
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
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.App_Masters;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0900_TabelasGeraisAmbienteGE.F0914_Paises
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
            TB299_PAISES tb299 = RetornaEntidade();

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                var ocorr = (from iTb299 in TB299_PAISES.RetornaTodosRegistros()
                             where iTb299.CO_ISO_PAISES == txtCodISO.Text || iTb299.CO_ISO3_PAISES == txtCodISO3.Text
                             select iTb299).FirstOrDefault();

                if (ocorr != null)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "País já cadastrado.");
                    return;
                }
            }
            else if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Não é permitida alteração ou exclusão.");
                return;
            }

            tb299.CO_ISO_PAISES = txtCodISO.Text.Trim().ToUpper();
            tb299.CO_ISO3_PAISES = txtCodISO3.Text.Trim().ToUpper();
            tb299.ID_PAISES = txtIDPais.Text != "" ? (short?)short.Parse(txtIDPais.Text) : null;
            tb299.NO_PAISES = txtNomePais.Text;

            CurrentPadraoCadastros.CurrentEntity = tb299;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB299_PAISES tb299 = RetornaEntidade();

            if (tb299 != null)
            {
                txtCodISO.Text = tb299.CO_ISO_PAISES.ToString();
                txtCodISO3.Text = tb299.CO_ISO3_PAISES.ToString();
                txtIDPais.Text = tb299.ID_PAISES != null ? tb299.ID_PAISES.ToString() : "";
                txtNomePais.Text = tb299.NO_PAISES; 
            }                       
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns></returns>
        private TB299_PAISES RetornaEntidade()
        {
            TB299_PAISES tb299 = TB299_PAISES.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Id));
            return (tb299 == null) ? new TB299_PAISES() : tb299;
        }
        #endregion
    }
}
