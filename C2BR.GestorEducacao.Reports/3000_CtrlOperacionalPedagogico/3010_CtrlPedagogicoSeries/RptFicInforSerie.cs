using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Text.RegularExpressions;
using C2BR.GestorEducacao.Reports.Helper;

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3010_CtrlPedagogicoSeries
{
    public partial class RptFicInforSerie : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptFicInforSerie()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                               int codEmp,
                               int codEmpRef,
                               int codMod,
                               int codCurso,
                               string strP_CO_NIVEL_CUR,
                               string strP_DES_NIVEL_CUR,
                               int strP_CO_DPTO_CUR,
                               int strP_CO_SUB_DPTO_CUR,
                               string strP_CO_SITU,
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

                #region Query Alunos/Serie

                var lst = from tb01 in ctx.TB01_CURSO
                          join tb77 in ctx.TB77_DPTO_CURSO on tb01.CO_DPTO_CUR equals tb77.CO_DPTO_CUR
                          join tb68 in ctx.TB68_COORD_CURSO on tb01.CO_SUB_DPTO_CUR equals tb68.CO_COOR_CUR
                          join tb03 in ctx.TB03_COLABOR on tb01.CO_COOR equals tb03.CO_COL into di
                          from x in di.DefaultIfEmpty()
                          where
                           tb01.CO_EMP == codEmpRef
                           && (codCurso != 0 ? tb01.CO_CUR == codCurso : 0 == 0)
                           && (codMod != 0 ? tb01.TB44_MODULO.CO_MODU_CUR == codMod : 0 == 0)
                           && (strP_CO_DPTO_CUR != 0 ? tb01.CO_DPTO_CUR == strP_CO_DPTO_CUR : 0 == 0)
                           && tb01.CO_SUB_DPTO_CUR == strP_CO_SUB_DPTO_CUR
                           && tb01.CO_NIVEL_CUR == strP_CO_NIVEL_CUR
                          select new InforSerie
                          {
                              Modalidade = tb01.TB44_MODULO.DE_MODU_CUR,
                              Departamento = tb77.NO_DPTO_CUR,
                              Coordenacao = tb68.NO_COOR_CUR,
                              CargaHoraria = tb01.QT_CARG_HORA_CUR,
                              PercCertif = tb01.PE_CERT_CUR,
                              Coordenador = x!= null ? x.NO_COL : "*****",
                              ObjetivoCur = tb01.TB19_INFOR_CURSO.DE_OBJE_CUR,
                              PublicAlvoCur = tb01.TB19_INFOR_CURSO.DE_PUBL_ALV_CUR,
                              PreRequisCur = tb01.TB19_INFOR_CURSO.DE_PRE_REQU_CUR,
                              MetodCur = tb01.TB19_INFOR_CURSO.DE_METO_CUR,
                              ProgramCur = tb01.TB19_INFOR_CURSO.DE_PROG_CUR,
                              MaterFornecCur = tb01.TB19_INFOR_CURSO.DE_MATE_FORN_CUR,
                              CertifCur = tb01.TB19_INFOR_CURSO.DE_CERT_CUR,
                              NivelCur = strP_DES_NIVEL_CUR,
                              Situacao = tb01.CO_SITU == "A" ? "Ativa" : tb01.CO_SITU == "C" ? "Cancelada" : "Suspenso"
                          };

                var res = lst.ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (InforSerie at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Informação Série do Relatorio

        public class InforSerie
        {
            public string Modalidade { get; set; }
            public string Departamento { get; set; }
            public string Coordenacao { get; set; }
            public string Coordenador { get; set; }
            public decimal? CargaHoraria { get; set; }
            public decimal? PercCertif { get; set; }
            public string Situacao { get; set; }
            public string NivelCur { get; set; }
            public string ObjetivoCur { get; set; }
            public string PublicAlvoCur { get; set; }
            public string PreRequisCur { get; set; }
            public string MetodCur { get; set; }
            public string ProgramCur { get; set; }
            public string MaterFornecCur { get; set; }
            public string CertifCur { get; set; }
        }

        #endregion
    }
}
