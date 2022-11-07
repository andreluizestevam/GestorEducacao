//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: DESEMPENHO, ÍNDICES E ESTATÍSTICAS
// SUBMÓDULO: ÍNDICES E INDICADORES (COLABORADORES)
// OBJETIVO:  MAPA DE DISTRIBUIÇÃO DE COLABORADORES POR REGIÃO
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
    public partial class MapaDistColRegiao : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CarregaCidades();
                CarregaBairros();
                CarregaTpUnidade();
                CarregaUnidades();
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
        /// Método que carrega o dropdown de Cidades
        /// </summary>
        private void CarregaCidades()
        {
            ddlCidade.DataSource = TB904_CIDADE.RetornaPeloUF(LoginAuxili.CO_UF_INSTITUICAO);

            ddlCidade.DataTextField = "NO_CIDADE";
            ddlCidade.DataValueField = "CO_CIDADE";
            ddlCidade.DataBind();

            ddlCidade.Items.Insert(0, new ListItem("Todas", "T"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Bairros
        /// </summary>
        private void CarregaBairros()
        {
            int coCidade = ddlCidade.SelectedValue != "T" ? int.Parse(ddlCidade.SelectedValue) : 0;

            ddlBairro.Items.Clear();

            if (coCidade != 0)
            {
                ddlBairro.DataSource = TB905_BAIRRO.RetornaPelaCidade(coCidade);

                ddlBairro.DataTextField = "NO_BAIRRO";
                ddlBairro.DataValueField = "CO_BAIRRO";
                ddlBairro.DataBind();
            }

            ddlBairro.Items.Insert(0, new ListItem("Todos", "T"));
        }
        #endregion

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            grdResulParam.DataSource = null;
            grdResulParam.DataBind();

            int coCidade = ddlCidade.SelectedValue != "T" ? int.Parse(ddlCidade.SelectedValue) : 0;
            int coBairro = ddlBairro.SelectedValue != "T" ? int.Parse(ddlBairro.SelectedValue) : 0;
            int coUnidade = ddlUnidadeEscolar.SelectedValue != "T" ? int.Parse(ddlUnidadeEscolar.SelectedValue) : 0;
            int coTpUnidade = ddlTpUnidade.SelectedValue != "T" ? int.Parse(ddlTpUnidade.SelectedValue) : 0;

            DataTable dt = new DataTable();

            dt.Columns.Add("NO_BAIRRO");
            dt.Columns.Add("ATI");
            dt.Columns.Add("ATE");
            dt.Columns.Add("FCE");
            dt.Columns.Add("FES");
            dt.Columns.Add("LFR");
            dt.Columns.Add("LME");
            dt.Columns.Add("LMA");
            dt.Columns.Add("SUS");
            dt.Columns.Add("TRE");
            dt.Columns.Add("FER");
            dt.Columns.Add("TOTAL");

            var qryBairros = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                              join tb905 in TB905_BAIRRO.RetornaTodosRegistros() on tb03.CO_BAIRRO equals tb905.CO_BAIRRO
                              where (coCidade != 0 ? tb03.CO_CIDADE == coCidade : coCidade == 0) && (coBairro != 0 ? tb03.CO_BAIRRO == coBairro : coBairro == 0)
                              && (coUnidade != 0 ? tb03.TB25_EMPRESA.CO_EMP == coUnidade : coUnidade == 0)
                              && (coTpUnidade != 0 ? tb03.TB25_EMPRESA.TB24_TPEMPRESA.CO_TIPOEMP == coTpUnidade : coTpUnidade == 0)
                              && tb03.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                              select new { tb905.NO_BAIRRO, tb905.CO_BAIRRO }).Distinct().OrderBy( c => c.NO_BAIRRO );

            int[] totalSitu = new int[10];

            foreach (var qryB in qryBairros)
            {
                int[] parcSitu = new int[10];
                
                var qryGrid = from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                              where (coCidade != 0 ? tb03.CO_CIDADE == coCidade : coCidade == 0) && (tb03.CO_BAIRRO == qryB.CO_BAIRRO)
                              && (coUnidade != 0 ? tb03.TB25_EMPRESA.CO_EMP == coUnidade : coUnidade == 0)
                              && (coTpUnidade != 0 ? tb03.TB25_EMPRESA.TB24_TPEMPRESA.CO_TIPOEMP == coTpUnidade : coTpUnidade == 0)
                              && tb03.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                              group tb03 by tb03.CO_SITU_COL into g
                              orderby g.Key
                              select new { CO_SITU_COL = g.Key, total = g.Count() };

                foreach (var qryG in qryGrid)
                {
                    if (qryG.CO_SITU_COL == "ATI")
                    {
                        parcSitu[0] = qryG.total;
                        totalSitu[0] = totalSitu[0] + qryG.total;
                    }
                    else if (qryG.CO_SITU_COL == "ATE")
                    {
                        parcSitu[1] = qryG.total;
                        totalSitu[1] = totalSitu[1] + qryG.total;
                    }
                    else if (qryG.CO_SITU_COL == "FCE")
                    {
                        parcSitu[2] = qryG.total;
                        totalSitu[2] = totalSitu[2] + qryG.total;
                    }
                    else if (qryG.CO_SITU_COL == "FES")
                    {
                        parcSitu[3] = qryG.total;
                        totalSitu[3] = totalSitu[3] + qryG.total;
                    }
                    else if (qryG.CO_SITU_COL == "LFR")
                    {
                        parcSitu[4] = qryG.total;
                        totalSitu[4] = totalSitu[4] + qryG.total;
                    }
                    else if (qryG.CO_SITU_COL == "LME")
                    {
                        parcSitu[5] = qryG.total;
                        totalSitu[5] = totalSitu[5] + qryG.total;
                    }
                    else if (qryG.CO_SITU_COL == "LMA")
                    {
                        parcSitu[6] = qryG.total;
                        totalSitu[6] = totalSitu[6] + qryG.total;
                    }
                    else if (qryG.CO_SITU_COL == "SUS")
                    {
                        parcSitu[7] = qryG.total;
                        totalSitu[7] = totalSitu[7] + qryG.total;
                    }
                    else if (qryG.CO_SITU_COL == "TRE")
                    {
                        parcSitu[8] = qryG.total;
                        totalSitu[8] = totalSitu[8] + qryG.total;
                    }
                    else if (qryG.CO_SITU_COL == "FER")
                    {
                        parcSitu[9] = qryG.total;
                        totalSitu[9] = totalSitu[9] + qryG.total;
                    }

                }

                int total = parcSitu[0] + parcSitu[1] + parcSitu[2] + parcSitu[3] + parcSitu[4] + parcSitu[5] + parcSitu[6] + parcSitu[7] + parcSitu[8] + parcSitu[9];

                dt.Rows.Add(qryB.NO_BAIRRO, parcSitu[0], parcSitu[1], parcSitu[2], parcSitu[3], parcSitu[4], parcSitu[5], parcSitu[6], parcSitu[7], parcSitu[8], parcSitu[9], total);
            }

            lblTotATI.Text = totalSitu[0].ToString();
            lblTotATE.Text = totalSitu[1].ToString();
            lblTotFCE.Text = totalSitu[2].ToString();
            lblTotFES.Text = totalSitu[3].ToString();
            lblTotLFR.Text = totalSitu[4].ToString();
            lblTotLME.Text = totalSitu[5].ToString();
            lblTotLMA.Text = totalSitu[6].ToString();
            lblTotSUS.Text = totalSitu[7].ToString();
            lblTotTRE.Text = totalSitu[8].ToString();
            lblTotFER.Text = totalSitu[9].ToString();

            lblTotal.Text = (totalSitu[0] + totalSitu[1] + totalSitu[2] + totalSitu[3] + totalSitu[4] + totalSitu[5] + totalSitu[6] + totalSitu[7] + totalSitu[8] + totalSitu[9]).ToString();

            grdResulParam.DataSource = dt;
            grdResulParam.DataBind();

            liGridMDCR.Visible = true;
            liLegMDCR.Visible = true;
   
            string[] xValues = new string[] { "Atividade Interna", "Atividade Externa", "Cedido", "Estagiário", "Licença Funcional", "Licença Médica", "Licença Maternidade", "Suspenso", "Treinamento", "Férias" };

            Grafico1.Series["Default"].Points.DataBindXY(xValues, totalSitu);
        }

        protected void ddlTpUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaUnidades();
        }

        protected void ddlCidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaBairros();
        }        
    }
}
