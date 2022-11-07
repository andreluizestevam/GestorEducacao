using System;
using System.Linq;
using System.Drawing;
using C2BR.GestorEducacao.Reports.Helper;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Data.Objects;
using System.Text;

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3500_CtrlDesempenhoEscolar
{
    public partial class RptRendiEscolAluno : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptRendiEscolAluno()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              int coEmp,
                              string infos,
                              int strP_CO_EMP_REF,
                              string strP_CO_ANO_MES_MAT,
                              int strP_CO_ALU,
                              string nomeFunc
                              )
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;
                this.VisiblePageHeader = true;
                this.VisibleNumeroPage = false;
                this.VisibleDataHeader = false;
                this.VisibleHoraHeader = false;

                // Seta o nome do título
                if (nomeFunc != null) { lblTitulo.Text = nomeFunc.ToUpper(); } else { lblTitulo.Text = "SÍNTESE ANUAL DE DESEMPENHO DO ALUNO"; }

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(coEmp);

                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                var tb08 = (from iTb08 in ctx.TB08_MATRCUR
                            where iTb08.CO_ANO_MES_MAT == strP_CO_ANO_MES_MAT && iTb08.CO_ALU == strP_CO_ALU
                            select new
                            {
                                matricula = iTb08.CO_ALU_CAD
                            });

                string matriculaRefer = "";

                foreach (var item in tb08)
                {
                    matriculaRefer = item.matricula;
                }

                #region Query Rendimento Escolar Aluno

                //Verificação da referência utilizada pela empresa 
                var tipoR = "B";

                var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);

                tb25.TB83_PARAMETROReference.Load();
                if (tb25.TB83_PARAMETRO != null)
                    tipoR = tb25.TB83_PARAMETRO.TP_PERIOD_AVAL;

                var lst = (from tb079 in ctx.TB079_HIST_ALUNO
                           join matr in ctx.TB08_MATRCUR on tb079.CO_ALU equals matr.CO_ALU
                           join tb43 in ctx.TB43_GRD_CURSO on tb079.CO_CUR equals tb43.CO_CUR
                           join alu in ctx.TB07_ALUNO on tb079.CO_ALU equals alu.CO_ALU
                           join ser in ctx.TB01_CURSO on tb079.CO_CUR equals ser.CO_CUR
                           join tur in ctx.TB06_TURMAS on tb079.CO_TUR equals tur.CO_TUR
                           join mat in ctx.TB02_MATERIA on tb079.CO_MAT equals mat.CO_MAT
                           join cadMat in ctx.TB107_CADMATERIAS on mat.ID_MATERIA equals cadMat.ID_MATERIA
                           //join tb25 in ctx.TB25_EMPRESA on tb079.CO_EMP equals tb25.CO_EMP
                           where (tb079.CO_ANO_REF == strP_CO_ANO_MES_MAT) && tb079.CO_ANO_REF == matr.CO_ANO_MES_MAT
                           && tb079.CO_ANO_REF == tb43.CO_ANO_GRADE && tb079.CO_MAT == tb43.CO_MAT
                           && (strP_CO_ALU != 0 ? tb079.CO_ALU == strP_CO_ALU : 0 == 0)
                           && matr.CO_EMP == tb079.CO_EMP && tb43.CO_EMP == tb079.CO_EMP
                           && tb079.CO_EMP == strP_CO_EMP_REF && matr.CO_ALU_CAD == matriculaRefer
                           && tb43.ID_MATER_AGRUP == null
                               //Listar as disciplinas diferentes da classificação "Não se Aplica"
                           && cadMat.CO_CLASS_BOLETIM != 4
                           select new RendiEscolarAluno
                           {
                               CO_EMP = tb079.TB25_EMPRESA.CO_EMP,
                               ordImp = tb43.CO_ORDEM_IMPRE ?? 20,
                               coAlu = alu.CO_ALU,
                               NomeFanEmp = tb25.NO_FANTAS_EMP,
                               NomeResp = alu.TB108_RESPONSAVEL.NO_RESP,
                               NomeAluno = alu.NO_ALU.ToUpper(),
                               MatriculaAluno = matr.CO_ALU_CAD,
                               NomePai = alu.NO_PAI_ALU,
                               NomeMae = alu.NO_MAE_ALU,
                               NireAluno_R = alu.NU_NIRE,
                               Ano = tb079.CO_ANO_REF,
                               Modalidade = tb079.TB44_MODULO.DE_MODU_CUR.ToUpper(),
                               Serie = ser.NO_CUR.ToUpper(),
                               Turma = tur.TB129_CADTURMAS.NO_TURMA.ToUpper(),
                               Turno = tur.CO_PERI_TUR == "M" ? "MANHÃ" : tur.CO_PERI_TUR == "N" ? "NOITE" : "TARDE",
                               Disciplina = cadMat.NO_RED_MATERIA.ToUpper(),
                               CO_MAT = tb079.CO_MAT,
                               CO_MODU_CUR = tb079.CO_MODU_CUR,
                               CO_CUR = tb079.CO_CUR,
                               CO_TUR = tb079.CO_TUR,
                               MediaR1 = (tipoR == "T" ? tb079.VL_MEDIA_TRI1 : tb079.VL_MEDIA_BIM1),
                               MediaR2 = (tipoR == "T" ? tb079.VL_MEDIA_TRI2 : tb079.VL_MEDIA_BIM2),
                               MediaR3 = (tipoR == "T" ? tb079.VL_MEDIA_TRI3 : tb079.VL_MEDIA_BIM3),
                               MediaR4 = (tipoR == "T" ? null : tb079.VL_NOTA_BIM4),
                               FaltaR1 = (tipoR == "T" ? tb079.QT_FALTA_TRI1 : tb079.QT_FALTA_BIM1),
                               FaltaR2 = (tipoR == "T" ? tb079.QT_FALTA_TRI2 : tb079.QT_FALTA_BIM2),
                               FaltaR3 = (tipoR == "T" ? tb079.QT_FALTA_TRI3 : tb079.QT_FALTA_BIM3),
                               FaltaR4 = (tipoR == "T" ? null : tb079.QT_FALTA_BIM4),
                               AulaR1 = (tipoR == "T" ? tb43.QT_AULAS_TRI1 : tb43.QT_AULAS_BIM1),
                               AulaR2 = (tipoR == "T" ? tb43.QT_AULAS_TRI2 : tb43.QT_AULAS_BIM2),
                               AulaR3 = (tipoR == "T" ? tb43.QT_AULAS_TRI3 : tb43.QT_AULAS_BIM3),
                               AulaR4 = (tipoR == "T" ? null : tb43.QT_AULAS_BIM4),
                               MediaFinal = tb079.VL_MEDIA_FINAL,
                               ProvaFinal = tb079.VL_PROVA_FINAL,
                               SituMatr = matr.CO_SIT_MAT,
                               Resultado = matr.CO_STA_APROV != null ? (matr.CO_STA_APROV == "A" && (matr.CO_STA_APROV_FREQ == "A" || matr.CO_STA_APROV_FREQ == null) ? "APROVADO" : "REPROVADO") : "CURSANDO"
                           }
                  );

                var res = lst.Distinct().OrderBy(r => r.NomeAluno).ThenBy(o => o.ordImp).ThenBy(r => r.Disciplina).ToList();

                #endregion

                if (res.Count == 0)
                    return -1;
                // Seta os dados no DataSource do Relatorio
                bsReport.Clear();

                int auxCount = 0;
                int somFalt = 0;
                foreach (var at in res)
                {
                    //Contabiliza a média de faltas total do aluno
                    auxCount++;
                    somFalt += (at.PercFreq != " - " ? int.Parse(at.PercFreq) : 0);
                    if (auxCount == res.Count)
                        at.totalPercFaltas = (somFalt / auxCount);

                    bsReport.Add(at);
                }

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Rendimento Escolar
        public class RendiEscolarAluno
        {
            public int CO_EMP { get; set; }
            public int? ordImp { get; set; }
            public int coAlu { get; set; }
            public string NomeFanEmp { get; set; }
            public string NomeResp { get; set; }
            public string NomePai { get; set; }
            public string NomeMae { get; set; }
            public int NireAluno_R { get; set; }
            public string NireAluno
            {
                get
                {
                    return this.NireAluno_R.ToString().PadLeft(7, '0');
                }
            }
            public string MatriculaAluno { get; set; }
            public string NomeAluno { get; set; }
            public string SituMatr { get; set; }
            public string Ano { get; set; }
            public string Modalidade { get; set; }
            public string Serie { get; set; }
            public string Turma { get; set; }
            public string Turno { get; set; }
            public string Disciplina { get; set; }
            public decimal? MediaR1 { get; set; }
            public decimal? MediaR2 { get; set; }
            public decimal? MediaR3 { get; set; }
            public decimal? MediaR4 { get; set; }
            public decimal? MediaFinal { get; set; }
            public decimal? ProvaFinal { get; set; }

            public string tipoReferencia
            {
                get
                {
                    //Verificação da referência utilizada pela empresa 
                    var tipo = "B";

                    var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(this.CO_EMP);

                    tb25.TB83_PARAMETROReference.Load();
                    if (tb25.TB83_PARAMETRO != null)
                        tipo = tb25.TB83_PARAMETRO.TP_PERIOD_AVAL;

                    return tipo;
                }
            }

            public string MR1 { get { if (tipoReferencia == "T") { return "MT1"; } else { return "MB1"; } } }
            public string MR2 { get { if (tipoReferencia == "T") { return "MT2"; } else { return "MB2"; } } }
            public string MR3 { get { if (tipoReferencia == "T") { return "MT3"; } else { return "MB3"; } } }
            public string MR4 { get { if (tipoReferencia == "T") { return null; } else { return "MB4"; } } }

            public string rltMr1
            {
                get
                {
                    return (this.MediaR1.HasValue ? this.MediaR1.Value.ToString("n1") : " - ");

                }
            }
            public string rltMr2
            {
                get
                {
                    return (this.MediaR2.HasValue ? this.MediaR2.Value.ToString("n1") : " - ");
                }
            }
            public string rltMr3
            {
                get
                {
                    return (this.MediaR3.HasValue ? this.MediaR3.Value.ToString("n1") : " - ");
                }
            }
            public string rltMr4
            {
                get
                {
                    if (tipoReferencia == "T")
                        return null;
                    else
                        return (this.MediaR4.HasValue ? this.MediaR4.Value.ToString("n1") : " - ");
                }
            }
            public string SR
            {
                get
                {
                    if (tipoReferencia == "T") { return "ST"; } else { return "SB"; }
                }
            }

            public string MFIN
            {
                get
                {
                    return (this.MediaFinal.HasValue ? this.MediaFinal.Value.ToString("n1") : " - ");
                }
            }
            public string PRFIM
            {
                get
                {
                    return (this.ProvaFinal.HasValue ? this.ProvaFinal.Value.ToString("n1") : " - ");
                }
            }

            public decimal? SinteseReferencia
            {
                get
                {
                    decimal sm = 0;
                    int cm = 0;
                    decimal? ms = 0;

                    decimal? mr1 = this.MediaR1 != null ? this.MediaR1 : null;
                    decimal? mr2 = this.MediaR2 != null ? this.MediaR2 : null;
                    decimal? mr3 = this.MediaR3 != null ? this.MediaR3 : null;
                    decimal? mr4 = this.MediaR4 != null ? this.MediaR4 : null;

                    if (mr1 != null)
                    {
                        sm = sm + mr1.Value;
                        cm++;
                    }

                    if (mr2 != null)
                    {
                        sm = sm + mr2.Value;
                        cm++;
                    }

                    if (mr3 != null)
                    {
                        sm = sm + mr3.Value;
                        cm++;
                    }

                    if (mr4 != null)
                    {
                        sm = sm + mr4.Value;
                        cm++;
                    }

                    ms = cm != 0 ? (decimal?)sm / cm : null;
                    return ms != null ? Math.Round(ms.Value, 1) : (decimal?)null;
                }
            }

            public int CO_MAT { get; set; }
            public int CO_MODU_CUR { get; set; }
            public int CO_CUR { get; set; }
            public int CO_TUR { get; set; }
            public string QTA
            {
                get
                {
                    //Contabiliza quantas aulas houveram para aquela determinada disciplina e turma no ano em questão
                    int? coun = (from tb119 in TB119_ATIV_PROF_TURMA.RetornaTodosRegistros()
                                 where tb119.CO_ANO_MES_MAT == this.Ano
                                 && tb119.CO_MAT == this.CO_MAT
                                 && tb119.CO_MODU_CUR == this.CO_MODU_CUR
                                 && tb119.CO_CUR == this.CO_CUR
                                 && tb119.CO_TUR == this.CO_TUR
                                 && tb119.FL_HOMOL_ATIV == "S"
                                 select new { tb119.DT_ATIV_REAL }).Count();
                    return coun.HasValue ? coun.ToString() : " - ";
                }
            }
            public string DescSinteseReferencia
            {
                get
                {
                    decimal? d = this.SinteseReferencia != null ? Math.Round(this.SinteseReferencia.Value, 1) : (decimal?)null;
                    return d != null ? d.ToString() : "-";
                }
            }
            public int? FaltaR1 { get; set; }
            public int? FaltaR2 { get; set; }
            public int? FaltaR3 { get; set; }
            public int? FaltaR4 { get; set; }
            public int? AulaR1 { get; set; }
            public int? AulaR2 { get; set; }
            public int? AulaR3 { get; set; }
            public int? AulaR4 { get; set; }
            public string Resultado { get; set; }
            public string DescResultado { get; set; }

            public int totalPercFaltas { get; set; }
            public int QtdeFaltaTotal
            {
                get
                {
                    int flt = (from tb132 in TB132_FREQ_ALU.RetornaTodosRegistros()
                               where tb132.TB01_CURSO.CO_CUR == this.CO_CUR
                               && tb132.TB01_CURSO.TB44_MODULO.CO_MODU_CUR == this.CO_MODU_CUR
                               && tb132.CO_TUR == this.CO_TUR
                               && tb132.TB07_ALUNO.CO_ALU == this.coAlu
                               && tb132.CO_MAT == this.CO_MAT
                               && tb132.CO_FLAG_FREQ_ALUNO == "N"
                               && tb132.CO_ATIV_PROF_TUR != null
                               select tb132.CO_FLAG_FREQ_ALUNO).Count();
                    
                    return flt;
                }
            }

            public string PercFreq
            {
                get
                {
                    //Conta em relação à quantidade de atividades lançadas, quantas em porcentagem o aluno levou falta
                    int flt = (from tb132 in TB132_FREQ_ALU.RetornaTodosRegistros()
                               where tb132.TB01_CURSO.CO_CUR == this.CO_CUR
                               && tb132.TB01_CURSO.TB44_MODULO.CO_MODU_CUR == this.CO_MODU_CUR
                               && tb132.CO_TUR == this.CO_TUR
                               && tb132.TB07_ALUNO.CO_ALU == this.coAlu
                               && tb132.CO_MAT == this.CO_MAT
                               && tb132.CO_FLAG_FREQ_ALUNO == "N"
                               && tb132.CO_ATIV_PROF_TUR != null
                               select tb132.CO_FLAG_FREQ_ALUNO).Count();

                    int lo = flt * 100;
                    int qta = (this.QTA != " - " ? int.Parse(this.QTA) : 0);

                    if (qta != 0)
                    {
                        int tot = lo / qta;
                        return tot.ToString();
                    }
                    else
                        return " - ";
                }
            }

            public string QtdeAulaTotal
            {
                get
                {
                    //Contabiliza quantas horas/aula houveram para aquela determinada disciplina e turma no ano em questão
                    var coun = (from tb119 in TB119_ATIV_PROF_TURMA.RetornaTodosRegistros()
                                where tb119.CO_ANO_MES_MAT == this.Ano
                                && tb119.CO_MAT == this.CO_MAT
                                && tb119.CO_MODU_CUR == this.CO_MODU_CUR
                                && tb119.CO_CUR == this.CO_CUR
                                && tb119.CO_TUR == this.CO_TUR
                                && tb119.FL_HOMOL_ATIV == "S"
                                select new { tb119.HR_INI_ATIV, tb119.HR_TER_ATIV }).ToList();

                    DateTime final = new DateTime(1999, 1, 1);
                    TimeSpan dtResultado = new TimeSpan(0, 0, 0);
                    //Loop para calcular o intervalo de tempo início/fim de cada atividade e somar o valor à uma variável;
                    foreach (var li in coun)
                    {
                        DateTime dtInicio, dtTermino;
                        DateTime.TryParse(li.HR_INI_ATIV, out dtInicio);
                        DateTime.TryParse((li.HR_TER_ATIV), out dtTermino);

                        if (dtInicio != null && dtTermino != null && dtInicio != DateTime.MinValue && dtTermino != DateTime.MinValue)
                        {
                            if (dtTermino > dtInicio)
                            {
                                dtResultado = dtTermino - dtInicio;
                            }
                        }
                        final = final.Add(dtResultado);
                    }

                    //É mostrado apenas a quantidade de horas no final.
                    string fim = final.ToShortTimeString().Substring(0, 2).TrimStart('0');
                    return (!string.IsNullOrEmpty(fim) ? fim : " - ");
                }
            }

            public string rltLegenMr1 { get { if (tipoReferencia == "T") { return "MT1 - Média do 1º Trimestre"; } else { return "MB1 - Média do 1º Bimestre"; } } }
            public string rltLegenMr2 { get { if (tipoReferencia == "T") { return "MT2 - Média do 2º Trimestre"; } else { return "MB2 - Média do 2º Bimestre"; } } }
            public string rltLegenMr3 { get { if (tipoReferencia == "T") { return "MT3 - Média do 3º Trimestre"; } else { return "MB3 - Média do 3º Bimestre"; } } }
            public string rltLegenMr4 { get { if (tipoReferencia == "T") { return null; } else { return "MB4 - Média do 4º Bimestre"; } } }
            public string rltLegenSr { get { if (tipoReferencia == "T") { return "ST - Síntese dos Trimestres"; } else { return "SB - Síntese dos Bimestres"; } } }
        }

        #endregion
    }
}
