//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: CÁLCULO E ATUALIZAÇÃO DE ESTATÍSTICAS
// OBJETIVO: CÁLCULO E ATUALIZAÇÃO DE SALÁRIO DE COLABORADORES NA BASE DE DADOS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Linq;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;
using System.Collections.Generic;
using System.Data;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0400_CalculosAtualizacaoEstatistica.F0401_CalculoSalarioColabor
{
    public partial class Cadastro : System.Web.UI.Page
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
                ddlUnidadeEscolar.DataSource = TB25_EMPRESA.RetornaTodosRegistros().Where(e => e.TB24_TPEMPRESA.CO_TIPOEMP == coTipoEmp && e.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO).OrderBy(e => e.NO_FANTAS_EMP);

                ddlUnidadeEscolar.DataTextField = "NO_FANTAS_EMP";
                ddlUnidadeEscolar.DataValueField = "CO_EMP";
                ddlUnidadeEscolar.DataBind();
            }

            ddlUnidadeEscolar.Items.Insert(0, new ListItem("Todas", "T"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Tipos de Unidades Escolares
        /// </summary>
        private void CarregaTpUnidade()
        {
            ddlTpUnidade.DataSource = TB24_TPEMPRESA.RetornaTodosRegistros().Where(p => p.CL_CLAS_EMP == "E").OrderBy(t => t.NO_TIPOEMP);

            ddlTpUnidade.DataTextField = "NO_TIPOEMP";
            ddlTpUnidade.DataValueField = "CO_TIPOEMP";
            ddlTpUnidade.DataBind();

            ddlTpUnidade.Items.Insert(0, new ListItem("Todos", "T"));
        }
        #endregion

//====> Método executado quando botão de calculo de salário clicado
        protected void btnCalcSalario_Click(object sender, EventArgs e)
        {
            if (!liGridCS.Visible)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Primeiro faça a pesquisa, para depois salvar.");
                return;
            }

            if (Page.IsValid)
            {
                int coEmp = ddlUnidadeEscolar.SelectedValue != "T" ? int.Parse(ddlUnidadeEscolar.SelectedValue) : 0;
                int coTipoEmp = ddlTpUnidade.SelectedValue != "T" ? int.Parse(ddlTpUnidade.SelectedValue) : 0;

                var listaTb03 = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                                 where (coEmp != 0 ? tb03.TB25_EMPRESA.CO_EMP == coEmp : coEmp == 0)
                                 && (coTipoEmp != 0 ? tb03.TB25_EMPRESA.TB24_TPEMPRESA.CO_TIPOEMP == coTipoEmp : coTipoEmp == 0)
                                 && tb03.CO_TPCAL != null && tb03.VL_SALAR_COL != null && tb03.CO_TPCON != null
                                 && tb03.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                 select new { tb03.TB25_EMPRESA.CO_EMP, tb03.CO_COL }).ToList();

                if (listaTb03.Count > 0)
                {
                    foreach (var tb03 in listaTb03)
                    {
                        TB03_COLABOR colaborSelec = TB03_COLABOR.RetornaPelaChavePrimaria(tb03.CO_EMP, tb03.CO_COL);

                        if (colaborSelec.CO_TPCAL == 1)
                            colaborSelec.VL_SALAR_COLAB = 30 * (decimal)colaborSelec.VL_SALAR_COL;
                        else if (colaborSelec.CO_TPCAL == 2)
                            colaborSelec.VL_SALAR_COLAB = 4 * (decimal)colaborSelec.VL_SALAR_COL;
                        else if (colaborSelec.CO_TPCAL == 3)
                            colaborSelec.VL_SALAR_COLAB = (decimal)colaborSelec.VL_SALAR_COL;
                        else if (colaborSelec.CO_TPCAL == 4)
                            colaborSelec.VL_SALAR_COLAB = colaborSelec.NU_CARGA_HORARIA * (decimal)colaborSelec.VL_SALAR_COL;

                        GestorEntities.SaveOrUpdate(colaborSelec);
                    }

                    AuxiliPagina.RedirecionaParaPaginaSucesso("Salários Calculados com sucesso", Request.Url.AbsoluteUri);
                }
                else
                    AuxiliPagina.RedirecionaParaPaginaErro("Nenhum salário atualizado com sucesso", Request.Url.AbsoluteUri);
                                 
            }
        }

