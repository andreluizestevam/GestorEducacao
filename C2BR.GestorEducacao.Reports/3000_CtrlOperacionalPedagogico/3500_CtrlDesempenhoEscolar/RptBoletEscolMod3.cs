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
    public partial class RptBoletEscolMod3 : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptBoletEscolMod3()
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
                              string strP_OBS
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

                #region Seta o secretário escolar


                //select TP_CTRLE_SECRE_ESCOL from TB149_PARAM_INSTI
                string noSecretario = "";
                string matSecretario = "";
                TB25_EMPRESA emp = TB25_EMPRESA.RetornaPelaChavePrimaria(strP_CO_EMP_REF);
                emp.TB000_INSTITUICAOReference.Load();

                int coOrg = emp.TB000_INSTITUICAO.ORG_CODIGO_ORGAO;
                TB149_PARAM_INSTI tb149 = TB149_PARAM_INSTI.RetornaPelaChavePrimaria(coOrg);
                tb149.TB03_COLABOR3Reference.Load();

                if (tb149.TP_CTRLE_SECRE_ESCOL == "I")
                {
                    // Secretaria escolar controlada por instituição
                    if (tb149.TB03_COLABOR3 != null)
                    {
                        noSecretario = TB03_COLABOR.RetornaPelaChavePrimaria(strP_CO_EMP_REF, tb149.TB03_COLABOR3.CO_COL).NO_COL;
                        matSecretario = TB03_COLABOR.RetornaPelaChavePrimaria(strP_CO_EMP_REF, tb149.TB03_COLABOR3.CO_COL).CO_MAT_COL;
                    }
                }
                else
                {
                    // Secretaria escolar controlada por unidade
                    if (tb149.TB03_COLABOR3 != null)
                    {
                        noSecretario = TB03_COLABOR.RetornaPelaChavePrimaria(strP_CO_EMP_REF, tb149.TB03_COLABOR3.CO_COL).NO_COL;
                        matSecretario = TB03_COLABOR.RetornaPelaChavePrimaria(strP_CO_EMP_REF, tb149.TB03_COLABOR3.CO_COL).CO_MAT_COL;
                    }
                }

                if (matSecretario != "" && noSecretario != "")
                {
                    lblScreEscolar.Text = matSecretario + " - " + noSecretario;
                }
                else
                {
                    //Trata para mostrar ou não a imagem de assinatura, se for do reação mostra, senão não.
                    string cnpjEmp = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp).CO_CPFCGC_EMP;
                    string cnpjReacao = 05410813000173.ToString().PadLeft(14, '0');
                    if (cnpjEmp == cnpjReacao)
                    {
                        lblScreEscolar.Text = "Nome:";
                        lblScreEscolar.Text = "Camila Nogueira Rodrigues";
                        pbAssinatura.Visible = lblSecretario.Visible = true;
                    }
                    else
                    {
                        lblScreEscolar.Text = "Secretário(a) Escolar";
                        pbAssinatura.Visible = lblSecretario.Visible = false;
                    }
                    //lblScreEscolar.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                }

                #endregion

                #region Coloca a assinatura do secretário

                //System.Drawing.Image i = System.Drawing.Image.FromFile("/Library/IMG/Reação_CamilaAssinaturaDigital_Transp.png");

                pbAssinatura.ImageUrl = "/Library/IMG/Reação_CamilaAssinaturaDigital_Transp.png";

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

                if (strP_OBS == "")
                {
                    lblObs.Visible = false;
                }
                else
                {
                    lblObs.Text = strP_OBS;
                }

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
                               MediaS1 = tb079.VL_RECU_SEM1,
                               MediaS2 = tb079.VL_RECU_SEM2,
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

                #endregion

                if (res.Count == 0)
                    return -1;
                // Seta os dados no DataSource do Relatorio
                bsReport.Clear();

                foreach (var at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Boletim Aluno
        public class BoletimAluno
        {
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
            public string deInfoTurma
            {
                get
                {
                    return "Ano: " + Ano + "  Modalidade: " + Modalidade + "  Série: " + Serie + "  Turma: " + Turma + "  Turno: " + Turno;
                }
            }
            public string Disciplina { get; set; }
            public decimal? MediaB1 { get; set; }
            public decimal? MediaB2 { get; set; }
            public decimal? MediaB3 { get; set; }
            public decimal? MediaB4 { get; set; }
            public decimal? MediaS1 { get; set; }
            public decimal? MediaS2 { get; set; }
            public decimal? MediaFinal { get; set; }
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
                    return this.MediaB1 == null && this.MediaB2 == null ? null : this.MediaB1 != null && this.MediaB2 != null ? (decimal?)((this.MediaB1 ?? 0) + (this.MediaB2 ?? 0)) / 2 : this.MediaB1;
                }
            }

            public int? QtdeFaltaTotal
            {
                get
                {
                    return (this.FaltaB1 ?? 0) + (this.FaltaB2 ?? 0) + (this.FaltaB3 ?? 0) + (this.FaltaB4 ?? 0) != 0 ? (int?)(this.FaltaB1 ?? 0) + (this.FaltaB2 ?? 0) + (this.FaltaB3 ?? 0) + (this.FaltaB4 ?? 0) : null;
                }
            }

            public decimal? PercFreq
            {
                get
                {
                    if (this.AulaB1 != null && this.AulaB2 != null && this.AulaB3 != null && this.AulaB4 != null)
                    {
                        if (this.FaltaB1 == null && this.FaltaB2 == null && this.FaltaB3 == null && this.FaltaB4 == null)
                        {
                            return null;
                        }
                        else
                        {
                            int qtdft = this.QtdeFaltaTotal != null ? this.QtdeFaltaTotal.Value : 0;
                            decimal? dcmValor = (decimal)(((this.AulaB1 + this.AulaB2 + this.AulaB3 + this.AulaB4) - qtdft) * 100) / (this.AulaB1 + this.AulaB2 + this.AulaB3 + this.AulaB4);
                            return Math.Round(dcmValor.Value, 2);
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        #endregion
    }
}
