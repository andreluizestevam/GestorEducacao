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
    public partial class RptTempoAtendimento2 : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptTempoAtendimento2()
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
                    lblTitulo.Text = "DEMONSTRATIVO DE AGENDA DE HORÁRIOS POR SITUAÇÃO";

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;
                DateTime Data = Convert.ToDateTime(data);
                // Setar o header do relatorio
                this.BaseInit(header);

                var ress = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                            join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                            join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tb03.CO_ESPEC equals tb63.CO_ESPECIALIDADE
                            where (Profissional != 0 ? tbs174.CO_COL == Profissional : Profissional == 0)
                            && (local != 0 ? tbs174.CO_DEPT == local : 0 == 0)
                            && (classFunci != "0" ? tb03.CO_CLASS_PROFI.Equals(classFunci) : 0 == 0)
                            && (!tbs174.CO_SITUA_AGEND_HORAR.Equals("E")) && (!tbs174.CO_SITUA_AGEND_HORAR.Equals("M"))
                            select new Relatorio
                            {
                                DTAgend = tbs174.DT_AGEND_HORAR,
                                CoProfi = tb03.CO_COL,
                                Profissional = tb03.NO_COL,
                                Atende = tbs174.CO_ALU.HasValue,
                                Horario = tbs174.HR_AGEND_HORAR,
                                Duracao = tbs174.HR_DURACAO_AGENDA,
                                Local = tbs174.CO_DEPT,
                                Funcao = tb63.NO_ESPECIALIDADE,
                                Situ = tbs174.CO_SITUA_AGEND_HORAR
                            }).OrderBy(w => w.Horario).ToList();

                ress = ress.Where(a => a.DTAgend == Data).ToList();

                var res = new List<Relatorio>();

                int qntAberto = 0;
                int qntAgendado = 0;
                int qntCancelado = 0;
                int qntAtendido = 0;

                foreach (var item in ress)
                {
                    int result = res.Where(r => r.CoProfi == item.CoProfi && r.Local == item.Local).ToList().Count;

                    if (result == 0)
                    {
                        foreach (var it in ress.Where(x => x.CoProfi == item.CoProfi && x.Local == item.Local))
                        {
                            if (!it.Atende)
                            {
                                qntAberto++;
                            }
                            else
                            {
                                switch (it.Situ)
                                {
                                    case "A":
                                        qntAgendado++;
                                        break;
                                    case "C":
                                        qntCancelado++;
                                        break;
                                    case "R":
                                        qntAtendido++;
                                        break;
                                }
                            }

                            if (String.IsNullOrEmpty(item.hora1))
                            {
                                item.hora1 = it.Horario;
                                item.Atende1 = it.Atende;
                                item.Situ1 = it.Situ;
                            }
                            else if (String.IsNullOrEmpty(item.hora2))
                            {
                                item.hora2 = it.Horario;
                                item.Atende2 = it.Atende;
                                item.Situ2 = it.Situ;
                            }
                            else if (String.IsNullOrEmpty(item.hora3))
                            {
                                item.hora3 = it.Horario;
                                item.Atende3 = it.Atende;
                                item.Situ3 = it.Situ;
                            }
                            else if (String.IsNullOrEmpty(item.hora4))
                            {
                                item.hora4 = it.Horario;
                                item.Atende4 = it.Atende;
                                item.Situ4 = it.Situ;
                            }
                            else if (String.IsNullOrEmpty(item.hora5))
                            {
                                item.hora5 = it.Horario;
                                item.Atende5 = it.Atende;
                                item.Situ5 = it.Situ;
                            }
                            else if (String.IsNullOrEmpty(item.hora6))
                            {
                                item.hora6 = it.Horario;
                                item.Atende6 = it.Atende;
                                item.Situ6 = it.Situ;
                            }
                            else if (String.IsNullOrEmpty(item.hora7))
                            {
                                item.hora7 = it.Horario;
                                item.Atende7 = it.Atende;
                                item.Situ7 = it.Situ;
                            }
                            else if (String.IsNullOrEmpty(item.hora8))
                            {
                                item.hora8 = it.Horario;
                                item.Atende8 = it.Atende;
                                item.Situ8 = it.Situ;
                            }
                            else if (String.IsNullOrEmpty(item.hora9))
                            {
                                item.hora9 = it.Horario;
                                item.Atende9 = it.Atende;
                                item.Situ9 = it.Situ;
                            }
                            else if (String.IsNullOrEmpty(item.hora10))
                            {
                                item.hora10 = it.Horario;
                                item.Atende10 = it.Atende;
                                item.Situ10 = it.Situ;
                            }
                            else if (String.IsNullOrEmpty(item.hora11))
                            {
                                item.hora11 = it.Horario;
                                item.Atende11 = it.Atende;
                                item.Situ11 = it.Situ;
                            }
                            else if (String.IsNullOrEmpty(item.hora12))
                            {
                                item.hora12 = it.Horario;
                                item.Atende12 = it.Atende;
                                item.Situ12 = it.Situ;
                            }
                            else if (String.IsNullOrEmpty(item.hora13))
                            {
                                item.hora13 = it.Horario;
                                item.Atende13 = it.Atende;
                                item.Situ13 = it.Situ;
                            }
                            else if (String.IsNullOrEmpty(item.hora14))
                            {
                                item.hora14 = it.Horario;
                                item.Atende14 = it.Atende;
                                item.Situ14 = it.Situ;
                            }
                            else if (String.IsNullOrEmpty(item.hora15))
                            {
                                item.hora15 = it.Horario;
                                item.Atende15 = it.Atende;
                                item.Situ15 = it.Situ;
                            }
                            else if (String.IsNullOrEmpty(item.hora16))
                            {
                                item.hora16 = it.Horario;
                                item.Atende16 = it.Atende;
                                item.Situ16 = it.Situ;
                            }
                            else if (String.IsNullOrEmpty(item.hora17))
                            {
                                item.hora17 = it.Horario;
                                item.Atende17 = it.Atende;
                                item.Situ17 = it.Situ;
                            }
                            else if (String.IsNullOrEmpty(item.hora18))
                            {
                                item.hora18 = it.Horario;
                                item.Atende18 = it.Atende;
                                item.Situ18 = it.Situ;
                            }
                            else if (String.IsNullOrEmpty(item.hora19))
                            {
                                item.hora19 = it.Horario;
                                item.Atende19 = it.Atende;
                                item.Situ19 = it.Situ;
                            }
                            else if (String.IsNullOrEmpty(item.hora20))
                            {
                                item.hora20 = it.Horario;
                                item.Atende20 = it.Atende;
                                item.Situ20 = it.Situ;
                            }
                            else if (String.IsNullOrEmpty(item.hora21))
                            {
                                item.hora21 = it.Horario;
                                item.Atende21 = it.Atende;
                                item.Situ21 = it.Situ;
                            }
                        }
                        res.Add(item);
                    }
                }

                if (res.Count == 0)
                    return -1;

                //Adiciona ao DataSource do Relatório
                bsReport.Clear();

                lblQuantitativos.Text = "Quantitativo de Horários: " + qntAberto + " Abertos | " + qntAgendado + " Agendados | " + qntCancelado + " Cancelados | " + qntAtendido + " Atendidos";

                foreach (var item in res.OrderBy(r => r.Profissional).ThenBy(r => r.Horario))
                    bsReport.Add(item);


                return 1;

            }
            catch { return 0; }
        }

        public class Relatorio
        {
            public DateTime DTAgend { get; set; }
            public int CoProfi { get; set; }
            public string Profissional { get; set; }
            public string Profissional_V
            {
                get
                {
                    return this.Profissional.Length > 36 ? this.Profissional.Substring(0, 35) + "..." : this.Profissional;
                }
            }
            public bool Atende { get; set; }
            public string Horario { get; set; }
            public string Duracao { get; set; }
            public int? Local { get; set; }
            public string Funcao { get; set; }
            public string Situ { get; set; }

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
            public bool Atende1 { get; set; }
            public string Situ1 { get; set; }

            public string hora2 { get; set; }
            public bool Atende2 { get; set; }
            public string Situ2 { get; set; }

            public string hora3 { get; set; }
            public bool Atende3 { get; set; }
            public string Situ3 { get; set; }

            public string hora4 { get; set; }
            public bool Atende4 { get; set; }
            public string Situ4 { get; set; }

            public string hora5 { get; set; }
            public bool Atende5 { get; set; }
            public string Situ5 { get; set; }

            public string hora6 { get; set; }
            public bool Atende6 { get; set; }
            public string Situ6 { get; set; }

            public string hora7 { get; set; }
            public bool Atende7 { get; set; }
            public string Situ7 { get; set; }

            public string hora8 { get; set; }
            public bool Atende8 { get; set; }
            public string Situ8 { get; set; }

            public string hora9 { get; set; }
            public bool Atende9 { get; set; }
            public string Situ9 { get; set; }

            public string hora10 { get; set; }
            public bool Atende10 { get; set; }
            public string Situ10 { get; set; }

            public string hora11 { get; set; }
            public bool Atende11 { get; set; }
            public string Situ11 { get; set; }

            public string hora12 { get; set; }
            public bool Atende12 { get; set; }
            public string Situ12 { get; set; }

            public string hora13 { get; set; }
            public bool Atende13 { get; set; }
            public string Situ13 { get; set; }

            public string hora14 { get; set; }
            public bool Atende14 { get; set; }
            public string Situ14 { get; set; }

            public string hora15 { get; set; }
            public bool Atende15 { get; set; }
            public string Situ15 { get; set; }

            public string hora16 { get; set; }
            public bool Atende16 { get; set; }
            public string Situ16 { get; set; }

            public string hora17 { get; set; }
            public bool Atende17 { get; set; }
            public string Situ17 { get; set; }

            public string hora18 { get; set; }
            public bool Atende18 { get; set; }
            public string Situ18 { get; set; }

            public string hora19 { get; set; }
            public bool Atende19 { get; set; }
            public string Situ19 { get; set; }

            public string hora20 { get; set; }
            public bool Atende20 { get; set; }
            public string Situ20 { get; set; }

            public string hora21 { get; set; }
            public bool Atende21 { get; set; }
            public string Situ21 { get; set; }
        }
    }
}
