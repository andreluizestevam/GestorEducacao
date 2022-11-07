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

namespace C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8120_CtrlConsultas
{
    public partial class RptEspecialidadesAgend : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptEspecialidadesAgend()
        {
            InitializeComponent();
        }

        public int InitReport(
              string parametros,
              string infos,
              int coEmp,
              int local,
              int Operadora,
              int Plano,
              int Profissional,
              int Paciente,
              string situacao,
              string dataIni,
              string dataFim,
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
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;
                DateTime DataInical = Convert.ToDateTime(dataIni);
                DateTime DataFinal = Convert.ToDateTime(dataFim);
                // Setar o header do relatorio
                this.BaseInit(header);

                var ress = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros().Where(a => a.DT_AGEND_HORAR >= DataInical && a.DT_AGEND_HORAR <= DataFinal)
                            join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                            join tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros() on tb07.TB108_RESPONSAVEL.CO_RESP equals tb108.CO_RESP
                            where (Operadora != 0 ? tbs174.TB250_OPERA.ID_OPER.Equals(Operadora) : Operadora == 0
                                   && Plano != 0 ? tbs174.TB251_PLANO_OPERA.ID_PLAN.Equals(Plano) : Plano == 0
                                   && Profissional != 0 ? tbs174.CO_COL == Profissional : Profissional == 0
                                   && Paciente != 0 ? tb07.CO_ALU == Paciente : 0 == 0
                                   && local != 0 ? tbs174.CO_DEPT == local : 0 == 0
                                   && situacao != "" ?
                                     situacao == "QCA" ? tbs174.CO_SITUA_AGEND_HORAR == "C" && (String.IsNullOrEmpty(tbs174.FL_JUSTI_CANCE) || tbs174.FL_JUSTI_CANCE == "C")
                                   : situacao == "QFA" ? tbs174.CO_SITUA_AGEND_HORAR == "C" && tbs174.FL_JUSTI_CANCE == "N"
                                   : situacao == "QFJ" ? tbs174.CO_SITUA_AGEND_HORAR == "C" && tbs174.FL_JUSTI_CANCE == "S"
                                   : situacao == "QPR" ? tbs174.CO_SITUA_AGEND_HORAR == "A" && tbs174.FL_CONF_AGEND == "S" && (tbs174.FL_AGEND_ENCAM == "N" || tbs174.FL_AGEND_ENCAM == null)
                                   : situacao == "QEN" ? tbs174.CO_SITUA_AGEND_HORAR == "A" && tbs174.FL_CONF_AGEND == "S" && tbs174.FL_AGEND_ENCAM == "S"
                                   : situacao == "QAT" ? tbs174.CO_SITUA_AGEND_HORAR == "R" && tbs174.FL_CONF_AGEND == "S" && tbs174.FL_AGEND_ENCAM == "S"
                            /*QIN*/: tbs174.CO_SITUA_AGEND_HORAR == "R" && tbs174.FL_CONF_AGEND == "N"
                                   : 0 == 0)

                            select new Relatorio
                            {
                                coAlu = tb07.CO_ALU,
                                Paciente = tb07.NO_ALU.ToUpper(),
                                Nascimento = tb07.DT_NASC_ALU,
                                Responsavel = tb108.NO_RESP.Length > 25 ? tb108.NO_RESP.Substring(0, 20).ToUpper() + "..." : tb108.NO_RESP.ToUpper(),
                                Operadora = tbs174.TB250_OPERA != null ? (!String.IsNullOrEmpty(tbs174.TB250_OPERA.NM_SIGLA_OPER) ? tbs174.TB250_OPERA.NM_SIGLA_OPER : tbs174.TB250_OPERA.NOM_OPER) : " - ",
                                Plano = tbs174.TB251_PLANO_OPERA != null ? (!String.IsNullOrEmpty(tbs174.TB251_PLANO_OPERA.NM_SIGLA_PLAN) ? tbs174.TB251_PLANO_OPERA.NM_SIGLA_PLAN : tbs174.TB251_PLANO_OPERA.NOM_PLAN) : " - ",
                                NumPlano = tb07.NU_PLANO_SAUDE,
                                TipoAgend = tbs174.TP_AGEND_HORAR,
                                flgJusCan = tbs174.FL_JUSTI_CANCE,
                                Situacao = tbs174.CO_SITUA_AGEND_HORAR
                            }).OrderBy(w => w.Paciente).ToList();

