using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports.Helper;

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3600_CtrlInformacoesAlunos
{
    public partial class RptResumoMatPeriodo : C2BR.GestorEducacao.Reports.RptRetrato
    {
        #region ctor

        public RptResumoMatPeriodo()
        {
            InitializeComponent();
        }

        #endregion

        #region Init Report

        public int InitReport(string parametros,
                                int codEmp,
                                int codUndCont,
                                int coMod,
                                int coSer,
                                int coTur,
                                string coAno,
                                string coSit,
                                DateTime dtInicio,
                                DateTime dtFim,
                                string infos,
                                bool mValores)
        {
            try
            {
                MostrarValores(mValores);

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

                #region Query

                var lst = from mat in ctx.TB08_MATRCUR
                          join turma in ctx.TB06_TURMAS on mat.CO_TUR equals turma.CO_TUR
                          join cur in ctx.TB01_CURSO on mat.CO_CUR equals cur.CO_CUR
                          where 
                          (dtInicio != DateTime.MinValue ? mat.DT_EFE_MAT >= dtInicio : 0 == 0)
                          && (dtFim != DateTime.MinValue ? ((mat.DT_EFE_MAT < dtFim) || ((mat.DT_EFE_MAT.Day == dtFim.Day) && (mat.DT_EFE_MAT.Month == dtFim.Month) && (mat.DT_EFE_MAT.Year == dtFim.Year))) : 0 == 0)
                          && (codUndCont != -1 ? turma.CO_EMP == codUndCont : 0 == 0)
                          && (coMod != 0 ? mat.TB44_MODULO.CO_MODU_CUR == coMod : 0 == 0)
                          && (coSer != 0 ? mat.CO_CUR == coSer : 0 == 0)
                          && (coTur != 0 ? mat.CO_TUR == coTur : 0 == 0)
                          && (coAno != "-1" ? mat.CO_ANO_MES_MAT == coAno : 0 == 0)
                          && (coSit != "0" ? mat.CO_SIT_MAT == coSit : 0 == 0)
                          group new
                          {
                              mat,
                              turma,
                              cur
                          }
                          by new
                          {
                              mat.TB44_MODULO.DE_MODU_CUR,
                              turma.TB129_CADTURMAS.NO_TURMA,
                              cur.NO_CUR,
                              //mat.DT_EFE_MAT,
                          }
                              into grp
                              select new Registro
                              {
                                  Modalidade = grp.Key.DE_MODU_CUR,
                                  Turma = grp.Key.NO_TURMA,
                                  Serie = grp.Key.NO_CUR,
                                  Qtd = grp.Count(),
                                  VlMes = grp.Select(x => x.mat).Sum(x => ((x.VL_PAR_MOD_MAT ?? 0) - (((x.VL_DES_MOD_MAT ?? 0) + (x.VL_DES_BOL_MOD_MAT ?? 0)) / (x.QT_PAR_MOD_MAT ?? 1)))),
                                  VlContrato = grp.Select(x => x.mat).Sum(x => ((x.VL_TOT_MODU_MAT ?? 0) - ((x.VL_DES_MOD_MAT ?? 0) + (x.VL_DES_BOL_MOD_MAT ?? 0)))),
                                  vlMensalidade = grp.Select(x => x.mat).Average(x => x.VL_PAR_MOD_MAT),
                                  vlMedia = grp.Select(x => x.mat).Average(x => ((x.VL_PAR_MOD_MAT ?? 0) - (((x.VL_DES_MOD_MAT ?? 0) + (x.VL_DES_BOL_MOD_MAT ?? 0)) / (x.QT_PAR_MOD_MAT ?? 1))))
                              };

                var res = lst.OrderBy(x => x.Modalidade).ThenBy(x => x.Serie).ThenBy(x => x.Turma).ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (Registro at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        private DateTime FormataData(DateTime dt)
        {
            string dtFormat = dt.ToString().Substring(0, 10);
            return DateTime.Parse(dtFormat);
        }

        #region Classe Alunos do Relatorio
        /// <summary>
        /// Classe de lista de matrículas
        /// </summary>
        public class Registro
        {
            public string Serie { get; set; }
            public string USerie
            {
                get
                {
                    return this.Serie.ToUpper();
                }
            }

            public string Modalidade { get; set; }
            public string UModalidade
            {
                get
                {
                    return this.Modalidade.ToUpper();
                }
            }

            public string Turma { get; set; }
            public string UTurma
            {
                get
                {
                    return this.Turma.ToUpper();
                }
            }
            public int Qtd { get; set; }
            public decimal? VlMes { get; set; }
            public decimal? VlContrato { get; set; }
            public decimal? vlMensalidade { get; set; }
            public decimal? vlMedia { get; set; }
            public int qtdPrevista { get; set; }
        }

        #endregion

        #region Metodos personalizados
        /// <summary>
        /// Esconde as colunas de valores quando solicitado pela página de sem valores
        /// </summary>
        /// <param name="mostrar">Valores recebido da página solicitante</param>
        private void MostrarValores(bool mostrar = true)
        {
            if (!mostrar)
            {
                float largura = (xrTbMensTi.WidthF + xrTableCell7.WidthF + xrTableCell4.WidthF + xrTableCell8.WidthF);
                float larguraAnterior = xrTableCell3.WidthF;
                float larguraTabela = xrTable1.WidthF;
            
                #region Diminuir tamanho colunas valores

                xrTbMensTi.WidthF = float.MinValue;
                xrTableCell7.WidthF = float.MinValue;
                xrTableCell4.WidthF = float.MinValue;
                xrTableCell8.WidthF = float.MinValue;

                xrTableCell21.WidthF = float.MinValue;
                xrTableCell14.WidthF = float.MinValue;
                xrTableCell15.WidthF = float.MinValue;
                xrTableCell5.WidthF = float.MinValue;

                xrTableCell24.WidthF = float.MinValue;
                xrTableCell23.WidthF = float.MinValue;
                xrTableCell10.WidthF = float.MinValue;
                xrTableCell11.WidthF = float.MinValue;

                xrTableCell26.WidthF = float.MinValue;
                xrTableCell25.WidthF = float.MinValue;
                xrTableCell19.WidthF = float.MinValue;
                xrTableCell20.WidthF = float.MinValue;

                #endregion

                #region Esconder colunas valores

                xrTbMensTi.Visible = mostrar;
                xrTableCell7.Visible = mostrar;
                xrTableCell4.Visible = mostrar;
                xrTableCell8.Visible = mostrar;

                xrTableCell21.Visible = mostrar;
                xrTableCell14.Visible = mostrar;
                xrTableCell15.Visible = mostrar;
                xrTableCell5.Visible = mostrar;

                xrTableCell24.Visible = mostrar;
                xrTableCell23.Visible = mostrar;
                xrTableCell10.Visible = mostrar;
                xrTableCell11.Visible = mostrar;

                xrTableCell26.Visible = mostrar;
                xrTableCell25.Visible = mostrar;
                xrTableCell19.Visible = mostrar;
                xrTableCell20.Visible = mostrar;

                #endregion

                #region Retornar tamanho tabelas

                xrTable1.WidthF = larguraTabela;
                xrTable2.WidthF = larguraTabela;
                xrTable3.WidthF = larguraTabela;
                xrTable4.WidthF = larguraTabela;

                #endregion

                #region Dimunuir linhas
            
                float difer = (4 * xrTbMensTi.WidthF);
                xrLine3.WidthF -= difer;
                xrLine2.WidthF -= difer;
                xrLine1.WidthF -= difer;
            
                #endregion
            }

            

        }
        #endregion
    }
}
