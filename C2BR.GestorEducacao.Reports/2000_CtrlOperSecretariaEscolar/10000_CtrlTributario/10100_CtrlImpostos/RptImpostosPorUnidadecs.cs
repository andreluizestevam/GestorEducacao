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
using System.Data;
using System.Web;
using System.Resources;

namespace C2BR.GestorEducacao.Reports._10000_CtrlTributario._10100_CtrlImpostos
{
    public partial class RptImpostosPorUnidadecs : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptImpostosPorUnidadecs()
        {
            InitializeComponent();
        }
        #region Init Report

        public int InitReport(string parametros,
                        string infos,
                        int coEmp,
                        int Unidade
                        )
        {


            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = C2BR.GestorEducacao.Reports.ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;

                lblParametros.Text = parametros;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;
                #region Query

                var res = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()

                           where
                              (Unidade != 0 ? tb25.CO_EMP == Unidade : 0 == 0)

                           select new RelFuncionalColabor
                           {
                               unidade = tb25.NO_FANTAS_EMP

                           }).OrderBy(m => m.unidade).ToList();

                #endregion


                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (RelFuncionalColabor at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        public class RelFuncionalColabor
        {
            public string unidade { get; set; }
        }
    
    }
}

