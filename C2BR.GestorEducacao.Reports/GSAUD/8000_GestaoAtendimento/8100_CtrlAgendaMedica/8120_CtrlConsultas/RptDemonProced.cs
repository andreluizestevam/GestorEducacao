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
using DevExpress.XtraCharts;
using System.Data.Objects;

namespace C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8120_CtrlConsultas
{
    public partial class RptDemonProced : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptDemonProced()
        {
            InitializeComponent();
        }

        public int InitReport(
              string parametros,
              string infos,
              int coEmpLogado,
              int coEmp,
              int Grupo,
              int subGrupo,
              string dataIni,
              string dataFim,
              string ordem,
              bool crescente,
              string NomeFuncionalidade
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
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(coEmpLogado);
                if (header == null)
                    return 0;
                DateTime DataInicial = Convert.ToDateTime(dataIni);
                DateTime DataFinal = Convert.ToDateTime(dataFim);

                DateTime DataAtras = DataInicial.AddYears(-1);
                // Setar o header do relatorio
                this.BaseInit(header);

                var ress = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                            join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs174.CO_EMP equals tb25.CO_EMP
                            join tbs389 in TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros() on tbs174.ID_AGEND_HORAR equals tbs389.TBS174_AGEND_HORAR.ID_AGEND_HORAR
                            join tbs386 in TBS386_ITENS_PLANE_AVALI.RetornaTodosRegistros() on tbs389.TBS386_ITENS_PLANE_AVALI.ID_ITENS_PLANE_AVALI equals tbs386.ID_ITENS_PLANE_AVALI
                            join tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros() on tbs386.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE equals tbs356.ID_PROC_MEDI_PROCE
                            join tbs353 in TBS353_VALOR_PROC_MEDIC_PROCE.RetornaTodosRegistros() on tbs356.ID_PROC_MEDI_PROCE equals tbs353.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE
                            where (coEmp != 0 ? tbs174.CO_EMP == coEmp : 0 == 0)
                            && (Grupo != 0 ? tbs356.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO == Grupo : 0 == 0)
                            && (subGrupo != 0 ? tbs356.TBS355_PROC_MEDIC_SGRUP.ID_PROC_MEDIC_SGRUP == subGrupo : 0 == 0)
                            select new
                            {
                                PROCED = tbs356.CO_PROC_MEDI + "   " + tbs356.NM_REDUZ_PROC_MEDI,
                                CoProced = tbs356.ID_PROC_MEDI_PROCE,
                                SITU_AGEND = tbs174.CO_SITUA_AGEND_HORAR,
                                VALOR = tbs353.VL_RESTI,
                                DT_AGEND = tbs174.DT_AGEND_HORAR,
                                CO_ALU = (tbs174.CO_ALU.HasValue ? tbs174.CO_ALU : 0),
                                ABERTO = (tbs174.CO_ALU.HasValue ? false : true)
                            }).ToList();

                if (ress.Count == 0)
                    return -1;

                bsReport.Clear();
                List<Relatorio> lstRelat = new List<Relatorio>();
                foreach (var b in ress.Where(x => (x.DT_AGEND >= DataInicial && x.DT_AGEND <= DataFinal)).OrderBy(x => x.PROCED))
                {
                    Relatorio proced = new Relatorio();

                    proced.NomeProced = b.PROCED;
                    proced.CodigoProced = b.CoProced;

                    proced.QTUsr = 0;
                    proced.QTAberto = 0;
                    proced.QTAnt = 0;
                    proced.QTCance = 0;
                    proced.QTExec = 0;
                    proced.QTPlan = 0;

                    proced.ValorAberto = 0;
                    proced.ValorAnt = 0;
                    proced.ValorCance = 0;
                    proced.ValorExec = 0;
                    proced.ValorPlan = 0;

                    foreach (var c in ress.Where(z => z.CoProced == b.CoProced))
                    {
                        switch (c.SITU_AGEND)
                        {
                            case "R":
                                proced.QTExec++;
                                proced.ValorExec += (c.VALOR.HasValue ? c.VALOR.Value : 0);
                                break;

                            case "C":
                                proced.QTCance++;
                                proced.ValorCance += (c.VALOR.HasValue ? c.VALOR.Value : 0);
                                break;

                            default:
                                proced.QTAberto++;
                                proced.ValorAberto += (c.VALOR.HasValue ? c.VALOR.Value : 0);
                                break;
                        }

                        proced.QTPlan++;
                        proced.ValorPlan += (c.VALOR.HasValue ? c.VALOR.Value : 0);
                    }

                    foreach (var d in ress.Where(n => (n.DT_AGEND >= DataAtras && n.DT_AGEND <= DataInicial) && (n.CoProced == b.CoProced)))
                    {
                        proced.QTAnt++;
                        proced.ValorAnt += (d.VALOR.HasValue ? d.VALOR.Value : 0);
                    }

                    foreach (var e in ress.Where(m => (m.DT_AGEND >= DataInicial && m.DT_AGEND <= DataFinal) && (m.CoProced == b.CoProced)).DistinctBy(m => m.CO_ALU))
                    {
                        if (e.CO_ALU != 0)
                            proced.QTUsr++;
                    }

                    lstRelat.Add(proced);
                }

