//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE LETIVO DE AULAS E ATIVIDADES
// OBJETIVO: CADASTRO DAS ATIVIDADES LETIVAS DE MATÉRIAS POR SÉRIE/TURMA
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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3100_CtrlOperProfessores.F3110_CtrlPlanejamentoAulas.F3111_CadastroAtividadeLetivaMateria
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
            if (Page.IsPostBack) return;

            CarregaAnos();
            CarregaModalidades();

            if (LoginAuxili.TIPO_USU.Equals("A"))
            {
                var tb08 = TB08_MATRCUR.RetornaPeloAluno(LoginAuxili.CO_RESP);
                tb08.TB44_MODULOReference.Load();

                if (!string.IsNullOrEmpty(tb08.CO_ANO_MES_MAT) && ddlAno.Items.FindByValue(tb08.CO_ANO_MES_MAT) != null)
                    ddlAno.SelectedValue = tb08.CO_ANO_MES_MAT;

                if (tb08.TB44_MODULO.CO_MODU_CUR != 0 && ddlModalidade.Items.FindByValue(tb08.TB44_MODULO.CO_MODU_CUR.ToString()) != null)
                    ddlModalidade.SelectedValue = tb08.TB44_MODULO.CO_MODU_CUR.ToString();

                CarregaSerieCurso();

                if (tb08.CO_CUR != 0 && ddlSerieCurso.Items.FindByValue(tb08.CO_CUR.ToString()) != null)
                    ddlSerieCurso.SelectedValue = tb08.CO_CUR.ToString();

                CarregaTurma();

                if (tb08.CO_TUR != 0 && ddlTurma.Items.FindByValue(tb08.CO_TUR.ToString()) != null)
                    ddlTurma.SelectedValue = tb08.CO_TUR.ToString();

                ddlAno.Enabled =
                ddlModalidade.Enabled =
                ddlSerieCurso.Enabled =
                ddlTurma.Enabled = false;
            }
            else
            {
                CarregaSerieCurso();
                CarregaTurma();
            }

            CarregaMaterias();
            CarregaProfessor();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            //André
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_PLA_AULA" };

            BoundField bfPlanejado = new BoundField();
            bfPlanejado.DataField = "DT_PREV_PLA";
            bfPlanejado.HeaderText = "Previsão";
            bfPlanejado.ItemStyle.CssClass = "numeroCol";
            bfPlanejado.DataFormatString = "{0:d}";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfPlanejado);

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_RED_MATERIA",
                HeaderText = "Matéria"
            });
            
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_COL",
                HeaderText = "Professor"
            });

            BoundField bfPlanejado2 = new BoundField();
            bfPlanejado2.DataField = "DE_TEMA_AULA";
            bfPlanejado2.HeaderText = "Tema da aula";
            bfPlanejado2.ItemStyle.CssClass = "numeroCol";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfPlanejado2);

            BoundField bfPlanejado3 = new BoundField();
            bfPlanejado3.DataField = "HR_FIM_AULA_PLA";
            bfPlanejado3.HeaderText = "Término";
            bfPlanejado3.ItemStyle.CssClass = "numeroCol";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfPlanejado3);

            //BoundField bfPlanejado3 = new BoundField();
            //bfPlanejado3.DataField = "HR_FIM_AULA_PLA";
            //bfPlanejado3.HeaderText = "Término";
            //bfPlanejado3.ItemStyle.CssClass = "numeroCol";
            //CurrentPadraoBuscas.GridBusca.Columns.Add(bfPlanejado3);

            //BoundField bfPlanejado4 = new BoundField();
            //bfPlanejado4.DataField = "FLA_HOMOLOG";
            //bfPlanejado4.HeaderText = "HOMOLOG";
            //bfPlanejado4.ItemStyle.CssClass = "numeroCol";
            //CurrentPadraoBuscas.GridBusca.Columns.Add(bfPlanejado4);
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coAnoRefPla = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int materia = ddlDisciplina.SelectedValue != "" ? int.Parse(ddlDisciplina.SelectedValue) : 0;
            int coCol = ddlProfessor.SelectedValue != "" ? int.Parse(ddlProfessor.SelectedValue) : 0;
            int mesRefer = ddlMes.SelectedValue != "" ? int.Parse(ddlMes.SelectedValue) : 0;   
            

            //André
            if (System.Configuration.ConfigurationManager.AppSettings["Testes"] != null)
            {
                if (System.Configuration.ConfigurationManager.AppSettings["Testes"] == "Sim")
                {
                    var resultado = (from tb17 in TB17_PLANO_AULA.RetornaTodosRegistros()
                                     join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb17.CO_MAT equals tb02.CO_MAT
                                     join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                     select new
                                     {
                                         tb17.HR_FIM_AULA_PLA,
                                         tb17.HR_INI_AULA_PLA,
                                         tb17.DT_PREV_PLA,
                                         tb17.TB03_COLABOR.NO_COL,
                                         tb17.CO_PLA_AULA,
                                         tb17.DE_TEMA_AULA,
                                         FLA_HOMOLOG = tb17.FLA_HOMOLOG == "S" ? "Sim" : "Não",
                                         tb107.NO_RED_MATERIA
                                     }).OrderBy(p => p.DT_PREV_PLA);
                    CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
                }
                else
                {
                    var resultado = (from tb17 in TB17_PLANO_AULA.RetornaTodosRegistros()
                                     join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb17.CO_MAT equals tb02.CO_MAT
                                     join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                     where tb17.CO_EMP == LoginAuxili.CO_EMP && tb17.CO_ANO_REF_PLA == coAnoRefPla
                                     && (modalidade != 0 ? tb17.CO_MODU_CUR == modalidade : modalidade == 0)
                                     && (serie != 0 ? tb17.CO_CUR == serie : serie == 0) && (turma != 0 ? tb17.CO_TUR == turma : turma == 0)
                                     && (materia != 0 ? tb17.CO_MAT == materia : materia == 0) && (coCol != 0 ? tb17.TB03_COLABOR.CO_COL == coCol : coCol == 0)
                                     && (mesRefer != 0 ? tb17.DT_PREV_PLA.Month == mesRefer : mesRefer == 0)
                                     select new
                                     {
                                         tb17.HR_FIM_AULA_PLA,
                                         tb17.HR_INI_AULA_PLA,
                                         tb17.DT_PREV_PLA,
                                         tb17.TB03_COLABOR.NO_COL,
                                         tb17.CO_PLA_AULA,
                                         tb17.DE_TEMA_AULA,
                                         FLA_HOMOLOG = tb17.FLA_HOMOLOG == "S" ? "Sim" : "Não",
                                         tb107.NO_RED_MATERIA
                                     }).OrderBy(p => p.DT_PREV_PLA);
                    CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
                }
            }
            else
            {
                var resultado = (from tb17 in TB17_PLANO_AULA.RetornaTodosRegistros()
                                 join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb17.CO_MAT equals tb02.CO_MAT
                                 join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                 where tb17.CO_EMP == LoginAuxili.CO_EMP && tb17.CO_ANO_REF_PLA == coAnoRefPla
                                 && (modalidade != 0 ? tb17.CO_MODU_CUR == modalidade : modalidade == 0)
                                 && (serie != 0 ? tb17.CO_CUR == serie : serie == 0) && (turma != 0 ? tb17.CO_TUR == turma : turma == 0)
                                 && (materia != 0 ? tb17.CO_MAT == materia : materia == 0) && (coCol != 0 ? tb17.TB03_COLABOR.CO_COL == coCol : coCol == 0)
                                 && (mesRefer != 0 ? tb17.DT_PREV_PLA.Month == mesRefer : mesRefer == 0)
                                 select new
                                 {
                                     tb17.HR_FIM_AULA_PLA,
                                     tb17.HR_INI_AULA_PLA,
                                     tb17.DT_PREV_PLA,
                                     tb17.TB03_COLABOR.NO_COL,
                                     tb17.CO_PLA_AULA,
                                     tb17.DE_TEMA_AULA,
                                     FLA_HOMOLOG = tb17.FLA_HOMOLOG == "S" ? "Sim" : "Não",
                                     tb107.NO_RED_MATERIA
                                 }).OrderBy(p => p.DT_PREV_PLA);
                CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
            }            
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_PLA_AULA"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion      

        #region Carregamento DropDown

