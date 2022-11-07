//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE DE FREQÜÊNCIA ESCOLAR DO ALUNO
// OBJETIVO: LANÇAMENTO E MANUTENÇÃO INDIVIDUAL DE FREQÜÊNCIA DE ALUNOS
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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3400_CtrlFrequenciaEscolar.F3401_LancManutIndivFreqAluno
{
    public partial class Busca : System.Web.UI.Page
    {
        public PadraoBuscas CurrentPadraoBuscas { get { return ((PadraoBuscas)this.Master); } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            CurrentPadraoBuscas.OnAcaoBuscaDefineGridView += new PadraoBuscas.OnAcaoBuscaDefineGridViewHandler(CurrentPadraoBuscas_OnAcaoBuscaDefineGridView);
            CurrentPadraoBuscas.OnDefineColunasGridView += new PadraoBuscas.OnDefineColunasGridViewHandler(CurrentPadraoBuscas_OnDefineColunasGridView);
            CurrentPadraoBuscas.OnDefineQueryStringIds += new PadraoBuscas.OnDefineQueryStringIdsHandler(CurrentPadraoBuscas_OnDefineQueryStringIds);
        }

        void Page_Load()
        {
            CurrentPadraoBuscas.DefineMensagem(MensagensAjuda.MessagemCampoObrigatorio, MensagensAjuda.MessagemBusca);

            if (IsPostBack) return;

            CarregaAnos();
            CarregaModalidades();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_FREQ_ALUNO" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_ANO_REFER_FREQ_ALUNO",
                HeaderText = "Ano"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_MODU_CUR",
                HeaderText = "Modalidade"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_CUR",
                HeaderText = "Curso"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_TURMA",
                HeaderText = "Turma"
            });

            BoundField bfRealizado2 = new BoundField();
            bfRealizado2.DataField = "DT_FRE";
            bfRealizado2.HeaderText = "Data";
            bfRealizado2.ItemStyle.CssClass = "codCol";
            bfRealizado2.DataFormatString = "{0:d}";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfRealizado2);

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_MATERIA",
                HeaderText = "Matéria"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_FLAG_FREQ_ALUNO",
                HeaderText = "Frequência"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            DateTime dataVerif = DateTime.Now;

            if (!DateTime.TryParse(txtPeriodoDe.Text, out dataVerif))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Data Inicial informada não é válida.");
                return;
            }

            if (!DateTime.TryParse(txtPeriodoAte.Text, out dataVerif))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Data Final informada não é válida.");
                return;
            }

            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;

            DateTime dataInicioFreq = DateTime.Parse(txtPeriodoDe.Text);
            DateTime dataFimFreq = DateTime.Parse(txtPeriodoAte.Text);

            if (LoginAuxili.FLA_PROFESSOR != "S")
            {
                var resultado = (from tb132 in TB132_FREQ_ALU.RetornaTodosRegistros()
                                 where tb132.TB07_ALUNO.CO_ALU == coAlu && (serie != 0 ? tb132.TB01_CURSO.CO_CUR == serie : serie == 0)
                                 && (modalidade != 0 ? tb132.TB01_CURSO.CO_MODU_CUR == modalidade : modalidade == 0)
                                 && (turma != 0 ? tb132.CO_TUR == turma : turma == 0) && tb132.TB01_CURSO.CO_EMP == LoginAuxili.CO_EMP
                                 && tb132.DT_FRE >= dataInicioFreq && tb132.DT_FRE <= dataFimFreq
                                 join tb129 in TB129_CADTURMAS.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on tb132.CO_TUR equals tb129.CO_TUR
                                 join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb132.CO_MAT equals tb02.CO_MAT
                                 join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                 select new
                                 {
                                     tb132.CO_ANO_REFER_FREQ_ALUNO,
                                     tb132.TB01_CURSO.TB44_MODULO.DE_MODU_CUR,
                                     tb132.TB01_CURSO.NO_CUR,
                                     tb129.NO_TURMA,
                                     tb132.DT_FRE,
                                     tb107.NO_MATERIA,
                                     CO_FLAG_FREQ_ALUNO = tb132.CO_FLAG_FREQ_ALUNO == "S" ? "Presente" : "Faltou",
                                     tb132.ID_FREQ_ALUNO
                                 }).OrderBy(f => f.DT_FRE);

                CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
            }
            else
            {
                var resultado = (from tb132 in TB132_FREQ_ALU.RetornaTodosRegistros()
                                 where tb132.TB07_ALUNO.CO_ALU == coAlu && (serie != 0 ? tb132.TB01_CURSO.CO_CUR == serie : serie == 0)
                                 && (modalidade != 0 ? tb132.TB01_CURSO.CO_MODU_CUR == modalidade : modalidade == 0)
                                 && (turma != 0 ? tb132.CO_TUR == turma : turma == 0) && tb132.TB01_CURSO.CO_EMP == LoginAuxili.CO_EMP
                                 && tb132.DT_FRE >= dataInicioFreq && tb132.DT_FRE <= dataFimFreq
                                 join tb129 in TB129_CADTURMAS.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on tb132.CO_TUR equals tb129.CO_TUR
                                 join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb132.CO_MAT equals tb02.CO_MAT
                                 join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                 join rm in TB_RESPON_MATERIA.RetornaTodosRegistros() on tb132.CO_MAT equals rm.CO_MAT
                                 where rm.CO_COL_RESP == LoginAuxili.CO_COL
                                 select new
                                 {
                                     tb132.CO_ANO_REFER_FREQ_ALUNO,
                                     tb132.TB01_CURSO.TB44_MODULO.DE_MODU_CUR,
                                     tb132.TB01_CURSO.NO_CUR,
                                     tb129.NO_TURMA,
                                     tb132.DT_FRE,
                                     tb107.NO_MATERIA,
                                     CO_FLAG_FREQ_ALUNO = tb132.CO_FLAG_FREQ_ALUNO == "S" ? "Presente" : "Faltou",
                                     tb132.ID_FREQ_ALUNO
                                 }).OrderBy(f => f.DT_FRE);

                CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
            }
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_FREQ_ALUNO"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Carrega os anos
        /// </summary>
        private void CarregaAnos()
        {
            AuxiliCarregamentos.CarregaAno(ddlAno, LoginAuxili.CO_EMP, false);
        }

        //====> Método que carrega o DropDown de Modalidades, verifica se o usuário é professor.
        private void CarregaModalidades()
        {
            if (LoginAuxili.FLA_PROFESSOR != "S")
                AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, false, false, true, true);
            else
            {
                int ano = (!string.IsNullOrEmpty(ddlAno.SelectedValue) ? int.Parse(ddlAno.SelectedValue) : 0);
                AuxiliCarregamentos.carregaModalidadesProfeResp(ddlModalidade, LoginAuxili.CO_COL, ano, false);
            }
            CarregaSerieCurso();
        }

        //====> Método que carrega o DropDown de Séries, verifica se o usuário é professor.
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            if (LoginAuxili.FLA_PROFESSOR != "S")
                AuxiliCarregamentos.carregaSeriesCursos(ddlSerieCurso, modalidade, LoginAuxili.CO_EMP, false, false, false, true, true);
            else
            {
                int ano = (!string.IsNullOrEmpty(ddlAno.SelectedValue) ? int.Parse(ddlAno.SelectedValue) : 0);
                AuxiliCarregamentos.carregaSeriesCursosProfeResp(ddlSerieCurso, modalidade, LoginAuxili.CO_COL, ano, false);
            }
            CarregaTurma();
        }

        //====> Método que carrega o DropDown de Turmas, verifica se o usuário é professor.
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            if (LoginAuxili.FLA_PROFESSOR != "S")
                AuxiliCarregamentos.CarregaTurmas(ddlTurma, LoginAuxili.CO_EMP, modalidade, serie, false, false, true, true);
            else
            {
                int ano = (!string.IsNullOrEmpty(ddlAno.SelectedValue) ? int.Parse(ddlAno.SelectedValue) : 0);
                AuxiliCarregamentos.CarregaTurmasProfeResp(ddlTurma, LoginAuxili.CO_EMP, modalidade, serie, LoginAuxili.CO_COL, ano, false);
            }
            CarregaAluno();
        }

        //====> Método que carrega o DropDown de Alunos
        private void CarregaAluno()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaAlunoMatriculado(ddlAluno, LoginAuxili.CO_EMP, modalidade, serie, turma, ddlAno.SelectedValue, false);
        }
        #endregion

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
            CarregaAluno();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
            CarregaAluno();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAluno();
        }

        protected void ddlAno_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaModalidades();
        }
    }
}
