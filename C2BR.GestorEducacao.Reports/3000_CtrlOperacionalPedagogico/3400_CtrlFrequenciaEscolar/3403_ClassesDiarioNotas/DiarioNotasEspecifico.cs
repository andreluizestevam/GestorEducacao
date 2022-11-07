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
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar._3404_ClassesDiario;

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar._3403_ClassesDiarioNotas
{
    public class DiarioNotasEspecifico
    {
        public string noCoord { get; set; }
        public string noCol { get; set; }
        public string turmaUnica { get; set; }
        public string nomeProf
        {
            get
            {
                return (!string.IsNullOrEmpty(this.noCol) ? this.noCol.ToUpper() : "");
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

        //Códigos para selects
        public int coTur { get; set; }
        public int CO_CUR { get; set; }
        public string CO_ANO_R { get; set; }
        public int CO_ANO
        {
            get
            {
                return int.Parse(this.CO_ANO_R);
            }
        }
        public int ID_MATERIA { get; set; }

        public DateTime DT_TRANS { get; set; }
        public int Num { get; set; }
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

        public decimal? NT_MAX_PROV { get; set; }
        public decimal? NT_MAX_SIMU { get; set; }
        public decimal? NT_MAX_ATIV { get; set; }

        public string FL_AGRUPADORA_V { get; set; }
        public int CO_MODU_CUR { get; set; }
        public int CO_MAT { get; set; }

        //Notas
        //Notas simulado e prova 1 respectivamente
        public string NOTA1
        {
            get
            {
                //Tratamento feito quando a disciplina em questao e agrupadora
                if (this.FL_AGRUPADORA_V == "S")
                {
                    string ano = this.CO_ANO.ToString();

                    //faz uma verificacao para identificar quais as disciplinas filhas da disciplina em questao
                    var liagrup = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                                   join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                                   join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                   where tb43.TB44_MODULO.CO_MODU_CUR == CO_MODU_CUR
                                   && tb43.CO_CUR == this.CO_CUR
                                   && tb43.ID_MATER_AGRUP == this.CO_MAT
                                   && tb43.CO_ANO_GRADE == ano
                                   select new { tb107.ID_MATERIA }).ToList();

                    decimal notaAtividade = 0;
                    decimal Media = 0;
                    int count = 0;
                    //Percorre a lista de disciplinas agrupadas contabilizando as notas
                    foreach (var li in liagrup)
                    {
                        var lisAgruAtiv = (from tb49 in TB49_NOTA_ATIV_ALUNO.RetornaTodosRegistros()
                                           join tb273 in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros() on tb49.TB273_TIPO_ATIVIDADE.ID_TIPO_ATIV equals tb273.ID_TIPO_ATIV
                                           where tb49.TB07_ALUNO.CO_ALU == this.coAlu
                                           && tb49.TB01_CURSO.CO_CUR == this.CO_CUR
                                           && tb49.CO_ANO == this.CO_ANO
                                           && tb49.CO_BIMESTRE == this.coBimestre
                                           && tb49.TB107_CADMATERIAS.ID_MATERIA == li.ID_MATERIA
                                           && tb273.CO_SIGLA_ATIV == "PR"
                                           && tb49.CO_REFER_NOTA == "N1"
                                           select new
                                           {
                                               tb273.CO_SIGLA_ATIV,
                                               tb49.VL_NOTA,
                                           }).ToList();

                        foreach (var l in lisAgruAtiv)
                        {
                            notaAtividade += l.VL_NOTA;
                            count++;
                        }
                    }

                    //faz o calculo final para atribuir a nota das atividades das disciplinas agrupadas
                    if (count > 0)
                    {
                        //Media = notaAtividade / count;
                        Media = notaAtividade;
                        if (this.NT_MAX_PROV.HasValue)
                        {
                            Media = (Media >= this.NT_MAX_PROV.Value ? this.NT_MAX_PROV.Value : Media);
                            return (!(Media >= 10) ? Media.ToString("N1") : "10");
                        }
                        else
                            return (!(Media >= 10) ? Media.ToString("N1") : "10");
                    }
                    else
                        return " - ";
                }
                //Caso nao seja agrupadora, realiza o calculo das notas da maneira convencional
                else
                {
                    //Faz o cálculo da média em simulados lançados para o ano dentro do contexto
                    var result = (from tb49 in TB49_NOTA_ATIV_ALUNO.RetornaTodosRegistros()
                                  join tb273 in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros() on tb49.TB273_TIPO_ATIVIDADE.ID_TIPO_ATIV equals tb273.ID_TIPO_ATIV
                                  where tb49.TB07_ALUNO.CO_ALU == this.coAlu
                                  && tb49.TB01_CURSO.CO_CUR == this.CO_CUR
                                  && tb49.CO_ANO == this.CO_ANO
                                  && tb49.CO_BIMESTRE == this.coBimestre
                                  && tb49.TB107_CADMATERIAS.ID_MATERIA == this.ID_MATERIA
                                  && tb273.CO_SIGLA_ATIV == "PR"
                                  && tb49.CO_REFER_NOTA == "N1"
                                  select new
                                  {
                                      tb273.CO_SIGLA_ATIV,
                                      tb49.VL_NOTA,
                                  }).FirstOrDefault();

                    decimal media = 0;
                    if (result != null)
                    {
                        if (this.NT_MAX_PROV.HasValue)
                        {
                            media = (result.VL_NOTA >= this.NT_MAX_PROV.Value ? this.NT_MAX_PROV.Value : result.VL_NOTA);
                            return (!(media >= 10) ? media.ToString("N1") : "10");
                        }
                        else
                            return (!(result.VL_NOTA >= 10) ? result.VL_NOTA.ToString("N2") : "10");
                    }
                    else
                        return " - ";
                }
            }
        }
        public string NOTA2
        {
            get
            {
                //Tratamento feito quando a disciplina em questao e agrupadora
                if (this.FL_AGRUPADORA_V == "S")
                {
                    string ano = this.CO_ANO.ToString();

                    //faz uma verificacao para identificar quais as disciplinas filhas da disciplina em questao
                    var liagrup = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                                   join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                                   join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                   where tb43.TB44_MODULO.CO_MODU_CUR == CO_MODU_CUR
                                   && tb43.CO_CUR == this.CO_CUR
                                   && tb43.ID_MATER_AGRUP == this.CO_MAT
                                   && tb43.CO_ANO_GRADE == ano
                                   select new { tb107.ID_MATERIA }).ToList();

                    decimal notaAtividade = 0;
                    decimal Media = 0;
                    int count = 0;
                    //Percorre a lista de disciplinas agrupadas contabilizando as notas
                    foreach (var li in liagrup)
                    {
                        var lisAgruAtiv = (from tb49 in TB49_NOTA_ATIV_ALUNO.RetornaTodosRegistros()
                                           join tb273 in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros() on tb49.TB273_TIPO_ATIVIDADE.ID_TIPO_ATIV equals tb273.ID_TIPO_ATIV
                                           where tb49.TB07_ALUNO.CO_ALU == this.coAlu
                                           && tb49.TB01_CURSO.CO_CUR == this.CO_CUR
                                           && tb49.CO_ANO == this.CO_ANO
                                           && tb49.CO_BIMESTRE == this.coBimestre
                                           && tb49.TB107_CADMATERIAS.ID_MATERIA == li.ID_MATERIA
                                           && tb273.CO_SIGLA_ATIV == "PR"
                                           && tb49.CO_REFER_NOTA == "N2"
                                           select new
                                           {
                                               tb273.CO_SIGLA_ATIV,
                                               tb49.VL_NOTA,
                                           }).ToList();

                        foreach (var l in lisAgruAtiv)
                        {
                            notaAtividade += l.VL_NOTA;
                            count++;
                        }
                    }

                    //faz o calculo final para atribuir a nota das atividades das disciplinas agrupadas
                    if (count > 0)
                    {
                        //Media = notaAtividade / count;
                        Media = notaAtividade;
                        if (this.NT_MAX_PROV.HasValue)
                        {
                            Media = (Media >= this.NT_MAX_PROV.Value ? this.NT_MAX_PROV.Value : Media);
                            return (!(Media >= 10) ? Media.ToString("N1") : "10");
                        }
                        else
                            return (!(Media >= 10) ? Media.ToString("N1") : "10");
                    }
                    else
                        return " - ";
                }
                //Caso nao seja agrupadora, realiza o calculo das notas da maneira convencional
                else
                {
                    //Faz o cálculo da média em simulados lançados para o ano dentro do contexto
                    var result = (from tb49 in TB49_NOTA_ATIV_ALUNO.RetornaTodosRegistros()
                                  join tb273 in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros() on tb49.TB273_TIPO_ATIVIDADE.ID_TIPO_ATIV equals tb273.ID_TIPO_ATIV
                                  where tb49.TB07_ALUNO.CO_ALU == this.coAlu
                                  && tb49.TB01_CURSO.CO_CUR == this.CO_CUR
                                  && tb49.CO_ANO == this.CO_ANO
                                  && tb49.CO_BIMESTRE == this.coBimestre
                                  && tb49.TB107_CADMATERIAS.ID_MATERIA == this.ID_MATERIA
                                  && tb273.CO_SIGLA_ATIV == "PR"
                                  && tb49.CO_REFER_NOTA == "N2"
                                  select new
                                  {
                                      tb273.CO_SIGLA_ATIV,
                                      tb49.VL_NOTA,
                                  }).FirstOrDefault();

                    decimal media = 0;
                    if (result != null)
                    {
                        if (this.NT_MAX_PROV.HasValue)
                        {
                            media = (result.VL_NOTA >= this.NT_MAX_PROV.Value ? this.NT_MAX_PROV.Value : result.VL_NOTA);
                            return (!(media >= 10) ? media.ToString("N1") : "10");
                        }
                        else
                            return (!(result.VL_NOTA >= 10) ? result.VL_NOTA.ToString("N2") : "10");
                    }
                    else
                        return " - ";
                }
            }
        }
        public string MDSML_V
        {
            get
            {
                //Faz o cálculo da média em simulados lançados para o ano dentro do contexto
                var result = (from tb49 in TB49_NOTA_ATIV_ALUNO.RetornaTodosRegistros()
                              join tb273 in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros() on tb49.TB273_TIPO_ATIVIDADE.ID_TIPO_ATIV equals tb273.ID_TIPO_ATIV
                              where tb49.TB07_ALUNO.CO_ALU == this.coAlu
                              && tb49.TB01_CURSO.CO_CUR == this.CO_CUR
                              && tb49.CO_ANO == this.CO_ANO
                              && tb49.CO_BIMESTRE == this.coBimestre
                              && tb49.TB107_CADMATERIAS.ID_MATERIA == this.ID_MATERIA
                              && tb273.CO_SIGLA_ATIV == "SI"
                              select new
                              {
                                  tb273.CO_SIGLA_ATIV,
                                  tb49.VL_NOTA,
                              }).ToList();

                decimal nota = 0;
                decimal Media = 0;
                int count = 0;
                foreach (var l in result)
                {
                    nota += l.VL_NOTA;
                    count++;
                }

                if (result.Count >= 1)
                {
                    Media = nota / count;
                    if (this.NT_MAX_SIMU.HasValue)
                    {
                        Media = (Media >= this.NT_MAX_SIMU.Value ? this.NT_MAX_SIMU.Value : Media);
                        return (!(Media >= 10) ? Media.ToString("N1") : "10");
                    }
                    else
                        return (!(Media >= 10) ? Media.ToString("N1") : "10");
                }
                else
                    return " - ";

            }
        }

        /// <summary>
        /// Nota Bimestral
        /// </summary>
        public decimal? VL_NOTA_BIM { get; set; }
        public string VL_NOTA_BIM_V
        {
            get
            {
                return (this.VL_NOTA_BIM.HasValue ? (!(this.VL_NOTA_BIM >= 10) ? this.VL_NOTA_BIM.Value.ToString("N1") : "10") : " - ");
            }
        }

        /// <summary>
        /// Nota Recuperação Bimestral
        /// </summary>
        public decimal? VL_RECU_BIM { get; set; }
        public string VL_RECU_BIM_V
        {
            get
            {
                return (this.VL_RECU_BIM.HasValue ? (!(this.VL_RECU_BIM >= 10) ? this.VL_RECU_BIM.Value.ToString("N1") : "10") : " - ");
            }
        }

        /// <summary>
        /// Nota do Conselho Bimestral
        /// </summary>
        public decimal? VL_CONSE_BIM { get; set; }
        public string VL_CONSE_BIM_V
        {
            get
            {
                return (this.VL_CONSE_BIM.HasValue ? (!(this.VL_CONSE_BIM >= 10) ? this.VL_CONSE_BIM.Value.ToString("N1") : "10") : " - ");
            }
        }

        /// <summary>
        /// Nota Média final
        /// </summary>
        public decimal? VL_MEDIA_FINAL { get; set; }
        public string VL_MEDIA_FINAL_V
        {
            get
            {
                return (this.VL_MEDIA_FINAL.HasValue ? (!(this.VL_MEDIA_FINAL >= 10) ? this.VL_MEDIA_FINAL.Value.ToString("N1") : "10") : " - ");
            }
        }

        /// <summary>
        /// Nota Recuperação Final
        /// </summary>
        public decimal? VL_RECUP_FINAL { get; set; }
        public string VL_RECUP_FINAL_V
        {
            get
            {
                return (this.VL_RECUP_FINAL.HasValue ? (!(this.VL_RECUP_FINAL >= 10) ? this.VL_RECUP_FINAL.Value.ToString("N1") : "10") : " - ");
            }
        }

        /// <summary>
        /// Nota do Conselho Final
        /// </summary>
        public decimal? VL_CONSE_FINAL { get; set; }
        public string VL_CONSE_FINAL_V
        {
            get
            {
                return (this.VL_CONSE_FINAL.HasValue ? (!(this.VL_CONSE_FINAL >= 10) ? this.VL_CONSE_FINAL.Value.ToString("N1") : "10") : " - ");
            }
        }
    }
}