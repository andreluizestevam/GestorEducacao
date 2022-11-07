using System;
using System.Data.Entity;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Text.RegularExpressions;
using C2BR.GestorEducacao.Reports.Helper;
using System.Globalization;

namespace C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3400_CtrlAtendimentoUsuario
{
    public partial class RptDemonPreAtend : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptDemonPreAtend()
        {
            InitializeComponent();
        }

        public int InitReport(string parametros,
                              string infos,
                              string NomeFuncionalidadeCadastrada,
                              int rptUnidade,
                              int rptPaciente,
                              int rptClassRisco,
                              int rptEspec,
                              string rptTipoEncam,
                              DateTime rptDtIni,
                              DateTime rptDtFim)
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;

                // Instancia o header do relatorio
                ReportHeader header = C2BR.GestorEducacao.Reports.ReportHeader.GetHeaderFromEmpresa(rptUnidade);
                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                if (!String.IsNullOrEmpty(NomeFuncionalidadeCadastrada))
                {
                    lblTitulo.Text = NomeFuncionalidadeCadastrada;
                }
                else
                {
                    lblTitulo.Text = "Demonstrativo de Pré-Atendimento (Triagem)";
                }

                #endregion

                rptDtFim = rptDtFim.AddHours(23).AddMinutes(59).AddSeconds(59);

                var result1 = (from tbs194 in TBS194_PRE_ATEND.RetornaTodosRegistros()
                               join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs194.CO_ALU equals tb07.CO_ALU
                               join tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros() on tbs194.ID_AGEND_HORAR equals tbs174.ID_AGEND_HORAR
                               where (rptPaciente != 0 ? rptPaciente == tb07.CO_ALU : 0 == 0)
                               && (rptTipoEncam != "T" ? rptTipoEncam == tbs194.FL_TIPO_ENCAM : 0 == 0)
                               && (rptClassRisco != -1 ? rptClassRisco == tbs194.CO_TIPO_RISCO : 0 == 0)
                               && (rptEspec != -1 ? rptEspec == tbs194.CO_ESPEC : 0 == 0)
                               && (DateTime.Compare(tbs174.DT_AGEND_HORAR, rptDtIni) >= 0)
                               && (DateTime.Compare(tbs174.DT_AGEND_HORAR, rptDtFim) <= 0)
                               select new DemonPreAtend
                               {
                                   dataAgend = tbs174.DT_AGEND_HORAR,
                                   horaAgend = tbs174.HR_AGEND_HORAR,
                                   numerRap = tbs174.NU_REGIS_CONSUL,
                                   nomePacie = tb07.NO_ALU,
                                   dataRecep = tbs174.DT_PRESE,
                                   dataIniciTriag = tbs174.DT_PRESE,
                                   dataFinalTriag = tbs194.DT_SITUA_PRE_ATEND,
                                   classRisco = tbs194.CO_TIPO_RISCO,
                                   tipoEncam = tbs174.FL_AGEND_ENCAM,
                                   Espec = tbs194.CO_ESPEC,
                               }).ToList();

                bsReport.Clear();

                var result2 = result1.Count();

                foreach (var i in result1)
                {
                    bsReport.Add(i);
                }

                foreach (var i in result1)
                {
                    var res = (from tbs435 in TBS435_CLASS_RISCO.RetornaTodosRegistros()
                               where i.classRisco == tbs435.ID_CLASS_RISCO
                               select new
                               {
                                   tbs435.NO_PRIOR,
                                   tbs435.CO_COR,
                                   tbs435.NO_COR
                               }).FirstOrDefault();

                    xrTableCell35.BackColor = ColorTranslator.FromHtml(res.CO_COR);
                }

                //xrTableRow2.Styles.EvenStyle.BackColor = System.Drawing.Color.Gray; 

                return 1;
            }
            catch { return 0; }
        }

        public class DemonPreAtend
        {

            public DateTime dataAgend { get; set; }
            public string horaAgend { get; set; }
            public string trtDataAgend
            {
                get
                {
                    return dataAgend.ToShortDateString().ToString() + " " + horaAgend;
                }
            }
            public string numerRap { get; set; }
            public string nomePacie { get; set; }
            public DateTime? dataRecep { get; set; }
            public string tempoAteAtend
            {
                get
                {
                    int horaSub, minutSub;

                    if (dataRecep.HasValue && dataIniciTriag.HasValue)
                    {
                        horaSub = Convert.ToDateTime(dataRecep).Hour - Convert.ToDateTime(dataIniciTriag).Hour;
                        minutSub = Convert.ToDateTime(dataRecep).Minute - Convert.ToDateTime(dataIniciTriag).Minute;
                    }
                    else
                    {
                        horaSub = 0;
                        minutSub = 0;
                    }

                    return horaSub + "h " + minutSub + "m ";
                }
            }
            public DateTime? dataIniciTriag { get; set; }
            public DateTime? dataFinalTriag { get; set; }
            public string tempoDuracTriag
            {
                get
                {
                    int horaSub, minutSub;

                    if (dataIniciTriag.HasValue && dataFinalTriag.HasValue)
                    {
                        horaSub = Convert.ToDateTime(dataRecep).Hour - Convert.ToDateTime(dataIniciTriag).Hour;
                        minutSub = Convert.ToDateTime(dataRecep).Minute - Convert.ToDateTime(dataIniciTriag).Minute;
                    }
                    else
                    {
                        horaSub = 0;
                        minutSub = 0;
                    }

                    return horaSub + "h " + minutSub + "m ";
                }
            }
            public int? classRisco { get; set; }
            public string trtClassRisco
            {
                get
                {
                    var res = (from tbs435 in TBS435_CLASS_RISCO.RetornaTodosRegistros()
                               where classRisco == tbs435.ID_CLASS_RISCO
                               select new { 
                                tbs435.CO_PRIOR
                               }).FirstOrDefault();

                    return res.CO_PRIOR;
                }
            }
            public string tipoEncam { get; set; }
            public string trtTipoEncam
            {
                get
                {
                    if (tipoEncam == "P")
                    { return "Recepção"; }
                    else if (tipoEncam == "A")
                    { return "Atendimento"; }
                    else
                    { return "Não definido"; }
                }
            }
            public int? Espec { get; set; }
            public string trtEspec
            {
                get
                {
                    var res = (from tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros()
                               where Espec == tb63.CO_ESPECIALIDADE
                               select new { tb63.CO_ESPECIALIDADE, tb63.NO_ESPECIALIDADE }).FirstOrDefault();

                    return res.NO_ESPECIALIDADE;
                }
            }

        }
    }
}