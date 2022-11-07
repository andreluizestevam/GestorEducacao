//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: CONTROLE DE TURMAS POR SÉRIES/CURSOS
// OBJETIVO: GERA GRADE DE HORÁRIO PARA TURMA (MODALIDADE/SÉRIE) 
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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3020_CtrlPedagogicoTurmas.F3025_GeraGradeHorarioTurma
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
            if (!Page.IsPostBack)
            {                
                CarregaModalidades();
                CarregaSerieCurso(null);
                CarregaAnos();
                CarregaTurma(null);
            }
        }

        protected void CurrentPadraoBuscas_OnDefineColunasGridView()
        {            
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_EMP", "CO_MODU_CUR", "CO_CUR", "CO_TUR", "CO_MAT", "CO_DIA_SEMA_GRD", "TP_TURNO", "NR_TEMPO", "CO_ANO_GRADE" };

            BoundField bfTP = new BoundField();
            bfTP.DataField = "TIPO";
            bfTP.HeaderText = "TIPO";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfTP);

            BoundField bfDia = new BoundField();
            bfDia.DataField = "DIA_SEMANA";
            bfDia.HeaderText = "Dia";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfDia);

            BoundField bfTempo = new BoundField();
            bfTempo.DataField = "TEMPO_AULA";
            bfTempo.HeaderText = "Tempo";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfTempo);

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_ANO_GRADE",
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
                HeaderText = "Série"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_SIGLA_TURMA",
                HeaderText = "Turma"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_RED_MATERIA",
                HeaderText = "Máteria"
            });

            BoundField bfHRInicio = new BoundField();
            bfHRInicio.DataField = "HR_INICIO";
            bfHRInicio.HeaderText = "Início";
            bfHRInicio.ItemStyle.CssClass = "codCol";
            bfHRInicio.DataFormatString = "{0:d}";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfHRInicio);

            BoundField bfHRtermino = new BoundField();
            bfHRtermino.DataField = "HR_TERMI";
            bfHRtermino.HeaderText = "Término";
            bfHRtermino.ItemStyle.CssClass = "codCol";
            bfHRtermino.DataFormatString = "{0:d}";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfHRtermino);
        }

        protected void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int modalidade = ddlModalidade.SelectedValue != "0" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "0" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "0" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int coAnoGrade = ddlAno.SelectedValue != "0" ? int.Parse(ddlAno.SelectedValue) : 0;
            string tphr = ddlTpHorario.SelectedValue;

            var resultado = (from tb05 in TB05_GRD_HORAR.RetornaTodosRegistros()
                             join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tb05.CO_TUR equals tb129.CO_TUR
                             join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb05.CO_MAT equals tb02.CO_MAT
                             join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                             where tb05.CO_EMP == tb02.TB01_CURSO.CO_EMP && tb05.CO_MODU_CUR == tb02.TB01_CURSO.CO_MODU_CUR && tb05.CO_CUR == tb02.TB01_CURSO.CO_CUR
                             && tb05.CO_EMP == LoginAuxili.CO_EMP
                             && (modalidade != 0 ? tb05.CO_MODU_CUR == modalidade : modalidade == 0)
                             && (serie != 0 ? tb05.CO_CUR == serie : serie == 0)
                             && (turma != 0 ? tb05.CO_TUR == turma : turma == 0)
                             && (coAnoGrade != 0 ? tb05.CO_ANO_GRADE == coAnoGrade : coAnoGrade == 0)
                             && (tphr != "0" ? tb05.TP_HORAR_AGEND == tphr : 0 == 0)
                             select new
                             {
                                 tb05.CO_EMP,
                                 tb05.CO_MODU_CUR,
                                 tb05.CO_CUR,
                                 tb05.CO_TUR,
                                 tb107.NO_RED_MATERIA,
                                 DIA_SEMANA = (tb05.CO_DIA_SEMA_GRD.Equals(0) ? "Domingo" : (tb05.CO_DIA_SEMA_GRD.Equals(1) ? "Segunda-Feira" :
                                                   (tb05.CO_DIA_SEMA_GRD.Equals(2) ? "Terça-Feira" : (tb05.CO_DIA_SEMA_GRD.Equals(3) ? "Quarta-Feira" :
                                                   (tb05.CO_DIA_SEMA_GRD.Equals(4) ? "Quinta-Feira" : (tb05.CO_DIA_SEMA_GRD.Equals(5) ? "Sexta-Feira" :
                                                   (tb05.CO_DIA_SEMA_GRD.Equals(6) ? "Sábado" : ""))))))),
                                 TEMPO_AULA = (tb05.NR_TEMPO.Equals(1) ? "1ºTempo" : (tb05.NR_TEMPO.Equals(2) ? "2ºTempo" :
                                                   (tb05.NR_TEMPO.Equals(3) ? "3ºTempo" : (tb05.NR_TEMPO.Equals(4) ? "4ºTempo" :
                                                   (tb05.NR_TEMPO.Equals(5) ? "5ºTempo" : (tb05.NR_TEMPO.Equals(6) ? "6ºTempo" :
                                                   (tb05.NR_TEMPO.Equals(7) ? "7ºTempo" : ""))))))),
                                 TIPO = (tb05.TP_HORAR_AGEND.Equals("DEP") ? "Dependência" : (tb05.TP_HORAR_AGEND.Equals("REG") ? "Regular" :
                                        (tb05.TP_HORAR_AGEND.Equals("REC") ? "Recuperação" : (tb05.TP_HORAR_AGEND.Equals("REF") ? "Reforço" :
                                        (tb05.TP_HORAR_AGEND.Equals("ERE") ? "Ensino Remoto" : "-"))))),
                                 tb05.CO_DIA_SEMA_GRD,
                                 tb05.NR_TEMPO,
                                 tb05.CO_ANO_GRADE,
                                 tb05.CO_MAT,
                                 tb05.TP_TURNO,
                                 tb05.TB131_TEMPO_AULA.HR_INICIO,
                                 tb05.TB131_TEMPO_AULA.HR_TERMI,
                                 tb02.TB01_CURSO.TB44_MODULO.DE_MODU_CUR,
                                 tb02.TB01_CURSO.NO_CUR,
                                 tb129.CO_SIGLA_TURMA
                             }).OrderBy(o => o.CO_MODU_CUR).ThenBy(o => o.CO_CUR).ThenBy(o => o.CO_TUR).ThenBy(o => o.CO_DIA_SEMA_GRD).ThenBy(o => o.TEMPO_AULA);
                              //}).OrderBy(o => o.CO_ANO_GRADE).ThenBy(o => o.DE_MODU_CUR).ThenBy(o => o.NO_CUR).ThenBy(o => o.CO_SIGLA_TURMA).ThenBy(o => o.NO_RED_MATERIA).OrderBy(o => o.CO_DIA_SEMA_GRD).ThenBy(o => o.TEMPO_AULA).Distinct();
            /*
            var resul2 = (from res in resultado
                         join tb02 in TB02_MATERIA.RetornaTodosRegistros() on res.CO_MAT equals tb02.CO_MAT
                         join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                         where res.CO_EMP == tb02.CO_EMP && res.CO_MODU_CUR == tb02.CO_MODU_CUR && res.CO_CUR == tb02.CO_CUR
                         select new
                         {
                             res.CO_EMP, res.CO_MODU_CUR, res.CO_CUR, res.CO_TUR,
                             res.DIA_SEMANA, tb107.NO_MATERIA, res.TEMPO_AULA, 
                             res.CO_DIA_SEMA_GRD, res.NR_TEMPO, res.CO_ANO_GRADE, res.CO_MAT, res.TP_TURNO,
                             res.HR_INICIO, res.HR_TERMI
                         }).OrderBy(o => o.CO_DIA_SEMA_GRD).ThenBy(o => o.TEMPO_AULA).ThenBy(o => o.NO_MATERIA).AsQueryable();*/


            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        protected void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoEmp, "CO_EMP"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoModuCur, "CO_MODU_CUR"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoCur, "CO_CUR"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoTur, "CO_TUR"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoMat, "CO_MAT"));
            queryStringKeys.Add(new KeyValuePair<string, string>("CoDia", "CO_DIA_SEMA_GRD"));
            queryStringKeys.Add(new KeyValuePair<string, string>("TpTurno", "TP_TURNO"));
            queryStringKeys.Add(new KeyValuePair<string, string>("NuTemp", "NR_TEMPO"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Ano, "CO_ANO_GRADE"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

//====> Método que carrega o DropDown de Anos
        private void CarregaAnos()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            ddlAno.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                                 where tb43.CO_EMP == LoginAuxili.CO_EMP && tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.CO_CUR == serie
                                 select new { tb43.CO_ANO_GRADE }).Distinct().OrderByDescending(g => g.CO_ANO_GRADE);
             
            ddlAno.DataTextField = "CO_ANO_GRADE";
            ddlAno.DataValueField = "CO_ANO_GRADE";
            ddlAno.DataBind();

            ddlAno.Items.Insert(0, new ListItem("Todos", "0"));
        }

