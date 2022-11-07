//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: **********************
// SUBMÓDULO: *******************
// OBJETIVO: ********************
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
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1900_TabelasGenerCtrlAdmEscolar.F1904_EspecialidadeFuncional
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

        void Page_Load()
        {
            if (IsPostBack) return;

            CarregaCursos();
            CarregaGrupoEspecialidade();
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TB63_ESPECIALIDADE tb63 = RetornaEntidade();

            if (tb63.CO_ESPECIALIDADE == 0)
            {
                tb63.CO_EMP = LoginAuxili.CO_EMP;
                tb63.CO_ESPEC = int.Parse(ddlCurso.SelectedValue);
            }

            tb63.NO_ESPECIALIDADE = txtDescricao.Text;
            tb63.NO_SIGLA_ESPECIALIDADE = txtSigla.Text.Trim().ToUpper();

            int idGrupo = !string.IsNullOrEmpty(ddlGrupo.SelectedValue) ? int.Parse(ddlGrupo.SelectedValue) : 0;
            if (idGrupo > 0)
            {
                tb63.TB115_GRUPO_ESPECIALIDADE = TB115_GRUPO_ESPECIALIDADE.RetornaPelaChavePrimaria(idGrupo);
            }

            CurrentPadraoCadastros.CurrentEntity = tb63;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB63_ESPECIALIDADE tb63 = RetornaEntidade();

            if (tb63 != null)
            {
                txtDescricao.Text = tb63.NO_ESPECIALIDADE;
                txtSigla.Text = tb63.NO_SIGLA_ESPECIALIDADE;

                try
                {
                    txtTipo.Text = EnumAuxili.GetEnum<ClassificacaoCurso>(TB100_ESPECIALIZACAO.RetornaPeloCoEspec(tb63.CO_ESPEC).TP_ESPEC).GetValue();
                }
                catch (Exception)
                {
                    txtTipo.Text = "";
                }

                ddlCurso.SelectedValue = tb63.CO_ESPEC.ToString();
                ddlGrupo.SelectedValue = !string.IsNullOrEmpty(tb63.TB115_GRUPO_ESPECIALIDADE.ID_GRUPO_ESPECI.ToString()) ? tb63.TB115_GRUPO_ESPECIALIDADE.ID_GRUPO_ESPECI.ToString() : "0";
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB63_ESPECIALIDADE</returns>
        private TB63_ESPECIALIDADE RetornaEntidade()
        {
            TB63_ESPECIALIDADE tb63 = TB63_ESPECIALIDADE.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, QueryStringAuxili.RetornaQueryStringComoIntPelaChave("espec"), QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb63 == null) ? new TB63_ESPECIALIDADE() : tb63;
        }
        #endregion

        #region Carregamento DropDown

        private void CarregaGrupoEspecialidade()
        {
            AuxiliCarregamentos.CarregaGrupoEspecialidade(ddlGrupo, false);
        }

        /// <summary>
        /// Método que carrega o dropdown de Cursos
        /// </summary>
        private void CarregaCursos()
        {
            ddlCurso.DataSource = (from tb100 in TB100_ESPECIALIZACAO.RetornaTodosRegistros()
                                   where tb100.TP_ESPEC.Equals("GR")
                                   select new { tb100.CO_ESPEC, tb100.DE_ESPEC });

            ddlCurso.DataTextField = "DE_ESPEC";
            ddlCurso.DataValueField = "CO_ESPEC";
            ddlCurso.DataBind();

            ddlCurso.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion

        protected void ddlCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtTipo.Text = EnumAuxili.GetEnum<ClassificacaoCurso>(TB100_ESPECIALIZACAO.RetornaPeloCoEspec(int.Parse(ddlCurso.SelectedValue)).TP_ESPEC).GetValue();
            }
            catch (Exception)
            {
                txtTipo.Text = "";
            }
        }
    }
}