//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: DESEMPENHO, ÍNDICES E ESTATÍSTICAS
// SUBMÓDULO: ÍNDICES E INDICADORES (COLABORADORES)
// OBJETIVO:  MAPA ANUAL DE EVASÃO FUNCIONAL POR UNIDADE ESCOLAR
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
namespace C2BR.GestorEducacao.UI.GEDUC.F9000_CtrlResultados.F9010_IndicesCtrlColaboradores
{
    public partial class MapaEvasaoFunciUnidEsc : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CarregaTpUnidade();
                CarregaAnos();
            }
        }
        #endregion

        #region Carregamento DropDown

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
            ddlAnoRefer.Items.Clear();

            for (int i = DateTime.Now.Year ; i >= DateTime.Now.Year - 10; i--)
            {
                ddlAnoRefer.Items.Add(new ListItem(i.ToString(), i.ToString()));   
            }
        }
        #endregion

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            grdResulParam.DataSource = null;
            grdResulParam.DataBind();

            int coTpUnidade = ddlTpUnidade.SelectedValue != "T" ? int.Parse(ddlTpUnidade.SelectedValue) : 0;
            int anoRefer = ddlAnoRefer.SelectedValue != "T" ? int.Parse(ddlAnoRefer.SelectedValue) : 0;

            DataTable dt = new DataTable();

            dt.Columns.Add("NO_FANTAS_EMP");
            dt.Columns.Add("JAN");
            dt.Columns.Add("FEV");
            dt.Columns.Add("MAR");
            dt.Columns.Add("ABR");
            dt.Columns.Add("MAI");
            dt.Columns.Add("JUN");
            dt.Columns.Add("JUL");
            dt.Columns.Add("AGO");
            dt.Columns.Add("SET");
            dt.Columns.Add("OUT");
            dt.Columns.Add("NOV");
            dt.Columns.Add("DEZ");
            dt.Columns.Add("TOTAL");

            var qryFuncoes = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                              where (coTpUnidade != 0 ? tb03.TB25_EMPRESA.TB24_TPEMPRESA.CO_TIPOEMP == coTpUnidade : coTpUnidade == 0) 
                              && tb03.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                             select new { tb03.TB25_EMPRESA.NO_FANTAS_EMP, tb03.TB25_EMPRESA.CO_EMP }).Distinct().OrderBy( c => c.NO_FANTAS_EMP );

            int[] totalSitu = new int[12];

            foreach (var qryB in qryFuncoes)
            {
                int[] parcSitu = new int[12];
                
                var qryGrid = from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                              join tb199 in TB199_FREQ_FUNC.RetornaTodosRegistros() on tb03.CO_COL equals tb199.TB03_COLABOR.CO_COL
                              where tb03.TB25_EMPRESA.CO_EMP == tb199.TB03_COLABOR.TB25_EMPRESA.CO_EMP
                              && (coTpUnidade != 0 ? tb03.TB25_EMPRESA.TB24_TPEMPRESA.CO_TIPOEMP == coTpUnidade : coTpUnidade == 0)
                              && (tb03.TB25_EMPRESA.CO_EMP == qryB.CO_EMP) && tb199.TP_FREQ == "F" && tb199.DT_FREQ.Year == anoRefer && tb199.STATUS == "A"
                              group tb03 by tb199.DT_FREQ.Month into g
                              orderby g.Key
                              select new { MES = g.Key, total = g.Count() };

                foreach (var qryG in qryGrid)
                {
                    if (qryG.MES == 1)
                    {
                        parcSitu[0] = qryG.total;
                        totalSitu[0] = totalSitu[0] + qryG.total;
                    }
                    else if (qryG.MES == 2)
                    {
                        parcSitu[1] = qryG.total;
                        totalSitu[1] = totalSitu[1] + qryG.total;
                    }
                    else if (qryG.MES == 3)
                    {
                        parcSitu[2] = qryG.total;
                        totalSitu[2] = totalSitu[2] + qryG.total;
                    }
                    else if (qryG.MES == 4)
                    {
                        parcSitu[3] = qryG.total;
                        totalSitu[3] = totalSitu[3] + qryG.total;
                    }
                    if (qryG.MES == 5)
                    {
                        parcSitu[4] = qryG.total;
                        totalSitu[4] = totalSitu[4] + qryG.total;
                    }
                    else if (qryG.MES == 6)
                    {
                        parcSitu[5] = qryG.total;
                        totalSitu[5] = totalSitu[5] + qryG.total;
                    }
                    else if (qryG.MES == 7)
                    {
                        parcSitu[6] = qryG.total;
                        totalSitu[6] = totalSitu[6] + qryG.total;
                    }
                    else if (qryG.MES == 8)
                    {
                        parcSitu[7] = qryG.total;
                        totalSitu[7] = totalSitu[7] + qryG.total;
                    }
                    else if (qryG.MES == 9)
                    {
                        parcSitu[8] = qryG.total;
                        totalSitu[8] = totalSitu[8] + qryG.total;
                    }
                    else if (qryG.MES == 10)
                    {
                        parcSitu[9] = qryG.total;
                        totalSitu[9] = totalSitu[9] + qryG.total;
                    }
                    else if (qryG.MES == 11)
                    {
                        parcSitu[10] = qryG.total;
                        totalSitu[10] = totalSitu[10] + qryG.total;
                    }
                    else if (qryG.MES == 12)
                    {
                        parcSitu[11] = qryG.total;
                        totalSitu[11] = totalSitu[11] + qryG.total;
                    }
                }

                int total = parcSitu[0] + parcSitu[1] + parcSitu[2] + parcSitu[3] + parcSitu[4] + parcSitu[5] + parcSitu[6] + parcSitu[7] + parcSitu[8] + parcSitu[9] + parcSitu[10] + parcSitu[11];

                dt.Rows.Add(qryB.NO_FANTAS_EMP, parcSitu[0], parcSitu[1], parcSitu[2], parcSitu[3], parcSitu[4], parcSitu[5], parcSitu[6], parcSitu[7], parcSitu[8], parcSitu[9], parcSitu[10], parcSitu[11], total);
            }

            lblTotJAN.Text = totalSitu[0].ToString();
            lblTotFEV.Text = totalSitu[1].ToString();
            lblTotMAR.Text = totalSitu[2].ToString();
            lblTotABR.Text = totalSitu[3].ToString();
            lblTotMAI.Text = totalSitu[4].ToString();
            lblTotJUN.Text = totalSitu[5].ToString();
            lblTotJUL.Text = totalSitu[6].ToString();
            lblTotAGO.Text = totalSitu[7].ToString();
            lblTotSET.Text = totalSitu[8].ToString();
            lblTotOUT.Text = totalSitu[9].ToString();
            lblTotNOV.Text = totalSitu[10].ToString();
            lblTotDEZ.Text = totalSitu[11].ToString();

            lblTotal.Text = (totalSitu[0] + totalSitu[1] + totalSitu[2] + totalSitu[3] + totalSitu[4] + totalSitu[5] + totalSitu[6] + totalSitu[7] + totalSitu[8] + totalSitu[9] + totalSitu[10] + totalSitu[11]).ToString();

            grdResulParam.DataSource = dt;
            grdResulParam.DataBind();

            liGridMEFUE.Visible = true;
            liLegMEFUE.Visible = true;
        
            string[] xValues = new string[] { "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro" };

            Grafico1.Series["Default"].Points.DataBindXY(xValues, totalSitu);
        }
    }
}
