using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3500_CtrlDesempenhoEscolar._3513_CalculoMediaAnual
{
    public partial class Cadastro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarAno();
            }
        }

        #region Carregadores

        /// <summary>
        /// Carrega os anos com base na grades de cursos já criadas
        /// </summary>
        private void CarregarAno()
        {
            ddlAno.Items.Clear();
            ddlAno.Items.AddRange(AuxiliBaseApoio.AnosDDL(LoginAuxili.CO_EMP, true));

            ddlModalidade.Items.Clear();
            ddlSerieCurso.Items.Clear();
            ddlTurma.Items.Clear();
        }

        /// <summary>
        /// Carrega todos os módulos com base no orgão atual
        /// </summary>
        private void CarregarModulo()
        {
            ddlModalidade.Items.Clear();
            ddlModalidade.Items.AddRange(AuxiliBaseApoio.ModulosDDL(LoginAuxili.ORG_CODIGO_ORGAO, true));

            ddlSerieCurso.Items.Clear();
            ddlTurma.Items.Clear();
        }

        /// <summary>
        /// Carrega todas as série com base no ano e módulo escolhido
        /// </summary>
        private void CarregarSerie()
        {
            ddlSerieCurso.Items.Clear();
            ddlSerieCurso.Items.AddRange(AuxiliBaseApoio.SeriesDDL(LoginAuxili.CO_EMP, ddlModalidade.SelectedValue, ddlAno.SelectedValue, true));
            ddlTurma.Items.Clear();
        }

        /// <summary>
        /// Carrega todas as turmas com base no modulo e série escolhidos
        /// </summary>
        private void CarregarTurma()
        {
            ddlTurma.Items.Clear();
            ddlTurma.Items.AddRange(AuxiliBaseApoio.TurmasDDL(LoginAuxili.CO_EMP, ddlModalidade.SelectedValue, ddlSerieCurso.SelectedValue, ddlAno.SelectedValue, true));
        }

        #endregion

        #region Eventos dos componentes
        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "")
                CarregarModulo();
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "")
                CarregarSerie();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "")
                CarregarTurma();
        }

        /// <summary>
        /// Executa o cálculo das médias da turma escohida e já salva automáticamente, e exibi na gride o resultado
        /// </summary>
        /// <param name="sender">Objeto alvo</param>
        /// <param name="e">Evento</param>
        protected void btnCalcular_Click(object sender, EventArgs e)
        {
            string ano = ddlAno.SelectedValue;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            grdBusca.DataSource = AuxiliCalculos.calculaMedia(LoginAuxili.CO_EMP, ano, modalidade, serie, turma);
            grdBusca.DataBind();
            string nomeTurma = turma == 0 ? "Turma não localizada" : TB129_CADTURMAS.RetornaPelaChavePrimaria(turma).NO_TURMA;
            if(grdBusca.Rows.Count > 0)
                AuxiliPagina.EnvioAvisoGeralSistema(this, string.Format("Turma '{0}' calculada com sucesso!", nomeTurma));
            else
                AuxiliPagina.EnvioAvisoGeralSistema(this, string.Format("Turma '{0}' sem materias e/ou alunos vinculados!", nomeTurma));
        }

        #endregion

    }
}