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

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3030_CtrlMonitoria._3032_RptExtFinanceiroMonitoria
{
    public partial class RptFinanceiroPorDisciplina : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptFinanceiroPorDisciplina()
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

                var res = (from tb189 in TB189_AGEND_MONIT_PROFE.RetornaTodosRegistros()
                           join tb188 in TB188_MONIT_CURSO_PROFE.RetornaTodosRegistros() on tb189.TB188_MONIT_CURSO_PROFE.ID_MONIT_CURSO_PROFE equals tb188.ID_MONIT_CURSO_PROFE
                           join tb44 in TB44_MODULO.RetornaTodosRegistros() on tb188.CO_MODU_CUR equals tb44.CO_MODU_CUR
                           join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb188.CO_CUR equals tb01.CO_CUR
                           join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tb188.CO_TUR equals tb129.CO_TUR
                           join coM in TB02_MATERIA.RetornaTodosRegistros() on tb188.CO_MAT equals coM.ID_MATERIA
                           join m in TB107_CADMATERIAS.RetornaTodosRegistros() on coM.ID_MATERIA equals m.ID_MATERIA
                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tb189.CO_COL equals tb03.CO_COL


                           where (modalidade != 0 ? tb188.CO_MODU_CUR == modalidade : 0 == 0)
                              && (serieCurso != 0 ? tb188.CO_CUR == serieCurso : 0 == 0)
                              && (Turma != 0 ? tb188.CO_TUR == Turma : 0 == 0)
                              && (materia != 0 ? tb188.CO_MAT == materia : 0 == 0)
                              && (professor != 0 ? tb188.CO_COL == professor : 0 == 0)
                              && (situ != "0" ? tb189.FL_SITUA == situ : 0 == 0)
                              && ((tb189.DT_MONITORIA >= dataIni1) && (tb189.DT_MONITORIA <= dataFim1))

                           select new ExtratoMonitoriaPorDisciplina
                           {
                               Professor = tb03.NO_COL,
                               Materia = m.NO_MATERIA,
                               modalidade = tb44.DE_MODU_CUR,
                               serieCurso = tb01.NO_CUR,
                               turma = tb129.NO_TURMA,
                               dt = tb189.DT_MONITORIA,
                               hrini = tb189.HR_INICIO,
                               hrFim = tb189.HR_FIM,
                               vlHora = tb188.VL_HORA,

                           }).OrderBy(m => m.modalidade).ThenBy(n => n.serieCurso).ThenBy(y => y.turma).Distinct().ToList();

                #endregion


                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (ExtratoMonitoriaPorDisciplina at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        public class ExtratoMonitoriaPorDisciplina
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

            //Informações Financeiras
            public decimal? vlHora { get; set; }
            public decimal vlHoraV
            {
                get
                {
                    if ((this.vlHora == null) || (this.vlHora == 0))
                    {
                        return 0;
                    }

                    else
                    {
                        return vlHora.Value;
                    }
                }
            }
            public string vHoraRep
            {
                get
                {
                    if ((this.vlHora == null) || (this.vlHora == 0))
                    { return "-"; }

                    else
                    { return this.vlHora.ToString(); }
                }
            }
            public TimeSpan hrini { get; set; }
            public TimeSpan hrFim { get; set; }
            public string tempoAP
            {
                get
                {
                    TimeSpan tp = this.hrFim - this.hrini;
                    return tp.ToString().Substring(0, 5);
                }
            }
            public decimal vlTotal
            {
                get
                {
                    TimeSpan tp = this.hrFim - this.hrini;
                    int th = tp.Hours;
                    int tm = tp.Minutes;

                    return ((th * this.vlHoraV) + ((((100 * tm) / 60) / 100) * this.vlHoraV));
                }
            }
        }
    }
}

