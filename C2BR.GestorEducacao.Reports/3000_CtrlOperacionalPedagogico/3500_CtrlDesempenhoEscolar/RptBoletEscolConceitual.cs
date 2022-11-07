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
    public partial class RptBoletEscolConceitual : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptBoletEscolConceitual()
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
                              string strDES_OBS,
                              bool mostraAvalGeral,
                              bool mostraTotFal,
                              int totFal,
                              bool chk1Bim,
                              bool chk2Bim,
                              bool chk3Bim,
                              bool chk4Bim
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
                this.lblTitulo.Text = "FICHA DE AVALIAÇÃO CONCEITUAL - ANO LETIVO " + strP_CO_ANO_MES_MAT.Trim();
                this.lblObservacao.Text = strDES_OBS;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(coEmp);

                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                //var resa = (from tb126 in TB126_HIST_ALUNO_AVAL.RetornaTodosRegistros()
                //            where tb126.CO_MODU_CUR == strP_CO_MODU_CUR
                //            && tb126.CO_CUR == strP_CO_CUR
                //            && tb126.CO_TUR == strP_CO_TUR
                //            && tb126.CO_ALU == strP_CO_ALU
                //            && tb126.CO_ANO_REF == strP_CO_ANO_MES_MAT
                //           select new AvalGeral
                //           {
                //               aval = tb126.DE_AVAL
                //           });

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

                //if (avalGeral != "")
                //{
                //    lblAvalGeral.Text = avalGeral;

                //    lblTitAvalGeral.Visible = mostraAvalGeral;
                //    lblAvalGeral.Visible = mostraAvalGeral;
                //}
                //else
                //{
                //    lblTitAvalGeral.Visible = false;
                //    lblAvalGeral.Visible = false;
                //}

                lblTitAvalGeral.Visible = mostraAvalGeral;
                lblAvalBim1.Visible = mostraAvalGeral;

                if (mostraTotFal)
                {
                    lblTotFal.Text = totFal.ToString();
                    lblTotFalTit.Visible = true;
                    lblTotFal.Visible = true;
                }
                else
                {
                    lblTotFal.Text = "";
                    lblTotFalTit.Visible = false;
                    lblTotFal.Visible = false;
                }

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
                    lblSecretario.Text = (tb83.matriculaSecretario != "" ? tb83.matriculaSecretario : "XXXXX") + " - " + tb83.nomeSecretario;
                }

                #region Query que retorna a legenda dos conceitos

                var lLeg = (from tb200 in TB200_EQUIV_NOTA_CONCEITO.RetornaTodosRegistros()
                            where tb200.CO_SITU_CONC == "A"
                            && tb200.TB25_EMPRESA.CO_EMP == coEmp
                            select new LegendaConceito
                            {
                                coSigla = tb200.CO_SIGLA_CONCEITO,
                                deConce = tb200.DE_CONCEITO
                            });

                string Legenda = "";
                foreach (LegendaConceito lc in lLeg)
                {
                    switch (lc.coSigla)
                    {
                        case "E":
                            lc.deConce = "PROCESSO DE DESENVOLVIMENTO";
                            break;
                        case "O":
                            lc.deConce = "NÃO OBSERVADO NO BIMESTRE";
                            break;
                    }

                    if (Legenda == "")
                    {
                        Legenda += "Legenda: " + lc.coSigla + " (" + lc.deConce + ")";
                    }
                    else
                    {
                        Legenda += " - " + lc.coSigla + " (" + lc.deConce + ")";
                    }
                }
                lblLegenda.Text = Legenda;

                #endregion

                #region Query Boletim Aluno

                var lst = (from tb079 in ctx.TB079_HIST_ALUNO
                           join tb25 in ctx.TB25_EMPRESA on tb079.CO_EMP equals tb25.CO_EMP
                           join matr in ctx.TB08_MATRCUR on tb079.CO_ALU equals matr.CO_ALU
                           join tb43 in ctx.TB43_GRD_CURSO on tb079.CO_CUR equals tb43.CO_CUR
                           join alu in ctx.TB07_ALUNO on tb079.CO_ALU equals alu.CO_ALU
                           join tb108 in ctx.TB108_RESPONSAVEL on alu.TB108_RESPONSAVEL.CO_RESP equals tb108.CO_RESP
                           
                           join ser in ctx.TB01_CURSO on tb079.CO_CUR equals ser.CO_CUR
                           join tur in ctx.TB06_TURMAS on tb079.CO_TUR equals tur.CO_TUR
                           join mat in ctx.TB02_MATERIA on tb079.CO_MAT equals mat.CO_MAT
                           join cadMat in ctx.TB107_CADMATERIAS on mat.ID_MATERIA equals cadMat.ID_MATERIA

                           where tb079.CO_MODU_CUR == strP_CO_MODU_CUR && tb079.CO_CUR == strP_CO_CUR && tb079.CO_TUR == strP_CO_TUR
                           && (tb079.CO_ANO_REF == strP_CO_ANO_MES_MAT) && tb079.CO_ANO_REF == matr.CO_ANO_MES_MAT
                           && tb079.CO_ANO_REF == tb43.CO_ANO_GRADE && tb079.CO_MAT == tb43.CO_MAT
                           && (strP_CO_ALU != 0 ? tb079.CO_ALU == strP_CO_ALU : 0 == 0)
                           && matr.CO_EMP == tb079.CO_EMP && tb43.CO_EMP == tb079.CO_EMP
                           && tb079.CO_EMP == strP_CO_EMP_REF
                           && cadMat.CO_CLASS_BOLETIM != 4
                           select new BoletimAlunoConceitual
                           {
                               NomeAluno = alu.NO_ALU.ToUpper(),
                               NireAluno = alu.NU_NIRE,
                               coAlu = alu.CO_ALU,
                               coModu = tb079.CO_MODU_CUR,
                               coCur = tb079.CO_CUR,
                               coTur = tb079.CO_TUR,
                               coAno = tb079.CO_ANO_REF,
                               CodigoAluno = alu.NU_NIRE,
                               NasctoAluno = alu.DT_NASC_ALU,
                               SexoReceb = alu.CO_SEXO_ALU,
                               Ano = tb079.CO_ANO_REF,
                               Modalidade = tb079.TB44_MODULO.DE_MODU_CUR,
                               Serie = ser.NO_CUR,
                               Turma = tur.TB129_CADTURMAS.CO_SIGLA_TURMA,
                               TurnoReceb = tur.CO_PERI_TUR,
                               Disciplina = cadMat.NO_MATERIA,
                               MB1 = tb079.VL_CONC_BIM1,
                               MB2 = tb079.VL_CONC_BIM2,
                               MB3 = tb079.VL_CONC_BIM3,
                               MB4 = tb079.VL_CONC_BIM4,
                               FaltaB1 = tb079.QT_FALTA_BIM1,
                               FaltaB2 = tb079.QT_FALTA_BIM2,
                               FaltaB3 = tb079.QT_FALTA_BIM3,
                               FaltaB4 = tb079.QT_FALTA_BIM4,
                               ResponsavelAluno = tb108.NO_RESP,
                               Unidade = tb25.NO_FANTAS_EMP,
                           }
                  );

                var res = lst.Distinct().OrderBy(r => r.NomeAluno).ThenBy(r => r.Disciplina).ToList();

                #endregion

                if (res.Count == 0)
                    return -1;
                // Seta os dados no DataSource do Relatorio
                bsReport.Clear();

                foreach (var at in res)
                {
                    var imaalu = TB07_ALUNO.RetornaPeloCoAlu(at.coAlu).Image;
                                        
                    if(imaalu != null)
                        at.ImagemAluno = imaalu.ImageStream;

                    //Trata o que o usuário selecionou, para mostrar ou não as avaliações dos bimestres de acordo com escolha 
                    if(chk1Bim == true)
                    {
                        var aval1 = TB126_HIST_ALUNO_AVAL.RetornaTodosRegistros().Where(w => w.CO_ALU == at.coAlu && w.CO_MODU_CUR == at.coModu && w.CO_CUR == at.coCur && w.CO_TUR == at.coTur && w.CO_ANO_REF == at.coAno && w.CO_BIMESTRE == "B1").FirstOrDefault();
                        if (aval1 != null)
                        {
                            if (aval1.DE_AVAL != null)
                                this.lblAvalBim1.Visible = true; at.AvalBim1 = "1º Bimestre - " + aval1.DE_AVAL;
                        }
                    }
                    if (chk2Bim == true)
                    {
                        var aval2 = TB126_HIST_ALUNO_AVAL.RetornaTodosRegistros().Where(w => w.CO_ALU == at.coAlu && w.CO_MODU_CUR == at.coModu && w.CO_CUR == at.coCur && w.CO_TUR == at.coTur && w.CO_ANO_REF == at.coAno && w.CO_BIMESTRE == "B2").FirstOrDefault();
                        if (aval2 != null)
                        {
                            if (aval2.DE_AVAL != null)
                                this.lblAvalBim2.Visible = true; at.AvalBim2 = "2º Bimestre - " + aval2.DE_AVAL;
                        }
                    }
                    if (chk3Bim == true)
                    {
                        var aval3 = TB126_HIST_ALUNO_AVAL.RetornaTodosRegistros().Where(w => w.CO_ALU == at.coAlu && w.CO_MODU_CUR == at.coModu && w.CO_CUR == at.coCur && w.CO_TUR == at.coTur && w.CO_ANO_REF == at.coAno && w.CO_BIMESTRE == "B3").FirstOrDefault();
                        if (aval3 != null)
                        {
                            if (aval3.DE_AVAL != null)
                                this.lblAvalBim3.Visible = true; at.AvalBim3 = "3º Bimestre - " + aval3.DE_AVAL;
                        }
                    }
                    if (chk4Bim == true)
                    {
                        var aval4 = TB126_HIST_ALUNO_AVAL.RetornaTodosRegistros().Where(w => w.CO_ALU == at.coAlu && w.CO_MODU_CUR == at.coModu && w.CO_CUR == at.coCur && w.CO_TUR == at.coTur && w.CO_ANO_REF == at.coAno && w.CO_BIMESTRE == "B4").FirstOrDefault();
                        if (aval4 != null)
                        {
                            if (aval4.DE_AVAL != null)
                                this.lblAvalBim4.Visible = true; at.AvalBim4 = "4º Bimestre - " + aval4.DE_AVAL;
                        }
                    }

                    bsReport.Add(at);
                }

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        public class AvalGeral
        {
            public string aval { get; set; }
        }

        public class LegendaConceito
        {
            public string deConce { get; set; }
            public string coSigla { get; set; }
        }

        #region Classe Boletim Aluno
        public class BoletimAlunoConceitual
        {
            public int coAlu { get; set; }
            public int coModu { get; set; }
            public int coCur { get; set; }
            public int coTur { get; set; }
            public string coAno { get; set; }
            public int CodigoAluno { get; set; }
            public int NireAluno { get; set; }
            public DateTime? NasctoAluno { get; set; }
            public string NomeAluno { get; set; }
            public byte[] ImagemAluno { get; set; }
            public string Sexo
            {
                get
                {
                    return (this.SexoReceb == "F" ? "FEM" : "MAS");
                }
            }
            public string SexoReceb { get; set; }
            public string SexoE
            {
                get
                {
                    return this.Sexo == "MAS" ? "Masculino" : "Feminino";
                }
            }
            public string ResponsavelAluno { get; set; }
            public string Ano { get; set; }
            public string Unidade { get; set; }
            public string Modalidade { get; set; }
            public string Serie { get; set; }
            public string Turma { get; set; }
            public string Turno
            {
                get
                {
                    return(this.TurnoReceb == "M" ? "Manhã" : this.TurnoReceb == "N" ? "Noite" : "Tarde");
                }
            }
            public string TurnoReceb { get; set; }
            public string Disciplina { get; set; }
            public string MB1 { get; set; }
            public string MB2 { get; set; }
            public string MB3 { get; set; }
            public string MB4 { get; set; }

            public string AvalGeral { get; set; }
            public string AvalGeralRefeita { get; set; }
            public string AvalBim1 { get; set; }
            public string AvalBim2 { get; set; }
            public string AvalBim3 { get; set; }
            public string AvalBim4 { get; set; }

            public int? id_hist_val { get; set; }
            public string MediaB1 
            {
                get
                {
                    string m = "";
                    switch (this.MB1)
                    {
                        case "O":
                            m = "S";
                            break;
                        case "B":
                            m = "AV";
                            break;
                        case "R":
                            m = "N";
                            break;
                        case "I":
                            m = "AN";
                            break;
                        case "Z":
                            m = "NA";
                            break;
                    }
                    return m;
                }
            }
            public string MediaB2
            {
                get
                {
                    string m = "";
                    switch (this.MB2)
                    {
                        case "O":
                            m = "S";
                            break;
                        case "B":
                            m = "AV";
                            break;
                        case "R":
                            m = "N";
                            break;
                        case "I":
                            m = "AN";
                            break;
                        case "Z":
                            m = "NA";
                            break;
                    }
                    return m;
                }
            }
            public string MediaB3
            {
                get
                {
                    string m = "";
                    switch (this.MB3)
                    {
                        case "O":
                            m = "S";
                            break;
                        case "B":
                            m = "AV";
                            break;
                        case "R":
                            m = "N";
                            break;
                        case "I":
                            m = "AN";
                            break;
                        case "Z":
                            m = "NA";
                            break;
                    }
                    return m;
                }
            }
            public string MediaB4
            {
                get
                {
                    string m = "";
                    switch (this.MB4)
                    {
                        case "O":
                            m = "S";
                            break;
                        case "B":
                            m = "AV";
                            break;
                        case "R":
                            m = "N";
                            break;
                        case "I":
                            m = "AN";
                            break;
                        case "Z":
                            m = "NA";
                            break;
                    }
                    return m;
                }
            }
            public int? FaltaB1 { get; set; }
            public int? FaltaB2 { get; set; }
            public int? FaltaB3 { get; set; }
            public int? FaltaB4 { get; set; }

            public string AlunoDesc
            {
                get
                {
                    return this.NireAluno.ToString() + " - " + this.NomeAluno;
                }
            }

            public string DescFichaReceb
            {
                get
                {
                    return "Declaro ter recebido nesta data __/__/____ a ficha de rendimento escolar do(a) aluno(a) " + this.NomeAluno.ToUpper() + ". Registro nr. " +
                               this.NireAluno.ToString() + " matriculado(a) na modalidade " + this.Modalidade.ToUpper() + ", série " + this.Serie.ToUpper() + " e turma " + this.Turma.ToUpper() +
                               " da " + this.Unidade.ToUpper() + " - Responsável do aluno(a): " + (this.ResponsavelAluno != null ? this.ResponsavelAluno.ToUpper() : "XXXXX") + ".";
                }
            }

            public string DescModalidade
            {
                get
                {
                    return "Modalidade: " + this.Modalidade.ToUpper();
                }
            }

            public string DescSerie
            {
                get
                {
                    return "Série: " + this.Serie.ToUpper() + " - Turma: " + this.Turma.ToUpper() + " - Turno: " + this.Turno.ToUpper();
                }
            }
        }

        #endregion
    }
}