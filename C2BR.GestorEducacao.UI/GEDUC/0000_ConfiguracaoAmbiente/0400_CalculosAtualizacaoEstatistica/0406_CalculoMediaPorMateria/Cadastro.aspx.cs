//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: CÁLCULO E ATUALIZAÇÃO DE ESTATÍSTICAS
// OBJETIVO: CÁLCULO DA MÉDIA DE DESEMPENHO ESCOLAR POR UNIDADE
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
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0400_CalculosAtualizacaoEstatistica.F0406_CalculoMediaPorMateria
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
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
        /// <param name="idMateria">Id da matéria</param>
        /// <param name="anoRefer">Ano de referência</param>
        /// <returns>Entidade TB954_ESTAT_MATER</returns>
        private TB954_ESTAT_MATER RetornaEntidade(int coEmp, int idMateria, int anoRefer)
        {
            return TB954_ESTAT_MATER.RetornaPeloOcorrEstatMater(coEmp, idMateria, anoRefer);
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaUnidades()
        {
            ddlUnidadeEscolar.DataSource = TB25_EMPRESA.RetornaTodosRegistros().Where( e =>  e.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO ).OrderBy(e => e.NO_FANTAS_EMP);

            ddlUnidadeEscolar.DataTextField = "NO_FANTAS_EMP";
            ddlUnidadeEscolar.DataValueField = "CO_EMP";
            ddlUnidadeEscolar.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Ano de Referência
        /// </summary>
        private void CarregaAnos()
        {
            int coEmp = ddlUnidadeEscolar.SelectedValue != "" ? int.Parse(ddlUnidadeEscolar.SelectedValue) : 0;

            ddlAnoRefer.Items.Clear();

            ddlAnoRefer.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                                      join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb43.CO_EMP equals tb25.CO_EMP
                                      where (coEmp != 0 ? tb43.CO_EMP == coEmp : coEmp == 0)
                                       && tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                      select new { tb43.CO_ANO_GRADE }).Distinct().OrderByDescending(g => g.CO_ANO_GRADE);

            ddlAnoRefer.DataTextField = "CO_ANO_GRADE";
            ddlAnoRefer.DataValueField = "CO_ANO_GRADE";
            ddlAnoRefer.DataBind();

            ddlAnoRefer.Items.Insert(0, new ListItem("Todos", "T"));
        }
        #endregion

//====> Método executado quando botão de pesquisa clicado
        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            int coEmp = ddlUnidadeEscolar.SelectedValue != "" ? int.Parse(ddlUnidadeEscolar.SelectedValue) : 0;
            string coAnoRef = ddlAnoRefer.SelectedValue != "T" ? ddlAnoRefer.SelectedValue : "";
            decimal totalMedia = 0;

            lblT1.Text = "-";
            lblT2.Text = "-";
            lblT3.Text = "-";
            lblT4.Text = "-";
            lblT5.Text = "-";
            lblT6.Text = "-";
            lblT7.Text = "-";
            lblT8.Text = "-";

            DataTable dt = new DataTable();

            dt.Columns.Add("NO_MATERIA");
            dt.Columns.Add("T1");
            dt.Columns.Add("T2");
            dt.Columns.Add("T3");
            dt.Columns.Add("T4");
            dt.Columns.Add("T5");
            dt.Columns.Add("T6");
            dt.Columns.Add("T7");
            dt.Columns.Add("T8");
            dt.Columns.Add("MEDIA");
            
//--------> Lista as matérias da unidade informada
            var qryMaterias = (from tb107 in TB107_CADMATERIAS.RetornaTodosRegistros()
                               join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb107.CO_EMP equals tb25.CO_EMP
                               where (coEmp != 0 ? tb107.CO_EMP == coEmp : coEmp == 0)
                               && tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                               select new { tb107.ID_MATERIA, tb107.NO_MATERIA }).Distinct().OrderBy(u => u.NO_MATERIA);

            decimal[] totalSitu = new decimal[8];

            foreach (var qryM in qryMaterias)
            {
                decimal[] parcSitu = new decimal[8];
                string[] camposTab = new string[8];
               
                var qryGrid = from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                              join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb079.CO_MAT equals tb02.CO_MAT
                              where tb079.TB25_EMPRESA.CO_EMP == coEmp && (coAnoRef != "" ? tb079.CO_ANO_REF == coAnoRef : coAnoRef == "")
                              && tb02.CO_EMP == tb079.CO_EMP && tb02.ID_MATERIA == qryM.ID_MATERIA
                              group tb079 by tb079.CO_ANO_REF into g
                              orderby g.Key
                              select new { CO_ANO_REF = g.Key, total = g.Sum(p => (p.VL_NOTA_BIM1 + p.VL_NOTA_BIM2 + p.VL_NOTA_BIM3 + p.VL_NOTA_BIM4) / 4) };                
                
                int i = 0;

                foreach (var qryG in qryGrid)
                {
                    var qtdAluno = (from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                                    join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb079.CO_MAT equals tb02.CO_MAT
                                    where tb079.TB25_EMPRESA.CO_EMP == coEmp && tb079.CO_ANO_REF == qryG.CO_ANO_REF
                                    && tb02.CO_EMP == tb079.CO_EMP && tb02.ID_MATERIA == qryM.ID_MATERIA
                                    select new { tb079.CO_ALU }).Count();

                    if (i == 0)
                    {
                        lblT1.Text = qryG.CO_ANO_REF;
                        parcSitu[0] = qryG.total != null ? (decimal)qryG.total / qtdAluno : 0;
                        camposTab[0] = parcSitu[0] != 0 ? String.Format("{0:N2}", parcSitu[0]) : "-";
                        totalSitu[0] = qryG.total != null ? totalSitu[0] + (decimal)qryG.total / qtdAluno : totalSitu[0];
                    }
                    else if (i == 1)
                    {
                        lblT2.Text = qryG.CO_ANO_REF;
                        parcSitu[1] = qryG.total != null ? (decimal)qryG.total / qtdAluno : 0;
                        camposTab[1] = parcSitu[1] != 0 ? String.Format("{0:N2}", parcSitu[1]) : "-";
                        totalSitu[1] = qryG.total != null ? totalSitu[1] + (decimal)qryG.total / qtdAluno: totalSitu[1];
                    }
                    else if (i == 2)
                    {
                        lblT3.Text = qryG.CO_ANO_REF;
                        parcSitu[2] = qryG.total != null ? (decimal)qryG.total / qtdAluno : 0;
                        camposTab[2] = parcSitu[2] != 0 ? String.Format("{0:N2}", parcSitu[2]) : "-";
                        totalSitu[2] = qryG.total != null ? totalSitu[2] + (decimal)qryG.total / qtdAluno : totalSitu[2];
                    }
                    else if (i == 3)
                    {
                        lblT4.Text = qryG.CO_ANO_REF;
                        parcSitu[3] = qryG.total != null ? (decimal)qryG.total / qtdAluno : 0;
                        camposTab[3] = parcSitu[3] != 0 ? String.Format("{0:N2}", parcSitu[3]) : "-";
                        totalSitu[3] = qryG.total != null ? totalSitu[3] + (decimal)qryG.total / qtdAluno : totalSitu[3];
                    }
                    else if (i == 4)
                    {
                        lblT5.Text = qryG.CO_ANO_REF;
                        parcSitu[4] = qryG.total != null ? (decimal)qryG.total / qtdAluno : 0;
                        camposTab[4] = parcSitu[4] != 0 ? String.Format("{0:N2}", parcSitu[4]) : "-";
                        totalSitu[4] = qryG.total != null ? totalSitu[4] + (decimal)qryG.total / qtdAluno : totalSitu[4];
                    }
                    else if (i == 5)
                    {
                        lblT6.Text = qryG.CO_ANO_REF;
                        parcSitu[5] = qryG.total != null ? (decimal)qryG.total / qtdAluno : 0;
                        camposTab[5] = parcSitu[5] != 0 ? String.Format("{0:N2}", parcSitu[5]) : "-";
                        totalSitu[5] = qryG.total != null ? totalSitu[5] + (decimal)qryG.total / qtdAluno : totalSitu[5];
                    }
                    else if (i == 6)
                    {
                        lblT7.Text = qryG.CO_ANO_REF;
                        parcSitu[6] = qryG.total != null ? (decimal)qryG.total / qtdAluno : 0;
                        camposTab[6] = parcSitu[6] != 0 ? String.Format("{0:N2}", parcSitu[6]) : "-";
                        totalSitu[6] = qryG.total != null ? totalSitu[6] + (decimal)qryG.total / qtdAluno : totalSitu[6];
                    }
                    else if (i == 7)
                    {
                        lblT8.Text = qryG.CO_ANO_REF;
                        parcSitu[7] = qryG.total != null ? (decimal)qryG.total / qtdAluno : 0;
                        camposTab[7] = parcSitu[7] != 0 ? String.Format("{0:N2}", parcSitu[7]) : "-";
                        totalSitu[7] = qryG.total != null ? totalSitu[7] + (decimal)qryG.total / qtdAluno : totalSitu[7];
                    }
                    i++;
                }

                decimal media = (parcSitu[0] + parcSitu[1] + parcSitu[2] + parcSitu[3] + parcSitu[4] + parcSitu[5] + parcSitu[6] + parcSitu[7]) / (qryGrid.Count() > 0 ? qryGrid.Count() : 1);                

                totalMedia = totalMedia + media;

                dt.Rows.Add(qryM.NO_MATERIA, camposTab[0], camposTab[1], camposTab[2], camposTab[3], camposTab[4], camposTab[5], camposTab[6], camposTab[7], media != 0 ? String.Format("{0:N2}", media) : "-");
            }

            grdResulParam.Columns.Clear();

            BoundField bfUnid = new BoundField();
            bfUnid.DataField = "NO_MATERIA";
            bfUnid.HeaderText = "Matéria";
            bfUnid.ItemStyle.Width = 300;
            grdResulParam.Columns.Add(bfUnid);

            BoundField bfS1 = new BoundField();
            bfS1.DataField = "T1";
            bfS1.HeaderText = lblT1.Text;
            bfS1.ItemStyle.Width = 25;
            bfS1.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfS1);

            BoundField bfS2 = new BoundField();
            bfS2.DataField = "T2";
            bfS2.HeaderText = lblT2.Text;
            bfS2.ItemStyle.Width = 25;
            bfS2.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfS2);

            BoundField bfS3 = new BoundField();
            bfS3.DataField = "T3";
            bfS3.HeaderText = lblT3.Text;
            bfS3.ItemStyle.Width = 25;
            bfS3.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfS3);

            BoundField bfS4 = new BoundField();
            bfS4.DataField = "T4";
            bfS4.HeaderText = lblT4.Text;
            bfS4.ItemStyle.Width = 25;
            bfS4.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfS4);

            BoundField bfS5 = new BoundField();
            bfS5.DataField = "T5";
            bfS5.HeaderText = lblT5.Text;
            bfS5.ItemStyle.Width = 25;
            bfS5.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfS5);

            BoundField bfS6 = new BoundField();
            bfS6.DataField = "T6";
            bfS6.HeaderText = lblT6.Text;
            bfS6.ItemStyle.Width = 25;
            bfS6.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfS6);

            BoundField bfS7 = new BoundField();
            bfS7.DataField = "T7";
            bfS7.HeaderText = lblT7.Text;
            bfS7.ItemStyle.Width = 25;
            bfS7.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfS7);

            BoundField bfS8 = new BoundField();
            bfS8.DataField = "T8";
            bfS8.HeaderText = lblT8.Text;
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

            lblTotMed.Text = totalMedia != 0 ? String.Format("{0:N2}", totalMedia / (grdResulParam.Rows.Count > 0 ? grdResulParam.Rows.Count : 1)) : "-" ;

            liGridMCFF.Visible = true;
        }

