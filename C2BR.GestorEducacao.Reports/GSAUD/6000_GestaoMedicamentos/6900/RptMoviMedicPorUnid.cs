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
    public partial class RptMoviMedicPorUnid : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptMoviMedicPorUnid()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                                string infos,
                                int unidSolic,
                                int coEmp,
                                int medic,
                                int regiao,
                                int area,
                                int subarea,
                                string dataIni,
                                string dataFim                                       
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
                lblParametros.Text = parametros;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;
            #region Query

            var lst = (from tb92 in TB092_RESER_MEDIC.RetornaTodosRegistros()

                       join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb92.CO_EMP equals tb25.CO_EMP
                       join tb94 in TB094_ITEM_RESER_MEDIC.RetornaTodosRegistros() on tb92.ID_RESER_MEDIC equals tb94.TB092_RESER_MEDIC.ID_RESER_MEDIC
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb92.CO_ALU equals tb07.CO_ALU
                       join tb109 in TB109_DETAL_ENTRE.RetornaTodosRegistros() on tb94.ID_ITEM_RESER_MEDIC equals tb109.TB094_ITEM_RESER_MEDIC.ID_ITEM_RESER_MEDIC
                       join tb90 in TB90_PRODUTO.RetornaTodosRegistros() on tb94.TB90_PRODUTO.CO_PROD equals tb90.CO_PROD

                       where (regiao != 0 ? tb07.TB906_REGIAO.ID_REGIAO == regiao : 0 == 0)
                       &&
                             (area != 0 ? tb07.TB907_AREA.ID_AREA == area : 0 == 0)
                       &&
                             (subarea != 0 ? tb07.TB908_SUBAREA.ID_SUBAREA == subarea : 0 == 0)
                       &&
                             (unidSolic != 0 ? tb25.CO_EMP == unidSolic : 0 == 0)
                       &&
                             (medic != 0 ? tb90.CO_PROD == medic : 0 == 0)
                       &&
                             ((tb92.DT_RESER_MEDIC >= dataIni1 ) && (tb92.DT_RESER_MEDIC <= dataFim1))
                     

                       select new MovimentacaoPorUnidade
                       {
                           coSoli = tb92.CO_RESER_MEDIC,
                           medic = tb90.NO_PROD,
                           unid = tb25.NO_FANTAS_EMP,
                           qte = tb109.QT_ENTREGA,
                           usuario = tb07.NO_ALU,
                           dt_solic = tb92.DT_RESER_MEDIC
                       }).OrderBy(p => p.dt_solic).ThenBy(p => p.usuario);

            var res = lst.ToList();

            #endregion

        
                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (MovimentacaoPorUnidade at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }
        
        #endregion


        public class MovimentacaoPorUnidade
        {
            public string coSoli { get; set; }
            public string medic { get; set; }
            public string unid { get; set; }
            public decimal qte { get; set; }
            public string usuario { get; set; }
            public DateTime dt_solic {get; set;}
            public string data {
                get {
                    return this.dt_solic.ToString("dd/MM/yyyy");
                }
            }
            public string hora {
                get { 
                    return this.dt_solic.ToString("hh:mm:ss");
                }
            }
            public string nroSolic 
            {
                get
                {
                    return Convert.ToInt64(this.coSoli).ToString(@"0000\.000\.0000000");
                }
            }
        }

    }
}