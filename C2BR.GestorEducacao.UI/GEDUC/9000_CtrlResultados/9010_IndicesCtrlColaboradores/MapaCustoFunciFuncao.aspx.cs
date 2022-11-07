//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: DESEMPENHO, ÍNDICES E ESTATÍSTICAS
// SUBMÓDULO: ÍNDICES E INDICADORES (COLABORADORES)
// OBJETIVO:  MAPA DE CUSTO FUNCIONAL (SALÁRIO) POR CARGO/FUNÇÃO
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
    public partial class MapaCustoFunciFuncao : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CarregaTpUnidade();
                CarregaUnidades();
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
        #endregion

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            grdResulParam.DataSource = null;
            grdResulParam.DataBind();

            int coTpUnidade = ddlTpUnidade.SelectedValue != "T" ? int.Parse(ddlTpUnidade.SelectedValue) : 0;
            int coUnidade = ddlUnidadeEscolar.SelectedValue != "T" ? int.Parse(ddlUnidadeEscolar.SelectedValue) : 0;
            string coSituAtu = ddlSituacao.SelectedValue != "T" ? ddlSituacao.SelectedValue : "";

            DataTable dt = new DataTable();

            dt.Columns.Add("NO_FUN");
            dt.Columns.Add("COM");
            dt.Columns.Add("PJU");
            dt.Columns.Add("COO");
            dt.Columns.Add("EST");
            dt.Columns.Add("AUT");
            dt.Columns.Add("EFE");
            dt.Columns.Add("CTR");
            dt.Columns.Add("TOTAL");

            var qryFuncoes = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                              join tb15 in TB15_FUNCAO.RetornaTodosRegistros() on tb03.CO_FUN equals tb15.CO_FUN
                              where (coTpUnidade != 0 ? tb03.TB25_EMPRESA.TB24_TPEMPRESA.CO_TIPOEMP == coTpUnidade : coTpUnidade == 0)
                              && (coUnidade != 0 ? tb03.TB25_EMPRESA.CO_EMP == coUnidade : coUnidade == 0)
                              && (coSituAtu != "" ? tb03.CO_SITU_COL == coSituAtu : coSituAtu == "")
                              && tb03.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                              select new { tb15.NO_FUN, tb15.CO_FUN }).Distinct().OrderBy( c => c.NO_FUN );

            decimal[] totalSitu = new decimal[7];

            foreach (var qryF in qryFuncoes)
            {
                decimal[] parcSitu = new decimal[7];                
               
                var qryGrid = from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                              where (coUnidade != 0 ? tb03.TB25_EMPRESA.CO_EMP == coUnidade : coUnidade == 0)
                              && (coTpUnidade != 0 ? tb03.TB25_EMPRESA.TB24_TPEMPRESA.CO_TIPOEMP == coTpUnidade : coTpUnidade == 0)
                              && (coSituAtu != "" ? tb03.CO_SITU_COL == coSituAtu : coSituAtu == "")
                              && (tb03.CO_FUN == qryF.CO_FUN) && tb03.VL_SALAR_COLAB != null
                              && tb03.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                              group tb03 by tb03.CO_TPCON into g
                              orderby g.Key
                              select new { CO_TPCON = g.Key, total = g.Sum(p => p.VL_SALAR_COLAB) };                
                
                foreach (var qryG in qryGrid)
                {
                    if (qryG.CO_TPCON == 1)
                    {
                        parcSitu[0] = qryG.total != null ? (decimal)qryG.total : 0;
                        totalSitu[0] = qryG.total != null ? totalSitu[0] + (decimal)qryG.total : totalSitu[0];
                    }
                    else if (qryG.CO_TPCON == 3)
                    {
                        parcSitu[1] = qryG.total != null ? (decimal)qryG.total : 0;
                        totalSitu[1] = qryG.total != null ? totalSitu[1] + (decimal)qryG.total : totalSitu[1];
                    }
                    else if (qryG.CO_TPCON == 4)
                    {
                        parcSitu[2] = qryG.total != null ? (decimal)qryG.total : 0;
                        totalSitu[2] = qryG.total != null ? totalSitu[2] + (decimal)qryG.total : totalSitu[2];
                    }
                    else if (qryG.CO_TPCON == 5)
                    {
                        parcSitu[3] = qryG.total != null ? (decimal)qryG.total : 0;
                        totalSitu[3] = qryG.total != null ? totalSitu[3] + (decimal)qryG.total : totalSitu[3];
                    }
                    else if (qryG.CO_TPCON == 6)
                    {
                        parcSitu[4] = qryG.total != null ? (decimal)qryG.total : 0;
                        totalSitu[4] = qryG.total != null ? totalSitu[4] + (decimal)qryG.total : totalSitu[4];
                    }
                    else if (qryG.CO_TPCON == 7)
                    {
                        parcSitu[5] = qryG.total != null ? (decimal)qryG.total : 0;
                        totalSitu[5] = qryG.total != null ? totalSitu[5] + (decimal)qryG.total : totalSitu[5];
                    }
                    else if (qryG.CO_TPCON == 8)
                    {
                        parcSitu[6] = qryG.total != null ? (decimal)qryG.total : 0;
                        totalSitu[6] = qryG.total != null ? totalSitu[6] + (decimal)qryG.total : totalSitu[6];
                    }  

                }

                decimal total = parcSitu[0] + parcSitu[1] + parcSitu[2] + parcSitu[3] + parcSitu[4] + parcSitu[5] + parcSitu[6];

                dt.Rows.Add(qryF.NO_FUN, parcSitu[0], parcSitu[1], parcSitu[2], parcSitu[3], parcSitu[4], parcSitu[5], parcSitu[6], total);
            }

            lblTotCOM.Text = String.Format("{0:n}",totalSitu[0]);
            lblTotPJU.Text = String.Format("{0:n}",totalSitu[1]);
            lblTotCOO.Text = String.Format("{0:n}",totalSitu[2]);
            lblTotEST.Text = String.Format("{0:n}",totalSitu[3]);
            lblTotAUT.Text = String.Format("{0:n}",totalSitu[4]);
            lblTotEFE.Text = String.Format("{0:n}",totalSitu[5]);
            lblTotCTR.Text = String.Format("{0:n}",totalSitu[6]);

            lblTotal.Text = String.Format("{0:n}",totalSitu[0] + totalSitu[1] + totalSitu[2] + totalSitu[3] + totalSitu[4] + totalSitu[5] + totalSitu[6]);

            grdResulParam.DataSource = dt;
            grdResulParam.DataBind();

            liGridMCFF.Visible = true;
            liLegMCFF.Visible = true;
                    
            string[] xValues = new string[] { "Comissionado", "Pessoa Jurídica", "Cooperado", "Estagiário", "Autônomo", "Efetivo", "Contrato" };

            Grafico1.Series["Default"].Points.DataBindXY(xValues, totalSitu);
        }

        protected void ddlTpUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaUnidades();
        }
    }
}
