//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: DESEMPENHO, ÍNDICES E ESTATÍSTICAS
// SUBMÓDULO: ÍNDICES E INDICADORES (PEDAGÓGICO)
// OBJETIVO:  MAPA DE DESEMPENHO ESCOLAR POR UNIDADE/SÉRIES
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F9000_CtrlResultados.F9020_IndicesCtrlPedagogico
{
    public partial class MapaEvoDesMediaEsc : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CarregaTpUnidade();
                CarregaUnidades();
                CarregaModalidades();
                CarregaSerieCurso();
                CarregaAnos();
            }
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaUnidades()
        {
            int coTipoEmp = ddlTpUnidade.SelectedValue != "T" ? int.Parse(ddlTpUnidade.SelectedValue) : 0;

            ddlUnidadeEscolar.Items.Clear();

            if (coTipoEmp != 0)
            {

                ddlUnidadeEscolar.DataSource = TB25_EMPRESA.RetornaTodosRegistros().Where(p => p.TB24_TPEMPRESA.CO_TIPOEMP == coTipoEmp && p.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

                ddlUnidadeEscolar.DataTextField = "NO_FANTAS_EMP";
                ddlUnidadeEscolar.DataValueField = "CO_EMP";
                ddlUnidadeEscolar.DataBind();
            }

            ddlUnidadeEscolar.Items.Insert(0, new ListItem("Todas", "T"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Tipo de Unidade
        /// </summary>
        private void CarregaTpUnidade()
        {
            ddlTpUnidade.DataSource = TB24_TPEMPRESA.RetornaTodosRegistros().Where(p => p.CL_CLAS_EMP == "E").OrderBy(t => t.NO_TIPOEMP);

            ddlTpUnidade.DataTextField = "NO_TIPOEMP";
            ddlTpUnidade.DataValueField = "CO_TIPOEMP";
            ddlTpUnidade.DataBind();

            ddlTpUnidade.Items.Insert(0, new ListItem("Todos", "T"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Anos de Referência
        /// </summary>
        private void CarregaAnos()
        {
            int coTipoEmp = ddlTpUnidade.SelectedValue != "T" ? int.Parse(ddlTpUnidade.SelectedValue) : 0;
            int coEmp = ddlUnidadeEscolar.SelectedValue != "T" ? int.Parse(ddlUnidadeEscolar.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "T" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "T" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            ddlAnoRefer.DataSource = (from tb901 in TB901_DESEMP_ESCOL.RetornaTodosRegistros()
                                      where (coEmp != 0 ? tb901.TB06_TURMAS.CO_EMP == coEmp : coEmp == 0)
                                      && (coTipoEmp != 0 ? tb901.TB06_TURMAS.TB01_CURSO.TB25_EMPRESA.TB24_TPEMPRESA.CO_TIPOEMP == coTipoEmp : coTipoEmp == 0)
                                      && (modalidade != 0 ? tb901.TB06_TURMAS.CO_MODU_CUR == modalidade : modalidade == 0)
                                      && (serie != 0 ? tb901.TB06_TURMAS.CO_CUR == serie : serie == 0)
                                      && tb901.TB03_COLABOR.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                      select new { tb901.CO_ANO_REF }).Distinct().OrderByDescending( d => d.CO_ANO_REF );

            ddlAnoRefer.DataTextField = "CO_ANO_REF";
            ddlAnoRefer.DataValueField = "CO_ANO_REF";
            ddlAnoRefer.DataBind();                
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where( m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();

            ddlModalidade.Items.Insert(0, new ListItem("Todas", "T"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {            
            int coEmp = ddlUnidadeEscolar.SelectedValue != "T" ? int.Parse(ddlUnidadeEscolar.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "T" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            ddlSerieCurso.Items.Clear();

            ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(coEmp)
                                        where tb01.CO_MODU_CUR == modalidade
                                        select new { tb01.NO_CUR, tb01.CO_CUR }).OrderBy( c => c.NO_CUR );

            ddlSerieCurso.DataTextField = "NO_CUR";
            ddlSerieCurso.DataValueField = "CO_CUR";
            ddlSerieCurso.DataBind();

            ddlSerieCurso.Items.Insert(0, new ListItem("Todas", "T"));
        }
        #endregion

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            int coTpUnidade = ddlTpUnidade.SelectedValue != "T" ? int.Parse(ddlTpUnidade.SelectedValue) : 0;
            int coUnidade = ddlUnidadeEscolar.SelectedValue != "T" ? int.Parse(ddlUnidadeEscolar.SelectedValue) : 0;
            int coModalidade = ddlModalidade.SelectedValue != "T" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int coSerie = ddlSerieCurso.SelectedValue != "T" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            string coAnoRefer = ddlAnoRefer.SelectedValue != "T" ? ddlAnoRefer.SelectedValue : "0";
            string[] siglaSer = new string[9];
            string[] nomeSer = new string[9];
            decimal totalMedia = 0;

            for (int i = 0; i <= siglaSer.Length; i++)
            {
                //siglaSer[i] = "-";
                //nomeSer[i] = "-";
            }

            DataTable dt = new DataTable();

            dt.Columns.Add("NO_FANTAS_EMP");
            dt.Columns.Add("S1");
            dt.Columns.Add("P1");
            dt.Columns.Add("S2");
            dt.Columns.Add("P2");
            dt.Columns.Add("S3");
            dt.Columns.Add("P3");
            dt.Columns.Add("S4");
            dt.Columns.Add("P4");
            dt.Columns.Add("S5");
            dt.Columns.Add("P5");
            dt.Columns.Add("S6");
            dt.Columns.Add("P6");
            dt.Columns.Add("S7");
            dt.Columns.Add("P7");
            dt.Columns.Add("S8");
            dt.Columns.Add("P8");
            dt.Columns.Add("S9");
            dt.Columns.Add("P9");
            dt.Columns.Add("MEDIA");

            var qryUnidades = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                               where (coTpUnidade != 0 ? tb25.TB24_TPEMPRESA.CO_TIPOEMP == coTpUnidade : coTpUnidade == 0)
                                && (coUnidade != 0 ? tb25.CO_EMP == coUnidade : coUnidade == 0)
                                && tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                               select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP }).Distinct();

            decimal[] totalSitu = new decimal[9];

            foreach (var qryM in qryUnidades)
            {
                decimal[] parcSitu = new decimal[9];
                decimal[] percSitu = new decimal[9];

                var qryGrid = from tb901 in TB901_DESEMP_ESCOL.RetornaTodosRegistros()
                              join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb901.TB06_TURMAS.CO_EMP equals tb25.CO_EMP
                              where (coModalidade != 0 ? tb901.TB06_TURMAS.CO_MODU_CUR == coModalidade : coModalidade == 0)
                              && (coSerie != 0 ? tb901.TB06_TURMAS.CO_CUR == coSerie : coSerie == 0)
                              && tb25.CO_EMP == qryM.CO_EMP && tb901.CO_ANO_REF == coAnoRefer && tb901.VL_MEDIA_DESEMP != null
                              group tb901 by tb901.TB06_TURMAS.CO_CUR into g
                              orderby g.Key
                              select new { CO_CUR = g.Key, media = g.Sum(p => (p.VL_MEDIA_DESEMP)) / g.Count() };

                int i = 0;

                foreach (var qryG in qryGrid)
                {
                    var noSerie = TB01_CURSO.RetornaPeloCoCur(qryG.CO_CUR);

                    var medGeral = (from tb901 in TB901_DESEMP_ESCOL.RetornaTodosRegistros()
                                    join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb901.TB06_TURMAS.CO_CUR equals tb01.CO_CUR
                                    where tb901.TB06_TURMAS.CO_MODU_CUR == tb01.CO_MODU_CUR
                                    && tb901.TB06_TURMAS.CO_EMP == tb01.CO_EMP && tb01.CO_SIGL_CUR == noSerie.CO_SIGL_CUR && tb901.VL_MEDIA_DESEMP != null
                                    select new { tb901.VL_MEDIA_DESEMP });

                    decimal mediaGeral = (decimal)medGeral.Sum(p => p.VL_MEDIA_DESEMP) / medGeral.Count();

                    if (i == 0)
                    {
                        nomeSer[0] = noSerie.NO_CUR;
                        siglaSer[0] = noSerie.CO_SIGL_CUR;
                        parcSitu[0] = qryG.media != null ? (decimal)qryG.media : 0;
                        percSitu[0] = (qryG.media != null) && mediaGeral != 0 ? (decimal)(qryG.media * 100) / mediaGeral : 0;
                        totalSitu[0] = qryG.media != null ? totalSitu[0] + (decimal)qryG.media : totalSitu[0];
                    }
                    else if (i == 1)
                    {
                        nomeSer[1] = noSerie.NO_CUR;
                        siglaSer[1] = noSerie.CO_SIGL_CUR;
                        parcSitu[1] = qryG.media != null ? (decimal)qryG.media : 0;
                        percSitu[1] = (qryG.media != null) && mediaGeral != 0 ? (decimal)(qryG.media * 100) / mediaGeral : 0;
                        totalSitu[1] = qryG.media != null ? totalSitu[1] + (decimal)qryG.media : totalSitu[1];
                    }
                    else if (i == 2)
                    {
                        nomeSer[2] = noSerie.NO_CUR;
                        siglaSer[2] = noSerie.CO_SIGL_CUR;
                        parcSitu[2] = qryG.media != null ? (decimal)qryG.media : 0;
                        percSitu[2] = (qryG.media != null) && mediaGeral != 0 ? (decimal)(qryG.media * 100) / mediaGeral : 0;
                        totalSitu[2] = qryG.media != null ? totalSitu[2] + (decimal)qryG.media : totalSitu[2];
                    }
                    else if (i == 3)
                    {
                        nomeSer[3] = noSerie.NO_CUR;
                        siglaSer[3] = noSerie.CO_SIGL_CUR;
                        parcSitu[3] = qryG.media != null ? (decimal)qryG.media : 0;
                        percSitu[3] = (qryG.media != null) && mediaGeral != 0 ? (decimal)(qryG.media * 100) / mediaGeral : 0;
                        totalSitu[3] = qryG.media != null ? totalSitu[3] + (decimal)qryG.media : totalSitu[3];
                    }
                    else if (i == 4)
                    {
                        nomeSer[4] = noSerie.NO_CUR;
                        siglaSer[4] = noSerie.CO_SIGL_CUR;
                        parcSitu[4] = qryG.media != null ? (decimal)qryG.media : 0;
                        percSitu[4] = (qryG.media != null) && mediaGeral != 0 ? (decimal)(qryG.media * 100) / mediaGeral : 0;
                        totalSitu[4] = qryG.media != null ? totalSitu[4] + (decimal)qryG.media : totalSitu[4];
                    }
                    else if (i == 5)
                    {
                        nomeSer[5] = noSerie.NO_CUR;
                        siglaSer[5] = noSerie.CO_SIGL_CUR;
                        parcSitu[5] = qryG.media != null ? (decimal)qryG.media : 0;
                        percSitu[5] = (qryG.media != null) && mediaGeral != 0 ? (decimal)(qryG.media * 100) / mediaGeral : 0;
                        totalSitu[5] = qryG.media != null ? totalSitu[5] + (decimal)qryG.media : totalSitu[5];
                    }
                    else if (i == 6)
                    {
                        nomeSer[6] = noSerie.NO_CUR;
                        siglaSer[6] = noSerie.CO_SIGL_CUR;
                        parcSitu[6] = qryG.media != null ? (decimal)qryG.media : 0;
                        percSitu[6] = (qryG.media != null) && mediaGeral != 0 ? (decimal)(qryG.media * 100) / mediaGeral : 0;
                        totalSitu[6] = qryG.media != null ? totalSitu[6] + (decimal)qryG.media : totalSitu[6];
                    }
                    else if (i == 7)
                    {
                        nomeSer[7] = noSerie.NO_CUR;
                        siglaSer[7] = noSerie.CO_SIGL_CUR;
                        parcSitu[7] = qryG.media != null ? (decimal)qryG.media : 0;
                        percSitu[7] = (qryG.media != null) && mediaGeral != 0 ? (decimal)(qryG.media * 100) / mediaGeral : 0;
                        totalSitu[7] = qryG.media != null ? totalSitu[7] + (decimal)qryG.media : totalSitu[7];
                    }
                    else if (i == 8)
                    {
                        nomeSer[8] = noSerie.NO_CUR;
                        siglaSer[8] = noSerie.CO_SIGL_CUR;
                        parcSitu[8] = qryG.media != null ? (decimal)qryG.media : 0;
                        percSitu[8] = (qryG.media != null) && mediaGeral != 0 ? (decimal)(qryG.media * 100) / mediaGeral : 0;
                        totalSitu[8] = qryG.media != null ? totalSitu[8] + (decimal)qryG.media : totalSitu[8];
                    }
                    i++;
                }

                decimal media = (parcSitu[0] + parcSitu[1] + parcSitu[2] + parcSitu[3] + parcSitu[4] + parcSitu[5] + parcSitu[6] + parcSitu[7] + parcSitu[8]) / (qryGrid.Count() > 0 ? qryGrid.Count() : 1);

                totalMedia = totalMedia + media;

                dt.Rows.Add(qryM.NO_FANTAS_EMP, String.Format("{0:N2}", parcSitu[0]), String.Format("{0:N1}", percSitu[0]), String.Format("{0:N2}", parcSitu[1]), String.Format("{0:N1}", percSitu[1]), String.Format("{0:N2}", parcSitu[2]),
                String.Format("{0:N1}", percSitu[2]), String.Format("{0:N2}", parcSitu[3]), String.Format("{0:N1}", percSitu[3]), String.Format("{0:N2}", parcSitu[4]), String.Format("{0:N1}", percSitu[4]), String.Format("{0:N2}", parcSitu[5]),
                String.Format("{0:N1}", percSitu[5]), String.Format("{0:N2}", parcSitu[6]), String.Format("{0:N1}", percSitu[6]), String.Format("{0:N2}", parcSitu[7]), String.Format("{0:N1}", percSitu[7]), String.Format("{0:N2}", parcSitu[8]),
                String.Format("{0:N1}", percSitu[8]), String.Format("{0:N2}", media));
            }

            grdResulParam.Columns.Clear();

            BoundField bfUnid = new BoundField();
            bfUnid.DataField = "NO_FANTAS_EMP";
            bfUnid.HeaderText = "Unidade";
            bfUnid.ItemStyle.Width = 370;
            grdResulParam.Columns.Add(bfUnid);

            BoundField bfS1 = new BoundField();
            bfS1.DataField = "S1";
            bfS1.HeaderText = siglaSer[0];
            bfS1.ItemStyle.Width = 25;
            bfS1.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfS1);

            BoundField bfP1 = new BoundField();
            bfP1.DataField = "P1";
            bfP1.HeaderText = "%";
            bfP1.ItemStyle.Width = 25;
            bfP1.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfP1);

            BoundField bfS2 = new BoundField();
            bfS2.DataField = "S2";
            bfS2.HeaderText = siglaSer[1];
            bfS2.ItemStyle.Width = 25;
            bfS2.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfS2);

            BoundField bfP2 = new BoundField();
            bfP2.DataField = "P2";
            bfP2.HeaderText = "%";
            bfP2.ItemStyle.Width = 25;
            bfP2.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfP2);

            BoundField bfS3 = new BoundField();
            bfS3.DataField = "S3";
            bfS3.HeaderText = siglaSer[2];
            bfS3.ItemStyle.Width = 25;
            bfS3.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfS3);

            BoundField bfP3 = new BoundField();
            bfP3.DataField = "P3";
            bfP3.HeaderText = "%";
            bfP3.ItemStyle.Width = 25;
            bfP3.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfP3);

            BoundField bfS4 = new BoundField();
            bfS4.DataField = "S4";
            bfS4.HeaderText = siglaSer[3];
            bfS4.ItemStyle.Width = 25;
            bfS4.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfS4);

            BoundField bfP4 = new BoundField();
            bfP4.DataField = "P4";
            bfP4.HeaderText = "%";
            bfP4.ItemStyle.Width = 25;
            bfP4.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfP4);

            BoundField bfS5 = new BoundField();
            bfS5.DataField = "S5";
            bfS5.HeaderText = siglaSer[4];
            bfS5.ItemStyle.Width = 25;
            bfS5.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfS5);

            BoundField bfP5 = new BoundField();
            bfP5.DataField = "P5";
            bfP5.HeaderText = "%";
            bfP5.ItemStyle.Width = 25;
            bfP5.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfP5);

            BoundField bfS6 = new BoundField();
            bfS6.DataField = "S6";
            bfS6.HeaderText = siglaSer[5];
            bfS6.ItemStyle.Width = 25;
            bfS6.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfS6);

            BoundField bfP6 = new BoundField();
            bfP6.DataField = "P6";
            bfP6.HeaderText = "%";
            bfP6.ItemStyle.Width = 25;
            bfP6.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfP6);

            BoundField bfS7 = new BoundField();
            bfS7.DataField = "S7";
            bfS7.HeaderText = siglaSer[6];
            bfS7.ItemStyle.Width = 25;
            bfS7.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfS7);

            BoundField bfP7 = new BoundField();
            bfP7.DataField = "P7";
            bfP7.HeaderText = "%";
            bfP7.ItemStyle.Width = 25;
            bfP7.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfP7);

            BoundField bfS8 = new BoundField();
            bfS8.DataField = "S8";
            bfS8.HeaderText = siglaSer[7];
            bfS8.ItemStyle.Width = 25;
            bfS8.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfS8);

            BoundField bfP8 = new BoundField();
            bfP8.DataField = "P8";
            bfP8.HeaderText = "%";
            bfP8.ItemStyle.Width = 25;
            bfP8.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfP8);

            BoundField bfS9 = new BoundField();
            bfS9.DataField = "S9";
            bfS9.HeaderText = siglaSer[8];
            bfS9.ItemStyle.Width = 25;
            bfS9.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfS9);

            BoundField bfP9 = new BoundField();
            bfP9.DataField = "P9";
            bfP9.HeaderText = "%";
            bfP9.ItemStyle.Width = 25;
            bfP9.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfP9);

            BoundField bfMedia = new BoundField();
            bfMedia.DataField = "MEDIA";
            bfMedia.HeaderText = "Média";
            bfMedia.ItemStyle.Width = 25;
            bfMedia.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfMedia);

            grdResulParam.DataSource = dt;
            grdResulParam.DataBind();

            liGridMCFF.Visible = true;
            liLegMCFF.Visible = true;

            double[] yValues = new double[9];

            for (int i = 0; i < totalSitu.Length; i++)
            {
                yValues[i] = (double)Math.Round((totalSitu[i] / (grdResulParam.Rows.Count > 0 ? grdResulParam.Rows.Count : 1)), 2);
            }

            Chart1.Series["Default"].Points.DataBindXY(nomeSer, yValues);


            Chart1.Series["Default"].ChartType = SeriesChartType.Pie;

            Chart1.Series["Default"]["PieLabelStyle"] = "Disabled";

            Chart1.Series["Default"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Pie", true);

            Chart1.Titles[0].Text = "Médias Por Série";

            Chart1.Series["Default"]["PieLabelStyle"] = "Inside";

            Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;

            Chart1.Series[0]["PieDrawingStyle"] = "SoftEdge";

            this.Chart1.Legends[0].Enabled = true;
        }

        protected void ddlTpUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaUnidades();
            CarregaSerieCurso();
        }

        protected void ddlUnidadeEscolar_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
        }
    }
}
