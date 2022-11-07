using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar;

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar._3404_ClassesDiario
{
    public class DiarioFrente
    {
        public class RelDataFre
        {
            public DateTime DT_FRE { get; set; }
            public int? NR_TEMPO { get; set; }
            public int? CO_ATIV { get; set; }
        }

        public class DiarioClasseFrente
        {
            //public DiarioNotasSupremo NotasSupremo { get; set; } 
            public string noCoord { get; set; }
            public string noCol { get; set; }
            public string turmaUnica { get; set; }
            public string nomeProf
            {
                get
                {
                    return this.noCol.ToUpper();
                }
            }
            public int coAtiv { get; set; }
            public string noModuCur { get; set; }
            public string noCur { get; set; }
            public string ModSerie
            {
                get
                {
                    return this.noModuCur + " / " + this.noCur;
                }
            }
            public int? CO_ATIV { get; set; }
            public string coPeriTur { get; set; }
            public string Turno
            {
                get
                {
                    string t = "";
                    switch (this.coPeriTur)
                    {
                        case "M":
                            t = "Matutino";
                            break;
                        case "V":
                            t = "Vespertino";
                            break;
                        case "N":
                            t = "Noturno";
                            break;
                    }
                    return t;
                }
            }
            public string noTurma { get; set; }
            public string Disciplina { get; set; }
            public string coBimestre { get; set; }
            public string Bimestre
            {
                get
                {
                    string b = "";
                    switch (this.coBimestre)
                    {
                        case "B1":
                            b = "1° Bimestre";
                            break;
                        case "B2":
                            b = "2° Bimestre";
                            break;
                        case "B3":
                            b = "3° Bimestre";
                            break;
                        case "B4":
                            b = "4° Bimestre";
                            break;
                    }
                    return b;
                }
            }

            public decimal? VL_NOTA_BIM { get; set; }
            public string VL_NOTA_BIM_V
            {
                get
                {
                    return (this.VL_NOTA_BIM.HasValue ? this.VL_NOTA_BIM.Value.ToString("N1") : "");
                }
            }

            public decimal? VL_RECU_BIM { get; set; }
            public string VL_RECU_BIM_V
            {
                get
                {
                    return (this.VL_RECU_BIM.HasValue ? this.VL_RECU_BIM.Value.ToString("N1") : "");
                }
            }

            public string ParametrosFrente1
            {
                get
                {
                    string bimestreD = this.Bimestre + " (" + this.dtInicialBimestre.ToShortDateString() + " a " + this.dtFinalBimestre.ToShortDateString() +
                        ") - Aulas (Previstas/Realizadas): " + ((this.AulasPrev != null) ? this.AulasPrev.Value.ToString() : "**") + "/" + this.AulasDadas + ") ";
                    return bimestreD;
                }
            }
            public string ParametrosFrente2
            {
                get
                {
                    string bimestreD = "";
                    if (this.turmaUnica != "S")
                    {
                        bimestreD = "Matéria: " + this.materia.ToUpper() + " - Professor(a): " + this.nomeProf.ToUpper();
                    }
                    else
                    {
                        bimestreD = "Professor(a): " + this.nomeProf.ToUpper();
                    }
                    return bimestreD;
                }
            }

            public int? qtAulasB1 { get; set; }
            public int? qtAulasB2 { get; set; }
            public int? qtAulasB3 { get; set; }
            public int? qtAulasB4 { get; set; }
            public int? AulasPrev
            {
                get
                {
                    int? b = 0;
                    switch (this.coBimestre)
                    {
                        case "B1":
                            b = this.qtAulasB1;
                            break;
                        case "B2":
                            b = this.qtAulasB2;
                            break;
                        case "B3":
                            b = this.qtAulasB3;
                            break;
                        case "B4":
                            b = this.qtAulasB4;
                            break;
                    }
                    return b;
                }
            }
            public int AulasDadas { get; set; }

            //Parâmetros para fazer query e saber se o aulo foi transferido de turma
            public int coTur { get; set; }
            public DateTime DT_TRANS { get; set; }
            public int Num { get; set; }
            public string NumTratado
            {
                get
                {
                    return this.Num.ToString().PadLeft(2, '0');
                }
            }
            public string noAlu { get; set; }
            public int coAlu { get; set; }
            public int nuNire { get; set; }
            public string Nire
            {
                get
                {
                    int l = this.nuNire.ToString().Length;
                    string n = this.nuNire.ToString();
                    while (l < 7)
                    {
                        n = "0" + n;
                        l = n.Length;
                    }
                    return n;
                }
            }
            public string NomeAluno
            {
                get
                {
                    //Se o aluno tiver registro de transferência, concatena o seu nire e nome, com a data na qual foi realizada
                    string noAlu = "";
                    if (this.ORD_SITU == 0) //Se não estiver transferido, usa o nome até 30 caracteres
                        noAlu = (this.noAlu.Length > 30 ? this.noAlu.Substring(0, 30).ToUpper() + "..." : this.noAlu);
                    else //Se estiver transferido, usa até 21 caracteres, dando assim, espaço para a data de transferência
                        noAlu = (this.noAlu.Length > 21 ? this.noAlu.Substring(0, 21).ToUpper() + "..." : this.noAlu);

                    return this.Nire + " - " + noAlu + (this.ORD_SITU == 1 ? " - " + this.DT_TRANS.ToString("dd/MM/yy") : "");
                }
            }
            public string coSituAlu { get; set; }
            public int ORD_SITU { get; set; }
            public string ST
            {
                get
                {
                    //Verifica se existe algum registro de transferência interna entre turmas
                    bool res = (from tbtrans in TB_TRANSF_INTERNA.RetornaTodosRegistros()
                                where tbtrans.TB07_ALUNO.CO_ALU == this.coAlu
                                && tbtrans.CO_TURMA_ATUAL == this.coTur
                                select tbtrans).Any();
                    if (res == true)
                    {
                        this.ORD_SITU = 1;
                        return "TRI";
                    }
                    else
                    {
                        string r = "";
                        switch (this.coSituAlu)
                        {
                            case "A":
                                r = "MAT";
                                this.ORD_SITU = 0;
                                break;
                            case "T":
                                r = "TRA";
                                this.ORD_SITU = 1;
                                break;
                            case "X":
                                r = "TRE";
                                this.ORD_SITU = 1;
                                break;
                            case "F":
                                r = "FIN";
                                this.ORD_SITU = 0;
                                break;
                            case "C":
                                r = "CAN";
                                this.ORD_SITU = 0;
                                break;
                        }
                        return r;
                    }
                }
            }
            //public int mesBimestre { get; set; }

            //public string mesBimestreE
            //{

            //    get
            //    {
            //        string nome = "";

            //        switch (this.mesBimestre.ToString())
            //        {
            //            case "1":
            //                nome = "Janeiro";
            //                break;
            //            case "2":
            //                nome = "Fevereiro";
            //                break;
            //            case "3":
            //                nome = "março";
            //                break;
            //            case "4":
            //                nome = "Abril";
            //                break;
            //            case "5":
            //                nome = "Maio";
            //                break;
            //            case "6":
            //                nome = "Junho";
            //                break;
            //            case "7":
            //                nome = "Julho";
            //                break;
            //            case "8":
            //                nome = "Agosto";
            //                break;
            //            case "9":
            //                nome = "Setembro";
            //                break;
            //            case "10":
            //                nome = "Outubro";
            //                break;
            //            case "11":
            //                nome = "Novembro";
            //                break;
            //            case "12":
            //                nome = "Dezembro";

            //                break;
            //        }
            //        return nome;
            //    }
            //}

            public DateTime dtInicialBimestre { get; set; }
            public DateTime dtFinalBimestre { get; set; }
            public string professorCod { get; set; }
            public string professor { get; set; }
            public string materia { get; set; }

            #region Variáveis das 47 colunas
            public string tc01 { get; set; }
            public string tc02 { get; set; }
            public string tc03 { get; set; }
            public string tc04 { get; set; }
            public string tc05 { get; set; }
            public string tc06 { get; set; }
            public string tc07 { get; set; }
            public string tc08 { get; set; }
            public string tc09 { get; set; }
            public string tc10 { get; set; }
            public string tc11 { get; set; }
            public string tc12 { get; set; }
            public string tc13 { get; set; }
            public string tc14 { get; set; }
            public string tc15 { get; set; }
            public string tc16 { get; set; }
            public string tc17 { get; set; }
            public string tc18 { get; set; }
            public string tc19 { get; set; }
            public string tc20 { get; set; }
            public string tc21 { get; set; }
            public string tc22 { get; set; }
            public string tc23 { get; set; }
            public string tc24 { get; set; }
            public string tc25 { get; set; }
            public string tc26 { get; set; }
            public string tc27 { get; set; }
            public string tc28 { get; set; }
            public string tc29 { get; set; }
            public string tc30 { get; set; }
            public string tc31 { get; set; }
            public string tc32 { get; set; }
            public string tc33 { get; set; }
            public string tc34 { get; set; }
            public string tc35 { get; set; }
            public string tc36 { get; set; }
            public string tc37 { get; set; }
            public string tc38 { get; set; }
            public string tc39 { get; set; }
            public string tc40 { get; set; }
            public string tc41 { get; set; }
            public string tc42 { get; set; }
            public string tc43 { get; set; }
            public string tc44 { get; set; }
            public string tc45 { get; set; }
            public string tc46 { get; set; }
            public string tc47 { get; set; }

            #endregion

            #region Variáveis das 47 colunas de datas
            public string tcDt01 { get; set; }
            public string tcDt02 { get; set; }
            public string tcDt03 { get; set; }
            public string tcDt04 { get; set; }
            public string tcDt05 { get; set; }
            public string tcDt06 { get; set; }
            public string tcDt07 { get; set; }
            public string tcDt08 { get; set; }
            public string tcDt09 { get; set; }
            public string tcDt10 { get; set; }
            public string tcDt11 { get; set; }
            public string tcDt12 { get; set; }
            public string tcDt13 { get; set; }
            public string tcDt14 { get; set; }
            public string tcDt15 { get; set; }
            public string tcDt16 { get; set; }
            public string tcDt17 { get; set; }
            public string tcDt18 { get; set; }
            public string tcDt19 { get; set; }
            public string tcDt20 { get; set; }
            public string tcDt21 { get; set; }
            public string tcDt22 { get; set; }
            public string tcDt23 { get; set; }
            public string tcDt24 { get; set; }
            public string tcDt25 { get; set; }
            public string tcDt26 { get; set; }
            public string tcDt27 { get; set; }
            public string tcDt28 { get; set; }
            public string tcDt29 { get; set; }
            public string tcDt30 { get; set; }
            public string tcDt31 { get; set; }
            public string tcDt32 { get; set; }
            public string tcDt33 { get; set; }
            public string tcDt34 { get; set; }
            public string tcDt35 { get; set; }
            public string tcDt36 { get; set; }
            public string tcDt37 { get; set; }
            public string tcDt38 { get; set; }
            public string tcDt39 { get; set; }
            public string tcDt40 { get; set; }
            public string tcDt41 { get; set; }
            public string tcDt42 { get; set; }
            public string tcDt43 { get; set; }
            public string tcDt44 { get; set; }
            public string tcDt45 { get; set; }
            public string tcDt46 { get; set; }
            public string tcDt47 { get; set; }

            public string tcMe01 { get; set; }
            public string tcMe02 { get; set; }
            public string tcMe03 { get; set; }
            public string tcMe04 { get; set; }
            public string tcMe05 { get; set; }
            public string tcMe06 { get; set; }
            public string tcMe07 { get; set; }
            public string tcMe08 { get; set; }
            public string tcMe09 { get; set; }
            public string tcMe10 { get; set; }
            public string tcMe11 { get; set; }
            public string tcMe12 { get; set; }
            public string tcMe13 { get; set; }
            public string tcMe14 { get; set; }
            public string tcMe15 { get; set; }
            public string tcMe16 { get; set; }
            public string tcMe17 { get; set; }
            public string tcMe18 { get; set; }
            public string tcMe19 { get; set; }
            public string tcMe20 { get; set; }
            public string tcMe21 { get; set; }
            public string tcMe22 { get; set; }
            public string tcMe23 { get; set; }
            public string tcMe24 { get; set; }
            public string tcMe25 { get; set; }
            public string tcMe26 { get; set; }
            public string tcMe27 { get; set; }
            public string tcMe28 { get; set; }
            public string tcMe29 { get; set; }
            public string tcMe30 { get; set; }
            public string tcMe31 { get; set; }
            public string tcMe32 { get; set; }
            public string tcMe33 { get; set; }
            public string tcMe34 { get; set; }
            public string tcMe35 { get; set; }
            public string tcMe36 { get; set; }
            public string tcMe37 { get; set; }
            public string tcMe38 { get; set; }
            public string tcMe39 { get; set; }
            public string tcMe40 { get; set; }
            public string tcMe41 { get; set; }
            public string tcMe42 { get; set; }
            public string tcMe43 { get; set; }
            public string tcMe44 { get; set; }
            public string tcMe45 { get; set; }
            public string tcMe46 { get; set; }
            public string tcMe47 { get; set; }

            #endregion

            #region Variáveis das 47 colunas de tempo
            public string tcTp01 { get; set; }
            public string tcTp02 { get; set; }
            public string tcTp03 { get; set; }
            public string tcTp04 { get; set; }
            public string tcTp05 { get; set; }
            public string tcTp06 { get; set; }
            public string tcTp07 { get; set; }
            public string tcTp08 { get; set; }
            public string tcTp09 { get; set; }
            public string tcTp10 { get; set; }
            public string tcTp11 { get; set; }
            public string tcTp12 { get; set; }
            public string tcTp13 { get; set; }
            public string tcTp14 { get; set; }
            public string tcTp15 { get; set; }
            public string tcTp16 { get; set; }
            public string tcTp17 { get; set; }
            public string tcTp18 { get; set; }
            public string tcTp19 { get; set; }
            public string tcTp20 { get; set; }
            public string tcTp21 { get; set; }
            public string tcTp22 { get; set; }
            public string tcTp23 { get; set; }
            public string tcTp24 { get; set; }
            public string tcTp25 { get; set; }
            public string tcTp26 { get; set; }
            public string tcTp27 { get; set; }
            public string tcTp28 { get; set; }
            public string tcTp29 { get; set; }
            public string tcTp30 { get; set; }
            public string tcTp31 { get; set; }
            public string tcTp32 { get; set; }
            public string tcTp33 { get; set; }
            public string tcTp34 { get; set; }
            public string tcTp35 { get; set; }
            public string tcTp36 { get; set; }
            public string tcTp37 { get; set; }
            public string tcTp38 { get; set; }
            public string tcTp39 { get; set; }
            public string tcTp40 { get; set; }
            public string tcTp41 { get; set; }
            public string tcTp42 { get; set; }
            public string tcTp43 { get; set; }
            public string tcTp44 { get; set; }
            public string tcTp45 { get; set; }
            public string tcTp46 { get; set; }
            public string tcTp47 { get; set; }

            #endregion

            public string TF { get; set; }
        }

        public class ListaFrequencia
        {
            public string CO_FLAG_FREQ_ALUNO { get; set; }
            public DateTime DT_LANCTO_FREQ_ALUNO { get; set; }
            public int coAtividade { get; set; }
            public int? NumeroTempo { get; set; }
            public string NR_TEMPO
            {
                get
                {
                    return (this.NumeroTempo == null ? "" : this.NumeroTempo.ToString() + "º");
                }
            }
        }


        /// <summary>
        /// Método responsável por montar a linha de parâmetros
        /// </summary>
        public static string MontaParametros(string ParametrosPagina, int strP_CO_MODU_CUR, int strP_CO_CUR,
            int strP_CO_TUR, decimal dtRef, string strP_CO_ANO_REFER, int strP_CO_MAT, DateTime dtIniBim,
            DateTime dtFimBim, string coBimestre, int CO_COL
            )
        {
            var lstQtAulas = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                              join tbRespMat in TB_RESPON_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tbRespMat.CO_MAT
                              where tb43.TB44_MODULO.CO_MODU_CUR == strP_CO_MODU_CUR
                              && tb43.CO_CUR == strP_CO_CUR
                              && tb43.CO_ANO_GRADE == strP_CO_ANO_REFER
                              && (strP_CO_MAT != 0 ? tb43.CO_MAT == strP_CO_MAT : true)
                              && (strP_CO_MAT == 0 ? tbRespMat.CO_COL_RESP == CO_COL : true)
                              select new
                              {
                                  tb43.QT_AULAS_BIM1,
                                  tb43.QT_AULAS_BIM2,
                                  tb43.QT_AULAS_BIM3,
                                  tb43.QT_AULAS_BIM4
                              }).ToList().FirstOrDefault();

            var lstDtFreq = (from tb132 in TB132_FREQ_ALU.RetornaTodosRegistros()
                             where tb132.FL_HOMOL_FREQU == "S"
                             && tb132.TB01_CURSO.CO_MODU_CUR == strP_CO_MODU_CUR
                             && tb132.TB01_CURSO.CO_CUR == strP_CO_CUR
                             && tb132.CO_TUR == strP_CO_TUR
                             && tb132.CO_ANO_REFER_FREQ_ALUNO == dtRef
                             && (strP_CO_MAT != 0 ? tb132.CO_MAT == strP_CO_MAT : true)
                             && (strP_CO_MAT == 0 ? tb132.CO_COL == CO_COL : true)
                             && ((tb132.DT_FRE >= dtIniBim) && (tb132.DT_FRE <= dtFimBim))
                             select new DiarioFrente.RelDataFre
                             {
                                 DT_FRE = tb132.DT_FRE,
                                 NR_TEMPO = tb132.NR_TEMPO,
                                 CO_ATIV = tb132.CO_ATIV_PROF_TUR
                             }).Distinct().OrderBy(w => w.DT_FRE).ToList();


            DiarioClasseFrente dcf = new DiarioClasseFrente();
            dcf.AulasDadas = lstDtFreq.Count();
            dcf.qtAulasB1 = lstQtAulas.QT_AULAS_BIM1;
            dcf.qtAulasB2 = lstQtAulas.QT_AULAS_BIM2;
            dcf.qtAulasB3 = lstQtAulas.QT_AULAS_BIM3;
            dcf.qtAulasB4 = lstQtAulas.QT_AULAS_BIM4;
            dcf.coBimestre = coBimestre;
            dcf.dtInicialBimestre = dtIniBim;
            dcf.dtFinalBimestre = dtFimBim;

            return ParametrosPagina + " - " + dcf.ParametrosFrente1;
        }
    }
}
