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
    public partial class RptHistoFrequeAluno : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        int qtdeTotFaltas, qtdeTotPresen, qtdeTotJustif, qtdeTotNaoJustif = 0;

        #region ctor

        public RptHistoFrequeAluno()
        {
            InitializeComponent();
        }

        #endregion

        #region Init Report

        public int InitReport(string parametros,
                               int codEmp,
                               int codEmpRef,
                               int codMod,
                               int codCurso,
                               int ano,
                               int codTurma,
                               int aluno,
                               string strP_DT_INI,
                               string strP_DT_FIM,
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

                #region Query Frequência Aluno

                DateTime dtInicio = DateTime.Parse(strP_DT_INI + " 00:00:00");
                DateTime dtFim = DateTime.Parse(strP_DT_FIM + " 23:59:59");

                /*
                ' select distinct FR.CO_CUR,cu.CO_PARAM_FREQ_TIPO, FR.CO_TUR,a.nu_nis,mm.co_alu_cad,PLA.dt_prev_pla,apt.HR_INI_ATIV,apt.HR_TER_ATIV, 
                 * fr.dt_fre, PLA.nu_temp_pla,cu.no_cur,ct.no_turma as no_tur,'+
               'cm.no_materia,fr.DE_JUSTI_FREQ_ALUNO,fr.CO_FLAG_FREQ_ALUNO, PLA.DE_TEMA_AULA,PLA.DE_OBJE_AULA '+
               */

                var lst = from tb132 in ctx.TB132_FREQ_ALU
                          join tb119 in ctx.TB119_ATIV_PROF_TURMA on tb132.CO_ATIV_PROF_TUR equals tb119.CO_ATIV_PROF_TUR
                          join tb17 in ctx.TB17_PLANO_AULA on tb119.CO_PLA_AULA equals tb17.CO_PLA_AULA into pla
                          from planoAula in pla.DefaultIfEmpty()
                          join tb08 in ctx.TB08_MATRCUR on tb132.TB07_ALUNO.CO_ALU equals tb08.TB07_ALUNO.CO_ALU
                          join tb02 in ctx.TB02_MATERIA on tb119.CO_MAT equals tb02.CO_MAT
                          join tb107 in ctx.TB107_CADMATERIAS on tb02.ID_MATERIA equals tb107.ID_MATERIA
                          where tb08.CO_CUR == tb132.TB01_CURSO.CO_CUR && tb08.CO_TUR == tb132.CO_TUR && tb132.TB01_CURSO.CO_EMP == codEmpRef &&
                           tb132.TB01_CURSO.CO_MODU_CUR == codMod && tb132.TB01_CURSO.CO_CUR == codCurso && tb132.CO_TUR == codTurma &&
                           (aluno != 0 ? tb132.TB07_ALUNO.CO_ALU == aluno : 0==0) && tb132.CO_ANO_REFER_FREQ_ALUNO == ano
                           && (tb132.DT_FRE >= dtInicio && tb132.DT_FRE <= dtFim)
                           && (tb08.CO_SIT_MAT == "A" || tb08.CO_SIT_MAT == "F" || tb08.CO_SIT_MAT == "T")
                          select new FrequenciaAluno
                          {
                              TipoFreq = tb132.TB01_CURSO.CO_PARAM_FREQ_TIPO,
                              NIRE = tb132.TB07_ALUNO.NU_NIRE,
                              Aluno = tb132.TB07_ALUNO.NO_ALU,
                              DataFreq = tb132.DT_FRE,
                              FlagFreq = tb132.CO_FLAG_FREQ_ALUNO == "S" ? "Sim" : "Não",
                              NuTempoPla = planoAula != null ? planoAula.NU_TEMP_PLA : null,
                              DescHora = tb132.TB01_CURSO.CO_PARAM_FREQ_TIPO == "D" ? "**************" : (planoAula != null ? planoAula.HR_INI_AULA_PLA + " / " + planoAula.HR_FIM_AULA_PLA :"**************"),
                              Materia = tb132.TB01_CURSO.CO_PARAM_FREQ_TIPO == "D" ? "**************" : tb107.NO_RED_MATERIA,
                              DescJustifi = tb132.CO_FLAG_FREQ_ALUNO == "S" ? "" : (tb132.DE_JUSTI_FREQ_ALUNO != null && tb132.DE_JUSTI_FREQ_ALUNO != "" ? tb132.DE_JUSTI_FREQ_ALUNO : "***** Falta sem Justificativa *****"),                              
                              descPlanoAula = tb119.DE_TEMA_AULA,
                              descObjetivoAula = planoAula != null ? planoAula.DE_OBJE_AULA : null
                          };

                var res = lst.OrderBy(p => p.Aluno).ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (FrequenciaAluno at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Alunos/Frequência do Relatorio

        public class FrequenciaAluno
        {
            public string TipoFreq { get; set; }
            public int NIRE { get; set; }
            public string Aluno { get; set; }
            public string DescHora { get; set; }
            public DateTime DataFreq { get; set; }
            public Decimal? NuTempoPla { get; set; }
            public string Materia { get; set; }
            public string DescJustifi { get; set; }
            public string FlagFreq { get; set; }
            public string descPlanoAula { get; set; }
            public string descObjetivoAula { get; set; }
            
            public string DescAluno
            {
                get
                {
                    return this.NIRE.ToString().PadLeft(9, '0') + " - " + this.Aluno.ToUpper();
                }
            }

            public string DescNuTempoPla
            {
                get
                {
                    return this.TipoFreq != "D" && this.NuTempoPla != null ? (this.NuTempoPla + 1).ToString() + "º" : "**";
                }
            }
        }

        #endregion

        private void lblPresenca_AfterPrint(object sender, EventArgs e)
        {
            if (lblPresenca.Text == "Sim")
	        {
                qtdeTotPresen += 1;
	        }
            else
            {
                qtdeTotFaltas += 1;
            }
            
        }

        private void lblJustificativa_AfterPrint(object sender, EventArgs e)
        {
            if (lblPresenca.Text == "Não")
            {
                if (lblJustificativa.Text == "***** Falta sem Justificativa *****")
                {
                    qtdeTotNaoJustif += 1;
                }
                else
                {
                    qtdeTotJustif += 1;
                }                
            }
        }

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblTotPresenca.Text = qtdeTotPresen.ToString().PadLeft(3, '0') + " (" + ((qtdeTotPresen * 100) / (qtdeTotPresen + qtdeTotFaltas)).ToString("N1") + "%)";
            lblTotFaltas.Text = qtdeTotFaltas.ToString().PadLeft(3, '0') + " (" + ((qtdeTotFaltas * 100) / (qtdeTotPresen + qtdeTotFaltas)).ToString("N1") + "%)";
            if (qtdeTotFaltas > 0)
            {
                lblTotFaltasJusti.Text = qtdeTotJustif.ToString().PadLeft(3, '0') + " (" + ((qtdeTotJustif * 100) / (qtdeTotFaltas)).ToString("N1") + "%)";
                lblTotFaltasNaoJusti.Text = qtdeTotNaoJustif.ToString().PadLeft(3, '0') + " (" + ((qtdeTotNaoJustif * 100) / (qtdeTotFaltas)).ToString("N1") + "%)";
            }
            else
            {
                lblTotFaltasJusti.Text = qtdeTotJustif.ToString().PadLeft(3, '0') + " (0%)";
                lblTotFaltasNaoJusti.Text = qtdeTotNaoJustif.ToString().PadLeft(3, '0') + " (0%)";
            }
        }

        private void GroupFooter1_AfterPrint(object sender, EventArgs e)
        {
            qtdeTotPresen = qtdeTotFaltas = qtdeTotJustif = qtdeTotNaoJustif = 0;
        }
    }
}
