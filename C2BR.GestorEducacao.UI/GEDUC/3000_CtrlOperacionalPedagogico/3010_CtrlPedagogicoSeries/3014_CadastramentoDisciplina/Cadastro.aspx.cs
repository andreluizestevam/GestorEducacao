//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: CONTROLE DE SÉRIES OU CURSOS
// OBJETIVO: CADASTRAMENTO DE DISCIPLINAS (MATÉRIAS)
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
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3010_CtrlPedagogicoSeries.F3014_CadastramentoDisciplina
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
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                    txtDataSituacao.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            DateTime dataRetorno = DateTime.Now;

            int ocorrDisciplina = (from lTb107 in TB107_CADMATERIAS.RetornaTodosRegistros()
                                   where lTb107.CO_EMP == LoginAuxili.CO_EMP && lTb107.NO_MATERIA == txtNome.Text
                                   select new { lTb107.NO_MATERIA }).Count();

            if (ocorrDisciplina > 0 && QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                AuxiliPagina.EnvioMensagemErro(this, "Disciplina já existe no sistema.");
            else
            {
                TB107_CADMATERIAS tb107 = RetornaEntidade();

                if (tb107.ID_MATERIA == 0)
                {
                    tb107 = new TB107_CADMATERIAS();
                    tb107.CO_EMP = LoginAuxili.CO_EMP;
                }
                else
                {
                    ocorrDisciplina = (from lTb107 in TB107_CADMATERIAS.RetornaTodosRegistros()
                                       where lTb107.CO_EMP == LoginAuxili.CO_EMP && lTb107.NO_MATERIA == txtNome.Text && lTb107.ID_MATERIA != tb107.ID_MATERIA
                                       select new { lTb107.NO_MATERIA }).Count();
                    if (ocorrDisciplina > 0)
                        AuxiliPagina.EnvioMensagemErro(this, "Disciplina já existe no sistema.");
                }

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {
                    tb107.FL_INCLU_MATERIA = true;
                    tb107.FL_ALTER_MATERIA = false;
                }

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                {
                    tb107.FL_ALTER_MATERIA = true;
                }   
                                
                tb107.NO_MATERIA = txtNome.Text;
                tb107.NO_SIGLA_MATERIA = txtSigla.Text;
                tb107.NO_RED_MATERIA = txtNomeReduzido.Text;
                tb107.DE_MATERIA = txtDescricao.Text;
                tb107.CO_CLASS_BOLETIM = int.Parse(ddlBoletim.SelectedValue);
                tb107.DT_STATUS = DateTime.TryParse(txtDataSituacao.Text, out dataRetorno) ? dataRetorno : DateTime.Now;
                tb107.CO_STATUS = ddlStatus.SelectedValue;
                
                CurrentPadraoCadastros.CurrentEntity = tb107;
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB107_CADMATERIAS tb107 = RetornaEntidade();

            if (tb107 != null)
            {
                txtDescricao.Text = tb107.DE_MATERIA;
                txtSigla.Text = tb107.NO_SIGLA_MATERIA;
                ddlStatus.SelectedValue = tb107.CO_STATUS;
                txtDataSituacao.Text = tb107.DT_STATUS.ToString("dd/MM/yyyy");
                txtNome.Text = tb107.NO_MATERIA;
                txtNomeReduzido.Text = tb107.NO_RED_MATERIA;
                ddlBoletim.SelectedValue = tb107.CO_CLASS_BOLETIM.ToString();
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB107_CADMATERIAS</returns>
        private TB107_CADMATERIAS RetornaEntidade()
        {
            TB107_CADMATERIAS tb107 = TB107_CADMATERIAS.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb107 == null) ? new TB107_CADMATERIAS() : tb107;
        }
        #endregion
    }
}