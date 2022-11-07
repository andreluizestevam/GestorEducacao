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
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar._3401_DiarioClasseProfessor;

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar
{
    public partial class RptDiarioClasse : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptDiarioClasse()
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
                              int strP_MES,
                              string strP_PROF_RESP,
                              string strP_LAYOUT,
                              DateTime dataInicial,
                              DateTime dataFinal,
                              string strProfessorCod,
                              string strProfessor,
                              string strMateria,
                              DateTime dtIniBim,
                              DateTime dtFimBim,
                              bool verDeta,
                              bool AssinFreq,
                              bool DiarioProfessor,
                              int CO_COL
                              )
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

                //this.lblUnidadeTitulo.Text = header.Unidade;
                //this.lblProfessor.Text = strP_PROF_RESP;
                //this.VisiblePageHeader = false;
                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                decimal dtRef = decimal.Parse(strP_CO_ANO_REFER);

                int intAno = int.Parse(strP_CO_ANO_REFER);
                int valoresFrente = 1;
                int valoresVerso = 1;
                Verso.Visible = true;
                Frente.Visible = true;
                if (strP_LAYOUT == "A" || strP_LAYOUT == "F")
                {
                    if (strP_LAYOUT == "F")
                    {
                        valoresVerso = 0;
                        Verso.Visible = false;
                        GroupFooter1.PageBreak = PageBreak.None;
                    }

                    //Verifica se o usuário escolheu emitir o diário do professor
                    if (DiarioProfessor)
                    {
                        RptDiarioClasseFrenteP rptf = new RptDiarioClasseFrenteP();
                        DevExpress.XtraReports.UI.XtraReport rptfR = rptf.InitReport(parametros, codInst, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, infos, strP_CO_ANO_REFER, strP_BIMESTRE, strP_CO_MAT, strP_MES, strP_PROF_RESP, dataInicial, dataFinal, strProfessorCod, strProfessor, strMateria, dtIniBim, dtFimBim, AssinFreq, CO_COL);
                        if (rptfR != null)
                            Frente.ReportSource = rptfR;
                        else
                        {
                            Frente.Visible = false;
                            GroupFooter1.PageBreak = PageBreak.None;
                            valoresFrente = 0;
                        }
                    }
                    else
                    {   //Chama o diário pelo professor
                        RptDiarioClasseFrente rptf = new RptDiarioClasseFrente();
                        DevExpress.XtraReports.UI.XtraReport rptfR = rptf.InitReport(parametros, codInst, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, infos, strP_CO_ANO_REFER, strP_BIMESTRE, strP_CO_MAT, strP_MES, strP_PROF_RESP, dataInicial, dataFinal, strProfessorCod, strProfessor, strMateria, dtIniBim, dtFimBim, AssinFreq);
                        if (rptfR != null)
                            Frente.ReportSource = rptfR;
                        else
                        {
                            Frente.Visible = false;
                            GroupFooter1.PageBreak = PageBreak.None;
                            valoresFrente = 0;
                        }
                    }
                }

                if (strP_LAYOUT == "A" || strP_LAYOUT == "V")
                {
                    if (strP_LAYOUT == "V")
                    {
                        valoresFrente = 0;
                        Frente.Visible = false;
                        GroupFooter1.PageBreak = PageBreak.None;
                    }

                    //Verifica se o usuário escolheu a versão simplificada ou a detalhada, para chamar o relatório correspondente
                    if (verDeta == true)
                    {
                        //Verifica se o usuário escolheu emitir o diário do professor
                        if (DiarioProfessor)
                        {
                            //Chama o diário pelo professor
                            RptDiarioClasseVersoP rptv = new RptDiarioClasseVersoP();
                            DevExpress.XtraReports.UI.XtraReport rptvR = rptv.InitReport(parametros, codInst, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, infos, strP_CO_ANO_REFER, strP_BIMESTRE, strP_CO_MAT, strP_PROF_RESP, strP_MES, dataInicial, dataFinal, strProfessorCod, strProfessor, strMateria, dtIniBim, dtFimBim, CO_COL);
                            if (rptvR != null)
                                Verso.ReportSource = rptvR;
                            else
                            {
                                Verso.Visible = false;
                                GroupFooter1.PageBreak = PageBreak.None;
                                valoresVerso = 0;
                            }
                        }
                        else
                        {
                            //Chama o diário pela Disciplina
                            RptDiarioClasseVerso rptv = new RptDiarioClasseVerso();
                            DevExpress.XtraReports.UI.XtraReport rptvR = rptv.InitReport(parametros, codInst, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, infos, strP_CO_ANO_REFER, strP_BIMESTRE, strP_CO_MAT, strP_PROF_RESP, strP_MES, dataInicial, dataFinal, strProfessorCod, strProfessor, strMateria, dtIniBim, dtFimBim);
                            if (rptvR != null)
                                Verso.ReportSource = rptvR;
                            else
                            {
                                Verso.Visible = false;
                                GroupFooter1.PageBreak = PageBreak.None;
                                valoresVerso = 0;
                            }
                        }
                    }
                    else
                    {
                        //Verifica se o usuário escolheu emitir o diário do professor
                        if (DiarioProfessor)
                        {
                            //Chama o diário pelo professor
                            RptDiarioClasseVersoSimpP rptv = new RptDiarioClasseVersoSimpP();
                            DevExpress.XtraReports.UI.XtraReport rptvR = rptv.InitReport(parametros, codInst, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, infos, strP_CO_ANO_REFER, strP_BIMESTRE, strP_CO_MAT, strP_PROF_RESP, strP_MES, dataInicial, dataFinal, strProfessorCod, strProfessor, strMateria, dtIniBim, dtFimBim, CO_COL);
                            if (rptvR != null)
                                Verso.ReportSource = rptvR;
                            else
                            {
                                Verso.Visible = false;
                                GroupFooter1.PageBreak = PageBreak.None;
                                valoresVerso = 0;
                            }
                        }
                        else
                        {
                            //Chama o diário pelo Disciplina
                            RptDiarioClasseVersoSimp rptv = new RptDiarioClasseVersoSimp();
                            DevExpress.XtraReports.UI.XtraReport rptvR = rptv.InitReport(parametros, codInst, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, infos, strP_CO_ANO_REFER, strP_BIMESTRE, strP_CO_MAT, strP_PROF_RESP, strP_MES, dataInicial, dataFinal, strProfessorCod, strProfessor, strMateria, dtIniBim, dtFimBim);
                            if (rptvR != null)
                                Verso.ReportSource = rptvR;
                            else
                            {
                                Verso.Visible = false;
                                GroupFooter1.PageBreak = PageBreak.None;
                                valoresVerso = 0;
                            }
                        }
                    }
                }

                if (valoresFrente == 1 || valoresVerso == 1)
                    return 1;
                else
                    return -1;
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

        private void bsReport_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}
