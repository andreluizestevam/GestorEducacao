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
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0500_SuporteOperacionalGE.F0505_AssociaUsuarioGrafico
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

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao)) 
            {
                CarregaUsuarios();
                CarregaTituloGrafico();
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int idUsuario = int.Parse(ddlUsuario.SelectedValue);
            int idTitulGrafi = int.Parse(ddlTitulGrafi.SelectedValue);

            TB308_GRAFI_USUAR tb308 = RetornaEntidade();

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                var ocorr = from iTb308 in TB308_GRAFI_USUAR.RetornaTodosRegistros()
                            where iTb308.ADMUSUARIO.ideAdmUsuario == idUsuario && iTb308.TB307_GRAFI_GERAL.ID_GRAFI_GERAL == idTitulGrafi
                            select iTb308;

                if (ocorr.Count() > 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Associação já realizada.");
                    return;
                }
            }
            else if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
            {
                int idGrafiUsuar = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);

                var ocorr = from iTb308 in TB308_GRAFI_USUAR.RetornaTodosRegistros()
                            where iTb308.ADMUSUARIO.ideAdmUsuario == idUsuario && iTb308.TB307_GRAFI_GERAL.ID_GRAFI_GERAL == idTitulGrafi
                            && iTb308.ID_GRAFI_USUAR != idGrafiUsuar
                            select iTb308;

                if (ocorr.Count() > 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Associação já realizada.");
                    return;
                }
            }


            tb308.ADMUSUARIO = ADMUSUARIO.RetornaPelaChavePrimaria(int.Parse(ddlUsuario.SelectedValue));
            tb308.TB307_GRAFI_GERAL = TB307_GRAFI_GERAL.RetornaPelaChavePrimaria(int.Parse(ddlTitulGrafi.SelectedValue));
            tb308.FLA_STATUS = ddlStatus.SelectedValue;

            CurrentPadraoCadastros.CurrentEntity = tb308;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB308_GRAFI_USUAR tb308 = RetornaEntidade();

            if (tb308 != null)
            {
                tb308.ADMUSUARIOReference.Load();
                tb308.TB307_GRAFI_GERALReference.Load();

                CarregaUsuarios();
                ddlUsuario.SelectedValue = tb308.ADMUSUARIO.ideAdmUsuario.ToString();
                CarregaTituloGrafico();
                ddlTitulGrafi.SelectedValue = tb308.TB307_GRAFI_GERAL.ID_GRAFI_GERAL.ToString();
                ddlStatus.SelectedValue = tb308.FLA_STATUS;
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB308_GRAFI_USUAR</returns>
        private TB308_GRAFI_USUAR RetornaEntidade()
        {
            TB308_GRAFI_USUAR tb308 = TB308_GRAFI_USUAR.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb308 == null) ? new TB308_GRAFI_USUAR() : tb308;
        }

        #endregion       

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Títulos dos Gráficos
        /// </summary>
        private void CarregaTituloGrafico()
        {
            ddlTitulGrafi.DataSource = (from tb307 in TB307_GRAFI_GERAL.RetornaTodosRegistros()
                                        where tb307.CO_STATUS_GRAFI == "A" && tb307.CO_CLASS_GRAFI == "R"
                                        select new { tb307.ID_GRAFI_GERAL, tb307.NM_TITULO_GRAFI }).Distinct().OrderBy(b => b.NM_TITULO_GRAFI);

            ddlTitulGrafi.DataValueField = "ID_GRAFI_GERAL";
            ddlTitulGrafi.DataTextField = "NM_TITULO_GRAFI";
            ddlTitulGrafi.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Grupo de Usuários
        /// </summary>
        private void CarregaUsuarios()
        {
            ddlUsuario.DataSource = (from admUsuario in ADMUSUARIO.RetornaTodosRegistros()
                                     join tb03 in TB03_COLABOR.RetornaTodosRegistros() on admUsuario.CodUsuario equals tb03.CO_COL
                                     select new { tb03.NO_COL, admUsuario.ideAdmUsuario }).OrderBy(a => a.NO_COL);

            ddlUsuario.DataTextField = "NO_COL";
            ddlUsuario.DataValueField = "ideAdmUsuario";
            ddlUsuario.DataBind();
        }

        #endregion
    }
}