//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: CÁLCULO E ATUALIZAÇÃO DE ESTATÍSTICAS
// OBJETIVO: CÁLCULO DA MÉDIA DE DESEMPENHO ESCOLAR POR MODALIDADE
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
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0400_CalculosAtualizacaoEstatistica.F0404_CalculoMediaPorModalidade
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
                CarregaAnos();
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <param name="coEmp">Id da unidade</param>
        /// <param name="modalidade">Id da modalidade</param>
        /// <param name="anoRefer">Ano de referência</param>
        /// <returns></returns>
        private TB952_ESTAT_MODAL RetornaEntidade(int coEmp, int modalidade, int anoRefer)
        {
            return TB952_ESTAT_MODAL.RetornaPeloOcorrEstatModal(coEmp, modalidade, anoRefer);
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
            ddlTpUnidade.DataSource = TB24_TPEMPRESA.RetornaTodosRegistros().Where(t => t.CL_CLAS_EMP == "E").OrderBy(t => t.NO_TIPOEMP);

            ddlTpUnidade.DataTextField = "NO_TIPOEMP";
            ddlTpUnidade.DataValueField = "CO_TIPOEMP";
            ddlTpUnidade.DataBind();

            ddlTpUnidade.Items.Insert(0, new ListItem("Todas", "T"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Ano de Referência
        /// </summary>
        private void CarregaAnos()
        {
            int coEmp = ddlUnidadeEscolar.SelectedValue != "T" ? int.Parse(ddlUnidadeEscolar.SelectedValue) : 0;
            int coTipoEmp = ddlTpUnidade.SelectedValue != "T" ? int.Parse(ddlTpUnidade.SelectedValue) : 0;

            ddlAnoRefer.Items.Clear();

            ddlAnoRefer.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                                      join tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                      on tb43.CO_EMP equals tb25.CO_EMP
                                      where (coEmp != 0 ? tb43.CO_EMP == coEmp : coEmp == 0)
                                      && (coTipoEmp != 0 ? tb25.TB24_TPEMPRESA.CO_TIPOEMP == coTipoEmp : coTipoEmp == 0)
                                      && tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                      select new { tb43.CO_ANO_GRADE }).Distinct().OrderByDescending(g => g.CO_ANO_GRADE);

            ddlAnoRefer.DataTextField = "CO_ANO_GRADE";
            ddlAnoRefer.DataValueField = "CO_ANO_GRADE";
            ddlAnoRefer.DataBind();
        }
        #endregion

//====> Método executado quando botão de pesquisar clicado
        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (ddlAnoRefer.Items.Count == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Ano de Referência é obrigatório");
                return;
            }

            int coTipoEmp = ddlTpUnidade.SelectedValue != "T" ? int.Parse(ddlTpUnidade.SelectedValue) : 0;
            int coEmp = ddlUnidadeEscolar.SelectedValue != "T" ? int.Parse(ddlUnidadeEscolar.SelectedValue) : 0;
            string coAnoRef = ddlAnoRefer.SelectedValue != "T" ? ddlAnoRefer.SelectedValue : "";     
            decimal totalMedia = 0;

            DataTable dt = new DataTable();

            dt.Columns.Add("NO_FANTAS_EMP");
            dt.Columns.Add("T1");
            dt.Columns.Add("T2");
            dt.Columns.Add("T3");
            dt.Columns.Add("T4");
            dt.Columns.Add("T5");
            dt.Columns.Add("T6");
            dt.Columns.Add("T7");
            dt.Columns.Add("T8");
            dt.Columns.Add("MEDIA");

            var qryUnidades = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                               where (coTipoEmp != 0 ? tb25.TB24_TPEMPRESA.CO_TIPOEMP == coTipoEmp : coTipoEmp == 0)
                                && (coEmp != 0 ? tb25.CO_EMP == coEmp : coEmp == 0)
                                && tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                               select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP }).Distinct().OrderBy( u => u.NO_FANTAS_EMP );

            decimal[] totalSitu = new decimal[8];

            foreach (var qryM in qryUnidades)
            {
                decimal[] parcSitu = new decimal[8];
                string[] camposTab = new string[8];
               
                var qryGrid = from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                              where tb079.TB25_EMPRESA.CO_EMP == qryM.CO_EMP && tb079.CO_ANO_REF == coAnoRef
                              group tb079 by tb079.CO_MODU_CUR into g
                              orderby g.Key
                              select new { CO_MODU_CUR = g.Key, total = g.Sum(p => (p.VL_NOTA_BIM1 + p.VL_NOTA_BIM2 + p.VL_NOTA_BIM3 + p.VL_NOTA_BIM4) / 4) };                
                
                int i = 0;                

                foreach (var qryG in qryGrid)
                {
                    var qtdAluno = (from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                                    where tb079.TB25_EMPRESA.CO_EMP == qryM.CO_EMP && tb079.CO_ANO_REF == coAnoRef && tb079.CO_MODU_CUR == qryG.CO_MODU_CUR
                                    select new { tb079.CO_ALU }).Count();

                    var modalidade = TB44_MODULO.RetornaPelaChavePrimaria(qryG.CO_MODU_CUR);

                    if (i == 0)
                    {
                        lblSig1.Text = modalidade.CO_SIGLA_MODU_CUR;
                        lblM1.Text = modalidade.DE_MODU_CUR;
                        parcSitu[0] = qryG.total != null ? (decimal)qryG.total / qtdAluno : 0;
                        camposTab[0] = parcSitu[0] != 0 ? String.Format("{0:N2}", parcSitu[0]) : "-";
                        totalSitu[0] = qryG.total != null ? totalSitu[0] + (decimal)qryG.total / qtdAluno : totalSitu[0];
                    }
                    else if (i == 1)
                    {
                        lblSig2.Text = modalidade.CO_SIGLA_MODU_CUR;
                        lblM2.Text = modalidade.DE_MODU_CUR;
                        parcSitu[1] = qryG.total != null ? (decimal)qryG.total / qtdAluno : 0;
                        camposTab[1] = parcSitu[1] != 0 ? String.Format("{0:N2}", parcSitu[1]) : "-";
                        totalSitu[1] = qryG.total != null ? totalSitu[1] + (decimal)qryG.total / qtdAluno: totalSitu[1];
                    }
                    else if (i == 2)
                    {
                        lblSig3.Text = modalidade.CO_SIGLA_MODU_CUR;
                        lblM3.Text = modalidade.DE_MODU_CUR;
                        parcSitu[2] = qryG.total != null ? (decimal)qryG.total / qtdAluno : 0;
                        camposTab[2] = parcSitu[2] != 0 ? String.Format("{0:N2}", parcSitu[2]) : "-";
                        totalSitu[2] = qryG.total != null ? totalSitu[2] + (decimal)qryG.total / qtdAluno : totalSitu[2];
                    }
                    else if (i == 3)
                    {
                        lblSig4.Text = modalidade.CO_SIGLA_MODU_CUR;
                        lblM4.Text = modalidade.DE_MODU_CUR;
                        parcSitu[3] = qryG.total != null ? (decimal)qryG.total / qtdAluno : 0;
                        camposTab[3] = parcSitu[3] != 0 ? String.Format("{0:N2}", parcSitu[3]) : "-";
                        totalSitu[3] = qryG.total != null ? totalSitu[3] + (decimal)qryG.total / qtdAluno : totalSitu[3];
                    }
                    else if (i == 4)
                    {
                        lblSig5.Text = modalidade.CO_SIGLA_MODU_CUR;
                        lblM5.Text = modalidade.DE_MODU_CUR;
                        parcSitu[4] = qryG.total != null ? (decimal)qryG.total / qtdAluno : 0;
                        camposTab[4] = parcSitu[4] != 0 ? String.Format("{0:N2}", parcSitu[4]) : "-";
                        totalSitu[4] = qryG.total != null ? totalSitu[4] + (decimal)qryG.total / qtdAluno : totalSitu[4];
                    }
                    else if (i == 5)
                    {
                        lblSig6.Text = modalidade.CO_SIGLA_MODU_CUR;
                        lblM6.Text = modalidade.DE_MODU_CUR;
                        parcSitu[5] = qryG.total != null ? (decimal)qryG.total / qtdAluno : 0;
                        camposTab[5] = parcSitu[5] != 0 ? String.Format("{0:N2}", parcSitu[5]) : "-";
                        totalSitu[5] = qryG.total != null ? totalSitu[5] + (decimal)qryG.total / qtdAluno : totalSitu[5];
                    }
                    else if (i == 6)
                    {
                        lblSig7.Text = modalidade.CO_SIGLA_MODU_CUR;
                        lblM7.Text = modalidade.DE_MODU_CUR;
                        parcSitu[6] = qryG.total != null ? (decimal)qryG.total / qtdAluno : 0;
                        camposTab[6] = parcSitu[6] != 0 ? String.Format("{0:N2}", parcSitu[6]) : "-";
                        totalSitu[6] = qryG.total != null ? totalSitu[6] + (decimal)qryG.total / qtdAluno : totalSitu[6];
                    }
                    else if (i == 7)
                    {
                        lblSig8.Text = modalidade.CO_SIGLA_MODU_CUR;
                        lblM8.Text = modalidade.DE_MODU_CUR;
                        parcSitu[7] = qryG.total != null ? (decimal)qryG.total / qtdAluno : 0;
                        camposTab[7] = parcSitu[7] != 0 ? String.Format("{0:N2}", parcSitu[7]) : "-";
                        totalSitu[7] = qryG.total != null ? totalSitu[7] + (decimal)qryG.total / qtdAluno : totalSitu[7];
                    }
                    i++;
                }

                decimal media = (parcSitu[0] + parcSitu[1] + parcSitu[2] + parcSitu[3] + parcSitu[4] + parcSitu[5] + parcSitu[6] + parcSitu[7]) / (qryGrid.Count() > 0 ? qryGrid.Count() : 1);

                totalMedia = totalMedia + media;

                dt.Rows.Add(qryM.NO_FANTAS_EMP, camposTab[0], camposTab[1], camposTab[2],
                    camposTab[3], camposTab[4], camposTab[5], camposTab[6], camposTab[7], media != 0 ? String.Format("{0:N2}", media) : "-");
            }

            grdResulParam.Columns.Clear();

            BoundField bfUnid = new BoundField();
            bfUnid.DataField = "NO_FANTAS_EMP";
            bfUnid.HeaderText = "Unidade";
            bfUnid.ItemStyle.Width = 300;
            grdResulParam.Columns.Add(bfUnid);

            BoundField bfS1 = new BoundField();
            bfS1.DataField = "T1";
            bfS1.HeaderText = lblSig1.Text;
            bfS1.ItemStyle.Width = 25;
            bfS1.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfS1);

            BoundField bfS2 = new BoundField();
            bfS2.DataField = "T2";
            bfS2.HeaderText = lblSig2.Text;
            bfS2.ItemStyle.Width = 25;
            bfS2.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfS2);

            BoundField bfS3 = new BoundField();
            bfS3.DataField = "T3";
            bfS3.HeaderText = lblSig3.Text;
            bfS3.ItemStyle.Width = 25;
            bfS3.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfS3);

            BoundField bfS4 = new BoundField();
            bfS4.DataField = "T4";
            bfS4.HeaderText = lblSig4.Text;
            bfS4.ItemStyle.Width = 25;
            bfS4.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfS4);

            BoundField bfS5 = new BoundField();
            bfS5.DataField = "T5";
            bfS5.HeaderText = lblSig5.Text;
            bfS5.ItemStyle.Width = 25;
            bfS5.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfS5);

            BoundField bfS6 = new BoundField();
            bfS6.DataField = "T6";
            bfS6.HeaderText = lblSig6.Text;
            bfS6.ItemStyle.Width = 25;
            bfS6.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfS6);

            BoundField bfS7 = new BoundField();
            bfS7.DataField = "T7";
            bfS7.HeaderText = lblSig7.Text;
            bfS7.ItemStyle.Width = 25;
            bfS7.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfS7);

            BoundField bfS8 = new BoundField();
            bfS8.DataField = "T8";
            bfS8.HeaderText = lblSig8.Text;
            bfS8.ItemStyle.Width = 25;
            bfS8.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfS8);

            BoundField bfMedia = new BoundField();
            bfMedia.DataField = "MEDIA";
            bfMedia.HeaderText = "Média";
            bfMedia.ItemStyle.Width = 25;
            bfMedia.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfMedia);   

            grdResulParam.DataSource = dt;
            grdResulParam.DataBind();

            lblTotT1.Text = totalSitu[0] != 0 ? String.Format("{0:N2}", totalSitu[0] / (grdResulParam.Rows.Count > 0 ? grdResulParam.Rows.Count : 1)) : "-";
            lblTotT2.Text = totalSitu[1] != 0 ? String.Format("{0:N2}", totalSitu[1] / (grdResulParam.Rows.Count > 0 ? grdResulParam.Rows.Count : 1)) : "-";
            lblTotT3.Text = totalSitu[2] != 0 ? String.Format("{0:N2}", totalSitu[2] / (grdResulParam.Rows.Count > 0 ? grdResulParam.Rows.Count : 1)) : "-";
            lblTotT4.Text = totalSitu[3] != 0 ? String.Format("{0:N2}", totalSitu[3] / (grdResulParam.Rows.Count > 0 ? grdResulParam.Rows.Count : 1)) : "-";
            lblTotT5.Text = totalSitu[4] != 0 ? String.Format("{0:N2}", totalSitu[4] / (grdResulParam.Rows.Count > 0 ? grdResulParam.Rows.Count : 1)) : "-";
            lblTotT6.Text = totalSitu[5] != 0 ? String.Format("{0:N2}", totalSitu[5] / (grdResulParam.Rows.Count > 0 ? grdResulParam.Rows.Count : 1)) : "-";
            lblTotT7.Text = totalSitu[6] != 0 ? String.Format("{0:N2}", totalSitu[6] / (grdResulParam.Rows.Count > 0 ? grdResulParam.Rows.Count : 1)) : "-";
            lblTotT8.Text = totalSitu[7] != 0 ? String.Format("{0:N2}", totalSitu[7] / (grdResulParam.Rows.Count > 0 ? grdResulParam.Rows.Count : 1)) : "-";
            
            lblTotMed.Text = String.Format("{0:N2}", totalMedia / (grdResulParam.Rows.Count > 0 ? grdResulParam.Rows.Count : 1));

            liGridMCFF.Visible = true;
            liLegMCFF.Visible = true;
        }

