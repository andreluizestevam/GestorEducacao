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

namespace C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3300_Consultas
{
    public partial class RptTempoAtendimento : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptTempoAtendimento()
        {
            InitializeComponent();
        }

        public int InitReport(
              string parametros,
              string infos,
              int coEmp,
              int Profissional,
              string data,
              string NomeFuncionalidade,
              int local,
              string classFunci
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
                    //lblTitulo.Text = NomeFuncionalidade.ToUpper();
                    lblTitulo.Text = "DEMONSTRATIVO DE AGENDA DE HORÁRIOS POR INTERVALO";

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;
                DateTime Data = Convert.ToDateTime(data);
                // Setar o header do relatorio
                this.BaseInit(header);

                var ress = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros().Where(a => a.DT_AGEND_HORAR == Data)
                            join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                            join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tb03.CO_ESPEC equals tb63.CO_ESPECIALIDADE
                            where (Profissional != 0 ? tbs174.CO_COL == Profissional : Profissional == 0)
                            && (local != 0 ? tbs174.CO_DEPT == local : 0 == 0)
                            && (classFunci != "0" ? tb03.CO_CLASS_PROFI.Equals(classFunci) : 0 == 0)
                            select new Relatorio
                            {
                                Profissional = tb03.NO_COL,
                                Horario = tbs174.HR_AGEND_HORAR,
                                Duracao = tbs174.HR_DURACAO_AGENDA,
                                Atende = tbs174.CO_ALU.HasValue,
                                Local = tbs174.CO_DEPT,
                                Funcao = tb63.NO_ESPECIALIDADE
                            }).OrderBy(w => w.Horario).ToList();

                var res = new List<Relatorio>();

                var hrs = new List<TimeSpan>();
                TimeSpan h, duracao = new TimeSpan();

                var h1 = new TimeSpan();
                var h2 = new TimeSpan();
                var h3 = new TimeSpan();
                var h4 = new TimeSpan();
                var h5 = new TimeSpan();
                var h6 = new TimeSpan();
                var h7 = new TimeSpan();
                var h8 = new TimeSpan();
                var h9 = new TimeSpan();
                var h10 = new TimeSpan();
                var h11 = new TimeSpan();
                var h12 = new TimeSpan();

                foreach (var item in ress)
                {
                    h = TimeSpan.Parse(item.Horario);
                    duracao = TimeSpan.Parse(item.Duracao);

                    if (String.IsNullOrEmpty(lblHora1.Text) && !hrs.Contains(h))
                    {
                        lblHora1.Text = item.Horario;
                        hrs.Add(h);
                        h1 = h + duracao;
                    }
                    else if (String.IsNullOrEmpty(lblHora2.Text) && !hrs.Contains(h))
                    {
                        if (h >= h1)
                        {
                            lblHora2.Text = item.Horario;
                            hrs.Add(h);
                            h2 = h + duracao;
                        }
                    }
                    else if (String.IsNullOrEmpty(lblHora3.Text) && !hrs.Contains(h))
                    {
                        if (h >= h2)
                        {
                            lblHora3.Text = item.Horario;
                            hrs.Add(h);
                            h3 = h + duracao;
                        }
                    }
                    else if (String.IsNullOrEmpty(lblHora4.Text) && !hrs.Contains(h))
                    {
                        if (h >= h3)
                        {
                            lblHora4.Text = item.Horario;
                            hrs.Add(h);
                            h4 = h + duracao;
                        }
                    }
                    else if (String.IsNullOrEmpty(lblHora5.Text) && !hrs.Contains(h))
                    {
                        if (h >= h4)
                        {
                            lblHora5.Text = item.Horario;
                            hrs.Add(h);
                            h5 = h + duracao;
                        }
                    }
                    else if (String.IsNullOrEmpty(lblHora6.Text) && !hrs.Contains(h))
                    {
                        if (h >= h5)
                        {
                            lblHora6.Text = item.Horario;
                            hrs.Add(h);
                            h6 = h + duracao;
                        }
                    }
                    else if (String.IsNullOrEmpty(lblHora7.Text) && !hrs.Contains(h))
                    {
                        if (h >= h6)
                        {
                            lblHora7.Text = item.Horario;
                            hrs.Add(h);
                            h7 = h + duracao;
                        }
                    }
                    else if (String.IsNullOrEmpty(lblHora8.Text) && !hrs.Contains(h))
                    {
                        if (h >= h7)
                        {
                            lblHora8.Text = item.Horario;
                            hrs.Add(h);
                            h8 = h + duracao;
                        }
                    }
                    else if (String.IsNullOrEmpty(lblHora9.Text) && !hrs.Contains(h))
                    {
                        if (h >= h8)
                        {
                            lblHora9.Text = item.Horario;
                            hrs.Add(h);
                            h9 = h + duracao;
                        }
                    }
                    else if (String.IsNullOrEmpty(lblHora10.Text) && !hrs.Contains(h))
                    {
                        if (h >= h9)
                        {
                            lblHora10.Text = item.Horario;
                            hrs.Add(h);
                            h10 = h + duracao;
                        }
                    }
                    else if (String.IsNullOrEmpty(lblHora11.Text) && !hrs.Contains(h))
                    {
                        if (h >= h10)
                        {
                            lblHora11.Text = item.Horario;
                            hrs.Add(h);
                            h11 = h + duracao;
                        }
                    }
                    else if (String.IsNullOrEmpty(lblHora12.Text) && !hrs.Contains(h))
                    {
                        if (h >= h11)
                        {
                            lblHora12.Text = item.Horario;
                            hrs.Add(h);
                            h12 = h + duracao;
                        }
                    }
                }

