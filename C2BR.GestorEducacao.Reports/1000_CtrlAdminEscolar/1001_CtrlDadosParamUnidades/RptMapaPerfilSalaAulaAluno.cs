using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.Reports._1000_CtrlAdminEscolar._1001_CtrlDadosParamUnidades
{
    public partial class RptMapaPerfilSalaAulaAluno : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptMapaPerfilSalaAulaAluno()
        {
            InitializeComponent();
        }



        public int InitReport(string parametros,
                                int codEmp,
                                string strP_CO_EMP,
                                string strP_CO_TIPO_SALA,
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
                #region Query

                int intP_CO_EMP = strP_CO_EMP != "T" ? int.Parse(strP_CO_EMP) : 0;
                var res = (from tb248 in ctx.TB248_UNIDADE_SALAS_AULA
                           where strP_CO_EMP != "T" ? tb248.TB25_EMPRESA.CO_EMP == intP_CO_EMP : (0 == 0)
                           && strP_CO_TIPO_SALA != "T" ? tb248.CO_TIPO_SALA_AULA == strP_CO_TIPO_SALA : (0 == 0)
                           select new PerfilSalaAulaAluno
                           {
                               Instituicao = tb248.TB25_EMPRESA.NO_FANTAS_EMP,
                               Codigo = tb248.CO_IDENTI_SALA_AULA,
                               Tipo = tb248.CO_TIPO_SALA_AULA,
                               Largura = tb248.VL_LARGUR_SALA_AULA,
                               Comprimento = tb248.VL_COMPRI_SALA_AULA,
                               Altura = tb248.VL_ALTURA_SALA_AULA,
                               AlunoMax = tb248.QT_ALUNOS_MAXIM_SALA_AULA,
                               AlunoMat = tb248.QT_ALUNOS_MATRIC_SALA_AULA,
                               CadeiraMax = tb248.QT_CADEIR_MAXIM_SALA_AULA,
                               CadeiraDisp = tb248.QT_CADEIR_DISPON_SALA_AULA,
                               Ventilador = tb248.QT_VENTIL_SALA_AULA,
                               ArCond = tb248.QT_ARCOND_SALA_AULA
                           }).Distinct().ToList();
                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (PerfilSalaAulaAluno at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }


        public class PerfilSalaAulaAluno
        {
            public string Instituicao { get; set; }
            public string Codigo { get; set; }
            public string Tipo { get; set; }
            public decimal? Largura { get; set; }
            public decimal? Comprimento { get; set; }
            public decimal? Altura { get; set; }
            public decimal? Area
            {
                get
                {
                    return Largura != null ? Largura : 0 * Comprimento != null ?
                                 Comprimento : 0;
                }
            }
            public int? AlunoMax { get; set; }
            public int? AlunoMat { get; set; }
            public int? VagaDisp
            {
                get
                {
                    return AlunoMax != null ? AlunoMax : 0 - AlunoMat != null ? AlunoMat : 0;
                }
            }
            public int? CadeiraMax { get; set; }
            public int? CadeiraDisp { get; set; }
            public int? CadeiraLoc
            {
                get
                {
                    return (CadeiraMax != null ? CadeiraMax.Value : 0) - (CadeiraDisp != null ? CadeiraDisp.Value : 0);
                }
            }
            public decimal? Ventilador { get; set; }
            public decimal? ArCond { get; set; }

        }

        private void xrTableCell2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell obj = (XRTableCell)sender;
            if (!string.IsNullOrEmpty(obj.Text))
                obj.Text = String.Format("{0:#,##0.00}", decimal.Parse(obj.Text));
        }

        private void xrTableCell14_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell obj = (XRTableCell)sender;

            switch (obj.Text)
            {
                case "A": obj.Text = "Aula"; break;
                case "L": obj.Text = "Laboratório"; break;
                case "E": obj.Text = "Estudo"; break;
                case "M": obj.Text = "Monitoria"; break;
                case "O": obj.Text = "Outros"; break;
            }

        }
    }
}
