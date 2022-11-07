using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.Reports._1000_CtrlAdminEscolar._1200_GestaoOperColaboradores
{
    public partial class RptOcorSaude : C2BR.GestorEducacao.Reports.RptRetrato
    {
        #region ctor

        public RptOcorSaude()
        {
            InitializeComponent();
        }

        #endregion

        #region Init Report

        public int InitReport(int codEmp, int codFun, string dtInicio, string dtFim, string infos)
        {
            try
            {
                #region Valida Datas

                DateTime dataFim;
                DateTime dataInicio;

                if (!DateTime.TryParse(dtInicio, out dataInicio))
                    return 0;

                if (!DateTime.TryParse(dtFim, out dataFim))
                    return 0;

                #endregion

                #region Inicializa o header/Labels

                // Seta as informaçoes do rodape do relatorio
                this.InfosRodape = infos;

                // Cria o header a partir do cod da instituicao
                var header = ReportHeader.GetHeaderFromEmpresa(codEmp);
                if (header == null)
                    return -1;

                // Inicializa o header
                base.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Ocorrencias

                var res = (from o in ctx.TB143_ATEST_MEDIC
                           join c in ctx.TB03_COLABOR on o.CO_USU equals c.CO_COL
                           where o.TB25_EMPRESA.CO_EMP == codEmp
                           && (codFun != 0 ? o.CO_USU == codFun : codFun == 0)
                           && o.DT_CONSU >= dataInicio && o.DT_CONSU <= dataFim
                           select new Ocorrencia
                           {
                               Colaborador = c.NO_COL,
                               Doenca = o.TB117_CODIGO_INTERNACIONAL_DOENCA.NO_CID,
                               DtConsulta = o.DT_CONSU,
                               DtEntrega = o.DT_ENTRE_ATEST,
                               Medico = o.NO_MEDIC,
                               QtdDias = o.QT_DIAS_LICEN
                           }).OrderBy(p => p.DtConsulta);

                if (res.Count() == 0)
                    return -1;

                #endregion

                // Adiciona as Ocorrencias ao DataSource do Relatório
                bsReport.Clear();

                foreach (var o in res)
                    bsReport.Add(o);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Class Ocorrencias

        public class Ocorrencia
        {
            public string Doenca { get; set; }
            public string Colaborador { get; set; }
            public string Medico { get; set; }
            public DateTime DtConsulta { get; set; }
            public DateTime DtEntrega { get; set; }
            public int? QtdDias { get; set; }
            public string QtdDiasStr
            {
                get { return (QtdDias.HasValue) ? QtdDias.Value.ToString("n0") : "-"; }
            }
        }

        #endregion
    }
}
