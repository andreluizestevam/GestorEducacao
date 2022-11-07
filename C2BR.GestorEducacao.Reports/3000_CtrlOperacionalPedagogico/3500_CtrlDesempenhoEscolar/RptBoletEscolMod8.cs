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
    public partial class RptBoletEscolMod8 : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptBoletEscolMod8()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              int coEmp,
                              string infos,
                              int strP_CO_EMP_REF,
                              string strP_CO_ANO_MES_MAT,
                              int strP_CO_MODU_CUR,
                              int strP_CO_CUR,
                              int strP_CO_TUR,
                              int strP_CO_ALU,
                              string strP_OBS,
                              Boolean boolP_CO_TOT,
                              Boolean boolP_CO_IMG,
                              string strP_TITULO,
                              bool mostraAvaliacao,
                              bool mostraTotFal,
                              int totFal,
                              bool chk1Bim,
                              bool chk2Bim,
                              bool chk3Bim,
                              bool chk4Bim,
                              bool mostraAnalFreq,
                              string TipoNomeDisc
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

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(coEmp);

                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                //var tb83 = (from iTb83 in ctx.TB83_PARAMETRO
                //            where iTb83.CO_EMP == strP_CO_EMP_REF
                //            select new
                //            {
                //                nomeSecretario = iTb83.TB03_COLABOR != null ? iTb83.TB03_COLABOR.NO_COL : "",
                //                matriculaSecretario = iTb83.TB03_COLABOR != null ? iTb83.TB03_COLABOR.CO_MAT_COL : ""
                //            }).FirstOrDefault();

                //if (tb83 != null)
                //{
                //    lblSecretario.Text = (tb83.matriculaSecretario != "" ? tb83.matriculaSecretario : "XXXXX") + " - " + tb83.nomeSecretario;
                //}

                string strAprov;

                var resResul = (from serie in ctx.TB01_CURSO
                                join proxSerie in ctx.TB01_CURSO on serie.CO_PREDEC_CUR equals proxSerie.CO_CUR
                                where serie.CO_CUR == strP_CO_CUR
                                select new { proxSerie.NO_CUR, proxSerie.TB44_MODULO.DE_MODU_CUR }).FirstOrDefault();

                if (resResul != null)
                    strAprov = "O(a) Aluno(a) foi promovido(a) para o " + resResul.NO_CUR + " do " + resResul.DE_MODU_CUR + ".";
                else
                    strAprov = "O(a) Aluno(a) foi promovido(a) para o XXXXX do XXXXX.";

                //if (strP_OBS == "")
                //{
                //    lblObs.Visible = false;
                //}
                //else
                //{
                //    lblObs.Text = strP_OBS;
                //}

                if (strP_TITULO != "")
                {
                    lblTitulo.Text = strP_TITULO;
                    lblTitProtocEntreg.Text = strP_TITULO.ToUpper();
                }

                //if (mostraAvaliacao)
                //{
                    //var resa = (from tb126 in TB126_HIST_ALUNO_AVAL.RetornaTodosRegistros()
                    //            where tb126.CO_MODU_CUR == strP_CO_MODU_CUR
                    //            && tb126.CO_CUR == strP_CO_CUR
                    //            && tb126.CO_TUR == strP_CO_TUR
                    //            && tb126.CO_ALU == strP_CO_ALU
                    //            && tb126.CO_ANO_REF == strP_CO_ANO_MES_MAT
                    //            select new AvalGeral
                    //            {
                    //                aval = tb126.DE_AVAL
                    //            });

                    //string avalGeral = "";
                    //foreach (AvalGeral a in resa)
                    //{
                    //    if (avalGeral != "")
                    //    {
                    //        avalGeral += " " + a.aval;
                    //    }
                    //    else
                    //    {
                    //        avalGeral = a.aval;
                    //    }
                    //}

                //    //lblObs.Text = avalGeral;
                //    lblObsTit.Text = "Avaliação do Aluno";
                //}
                //else
                //{
                //    lblObs.Text = strP_OBS;
                //    lblObsTit.Text = "Observação";
                //}
                #region Query Boletim Aluno

                var lst = (from tb079 in ctx.TB079_HIST_ALUNO
                           join matr in ctx.TB08_MATRCUR on tb079.CO_ALU equals matr.CO_ALU
                           join tb43 in ctx.TB43_GRD_CURSO on tb079.CO_CUR equals tb43.CO_CUR
                           join alu in ctx.TB07_ALUNO on tb079.CO_ALU equals alu.CO_ALU
                           join ser in ctx.TB01_CURSO on tb079.CO_CUR equals ser.CO_CUR
                           join tur in ctx.TB06_TURMAS on tb079.CO_TUR equals tur.CO_TUR
                           join mat in ctx.TB02_MATERIA on tb079.CO_MAT equals mat.CO_MAT
                           join cadMat in ctx.TB107_CADMATERIAS on mat.ID_MATERIA equals cadMat.ID_MATERIA
                           join tb25 in ctx.TB25_EMPRESA on tb079.CO_EMP equals tb25.CO_EMP
                           //join tb126 in ctx.TB126_HIST_ALUNO_AVAL on alu.CO_ALU equals tb126.CO_ALU
                           where tb079.CO_MODU_CUR == strP_CO_MODU_CUR && tb079.CO_CUR == strP_CO_CUR && tb079.CO_TUR == strP_CO_TUR
                           && (tb079.CO_ANO_REF == strP_CO_ANO_MES_MAT) && tb079.CO_ANO_REF == matr.CO_ANO_MES_MAT
                           && tb079.CO_ANO_REF == tb43.CO_ANO_GRADE && tb079.CO_MAT == tb43.CO_MAT
                           && (strP_CO_ALU != 0 ? tb079.CO_ALU == strP_CO_ALU : 0 == 0)
                           && matr.CO_EMP == tb079.CO_EMP && tb43.CO_EMP == tb079.CO_EMP
                           && tb079.CO_EMP == strP_CO_EMP_REF
                           && tb43.ID_MATER_AGRUP == null
                               //Listar as disciplinas diferentes da classificação "Não se Aplica"
                           && cadMat.CO_CLASS_BOLETIM != 4
                           select new BoletimAlunoModelo8
                           {
                               coAlu = alu.CO_ALU,
                               CodigoMateria = cadMat.ID_MATERIA,
                               CodigoEmp = matr.CO_EMP,
                               CodigoTurma = tur.CO_TUR,
                               CodigoCur = tb079.CO_CUR,
                               NomeFanEmp = tb25.NO_FANTAS_EMP,
                               NomeResp = alu.TB108_RESPONSAVEL.NO_RESP,
                               NomeAluno = alu.NO_ALU.ToUpper(),
                               NireAluno = alu.NU_NIRE,
                               dtNasc = alu.DT_NASC_ALU.Value,
                               CodigoAluno = alu.NU_NIRE,
                               Sexo = alu.CO_SEXO_ALU == "M" ? "MASCULINO" : "FEMININO",
                               Ano = tb079.CO_ANO_REF,
                               Modalidade = tb079.TB44_MODULO.DE_MODU_CUR.ToUpper(),
                               Serie = ser.NO_CUR.ToUpper(),
                               Turma = tur.TB129_CADTURMAS.NO_TURMA.ToUpper(),
                               Turno = tur.CO_PERI_TUR == "M" ? "MANHÃ" : tur.CO_PERI_TUR == "N" ? "NOITE" : "TARDE",
                               Turno_ = tur.CO_PERI_TUR == "M" ? "MATUTINO" : tur.CO_PERI_TUR == "N" ? "NOTURNO" : "VESPERTINO",
                               Disciplina = cadMat.NO_RED_MATERIA.ToUpper(),
                               DiscNome = cadMat.NO_MATERIA.ToUpper(),
                               tipoNome = TipoNomeDisc,
                               MediaB1 = tb079.VL_MEDIA_BIM1,
                               MediaB2 = tb079.VL_MEDIA_BIM2,
                               MediaB3 = tb079.VL_MEDIA_BIM3,
                               MediaB4 = tb079.VL_MEDIA_BIM4,
                               MediaS1 = tb079.VL_NOTA_SEM1,
                               MediaS2 = tb079.VL_NOTA_SEM2,
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
                               RecSemestre1 = tb079.VL_RECU_SEM1,
                               RecSemestre2 = tb079.VL_RECU_SEM2,
                               Resultado = matr.CO_STA_APROV != null ? (matr.CO_STA_APROV == "A" && (matr.CO_STA_APROV_FREQ == "A" || matr.CO_STA_APROV_FREQ == null) ? "APROVADO" : "REPROVADO") : "CURSANDO",
                               DescResultado = matr.CO_STA_APROV != null ? (matr.CO_STA_APROV == "A" && (matr.CO_STA_APROV_FREQ == "A" || matr.CO_STA_APROV_FREQ == null) ? strAprov :
                               "O(a) Aluno(a) foi reprovado(a) e continuará no " + ser.NO_CUR + " do " + tb079.TB44_MODULO.DE_MODU_CUR + ".") : "",
                               totFal = totFal,
                               mostraAval = mostraAvaliacao,
                               Obs = strP_OBS,
                               coAnoMesMat = tb079.CO_ANO_REF,
                               coCur = matr.CO_CUR,
                               coModuCur = matr.TB44_MODULO.CO_MODU_CUR,
                               coTur = matr.CO_TUR,
                               //Mostra a Análise de Frequência apenas se isso tiver sido selecionado no parâmetro
                               mostraAnalFreq = mostraAnalFreq,
                               ordImp = tb43.CO_ORDEM_IMPRE ?? 20,
                               MedMinim = ser.MED_FINAL_CUR
                           }
                  );

                var res = lst.Distinct().OrderBy(r => r.NomeAluno).ThenBy(w => w.ordImp).ThenBy(r => r.Disciplina).ToList();

                foreach (var r in res)
                {
                    //int idTpAtiv = (from t in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros() where t.CO_SIGLA_ATIV == "RECSE" select new { t.ID_TIPO_ATIV }).FirstOrDefault().ID_TIPO_ATIV;
                    //decimal? v = null;

                    //if ((from a in TB49_NOTA_ATIV_ALUNO.RetornaTodosRegistros() where a.TB01_CURSO.CO_CUR == r.CodigoCur && a.TB06_TURMAS.CO_TUR == r.CodigoTurma && a.TB107_CADMATERIAS.ID_MATERIA == r.CodigoMateria && a.TB273_TIPO_ATIVIDADE.ID_TIPO_ATIV == idTpAtiv && a.TB07_ALUNO.CO_ALU == r.coAlu select new { a.VL_NOTA }).Any())
                    //{
                    //    v = (from a in TB49_NOTA_ATIV_ALUNO.RetornaTodosRegistros()
                    //         where a.TB01_CURSO.CO_CUR == r.CodigoCur
                    //         && a.TB06_TURMAS.CO_TUR == r.CodigoTurma
                    //         && a.TB107_CADMATERIAS.ID_MATERIA == r.CodigoMateria
                    //         && a.TB273_TIPO_ATIVIDADE.ID_TIPO_ATIV == idTpAtiv
                    //         && a.TB07_ALUNO.CO_ALU == r.coAlu
                    //         select new
                    //         {
                    //             a.VL_NOTA
                    //         }).FirstOrDefault().VL_NOTA;
                    //}

                    //if (v != null)
                    //{
                    //    r.RecSemestre1 = v != null ? v.Value : (decimal?)null;
                    //}

                    TB07_ALUNO alu = ctx.TB07_ALUNO.Where(a => a.CO_ALU == r.coAlu).FirstOrDefault();
                    alu.ImageReference.Load();

                    if (alu.Image != null)
                        r.foto = alu.Image.ImageStream;
                }

                #endregion

                if (res.Count == 0)
                    return -1;
                // Seta os dados no DataSource do Relatorio
                bsReport.Clear();

                foreach (var at in res)
                {
                    //Coleta as informações das Avaliações do Aluno em questão.
                    if (at.mostraAval)
                    {
                        //var resa = (from tb126 in TB126_HIST_ALUNO_AVAL.RetornaTodosRegistros()
                        //            where tb126.CO_MODU_CUR == at.coModuCur
                        //            && tb126.CO_CUR == at.coCur
                        //            && tb126.CO_TUR == at.coTur.Value
                        //            && tb126.CO_ALU == at.coAlu
                        //            && tb126.CO_ANO_REF == at.coAnoMesMat
                        //            select new AvalGeral
                        //            {
                        //                aval = tb126.DE_AVAL
                        //            });

                        //string avalGeral = "";
                        //foreach (AvalGeral a in resa)
                        //{
                        //    if (avalGeral != "")
                        //        avalGeral += " " + a.aval;
                        //    else                          
                        //        avalGeral = a.aval;
                        //}

                        //at.AvalAlunoOb = avalGeral;
                        lblObsTit.Text = "Avaliação do Aluno";

                        //Trata o que o usuário selecionou, para mostrar ou não as avaliações dos bimestres de acordo com escolha 
                        if (chk1Bim == true)
                        {
                            var aval1 = TB126_HIST_ALUNO_AVAL.RetornaTodosRegistros().Where(w => w.CO_ALU == at.coAlu && w.CO_MODU_CUR == at.coModuCur && w.CO_CUR == at.coCur && w.CO_TUR == at.coTur && w.CO_ANO_REF == at.coAnoMesMat && w.CO_BIMESTRE == "B1").FirstOrDefault();
                            if (aval1 != null)
                            {
                                if (aval1.DE_AVAL != null)
                                    this.tbAval1B.Visible = true; at.AvalBim1 = "1º Bimestre - " + aval1.DE_AVAL;
                            }
                        }
                        if (chk2Bim == true)
                        {
                            var aval2 = TB126_HIST_ALUNO_AVAL.RetornaTodosRegistros().Where(w => w.CO_ALU == at.coAlu && w.CO_MODU_CUR == at.coModuCur && w.CO_CUR == at.coCur && w.CO_TUR == at.coTur && w.CO_ANO_REF == at.coAnoMesMat && w.CO_BIMESTRE == "B2").FirstOrDefault();
                            if (aval2 != null)
                            {
                                if (aval2.DE_AVAL != null)
                                    this.tbAval2B.Visible = true; at.AvalBim2 = "2º Bimestre - " + aval2.DE_AVAL;
                            }
                        }
                        if (chk3Bim == true)
                        {
                            var aval3 = TB126_HIST_ALUNO_AVAL.RetornaTodosRegistros().Where(w => w.CO_ALU == at.coAlu && w.CO_MODU_CUR == at.coModuCur && w.CO_CUR == at.coCur && w.CO_TUR == at.coTur && w.CO_ANO_REF == at.coAnoMesMat && w.CO_BIMESTRE == "B3").FirstOrDefault();
                            if (aval3 != null)
                            {
                                if (aval3.DE_AVAL != null)
                                    this.tbAval3B.Visible = true; at.AvalBim3 = "3º Bimestre - " + aval3.DE_AVAL;
                            }
                        }
                        if (chk4Bim == true)
                        {
                            var aval4 = TB126_HIST_ALUNO_AVAL.RetornaTodosRegistros().Where(w => w.CO_ALU == at.coAlu && w.CO_MODU_CUR == at.coModuCur && w.CO_CUR == at.coCur && w.CO_TUR == at.coTur && w.CO_ANO_REF == at.coAnoMesMat && w.CO_BIMESTRE == "B4").FirstOrDefault();
                            if (aval4 != null)
                            {
                                if (aval4.DE_AVAL != null)
                                    this.tbAval4B.Visible = true; at.AvalBim4 = "4º Bimestre - " + aval4.DE_AVAL;
                            }
                        }
                    }
                    else
                    {
                        this.tbAval1B.Visible = true;
                        at.AvalBim1 = at.Obs;
                        lblObsTit.Text = "Observação";
                    }

                    lblMedMin.Text = at.lblMedMin;

                    bsReport.Add(at);
                }

                xrTable4.Visible = boolP_CO_TOT;

                if (boolP_CO_IMG)
                {
                    xrPictureBox1.Visible = boolP_CO_IMG;
                }
                else
                {
                    xrPictureBox1.Visible = boolP_CO_IMG;
                    this.GroupHeader1.HeightF = 247;
                }

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Boletim Aluno
        public class AvalGeral
        {
            public string aval { get; set; }
        }

        public class BoletimAlunoModelo8
        {
            //Atributos para alimentar as informações de Avaliação do Aluno.
            public int coCur { get; set ;}
            public int? coTur { get; set ;}
            //public int coAlu { get; set; }
            public string coAnoMesMat { get; set ;}
            public int coModuCur { get; set; }
            public string AvalAlunoOb { get; set; }

            public int? ordImp { get; set; }
            public string AvalBim1 { get; set; }
            public string AvalBim2 { get; set; }
            public string AvalBim3 { get; set; }
            public string AvalBim4 { get; set; }

            public string Obs { get; set; }
            public bool mostraAval { get; set; }
            public int coAlu { get; set; }
            public int CodigoMateria { get; set; }
            public int CodigoCur { get; set; }
            public int CodigoEmp { get; set; }
            public int CodigoTurma { get; set; }
            public int CodigoCol { get; set; }
            public string NomeFanEmp { get; set; }
            public string NomeResp { get; set; }
            public int CodigoAluno { get; set; }
            public int NireAluno { get; set; }
            public DateTime dtNasc { get; set; }
            public string dtNascimento
            {
                get
                {
                    return dtNasc.ToString("dd/MM/yyyy");
                }
            }
            public string NomeAluno { get; set; }
            public string Sexo { get; set; }
            public string Ano { get; set; }
            public string Modalidade { get; set; }
            public string Serie { get; set; }
            public string Turma { get; set; }
            public string Turno { get; set; }
            public string Turno_ { get; set; }
            public string idade
            {

                get
                {
                    string i = "-";

                    if (this.dtNasc.Year > 1923){
                        i = (DateTime.Now.Year - this.dtNasc.Year).ToString();
                    }

                    return i;
                }
            }
            public string deInfoTurma
            {
                get
                {
                    return "Ano Letivo: " + Ano + " - " + Modalidade + " - " + Serie + " - " + Turma + " - " + Turno;
                }
            }
            public string deInfoTurma2
            {
                get
                {
                    return "Data Nascto:" + this.dtNascimento + " (" + this.idade + ")" + " - Sexo: " + this.Sexo + " - Responsável: " + this.NomeResp;
                }
            }
            public string DiscNome { get; set; }
            public string tipoNome { get; set; }
            public string Disciplina { get; set; }
            public string Disciplina_V
            {
                get
                {
                    if (this.tipoNome == "C")
                        return (this.DiscNome.Length > 30 ? this.DiscNome.Substring(0, 30) + "..." : this.DiscNome);
                    else
                        return this.Disciplina;
                }
            }
            public decimal? MediaB1 { get; set; }
            public decimal? MediaB2 { get; set; }
            public decimal? MediaB3 { get; set; }
            public decimal? MediaB4 { get; set; }
            public decimal? MediaFinal { get; set; }
            public decimal? ProvaFinal { get; set; }
            public string MB1
            {
                get
                {
                    return this.MediaB1.HasValue ? (this.MediaB1.Value != 10 ? this.MediaB1.Value.ToString("N1") : "10") : "-";
                }
            }
            public string MB2
            {
                get
                {
                    return this.MediaB2.HasValue ? (this.MediaB2.Value != 10 ? this.MediaB2.Value.ToString("N1") : "10") : "-";
                }
            }
            public string MB3
            {
                get
                {
                    return this.MediaB3.HasValue ? (this.MediaB3.Value != 10 ? this.MediaB3.Value.ToString("N1") : "10") : "-";
                }
            }
            public string MB4
            {
                get
                {
                    return this.MediaB4.HasValue ? (this.MediaB4.Value != 10 ? this.MediaB4.Value.ToString("N1") : "10") : "-";
                }
            }
            public string MF
            {
                get
                {
                    return this.MediaFinal.HasValue ? (this.MediaFinal.Value != 10 ? this.MediaFinal.Value.ToString("N1") : "10") : "-";
                }
            }
            public decimal? MediaS1 { get; set; }
            public decimal? MediaS2 { get; set; }
            public int? FaltaB1 { get; set; }
            public int? FaltaB2 { get; set; }
            public int? FaltaB3 { get; set; }
            public int? FaltaB4 { get; set; }
            public string FB1 
            {
                get
                {
                    return this.FaltaB1 != null ? this.FaltaB1.Value.ToString() : "-";
                }
            }
            public string FB2
            {
                get
                {
                    return this.FaltaB2 != null ? this.FaltaB2.Value.ToString() : "-";
                }
            }
            public string FB3
            {
                get
                {
                    return this.FaltaB3 != null ? this.FaltaB3.Value.ToString() : "-";
                }
            }
            public string FB4
            {
                get
                {
                    return this.FaltaB4 != null ? this.FaltaB4.Value.ToString() : "-";
                }
            }
            public int? AulaB1 { get; set; }
            public int? AulaB2 { get; set; }
            public int? AulaB3 { get; set; }
            public int? AulaB4 { get; set; }
            public string AB1
            {
                get
                {
                    return this.AulaB1 != null ? this.AulaB1.Value.ToString() : "-";
                }
            }
            public string AB2
            {
                get
                {
                    return this.AulaB2 != null ? this.AulaB2.Value.ToString() : "-";
                }
            }
            public string AB3
            {
                get
                {
                    return this.AulaB3 != null ? this.AulaB3.Value.ToString() : "-";
                }
            }
            public string AB4
            {
                get
                {
                    return this.AulaB4 != null ? this.AulaB4.Value.ToString() : "-";
                }
            }
            public string Resultado { get; set; }
            public string DescResultado { get; set; }

            public int totFal { get; set; }

            public string strTotFal
            {
                get
                {
                    return this.totFal != 0 ? this.totFal.ToString() : "-";
                }
            }

            public byte[] foto { get; set; }

            public string Declaro
            {
                get
                {
                    return "Declaro ter recebido nesta data  ___/___/_____  a ficha de rendimento escolar do aluno " + this.NomeAluno + ", Registro nr. " + this.NireAluno + " matriculado na modalidade " + this.Modalidade + ", série " + this.Serie + " e turno " + this.Turno_ + " - Responsável do aluno: " + this.NomeResp + ".";
                }
            }

            public string AlunoDesc
            {
                get
                {
                    return this.NireAluno.ToString().PadLeft(7, '0')  + " - " + this.NomeAluno;
                }
            }

            public decimal? MediaSemestre1
            {
                get
                {
                    if (!MediaS1.HasValue)
                    {
                        decimal sm = 0;
                        int cm = 0;
                        decimal? ms = 0;

                        decimal? mb1 = this.MediaB1 != null ? this.MediaB1 : null;
                        decimal? mb2 = this.MediaB2 != null ? this.MediaB2 : null;

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

                        ms = cm != 0 ? (decimal?)sm / cm : null;
                        return ms;// != null ? (ms.Value != 10 ? Math.Round(ms.Value, 1) : 10) : (decimal?)null;
                    }
                    else
                    {
                        decimal ms = 0;

                        if (RecSemestre1.HasValue)
                        {
                            if (MediaB1.HasValue && MediaB2.HasValue)
                            {
                                if (RecSemestre1.Value > MediaB1.Value || RecSemestre1.Value > MediaB2.Value)
                                {
                                    if (RecSemestre1.Value > MediaB1.Value && RecSemestre1.Value > MediaB2.Value)
                                        ms = RecSemestre1.Value;
                                    else if (MediaB1.Value > MediaB2.Value)
                                        ms = (MediaB1.Value + RecSemestre1.Value) / 2;
                                    else
                                        ms = (MediaB2.Value + RecSemestre1.Value) / 2;
                                }
                                else
                                    ms = (MediaB1.Value + MediaB2.Value) / 2;
                            }
                            else if (MediaB1.HasValue)
                                ms = (MediaB1.Value + RecSemestre1.Value) / 2;
                            else
                                ms = (MediaB2.Value + RecSemestre1.Value) / 2;
                        }
                        else
                            ms = MediaS1.Value;

                        return ms;// != 10 ? Math.Round(ms, 1) : 10;
                    }
                }
            }

            public string MS1
            {
                get
                {
                    return this.MediaSemestre1.HasValue ? (this.MediaSemestre1.Value != 10 ? this.MediaSemestre1.Value.ToString("N1") : "10") : "-";
                }
            }

            public decimal? MediaSemestre2
            {
                get
                {
                    if (!MediaS2.HasValue)
                    {
                        decimal sm = 0;
                        int cm = 0;
                        decimal? ms = 0;

                        decimal? mb3 = this.MediaB3 != null ? this.MediaB3 : null;
                        decimal? mb4 = this.MediaB4 != null ? this.MediaB4 : null;

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
                        return ms;// != null ? (ms.Value != 10 ? Math.Round(ms.Value, 1) : 10) : (decimal?)null;
                    }
                    else
                    {
                        decimal ms = 0;

                        if (RecSemestre2.HasValue)
                        {
                            if (MediaB3.HasValue && MediaB4.HasValue)
                            {
                                if (RecSemestre2.Value > MediaB3.Value || RecSemestre2.Value > MediaB4.Value)
                                {
                                    if (RecSemestre2.Value > MediaB3.Value && RecSemestre2.Value > MediaB4.Value)
                                        ms = RecSemestre2.Value;
                                    else if (MediaB3.Value > MediaB4.Value)
                                        ms = (MediaB3.Value + RecSemestre2.Value) / 2;
                                    else
                                        ms = (MediaB4.Value + RecSemestre2.Value) / 2;
                                }
                                else
                                    ms = (MediaB3.Value + MediaB4.Value) / 2;
                            }
                            else if (MediaB3.HasValue)
                                ms = (MediaB3.Value + RecSemestre2.Value) / 2;
                            else
                                ms = (MediaB4.Value + RecSemestre2.Value) / 2;
                        }
                        else
                            ms = MediaS2.Value;

                        return ms;// != 10 ? Math.Round(ms, 1) : 10;
                    }
                }
            }

            public string MS2
            {
                get
                {
                    return this.MediaSemestre2.HasValue ? (this.MediaSemestre2.Value != 10 ? this.MediaSemestre2.Value.ToString("N1") : "10") : "-";
                }
            }

            public decimal? RecSemestre1 { get; set; }
            public decimal? RecSemestre2 { get; set; }

            public string RS1
            {
                get
                {
                    return this.RecSemestre1.HasValue ? (this.RecSemestre1.Value != 10 ? this.RecSemestre1.Value.ToString("N1") : "10") : "-";
                }
            }

            public string PRecSemestre1
            {
                get
                {
                    /*decimal? d = null;
                    if (this.RecSemestre1 != null)
                    {
                        if (this.MediaSemestre1 != null)
                        {
                            d = this.RecSemestre1.Value > this.MediaSemestre1.Value ? (this.RecSemestre1.Value != 10 ? Math.Round(this.RecSemestre1.Value, 1) : 10) : (this.MediaSemestre1.Value != 10 ? Math.Round(this.MediaSemestre1.Value, 1) : 10);
                        }
                        else
                        {
                            d = (this.RecSemestre1.Value != 10 ? Math.Round(this.RecSemestre1.Value, 1) : 10);
                        }
                    }
                    return d != null ? d.Value.ToString() : "-";*/
                    return "-";
                }
            }

            public string RS2
            {
                get
                {
                    return this.RecSemestre2.HasValue ? (this.RecSemestre2.Value != 10 ? this.RecSemestre2.Value.ToString("N1") : "10") : "-";
                }
            }

            public string PRecSemestre2
            {
                get
                {
                    /*decimal? d = null;
                    if (this.RecSemestre2 != null)
                    {
                        if (this.MediaSemestre2 != null)
                        {
                            d = this.RecSemestre2.Value > this.MediaSemestre2.Value ? (this.RecSemestre2.Value != 10 ? Math.Round(this.RecSemestre2.Value, 1) : 10) : (this.MediaSemestre2.Value != 10 ? Math.Round(this.MediaSemestre2.Value, 1) : 10);
                        }
                        else
                        {
                            d = (this.RecSemestre2.Value != 10 ? Math.Round(this.RecSemestre2.Value, 1) : 10);
                        }
                    }
                    return d != null ? d.Value.ToString() : "-";*/
                    return "-";
                }
            }

            public string PF
            {
                get
                {
                    return this.ProvaFinal.HasValue ? (this.ProvaFinal.Value != 10 ? this.ProvaFinal.Value.ToString("N1") : "10") : "-";
                }
            }

            public string QtdeFaltaTotal
            {
                get
                {
                    int? d = (this.FaltaB1 ?? 0) + (this.FaltaB2 ?? 0) + (this.FaltaB3 ?? 0) + (this.FaltaB4 ?? 0) != 0 ? (int?)(this.FaltaB1 ?? 0) + (this.FaltaB2 ?? 0) + (this.FaltaB3 ?? 0) + (this.FaltaB4 ?? 0) : null;
                    return d != null ? d.Value.ToString() : "-";
                }
            }

            //Mostra a Análise de Frequência apenas se isso tiver sido selecionado no parâmetro
            public bool mostraAnalFreq { get; set; }
            public string PercFreq
            {
                get
                {
                    if (mostraAnalFreq)
                    {
                        if (this.AulaB1 != null && this.AulaB2 != null && this.AulaB3 != null && this.AulaB4 != null)
                        {
                            if (this.FaltaB1 == null && this.FaltaB2 == null && this.FaltaB3 == null && this.FaltaB4 == null)
                            {
                                return "-";
                            }
                            else
                            {
                                int qtdft = this.QtdeFaltaTotal != "-" ? int.Parse(this.QtdeFaltaTotal) : 0;
                                decimal? dcmValor = (decimal)(((this.AulaB1 + this.AulaB2 + this.AulaB3 + this.AulaB4) - qtdft) * 100) / (this.AulaB1 + this.AulaB2 + this.AulaB3 + this.AulaB4);
                                return (Math.Round(dcmValor.Value, 1)).ToString();
                            }
                        }
                        else
                        {
                            return "-";
                        }
                    }
                    else
                    {
                        return "-";
                    }
                }
            }

            public decimal? MedMinim { get; set; }
            public string lblMedMin
            {
                get
                {
                    string media = this.MedMinim.HasValue ? this.MedMinim.Value.ToString() : "**";
                    return "Nota: De acordo com o regimento escolar, a nota mínima para aprovação é " + media + ".";
                }
            }
        }

        #endregion
    }
}
