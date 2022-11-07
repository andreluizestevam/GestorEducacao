using System;
using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports.Helper;
using System.Data.Objects;

namespace C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2104_Calendario
{
    public partial class RptCalendario2013 : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptCalendario2013()
        {
            InitializeComponent();

        }

        public int InitReport(string infos,int codEmp, int tipo, string parametros, string Ano )
        {
           try
            {


                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(codEmp);
                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);


                // Instancia o contexto
                //var ctx = GestorEntities.CurrentContext;

                // Conversão das variáveis necessárias
               /* int int_codBairro = codBairro != "T" ? int.Parse(codBairro) : 0;
                int int_codCidade = codCidade != "T" ? int.Parse(codCidade) : 0;
                int int_codSerieCur = codSerieCur != "T" ? int.Parse(codSerieCur) : 0;
                int int_codTurma = codTurma != "T" ? int.Parse(codTurma) : 0;
                int int_codInst = grInst != "T" ? int.Parse(grInst) : 0;
                */

                //if (Ano != "2013")
                //    return -1;

                    #region Query
                    int ano = int.Parse(Ano);
                    
                    var ctx = GestorEntities.CurrentContext;

                    var lstJan = (from calend in ctx.TB157_CALENDARIO_ATIVIDADES
                                  where calend.CAL_DATA_CALEND.Month == 1
                                  && calend.CAL_DATA_CALEND.Year == ano
                                  && (tipo != 0 ? calend.TB152_CALENDARIO_TIPO.CAT_ID_TIPO_CALEN == tipo : 0 == 0)

                                  select new CalendarioJaneiro
                                 {
                                     nomeAtiv = calend.CAL_NOME_ATIVID_CALEND,
                                     data = calend.CAL_DATA_CALEND,
                                     tipoCal = calend.TB152_CALENDARIO_TIPO.CAT_NOME_TIPO_CALEN,
                                     tipodia = (calend.CAL_TIPO_DIA_CALEND == "F" ? "Feriado" : "Outro"),
                                     dataFim = calend.CAL_DATA_CALEND,
                                     obs = calend.CAL_OBSE_ATIVID_CALEND
                                     //dia = calend.CAL_DATA_CALEND.Day.ToString()
                                 }

                               );
                    var lstFev = (from calend in ctx.TB157_CALENDARIO_ATIVIDADES
                                  where calend.CAL_DATA_CALEND.Month == 2
                                   && calend.CAL_DATA_CALEND.Year == ano
                                   && (tipo != 0 ? calend.TB152_CALENDARIO_TIPO.CAT_ID_TIPO_CALEN == tipo : 0 == 0)
                                  select new CalendarioFevereiro
                                  {
                                      nomeAtiv = calend.CAL_NOME_ATIVID_CALEND,
                                      data = calend.CAL_DATA_CALEND,
                                      tipoCal = calend.TB152_CALENDARIO_TIPO.CAT_NOME_TIPO_CALEN,
                                      tipodia = (calend.CAL_TIPO_DIA_CALEND == "F" ? "Feriado" : "Outro"),
                                      dataFim = calend.CAL_DATA_CALEND,
                                      //dia = calend.CAL_DATA_CALEND.Day.ToString()
                                      obs = calend.CAL_OBSE_ATIVID_CALEND
                                  }

                              );

                    var lstMar = (from calend in ctx.TB157_CALENDARIO_ATIVIDADES
                                  where calend.CAL_DATA_CALEND.Month == 3
                                   && calend.CAL_DATA_CALEND.Year == ano
                                   && (tipo != 0 ? calend.TB152_CALENDARIO_TIPO.CAT_ID_TIPO_CALEN == tipo : 0 == 0)
                                  select new CalendarioMarço
                                  {
                                      nomeAtiv = calend.CAL_NOME_ATIVID_CALEND,
                                      data = calend.CAL_DATA_CALEND,
                                      tipoCal = calend.TB152_CALENDARIO_TIPO.CAT_NOME_TIPO_CALEN,
                                      tipodia = (calend.CAL_TIPO_DIA_CALEND == "F" ? "Feriado" : "Outro"),
                                      dataFim = calend.CAL_DATA_CALEND,
                                      // dia = calend.CAL_DATA_CALEND.Day.ToString()
                                      obs = calend.CAL_OBSE_ATIVID_CALEND
                                  }

                              );

                    var lstAbr = (from calend in ctx.TB157_CALENDARIO_ATIVIDADES
                                  where calend.CAL_DATA_CALEND.Month == 4
                                   && calend.CAL_DATA_CALEND.Year == ano
                                   && (tipo != 0 ? calend.TB152_CALENDARIO_TIPO.CAT_ID_TIPO_CALEN == tipo : 0 == 0)
                                  select new CalendarioAbril
                                  {
                                      nomeAtiv = calend.CAL_NOME_ATIVID_CALEND,
                                      data = calend.CAL_DATA_CALEND,
                                      tipoCal = calend.TB152_CALENDARIO_TIPO.CAT_NOME_TIPO_CALEN,
                                      tipodia = (calend.CAL_TIPO_DIA_CALEND == "F" ? "Feriado" : "Outro"),
                                      dataFim = calend.CAL_DATA_CALEND,
                                      //dia = calend.CAL_DATA_CALEND.Day.ToString()
                                      obs = calend.CAL_OBSE_ATIVID_CALEND
                                  }

                              );

                    var lstMaio = (from calend in ctx.TB157_CALENDARIO_ATIVIDADES
                                   where calend.CAL_DATA_CALEND.Month == 5
                                    && calend.CAL_DATA_CALEND.Year == ano
                                    && (tipo != 0 ? calend.TB152_CALENDARIO_TIPO.CAT_ID_TIPO_CALEN == tipo : 0 == 0)
                                   select new CalendarioMaio
                                   {
                                       nomeAtiv = calend.CAL_NOME_ATIVID_CALEND,
                                       data = calend.CAL_DATA_CALEND,
                                       tipoCal = calend.TB152_CALENDARIO_TIPO.CAT_NOME_TIPO_CALEN,
                                       tipodia = (calend.CAL_TIPO_DIA_CALEND == "F" ? "Feriado" : "Outro"),
                                       dataFim = calend.CAL_DATA_CALEND,
                                       //dia = calend.CAL_DATA_CALEND.Day.ToString()
                                       obs = calend.CAL_OBSE_ATIVID_CALEND
                                   }

                              );

                    var lstJunh = (from calend in ctx.TB157_CALENDARIO_ATIVIDADES
                                   where calend.CAL_DATA_CALEND.Month == 6
                                    && calend.CAL_DATA_CALEND.Year == ano
                                    && (tipo != 0 ? calend.TB152_CALENDARIO_TIPO.CAT_ID_TIPO_CALEN == tipo : 0 == 0)
                                   select new CalendarioJunho
                                   {
                                       nomeAtiv = calend.CAL_NOME_ATIVID_CALEND,
                                       data = calend.CAL_DATA_CALEND,
                                       tipoCal = calend.TB152_CALENDARIO_TIPO.CAT_NOME_TIPO_CALEN,
                                       tipodia = (calend.CAL_TIPO_DIA_CALEND == "F" ? "Feriado" : "Outro"),
                                       dataFim = calend.CAL_DATA_CALEND,
                                       //dia = calend.CAL_DATA_CALEND.Day.ToString()
                                       obs = calend.CAL_OBSE_ATIVID_CALEND
                                   }

                              );

                    var lstJulh = (from calend in ctx.TB157_CALENDARIO_ATIVIDADES
                                   where calend.CAL_DATA_CALEND.Month == 7
                                    && calend.CAL_DATA_CALEND.Year == ano
                                    && (tipo != 0 ? calend.TB152_CALENDARIO_TIPO.CAT_ID_TIPO_CALEN == tipo : 0 == 0)
                                   select new CalendarioJulho
                                   {
                                       nomeAtiv = calend.CAL_NOME_ATIVID_CALEND,
                                       data = calend.CAL_DATA_CALEND,
                                       tipoCal = calend.TB152_CALENDARIO_TIPO.CAT_NOME_TIPO_CALEN,
                                       tipodia = (calend.CAL_TIPO_DIA_CALEND == "F" ? "Feriado" : "Outro"),
                                       dataFim = calend.CAL_DATA_CALEND,
                                       // dia = calend.CAL_DATA_CALEND.Day.ToString()
                                       obs = calend.CAL_OBSE_ATIVID_CALEND
                                   }

                              );

                    var lstAgost = (from calend in ctx.TB157_CALENDARIO_ATIVIDADES
                                    where calend.CAL_DATA_CALEND.Month == 8
                                     && calend.CAL_DATA_CALEND.Year == ano
                                     && (tipo != 0 ? calend.TB152_CALENDARIO_TIPO.CAT_ID_TIPO_CALEN == tipo : 0 == 0)
                                    select new CalendarioAgosto
                                    {
                                        nomeAtiv = calend.CAL_NOME_ATIVID_CALEND,
                                        data = calend.CAL_DATA_CALEND,
                                        tipoCal = calend.TB152_CALENDARIO_TIPO.CAT_NOME_TIPO_CALEN,
                                        tipodia = (calend.CAL_TIPO_DIA_CALEND == "F" ? "Feriado" : "Outro"),
                                        dataFim = calend.CAL_DATA_CALEND,
                                        // dia = calend.CAL_DATA_CALEND.Day.ToString()
                                        obs = calend.CAL_OBSE_ATIVID_CALEND
                                    }

                              );

                    var lstSet = (from calend in ctx.TB157_CALENDARIO_ATIVIDADES
                                  where calend.CAL_DATA_CALEND.Month == 9
                                   && calend.CAL_DATA_CALEND.Year == ano
                                   && (tipo != 0 ? calend.TB152_CALENDARIO_TIPO.CAT_ID_TIPO_CALEN == tipo : 0 == 0)
                                  select new CalendarioSetembro
                                  {
                                      nomeAtiv = calend.CAL_NOME_ATIVID_CALEND,
                                      data = calend.CAL_DATA_CALEND,
                                      tipoCal = calend.TB152_CALENDARIO_TIPO.CAT_NOME_TIPO_CALEN,
                                      tipodia = (calend.CAL_TIPO_DIA_CALEND == "F" ? "Feriado" : "Outro"),
                                      dataFim = calend.CAL_DATA_CALEND,
                                      //dia = calend.CAL_DATA_CALEND.Day.ToString()
                                      obs = calend.CAL_OBSE_ATIVID_CALEND
                                  }

                              );

                    var lstOut = (from calend in ctx.TB157_CALENDARIO_ATIVIDADES
                                  where calend.CAL_DATA_CALEND.Month == 10
                                   && calend.CAL_DATA_CALEND.Year == ano
                                   && (tipo != 0 ? calend.TB152_CALENDARIO_TIPO.CAT_ID_TIPO_CALEN == tipo : 0 == 0)
                                  select new CalendarioOutubro
                                  {
                                      nomeAtiv = calend.CAL_NOME_ATIVID_CALEND,
                                      data = calend.CAL_DATA_CALEND,
                                      tipoCal = calend.TB152_CALENDARIO_TIPO.CAT_NOME_TIPO_CALEN,
                                      tipodia = (calend.CAL_TIPO_DIA_CALEND == "F" ? "Feriado" : "Outro"),
                                      dataFim = calend.CAL_DATA_CALEND,
                                      //dia = calend.CAL_DATA_CALEND.Day.ToString()
                                      obs = calend.CAL_OBSE_ATIVID_CALEND
                                  }

                              );

                    var lstNov = (from calend in ctx.TB157_CALENDARIO_ATIVIDADES
                                  where calend.CAL_DATA_CALEND.Month == 11
                                   && calend.CAL_DATA_CALEND.Year == ano
                                   && (tipo != 0 ? calend.TB152_CALENDARIO_TIPO.CAT_ID_TIPO_CALEN == tipo : 0 == 0)
                                  select new CalendarioNovembro
                                  {
                                      nomeAtiv = calend.CAL_NOME_ATIVID_CALEND,
                                      data = calend.CAL_DATA_CALEND,
                                      tipoCal = calend.TB152_CALENDARIO_TIPO.CAT_NOME_TIPO_CALEN,
                                      tipodia = (calend.CAL_TIPO_DIA_CALEND == "F" ? "Feriado" : "Outro"),
                                      dataFim = calend.CAL_DATA_CALEND,
                                      //dia = calend.CAL_DATA_CALEND.Day.ToString()
                                      obs = calend.CAL_OBSE_ATIVID_CALEND
                                  }

                              );

                    var lstDez = (from calend in ctx.TB157_CALENDARIO_ATIVIDADES
                                  where calend.CAL_DATA_CALEND.Month == 12
                                  && calend.CAL_DATA_CALEND.Year == ano
                                  && (tipo != 0 ? calend.TB152_CALENDARIO_TIPO.CAT_ID_TIPO_CALEN == tipo : 0 == 0)
                                  select new CalendarioDezembro
                                  {
                                      nomeAtiv = calend.CAL_NOME_ATIVID_CALEND,
                                      data = calend.CAL_DATA_CALEND,
                                      tipoCal = calend.TB152_CALENDARIO_TIPO.CAT_NOME_TIPO_CALEN,
                                      tipodia = (calend.CAL_TIPO_DIA_CALEND == "F" ? "Feriado" : "Outro"),
                                      dataFim = calend.CAL_DATA_CALEND,
                                      //dia = calend.CAL_DATA_CALEND.Day.ToString()
                                      obs = calend.CAL_OBSE_ATIVID_CALEND
                                  }

                              );

                    Calendario ca = new Calendario();
                    ca.Janeiro = lstJan.ToList();
                    ca.Fevereiro = lstFev.ToList();
                    ca.Março = lstMar.ToList();
                    ca.Abril = lstAbr.ToList();
                    ca.Maio = lstMaio.ToList();
                    ca.Junho = lstJunh.ToList();
                    ca.Julho = lstJulh.ToList();
                    ca.Agosto = lstAgost.ToList();
                    ca.Setembro = lstSet.ToList();
                    ca.Outubro = lstOut.ToList();
                    ca.Novembro = lstNov.ToList();
                    ca.Dezembro = lstDez.ToList();
                    #endregion

                    #region preenche tbl Ocorrencias de Janeiro
                    if (ca.Janeiro.Count > 0)
                    {
                        xrTableCell3.Text = ca.Janeiro[0].dia;
                        xrTableCell21.Text = ca.Janeiro[0].nomeAtiv;
                        xrTableCell37.Text = ca.Janeiro[0].obs;
                        xrTableCell29.Text = ca.Janeiro[0].tipoCal;

                    }
                    if (ca.Janeiro.Count > 1)
                    {
                        xrTableCell233.Text = ca.Janeiro[1].dia;
                        xrTableCell234.Text = ca.Janeiro[1].nomeAtiv;
                        xrTableCell235.Text = ca.Janeiro[1].obs;
                        xrTableCell236.Text = ca.Janeiro[1].tipoCal;

                    }
                    if (ca.Janeiro.Count > 2)
                    {
                        xrTableCell237.Text = ca.Janeiro[2].dia;
                        xrTableCell238.Text = ca.Janeiro[2].nomeAtiv;
                        xrTableCell239.Text = ca.Janeiro[2].obs;
                        xrTableCell240.Text = ca.Janeiro[2].tipoCal;

                    }
                    if (ca.Janeiro.Count > 3)
                    {
                        xrTableCell241.Text = ca.Janeiro[3].dia;
                        xrTableCell242.Text = ca.Janeiro[3].nomeAtiv;
                        xrTableCell243.Text = ca.Janeiro[3].obs;
                        xrTableCell244.Text = ca.Janeiro[3].tipoCal;

                    }

                    if (ca.Janeiro.Count > 4)
                    {
                        xrTableCell245.Text = ca.Janeiro[4].dia;
                        xrTableCell246.Text = ca.Janeiro[4].nomeAtiv;
                        xrTableCell247.Text = ca.Janeiro[4].obs;
                        xrTableCell248.Text = ca.Janeiro[4].tipoCal;

                    }


                    if (ca.Janeiro.Count > 5)
                    {
                        xrTableCell249.Text = ca.Janeiro[6].dia;
                        xrTableCell250.Text = ca.Janeiro[6].nomeAtiv;
                        xrTableCell251.Text = ca.Janeiro[6].obs;
                        xrTableCell252.Text = ca.Janeiro[6].tipoCal;

                    }
                    if (ca.Janeiro.Count > 6)
                    {
                        xrTableCell253.Text = ca.Janeiro[7].dia;
                        xrTableCell254.Text = ca.Janeiro[7].nomeAtiv;
                        xrTableCell255.Text = ca.Janeiro[7].obs;
                        xrTableCell256.Text = ca.Janeiro[7].tipoCal;

                    }
                    if (ca.Janeiro.Count > 7)
                    {
                        xrTableCell257.Text = ca.Janeiro[5].dia;
                        xrTableCell258.Text = ca.Janeiro[5].nomeAtiv;
                        xrTableCell259.Text = ca.Janeiro[5].obs;
                        xrTableCell260.Text = ca.Janeiro[5].tipoCal;

                    }

                    #endregion

                    #region preenche tbl Ocorrencias de Fevereiro
                    if (ca.Fevereiro.Count > 0)
                    {
                        xrTableCell289.Text = ca.Fevereiro[0].dia;
                        xrTableCell290.Text = ca.Fevereiro[0].nomeAtiv;
                        xrTableCell755.Text = ca.Fevereiro[0].obs;
                        xrTableCell756.Text = ca.Fevereiro[0].tipoCal;

                    }
                    if (ca.Fevereiro.Count > 1)
                    {
                        xrTableCell757.Text = ca.Fevereiro[1].dia;
                        xrTableCell758.Text = ca.Fevereiro[1].nomeAtiv;
                        xrTableCell759.Text = ca.Fevereiro[1].obs;
                        xrTableCell760.Text = ca.Fevereiro[1].tipoCal;

                    }
                    if (ca.Fevereiro.Count > 2)
                    {
                        xrTableCell761.Text = ca.Fevereiro[2].dia;
                        xrTableCell762.Text = ca.Fevereiro[2].nomeAtiv;
                        xrTableCell763.Text = ca.Fevereiro[2].obs;
                        xrTableCell764.Text = ca.Fevereiro[2].tipoCal;

                    }
                    if (ca.Fevereiro.Count > 3)
                    {
                        xrTableCell765.Text = ca.Fevereiro[3].dia;
                        xrTableCell766.Text = ca.Fevereiro[3].nomeAtiv;
                        xrTableCell767.Text = ca.Fevereiro[3].obs;
                        xrTableCell768.Text = ca.Fevereiro[3].tipoCal;

                    }

                    if (ca.Fevereiro.Count > 4)
                    {
                        xrTableCell769.Text = ca.Fevereiro[4].dia;
                        xrTableCell770.Text = ca.Fevereiro[4].nomeAtiv;
                        xrTableCell771.Text = ca.Fevereiro[4].obs;
                        xrTableCell772.Text = ca.Fevereiro[4].tipoCal;

                    }

                    if (ca.Fevereiro.Count > 5)
                    {
                        xrTableCell773.Text = ca.Fevereiro[5].dia;
                        xrTableCell774.Text = ca.Fevereiro[5].nomeAtiv;
                        xrTableCell775.Text = ca.Fevereiro[5].obs;
                        xrTableCell776.Text = ca.Fevereiro[5].tipoCal;

                    }
                    if (ca.Fevereiro.Count > 6)
                    {
                        xrTableCell777.Text = ca.Fevereiro[6].dia;
                        xrTableCell778.Text = ca.Fevereiro[6].nomeAtiv;
                        xrTableCell779.Text = ca.Fevereiro[6].obs;
                        xrTableCell780.Text = ca.Fevereiro[6].tipoCal;

                    }
                    if (ca.Fevereiro.Count > 7)
                    {
                        xrTableCell781.Text = ca.Fevereiro[7].dia;
                        xrTableCell782.Text = ca.Fevereiro[7].nomeAtiv;
                        xrTableCell783.Text = ca.Fevereiro[7].obs;
                        xrTableCell784.Text = ca.Fevereiro[7].tipoCal;

                    }



                    #endregion

                    #region preenche tbl Ocorrencias de Março
                    if (ca.Março.Count > 0)
                    {
                        xrTableCell113.Text = ca.Março[0].dia;
                        xrTableCell114.Text = ca.Março[0].nomeAtiv;
                        xrTableCell115.Text = ca.Março[0].obs;
                        xrTableCell116.Text = ca.Março[0].tipoCal;

                    }
                    if (ca.Março.Count > 1)
                    {
                        xrTableCell122.Text = ca.Março[1].dia;
                        xrTableCell123.Text = ca.Março[1].nomeAtiv;
                        xrTableCell124.Text = ca.Março[1].obs;
                        xrTableCell125.Text = ca.Março[1].tipoCal;

                    }
                    if (ca.Março.Count > 2)
                    {
                        xrTableCell261.Text = ca.Março[2].dia;
                        xrTableCell262.Text = ca.Março[2].nomeAtiv;
                        xrTableCell263.Text = ca.Março[2].obs;
                        xrTableCell264.Text = ca.Março[2].tipoCal;

                    }
                    if (ca.Março.Count > 3)
                    {
                        xrTableCell265.Text = ca.Março[3].dia;
                        xrTableCell266.Text = ca.Março[3].nomeAtiv;
                        xrTableCell267.Text = ca.Março[3].obs;
                        xrTableCell268.Text = ca.Março[3].tipoCal;

                    }

                    if (ca.Março.Count > 4)
                    {
                        xrTableCell269.Text = ca.Março[4].dia;
                        xrTableCell270.Text = ca.Março[4].nomeAtiv;
                        xrTableCell271.Text = ca.Março[4].obs;
                        xrTableCell272.Text = ca.Março[4].tipoCal;

                    }

                    if (ca.Março.Count > 5)
                    {
                        xrTableCell273.Text = ca.Março[5].dia;
                        xrTableCell274.Text = ca.Março[5].nomeAtiv;
                        xrTableCell275.Text = ca.Março[5].obs;
                        xrTableCell276.Text = ca.Março[5].tipoCal;

                    }
                    if (ca.Março.Count > 6)
                    {
                        xrTableCell277.Text = ca.Março[6].dia;
                        xrTableCell278.Text = ca.Março[6].nomeAtiv;
                        xrTableCell279.Text = ca.Março[6].obs;
                        xrTableCell280.Text = ca.Março[6].tipoCal;

                    }
                    if (ca.Março.Count > 7)
                    {
                        xrTableCell281.Text = ca.Março[7].dia;
                        xrTableCell282.Text = ca.Março[7].nomeAtiv;
                        xrTableCell283.Text = ca.Março[7].obs;
                        xrTableCell284.Text = ca.Março[7].tipoCal;

                    }



                    #endregion

                    #region preenche tbl Ocorrencias de Abril
                    if (ca.Abril.Count > 0)
                    {
                        xrTableCell175.Text = ca.Abril[0].dia;
                        xrTableCell176.Text = ca.Abril[0].nomeAtiv;
                        xrTableCell177.Text = ca.Abril[0].obs;
                        xrTableCell178.Text = ca.Abril[0].tipoCal;

                    }
                    if (ca.Abril.Count > 1)
                    {
                        xrTableCell285.Text = ca.Abril[1].dia;
                        xrTableCell286.Text = ca.Abril[1].nomeAtiv;
                        xrTableCell287.Text = ca.Abril[1].obs;
                        xrTableCell288.Text = ca.Abril[1].tipoCal;

                    }
                    if (ca.Abril.Count > 2)
                    {
                        xrTableCell785.Text = ca.Abril[2].dia;
                        xrTableCell786.Text = ca.Abril[2].nomeAtiv;
                        xrTableCell787.Text = ca.Abril[2].obs;
                        xrTableCell788.Text = ca.Abril[2].tipoCal;

                    }
                    if (ca.Abril.Count > 3)
                    {
                        xrTableCell789.Text = ca.Abril[3].dia;
                        xrTableCell790.Text = ca.Abril[3].nomeAtiv;
                        xrTableCell791.Text = ca.Abril[3].obs;
                        xrTableCell792.Text = ca.Abril[3].tipoCal;

                    }

                    if (ca.Abril.Count > 4)
                    {
                        xrTableCell793.Text = ca.Abril[4].dia;
                        xrTableCell794.Text = ca.Abril[4].nomeAtiv;
                        xrTableCell795.Text = ca.Abril[4].obs;
                        xrTableCell796.Text = ca.Abril[4].tipoCal;

                    }

                    if (ca.Abril.Count > 5)
                    {
                        xrTableCell797.Text = ca.Abril[5].dia;
                        xrTableCell798.Text = ca.Abril[5].nomeAtiv;
                        xrTableCell799.Text = ca.Abril[5].obs;
                        xrTableCell800.Text = ca.Abril[5].tipoCal;

                    }
                    if (ca.Abril.Count > 6)
                    {
                        xrTableCell801.Text = ca.Abril[6].dia;
                        xrTableCell802.Text = ca.Abril[6].nomeAtiv;
                        xrTableCell803.Text = ca.Abril[6].obs;
                        xrTableCell804.Text = ca.Abril[6].tipoCal;

                    }
                    if (ca.Abril.Count > 7)
                    {
                        xrTableCell805.Text = ca.Abril[7].dia;
                        xrTableCell806.Text = ca.Abril[7].nomeAtiv;
                        xrTableCell807.Text = ca.Abril[7].obs;
                        xrTableCell808.Text = ca.Abril[7].tipoCal;

                    }



                    #endregion

                    #region preenche tbl Ocorrencias de Maio
                    if (ca.Maio.Count > 0)
                    {
                        xrTableCell345.Text = ca.Maio[0].dia;
                        xrTableCell346.Text = ca.Maio[0].nomeAtiv;
                        xrTableCell347.Text = ca.Maio[0].obs;
                        xrTableCell348.Text = ca.Maio[0].tipoCal;

                    }
                    if (ca.Maio.Count > 1)
                    {
                        xrTableCell809.Text = ca.Maio[1].dia;
                        xrTableCell810.Text = ca.Maio[1].nomeAtiv;
                        xrTableCell811.Text = ca.Maio[1].obs;
                        xrTableCell812.Text = ca.Maio[1].tipoCal;

                    }
                    if (ca.Maio.Count > 2)
                    {
                        xrTableCell813.Text = ca.Maio[2].dia;
                        xrTableCell814.Text = ca.Maio[2].nomeAtiv;
                        xrTableCell815.Text = ca.Maio[2].obs;
                        xrTableCell816.Text = ca.Maio[2].tipoCal;

                    }
                    if (ca.Maio.Count > 3)
                    {
                        xrTableCell817.Text = ca.Maio[3].dia;
                        xrTableCell818.Text = ca.Maio[3].nomeAtiv;
                        xrTableCell819.Text = ca.Maio[3].obs;
                        xrTableCell820.Text = ca.Maio[3].tipoCal;

                    }

                    if (ca.Maio.Count > 4)
                    {
                        xrTableCell821.Text = ca.Maio[4].dia;
                        xrTableCell822.Text = ca.Maio[4].nomeAtiv;
                        xrTableCell823.Text = ca.Maio[4].obs;
                        xrTableCell824.Text = ca.Maio[4].tipoCal;

                    }

                    if (ca.Maio.Count > 5)
                    {
                        xrTableCell825.Text = ca.Maio[5].dia;
                        xrTableCell826.Text = ca.Maio[5].nomeAtiv;
                        xrTableCell827.Text = ca.Maio[5].obs;
                        xrTableCell828.Text = ca.Maio[5].tipoCal;

                    }
                    if (ca.Maio.Count > 6)
                    {
                        xrTableCell829.Text = ca.Maio[6].dia;
                        xrTableCell830.Text = ca.Maio[6].nomeAtiv;
                        xrTableCell831.Text = ca.Maio[6].obs;
                        xrTableCell832.Text = ca.Maio[6].tipoCal;

                    }
                    if (ca.Maio.Count > 7)
                    {
                        xrTableCell833.Text = ca.Maio[7].dia;
                        xrTableCell834.Text = ca.Maio[7].nomeAtiv;
                        xrTableCell835.Text = ca.Maio[7].obs;
                        xrTableCell836.Text = ca.Maio[7].tipoCal;

                    }



                    #endregion

                    #region preenche tbl Ocorrencias de Junho
                    if (ca.Junho.Count > 0)
                    {
                        xrTableCell751.Text = ca.Junho[0].dia;
                        xrTableCell752.Text = ca.Junho[0].nomeAtiv;
                        xrTableCell753.Text = ca.Junho[0].obs;
                        xrTableCell754.Text = ca.Junho[0].tipoCal;

                    }
                    if (ca.Junho.Count > 1)
                    {
                        xrTableCell837.Text = ca.Junho[1].dia;
                        xrTableCell838.Text = ca.Junho[1].nomeAtiv;
                        xrTableCell839.Text = ca.Junho[1].obs;
                        xrTableCell840.Text = ca.Junho[1].tipoCal;

                    }
                    if (ca.Junho.Count > 2)
                    {
                        xrTableCell841.Text = ca.Junho[2].dia;
                        xrTableCell842.Text = ca.Junho[2].nomeAtiv;
                        xrTableCell843.Text = ca.Junho[2].obs;
                        xrTableCell844.Text = ca.Junho[2].tipoCal;

                    }
                    if (ca.Junho.Count > 3)
                    {
                        xrTableCell845.Text = ca.Junho[3].dia;
                        xrTableCell846.Text = ca.Junho[3].nomeAtiv;
                        xrTableCell847.Text = ca.Junho[3].obs;
                        xrTableCell848.Text = ca.Junho[3].tipoCal;

                    }

                    if (ca.Junho.Count > 4)
                    {
                        xrTableCell849.Text = ca.Junho[4].dia;
                        xrTableCell850.Text = ca.Junho[4].nomeAtiv;
                        xrTableCell851.Text = ca.Junho[4].obs;
                        xrTableCell852.Text = ca.Junho[4].tipoCal;

                    }

                    if (ca.Junho.Count > 5)
                    {
                        xrTableCell853.Text = ca.Junho[5].dia;
                        xrTableCell854.Text = ca.Junho[5].nomeAtiv;
                        xrTableCell855.Text = ca.Junho[5].obs;
                        xrTableCell856.Text = ca.Junho[5].tipoCal;

                    }
                    if (ca.Junho.Count > 6)
                    {
                        xrTableCell857.Text = ca.Junho[6].dia;
                        xrTableCell858.Text = ca.Junho[6].nomeAtiv;
                        xrTableCell859.Text = ca.Junho[6].obs;
                        xrTableCell860.Text = ca.Junho[6].tipoCal;

                    }
                    if (ca.Junho.Count > 7)
                    {
                        xrTableCell861.Text = ca.Junho[7].dia;
                        xrTableCell862.Text = ca.Junho[7].nomeAtiv;
                        xrTableCell863.Text = ca.Junho[7].obs;
                        xrTableCell864.Text = ca.Junho[7].tipoCal;

                    }



                    #endregion

                    #region preenche tbl Ocorrencias de Julho
                    if (ca.Julho.Count > 0)
                    {
                        xrTableCell403.Text = ca.Julho[0].dia;
                        xrTableCell404.Text = ca.Julho[0].nomeAtiv;
                        xrTableCell405.Text = ca.Julho[0].obs;
                        xrTableCell406.Text = ca.Julho[0].tipoCal;

                    }
                    if (ca.Julho.Count > 1)
                    {
                        xrTableCell865.Text = ca.Julho[1].dia;
                        xrTableCell866.Text = ca.Julho[1].nomeAtiv;
                        xrTableCell867.Text = ca.Julho[1].obs;
                        xrTableCell868.Text = ca.Julho[1].tipoCal;

                    }
                    if (ca.Julho.Count > 2)
                    {
                        xrTableCell869.Text = ca.Julho[2].dia;
                        xrTableCell870.Text = ca.Julho[2].nomeAtiv;
                        xrTableCell871.Text = ca.Julho[2].obs;
                        xrTableCell872.Text = ca.Julho[2].tipoCal;

                    }
                    if (ca.Julho.Count > 3)
                    {
                        xrTableCell873.Text = ca.Julho[3].dia;
                        xrTableCell874.Text = ca.Julho[3].nomeAtiv;
                        xrTableCell875.Text = ca.Julho[3].obs;
                        xrTableCell876.Text = ca.Julho[3].tipoCal;

                    }

                    if (ca.Julho.Count > 4)
                    {
                        xrTableCell877.Text = ca.Julho[4].dia;
                        xrTableCell878.Text = ca.Julho[4].nomeAtiv;
                        xrTableCell879.Text = ca.Julho[4].obs;
                        xrTableCell880.Text = ca.Julho[4].tipoCal;

                    }

                    if (ca.Julho.Count > 5)
                    {
                        xrTableCell881.Text = ca.Julho[5].dia;
                        xrTableCell882.Text = ca.Julho[5].nomeAtiv;
                        xrTableCell883.Text = ca.Julho[5].obs;
                        xrTableCell884.Text = ca.Julho[5].tipoCal;

                    }
                    if (ca.Julho.Count > 6)
                    {
                        xrTableCell885.Text = ca.Julho[6].dia;
                        xrTableCell886.Text = ca.Julho[6].nomeAtiv;
                        xrTableCell887.Text = ca.Julho[6].obs;
                        xrTableCell888.Text = ca.Julho[6].tipoCal;

                    }
                    if (ca.Julho.Count > 7)
                    {
                        xrTableCell889.Text = ca.Julho[7].dia;
                        xrTableCell890.Text = ca.Julho[7].nomeAtiv;
                        xrTableCell891.Text = ca.Julho[7].obs;
                        xrTableCell892.Text = ca.Julho[7].tipoCal;

                    }



                    #endregion

                    #region preenche tbl Ocorrencias de Agosto
                    if (ca.Agosto.Count > 0)
                    {
                        xrTableCell893.Text = ca.Agosto[0].dia;
                        xrTableCell894.Text = ca.Agosto[0].nomeAtiv;
                        xrTableCell895.Text = ca.Agosto[0].obs;
                        xrTableCell896.Text = ca.Agosto[0].tipoCal;

                    }
                    if (ca.Agosto.Count > 1)
                    {
                        xrTableCell897.Text = ca.Agosto[1].dia;
                        xrTableCell898.Text = ca.Agosto[1].nomeAtiv;
                        xrTableCell899.Text = ca.Agosto[1].obs;
                        xrTableCell900.Text = ca.Agosto[1].tipoCal;

                    }
                    if (ca.Agosto.Count > 2)
                    {
                        xrTableCell901.Text = ca.Agosto[2].dia;
                        xrTableCell902.Text = ca.Agosto[2].nomeAtiv;
                        xrTableCell903.Text = ca.Agosto[2].obs;
                        xrTableCell904.Text = ca.Agosto[2].tipoCal;

                    }
                    if (ca.Agosto.Count > 3)
                    {
                        xrTableCell905.Text = ca.Agosto[3].dia;
                        xrTableCell906.Text = ca.Agosto[3].nomeAtiv;
                        xrTableCell907.Text = ca.Agosto[3].obs;
                        xrTableCell908.Text = ca.Agosto[3].tipoCal;

                    }

                    if (ca.Agosto.Count > 4)
                    {
                        xrTableCell909.Text = ca.Agosto[4].dia;
                        xrTableCell910.Text = ca.Agosto[4].nomeAtiv;
                        xrTableCell911.Text = ca.Agosto[4].obs;
                        xrTableCell912.Text = ca.Agosto[4].tipoCal;

                    }

                    if (ca.Agosto.Count > 5)
                    {
                        xrTableCell913.Text = ca.Agosto[5].dia;
                        xrTableCell914.Text = ca.Agosto[5].nomeAtiv;
                        xrTableCell915.Text = ca.Agosto[5].obs;
                        xrTableCell916.Text = ca.Agosto[5].tipoCal;

                    }
                    if (ca.Agosto.Count > 6)
                    {
                        xrTableCell917.Text = ca.Agosto[6].dia;
                        xrTableCell918.Text = ca.Agosto[6].nomeAtiv;
                        xrTableCell919.Text = ca.Agosto[6].obs;
                        xrTableCell920.Text = ca.Agosto[6].tipoCal;

                    }
                    if (ca.Agosto.Count > 7)
                    {
                        xrTableCell921.Text = ca.Agosto[7].dia;
                        xrTableCell922.Text = ca.Agosto[7].nomeAtiv;
                        xrTableCell923.Text = ca.Agosto[7].obs;
                        xrTableCell924.Text = ca.Agosto[7].tipoCal;

                    }



                    #endregion

                    #region preenche tbl Ocorrencias de Setembro
                    if (ca.Setembro.Count > 0)
                    {
                        xrTableCell461.Text = ca.Setembro[0].dia;
                        xrTableCell462.Text = ca.Setembro[0].nomeAtiv;
                        xrTableCell463.Text = ca.Setembro[0].obs;
                        xrTableCell464.Text = ca.Setembro[0].tipoCal;

                    }
                    if (ca.Setembro.Count > 1)
                    {
                        xrTableCell519.Text = ca.Setembro[1].dia;
                        xrTableCell520.Text = ca.Setembro[1].nomeAtiv;
                        xrTableCell521.Text = ca.Setembro[1].obs;
                        xrTableCell522.Text = ca.Setembro[1].tipoCal;

                    }
                    if (ca.Setembro.Count > 2)
                    {
                        xrTableCell925.Text = ca.Setembro[2].dia;
                        xrTableCell926.Text = ca.Setembro[2].nomeAtiv;
                        xrTableCell927.Text = ca.Setembro[2].obs;
                        xrTableCell928.Text = ca.Setembro[2].tipoCal;

                    }
                    if (ca.Setembro.Count > 3)
                    {
                        xrTableCell929.Text = ca.Setembro[3].dia;
                        xrTableCell930.Text = ca.Setembro[3].nomeAtiv;
                        xrTableCell931.Text = ca.Setembro[3].obs;
                        xrTableCell932.Text = ca.Setembro[3].tipoCal;

                    }

                    if (ca.Setembro.Count > 4)
                    {
                        xrTableCell933.Text = ca.Setembro[4].dia;
                        xrTableCell934.Text = ca.Setembro[4].nomeAtiv;
                        xrTableCell935.Text = ca.Setembro[4].obs;
                        xrTableCell936.Text = ca.Setembro[4].tipoCal;

                    }

                    if (ca.Setembro.Count > 5)
                    {
                        xrTableCell937.Text = ca.Setembro[5].dia;
                        xrTableCell938.Text = ca.Setembro[5].nomeAtiv;
                        xrTableCell939.Text = ca.Setembro[5].obs;
                        xrTableCell940.Text = ca.Setembro[5].tipoCal;

                    }
                    if (ca.Setembro.Count > 6)
                    {
                        xrTableCell941.Text = ca.Setembro[6].dia;
                        xrTableCell942.Text = ca.Setembro[6].nomeAtiv;
                        xrTableCell943.Text = ca.Setembro[6].obs;
                        xrTableCell944.Text = ca.Setembro[6].tipoCal;

                    }
                    if (ca.Setembro.Count > 7)
                    {
                        xrTableCell945.Text = ca.Setembro[7].dia;
                        xrTableCell946.Text = ca.Setembro[7].nomeAtiv;
                        xrTableCell947.Text = ca.Setembro[7].obs;
                        xrTableCell948.Text = ca.Setembro[7].tipoCal;

                    }



                    #endregion

                    #region preenche tbl Ocorrencias de Outubro
                    if (ca.Outubro.Count > 0)
                    {
                        xrTableCell577.Text = ca.Outubro[0].dia;
                        xrTableCell578.Text = ca.Outubro[0].nomeAtiv;
                        xrTableCell579.Text = ca.Outubro[0].obs;
                        xrTableCell580.Text = ca.Outubro[0].tipoCal;

                    }
                    if (ca.Outubro.Count > 1)
                    {
                        xrTableCell949.Text = ca.Outubro[1].dia;
                        xrTableCell950.Text = ca.Outubro[1].nomeAtiv;
                        xrTableCell951.Text = ca.Outubro[1].obs;
                        xrTableCell952.Text = ca.Outubro[1].tipoCal;

                    }
                    if (ca.Outubro.Count > 2)
                    {
                        xrTableCell953.Text = ca.Outubro[2].dia;
                        xrTableCell954.Text = ca.Outubro[2].nomeAtiv;
                        xrTableCell955.Text = ca.Outubro[2].obs;
                        xrTableCell956.Text = ca.Outubro[2].tipoCal;

                    }
                    if (ca.Outubro.Count > 3)
                    {
                        xrTableCell957.Text = ca.Outubro[3].dia;
                        xrTableCell958.Text = ca.Outubro[3].nomeAtiv;
                        xrTableCell959.Text = ca.Outubro[3].obs;
                        xrTableCell960.Text = ca.Outubro[3].tipoCal;

                    }

                    if (ca.Outubro.Count > 4)
                    {
                        xrTableCell961.Text = ca.Outubro[4].dia;
                        xrTableCell962.Text = ca.Outubro[4].nomeAtiv;
                        xrTableCell963.Text = ca.Outubro[4].obs;
                        xrTableCell964.Text = ca.Outubro[4].tipoCal;

                    }

                    if (ca.Outubro.Count > 5)
                    {
                        xrTableCell965.Text = ca.Outubro[5].dia;
                        xrTableCell966.Text = ca.Outubro[5].nomeAtiv;
                        xrTableCell967.Text = ca.Outubro[5].obs;
                        xrTableCell968.Text = ca.Outubro[5].tipoCal;

                    }
                    if (ca.Outubro.Count > 6)
                    {
                        xrTableCell969.Text = ca.Outubro[6].dia;
                        xrTableCell970.Text = ca.Outubro[6].nomeAtiv;
                        xrTableCell971.Text = ca.Outubro[6].obs;
                        xrTableCell972.Text = ca.Outubro[6].tipoCal;

                    }
                    if (ca.Outubro.Count > 7)
                    {
                        xrTableCell973.Text = ca.Outubro[7].dia;
                        xrTableCell974.Text = ca.Outubro[7].nomeAtiv;
                        xrTableCell975.Text = ca.Outubro[7].obs;
                        xrTableCell976.Text = ca.Outubro[7].tipoCal;

                    }



                    #endregion

                    #region preenche tbl Ocorrencias de Novembro
                    if (ca.Novembro.Count > 0)
                    {
                        xrTableCell635.Text = ca.Novembro[0].dia;
                        xrTableCell636.Text = ca.Novembro[0].nomeAtiv;
                        xrTableCell637.Text = ca.Novembro[0].obs;
                        xrTableCell638.Text = ca.Novembro[0].tipoCal;

                    }
                    if (ca.Novembro.Count > 1)
                    {
                        xrTableCell977.Text = ca.Novembro[1].dia;
                        xrTableCell978.Text = ca.Novembro[1].nomeAtiv;
                        xrTableCell979.Text = ca.Novembro[1].obs;
                        xrTableCell980.Text = ca.Novembro[1].tipoCal;

                    }
                    if (ca.Novembro.Count > 2)
                    {
                        xrTableCell981.Text = ca.Novembro[2].dia;
                        xrTableCell982.Text = ca.Novembro[2].nomeAtiv;
                        xrTableCell983.Text = ca.Novembro[2].obs;
                        xrTableCell984.Text = ca.Novembro[2].tipoCal;

                    }
                    if (ca.Novembro.Count > 3)
                    {
                        xrTableCell985.Text = ca.Novembro[3].dia;
                        xrTableCell986.Text = ca.Novembro[3].nomeAtiv;
                        xrTableCell987.Text = ca.Novembro[3].obs;
                        xrTableCell988.Text = ca.Novembro[3].tipoCal;

                    }

                    if (ca.Novembro.Count > 4)
                    {
                        xrTableCell989.Text = ca.Novembro[4].dia;
                        xrTableCell990.Text = ca.Novembro[4].nomeAtiv;
                        xrTableCell991.Text = ca.Novembro[4].obs;
                        xrTableCell992.Text = ca.Novembro[4].tipoCal;

                    }

                    if (ca.Novembro.Count > 5)
                    {
                        xrTableCell993.Text = ca.Novembro[5].dia;
                        xrTableCell994.Text = ca.Novembro[5].nomeAtiv;
                        xrTableCell995.Text = ca.Novembro[5].obs;
                        xrTableCell996.Text = ca.Novembro[5].tipoCal;

                    }
                    if (ca.Novembro.Count > 6)
                    {
                        xrTableCell997.Text = ca.Novembro[6].dia;
                        xrTableCell998.Text = ca.Novembro[6].nomeAtiv;
                        xrTableCell999.Text = ca.Novembro[6].obs;
                        xrTableCell1000.Text = ca.Novembro[6].tipoCal;

                    }
                    if (ca.Novembro.Count > 7)
                    {
                        xrTableCell1001.Text = ca.Novembro[7].dia;
                        xrTableCell1002.Text = ca.Novembro[7].nomeAtiv;
                        xrTableCell1003.Text = ca.Novembro[7].obs;
                        xrTableCell1004.Text = ca.Novembro[7].tipoCal;

                    }



                    #endregion

                    #region preenche tbl Ocorrencias de Dezembro
                    if (ca.Dezembro.Count > 0)
                    {
                        xrTableCell693.Text = ca.Dezembro[0].dia;
                        xrTableCell694.Text = ca.Dezembro[0].nomeAtiv;
                        xrTableCell695.Text = ca.Dezembro[0].obs;
                        xrTableCell696.Text = ca.Dezembro[0].tipoCal;

                    }
                    if (ca.Dezembro.Count > 1)
                    {
                        xrTableCell1005.Text = ca.Dezembro[1].dia;
                        xrTableCell1006.Text = ca.Dezembro[1].nomeAtiv;
                        xrTableCell1007.Text = ca.Dezembro[1].obs;
                        xrTableCell1008.Text = ca.Dezembro[1].tipoCal;

                    }
                    if (ca.Dezembro.Count > 2)
                    {
                        xrTableCell1009.Text = ca.Dezembro[2].dia;
                        xrTableCell1010.Text = ca.Dezembro[2].nomeAtiv;
                        xrTableCell1011.Text = ca.Dezembro[2].obs;
                        xrTableCell1012.Text = ca.Dezembro[2].tipoCal;

                    }
                    if (ca.Dezembro.Count > 3)
                    {
                        xrTableCell1013.Text = ca.Dezembro[3].dia;
                        xrTableCell1014.Text = ca.Dezembro[3].nomeAtiv;
                        xrTableCell1015.Text = ca.Dezembro[3].obs;
                        xrTableCell1016.Text = ca.Dezembro[3].tipoCal;

                    }

                    if (ca.Dezembro.Count > 4)
                    {
                        xrTableCell1017.Text = ca.Dezembro[4].dia;
                        xrTableCell1018.Text = ca.Dezembro[4].nomeAtiv;
                        xrTableCell1019.Text = ca.Dezembro[4].obs;
                        xrTableCell1020.Text = ca.Dezembro[4].tipoCal;

                    }

                    if (ca.Dezembro.Count > 5)
                    {
                        xrTableCell1021.Text = ca.Dezembro[5].dia;
                        xrTableCell1022.Text = ca.Dezembro[5].nomeAtiv;
                        xrTableCell1023.Text = ca.Dezembro[5].obs;
                        xrTableCell1024.Text = ca.Dezembro[5].tipoCal;

                    }
                    if (ca.Dezembro.Count > 6)
                    {
                        xrTableCell1025.Text = ca.Dezembro[6].dia;
                        xrTableCell1026.Text = ca.Dezembro[6].nomeAtiv;
                        xrTableCell1027.Text = ca.Dezembro[6].obs;
                        xrTableCell1028.Text = ca.Dezembro[6].tipoCal;

                    }
                    if (ca.Dezembro.Count > 7)
                    {
                        xrTableCell1029.Text = ca.Dezembro[7].dia;
                        xrTableCell1030.Text = ca.Dezembro[7].nomeAtiv;
                        xrTableCell1031.Text = ca.Dezembro[7].obs;
                        xrTableCell1032.Text = ca.Dezembro[7].tipoCal;

                    }



                    #endregion

                
                //if(ca.Janeiro.)
                // Erro: não encontrou registros
                /*if (res.Count == 0)
                    return -1;
                */
                //xrTableCell234.Text = "teste";
                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
               
                    bsReport.Add(ca);
               

                return 1;
            }
            catch { return 0; }
        }
        public class Calendario
        {
            public Calendario()
            {

                this.Janeiro = new List<CalendarioJaneiro>();
                this.Fevereiro = new List<CalendarioFevereiro>();
                this.Março = new List<CalendarioMarço>();
                this.Abril = new List<CalendarioAbril>();
                this.Maio = new List<CalendarioMaio>();
                this.Junho = new List<CalendarioJunho>();
                this.Julho = new List<CalendarioJulho>();
                this.Agosto = new List<CalendarioAgosto>();
                this.Setembro = new List<CalendarioSetembro>();
                this.Outubro = new List<CalendarioOutubro>();
                this.Novembro = new List<CalendarioNovembro>();
                this.Dezembro = new List<CalendarioDezembro>();
            }
            public List<CalendarioJaneiro> Janeiro { get; set; }
            public List<CalendarioFevereiro> Fevereiro { get; set; }
            public List<CalendarioMarço> Março { get; set; }
            public List<CalendarioAbril> Abril { get; set; }
            public List<CalendarioMaio> Maio { get; set; }
            public List<CalendarioJunho> Junho { get; set; }
            public List<CalendarioJulho> Julho { get; set; }
            public List<CalendarioAgosto> Agosto { get; set; }
            public List<CalendarioSetembro> Setembro { get; set; }
            public List<CalendarioOutubro> Outubro { get; set; }
            public List<CalendarioNovembro> Novembro { get; set; }
            public List<CalendarioDezembro> Dezembro { get; set; }

        }

        public class CalendarioJaneiro
        {
            public DateTime data { get; set; }
            public DateTime dataFim { get; set; }
            public string tipoCal { get; set; }
            public string tipodia { get; set; }
            public string nomeAtiv { get; set; }
            public string obs { get; set; }
            public string dia { get; set; }
        }

        public class CalendarioFevereiro
        {
            public DateTime data { get; set; }
            public DateTime dataFim { get; set; }
            public string tipoCal { get; set; }
            public string tipodia { get; set; }
            public string nomeAtiv { get; set; }
            public string obs { get; set; }
            public string dia
            {
                get
                {
                    return this.data.Day.ToString();
                }
            }
        }

        public class CalendarioMarço
        {
            public DateTime data { get; set; }
            public DateTime dataFim { get; set; }
            public string tipoCal { get; set; }
            public string tipodia { get; set; }
            public string nomeAtiv { get; set; }
            public string obs { get; set; }
            public string dia {
                get
                {
                    return this.data.Day.ToString();
                }
            }
        }

        public class CalendarioAbril
        {
            public DateTime data { get; set; }
            public DateTime dataFim { get; set; }
            public string tipoCal { get; set; }
            public string tipodia { get; set; }
            public string nomeAtiv { get; set; }
            public string obs { get; set; }
            public string dia {
                get
                {
                    return this.data.Day.ToString();
                }
            }
        }
        
        public class CalendarioMaio
        {
            public DateTime data { get; set; }
            public DateTime dataFim { get; set; }
            public string tipoCal { get; set; }
            public string tipodia { get; set; }
            public string nomeAtiv { get; set; }
            public string obs { get; set; }
            public string dia {
                get
                {
                    return this.data.Day.ToString();
                }
            }
        }
        
        public class CalendarioJunho
        {
            public DateTime data { get; set; }
            public DateTime dataFim { get; set; }
            public string tipoCal { get; set; }
            public string tipodia { get; set; }
            public string nomeAtiv { get; set; }
            public string obs { get; set; }
            public string dia {
                get
                {
                    return this.data.Day.ToString();
                }
            }
        }
        
        public class CalendarioJulho
        {
            public DateTime data { get; set; }
            public DateTime dataFim { get; set; }
            public string tipoCal { get; set; }
            public string tipodia { get; set; }
            public string nomeAtiv { get; set; }
            public string obs { get; set; }
            public string dia {
                get
                {
                    return this.data.Day.ToString();
                }
            }
        }

        public class CalendarioAgosto
        {
            public DateTime data { get; set; }
            public DateTime dataFim { get; set; }
            public string tipoCal { get; set; }
            public string tipodia { get; set; }
            public string nomeAtiv { get; set; }
            public string obs { get; set; }
            public string dia {
                get
                {
                    return this.data.Day.ToString();
                }
            }
        }
        
        public class CalendarioSetembro
        {
            public DateTime data { get; set; }
            public DateTime dataFim { get; set; }
            public string tipoCal { get; set; }
            public string tipodia { get; set; }
            public string nomeAtiv { get; set; }
            public string obs { get; set; }
            public string dia {
                get
                {
                    return this.data.Day.ToString();
                }
            }
        }

        public class CalendarioOutubro
        {
            public DateTime data { get; set; }
            public DateTime dataFim { get; set; }
            public string tipoCal { get; set; }
            public string tipodia { get; set; }
            public string nomeAtiv { get; set; }
            public string obs { get; set; }
            public string dia {
                get
                {
                    return this.data.Day.ToString();
                }
            }
        }

        public class CalendarioNovembro
        {
            public DateTime data { get; set; }
            public DateTime dataFim { get; set; }
            public string tipoCal { get; set; }
            public string tipodia { get; set; }
            public string nomeAtiv { get; set; }
            public string obs { get; set; }
            public string dia {
                get
                {
                    return this.data.Day.ToString();
                }
            }
        }
        
        public class CalendarioDezembro
        {
            public DateTime data { get; set; }
            public DateTime dataFim { get; set; }
            public string tipoCal { get; set; }
            public string tipodia { get; set; }
            public string nomeAtiv { get; set; }
            public string obs { get; set; }
            public string dia {
                get
                {
                    return this.data.Day.ToString();
                }
            }
        }

        private void xrTable52_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

    }
}
