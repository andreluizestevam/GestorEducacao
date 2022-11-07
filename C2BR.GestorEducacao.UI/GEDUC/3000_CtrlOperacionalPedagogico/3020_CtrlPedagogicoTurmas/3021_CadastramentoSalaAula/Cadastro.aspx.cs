//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: CONTROLE DE TURMAS POR SÉRIES/CURSOS
// OBJETIVO: CADASTRAMENTO DE SALAS DE AULA
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3020_CtrlPedagogicoTurmas.F3021_CadastramentoSalaAula
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
                CarregaUnidade();
                if (QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id) == 0)
                    ddlUnidade.Enabled = true;
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentCadastroMasterPage_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentCadastroMasterPage_OnAcaoBarraCadastro()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            decimal decimalLargura = 0;
            decimal decimalAltura = 0;
            decimal decimalComprimento = 0;

            TB248_UNIDADE_SALAS_AULA tb248 = RetornaEntidade();

            if (tb248.ID_SALA_AULA == 0)
            {
                tb248.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);
                tb248.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
            }

            tb248.CO_TIPO_SALA_AULA = ddlTipoSala.SelectedValue;
            tb248.CO_PISO_SALA_AULA = ddlPisoSala.SelectedValue != "" ? ddlPisoSala.SelectedValue : null;
            tb248.DE_SALA_AULA = txtDescSala.Text;
            tb248.CO_IDENTI_SALA_AULA = txtCodigoIdentificador.Text;
            tb248.VL_LARGUR_SALA_AULA = decimal.TryParse(txtLargura.Text, out decimalLargura) ? (decimal?)decimalLargura : null;
            tb248.VL_ALTURA_SALA_AULA = decimal.TryParse(txtAltura.Text, out decimalAltura) ? (decimal?)decimalAltura : null;            
            tb248.VL_COMPRI_SALA_AULA = decimal.TryParse(txtComprimento.Text, out decimalComprimento) ? (decimal?)decimalComprimento : null;
            tb248.QT_ALUNOS_MAXIM_SALA_AULA = int.Parse(txtMaximoAlunos.Text);
            tb248.QT_ALUNOS_MATRIC_SALA_AULA = int.Parse(txtAlunosMatriculados.Text);
            tb248.QT_CADEIR_MAXIM_SALA_AULA = int.Parse(txtMaximoCadeira.Text);
            tb248.QT_CADEIR_DISPON_SALA_AULA = int.Parse(txtCadeirasDisponiveis.Text);
            tb248.QT_VENTIL_SALA_AULA = int.Parse(txtQuantidadeDeVentiladores.Text);
            tb248.QT_ARCOND_SALA_AULA = int.Parse(txtQuantidadeDeAr.Text);

            CurrentCadastroMasterPage.CurrentEntity = tb248;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB248_UNIDADE_SALAS_AULA tb248 = RetornaEntidade();

            if (tb248 != null)
            {
                tb248.TB25_EMPRESAReference.Load();
                tb248.TB000_INSTITUICAOReference.Load();

                ddlUnidade.SelectedValue = tb248.TB25_EMPRESA.CO_EMP.ToString();
                txtDescSala.Text = tb248.DE_SALA_AULA;
                ddlTipoSala.SelectedValue = tb248.CO_TIPO_SALA_AULA.ToString();
                ddlPisoSala.SelectedValue = tb248.CO_PISO_SALA_AULA != null ? tb248.CO_PISO_SALA_AULA : "";
                txtAltura.Text = tb248.VL_ALTURA_SALA_AULA.ToString();
                txtAlunosMatriculados.Text = tb248.QT_ALUNOS_MATRIC_SALA_AULA.ToString();
                txtCadeirasDisponiveis.Text = tb248.QT_CADEIR_DISPON_SALA_AULA.ToString();
                txtCodigoIdentificador.Text = tb248.CO_IDENTI_SALA_AULA.ToString();
                txtComprimento.Text = tb248.VL_COMPRI_SALA_AULA.ToString();
                txtLargura.Text = tb248.VL_LARGUR_SALA_AULA.ToString();
                txtMaximoAlunos.Text = tb248.QT_ALUNOS_MAXIM_SALA_AULA.ToString();
                txtMaximoCadeira.Text = tb248.QT_CADEIR_MAXIM_SALA_AULA.ToString();
                txtQuantidadeDeAr.Text = tb248.QT_ARCOND_SALA_AULA.ToString();
                txtQuantidadeDeVentiladores.Text = tb248.QT_VENTIL_SALA_AULA.ToString();
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB248_UNIDADE_SALAS_AULA</returns>
        private TB248_UNIDADE_SALAS_AULA RetornaEntidade()
        {
            int idNecessidade = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);

            if (idNecessidade == 0)
                return new TB248_UNIDADE_SALAS_AULA();

            return TB248_UNIDADE_SALAS_AULA.RetornaPelaChavePrimaria(idNecessidade);
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaUnidade()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                     where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy( e => e.NO_FANTAS_EMP );

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.Items.Insert(0, new ListItem("Selecione", ""));
            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }
        #endregion
    }
}
