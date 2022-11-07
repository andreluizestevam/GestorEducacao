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
    public partial class RptRendiEscolAlunoSupremo : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptRendiEscolAlunoSupremo()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              int coEmp,
                              string infos,
                              int strP_CO_EMP_REF,
                              string strP_CO_ANO_MES_MAT,
                              string strP_CO_BIMESTRE,
                              int strP_CO_ALU, string NomeFuncionalidadeCadastrada
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

                if (strP_CO_BIMESTRE != null)
                {

                    switch (strP_CO_BIMESTRE)
                    {
                        case "B1":
                            LblBimestre.Text = " 1º BIMESTRE";
                            break;
                        case "B2":
                            LblBimestre.Text = "2º BIMESTRE";
                            break;
                        case "B3":
                            LblBimestre.Text = "3º BIMESTRE";
                            break;
                        case "B4":
                            LblBimestre.Text = "4º BIMESTRE";
                            break;
                        default:
                            LblBimestre.Text = " - ";
                            break;
                    }
                }
                else
                {
                    LblBimestre.Text = " - ";
                }
                if (NomeFuncionalidadeCadastrada == "")
                {
                    lblTitulo.Text = "";
                }
                else
                {
                    lblTitulo.Text = NomeFuncionalidadeCadastrada.ToUpper();
                }
                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(coEmp);

                if (header == null)
                    return 0;

                string cpfCNPJUnid = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp).CO_CPFCGC_EMP;
                // Setar o header do relatorio
                if ((cpfCNPJUnid == "15280144000116") || (cpfCNPJUnid == "03946574000145"))
                    this.BaseInit(header, ETipoCabecalho.ColegioSupremo);
                else
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

                var lst = (from tb079 in ctx.TB079_HIST_ALUNO
                           join matr in ctx.TB08_MATRCUR on tb079.CO_ALU equals matr.CO_ALU
                           join tb43 in ctx.TB43_GRD_CURSO on tb079.CO_CUR equals tb43.CO_CUR
                           join alu in ctx.TB07_ALUNO on tb079.CO_ALU equals alu.CO_ALU
                           join ser in ctx.TB01_CURSO on tb079.CO_CUR equals ser.CO_CUR
                           join tur in ctx.TB06_TURMAS on tb079.CO_TUR equals tur.CO_TUR
                           join mat in ctx.TB02_MATERIA on tb079.CO_MAT equals mat.CO_MAT
                           join cadMat in ctx.TB107_CADMATERIAS on mat.ID_MATERIA equals cadMat.ID_MATERIA
                           join tb25 in ctx.TB25_EMPRESA on tb079.CO_EMP equals tb25.CO_EMP
                           where (tb079.CO_ANO_REF == strP_CO_ANO_MES_MAT) && tb079.CO_ANO_REF == matr.CO_ANO_MES_MAT
                           && tb079.CO_ANO_REF == tb43.CO_ANO_GRADE && tb079.CO_MAT == tb43.CO_MAT
                           && (strP_CO_ALU != 0 ? tb079.CO_ALU == strP_CO_ALU : 0 == 0)
                           && matr.CO_EMP == tb079.CO_EMP && tb43.CO_EMP == tb079.CO_EMP
                           && tb079.CO_EMP == strP_CO_EMP_REF && matr.CO_ALU_CAD == matriculaRefer
                           && tb43.ID_MATER_AGRUP == null
                               //Listar as disciplinas diferentes da classificação "Não se Aplica"
                           && cadMat.CO_CLASS_BOLETIM != 4
                           select new RendiEscolarAlunoSupremo
                           {
                               IdMateria = cadMat.ID_MATERIA,
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
                               ModalidadeCod = matr.TB44_MODULO.CO_MODU_CUR,

                               Serie = ser.NO_CUR.ToUpper(),
                               Turma = tur.TB129_CADTURMAS.NO_TURMA.ToUpper(),
                               Turno = tur.CO_PERI_TUR == "M" ? "MANHÃ" : tur.CO_PERI_TUR == "N" ? "NOITE" : "TARDE",
                               Disciplina = cadMat.NO_RED_MATERIA.ToUpper(),
                               CO_MAT = tb079.CO_MAT,
                               CO_MODU_CUR = tb079.CO_MODU_CUR,
                               CO_CUR = tb079.CO_CUR,
                               CO_TUR = tb079.CO_TUR,
                               MediaB1 = tb079.VL_NOTA_BIM1,
                               MediaB2 = tb079.VL_NOTA_BIM2,
                               MediaB3 = tb079.VL_NOTA_BIM3,
                               MediaB4 = tb079.VL_NOTA_BIM4,
                               FaltaB1 = tb079.QT_FALTA_BIM1,
                               FaltaB2 = tb079.QT_FALTA_BIM2,
                               FaltaB3 = tb079.QT_FALTA_BIM3,
                               FaltaB4 = tb079.QT_FALTA_BIM4,
                               AulaB1 = tb43.QT_AULAS_BIM1,
                               AulaB2 = tb43.QT_AULAS_BIM2,
                               AulaB3 = tb43.QT_AULAS_BIM3,
                               AulaB4 = tb43.QT_AULAS_BIM4,
                               MediaFinal = tb079.VL_MEDIA_FINAL,
                               ProvaFinal = tb079.VL_PROVA_FINAL,
                               SituMatr = matr.CO_SIT_MAT,
                               Resultado = matr.CO_STA_APROV != null ? (matr.CO_STA_APROV == "A" && (matr.CO_STA_APROV_FREQ == "A" || matr.CO_STA_APROV_FREQ == null) ? "APROVADO" : "REPROVADO") : "CURSANDO",
                               Av2 = "-",
                               Av3 = "-",
                               Av4 = "-",
                               Av5 = "-",

                           }
                  );

                var res = lst.Distinct().OrderBy(r => r.NomeAluno).ThenBy(o => o.ordImp).ThenBy(r => r.Disciplina).ToList();

                //Verifica as notas bimestrais para a turma em contexto






                #region Notas Atividades





                foreach (var item in res)
                {

                    #region Nota do Bimestre
                    var nota = (from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                                join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on tb079.CO_CUR equals tb43.CO_CUR
                                join alu in TB07_ALUNO.RetornaTodosRegistros() on tb079.CO_ALU equals alu.CO_ALU
                                where tb079.CO_MODU_CUR == item.ModalidadeCod && tb079.CO_CUR == item.CO_CUR && tb079.CO_TUR == item.CO_TUR
                                && (tb079.CO_ANO_REF == strP_CO_ANO_MES_MAT)
                                && tb079.CO_ANO_REF == tb43.CO_ANO_GRADE && tb079.CO_MAT == tb43.CO_MAT
                                && (item.coAlu != 0 ? tb079.CO_ALU == item.coAlu : 0 == 0)
                                && tb43.CO_EMP == tb079.CO_EMP
                                && tb079.CO_EMP == strP_CO_EMP_REF
                                && tb079.CO_MAT == item.CO_MAT
                                select new
                                {
                                    media = (strP_CO_BIMESTRE == "B1" ? tb079.VL_NOTA_BIM1 : strP_CO_BIMESTRE == "B2" ? tb079.VL_NOTA_BIM2 : strP_CO_BIMESTRE == "B3" ? tb079.VL_NOTA_BIM3 : tb079.VL_NOTA_BIM4),
                                    idagrup = tb43.ID_MATER_AGRUP,
                                }).FirstOrDefault();
                    //Atribui a nota verificando se é nulo e se é disciplina filha
                    if (nota != null)
                        item.MB = (nota.idagrup == null ? nota.media.HasValue ? nota.media.Value.ToString("N1") : "" : " - ");
                    else
                        item.MB = "-";

                    #endregion

                    int ano = int.Parse(item.Ano);
                    int idMat = item.IdMateria;
                    var result = (from tb49 in TB49_NOTA_ATIV_ALUNO.RetornaTodosRegistros()
                                  join tb273 in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros() on tb49.TB273_TIPO_ATIVIDADE.ID_TIPO_ATIV equals tb273.ID_TIPO_ATIV
                                  where tb49.TB07_ALUNO.CO_ALU == item.coAlu
                                  && tb49.TB01_CURSO.CO_CUR == item.CO_CUR
                                  && tb49.CO_ANO == ano
                                  && tb49.CO_BIMESTRE == strP_CO_BIMESTRE
                                  && tb49.TB107_CADMATERIAS.ID_MATERIA == idMat
                                  select new
                                  {
                                      tb273.CO_SIGLA_ATIV,
                                      tb49.VL_NOTA,
                                      tb49.CO_REFER_NOTA,

                                  }).ToList();
                    foreach (var l in result)
                    {


                        if (l.CO_REFER_NOTA == "AV1")
                        {
                            item.Av1 = "";
                            item.Av1 += "  " + l.VL_NOTA.ToString("N1");

                        }
                        if (l.CO_REFER_NOTA == "AV2")
                        {
                            item.Av2 = "";
                            item.Av2 += "  " + l.VL_NOTA.ToString("N1");


                        }
                        if (l.CO_REFER_NOTA == "AV3")
                        {
                            item.Av3 = "";
                            item.Av3 += " " + l.VL_NOTA.ToString("N1");

                        }
                        if (l.CO_REFER_NOTA == "AV4")
                        {
                            item.Av4 = "";
                            item.Av4 += "  " + l.VL_NOTA.ToString("N1");

                        }
                        if (l.CO_REFER_NOTA == "AV5")
                        {
                            item.Av5 = "";
                            item.Av5 += " " + l.VL_NOTA.ToString("N1");

                        }
                        
                        //item.MB = Convert.ToString( ((Convert.ToDecimal(item.Av1) + Convert.ToDecimal(item.Av2) + Convert.ToDecimal(item.Av3) + Convert.ToDecimal(item.Av4)+ Convert.ToDecimal(item.Av5))/3).ToString("N2"));
                    }



                #endregion
                }
                


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
        public class RendiEscolarAlunoSupremo
        {
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
            public int ModalidadeCod { get; set; }
            public string Serie { get; set; }
            public string Turma { get; set; }
            public string Turno { get; set; }
            public string Disciplina { get; set; }
            public decimal? MediaB1 { get; set; }
            public decimal? MediaB2 { get; set; }
            public decimal? MediaB3 { get; set; }
            public decimal? MediaB4 { get; set; }
            public decimal? MediaB5 { get; set; }
            public decimal? MediaFinal { get; set; }
            public decimal? ProvaFinal { get; set; }

            //Nota de Provas

            public string Av1 { get; set; }
            public string Av2 { get; set; }
            public string Av3 { get; set; }
            public string Av4 { get; set; }
            public string Av5 { get; set; }
            public int IdMateria { get; set; }
            public string MB { get; set; }


            public string Av1FINAL 
            {
                get
                {
                    return (this.Av1 != "" ? this.Av1 : " ");
                }
            }
            public string Av2FINAL 
                {
                get
                {
                    return (this.Av2 != "" ? this.Av1 : " ");
                }
            }
            public string Av3FINAL
            {
                get
                {
                    return (this.Av3 != "" ? this.Av1 : " ");
                }
            }
            public string Av4FINAL
            {
                get
                {
                    return (this.Av4 != "" ? this.Av1 : " ");
                }
            }
            public string Av5FINAL
            {
                get
                {
                    return (this.Av5 != "" ? this.Av1 : " ");
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


            public decimal? SinteseBimestre
            {
                get
                {
                    decimal sm = 0;
                    int cm = 0;
                    decimal? ms = 0;

                    decimal? mb1 = this.MediaB1 != null ? this.MediaB1 : null;
                    decimal? mb2 = this.MediaB2 != null ? this.MediaB2 : null;
                    decimal? mb3 = this.MediaB3 != null ? this.MediaB3 : null;
                    decimal? mb4 = this.MediaB4 != null ? this.MediaB4 : null;

                    if (mb1 != null)
                    {
                        sm = sm + mb1.Value;
                        cm++;
                    }

                    if (mb2 != null)
                    {
                        sm = sm + mb2.Value;
                        cm++;
                    }

                    if (mb3 != null)
                    {
                        sm = sm + mb3.Value;
                        cm++;
                    }

                    if (mb4 != null)
                    {
                        sm = sm + mb4.Value;
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
            public string DescSinteseBimestre
            {
                get
                {
                    decimal? d = this.SinteseBimestre != null ? Math.Round(this.SinteseBimestre.Value, 1) : (decimal?)null;
                    return d != null ? d.ToString() : "-";
                }
            }
            public int? FaltaB1 { get; set; }
            public int? FaltaB2 { get; set; }
            public int? FaltaB3 { get; set; }
            public int? FaltaB4 { get; set; }
            public int? AulaB1 { get; set; }
            public int? AulaB2 { get; set; }
            public int? AulaB3 { get; set; }
            public int? AulaB4 { get; set; }
            public string Resultado { get; set; }
            public string DescResultado { get; set; }

            public int totalPercFaltas { get; set; }
            public int QtdeFaltaTotal
            {
                get
                {
                    //=============================> Maneira que era calculada a quantidade de faltas anteriormente <=============================

                    //int? d = (this.FaltaB1 ?? 0) + (this.FaltaB2 ?? 0) + (this.FaltaB3 ?? 0) + (this.FaltaB4 ?? 0) != 0 ? (int?)(this.FaltaB1 ?? 0) + (this.FaltaB2 ?? 0) + (this.FaltaB3 ?? 0) + (this.FaltaB4 ?? 0) : null;
                    //return d != null ? d.Value.ToString() : "-";

                    int flt = (from tb132 in TB132_FREQ_ALU.RetornaTodosRegistros()
                               where tb132.TB01_CURSO.CO_CUR == this.CO_CUR
                               && tb132.TB01_CURSO.TB44_MODULO.CO_MODU_CUR == this.CO_MODU_CUR
                               && tb132.CO_TUR == this.CO_TUR
                               && tb132.TB07_ALUNO.CO_ALU == this.coAlu
                               && tb132.CO_MAT == this.CO_MAT
                               && tb132.FL_HOMOL_FREQU == "S"
                               && tb132.CO_FLAG_FREQ_ALUNO == "N"
                               && tb132.CO_ATIV_PROF_TUR != null
                               select tb132).Count();

                    return flt;
                }
            }

            public string PercFreq
            {
                get
                {
                    //=============================> Maneira que era calculada a quantidade de faltas anteriormente <=============================

                    //if (this.AulaB1 != null && this.AulaB2 != null && this.AulaB3 != null && this.AulaB4 != null)
                    //{
                    //    if (this.FaltaB1 == null && this.FaltaB2 == null && this.FaltaB3 == null && this.FaltaB4 == null)
                    //    {
                    //        return "-";
                    //    }
                    //    else
                    //    {
                    //        int qtdft = this.QtdeFaltaTotal != "-" ? int.Parse(this.QtdeFaltaTotal) : 0;
                    //        decimal? dcmValor = (decimal)(((this.AulaB1 + this.AulaB2 + this.AulaB3 + this.AulaB4) - qtdft) * 100) / (this.AulaB1 + this.AulaB2 + this.AulaB3 + this.AulaB4);
                    //        return (Math.Round(dcmValor.Value, 1)).ToString();
                    //    }
                    //}
                    //else
                    //{
                    //    return "-";
                    //}

                    //Conta em relação à quantidade de atividades lançadas, quantas em porcentagem o aluno levou falta
                    int flt = (from tb132 in TB132_FREQ_ALU.RetornaTodosRegistros()
                               where tb132.TB01_CURSO.CO_CUR == this.CO_CUR
                               && tb132.TB01_CURSO.TB44_MODULO.CO_MODU_CUR == this.CO_MODU_CUR
                               && tb132.CO_TUR == this.CO_TUR
                               && tb132.TB07_ALUNO.CO_ALU == this.coAlu
                               && tb132.CO_MAT == this.CO_MAT
                               && tb132.FL_HOMOL_FREQU == "S"
                               && tb132.CO_FLAG_FREQ_ALUNO == "N"
                               && tb132.CO_ATIV_PROF_TUR != null
                               select tb132).Count();

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
                    //Forma antiga
                    //int? d = (this.AulaB1 ?? 0) + (this.AulaB2 ?? 0) + (this.AulaB3 ?? 0) + (this.AulaB4 ?? 0) != 0 ? (int?)(this.AulaB1 ?? 0) + (this.AulaB2 ?? 0) + (this.AulaB3 ?? 0) + (this.AulaB4 ?? 0) : null;
                    //return d != null ? d.Value.ToString() : "-";

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
                                //return DateTime.Parse(dtResultado.ToString()).ToString("t");
                            }
                        }
                        final = final.Add(dtResultado);
                    }

                    //É mostrado apenas a quantidade de horas no final.
                    string fim = final.ToShortTimeString().Substring(0, 2).TrimStart('0');
                    return (!string.IsNullOrEmpty(fim) ? fim : " - ");
                }
            }
        }

        #endregion
    }
}
