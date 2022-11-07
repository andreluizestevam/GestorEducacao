using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Text.RegularExpressions;
using C2BR.GestorEducacao.Reports.Helper;

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar
{
    public partial class RptMapaFreqAlunoDiscAnual : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptMapaFreqAlunoDiscAnual()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              int codInst,
                              string infos,
                              string strP_CO_EMP,
                              string strP_CO_MODU_CUR,
                              string strP_CO_CUR,
                              string strP_CO_TUR,
                              string strP_CO_ANO_REF,
                              string strP_CO_ALU,
                              string strP_CO_MAT,
                              string strP_CO_PARAM_FREQ,
                              string strP_CO_PARAM_FREQ_TIPO,
                              string NO_RELATORIO
            )
        {
            try
            {
                int intP_CO_EMP = int.Parse(strP_CO_EMP);

                #region Setar o Header e as Labels

                //Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                //Seta o título dinamicamente
                this.lblTitulo.Text = (!string.IsNullOrEmpty(NO_RELATORIO) ? NO_RELATORIO : "MAPA DE FREQUÊNCIA DE ALUNOS/DISCIPLINA (ANUAL)*");

                //Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(intP_CO_EMP);

                if (header == null)
                    return 0;

                //Setar o header do relatorio
                this.BaseInit(header);

                #endregion


                //Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Mapa de Frequencia Anual

                int coModuCur = int.Parse(strP_CO_MODU_CUR);
                int coCur = int.Parse(strP_CO_CUR);
                int coTur = int.Parse(strP_CO_TUR);
                int coAlu = int.Parse(strP_CO_ALU);
                int anoRefer = int.Parse(strP_CO_ANO_REF.Trim());

                #region Query de alunos

                List<string> coSit = new List<string>() { "A", "F", "T", "X" };

                var resultado = (from f in TB132_FREQ_ALU.RetornaTodosRegistros() 
	                             join g in TB48_GRADE_ALUNO.RetornaTodosRegistros()  on f.TB07_ALUNO.CO_ALU equals g.CO_ALU
	                             join m in TB02_MATERIA.RetornaTodosRegistros()  on f.CO_MAT equals m.CO_MAT
	                             join cm in TB107_CADMATERIAS.RetornaTodosRegistros()  on f.CO_MAT equals cm.ID_MATERIA 
	                             join c in TB01_CURSO.RetornaTodosRegistros()  on f.TB01_CURSO.CO_CUR equals c.CO_CUR
	                             join mo in TB44_MODULO.RetornaTodosRegistros()  on f.TB01_CURSO.CO_MODU_CUR equals mo.CO_MODU_CUR
	                             join t in TB129_CADTURMAS.RetornaTodosRegistros()  on f.CO_TUR equals t.CO_TUR
                                 join mm in TB08_MATRCUR.RetornaTodosRegistros() on f.TB07_ALUNO.CO_ALU equals mm.CO_ALU
	                             join gc in TB43_GRD_CURSO.RetornaTodosRegistros()  on f.CO_MAT equals gc.CO_MAT
                                 where g.CO_EMP == intP_CO_EMP
                                 && f.TB01_CURSO.CO_MODU_CUR == coModuCur
                                 && g.CO_ANO_MES_MAT == strP_CO_ANO_REF
                                 && g.NU_SEM_LET == "1"
                                 && f.TB01_CURSO.CO_CUR == (coCur != 0 ? coCur : f.TB01_CURSO.CO_CUR)
                                 && f.TB01_CURSO.CO_MODU_CUR == (coModuCur != 0 ? coModuCur : f.TB01_CURSO.CO_MODU_CUR)
                                 && f.CO_TUR == (coTur != 0 ? coTur : f.CO_TUR)
                                 && coSit.Contains(mm.CO_SIT_MAT)
                                 && f.TB07_ALUNO.CO_ALU == (coAlu != 0 ? coAlu : f.TB07_ALUNO.CO_ALU)

                                 select new
                                 {
                                     serie = f.TB01_CURSO.NO_CUR,
                                     turma = t.CO_SIGLA_TURMA,
                                     materia = cm.NO_RED_MATERIA,
                                     coMateria = gc.CO_MAT,
                                     coAluno = f.TB07_ALUNO.CO_ALU,
                                     aluno = f.TB07_ALUNO.NO_ALU,
                                     NIREAluno = f.TB07_ALUNO.NU_NIRE,
                                     MatriculaAluno = mm.CO_ALU_CAD,
                                     ORD_IMP = gc.CO_ORDEM_IMPRE ?? 20,
                                     SituacaoAluno = mm.CO_SIT_MAT == "A" ? "Em Aberto" : mm.CO_SIT_MAT == "F" ? "Finalizado" :
                                     mm.CO_SIT_MAT == "T" ? "Trancado" : "Transferido",
                                     DiscAgrupadora = gc.FL_DISCI_AGRUPA,
                                     coModalidade = mm.TB44_MODULO.CO_MODU_CUR,
                                     coCur = mm.CO_CUR,
                                     coMat = gc.CO_MAT,
                                     coAno = mm.CO_ANO_MES_MAT,
                                     idMatAgrup = gc.ID_MATER_AGRUP,
                                 }).ToList().Distinct().OrderBy(w => w.ORD_IMP).ThenBy(p => p.aluno).ThenBy(p => p.materia);

                var lst = (from iRes in resultado

                           select new RelMapaFreqAlunoDescAnual
                           {
                               serie = iRes.serie,
                               turma = iRes.turma,
                               materia = iRes.materia.ToLower(),
                               coMateria = iRes.coMateria,
                               coAluno = iRes.coAluno,
                               aluno = iRes.aluno.ToUpper(),
                               NIREAluno = iRes.NIREAluno,
                               SituacaoAluno = iRes.SituacaoAluno,
                               MatriculaAluno = iRes.MatriculaAluno.Insert(2, ".").Insert(6, "."),
                               TipoRelatorio = coAlu == 0 ? "S" : "A",
                               ORD_IMP = iRes.ORD_IMP,

                               DiscAgrupadora = iRes.DiscAgrupadora,
                               coModalidade = iRes.coModalidade,
                               coCur = iRes.coCur,
                               coMat = iRes.coMat,
                               coAno = iRes.coAno,
                               idMatAgrup = iRes.idMatAgrup,
                           }).ToList().OrderBy(p => p.aluno).ThenBy(p => p.materia);

                // Erro: não encontrou registros
                if (lst.ToList().Count == 0)
                    return -1;

                var res = lst.ToList().OrderBy(o => o.OrdImp_Valid).ThenBy(l => l.OrdImpFilhas).ThenBy(p => p.aluno).ThenBy(p => p.materia);

                foreach (var item in res)
                {
                    var lstOcor = (from tb132 in ctx.TB132_FREQ_ALU
                                   where tb132.TB01_CURSO.CO_CUR == coCur && tb132.CO_MAT == item.coMateria
                                   && tb132.TB01_CURSO.CO_MODU_CUR == coModuCur && tb132.CO_TUR == coTur
                                   && tb132.CO_ANO_REFER_FREQ_ALUNO == anoRefer
                                   && tb132.TB07_ALUNO.CO_ALU == item.coAluno
                                   select new
                                   {
                                       tb132.DT_FRE,
                                       tb132.CO_FLAG_FREQ_ALUNO
                                   }).ToList();

                    if (lstOcor.Count > 0)
                    {
                        item.Disciplina.Add(new Disciplina
                        {
                            TotJanP = lstOcor.Where(p => p.DT_FRE.Month == 1 && p.CO_FLAG_FREQ_ALUNO == "S").Count(),
                            TotJanF = lstOcor.Where(p => p.DT_FRE.Month == 1 && p.CO_FLAG_FREQ_ALUNO == "N").Count(),
                            TotFevP = lstOcor.Where(p => p.DT_FRE.Month == 2 && p.CO_FLAG_FREQ_ALUNO == "S").Count(),
                            TotFevF = lstOcor.Where(p => p.DT_FRE.Month == 2 && p.CO_FLAG_FREQ_ALUNO == "N").Count(),
                            TotMarP = lstOcor.Where(p => p.DT_FRE.Month == 3 && p.CO_FLAG_FREQ_ALUNO == "S").Count(),
                            TotMarF = lstOcor.Where(p => p.DT_FRE.Month == 3 && p.CO_FLAG_FREQ_ALUNO == "N").Count(),
                            TotAbrP = lstOcor.Where(p => p.DT_FRE.Month == 4 && p.CO_FLAG_FREQ_ALUNO == "S").Count(),
                            TotAbrF = lstOcor.Where(p => p.DT_FRE.Month == 4 && p.CO_FLAG_FREQ_ALUNO == "N").Count(),
                            TotMaiP = lstOcor.Where(p => p.DT_FRE.Month == 5 && p.CO_FLAG_FREQ_ALUNO == "S").Count(),
                            TotMaiF = lstOcor.Where(p => p.DT_FRE.Month == 5 && p.CO_FLAG_FREQ_ALUNO == "N").Count(),
                            TotJunP = lstOcor.Where(p => p.DT_FRE.Month == 6 && p.CO_FLAG_FREQ_ALUNO == "S").Count(),
                            TotJunF = lstOcor.Where(p => p.DT_FRE.Month == 6 && p.CO_FLAG_FREQ_ALUNO == "N").Count(),
                            TotJulP = lstOcor.Where(p => p.DT_FRE.Month == 7 && p.CO_FLAG_FREQ_ALUNO == "S").Count(),
                            TotJulF = lstOcor.Where(p => p.DT_FRE.Month == 7 && p.CO_FLAG_FREQ_ALUNO == "N").Count(),
                            TotAgoP = lstOcor.Where(p => p.DT_FRE.Month == 8 && p.CO_FLAG_FREQ_ALUNO == "S").Count(),
                            TotAgoF = lstOcor.Where(p => p.DT_FRE.Month == 8 && p.CO_FLAG_FREQ_ALUNO == "N").Count(),
                            TotSetP = lstOcor.Where(p => p.DT_FRE.Month == 9 && p.CO_FLAG_FREQ_ALUNO == "S").Count(),
                            TotSetF = lstOcor.Where(p => p.DT_FRE.Month == 9 && p.CO_FLAG_FREQ_ALUNO == "N").Count(),
                            TotOutP = lstOcor.Where(p => p.DT_FRE.Month == 10 && p.CO_FLAG_FREQ_ALUNO == "S").Count(),
                            TotOutF = lstOcor.Where(p => p.DT_FRE.Month == 10 && p.CO_FLAG_FREQ_ALUNO == "N").Count(),
                            TotNovP = lstOcor.Where(p => p.DT_FRE.Month == 11 && p.CO_FLAG_FREQ_ALUNO == "S").Count(),
                            TotNovF = lstOcor.Where(p => p.DT_FRE.Month == 11 && p.CO_FLAG_FREQ_ALUNO == "N").Count(),
                            TotDezP = lstOcor.Where(p => p.DT_FRE.Month == 12 && p.CO_FLAG_FREQ_ALUNO == "S").Count(),
                            TotDezF = lstOcor.Where(p => p.DT_FRE.Month == 12 && p.CO_FLAG_FREQ_ALUNO == "N").Count(),
                        });
                    }
                    else
                    {
                        item.Disciplina.Add(new Disciplina
                        {
                            TotJanP = 0,
                            TotJanF = 0,
                            TotFevP = 0,
                            TotFevF = 0,
                            TotMarP = 0,
                            TotMarF = 0,
                            TotAbrP = 0,
                            TotAbrF = 0,
                            TotMaiP = 0,
                            TotMaiF = 0,
                            TotJunP = 0,
                            TotJunF = 0,
                            TotJulP = 0,
                            TotJulF = 0,
                            TotAgoP = 0,
                            TotAgoF = 0,
                            TotSetP = 0,
                            TotSetF = 0,
                            TotOutP = 0,
                            TotOutF = 0,
                            TotNovP = 0,
                            TotNovF = 0,
                            TotDezP = 0,
                            TotDezF = 0,
                        });
                    }
                }

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (RelMapaFreqAlunoDescAnual at in res)
                    bsReport.Add(at);

                #endregion

                #endregion

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe MapaFreqAlunoDescAnual

        public class RelMapaFreqAlunoDescAnual
        {
            public RelMapaFreqAlunoDescAnual()
            {
                this.Disciplina = new List<Disciplina>();
            }

            public List<Disciplina> Disciplina { get; set; }

            public string coAno { get; set; }
            public int coModalidade { get; set; }
            public int coCur { get; set; }
            public int? idMatAgrup { get; set; }
            public int coMat { get; set; }

            public int ORD_IMP { get; set; }
            public string serie { get; set; }
            public string turma { get; set; }
            public string materia { get; set; }
            public int coMateria { get; set; }
            public string aluno { get; set; }
            public int coAluno { get; set; }
            public int NIREAluno { get; set; }
            public string MatriculaAluno { get; set; }
            public string SituacaoAluno { get; set; }
            public string TipoRelatorio { get; set; }

            public string DescMatricula
            {
                get
                {
                    if (this.TipoRelatorio == "S")
                    {
                        return "( Nº NIRE: " + this.NIREAluno.ToString().PadLeft(9, '0') + " - Nº Matrícula: " + this.MatriculaAluno + " - " + this.SituacaoAluno + " )";
                    }
                    else
                    {
                        return "( Série: " + this.serie + " - Turma: " + this.turma + " - Nº NIRE: " + this.NIREAluno.ToString().PadLeft(9, '0') + " - Nº Matrícula: " + this.MatriculaAluno + " - " + this.SituacaoAluno + " )";
                    }
                }
            }

            public string DescMateria
            {
                get
                {
                    System.Globalization.CultureInfo culInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
                    return culInfo.TextInfo.ToTitleCase(this.materia);
                }
            }

            public string DiscAgrupadora { get; set; }
            public int OrdImp_Valid
            {
                get
                {
                    if (this.idMatAgrup.HasValue)
                    {
                        int? ordIm = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                                      where tb43.CO_ANO_GRADE == this.coAno
                                      && tb43.TB44_MODULO.CO_MODU_CUR == this.coModalidade
                                      && tb43.CO_CUR == this.coCur
                                      && tb43.CO_MAT == this.idMatAgrup
                                      select new { tb43.CO_ORDEM_IMPRE }).FirstOrDefault().CO_ORDEM_IMPRE;

                        return ordIm ?? 40;
                    }
                    else
                    {
                        return this.ORD_IMP;
                    }
                }
            }
            public int OrdImpFilhas
            {
                get
                {
                    //Serve para ordenar as disciplinas filhas das agrupadores em ordem alfabética sem perder a ordem das demais
                    if (this.idMatAgrup.HasValue)
                    {
                        var res = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                                   join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                                   join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                   where tb43.TB44_MODULO.CO_MODU_CUR == this.coModalidade
                                   && tb43.CO_CUR == this.coCur
                                   && tb43.ID_MATER_AGRUP == this.idMatAgrup.Value
                                   && tb43.CO_ANO_GRADE == this.coAno
                                   select new
                                   {
                                       tb107.NO_MATERIA,
                                       tb43.CO_MAT,
                                   }).OrderBy(o => o.NO_MATERIA).ToList();

                        int posicao = 0;
                        foreach (var li in res)
                        {
                            posicao++;
                            if (li.CO_MAT == this.coMat)
                                break;
                        }

                        return posicao;
                    }
                    else
                    {
                        if (this.DiscAgrupadora == "S")
                            return 0;
                        else
                            return this.OrdImp_Valid;
                    }
                }
            }
        }
        #endregion

        #region Classe Disciplina

        public class Disciplina
        {
            public int TotJanP { get; set; }
            public int TotJanF { get; set; }
            public int TotFevP { get; set; }
            public int TotFevF { get; set; }
            public int TotMarP { get; set; }
            public int TotMarF { get; set; }
            public int TotAbrP { get; set; }
            public int TotAbrF { get; set; }
            public int TotMaiP { get; set; }
            public int TotMaiF { get; set; }
            public int TotJunP { get; set; }
            public int TotJunF { get; set; }
            public int TotJulP { get; set; }
            public int TotJulF { get; set; }
            public int TotAgoP { get; set; }
            public int TotAgoF { get; set; }
            public int TotSetP { get; set; }
            public int TotSetF { get; set; }
            public int TotOutP { get; set; }
            public int TotOutF { get; set; }
            public int TotNovP { get; set; }
            public int TotNovF { get; set; }
            public int TotDezP { get; set; }
            public int TotDezF { get; set; }
            public int TotalP { get; set; }
            public int TotalF { get; set; }
        }

        #endregion
    }
}
