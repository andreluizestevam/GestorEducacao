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
    public partial class RptBoletEscolMod5 : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptBoletEscolMod5()
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
                              int strP_BIMESTRE,
                              bool strP_FLA_FOTO
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
                           && tb43.ID_MATER_AGRUP == null
                               //Listar as disciplinas diferentes da classificação "Não se Aplica"
                           && cadMat.CO_CLASS_BOLETIM != 4
                           select new BoletimAluno
                           {
                               NomeFanEmp = tb25.NO_FANTAS_EMP,
                               NomeResp = alu.TB108_RESPONSAVEL.NO_RESP,
                               Foto = strP_FLA_FOTO ? alu.Image.ImageStream : null,
                               NomeAluno = alu.NO_ALU.ToUpper(),
                               NireAluno = alu.NU_NIRE,
                               CodigoAluno = alu.NU_NIRE,
                               Sexo = alu.CO_SEXO_ALU == "M" ? "MASCULINO" : "FEMININO",
                               Ano = tb079.CO_ANO_REF.Trim(),
                               Modalidade = tb079.TB44_MODULO.DE_MODU_CUR.ToUpper(),
                               Serie = ser.NO_CUR.ToUpper(),
                               Turma = tur.TB129_CADTURMAS.NO_TURMA.ToUpper(),
                               Turno = tur.CO_PERI_TUR == "M" ? "MANHÃ" : tur.CO_PERI_TUR == "N" ? "NOITE" : "TARDE",
                               Disciplina = cadMat.NO_RED_MATERIA.ToUpper(),
                               Bimestre = strP_BIMESTRE,
                               MediaB1 = tb079.VL_NOTA_BIM1,
                               MediaB2 = strP_BIMESTRE >= 2 ? tb079.VL_NOTA_BIM2 : null,
                               MediaB3 = strP_BIMESTRE >= 3 ? tb079.VL_NOTA_BIM3 : null,
                               MediaB4 = strP_BIMESTRE >= 4 ? tb079.VL_NOTA_BIM4 : null, 
                               MediaFinal = tb079.VL_MEDIA_FINAL,
                               MediaAnual = tb079.VL_MEDIA_ANUAL,
                               FaltaB1 = tb079.QT_FALTA_BIM1,
                               FaltaB2 = strP_BIMESTRE >= 2 ? tb079.QT_FALTA_BIM2 : null,
                               FaltaB3 = strP_BIMESTRE >= 3 ? tb079.QT_FALTA_BIM3 : null,
                               FaltaB4 = strP_BIMESTRE >= 4 ? tb079.QT_FALTA_BIM4 : null,
                               AulaB1 = tb43.QT_AULAS_BIM1,
                               AulaB2 = strP_BIMESTRE >= 2 ? tb43.QT_AULAS_BIM2 : null,
                               AulaB3 = strP_BIMESTRE >= 3 ? tb43.QT_AULAS_BIM3 : null,
                               AulaB4 = strP_BIMESTRE >= 4 ? tb43.QT_AULAS_BIM4 : null,

                               AprovMateria = tb079.CO_STA_APROV_MATERIA != null && (tb079.VL_NOTA_BIM1 != null && tb079.VL_NOTA_BIM2 != null &&
                               tb079.VL_NOTA_BIM3 != null && tb079.VL_NOTA_BIM4 != null) ? tb079.CO_STA_APROV_MATERIA == "A" ? "APRV" : "REPR" : "-",
                               Resultado = matr.CO_STA_APROV != null ? (matr.CO_STA_APROV == "A" && (matr.CO_STA_APROV_FREQ == "A" || matr.CO_STA_APROV_FREQ == null) ? "APROVADO" : "REPROVADO") : "CURSANDO"                               
                           }
                  );

                var res = lst.OrderBy(r => r.NomeAluno).ThenBy(r => r.Disciplina).ToList();

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
            public byte[] Foto { get; set; }
            public string NomeResp { get; set; }
            public int CodigoAluno { get; set; }
            public int NireAluno { get; set; }
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
                    return "Modalidade: " + Modalidade + " - Série: " + Serie + " - Turma: " + Turma;
                }
            }
            public string Disciplina { get; set; }
            public int Bimestre { get; set; }
            public decimal? MediaB1 { get; set; }
            public decimal? MediaB2 { get; set; }
            public decimal? MediaB3 { get; set; }
            public decimal? MediaB4 { get; set; }
            public int? FaltaB1 { get; set; }
            public int? FaltaB2 { get; set; }
            public int? FaltaB3 { get; set; }
            public int? FaltaB4 { get; set; }
            public int? AulaB1 { get; set; }
            public int? AulaB2 { get; set; }
            public int? AulaB3 { get; set; }
            public int? AulaB4 { get; set; }
            public string Resultado { get; set; }
            public string AprovMateria { get; set; }

            public string DescNIRE
            {
                get
                {
                    return this.NireAluno.ToString().PadLeft(9,'0');
                }
            }

            public decimal? MediaFinal { get; set; }
            public decimal? MediaAnual { get; set; }

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