//====> Método executado quando botão de gravar médias clicado
        protected void btnGravarMedias_Click(object sender, EventArgs e)
        {
            if (!liGridMCFF.Visible)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Primeiro faça a pesquisa.");
                return; 
            }

            int coEmp = ddlUnidadeEscolar.SelectedValue != "" ? int.Parse(ddlUnidadeEscolar.SelectedValue) : 0;
            string coAnoRef = ddlAnoRefer.SelectedValue != "T" ? ddlAnoRefer.SelectedValue : "";
            bool ocorrReg = false;
            bool ocorrAlterReg = false;

            var qryMaterias = (from tb107 in TB107_CADMATERIAS.RetornaTodosRegistros()
                               where (coEmp != 0 ? tb107.CO_EMP == coEmp : coEmp == 0)
                               select new { tb107.ID_MATERIA, tb107.NO_MATERIA }).Distinct().OrderBy(u => u.NO_MATERIA);

            foreach (var qryM in qryMaterias)
            {
                var qryGrid = from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                              join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb079.CO_MAT equals tb02.CO_MAT
                              where tb079.TB25_EMPRESA.CO_EMP == coEmp && (coAnoRef != "" ? tb079.CO_ANO_REF == coAnoRef : coAnoRef == "")
                              && tb079.CO_EMP == tb02.CO_EMP && tb02.ID_MATERIA == qryM.ID_MATERIA
                              group tb079 by tb079.CO_ANO_REF into g
                              orderby g.Key                              
                              select new { CO_ANO_REF = g.Key, total = g.Sum(p => ((p.VL_NOTA_BIM1) + (p.VL_NOTA_BIM2) + (p.VL_NOTA_BIM3) + (p.VL_NOTA_BIM4))/4), totalB1 = g.Sum(p => (p.VL_NOTA_BIM1)), totalB2 = g.Sum(p => (p.VL_NOTA_BIM2)), totalB3 = g.Sum(p => (p.VL_NOTA_BIM3)), totalB4 = g.Sum(p => (p.VL_NOTA_BIM4)) };                             

                foreach (var qryG in qryGrid)
                {
                    var qtdAluno = (from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                                    join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb079.CO_MAT equals tb02.CO_MAT
                                    where tb079.TB25_EMPRESA.CO_EMP == coEmp && tb079.CO_ANO_REF == qryG.CO_ANO_REF
                                    && tb079.CO_EMP == tb02.CO_EMP && tb02.ID_MATERIA == qryM.ID_MATERIA
                                    select new { tb079.CO_ALU }).Count();

                    TB954_ESTAT_MATER desEsc = RetornaEntidade(coEmp, qryM.ID_MATERIA, int.Parse(qryG.CO_ANO_REF));

                    if (desEsc == null)
                    {
                        ocorrReg = true;

                        desEsc = new TB954_ESTAT_MATER();

                        desEsc.TB107_CADMATERIAS = TB107_CADMATERIAS.RetornaPelaChavePrimaria(coEmp, qryM.ID_MATERIA);                       
                        desEsc.CO_ANO_REF = int.Parse(qryG.CO_ANO_REF);
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
            {
                AuxiliPagina.RedirecionaParaPaginaSucesso("A atualização de dados foi PARCIAL - Já existem Unidades com ANO DE REFERÊNCIA selecionado homologado.", Request.Url.AbsoluteUri);
            }
            else
            {
                if (ocorrReg)
                    AuxiliPagina.RedirecionaParaPaginaSucesso("Os dados de TODAS as Matérias foram atualizados com sucesso.", Request.Url.AbsoluteUri);
                else
                    AuxiliPagina.RedirecionaParaPaginaErro("NENHUM dado foi atualizado - TODAS as Matérias estão com o ANO DE REFERÊNCIA selecionado homologado.", Request.Url.AbsoluteUri);
            }
        }

        protected void ddlUnidadeEscolar_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAnos();
        }
    }
}
