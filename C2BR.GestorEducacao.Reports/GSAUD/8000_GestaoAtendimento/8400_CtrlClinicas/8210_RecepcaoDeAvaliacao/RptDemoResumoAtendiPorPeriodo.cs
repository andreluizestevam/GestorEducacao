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
    public partial class RptDemoResumoAtendiPorPeriodo : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptDemoResumoAtendiPorPeriodo()
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
                var res = (from tbs367 in TBS367_RECEP_SOLIC.RetornaTodosRegistros()
                           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs367.CO_ALU equals tb07.CO_ALU
                           where (CoUnidade != 0 ? tbs367.CO_EMP == CoUnidade : 0 == 0)
                           && (Paciente != 0 ? tb07.CO_ALU == Paciente : 0 == 0)
                           && ((tbs367.DT_CADAS >= dataIni1) && (tbs367.DT_CADAS <= dataFim1))
                           select new Demo
                           {
                               IdRecep_Solic = tbs367.ID_RECEP_SOLIC,
                               DataHoraRecebe = tbs367.DT_CADAS,
                               Paciente = tb07.NO_ALU,
                               Neri = tb07.NU_NIRE,
                               Sexo = tb07.CO_SEXO_ALU,
                               DataNascimento = tb07.DT_NASC_ALU,
                               TelefoneRecebe = tb07.NU_TELE_RESI_ALU,
                               NumeroRegistro = tbs367.NU_REGIS_RECEP_SOLIC,
                               CoPlano = Plano,
                               CoCategoria = Categoria,
                               CoOperadora = Categoria,
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
                foreach (Demo item in res)
                {
                    bsReport.Add(res);

                }
                return 1;
            }
            catch { return 0; }
        }

        #endregion


        public class Demo
        {
            public int CoPlano { get; set; }
            public int CoCategoria { get; set; }
            public int CoOperadora { get; set; }

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

                    return Funcoes.Format(TelefoneRecebe, TipoFormat.Telefone);

                }

            }
            public string NumeroRegistro { get; set; }
            public string Idade
            {
                get
                {
                    if (DataNascimento == null)
                    {
                        return "-";
                    }
                    else
                    {
                        return Funcoes.FormataDataNascimento(this.DataNascimento.Value);
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
            public int Qtdsessoes
            {

                get
                {

                    int restbs368s = TBS368_RECEP_SOLIC_ITENS.RetornaTodosRegistros().Where(reg => reg.TBS367_RECEP_SOLIC.ID_RECEP_SOLIC == this.IdRecep_Solic
                        && CoPlano != 0 ? reg.NU_PLAN_SAUDE == CoPlano : reg.NU_PLAN_SAUDE == 0
                        && (CoCategoria != 0 ? reg.TB367_CATEG_PLANO_SAUDE.ID_CATEG_PLANO_SAUDE == CoCategoria : 0 == 0)
                        && CoOperadora != 0 ? reg.TB250_OPERA.ID_OPER == CoOperadora : 0 == 0).ToList().Sum(w => (w.NU_QTDE_SESSO.HasValue ? w.NU_QTDE_SESSO.Value : 0));
                    return Qtdsessoes;





                }

            }
            public decimal QtdValorTotalSessoes
            {

                get
                {




                    var lstItens = TBS368_RECEP_SOLIC_ITENS.RetornaTodosRegistros().Where(reg => reg.TBS367_RECEP_SOLIC.ID_RECEP_SOLIC == this.IdRecep_Solic
                                   && CoPlano != 0 ? reg.NU_PLAN_SAUDE == CoPlano : reg.NU_PLAN_SAUDE == 0
                                   && (CoCategoria != 0 ? reg.TB367_CATEG_PLANO_SAUDE.ID_CATEG_PLANO_SAUDE == CoCategoria : 0 == 0)
                                   && CoOperadora != 0 ? reg.TB250_OPERA.ID_OPER == CoOperadora : 0 == 0);
                    decimal vlTotal = 0;
                    foreach (var i in lstItens)
                    {
                        decimal aux = (i.VL_LIQUI.HasValue ? i.VL_LIQUI.Value : 0) * (i.NU_QTDE_SESSO.HasValue ? i.NU_QTDE_SESSO.Value : 0);
                        vlTotal += aux;
                    }

                    return vlTotal;




                }

            }
            public string Fpp { get; set; }

        }

    }
}
