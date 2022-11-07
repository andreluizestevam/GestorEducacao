//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE DE FREQÜÊNCIA ESCOLAR DO ALUNO
// OBJETIVO: LANÇAMENTO E MANUTENÇÃO DE FREQÜÊNCIA DE ALUNOS DE TURMAS MULTISÉRIE
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 16/07/2013| André Nobre Vinagre        | Colocado o tratamento da data de lançamento do bimestre
//           |                            | vindo da tabela de unidade
//           |                            |
// ----------+----------------------------+-------------------------------------

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3400_CtrlFrequenciaEscolar.F3403_LancManutFreqAlunoTurmaMulti
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

//--------> Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            CarregaModalidades();
            CarregaAnos();
            divGrid.Visible = false;
        }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            
            if ( modalidade == 0 || turma == 0 )
            {
                grdBusca.DataBind();
                return;
            }

            DateTime dataFrequencia = DateTime.Parse(ddlDataFrequencia.SelectedValue);

            if (dataFrequencia > DateTime.Now)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Data da Frequência não pode ser superior a data atual.");
                return;
            }
           
            TB132_FREQ_ALU tb132;

//--------> Varre toda a grid de Busca
            foreach (GridViewRow row in grdBusca.Rows)
            {
//------------> Recebe o código do Aluno
                int coAlu = Convert.ToInt32(grdBusca.DataKeys[row.RowIndex].Values[0]);

//------------> Faz a verificação para saber se já existe frequência para o dia informado
                TB132_FREQ_ALU ocoTb132 = (from lTb132 in TB132_FREQ_ALU.RetornaTodosRegistros()
                                           where lTb132.TB07_ALUNO.CO_ALU == coAlu && lTb132.TB01_CURSO.CO_EMP == LoginAuxili.CO_EMP
                                           && lTb132.TB01_CURSO.CO_MODU_CUR == modalidade && lTb132.DT_FRE == dataFrequencia && lTb132.CO_TUR == turma
                                           select lTb132).FirstOrDefault();

                string strFlagFreqAluno = ((DropDownList)row.Cells[3].FindControl("ddlFreq")).SelectedValue;
                var serieT = row.Cells[2].FindControl("txtSerie");
                int serie = 0;
                if (row.Cells[4] != null && row.Cells[4].GetType() == typeof(DataControlFieldCell))
                    int.TryParse(((DataControlFieldCell)row.Cells[4]).Text, out serie);
                if (ocoTb132 != null)
                {
                    if (ocoTb132.CO_FLAG_FREQ_ALUNO != strFlagFreqAluno)
                    {
                        ocoTb132.CO_FLAG_FREQ_ALUNO = strFlagFreqAluno;
                        TB132_FREQ_ALU.SaveOrUpdate(ocoTb132, true);
                    }
                }
                else
                {
                    tb132 = new TB132_FREQ_ALU();
                    
                    int coAtivProfTur = (from tb119 in TB119_ATIV_PROF_TURMA.RetornaTodosRegistros()
                                         where tb119.CO_EMP == LoginAuxili.CO_EMP && tb119.CO_MODU_CUR == modalidade && tb119.CO_TUR == turma
                                         && tb119.CO_ANO_MES_MAT == ddlAno.SelectedValue && tb119.DT_ATIV_REAL == dataFrequencia
                                         select new { tb119.CO_ATIV_PROF_TUR }).FirstOrDefault().CO_ATIV_PROF_TUR;

                    tb132.TB01_CURSO = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie);
                    tb132.TB07_ALUNO = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                    tb132.CO_FLAG_FREQ_ALUNO = strFlagFreqAluno;
                    tb132.CO_TUR = turma;
                    tb132.DT_FRE = dataFrequencia;
                    tb132.CO_ATIV_PROF_TUR = coAtivProfTur;
                    tb132.CO_COL = LoginAuxili.CO_COL;
                    tb132.DT_LANCTO_FREQ_ALUNO = DateTime.Now;

                    tb132.CO_ANO_REFER_FREQ_ALUNO = dataFrequencia.Year;

                    TB132_FREQ_ALU.SaveOrUpdate(tb132, true);

                    if (tb132.EntityState != System.Data.EntityState.Unchanged && GestorEntities.SaveOrUpdate(tb132) < 1)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao salvar os itens");
                        return;
                    }
                }               
            }

            AuxiliPagina.RedirecionaParaPaginaSucesso("Salvo com sucesso.", Request.Url.AbsoluteUri);
        }

        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega a grid de Busca
        /// </summary>
        private void CarregaGrid()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string anoMesMat = ddlAno.SelectedValue;

            if (modalidade == 0 || turma == 0)
            {
                grdBusca.DataBind();
                return;
            }

            divGrid.Visible = true;        
            
            DateTime dataFrequencia = Convert.ToDateTime(ddlDataFrequencia.SelectedValue);


            var lstAlunoMatricula = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                     join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb08.CO_CUR equals tb01.CO_CUR 
                                     where tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP && tb08.CO_ANO_MES_MAT == anoMesMat
                                     && tb08.CO_TUR == turma && tb08.CO_SIT_MAT == "A"                                
                                     select new
                                     {
                                         tb08.CO_ALU, tb08.TB07_ALUNO.NO_ALU, tb08.CO_CUR, tb01.NO_CUR, tb08.CO_ANO_MES_MAT,
                                         CO_ALU_CAD = tb08.CO_ALU_CAD.Insert(2, ".").Insert(6, "."), tb08.CO_SIT_MAT
                                     }).ToList().OrderBy( m => m.NO_ALU );


            if (lstAlunoMatricula.Count() > 0)
            {
                // Habilita o botão de salvar
            }

            grdBusca.DataKeyNames = new string[] { "CO_ALU" };

            grdBusca.DataSource = lstAlunoMatricula;
            grdBusca.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            ddlAno.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                 where tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                 select new { tb08.CO_ANO_MES_MAT }).Distinct().OrderByDescending( m => m.CO_ANO_MES_MAT );

            ddlAno.DataTextField = "CO_ANO_MES_MAT";
            ddlAno.DataValueField = "CO_ANO_MES_MAT";
            ddlAno.DataBind();
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

            ddlModalidade.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            if (modalidade != 0)
            {
                ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                       where tb06.CO_MODU_CUR == modalidade && tb06.CO_EMP == LoginAuxili.CO_EMP && tb06.TB129_CADTURMAS.CO_FLAG_MULTI_SERIE == "S"
                                       select new { tb06.TB129_CADTURMAS.CO_SIGLA_TURMA, tb06.CO_TUR }).Distinct().OrderBy( t => t.CO_SIGLA_TURMA );

                ddlTurma.DataTextField = "CO_SIGLA_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataBind();
            }
            else
                ddlTurma.Items.Clear();

            ddlTurma.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
            CarregaGrid();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;

            if(turma != 0)
            {
                var resultado = (from tb119 in TB119_ATIV_PROF_TURMA.RetornaTodosRegistros()
                                 where tb119.CO_ANO_MES_MAT == ddlAno.SelectedValue && tb119.CO_MODU_CUR == modalidade 
                                 && tb119.CO_TUR == turma && tb119.CO_EMP == LoginAuxili.CO_EMP
                                 select new { tb119.DT_ATIV_REAL }).ToList().Distinct();

                var resultado2 = (from result in resultado
                                  select new { DT_ATIV_REAL = result.DT_ATIV_REAL.ToString("dd/MM/yyyy") }).OrderBy( r => r.DT_ATIV_REAL );

                ddlDataFrequencia.DataSource = resultado2;

                ddlDataFrequencia.DataTextField = "DT_ATIV_REAL";
                ddlDataFrequencia.DataValueField = "DT_ATIV_REAL";
                ddlDataFrequencia.DataBind();

                ddlDataFrequencia.Items.Insert(0, new ListItem("Selecione", ""));
            }
        }

        protected void grdBusca_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdCoAluno = (HiddenField)e.Row.FindControl("hdCoAluno");
                DropDownList ddlFreq = (DropDownList)e.Row.FindControl("ddlFreq");

                int coAlu = Convert.ToInt32(hdCoAluno.Value);

                int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
                int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
                DateTime dataFrequencia = DateTime.Parse(ddlDataFrequencia.SelectedValue);

                var varFlagFreqAluno = (from tb132 in TB132_FREQ_ALU.RetornaTodosRegistros()
                                           where tb132.TB07_ALUNO.CO_ALU == coAlu && tb132.TB01_CURSO.CO_EMP == LoginAuxili.CO_EMP
                                           && tb132.TB01_CURSO.CO_MODU_CUR == modalidade && tb132.DT_FRE == dataFrequencia && tb132.CO_TUR == turma
                                           select new { tb132.CO_FLAG_FREQ_ALUNO }).FirstOrDefault();

                ddlFreq.SelectedValue = varFlagFreqAluno != null ? varFlagFreqAluno.CO_FLAG_FREQ_ALUNO : "S";

                var ocorTransfInterna = (from tbTransfInterna in TB_TRANSF_INTERNA.RetornaTodosRegistros()
                                         where tbTransfInterna.TB07_ALUNO.CO_ALU == coAlu && tbTransfInterna.CO_UNIDA_DESTI == LoginAuxili.CO_EMP
                                         && tbTransfInterna.CO_TURMA_DESTI == turma
                                         select tbTransfInterna);


                if (ocorTransfInterna.Count() > 0)
               {
                   DateTime dataFreqTrans = ocorTransfInterna.Max( t => t.DT_EFETI_TRANSF );

                   if (dataFreqTrans >= dataFrequencia)
                       ddlFreq.Enabled = false;
               }            
            }
        }

        protected void ddlAno_SelectedIndexChanged1(object sender, EventArgs e)
        {
            CarregaModalidades();           
            CarregaTurma();
            CarregaGrid();                 
        }

        protected void ddlDataFrequencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGrid();
        }      
    }
}