﻿//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// DATA DE CRIAÇÃO: 
//-------------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-------------------------------------------------------------------------------
//  DATA      |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// -----------+----------------------------+-------------------------------------
// 06/06/2013 | Victor Martins Machado     | Corrigido o problema da divisão por 0 no cálculo
//            |                            | da porcentagem total de faltas (TF).
//            |                            | 

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
    public partial class RptBoletEscolMod4 : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptBoletEscolMod4()
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
                //this.VisiblePageHeader = true;
                //this.VisibleNumeroPage = false;
                //this.VisibleDataHeader = false;
                //this.VisibleHoraHeader = false;

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

                if (tb83 != null)
                {
                    //lblSecretario.Text = (tb83.matriculaSecretario != "" ? tb83.matriculaSecretario : "XXXXX") + " - " + tb83.nomeSecretario;
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
                               coEmp = tb25.CO_EMP,
                               coModuCur = tb079.TB44_MODULO.CO_MODU_CUR,
                               coCur = tb079.CO_CUR,
                               coTur = tur.CO_TUR,
                               coMat = tb079.CO_MAT,
                               anoRef = matr.CO_ANO_MES_MAT,

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

                               MBB1 = tb079.VL_NOTA_BIM1,
                               MBB2 = tb079.VL_NOTA_BIM2,
                               MBB3 = tb079.VL_NOTA_BIM3,
                               MBB4 = tb079.VL_NOTA_BIM4,

                               FBB1 = tb079.QT_FALTA_BIM1 != null ? tb079.QT_FALTA_BIM1 : 0,
                               FBB2 = tb079.QT_FALTA_BIM2 != null ? tb079.QT_FALTA_BIM2 : 0,
                               FBB3 = tb079.QT_FALTA_BIM3 != null ? tb079.QT_FALTA_BIM3 : 0,
                               FBB4 = tb079.QT_FALTA_BIM4 != null ? tb079.QT_FALTA_BIM4 : 0,

                               ADB1 = tb43.QT_AULAS_BIM1,
                               ADB2 = tb43.QT_AULAS_BIM2,
                               ADB3 = tb43.QT_AULAS_BIM3,
                               ADB4 = tb43.QT_AULAS_BIM4,

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
            public int coEmp  { get; set; }
            public int coModuCur { get; set; }
            public int coCur { get; set; }
            public int coTur { get; set; }
            public int coMat { get; set; }
            public string anoRef { get; set; }

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

            public decimal? MBB1 { get; set; }
            public decimal? MBB2 { get; set; }
            public decimal? MBB3 { get; set; }
            public decimal? MBB4 { get; set; }

            public decimal TP 
            {
                get
                {
                    decimal MB1 = 0;
                    decimal MB2 = 0;
                    decimal MB3 = 0;
                    decimal MB4 = 0;
                    decimal r = 0;
                    decimal med = 0;
                    int i = 0;
                    int ano = int.Parse(this.anoRef);

                    switch (ano)
                    {
                        case 2013:
                            MB1 = this.MBB1 != null ? this.MBB1.Value * 2 : 0;
                            i += this.MBB1 != null ? 1 : 0;
                            MB2 = this.MBB2 != null ? this.MBB2.Value * 2 : 0;
                            i += this.MBB2 != null ? 1 : 0;
                            MB3 = this.MBB3 != null ? this.MBB3.Value * 3 : 0;
                            i += this.MBB3 != null ? 1 : 0;
                            MB4 = this.MBB4 != null ? this.MBB4.Value * 3 : 0;
                            i += this.MBB4 != null ? 1 : 0;
                            med = 50;
                            break;

                        case 2014:
                            MB1 = this.MBB1 != null ? this.MBB1.Value : 0;
                            i += this.MBB1 != null ? 1 : 0;
                            MB2 = this.MBB2 != null ? this.MBB2.Value : 0;
                            i += this.MBB2 != null ? 1 : 0;
                            MB3 = this.MBB3 != null ? this.MBB3.Value : 0;
                            i += this.MBB3 != null ? 1 : 0;
                            MB4 = this.MBB4 != null ? this.MBB4.Value : 0;
                            i += this.MBB4 != null ? 1 : 0;
                            med = 20;
                            break;
                    }

                    r = Math.Round((decimal)(MB1 + MB2 + MB3 + MB4), 2);

                    if (this.MBB4 != null)
                    {
                        if (r >= med)
                        {
                            this.Resultado = "APROVADO";
                        }
                        else
                        {
                            this.Resultado = "REPROVADO";
                        }
                    }

                    return r;
                }
            }

            public int? FBB1 { get; set; }
            public int? FBB2 { get; set; }
            public int? FBB3 { get; set; }
            public int? FBB4 { get; set; }

            public decimal? MCB1
            {
                get
                {
                    return Funcoes.CalculaMediaTurma(this.coEmp, this.coModuCur, this.coCur, this.coTur, this.coMat, this.anoRef, 1);
                }
            }
            public decimal? MCB2
            {
                get
                {
                    return Funcoes.CalculaMediaTurma(this.coEmp, this.coModuCur, this.coCur, this.coTur, this.coMat, this.anoRef, 2);
                }
            }
            public decimal? MCB3
            {
                get
                {
                    return Funcoes.CalculaMediaTurma(this.coEmp, this.coModuCur, this.coCur, this.coTur, this.coMat, this.anoRef, 3);
                }
            }
            public decimal? MCB4
            {
                get
                {
                    return Funcoes.CalculaMediaTurma(this.coEmp, this.coModuCur, this.coCur, this.coTur, this.coMat, this.anoRef, 4);
                }
            }

            public int? ADB1 { get; set; }
            public int? ADB2 { get; set; }
            public int? ADB3 { get; set; }
            public int? ADB4 { get; set; }

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

            public decimal TF
            {
                get
                {
                    int FB1 = this.FBB1 != null ? this.FBB1.Value : 0;
                    int FB2 = this.FBB2 != null ? this.FBB2.Value : 0;
                    int FB3 = this.FBB3 != null ? this.FBB3.Value : 0;
                    int FB4 = this.FBB4 != null ? this.FBB4.Value : 0;

                    int TFB = FB1 + FB2 + FB3 + FB4;

                    int AB1 = this.ADB1 != null ? this.ADB1.Value : 0;
                    int AB2 = this.ADB2 != null ? this.ADB2.Value : 0;
                    int AB3 = this.ADB3 != null ? this.ADB3.Value : 0;
                    int AB4 = this.ADB4 != null ? this.ADB4.Value : 0;

                    int TAB = AB1 + AB2 + AB3 + AB4;

                    decimal i = (decimal)0;
                    if (TAB > 0)
                    {
                        i = (decimal)(TFB * 100) / TAB;
                    }
                    
                    return Math.Round(i, 2);
                }
            }

            public decimal? PFB1
            {
                get
                {
                    if (this.ADB1 != null)
                    {
                        if (this.FBB1 != null)
                        {
                            decimal? PF = (decimal)(this.FBB1 * 100) / this.ADB1;
                            return Math.Round(PF.Value, 2);
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            public decimal? PFB2
            {
                get
                {
                    if (this.ADB2 != null)
                    {
                        if (this.FBB2 != null)
                        {
                            decimal? PF = (decimal)(this.FBB2 * 100) / this.ADB2;
                            return Math.Round(PF.Value, 2);
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            public decimal? PFB3
            {
                get
                {
                    if (this.ADB3 != null)
                    {
                        if (this.FBB3 != null)
                        {
                            decimal? PF = (decimal)(this.FBB3 * 100) / this.ADB3;
                            return Math.Round(PF.Value, 2);
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            public decimal? PFB4
            {
                get
                {
                    if (this.ADB4 != null)
                    {
                        if (this.FBB4 != null)
                        {
                            decimal? PF = (decimal)(this.FBB4 * 100) / this.ADB4;
                            return Math.Round(PF.Value, 2);
                        }
                        else
                        {
                            return null;
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