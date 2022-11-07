﻿using System;
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
    public partial class RptBoletEscolMod11 : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptBoletEscolMod11()
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
                lblTitulo.Text = "Boletim escolar".ToUpper();

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
                //Coordenador(a) Série/Curso
                var tb01 = (from itb01 in ctx.TB01_CURSO
                            join tb03 in ctx.TB03_COLABOR on itb01.CO_COOR equals tb03.CO_COL
                            where itb01.CO_CUR == strP_CO_CUR
                            select new
                            {
                                nomeSecretario = tb03.NO_COL,
                                matriculaSecretario = tb03.CO_MAT_COL
                            }).FirstOrDefault();

                if (tb01 != null)
                {
                    lblCoordenador.Text = (tb01.matriculaSecretario != "" ? tb01.matriculaSecretario : "XXXXX") + " - " + tb01.nomeSecretario;
                }
                else
                    lblCoordenador.Text = "";


                //Secretário (a) Diretor(a) Escolar

                var SecretarioEDiretor = (from tb083 in ctx.TB83_PARAMETRO.Where(m => m.CO_EMP.Equals(coEmp))
                                          join tb03 in ctx.TB03_COLABOR on tb083.CO_DIR1 equals tb03.CO_COL into l1
                                          from ldir in l1.DefaultIfEmpty()
                                          select new
                                                                 {
                                                                     nomeDir1 = (ldir != null ? ldir.NO_COL : ""),
                                                                     nomeSecr1 = tb083.TB03_COLABOR.NO_COL
                                                                 }).Distinct().OrderBy(o => o.nomeDir1).FirstOrDefault();

                if (SecretarioEDiretor != null)
                {
                    lblDiretor.Text = SecretarioEDiretor.nomeDir1 != "" ? SecretarioEDiretor.nomeDir1 : " - " + SecretarioEDiretor.nomeDir1;
                    lblSecretario.Text = SecretarioEDiretor.nomeSecr1 != "" ? SecretarioEDiretor.nomeSecr1 : " - " + SecretarioEDiretor.nomeSecr1;
                }
                else
                {
                    lblDiretor.Text = "";
                    lblSecretario.Text = "";
                }


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

                               RecEspecial = tb079.VL_MEDIA_FINAL,
                               MediaFinal = tb079.VL_MEDIA_FINAL,

                               QtdNotaDeConselho = tb079.VL_CONC_FINAL,
                               NotaDeConselho = tb079.VL_CONC_FINAL,

                               NomeFanEmp = tb25.NO_FANTAS_EMP,
                               NomeResp = alu.TB108_RESPONSAVEL.NO_RESP,
                               NomeAluno = alu.NO_ALU.ToUpper(),
                               NireAluno = alu.NU_NIRE,
                               dtNasc = alu.DT_NASC_ALU.Value,
                               CodigoAluno = alu.NU_NIRE,
                               Sexo = alu.CO_SEXO_ALU == "M" ? "MASCULINO" : "FEMININO",
                               Ano = tb079.CO_ANO_REF.Trim(),
                               Modalidade = tb079.TB44_MODULO.DE_MODU_CUR.ToUpper(),
                               Serie = ser.NO_CUR.ToUpper(),
                               Turma = tur.TB129_CADTURMAS.NO_TURMA.ToUpper(),
                               Turno = tur.CO_PERI_TUR == "M" ? "MANHÃ" : tur.CO_PERI_TUR == "N" ? "NOITE" : "TARDE",
                               Disciplina = cadMat.NO_RED_MATERIA.ToUpper(),
                               MediaB1 = tb079.VL_NOTA_BIM1,
                               MediaB2 = tb079.VL_NOTA_BIM2,
                               MediaB3 = tb079.VL_NOTA_BIM3,
                               MediaB4 = tb079.VL_NOTA_BIM4,
                               MediaSemestre1 = tb079.VL_NOTA_SEM1,
                               MediaSemestre2 = tb079.VL_NOTA_SEM2,
                               MediaAnual = tb079.VL_MEDIA_ANUAL,
                               FaltaB1 = tb079.QT_FALTA_BIM1,
                               FaltaB2 = tb079.QT_FALTA_BIM2,
                               FaltaB3 = tb079.QT_FALTA_BIM3,
                               FaltaB4 = tb079.QT_FALTA_BIM4,
                               RecurSem1 = tb079.VL_RECU_SEM1,
                               RecurSem2 = tb079.VL_RECU_SEM2,
                               AulaB1 = tb43.QT_AULAS_BIM1,
                               AulaB2 = tb43.QT_AULAS_BIM2,
                               AulaB3 = tb43.QT_AULAS_BIM3,
                               AulaB4 = tb43.QT_AULAS_BIM4,
                               ResultadoFinal = tb079.VL_MEDIA_FINAL,
                               ProvaFinal = tb079.VL_PROVA_FINAL,
                               Resultado = matr.CO_STA_APROV != null ? (matr.CO_STA_APROV == "A" && (matr.CO_STA_APROV_FREQ == "A" || matr.CO_STA_APROV_FREQ == null) ? "RESULTADO FINAL: APROVADO" : "RESULTADO FINAL: REPROVADO") : "RESULTADO FINAL: CURSANDO",
                               DescResultado = matr.CO_STA_APROV != null ? (matr.CO_STA_APROV == "A" && (matr.CO_STA_APROV_FREQ == "A" || matr.CO_STA_APROV_FREQ == null) ? strAprov :
                               "O(a) Aluno(a) foi reprovado(a) e continuará no " + ser.NO_CUR + " do " + tb079.TB44_MODULO.DE_MODU_CUR + ".") : "",
                               //Av2 = "-",
                               //Av3 = "-",
                               //Av4 = "-",
                               //Av5 = "-",
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
            public decimal? RecEspecial { get; set; }
            public decimal? MediaFinal { get; set; }
            public string NotaDeConselho { get; set; }

            public string NomeFanEmp { get; set; }
            public string NomeResp { get; set; }
            public int CodigoAluno { get; set; }
            public int NireAluno { get; set; }
            public DateTime? dtNasc { get; set; }
            public string dtNascimento
            {
                get
                {
                    return dtNasc == null ? "-" : Convert.ToDateTime(dtNasc).ToString("dd/MM/yyyy");
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
                    return "Ano: " + Ano + " - Modalidade: " + Modalidade + " - Série: " + Serie + " - Turma: " + Turma + " - Turno: " + Turno;
                }
            }
            public string Disciplina { get; set; }
            public decimal? MediaB1 { get; set; }
            public decimal? MediaB2 { get; set; }
            public decimal? MediaB3 { get; set; }
            public decimal? MediaB4 { get; set; }
            public decimal? RecurSem1 { get; set; }
            public decimal? RecurSem2 { get; set; }
            public decimal? ProvaFinal { get; set; }
            public decimal? ResultadoFinal { get; set; }
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
                    return "Declaro ter recebido nesta data  ___/___/_____  a demonstrativo  de  rendimento  escolar do aluno " + this.NomeAluno + ", Registro nr. " + this.NireAluno + " matriculado na modalidade " + this.Modalidade + ", série " + this.Serie + " e turma " + this.Turma + " da " + this.NomeFanEmp + " - Responsável do aluno: " + this.NomeResp + ".";
                }
            }

            public string AlunoDesc
            {
                get
                {
                    return this.NireAluno.ToString() + " - " + this.NomeAluno;
                }
            }

            public decimal? MediaSemestre1 { get; set; }

            public decimal? MediaSemestre2 { get; set; }

            public decimal? MediaAnual { get; set; }

            public int? QtdeFaltaTotal
            {
                get
                {
                    return (this.FaltaB1 ?? 0) + (this.FaltaB2 ?? 0) + (this.FaltaB3 ?? 0) + (this.FaltaB4 ?? 0) != 0 ? (int?)(this.FaltaB1 ?? 0) + (this.FaltaB2 ?? 0) + (this.FaltaB3 ?? 0) + (this.FaltaB4 ?? 0) : null;
                }
            }

            public string QtdNotaDeConselho { get; set; }

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

            public string PF
            {
                get
                {
                    return this.ProvaFinal != null ? this.ProvaFinal.Value.ToString() : "-";
                }
            }
        }

        #endregion

    }
}