                lstRelat = lstRelat.DistinctBy(x => x.CodigoProced).ToList();

                //Ordena de acordo com o filtro
                switch (ordem)
                {
                    case "QA":
                        if (crescente)
                            lstRelat = lstRelat.OrderBy(x => x.QTAnt).ToList();
                        else
                            lstRelat = lstRelat.OrderByDescending(x => x.QTAnt).ToList();
                        break;

                    case "VA":
                        if (crescente)
                            lstRelat = lstRelat.OrderBy(x => x.ValorAnt).ToList();
                        else
                            lstRelat = lstRelat.OrderByDescending(x => x.ValorAnt).ToList();
                        break;

                    case "QP":
                        if (crescente)
                            lstRelat = lstRelat.OrderBy(x => x.QTPlan).ToList();
                        else
                            lstRelat = lstRelat.OrderByDescending(x => x.QTPlan).ToList();
                        break;

                    case "VP":
                        if (crescente)
                            lstRelat = lstRelat.OrderBy(x => x.ValorPlan).ToList();
                        else
                            lstRelat = lstRelat.OrderByDescending(x => x.ValorPlan).ToList();
                        break;

                    case "QE":
                        if (crescente)
                            lstRelat = lstRelat.OrderBy(x => x.QTExec).ToList();
                        else
                            lstRelat = lstRelat.OrderByDescending(x => x.QTExec).ToList();
                        break;

                    case "VE":
                        if (crescente)
                            lstRelat = lstRelat.OrderBy(x => x.ValorExec).ToList();
                        else
                            lstRelat = lstRelat.OrderByDescending(x => x.ValorExec).ToList();
                        break;

                    case "QC":
                        if (crescente)
                            lstRelat = lstRelat.OrderBy(x => x.QTCance).ToList();
                        else
                            lstRelat = lstRelat.OrderByDescending(x => x.QTCance).ToList();
                        break;

                    case "VC":
                        if (crescente)
                            lstRelat = lstRelat.OrderBy(x => x.ValorCance).ToList();
                        else
                            lstRelat = lstRelat.OrderByDescending(x => x.ValorCance).ToList();
                        break;

                    case "QB":
                        if (crescente)
                            lstRelat = lstRelat.OrderBy(x => x.QTAberto).ToList();
                        else
                            lstRelat = lstRelat.OrderByDescending(x => x.QTAberto).ToList();
                        break;

                    case "VB":
                        if (crescente)
                            lstRelat = lstRelat.OrderBy(x => x.ValorAberto).ToList();
                        else
                            lstRelat = lstRelat.OrderByDescending(x => x.ValorAberto).ToList();
                        break;
                }

                lstRelat = lstRelat.ToList();

                //Adiciona ao DataSource do Relatório
                foreach (var item in lstRelat)
                {
                    bsReport.Add(item);
                }
                return 1;
            }
            catch { return 0; }
        }

        public class Relatorio
        {
            public string NomeProced { get; set; }
            public int CodigoProced { get; set; }

            public int QTUsr { get; set; }
            public int QTAnt { get; set; }
            public decimal ValorAnt { get; set; }
            public int QTPlan { get; set; }
            public decimal ValorPlan { get; set; }
            public int QTExec { get; set; }
            public decimal ValorExec { get; set; }
            public int QTCance { get; set; }
            public decimal ValorCance { get; set; }
            public int QTAberto { get; set; }
            public decimal ValorAberto { get; set; }
        }
    }
}
