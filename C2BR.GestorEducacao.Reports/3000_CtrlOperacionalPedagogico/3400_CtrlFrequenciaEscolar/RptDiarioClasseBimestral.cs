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
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar._3402_DiarioClassePaginaNotas;
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar._3405_ModelosDiario;
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar._3404_ClassesDiario;

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar
{
    public enum EModeloDiario
    {
        Padrao,
        ColegioSupremo,
        ColegioEspecifico,
        ModeloSupremoNovo,
    }

    public enum ETipoDiario
    {
        Completo,
        SemFolhaResumo,
        ApenasFolhaResumo,
    }

    public partial class RptDiarioClasseBimestral : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptDiarioClasseBimestral()
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
                              bool DiarioProfessor,
                              int CO_COL,
                              bool ImprimeMedias,
                              bool PresencaAst,
                              bool AssinFreq,
                              ETipoDiario tpDiario = ETipoDiario.Completo,
                              EModeloDiario ModeleDiario = EModeloDiario.Padrao
                              )
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                decimal dtRef = decimal.Parse(strP_CO_ANO_REFER);
                this.Parametros = DiarioFrente.MontaParametros(parametros,strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, dtRef, strP_CO_ANO_REFER, strP_CO_MAT, dtIniBim, dtFimBim, strP_BIMESTRE, CO_COL);


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

                int intAno = int.Parse(strP_CO_ANO_REFER);
                int valoresFrente = 1;
                int valoresFrentePg2 = 0;
                int valoresVerso = 1;
                int valoresMeio = 1;
                Verso.Visible = true;
                Frente.Visible = true;
                if (strP_LAYOUT == "A" || strP_LAYOUT == "F")
                {
                    if (strP_LAYOUT == "F")
                    {
                        Verso.Visible = false;
                        GroupFooter1.PageBreak = PageBreak.None;
                    }

                    #region Mostrar Frente

                    //Verifica se o usuário escolheu emitir o diário do professor
                    if (DiarioProfessor)
                    {
                        #region Quando é professor

                        RptDiarioClasseFrenteBP rptf = new RptDiarioClasseFrenteBP();
                        DevExpress.XtraReports.UI.XtraReport rptfR = rptf.InitReport(parametros, codInst, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, infos, strP_CO_ANO_REFER, strP_BIMESTRE, strP_CO_MAT, strP_PROF_RESP, dataInicial, dataFinal, strProfessorCod, strProfessor, strMateria, dtIniBim, dtFimBim, CO_COL, PresencaAst, AssinFreq);
                        if (rptfR != null)
                        {
                            if (tpDiario != ETipoDiario.ApenasFolhaResumo) //Apenas mostra a frente se a opção for tipo completo
                                Frente.ReportSource = rptfR;
                            else
                            {
                                Frente.Visible = false;
                                GroupFooter1.PageBreak = PageBreak.None;
                                valoresFrente = 0;
                            }
                        }
                        else
                        {
                            Frente.Visible = false;
                            GroupFooter1.PageBreak = PageBreak.None;
                            valoresFrente = 0;
                        }

                        //Se é por profesor, não mostrará o diário com notas
                        FrenteNotas.Visible = false;
                        GroupFooter3.PageBreak = PageBreak.None;

                        #endregion
                    }
                    else
                    {
                        #region Quando não é professor

                        ///Verifica o tipo de diário e mostra a frente de acordo com o escolhido
                        switch (ModeleDiario)
                        {
                            case EModeloDiario.ModeloSupremoNovo: // Modelo novo com estrutura diferenciada
                                #region Modelo Novo

                                #region Página da Frente

                                RptDiarioClasseFrenteBimSupremo rptFN = new RptDiarioClasseFrenteBimSupremo();
                                DevExpress.XtraReports.UI.XtraReport rptfNR = rptFN.InitReport(parametros, codInst, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, infos, strP_CO_ANO_REFER, strP_BIMESTRE, strP_CO_MAT, strP_PROF_RESP, dataInicial, dataFinal, strProfessorCod, strProfessor, strMateria, dtIniBim, dtFimBim, ImprimeMedias, PresencaAst, AssinFreq);
                                if (rptfNR != null)
                                {
                                    // Só vai mostrar a frente se a opção for diferente de apenas folha resumo
                                    if (tpDiario != ETipoDiario.ApenasFolhaResumo)
                                        Frente.ReportSource = rptfNR;
                                    else
                                    {
                                        Frente.Visible = false;
                                        GroupFooter1.PageBreak = PageBreak.None;
                                        valoresFrente = 0;
                                    }
                                }
                                else
                                {
                                    Frente.Visible = false;
                                    GroupFooter1.PageBreak = PageBreak.None;
                                    valoresFrente = 0;
                                }

                                #endregion

                                #region 2 Página da Frente

                                RptDiarioClasseFrenteBimSupremoPg2 rptFNpg2 = new RptDiarioClasseFrenteBimSupremoPg2();
                                DevExpress.XtraReports.UI.XtraReport rptfNRpg2 = rptFNpg2.InitReport(parametros, codInst, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, infos, strP_CO_ANO_REFER, strP_BIMESTRE, strP_CO_MAT, strP_PROF_RESP, dataInicial, dataFinal, strProfessorCod, strProfessor, strMateria, dtIniBim, dtFimBim, ImprimeMedias, PresencaAst, AssinFreq);
                                if (rptfNRpg2 != null)
                                {
                                    // Só vai mostrar a frente se a opção for diferente de apenas folha resumo
                                    if (tpDiario != ETipoDiario.ApenasFolhaResumo)
                                    {
                                        Frente2Pagina.ReportSource = rptfNRpg2;
                                        valoresFrentePg2 = 1;
                                    }
                                    else
                                    {
                                        Frente2Pagina.Visible = false;
                                        GroupFooter4.PageBreak = PageBreak.None;
                                        valoresFrentePg2 = 0;
                                    }
                                }
                                else
                                {
                                    Frente2Pagina.Visible = false;
                                    GroupFooter4.PageBreak = PageBreak.None;
                                    valoresFrentePg2 = 0;
                                }

                                #endregion

                                #endregion
                                break;

                            case EModeloDiario.Padrao: // Modelo padrão, o que já é feito hoje
                            default:
                                #region ModeloPadrao
                                RptDiarioClasseFrenteBimestral2 rptf = new RptDiarioClasseFrenteBimestral2();
                                DevExpress.XtraReports.UI.XtraReport rptfR = rptf.InitReport(parametros, codInst, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, infos, strP_CO_ANO_REFER, strP_BIMESTRE, strP_CO_MAT, strP_PROF_RESP, dataInicial, dataFinal, strProfessorCod, strProfessor, strMateria, dtIniBim, dtFimBim, ImprimeMedias, PresencaAst, AssinFreq);
                                if (rptfR != null)
                                {
                                    // Só vai mostrar a frente se a opção for diferente de apenas folha resumo
                                    if (tpDiario != ETipoDiario.ApenasFolhaResumo)
                                        Frente.ReportSource = rptfR;
                                    else
                                    {
                                        Frente.Visible = false;
                                        GroupFooter1.PageBreak = PageBreak.None;
                                        valoresFrente = 0;
                                    }

                                }
                                else
                                {
                                    Frente.Visible = false;
                                    GroupFooter1.PageBreak = PageBreak.None;
                                    valoresFrente = 0;
                                }
                                #endregion

                                Frente2Pagina.Visible = false;
                                GroupFooter4.PageBreak = PageBreak.None;
                                valoresFrentePg2 = 0;

                                break;
                        }

                        #endregion

                        //Segunda parte da frente, onde são mostradas as notas
                        #region Trata para mostrar a segunda página da frente, onde serão apresentadas as notas de cada aluno

                        #region Tipo de Diário Diferente de Sem Folha Resumo de Notas
                        //Mostra a folha resumo apenas se o tipo for diferente de sem folha resumo
                        if (tpDiario != ETipoDiario.SemFolhaResumo)
                        {
                            switch (ModeleDiario)
                            {
                                case EModeloDiario.ColegioSupremo:
                                    #region Modelo Colégio Supremo

                                    //Se for o modeo do supremo, chama o relatório correspondente
                                    RptDiarioPaginaNotasSupremo rptDPSP = new RptDiarioPaginaNotasSupremo();
                                    DevExpress.XtraReports.UI.XtraReport rptDPSPRpor = rptDPSP.InitReport(parametros, codInst, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, infos, strP_CO_ANO_REFER, strP_BIMESTRE, strP_CO_MAT, strP_PROF_RESP, dataInicial, dataFinal, strProfessorCod, strProfessor, strMateria, dtIniBim, dtFimBim, ImprimeMedias, PresencaAst, AssinFreq);
                                    if (rptDPSPRpor != null)
                                        FrenteNotas.ReportSource = rptDPSPRpor;
                                    else
                                    {
                                        FrenteNotas.Visible = false;
                                        GroupFooter3.PageBreak = PageBreak.None;
                                        //valoresFrente = 0;
                                        valoresMeio = 0;
                                    }
                                    #endregion
                                    break;

                                case EModeloDiario.ColegioEspecifico:
                                    #region Modelo Colégio Específico

                                    //Se for o modeo do Específico, chama o relatório correspondente
                                    RptDiarioPaginaNotasEspecifico rptPNES = new RptDiarioPaginaNotasEspecifico();
                                    DevExpress.XtraReports.UI.XtraReport rptPNESpor = rptPNES.InitReport(parametros, codInst, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, infos, strP_CO_ANO_REFER, strP_BIMESTRE, strP_CO_MAT, strP_PROF_RESP, dataInicial, dataFinal, strProfessorCod, strProfessor, strMateria, dtIniBim, dtFimBim, ImprimeMedias, PresencaAst, AssinFreq);
                                    if (rptPNESpor != null)
                                        FrenteNotas.ReportSource = rptPNESpor;
                                    else
                                    {
                                        FrenteNotas.Visible = false;
                                        GroupFooter3.PageBreak = PageBreak.None;
                                        //valoresFrente = 0;
                                        valoresMeio = 0;
                                    }

                                    #endregion
                                    break;

                                default:
                                    FrenteNotas.Visible = false;
                                    GroupFooter3.PageBreak = PageBreak.None;
                                    valoresMeio = 0;
                                    break;
                            }
                        }
                        else
                        {
                            FrenteNotas.Visible = false;
                            GroupFooter3.PageBreak = PageBreak.None;
                            valoresFrente = 0;
                            valoresMeio = 0;
                        }

                        #endregion

                        #endregion
                    }

                    #endregion
                }

                //Chama o relatório verso
                if (strP_LAYOUT == "A" || strP_LAYOUT == "V")
                {
                    if (strP_LAYOUT == "V")
                    {
                        Frente.Visible = false;
                        GroupFooter1.PageBreak = PageBreak.None;
                    }

                    if (tpDiario != ETipoDiario.ApenasFolhaResumo) // Apenas se tiver sido escolhida emissão completa
                    {
                        //Verifica se o usuário escolheu ver detalhadamente
                        if (verDeta == true)
                        {
                            #region Mostrar Verso Com Detalhes

                            //Verifica se o usuário escolheu emitir o diário do professor ou por disciplina
                            if (DiarioProfessor)
                            {
                                RptDiarioClasseVersoBP rptv = new RptDiarioClasseVersoBP();
                                DevExpress.XtraReports.UI.XtraReport rptvR = rptv.InitReport(parametros, codInst, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, infos, strP_CO_ANO_REFER, strP_BIMESTRE, strP_CO_MAT, strP_PROF_RESP, dataInicial, dataFinal, strProfessorCod, strProfessor, strMateria, dtIniBim, dtFimBim, CO_COL);
                                if (rptvR != null)
                                    Verso.ReportSource = rptvR;
                                else
                                {
                                    Verso.Visible = false;
                                    GroupFooter1.PageBreak = PageBreak.None;
                                    valoresVerso = 0;
                                }
                            }
                            //Se foi escolhido por Disciplina
                            else
                            {
                                RptDiarioClasseVersoBimestral rptv = new RptDiarioClasseVersoBimestral();
                                DevExpress.XtraReports.UI.XtraReport rptvR = rptv.InitReport(parametros, codInst, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, infos, strP_CO_ANO_REFER, strP_BIMESTRE, strP_CO_MAT, strP_PROF_RESP, dataInicial, dataFinal, strProfessorCod, strProfessor, strMateria, dtIniBim, dtFimBim);
                                if (rptvR != null)
                                    Verso.ReportSource = rptvR;
                                else
                                {
                                    Verso.Visible = false;
                                    GroupFooter1.PageBreak = PageBreak.None;
                                    valoresVerso = 0;
                                }
                            }

                            #endregion
                        }
                        else
                        {
                            #region Mostrar Verso Sem Detalhes

                            //Verifica se o usuário escolheu emitir o diário do professor
                            if (DiarioProfessor)
                            {
                                RptDiarioClasseVersoBSimpP rptv = new RptDiarioClasseVersoBSimpP();
                                DevExpress.XtraReports.UI.XtraReport rptvR = rptv.InitReport(parametros, codInst, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, infos, strP_CO_ANO_REFER, strP_BIMESTRE, strP_CO_MAT, strP_PROF_RESP, dataInicial, dataFinal, strProfessorCod, strProfessor, strMateria, dtIniBim, dtFimBim, CO_COL);
                                if (rptvR != null)
                                    Verso.ReportSource = rptvR;
                                else
                                {
                                    Verso.Visible = false;
                                    GroupFooter1.PageBreak = PageBreak.None;
                                    valoresVerso = 0;
                                }
                            }
                            //Se foi escolhido por Disciplina
                            else
                            {
                                RptDiarioClasseVersoBimestralSimp rptv = new RptDiarioClasseVersoBimestralSimp();
                                DevExpress.XtraReports.UI.XtraReport rptvR = rptv.InitReport(parametros, codInst, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, infos, strP_CO_ANO_REFER, strP_BIMESTRE, strP_CO_MAT, strP_PROF_RESP, dataInicial, dataFinal, strProfessorCod, strProfessor, strMateria, dtIniBim, dtFimBim);
                                if (rptvR != null)
                                    Verso.ReportSource = rptvR;
                                else
                                {
                                    Verso.Visible = false;
                                    GroupFooter1.PageBreak = PageBreak.None;
                                    valoresVerso = 0;
                                }
                            }

                            #endregion
                        }
                    }
                    else
                    {
                        Verso.Visible = false;
                        GroupFooter1.PageBreak = PageBreak.None;
                        valoresVerso = 0;
                    }
                }

                if (valoresFrente == 1 || valoresVerso == 1 || valoresMeio == 1 || valoresFrentePg2 == 1)
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
    }
}
