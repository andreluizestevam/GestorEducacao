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
    public partial class RptExtRecepcaoPorPeriodoDemonstrativo2 : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptExtRecepcaoPorPeriodoDemonstrativo2()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              string infos,
                              int coEmp,
                             int CoUnidade,
                             int Paciente, 
                             int Operadora,
                             int Plano,
                             int Categoria,
                             string dataIni,
                             string dataFim,
                             string OrdenadoPor, 
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

                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(coEmp);

                if (header == null)
                    return 0;

                string cpfCNPJUnid = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp).CO_CPFCGC_EMP;
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
                           //join tbs368 in TBS368_RECEP_SOLIC_ITENS.RetornaTodosRegistros() on tbs367.ID_RECEP_SOLIC equals tbs368.TBS367_RECEP_SOLIC.ID_RECEP_SOLIC
                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs367.CO_COL_CADAS equals tb03.CO_COL
                           where (CoUnidade != 0 ? tbs367.CO_EMP == CoUnidade : 0 == 0)
                           && (Paciente != 0 ? tb07.CO_ALU == Paciente : 0 == 0)
                               //&& (Plano != 0 ? tbs368.NU_PLAN_SAUDE == Plano : 0 == 0)
                               //&& (Categoria != 0 ? tbs368.TB367_CATEG_PLANO_SAUDE.ID_CATEG_PLANO_SAUDE == Categoria : 0 == 0)
                               //&& (Operadora != 0 ? tbs368.TB250_OPERA.ID_OPER == Operadora : 0 == 0)
                           && ((tbs367.DT_CADAS >= dataIni1) && (tbs367.DT_CADAS <= dataFim1))
                           select new Extrato
                           {
                               IdRecep_Solic = tbs367.ID_RECEP_SOLIC,
                               DataHoraRecebe = tbs367.DT_CADAS,
                               Paciente = tb07.NO_ALU,
                               Neri = tb07.NU_NIRE,
                               Sexo = tb07.CO_SEXO_ALU,
                               DataNascimento = tb07.DT_NASC_ALU,
                               TelefoneRecebe = tb07.NU_TELE_RESI_ALU,
                               NumeroRegistro = tbs367.NU_REGIS_RECEP_SOLIC,
                               
                               //-------------------------------------------------------------
                               CoPlano = Plano,
                               CoCategoria = Categoria,
                               CoOperadora = Operadora,
                               //-------------------------------------------------------------k
                               Matricula = tb03.CO_MAT_COL,
                               Atendente = tb03.NO_APEL_COL
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
                    //case "QP":
                    //    res.OrderBy(w => w.QtdProcedimento);
                    //    break;
                    //case "QS":
                    //    res.OrderBy(w => w.Qtdsessoes);
                    //    break;
                    //case "TS":
                    //    res.OrderBy(w => w.QtdValorTotalSessoes);
                    //    break;
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
            catch { return 0; }
        }

        #endregion


        public class Extrato
        {
            public string Plano { get; set; }
            public int IdRecep_Solic { get; set; }

            public string DataHora
            {


                get
                {

                    string data = this.DataHoraRecebe.ToShortDateString() + " " + this.DataHoraRecebe.ToShortTimeString();
                    return data;
                }

            }
            public DateTime DataHoraRecebe { get; set; }

            public string Atendente { get; set; }
            public string Matricula { get; set; }
            public string AtendenteMatricula
            {

                get
                {
                    return Convert.ToString(this.Matricula) + " - " + this.Atendente;

                }

            }

            public string Paciente { get; set; }
            public int Neri { get; set; }
            public string PacienteNeri
            {
                get
                {
                    if (Paciente.Length > 17)
                    {
                        string paci = Paciente.Substring(0, 17);
                        return Convert.ToString(this.Neri).PadLeft(7, '0') + " - " + paci + "...";

                    }
                    else
                    {
                        return Convert.ToString(this.Neri).PadLeft(7, '0') + " - " + this.Paciente;

                    }


                }
            }

            public string TelefoneRecebe { get; set; }
            public string Telefone
            {
                get
                {

                    return Funcoes.Format(TelefoneRecebe, TipoFormat.Telefone);

                }

            }

            public string Sexo { get; set; }
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
            public DateTime? DataNascimento { get; set; }

            public int CoPlano { get; set; }
            public int CoCategoria { get; set; }
            public int CoOperadora { get; set; }
            public int QuantidadeProcedimentosPorPlanoDeSaude
            {

                get
                {
                    int Qtd = TBS368_RECEP_SOLIC_ITENS.RetornaTodosRegistros().Where(reg => reg.TBS367_RECEP_SOLIC.ID_RECEP_SOLIC == this.IdRecep_Solic 
                        && reg.TB251_PLANO_OPERA.ID_PLAN != 0).ToList().Count();
                            
                    return Qtd;


                }


            }

            #region Quantidades
            public string NumeroRegistro { get; set; }
            public int QuantidadePlanoSaude { get; set; }


            public int QuantidadeProcedimentosPorConvenio { get; set; }
            public int QuantidadeProcedimentoParticular
            {
                get
                {
                    int Qtd = TBS368_RECEP_SOLIC_ITENS.RetornaTodosRegistros().Where(reg => reg.TBS367_RECEP_SOLIC.ID_RECEP_SOLIC == this.IdRecep_Solic
                               && reg.TB250_OPERA.ID_OPER != 0
                               ).ToList().Count();
                    return Qtd;

                }
            }
            public int QuantidadeProcedimentosOutros { get; set; }

            public string EmcamiamentoPreAnalise {
                get { return "-"; }
            }
            public decimal QuantidadeProcedimentosAutorizados 
            {
                get
                {
                    decimal Quantidade = TBS369_RECEP_REGUL_ITENS.RetornaTodosRegistros().Where(a => a.TBS367_RECEP_SOLIC.ID_RECEP_SOLIC == this.IdRecep_Solic).ToList().Sum(a => a.NU_QTDE_SESSO_AUTOR.HasValue ? a.NU_QTDE_SESSO_AUTOR.Value : 0);
                    return Quantidade++;
                }
            }
            public decimal QuantidadeSessaoAutorizadas 
            {
                get
                {
                    decimal Quantidade = TBS369_RECEP_REGUL_ITENS.RetornaTodosRegistros().Where(a => a.TBS367_RECEP_SOLIC.ID_RECEP_SOLIC == this.IdRecep_Solic).ToList().Sum(a => a.NU_QTDE_SESSO_AUTOR.HasValue ? a.NU_QTDE_SESSO_AUTOR.Value : 0);
                   return  Quantidade;
                }
            
            }
            public decimal Total
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

            #endregion

        }

    }
}
