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
using C2BR.GestorEducacao.Reports;


namespace C2BR.GestorEducacao.Reports._1000_CtrlAdminEscolar._1001_CtrlDadosParamUnidades
{
    public partial class RptMapaPerfilDesempAluno : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptMapaPerfilDesempAluno()
        {
            InitializeComponent();
        }

        public int InitReport(string parametros,
                                int codEmp,
                                string strP_CO_EMP_REF,
                                string strP_ANO,
                                string strP_CO_CUR,
                                string strP_CO_TUR,
                                string strP_CO_MODU_CUR,    
                                string strP_ID_MATERIA,
                                string strP_NOTAMIN,
                                string strP_NOTAMAX,
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

                decimal intNOTAMIN = strP_NOTAMIN != "T" ? decimal.Parse(strP_NOTAMIN) : 0;
                decimal intNOTAMAX = strP_NOTAMAX != "T" ? decimal.Parse(strP_NOTAMAX) : 0;

                int intP_CO_CUR = strP_CO_CUR != "T" ? int.Parse(strP_CO_CUR) : 0;
                int intP_CO_TUR = strP_CO_TUR != "T" ? int.Parse(strP_CO_TUR) : 0;
                int intP_ID_MATERIA = strP_ID_MATERIA != "T" ? int.Parse(strP_ID_MATERIA) : 0;
                int intCodEmp = strP_CO_EMP_REF != "T" ? int.Parse(strP_CO_EMP_REF) : 0;
                int intP_CO_MODU_CUR = strP_CO_MODU_CUR != "T" ? int.Parse(strP_CO_MODU_CUR) : 0;

                var lst = (from tb247 in ctx.TB247_UNIDADE_PERFIL_DESEMPENHO
                           join tb25 in ctx.TB25_EMPRESA on tb247.TB06_TURMAS.CO_EMP equals tb25.CO_EMP
                           join tb44 in ctx.TB44_MODULO on tb247.TB06_TURMAS.CO_MODU_CUR equals tb44.CO_MODU_CUR
                           join tb107 in ctx.TB107_CADMATERIAS on tb247.ID_MATERIA equals tb107.ID_MATERIA
                           where strP_CO_EMP_REF != "T" ? (tb25.CO_EMP == intCodEmp) : (0 == 0)
                           && strP_ANO != "T" ? (tb247.NR_ANO == strP_ANO) : (0 == 0)
                           && strP_CO_CUR != "T" ? (tb247.TB06_TURMAS.TB01_CURSO.CO_CUR == intP_CO_CUR) : (0 == 0)
                           && strP_CO_TUR != "T" ? (tb247.TB06_TURMAS.CO_TUR == intP_CO_TUR) : (0 == 0)
                           && strP_ID_MATERIA != "T" ? tb247.ID_MATERIA == intP_ID_MATERIA : (0 == 0)
                           && strP_CO_MODU_CUR != "T" ? tb44.CO_MODU_CUR == intP_CO_MODU_CUR : (0 == 0)                           
                           orderby tb25.sigla, tb247.NR_ANO, tb44.CO_SIGLA_MODU_CUR,
                           tb247.TB06_TURMAS.TB01_CURSO.CO_SIGL_CUR, tb247.TB06_TURMAS.TB129_CADTURMAS.CO_SIGLA_TURMA,
                           tb107.NO_SIGLA_MATERIA
                           select new UnidadePerfilDesemp
                           {
                               Unidade = tb25.sigla,
                               Ano = tb247.NR_ANO,
                               Modalidade = tb44.CO_SIGLA_MODU_CUR,
                               Serie = tb247.TB06_TURMAS.TB01_CURSO.CO_SIGL_CUR,
                               Turma = tb247.TB06_TURMAS.TB129_CADTURMAS.CO_SIGLA_TURMA,
                               Materia = tb107.NO_SIGLA_MATERIA,
                               bim1 = tb247.NR_MEDIA_BIM1_DESEMP,
                               bim2 = tb247.NR_MEDIA_BIM2_DESEMP,
                               bim3 = tb247.NR_MEDIA_BIM3_DESEMP,
                               bim4 = tb247.NR_MEDIA_BIM4_DESEMP
                           });


                var res = (from c in lst.ToList()
                           where (strP_NOTAMIN != "T" && strP_NOTAMAX != "T") ? (c.media >= intNOTAMIN
                           && c.media <= intNOTAMAX) : (0 == 0)
                           select c).ToList();

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (UnidadePerfilDesemp at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }


        public class UnidadePerfilDesemp
        {
            public string Unidade { get; set; }
            public string Ano { get; set; }
            public string Modalidade { get; set; }
            public string Serie { get; set; }
            public string Turma { get; set; }
            public string Materia { get; set; }
            public decimal? bim1 { get; set; }
            public decimal? bim2 { get; set; }
            public decimal? bim3 { get; set; }
            public decimal? bim4 { get; set; }

            public decimal? media
            {
                get
                {
                    return (bim1.GetValueOrDefault(0) + bim2.GetValueOrDefault(0) + bim3.GetValueOrDefault(0) + bim4.GetValueOrDefault(0)) / 4;
                }
            }

        }

        private void xrTableCell18_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell obj = (XRTableCell)sender;
            if (obj.Text != "")
            {
                obj.Text = String.Format("{0:#,##0.00}", decimal.Parse(obj.Text));
            }
            else { obj.Text = "-"; }
        }
    }

}
