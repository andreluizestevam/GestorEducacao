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
    public partial class RptOcorAdministrativas : C2BR.GestorEducacao.Reports.RptRetrato
    {
        #region ctor

        public RptOcorAdministrativas()
        {
            InitializeComponent();
        } 

        #endregion

        #region Init Report

        public int InitReport(int codEmp, int codFun, string tipoOcor, string infos)
        {
            try
            {
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

                var res = from o in ctx.TB151_OCORR_COLABOR
                          where o.TB25_EMPRESA.CO_EMP == codEmp
                          && (tipoOcor != "T" ? o.TB150_TIPO_OCORR.CO_SIGL_OCORR == tipoOcor : tipoOcor == "T")
                          && (codFun != 0 ? o.TB03_COLABOR.CO_COL == codFun : codFun == 0)
                          select new Ocorrencia
                          {
                              Colaborador = o.TB03_COLABOR.NO_COL,
                              Data = o.DT_OCORR,
                              Descricao = o.DE_OCORR,
                              Tipo = o.TB150_TIPO_OCORR.DE_TIPO_OCORR
                          };

                if (res.Count() == 0)
                    return -1;

                #endregion

                // Adiciona o Funcionario ao DataSource do Relatório
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
            public string Tipo { get; set; }
            public string Colaborador { get; set; }
            public string Descricao { get; set; }
            public DateTime Data { get; set; }
        }

        #endregion
    }
}