//====> Método que carrega o DropDown de Anos
        private void CarregaAnos()
        {
            ddlAno.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                 select new { tb43.CO_ANO_GRADE }).Distinct().OrderByDescending( g => g.CO_ANO_GRADE );

            ddlAno.DataTextField = "CO_ANO_GRADE";
            ddlAno.DataValueField = "CO_ANO_GRADE";
            ddlAno.DataBind();
        }

//====> Método que carrega o DropDown de Modalidades, verifica se o usuário logado é professor.
        private void CarregaModalidades()
        {
            int ano = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;

            if (LoginAuxili.FLA_PROFESSOR != "S")
            {
                //ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where(m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);
                AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, false);
            }
            else
            {
                var res = (from rm in TB_RESPON_MATERIA.RetornaTodosRegistros()
                                            join mo in TB44_MODULO.RetornaTodosRegistros() on rm.CO_MODU_CUR equals mo.CO_MODU_CUR
                                            where rm.CO_COL_RESP == LoginAuxili.CO_COL
                                            && rm.CO_ANO_REF == ano
                                            select new
                                            {
                                                mo.DE_MODU_CUR,
                                                rm.CO_MODU_CUR
                                            }).Distinct();

                ddlModalidade.DataTextField = "DE_MODU_CUR";
                ddlModalidade.DataValueField = "CO_MODU_CUR";
                ddlModalidade.DataSource = res;
                ddlModalidade.DataBind();

                if (res.Count() != 1)              
                    ddlModalidade.Items.Insert(0, new ListItem("Selecione", ""));
            }            
        }

