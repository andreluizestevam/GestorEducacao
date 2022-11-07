using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports.Helper;
using System.Globalization;

namespace C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3400_CtrlAtendimentoUsuario
{
    public partial class RptGuiaAtendPlantao : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptGuiaAtendPlantao()
        {
            InitializeComponent();
        }

        public int InitReport(
            int coPlantao,
            int coDepto,
            DateTime dtIni,
            DateTime dtFim,
            int coEmp
            )
        {
            try
            {
                #region Setar o Header e as Labels

                // Instancia o header do relatorio
                ReportHeader header = C2BR.GestorEducacao.Reports.ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;
                // Setar o header do relatorio
                this.BaseInit(header);
                #endregion

                var tb159 = TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
                            .Join(TB153_TIPO_PLANT.RetornaTodosRegistros(), a => a.ID_TIPO_PLANT, b => b.ID_TIPO_PLANT, (a, b) => new { a, b })
                            .Where(x => x.a.CO_AGEND_PLANT_COLAB == coPlantao).FirstOrDefault();

                if (tb159 != null)
                {
                    tb159.a.TB03_COLABORReference.Load();
                    tb159.a.TB03_COLABOR.TB25_EMPRESAReference.Load();
                    tb159.a.TB14_DEPTOReference.Load();
                    var profissional = new ProfissionalAtendente();
                    profissional.nomeProfissional = tb159.a.TB03_COLABOR.CO_MAT_COL + " - " + tb159.a.TB03_COLABOR.NO_COL.ToUpper() + " (" + tb159.a.TB03_COLABOR.DE_FUNC_COL + ")";
                    profissional.Unidade = "Unidade: " + tb159.a.TB03_COLABOR.TB25_EMPRESA.NO_FANTAS_EMP + " - Local: " + tb159.a.TB14_DEPTO.NO_DEPTO + ".";
                    profissional.dataIniPlantao = tb159.a.DT_INICIO_PREV;
                    profissional.dataFimPlantao = tb159.a.DT_TERMIN_PREV;
                    profissional.TipoPlantao = tb159.b.NO_TIPO_PLANT;
                    profissional.QtdeHoras = tb159.b.QT_HORAS;

                    var tbs455 = TBS455_AGEND_PROF_INTER.RetornaTodosRegistros()
                             .Where(x => (x.TB159_AGENDA_PLANT_COLABOR.CO_AGEND_PLANT_COLAB == coPlantao)
                                    && (coDepto > 0 && x.TBS451_INTER_REGIST != null ? x.TBS451_INTER_REGIST.TB14_DEPTO.CO_DEPTO == coDepto : coDepto > 0
                                    && x.TBS174_AGEND_HORAR != null ? x.TBS174_AGEND_HORAR.CO_DEPT == coDepto : 0 == 0))
                              .Select(x => new Plantao 
                              { 
                                    dtPrevista = x.DT_ACOMP_PROFI,
                                    hrPrevista = x.HR_ACOMP_PROFI,
                                    nomePaciente =  x.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.TB07_ALUNO.NO_ALU,
                                    nuNire = x.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.TB07_ALUNO.NU_NIRE
                              })
                              .ToList();

                    profissional.Plantoes = tbs455;

                    bsReport.Clear();
                    bsReport.Add(profissional);

                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            catch
            {
                return 0;
            }
        }

        public class ProfissionalAtendente
        {
            public string nomeProfissional { get; set; }
            public DateTime dataIniPlantao { get; set; }
            public string DataPlantao { get { return " com início previsto às " + this.dataIniPlantao.ToShortTimeString() + " " + this.dataIniPlantao.ToLongDateString() + " e termino às " + this.dataFimPlantao.ToShortTimeString() + " " + this.dataFimPlantao.ToLongDateString(); } }
            public DateTime dataFimPlantao { get; set; }
            public string Unidade { get; set; }
            public string TipoPlantao { get; set; }
            public int QtdeHoras { get; set; }
            public string infoPlantao { get { return "Plantão tipo " + this.TipoPlantao.ToUpper() + this.DataPlantao + " totalizando " + this.QtdeHoras.ToString() + " horas."; } }
            public List<Plantao> Plantoes { get; set; }
        }


        public class Plantao
        {
            public string nomePaciente { get; set; }
            public int nuNire { get; set; }
            public string Paciente { get { return this.nuNire.ToString() + " - " + this.nomePaciente; } }
            public DateTime dtPrevista { get; set; }
            public TimeSpan hrPrevista { get; set; }
            public string DataPrevista { get { return this.dtPrevista.ToShortDateString(); } }
            public string HoraPrevista { get { return this.hrPrevista.ToString(); } }
        }
    }
}
