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
    public partial class RptBoletEscolMod12 : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptBoletEscolMod12()
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
                              bool chk1Tri,
                              bool chk2Tri,
                              bool chk3Tri,
                              bool mostraAnalFreq,
                              string TipoNomeDisc,
                              bool isResponsavel
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

                string strAprov;

                var resResul = (from serie in ctx.TB01_CURSO
                                join proxSerie in ctx.TB01_CURSO on serie.CO_PREDEC_CUR equals proxSerie.CO_CUR
                                where serie.CO_CUR == strP_CO_CUR
                                select new { proxSerie.NO_CUR, proxSerie.TB44_MODULO.DE_MODU_CUR }).FirstOrDefault();

                if (resResul != null)
                    strAprov = "O(a) Aluno(a) foi promovido(a) para o " + resResul.NO_CUR + " do " + resResul.DE_MODU_CUR + ".";
                else
                    strAprov = "O(a) Aluno(a) foi promovido(a) para o XXXXX do XXXXX.";

                if (strP_TITULO != "")
                {
                    lblTitulo.Text = strP_TITULO;
                    lblTitProtocEntreg.Text = strP_TITULO.ToUpper();
                }

                #region Query Boletim Aluno
                if (isResponsavel)
                { }
                var lst = (from tb079 in ctx.TB079_HIST_ALUNO
                           join matr in ctx.TB08_MATRCUR on tb079.CO_ALU equals matr.CO_ALU
                           join tb43 in ctx.TB43_GRD_CURSO on tb079.CO_CUR equals tb43.CO_CUR
                           join alu in ctx.TB07_ALUNO on tb079.CO_ALU equals alu.CO_ALU
                           join ser in ctx.TB01_CURSO on tb079.CO_CUR equals ser.CO_CUR
                           join tur in ctx.TB06_TURMAS on tb079.CO_TUR equals tur.CO_TUR
                           join mat in ctx.TB02_MATERIA on tb079.CO_MAT equals mat.CO_MAT
                           join cadMat in ctx.TB107_CADMATERIAS on mat.ID_MATERIA equals cadMat.ID_MATERIA
                           join tb25 in ctx.TB25_EMPRESA on tb079.CO_EMP equals tb25.CO_EMP
                           where tb079.CO_MODU_CUR == strP_CO_MODU_CUR 
                           && tb079.CO_CUR == strP_CO_CUR 
                           && tb079.CO_TUR == strP_CO_TUR
                           && (tb079.CO_ANO_REF == strP_CO_ANO_MES_MAT) 
                           && tb079.CO_ANO_REF == matr.CO_ANO_MES_MAT
                           && tb079.CO_ANO_REF == tb43.CO_ANO_GRADE 
                           && tb079.CO_MAT == tb43.CO_MAT
                           && (strP_CO_ALU != 0 ? tb079.CO_ALU == strP_CO_ALU : 0 == 0)
                           && matr.CO_EMP == tb079.CO_EMP 
                           && tb43.CO_EMP == tb079.CO_EMP
                           && tb079.CO_EMP == strP_CO_EMP_REF
                            && ( isResponsavel ? tb079.CO_MAT == tb43.CO_MAT : tb43.ID_MATER_AGRUP == null)
                           //Listar as disciplinas diferentes da classificação "Não se Aplica"
                           && cadMat.CO_CLASS_BOLETIM != 4
                           select new BoletimAlunoModelo12
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
                               MediaT1 = tb079.VL_MEDIA_TRI1,
                               MediaT2 = tb079.VL_MEDIA_TRI2,
                               MediaT3 = tb079.VL_MEDIA_TRI3,
                               FaltaT1 = tb079.QT_FALTA_TRI1,
                               FaltaT2 = tb079.QT_FALTA_TRI2,
                               FaltaT3 = tb079.QT_FALTA_TRI3,
                               AulaT1 = tb43.QT_AULAS_TRI1,
                               AulaT2 = tb43.QT_AULAS_TRI2,
                               AulaT3 = tb43.QT_AULAS_TRI3,
                               MediaFinal = tb079.VL_MEDIA_FINAL,
                               ProvaFinal = tb079.VL_PROVA_FINAL,
                               RecTrimestre1 = tb079.VL_RECU_TRI1,
                               RecTrimestre2 = tb079.VL_RECU_TRI2,
                               RecTrimestre3 = tb079.VL_RECU_TRI3,
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

                        lblObsTit.Text = "Avaliação do Aluno";

                        //Trata o que o usuário selecionou, para mostrar ou não as avaliações dos bimestres de acordo com escolha 
                        if (chk1Tri == true)
                        {
                            var aval1 = TB126_HIST_ALUNO_AVAL.RetornaTodosRegistros().Where(w => w.CO_ALU == at.coAlu && w.CO_MODU_CUR == at.coModuCur && w.CO_CUR == at.coCur && w.CO_TUR == at.coTur && w.CO_ANO_REF == at.coAnoMesMat && w.CO_BIMESTRE == "T1").FirstOrDefault();
                            if (aval1 != null)
                            {
                                if (aval1.DE_AVAL != null)
                                    this.tbAval1B.Visible = true; at.AvalTri1 = "1º Trimestre - " + aval1.DE_AVAL;
                            }
                        }
                        if (chk2Tri == true)
                        {
                            var aval2 = TB126_HIST_ALUNO_AVAL.RetornaTodosRegistros().Where(w => w.CO_ALU == at.coAlu && w.CO_MODU_CUR == at.coModuCur && w.CO_CUR == at.coCur && w.CO_TUR == at.coTur && w.CO_ANO_REF == at.coAnoMesMat && w.CO_BIMESTRE == "T2").FirstOrDefault();
                            if (aval2 != null)
                            {
                                if (aval2.DE_AVAL != null)
                                    this.tbAval2B.Visible = true; at.AvalTri2 = "2º Trimestre - " + aval2.DE_AVAL;
                            }
                        }
                        if (chk3Tri == true)
                        {
                            var aval3 = TB126_HIST_ALUNO_AVAL.RetornaTodosRegistros().Where(w => w.CO_ALU == at.coAlu && w.CO_MODU_CUR == at.coModuCur && w.CO_CUR == at.coCur && w.CO_TUR == at.coTur && w.CO_ANO_REF == at.coAnoMesMat && w.CO_BIMESTRE == "T3").FirstOrDefault();
                            if (aval3 != null)
                            {
                                if (aval3.DE_AVAL != null)
                                    this.tbAval3B.Visible = true; at.AvalTri3 = "3º Trimestre - " + aval3.DE_AVAL;
                            }
                        }
                    }
                    else
                    {
                        this.tbAval1B.Visible = true;
                        at.AvalTri1 = at.Obs;
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
        //public class AvalGeral
        //{
        //    public string aval { get; set; }
        //}

        public class BoletimAlunoModelo12
        {
            //Atributos para alimentar as informações de Avaliação do Aluno.
            public int coCur { get; set; }
            public int? coTur { get; set; }
            //public int coAlu { get; set; }
            public string coAnoMesMat { get; set; }
            public int coModuCur { get; set; }
            public string AvalAlunoOb { get; set; }

            public int? ordImp { get; set; }
            public string AvalTri1 { get; set; }
            public string AvalTri2 { get; set; }
            public string AvalTri3 { get; set; }

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

                    if (this.dtNasc.Year > 1923)
                    {
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
            public decimal? MediaT1 { get; set; }
            public decimal? MediaT2 { get; set; }
            public decimal? MediaT3 { get; set; }
            public decimal? MediaFinal { get; set; }
            public decimal? ProvaFinal { get; set; }
            public string MT1
            {
                get
                {
                    return this.MediaT1.HasValue ? (this.MediaT1.Value != 10 ? this.MediaT1.Value.ToString("N1") : "10") : "-";
                }
            }
            public string MT2
            {
                get
                {
                    return this.MediaT2.HasValue ? (this.MediaT2.Value != 10 ? this.MediaT2.Value.ToString("N1") : "10") : "-";
                }
            }
            public string MT3
            {
                get
                {
                    return this.MediaT3.HasValue ? (this.MediaT3.Value != 10 ? this.MediaT3.Value.ToString("N1") : "10") : "-";
                }
            }
            public string MF
            {
                get
                {
                    return this.MediaFinal.HasValue ? (this.MediaFinal.Value != 10 ? this.MediaFinal.Value.ToString("N1") : "10") : "-";
                }
            }
            public int? FaltaT1 { get; set; }
            public int? FaltaT2 { get; set; }
            public int? FaltaT3 { get; set; }
            public string FT1
            {
                get
                {
                    return this.FaltaT1 != null ? this.FaltaT1.Value.ToString() : "-";
                }
            }
            public string FT2
            {
                get
                {
                    return this.FaltaT2 != null ? this.FaltaT2.Value.ToString() : "-";
                }
            }
            public string FT3
            {
                get
                {
                    return this.FaltaT3 != null ? this.FaltaT3.Value.ToString() : "-";
                }
            }
            public int? AulaT1 { get; set; }
            public int? AulaT2 { get; set; }
            public int? AulaT3 { get; set; }
            public string AT1
            {
                get
                {
                    return this.AulaT1 != null ? this.AulaT1.Value.ToString() : "-";
                }
            }
            public string AT2
            {
                get
                {
                    return this.AulaT2 != null ? this.AulaT2.Value.ToString() : "-";
                }
            }
            public string AT3
            {
                get
                {
                    return this.AulaT3 != null ? this.AulaT3.Value.ToString() : "-";
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
                    return this.NireAluno.ToString().PadLeft(7, '0') + " - " + this.NomeAluno;
                }
            }

            public decimal? RecTrimestre1 { get; set; }
            public decimal? RecTrimestre2 { get; set; }
            public decimal? RecTrimestre3 { get; set; }

            public string RT1
            {
                get
                {
                    return this.RecTrimestre1.HasValue ? (this.RecTrimestre1.Value != 10 ? this.RecTrimestre1.Value.ToString("N1") : "10") : "-";
                }
            }

            public string RT2
            {
                get
                {
                    return this.RecTrimestre2.HasValue ? (this.RecTrimestre2.Value != 10 ? this.RecTrimestre2.Value.ToString("N1") : "10") : "-";
                }
            }

            public string RT3
            {
                get
                {
                    return this.RecTrimestre3.HasValue ? (this.RecTrimestre3.Value != 10 ? this.RecTrimestre3.Value.ToString("N1") : "10") : "-";
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
                    int? d = (this.FaltaT1 ?? 0) + (this.FaltaT2 ?? 0) + (this.FaltaT3 ?? 0) != 0 ? (int?)(this.FaltaT1 ?? 0) + (this.FaltaT2 ?? 0) + (this.FaltaT3 ?? 0) : null;
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
                        if (this.AulaT1 != null && this.AulaT2 != null && this.AulaT3 != null)
                        {
                            if (this.FaltaT1 == null && this.FaltaT2 == null && this.FaltaT3 == null)
                            {
                                return "-";
                            }
                            else
                            {
                                int qtdft = this.QtdeFaltaTotal != "-" ? int.Parse(this.QtdeFaltaTotal) : 0;
                                decimal? dcmValor = (decimal)(((this.AulaT1 + this.AulaT2 + this.AulaT3) - qtdft) * 100) / (this.AulaT1 + this.AulaT2 + this.AulaT3);
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
