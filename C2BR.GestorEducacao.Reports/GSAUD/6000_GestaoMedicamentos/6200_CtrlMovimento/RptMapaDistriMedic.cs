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

namespace C2BR.GestorEducacao.Reports.GSAUD._6000_GestaoMedicamentos._6200_CtrlMovimento
{
    public partial class RptMapaDistriMedic : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptMapaDistriMedic()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              string infos,
                              int coEmp,
                              int coUnid,
                              int LocalDept,
                              string dataIni,
                              string dataFim
                               )
        {
            try
            {
                DateTime dataIni1;
                if (!DateTime.TryParse(dataIni, out dataIni1))
                {
                    return 0;
                }

                DateTime dataFim1;
                if (!DateTime.TryParse(dataFim, out dataFim1))
                {
                    return 0;
                }

                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = C2BR.GestorEducacao.Reports.ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Colaborador/Departamento

                var lst = (from tb91 in TB91_MOV_PRODUTO.RetornaTodosRegistros()
                           join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb91.CO_EMP equals tb25.CO_EMP
                           join tb93 in TB93_TIPO_MOVIMENTO.RetornaTodosRegistros() on tb91.TB93_TIPO_MOVIMENTO.CO_TIPO_MOV equals tb93.CO_TIPO_MOV
                           where (coUnid != 0 ? tb25.CO_EMP == coUnid : 0 == 0)
                           && ((tb91.DT_MOV_PROD >= dataIni1) && (tb91.DT_MOV_PROD <= dataFim1))
                           select new DistrMedic
                           {
                               NO_EMP = tb25.NO_FANTAS_EMP,
                               CO_EMP = tb25.CO_EMP,
                               CO_TP_MOVI = tb93.CO_TIPO_MOV,
                               TipoMovimen = tb93.DE_TIPO_MOV,
                               FlagTipo = tb93.FLA_TP_MOV,
                               dataIni = dataIni1,
                               dataFim = dataFim1,
                               
                           }).Distinct().ToList();

                var res = lst.ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();

                int qtdE = 0;
                int qtdS = 0;

                foreach (DistrMedic at in res)
                {
                    if (at.FlagTipo == "E")
                        qtdE += at.QTD;
                    else if (at.FlagTipo == "S")
                        qtdS += at.QTD;

                    at.LinhaTotal = qtdE + " movimentos de Entrada, e " + qtdS + " movimentos de saída.";

                    bsReport.Add(at);
                }

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        public class DistrMedic
        {
            public string NO_EMP { get; set; }
            public string NO_LOCAL { get; set; }
            public string TipoMovimen { get; set; }
            public string FlagTipo { get; set; }

            //Códigos de Auxílio
            public int CO_EMP { get; set; }
            public int CO_DEPTO { get; set; }
            public int CO_TP_MOVI { get; set; }
            public DateTime dataIni { get; set; }
            public DateTime dataFim { get; set; }

            public int QTD 
            {
                get
                {
                    int QTD = (from tb91 in TB91_MOV_PRODUTO.RetornaTodosRegistros()
                               where tb91.CO_EMP == this.CO_EMP
                               && tb91.TB93_TIPO_MOVIMENTO.CO_TIPO_MOV == this.CO_TP_MOVI
                               && ((tb91.DT_MOV_PROD >= this.dataIni) && (tb91.DT_MOV_PROD <= this.dataFim))
                               select new {tb91.CO_MOV}).Count();
                    return QTD;
                }
            }

            public string LinhaTotal { get; set; }
        }
    }
}
