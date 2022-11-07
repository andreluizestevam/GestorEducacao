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

namespace C2BR.GestorEducacao.Reports.GSAUD._7000_ControleOperRH
{
    public partial class RptExtratoFreqFuncional : C2BR.GestorEducacao.Reports._1000_CtrlAdminEscolar._1200_GestaoOperColaboradores.RptExtratoFreqFuncional
    {

        #region Fields

        string periodo;
        string colaborador;
        string resumo;

        #endregion

        #region ctor

        public RptExtratoFreqFuncional()
        {
            InitializeComponent();
        }
        #endregion

        #region Init Report

        public int InitReport(int codEmp, int codCol, DateTime dtInicio, DateTime dtFim, string infos)
        {
            try
            {
                #region Inicializa o header/Labels

                // Seta as informaçoes do rodape do relatorio
                this.InfosRodape = infos;

                // Cria o header a partir do cod da instituicao
                var header = ReportHeader.GetHeaderFromEmpresa(codEmp);
                if (header == null)
                    return -1;

                // Inicializa o header
                base.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Ocorrencias

                var res = (from tb199 in ctx.TB199_FREQ_FUNC
                           where tb199.CO_COL == codCol && tb199.STATUS == "A"
                           && tb199.DT_FREQ >= dtInicio && tb199.DT_FREQ <= dtFim
                           select new
                           {
                               Nome = tb199.TB03_COLABOR.NO_COL,
                               Mat = tb199.TB03_COLABOR.CO_MAT_COL,
                               Seq = tb199.CO_SEQ_FREQ,
                               Data = tb199.DT_FREQ,
                               Hora = tb199.HR_FREQ,
                               Tipo = tb199.TP_FREQ,
                               FlaJust = tb199.FL_JUSTI_FALTA
                           }).ToList();

                if (res.Count() == 0)
                    return -1;

                this.periodo = string.Format("{0} a {1}", dtInicio.ToString("dd/MM/yyyy"),
                    dtFim.ToString("dd/MM/yyyy"));
                this.colaborador = string.Format("{0} ({1})", res.First().Nome,
                    Funcoes.Format(res.First().Mat, TipoFormat.MatriculaColaborador));

                List<ExtratoFrequencia> lm = res.DistinctBy(x => x.Data)
                    .Select(x => new ExtratoFrequencia { Data = x.Data }).ToList();

                foreach (var mv in lm)
                {
                    var rEnt = res.Where(x => x.Data == mv.Data && x.Tipo == "E").OrderBy(x => x.Seq)
                        .Select(x => x.Hora.ToString()).ToList();
                    mv.Entrada1 = rEnt.ElementAtOrDefault(0);
                    mv.Entrada2 = rEnt.ElementAtOrDefault(1);
                    mv.Entrada3 = rEnt.ElementAtOrDefault(2);

                    var rSaida = res.Where(x => x.Data == mv.Data && x.Tipo == "S").OrderBy(x => x.Seq)
                        .Select(x => x.Hora.ToString()).ToList();
                    mv.Saida1 = rSaida.ElementAtOrDefault(0);
                    mv.Saida2 = rSaida.ElementAtOrDefault(1);
                    mv.Saida3 = rSaida.ElementAtOrDefault(2);

                    mv.Tipo = res.Any(x => x.Data == mv.Data && x.Tipo == "F" && x.FlaJust == "S")
                        ? "Falta Justificada"
                        : res.Any(x => x.Data == mv.Data && x.Tipo == "F") ? "Falta" : "Presenca";
                }

                this.resumo = string.Format("{0} dia(s) - {1} Presenca(s) - {2} Falta(s)",
                    lm.Count, lm.Count(x => x.Tipo == "Presenca"), lm.Count(x => x.Tipo != "Presenca"));

                #endregion

                // Adiciona os movimentos ao DataSource do Relatório
                bsReport.Clear();

                foreach (var m in lm)
                    bsReport.Add(m);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Label Events

        private void lblPeriodo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel lbl = sender as XRLabel;
            lbl.Text = this.periodo;
        }

        private void lblColaborador_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel lbl = sender as XRLabel;
            lbl.Text = this.colaborador;
        }

        private void lblResumo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel lbl = sender as XRLabel;
            lbl.Text = this.resumo;
        }

        #endregion

        #region Class QuantMovimento

        public class ExtratoFrequencia
        {
            private string entrada1;
            private string saida1;
            private string entrada2;
            private string saida2;
            private string entrada3;
            private string saida3;

            public DateTime Data { get; set; }

            public string DiaSemana
            {
                get { return Data.ToString("dddd", new CultureInfo("pt-BR")); }
            }

            public string Entrada1
            {
                get { return (string.IsNullOrEmpty(entrada1)) ? "-" : entrada1.PadLeft(4, '0').Insert(2, ":"); }
                set { this.entrada1 = value; }
            }
            public string Saida1
            {
                get { return (string.IsNullOrEmpty(saida1)) ? "-" : saida1.PadLeft(4, '0').Insert(2, ":"); }
                set { this.saida1 = value; }
            }
            public string Entrada2
            {
                get { return (string.IsNullOrEmpty(entrada2)) ? "-" : entrada2.PadLeft(4, '0').Insert(2, ":"); }
                set { this.entrada2 = value; }
            }
            public string Saida2
            {
                get { return (string.IsNullOrEmpty(saida2)) ? "-" : saida2.PadLeft(4, '0').Insert(2, ":"); }
                set { this.saida2 = value; }
            }
            public string Entrada3
            {
                get { return (string.IsNullOrEmpty(entrada3)) ? "-" : entrada3.PadLeft(4, '0').Insert(2, ":"); }
                set { this.entrada3 = value; }
            }
            public string Saida3
            {
                get { return (string.IsNullOrEmpty(saida3)) ? "-" : saida3.PadLeft(4, '0').Insert(2, ":"); }
                set { this.saida3 = value; }
            }

            public string Tipo { get; set; }
        }

        #endregion
    }
}
