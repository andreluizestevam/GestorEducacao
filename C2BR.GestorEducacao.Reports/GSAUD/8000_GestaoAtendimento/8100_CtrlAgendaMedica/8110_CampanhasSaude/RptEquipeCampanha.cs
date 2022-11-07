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

namespace C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8110_CampanhasSaude
{
    public partial class RptEquipeCampanha : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptEquipeCampanha()
        {
            InitializeComponent();
        }
        #region Init Report

        public int InitReport(string parametros,
                              string infos,
                              int coEmp,
                              int coUnid,
                              string coTipo,
                              string dataIni,
                              string dataFim
            )
        {
            try
            {
                #region Inicializa o header/Labels
                
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

                // Seta as informaçoes do rodape do relatorio
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Cria o header a partir do cod da instituicao
                var header = ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return -1;

                // Inicializa o headero
                base.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                var res = (from tbs339 in TBS339_CAMPSAUDE.RetornaTodosRegistros()
                           join tbs340 in TBS340_CAMPSAUDE_COLABOR.RetornaTodosRegistros() on tbs339.ID_CAMPAN equals tbs340.TBS339_CAMPSAUDE.ID_CAMPAN
                           where (coUnid != 0 ? tbs339.CO_EMP_LOCAL_CAMPAN == coUnid : 0 == 0)
                           && (coTipo != "0" ? tbs339.CO_TIPO_CAMPAN == coTipo : 0 == 0)
                           select new EquipeCampanha
                           {
                               ID_CAMPAN = tbs339.ID_CAMPAN,
                               NO_CAMPAN = tbs339.NM_CAMPAN,
                               CO_TIPO = tbs339.CO_TIPO_CAMPAN,
                               DT_CAMPAN = tbs339.DT_INICI_CAMPAN,
                               NO_COLAB = tbs340.NM_COLABO_CAMPAN,
                               FUN_COLAB = tbs340.NM_FUNCA_COLABO_CAMPAN,
                               VL_DIARIO = tbs340.VL_DIARI_COLABO_CAMPAN ?? 0,
                               VL_FINAL = tbs340.VL_FINAL_COLABO_CAMPAN ?? 0,
                               dataInicioCamp = tbs339.DT_INICI_CAMPAN,
                               dataTerminCamp = tbs339.DT_TERMI_CAMPAN,
                           }).OrderBy(w=>w.NO_CAMPAN).OrderBy(y=>y.NO_COLAB).ToList();

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Adiciona ao DataSource do Relatório
                bsReport.Clear();

                //Variáveis de Auxílio
                int coCamp = 0;
                decimal AUXTotCampanDiario = 0;
                decimal AUXTotCampanFinal = 0;

                decimal AUXTotGeralDiario = 0;
                decimal AUXTotGeralFinal = 0; 

                foreach (EquipeCampanha at in res)
                {
                    if (coCamp != at.ID_CAMPAN)
                    {
                        coCamp = at.ID_CAMPAN;
                        AUXTotCampanDiario = AUXTotCampanFinal = 0;
                    }

                    //Calcula os valores totais
                    AUXTotCampanDiario += (at.VL_DIARIO.HasValue ? at.VL_DIARIO.Value : 0);
                    AUXTotCampanFinal += (at.VL_FINAL.HasValue ? at.VL_FINAL.Value : 0);

                    AUXTotGeralDiario += (at.VL_DIARIO.HasValue ? at.VL_DIARIO.Value : 0);
                    AUXTotGeralFinal += (at.VL_FINAL.HasValue ? at.VL_FINAL.Value : 0);

                    at.TOTCampan_VL_DIARIO = AUXTotCampanDiario.ToString("N2");
                    at.TOTCampan_VL_FINAL = AUXTotCampanFinal.ToString("N2");

                    decimal m = AUXTotCampanDiario + AUXTotCampanFinal;
                    at.TOTCampan_TOT = m.ToString("N2");


                    at.TOTGeral_VL_DIARIO = AUXTotGeralDiario.ToString("N2");
                    at.TOTGeral_VL_FINAL = AUXTotGeralFinal.ToString("N2");

                    decimal m2 = AUXTotGeralDiario + AUXTotGeralFinal;
                    at.TOTGeral_TOT = m2.ToString("N2");


                    //Calcula o valor total
                    decimal vlDia = (at.VL_DIARIO.HasValue ? at.VL_DIARIO.Value : 0);
                    decimal vlFinal = (at.VL_FINAL.HasValue ? at.VL_FINAL.Value : 0);
                    TimeSpan ts = at.dataTerminCamp.Subtract(at.dataInicioCamp);
                    int qtDiasTotal = int.Parse(ts.TotalDays.ToString());
                    decimal vlTot = (vlDia * qtDiasTotal) + vlFinal;
                    at.VL_TOTAL = vlTot.ToString("N2");

                    bsReport.Add(at);
                }

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        public class EquipeCampanha
        {
            public int ID_CAMPAN { get; set; }
            public string NO_CAMPAN { get; set; }
            public string CO_TIPO { get; set; }
            public string tipo_Valid
            {
                get
                {
                    string tipo = "";
                    switch (this.CO_TIPO)
                    {
                        case "V":
                            tipo = "Vacinação";
                            break;
                        case "A":
                            tipo = "Ações";
                            break;
                        case "P":
                            tipo = "Programas";
                            break;
                    }

                    return tipo;
                }
            }
            public DateTime DT_CAMPAN { get; set; }
            public string NO_CONCAT
            {
                get
                {
                    return this.tipo_Valid + " - " + this.NO_CAMPAN + " - " + this.DT_CAMPAN.ToString("dd/MM/yy");
                }
            }
            public string NO_COLAB { get; set; }
            public string FUN_COLAB { get; set; }
            public decimal? VL_DIARIO { get; set; }
            public decimal? VL_FINAL { get; set; }
            public string VL_TOTAL { get; set; }
            public DateTime dataInicioCamp { get; set; }
            public DateTime dataTerminCamp { get; set; }

            //Totais
            public string TOTCampan_VL_DIARIO { get; set; }
            public string TOTCampan_VL_FINAL { get; set; }
            public string TOTCampan_TOT { get; set; }

            public string TOTGeral_VL_DIARIO { get; set; }
            public string TOTGeral_VL_FINAL { get; set; }
            public string TOTGeral_TOT { get; set; }
        }
    }
}

