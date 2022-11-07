using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.Reports.Helper;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Globalization;

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3030_CtrlMonitoria._3031_RptAgendaMonitoria
{
    public partial class RptAgendaMonitoriaProfessorXDisciplina : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptAgendaMonitoriaProfessorXDisciplina()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                        string infos,
                        int coEmp,
                        int modalidade,
                        int serieCurso,
                        int Turma,
                        int materia,
                        string dataIni,
                        string dataFim,
                        int professor,
                        string situ
                        )
        {


            try
            {
                #region Setar o Header e as Labels

                DateTime dataIni1;
                if (!DateTime.TryParse(dataIni, out dataIni1))
                {
                    return 0;
                }

                DateTime dataFim1;
                if (!DateTime.TryParse(dataFim, out dataFim1))
                {
                    return 0;
                }

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = C2BR.GestorEducacao.Reports.ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;

                lblParametros.Text = parametros;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;
                #region Query

                var res = ( from tb189 in TB189_AGEND_MONIT_PROFE.RetornaTodosRegistros()
                            join tb188 in TB188_MONIT_CURSO_PROFE.RetornaTodosRegistros() on tb189.TB188_MONIT_CURSO_PROFE.ID_MONIT_CURSO_PROFE equals tb188.ID_MONIT_CURSO_PROFE
                            join tb44 in TB44_MODULO.RetornaTodosRegistros() on tb188.CO_MODU_CUR equals tb44.CO_MODU_CUR
                            join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb188.CO_CUR equals tb01.CO_CUR
                            join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tb188.CO_TUR equals tb129.CO_TUR
                            join coM in TB02_MATERIA.RetornaTodosRegistros() on tb188.CO_MAT equals coM.ID_MATERIA
                            join m in TB107_CADMATERIAS.RetornaTodosRegistros() on coM.ID_MATERIA equals m.ID_MATERIA
                            join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tb189.CO_COL equals tb03.CO_COL

                            where (modalidade != 0 ? tb188.CO_MODU_CUR == modalidade : 0 == 0)
                            &&    (serieCurso != 0 ? tb188.CO_CUR == serieCurso : 0 == 0)
                            &&    (Turma != 0 ? tb188.CO_TUR == Turma : 0 == 0)
                            &&    (materia != 0 ? tb188.CO_MAT == materia : 0 == 0)
                            &&    (professor != 0 ? tb189.CO_COL == professor : 0 == 0)
                            &&    (situ != "0" ? tb189.FL_SITUA == situ : 0 == 0)
                            &&    ((tb189.DT_MONITORIA >= dataIni1) && (tb189.DT_MONITORIA <= dataFim1))

                           select new ExtratoProfessorXDisciplina
                           {
                               Professor = tb03.NO_COL,
                               Materia = m.NO_MATERIA,
                               modalidade = tb44.CO_SIGLA_MODU_CUR,
                               serieCurso = tb01.NO_CUR,
                               turma = tb129.CO_SIGLA_TURMA,
                               dt = tb189.DT_MONITORIA,
							   situacao = tb189.FL_SITUA,
							   hrini = tb189.HR_INICIO,
							   hrFim = tb189.HR_FIM,

                           }).OrderBy(m => m.dt).ThenBy(n => n.Professor).Distinct().ToList();

                #endregion


                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (ExtratoProfessorXDisciplina at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        public class ExtratoProfessorXDisciplina
        {
            public string Professor { get; set; }
            public string Materia { get; set; }
            public string ALunoCPF { get; set; }
            public string turma { get; set; }
            public string modalidade { get; set; }
            public string serieCurso { get; set; }
            public DateTime dt { get; set; }
            public string dtV
            {
                get
                {
                    return dt.ToString("dd/MM/yy");
                }
            }
            public TimeSpan hrini { get; set; }
            public TimeSpan hrFim { get; set; }
            public string horario
            {
                get
                {
                    return this.hrini.Hours.ToString().PadLeft(2, '0') + ":" + this.hrini.Minutes.ToString().PadLeft(2, '0')
                        + " - " + this.hrFim.Hours.ToString().PadLeft(2, '0') + ":" + this.hrFim.Minutes.ToString().PadLeft(2, '0');
                }
            }
            public string situacao { get; set; }
            public string situNo
            {
                get
                {
                    var res = "";
                    switch (this.situacao)
                    {
                        case "A":
                            res = "Em Aberto";
                            break;

                        case "C":
                            res = "Cancelado";
                            break;

                        case "R":
                            res = "Realizado";
                            break;

                        case "N":
                            res = "Não Realizado";
                            break;

                        case "S":
                            res = "Suspenso";
                            break;
                    }

                    return res;
                }
            }
            public string temaMonit { get; set; }
        }
    }
}

