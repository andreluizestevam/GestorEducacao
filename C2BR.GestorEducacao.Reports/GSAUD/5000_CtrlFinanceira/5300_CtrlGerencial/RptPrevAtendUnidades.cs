using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports.Helper;
using C2BR.GestorEducacao.Reports;
using DevExpress.XtraCharts;

namespace C2BR.GestorEducacao.Reports.GSAUD._5000_CtrlFinanceira._5300_CtrlGerencial
{
    public partial class RptPrevAtendUnidades : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptPrevAtendUnidades()
        {
            InitializeComponent();
        }

        public int InitReport(
              string titulo,
              string infos,
              string parametros,
              int coEmp,
              string UF,
              int Cidade,
              int Unidade,
              int Mes,
              int Ano
            )
        {
            try
            {
                // Seta as informaçoes do rodape do relatorio
                this.InfosRodape = infos;
                this.Parametros = parametros;
                this.lblTitulo.Text = titulo.ToUpper();

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;
                
                // Setar o header do relatorio
                this.BaseInit(header);

                var res = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                           join tb904 in TB904_CIDADE.RetornaTodosRegistros() on tb25.CO_CIDADE equals tb904.CO_CIDADE
                           where (tb25.CO_UF_EMP == UF)
                           && (Cidade != 0 ? tb25.CO_CIDADE == Cidade : Cidade == 0)
                           && (Unidade != 0 ? tb25.CO_EMP == Unidade : Unidade == 0)
                           select new Relatorio
                           {
                               idUnidade = tb25.CO_EMP,
                               Unidade = tb25.NO_FANTAS_EMP,
                               UF = tb25.CO_UF_EMP,
                               Cidade = tb904.NO_CIDADE,
                               CNPJ = tb25.CO_CPFCGC_EMP
                           }).OrderBy(w => w.UF).ThenBy(w => w.Cidade).ThenBy(w => w.Unidade).DistinctBy(w => w.idUnidade).ToList();

                if (res.Count == 0)
                    return -1;

                DateTime dtInicalAno = new DateTime(Ano, 1, 1);
                DateTime dtFinalAno = dtInicalAno.AddYears(1).AddDays(-1);

                DateTime dtInicalPrimeiro = new DateTime(Ano, Mes, 1);
                DateTime dtFinalPrimeiro = dtInicalPrimeiro.AddMonths(1).AddDays(-1);

                DateTime dtInicalSegundo = dtInicalPrimeiro.AddMonths(1);
                DateTime dtFinalSegundo = dtInicalSegundo.AddMonths(1).AddDays(-1);

                DateTime dtInicalTerceiro = dtInicalSegundo.AddMonths(1);
                DateTime dtFinalTerceiro = dtInicalTerceiro.AddMonths(1).AddDays(-1);

                DateTime dtInicalQuarto = dtInicalTerceiro.AddMonths(1);
                DateTime dtFinalQuarto = dtInicalQuarto.AddMonths(1).AddDays(-1);

                var anoRef = Ano.ToString();
                //Alteração para iniciar a partir da data atual por não ser possivel atender no passado caso seja nesse ano
                if (dtInicalAno.Year == DateTime.Now.Year)
                    dtInicalAno = DateTime.Now.Date;

                if (dtInicalAno.Year != dtInicalQuarto.Year)
                {
                    anoRef = Ano.ToString() + "/" + dtInicalQuarto.Year.ToString();
                    dtFinalAno = dtInicalQuarto;
                }

                foreach (var i in res)
                {
                    i.Ano = anoRef;
                    i.MesPrimeiro = dtInicalPrimeiro.ToString("MMMM").ToUpper();
                    i.MesSegundo = dtInicalSegundo.ToString("MMMM").ToUpper();
                    i.MesTerceiro = dtInicalTerceiro.ToString("MMMM").ToUpper();
                    i.MesQuarto = dtInicalQuarto.ToString("MMMM").ToUpper();

                    var agds = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                where tbs174.CO_EMP == i.idUnidade
                                && tbs174.DT_AGEND_HORAR >= dtInicalAno
                                && tbs174.DT_AGEND_HORAR <= dtFinalAno
                                && tbs174.CO_ALU != null
                                //Recuperando apenas atendimentos que estejam presentes ou em aberto
                                && (tbs174.CO_SITUA_AGEND_HORAR == "A" && (tbs174.FL_AGEND_ENCAM == "N" || String.IsNullOrEmpty(tbs174.FL_AGEND_ENCAM)))
                                select tbs174).ToList();

                    if (agds != null)
                    {
                        foreach (var a in agds)
                        {
                            decimal vlrConsul = 0;
                            a.TBS389_ASSOC_ITENS_PLANE_AGEND.Load();
                            if (a.TBS389_ASSOC_ITENS_PLANE_AGEND != null && a.TBS389_ASSOC_ITENS_PLANE_AGEND.Count > 0)
                            {
                                foreach (var p in a.TBS389_ASSOC_ITENS_PLANE_AGEND)
                                    vlrConsul += p.TBS386_ITENS_PLANE_AVALI.VL_PROCED.HasValue ? p.TBS386_ITENS_PLANE_AVALI.VL_PROCED.Value : 0;
                            }
                            else
                                vlrConsul = a.VL_CONSUL.HasValue ? a.VL_CONSUL.Value : 0;

                            i.QtdAno++;
                            i.VlrAno += vlrConsul;

                            if (a.DT_AGEND_HORAR >= dtInicalPrimeiro && a.DT_AGEND_HORAR <= dtFinalPrimeiro)
                            {
                                i.QtdPrimeiro++;
                                i.VlrPrimeiro += vlrConsul;
                            }

                            if (a.DT_AGEND_HORAR >= dtInicalSegundo && a.DT_AGEND_HORAR <= dtFinalSegundo)
                            {
                                i.QtdSegundo++;
                                i.VlrSegundo += vlrConsul;
                            }

                            if (a.DT_AGEND_HORAR >= dtInicalTerceiro && a.DT_AGEND_HORAR <= dtFinalTerceiro)
                            {
                                i.QtdTerceiro++;
                                i.VlrTerceiro += vlrConsul;
                            }

                            if (a.DT_AGEND_HORAR >= dtInicalQuarto && a.DT_AGEND_HORAR <= dtFinalQuarto)
                            {
                                i.QtdQuarto++;
                                i.VlrQuarto += vlrConsul;
                            }
                        }
                    }
                }

