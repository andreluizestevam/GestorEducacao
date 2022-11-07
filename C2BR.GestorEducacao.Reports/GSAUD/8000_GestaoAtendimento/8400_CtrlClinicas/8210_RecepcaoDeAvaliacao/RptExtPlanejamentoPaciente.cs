using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.Reports.Helper;
using System.Globalization;
using C2BR.GestorEducacao.Reports;
using DevExpress.XtraCharts;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8400_CtrlClinicas._8210_RecepcaoDeAvaliacao
{
    public partial class RptExtPlanejamentoPaciente : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptExtPlanejamentoPaciente()
        {
            InitializeComponent();
        }



        #region Init Report

        public int InitReport(string parametros,
                              string infos,
                              int coEmp,
                             int CoUnidade,
                             int Paciente, int Operadora, int Plano, int Categoria, string dataIni, string dataFim, string OrdenadoPor, string Titulo
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


                var res = (from tbs368 in TBS368_RECEP_SOLIC_ITENS.RetornaTodosRegistros()
                           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs368.TBS367_RECEP_SOLIC.CO_ALU equals tb07.CO_ALU
                           //join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs368.CO_COL_OBJET_ANALI equals tb03.CO_COL
                           //join tbs370 in TBS370_PLANO_ACAO_PROCE.RetornaTodosRegistros() on tbs368.ID_RECEP_SOLIC_ITENS equals tbs370.TBS368_RECEP_SOLIC_ITENS1.ID_RECEP_SOLIC_ITENS
                           where (CoUnidade != 0 ? tbs368.CO_EMP_CADAS == CoUnidade : 0 == 0)
                           && (Paciente != 0 ? tb07.CO_ALU == Paciente : 0 == 0)
                               && (Plano != 0 ? tbs368.NU_PLAN_SAUDE == Plano : 0 == 0)
                               && (Categoria != 0 ? tbs368.TB367_CATEG_PLANO_SAUDE.ID_CATEG_PLANO_SAUDE == Categoria : 0 == 0)
                               && (Operadora != 0 ? tbs368.TB250_OPERA.ID_OPER == Operadora : 0 == 0)
                           && ((tbs368.DT_CADAS >= dataIni1) && (tbs368.DT_CADAS <= dataFim1))
                           // where (CoUnidade != 0 ? tbs367.CO_EMP == CoUnidade : 0 == 0)
                           //&& (Paciente != 0 ? tb07.CO_ALU == Paciente : 0 == 0)
                           //&& (Plano != 0 ? tbs368.NU_PLAN_SAUDE == Plano : 0 == 0)
                           //&& (Categoria != 0 ? tbs368.TB367_CATEG_PLANO_SAUDE.ID_CATEG_PLANO_SAUDE == Categoria : 0 == 0)
                           //&& (Operadora != 0 ? tbs368.TB250_OPERA.ID_OPER == Operadora : 0 == 0)
                           //&& ((tbs367.DT_CADAS >= dataIni1) && (tbs367.DT_CADAS <= dataFim1))
                           select new ExtratoAgendamento
                           {
                               Procedimento = tbs368.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
                               CoProcedimento = tbs368.TBS356_PROC_MEDIC_PROCE.CO_PROC_MEDI,
                               DataHoraRecebe = tbs368.DT_CADAS,
                               Paciente = tb07.NO_ALU,
                               Neri = tb07.NU_NIRE,
                               //QuantidadeSecao = tbs370.QT_SESSO,
                               QuantidadeQuantidadeSecaoAutorizadaPlanoSaude = tbs368.NU_QTDE_SESSO,
                              // DataHoraInicioRecebe = tbs370.DT_INICIO,
                              // DataHoraFinalRecebe = tbs370.DT_PREV_TERM,
                               //Profissional = tb03.NO_COL

                           }).ToList();
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


        public class ExtratoAgendamento
        {
            public string DataHora
            {


                get
                {
                    if (DataHoraRecebe == null)
                    {
                        return "-";
                    }
                    else
                    {
                        string data = this.DataHoraRecebe.Value.ToShortDateString() + " - " + this.DataHoraRecebe.Value.ToShortTimeString();
                        return data;
                    }

                }

            }
            public DateTime? DataHoraRecebe { get; set; }

            public string Paciente { get; set; }
            public int Neri { get; set; }
            public string PacienteNeri
            {
                get
                {
                    if (Paciente.Length > 21)
                    {
                        string NovoPacientel = Paciente.Substring(0, 19);
                        return Convert.ToString(this.Neri).PadLeft(7, '0') + " - " + NovoPacientel + "...";
                    }
                    else
                    {
                        return Convert.ToString(this.Neri).PadLeft(7, '0') + " - " + this.Paciente;
                    }
                }
            }

            public string Procedimento { get; set; }
            public string CoProcedimento { get; set; }
            public string ProcedimentoDoPaciente
            {

                get
                {


                    return this.CoProcedimento + " - " + this.Procedimento;
                }

            }

            public DateTime DataHoraInicioRecebe { get; set; }
            public DateTime DataHoraFinalRecebe { get; set; }

            public string DataHoraInicio
            {
                get
                {
                    return this.DataHoraInicioRecebe.ToShortDateString();

                }
            }
            public string DataHoraFinal
            {
                get
                {
                    return this.DataHoraInicioRecebe.ToShortDateString();

                }
            }

            public int? QuantidadeSecao { get; set; }
            public int? QuantidadeQuantidadeSecaoAutorizadaPlanoSaude { get; set; }

            public string Profissional { get; set; }
            public string ProfissionalSuade
            {
                get
                {
                   
                    if (Profissional.Length > 25)
                    {
                        string NovoProfissional = Profissional.Substring(0, 25);
                        return NovoProfissional + "...";
                    }
                    else
                    {
                        return Profissional;
                    }


                }
            }
        }

    }
}
