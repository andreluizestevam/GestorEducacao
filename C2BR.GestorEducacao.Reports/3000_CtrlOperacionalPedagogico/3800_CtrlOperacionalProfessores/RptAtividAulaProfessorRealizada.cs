using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.Reports.Helper;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Globalization;


namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3800_CtrlOperacionalProfessores
{
    public partial class RptAtividAulaProfessorRealizada : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptAtividAulaProfessorRealizada()
        {
            InitializeComponent();
        }

        #region Init Report

        /// <summary>
        /// Esta função carrega o relatório de Atividades Realizadas do Professor
        /// </summary>
        /// <param name="parametros"></param>
        /// <param name="codEmp"></param>
        /// <param name="infos"></param>
        /// <param name="Unidade"></param>
        /// <param name="Colaborador"></param>
        /// <param name="anoGrade"></param>
        /// <param name="Modalidade"></param>
        /// <param name="serieCur"></param>
        /// <param name="Turma"></param>
        /// <returns></returns>
        public int InitReport(string parametros,
                                int codEmp,
                                string infos,
                                int Unidade,
                                int Colaborador,
                                int anoGrade,
                                int Modalidade,
                                int serieCur,
                                int Turma)
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

                // Conversão das variáveis necessárias
                //int intP_CO_EMP = int.Parse(strP_CO_EMP);
                //int intP_CO_COL = int.Parse(strP_CO_COL);
                //int intP_CO_MODU_CUR = int.Parse(strP_CO_MODU_CUR);
                //int intP_CO_CUR = int.Parse(strP_CO_CUR);
                //int intP_CO_TUR = int.Parse(strP_CO_TUR);
                string ano = anoGrade.ToString();

                #region Query

                var lst = (from tb119 in ctx.TB119_ATIV_PROF_TURMA
                           join x  in ctx.TB17_PLANO_AULA on tb119.CO_PLA_AULA equals x.CO_PLA_AULA into t1
                           from tb17 in t1.DefaultIfEmpty()
                            join tb06 in ctx.TB06_TURMAS
                                on tb119.CO_TUR equals tb06.CO_TUR
                            join tb129 in ctx.TB129_CADTURMAS
                                on tb06.CO_TUR equals tb129.CO_TUR
                            join tb03 in ctx.TB03_COLABOR
                                on tb119.CO_COL_ATIV equals tb03.CO_COL
                            join tb02 in ctx.TB02_MATERIA
                                on tb119.CO_MAT equals tb02.CO_MAT
                            join tb107 in ctx.TB107_CADMATERIAS
                                on tb02.ID_MATERIA equals tb107.ID_MATERIA

                           where (Modalidade != 0 ? tb119.CO_MODU_CUR == Modalidade : 0 == 0)
                           &&
                                 (serieCur != 0 ? tb119.CO_CUR == serieCur : 0 == 0)
                           &&
                                 (Turma != 0 ? tb119.CO_TUR == Turma : 0 == 0)
                           &&
                                 (Unidade != 0 ? tb119.CO_EMP == Unidade : 0 == 0)
                           && 
                                 (Colaborador != 0 ? tb119.CO_COL_ATIV == Colaborador : 0 == 0)
                           && 
                                 (ano != "0" ? tb119.CO_ANO_MES_MAT == ano : 0 == 0)

                           //tb119.CO_EMP == intP_CO_EMP
                           //&& tb03.CO_EMP == tb119.CO_EMP
                           //&& tb119.CO_CUR == tb06.CO_CUR
                           //&& tb119.CO_MODU_CUR == intP_CO_MODU_CUR
                           //&& tb119.CO_ANO_MES_MAT == anoGrade
                           //&& tb119.CO_CUR == intP_CO_CUR

                           select new AtividadeRel
                           {
                               DataReal = tb119.DT_ATIV_REAL,
                               HoraIniReal = tb119.HR_INI_ATIV,
                               HoraFimReal = tb119.HR_TER_ATIV,
                               DataPlan = tb17.DT_PREV_PLA,
                               CargaHoraria = tb17.QT_CARG_HORA_PLA,
                               Turma = tb129.CO_SIGLA_TURMA,
                               Disciplina = tb107.NO_MATERIA,
                               TemaResu = tb119.DE_TEMA_AULA,
                               //TemaResu = tb17.DE_TEMA_AULA,
                               descricaoAtiv = tb119.DE_RES_ATIV
                           }

                           ).Distinct();

                var res = lst.ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (AtividadeRel at in res)
                {
                    bsReport.Add(at);
                }

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        public class AtividadeRel
        {
            public DateTime DataReal { get; set; }
            public string DataRealValid
            {
                get
                {
                    return DataReal.Date.ToString("dd/MM/yy");
                }
            }

            public DateTime? DataPlan { get; set; }
            public string HoraIniReal { get; set; }
            public string HoraFimReal { get; set; }
            public decimal? CargaHoraria { get; set; }
			public string cargaHorariaValid 
			{
				get
				{
					string horaIni = this.HoraIniReal.Trim();
                    string horaFim = this.HoraFimReal.Trim();
                    if ((horaIni != "0") && (horaFim != "0"))
                    {
                        TimeSpan duracao = DateTime.Parse(horaIni).Subtract(DateTime.Parse(horaFim));
                        string var1 = duracao.ToString();
                        //string var = DateTime.Parse(var1);
                        //string tempo = duracao.Hours.ToString("hh:mm");

                        return duracao.ToString();
                    }
                    else
                        return " - ";
				}
			}
            public string DataCargaPlan
            {
                get
                {
                    return this.DataPlan != null ? this.DataPlan.Value.Date.ToString("dd/MM/yy") + " - " + this.cargaHorariaValid : "***";
                }
            }
            
            public string Turma { get; set; }
            public string Disciplina { get; set; }
            public string descricaoAtiv { get; set; }
            public string TemaResu { get; set; }
            public string de_tema_aulaValid
            {
                get
                {
                    if ((TemaResu == null) || (TemaResu == ""))
                    {
                        return "***";
                    }
                    return TemaResu;
                }
            }
            public string deTemaAula
            {
                get
                {
                    return de_tema_aulaValid + " - " + descricaoAtiv;
                }
             }
        }
    }
}
