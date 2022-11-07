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

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar
{
    public partial class RptPautaChamadaSerieTurmaVerso : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptPautaChamadaSerieTurmaVerso()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              int codInst,
                              int strP_CO_EMP,
                              string strP_PROF_RESP,
                              string infos,
                              int mes)
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(strP_CO_EMP);

                if (header == null)
                    return 0;

                this.lblUnidadeTitulo.Text = header.Unidade;
                this.lblProfessor.Text = strP_PROF_RESP;
                this.VisiblePageHeader = false;
                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                lblNomeMes.Text = Funcoes.GetMes(mes).ToUpper();

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Colaborador Parametrizada

                var lst = new List<RelListaPautaChamadaVerso>();

                for (int i = 1; i <= 33; i++)
                {
                    lst.Add(new RelListaPautaChamadaVerso { Posicao = i });
                }                

                var res = lst.ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.ToList().Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (RelListaPautaChamadaVerso at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Lista Pauta Chamada Verso

        public class RelListaPautaChamadaVerso
        {
            public int Posicao { get; set; }
        }
        #endregion
    }
}
