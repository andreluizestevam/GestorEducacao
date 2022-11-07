using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Text.RegularExpressions;
using C2BR.GestorEducacao.Reports.Helper;

namespace C2BR.GestorEducacao.Reports._1000_CtrlAdminEscolar._1300_ServicosApoioAdministrativo._1310_CtrlAgendaAtividadesFuncional
{
    public partial class RptRelHistoTarefAgendada : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptRelHistoTarefAgendada()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                               int codInst,
                               int codEmp,
                               int coEmpRef,
                               DateTime strP_DT_INI,
                               DateTime strP_DT_FIM,
                               int strP_CO_RESP,
                               string strP_PRIOR,
                               int strP_CO_SOLIC,
                               string infos)
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape                 
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(codEmp);
                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Tarefas Agendadas

                var lst = (from tb137 in ctx.TB137_TAREFAS_AGENDA
                           where tb137.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == codInst
                            && tb137.CO_EMP == coEmpRef
                            && tb137.CO_COL == strP_CO_RESP
                            && tb137.DT_COMPR_TAREF_AGEND >= strP_DT_INI
                            && tb137.DT_COMPR_TAREF_AGEND <= strP_DT_FIM
                            && (strP_CO_SOLIC != 0 ? tb137.TB03_COLABOR1.CO_COL == strP_CO_SOLIC : strP_CO_SOLIC == 0)
                            && (strP_PRIOR != "T" ? tb137.TB140_PRIOR_TAREF_AGEND.CO_PRIOR_TAREF_AGEND == strP_PRIOR : strP_PRIOR == "T")
                           select new TarefaAgendHisto
                           {
                               Codigo = tb137.CO_CHAVE_UNICA_TAREF,
                               Titulo = tb137.NM_RESUM_TAREF_AGEND,
                               DescTarefa = tb137.DE_DETAL_TAREF_AGEND,
                               DataCompr = tb137.DT_COMPR_TAREF_AGEND,
                               DataCadas = tb137.DT_CADAS_TAREF_AGEND,
                               DataPrazo = tb137.DT_LIMIT_TAREF_AGEND,
                               MatricRespo = tb137.TB03_COLABOR.CO_MAT_COL,
                               SiglaUnidRespo = tb137.TB03_COLABOR.TB25_EMPRESA.sigla,
                               NomeRespo = tb137.TB03_COLABOR.NO_COL,
                               MatricSolic = tb137.TB03_COLABOR1.CO_MAT_COL,
                               SiglaUnidSolic = tb137.TB03_COLABOR1.TB25_EMPRESA.sigla,
                               NomeSolic = tb137.TB03_COLABOR1.NO_COL,
                               Status = tb137.TB139_SITU_TAREF_AGEND.DE_SITU_TAREF_AGEND,
                               Prioridade = tb137.TB140_PRIOR_TAREF_AGEND.DE_PRIOR_TAREF_AGEND,
                               EnviaSMS = tb137.CO_FLA_SMS_TAREF_AGEND
                           }).OrderBy(p => p.Codigo).ThenBy(p => p.DataCompr);

                var res = lst.ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta lista no DataSource do Relatorio
                bsReport.Clear();
                foreach (TarefaAgendHisto at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion                
    }

    #region Classe Histórico de Tarefas Agendadas do Relatorio

    public class TarefaAgendHisto
    {
        public double Codigo { get; set; }
        public string Titulo { get; set; }
        public string DescTarefa { get; set; }
        public string SiglaUnidRespo { get; set; }
        public string MatricRespo { get; set; }
        public string NomeRespo { get; set; }
        public string SiglaUnidSolic { get; set; }
        public string MatricSolic { get; set; }
        public string NomeSolic { get; set; }
        public DateTime DataCompr { get; set; }
        public DateTime? DataPrazo { get; set; }
        public DateTime DataCadas { get; set; }
        public string Status { get; set; }
        public string Prioridade { get; set; }
        public string EnviaSMS { get; set; }


        public string CodigoDesc
        {
            get
            {
                return this.Codigo.ToString().PadLeft(6, '0') + " - " + this.Titulo;
            }
        }

        public string ResponDesc
        {
            get
            {
                return this.MatricRespo.Insert(5, "-").Insert(2, ".") + " " + this.NomeRespo;
            }
        }

        public string SolicDesc
        {
            get
            {
                return this.MatricSolic.Insert(5, "-").Insert(2, ".") + " " + this.NomeSolic;
            }
        }

        public string EnviaSMSDesc
        {
            get
            {
                return this.EnviaSMS == "S" ? "Sim" : "Não";
            }
        }
    }
    #endregion
}
