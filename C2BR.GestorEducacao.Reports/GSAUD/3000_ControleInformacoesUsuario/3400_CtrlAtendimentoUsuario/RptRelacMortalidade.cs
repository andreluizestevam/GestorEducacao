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
namespace C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3400_CtrlAtendimentoUsuario
{
    public partial class RptRelacMortalidade : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptRelacMortalidade()
        {
            InitializeComponent();
        }
        #region Init Report

        public int InitReport(string parametros,
                              string infos,
                              int coEmp,
                              int coUnid
            )
        {
            try
            {
                #region Inicializa o header/Labels

                // Seta as informaçoes do rodape do relatorio
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Cria o header a partir do cod da instituicao
                var header = C2BR.GestorEducacao.Reports.ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return -1;

                // Inicializa o headero
                base.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                var res = (from tbs343 in TBS343_MORTALIDADE.RetornaTodosRegistros()
                           select new EquipeCampanha
                           {
                              INTERcAPIT = tbs343.NM_CAPIT_MORTAL,
                              Menor1 = tbs343.QT_FAIXA_MORTAL1 ?? 0,
                              um4 = tbs343.QT_FAIXA_MORTAL1_4 ?? 0,
                              a5a9 = tbs343.QT_FAIXA_MORTAL5_9 ?? 0,
                              a10a14 = tbs343.QT_FAIXA_MORTAL10_14 ??0,
                              a15a19 = tbs343.QT_FAIXA_MORTAL15_19 ?? 0,
                              a20a29 = tbs343.QT_FAIXA_MORTAL20_29 ?? 0,
                              a30a39 = tbs343.QT_FAIXA_MORTAL30_39 ?? 0,
                              a40a49 = tbs343.QT_FAIXA_MORTAL40_49 ?? 0,
                              a50a59 = tbs343.QT_FAIXA_MORTAL50_59 ?? 0,
                              a60a69 = tbs343.QT_FAIXA_MORTAL60_69 ?? 0,
                              a70a79 = tbs343.QT_FAIXA_MORTAL70_79 ?? 0,
                              a80 = tbs343.QT_FAIXA_MORTAL80 ?? 0,

                           }).OrderBy(w => w.INTERcAPIT).ToList();

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Adiciona ao DataSource do Relatório
                bsReport.Clear();

                foreach (EquipeCampanha at in res)
                {
                    at.Total = at.Um + at.um4 + at.a5a9 + at.a10a14 + at.a15a19 +
                        at.a20a29 + at.a30a39 + at.a40a49 + at.a50a59 + at.a60a69 + at.a70a79 + at.a80;
                    bsReport.Add(at);
                }

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        public class EquipeCampanha
        {
            public string INTERcAPIT { get; set; }
            public int Menor1 { get; set; }
            public int Um { get; set; }
            public int um4 { get; set; }
            public int a5a9 { get; set; }
            public int a10a14 { get; set; }
            public int a15a19 { get; set; }
            public int a20a29 { get; set; }
            public int a30a39 { get; set; }
            public int a40a49 { get; set; }
            public int a50a59 { get; set; }
            public int a60a69 { get; set; }
            public int a70a79 { get; set; }
            public int a80 { get; set; }
            public int Total { get; set; }
        }
    }
}

