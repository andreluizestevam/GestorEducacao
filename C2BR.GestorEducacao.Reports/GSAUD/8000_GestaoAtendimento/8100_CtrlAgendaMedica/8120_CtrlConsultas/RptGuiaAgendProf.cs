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
using System.Data.Objects;

namespace C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8120_CtrlConsultas
{
    public partial class RptGuiaAgendProf : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptGuiaAgendProf()
        {
            InitializeComponent();
        }
        public int InitReport(
                              string infos,
                              int codEmp,
                              int coCol,
                              DateTime dtIni,
                              DateTime dtFim
            )
        {


            try
            {
                #region Setar o Header e as Labels

                // Instancia o header do relatorio
                ReportHeader header = C2BR.GestorEducacao.Reports.ReportHeader.GetHeaderFromEmpresa(codEmp);
                if (header == null)
                    return 0;
                
                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                string dataIni = dtIni.ToString("yyyy/MM/dd");
                string dataFim = dtFim.ToString("yyyy/MM/dd");
                DateTime dt1 = DateTime.Parse(dataIni);
                DateTime dt2 = DateTime.Parse(dataFim);

                #region Profissional - Agendas
                
                var prof = TB03_COLABOR.RetornaTodosRegistros().Where(x => x.CO_COL == coCol)
                            .Select(x => new  
                            { 
                                nomeProfissional = x.NO_COL + " | " + x.CO_SIGLA_ENTID_PROFI + " - " + x.NU_ENTID_PROFI
                            }).FirstOrDefault();

                var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                           join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tbs174.CO_DEPT equals tb14.CO_DEPTO
                           where tbs174.CO_COL == coCol
                           && tbs174.DT_AGEND_HORAR >= dt1 && tbs174.DT_AGEND_HORAR <= dt2  
                           select new Agendas 
                           {
                               DT_AGEND = tbs174.DT_AGEND_HORAR,
                               CO_PACI = tbs174.CO_ALU,
                               HR_AGEND = tbs174.HR_AGEND_HORAR,
                               SITU = tbs174.CO_SITUA_AGEND_HORAR,
                               LOCAL = tb14.CO_SIGLA_DEPTO,
                               FL_AGEND_ENCAM = tbs174.FL_AGEND_ENCAM,
                               FL_CONF = tbs174.FL_CONF_AGEND,
                               FL_SITUA_TRIAGEM = tbs174.FL_SITUA_TRIAGEM
                           }).OrderBy(w => w.DT_AGEND).ThenBy(w => w.HR_AGEND).ToList();

                Profissional p = new Profissional
                {
                    nomeProfissional = prof.nomeProfissional,
                    dtIni = dtIni,
                    dtFim = dtFim,
                    Agendas = res
                };
                

          #endregion
                bsReport.Clear();
                bsReport.Add(p);
                return 1;
            }
            catch { return 0; }
        }

        public class Profissional
        {
            public string nomeProfissional { get; set; }
            public DateTime dtIni { get; set; }
            public DateTime dtFim { get; set; }
            public string Data { get { return this.dtIni.ToShortDateString() + " até " + this.dtFim.ToShortDateString(); } }
            public List<Agendas> Agendas { get; set; }
        }

        public class Agendas
        {
            public DateTime DT_AGEND { get; set; }
            public String DE_DT_AGEND
            {
                get
                {
                    return this.DT_AGEND.ToShortDateString();
                }
            }
            public String HR_AGEND { get; set; }
            public String LOCAL { get; set; }
            public int? CO_PACI { get; set; }
            public String NO_PACI
            {
                get
                {
                    return (this.CO_PACI.HasValue ? TB07_ALUNO.RetornaPeloCoAlu(CO_PACI.Value).NO_ALU.Length > 37 ? TB07_ALUNO.RetornaPeloCoAlu((int)this.CO_PACI).NO_ALU.Substring(0, 37) + "..." : TB07_ALUNO.RetornaPeloCoAlu((int)this.CO_PACI).NO_ALU : " - ");
                }
            }
            public String PASTA
            {
                get { return this.CO_PACI.HasValue ? !string.IsNullOrEmpty(TB07_ALUNO.RetornaPeloCoAlu(CO_PACI.Value).DE_PASTA_CONTR) ? TB07_ALUNO.RetornaPeloCoAlu(CO_PACI.Value).DE_PASTA_CONTR : "-" : "-"; }
            }
            public String SITU { get; set; }
            public String FL_AGEND_ENCAM { get; set; }
            public String FL_CONF { get; set; }
            public String FL_SITUA_TRIAGEM { get; set; }
            public String DE_SITU
            {
                get
                {
                    //Trata as situações possíveis
                    if (this.SITU == "A")
                    {
                        if (this.FL_AGEND_ENCAM == "S")
                            return "Encaminhado";
                        else if (this.FL_AGEND_ENCAM == "A")
                            return "Atendimento";
                        else if (this.FL_AGEND_ENCAM == "T")
                            return "Triagem";
                        else if (this.FL_CONF == "S" && this.FL_SITUA_TRIAGEM == "S")
                            return "Presente";
                        else if (this.FL_CONF == "S")
                            return "Presente";
                        else if ((this.CO_PACI != null && !TB07_ALUNO.RetornaPeloCoAlu((int)this.CO_PACI).CO_SITU_ALU.Equals("I")) || (this.CO_PACI <= 0 && !TB07_ALUNO.RetornaPeloCoAlu((int)this.CO_PACI).CO_SITU_ALU.Equals("I")))
                            return "Agendado";
                        else if (this.CO_PACI != null && TB07_ALUNO.RetornaPeloCoAlu((int)this.CO_PACI).CO_SITU_ALU.Equals("I"))
                            return "Inativo";
                        else
                            return "Livre";
                    }
                    else if (this.SITU == "C")
                    {
                        return "Cancelado";
                    }
                    else if (this.SITU == "R")
                        return "Relalizado";
                    else if (this.SITU == "M")
                        return "Movimentado";
                    else
                        return "-";
                }
            }
        }
    }
}