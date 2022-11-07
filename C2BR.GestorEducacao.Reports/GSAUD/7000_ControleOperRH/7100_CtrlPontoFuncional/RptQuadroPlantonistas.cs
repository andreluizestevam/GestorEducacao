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

namespace C2BR.GestorEducacao.Reports.GSAUD._7000_ControleOperRH._7100_CtrlPontoFuncional
{
    public partial class RptQuadroPlantonistas : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptQuadroPlantonistas()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros, 
                              string infos, 
                              int coEmp, 
                              int CoUnid,
                              int coDepto,
                              int coEspec,
                              int coCol,
                              string situacao,
                              int classificacao,
                              string dataIni,
                              string dataFim,
                              string noPeriodo,
                              int coTipoContrato
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
                this.lblTitulo.Text = "QUADRO DE ESCALA PLANTONISTA " + "- " + noPeriodo;

                // Cria o header a partir do cod da instituicao
                ReportHeader header = C2BR.GestorEducacao.Reports.ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return -1;

                // Inicializa o headero
                base.BaseInit(header);

                #endregion

                var res = (from tb159 in TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
                           join tb153 in TB153_TIPO_PLANT.RetornaTodosRegistros() on tb159.ID_TIPO_PLANT equals tb153.ID_TIPO_PLANT
                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tb159.TB03_COLABOR.CO_COL equals tb03.CO_COL
                           join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb159.CO_EMP_AGEND_PLANT equals tb25.CO_EMP
                           join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tb159.CO_ESPEC_PLANT equals tb63.CO_ESPECIALIDADE
                           join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tb159.TB14_DEPTO.CO_DEPTO equals tb14.CO_DEPTO
                           where (CoUnid != 0 ? tb159.CO_EMP_AGEND_PLANT == CoUnid : 0 == 0)
                           && (coTipoContrato != 0 ? tb03.CO_TPCON == coTipoContrato : 0 == 0)
                           &&   (coDepto != 0 ? tb159.TB14_DEPTO.CO_DEPTO == coDepto : 0 == 0)
                           &&   (coEspec != 0 ? tb159.CO_ESPEC_PLANT == coEspec : 0 == 0)
                           &&   (coCol != 0 ? tb159.TB03_COLABOR.CO_COL == coCol : 0 == 0)
                           &&   (situacao != "0" ? tb159.CO_SITUA_AGEND == situacao : 0 == 0)
                           &&   ((tb159.DT_INICIO_PREV.Day >= dataIni1.Day) && (tb159.DT_INICIO_PREV.Month >= dataIni1.Month) && (tb159.DT_INICIO_PREV.Year >= dataIni1.Year)) 
                           &&   ((tb159.DT_INICIO_PREV.Day <= dataFim1.Day) && (tb159.DT_INICIO_PREV.Month <= dataFim1.Month) && (tb159.DT_INICIO_PREV.Year <= dataFim1.Year))

                           select new QuadropPlantonistas
                           {
                               //Dados do Colaborador
                               noCol = tb03.NO_COL,
                               matCol = tb03.CO_MAT_COL,
                               especialidade = tb63.NO_ESPECIALIDADE,
                               funCol = tb03.DE_FUNC_COL,

                               //Dados do Agendamento
                               unidade = tb25.sigla,
                               local = tb14.NO_DEPTO,
                               situacao = tb159.CO_SITUA_AGEND,
                               dtAgend = tb159.DT_INICIO_PREV,
                               RI = (tb159.FL_INCON_AGEND == "S" ? "*" : ""),

                               //Dados do tipo de plantão
                               sglTpPlantao = tb153.CO_SIGLA_TIPO_PLANT,
                               qtHorasTPPlan = tb153.QT_HORAS,
                               hrIniTPPlan = tb153.HR_INI_TIPO_PLANT,
                           }).ToList();

