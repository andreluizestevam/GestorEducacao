//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: PLANEJAMENTO ESCOLAR (CENÁRIO ALUNOS)
// OBJETIVO: PLANEJAMENTO DISPONIBILIDADE DE VAGAS POR MODALIDADE/SÉRIE/TURNO NO ANO LETIVO *** REGISTRO ANUAL DE DISPONIBILIDADE DE VAGAS MODALIDADE/SÉRIE/TURMA DE UNIDADE DE ENSINO
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
using Resources;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.UI.Library.Auxiliares;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1120_PlanejEscolarAlunos.F1122_PlanejDisVagaModSerTurnAnoLet
{
  public partial class Cadastro : System.Web.UI.Page 
  {
        public PadraoCadastros CurrentCadastroMasterPage { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e) 
        {
            base.OnPreInit(e);

//--------> Criação das instâncias utilizadas
            CurrentCadastroMasterPage.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentCadastroMasterPage_OnAcaoBarraCadastro);
            CurrentCadastroMasterPage.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentCadastroMasterPage_OnCarregaFormulario);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaModalidades();
                txtAno.Text = DateTime.Now.Year.ToString();
                ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
                ddlTurma.Items.Insert(0, new ListItem("Selecione", ""));

            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentCadastroMasterPage_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentCadastroMasterPage_OnAcaoBarraCadastro() 
        {                
            TB289_DISP_VAGA_TURMA tb289 = RetornaEntidade();
            int intRetorno = 0;

            tb289.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
            tb289.CO_ANO = txtAno.Text;
            tb289.TB44_MODULO = TB44_MODULO.RetornaPelaChavePrimaria(int.Parse(ddlModalidade.SelectedValue));
            tb289.CO_CUR = int.Parse(ddlSerieCurso.SelectedValue);            
            tb289.CO_TUR = int.Parse(ddlTurma.SelectedValue);
            tb289.NU_SEM_LET = 1;
            tb289.CO_PERI_TUR = rblTurno.SelectedValue;
            tb289.QTDE_VAG_DISP = int.TryParse(txtQtdeDisponivel.Text, out intRetorno) ? intRetorno : 0;
            tb289.QTDE_VAG_MAT = int.TryParse(txtQtdeMatriculada.Text, out intRetorno) ? (int?)intRetorno : null;

            CurrentCadastroMasterPage.CurrentEntity = tb289;
        }
        #endregion
        
        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB289_DISP_VAGA_TURMA tb289 = RetornaEntidade();

            if (tb289 != null)
            {
                txtAno.Text = tb289.CO_ANO;
                ddlModalidade.SelectedValue = tb289.TB44_MODULO.CO_MODU_CUR.ToString();
                CarregaSerieCurso();
                ddlSerieCurso.SelectedValue = tb289.CO_CUR.ToString();
                CarregaTurma();
                ddlTurma.SelectedValue = tb289.CO_TUR.ToString();
                rblTurno.SelectedValue = tb289.CO_PERI_TUR;
                txtQtdeDisponivel.Text = tb289.QTDE_VAG_DISP.ToString();
                txtQtdeMatriculada.Text = tb289.QTDE_VAG_MAT.ToString();
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns></returns>
        private TB289_DISP_VAGA_TURMA RetornaEntidade() 
        {
            TB289_DISP_VAGA_TURMA tb289 = TB289_DISP_VAGA_TURMA.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb289 == null) ? new TB289_DISP_VAGA_TURMA() : tb289;
        }
        #endregion

        #region Carregamento DropDown
        
        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades() 
        {
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where( m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);
            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();

            ddlModalidade.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso() 
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                        where tb01.CO_MODU_CUR == modalidade
                                        select new { tb01.CO_CUR, tb01.NO_CUR }).OrderBy(c => c.NO_CUR);

            ddlSerieCurso.DataTextField = "NO_CUR";
            ddlSerieCurso.DataValueField = "CO_CUR";
            ddlSerieCurso.DataBind();

            ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        private void CarregaTurma() 
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                   where (modalidade != 0 ? tb06.CO_MODU_CUR == modalidade : modalidade == 0)
                                   && (serie != 0 ? tb06.CO_CUR == serie : serie == 0)
                                   select new { tb06.TB129_CADTURMAS.NO_TURMA, tb06.CO_TUR }).OrderBy(t => t.NO_TURMA);

            ddlTurma.DataTextField = "NO_TURMA";
            ddlTurma.DataValueField = "CO_TUR";
            ddlTurma.DataBind();

            ddlTurma.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e) 
        {
            CarregaSerieCurso();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e) 
        {
            CarregaTurma();
        }
  }
}
