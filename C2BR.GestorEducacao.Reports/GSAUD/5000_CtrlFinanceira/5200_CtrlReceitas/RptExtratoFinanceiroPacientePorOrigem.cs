using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Objects;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text.RegularExpressions;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports.Helper;
using DevExpress.XtraReports.UI;

namespace C2BR.GestorEducacao.Reports.GSAUD._5000_CtrlFinanceira._5200_CtrlReceitas
{
    public partial class RptExtratoFinanceiroPacientePorOrigem : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptExtratoFinanceiroPacientePorOrigem()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              int codUnid,
                              int CO_EMP,
                              int CO_PACI,
                              string RAP,
                              string Origem,
                              int Profissional,
                              int Operadora,
                              string Situa,
                              DateTime dataIni,
                              DateTime dataFim,
                              string infos,
                              string NO_RELATORIO)
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(CO_EMP);

                if (header == null)
                    return 0;

                this.lblTitulo.Text = (!string.IsNullOrEmpty(NO_RELATORIO) ? NO_RELATORIO.ToUpper() : "EXTRATO FINANCEIRO PACIENTE - SIMPLES");

                this.lblLegenda.Text = "Legenda: UNID (Unidade Procedimento) - SIT (Situação Procedimento: AGE = Agendado | CAN = Cancelado | REA = Realizado) - ORI (Origem Procedimento: CO = Consulta | EX = Exame | OU = Outros | PR = Procedimento | SA = Serviço Ambulatorial | SS = Serviço Saúde | IN = Internação | VA = Vacina) - CONTRAT (Contratação) - R$ UNIT (Valor Unitário)";

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                ///Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Parametrizada

                //string strDataIni = dataIni.ToUniversalTime().ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss'.'fff''");
                //string strDataFim = dataFim.ToUniversalTime().ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss'.'fff''");

                //DateTime dtIni = DateTime.Parse(strDataIni);
                //DateTime dtFim = DateTime.Parse(strDataFim);

                var lst = (from tbs174 in ctx.TBS174_AGEND_HORAR
                           join tb07 in ctx.TB07_ALUNO on tbs174.CO_ALU equals tb07.CO_ALU
                           join tb108 in ctx.TB108_RESPONSAVEL on tb07.TB108_RESPONSAVEL.CO_RESP equals tb108.CO_RESP
                           join tb25 in ctx.TB25_EMPRESA on tbs174.CO_EMP equals tb25.CO_EMP
                           join tb03 in ctx.TB03_COLABOR on tbs174.CO_COL equals tb03.CO_COL
                           join tbs356 in ctx.TBS356_PROC_MEDIC_PROCE on tbs174.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE equals tbs356.ID_PROC_MEDI_PROCE
                           where (codUnid != 0 ? tbs174.CO_EMP == codUnid : 0 == 0)
                           && (tb07.CO_ALU == CO_PACI)
                           && (String.IsNullOrEmpty(RAP) ? 0 == 0 : tbs174.NU_REGIS_CONSUL == RAP)
                           && (Origem.Equals("0") ? 0 == 0 : tbs174.TBS356_PROC_MEDIC_PROCE.CO_TIPO_PROC_MEDI == Origem)
                           && (Operadora != 0 ? tbs174.TB250_OPERA.ID_OPER == Operadora : 0 == 0)
                           && (Profissional != 0 ? tb03.CO_COL == Profissional : 0 == 0)
                           && (Situa.Equals("0") ? 0 == 0 : tbs174.CO_SITUA_AGEND_HORAR.Equals(Situa))
                           && (DateTime.Compare(tbs174.DT_AGEND_HORAR, dataIni) >= 0) && (DateTime.Compare(tbs174.DT_AGEND_HORAR, dataFim) <= 0)
                           //&& ((tbs174.DT_AGEND_HORAR.Year >= dataIni.Year) && (tbs174.DT_AGEND_HORAR.Month >= dataIni.Month)) && ((tbs174.DT_AGEND_HORAR.Year <= dataFim.Year) && (tbs174.DT_AGEND_HORAR.Month <= dataFim.Month))
                           select new
                           {
                               Nome = tb07.NO_ALU,
                               NIRE = tb07.NU_NIRE,
                               Responsavel = tb108.NO_RESP,
                               CpfResp = tb108.NU_CPF_RESP,
                               Profissional = tb03.NO_APEL_COL,
                               DataAgendamento = tbs174.DT_AGEND_HORAR,
                               OrigemProcedimento = tbs174.TBS356_PROC_MEDIC_PROCE.CO_TIPO_PROC_MEDI,
                               CodigoProcedimento = tbs174.TBS356_PROC_MEDIC_PROCE.CO_PROC_MEDI,
                               Procedimento = tbs174.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
                               Operadora = tbs174.TB250_OPERA.NM_SIGLA_OPER,
                               ValorUnit = tbs356.TBS353_VALOR_PROC_MEDIC_PROCE.Where(x => x.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == tbs356.ID_PROC_MEDI_PROCE).FirstOrDefault().VL_BASE,
                               RLZ = tbs174.FL_CONF_AGEND.Equals("S") ? "X" : "-",
                               Rap = tbs174.NU_REGIS_CONSUL,
                               CO_SITU = tbs174.CO_SITUA_AGEND_HORAR,
                               SiglaUnid = tb25.sigla,
                               SIGLA_ENTI_PROFI = tb03.CO_SIGLA_ENTID_PROFI,
                               NU_ENTI_PROFI = tb03.NU_ENTID_PROFI
                           }).OrderBy(p => p.OrigemProcedimento).ThenBy(p => p.DataAgendamento).ThenBy(p => p.Nome);

                var res = lst.ToList();
                #endregion

                ///Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                Resultado result = new Resultado();
                List<Origem> listOrigem = new List<Origem>();                

                bsReport.Clear();
                int idx = 0;
                decimal valorAgend = 0;
                decimal valorCance = 0;
                decimal valorReali = 0;
                int qtCance = 0;
                int qtAgend = 0;
                int qtReali = 0;

                foreach (var at in res)
                {
                    if (listOrigem.Where(x => x.TipoOrigem == at.OrigemProcedimento).ToList().Count == 0) {

                        Origem ori = new Origem();
                        ori.TipoOrigem = at.OrigemProcedimento;
                        List<RelExtratoFinanceiroPaciente> listExtrato = new List<RelExtratoFinanceiroPaciente>();
                        foreach (var r in res.Where(y => y.OrigemProcedimento == ori.TipoOrigem))
                        {
                            RelExtratoFinanceiroPaciente rel = new RelExtratoFinanceiroPaciente();
                            rel.Nome = r.Nome;
                            rel.NIRE = r.NIRE;
                            rel.Responsavel = r.Responsavel;
                            rel.CpfResp = r.CpfResp;
                            rel.Profissional = r.Profissional;
                            rel.DataAgendamento = r.DataAgendamento;
                            rel.OrigemProcedimento = r.OrigemProcedimento;
                            rel.CodigoProcedimento = r.CodigoProcedimento;
                            rel.Procedimento = r.Procedimento;
                            rel.Operadora = r.Operadora;
                            rel.ValorUnit = r.ValorUnit;
                            rel.RLZ = r.RLZ;
                            rel.Rap = r.Rap;
                            rel.CO_SITU = r.CO_SITU;
                            rel.SIGLA_ENTI_PROFI = r.SIGLA_ENTI_PROFI;
                            rel.NU_ENTI_PROFI = r.NU_ENTI_PROFI;
                            rel.SiglaUnid = r.SiglaUnid;

                            switch (rel.CO_SITU)
                            {
                                case "A":
                                    qtAgend++;
                                    valorAgend += r.ValorUnit;
                                    result.qtAgend = qtAgend;
                                    result.ValorAgend = valorAgend;
                                    ori.qtAgendOri++;
                                    ori.ValorAgendOri += r.ValorUnit;
                                    break;
                                case "C":
                                    qtCance++;
                                    valorCance += r.ValorUnit;
                                    result.ValorCance = valorCance;
                                    result.qtCance = qtCance;
                                    ori.qtCanceOri++;
                                    ori.ValorCanceOri += r.ValorUnit;
                                    break;
                                case "R":
                                    qtReali++;
                                    valorReali += r.ValorUnit;
                                    result.qtReali = qtReali;
                                    result.ValorReali = valorReali;
                                    ori.qtRealiOri++;
                                    ori.ValorRealiOri += r.ValorUnit;
                                    break;
                            }

                            result.DesRealizados = "(Agendados: " + qtAgend.ToString() + " - R$" + valorAgend.ToString("N2") + "   /   " + "Cancelados: " + qtCance.ToString() + " - R$" + valorCance.ToString("N2") + "   /   " + "Realizados: " + qtReali.ToString() + " - R$" + valorReali.ToString("N2") + ")";
                            ori.DesRealizadosOri = "Total Origem (Agendados: " + ori.qtAgendOri.ToString() + " - R$" + ori.ValorAgendOri.ToString("N2") + "   /   " + "Cancelados: " + ori.qtCanceOri.ToString() + " - R$" + ori.ValorCanceOri.ToString("N2") + "   /   " + "Realizados: " + ori.qtRealiOri.ToString() + " - R$" + ori.ValorRealiOri.ToString("N2") + ")";
                            result.ValorTotal += at.ValorUnit;
                            ori.ValorTotalOri = ori.ValorAgendOri + ori.ValorCanceOri + ori.ValorRealiOri;
                            listExtrato.Add(rel);
                        }

                        ori.RelExtratoFinanceiro = listExtrato;
                        listOrigem.Add(ori);
                    }                    
                }
                result.Origem = listOrigem;
                bsReport.Add(result);

                if (listOrigem.Count == 1)
                {
                    this.xrTableCell9.Visible = false;
                    this.xrTable4.Visible = false;
                }

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Histórico Financeiro do Aluno

        public class Resultado
        {
            public int qtCance { get; set; }
            public int qtReali { get; set; }
            public int qtAgend { get; set; }
            public decimal ValorTotal { get; set; }
            public decimal ValorCance { get; set; }
            public decimal ValorReali { get; set; }
            public decimal ValorAgend { get; set; }
            public string DesRealizados { get; set; }

            public List<Origem> Origem { get; set; }
        }

        public class Origem
        {
            public int qtCanceOri { get; set; }
            public int qtRealiOri { get; set; }
            public int qtAgendOri { get; set; }
            public decimal ValorTotalOri { get; set; }
            public decimal ValorCanceOri { get; set; }
            public decimal ValorRealiOri { get; set; }
            public decimal ValorAgendOri { get; set; }
            public string DesRealizadosOri { get; set; }

            public string TipoOrigem { get; set; }
            public string DE_TipoOrigem
            {
                get
                {
                    switch (this.TipoOrigem)
                    {
                        case "CO":
                            return "CONSULTA";
                        case "EX":
                            return "EXAME";
                        case "PR":
                            return "PROCEDIMENTO";
                        case "SA":
                            return "SERVIÇO AMBULATÓRIO";
                        case "SS":
                            return "SERVIÇO SAÚDE";
                        case "IN":
                            return "INTERNAÇÃO";
                        case "VA":
                            return "VACINA";
                        default:
                            return "OUTROS";
                    }
                }
            }
            public List<RelExtratoFinanceiroPaciente> RelExtratoFinanceiro { get; set; }
        }

        public class RelExtratoFinanceiroPaciente
        {
            public int Index { get; set; }
            public int CO_EMP_LOGADA { get; set; }
            public string SiglaUnid { get; set; }
            public string Nome { get; set; }
            public int NIRE { get; set; }
            public string Rap { get; set; }
            public string Responsavel { get; set; }
            public DateTime? DataAgendamento { get; set; }
            public string OrigemProcedimento { get; set; }
            public string CodigoProcedimento { get; set; }
            public string Procedimento { get; set; }
            public string Procedimento_V
            {
                get
                {
                    return (this.CodigoProcedimento + " - " + this.Procedimento).Length > 80 ? ((this.CodigoProcedimento + " - " + this.Procedimento).Substring(0, 77) + "...") : (this.CodigoProcedimento + " - " + this.Procedimento);
                }
            }
            public string Profissional { get; set; }
            public string SIGLA_ENTI_PROFI { get; set; }
            public string NU_ENTI_PROFI { get; set; }
            public string DE_Profissional
            {
                get
                {
                    return String.IsNullOrEmpty(this.SIGLA_ENTI_PROFI) || String.IsNullOrEmpty(this.NU_ENTI_PROFI) ? this.Profissional : this.Profissional + "(" + this.SIGLA_ENTI_PROFI + " " + this.NU_ENTI_PROFI + ")";
                }
            }
            public string Operadora { get; set; }
            public string RLZ { get; set; }

            public decimal ValorUnit { get; set; }

            public string CpfResp { get; set; }
            public string CpfRespDesc
            {
                get
                {
                    string retorno;
                    if (this.CpfResp != null)
                    {
                        retorno = this.CpfResp.Insert(3, ".");
                        retorno = retorno.Insert(7, ".");
                        retorno = retorno.Insert(11, "-");
                    }
                    else
                    {
                        retorno = "-";

                    }
                    return retorno;
                }
            }
            public string DescResp
            {
                get
                {
                    return "Responsável Financeiro: " + this.Responsavel + " (" + "CPF: " + this.CpfRespDesc + ")";
                }
            }

            public string CO_SITU { get; set; }

            public string DE_SITU
            {
                get
                {
                    switch (this.CO_SITU.Substring(0, 1))
                    {
                        case "A":
                            return "AGE";
                        case "C":
                            return "CAN";
                        case "R":
                            return "REA";
                        default:
                            return "-";
                    }
                }
            }

            public string CO_TIPO_EMP
            {
                get
                {
                    return TB25_EMPRESA.RetornaPelaChavePrimaria(this.CO_EMP_LOGADA).CO_TIPO_UNID;
                }
            }
            public string DescAlu
            {
                get
                {
                    return "( NIRE: " + this.NIRE + " )";
                }
            }
        }
        #endregion
    }
}