//====> Método que carrega o DropDown de Séries, verifica se o usuário logado é professor.
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            string coAnoGrade = ddlAno.SelectedValue;
            int ano = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;

            //if (modalidade != 0)
            //{
                if (LoginAuxili.FLA_PROFESSOR != "S")
                {
                    //ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                    //                            where tb01.CO_MODU_CUR == modalidade
                    //                            join tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on tb01.CO_CUR equals tb43.CO_CUR
                    //                            where tb43.CO_ANO_GRADE == coAnoGrade && tb43.TB44_MODULO.CO_MODU_CUR == tb01.CO_MODU_CUR
                    //                            select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct().OrderBy(c => c.NO_CUR);

                    //ddlSerieCurso.DataTextField = "NO_CUR";
                    //ddlSerieCurso.DataValueField = "CO_CUR";
                    //ddlSerieCurso.DataBind();
                    //ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
                    AuxiliCarregamentos.carregaSeriesCursos(ddlSerieCurso, modalidade, LoginAuxili.CO_EMP, false);
                }
                else
                {
                    var res = (from rm in TB_RESPON_MATERIA.RetornaTodosRegistros()
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

                    ddlSerieCurso.DataTextField = "NO_CUR";
                    ddlSerieCurso.DataValueField = "CO_CUR";
                    ddlSerieCurso.DataSource = res;
                    ddlSerieCurso.DataBind();

                    if (res.Count() != 1)
                        ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));                   
                }
            //}
        }

