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

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3900_CtrlEncerramentoLetivo
{
    public partial class RptGradeNotasBimestral : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptGradeNotasBimestral()
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
                              int strP_CO_MAT
                              )
        {
            try
            {
                #region Setar o Header e as Labels
                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = "(" + parametros + ")";

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(coEmp);

                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion
                //Conversão
                int intP_CO_ANO_MES_MAT = int.Parse(strP_CO_ANO_MES_MAT);
                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Grade aluno

                var lst = (from tb079 in ctx.TB079_HIST_ALUNO
                           join alu in ctx.TB07_ALUNO on tb079.CO_ALU equals alu.CO_ALU
                           join mat in ctx.TB02_MATERIA on tb079.CO_MAT equals mat.CO_MAT
                           join cadMat in ctx.TB107_CADMATERIAS on mat.ID_MATERIA equals cadMat.ID_MATERIA
                           join tb25 in ctx.TB25_EMPRESA on tb079.CO_EMP equals tb25.CO_EMP
                           join tur in ctx.TB06_TURMAS on tb079.CO_TUR equals tur.CO_TUR
                           join tb43 in ctx.TB43_GRD_CURSO on tb079.CO_MAT equals tb43.CO_MAT
                           join tb08 in ctx.TB08_MATRCUR on tb079.CO_ALU equals tb08.CO_ALU
                           where tb079.CO_MODU_CUR == strP_CO_MODU_CUR && tb079.CO_CUR == strP_CO_CUR && tb079.CO_TUR == strP_CO_TUR
                           && (tb079.CO_ANO_REF == strP_CO_ANO_MES_MAT)
                           && (strP_CO_MAT != 0 ? tb079.CO_MAT == strP_CO_MAT : 0 == 0)
                           && tb079.CO_EMP == strP_CO_EMP_REF

                           && tb08.CO_ANO_MES_MAT == tb079.CO_ANO_REF
                           && tb08.TB44_MODULO.CO_MODU_CUR == tb079.CO_MODU_CUR
                           && tb08.CO_CUR == tb079.CO_CUR
                           && tb43.CO_EMP == tb079.CO_EMP
                           && tb43.CO_CUR == tb079.CO_CUR
                           && tb43.CO_ANO_GRADE == tb079.CO_ANO_REF
                           && tb43.CO_MAT == tb079.CO_MAT
                            //Mostrará apenas as agrupadoras, e não mais as associadas
                           && tb43.ID_MATER_AGRUP == null

                           && cadMat.CO_CLASS_BOLETIM != 4
                           select new GradeNotas
                           {
                               NomeAluno = alu.NO_ALU.ToUpper(),
                               NireAluno_R = alu.NU_NIRE,
                               Disciplina = cadMat.NO_MATERIA.ToUpper(),
                               MediaB1 = tb079.VL_NOTA_BIM1_ORI,
                               MediaB2 = tb079.VL_NOTA_BIM2_ORI,
                               MediaB3 = tb079.VL_NOTA_BIM3_ORI,
                               MediaB4 = tb079.VL_NOTA_BIM4_ORI,
                               MediaRecuSem1 = tb079.VL_RECU_SEM1,
                               MediaRecuSem2 = tb079.VL_RECU_SEM2,
                               MediaSemestre1 = tb079.VL_NOTA_BIM1_ORI != null && tb079.VL_NOTA_BIM2_ORI != null ? (decimal?)((tb079.VL_NOTA_BIM1_ORI ?? 0) + (tb079.VL_NOTA_BIM2_ORI ?? 0)) / 2 : null,
                               MediaSemestre2 = tb079.VL_NOTA_BIM3_ORI != null && tb079.VL_NOTA_BIM4_ORI != null ? (decimal?)((tb079.VL_NOTA_BIM3_ORI ?? 0) + (tb079.VL_NOTA_BIM4_ORI ?? 0)) / 2 : null,
                               MediaFinal = tb079.VL_MEDIA_FINAL,

                               coAno = strP_CO_ANO_MES_MAT,
                               coCur = strP_CO_CUR,
                               coEmp = strP_CO_EMP_REF,
                               coMat = strP_CO_MAT,
                               coMod = strP_CO_MODU_CUR,
                               coTur = strP_CO_TUR,
                               tipoFlag = tur.CO_FLAG_RESP_TURMA,
                               situacao = (tb08.CO_SIT_MAT == "A" ? "MAT" : tb08.CO_SIT_MAT == "C" ? "CAN" : tb08.CO_SIT_MAT == "X" ? "TRF" :
                               tb08.CO_SIT_MAT == "T" ? "MT" : tb08.CO_SIT_MAT == "D" ? "DE" : tb08.CO_SIT_MAT == "I" ? "IN" :
                               tb08.CO_SIT_MAT == "J" ? "JU" : tb08.CO_SIT_MAT == "Y" ? "TC" : "SM"),
                               
                           }
                  );
                var res = lst.Distinct().OrderBy(r => r.NomeAluno).ThenBy(r => r.Disciplina).ToList();

                #region Parametros
                var medias = (from lista in lst
                              join cur in ctx.TB01_CURSO on new { empc = lista.coEmp, modc = lista.coMod, curc = lista.coCur }
                                equals new { empc = cur.CO_EMP, modc = cur.CO_MODU_CUR, curc = cur.CO_CUR } into resultado2
                              from cur in resultado2
                              select new
                             {
                                 lista.MediaB1
                                 ,
                                 lista.MediaB2
                                 ,
                                 lista.MediaB3
                                 ,
                                 lista.MediaB4
                                 ,
                                 lista.MediaSemestre1
                                 ,
                                 lista.MediaSemestre2
                                 ,
                                 valorMedia = cur.MED_FINAL_CUR
                             }
                                 );
                QtdBim1.Value = medias.Where(w => w.MediaB1 != null && w.valorMedia != null && w.MediaB1 < w.valorMedia).Count().ToString();
                QtdBim2.Value = medias.Where(w => w.MediaB2 != null && w.valorMedia != null && w.MediaB2 < w.valorMedia).Count().ToString();
                QtdBim3.Value = medias.Where(w => w.MediaB3 != null && w.valorMedia != null && w.MediaB3 < w.valorMedia).Count().ToString();
                QtdBim4.Value = medias.Where(w => w.MediaB4 != null && w.valorMedia != null && w.MediaB4 < w.valorMedia).Count().ToString();
                QtdSem1.Value = medias.Where(w => w.MediaSemestre1 != null && w.valorMedia != null && w.MediaSemestre1 < w.valorMedia).Count().ToString();
                QtdSem2.Value = medias.Where(w => w.MediaSemestre2 != null && w.valorMedia != null && w.MediaSemestre2 < w.valorMedia).Count().ToString();
                #endregion

                #endregion

                if (res.Count == 0)
                    return -1;
                // Seta os dados no DataSource do Relatorio
                bsReport.Clear();

                foreach (var at in res)
                {
                    //Conta quantos alunos existem na turma em questão
                    int qtA = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                               where tb08.CO_EMP == at.coEmp
                               && tb08.CO_CUR == at.coCur
                               && tb08.TB44_MODULO.CO_MODU_CUR == at.coMod
                               && tb08.CO_ANO_MES_MAT == at.coAno
                               && tb08.CO_TUR == at.coTur
                               select tb08).Count();
                    QtdAlunos.Value = qtA;

                    bsReport.Add(at);
                }

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe grade notas bimestral
        public class GradeNotas
        {
            public int NireAluno_R { get; set; }
            public string NireAluno
            {
                get
                {
                    return this.NireAluno_R.ToString().PadLeft(7, '0');
                }
            }
            public string situacao { get; set; }
            public string NomeAluno { get; set; }
            public string Disciplina { get; set; }
            public decimal? MediaB1 { get; set; }
            public decimal? MediaB2 { get; set; }
            public decimal? MediaRecuSem1 { get; set; }
            public decimal? MediaB3 { get; set; }
            public decimal? MediaB4 { get; set; }
            public decimal? MediaRecuSem2 { get; set; }
            public decimal? MediaFinal { get; set; }

            public string coAno { get; set; }
            public int coCur { get; set; }
            public int coEmp { get; set; }
            public int coMat { get; set; }
            public int coMod { get; set; }
            public int coTur { get; set; }
            public string tipoFlag { get; set; }
            public string DescSuperior
            {
                get
                {
                    return ("Disciplina: " + this.Disciplina.ToUpper() + " - " + "Professor(a): " + this.Professor.ToUpper());
                }
            }

            public string Professor 
            {
                get
                {
                    var ctx = GestorEntities.CurrentContext;
                    int codigoAno = int.Parse(this.coAno.Trim());
                    string nome = (
                                from rm in ctx.TB_RESPON_MATERIA
                                join col in ctx.TB03_COLABOR on rm.CO_COL_RESP equals col.CO_COL
                                where rm.CO_ANO_REF == codigoAno
                                && rm.CO_CUR == this.coCur
                                && rm.CO_EMP == this.coEmp
                                && tipoFlag == "S" ? rm.CO_MAT == this.coMat : 0 == 0
                                && rm.CO_MODU_CUR == this.coMod
                                && rm.CO_TUR == this.coTur
                                select col.NO_COL.ToUpper()
                                ).First().ToString().ToUpper();
                    return nome;

                }
            
            }


            public string DescNIRE
            {
                get
                {
                    return this.NireAluno.ToString().PadLeft(9, '0');
                }
            }

            public decimal? MediaSemestre1 { get; set; }

            public decimal? MediaSemestre2 { get; set; }

            
        }

        #endregion

    }
}