//====> Método executado quando botão de pesquisa clicado
        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            int coTipoEmp = ddlTpUnidade.SelectedValue != "T" ? int.Parse(ddlTpUnidade.SelectedValue) : 0;
            int coEmp = ddlUnidadeEscolar.SelectedValue != "T" ? int.Parse(ddlUnidadeEscolar.SelectedValue) : 0;

            DataTable dt = new DataTable();

            dt.Columns.Add("NO_FANTAS_EMP");
            dt.Columns.Add("COM");
            dt.Columns.Add("PJU");
            dt.Columns.Add("COO");
            dt.Columns.Add("EST");
            dt.Columns.Add("AUT");
            dt.Columns.Add("EFE");
            dt.Columns.Add("CTR");
            dt.Columns.Add("TOTAL");

            var qryFuncoes = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                              where (coTipoEmp != 0 ? tb03.TB25_EMPRESA.TB24_TPEMPRESA.CO_TIPOEMP == coTipoEmp : coTipoEmp == 0)
                              && (coEmp != 0 ? tb03.TB25_EMPRESA.CO_EMP == coEmp : coEmp == 0)
                              && tb03.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                              select new
                              {
                                  tb03.TB25_EMPRESA.NO_FANTAS_EMP, tb03.TB25_EMPRESA.CO_EMP
                              }).Distinct().OrderBy( c => c.NO_FANTAS_EMP );

            int[] totalSitu = new int[7];

            foreach (var qryF in qryFuncoes)
            {
                int[] parcSitu = new int[7];

                var qryGrid = from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                              where (tb03.TB25_EMPRESA.CO_EMP == qryF.CO_EMP) && tb03.CO_TPCAL != null && tb03.VL_SALAR_COL != null
                              && tb03.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                              group tb03 by tb03.CO_TPCON into g
                              orderby g.Key
                              select new { CO_TPCON = g.Key, total = g.Count() };

                foreach (var qryG in qryGrid)
                {
                    if (qryG.CO_TPCON == 1)
                    {
                        parcSitu[0] = qryG.total;
                        totalSitu[0] = totalSitu[0] + qryG.total;
                    }
                    else if (qryG.CO_TPCON == 3)
                    {
                        parcSitu[1] = qryG.total;
                        totalSitu[1] = totalSitu[1] + qryG.total;
                    }
                    else if (qryG.CO_TPCON == 4)
                    {
                        parcSitu[2] = qryG.total;
                        totalSitu[2] = totalSitu[2] + qryG.total;
                    }
                    else if (qryG.CO_TPCON == 5)
                    {
                        parcSitu[3] = qryG.total;
                        totalSitu[3] = totalSitu[3] + qryG.total;
                    }
                    else if (qryG.CO_TPCON == 6)
                    {
                        parcSitu[4] = qryG.total;
                        totalSitu[4] = totalSitu[4] + qryG.total;
                    }
                    else if (qryG.CO_TPCON == 7)
                    {
                        parcSitu[5] = qryG.total;
                        totalSitu[5] = totalSitu[5] + qryG.total;
                    }
                    else if (qryG.CO_TPCON == 8)
                    {
                        parcSitu[6] = qryG.total;
                        totalSitu[6] = totalSitu[6] + qryG.total;
                    }

                }

                int total = parcSitu[0] + parcSitu[1] + parcSitu[2] + parcSitu[3] + parcSitu[4] + parcSitu[5] + parcSitu[6];

                dt.Rows.Add(qryF.NO_FANTAS_EMP, parcSitu[0], parcSitu[1], parcSitu[2], parcSitu[3], parcSitu[4], parcSitu[5], parcSitu[6], total);
            }

            lblTotCOM.Text = totalSitu[0].ToString();
            lblTotPJU.Text = totalSitu[1].ToString();
            lblTotCOO.Text = totalSitu[2].ToString();
            lblTotEST.Text = totalSitu[3].ToString();
            lblTotAUT.Text = totalSitu[4].ToString();
            lblTotEFE.Text = totalSitu[5].ToString();
            lblTotCTR.Text = totalSitu[6].ToString();

            lblTotal.Text = (totalSitu[0] + totalSitu[1] + totalSitu[2] + totalSitu[3] + totalSitu[4] + totalSitu[5] + totalSitu[6]).ToString();

            grdResulParam.DataSource = dt;
            grdResulParam.DataBind();

            liGridCS.Visible = true;
            liLegCS.Visible = true;
        }

        protected void ddlTpUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaUnidades();
        }
    }
}