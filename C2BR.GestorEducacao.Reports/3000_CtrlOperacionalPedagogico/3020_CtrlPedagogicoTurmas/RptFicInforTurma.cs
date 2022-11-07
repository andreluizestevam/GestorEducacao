using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Text.RegularExpressions;
using C2BR.GestorEducacao.Reports.Helper;

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3020_CtrlPedagogicoTurmas
{
    public partial class RptFicInforTurma : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptFicInforTurma()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                               int codEmp,
                               int codEmpRef,
                               int codMod,
                               int codCurso,
                               int codTur,
                               int coAnoRef,
                               string coTipoHorario,
                               string infos)
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(codEmp);
                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Grade Horário

                var resultado = (from tb05 in ctx.TB05_GRD_HORAR
                                 join tb131 in ctx.TB131_TEMPO_AULA on tb05.NR_TEMPO equals tb131.NR_TEMPO
                                 where tb131.CO_CUR == tb05.CO_CUR && tb131.CO_EMP == tb05.CO_EMP && tb131.CO_MODU_CUR == tb05.CO_MODU_CUR
                                  && tb05.CO_EMP == codEmpRef
                                  && tb05.CO_CUR == codCurso
                                  && tb05.CO_MODU_CUR == codMod
                                  && tb05.CO_TUR == codTur
                                  && tb05.CO_ANO_GRADE == coAnoRef
                                  && tb05.TP_TURNO == tb131.TP_TURNO
                                  && (coTipoHorario != "" ? tb05.TP_HORAR_AGEND == coTipoHorario : true)
                                 select new
                                 {
                                     HrInicio = tb131.HR_INICIO,
                                     HrFim = tb131.HR_TERMI,
                                     Tempo = tb131.NR_TEMPO,
                                     Turno = tb131.TP_TURNO,
                                     TipoHorario = (tb05.TP_HORAR_AGEND.Equals("DEP") ? "Dependência" : (tb05.TP_HORAR_AGEND.Equals("REG") ? "Regular" :
                                        (tb05.TP_HORAR_AGEND.Equals("REC") ? "Recuperação" : (tb05.TP_HORAR_AGEND.Equals("REF") ? "Reforço" :
                                        (tb05.TP_HORAR_AGEND.Equals("ERE") ? "Ensino Remoto" : "-")))))
                                 }).ToList().OrderBy(p => p.Tempo).Distinct();

                // Erro: não encontrou registros
                if (resultado.ToList().Count == 0)
                    return -1;

                var lstGradeHorario = new List<GradeHorario>();
                System.Globalization.CultureInfo culInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
                string strLegenda = "(";

                foreach (var item in resultado)
                {
                    GradeHorario g = new GradeHorario();
                    g.HrFim = item.HrFim;
                    g.HrInicio = item.HrInicio;
                    g.Tempo = item.Tempo;
                    g.TipoHorario = item.TipoHorario;
                    g.Disciplina1 = "*****";
                    g.Disciplina2 = "*****";
                    g.Disciplina3 = "*****";
                    g.Disciplina4 = "*****";
                    g.Disciplina5 = "*****";
                    g.Disciplina6 = "*****";
                    g.Disciplina7 = "*****";
                        
                    var disc = (from tb05 in ctx.TB05_GRD_HORAR
                                join tb02 in ctx.TB02_MATERIA on tb05.CO_MAT equals tb02.CO_MAT
                                join tb107 in ctx.TB107_CADMATERIAS on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                where tb05.NR_TEMPO == item.Tempo && tb05.CO_EMP == codEmpRef
                                  && tb05.CO_CUR == codCurso
                                  && tb05.CO_MODU_CUR == codMod
                                  && tb05.CO_TUR == codTur
                                  && tb05.CO_ANO_GRADE == coAnoRef
                                  && tb05.TP_TURNO == item.Turno
                                  && (coTipoHorario != "" ? tb05.TP_HORAR_AGEND == coTipoHorario : true)
                                select new
                                {
                                    DiaSemana = tb05.CO_DIA_SEMA_GRD,
                                    Disciplina = tb107.NO_MATERIA,
                                    SiglaDisciplina = tb107.NO_SIGLA_MATERIA
                                }).ToList();

                    if (disc.Count > 0)
                    {
                        foreach (var iDisc in disc)
                        {
                            if (iDisc.DiaSemana == 0)
                            {
                                g.Disciplina1 = iDisc.SiglaDisciplina.ToUpper();
                                if (!strLegenda.Contains(iDisc.SiglaDisciplina.ToUpper()))
	                            {
                                    strLegenda = strLegenda + " " + iDisc.SiglaDisciplina.ToUpper() + " - " + culInfo.TextInfo.ToTitleCase(iDisc.Disciplina.ToLower()) + " / ";
	                            }                                                                
                            }
                            else if (iDisc.DiaSemana == 1)
                            {
                                g.Disciplina2 = iDisc.SiglaDisciplina.ToUpper();
                                if (!strLegenda.Contains(iDisc.SiglaDisciplina.ToUpper()))
                                {
                                    strLegenda = strLegenda + " " + iDisc.SiglaDisciplina.ToUpper() + " - " + culInfo.TextInfo.ToTitleCase(iDisc.Disciplina.ToLower()) + " / ";
                                }
                            }
                            else if (iDisc.DiaSemana == 2)
                            {
                                g.Disciplina3 = iDisc.SiglaDisciplina.ToUpper();
                                if (!strLegenda.Contains(iDisc.SiglaDisciplina.ToUpper()))
                                {
                                    strLegenda = strLegenda + " " + iDisc.SiglaDisciplina.ToUpper() + " - " + culInfo.TextInfo.ToTitleCase(iDisc.Disciplina.ToLower()) + " / ";
                                }
                            }
                            else if (iDisc.DiaSemana == 3)
                            {
                                g.Disciplina4 = iDisc.SiglaDisciplina.ToUpper();
                                if (!strLegenda.Contains(iDisc.SiglaDisciplina.ToUpper()))
                                {
                                    strLegenda = strLegenda + " " + iDisc.SiglaDisciplina.ToUpper() + " - " + culInfo.TextInfo.ToTitleCase(iDisc.Disciplina.ToLower()) + " / ";
                                }
                            }
                            else if (iDisc.DiaSemana == 4)
                            {
                                g.Disciplina5 = iDisc.SiglaDisciplina.ToUpper();
                                if (!strLegenda.Contains(iDisc.SiglaDisciplina.ToUpper()))
                                {
                                    strLegenda = strLegenda + " " + iDisc.SiglaDisciplina.ToUpper() + " - " + culInfo.TextInfo.ToTitleCase(iDisc.Disciplina.ToLower()) + " / ";
                                }
                            }
                            else if (iDisc.DiaSemana == 5)
                            {
                                g.Disciplina6 = iDisc.SiglaDisciplina.ToUpper();
                                if (!strLegenda.Contains(iDisc.SiglaDisciplina.ToUpper()))
                                {
                                    strLegenda = strLegenda + " " + iDisc.SiglaDisciplina.ToUpper() + " - " + culInfo.TextInfo.ToTitleCase(iDisc.Disciplina.ToLower()) + " / ";
                                }
                            }
                            else if (iDisc.DiaSemana == 6)
                            {
                                g.Disciplina7 = iDisc.SiglaDisciplina.ToUpper();
                                if (!strLegenda.Contains(iDisc.SiglaDisciplina.ToUpper()))
                                {
                                    strLegenda = strLegenda + " " + iDisc.SiglaDisciplina.ToUpper() + " - " + culInfo.TextInfo.ToTitleCase(iDisc.Disciplina.ToLower()) + " / ";
                                }
                            }
                        }
                    }

                    lstGradeHorario.Add(g);
                }

                this.lblLegenda.Text = strLegenda + " )";
                var res = lstGradeHorario.ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (GradeHorario at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Grade de Horário do Relatorio

        public class GradeHorario
        {
            public string HrInicio { get; set; }
            public string HrFim { get; set; }
            public string Turno { get; set; }
            public int Tempo { get; set; }
            public string Disciplina1 { get; set; }
            public string Disciplina2 { get; set; }
            public string Disciplina3 { get; set; }
            public string Disciplina4 { get; set; }
            public string Disciplina5 { get; set; }
            public string Disciplina6 { get; set; }
            public string Disciplina7 { get; set; }
            public string TipoHorario { get; set; }

            public string TempoAulaDesc
            {
                get
                {
                    return this.Tempo.ToString() + "º Tempo - " + this.HrInicio + " / " + this.HrFim;
                }
            }
        }

        #endregion
    }
}