//====> Método que carrega o DropDown de Turmas, verifica se o usuário logado é professor.
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int ano = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;

            //if ((modalidade != 0) && (serie != 0))
            //{
                if (LoginAuxili.FLA_PROFESSOR != "S")
                {
                    //ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                    //                       where tb06.CO_MODU_CUR == modalidade && tb06.CO_CUR == serie
                    //                       select new { tb06.TB129_CADTURMAS.CO_SIGLA_TURMA, tb06.CO_TUR, });

                    //ddlTurma.DataTextField = "CO_SIGLA_TURMA";
                    //ddlTurma.DataValueField = "CO_TUR";
                    //ddlTurma.DataBind();
                    //ddlTurma.Items.Insert(0, new ListItem("Selecione", ""));
                    AuxiliCarregamentos.CarregaTurmas(ddlTurma, LoginAuxili.CO_EMP, modalidade, serie, false);
                }
                else
                {
                    var res = (from rm in TB_RESPON_MATERIA.RetornaTodosRegistros()
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
                    ddlTurma.DataTextField = "CO_SIGLA_TURMA";
                    ddlTurma.DataValueField = "CO_TUR";
                    ddlTurma.DataSource = res;
                    ddlTurma.DataBind();

                    if (res.Count() != 1)
                        ddlTurma.Items.Insert(0, new ListItem("Selecione", ""));                   
                }
            //}
        }

//====> Método que carrega o DropDown de Matérias, verifica se o usuário logado é professor.
        private void CarregaMaterias()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int ano = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;
            string anoGrade = ddlAno.SelectedValue;

            if (LoginAuxili.FLA_PROFESSOR != "S")
            {
                //ddlDisciplina.DataSource = (from tb02 in TB02_MATERIA.RetornaPelaModalidadeSerie(LoginAuxili.CO_EMP, modalidade, serie)
                //                            join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                //                            select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy(m => m.NO_MATERIA);
                //ddlDisciplina.DataTextField = "NO_MATERIA";
                //ddlDisciplina.DataValueField = "CO_MAT";
                //ddlDisciplina.DataBind();
                //ddlDisciplina.Items.Insert(0, new ListItem("Selecione", ""));
                AuxiliCarregamentos.CarregaMateriasGradeCurso(ddlDisciplina, LoginAuxili.CO_EMP, modalidade, serie, anoGrade, false);
            }
            else
            {

                var res = (from rm in TB_RESPON_MATERIA.RetornaTodosRegistros()
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

                ddlDisciplina.DataTextField = "NO_MATERIA";
                ddlDisciplina.DataValueField = "CO_MAT";
                ddlDisciplina.DataSource = res;
                ddlDisciplina.DataBind();

                if (res.Count() != 1)
                    ddlDisciplina.Items.Insert(0, new ListItem("Selecione", ""));                
            }
        
            //if (ddlDisciplina.Items.Count <= 0) 
                
            //    //ddlDisciplina.Items.Clear();
            //    ddlDisciplina.Items.Insert(0, new ListItem("Selecione", ""));
        }

//====> Método que carrega o DropDown de Professores, verifica se o usuário logado é professor.
        private void CarregaProfessor()
        {
            if (LoginAuxili.FLA_PROFESSOR != "S")
            {
                ddlProfessor.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(LoginAuxili.CO_EMP)
                                           where tb03.FLA_PROFESSOR == "S"
                                           select new { tb03.CO_COL, tb03.NO_COL }).OrderBy(c => c.NO_COL);
            }
            else
            {
                ddlProfessor.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(LoginAuxili.CO_EMP)
                                           where tb03.CO_COL == LoginAuxili.CO_COL
                                           select new { tb03.CO_COL, tb03.NO_COL }).OrderBy(c => c.NO_COL);
            }

            ddlProfessor.DataTextField = "NO_COL";
            ddlProfessor.DataValueField = "CO_COL";
            ddlProfessor.DataBind();

            if (LoginAuxili.FLA_PROFESSOR != "S")
            {
                ddlProfessor.Items.Insert(0, new ListItem("Selecione", ""));
            }
            else
            {
                ddlProfessor.Enabled = false;
            }
        }
        #endregion

        protected void ddlAno_SelectedIndexChanged1(object sender, EventArgs e)
        {
            CarregaModalidades();
            CarregaSerieCurso();
            CarregaTurma();
            CarregaMaterias();
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
            CarregaMaterias();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
            CarregaMaterias();      
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaMaterias();
        }
    }
}