                //Adiciona ao DataSource do Relatório
                bsReport.Clear();
                foreach (var item in res)
                    bsReport.Add(item);
                return 1;
            }
            catch { return 0; }
        }

        public class Relatorio
        {
            public int idUnidade { get; set; }
            public string Unidade { get; set; }
            public string UF { get; set; }
            public string Cidade { get; set; }
            private string CNPJ_;
            public string CNPJ
            {
                get
                {
                    return (!String.IsNullOrEmpty(CNPJ_) ? CNPJ_.Format(TipoFormat.CNPJ) : "-");
                }
                set
                {
                    CNPJ_ = value;
                }
            }

            public string MesPrimeiro { get; set; }
            public int QtdPrimeiro { get; set; }
            public decimal VlrPrimeiro { get; set; }

            public string MesSegundo { get; set; }
            public int QtdSegundo { get; set; }
            public decimal VlrSegundo { get; set; }

            public string MesTerceiro { get; set; }
            public int QtdTerceiro { get; set; }
            public decimal VlrTerceiro { get; set; }

            public string MesQuarto { get; set; }
            public int QtdQuarto { get; set; }
            public decimal VlrQuarto { get; set; }

            public string Ano { get; set; }
            public int QtdAno { get; set; }
            public decimal VlrAno { get; set; }
        }
    }
}
