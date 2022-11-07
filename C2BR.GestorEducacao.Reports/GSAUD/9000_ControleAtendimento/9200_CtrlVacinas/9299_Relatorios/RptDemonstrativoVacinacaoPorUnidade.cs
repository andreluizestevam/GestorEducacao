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

namespace C2BR.GestorEducacao.Reports.GSAUD._9000_ControleAtendimento._9200_CtrlVacinas._9299_Relatorios
{
    public partial class RptDemonstrativoVacinacaoPorUnidade : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptDemonstrativoVacinacaoPorUnidade()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              string infos,
                              int coEmp,
                              int CoUnidade,
                              int campanha,
                              int grupoRisco,
                              int faixaEtaria,
                              string usuESus,
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

                Random x = new Random();

                var res = (from tbs341 in TBS341_CAMP_ATEND.RetornaTodosRegistros()
                           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs341.CO_ALU equals tb07.CO_ALU
                           join tbs339 in TBS339_CAMPSAUDE.RetornaTodosRegistros() on tbs341.ID_CAMP_ATEND equals tbs339.ID_CAMPAN
                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs341.CO_COL_ATEND equals tb03.CO_COL
                           join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs341.CO_EMP_ATEND equals tb25.CO_EMP
                           join tbs345 in TBS345_VACINA.RetornaTodosRegistros() on tbs341.TBS345_VACINA.ID_VACINA equals tbs345.ID_VACINA
                           where (CoUnidade != 0 ? tbs341.CO_EMP_ALU == CoUnidade : 0 == 0)
                               //&& (Paciente != 0 ? tb07.CO_ALU == Paciente : 0 == 0)
                               //&& (Plano != 0 ? tbs368.NU_PLAN_SAUDE == Plano : 0 == 0)
                               //&& (Categoria != 0 ? tbs368.TB367_CATEG_PLANO_SAUDE.ID_CATEG_PLANO_SAUDE == Categoria : 0 == 0)
                               //&& (Operadora != 0 ? tbs368.TB250_OPERA.ID_OPER == Operadora : 0 == 0)
                               && ((tbs341.DT_ATEND_CAMP >= dataIni1) && (tbs341.DT_ATEND_CAMP <= dataFim1))
                           select new Vacinacao
                           {
                               Paciente = tb07.NO_ALU,
                               Neri = tb07.NU_NIRE,
                               Sexo = tb07.CO_SEXO_ALU,
                               DataNascimento = tb07.DT_NASC_ALU,
                               cod = 1000000,
                               NomeUnidade = tb25.NO_FANTAS_EMP,
                               DataHoraRecebe = tbs341.DT_ATEND_CAMP,
                               Hora = tbs341.HR_ATEND_CAMP,
                               Vacina = !String.IsNullOrEmpty(tbs345.NM_VACINA) ? tbs345.NM_VACINA : "-",
                               Dose = "PRIM",
                               NumLote = "6649436",
                               Matricula = tb03.CO_MAT_COL,
                               Atendente = tb03.NO_APEL_COL
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


        public class Vacinacao
        {
            public string Paciente { get; set; }
            public int Neri { get; set; }
            public string PacienteNeri
            {
                get
                {
                    if (Paciente.Length > 30)
                    {
                        string paci = Paciente.Substring(0, 30);
                        return Convert.ToString(this.Neri).PadLeft(7, '0') + " - " + paci + "...";

                    }
                    else
                    {
                        return Convert.ToString(this.Neri).PadLeft(7, '0') + " - " + this.Paciente;

                    }


                }
            }

            public string Sexo { get; set; }
            public DateTime? DataNascimento { get; set; }
            public string Idade
            {
                get
                {
                    if (DataNascimento.HasValue)
                    {
                        return Funcoes.FormataDataNascimento(DataNascimento.Value);
                    }
                    else
                    {
                        return "-";
                    }
                }
            }

            public int cod { get; set; }
            public string CODESUS
            {
                get
                {
                    return cod.ToString();
                }
            }
            public string CAD { get; set; }
            public string NomeUnidade { get; set; }

            public DateTime DataHoraRecebe { get; set; }
            public string Hora { get; set; }
            public string DataHora
            {
                get
                {
                    string data = this.DataHoraRecebe.ToShortDateString() + " " + this.Hora;
                    return data;
                }
            }

            public string Vacina { get; set; }
            public string Dose { get; set; }
            public string NumLote { get; set; }
            public string Ocorrencia { get; set; }

            public string Atendente { get; set; }
            public string Matricula { get; set; }
            public string AtendenteMatricula
            {
                get
                {
                    return Convert.ToString(this.Matricula) + " - " + this.Atendente;
                }
            }
        }

    }
}
