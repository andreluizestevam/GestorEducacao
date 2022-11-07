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

namespace C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8400_CtrlClinicas._8220_RecepcaoEncaminhamento
{
    public partial class RptRecepcaoEncam : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptRecepcaoEncam()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              string infos,
                              int coEmp,
                              int CoUnidade,
                              string ClassFuncio,
                              int CoProfissional,
                              string Status,
                              string Ordem,
                              string dataIni,
                              string dataFim,
                              string Titulo
            )
        {
            try
            {
                // Seta as informaçoes do rodape do relatorio
                this.InfosRodape = infos;
                this.Parametros = parametros;
                // Instancia o header do relatorio

                if (Titulo == "")
                {
                    lblTitulo.Text = "-";
                }
                else
                {
                    lblTitulo.Text = Titulo.ToUpper();
                }

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

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
                DateTime dtFim = dataFim1.Add(new TimeSpan(23, 59, 59));

                var res = new List<Relatorio>();

                if (Status == "AA")
                {
                    res = (from tbs372 in TBS372_AGEND_AVALI.RetornaTodosRegistros()
                           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs372.CO_ALU equals tb07.CO_ALU
                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs372.CO_COL_SITUA equals tb03.CO_COL
                           where (CoUnidade != 0 ? tbs372.CO_EMP_COL_SITUA == CoUnidade : 0 == 0)
                           && (ClassFuncio != "0" ? tb03.CO_CLASS_PROFI == ClassFuncio : 0 == 0)
                           && (CoProfissional != 0 ? tb03.CO_COL == CoProfissional : 0 == 0)
                           && ((tbs372.DT_AGEND >= dataIni1) && (tbs372.DT_AGEND <= dtFim))
                           select new Relatorio
                           {
                               DataHora_ = tbs372.DT_AGEND.Value,
                               Paciente = tb07.NO_ALU,
                               Neri = tb07.NU_NIRE,
                               Sexo = !String.IsNullOrEmpty(tb07.CO_SEXO_ALU) ? tb07.CO_SEXO_ALU : " - ",
                               DataNascimento = tb07.DT_NASC_ALU,
                               Responsavel = !String.IsNullOrEmpty(tb07.TB108_RESPONSAVEL.NO_RESP) ? tb07.TB108_RESPONSAVEL.NO_RESP : " - ",
                               classificacaoFuncional_ = tb03.CO_CLASS_PROFI,
                               Prof = tb03.NO_APEL_COL,
                               status_ = tbs372.CO_SITUA,
                               Hora = tbs372.HR_AGEND,
                               DtSitua = tbs372.DT_SITUA,
                               NumeroRegistro = "-",
                               TipoRel = "AA"
                           }).ToList();
                }
                else
                {
                    res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                           where (CoUnidade != 0 ? tbs174.CO_EMP == CoUnidade : 0 == 0)
                           && (ClassFuncio != "0" ? tb03.CO_CLASS_PROFI == ClassFuncio : 0 == 0)
                           && (CoProfissional != 0 ? tb03.CO_COL == CoProfissional : 0 == 0)
                           && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dtFim))
                           select new Relatorio
                           {
                               DataHora_ = tbs174.DT_AGEND_HORAR,
                               Paciente = tb07.NO_ALU,
                               Neri = tb07.NU_NIRE,
                               Sexo = !String.IsNullOrEmpty(tb07.CO_SEXO_ALU) ? tb07.CO_SEXO_ALU : " - ",
                               DataNascimento = tb07.DT_NASC_ALU,
                               Responsavel = !String.IsNullOrEmpty(tb07.TB108_RESPONSAVEL.NO_RESP) ? tb07.TB108_RESPONSAVEL.NO_RESP : " - ",
                               classificacaoFuncional_ = tb03.CO_CLASS_PROFI,
                               Prof = tb03.NO_APEL_COL,
                               status_ = tbs174.CO_SITUA_AGEND_HORAR,
                               Hora = tbs174.HR_AGEND_HORAR,
                               DtSitua = tbs174.DT_SITUA_AGEND_HORAR,
                               NumeroRegistro = tbs174.NU_REGIS_CONSUL,
                               TipoRel = "AT",
                               flgConAgn = tbs174.FL_CONF_AGEND,
                               flgEncAgn = tbs174.FL_AGEND_ENCAM,
                               flgJusCan = tbs174.FL_JUSTI_CANCE
                           }).ToList();
                }

                if (res.Count == 0)
                    return -1;

                switch (Ordem)
                {
                    case "DT":
                        res = res.OrderBy(r => r.DataHora).ToList();
                        break;
                    case "PA":
                        res = res.OrderBy(r => r.Paciente).ToList();
                        break;
                    case "RS":
                        res = res.OrderBy(r => r.Responsavel).ToList();
                        break;
                    case "PF":
                        res = res.OrderBy(r => r.Prof).ToList();
                        break;
                    case "ST":
                        res = res.OrderBy(r => r.status).ToList();
                        break;
                    default:
                        res = res.OrderBy(r => r.DataHora).ToList();
                        break;
                }

                bsReport.Clear();
                foreach (var item in res)
                    bsReport.Add(item);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        public class Relatorio
        {
            public string TipoRel { get; set; }
            public string NumeroRegistro { get; set; }
            public string Hora { get; set; }

            public DateTime DataHora_ { get; set; }
            public string DataHora
            {
                get
                {
                    return this.DataHora_.ToString("dd/MM/yy") + (!String.IsNullOrEmpty(this.Hora) ? " - " + this.Hora : "");
                }
            }

            public DateTime DtSitua { get; set; }
            public string Paciente { get; set; }
            public int Neri { get; set; }

            public string PacienteNeri
            {
                get
                {
                    return Convert.ToString(this.Neri).PadLeft(7, '0') + " - " + this.Paciente;
                }
            }

            public string Sexo { get; set; }
            public string Responsavel { get; set; }
            public string Prof { get; set; }
            public DateTime? DataNascimento { get; set; }

            public string Idade
            {
                get
                {
                    return Funcoes.FormataDataNascimento(this.DataNascimento.Value);
                }
            }

            public string classificacaoFuncional_ { get; set; }
            public string classificacaoFuncional
            {
                get
                {
                    switch (classificacaoFuncional_)
                    {
                        case "T":
                            return "TERA OCU";
                        case "O":
                            return "OUTROS";
                        case "F":
                            return "OFTALO";
                        case "D":
                            return "ODONTO";
                        case "P":
                            return "PSICOL";
                        case "M":
                            return "MEDIC";
                        case "N":
                            return "FONOA";
                        case "I":
                            return "FISIOT";
                        case "E":
                            return "ENFERME";
                        default:
                            return "-";
                    }
                }
            }

            public string flgConAgn { get; set; }
            public string flgEncAgn { get; set; }
            public string flgJusCan { get; set; }

            public string status_ { get; set; }
            public string status
            {
                get
                {
                    var s = "";

                    if (TipoRel == "AA")
                        switch (this.status_)
                        {
                            case "A":
                                s = "EM ABERTO";
                                break;
                            case "C":
                                s = "CANCELADO";
                                break;
                            case "F":
                                s = "FINALIZADO";
                                break;
                            case "E":
                                s = "ENCAMINHADO";
                                break;
                            case "M":
                                s = "MOVIMENTADO";
                                break;
                            case "R":
                                s = "REALIZADO";
                                break;
                            default:
                                s = "-";
                                break;
                        }
                    else
                        if (this.status_ == "C" && this.flgJusCan == "N")
                            s = "Falta";
                        else if (this.status_ == "C" && this.flgJusCan == "S")
                            s = "Falta Just.";
                        else if (this.status_ == "A" && this.flgConAgn == "S" && (this.flgEncAgn == "N" || String.IsNullOrEmpty(this.flgEncAgn)))
                            s = "Presença";
                        else if (this.status_ == "A" && this.flgConAgn == "S" && this.flgEncAgn == "S")
                            s = "Encaminhado";
                        else if (this.status_ == "R" && this.flgConAgn == "S" && this.flgEncAgn == "S")
                            s = "Atendido";
                        else if (this.status_ == "A" && this.flgConAgn == "N")
                            s = "Em Aberto";
                        else if (this.status_ == "A")
                            s = "Em Aberto";
                        else if (this.status_ == "C")
                            s = "Cancelado";
                        else if (this.status_ == "M")
                            s = "Movimentado";
                        else if (this.status_ == "R")
                            s = "Realizado";
                        else if (this.status_ == "E")
                            s = "Encaminhado";

                    return s;
                }
            }
        }
    }
}
