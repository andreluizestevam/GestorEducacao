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
using C2BR.GestorEducacao.Reports;

namespace C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8100_CtrlAtendimetoMedico
{
    public partial class RptQntProcePlano : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptQntProcePlano()
        {
            InitializeComponent();
        }

        public int InitReport(
              string nomeFunc,
              string parametros,
              string infos,
              int coEmp,
              int Procedimento,
              int oper,
              int plano,
              DateTime dataIni,
              DateTime dataFim
            )
        {
            try
            {
                // Seta as informaçoes do rodape do relatorio
                this.InfosRodape = infos;
                this.Parametros = parametros;
                lblTitulo.Text = !String.IsNullOrEmpty(nomeFunc) ? nomeFunc.ToUpper() : "-";

                // Instancia o header do relatorio
                ReportHeader header = C2BR.GestorEducacao.Reports.ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                var res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                           select new Result
                           {

                           }).FirstOrDefault();

                if (res == null)
                    return -1;

                //dataFim = dataFim.AddDays(1);
                var Result = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                              join tbs427 in TBS427_SERVI_AMBUL_ITENS.RetornaTodosRegistros() on tbs356.ID_PROC_MEDI_PROCE equals tbs427.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE
                              join tbs428 in TBS428_APLIC_SERVI_AMBUL.RetornaTodosRegistros() on tbs427.ID_LISTA_SERVI_AMBUL equals tbs428.TBS427_SERVI_AMBUL_ITENS.ID_LISTA_SERVI_AMBUL
                              join tbs353 in TBS353_VALOR_PROC_MEDIC_PROCE.RetornaTodosRegistros() on tbs356.ID_PROC_MEDI_PROCE equals tbs353.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE
                              where tbs428.DT_APLIC__SERVI_AMBUL >= (DateTime?)dataIni && tbs428.DT_APLIC__SERVI_AMBUL <= (DateTime?)dataFim
                              && (Procedimento == 0 ? 0 == 0 : Procedimento == tbs356.ID_PROC_MEDI_PROCE)
                              && (coEmp == 0 ? 0==0 : coEmp == tbs353.CO_EMP_LANC)
                              select tbs356).ToList();

                if (Result.Count == 0)
                    return -1;

                res.ProcedList = new List<Procedi>();
                


                foreach (var p in Result)
                {
                    

                    p.TBS353_VALOR_PROC_MEDIC_PROCE.Load();
                    p.TBS427_SERVI_AMBUL_ITENS.Load();

                    var Proced = new Procedi();
                    var resQntProced = new QntProcedimento();
                    Proced.QntProcedList = new List<QntProcedimento>();

                    if (res.ProcedList.Where(x => x.Id == p.ID_PROC_MEDI_PROCE).FirstOrDefault() != null)
                    {
                        Proced = res.ProcedList.Where(x => x.Id == p.ID_PROC_MEDI_PROCE).FirstOrDefault();
                    }
                    else
                    {
                        Proced.Id = p.ID_PROC_MEDI_PROCE;
                        Proced.Proced = p.NM_PROC_MEDI;
                        Proced.codigo = p.CO_PROC_MEDI;
                        Proced.Descricao = p.DE_OBSE_PROC_MEDI;
                        


                        foreach (var v in p.TBS353_VALOR_PROC_MEDIC_PROCE)
                        {

                            if (res.ProcedList.Where(x => x.Id == v.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE).FirstOrDefault() != null)
                            {
                                Proced = res.ProcedList.Where(x => x.Id == v.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE).FirstOrDefault();
                            }
                            else
                            {                                
                                Proced.valorUnit = v.VL_BASE;                   
                                                               
                            }
                        }

                        var qntProced = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                                         join tbs427 in TBS427_SERVI_AMBUL_ITENS.RetornaTodosRegistros() on tbs356.ID_PROC_MEDI_PROCE equals tbs427.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE
                                         join tbs426 in TBS426_SERVI_AMBUL.RetornaTodosRegistros() on tbs427.TBS426_SERVI_AMBUL.ID_SERVI_AMBUL equals tbs426.ID_SERVI_AMBUL
                                         join tbs428 in TBS428_APLIC_SERVI_AMBUL.RetornaTodosRegistros() on tbs427.ID_LISTA_SERVI_AMBUL equals tbs428.TBS427_SERVI_AMBUL_ITENS.ID_LISTA_SERVI_AMBUL
                                         join tbs353 in TBS353_VALOR_PROC_MEDIC_PROCE.RetornaTodosRegistros() on tbs356.ID_PROC_MEDI_PROCE equals tbs353.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE
                                         where tbs428.DT_APLIC__SERVI_AMBUL >= (DateTime?)dataIni && tbs428.DT_APLIC__SERVI_AMBUL <= (DateTime?)dataFim
                                         && p.ID_PROC_MEDI_PROCE == tbs356.ID_PROC_MEDI_PROCE
                                         && (coEmp == 0 ? 0 == 0 : coEmp == tbs353.CO_EMP_LANC)
                                         select new
                                         {
                                             tbs356.ID_PROC_MEDI_PROCE,
                                             tbs353.VL_BASE,
                                             tbs428.IS_APLIC_SERVI_AMBUL
                                         }).ToList();
                        resQntProced.qntProced = qntProced.Count;
                        resQntProced.qntFinalizado = qntProced.Where(x => x.IS_APLIC_SERVI_AMBUL.Equals("S")).ToList().Count;
                        resQntProced.qntNFinalizado = qntProced.Where(x => x.IS_APLIC_SERVI_AMBUL.Equals("N")).ToList().Count;
                        resQntProced.valorTotal = resQntProced.qntFinalizado * Proced.valorUnit;
                        Proced.QntProcedList.Add(resQntProced);
                        res.ProcedList.Add(Proced);
                    }                    
                    
                }

                //Adiciona ao DataSource do Relatório
                bsReport.Clear();
                bsReport.Add(res);
                return 1;

            }
            catch { return 0; }
        }

        public class Result
        {
            public List<Procedi> ProcedList { get; set; }
            
        }

        public class Procedi
        {
            public int Id { get; set; }
            public string codigo { get; set; }
            public decimal valorUnit { get; set; }
            public string Proced { get; set; }
            public string Descricao { get; set; }
            public List<QntProcedimento> QntProcedList { get; set; }

        }

        public class QntProcedimento
        {
            public decimal valorTotal { get; set; }
            public int qntProced { get; set; }
            public int qntFinalizado { get; set; }
            public int qntNFinalizado { get; set; }
        }
    }
}