                var res = new List<Relatorio>();

                foreach (var item in ress)
                {
                    //var Total = ress.Where(i => i.coAlu == item.coAlu && i.TipoAgend != "AM" && i.TipoAgend != "AO" && i.TipoAgend != "EN" && i.TipoAgend != "OF").Count();
                    var Total = ress.Where(i => i.coAlu == item.coAlu && i.flgJusCan != "M" && i.Situacao != "M").Count();
                    var Teo = ress.Where(i => i.coAlu == item.coAlu && i.TipoAgend == "TO" && i.flgJusCan != "M" && i.Situacao != "M").Count();
                    var Out = ress.Where(i => i.coAlu == item.coAlu && i.TipoAgend == "OU" && i.flgJusCan != "M" && i.Situacao != "M").Count();
                    var Oft = ress.Where(i => i.coAlu == item.coAlu && i.TipoAgend == "OF" && i.flgJusCan != "M" && i.Situacao != "M").Count();
                    var Psi = ress.Where(i => i.coAlu == item.coAlu && i.TipoAgend == "PI" && i.flgJusCan != "M" && i.Situacao != "M").Count();
                    var Fon = ress.Where(i => i.coAlu == item.coAlu && i.TipoAgend == "FO" && i.flgJusCan != "M" && i.Situacao != "M").Count();
                    var Fis = ress.Where(i => i.coAlu == item.coAlu && i.TipoAgend == "FI" && i.flgJusCan != "M" && i.Situacao != "M").Count();
                    var Enf = ress.Where(i => i.coAlu == item.coAlu && i.TipoAgend == "EN" && i.flgJusCan != "M" && i.Situacao != "M").Count();
                    var Odn = ress.Where(i => i.coAlu == item.coAlu && i.TipoAgend == "AO" && i.flgJusCan != "M" && i.Situacao != "M").Count();
                    var Mdc = ress.Where(i => i.coAlu == item.coAlu && i.TipoAgend == "AM" && i.flgJusCan != "M" && i.Situacao != "M").Count();

                    item.Total = Total != 0 ? Total.ToString() : "-";
                    item.Teo = Teo != 0 ? Teo.ToString() : "-";
                    item.Out = Out != 0 ? Out.ToString() : "-";
                    item.Oft = Oft != 0 ? Oft.ToString() : "-";
                    item.Psi = Psi != 0 ? Psi.ToString() : "-";
                    item.Fon = Fon != 0 ? Fon.ToString() : "-";
                    item.Fis = Fis != 0 ? Fis.ToString() : "-";
                    item.Enf = Enf != 0 ? Enf.ToString() : "-";
                    item.Odn = Odn != 0 ? Odn.ToString() : "-";
                    item.Mdc = Mdc != 0 ? Mdc.ToString() : "-";

                    res.Add(item);
                }

                if (res.Count == 0)
                    return -1;

                //Adiciona ao DataSource do Relatório
                bsReport.Clear();
                foreach (var item in res.DistinctBy(r => r.coAlu))
                {
                    bsReport.Add(item);
                }
                return 1;

            }
            catch { return 0; }
        }

        public class Relatorio
        {
            public string Situacao;
            public int coAlu { get; set; }
            public string Paciente { get; set; }
            public DateTime? Nascimento { get; set; }
            public string Responsavel { get; set; }
            public string Operadora { get; set; }
            public string Plano { get; set; }
            public string NumPlano { get; set; }

            public string flgJusCan { get; set; }
            public string TipoAgend { get; set; }
            public string Teo { get; set; }
            public string Out { get; set; }
            public string Oft { get; set; }
            public string Psi { get; set; }
            public string Fon { get; set; }
            public string Fis { get; set; }
            public string Enf { get; set; }
            public string Odn { get; set; }
            public string Mdc { get; set; }
            public string Total { get; set; }
        }
    }
}
