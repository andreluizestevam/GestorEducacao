using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3900_CtrlEncerramentoLetivo.F3901_ProcessaFreqFinalAlunoTurma;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.UI.GEDUC._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2107_MatriculaAluno.AtivacaoPreMatriculaAluno
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }
        Dictionary<string, string> statusMatricula = AuxiliBaseApoio.chave(statusMatriculaAluno.ResourceManager);
        Dictionary<string, string> statusAprovacao = AuxiliBaseApoio.chave(statusAprovacaoAluno.ResourceManager);

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            ///Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            CarregaAnos();
            CarregaModalidades();
            CarregaSerieCurso();
            CarregaTurma();
            divGrid.Visible = false;
        }

        /// <summary>
        /// Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        /// </summary>
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TB08_MATRCUR tb08;
            int intQtdeAlunos = 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;

            //--------> Varre toda a grid de Alunos
            foreach (GridViewRow row in grdAlunos.Rows)
            {
                int coAlu = Convert.ToInt32(grdAlunos.DataKeys[row.RowIndex].Values[0]);

                tb08 = (from lTb08 in TB08_MATRCUR.RetornaTodosRegistros()
                        where lTb08.CO_ALU == coAlu
                        && lTb08.CO_ANO_MES_MAT == ddlAno.SelectedValue
                        && lTb08.CO_CUR == serie
                        && lTb08.CO_TUR == turma
                        && lTb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                        select lTb08).FirstOrDefault();

                if (tb08.CO_SIT_MAT == statusMatricula[statusMatriculaAluno.A] && (tb08.CO_STA_APROV == statusMatricula[statusMatriculaAluno.A] || tb08.CO_STA_APROV == statusMatricula[statusMatriculaAluno.R]))
                {
                    //----------------> Faz a verificação para saber se o status já está como Finalizado
                    if (tb08.CO_SIT_MAT != statusMatricula[statusMatriculaAluno.F])
                    {
                        tb08.CO_SIT_MAT = statusMatricula[statusMatriculaAluno.F];
                        intQtdeAlunos += 1;
                        TB08_MATRCUR.SaveOrUpdate(tb08, true);
                    }
                }
            }

            if (intQtdeAlunos > 0)
                AuxiliPagina.RedirecionaParaPaginaSucesso(intQtdeAlunos.ToString() + " Aluno(s) foi(ram) Finzalizado(s) com Sucesso", Request.Url.AbsoluteUri);
            else
                AuxiliPagina.RedirecionaParaPaginaSucesso("Não Existem Alunos para o Processo de Finalização de Matrícula", Request.Url.AbsoluteUri);
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            ddlAno.Items.Clear();
            ddlAno.Items.AddRange(AuxiliBaseApoio.AnosDDL(LoginAuxili.CO_EMP));
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            ddlModalidade.Items.Clear();
            ddlModalidade.Items.AddRange(AuxiliBaseApoio.ModulosDDL(LoginAuxili.ORG_CODIGO_ORGAO, true));
        }

        /// <summary>
        /// Método que carrega o dropdown de Série
        /// </summary>
        private void CarregaSerieCurso()
        {
            ddlSerieCurso.Items.Clear();
            ddlSerieCurso.Items.AddRange(AuxiliBaseApoio.SeriesDDL(LoginAuxili.CO_EMP, ddlModalidade.SelectedValue, ddlAno.SelectedValue, true));
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        private void CarregaTurma()
        {
            ddlTurma.Items.Clear();
            ddlTurma.Items.AddRange(AuxiliBaseApoio.TurmasDDL(LoginAuxili.CO_EMP, ddlModalidade.SelectedValue, ddlSerieCurso.SelectedValue, ddlAno.SelectedValue, true));
        }
        #endregion

        #region Eventos de componentes

        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "")
            {
                CarregaSerieCurso();
                CarregaTurma();
            }
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "")
            {
                CarregaSerieCurso();
                CarregaTurma();
            }
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "")
                CarregaTurma();
        }

        protected void imgAdd_Click(object sender, EventArgs e)
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;

            if (modalidade == 0 || serie == 0 || turma == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Modalidade, Série/Curso e turma devem ser selecionados.");
                return;
            }
            string letraAprovado = statusMatricula[statusMatriculaAluno.A];
            string letraReprovado = statusMatricula[statusMatriculaAluno.R];
            string letraFinalizado = statusMatricula[statusMatriculaAluno.F];
            string textoAprovado = statusMatriculaAluno.A;
            string textoFinalizado = statusMatriculaAluno.F;
            string textReprovado = statusMatriculaAluno.R;
            string textoAprovadoSt = statusAprovacaoAluno.A;
            string textoReprovadoSt = statusAprovacaoAluno.R;
            var lstAlunos = (from lTb08 in TB08_MATRCUR.RetornaTodosRegistros()
                             where lTb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP && lTb08.CO_ANO_MES_MAT == ddlAno.SelectedValue
                             && lTb08.CO_CUR == serie && lTb08.CO_TUR == turma && lTb08.TB44_MODULO.CO_MODU_CUR == modalidade
                             && (lTb08.CO_SIT_MAT == letraAprovado || lTb08.CO_SIT_MAT == letraFinalizado)
                             select new
                             {
                                 CO_ALU_CAD = lTb08.CO_ALU_CAD.Insert(2, ".").Insert(6, "."),
                                 lTb08.TB07_ALUNO.NU_NIRE,
                                 lTb08.TB07_ALUNO.CO_ALU,
                                 lTb08.TB07_ALUNO.NO_ALU,
                                 lTb08.CO_SIT_MAT,
                                 lTb08.CO_STA_APROV,
                                 OBS_MAT = ((lTb08.CO_STA_APROV != null && lTb08.CO_SIT_MAT != letraFinalizado) ? textoAprovado : ((lTb08.CO_SIT_MAT == letraAprovado && lTb08.CO_STA_APROV == null) ? textoAprovado : (lTb08.CO_SIT_MAT == letraFinalizado ? textoFinalizado : null))),
                                 STATUS = ((lTb08.CO_STA_APROV == letraAprovado && lTb08.CO_STA_APROV_FREQ != letraReprovado) ? textoAprovadoSt : ((lTb08.CO_STA_APROV == letraReprovado || lTb08.CO_STA_APROV_FREQ == letraReprovado) ? textoReprovadoSt : null))
                             }).ToList().OrderBy(m => m.NO_ALU);

            divGrid.Visible = true;

            if (lstAlunos.Count() > 0)
            {
                //Habilita o botão de salvar
                grdAlunos.DataBind();
            }

            grdAlunos.DataKeyNames = new string[] { "CO_ALU" };
            grdAlunos.DataSource = lstAlunos;
            grdAlunos.DataBind();
        }

        protected void grdAlunos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hfSituacaoMatricula = (HiddenField)e.Row.FindControl("hdSitMat");
                HiddenField hfObservacao = (HiddenField)e.Row.FindControl("hdOB");

                if (hfSituacaoMatricula.Value != null && hfSituacaoMatricula.Value != "" && hfObservacao.Value == "Processado")
                    e.Row.ControlStyle.BackColor = System.Drawing.Color.FromArgb(251, 233, 183);
            }
        }

        #endregion
    }
}