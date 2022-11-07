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

namespace C2BR.GestorEducacao.Reports.GSAUD._7000_ControleOperRH._7900_Plantoes
{
    public partial class RptExtratoFuncResum : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptExtratoFuncResum()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(
                            string parametros,
                            string infos,
                            int coEmp,
                            int regiao,
                            int area,
                            int subArea
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

                var res = (from tb159 in TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tb159.TB03_COLABOR.CO_COL equals tb03.CO_COL
                           join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb159.CO_EMP_AGEND_PLANT equals tb25.CO_EMP
                           join tb14P in TB14_DEPTO.RetornaTodosRegistros() on tb159.TB14_DEPTO.CO_DEPTO equals tb14P.CO_DEPTO
                           join tb14C in TB14_DEPTO.RetornaTodosRegistros() on tb03.CO_DEPTO equals tb14C.CO_DEPTO
                           join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tb03.CO_ESPEC equals tb63.CO_ESPEC into l1
                           from ls in l1.DefaultIfEmpty()

                           select new ExtratoFuncionalPlantoesResumido
                           {
                               //Coleta Informações do Aluno
                               unidade = tb25.NO_FANTAS_EMP,
                               Medico = tb03.NO_COL,
                               DepartMedic = tb14C.NO_DEPTO,
                               cpfMedic = tb03.NU_CPF_COL,
                               Espec = ls.NO_ESPECIALIDADE,
                               sexo = tb03.CO_SEXO_COL,
                               DepartPlant = tb14P.NO_DEPTO,
                               Situacao = tb159.CO_SITUA_AGEND,
                               datainiPrev = tb159.DT_INICIO_PREV,
                               datainiReal = tb159.DT_INICIO_REAL,

                           }).OrderBy(m => m.unidade).ToList();

                #endregion


                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (ExtratoFuncionalPlantoesResumido at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        public class ExtratoFuncionalPlantoesResumido
        {

            public string unidade { get; set; }
            public string Medico { get; set; }
            public string Espec { get; set; }
            public string cpfMedic { get; set; }
            public string Situacao { get; set; }
            public string SituacaoValid
            {
                get
                {
                    string s = "";
                    switch (this.Situacao)
                    {
                        case "A":
                            s = "Em Aberto";
                            break;

                        case "C":
                            s = "Cancelado";
                            break;

                        case "R":
                            s = "Realizada";
                            break;
                    }
                    return s;
                }
            }
            public string MedicCPFValid
            {
                get
                {
                    if ((this.cpfMedic == null) || (this.cpfMedic == ""))
                    {
                        return "***.***.***-**";
                    }

                    return this.cpfMedic.Insert(3, ".").Insert(7, ".").Insert(11, "-");
                }
            }

            public DateTime datainiPrev { get; set; }

            public DateTime? datainiReal { get; set; }

            public bool dataValidShow
            {
                get
                {
                    if (this.datainiReal.HasValue)
                        return true;

                    else
                        return false;
                }
            }
            public string DataShow
            {
                get
                {
                    if (this.dataValidShow == true)
                        return this.datainiReal.Value.ToString("dd/MM/yy");

                    else
                        return this.datainiPrev.ToString("dd/MM/yy");
                }
            }
            public string horaShow
            {
                get
                {
                    if (this.dataValidShow == true)
                        return this.datainiReal.Value.ToString("hh:mm");

                    else
                        return this.datainiPrev.ToString("dd/MM/yy");
                }
            }


            public string sexo { get; set; }
            public string DepartMedic { get; set; }
            public string DepartPlant { get; set; }
        }
    }
}
