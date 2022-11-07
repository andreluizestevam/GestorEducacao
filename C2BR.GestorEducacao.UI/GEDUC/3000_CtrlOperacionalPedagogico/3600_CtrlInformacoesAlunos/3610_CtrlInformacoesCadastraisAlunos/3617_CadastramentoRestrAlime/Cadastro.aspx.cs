//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CADASTRO E INFORMAÇÕES DE ALUNOS
// OBJETIVO: CADASTRAMENTO DE MÚLTIPLOS ENDEREÇOS DO ALUNO
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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3600_CtrlInformacoesAlunos.F3610_CtrlInformacoesCadastraisAlunos.F3617_CadastramentoRestrAlime
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
                CarregaAlunos();
                CarregaCodigoRestr();

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {
                    txtDtRestricao.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
            }
        }
        
//====> Chamada do método de preenchimento do formulário da funcionalidade
        private void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        private void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;
            DateTime dataRetorno;

            TB294_RESTR_ALIMEN tb294 = RetornaEntidade();

            var refAluno = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
            tb294.TB07_ALUNO = refAluno;
            refAluno.TB25_EMPRESA1Reference.Load();
            tb294.TB25_EMPRESA = refAluno.TB25_EMPRESA1;
            tb294.ID_REFER_GEDUC_RESTR_ALIMEN = ddlCodRestr.SelectedValue != "" ? ddlCodRestr.SelectedValue : null;
            tb294.TP_RESTR_ALIMEN = ddlTpRestri.SelectedValue;
            tb294.NM_RESTR_ALIMEN = txtDescRestri.Text;
            tb294.DE_ACAO_RESTR_ALIMEN = txtAcaoRestri.Text != "" ? txtAcaoRestri.Text : null;
            tb294.DT_INFORM_RESTR_ALIMEN = DateTime.Parse(this.txtDtRestricao.Text); 
            tb294.DT_INICIO_RESTR_ALIMEN = DateTime.Parse(this.txtDataPeriodoIni.Text);
            tb294.DT_TERMI_RESTR_ALIMEN = DateTime.TryParse(this.txtDataPeriodoFim.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
            tb294.CO_GRAU_RESTR_ALIMEN = ddlGrauRestri.SelectedValue;

            CurrentPadraoCadastros.CurrentEntity = tb294;

        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            if (!IsPostBack)
            {
                TB294_RESTR_ALIMEN tb294 = RetornaEntidade();

                if (tb294 != null)
                {
                    tb294.TB07_ALUNOReference.Load();

                    ddlAluno.SelectedValue = tb294.TB07_ALUNO.CO_ALU.ToString();
                    ddlGrauRestri.SelectedValue = tb294.CO_GRAU_RESTR_ALIMEN;
                    ddlTpRestri.SelectedValue = tb294.TP_RESTR_ALIMEN;
                    txtDescRestri.Text = tb294.NM_RESTR_ALIMEN;
                    ddlCodRestr.SelectedValue = tb294.ID_REFER_GEDUC_RESTR_ALIMEN != null ? tb294.ID_REFER_GEDUC_RESTR_ALIMEN : "";
                    txtAcaoRestri.Text = tb294.DE_ACAO_RESTR_ALIMEN != null ? tb294.DE_ACAO_RESTR_ALIMEN : "";
                    txtDataPeriodoIni.Text = tb294.DT_INICIO_RESTR_ALIMEN != null ? tb294.DT_INICIO_RESTR_ALIMEN.ToString("dd/MM/yyyy") : "";
                    txtDataPeriodoFim.Text = tb294.DT_TERMI_RESTR_ALIMEN != null ? tb294.DT_TERMI_RESTR_ALIMEN.Value.ToString("dd/MM/yyyy") : "";
                    txtDtRestricao.Text = tb294.DT_INFORM_RESTR_ALIMEN.ToString("dd/MM/yyyy");
                }
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB294_RESTR_ALIMEN</returns>
        private TB294_RESTR_ALIMEN RetornaEntidade()
        {
            TB294_RESTR_ALIMEN tb294 = TB294_RESTR_ALIMEN.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb294 == null) ? new TB294_RESTR_ALIMEN() : tb294;
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        private void CarregaAlunos()
        {
            ddlAluno.DataSource = (from tb07 in TB07_ALUNO.RetornaPelaUnid(LoginAuxili.CO_EMP)
                                   where tb07.CO_SITU_ALU.ToUpper() == "A"
                                   select new { tb07.CO_ALU, tb07.NO_ALU, }).OrderBy( a => a.NO_ALU );

            ddlAluno.DataValueField = "CO_ALU";
            ddlAluno.DataTextField  = "NO_ALU";
            ddlAluno.DataBind();
            
            ddlAluno.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Restrições Alimentares
        /// </summary>
        private void CarregaCodigoRestr()
        {
            ddlCodRestr.DataSource = (from tb117 in TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaTodosRegistros()
                                      select new { tb117.CO_CID, tb117.NO_CID }).OrderBy(c => c.NO_CID);

            ddlCodRestr.DataTextField = "NO_CID";
            ddlCodRestr.DataValueField = "CO_CID";
            ddlCodRestr.DataBind();

            ddlCodRestr.Items.Insert(0, new ListItem("", ""));
        }
        #endregion   
    }
}