//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE DE AVALIAÇÃO ESCOLAR DO ALUNO
// OBJETIVO: ******
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 10/12/2014| Maxwell Almeida            | Criação da funcionalidade para cálculo de média final

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução

namespace C2BR.GestorEducacao.UI.GEDUC._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2107_MatriculaAluno.ReativarMatriculaFinalizada
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            CarregaAnos();
            CarregaModalidades();
            CarregaSerieCurso();
            CarregaTurma();
            CarregaAluno();
            CarregaUnidade();
            divGrid.Visible = false;
        }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (grdBusca.Rows.Count == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Nenhum aluno(a) na grid!");
            }

            bool marcado = false;
            foreach (GridViewRow linha in grdBusca.Rows)
            {
                //Verifica se algum item foi selecionado
                if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked == true)
                {
                    marcado = true;
                    break; // quebra quando achar um selecionado para não sobrescrever o bool
                }
            }

            if (!marcado)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Nenhum(a) aluno(a) selecionado(a) para reativação!");
                return;
            }

            //--------> Varre toda a grid de Busca
            foreach (GridViewRow linha in grdBusca.Rows)
            {
                if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                {
                    //Recebe matrícula
                    string codMatricula = (((HiddenField)linha.Cells[0].FindControl("hidCoAluCad")).Value);
                    int coEMp = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidCoEmp")).Value);

                    //Instancia objeto da matrícula em questão
                    TB08_MATRCUR tb08 = (from lTb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                         where lTb08.CO_ALU_CAD == codMatricula && lTb08.TB25_EMPRESA.CO_EMP == coEMp
                            select lTb08).FirstOrDefault();

                    if (tb08 != null)
                    {
                        tb08.CO_SIT_MAT = "A"; // impõe status ativo na matrícula
                        TB08_MATRCUR.SaveOrUpdate(tb08, true); // salva
                    }
                    else
                    {
                        AuxiliPagina.RedirecionaParaPaginaErro("Não foi possível salvar as alterações do aluno.", Request.Url.AbsoluteUri);
                        return;
                    }
                }
            }

            AuxiliPagina.RedirecionaParaPaginaSucesso("Registros Salvos com sucesso.", Request.Url.AbsoluteUri);
        }

        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega a grid
        /// </summary>
        private void CarregaGrid()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;
            string anoMesMat = ddlAno.SelectedValue;
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int intAno = int.Parse(ddlAno.SelectedValue.Trim());

            divGrid.Visible = true;

            var resultado = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                             join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb08.CO_EMP equals tb25.CO_EMP
                             join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb08.CO_CUR equals tb01.CO_CUR
                             join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tb08.CO_TUR equals tb129.CO_TUR
                             where
                              (coEmp != 0 ? tb08.TB25_EMPRESA.CO_EMP == coEmp : 0 == 0)
                             && tb08.CO_ANO_MES_MAT == anoMesMat
                             && (serie != 0 ? tb08.CO_CUR == serie : 0 == 0)
                             && (turma != 0 ? tb08.CO_TUR == turma : 0 == 0)
                             && (modalidade != 0 ? tb08.TB44_MODULO.CO_MODU_CUR == modalidade : 0 == 0)
                             && tb08.CO_SIT_MAT == "F"
                             && (coAlu != 0 ? tb08.CO_ALU == coAlu : 0 == 0)
                             select new NotasAluno
                             {
                                 CO_ALU = tb08.TB07_ALUNO.CO_ALU,
                                 CO_ALU_CAD = tb08.CO_ALU_CAD,

                                 NO_ALU = tb08.TB07_ALUNO.NO_ALU.ToUpper(),
                                 NU_NIRE = tb08.TB07_ALUNO.NU_NIRE,
                                 UNID = tb25.sigla,
                                 MODALIDADE = tb08.TB44_MODULO.DE_MODU_CUR,
                                 SERIE = tb01.NO_CUR,
                                 TURMA = tb129.CO_SIGLA_TURMA,
                                 CO_EMP = tb25.CO_EMP,
                             }).OrderBy(w => w.UNID).ThenBy(w => w.MODALIDADE).ThenBy(w => w.SERIE).ThenBy(w => w.TURMA).ThenBy(w => w.NO_ALU).ToList();

            divGrid.Visible = true;

            grdBusca.DataKeyNames = new string[] { "CO_ALU" };

            grdBusca.DataSource = resultado.Count() > 0 ? resultado.OrderBy(p => p.NO_ALU) : null;
            grdBusca.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            AuxiliCarregamentos.CarregaAno(ddlAno, LoginAuxili.CO_EMP, true);
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            string anoGrade = ddlAno.SelectedValue;
            AuxiliCarregamentos.carregaSeriesCursos(ddlSerieCurso, modalidade, coEmp, true);
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaTurmas(ddlTurma, coEmp, modalidade, serie, true);
        }

        /// <summary>
        /// /Carrega as unidades
        /// </summary>
        private void CarregaUnidade()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        /// <summary>
        /// Carrega os alunos
        /// </summary>
        private void CarregaAluno()
        {
            ddlAluno.Items.Clear();

            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaAlunoMatriculado(ddlAluno, coEmp, modalidade, serie, turma, ddlAno.SelectedValue, true);
        }

        #endregion

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
            CarregaAluno();
            grdBusca.DataSource = null;
            grdBusca.DataBind();
            divGrid.Visible = false;
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
            CarregaAluno();
            grdBusca.DataSource = null;
            grdBusca.DataBind();
            divGrid.Visible = false;
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAluno();
            grdBusca.DataSource = null;
            grdBusca.DataBind();
            divGrid.Visible = false;
        }

        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
            CarregaAluno();
            grdBusca.DataSource = null;
            grdBusca.DataBind();
            divGrid.Visible = false;
        }

        protected void ddlUnidade_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaAluno();
        }

        protected void btnPesqGride_Click(object sender, EventArgs e)
        {
            if (ddlModalidade.SelectedValue == "" || ddlSerieCurso.SelectedValue == "" || ddlTurma.SelectedValue == ""
                || ddlAno.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Ano, Modalidade, Série e Turma devem ser informados.");
                return;
            }

            CarregaGrid();
        }

        protected void chkMarcaTodos_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkMarca = ((CheckBox)grdBusca.HeaderRow.Cells[0].FindControl("chkMarcaTodos"));

            //Percorre alterando o checkbox de acordo com o selecionado em marcar todos
            foreach (GridViewRow li in grdBusca.Rows)
            {
                CheckBox ck = (((CheckBox)li.Cells[0].FindControl("ckSelect")));
                ck.Checked = chkMarca.Checked;
            }
        }

        #region Lista de Notas dos Alunos
        public class NotasAluno
        {
            public string CO_ALU_CAD { get; set; }
            public int CO_EMP { get; set; }
            public string NO_ALU { get; set; }
            public string UNID { get; set; }
            public string MODALIDADE { get; set; }
            public string SERIE { get; set; }
            public string TURMA { get; set; }
            public int NU_NIRE { get; set; }
            public int CO_ALU { get; set; }
            public string DescNU_NIRE { get { return this.NU_NIRE.ToString().PadLeft(7, '0'); } }
        }
        #endregion
    }
}