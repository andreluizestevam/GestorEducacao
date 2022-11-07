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
    public partial class RptDemoResumoAnalisePrevio : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptDemoResumoAnalisePrevio()
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

                /*Retornando erro sempre pois esse relatório usava uma modelagem que foi alterada, e ele ainda não foi refeito 
                * 
                * MAXWELL ALMEIDA - 11/06/2015
                */
                return 0;

                var res = (from tbs367 in TBS367_RECEP_SOLIC.RetornaTodosRegistros()
                           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs367.CO_ALU equals tb07.CO_ALU
                           where (CoUnidade != 0 ? tbs367.CO_EMP == CoUnidade : 0 == 0)
                           && (Paciente != 0 ? tb07.CO_ALU == Paciente : 0 == 0)
                           && ((tbs367.DT_CADAS >= dataIni1) && (tbs367.DT_CADAS <= dataFim1))
                           select new Extrato
                           {
                               IdRecep_Solic = tbs367.ID_RECEP_SOLIC,
                               DataHoraRecebe = tbs367.DT_CADAS,
                               Paciente = tb07.NO_ALU,
                               Neri = tb07.NU_NIRE,
                               Sexo = tb07.CO_SEXO_ALU,
                               DataNascimento = tb07.DT_NASC_ALU,
                               NumeroRegistro = tbs367.NU_REGIS_RECEP_SOLIC,
                               CoPlano = Plano,
                               CoCategoria = Categoria,
                               CoOperadora = Operadora,
                               Med = "Não",
                               Fis = "Não",
                               Psi = "Não",
                               Fon = "Não",
                               Toc = "Não",
          
                           }).DistinctBy(r => r.IdRecep_Solic).ToList();
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
                    default:
                        break;
                }


                //Adiciona ao DataSource do Relatório
                bsReport.Clear();
                foreach (var item in res)
                {
                    


                    //var t = (from tbs368 in TBS368_RECEP_SOLIC_ITENS.RetornaTodosRegistros()
                    //           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs368.CO_COL_OBJET_ANALI equals tb03.CO_COL
                    //           where tbs368.TBS367_RECEP_SOLIC.ID_RECEP_SOLIC == item.IdRecep_Solic
                    //           select new
                    //           {
                    //               tbs368.CO_SITUA,
                    //               tb03.CO_CLASS_PROFI,
                    //           }).ToList();
                   
                    //foreach (var ite in t)
                    //{
                        
                    //    if (ite.CO_CLASS_PROFI == "M")
                    //    {

                    //        item.Med = "Sim";
                    //    }
                    //    if (ite.CO_CLASS_PROFI == "I")
                    //    {
                    //        item.Fis = "Sim";
                    //    }
                    //    if (ite.CO_CLASS_PROFI == "P")
                    //    {
                    //        item.Psi = "Sim";
                    //    }
                    //    if (ite.CO_CLASS_PROFI == "N")
                    //    {
                    //        item.Fon = "Sim";
                    //    }
                    //    else
                    //    {
                    //        item.Toc = "Sim";
                    //    }

                    //}
                    //bsReport.Add(item);
                }
                return 1;
            }
            catch { return 0; }
        }

        #endregion


        public class Extrato
        {
            public int CoPlano { get; set; }
            public int CoCategoria { get; set; }
            public int CoOperadora { get; set; }
            public int CoPaciente { get; set; }
            public int IdRecep_Solic { get; set; }
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

                    return Convert.ToString(this.Neri).PadLeft(7, '0') + " - " + this.Paciente;

                }
            }
            public string Sexo { get; set; }
            public DateTime? DataNascimento { get; set; }

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

            public decimal QuantidadeEncaminhamento
            {
                get
                {

                    var res = (from tbs367 in TBS367_RECEP_SOLIC.RetornaTodosRegistros()
                               join tbs368 in TBS368_RECEP_SOLIC_ITENS.RetornaTodosRegistros() on tbs367.ID_RECEP_SOLIC equals tbs368.TBS367_RECEP_SOLIC.ID_RECEP_SOLIC
                               where tbs367.ID_RECEP_SOLIC == this.IdRecep_Solic && tbs368.CO_SITUA == "E" &&
                               (CoPlano != 0 ? tbs368.NU_PLAN_SAUDE == CoPlano : 0 == 0)
                               && (CoCategoria != 0 ? tbs368.TB367_CATEG_PLANO_SAUDE.ID_CATEG_PLANO_SAUDE == CoCategoria : 0 == 0)
                               && (CoOperadora != 0 ? tbs368.TB250_OPERA.ID_OPER == CoOperadora : 0 == 0)
                               select new
                               {

                                   Situacao = tbs368.CO_SITUA,

                               }).ToList();
                    return res.Count();


                }

            }
            public int QuantidadeSecaoAutorizada
            {
                get
                {

                    var res = (from tbs369 in TBS369_RECEP_REGUL_ITENS.RetornaTodosRegistros()
                               join tbs368 in TBS368_RECEP_SOLIC_ITENS.RetornaTodosRegistros() on tbs369.TBS367_RECEP_SOLIC.ID_RECEP_SOLIC equals tbs368.TBS367_RECEP_SOLIC.ID_RECEP_SOLIC
                               where tbs369.TBS367_RECEP_SOLIC.ID_RECEP_SOLIC == this.IdRecep_Solic
                               && (CoPlano != 0 ? tbs368.NU_PLAN_SAUDE == CoPlano : 0 == 0)
                               && (CoCategoria != 0 ? tbs368.TB367_CATEG_PLANO_SAUDE.ID_CATEG_PLANO_SAUDE == CoCategoria : 0 == 0)
                               && (CoOperadora != 0 ? tbs368.TB250_OPERA.ID_OPER == CoOperadora : 0 == 0)
                               select new
                               {
                                   tbs369.NU_QTDE_SESSO_AUTOR,
                               }).ToList();

                    return res.Count();

                }

            }
            public int QuantidadeProcedimentos
            {

                get
                {
                    var res = (from tbs367 in TBS367_RECEP_SOLIC.RetornaTodosRegistros()
                               join tbs368 in TBS368_RECEP_SOLIC_ITENS.RetornaTodosRegistros() on tbs367.ID_RECEP_SOLIC equals tbs368.TBS367_RECEP_SOLIC.ID_RECEP_SOLIC
                               where tbs367.ID_RECEP_SOLIC == this.IdRecep_Solic &&
                               (CoPlano != 0 ? tbs368.NU_PLAN_SAUDE == CoPlano : 0 == 0)
                               && (CoCategoria != 0 ? tbs368.TB367_CATEG_PLANO_SAUDE.ID_CATEG_PLANO_SAUDE == CoCategoria : 0 == 0)
                               && (CoOperadora != 0 ? tbs368.TB250_OPERA.ID_OPER == CoOperadora : 0 == 0)
                               select new
                               {

                                   Situacao = tbs368.CO_SITUA,

                               }).ToList();

                    if (res != null)
                    {
                        return res.Count();
                    }
                    else
                    {
                        return 0;
                    }
                }

            }

            public string Med { get; set; }
            public string Fis { get; set; }
            public string Psi { get; set; }
            public string Fon { get; set; }
            public string Toc { get; set; }
        }

    }
}
