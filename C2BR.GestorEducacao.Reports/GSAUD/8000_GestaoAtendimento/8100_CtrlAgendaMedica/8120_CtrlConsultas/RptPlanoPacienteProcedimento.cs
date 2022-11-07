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
    public partial class RptPlanoPacienteProcedimento : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptPlanoPacienteProcedimento()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(
              string parametros,
              string infos,
              int coEmp,
              int Unidade,
              int Operadora,
              int Plano,
              int Profissional,
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
                {
                    lblTitulo.Text = "-";
                }
                else
                {
                    lblTitulo.Text = NomeFuncionalidade.ToUpper();
                }

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;
                DateTime DataInical = Convert.ToDateTime(dataIni);
                DateTime DataFinal = Convert.ToDateTime(dataFim);
                // Setar o header do relatorio
                this.BaseInit(header);


                var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                           join tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros() on tb07.TB250_OPERA.ID_OPER equals tbs356.TB250_OPERA.ID_OPER
                           join tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros() on tbs356.CO_OPER equals tbs174.TB250_OPERA.CO_OPER
                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                           where (tbs174.DT_AGEND_HORAR >= DataInical && tbs174.DT_AGEND_HORAR <= DataFinal
                          && (Unidade != 0 ? tb07.CO_EMP == Unidade : Unidade == 0)
                            && (Operadora != 0 ? tb07.TB250_OPERA.ID_OPER == Operadora : Operadora == 0)
                            && (Plano != 0 ? tb07.TB251_PLANO_OPERA.ID_PLAN == Plano : Plano == 0)
                            && (situacao != "" ? tb07.CO_SITU_ALU == situacao : situacao == "")
                            && Profissional != 0 ? tb03.CO_COL == Profissional : Profissional == 0)
                           select new Relatorio
                {
                    CodPac = tb07.CO_ALU,
                    Paciente = tb07.NO_ALU,
                    Sexo = tb07.CO_SEXO_ALU == "" ? "-" : tb07.CO_SEXO_ALU == null ? "-" : tb07.CO_SEXO_ALU,
                    DataNasc = tb07.DT_NASC_ALU.Value,
                    Plano = tb07.TB251_PLANO_OPERA.NM_SIGLA_PLAN,

                    Procedimento = tbs356.CO_PROC_MEDI + " - " + tbs356.NM_PROC_MEDI,
                }).DistinctBy(a => a.CodPac).OrderBy(w => w.Paciente).ToList();




                if (res.Count == 0)
                    return -1;
                //Adiciona ao DataSource do Relatório
                bsReport.Clear();
                foreach (var item in res)
                {
                    bsReport.Add(item);

                }
                return 1;
            }
            catch { return 0; }
        }

        #endregion

        public class Relatorio
        {
            public string Paciente { get; set; }
            public string Sexo { get; set; }
            public DateTime? DataNasc { get; set; }
            public string Data
            {
                get
                {
                    if (this.DataNasc == null)
                    {
                        return "-";

                    }
                    else
                    {
                        DateTime data = Convert.ToDateTime(DataNasc);
                        return data.ToString("dd/mm/yyyy");

                    }

                }
            }
            public string Plano { get; set; }
            public string Operadora { get; set; }
            public int CodPac { get; set; }
            public string CodProc { get; set; }
            public string Procedimento { get; set; }
            public string Profissional { get; set; }
            public string QPA { get; set; }
            public string QPR { get; set; }
            public string valorUnitario { get; set; }
            public string ValorTotal { get; set; }
            public int QtdAtendimento
            {
                get
                {
                    int res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                               join tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros() on tb07.TB250_OPERA.ID_OPER equals tbs356.TB250_OPERA.ID_OPER
                               where tb07.CO_ALU == this.CodPac && tbs356.CO_PROC_MEDI == this.CodProc
                               select new
                               {

                               }).ToList().Count();
                    return res;

                }
            }
        }
    }
}
