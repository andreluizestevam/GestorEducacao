//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE DE AVALIAÇÃO ESCOLAR DO ALUNO
// OBJETIVO: LANÇAMENTO E MANUTENÇÃO INDIVIDUAL DE NOTAS DE ATIVIDADES ESCOLARES DE ALUNO
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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3500_CtrlDesempenhoEscolar.F3501_LancManutIndNotaAtivEscAluno
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaUnidades();
                CarregaAnos();
                CarregaModalidades();
                CarregaSerieCurso();
                CarregaTurma();
                CarregaAluno();
                CarregaMaterias();
                CarregaMedidas();
            }
        }

        //====> Carrega o tipo de medida da Referência (Mensal/Bimestre/Trimestre/Semestre/Anual)
        private void CarregaMedidas()
        {
            var tipo = "B";

            var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

            tb25.TB83_PARAMETROReference.Load();
            if (tb25.TB83_PARAMETRO != null)
                tipo = tb25.TB83_PARAMETRO.TP_PERIOD_AVAL;

            AuxiliCarregamentos.CarregaMedidasTemporais(ddlReferencia, false, tipo, false, true);
        }

        private void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_NOTA_ATIV" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DT_VALID",
                HeaderText = "DATA"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_MATERIA",
                HeaderText = "Matéria"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_BIMESTRE",
                HeaderText = "Bimestre"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_ALU",
                HeaderText = "Aluno"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_NOTA_ATIV",
                HeaderText = "NOME ATIV"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_CLASS_V",
                HeaderText = "CLASSIF"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "VL_NOTA",
                HeaderText = "Nota"
            });
        }

        private void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            string strCoSemestre = ddlSemestre.SelectedValue;
            int coAno = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string coReferencia = ddlReferencia.SelectedValue;
            int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;
            int materia = ddlMateria.SelectedValue != "" ? int.Parse(ddlMateria.SelectedValue) : 0;

            if (LoginAuxili.FLA_PROFESSOR != "S")
            {
                var resultado = (from tb49 in TB49_NOTA_ATIV_ALUNO.RetornaTodosRegistros()
                                 where (coEmp != 0 ? tb49.TB06_TURMAS.CO_EMP == coEmp : coEmp == 0)
                                 && (strCoSemestre != "" ? tb49.CO_SEMESTRE == strCoSemestre : strCoSemestre == "")
                                 && (coAno != 0 ? tb49.CO_ANO == coAno : coAno == 0)
                                 && (modalidade != 0 ? tb49.TB06_TURMAS.CO_MODU_CUR == modalidade : modalidade == 0)
                                 && (serie != 0 ? tb49.TB06_TURMAS.CO_CUR == serie : serie == 0)
                                 && (turma != 0 ? tb49.TB06_TURMAS.CO_TUR == turma : turma == 0)
                                 && (tb49.CO_BIMESTRE == coReferencia)
                                 && (coAlu != 0 ? tb49.TB07_ALUNO.CO_ALU == coAlu : coAlu == 0)
                                 && (materia != 0 ? tb49.TB107_CADMATERIAS.ID_MATERIA == materia : materia == 0)
                                 select new saida
                                 {
                                     ID_NOTA_ATIV = tb49.ID_NOTA_ATIV,
                                     NO_MATERIA = tb49.TB107_CADMATERIAS.NO_MATERIA,
                                     ID_MATERIA = tb49.TB107_CADMATERIAS.ID_MATERIA,
                                     NO_ALU= tb49.TB07_ALUNO.NO_ALU,
                                     CO_ALU= tb49.TB07_ALUNO.CO_ALU,
                                     NO_NOTA_ATIV= tb49.NO_NOTA_ATIV,
                                     VL_NOTA= tb49.VL_NOTA,
                                     CO_TUR = tb49.TB06_TURMAS.CO_TUR,
                                     CO_EMP = tb49.TB06_TURMAS.CO_EMP,
                                     CO_SEMESTRE = tb49.CO_SEMESTRE,
                                     CO_ANO = tb49.CO_ANO,
                                     CO_MODU_CUR = tb49.TB06_TURMAS.CO_MODU_CUR,
                                     CO_CUR = tb49.TB01_CURSO.CO_CUR,
                                     DT = tb49.DT_NOTA_ATIV,
                                     CO_CLASS = tb49.CO_REFER_NOTA,
                                     CO_BIMESTRE = tb49.CO_BIMESTRE.Equals("B1") ? "1º Bimestre" : tb49.CO_BIMESTRE.Equals("B2") ? "2º Bimestre" : tb49.CO_BIMESTRE.Equals("B3") ? "3º Bimestre" : "4º Bimestre"
                                 }).OrderBy(n => n.NO_ALU).ThenBy(n => n.CO_BIMESTRE);

                CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
            }
            else
            {
                var resultado = (from tb49 in TB49_NOTA_ATIV_ALUNO.RetornaTodosRegistros()
                                 join rm in TB_RESPON_MATERIA.RetornaTodosRegistros() on tb49.TB06_TURMAS.CO_TUR equals rm.CO_TUR
                                 where rm.CO_COL_RESP == LoginAuxili.CO_COL
                                 where (coEmp != 0 ? tb49.TB06_TURMAS.CO_EMP == coEmp : coEmp == 0)
                                 && (strCoSemestre != "" ? tb49.CO_SEMESTRE == strCoSemestre : strCoSemestre == "")
                                 && (coAno != 0 ? tb49.CO_ANO == coAno : coAno == 0)
                                 && (modalidade != 0 ? tb49.TB06_TURMAS.CO_MODU_CUR == modalidade : modalidade == 0)
                                 && (serie != 0 ? tb49.TB06_TURMAS.CO_CUR == serie : serie == 0)
                                 && (turma != 0 ? tb49.TB06_TURMAS.CO_TUR == turma : turma == 0)
                                 && (tb49.CO_BIMESTRE == coReferencia)
                                 && (coAlu != 0 ? tb49.TB07_ALUNO.CO_ALU == coAlu : coAlu == 0)
                                 && (materia != 0 ? tb49.TB107_CADMATERIAS.ID_MATERIA == materia : materia == 0)
                                 select new saida
                                 {
                                     ID_NOTA_ATIV = tb49.ID_NOTA_ATIV,
                                     NO_MATERIA = tb49.TB107_CADMATERIAS.NO_MATERIA,
                                     ID_MATERIA = tb49.TB107_CADMATERIAS.ID_MATERIA,
                                     NO_ALU = tb49.TB07_ALUNO.NO_ALU,
                                     CO_ALU = tb49.TB07_ALUNO.CO_ALU,
                                     NO_NOTA_ATIV = tb49.NO_NOTA_ATIV,
                                     VL_NOTA = tb49.VL_NOTA,
                                     CO_TUR = tb49.TB06_TURMAS.CO_TUR,
                                     CO_EMP = tb49.TB06_TURMAS.CO_EMP,
                                     CO_SEMESTRE = tb49.CO_SEMESTRE,
                                     CO_ANO = tb49.CO_ANO,
                                     CO_MODU_CUR = tb49.TB06_TURMAS.CO_MODU_CUR,
                                     CO_CUR = tb49.TB01_CURSO.CO_CUR,
                                     DT = tb49.DT_NOTA_ATIV,
                                     CO_CLASS = tb49.CO_REFER_NOTA,
                                     CO_BIMESTRE = tb49.CO_BIMESTRE.Equals("B1") ? "1º Bimestre" : tb49.CO_BIMESTRE.Equals("B2") ? "2º Bimestre" : tb49.CO_BIMESTRE.Equals("B3") ? "3º Bimestre" : "4º Bimestre"
                                 }).Distinct().OrderBy(n => n.NO_ALU).ThenBy(n => n.CO_BIMESTRE);

                CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
            }
        }

        private void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_NOTA_ATIV"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        public class saida
        {
            public int ID_NOTA_ATIV { get; set; }
            public string NO_MATERIA { get; set; }
            public int ID_MATERIA { get; set; }
            public string NO_ALU { get; set; }
            public int CO_ALU { get; set; }
            public string NO_NOTA_ATIV { get; set; }
            public decimal VL_NOTA { get; set; }
            public int CO_TUR { get; set; }
            public int CO_EMP { get; set; }
            public string CO_SEMESTRE { get; set; }
            public int CO_ANO { get; set; }
            public int CO_MODU_CUR { get; set; }
            public int CO_CUR { get; set; }
            public string CO_BIMESTRE { get; set; }
            public DateTime DT { get; set; }
            public string DT_VALID
            {
                get
                {
                    return this.DT.ToString("dd/MM/yyyy");
                }
            }
            public string CO_CLASS { get; set; }
            public string CO_CLASS_V
            {
                get
                {
                    switch (this.CO_CLASS)
                    {
                        case "N1":
                            return "Nota 1";
                        case "N2":
                            return "Nota 2";
                        case "N3":
                            return "Simulado";
                        case "S1":
                            return "Extra";
                        case "AV1":
                            return "AV1";
                        case "AV2":
                            return "AV2";
                        case "AV3":
                            return "AV3";
                        case "AV4":
                            return "AV4";
                        case "AV5":
                            return "AV5";
                        default:
                            return " - ";
                    }
                }
            }
        }
        #endregion

        #region Carregamento DropDown

        //====> Método que carrega o DropDown de Unidades Escolares
        private void CarregaUnidades()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP }).OrderBy(e => e.NO_FANTAS_EMP);

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

        //====> Método que carrega o DropDown de Anos
        private void CarregaAnos()
        {
            ddlAno.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                 select new { tb43.CO_ANO_GRADE }).Distinct().OrderByDescending(g => g.CO_ANO_GRADE);

            ddlAno.DataTextField = "CO_ANO_GRADE";
            ddlAno.DataValueField = "CO_ANO_GRADE";
            ddlAno.DataBind();
        }

        //====> Método que carrega o DropDown de Modalidades, verifica se o usuário logado é professor.
        private void CarregaModalidades()
        {
            int ano = (ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0);
            if (LoginAuxili.FLA_PROFESSOR != "S")
            {
                ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where(m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);
            }
            else
            {
                ddlModalidade.DataSource = (from rm in TB_RESPON_MATERIA.RetornaTodosRegistros()
                                            join mo in TB44_MODULO.RetornaTodosRegistros() on rm.CO_MODU_CUR equals mo.CO_MODU_CUR
                                            where rm.CO_COL_RESP == LoginAuxili.CO_COL
                                             && rm.CO_ANO_REF == ano
                                            select new
                                            {
                                                mo.DE_MODU_CUR,
                                                rm.CO_MODU_CUR
                                            }).Distinct();
            }
            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();

            ddlModalidade.Items.Insert(0, new ListItem("Todas", ""));
        }

        //====> Método que carrega o DropDown de Séries, verifica se o usuário logado é o professor.
        private void CarregaSerieCurso()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            string coAnoGrade = ddlAno.SelectedValue;
            int ano = (ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0);

            if (LoginAuxili.FLA_PROFESSOR != "S")
            {
                ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(coEmp)
                                            where tb01.CO_MODU_CUR == modalidade
                                            join tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(coEmp) on tb01.CO_CUR equals tb43.CO_CUR
                                            where tb43.TB44_MODULO.CO_MODU_CUR == tb01.CO_MODU_CUR && tb43.CO_ANO_GRADE == coAnoGrade
                                            select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct().OrderBy(c => c.NO_CUR);
            }
            else
            {
                ddlSerieCurso.DataSource = (from rm in TB_RESPON_MATERIA.RetornaTodosRegistros()
                                            join c in TB01_CURSO.RetornaTodosRegistros() on rm.CO_CUR equals c.CO_CUR
                                            join tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on rm.CO_CUR equals tb43.CO_CUR
                                            where rm.CO_COL_RESP == LoginAuxili.CO_COL
                                            && rm.CO_MODU_CUR == modalidade
                                            && rm.CO_ANO_REF == ano
                                            select new
                                            {
                                                c.NO_CUR,
                                                rm.CO_CUR
                                            }).Distinct();
            }
            ddlSerieCurso.DataTextField = "NO_CUR";
            ddlSerieCurso.DataValueField = "CO_CUR";
            ddlSerieCurso.DataBind();

            ddlSerieCurso.Items.Insert(0, new ListItem("Todas", ""));
        }

        //====> Método que carrega o DropDown de Turmas, verifica se o usuário logado é professor.
        private void CarregaTurma()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            if (LoginAuxili.FLA_PROFESSOR != "S")
            {
                ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                       where tb06.CO_EMP == coEmp && tb06.CO_MODU_CUR == modalidade && tb06.CO_CUR == serie
                                       select new { tb06.TB129_CADTURMAS.CO_SIGLA_TURMA, tb06.CO_TUR }).OrderBy(t => t.CO_SIGLA_TURMA);
            }
            else
            {
                int ano = (ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0);
                ddlTurma.DataSource = (from rm in TB_RESPON_MATERIA.RetornaTodosRegistros()
                                       join t in TB129_CADTURMAS.RetornaTodosRegistros() on rm.CO_TUR equals t.CO_TUR
                                       where rm.CO_COL_RESP == LoginAuxili.CO_COL
                                       && rm.CO_MODU_CUR == modalidade
                                       && rm.CO_CUR == serie
                                       && rm.CO_ANO_REF == ano
                                       select new
                                       {
                                           t.NO_TURMA,
                                           rm.CO_TUR,
                                           t.CO_SIGLA_TURMA
                                       }).Distinct();
            }
            ddlTurma.DataTextField = "CO_SIGLA_TURMA";
            ddlTurma.DataValueField = "CO_TUR";
            ddlTurma.DataBind();

            ddlTurma.Items.Insert(0, new ListItem("Todas", ""));
        }

        //====> Método que carrega o DropDown de Matérias, verifica se o usuário é professor.
        private void CarregaMaterias()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int ano = (ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0);
            string anoStr = ano.ToString();

            if (LoginAuxili.FLA_PROFESSOR != "S")
            {
                var res = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                           where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.CO_CUR == serie && tb43.CO_ANO_GRADE == anoStr && tb43.CO_EMP == coEmp
                           join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                           join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                           select new { tb107.ID_MATERIA, tb107.NO_MATERIA }).Distinct().OrderBy(m => m.NO_MATERIA);

                if (res != null)
                {
                    ddlMateria.DataTextField = "NO_MATERIA";
                    ddlMateria.DataValueField = "ID_MATERIA";
                    ddlMateria.DataSource = res;
                    ddlMateria.DataBind();
                }
                ddlMateria.Items.Insert(0, new ListItem("Todos", "0"));

            }
            else
            {
                int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
                ddlMateria.DataSource = (from rm in TB_RESPON_MATERIA.RetornaTodosRegistros()
                                         join tb02 in TB02_MATERIA.RetornaTodosRegistros() on rm.CO_MAT equals tb02.CO_MAT
                                         join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                         where rm.CO_COL_RESP == LoginAuxili.CO_COL
                                         && rm.CO_MODU_CUR == modalidade
                                         && rm.CO_CUR == serie
                                         && rm.CO_ANO_REF == ano
                                         && rm.CO_TUR == turma
                                         select new
                                         {
                                             tb107.NO_MATERIA,
                                             rm.CO_MAT,
                                             tb107.ID_MATERIA
                                         }).Distinct();
            }

            ddlMateria.DataTextField = "NO_MATERIA";
            ddlMateria.DataValueField = "ID_MATERIA";
            ddlMateria.DataBind();

            ddlMateria.Items.Insert(0, new ListItem("Todas", ""));
        }

        //====> Método que carrega o DropDown de Alunos.
        private void CarregaAluno()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;

            ddlAluno.DataSource = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                                   join tb48 in TB48_GRADE_ALUNO.RetornaTodosRegistros() on tb07.CO_ALU equals tb48.CO_ALU
                                   where tb48.CO_EMP == coEmp && tb48.CO_MODU_CUR == modalidade && tb48.CO_CUR == serie && tb48.CO_TUR == turma
                                   select new { tb07.CO_ALU, tb07.NO_ALU, tb48.CO_CUR, tb48.CO_TUR, tb48.CO_MODU_CUR }).Distinct().OrderBy(a => a.NO_ALU);

            ddlAluno.DataTextField = "NO_ALU";
            ddlAluno.DataValueField = "CO_ALU";
            ddlAluno.DataBind();

            ddlAluno.Items.Insert(0, new ListItem("Todos", ""));
        }
        #endregion

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAnos();
            CarregaModalidades();
            CarregaSerieCurso();
            CarregaTurma();
            CarregaMaterias();
            CarregaAluno();
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
            CarregaMaterias();
            CarregaAluno();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
            CarregaMaterias();
            CarregaAluno();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaMaterias();
            CarregaAluno();
        }

        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaModalidades();
            CarregaSerieCurso();
            CarregaTurma();
            CarregaMaterias();
            CarregaAluno();
        }

        protected void ddlMateria_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAluno();
        }
    }
}
