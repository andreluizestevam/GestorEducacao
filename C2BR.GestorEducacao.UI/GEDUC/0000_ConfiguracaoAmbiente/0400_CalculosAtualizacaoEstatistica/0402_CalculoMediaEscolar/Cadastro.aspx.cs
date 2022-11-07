//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: CÁLCULO E ATUALIZAÇÃO DE ESTATÍSTICAS
// OBJETIVO: CÁLCULO E ATUALIZAÇÃO DE MÉDIA ESCOLAR DE UNIDADES ESCOLARES
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
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0400_CalculosAtualizacaoEstatistica.F0402_CalculoMediaEscolar
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
                CarregaModalidades();
                CarregaSerieCurso();
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
        /// <param name="serie">Id da série</param>
        /// <param name="turma">Id da turma</param>
        /// <param name="materia">Id da matéria</param>
        /// <param name="anoRefer">Ano de referência</param>
        /// <returns>Entidade TB901_DESEMP_ESCOL</returns>
        private TB901_DESEMP_ESCOL RetornaEntidade(int coEmp, int modalidade, int serie, int turma, int materia, string anoRefer)
        {
            return TB901_DESEMP_ESCOL.RetornaPeloOcorrDesempEscol(coEmp, modalidade, serie, turma, materia, anoRefer);
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaUnidades()
        {
            int coTipoEmp = ddlTpUnidade.SelectedValue != "" ? int.Parse(ddlTpUnidade.SelectedValue) : 0;

            ddlUnidadeEscolar.Items.Clear();

            if (coTipoEmp != 0)
            {
                ddlUnidadeEscolar.DataSource = TB25_EMPRESA.RetornaTodosRegistros().Where(e => e.TB24_TPEMPRESA.CO_TIPOEMP == coTipoEmp && e.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO).OrderBy(e => e.NO_FANTAS_EMP);
                ddlUnidadeEscolar.DataTextField = "NO_FANTAS_EMP";
                ddlUnidadeEscolar.DataValueField = "CO_EMP";
                ddlUnidadeEscolar.DataBind();
            }
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
        }

        /// <summary>
        /// Método que carrega o dropdown de Ano de Referência
        /// </summary>
        private void CarregaAnos()
        {
            int coEmp = ddlUnidadeEscolar.SelectedValue != "" ? int.Parse(ddlUnidadeEscolar.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            ddlAnoRefer.Items.Clear();

            if ((coEmp != 0) && (modalidade != 0) && (serie != 0))
            {
                ddlAnoRefer.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                                          where tb43.CO_EMP == coEmp && tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.CO_CUR == serie
                                          select new { tb43.CO_ANO_GRADE }).Distinct().OrderByDescending( g => g.CO_ANO_GRADE );

                ddlAnoRefer.DataTextField = "CO_ANO_GRADE";
                ddlAnoRefer.DataValueField = "CO_ANO_GRADE";
                ddlAnoRefer.DataBind();    
            }            
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades Escolares
        /// </summary>
        private void CarregaModalidades()
        {
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where( m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);
            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {
            ddlSerieCurso.Items.Clear();

            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int coEmp = ddlUnidadeEscolar.SelectedValue != "" ? int.Parse(ddlUnidadeEscolar.SelectedValue) : 0;

            if ((modalidade != 0) && (coEmp != 0))
            {
                ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaTodosRegistros()
                                            where tb01.TB25_EMPRESA.CO_EMP == coEmp && tb01.TB44_MODULO.CO_MODU_CUR == modalidade
                                            select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct().OrderBy( c => c.NO_CUR );

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();   
            }            
        }
        #endregion

//====> Método executado quando botão de pesquisar clicado
        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            int coTipoEmp = ddlTpUnidade.Items.Count > 0 ? int.Parse(ddlTpUnidade.SelectedValue) : 0;
            int coEmp = ddlUnidadeEscolar.Items.Count > 0 ? int.Parse(ddlUnidadeEscolar.SelectedValue) : 0;
            int modalidade = ddlModalidade.Items.Count > 0 ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.Items.Count > 0 ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            string coAnoRef = ddlAnoRefer.Items.Count > 0 ? ddlAnoRefer.SelectedValue : "";
            int numTurmas = 0;
            decimal totalMedia = 0;

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

            var qryMaterias = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                               join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                               join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                               join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb43.CO_EMP equals tb25.CO_EMP
                               where tb25.TB24_TPEMPRESA.CO_TIPOEMP == coTipoEmp && tb43.CO_EMP == coEmp && tb43.TB44_MODULO.CO_MODU_CUR == modalidade
                               && tb43.CO_CUR == serie && tb43.CO_EMP == tb107.CO_EMP && tb43.CO_ANO_GRADE == coAnoRef
                               && tb43.TB44_MODULO.CO_MODU_CUR == tb02.TB01_CURSO.TB44_MODULO.CO_MODU_CUR && tb43.CO_CUR == tb02.TB01_CURSO.CO_CUR
                               && tb43.CO_EMP == tb02.TB01_CURSO.TB25_EMPRESA.CO_EMP
                               select new { tb02.CO_MAT, tb107.NO_MATERIA }).Distinct().OrderBy( g => g.NO_MATERIA );

            decimal[] totalSitu = new decimal[8];

            foreach (var qryM in qryMaterias)
            {
                decimal[] parcSitu = new decimal[8];
                string[] camposTab = new string[8];
               
                var qryGrid = from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()                              
                              join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb079.TB25_EMPRESA.CO_EMP equals tb25.CO_EMP
                              where tb25.TB24_TPEMPRESA.CO_TIPOEMP == coTipoEmp && tb079.TB25_EMPRESA.CO_EMP == coEmp 
                              && tb079.TB44_MODULO.CO_MODU_CUR == modalidade && tb079.CO_CUR == serie 
                              && tb079.CO_ANO_REF == coAnoRef && tb079.CO_MAT == qryM.CO_MAT
                              group tb079 by tb079.CO_TUR into g
                              orderby g.Key
                              select new { CO_TUR = g.Key, total = g.Sum(p => (p.VL_NOTA_BIM1 + p.VL_NOTA_BIM2 + p.VL_NOTA_BIM3 + p.VL_NOTA_BIM4)/4) };                
                
                int i = 0;
                numTurmas = 0;

                foreach (var qryG in qryGrid)
                {
                    numTurmas++;

                    var qtdAluno = (from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                                    where tb079.TB44_MODULO.CO_MODU_CUR == modalidade && tb079.CO_CUR == serie && tb079.TB25_EMPRESA.CO_EMP == coEmp
                                    && tb079.CO_ANO_REF == coAnoRef && tb079.CO_TUR == qryG.CO_TUR && tb079.CO_MAT == qryM.CO_MAT
                                    select new { tb079.CO_ALU }).Count();

                    string noturma = TB129_CADTURMAS.RetornaPelaChavePrimaria(qryG.CO_TUR).CO_SIGLA_TURMA;

                    if (i == 0)
                    {
                        lblT1.Text = noturma;
                        parcSitu[0] = qryG.total != null ? (decimal)qryG.total / qtdAluno : 0;
                        camposTab[0] = parcSitu[0] != 0 ? String.Format("{0:N2}", parcSitu[0]) : "-";
                        totalSitu[0] = qryG.total != null ? totalSitu[0] + (decimal)qryG.total / qtdAluno : totalSitu[0];
                    }
                    else if (i == 1)
                    {
                        lblT2.Text = noturma;
                        parcSitu[1] = qryG.total != null ? (decimal)qryG.total / qtdAluno : 0;
                        camposTab[1] = parcSitu[1] != 0 ? String.Format("{0:N2}", parcSitu[1]) : "-";
                        totalSitu[1] = qryG.total != null ? totalSitu[1] + (decimal)qryG.total / qtdAluno: totalSitu[1];
                    }
                    else if (i == 2)
                    {
                        lblT3.Text = noturma;
                        parcSitu[2] = qryG.total != null ? (decimal)qryG.total / qtdAluno : 0;
                        camposTab[2] = parcSitu[2] != 0 ? String.Format("{0:N2}", parcSitu[2]) : "-";
                        totalSitu[2] = qryG.total != null ? totalSitu[2] + (decimal)qryG.total / qtdAluno : totalSitu[2];
                    }
                    else if (i == 3)
                    {
                        lblT4.Text = noturma;
                        parcSitu[3] = qryG.total != null ? (decimal)qryG.total / qtdAluno : 0;
                        camposTab[3] = parcSitu[3] != 0 ? String.Format("{0:N2}", parcSitu[3]) : "-";
                        totalSitu[3] = qryG.total != null ? totalSitu[3] + (decimal)qryG.total / qtdAluno : totalSitu[3];
                    }
                    else if (i == 4)
                    {
                        lblT5.Text = noturma;
                        parcSitu[4] = qryG.total != null ? (decimal)qryG.total / qtdAluno : 0;
                        camposTab[4] = parcSitu[4] != 0 ? String.Format("{0:N2}", parcSitu[4]) : "-";
                        totalSitu[4] = qryG.total != null ? totalSitu[4] + (decimal)qryG.total / qtdAluno : totalSitu[4];
                    }
                    else if (i == 5)
                    {
                        lblT6.Text = noturma;
                        parcSitu[5] = qryG.total != null ? (decimal)qryG.total / qtdAluno : 0;
                        camposTab[5] = parcSitu[5] != 0 ? String.Format("{0:N2}", parcSitu[5]) : "-";
                        totalSitu[5] = qryG.total != null ? totalSitu[5] + (decimal)qryG.total / qtdAluno : totalSitu[5];
                    }
                    else if (i == 6)
                    {
                        lblT7.Text = noturma;
                        parcSitu[6] = qryG.total != null ? (decimal)qryG.total / qtdAluno : 0;
                        camposTab[6] = parcSitu[6] != 0 ? String.Format("{0:N2}", parcSitu[6]) : "-";
                        totalSitu[6] = qryG.total != null ? totalSitu[6] + (decimal)qryG.total / qtdAluno : totalSitu[6];
                    }
                    else if (i == 7)
                    {
                        lblT8.Text = noturma;
                        parcSitu[7] = qryG.total != null ? (decimal)qryG.total / qtdAluno : 0;
                        camposTab[7] = parcSitu[7] != 0 ? String.Format("{0:N2}", parcSitu[7]) : "-";
                        totalSitu[7] = qryG.total != null ? totalSitu[7] + (decimal)qryG.total / qtdAluno : totalSitu[7];
                    }
                    i++;
                }

                decimal media = (parcSitu[0] + parcSitu[1] + parcSitu[2] + parcSitu[3] + parcSitu[4] + parcSitu[5] + parcSitu[6] + parcSitu[7]) / (qryGrid.Count() > 0 ? qryGrid.Count() : 1);

                totalMedia = totalMedia + media;

                dt.Rows.Add(qryM.NO_MATERIA, camposTab[0], camposTab[1], camposTab[2],
                    camposTab[3], camposTab[4], camposTab[5], camposTab[6], camposTab[7], media != 0 ? String.Format("{0:N2}", media) : "-");
            }

            grdResulParam.Columns.Clear();

            BoundField bfUnid = new BoundField();
            bfUnid.DataField = "NO_MATERIA";
            bfUnid.HeaderText = "Unidade";
            bfUnid.ItemStyle.Width = 250;
            grdResulParam.Columns.Add(bfUnid);

            BoundField bfS1 = new BoundField();
            bfS1.DataField = "T1";
            bfS1.HeaderText = lblT1.Text;
            lblTit1.Text = lblT1.Text != "" ? "T1" : "";
            bfS1.ItemStyle.Width = 50;
            bfS1.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfS1);

            BoundField bfS2 = new BoundField();
            bfS2.DataField = "T2";
            bfS2.HeaderText = lblT2.Text;
            lblTit2.Text = lblT2.Text != "" ? "T2" : "";
            bfS2.ItemStyle.Width = 50;
            bfS2.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfS2);

            BoundField bfS3 = new BoundField();
            bfS3.DataField = "T3";
            bfS3.HeaderText = lblT3.Text;
            lblTit3.Text = lblT3.Text != "" ? "T3" : "";
            bfS3.ItemStyle.Width = 50;
            bfS3.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfS3);

            BoundField bfS4 = new BoundField();
            bfS4.DataField = "T4";
            bfS4.HeaderText = lblT4.Text;
            lblTit4.Text = lblT4.Text != "" ? "T4" : "";
            bfS4.ItemStyle.Width = 50;
            bfS4.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfS4);

            BoundField bfS5 = new BoundField();
            bfS5.DataField = "T5";
            bfS5.HeaderText = lblT5.Text;
            lblTit5.Text = lblT5.Text != "" ? "T5" : "";
            bfS5.ItemStyle.Width = 50;
            bfS5.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfS5);

            BoundField bfS6 = new BoundField();
            bfS6.DataField = "T6";
            bfS6.HeaderText = lblT6.Text;
            lblTit6.Text = lblT6.Text != "" ? "T6" : "";
            bfS6.ItemStyle.Width = 50;
            bfS6.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfS6);

            BoundField bfS7 = new BoundField();
            bfS7.DataField = "T7";
            bfS7.HeaderText = lblT7.Text;
            lblTit7.Text = lblT7.Text != "" ? "T7" : "";
            bfS7.ItemStyle.Width = 50;
            bfS7.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfS7);

            BoundField bfS8 = new BoundField();
            bfS8.DataField = "T8";
            bfS8.HeaderText = lblT8.Text;
            lblTit8.Text = lblT8.Text != "" ? "T8" : "";
            bfS8.ItemStyle.Width = 50;
            bfS8.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdResulParam.Columns.Add(bfS8);

            BoundField bfMedia = new BoundField();
            bfMedia.DataField = "MEDIA";
            bfMedia.HeaderText = "Média";
            bfMedia.ItemStyle.Width = 50;
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

            if (ddlTpUnidade.Items.Count == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Selecione um Tipo de Unidade.");
                return;
            }

            if (ddlUnidadeEscolar.Items.Count == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Selecione uma Unidade Escolar.");
                return;
            }

            if (ddlModalidade.Items.Count == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Selecione uma Modalidade.");
                return;
            }

            if (ddlSerieCurso.Items.Count == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Selecione uma Série.");
                return;
            }

            if (ddlAnoRefer.Items.Count == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Selecione um Ano de Referência.");
                return;
            }

            int coTipoEmp = int.Parse(ddlTpUnidade.SelectedValue);
            int coEmp = int.Parse(ddlUnidadeEscolar.SelectedValue);
            int modalidade = int.Parse(ddlModalidade.SelectedValue);
            int serie = int.Parse(ddlSerieCurso.SelectedValue);
            string coAnoRef = ddlAnoRefer.SelectedValue;
            bool ocorrReg = false;
            bool ocorrAlterReg = false;

            var qryMaterias = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                               join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                               join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                               join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb43.CO_EMP equals tb25.CO_EMP
                               where tb25.TB24_TPEMPRESA.CO_TIPOEMP == coTipoEmp
                                && tb43.CO_EMP == coEmp && tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.CO_CUR == serie && tb43.CO_EMP == tb107.CO_EMP
                                && tb43.CO_ANO_GRADE == coAnoRef && tb43.TB44_MODULO.CO_MODU_CUR == tb02.TB01_CURSO.TB44_MODULO.CO_MODU_CUR
                                && tb43.CO_CUR == tb02.TB01_CURSO.CO_CUR && tb43.CO_EMP == tb02.TB01_CURSO.TB25_EMPRESA.CO_EMP
                               select new { tb02.CO_MAT, tb107.NO_MATERIA }).Distinct().OrderBy( g => g.NO_MATERIA );

            foreach (var qryM in qryMaterias)
            {
                var qryGrid = from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                              join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb079.TB25_EMPRESA.CO_EMP equals tb25.CO_EMP
                              where tb25.TB24_TPEMPRESA.CO_TIPOEMP == coTipoEmp && tb079.TB25_EMPRESA.CO_EMP == coEmp 
                              && tb079.TB44_MODULO.CO_MODU_CUR == modalidade && tb079.CO_CUR == serie 
                              && tb079.CO_EMP == coEmp && tb079.CO_ANO_REF == coAnoRef && tb079.CO_MAT == qryM.CO_MAT
                              group tb079 by tb079.CO_TUR into g
                              orderby g.Key
                              select new { CO_TUR = g.Key, total = g.Sum(p => ((p.VL_NOTA_BIM1) + (p.VL_NOTA_BIM2) + (p.VL_NOTA_BIM3) + (p.VL_NOTA_BIM4))/4), totalB1 = g.Sum(p => (p.VL_NOTA_BIM1)), totalB2 = g.Sum(p => (p.VL_NOTA_BIM2)), totalB3 = g.Sum(p => (p.VL_NOTA_BIM3)), totalB4 = g.Sum(p => (p.VL_NOTA_BIM4)) };                             

                foreach (var qryG in qryGrid)
                {
                    var qtdAluno = (from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                                    where tb079.TB44_MODULO.CO_MODU_CUR == modalidade && tb079.CO_CUR == serie && tb079.TB25_EMPRESA.CO_EMP == coEmp
                                    && tb079.CO_ANO_REF == coAnoRef && tb079.CO_TUR == qryG.CO_TUR && tb079.CO_MAT == qryM.CO_MAT
                                    select new { tb079.CO_ALU }).Count();

                    int idMateria = TB02_MATERIA.RetornaPelaChavePrimaria(coEmp, modalidade, qryM.CO_MAT, serie).ID_MATERIA;

                    TB901_DESEMP_ESCOL desEsc = RetornaEntidade(coEmp, modalidade, serie, qryG.CO_TUR, qryM.CO_MAT, coAnoRef);

                    if (desEsc == null)
                    {
                        ocorrReg = true;

                        desEsc = new TB901_DESEMP_ESCOL();

                        desEsc.TB06_TURMAS = TB06_TURMAS.RetornaTodosRegistros().Where(p => p.CO_EMP == coEmp && p.CO_MODU_CUR == modalidade && p.CO_CUR == serie && p.CO_TUR == qryG.CO_TUR).FirstOrDefault();
                        desEsc.CO_MAT_GRADE = qryM.CO_MAT;
                        desEsc.CO_MAT_GERAL = idMateria;
                        desEsc.CO_ANO_REF = coAnoRef;
                        desEsc.VL_MEDIA_BIM1 = qryG.totalB1 != null ? (decimal?)qryG.totalB1 / qtdAluno : null;
                        desEsc.VL_MEDIA_BIM2 = qryG.totalB2 != null ? (decimal?)qryG.totalB2 / qtdAluno : null;
                        desEsc.VL_MEDIA_BIM3 = qryG.totalB3 != null ? (decimal?)qryG.totalB3 / qtdAluno : null;
                        desEsc.VL_MEDIA_BIM4 = qryG.totalB4 != null ? (decimal?)qryG.totalB4 / qtdAluno : null;
                        desEsc.VL_MEDIA_DESEMP = qryG.total != null ? (decimal?)qryG.total / qtdAluno : null;
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
                            desEsc.VL_MEDIA_DESEMP = qryG.total != null ? (decimal?)qryG.total / qtdAluno : null;
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
                    AuxiliPagina.RedirecionaParaPaginaSucesso("Os dados da Unidade foram atualizados com sucesso.", Request.Url.AbsoluteUri);
                else
                    AuxiliPagina.RedirecionaParaPaginaErro("NENHUM dado foi atualizado - Unidade está com o ANO DE REFERÊNCIA selecionado homologado.", Request.Url.AbsoluteUri);
            }
        }

        protected void ddlTpUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaUnidades();
            CarregaSerieCurso();
            CarregaAnos();
        }

        protected void ddlUnidadeEscolar_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaAnos();
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaAnos();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAnos();
        }
    }
}