//====> Método executado quando botão de gravar médias clicado
        protected void btnGravarMedias_Click(object sender, EventArgs e)
        {
            if (!liGridMCFF.Visible)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Primeiro faça a pesquisa.");
                return; 
            }

            int coTipoEmp = ddlTpUnidade.SelectedValue != "T" ? int.Parse(ddlTpUnidade.SelectedValue) : 0;
            int coEmp = ddlUnidadeEscolar.SelectedValue != "T" ? int.Parse(ddlUnidadeEscolar.SelectedValue) : 0;
            string coAnoRef = ddlAnoRefer.SelectedValue != "T" ? ddlAnoRefer.SelectedValue : "";
            bool ocorrReg = false;
            bool ocorrAlterReg = false;

            var qryUnidades = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                               where (coTipoEmp != 0 ? tb25.TB24_TPEMPRESA.CO_TIPOEMP == coTipoEmp : coTipoEmp == 0)
                                && (coEmp != 0 ? tb25.CO_EMP == coEmp : coEmp == 0)
                                && tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                               select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP }).Distinct().OrderBy(p => p.NO_FANTAS_EMP);

            foreach (var qryM in qryUnidades)
            {
                var qryGrid = from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                              where tb079.TB25_EMPRESA.CO_EMP == qryM.CO_EMP && tb079.CO_ANO_REF == coAnoRef
                              group tb079 by tb079.CO_MODU_CUR into g
                              orderby g.Key
                              select new { CO_MODU_CUR = g.Key, total = g.Sum(p => ((p.VL_NOTA_BIM1) + (p.VL_NOTA_BIM2) + (p.VL_NOTA_BIM3) + (p.VL_NOTA_BIM4)) / 4), totalB1 = g.Sum(p => (p.VL_NOTA_BIM1)), totalB2 = g.Sum(p => (p.VL_NOTA_BIM2)), totalB3 = g.Sum(p => (p.VL_NOTA_BIM3)), totalB4 = g.Sum(p => (p.VL_NOTA_BIM4)) };                             

                foreach (var qryG in qryGrid)
                {
                    var qtdAluno = (from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                                    where tb079.TB25_EMPRESA.CO_EMP == qryM.CO_EMP && tb079.CO_ANO_REF == coAnoRef && tb079.CO_MODU_CUR == qryG.CO_MODU_CUR
                                    select new { tb079.CO_ALU }).Count();

                    TB952_ESTAT_MODAL desEsc = RetornaEntidade(qryM.CO_EMP, qryG.CO_MODU_CUR, int.Parse(coAnoRef));

                    if (desEsc == null)
                    {
                        ocorrReg = true;

                        desEsc = new TB952_ESTAT_MODAL();

                        desEsc.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(qryM.CO_EMP);
                        desEsc.TB44_MODULO = TB44_MODULO.RetornaPelaChavePrimaria(qryG.CO_MODU_CUR);
                        desEsc.CO_ANO_REF = int.Parse(coAnoRef);
                        desEsc.VL_MEDIA_BIM1 = qryG.totalB1 != null ? (decimal?)qryG.totalB1 / qtdAluno : null;
                        desEsc.VL_MEDIA_BIM2 = qryG.totalB2 != null ? (decimal?)qryG.totalB2 / qtdAluno : null;
                        desEsc.VL_MEDIA_BIM3 = qryG.totalB3 != null ? (decimal?)qryG.totalB3 / qtdAluno : null;
                        desEsc.VL_MEDIA_BIM4 = qryG.totalB4 != null ? (decimal?)qryG.totalB4 / qtdAluno : null;
                        desEsc.VL_MEDIA_ANO = qryG.total != null ? (decimal?)qryG.total / qtdAluno : null;
                        desEsc.DT_ULTIM_CALC = DateTime.Now;
                        desEsc.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_UNID_FUNC, LoginAuxili.CO_COL);
                        desEsc.CO_SITU_CALC = "A";
                        desEsc.DT_SITU_CALC = DateTime.Now;

                        CurrentPadraoCadastros.CurrentEntity = desEsc;
                    }
                    else
                    {
                        if (desEsc.CO_SITU_CALC == "A")
	                    {
                            ocorrAlterReg = true;

                            desEsc.VL_MEDIA_BIM1 = qryG.totalB1 != null ? (decimal?)qryG.totalB1 / qtdAluno : null;
                            desEsc.VL_MEDIA_BIM2 = qryG.totalB2 != null ? (decimal?)qryG.totalB2 / qtdAluno : null;
                            desEsc.VL_MEDIA_BIM3 = qryG.totalB3 != null ? (decimal?)qryG.totalB3 / qtdAluno : null;
                            desEsc.VL_MEDIA_BIM4 = qryG.totalB4 != null ? (decimal?)qryG.totalB4 / qtdAluno : null;
                            desEsc.VL_MEDIA_ANO = qryG.total != null ? (decimal?)qryG.total / qtdAluno : null;
                            desEsc.DT_ULTIM_CALC = DateTime.Now;
                            desEsc.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_UNID_FUNC, LoginAuxili.CO_COL);

                            CurrentPadraoCadastros.CurrentEntity = desEsc;     
	                    }                             
                    }	                       
                }                
            }

            GestorEntities.CurrentContext.SaveChanges();

            if (ocorrAlterReg)
                AuxiliPagina.RedirecionaParaPaginaSucesso("A atualização de dados foi PARCIAL - Já existem Unidades com ANO DE REFERÊNCIA selecionado homologado.", Request.Url.AbsoluteUri);
            else
            {
                if (ocorrReg)
                    AuxiliPagina.RedirecionaParaPaginaSucesso("Os dados de TODAS as Unidades foram atualizados com sucesso.", Request.Url.AbsoluteUri);
                else
                    AuxiliPagina.RedirecionaParaPaginaErro("NENHUM dado foi atualizado - TODAS as Unidades estão com o ANO DE REFERÊNCIA selecionado homologado.", Request.Url.AbsoluteUri);
            }
        }        

        protected void ddlTpUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaUnidades();
            CarregaAnos();
        }

        protected void ddlUnidadeEscolar_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAnos();
        }
    }
}