                //Orderna de acordo com o informado no parâmetro do relatório
                switch (classificacao)
                {
                    case 1:
                        res = res.OrderBy(w=>w.noCol).ThenBy(o=>o.dtAgend).ThenBy(w=>w.local).ThenBy(i=>i.sglTpPlantao).ThenBy(l=>l.especialidade).ToList();
                        break;
                    case 2:
                        res = res.OrderBy(w => w.unidade).ThenBy(i => i.especialidade).ThenBy(w=>w.dtAgend).ThenBy(y=>y.sglTpPlantao).ThenBy(e=>e.noCol).ToList();
                        break;
                    case 3:
                        res = res.OrderBy(o => o.unidade).ThenBy(w => w.local).ThenBy(i=>i.dtAgend).ThenBy(p=>p.sglTpPlantao).ThenBy(u=>u.noCol).ToList();
                        break;
                    case 4:
                        res = res.OrderBy(j => j.especialidade).ThenBy(p=>p.dtAgend).ThenBy(o=>o.local).ThenBy(v=>v.sglTpPlantao).ThenBy(o => o.noCol).ToList();
                        break;
                    default:
                        res = res.OrderBy(w => w.noCol).ToList();
                        break;
                }

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Adiciona ao DataSource do Relatório
                bsReport.Clear();

                foreach (QuadropPlantonistas at in res)
                {
                    bsReport.Add(at);
                }

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Class Extrato Plantão

        /// <summary>
        /// Método responsável por colocar primeira letra em maiúsculo
        /// </summary>
        /// <param name="palavra"></param>
        /// <returns></returns>
        public static string PrimeiraLetraMaiuscula(string palavra)
        {
            char primeira = char.ToUpper(palavra[0]);
            return primeira + palavra.Substring(1);
        }

        public class QuadropPlantonistas
        {
            //Dados do Colaborador
            public string noCol { get; set; }
            public string matCol { get; set; }
            public string funCol { get; set; }
            public string noColValid
            {
                get
                {
                    return this.matCol + " - " + this.noCol;
                }
            }

            //Dados do agendamento
            public DateTime dtAgend { get; set; }
            public string dtAgendValid
            {
                get
                {
                    return this.dtAgend.ToString("dd/MM/yy") + " - " + PrimeiraLetraMaiuscula(dtAgend.ToString("ddd", new CultureInfo("pt-BR")));
                }
            }
            public string unidade { get; set; }
            public string local { get; set; }
            public string especialidade { get; set; }
            public string situacao { get; set; }
            public string situacaoValid
            {
                get
                {
                    string s = "";

                    switch (this.situacao)
                    {
                        case "A":
                            s = "ABE";
                            break;
                        case "C":
                            s = "CAN";
                            break;
                        case "R":
                            s = "REA";
                            break;
                        case "P":
                            s = "PLA";
                            break;
                        case "S":
                            s = "SUS";
                            break;
                    }

                    return s;
                }
            }
            public string localPlantao
            {
                get
                {
                    return this.unidade + " / " + this.local;
                }
            }
            public string DiaSemana { get; set; }
            public string RI { get; set; }

            //Dados do tipo de Plantão
            public string sglTpPlantao { get; set; }
            public int qtHorasTPPlan { get; set; }
            public string hrIniTPPlan { get; set; }
            public string saida
            {
                get
                {
                    //Calcula as horas
                    DateTime dtAux = new DateTime(1999, 1, 1);
                    dtAux = dtAux.AddHours(int.Parse(this.hrIniTPPlan.Substring(0, 2)));
                    dtAux = dtAux.AddMinutes(int.Parse(this.hrIniTPPlan.Substring(3,2)));
                    dtAux = dtAux.AddHours(this.qtHorasTPPlan);

                    return this.hrIniTPPlan + " - " + dtAux.ToString("HH:mm") + " (CH " + this.qtHorasTPPlan.ToString().PadLeft(2,'0') + ")";
                    //return "CH: " + this.qtHorasTPPlan.ToString().PadLeft(2, '0') + "h - Início: " + this.hrIniTPPlan;
                }
            }
        }
        #endregion
    }
}
