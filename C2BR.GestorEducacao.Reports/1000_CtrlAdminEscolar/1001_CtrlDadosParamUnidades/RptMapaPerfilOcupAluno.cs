using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.Reports._1000_CtrlAdminEscolar._1001_CtrlDadosParamUnidades
{
    public partial class RptMapaPerfilOcupAluno : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptMapaPerfilOcupAluno()
        {
            InitializeComponent();
        }


        public int InitReport(string parametros,
                                int codEmp,
                                string strP_CO_EMP_REF,
                                string strP_ANO,
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

                int intP_CO_EMP_REF = strP_CO_EMP_REF != "T" ? int.Parse(strP_CO_EMP_REF) : 0;
                decimal intP_ANO = strP_ANO != "T" ? decimal.Parse(strP_ANO) : 0;

                var res = (from tb246 in ctx.TB246_UNIDADE_PERFIL_VAGAS
                           where strP_CO_EMP_REF != "T" ? tb246.TB25_EMPRESA.CO_EMP == intP_CO_EMP_REF : 0 == 0
                           && strP_ANO != "T" ? tb246.CO_ANO_PERFIL_VAGAS == intP_ANO : 0 == 0
                           orderby tb246.TB25_EMPRESA.sigla, tb246.CO_ANO_PERFIL_VAGAS
                           select new MapaPefilOcupAluno
                           {
                               Unidade = tb246.TB25_EMPRESA.sigla,
                               Ano = tb246.CO_ANO_PERFIL_VAGAS,
                               qvp = tb246.QT_VAGAS_PLANEJ_PERFIL,
                               qrv = tb246.QT_VAGAS_RESERV_PERFIL,
                               qmn = tb246.QT_VAGAS_MATRIC_PERFIL,
                               qmr = tb246.QT_VAGAS_RENOVA_PERFIL,
                               qaa = tb246.QT_VAGAS_ATIVOS_PERFIL,
                               qat = tb246.QT_VAGAS_TRANSF_PERFIL,
                               qac = tb246.QT_VAGAS_CANCEL_PERFIL,
                               qev = tb246.QT_VAGAS_EVADID_PERFIL,
                               qex = tb246.QT_VAGAS_EXPULS_PERFIL
                           }).ToList();


                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (MapaPefilOcupAluno at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }


        public class MapaPefilOcupAluno
        {
            public string Unidade { get; set; }
            public decimal Ano { get; set; }
            public decimal? qvp { get; set; }
            public decimal? qrv { get; set; }
            public decimal? qmn { get; set; }
            public decimal? qmr { get; set; }
            public decimal? qaa { get; set; }
            public decimal? qat { get; set; }
            public decimal? qac { get; set; }
            public decimal? qev { get; set; }
            public decimal? qex { get; set; }
        }
    }
}
