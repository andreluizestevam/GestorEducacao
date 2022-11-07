//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: PLANEJAMENTO ESCOLAR (CENÁRIO ALUNOS)
// OBJETIVO: REGISTRO DO PERFIL DE DESEMPENHO DE ALUNOS
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
using Resources;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1120_PlanejEscolarAlunos.F1125_RegisPerfilDesemAluno
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
                CarregaCombos();

                if (QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id) == 0)
                    ddlUnidade.Enabled = ddlModalidade.Enabled = ddlSerieCurso.Enabled = ddlTurma.Enabled = ddlMateria.Enabled = ddlAno.Enabled = true;
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentCadastroMasterPage_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentCadastroMasterPage_OnAcaoBarraCadastro()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int materia = ddlMateria.SelectedValue != "" ? int.Parse(ddlMateria.SelectedValue) : 0;

            TB247_UNIDADE_PERFIL_DESEMPENHO tb247 = RetornaEntidade();

            if (tb247.ID_UNIDADE_PERFIL_DESEM == 0)
            {
                tb247.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
                tb247.TB06_TURMAS = TB06_TURMAS.RetornaPelaChavePrimaria(coEmp, modalidade, serie, turma);                
            }
            tb247.ID_MATERIA = materia;
            tb247.NR_ANO = ddlAno.SelectedValue;                        
            tb247.NR_MEDIA_BIM1_DESEMP = Decimal.Parse(txtPrimeiro.Text);
            tb247.NR_MEDIA_BIM2_DESEMP = Decimal.Parse(txtSegundo.Text);
            tb247.NR_MEDIA_BIM3_DESEMP = Decimal.Parse(txtTerceiro.Text);
            tb247.NR_MEDIA_BIM4_DESEMP = Decimal.Parse(txtQuarto.Text);

            CurrentCadastroMasterPage.CurrentEntity = tb247;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB247_UNIDADE_PERFIL_DESEMPENHO tb247 = RetornaEntidade();

            if (tb247 != null)
            {
                tb247.TB06_TURMASReference.Load();
                ddlUnidade.SelectedValue = tb247.TB06_TURMAS.CO_EMP.ToString();
                ddlModalidade.SelectedValue = tb247.TB06_TURMAS.CO_MODU_CUR.ToString();
                ddlSerieCurso.SelectedValue = tb247.TB06_TURMAS.CO_CUR.ToString();
                CarregaTurma();
                ddlTurma.SelectedValue = tb247.TB06_TURMAS.CO_TUR.ToString();
                CarregaMaterias();
                ddlMateria.SelectedValue = tb247.ID_MATERIA.ToString();
                ddlAno.SelectedValue = tb247.NR_ANO;
                txtPrimeiro.Text = tb247.NR_MEDIA_BIM1_DESEMP.ToString();
                txtSegundo.Text = tb247.NR_MEDIA_BIM2_DESEMP.ToString();
                txtTerceiro.Text = tb247.NR_MEDIA_BIM3_DESEMP.ToString();
                txtQuarto.Text = tb247.NR_MEDIA_BIM4_DESEMP.ToString();
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB247_UNIDADE_PERFIL_DESEMPENHO</returns>
        private TB247_UNIDADE_PERFIL_DESEMPENHO RetornaEntidade()
        {
            int idUnidPerDes = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);
            if (idUnidPerDes == 0)
                return new TB247_UNIDADE_PERFIL_DESEMPENHO();
            return TB247_UNIDADE_PERFIL_DESEMPENHO.RetornaPelaChavePrimaria(idUnidPerDes);
        }

        /// <summary>
        /// Método que tem as chamadas de outros metódos que carregam o dropdown do Formulário
        /// </summary>
        private void CarregaCombos()
        {
            CarregaUnidade();
            CarregaModalidades();
            CarregaSerieCurso();
            CarregaTurma();
            CarregaMaterias();
            CarregaAnos();
        }
        
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades
        /// </summary>
        private void CarregaUnidade()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                     where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy(e => e.NO_FANTAS_EMP);

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.Items.Insert(0, new ListItem("Selecione", ""));
            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

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
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaTodosRegistros()
                                        where tb01.CO_EMP == coEmp && tb01.CO_MODU_CUR == modalidade
                                        select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct().OrderBy( c => c.NO_CUR );

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
                                   where tb06.CO_MODU_CUR == modalidade && tb06.CO_CUR == serie
                                   select new { tb06.TB129_CADTURMAS.CO_SIGLA_TURMA, tb06.CO_TUR }).OrderBy( t => t.CO_SIGLA_TURMA );

            ddlTurma.DataTextField = "CO_SIGLA_TURMA";
            ddlTurma.DataValueField = "CO_TUR";
            ddlTurma.DataBind();

            ddlTurma.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Matérias
        /// </summary>
        private void CarregaMaterias()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            if (serie != 0 && modalidade != 0)
            {

                ddlMateria.DataSource = (from tb02 in TB02_MATERIA.RetornaTodosRegistros()
                                         join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                         where tb02.CO_CUR == serie && tb02.CO_MODU_CUR == modalidade
                                         select new { tb107.ID_MATERIA, tb107.NO_MATERIA }).Distinct().OrderBy(m => m.NO_MATERIA);

                ddlMateria.DataTextField = "NO_MATERIA";
                ddlMateria.DataValueField = "ID_MATERIA";
                ddlMateria.DataBind();
            }

            ddlMateria.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Ano
        /// </summary>
        private void CarregaAnos()
        {
            ddlAno.Items.Clear();

            for (int a = DateTime.Now.Year - 5; a < DateTime.Now.Year + 5; a++)
                ddlAno.Items.Add(new ListItem(a.ToString(), a.ToString()));
            ddlAno.SelectedValue = DateTime.Now.Year.ToString("0000");
        }
        #endregion

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaModalidades();
            CarregaSerieCurso();
            CarregaTurma();
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();

        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
            CarregaMaterias();
        }
    }
}
