//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: PLANEJAMENTO ESCOLAR (CENÁRIO ALUNOS)
// OBJETIVO: REGISTRO DO PERFIL DE OCUPAÇÃO DE ALUNOS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 30/04/2013| André Nobre Vinagre        |Corrigido o tratamento de campo vazio
//           |                            |

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Linq;
using System.Web.UI;
using Resources;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1120_PlanejEscolarAlunos.F1124_RegisPerfilOcupAluno
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
                CarregaUnidades();     
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        private void CurrentCadastroMasterPage_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentCadastroMasterPage_OnAcaoBarraCadastro() 
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            TB246_UNIDADE_PERFIL_VAGAS tb246 = RetornaEntidade();

            tb246.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
            tb246.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);
            tb246.CO_ANO_PERFIL_VAGAS = decimal.Parse(txtAno.Text);
            tb246.QT_VAGAS_PLANEJ_PERFIL = txtVagasPlanej.Text != "" ? (decimal?)decimal.Parse(txtVagasPlanej.Text) : null;
            tb246.QT_VAGAS_RESERV_PERFIL = txtVagasResev.Text != "" ? (decimal?)decimal.Parse(txtVagasResev.Text) : null;
            tb246.QT_VAGAS_MATRIC_PERFIL = txtVagasMatric.Text != "" ? (decimal?)decimal.Parse(txtVagasMatric.Text) : null;
            tb246.QT_VAGAS_RENOVA_PERFIL = txtVagasRenova.Text != "" ? (decimal?)decimal.Parse(txtVagasRenova.Text) : null;
            tb246.QT_VAGAS_ATIVOS_PERFIL = txtVagasAtivos.Text != "" ? (decimal?)decimal.Parse(txtVagasAtivos.Text) : null;
            tb246.QT_VAGAS_TRANSF_PERFIL = txtVagasTransf.Text != "" ? (decimal?)decimal.Parse(txtVagasTransf.Text) : null;
            tb246.QT_VAGAS_CANCEL_PERFIL = txtVagasCancel.Text != "" ? (decimal?)decimal.Parse(txtVagasCancel.Text) : null;
            tb246.QT_VAGAS_EVADID_PERFIL = txtVagasEvadid.Text != "" ? (decimal?)decimal.Parse(txtVagasEvadid.Text) : null;
            tb246.QT_VAGAS_EXPULS_PERFIL = txtVagasExpuls.Text != "" ? (decimal?)decimal.Parse(txtVagasExpuls.Text) : null;

            CurrentCadastroMasterPage.CurrentEntity = tb246;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario() 
        {
            TB246_UNIDADE_PERFIL_VAGAS tb246 = RetornaEntidade();

            if (tb246 != null)
            {
                tb246.TB25_EMPRESAReference.Load();
                ddlUnidade.SelectedValue = tb246.TB25_EMPRESA.CO_EMP.ToString();
                txtAno.Text = tb246.CO_ANO_PERFIL_VAGAS.ToString();
                txtVagasPlanej.Text = tb246.QT_VAGAS_PLANEJ_PERFIL.ToString();
                txtVagasResev.Text = tb246.QT_VAGAS_RESERV_PERFIL.ToString();
                txtVagasMatric.Text = tb246.QT_VAGAS_MATRIC_PERFIL.ToString();
                txtVagasRenova.Text = tb246.QT_VAGAS_RENOVA_PERFIL.ToString();
                txtVagasAtivos.Text = tb246.QT_VAGAS_ATIVOS_PERFIL.ToString();
                txtVagasTransf.Text = tb246.QT_VAGAS_TRANSF_PERFIL.ToString();
                txtVagasCancel.Text = tb246.QT_VAGAS_CANCEL_PERFIL.ToString();
                txtVagasEvadid.Text = tb246.QT_VAGAS_EVADID_PERFIL.ToString();
                txtVagasExpuls.Text = tb246.QT_VAGAS_EXPULS_PERFIL.ToString();  
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB246_UNIDADE_PERFIL_VAGAS</returns>
        private TB246_UNIDADE_PERFIL_VAGAS RetornaEntidade() 
        {
            TB246_UNIDADE_PERFIL_VAGAS tb246 = TB246_UNIDADE_PERFIL_VAGAS.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb246 == null) ? new TB246_UNIDADE_PERFIL_VAGAS() : tb246;
        }

        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades
        /// </summary>
        private void CarregaUnidades() 
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                     where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy(e => e.NO_FANTAS_EMP);

            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataBind();
        }
        #endregion
    }
}

