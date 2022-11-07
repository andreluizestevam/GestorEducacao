//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE DE AVALIAÇÃO ESCOLAR DO ALUNO
// OBJETIVO: LANÇAMENTO E MANUTENÇÃO, POR SÉRIE/TURMA, DE MÉDIAS BIMESTRAIS DO ALUNO
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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3500_CtrlDesempenhoEscolar.F3503_LancManutSerieTurmaMediaBim
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

        protected void Page_Load()
        {
            if (IsPostBack) return;

            bool reca = CarregaParametros();

            if (reca == false)
            {
                CarregaAnos();
                CarregaModalidades();
                CarregaSerieCurso();
                CarregaTurma();
                CarregaMedidas();
                ddlAno.SelectedValue = DateTime.Now.Year.ToString();
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

        /// <summary>
        /// Método responsável por recarregar determinadas informações, caso isso tenha previamente sido inserido em uma variável de sessão
        /// </summary>
        /// <returns></returns>
        private bool CarregaParametros()
        {
            bool persist = false;
            if (HttpContext.Current.Session["buscaLancMediasBimestraisMD"] != null)
            {
                var parametros = HttpContext.Current.Session["buscaLancMediasBimestraisMD"].ToString();

                if (!string.IsNullOrEmpty(parametros))
                {
                    var par = parametros.ToString().Split(';');
                    var ano = par[0];
                    var coRefe = par[1];
                    var modalidade = par[2];
                    var serieCurso = par[3];
                    var turma = par[4];

                    CarregaAnos();
                    ddlAno.SelectedValue = ano;

                    ddlReferencia.SelectedValue = coRefe;

                    CarregaModalidades();
                    ddlModalidade.SelectedValue = modalidade;

                    CarregaSerieCurso();
                    ddlSerieCurso.SelectedValue = serieCurso;

                    CarregaTurma();
                    ddlTurma.SelectedValue = turma;

                    CurrentPadraoBuscas_OnAcaoBuscaDefineGridView();

                    persist = true;
                    HttpContext.Current.Session.Remove("buscaLancMediasBimestraisMD");
                }
            }
            return persist;
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_ANO_GRADE", "CO_BIM_REF", "CO_MODU_CUR", "CO_CUR", "CO_TUR", "CO_MAT" };

            BoundField bf1 = new BoundField();
            bf1.DataField = "NO_SIGLA_MATERIA";
            bf1.HeaderText = "Sigla";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf1);
            BoundField bf2 = new BoundField();
            bf2.DataField = "NO_MATERIA";
            bf2.HeaderText = "Matéria";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf2);
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int coReferencia = ddlReferencia.SelectedValue == "B1" ? 1 : ddlReferencia.SelectedValue == "B2" ? 2 : 
                ddlReferencia.SelectedValue == "B3" ? 3 : ddlReferencia.SelectedValue == "B4" ? 4 : ddlReferencia.SelectedValue == "T1" ? 5 : 
                ddlReferencia.SelectedValue == "T2" ? 6 : ddlReferencia.SelectedValue == "T3" ? 7 : 0;
            int ano = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;

            if (LoginAuxili.FLA_PROFESSOR != "S")
            {
                //Filtra se o usuário tem interesse em ver as matérias agrupadoras ou não
                if (chkMosAgrupa.Checked)
                {
                    var resultado = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                                     where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.CO_CUR == serie
                                     && tb43.CO_ANO_GRADE == ddlAno.SelectedValue && tb43.CO_EMP == LoginAuxili.CO_EMP
                                     && tb43.ID_MATER_AGRUP == null && tb43.FL_LANCA_NOTA == "S"
                                     join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                                     join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                     select new
                                     {
                                         tb43.CO_ANO_GRADE,
                                         CO_BIM_REF = coReferencia,
                                         tb43.TB44_MODULO.CO_MODU_CUR,
                                         tb43.CO_CUR,
                                         CO_TUR = turma,
                                         tb02.CO_MAT,
                                         tb107.NO_MATERIA,
                                         tb107.NO_SIGLA_MATERIA,
                                         tb43.CO_ORDEM_IMPRE
                                     }).OrderBy(w => w.CO_ORDEM_IMPRE).ThenBy(g => g.NO_MATERIA);

                    CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
                }
                else
                {
                    var resultado = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                                     where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.CO_CUR == serie
                                     && tb43.CO_ANO_GRADE == ddlAno.SelectedValue && tb43.CO_EMP == LoginAuxili.CO_EMP
                                     && tb43.FL_LANCA_NOTA == "S"
                                     && ((tb43.FL_DISCI_AGRUPA == "N") || (tb43.FL_DISCI_AGRUPA == null))
                                     join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                                     join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                     select new
                                     {
                                         tb43.CO_ANO_GRADE,
                                         CO_BIM_REF = coReferencia,
                                         tb43.TB44_MODULO.CO_MODU_CUR,
                                         tb43.CO_CUR,
                                         CO_TUR = turma,
                                         tb02.CO_MAT,
                                         tb107.NO_MATERIA,
                                         tb107.NO_SIGLA_MATERIA,
                                         tb43.CO_ORDEM_IMPRE
                                     }).OrderBy(w => w.CO_ORDEM_IMPRE).ThenBy(g => g.NO_MATERIA);

                    CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
                }
            }
            else
            {
                int coTur = (ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0);
                string tur = TB06_TURMAS.RetornaTodosRegistros().Where(w => w.CO_TUR == coTur).FirstOrDefault().CO_FLAG_RESP_TURMA;

                if (tur == "S")
                {
                    //var resultado = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                    //                 where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.CO_CUR == serie
                    //                 && tb43.CO_ANO_GRADE == ddlAno.SelectedValue && tb43.CO_EMP == LoginAuxili.CO_EMP
                    //                 join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                    //                 join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                    //                 join rm in TB_RESPON_MATERIA.RetornaTodosRegistros() on tb02.CO_MAT equals rm.CO_MAT
                    //                 where rm.CO_COL_RESP == LoginAuxili.CO_COL

                    var resultado = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                                     where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.CO_CUR == serie
                                     && tb43.CO_ANO_GRADE == ddlAno.SelectedValue && tb43.CO_EMP == LoginAuxili.CO_EMP && tb43.FL_LANCA_NOTA == "S"
                                     join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                                     join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                     select new
                                     {
                                         tb43.CO_ANO_GRADE,
                                         CO_BIM_REF = coReferencia,
                                         tb43.TB44_MODULO.CO_MODU_CUR,
                                         tb43.CO_CUR,
                                         CO_TUR = turma,
                                         tb02.CO_MAT,
                                         tb107.NO_MATERIA,
                                         tb107.NO_SIGLA_MATERIA
                                     }).Distinct().OrderBy(g => g.NO_MATERIA);

                    CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
                }
                else
                {
                    var resultado = (from tbresp in TB_RESPON_MATERIA.RetornaTodosRegistros()
                                     join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on tbresp.CO_MAT equals tb43.CO_MAT
                                     where tbresp.CO_MODU_CUR == modalidade && tbresp.CO_CUR == serie
                                     && tbresp.CO_ANO_REF == ano && tbresp.CO_COL_RESP == LoginAuxili.CO_COL && tbresp.CO_EMP == LoginAuxili.CO_EMP
                                     && tb43.TB44_MODULO.CO_MODU_CUR == tbresp.CO_MODU_CUR
                                     && tb43.CO_CUR == tbresp.CO_CUR
                                     && tb43.FL_LANCA_NOTA == "S"
                                     join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tbresp.CO_MAT equals tb02.CO_MAT
                                     join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                     select new
                                     {
                                         CO_ANO_GRADE = tbresp.CO_ANO_REF,
                                         CO_BIM_REF = coReferencia,
                                         tbresp.CO_MODU_CUR,
                                         tbresp.CO_CUR,
                                         CO_TUR = turma,
                                         tb02.CO_MAT,
                                         tb107.NO_MATERIA,
                                         tb107.NO_SIGLA_MATERIA
                                     }).Distinct().OrderBy(g => g.NO_MATERIA);

                    CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
                }

            }
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Ano, "CO_ANO_GRADE"));
            queryStringKeys.Add(new KeyValuePair<string, string>("ref", "CO_BIM_REF"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoModuCur, "CO_MODU_CUR"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoCur, "CO_CUR"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoTur, "CO_TUR"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoMat, "CO_MAT"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

        //====> Método que carrega o DropDown de Anos
        private void CarregaAnos()
        {
            AuxiliCarregamentos.CarregaAnoGrdCurs(ddlAno, LoginAuxili.CO_EMP, false);
        }

        //====> Método que carrega o DropDown de Modalidades, verfica se o usuário logado é professor.
        private void CarregaModalidades()
        {
            int ano = int.Parse(ddlAno.SelectedValue);
            if (LoginAuxili.FLA_PROFESSOR != "S")
                AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, false);
            else
                AuxiliCarregamentos.carregaModalidadesProfeResp(ddlModalidade, LoginAuxili.CO_COL, ano, false);

            CarregaSerieCurso();
        }

        //====> Método que carrega o DropDown de Séries, verifica se o usuário logado é professor.
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int ano = int.Parse(ddlAno.SelectedValue);

            string coAnoGrade = ddlAno.SelectedValue;

            if (LoginAuxili.FLA_PROFESSOR != "S")
                AuxiliCarregamentos.carregaSeriesCursos(ddlSerieCurso, modalidade, LoginAuxili.CO_EMP, false);
            else
                AuxiliCarregamentos.carregaSeriesCursosProfeResp(ddlSerieCurso, modalidade, LoginAuxili.CO_COL, ano, false);

            CarregaTurma();
        }

        //====> Método que carrega o DropDown de Turmas, verifica se o usuário logado é professor.
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int ano = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;
            if (LoginAuxili.FLA_PROFESSOR != "S")
                AuxiliCarregamentos.CarregaTurmas(ddlTurma, LoginAuxili.CO_EMP, modalidade, serie, false);
            else
                AuxiliCarregamentos.CarregaTurmasProfeResp(ddlTurma, LoginAuxili.CO_EMP, modalidade, serie, LoginAuxili.CO_COL, ano, false);
        }
        #endregion

        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
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
        }
    }
}