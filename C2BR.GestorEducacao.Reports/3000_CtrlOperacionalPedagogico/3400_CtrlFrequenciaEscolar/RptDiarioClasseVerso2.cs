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

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar
{
    public partial class RptDiarioClasseVerso2 : C2BR.GestorEducacao.Reports.RptPaisagem
    //public partial class RptDiarioClasseVerso : DevExpress.XtraReports.UI.XtraReport
    {
        public RptDiarioClasseVerso2()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              int codInst,
                              int strP_CO_EMP,
                              int strP_CO_MODU_CUR,
                              int strP_CO_CUR,
                              int strP_CO_TUR,
                              string infos,
                              string strP_CO_ANO_REFER,
                              string strP_BIMESTRE,
                              int strP_CO_MAT,
                              string strP_PROF_RESP)
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(strP_CO_EMP);

                if (header == null)
                    return 0;

                this.lblUnidadeTitulo.Text = header.Unidade;
                this.lblProfessor.Text = strP_PROF_RESP;
                //this.VisiblePageHeader = false;
                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Colaborador Parametrizada

                decimal dtRef = decimal.Parse(strP_CO_ANO_REFER);

                var lstDtFreq = (from tb132 in ctx.TB132_FREQ_ALU
                                 where tb132.TB01_CURSO.CO_EMP == strP_CO_EMP
                                 && tb132.TB01_CURSO.TB44_MODULO.CO_MODU_CUR == strP_CO_MODU_CUR
                                 && tb132.TB01_CURSO.CO_CUR == strP_CO_CUR
                                 && tb132.CO_TUR == strP_CO_TUR
                                 && tb132.CO_ANO_REFER_FREQ_ALUNO == dtRef
                                 && tb132.CO_BIMESTRE == strP_BIMESTRE
                                 && tb132.CO_MAT == strP_CO_MAT
                                 select new RelDataFre
                                 {
                                     DT_FRE = tb132.DT_FRE
                                 }).Distinct();

                DateTime dtIni = lstDtFreq.OrderBy(o => o.DT_FRE).First().DT_FRE;

                DateTime dtFim = lstDtFreq.OrderByDescending(o => o.DT_FRE).First().DT_FRE;

                var lst = (from tb119 in ctx.TB119_ATIV_PROF_TURMA
                           where tb119.CO_EMP == strP_CO_EMP
                           && tb119.CO_MODU_CUR == strP_CO_MODU_CUR
                           && tb119.CO_CUR == strP_CO_CUR
                           && tb119.CO_TUR == strP_CO_TUR
                           && tb119.CO_ANO_MES_MAT == strP_CO_ANO_REFER
                           && (tb119.DT_ATIV_REAL >= dtIni && tb119.DT_ATIV_REAL <= dtFim)
                           select new DiarioClasseVerso
                           {
                               dtAtv = tb119.DT_ATIV_REAL,
                               deResAtv = tb119.DE_RES_ATIV
                           }).OrderBy(o => o.dtAtv);


                var res = lst.ToList();

                int i = 1;
                foreach (DiarioClasseVerso dcv in res)
                {
                    dcv.dtIni = dtIni.ToString("dd/MM/yyyy");
                    dcv.dtFim = dtFim.ToString("dd/MM/yyyy");
                    dcv.Posicao = i;
                    i++;
                }

                #endregion

                // Erro: não encontrou registros
                if (res.ToList().Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (DiarioClasseVerso at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Lista Pauta Chamada Verso

        /*
         * Esta classe foi criada para receber as datas da consulta que retorna as datas que possuem lançamento de frequência
         * */
        public class RelDataFre
        {
            public DateTime DT_FRE { get; set; }
        }
        
        public class DiarioClasseVerso
        {
            public string dtIni { get; set; }
            public string dtFim { get; set; }
            public DateTime dtAtv { get; set; }
            public string DataAtiv
            {
                get
                {
                    return this.dtAtv.ToString("dd/MM");
                }
            }
            public string deResAtv { get; set; }
            public int Posicao { get; set; }
        }
        #endregion
    }
}
