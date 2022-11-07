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

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar
{
    public class DiarioNotasSupremo : DiarioFrente.DiarioClasseFrente
    {
        //Códigos para selects
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

        public decimal? NT_MAX_PROV { get; set; }
        public decimal? NT_MAX_SIMU { get; set; }
        public decimal? NT_MAX_ATIV { get; set; }

        /// <summary>
        /// Nota AV1 lançada no contexto
        /// </summary>
        public string AV1
        {
            get
            {
                //Coleta a nota do primeiro lançamento de prova para o aluno no contexto
                var result = (from tb49 in TB49_NOTA_ATIV_ALUNO.RetornaTodosRegistros()
                              join tb273 in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros() on tb49.TB273_TIPO_ATIVIDADE.ID_TIPO_ATIV equals tb273.ID_TIPO_ATIV
                              where tb49.TB07_ALUNO.CO_ALU == this.coAlu
                              && tb49.TB01_CURSO.CO_CUR == this.CO_CUR
                              && tb49.CO_ANO == this.CO_ANO
                              && tb49.CO_BIMESTRE == this.coBimestre
                              && tb49.TB107_CADMATERIAS.ID_MATERIA == this.ID_MATERIA
                                  //&& tb273.CO_SIGLA_ATIV == "AV1"
                              && tb49.CO_REFER_NOTA == "AV1"
                              select new
                              {
                                  tb273.CO_SIGLA_ATIV,
                                  tb49.VL_NOTA,
                                  tb49.DT_NOTA_ATIV
                              }).OrderByDescending(w => w.DT_NOTA_ATIV).FirstOrDefault();

                decimal media = 0;
                if (result != null)
                {
                    if (this.NT_MAX_SIMU.HasValue)
                    {
                        media = (result.VL_NOTA >= this.NT_MAX_SIMU.Value ? this.NT_MAX_SIMU.Value : result.VL_NOTA);
                        return (!(media >= 10) ? media.ToString("N1") : "10");
                    }
                    else
                        return (!(result.VL_NOTA >= 10) ? result.VL_NOTA.ToString("N1") : "10");
                }
                else
                    return " - ";
            }
        }

        /// <summary>
        /// Nota AV2 lançada no contexto
        /// </summary>
        public string AV2
        {
            get
            {
                //Coleta a nota do primeiro lançamento de prova para o aluno no contexto
                var result = (from tb49 in TB49_NOTA_ATIV_ALUNO.RetornaTodosRegistros()
                              join tb273 in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros() on tb49.TB273_TIPO_ATIVIDADE.ID_TIPO_ATIV equals tb273.ID_TIPO_ATIV
                              where tb49.TB07_ALUNO.CO_ALU == this.coAlu
                              && tb49.TB01_CURSO.CO_CUR == this.CO_CUR
                              && tb49.CO_ANO == this.CO_ANO
                              && tb49.CO_BIMESTRE == this.coBimestre
                              && tb49.TB107_CADMATERIAS.ID_MATERIA == this.ID_MATERIA
                                  //&& tb273.CO_SIGLA_ATIV == "AV2"
                              && tb49.CO_REFER_NOTA == "AV2"
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
                        return (!(result.VL_NOTA >= 10) ? result.VL_NOTA.ToString("N1") : "10");
                }
                else
                    return " - ";
            }
        }

        /// <summary>
        /// Nota AV3 lançada no contexto
        /// </summary>
        public string AV3
        {
            get
            {
                //Coleta a nota do primeiro lançamento de prova para o aluno no contexto
                var lst = (from tb49 in TB49_NOTA_ATIV_ALUNO.RetornaTodosRegistros()
                           join tb273 in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros() on tb49.TB273_TIPO_ATIVIDADE.ID_TIPO_ATIV equals tb273.ID_TIPO_ATIV
                           where tb49.TB07_ALUNO.CO_ALU == this.coAlu
                           && tb49.TB01_CURSO.CO_CUR == this.CO_CUR
                           && tb49.CO_ANO == this.CO_ANO
                           && tb49.CO_BIMESTRE == this.coBimestre
                           && tb49.TB107_CADMATERIAS.ID_MATERIA == this.ID_MATERIA
                               //&& tb273.CO_SIGLA_ATIV == "AV3"
                           && tb49.CO_REFER_NOTA == "AV3"
                           select new
                           {
                               tb273.CO_SIGLA_ATIV,
                               tb49.VL_NOTA,
                               tb49.DT_NOTA_ATIV
                           }).OrderByDescending(w => w.DT_NOTA_ATIV).FirstOrDefault();

                decimal media = 0;

                //if (lst.Count > 1)
                //{
                //    var result = lst[1]; // pega o segundo registro de simulado em ordem decrescente de data da atividade
                if (lst != null)
                {
                    if (this.NT_MAX_SIMU.HasValue)
                    {
                        media = (lst.VL_NOTA >= this.NT_MAX_SIMU.Value ? this.NT_MAX_SIMU.Value : lst.VL_NOTA);
                        return (!(media >= 10) ? media.ToString("N1") : "10");
                    }
                    else
                        return (!(lst.VL_NOTA >= 10) ? lst.VL_NOTA.ToString("N1") : "10");
                }
                else
                    return " - ";
                //}
                //else
                //    return " - ";
            }
        }

        /// <summary>
        /// Nota AV4 lançada no contexto
        /// </summary>
        public string AV4
        {
            get
            {
                //Coleta a nota do segundo lançamento de prova para o aluno no contexto
                var result = (from tb49 in TB49_NOTA_ATIV_ALUNO.RetornaTodosRegistros()
                              join tb273 in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros() on tb49.TB273_TIPO_ATIVIDADE.ID_TIPO_ATIV equals tb273.ID_TIPO_ATIV
                              where tb49.TB07_ALUNO.CO_ALU == this.coAlu
                              && tb49.TB01_CURSO.CO_CUR == this.CO_CUR
                              && tb49.CO_ANO == this.CO_ANO
                              && tb49.CO_BIMESTRE == this.coBimestre
                              && tb49.TB107_CADMATERIAS.ID_MATERIA == this.ID_MATERIA
                                  //&& tb273.CO_SIGLA_ATIV == "AV4"
                              && tb49.CO_REFER_NOTA == "AV4"
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
                        return (!(result.VL_NOTA >= 10) ? result.VL_NOTA.ToString("N1") : "10");
                }
                else
                    return " - ";
            }
        }

        /// <summary>
        /// Nota AV5 lançada no contexto
        /// </summary>
        public string AV5
        {
            get
            {
                //Coleta a nota do segundo lançamento de prova para o aluno no contexto
                var result = (from tb49 in TB49_NOTA_ATIV_ALUNO.RetornaTodosRegistros()
                              join tb273 in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros() on tb49.TB273_TIPO_ATIVIDADE.ID_TIPO_ATIV equals tb273.ID_TIPO_ATIV
                              where tb49.TB07_ALUNO.CO_ALU == this.coAlu
                              && tb49.TB01_CURSO.CO_CUR == this.CO_CUR
                              && tb49.CO_ANO == this.CO_ANO
                              && tb49.CO_BIMESTRE == this.coBimestre
                              && tb49.TB107_CADMATERIAS.ID_MATERIA == this.ID_MATERIA
                                  //&& tb273.CO_SIGLA_ATIV == "AV5"
                              && tb49.CO_REFER_NOTA == "AV5"
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
                        return (!(result.VL_NOTA >= 10) ? result.VL_NOTA.ToString("N1") : "10");
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
