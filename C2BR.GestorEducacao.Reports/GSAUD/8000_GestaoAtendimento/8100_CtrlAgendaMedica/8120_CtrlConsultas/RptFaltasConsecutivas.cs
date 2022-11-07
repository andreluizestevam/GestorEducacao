using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports.Helper;
using System.Globalization;
using C2BR.GestorEducacao.Reports;

namespace C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8120_CtrlConsultas
{
    public partial class RptFaltasConsecutivas : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptFaltasConsecutivas()
        {
            InitializeComponent();
        }

        public int InitReport(
              string parametros,
              string infos,
              int coEmp,
              int Local,
              int Operadora,
              int Plano,
              int Profissional,
              int Paciente,
              string dataIni,
              bool faltas,
              string NomeFuncionalidade,
              string Considerar
            )
        {
            try
            {
                // Seta as informaçoes do rodape do relatorio
                this.InfosRodape = infos;
                this.Parametros = parametros;
                // Instancia o header do relatorio

                if (NomeFuncionalidade == "")
                    lblTitulo.Text = "-";
                else
                    lblTitulo.Text = NomeFuncionalidade.ToUpper();

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;
                DateTime DataInical = Convert.ToDateTime(dataIni);
                // Setar o header do relatorio
                this.BaseInit(header);

                var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros().Where(a => a.DT_AGEND_HORAR == DataInical)
                           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                           join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tbs174.CO_DEPT equals tb14.CO_DEPTO
                           where (Paciente != 0 ? tb07.CO_ALU == Paciente : Paciente == 0)
                           && (Operadora != 0 ? tbs174.TB250_OPERA.ID_OPER.Equals(Operadora) : Operadora == 0)
                           && (Plano != 0 ? tbs174.TB251_PLANO_OPERA.ID_PLAN.Equals(Plano) : Plano == 0)
                           && (Profissional != 0 ? tbs174.CO_COL == Profissional : Profissional == 0)
                           && (Local != 0 ? tbs174.CO_DEPT == Local : Local == 0)
                           select new Relatorio
                           {
                               CodPac = tb07.CO_ALU,
                               Paciente = tb07.NO_ALU,
                               Responsavel = tb07.TB108_RESPONSAVEL.NO_APELIDO_RESP,
                               Telefone = (tb07.TB108_RESPONSAVEL != null ? tb07.TB108_RESPONSAVEL.NU_TELE_CELU_RESP : null),
                               Local = tb14.CO_SIGLA_DEPTO,

                               DataAgend = tbs174.DT_AGEND_HORAR,
                               HoraAgend = tbs174.HR_AGEND_HORAR,
                               Operadora = tbs174.TB250_OPERA.NM_SIGLA_OPER == null ? "-" : tbs174.TB250_OPERA.NM_SIGLA_OPER,
                               Plano = tbs174.TB251_PLANO_OPERA.NM_SIGLA_PLAN == null ? "-" : tbs174.TB251_PLANO_OPERA.NM_SIGLA_PLAN,
                           }).OrderByDescending(w => w.HoraAgend).DistinctBy(w => w.CodPac).ToList();

                if (res.Count == 0)
                    return -1;

                var ress = new List<Relatorio>();

                foreach (var i in res.OrderBy(x => x.HoraAgend).ThenBy(x => x.Paciente))
                {
                    var numAgends = 0;
                    var numFaltas = 0;

                    var agds = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                where tbs174.CO_ALU == i.CodPac
                                && tbs174.DT_AGEND_HORAR <= i.DataAgend
                                select tbs174).OrderByDescending(a => new { a.DT_AGEND_HORAR, a.HR_AGEND_HORAR }).ToList();

                    if (Considerar == "D")
                    {
                        agds = agds.DistinctBy(a => a.DT_AGEND_HORAR).ToList();
                    }

                    if (agds != null)
                    {
                        foreach (var a in agds)
                        {
                            if ((a.DT_AGEND_HORAR == i.DataAgend && TimeSpan.Parse(a.HR_AGEND_HORAR) <= TimeSpan.Parse(i.HoraAgend)) || a.DT_AGEND_HORAR < i.DataAgend)
                            {
                                numAgends++;

                                var dt = a.DT_AGEND_HORAR.ToShortDateString() + " " + a.HR_AGEND_HORAR;
                                var st = Funcoes.PrepararSituacao(a.CO_SITUA_AGEND_HORAR, a.FL_CONF_AGEND, a.FL_AGEND_ENCAM, a.FL_JUSTI_CANCE);
                                var tb03 = TB03_COLABOR.RetornaPeloCoCol(a.CO_COL.Value);
                                var pr = tb03 != null ? (!String.IsNullOrEmpty(tb03.NO_APEL_COL) ? tb03.NO_APEL_COL : tb03.NO_COL) : " - ";

                                if (pr.Length > 13)
                                    pr = pr.Substring(0, 10) + "...";

                                var resum = dt + "               " + pr + " - " + st;

                                if (numAgends == 1)
                                    i.AgAtual = resum;
                                else if (numAgends == 2)
                                    i.AgUltimo = resum;
                                else if (numAgends == 3)
                                    i.AgPenultimo = resum;
                                else if (numAgends == 4)
                                    i.AgAntePenultimo = resum;

                                if (numAgends > 1 && a.CO_SITUA_AGEND_HORAR == "C" && (a.FL_JUSTI_CANCE == "S" || a.FL_JUSTI_CANCE == "N"))
                                    numFaltas++;
                            }

                            if (numAgends == 4)
                                break;
                        }
                    }

                    if (!faltas || (faltas && numFaltas == 3))
                        ress.Add(i);
                }

                if (ress.Count == 0)
                    return -1;

                //Adiciona ao DataSource do Relatório
                bsReport.Clear();
                foreach (var item in ress)
                    bsReport.Add(item);
                return 1;

            }
            catch { return 0; }
        }

        public class Relatorio
        {
            public string Local { get; set; }
            public string Responsavel { get; set; }
            public string fone;
            public string Telefone
            {
                get
                {
                    return Funcoes.Format(fone, TipoFormat.Telefone);
                }
                set
                {
                    fone = value;
                }
            }
            public string Paciente { get; set; }
            public DateTime DataAgend { get; set; }
            public string HoraAgend { get; set; }
            public string Plano { get; set; }
            public string Operadora { get; set; }
            public int CodPac { get; set; }

            public string AgAtual { get; set; }
            public string AgUltimo { get; set; }
            public string AgPenultimo { get; set; }
            public string AgAntePenultimo { get; set; }
        }
    }
}
