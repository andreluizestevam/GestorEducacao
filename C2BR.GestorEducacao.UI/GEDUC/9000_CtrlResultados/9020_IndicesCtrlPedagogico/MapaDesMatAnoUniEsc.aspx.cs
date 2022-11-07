//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: DESEMPENHO, ÍNDICES E ESTATÍSTICAS
// SUBMÓDULO: ÍNDICES E INDICADORES (PEDAGÓGICO)
// OBJETIVO:  MAPA DE DESEMPENHO DE MATÉRIAS (ANO LETIVO) POR UNIDADE ESCOLAR
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
    public partial class MapaDesMatAnoUniEsc : System.Web.UI.Page
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
        /// Método que carrega o dropdown de Anos de Referência
        /// </summary>
        private void CarregaAnos()
        {
            int coTipoEmp = ddlTpUnidade.SelectedValue != "T" ? int.Parse(ddlTpUnidade.SelectedValue) : 0;
            int coEmp = ddlUnidadeEscolar.SelectedValue != "T" ? int.Parse(ddlUnidadeEscolar.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "T" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            ddlAnoRefer.DataSource = (from tb956 in TB956_ESTAT_SERTUR.RetornaTodosRegistros()
                                      where (coEmp != 0 ? tb956.TB06_TURMAS.CO_EMP == coEmp : coEmp == 0)
                                      && (coTipoEmp != 0 ? tb956.TB06_TURMAS.TB01_CURSO.TB25_EMPRESA.TB24_TPEMPRESA.CO_TIPOEMP == coTipoEmp : coTipoEmp == 0)
                                      && (modalidade != 0 ? tb956.TB06_TURMAS.CO_MODU_CUR == modalidade : modalidade == 0)
                                      && tb956.TB03_COLABOR.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                      select new { tb956.CO_ANO_REF }).Distinct().OrderByDescending( e => e.CO_ANO_REF );

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
        #endregion

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            int coTpUnidade = ddlTpUnidade.SelectedValue != "T" ? int.Parse(ddlTpUnidade.SelectedValue) : 0;
            int coUnidade = ddlUnidadeEscolar.SelectedValue != "T" ? int.Parse(ddlUnidadeEscolar.SelectedValue) : 0;
            int coModalidade = ddlModalidade.SelectedValue != "T" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int coAnoRefer = ddlAnoRefer.SelectedValue != "T" ? int.Parse(ddlAnoRefer.SelectedValue) : 0;
            decimal totalMedia = 0;

            lblS1.Text = "-";
            lblS2.Text = "-";
            lblS3.Text = "-";
            lblS4.Text = "-";
            lblS5.Text = "-";
            lblS6.Text = "-";
            lblS7.Text = "-";
            lblS8.Text = "-";
            lblS9.Text = "-";
            lblS10.Text = "-";
            lblS11.Text = "-";
            lblS12.Text = "-";

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
            dt.Columns.Add("S10");
            dt.Columns.Add("P10");
            dt.Columns.Add("S11");
            dt.Columns.Add("P11");
            dt.Columns.Add("S12");
            dt.Columns.Add("P12");
            dt.Columns.Add("MEDIA");

            var qryUnidades = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                               where (coTpUnidade != 0 ? tb25.TB24_TPEMPRESA.CO_TIPOEMP == coTpUnidade : coTpUnidade == 0)
                               && (coUnidade != 0 ? tb25.CO_EMP == coUnidade : coUnidade == 0)
                               && tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                               select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP }).Distinct();

            decimal[] totalSitu = new decimal[13];

            foreach (var qryM in qryUnidades)
            {
                decimal[] parcSitu = new decimal[13];
                decimal[] percSitu = new decimal[13];

                var qryGrid = from tb956 in TB956_ESTAT_SERTUR.RetornaTodosRegistros()
                              join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb956.ID_MATERIA equals tb107.ID_MATERIA
                              join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb956.TB06_TURMAS.CO_EMP equals tb25.CO_EMP
                              where (coModalidade != 0 ? tb956.TB06_TURMAS.CO_MODU_CUR == coModalidade : coModalidade == 0)
                              && tb25.CO_EMP == qryM.CO_EMP && tb956.CO_ANO_REF == coAnoRefer && tb956.VL_MEDIA_ANO != null
                              group tb956 by tb107.NO_SIGLA_MATERIA into g
                              orderby g.Key
                              select new { NO_SIGLA_MATERIA = g.Key, media = g.Sum(p => (p.VL_MEDIA_ANO)) / g.Count() };                
                
                int i = 0;

                if (qryGrid.Count() > 0)
                {
                    foreach (var qryG in qryGrid)
                    {

                        var medGeral = (from tb956 in TB956_ESTAT_SERTUR.RetornaTodosRegistros()
                                        join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb956.ID_MATERIA equals tb107.ID_MATERIA
                                        where tb956.TB06_TURMAS.CO_EMP == tb107.CO_EMP && tb107.NO_SIGLA_MATERIA == qryG.NO_SIGLA_MATERIA
                                        && tb956.VL_MEDIA_ANO != null
                                        select new { tb956.VL_MEDIA_ANO });

                        decimal mediaGeral = (decimal)medGeral.Sum(p => p.VL_MEDIA_ANO) / medGeral.Count();

                        string noMateria = TB107_CADMATERIAS.RetornaTodosRegistros().Where(p => p.NO_SIGLA_MATERIA == qryG.NO_SIGLA_MATERIA).FirstOrDefault().NO_RED_MATERIA;

                        if (i == 0)
                        {
                            lblSig1.Text = qryG.NO_SIGLA_MATERIA;
                            lblS1.Text = noMateria;
                            parcSitu[0] = qryG.media != null ? (decimal)qryG.media : 0;
                            percSitu[0] = (qryG.media != null) && mediaGeral != 0 ? (decimal)(qryG.media * 100) / mediaGeral : 0;
                            totalSitu[0] = qryG.media != null ? totalSitu[0] + (decimal)qryG.media : totalSitu[0];
                        }
                        else if (i == 1)
                        {
                            lblSig2.Text = qryG.NO_SIGLA_MATERIA;
                            lblS2.Text = noMateria;
                            parcSitu[1] = qryG.media != null ? (decimal)qryG.media : 0;
                            percSitu[1] = (qryG.media != null) && mediaGeral != 0 ? (decimal)(qryG.media * 100) / mediaGeral : 0;
                            totalSitu[1] = qryG.media != null ? totalSitu[1] + (decimal)qryG.media : totalSitu[1];
                        }
                        else if (i == 2)
                        {
                            lblSig3.Text = qryG.NO_SIGLA_MATERIA;
                            lblS3.Text = noMateria;
                            parcSitu[2] = qryG.media != null ? (decimal)qryG.media : 0;
                            percSitu[2] = (qryG.media != null) && mediaGeral != 0 ? (decimal)(qryG.media * 100) / mediaGeral : 0;
                            totalSitu[2] = qryG.media != null ? totalSitu[2] + (decimal)qryG.media : totalSitu[2];
                        }
                        else if (i == 3)
                        {
                            lblSig4.Text = qryG.NO_SIGLA_MATERIA;
                            lblS4.Text = noMateria;
                            parcSitu[3] = qryG.media != null ? (decimal)qryG.media : 0;
                            percSitu[3] = (qryG.media != null) && mediaGeral != 0 ? (decimal)(qryG.media * 100) / mediaGeral : 0;
                            totalSitu[3] = qryG.media != null ? totalSitu[3] + (decimal)qryG.media : totalSitu[3];
                        }
                        else if (i == 4)
                        {
                            lblSig5.Text = qryG.NO_SIGLA_MATERIA;
                            lblS5.Text = noMateria;
                            parcSitu[4] = qryG.media != null ? (decimal)qryG.media : 0;
                            percSitu[4] = (qryG.media != null) && mediaGeral != 0 ? (decimal)(qryG.media * 100) / mediaGeral : 0;
                            totalSitu[4] = qryG.media != null ? totalSitu[4] + (decimal)qryG.media : totalSitu[4];
                        }
                        else if (i == 5)
                        {
                            lblSig6.Text = qryG.NO_SIGLA_MATERIA;
                            lblS6.Text = noMateria;
                            parcSitu[5] = qryG.media != null ? (decimal)qryG.media : 0;
                            percSitu[5] = (qryG.media != null) && mediaGeral != 0 ? (decimal)(qryG.media * 100) / mediaGeral : 0;
                            totalSitu[5] = qryG.media != null ? totalSitu[5] + (decimal)qryG.media : totalSitu[5];
                        }
                        else if (i == 6)
                        {
                            lblSig7.Text = qryG.NO_SIGLA_MATERIA;
                            lblS7.Text = noMateria;
                            parcSitu[6] = qryG.media != null ? (decimal)qryG.media : 0;
                            percSitu[6] = (qryG.media != null) && mediaGeral != 0 ? (decimal)(qryG.media * 100) / mediaGeral : 0;
                            totalSitu[6] = qryG.media != null ? totalSitu[6] + (decimal)qryG.media : totalSitu[6];
                        }
                        else if (i == 7)
                        {
                            lblSig8.Text = qryG.NO_SIGLA_MATERIA;
                            lblS8.Text = noMateria;
                            parcSitu[7] = qryG.media != null ? (decimal)qryG.media : 0;
                            percSitu[7] = (qryG.media != null) && mediaGeral != 0 ? (decimal)(qryG.media * 100) / mediaGeral : 0;
                            totalSitu[7] = qryG.media != null ? totalSitu[7] + (decimal)qryG.media : totalSitu[7];
                        }
                        else if (i == 8)
                        {
                            lblSig9.Text = qryG.NO_SIGLA_MATERIA;
                            lblS9.Text = noMateria;
                            parcSitu[8] = qryG.media != null ? (decimal)qryG.media : 0;
                            percSitu[8] = (qryG.media != null) && mediaGeral != 0 ? (decimal)(qryG.media * 100) / mediaGeral : 0;
                            totalSitu[8] = qryG.media != null ? totalSitu[8] + (decimal)qryG.media : totalSitu[8];
                        }
                        else if (i == 9)
                        {
                            lblSig10.Text = qryG.NO_SIGLA_MATERIA;
                            lblS10.Text = noMateria;
                            parcSitu[9] = qryG.media != null ? (decimal)qryG.media : 0;
                            percSitu[9] = (qryG.media != null) && mediaGeral != 0 ? (decimal)(qryG.media * 100) / mediaGeral : 0;
                            totalSitu[9] = qryG.media != null ? totalSitu[9] + (decimal)qryG.media : totalSitu[9];
                        }
                        else if (i == 10)
                        {
                            lblSig11.Text = qryG.NO_SIGLA_MATERIA;
                            lblS11.Text = noMateria;
                            parcSitu[10] = qryG.media != null ? (decimal)qryG.media : 0;
                            percSitu[10] = (qryG.media != null) && mediaGeral != 0 ? (decimal)(qryG.media * 100) / mediaGeral : 0;
                            totalSitu[10] = qryG.media != null ? totalSitu[10] + (decimal)qryG.media : totalSitu[10];
                        }
                        else if (i == 11)
                        {
                            lblSig12.Text = qryG.NO_SIGLA_MATERIA;
                            lblS12.Text = noMateria;
                            parcSitu[11] = qryG.media != null ? (decimal)qryG.media : 0;
                            percSitu[11] = (qryG.media != null) && mediaGeral != 0 ? (decimal)(qryG.media * 100) / mediaGeral : 0;
                            totalSitu[11] = qryG.media != null ? totalSitu[11] + (decimal)qryG.media : totalSitu[11];
                        }
                        i++;
                    }

                    decimal media = (parcSitu[0] + parcSitu[1] + parcSitu[2] + parcSitu[3] + parcSitu[4] + parcSitu[5] + parcSitu[6] + parcSitu[7] + parcSitu[8] + parcSitu[9] + parcSitu[10] + parcSitu[11]) / (qryGrid.Count() > 0 ? qryGrid.Count() : 1);

                    totalMedia = totalMedia + media;

                    dt.Rows.Add(qryM.NO_FANTAS_EMP, String.Format("{0:N2}", parcSitu[0]), String.Format("{0:N1}", percSitu[0]), String.Format("{0:N2}", parcSitu[1]), String.Format("{0:N1}", percSitu[1]), String.Format("{0:N2}", parcSitu[2]),
                    String.Format("{0:N1}", percSitu[2]), String.Format("{0:N2}", parcSitu[3]), String.Format("{0:N1}", percSitu[3]), String.Format("{0:N2}", parcSitu[4]), String.Format("{0:N1}", percSitu[4]), String.Format("{0:N2}", parcSitu[5]),
                    String.Format("{0:N1}", percSitu[5]), String.Format("{0:N2}", parcSitu[6]), String.Format("{0:N1}", percSitu[6]), String.Format("{0:N2}", parcSitu[7]), String.Format("{0:N1}", percSitu[7]), String.Format("{0:N2}", parcSitu[8]),
                    String.Format("{0:N1}", percSitu[8]), String.Format("{0:N2}", parcSitu[9]), String.Format("{0:N1}", percSitu[9]), String.Format("{0:N2}", parcSitu[10]), String.Format("{0:N1}", percSitu[10]),
                    String.Format("{0:N2}", parcSitu[11]), String.Format("{0:N1}", percSitu[11]), String.Format("{0:N2}", media));
                }
                else
                {
                    decimal media = (parcSitu[0] + parcSitu[1] + parcSitu[2] + parcSitu[3] + parcSitu[4] + parcSitu[5] + parcSitu[6] + parcSitu[7] + parcSitu[8] + parcSitu[9] + parcSitu[10] + parcSitu[11]) / (qryGrid.Count() > 0 ? qryGrid.Count() : 1);

                    totalMedia = totalMedia + media;

                    dt.Rows.Add(qryM.NO_FANTAS_EMP, "S / R", "-", "S / R", "-", "S / R", "-", "S / R", "-", "S / R", "-", "S / R", "-", "S / R", "-", "S / R", "-", "S / R", "-", "S / R", "-", "S / R", "-",
                    "S / R", "-", String.Format("{0:N2}", media));
                }
            }
            
            grdResulParam.Columns.Clear();

            BoundField bfUnid = new BoundField();
            bfUnid.DataField = "NO_FANTAS_EMP";
            bfUnid.HeaderText = "Unidade";
            bfUnid.ItemStyle.Width = 370;
            grdResulParam.Columns.Add(bfUnid);
            
            BoundField bfS1 = new BoundField();
            bfS1.DataField = "S1";
            bfS1.HeaderText = lblSig1.Text;
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
            bfS2.HeaderText = lblSig2.Text;
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
            bfS3.HeaderText = lblSig3.Text;
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
            bfS4.HeaderText = lblSig4.Text;
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
            bfS5.HeaderText = lblSig5.Text;
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
            bfS6.HeaderText = lblSig6.Text;
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
            bfS7.HeaderText = lblSig7.Text;
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
            bfS8.HeaderText = lblSig8.Text;
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
            bfS9.HeaderText = lblSig9.Text;
            bfS9.ItemStyle.Width = 25;
            bfS9.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfS9);

            BoundField bfP9 = new BoundField();
            bfP9.DataField = "P9";
            bfP9.HeaderText = "%";
            bfP9.ItemStyle.Width = 25;
            bfP9.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfP9);

            BoundField bfS10 = new BoundField();
            bfS10.DataField = "S10";
            bfS10.HeaderText = lblSig10.Text;
            bfS10.ItemStyle.Width = 25;
            bfS10.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfS10);

            BoundField bfP10 = new BoundField();
            bfP10.DataField = "P10";
            bfP10.HeaderText = "%";
            bfP10.ItemStyle.Width = 25;
            bfP10.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfP10);

            BoundField bfS11 = new BoundField();
            bfS11.DataField = "S11";
            bfS11.HeaderText = lblSig11.Text;
            bfS11.ItemStyle.Width = 25;
            bfS11.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfS11);

            BoundField bfP11 = new BoundField();
            bfP11.DataField = "P11";
            bfP11.HeaderText = "%";
            bfP11.ItemStyle.Width = 25;
            bfP11.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfP11);

            BoundField bfS12 = new BoundField();
            bfS12.DataField = "S12";
            bfS12.HeaderText = lblSig12.Text;
            bfS12.ItemStyle.Width = 25;
            bfS12.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfS12);

            BoundField bfP12 = new BoundField();
            bfP12.DataField = "P12";
            bfP12.HeaderText = "%";
            bfP12.ItemStyle.Width = 25;
            bfP12.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfP12);

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
        }

        protected void ddlTpUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaUnidades();
        }
    }
}
