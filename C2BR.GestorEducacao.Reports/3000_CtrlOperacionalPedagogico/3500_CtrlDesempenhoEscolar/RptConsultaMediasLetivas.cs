using System;
using System.Linq;
using System.Drawing;
using C2BR.GestorEducacao.Reports.Helper;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Data.Objects;
using System.Text;

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3500_CtrlDesempenhoEscolar
{
    public partial class RptConsultaMediasLetivas : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptConsultaMediasLetivas()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              int coEmp,
                              string infos,
                              int CO_EMP_REF,
                              string anoMesMat,
                              int serie,
                              int turma,
                              int materia,
                              int modalidade,
                              string nomeFunc
                              )
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = C2BR.GestorEducacao.Reports.ReportHeader.GetHeaderFromEmpresa(coEmp);

                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                if (nomeFunc != null) { lblTitulo.Text = nomeFunc; } else { lblTitulo.Text = "CONSULTA DE MÉDIAS LETIVAS"; }

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                var res = (from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                           join tb08 in TB08_MATRCUR.RetornaTodosRegistros() on tb079.CO_ALU equals tb08.CO_ALU
                           where tb08.TB25_EMPRESA.CO_EMP == CO_EMP_REF && tb08.CO_ANO_MES_MAT == anoMesMat
                           && tb08.CO_CUR == serie && tb08.CO_TUR == turma && tb079.CO_MAT == materia && tb08.TB44_MODULO.CO_MODU_CUR == modalidade
                           select new Saida
                           {
                               CO_EMP = tb079.CO_EMP,
                               CO_ALU = tb079.CO_ALU,
                               NO_ALU = tb08.TB07_ALUNO.NO_ALU,
                               CO_ANO_REF = tb079.CO_ANO_REF,
                               CO_MAT = tb079.CO_MAT,
                               CO_CUR = tb079.CO_CUR,
                               CO_ALU_CAD_R = tb08.CO_ALU_CAD.Insert(2, ".").Insert(6, "."),
                               NU_NIRE = tb08.TB07_ALUNO.NU_NIRE,
                               VL_MEDIA_BIM1 = tb079.VL_MEDIA_BIM1,
                               VL_MEDIA_BIM2 = tb079.VL_MEDIA_BIM2,
                               VL_MEDIA_BIM3 = tb079.VL_MEDIA_BIM3,
                               VL_MEDIA_BIM4 = tb079.VL_MEDIA_BIM4,
                               VL_MEDIA_TRI1 = tb079.VL_MEDIA_TRI1,
                               VL_MEDIA_TRI2 = tb079.VL_MEDIA_TRI2,
                               VL_MEDIA_TRI3 = tb079.VL_MEDIA_TRI3,
                               FL_HOMOL_NOTA_BIM1 = tb079.FL_HOMOL_NOTA_BIM1,
                               FL_HOMOL_NOTA_BIM2 = tb079.FL_HOMOL_NOTA_BIM2,
                               FL_HOMOL_NOTA_BIM3 = tb079.FL_HOMOL_NOTA_BIM3,
                               FL_HOMOL_NOTA_BIM4 = tb079.FL_HOMOL_NOTA_BIM4,
                               FL_HOMOL_NOTA_TRI1 = tb079.FL_HOMOL_NOTA_TRI1,
                               FL_HOMOL_NOTA_TRI2 = tb079.FL_HOMOL_NOTA_TRI2,
                               FL_HOMOL_NOTA_TRI3 = tb079.FL_HOMOL_NOTA_TRI3,
                               VL_PROVA_FINAL = tb079.VL_PROVA_FINAL,
                               CO_STA_APROV_MATERIA_R = tb079.CO_STA_APROV_MATERIA,
                               CO_SIT_MAT = tb08.CO_SIT_MAT,
                           }).OrderBy(w => w.NO_ALU).ToList();

                if (res.Count == 0)
                    return -1;
                // Seta os dados no DataSource do Relatorio
                bsReport.Clear();

                foreach (var at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }


        #endregion

        #region Classe Boletim Aluno

        public class Saida
        {

            //Dados do Aluno
            public int CO_ALU { get; set; }
            public string NO_ALU { get; set; }
            public int NU_NIRE { get; set; }
            public bool ENABLED_V
            {
                get
                {
                    return this.CO_STA_APROV_MATERIA_R == "P" ? true : false;
                }
            }
            public bool ENABLED
            {
                get
                {
                    return this.CO_SIT_MAT == "A" && this.ENABLED_V == true ? true : this.CO_STA_APROV_MATERIA == "P" ? false : false;
                }
            }

            public string tipoReferencia
            {
                get
                {
                    //Verificação da referência utilizada pela empresa 
                    var tipo = "B";

                    var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(CO_EMP);

                    tb25.TB83_PARAMETROReference.Load();
                    if (tb25.TB83_PARAMETRO != null)
                        tipo = tb25.TB83_PARAMETRO.TP_PERIOD_AVAL;

                    string tip = Convert.ToString(tipo);
                    
                    return tip;
                }
            }

            public string MR1
            {
                get
                {
                    string mr = "MB1";

                    if (tipoReferencia == "B") { mr = "MB1"; } else { mr = "MT1"; }

                    return mr;
                }
            }
            public string MR2
            {
                get
                {
                    string mr = "MB2";

                    if (tipoReferencia == "B") { mr = "MB2"; } else { mr = "MT2"; }

                    return mr;
                }
            }
            public string MR3
            {
                get
                {
                    string mr = "MB3";

                    if (tipoReferencia == "B") { mr = "MB3"; } else { mr = "MT3"; }

                    return mr;
                }
            }
            public string MR4
            {
                get
                {
                    string mr = "MB4";

                    if (tipoReferencia == "B") { mr = "MB4"; } else { mr = null; }

                    return mr;
                }
            }

            public string SR
            {
                get
                {
                    string mr = "SB";

                    if (tipoReferencia == "B") { mr = "SB"; } else { mr = "ST"; }

                    return mr;
                }
            }

            //Dados de Matrícula
            public string CO_ALU_CAD_R { get; set; }
            public string CO_ALU_CAD
            {
                get
                {
                    return this.NU_NIRE.ToString().PadLeft(7, '0') + " - " + this.DESC_CO_SIT_MAT;
                }
            }
            public string CO_ANO_REF { get; set; }
            public int CO_CUR { get; set; }
            public int CO_MAT { get; set; }
            public string CO_STA_APROV_MATERIA_R { get; set; }
            public string CO_STA_APROV_MATERIA
            {
                get
                {
                    //Forma que era feito anteriormente, mostrando a situação de acordo com o informado no histórico do aluno
                    //return (this.CO_STA_APROV_MATERIA_R == "A" ? "Aprovado" : this.CO_STA_APROV_MATERIA_R == "R" ? "Reprovado" : this.CO_STA_APROV_MATERIA_R == "P" ? "Prova Final" : "Pendente");

                    //Verificação da referência utilizada pela empresa 
                    var tipo = "B";

                    var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(CO_EMP);

                    tb25.TB83_PARAMETROReference.Load();
                    if (tb25.TB83_PARAMETRO != null)
                        tipo = tb25.TB83_PARAMETRO.TP_PERIOD_AVAL;

                    bool pende = false;

                    if (tipo == "B")
                    {
                        if ((this.VL_MEDIA_BIM1.HasValue) && (this.FL_HOMOL_NOTA_BIM1 != "S"))
                            pende = true;
                        else if ((this.VL_MEDIA_BIM2.HasValue) && (this.FL_HOMOL_NOTA_BIM2 != "S"))
                            pende = true;
                        else if ((this.VL_MEDIA_BIM3.HasValue) && (this.FL_HOMOL_NOTA_BIM3 != "S"))
                            pende = true;
                        else if ((this.VL_MEDIA_BIM4.HasValue) && (this.FL_HOMOL_NOTA_BIM4 != "S"))
                            pende = true;

                        DateTime? dtIniLanc1 = TB82_DTCT_EMP.RetornaPelaEmpresa(this.CO_EMP).DT_LACTO_INICI_BIM1;
                        DateTime? dtIniLanc2 = TB82_DTCT_EMP.RetornaPelaEmpresa(this.CO_EMP).DT_LACTO_INICI_BIM2;
                        DateTime? dtIniLanc3 = TB82_DTCT_EMP.RetornaPelaEmpresa(this.CO_EMP).DT_LACTO_INICI_BIM3;
                        DateTime? dtIniLanc4 = TB82_DTCT_EMP.RetornaPelaEmpresa(this.CO_EMP).DT_LACTO_INICI_BIM4;

                        //Verifica se já iniciou o período de lançamento e caso ainda não exista nota coloca o status Em Aberto
                        bool emAberto = false;
                        int i = 0;
                        DateTime dt = FormataData(DateTime.Now);
                        if (dtIniLanc1.HasValue)
                        {
                            if (dt >= dtIniLanc1.Value)
                            {
                                if (this.VL_MEDIA_BIM1.HasValue)
                                    i++;
                                else
                                    emAberto = true;
                            }
                        }
                        if (dtIniLanc2.HasValue)
                        {
                            if (dt >= dtIniLanc2.Value)
                            {
                                if (this.VL_MEDIA_BIM2.HasValue)
                                    i++;
                                else
                                    emAberto = true;
                            }
                        }
                        if (dtIniLanc3.HasValue)
                        {
                            if (dt >= dtIniLanc3.Value)
                            {
                                if (this.VL_MEDIA_BIM3.HasValue)
                                    i++;
                                else
                                    emAberto = true;
                            }
                        }
                        if (dtIniLanc4.HasValue)
                        {
                            if (dt >= dtIniLanc4.Value)
                            {
                                if (this.VL_MEDIA_BIM4.HasValue)
                                    i++;
                                else
                                    emAberto = true;
                            }
                        }

                        if (emAberto == false)
                            return (pende == false ? "Homologada" : "Pendente");
                        else
                            return "Em Aberto";
                    }
                    else
                    {
                        if ((this.VL_MEDIA_TRI1.HasValue) && (this.FL_HOMOL_NOTA_TRI1 != "S"))
                            pende = true;
                        else if ((this.VL_MEDIA_TRI2.HasValue) && (this.FL_HOMOL_NOTA_TRI2 != "S"))
                            pende = true;
                        else if ((this.VL_MEDIA_TRI3.HasValue) && (this.FL_HOMOL_NOTA_TRI3 != "S"))
                            pende = true;

                        DateTime? dtIniLanc1 = TB82_DTCT_EMP.RetornaPelaEmpresa(this.CO_EMP).DT_LACTO_INICI_TRI1;
                        DateTime? dtIniLanc2 = TB82_DTCT_EMP.RetornaPelaEmpresa(this.CO_EMP).DT_LACTO_INICI_TRI2;
                        DateTime? dtIniLanc3 = TB82_DTCT_EMP.RetornaPelaEmpresa(this.CO_EMP).DT_LACTO_INICI_TRI3;

                        //Verifica se já iniciou o período de lançamento e caso ainda não exista nota coloca o status Em Aberto
                        bool emAberto = false;
                        int i = 0;
                        DateTime dt = FormataData(DateTime.Now);
                        if (dtIniLanc1.HasValue)
                        {
                            if (dt >= dtIniLanc1.Value)
                            {
                                if (this.VL_MEDIA_TRI1.HasValue)
                                    i++;
                                else
                                    emAberto = true;
                            }
                        }
                        if (dtIniLanc2.HasValue)
                        {
                            if (dt >= dtIniLanc2.Value)
                            {
                                if (this.VL_MEDIA_TRI2.HasValue)
                                    i++;
                                else
                                    emAberto = true;
                            }
                        }
                        if (dtIniLanc3.HasValue)
                        {
                            if (dt >= dtIniLanc3.Value)
                            {
                                if (this.VL_MEDIA_TRI3.HasValue)
                                    i++;
                                else
                                    emAberto = true;
                            }
                        }

                        if (emAberto == false)
                            return (pende == false ? "Homologada" : "Pendente");
                        else
                            return "Em Aberto";
                    }
                }
            }
            public string CO_STA_APROV { get; set; }
            public string STATUS
            {
                get
                {
                    return this.CO_STA_APROV == "A" ? "Aprovado" : this.CO_STA_APROV == "R" ? "Reprovado" : "";
                }
            }
            public string CO_SIT_MAT { get; set; }
            public string DESC_CO_SIT_MAT
            {
                get
                {
                    return this.CO_SIT_MAT == "A" ? "MAT" : this.CO_SIT_MAT == "X" ? "TRF" : this.CO_SIT_MAT == "F" ? "FIN" : this.CO_SIT_MAT == "T" ? "TRC" : this.CO_SIT_MAT == "C" ? "CAN" : "PEN";
                }
            }
            public int CO_EMP { get; set; }

            //Dados de Notas
            public decimal? VL_MEDIA_BIM1 { get; set; }
            public decimal? VL_MEDIA_BIM2 { get; set; }
            public decimal? VL_MEDIA_BIM3 { get; set; }
            public decimal? VL_MEDIA_BIM4 { get; set; }


            public decimal? VL_MEDIA_TRI1 { get; set; }
            public decimal? VL_MEDIA_TRI2 { get; set; }
            public decimal? VL_MEDIA_TRI3 { get; set; }

            //Trata as notas
            public string rltMR1
            {
                get
                {
                    if (tipoReferencia == "B")
                    {
                        return (this.VL_MEDIA_BIM1.HasValue ? this.VL_MEDIA_BIM1.Value.ToString("N2") : " - ");
                    }
                    else
                    {
                        return (this.VL_MEDIA_TRI1.HasValue ? this.VL_MEDIA_TRI1.Value.ToString("N2") : " - ");
                    }
                }
            }
            public string rltMR2
            {
                get
                {
                    if (tipoReferencia == "B")
                    {
                        return (this.VL_MEDIA_BIM2.HasValue ? this.VL_MEDIA_BIM2.Value.ToString("N2") : " - ");
                    }
                    else
                    {
                        return (this.VL_MEDIA_TRI2.HasValue ? this.VL_MEDIA_TRI2.Value.ToString("N2") : " - ");
                    }
                }
            }
            public string rltMR3
            {
                get
                {
                    if (tipoReferencia == "B")
                    {
                        return (this.VL_MEDIA_BIM3.HasValue ? this.VL_MEDIA_BIM3.Value.ToString("N2") : " - ");
                    }
                    else
                    {
                        return (this.VL_MEDIA_TRI3.HasValue ? this.VL_MEDIA_TRI3.Value.ToString("N2") : " - ");
                    }
                }
            }
            public string rltMR4
            {
                get
                {
                    if (tipoReferencia == "B")
                    {
                        return (this.VL_MEDIA_BIM4.HasValue ? this.VL_MEDIA_BIM4.Value.ToString("N2") : " - ");
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            public string FL_HOMOL_NOTA_BIM1 { get; set; }
            public string FL_HOMOL_NOTA_BIM2 { get; set; }
            public string FL_HOMOL_NOTA_BIM3 { get; set; }
            public string FL_HOMOL_NOTA_BIM4 { get; set; }

            public string FL_HOMOL_NOTA_TRI1 { get; set; }
            public string FL_HOMOL_NOTA_TRI2 { get; set; }
            public string FL_HOMOL_NOTA_TRI3 { get; set; }

            //Mostra asterisco ao lado da nota caso não esteja homologada
            public bool FL_HOMOL_VISIBLE_1
            {
                get
                {
                    return (this.VL_MEDIA_BIM1.HasValue ? (FL_HOMOL_NOTA_BIM1 == "S" ? false : true) : false);
                }
            }
            public bool FL_HOMOL_VISIBLE_2
            {
                get
                {
                    return (this.VL_MEDIA_BIM2.HasValue ? (FL_HOMOL_NOTA_BIM2 == "S" ? false : true) : false);
                }
            }
            public bool FL_HOMOL_VISIBLE_3
            {
                get
                {
                    return (this.VL_MEDIA_BIM3.HasValue ? (FL_HOMOL_NOTA_BIM3 == "S" ? false : true) : false);
                }
            }
            public bool FL_HOMOL_VISIBLE_4
            {
                get
                {
                    return (this.VL_MEDIA_BIM4.HasValue ? (FL_HOMOL_NOTA_BIM4 == "S" ? false : true) : false);
                }
            }

            public bool FL_HOMOL_VISIBLE_5
            {
                get
                {
                    return (this.VL_MEDIA_TRI1.HasValue ? (FL_HOMOL_NOTA_TRI1 == "S" ? false : true) : false);
                }
            }
            public bool FL_HOMOL_VISIBLE_6
            {
                get
                {
                    return (this.VL_MEDIA_TRI2.HasValue ? (FL_HOMOL_NOTA_TRI2 == "S" ? false : true) : false);
                }
            }
            public bool FL_HOMOL_VISIBLE_7
            {
                get
                {
                    return (this.VL_MEDIA_TRI3.HasValue ? (FL_HOMOL_NOTA_TRI3 == "S" ? false : true) : false);
                }
            }

            public decimal? VL_PROVA_FINAL { get; set; }
            public string VL_PROVA_FINAL_V
            {
                get
                {
                    return this.VL_PROVA_FINAL == null && this.ENABLED == false ? "*****" : this.VL_PROVA_FINAL == null && this.ENABLED == true ? null : this.VL_PROVA_FINAL.Value.ToString("0.00");
                }
            }
            public string VL_MEDIA_FINAL
            {
                get
                {
                    //Calcula a Síntese anual dos bimestres
                    int coun = 0;
                    decimal notaTotal = 0;

                    //Verificação da referência utilizada pela empresa 
                    var tipo = "B";

                    var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(CO_EMP);

                    tb25.TB83_PARAMETROReference.Load();
                    if (tb25.TB83_PARAMETRO != null)
                        tipo = tb25.TB83_PARAMETRO.TP_PERIOD_AVAL;

                    if (tipo == "B")
                    {
                        if (FL_HOMOL_NOTA_BIM1 == "S")
                        {
                            coun++;
                            notaTotal += (this.VL_MEDIA_BIM1.HasValue ? this.VL_MEDIA_BIM1.Value : 0);
                        }
                        if (FL_HOMOL_NOTA_BIM2 == "S")
                        {
                            coun++;
                            notaTotal += (this.VL_MEDIA_BIM2.HasValue ? this.VL_MEDIA_BIM2.Value : 0);
                        }
                        if (FL_HOMOL_NOTA_BIM3 == "S")
                        {
                            coun++;
                            notaTotal += (this.VL_MEDIA_BIM3.HasValue ? this.VL_MEDIA_BIM3.Value : 0);
                        }
                        if (FL_HOMOL_NOTA_BIM4 == "S")
                        {
                            coun++;
                            notaTotal += (this.VL_MEDIA_BIM4.HasValue ? this.VL_MEDIA_BIM4.Value : 0);
                        }
                    }
                    else
                    {
                        if (FL_HOMOL_NOTA_TRI1 == "S")
                        {
                            coun++;
                            notaTotal += (this.VL_MEDIA_TRI1.HasValue ? this.VL_MEDIA_TRI1.Value : 0);
                        }
                        if (FL_HOMOL_NOTA_TRI2 == "S")
                        {
                            coun++;
                            notaTotal += (this.VL_MEDIA_TRI2.HasValue ? this.VL_MEDIA_TRI2.Value : 0);
                        }
                        if (FL_HOMOL_NOTA_TRI3 == "S")
                        {
                            coun++;
                            notaTotal += (this.VL_MEDIA_TRI3.HasValue ? this.VL_MEDIA_TRI3.Value : 0);
                        }
                    }
                    if (coun > 0)
                    {
                        decimal final = 0;
                        final = notaTotal / coun;
                        return final.ToString("N2");
                    }
                    else
                        return "*****";
                }
            }
        }

        public static DateTime FormataData(DateTime dt)
        {
            string dtFormat = dt.ToString().Substring(0, 10);
            return DateTime.Parse(dtFormat);
        }


        #endregion
    }
}
