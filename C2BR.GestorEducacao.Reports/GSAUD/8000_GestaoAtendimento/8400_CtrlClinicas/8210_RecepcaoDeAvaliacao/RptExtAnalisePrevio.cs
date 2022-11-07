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
    public partial class RptExtAnalisePrevio : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptExtAnalisePrevio()
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
                    lblTitulo.Text = "Extrato de análise prévio".ToUpper();
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

                /*Retornando erro sempre pois esse relatório usava uma modelagem que foi alterada, e ele ainda não foi refeito 
                 * 
                 * MAXWELL ALMEIDA - 11/06/2015
                 */
                return 0;

                //var res = (from tbs367 in TBS367_RECEP_SOLIC.RetornaTodosRegistros()
                //           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs367.CO_ALU equals tb07.CO_ALU
                //           join tbs368 in TBS368_RECEP_SOLIC_ITENS.RetornaTodosRegistros() on tbs367.ID_RECEP_SOLIC equals tbs368.TBS367_RECEP_SOLIC.ID_RECEP_SOLIC
                //           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs368.CO_COL_CADAS equals tb03.CO_COL
                //           where (CoUnidade != 0 ? tbs367.CO_EMP == CoUnidade : 0 == 0)
                //           && (Paciente != 0 ? tb07.CO_ALU == Paciente : 0 == 0)
                //           && (Plano != 0 ? tbs368.NU_PLAN_SAUDE == Plano : 0 == 0)
                //           && (Categoria != 0 ? tbs368.TB367_CATEG_PLANO_SAUDE.ID_CATEG_PLANO_SAUDE == Categoria : 0 == 0)
                //           && (Operadora != 0 ? tbs368.TB250_OPERA.ID_OPER == Operadora : 0 == 0)
                //           && ((tbs367.DT_CADAS >= dataIni1) && (tbs367.DT_CADAS <= dataFim1))
                //           select new ExtratoAnalise
                //           {
                //               IdRecep_Solic = tbs367.ID_RECEP_SOLIC,
                //               DataHoraRecebeAnalisePrevio = tbs368.DT_CADAS,
                //               Paciente = tb07.NO_ALU,
                //               Neri = tb07.NU_NIRE,
                //               Sexo = tb07.CO_SEXO_ALU,
                //               DataNascimento = tb07.DT_NASC_ALU,
                //               TelefoneRecebe = tb07.NU_TELE_RESI_ALU,
                //               NumeroRegistro = tbs367.NU_REGIS_RECEP_SOLIC,
                //               DataHoraRecebeRecepcao = tbs367.DT_CADAS,
                //               Procedimento = tbs368.TBS356_PROC_MEDIC_PROCE.CO_PROC_MEDI,
                //               ProfissionalSaude = tb03.NO_COL,
                //               ClassificacaoProfissionalRecebe = tb03.CO_CLASS_PROFI
                //           }).ToList();
                //if (res.Count == 0)
                //    return -1;
                //switch (OrdenadoPor)
                //{
                //    case "PA":
                //        res.OrderBy(w => w.Paciente);
                //        break;
                //    case "NR":
                //        res.OrderBy(w => w.NumeroRegistro);
                //        break;
                //    case "DT":
                //        res.OrderBy(w => w.DataHoraAnalisePrevio);
                //        break;
                //    default:
                //        break;
                //}
                ////Adiciona ao DataSource do Relatório
                //bsReport.Clear();
                //foreach (var item in res)
                //{
                //    switch (item.ClassificacaoProfissionalRecebe)
                //    {
                //        case "E":
                //            item.ClassificacaoProfissional = "Enferm...".ToUpper();
                //            break;

                //        case "M":
                //            item.ClassificacaoProfissional = "Médico".ToUpper();
                //            break;
                //        case "D":
                //            item.ClassificacaoProfissional = "Odontó...".ToUpper();
                //            break;
                //        case "F":
                //            item.ClassificacaoProfissional = "Oftalm...".ToUpper();
                //            break;
                //        case "P":
                //            item.ClassificacaoProfissional = "Psicól...".ToUpper();
                //            break;
                //        case "I":
                //            item.ClassificacaoProfissional = "Fisiot...".ToUpper();
                //            break;
                //        case "N":
                //            item.ClassificacaoProfissional = "Fonoau..".ToUpper();
                //            break;
                //        case "T":
                //            item.ClassificacaoProfissional = "Tera..Ocup...".ToUpper();
                //            break;
                //        case "O":
                //            item.ClassificacaoProfissional = "Outros";
                //            break;
                //        default:
                //            item.ClassificacaoProfissional = "-".ToUpper();
                //            break;

                //    }
                //    bsReport.Add(item);

                //}

                //return 1;
            }
            catch { return 0; }
        }

        #endregion


        public class ExtratoAnalise
        {
            public int IdRecep_Solic { get; set; }

            public string DataHoraAnalisePrevio
            {


                get
                {
                    if (DataHoraRecebeAnalisePrevio == null)
                    {
                        return "-";
                    }
                    else
                    {
                        string data = this.DataHoraRecebeAnalisePrevio.Value.ToString("dd/MM/yy") + " " + this.DataHoraRecebeAnalisePrevio.Value.ToShortTimeString();
                        return data;
                    }

                }

            }
            public DateTime? DataHoraRecebeAnalisePrevio { get; set; }

            public string DataHoraRecepcao
            {


                get
                {
                    //return data.ToString("dd/MM/yy") + " - " + this.hora;
                    string data = this.DataHoraRecebeRecepcao.ToString("dd/MM/yy") + " " + this.DataHoraRecebeRecepcao.ToShortTimeString();
                    return data;
                }

            }
            public DateTime DataHoraRecebeRecepcao { get; set; }
            public int Tempo
            {
                get
                {
                    TimeSpan data = DataHoraRecebeRecepcao - DataHoraRecebeAnalisePrevio.Value;
                    return data.Hours;

                }
            }
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
            public string Procedimento { get; set; }
            public string ProfissionalSaude { get; set; }

            public string Profissional
            {
                get
                {

                    if (ProfissionalSaude.Length > 17)
                    {
                        string NovoProfissional = ProfissionalSaude.Substring(0, 20);
                        return NovoProfissional + "...";
                    }
                    else
                    {
                        return ProfissionalSaude;
                    }


                }

            }

            public string ClassificacaoProfissionalRecebe { get; set; }

            public string ClassificacaoProfissional { get; set; }

        }

    }
}
