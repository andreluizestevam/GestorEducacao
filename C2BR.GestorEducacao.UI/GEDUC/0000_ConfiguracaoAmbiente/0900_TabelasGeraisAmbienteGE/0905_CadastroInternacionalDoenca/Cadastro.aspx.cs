//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: CADASTRO INTERNACIONAL DE DOENÇAS.
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
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0900_TabelasGeraisAmbienteGE.F0905_CadastroInternacionalDoenca
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                carregaCIDGeral();
            }
        }


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
            TB117_CODIGO_INTERNACIONAL_DOENCA tb117 = RetornaEntidade();

            tb117.CO_CID = txtCID.Text.Trim().ToUpper();
            tb117.NO_CID = txtDescricaoCID.Text;
            //tb117.CO_SIGLA_CID = txtSigla.Text;
            if (!string.IsNullOrEmpty(ddlCIDGeral.SelectedValue))
            { tb117.TBS223_CID.ID_CID_GRUPO = int.Parse(ddlCIDGeral.SelectedValue); }

            CurrentPadraoCadastros.CurrentEntity = tb117;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB117_CODIGO_INTERNACIONAL_DOENCA tb117 = RetornaEntidade();

            tb117.TBS223_CIDReference.Load();

            if (tb117 != null)
            {
                txtCID.Text = tb117.CO_CID.ToString();
                txtDescricaoCID.Text = tb117.NO_CID.ToString();
                ddlCIDGeral.SelectedValue = tb117.TBS223_CID.ID_CID_GRUPO.ToString();
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB117_CODIGO_INTERNACIONAL_DOENCA</returns>
        private TB117_CODIGO_INTERNACIONAL_DOENCA RetornaEntidade()
        {
            TB117_CODIGO_INTERNACIONAL_DOENCA tb117 = TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb117 == null) ? new TB117_CODIGO_INTERNACIONAL_DOENCA() : tb117;
        }

        /// <summary>
        /// Carrega todos os CID Gerais
        /// </summary>
        private void carregaCIDGeral()
        {
            var res = (from tbs223 in TBS223_CID.RetornaTodosRegistros()
                       where tbs223.CO_SITUA_CID_GRUPO == "A"
                       select new
                       {
                           nomeCID = tbs223.NO_CID_GRUPO,
                           idCID = tbs223.ID_CID_GRUPO,
                       }).ToList();

            if (res != null)
            {
                ddlCIDGeral.DataTextField = "nomeCID";
                ddlCIDGeral.DataValueField = "idCID";
                ddlCIDGeral.DataSource = res;
                ddlCIDGeral.DataBind();
            }

            ddlCIDGeral.Items.Insert(0, new ListItem("Todos", "0"));
        }

        #endregion
    }
}
