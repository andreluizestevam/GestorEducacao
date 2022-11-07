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
    public partial class RptBoletEscolMod7 : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptBoletEscolMod7()
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
                              string strP_TITULO
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

                var tb83 = (from iTb83 in ctx.TB83_PARAMETRO
                            where iTb83.CO_EMP == strP_CO_EMP_REF
                            select new
                            {
                                nomeSecretario = iTb83.TB03_COLABOR != null ? iTb83.TB03_COLABOR.NO_COL : "",
                                matriculaSecretario = iTb83.TB03_COLABOR != null ? iTb83.TB03_COLABOR.CO_MAT_COL : ""
                            }).FirstOrDefault();

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

                lblObs.Text = strP_OBS;
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
                           where tb079.CO_MODU_CUR == strP_CO_MODU_CUR && tb079.CO_CUR == strP_CO_CUR && tb079.CO_TUR == strP_CO_TUR
                           && (tb079.CO_ANO_REF == strP_CO_ANO_MES_MAT) && tb079.CO_ANO_REF == matr.CO_ANO_MES_MAT
                           && tb079.CO_ANO_REF == tb43.CO_ANO_GRADE && tb079.CO_MAT == tb43.CO_MAT
                           && (strP_CO_ALU != 0 ? tb079.CO_ALU == strP_CO_ALU : 0 == 0)
                           && matr.CO_EMP == tb079.CO_EMP && tb43.CO_EMP == tb079.CO_EMP
                           && tb079.CO_EMP == strP_CO_EMP_REF
                           && tb43.ID_MATER_AGRUP == null
                               //Listar as disciplinas diferentes da classificação "Não se Aplica"
                           && cadMat.CO_CLASS_BOLETIM != 4
                           select new BoletimAluno
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
                               Disciplina = cadMat.NO_RED_MATERIA.ToUpper(),
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
                               Resultado = matr.CO_STA_APROV != null ? (matr.CO_STA_APROV == "A" && (matr.CO_STA_APROV_FREQ == "A" || matr.CO_STA_APROV_FREQ == null) ? "APROVADO" : "REPROVADO") : "CURSANDO",
                               DescResultado = matr.CO_STA_APROV != null ? (matr.CO_STA_APROV == "A" && (matr.CO_STA_APROV_FREQ == "A" || matr.CO_STA_APROV_FREQ == null) ? strAprov :
                               "O(a) Aluno(a) foi reprovado(a) e continuará no " + ser.NO_CUR + " do " + tb079.TB44_MODULO.DE_MODU_CUR + ".") : ""
                           }
                  );

                var res = lst.Distinct().OrderBy(r => r.NomeAluno).ThenBy(r => r.Disciplina).ToList();

                foreach (var r in res)
                {
                    int idTpAtiv = (from t in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros() where t.CO_SIGLA_ATIV == "RECSE" select new { t.ID_TIPO_ATIV }).FirstOrDefault().ID_TIPO_ATIV;
                    decimal? v = null;

                    if ((from a in TB49_NOTA_ATIV_ALUNO.RetornaTodosRegistros() where a.TB01_CURSO.CO_CUR == r.CodigoCur && a.TB06_TURMAS.CO_TUR == r.CodigoTurma && a.TB107_CADMATERIAS.ID_MATERIA == r.CodigoMateria && a.TB273_TIPO_ATIVIDADE.ID_TIPO_ATIV == idTpAtiv && a.TB07_ALUNO.CO_ALU == r.coAlu select new { a.VL_NOTA }).Any())
                    {
                        v = (from a in TB49_NOTA_ATIV_ALUNO.RetornaTodosRegistros()
                             where a.TB01_CURSO.CO_CUR == r.CodigoCur
                             && a.TB06_TURMAS.CO_TUR == r.CodigoTurma
                             && a.TB107_CADMATERIAS.ID_MATERIA == r.CodigoMateria
                             && a.TB273_TIPO_ATIVIDADE.ID_TIPO_ATIV == idTpAtiv
                             && a.TB07_ALUNO.CO_ALU == r.coAlu
                             select new
                             {
                                 a.VL_NOTA
                             }).FirstOrDefault().VL_NOTA;
                    }

                    if (v != null)
                    {
                        r.RecSemestre = v != null ? v.Value : (decimal?)null;
                    }

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
                    bsReport.Add(at);

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
        public class BoletimAluno
        {
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
            public string Disciplina { get; set; }
            public decimal? MediaB1 { get; set; }
            public decimal? MediaB2 { get; set; }
            public decimal? MediaB3 { get; set; }
            public decimal? MediaB4 { get; set; }
            public decimal? MediaFinal { get; set; }
            public string MB1
            {
                get
                {
                    decimal? d = this.MediaB1 != null ? Math.Round(this.MediaB1.Value, 1) : (decimal?)null;
                    return d != null ? d.ToString() : "-";
                }
            }
            public string MB2
            {
                get
                {
                    decimal? d = this.MediaB2 != null ? Math.Round(this.MediaB2.Value, 1) : (decimal?)null;
                    return d != null ? d.ToString() : "-";
                }
            }
            public string MB3
            {
                get
                {
                    decimal? d = this.MediaB3 != null ? Math.Round(this.MediaB3.Value, 1) : (decimal?)null;
                    return d != null ? d.ToString() : "-";
                }
            }
            public string MB4
            {
                get
                {
                    decimal? d = this.MediaB4 != null ? Math.Round(this.MediaB4.Value, 1) : (decimal?)null;
                    return d != null ? d.ToString() : "-";
                }
            }
            public string MF
            {
                get
                {
                    decimal? d = this.MediaFinal != null ? Math.Round(this.MediaFinal.Value, 1) : (decimal?)null;
                    return d != null ? d.ToString() : "-";
                }
            }
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

            public byte[] foto { get; set; }

            public string Declaro
            {
                get
                {
                    return "Declaro ter recebido nesta data  ___/___/_____  a ficha de rendimento escolar do aluno " + this.NomeAluno + ", Registro nr. " + this.NireAluno + " matriculado na modalidade " + this.Modalidade + ", série " + this.Serie + " e turma " + this.Turma + " da " + this.NomeFanEmp + " - Responsável do aluno: " + this.NomeResp + ".";
                }
            }

            public string AlunoDesc
            {
                get
                {
                    return this.NireAluno.ToString() + " - " + this.NomeAluno;
                }
            }

            public decimal? MediaSemestre
            {
                get
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
                    return ms != null ? Math.Round(ms.Value,1) : (decimal?)null;
                }
            }

            public decimal? RecSemestre { get; set; }

            public string RS
            {
                get
                {
                    return this.RecSemestre != null ? this.RecSemestre.Value.ToString() : "-";
                }
            }

            public string PRecSemestre
            {
                get
                {
                    decimal? d = null;
                    if (this.RecSemestre != null)
                    {
                        if (this.MediaSemestre != null)
                        {
                            d = this.RecSemestre.Value > this.MediaSemestre.Value ? Math.Round(this.RecSemestre.Value,1) : Math.Round(this.MediaSemestre.Value,1);
                        }
                        else
                        {
                            d = Math.Round(this.RecSemestre.Value,1);
                        }
                    }
                    return d != null ? d.Value.ToString() : "-";
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

            public string PercFreq
            {
                get
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
            }
        }

        #endregion
    }
}
