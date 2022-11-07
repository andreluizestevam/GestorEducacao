//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: PLANEJAMENTO ESCOLAR (FINANCEIRO)
// OBJETIVO: CADASTRAMENTO DE GRUPO DE CONTAS CONTÁBIL
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
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1110_PlanejEscolarFinanceiro.F1111_CadastramentoGrupoCtaContabil
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
            int intNrGrupo;
            if (!int.TryParse(txtNumGrupo.Text, out intNrGrupo))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Número informado não é válido");
                return;
            }

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                int nuGrp = int.Parse(txtNumGrupo.Text);
                
                int ocorrGrp = (from lTb53 in TB53_GRP_CTA.RetornaTodosRegistros()
                                where lTb53.TP_GRUP_CTA.Equals(ddlTipoConta.SelectedValue)
                                && lTb53.NR_GRUP_CTA == nuGrp
                                select new { lTb53.CO_GRUP_CTA }).Count();

                if (ocorrGrp > 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Número de grupo já cadastrado para tipo de conta informado.");
                    return;
                }
            }
            else if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
            {
                int nuGrp = int.Parse(txtNumGrupo.Text);
                int idGrp = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);

                int ocorrGrp = (from lTb53 in TB53_GRP_CTA.RetornaTodosRegistros()
                                where lTb53.TP_GRUP_CTA.Equals(ddlTipoConta.SelectedValue)
                                && lTb53.NR_GRUP_CTA == nuGrp && lTb53.CO_GRUP_CTA != idGrp
                                select new { lTb53.CO_GRUP_CTA }).Count();

                if (ocorrGrp > 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Número de grupo já cadastrado para tipo de conta informado.");
                    return;
                }
            }

            TB53_GRP_CTA tb53 = RetornaEntidade();

            if (tb53 == null)
                tb53 = new TB53_GRP_CTA();

            tb53.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
            tb53.TP_GRUP_CTA = ddlTipoConta.SelectedValue;                
            tb53.DE_GRUP_CTA = txtDescricaoGrupo.Text;
            tb53.NR_GRUP_CTA = int.Parse(txtNumGrupo.Text);
            tb53.DT_ALT_REGISTRO = DateTime.Now;

            CurrentPadraoCadastros.CurrentEntity = tb53;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {            
            TB53_GRP_CTA tb53 = RetornaEntidade();

            if (tb53 != null)
            {
                txtDescricaoGrupo.Text = tb53.DE_GRUP_CTA;
                ddlTipoConta.SelectedValue = tb53.TP_GRUP_CTA;
                txtNumGrupo.Text = tb53.NR_GRUP_CTA != null ? tb53.NR_GRUP_CTA.ToString().PadLeft(2,'0'): "";
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB53_GRP_CTA</returns>
        private TB53_GRP_CTA RetornaEntidade()
        {
            return TB53_GRP_CTA.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
        }
        #endregion
    }
}