//====> Método que carrega o DropDown de Modalidades
        private void CarregaModalidades()
        {
            ddlSerieCurso.Items.Clear();
            ddlAno.Items.Clear();
            ddlTurma.Items.Clear();

            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where( m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();

            ddlModalidade.Items.Insert(0, new ListItem("Todas", "0"));
        }

//====> Método que carrega o DropDown de Séries
        private void CarregaSerieCurso(int? coModuCur)
        {                        
            string anoGrade = ddlAno.SelectedValue;
            int modalidade;

            ddlAno.Items.Clear();
            ddlTurma.Items.Clear();

            if (ddlModalidade.SelectedValue != "0" || coModuCur > 0)
            {
                if (!String.IsNullOrEmpty(coModuCur.ToString()))
                    modalidade = Convert.ToInt32(coModuCur);
                else
                    modalidade = int.Parse(ddlModalidade.SelectedValue);

                ddlSerieCurso.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                            join tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on tb43.CO_CUR equals tb01.CO_CUR
                                            where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.TB44_MODULO.CO_MODU_CUR == tb01.CO_MODU_CUR
                                            select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct().OrderBy(g => g.NO_CUR);

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();
            }

            ddlSerieCurso.Items.Insert(0, new ListItem("Todas", "0"));
        }

//====> Método que carrega o DropDown de Turmas    
        private void CarregaTurma(int? coModuCur)
        {
            int modalidade;

            if (ddlModalidade.SelectedValue != "0" || coModuCur > 0)
            {
                if (!String.IsNullOrEmpty(coModuCur.ToString()))
                    modalidade = Convert.ToInt32(coModuCur);
                else
                    modalidade = int.Parse(ddlModalidade.SelectedValue);

                int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

                if (serie != 0)
                {
                    ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                           where tb06.CO_MODU_CUR == modalidade && tb06.CO_CUR == serie
                                           select new { tb06.TB129_CADTURMAS.CO_SIGLA_TURMA, tb06.CO_TUR }).OrderBy(t => t.CO_SIGLA_TURMA);

                    ddlTurma.DataTextField = "CO_SIGLA_TURMA";
                    ddlTurma.DataValueField = "CO_TUR";
                    ddlTurma.DataBind();
                }
                else
                    ddlTurma.Items.Clear();
            }
            else
                ddlTurma.Items.Clear();

            ddlTurma.Items.Insert(0, new ListItem("Todas", "0"));
        }
        #endregion
        
        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            int modalidade = ddlModalidade.SelectedValue != "0" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            if (modalidade != 0)
            {
                CarregaSerieCurso(modalidade);
                CarregaAnos();
                CarregaTurma(modalidade);
            }
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            int modalidade = ddlModalidade.SelectedValue != "0" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            CarregaAnos();

            if (modalidade != 0)
                CarregaTurma(modalidade);
        }

        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            int modalidade = ddlModalidade.SelectedValue != "0" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            if (modalidade != 0)
            {
                ddlTurma.Items.Clear();
                CarregaTurma(modalidade);
            }
        }
    }
}
