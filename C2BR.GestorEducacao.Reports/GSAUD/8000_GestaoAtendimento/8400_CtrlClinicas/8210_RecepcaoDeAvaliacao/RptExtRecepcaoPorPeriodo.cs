﻿using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
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

namespace C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8400_CtrlClinicas._8210_RecepcaoDeAvaliacao
{
    public partial class RptExtRecepcaoPorPeriodo : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptExtRecepcaoPorPeriodo()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              string infos,
                              int coEmp,
                             int CoUnidade,
                             int Paciente, int Operadora, int Plano, int Categoria, string dataIni, string dataFim, string OrdenadoPor, string procedimentos, string Titulo
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
                if (procedimentos == "C")
                {
                    var res = (from tbs367 in TBS367_RECEP_SOLIC.RetornaTodosRegistros()
                               join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs367.CO_ALU equals tb07.CO_ALU
                               join tbs368 in TBS368_RECEP_SOLIC_ITENS.RetornaTodosRegistros() on tbs367.ID_RECEP_SOLIC equals tbs368.TBS367_RECEP_SOLIC.ID_RECEP_SOLIC into itns
                               from tbs368 in itns.DefaultIfEmpty()
                               where (CoUnidade != 0 ? tbs367.CO_EMP == CoUnidade : 0 == 0)
                               && (Paciente != 0 ? tb07.CO_ALU == Paciente : 0 == 0)
                               && (Plano != 0 ? tbs368.NU_PLAN_SAUDE == Plano : 0 == 0)
                               && (Categoria != 0 ? tbs368.TB367_CATEG_PLANO_SAUDE.ID_CATEG_PLANO_SAUDE == Categoria : 0 == 0)
                               && (Operadora != 0 ? tbs368.TB250_OPERA.ID_OPER == Operadora : 0 == 0)
                               && ((tbs367.DT_CADAS >= dataIni1) && (tbs367.DT_CADAS <= dataFim1))
                               select new Extrato
                               {
                                   IdRecep_Solic = tbs367.ID_RECEP_SOLIC,
                                   DataHoraRecebe = tbs367.DT_CADAS,
                                   Paciente = tb07.NO_ALU,
                                   Neri = tb07.NU_NIRE,
                                   Sexo = tb07.CO_SEXO_ALU == null ? "-" : tb07.CO_SEXO_ALU == "" ? "" : tb07.CO_SEXO_ALU,
                                   DataNascimento = tb07.DT_NASC_ALU,
                                   TelefoneRecebe = tb07.NU_TELE_RESI_ALU,
                                   NumeroRegistro = tbs367.NU_REGIS_RECEP_SOLIC,
                                   NomePlano = tbs368.TB251_PLANO_OPERA.NM_SIGLA_PLAN != null ? tbs368.TB251_PLANO_OPERA.NM_SIGLA_PLAN : "-",
                                   NomeCategoria = tbs368.TB367_CATEG_PLANO_SAUDE.NM_SIGLA_CATEG != null ? tbs368.TB367_CATEG_PLANO_SAUDE.NM_SIGLA_CATEG : "-",
                                   NomeOperadora = tbs368.TB250_OPERA.NM_SIGLA_OPER != null ? tbs368.TB250_OPERA.NM_SIGLA_OPER : "-",
                                   QtdValorTotalSessoes = (tbs368.VL_LIQUI.HasValue ? tbs368.VL_LIQUI.Value : 0) * (tbs368.NU_QTDE_SESSO.HasValue ? tbs368.NU_QTDE_SESSO.Value : 0),
                                   Qtdsessoes = tbs368.NU_QTDE_SESSO.HasValue ? tbs368.NU_QTDE_SESSO.Value : 0                                   
                               }).ToList();
                    if (res.Count == 0)
                        return -1;
                    switch (OrdenadoPor)
                    {
                        case "PA":
                            res.OrderBy(w => w.Paciente);
                            break;
                        case "NR":
                            res.OrderBy(w => w.NumeroRegistro);
                            break;
                        case "QP":
                            res.OrderBy(w => w.QtdProcedimento);
                            break;
                        case "QS":
                            res.OrderBy(w => w.Qtdsessoes);
                            break;
                        case "TS":
                            res.OrderBy(w => w.QtdValorTotalSessoes);
                            break;
                        case "DT":
                            res.OrderBy(w => w.DataHoraRecebe);
                            break;
                        default:
                            break;
                    }
                    //Adiciona ao DataSource do Relatório
                    bsReport.Clear();
                    foreach (var item in res)
                    {
                        bsReport.Add(item);

                    }
                    return 1;
                }
                else
                {
                    var res = (from tbs367 in TBS367_RECEP_SOLIC.RetornaTodosRegistros()
                               join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs367.CO_ALU equals tb07.CO_ALU
                               where (CoUnidade != 0 ? tbs367.CO_EMP == CoUnidade : 0 == 0)
                               && (Paciente != 0 ? tb07.CO_ALU == Paciente : 0 == 0)
                                   //&& (Plano != 0 ? tbs367.NU_PLAN_SAUDE == Plano : 0 == 0)
                                   //&& (Categoria != 0 ? tbs367.TB367_CATEG_PLANO_SAUDE.ID_CATEG_PLANO_SAUDE == Categoria : 0 == 0)
                                   //&& (Operadora != 0 ? tbs367.TB250_OPERA.ID_OPER == Operadora : 0 == 0)
                               && ((tbs367.DT_CADAS >= dataIni1) && (tbs367.DT_CADAS <= dataFim1))
                               select new Extrato
                               {
                                   IdRecep_Solic = tbs367.ID_RECEP_SOLIC,
                                   DataHoraRecebe = tbs367.DT_CADAS,
                                   Paciente = tb07.NO_ALU,
                                   Neri = tb07.NU_NIRE,
                                   Sexo = tb07.CO_SEXO_ALU == null ? "-" : tb07.CO_SEXO_ALU == "" ? "" : tb07.CO_SEXO_ALU,
                                   DataNascimento = tb07.DT_NASC_ALU,
                                   TelefoneRecebe = tb07.NU_TELE_RESI_ALU,
                                   NumeroRegistro = tbs367.NU_REGIS_RECEP_SOLIC,
                                   //NomePlano = tbs368.TB251_PLANO_OPERA.NM_SIGLA_PLAN != null ? tbs368.TB251_PLANO_OPERA.NM_SIGLA_PLAN : "-",
                                   //NomeCategoria = tbs368.TB367_CATEG_PLANO_SAUDE.NM_SIGLA_CATEG != null ? tbs368.TB367_CATEG_PLANO_SAUDE.NM_SIGLA_CATEG : "-",
                                   //NomeOperadora = tbs368.TB250_OPERA.NM_SIGLA_OPER != null ? tbs368.TB250_OPERA.NM_SIGLA_OPER : "-",
                                   //QtdValorTotalSessoes = (tbs368.VL_LIQUI.HasValue ? tbs368.VL_LIQUI.Value : 0) * (tbs368.NU_QTDE_SESSO.HasValue ? tbs368.NU_QTDE_SESSO.Value : 0),
                                   //Qtdsessoes = tbs368.NU_QTDE_SESSO.HasValue ? tbs368.NU_QTDE_SESSO.Value : 0,
                               }).ToList();
                    if (res.Count == 0)
                        return -1;
                    switch (OrdenadoPor)
                    {
                        case "PA":
                            res.OrderBy(w => w.Paciente);
                            break;
                        case "NR":
                            res.OrderBy(w => w.NumeroRegistro);
                            break;
                        case "QP":
                            res.OrderBy(w => w.QtdProcedimento);
                            break;
                        case "QS":
                            res.OrderBy(w => w.Qtdsessoes);
                            break;
                        case "TS":
                            res.OrderBy(w => w.QtdValorTotalSessoes);
                            break;
                        case "DT":
                            res.OrderBy(w => w.DataHoraRecebe);
                            break;
                        default:
                            break;
                    }
                    //Adiciona ao DataSource do Relatório
                    bsReport.Clear();
                    foreach (var item in res)
                    {
                        bsReport.Add(item);

                    }
                    return 1;
                }

            }
            catch { return 0; }
        }

        #endregion


        public class Extrato
        {
            public string NomePlano { get; set; }
            public string NomeCategoria { get; set; }
            public string NomeOperadora { get; set; }
            public int IdRecep_Solic { get; set; }



            public string DataHora
            {


                get
                {

                    string data = this.DataHoraRecebe.ToShortDateString() + " - " + this.DataHoraRecebe.ToShortTimeString();
                    return data;
                }

            }
            public DateTime DataHoraRecebe { get; set; }

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
            public DateTime? DataNascimento { get; set; }
            public string TelefoneRecebe { get; set; }
            public string Telefone
            {
                get
                {
                    if (TelefoneRecebe == null || TelefoneRecebe == "")
                        return "-";
                        return Funcoes.Format(TelefoneRecebe, TipoFormat.Telefone);

                }

            }
            public string NumeroRegistro { get; set; }
            public int Idade
            {
                get
                {

                    if (DataNascimento != null)
                    {
                        DateTime dt = Convert.ToDateTime(DataNascimento);
                        int anos = DateTime.Now.Year - dt.Year;

                        if (DateTime.Now.Month < dt.Month || (DateTime.Now.Month == dt.Month && DateTime.Now.Day < dt.Day))
                            anos--;

                        return anos;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            public int QtdProcedimento
            {

                get
                {
                    var restbs368 = TBS368_RECEP_SOLIC_ITENS.RetornaTodosRegistros().Where(a => a.TBS367_RECEP_SOLIC.ID_RECEP_SOLIC == this.IdRecep_Solic).ToList();
                    if (restbs368 != null)
                    {
                        return restbs368.Count();
                    }
                    else
                    {
                        return 0;
                    }
                }
            }

            public int Qtdsessoes { get; set; }

            public decimal QtdValorTotalSessoes { get; set; }

            public string Fpp { get; set; }

        }

    }
}
