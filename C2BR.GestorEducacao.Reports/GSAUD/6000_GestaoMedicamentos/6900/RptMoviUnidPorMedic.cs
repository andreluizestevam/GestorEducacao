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

namespace C2BR.GestorEducacao.Reports.GSAUD._6000_GestaoMedicamentos._6900
{
    public partial class RptMoviUnidPorMedic : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptMoviUnidPorMedic()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                                string infos,
                                int grpItem,
                                int subGrpItem,
                                int coEmp,
                                int Item,
                                int Unid,
                                int regiao,
                                int area,
                                int subarea,
                                string dataIni,
                                string dataFim,
                                string tipoRelatorio
                                )
        {

            try
           {
                #region Setar o Header e as Labels

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

                #region Query

                if (tipoRelatorio == "U")
                {

                    var lst = (from tb94 in TB094_ITEM_RESER_MEDIC.RetornaTodosRegistros()

                               join tb90 in TB90_PRODUTO.RetornaTodosRegistros() on tb94.TB90_PRODUTO.CO_PROD equals tb90.CO_PROD
                               join tb260 in TB260_GRUPO.RetornaTodosRegistros() on tb90.TB260_GRUPO.ID_GRUPO equals tb260.ID_GRUPO
                               join tb261 in TB261_SUBGRUPO.RetornaTodosRegistros() on tb90.TB261_SUBGRUPO.ID_SUBGRUPO equals tb261.ID_SUBGRUPO
                               join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb94.CO_ALU equals tb07.CO_ALU
                               join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb94.CO_EMP equals tb25.CO_EMP
                               join tb109 in TB109_DETAL_ENTRE.RetornaTodosRegistros() on tb25.CO_EMP equals tb109.CO_EMP
                               join tb91 in TB91_MOV_PRODUTO.RetornaTodosRegistros() on tb90.CO_PROD equals tb91.CO_PROD
                               join tb93 in TB93_TIPO_MOVIMENTO.RetornaTodosRegistros() on tb91.TB93_TIPO_MOVIMENTO.CO_TIPO_MOV equals tb93.CO_TIPO_MOV

                               where
                                     (grpItem != 0 ? tb90.TB260_GRUPO.ID_GRUPO == grpItem : 0 == 0)
                               &&
                                     (subGrpItem != 0 ? tb261.ID_SUBGRUPO == subGrpItem : 0 == 0)
                               &&
                                     (Item != 0 ? tb90.CO_PROD == Item : 0 == 0)
                               &&
                                     (Unid != 0 ? tb25.CO_EMP == Unid : 0 == 0)
                               &&
                                     (regiao != 0 ? tb07.TB906_REGIAO.ID_REGIAO == regiao : 0 == 0)
                               &&
                                     (area != 0 ? tb07.TB907_AREA.ID_AREA == area : 0 == 0)
                               &&
                                     (regiao != 0 ? tb07.TB906_REGIAO.ID_REGIAO == regiao : 0 == 0)
                               &&
                                     (area != 0 ? tb07.TB907_AREA.ID_AREA == area : 0 == 0)
                               &&
                                     (subarea != 0 ? tb07.TB908_SUBAREA.ID_SUBAREA == subarea : 0 == 0)
                               &&
                                     ((tb109.DT_ENTRE >= dataIni1) && (tb109.DT_ENTRE <= dataFim1))


                               select new MovimentacaoPorMedic
                               {
                                   Grupo = tb90.NO_PROD,
                                   Item = tb25.NO_FANTAS_EMP,
                                   dt_Entrega = tb109.DT_ENTRE,
                                   coItem = tb90.CO_PROD,

                                   //Trata as entradas e saídas de um determinado medicamento
                                   status = tb91.TB93_TIPO_MOVIMENTO.FLA_TP_MOV,
                                   QtdEntrada = tb91.QT_MOV_PROD,
                                   QtdSaida = tb91.QT_MOV_PROD,
                                   QtdEntregue = tb109.QT_ENTREGA,
                                   TotEntrg = tb109.QT_ENTREGA
                                   
                                          }).OrderBy(p => p.Grupo).ThenBy(p => p.Item);

                             var res = lst.ToList();

                    //foreach (MovimentacaoPorMedic mpm in res)
                    //{
                    //    mpm.QtdEntrada = TB91_MOV_PRODUTO.RetornaTodosRegistros().Where(w => w.TB93_TIPO_MOVIMENTO.FLA_TP_MOV == "E" && w.CO_PROD == mpm.coItem).Sum(s => s.QT_MOV_PROD);
                    //    mpm.QtdSaida = TB91_MOV_PRODUTO.RetornaTodosRegistros().Where(y => y.TB93_TIPO_MOVIMENTO.FLA_TP_MOV == "S" && y.CO_PROD == mpm.coItem).Sum(ss => ss.QT_MOV_PROD);
                    //    mpm.QtdEntregue = TB109_DETAL_ENTRE.RetornaTodosRegistros().Where(z => z.TB094_ITEM_RESER_MEDIC.TB90_PRODUTO.CO_PROD == mpm.coItem).Sum(s => s.QT_ENTREGA);
                    //}

                    decimal? iE = res.Where(w => w.status == "E").Sum(s => s.QtdEntrada);
                    decimal? iS = res.Where(y => y.status == "S").Sum(r => r.QtdSaida);

                     if (res.Count == 0)
                        return -1;

                    // Seta os alunos no DataSource do Relatorio
                    bsReport.Clear();

                    //int countE = 0;
                    //int countS = 0;
                    //decimal countEntg = 0;
                    //string auxiItem = "";
                    foreach (MovimentacaoPorMedic at in res)
                    {
                        bsReport.Add(at);
                        at.QtdEntrada = iE;
                        at.QtdSaida = iS;
                        //if (auxiItem != at.Item)
                        //{
                        //    at.QtdEntrada = countE;
                        //    at.QtdSaida = countS;
                        //    at.QtdEntregue = countEntg;

                        //    auxiItem = at.Item;

                        //    countE = 0;
                        //    countS = 0;
                        //    countEntg = 0;

                        //    countEntg = countEntg + at.TotEntrg;
                        //    if (at.status == "E") { countE++; }
                        //    if (at.status == "S") { countS++; }
                        //}
                        //else
                        //{
                        //    countEntg = countEntg + at.TotEntrg;
                        //    if (at.status == "E") { countE++; }
                        //    if (at.status == "S") { countS++; }
                        //}
                    }

                    return 1;
                }


                else
                {
                    var lst = (from tb94 in TB094_ITEM_RESER_MEDIC.RetornaTodosRegistros()

                               join tb90 in TB90_PRODUTO.RetornaTodosRegistros() on tb94.TB90_PRODUTO.CO_PROD equals tb90.CO_PROD
                               join tb260 in TB260_GRUPO.RetornaTodosRegistros() on tb90.TB260_GRUPO.ID_GRUPO equals tb260.ID_GRUPO
                               join tb261 in TB261_SUBGRUPO.RetornaTodosRegistros() on tb90.TB261_SUBGRUPO.ID_SUBGRUPO equals tb261.ID_SUBGRUPO
                               join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb94.CO_ALU equals tb07.CO_ALU
                               join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb94.CO_EMP equals tb25.CO_EMP
                               join tb109 in TB109_DETAL_ENTRE.RetornaTodosRegistros() on tb94.CO_EMP equals tb109.CO_EMP
                               //join tb91 in TB91_MOV_PRODUTO.RetornaTodosRegistros() on tb90.CO_PROD equals tb91.CO_PROD
                               //join tb93 in TB93_TIPO_MOVIMENTO.RetornaTodosRegistros() on tb91.TB93_TIPO_MOVIMENTO.CO_TIPO_MOV equals tb93.CO_TIPO_MOV

                               where
                                     (grpItem != 0 ? tb260.ID_GRUPO == grpItem : 0 == 0)
                               &&
                                     (subGrpItem != 0 ? tb261.ID_SUBGRUPO == subGrpItem : 0 == 0)
                               &&
                                     (Item != 0 ? tb90.CO_PROD == Item : 0 == 0)
                               &&
                                     (Unid != 0 ? tb25.CO_EMP == Unid : 0 == 0)
                               &&
                                     (regiao != 0 ? tb07.TB906_REGIAO.ID_REGIAO == regiao : 0 == 0)
                               &&
                                     (area != 0 ? tb07.TB907_AREA.ID_AREA == area : 0 == 0)
                               &&
                                     (regiao != 0 ? tb07.TB906_REGIAO.ID_REGIAO == regiao : 0 == 0)
                               &&
                                     (area != 0 ? tb07.TB907_AREA.ID_AREA == area : 0 == 0)
                               &&
                                     (subarea != 0 ? tb07.TB908_SUBAREA.ID_SUBAREA == subarea : 0 == 0)
                               &&
                                     ((tb109.DT_ENTRE >= dataIni1) && (tb109.DT_ENTRE <= dataFim1))


                               select new MovimentacaoPorMedic
                               {
                                   Grupo = tb25.NO_FANTAS_EMP,
                                   Item = tb90.NO_PROD,
                                   dt_Entrega = tb109.DT_ENTRE,
                                   coItem = tb94.CO_EMP
                               }).OrderBy(p => p.Grupo).ThenBy(p => p.Item);

                    var res = lst.ToList();

                    foreach (MovimentacaoPorMedic mpm in res)
                    {
                        mpm.QtdEntrada = TB91_MOV_PRODUTO.RetornaTodosRegistros().Where(w => w.TB93_TIPO_MOVIMENTO.FLA_TP_MOV == "E" && w.CO_EMP == mpm.coItem).Sum(s => s.QT_MOV_PROD);
                        mpm.QtdSaida = TB91_MOV_PRODUTO.RetornaTodosRegistros().Where(y => y.TB93_TIPO_MOVIMENTO.FLA_TP_MOV == "S" && y.CO_EMP == mpm.coItem).Sum(ss => ss.QT_MOV_PROD);
                        mpm.QtdEntregue = TB109_DETAL_ENTRE.RetornaTodosRegistros().Where(z => z.CO_EMP == mpm.coItem).Sum(s => s.QT_ENTREGA);
                    }

                #endregion

                    // Erro: não encontrou registros
                    if (res.Count == 0)
                        return -1;

                    // Seta os alunos no DataSource do Relatorio
                    bsReport.Clear();
                    foreach (MovimentacaoPorMedic at in res)
                        bsReport.Add(at);



                    return 1;
                }
                
            }

            catch { return 0; }
        }

        #endregion
    

    public class MovimentacaoPorMedic
    {
        public string Grupo { get; set; }

        public string Item { get; set; }
        public int coItem { get; set; }
        public decimal? QtdEntrada { get; set; }
        public decimal? QtdSaida { get; set; }
        public decimal? QtdEntregue { get; set; }
        public string status { get; set; }
        public decimal TotEntrg { get; set; }
        public DateTime dt_Entrega { get; set; }
        public string data
        {
            get
            {
                return this.dt_Entrega.ToString("dd/MM/yyyy");
            }
        }
    }
}
}