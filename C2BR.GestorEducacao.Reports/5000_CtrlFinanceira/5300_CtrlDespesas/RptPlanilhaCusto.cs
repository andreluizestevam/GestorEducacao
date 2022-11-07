using System;
using System.Linq;
using System.Drawing;
using C2BR.GestorEducacao.Reports.Helper;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Data.Objects;
using System.Text;

namespace C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5300_CtrlDespesas
{
    public partial class RptPlanilhaCusto : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptPlanilhaCusto()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros, int coUndCont, string infos, int coMod, int coSer, string coAno, DateTime dtInicio, DateTime dtFim, string nomeFunc = null)
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;
                this.VisiblePageHeader = true;
                this.VisibleNumeroPage = false;
                this.VisibleDataHeader = false;
                this.VisibleHoraHeader = false;

                // Seta o nome do título
                if (nomeFunc != null) { lblTitulo.Text = nomeFunc.ToUpper(); } else { lblTitulo.Text = "PLanilha de Custo"; }

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(coUndCont);

                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                #region Query Planilha de Custo

                int ano = int.Parse(coAno);

                var PlanCusto = (from tb38 in TB38_CTA_PAGAR.RetornaTodosRegistros()
                                 join tb430 in TB430_MENSA_ANOBASE.RetornaTodosRegistros() on ano equals tb430.CO_ANO_BASE
                                 join tb01 in TB01_CURSO.RetornaTodosRegistros() on coSer equals tb01.CO_CUR
                                 join tb44 in TB44_MODULO.RetornaTodosRegistros() on coMod equals tb44.CO_MODU_CUR
                                 join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on coUndCont equals tb25.CO_EMP
                                 where tb01.TB44_MODULO.CO_MODU_CUR == tb44.CO_MODU_CUR
                                 && (dtInicio != DateTime.MinValue ? tb38.DT_VEN_DOC >= dtInicio : 0 == 0)
                                 && (dtFim != DateTime.MinValue ? ((tb38.DT_VEN_DOC < dtFim) || ((tb38.DT_VEN_DOC.Day == dtFim.Day) && (tb38.DT_VEN_DOC.Month == dtFim.Month) && (tb38.DT_VEN_DOC.Year == dtFim.Year))) : 0 == 0)
                                 && tb38.TB25_EMPRESA.CO_EMP == coUndCont
                                 select new PlanilhaCusto
                                 {
                                     Local = tb25.DE_END_EMP,
                                     MensadeAnoBase = (tb430.VALOR_MANHA != 0 ? tb430.VALOR_MANHA :
                                     tb430.VALOR_TARDE != 0 ? tb430.VALOR_TARDE :
                                     tb430.VALOR_NOITE != 0 ? tb430.VALOR_NOITE :
                                     tb430.VALOR_INTEGRAL != 0 ? tb430.VALOR_INTEGRAL : tb430.VALOR_ESPECIAL),
                                     MensaReajPropost = tb01.VL_CONTESP_AVIST ?? tb01.VL_CONTINT_AVIST ?? tb01.VL_CONTMAN_AVIST ?? tb01.VL_CONTNOI_AVIST ?? tb01.VL_CONTTAR_AVIST,
                                     DT_MensaReajPropost_V = tb01.DT_SITU_CUR,
                                     DT_Atual_V = DateTime.Now
                                 }
                  ).FirstOrDefault();

                var lst1 = (from tb38 in TB38_CTA_PAGAR.RetornaTodosRegistros()
                            join tb430 in TB430_MENSA_ANOBASE.RetornaTodosRegistros() on ano equals tb430.CO_ANO_BASE
                            join tb01 in TB01_CURSO.RetornaTodosRegistros() on coSer equals tb01.CO_CUR
                            join tb44 in TB44_MODULO.RetornaTodosRegistros() on coMod equals tb44.CO_MODU_CUR
                            join tb429 in TB429_REFER_CUSTO.RetornaTodosRegistros() on tb38.ID_ITEM_REFER_CUSTO equals tb429.ID_ITEM_REFER_CUSTO
                            where tb01.TB44_MODULO.CO_MODU_CUR == tb44.CO_MODU_CUR
                            && (dtInicio != DateTime.MinValue ? tb38.DT_VEN_DOC >= dtInicio : 0 == 0)
                            && (dtFim != DateTime.MinValue ? ((tb38.DT_VEN_DOC < dtFim) || ((tb38.DT_VEN_DOC.Day == dtFim.Day) && (tb38.DT_VEN_DOC.Month == dtFim.Month) && (tb38.DT_VEN_DOC.Year == dtFim.Year))) : 0 == 0)
                            && tb38.TB25_EMPRESA.CO_EMP == coUndCont
                            && tb38.ID_ITEM_REFER_CUSTO == tb429.ID_ITEM_REFER_CUSTO
                            && (tb429.NU_GRUPO == 1 || tb429.NU_GRUPO == 2)
                            select new Lista1_2
                            {
                                ComponenteCusto = tb429.NO_ITEM,
                                ValorAnoBase = "",
                                ValorAnoAplicacao = "",
                                NU_GRUPO = tb429.NU_GRUPO,
                                ORD_IMPRE = tb429.ORD_IMPRE
                            }
                  ).Distinct().OrderBy(n => n.NU_GRUPO).ThenBy(o => o.ORD_IMPRE).ToList();

                
                var lst2 = (from tb38 in TB38_CTA_PAGAR.RetornaTodosRegistros()
                            join tb430 in TB430_MENSA_ANOBASE.RetornaTodosRegistros() on ano equals tb430.CO_ANO_BASE
                            join tb01 in TB01_CURSO.RetornaTodosRegistros() on coSer equals tb01.CO_CUR
                            join tb44 in TB44_MODULO.RetornaTodosRegistros() on coMod equals tb44.CO_MODU_CUR
                            join tb429 in TB429_REFER_CUSTO.RetornaTodosRegistros() on tb38.ID_ITEM_REFER_CUSTO equals tb429.ID_ITEM_REFER_CUSTO
                            where tb01.TB44_MODULO.CO_MODU_CUR == tb44.CO_MODU_CUR
                            && (dtInicio != DateTime.MinValue ? tb38.DT_VEN_DOC >= dtInicio : 0 == 0)
                            && (dtFim != DateTime.MinValue ? ((tb38.DT_VEN_DOC < dtFim) || ((tb38.DT_VEN_DOC.Day == dtFim.Day) && (tb38.DT_VEN_DOC.Month == dtFim.Month) && (tb38.DT_VEN_DOC.Year == dtFim.Year))) : 0 == 0)
                            && tb38.TB25_EMPRESA.CO_EMP == coUndCont
                            && tb38.ID_ITEM_REFER_CUSTO == tb429.ID_ITEM_REFER_CUSTO
                            && (tb429.NU_GRUPO == 3 || tb429.NU_GRUPO == 4)
                            select new Lista3_4
                            {
                                ComponenteCusto = tb429.NO_ITEM,
                                ValorAnoBase = "",
                                ValorAnoAplicacao = "",
                                NU_GRUPO = tb429.NU_GRUPO,
                                ORD_IMPRE = tb429.ORD_IMPRE
                            }
                  ).Distinct().OrderBy(n => n.NU_GRUPO).ThenBy(o => o.ORD_IMPRE).ToList(); ;
                               

                var lst3 = (from tb38 in TB38_CTA_PAGAR.RetornaTodosRegistros()
                            join tb430 in TB430_MENSA_ANOBASE.RetornaTodosRegistros() on ano equals tb430.CO_ANO_BASE
                            join tb01 in TB01_CURSO.RetornaTodosRegistros() on coSer equals tb01.CO_CUR
                            join tb44 in TB44_MODULO.RetornaTodosRegistros() on coMod equals tb44.CO_MODU_CUR
                            join tb429 in TB429_REFER_CUSTO.RetornaTodosRegistros() on tb38.ID_ITEM_REFER_CUSTO equals tb429.ID_ITEM_REFER_CUSTO
                            where tb01.TB44_MODULO.CO_MODU_CUR == tb44.CO_MODU_CUR
                            && (dtInicio != DateTime.MinValue ? tb38.DT_VEN_DOC >= dtInicio : 0 == 0)
                            && (dtFim != DateTime.MinValue ? ((tb38.DT_VEN_DOC < dtFim) || ((tb38.DT_VEN_DOC.Day == dtFim.Day) && (tb38.DT_VEN_DOC.Month == dtFim.Month) && (tb38.DT_VEN_DOC.Year == dtFim.Year))) : 0 == 0)
                            && tb38.TB25_EMPRESA.CO_EMP == coUndCont
                            && tb38.ID_ITEM_REFER_CUSTO == tb429.ID_ITEM_REFER_CUSTO
                            && (tb429.NU_GRUPO == 5)
                            select new Lista5
                            {
                                ComponenteCusto = tb429.NO_ITEM,
                                ValorAnoBase = "",
                                ValorAnoAplicacao = "",
                                NU_GRUPO = tb429.NU_GRUPO,
                                ORD_IMPRE = tb429.ORD_IMPRE
                            }
                  ).Distinct().OrderBy(n => n.NU_GRUPO).ThenBy(o => o.ORD_IMPRE).ToList(); ;

                if (lst1.Count == 0 || lst2.Count == 0 || lst3.Count == 0)
                {
                    return -1;
                }
                else
                {
                    PlanCusto.lista1_2 = lst1;
                    PlanCusto.lista3_4 = lst2;
                    PlanCusto.lista5 = lst3;
                }
                               

                bsReport.Clear();
                bsReport.Add(PlanCusto);
                //var res = lst.Distinct().OrderBy(r => r.NomeAluno).ThenBy(o => o.ordImp).ToList();

                #endregion

                //if (res.Count == 0)
                //    return -1;
                //// Seta os dados no DataSource do Relatorio
                //bsReport.Clear();

                //foreach (var at in res)
                //{                 
                //    bsReport.Add(at);
                //}

                return 1;
            }
            catch { return 0; }
        }

        private DateTime FormataData(DateTime dt)
        {
            string dtFormat = dt.ToString().Substring(0, 10);
            return DateTime.Parse(dtFormat);
        }

        #endregion

        #region Classe Rendimento Escolar
        public class PlanilhaCusto
        {
            public PlanilhaCusto()
            {
                lista1_2 = new List<Lista1_2>();
                lista3_4 = new List<Lista3_4>();
                lista5 = new List<Lista5>();
            }
            public int NR_AlunosPagantes
            {
                get
                {
                    var alu = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                               join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb08.CO_ALU equals tb07.CO_ALU
                               where tb07.CO_ALU == tb08.CO_ALU
                               && tb08.CO_SIT_MAT == "A"
                               select new{
                                tb08.CO_ALU
                               }).Count();

                    return alu;
                }
            }
            public int NR_AlunosNaoPagantes {
                get
                {
                    var alu = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                               join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb08.CO_ALU equals tb07.CO_ALU
                               where tb07.CO_ALU == tb08.CO_ALU
                               && tb08.CO_SIT_MAT != "A"
                               select new
                               {
                                   tb08.CO_ALU
                               }).Count();

                    return alu;
                }
            }
            public Decimal? MensadeAnoBase { get; set; }
            public Decimal? MensaReajPropost { get; set; }
            public DateTime DT_MensaReajPropost_V { get; set; }
            public String DT_MensaReajPropost {
                get
                {
                    return this.DT_MensaReajPropost_V.ToString("dd/MM/yyyy");
                }
            }
            public String Local { get; set; }
            public DateTime DT_Atual_V { get; set; }
            public String DT_Atual
            {
                get
                {
                    return this.DT_Atual_V.ToString("dd/MM/yyyy");
                }
            }
            public List<Lista1_2> lista1_2 { get; set; }
            public List<Lista3_4> lista3_4 { get; set; }
            public List<Lista5> lista5 { get; set; }
        }

        public class Lista1_2
        {
            public String ComponenteCusto { get; set; }
            public String ValorAnoBase { get; set; }
            public String ValorAnoAplicacao { get; set; }
            public int? ORD_IMPRE { get; set; }
            public int NU_GRUPO { get; set; }
        }

        public class Lista3_4
        {
            public String ComponenteCusto { get; set; }
            public String ValorAnoBase { get; set; }
            public String ValorAnoAplicacao { get; set; }
            public int? ORD_IMPRE { get; set; }
            public int NU_GRUPO { get; set; }
        }

        public class Lista5
        {
            public String ComponenteCusto { get; set; }
            public String ValorAnoBase { get; set; }
            public String ValorAnoAplicacao { get; set; }
            public int? ORD_IMPRE { get; set; }
            public int NU_GRUPO { get; set; }
        }

        #endregion
    }
}