                foreach (var item in ress)
                {
                    if (!String.IsNullOrEmpty(lblHora1.Text))
                    {
                        var hr = ress.Where(r => r.Horario == item.Horario && TimeSpan.Parse(lblHora1.Text) <= TimeSpan.Parse(item.Horario) && h1 >= TimeSpan.Parse(item.Horario) && r.Profissional == item.Profissional && r.Atende).FirstOrDefault() ?? ress.Where(r => r.Profissional == item.Profissional && !String.IsNullOrEmpty(r.hora1)).FirstOrDefault();
                        if (hr != null)
                            item.hora1 = hr.Horario;
                    }
                    if (!String.IsNullOrEmpty(lblHora2.Text))
                    {
                        var hr = ress.Where(r => r.Horario == item.Horario && TimeSpan.Parse(lblHora2.Text) <= TimeSpan.Parse(item.Horario) && h2 >= TimeSpan.Parse(item.Horario) && r.Profissional == item.Profissional && r.Atende).FirstOrDefault() ?? ress.Where(r => r.Profissional == item.Profissional && !String.IsNullOrEmpty(r.hora2)).FirstOrDefault();
                        if (hr != null)
                            item.hora2 = hr.Horario;
                    }
                    if (!String.IsNullOrEmpty(lblHora3.Text))
                    {
                        var hr = ress.Where(r => r.Horario == item.Horario && TimeSpan.Parse(lblHora3.Text) <= TimeSpan.Parse(item.Horario) && h3 >= TimeSpan.Parse(item.Horario) && r.Profissional == item.Profissional && r.Atende).FirstOrDefault() ?? ress.Where(r => r.Profissional == item.Profissional && !String.IsNullOrEmpty(r.hora3)).FirstOrDefault();
                        if (hr != null)
                            item.hora3 = hr.Horario;
                    }
                    if (!String.IsNullOrEmpty(lblHora4.Text))
                    {
                        var hr = ress.Where(r => r.Horario == item.Horario && TimeSpan.Parse(lblHora4.Text) <= TimeSpan.Parse(item.Horario) && h4 >= TimeSpan.Parse(item.Horario) && r.Profissional == item.Profissional && r.Atende).FirstOrDefault() ?? ress.Where(r => r.Profissional == item.Profissional && !String.IsNullOrEmpty(r.hora4)).FirstOrDefault();
                        if (hr != null)
                            item.hora4 = hr.Horario;
                    }
                    if (!String.IsNullOrEmpty(lblHora5.Text))
                    {
                        var hr = ress.Where(r => r.Horario == item.Horario && TimeSpan.Parse(lblHora5.Text) <= TimeSpan.Parse(item.Horario) && h5 >= TimeSpan.Parse(item.Horario) && r.Profissional == item.Profissional && r.Atende).FirstOrDefault() ?? ress.Where(r => r.Profissional == item.Profissional && !String.IsNullOrEmpty(r.hora5)).FirstOrDefault();
                        if (hr != null)
                            item.hora5 = hr.Horario;
                    }
                    if (!String.IsNullOrEmpty(lblHora6.Text))
                    {
                        var hr = ress.Where(r => r.Horario == item.Horario && TimeSpan.Parse(lblHora6.Text) <= TimeSpan.Parse(item.Horario) && h6 >= TimeSpan.Parse(item.Horario) && r.Profissional == item.Profissional && r.Atende).FirstOrDefault() ?? ress.Where(r => r.Profissional == item.Profissional && !String.IsNullOrEmpty(r.hora6)).FirstOrDefault();
                        if (hr != null)
                            item.hora6 = hr.Horario;
                    }
                    if (!String.IsNullOrEmpty(lblHora7.Text))
                    {
                        var hr = ress.Where(r => r.Horario == item.Horario && TimeSpan.Parse(lblHora7.Text) <= TimeSpan.Parse(item.Horario) && h7 >= TimeSpan.Parse(item.Horario) && r.Profissional == item.Profissional && r.Atende).FirstOrDefault() ?? ress.Where(r => r.Profissional == item.Profissional && !String.IsNullOrEmpty(r.hora7)).FirstOrDefault();
                        if (hr != null)
                            item.hora7 = hr.Horario;
                    }
                    if (!String.IsNullOrEmpty(lblHora8.Text))
                    {
                        var hr = ress.Where(r => r.Horario == item.Horario && TimeSpan.Parse(lblHora8.Text) <= TimeSpan.Parse(item.Horario) && h8 >= TimeSpan.Parse(item.Horario) && r.Profissional == item.Profissional && r.Atende).FirstOrDefault() ?? ress.Where(r => r.Profissional == item.Profissional && !String.IsNullOrEmpty(r.hora8)).FirstOrDefault();
                        if (hr != null)
                            item.hora8 = hr.Horario;
                    }
                    if (!String.IsNullOrEmpty(lblHora9.Text))
                    {
                        var hr = ress.Where(r => r.Horario == item.Horario && TimeSpan.Parse(lblHora9.Text) <= TimeSpan.Parse(item.Horario) && h9 >= TimeSpan.Parse(item.Horario) && r.Profissional == item.Profissional && r.Atende).FirstOrDefault() ?? ress.Where(r => r.Profissional == item.Profissional && !String.IsNullOrEmpty(r.hora9)).FirstOrDefault();
                        if (hr != null)
                            item.hora9 = hr.Horario;
                    }
                    if (!String.IsNullOrEmpty(lblHora10.Text))
                    {
                        var hr = ress.Where(r => r.Horario == item.Horario && TimeSpan.Parse(lblHora10.Text) <= TimeSpan.Parse(item.Horario) && h10 >= TimeSpan.Parse(item.Horario) && r.Profissional == item.Profissional && r.Atende).FirstOrDefault() ?? ress.Where(r => r.Profissional == item.Profissional && !String.IsNullOrEmpty(r.hora10)).FirstOrDefault();
                        if (hr != null)
                            item.hora10 = hr.Horario;
                    }
                    if (!String.IsNullOrEmpty(lblHora11.Text))
                    {
                        var hr = ress.Where(r => r.Horario == item.Horario && TimeSpan.Parse(lblHora11.Text) <= TimeSpan.Parse(item.Horario) && h11 >= TimeSpan.Parse(item.Horario) && r.Profissional == item.Profissional && r.Atende).FirstOrDefault() ?? ress.Where(r => r.Profissional == item.Profissional && !String.IsNullOrEmpty(r.hora11)).FirstOrDefault();
                        if (hr != null)
                            item.hora11 = hr.Horario;
                    }
                    if (!String.IsNullOrEmpty(lblHora12.Text))
                    {
                        var hr = ress.Where(r => r.Horario == item.Horario && TimeSpan.Parse(lblHora12.Text) <= TimeSpan.Parse(item.Horario) && h12 >= TimeSpan.Parse(item.Horario) && r.Profissional == item.Profissional && r.Atende).FirstOrDefault() ?? ress.Where(r => r.Profissional == item.Profissional && !String.IsNullOrEmpty(r.hora12)).FirstOrDefault();
                        if (hr != null)
                            item.hora12 = hr.Horario;
                    }

                    res.Add(item);
                }

