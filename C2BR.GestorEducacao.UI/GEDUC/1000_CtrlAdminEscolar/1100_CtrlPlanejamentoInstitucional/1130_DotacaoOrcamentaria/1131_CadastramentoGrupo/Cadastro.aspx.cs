//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: DOTAÇÃO ORÇAMENTÁRIA
// OBJETIVO: CADASTRAMENTO GRUPO DOTAÇÃO ORÇAMENTÁRIA
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
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1130_DotacaoOrcamentaria.F1131_CadastramentoGrupo
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
            {
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                    txtDtSituacao.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TB302_DOTAC_GRUPO tb302 = RetornaEntidade();

            if (tb302 == null)
            {
                //int idGrupo = int.Parse(txtNumGrupo.Text);

                //int ocorGrupo = (from iTb302 in TB302_DOTAC_GRUPO.RetornaTodosRegistros()
                //                 where iTb302.ID_DOTAC_GRUPO == idGrupo
                //                 select iTb302).Count();

                //if (ocorGrupo > 0)
                //{
                //    AuxiliPagina.EnvioMensagemErro(this.Page, "Grupo com número informado já cadastrado.");
                //    return;
                //}

                tb302 = new TB302_DOTAC_GRUPO();
                tb302.DT_CADAS_DOTAC_GRUPO = DateTime.Now;
                tb302.ID_DOTAC_GRUPO = int.Parse(txtNumGrupo.Text);
            }
            else
            {
//                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
//                {
//                    int idGrupo = int.Parse(txtNumGrupo.Text);
////----------------> Id pego pela queryString
//                    int idQSGrupo = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);

//                    int ocorGrupo = (from iTb302 in TB302_DOTAC_GRUPO.RetornaTodosRegistros()
//                                     where iTb302.ID_DOTAC_GRUPO == idGrupo && iTb302.ID_DOTAC_GRUPO != idQSGrupo
//                                     select iTb302).Count();

//                    if (ocorGrupo > 0)
//                    {
//                        AuxiliPagina.EnvioMensagemErro(this.Page, "Grupo com número informado já cadastrado.");
//                        return;
//                    }
//                }
            }
            
            tb302.DE_DOTAC_GRUPO = txtDescricaoGrupo.Text;
            tb302.SIGLA_DOTAC_GRUPO = txtSiglaGrupo.Text.ToUpper();
            tb302.DT_SITUA_DOTAC_GRUPO = DateTime.Now;
            tb302.CO_SITUA_DOTAC_GRUPO = ddlSituacao.SelectedValue;

            CurrentPadraoCadastros.CurrentEntity = tb302;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB302_DOTAC_GRUPO tb302 = RetornaEntidade();

            if (tb302 != null)
            {
                txtNumGrupo.Text = tb302.ID_DOTAC_GRUPO.ToString();
                txtNumGrupo.Enabled = false;
                txtDescricaoGrupo.Text = tb302.DE_DOTAC_GRUPO;
                txtSiglaGrupo.Text = tb302.SIGLA_DOTAC_GRUPO;
                txtDtSituacao.Text = tb302.DT_SITUA_DOTAC_GRUPO.ToString("dd/MM/yyyy");
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB302_DOTAC_GRUPO</returns>
        private TB302_DOTAC_GRUPO RetornaEntidade()
        {
            return TB302_DOTAC_GRUPO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
        }
        #endregion
    }
}