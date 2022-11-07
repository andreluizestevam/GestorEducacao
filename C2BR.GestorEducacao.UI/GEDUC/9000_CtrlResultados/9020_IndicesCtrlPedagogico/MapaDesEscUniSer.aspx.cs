//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: DESEMPENHO, ÍNDICES E ESTATÍSTICAS
// SUBMÓDULO: ÍNDICES E INDICADORES (PEDAGÓGICO)
// OBJETIVO:  MAPA EVOLUTIVO DE DESEMPENHO (MÉDIA ESCOLAR) DAS UNIDADES DE ENSINO
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
    public partial class MapaDesEscUniSer : System.Web.UI.Page
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
            ddlTpUnidade.DataSource = TB24_TPEMPRESA.RetornaTodosRegistros().Where(p => p.CL_CLAS_EMP == "E").OrderBy(t => t.NO_TIPOEMP); ;

            ddlTpUnidade.DataTextField = "NO_TIPOEMP";
            ddlTpUnidade.DataValueField = "CO_TIPOEMP";
            ddlTpUnidade.DataBind();

            ddlTpUnidade.Items.Insert(0, new ListItem("Todos", "T"));
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
                                        select new { tb01.NO_CUR, tb01.CO_CUR }).OrderBy(c => c.NO_CUR);

            ddlSerieCurso.DataTextField = "NO_CUR";
            ddlSerieCurso.DataValueField = "CO_CUR";
            ddlSerieCurso.DataBind();

            ddlSerieCurso.Items.Insert(0, new ListItem("Todas", "T"));
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
        #endregion

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            grdResulParam.DataSource = null;
            grdResulParam.DataBind();

            int coTpUnidade = ddlTpUnidade.SelectedValue != "T" ? int.Parse(ddlTpUnidade.SelectedValue) : 0;
            int coUnidade = ddlUnidadeEscolar.SelectedValue != "T" ? int.Parse(ddlUnidadeEscolar.SelectedValue) : 0;
            int coModalidade = ddlModalidade.SelectedValue != "T" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int coSerie = ddlSerieCurso.SelectedValue != "T" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int coAnoRefer = int.Parse(ddlAnoRefer.SelectedValue);

            DataTable dt = new DataTable();

            dt.Columns.Add("NO_FANTAS_EMP");
            dt.Columns.Add("Ano1");
            dt.Columns.Add("Ano2");
            dt.Columns.Add("Perc1");
            dt.Columns.Add("Ano3");
            dt.Columns.Add("Perc2");
            dt.Columns.Add("Ano4");
            dt.Columns.Add("Perc3");

            var qryUnidades = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                               where (coTpUnidade != 0 ? tb25.TB24_TPEMPRESA.CO_TIPOEMP == coTpUnidade : coTpUnidade == 0)
                               && (coUnidade != 0 ? tb25.CO_EMP == coUnidade : coUnidade == 0)
                               && tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                               select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP }).Distinct();

            decimal[] totalSitu = new decimal[4];

            foreach (var qryM in qryUnidades)
            {
                decimal[] parcSitu = new decimal[4];

                var qryGrid1 = (from tb901 in TB901_DESEMP_ESCOL.RetornaTodosRegistros()
                                  join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb901.TB06_TURMAS.CO_EMP equals tb25.CO_EMP
                                  where (coModalidade != 0 ? tb901.TB06_TURMAS.CO_MODU_CUR == coModalidade : coModalidade == 0)
                                  && (coSerie != 0 ? tb901.TB06_TURMAS.CO_CUR == coSerie : coSerie == 0)
                                  && tb25.CO_EMP == qryM.CO_EMP
                                  select new { tb901 }).ToList();

                var qryGrid = from rTb901 in qryGrid1
                              where int.Parse(rTb901.tb901.CO_ANO_REF) >= coAnoRefer
                              group rTb901 by rTb901.tb901.CO_ANO_REF into g
                               orderby g.Key
                               select new { CO_ANO_REF = g.Key, media = g.Sum(p => (p.tb901.VL_MEDIA_DESEMP)) / g.Count() };

                int i = 0;

                foreach (var qryG in qryGrid)
                {
                    if (i == 0)
                    {
                        parcSitu[0] = qryG.media != null ? (decimal)qryG.media : 0;
                        totalSitu[0] = qryG.media != null ? totalSitu[0] + (decimal)qryG.media : totalSitu[0];
                    }
                    else if (i == 1)
                    {
                        parcSitu[1] = qryG.media != null ? (decimal)qryG.media : 0;
                        totalSitu[1] = qryG.media != null ? totalSitu[1] + (decimal)qryG.media : totalSitu[1];
                    }
                    else if (i == 2)
                    {
                        parcSitu[2] = qryG.media != null ? (decimal)qryG.media : 0;
                        totalSitu[2] = qryG.media != null ? totalSitu[2] + (decimal)qryG.media : totalSitu[2];
                    }
                    else if (i == 3)
                    {
                        parcSitu[3] = qryG.media != null ? (decimal)qryG.media : 0;
                        totalSitu[3] = qryG.media != null ? totalSitu[3] + (decimal)qryG.media : totalSitu[3];
                    }
                    i++;
                }

                decimal p1 = (parcSitu[0] > 0) && (parcSitu[1] > 0) ? ((parcSitu[1] * 100) / parcSitu[0]) - 100 : 0;
                decimal p2 = (parcSitu[1] > 0) && (parcSitu[2] > 0) ? ((parcSitu[2] * 100) / parcSitu[1]) - 100 : 0;
                decimal p3 = (parcSitu[2] > 0) && (parcSitu[3] > 0) ? ((parcSitu[3] * 100) / parcSitu[2]) - 100 : 0;

                dt.Rows.Add(qryM.NO_FANTAS_EMP, String.Format("{0:N2}", parcSitu[0]), String.Format("{0:N2}", parcSitu[1]), String.Format("{0:N2}", p1), String.Format("{0:N2}", parcSitu[2]),
                String.Format("{0:N2}", p2), String.Format("{0:N2}", parcSitu[3]), String.Format("{0:N2}", p3));//, String.Format("{0:N2}", media));
            }

            grdResulParam.Columns.Clear();

            BoundField bfUnid = new BoundField();
            bfUnid.DataField = "NO_FANTAS_EMP";
            bfUnid.HeaderText = "Unidade";
            bfUnid.ItemStyle.Width = 370;
            grdResulParam.Columns.Add(bfUnid);

            BoundField bfAno1 = new BoundField();
            bfAno1.DataField = "Ano1";
            bfAno1.HeaderText = ddlAnoRefer.SelectedValue;
            bfAno1.ItemStyle.Width = 25;
            bfAno1.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfAno1);

            BoundField bfAno2 = new BoundField();
            bfAno2.DataField = "Ano2";
            bfAno2.HeaderText = (int.Parse(ddlAnoRefer.SelectedValue) + 1).ToString();
            bfAno2.ItemStyle.Width = 25;
            bfAno2.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfAno2);

            BoundField bfPerc1 = new BoundField();
            bfPerc1.DataField = "Perc1";
            bfPerc1.HeaderText = "%";
            bfPerc1.ItemStyle.Width = 25;
            bfPerc1.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfPerc1);

            BoundField bfAno3 = new BoundField();
            bfAno3.DataField = "Ano3";
            bfAno3.HeaderText = (int.Parse(ddlAnoRefer.SelectedValue) + 2).ToString();
            bfAno3.ItemStyle.Width = 25;
            bfAno3.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfAno3);

            BoundField bfPerc2 = new BoundField();
            bfPerc2.DataField = "Perc2";
            bfPerc2.HeaderText = "%";
            bfPerc2.ItemStyle.Width = 25;
            bfPerc2.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfPerc2);

            BoundField bfAno4 = new BoundField();
            bfAno4.DataField = "Ano4";
            bfAno4.HeaderText = (int.Parse(ddlAnoRefer.SelectedValue) + 3).ToString();
            bfAno4.ItemStyle.Width = 25;
            bfAno4.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfAno4);

            BoundField bfPerc3 = new BoundField();
            bfPerc3.DataField = "Perc3";
            bfPerc3.HeaderText = "%";
            bfPerc3.ItemStyle.Width = 25;
            bfPerc3.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfPerc3);

            grdResulParam.DataSource = dt;
            grdResulParam.DataBind();

            lblTotT1.Text = String.Format("{0:N2}", totalSitu[0] / (grdResulParam.Rows.Count > 0 ? grdResulParam.Rows.Count : 1));
            lblTotT2.Text = String.Format("{0:N2}", totalSitu[1] / (grdResulParam.Rows.Count > 0 ? grdResulParam.Rows.Count : 1));
            lblTotT3.Text = String.Format("{0:N2}", totalSitu[2] / (grdResulParam.Rows.Count > 0 ? grdResulParam.Rows.Count : 1));
            lblTotT4.Text = String.Format("{0:N2}", totalSitu[3] / (grdResulParam.Rows.Count > 0 ? grdResulParam.Rows.Count : 1));

            liGridMCFF.Visible = true;
            liImgMDEUS.Visible = true;

            DataTable dtGrafico = new DataTable();

            dtGrafico.Columns.Add("Ano");
            dtGrafico.Columns.Add("Media");

            dtGrafico.Rows.Add(ddlAnoRefer.SelectedValue, lblTotT1.Text.Replace(",", "."));
            dtGrafico.Rows.Add((int.Parse(ddlAnoRefer.SelectedValue) + 1).ToString(), lblTotT2.Text.Replace(",", "."));
            dtGrafico.Rows.Add((int.Parse(ddlAnoRefer.SelectedValue) + 2).ToString(), lblTotT3.Text.Replace(",", "."));
            dtGrafico.Rows.Add((int.Parse(ddlAnoRefer.SelectedValue) + 3).ToString(), lblTotT4.Text.Replace(",", "."));

            Grafico1.DataSource = dtGrafico;
            Grafico1.DataBind();
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