                if (res.Count == 0)
                    return -1;

                //Adiciona ao DataSource do Relatório
                bsReport.Clear();

                foreach (var item in res.OrderByDescending(r => r.Horario).DistinctBy(r => r.Profissional))
                    bsReport.Add(item);

                return 1;

            }
            catch { return 0; }
        }

        public class Relatorio
        {
            public string Profissional { get; set; }
            public string Profissional_V
            {
                get
                {
                    return this.Profissional.Length > 40 ? this.Profissional.Substring(0, 40) + "..." : this.Profissional;
                }
            }
            public string Horario { get; set; }
            public string Duracao { get; set; }
            public int? Local { get; set; }
            public string Funcao { get; set; }
            public bool Atende { get; set; }

            public string Local_V
            {
                get
                {
                    return TB14_DEPTO.RetornaPelaChavePrimaria((int)this.Local).CO_SIGLA_DEPTO;
                }
            }

            public string Funcao_V
            {
                get
                {
                    switch (this.Funcao)
                    {
                        case "T":
                            return "Terapeuta Ocupacional";
                        case "O":
                            return "Triagem";
                        case "N":
                            return "Nutricionista";
                        case "S":
                            return "Esteticista";
                        case "D":
                            return "Odontólogo(a)";
                        case "P":
                            return "Psicólogo";
                        case "M":
                            return "Médico(a)";
                        case "F":
                            return "Fonoaudiólogo(a)";
                        case "I":
                            return "Fisioterapeuta";
                        case "E":
                            return "Enfermeiro(a)";
                        default:
                            return "-";
                    }
                }
            }

            public string hora1 { get; set; }
            public string hora2 { get; set; }
            public string hora3 { get; set; }
            public string hora4 { get; set; }
            public string hora5 { get; set; }
            public string hora6 { get; set; }
            public string hora7 { get; set; }
            public string hora8 { get; set; }
            public string hora9 { get; set; }
            public string hora10 { get; set; }
            public string hora11 { get; set; }
            public string hora12 { get; set; }
        }
    }
}
