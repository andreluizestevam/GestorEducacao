//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: CONTROLE DE SÉRIES OU CURSOS
// OBJETIVO: GERA GRADE ANUAL DE DISCIPLINA (MATÉRIA) DE SÉRIE/CURSO
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 20/03/2013| André Nobre Vinagre        | Criação do campo de agrupador da matéria.
//           |                            |  
// ----------+----------------------------+-------------------------------------
// 02/05/2013| André Nobre Vinagre        | Criação dos campos de quantidade de aulas
//           |                            | do bimestre 1, 2, 3 e 4
//           |                            |  
// 24/11/2014| Maxwell Almeida            | Criados os campos para informar se é possível lançar frequencia/disciplina para a disciplina em questão
//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3010_CtrlPedagogicoSeries.F3016_GeraGradeAnualDisciplina
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
            CurrentPadraoCadastros.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentPadraoCadastros_OnCarregaFormulario);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtAno.Text = DateTime.Now.Year.ToString();
                txtNtMaxi.Text = "10,0";

                CarregaModalidades();
                CarregaSerieCurso();
                CarregaMaterias();
                CarregaAgrupadorMaterias();

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {
                    //----------------> Se for inserção permite a edição dos seguintes campos:
                    txtAno.Enabled = ddlModalidade.Enabled = ddlSerieCurso.Enabled = ddlMateria.Enabled = true;

                    //----------------> Se for novo registro carregará os campos código e carga horária de acordo com a matéria selecionada
                    CarregaCargaHoraria();
                    txtDataSituacao.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
            }
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int materia = ddlMateria.SelectedValue != "" ? int.Parse(ddlMateria.SelectedValue) : 0;

            DateTime dataRetorno = DateTime.Now;
            int intRetorno = 0;

            int verifOcorr = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                              where tb43.CO_EMP == LoginAuxili.CO_EMP && tb43.CO_CUR == serie && tb43.CO_ANO_GRADE == txtAno.Text
                              && tb43.CO_MAT == materia && tb43.TB44_MODULO.CO_MODU_CUR == modalidade
                              select new { tb43.NU_SEM_GRADE }).Count();

            if (verifOcorr > 0 && QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                AuxiliPagina.EnvioMensagemErro(this, "Já existe grade gerada para Modalidade/Série/Disciplina no Ano selecionado.");
                return;
            }
            else
            {
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {
                    int verifMatric = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                       where tb08.CO_EMP == LoginAuxili.CO_EMP && tb08.CO_CUR == serie && tb08.CO_ANO_MES_MAT == txtAno.Text
                                       && tb08.TB44_MODULO.CO_MODU_CUR == modalidade
                                       select new { tb08.CO_ALU }).Count();

                    // Comentado para permitir a alteração da grade, nos campos que cabem a alteração
                    //if (verifMatric > 0)
                    //{
                    //    AuxiliPagina.EnvioMensagemErro(this, "Grade não pode ser alterada pois existe(m) aluno(s) matriculado(s).");
                    //    return;
                    //}
                }

                TB43_GRD_CURSO tb43 = RetornaEntidade();

                if (tb43 == null)
                {
                    tb43 = new TB43_GRD_CURSO();
                    tb43.CO_EMP = LoginAuxili.CO_EMP;
                    tb43.TB44_MODULO = TB44_MODULO.RetornaPelaChavePrimaria(modalidade);
                    tb43.CO_CUR = serie;
                    tb43.CO_ANO_GRADE = txtAno.Text;
                    tb43.CO_MAT = materia;
                    tb43.NU_SEM_GRADE = 1;
                }

                tb43.CO_ORDEM_IMPRE = int.TryParse(txtOrdImp.Text, out intRetorno) ? (int?)intRetorno : null;
                tb43.QTDE_CH_SEM = int.TryParse(txtCargaHoraria.Text, out intRetorno) ? (int?)intRetorno : null;
                tb43.QTDE_AULA_SEM = int.TryParse(txtQtdeAula.Text, out intRetorno) ? (int?)intRetorno : null;
                tb43.QT_AULAS_BIM1 = int.TryParse(txtQtdeAulaB1.Text, out intRetorno) ? (int?)intRetorno : null;
                tb43.QT_AULAS_BIM2 = int.TryParse(txtQtdeAulaB2.Text, out intRetorno) ? (int?)intRetorno : null;
                tb43.QT_AULAS_BIM3 = int.TryParse(txtQtdeAulaB3.Text, out intRetorno) ? (int?)intRetorno : null;
                tb43.QT_AULAS_BIM4 = int.TryParse(txtQtdeAulaB4.Text, out intRetorno) ? (int?)intRetorno : null;
                tb43.DT_SITU_MATE_GRC = DateTime.Now;
                tb43.CO_SITU_MATE_GRC = ddlSituacao.SelectedValue;
                tb43.ID_MATER_AGRUP = ddlAgrupMateria.SelectedValue != "" ? (int?)int.Parse(ddlAgrupMateria.SelectedValue) : null;
                tb43.FL_DISCI_AGRUPA = chkAgrupadora.Checked == true ? "S" : "N";
                tb43.VL_NOTA_MAXIM = (!string.IsNullOrEmpty(txtNtMaxi.Text) ? decimal.Parse(txtNtMaxi.Text) : (decimal?)null);
                tb43.VL_NOTA_MAXIM_ATIVI = (!string.IsNullOrEmpty(txtNtMaxAtiv.Text) ? decimal.Parse(txtNtMaxAtiv.Text) : (decimal?)null);
                tb43.VL_NOTA_MAXIM_PROVA = (!string.IsNullOrEmpty(txtNtMaxProva.Text) ? decimal.Parse(txtNtMaxProva.Text) : (decimal?)null);
                tb43.VL_NOTA_MAXIM_SIMUL = (!string.IsNullOrEmpty(txtNtMaxSimu.Text) ? decimal.Parse(txtNtMaxSimu.Text) : (decimal?)null);
                tb43.FL_NOTA1_MEDIA = (chkNota1Media.Checked ? "S" : "N");
                tb43.FL_LANCA_FREQU = chkLancFreq.Checked ? "S" : "N";
                tb43.FL_LANCA_NOTA = chkLancNota.Checked ? "S" : "N";

                CurrentPadraoCadastros.CurrentEntity = tb43;

                //Trata para o caso de o usuário ter selecionado uma disciplina agrupadora
                if (ddlAgrupMateria.SelectedValue != "")
                {
                    string anoM = txtAno.Text;
                    int matAgrup = int.Parse(ddlAgrupMateria.SelectedValue);
                    TB43_GRD_CURSO tb43_agpMat = TB43_GRD_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, anoM, serie, matAgrup);
                    tb43_agpMat.FL_DISCI_AGRUPA = "S";
                    TB43_GRD_CURSO.SaveOrUpdate(tb43_agpMat, true);
                }
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB43_GRD_CURSO tb43 = RetornaEntidade();

            if (tb43 != null)
            {
                txtQtdeAula.Text = tb43.QTDE_AULA_SEM.ToString();
                txtCargaHoraria.Text = tb43.QTDE_CH_SEM.ToString();
                txtQtdeAulaB1.Text = tb43.QT_AULAS_BIM1.ToString();
                txtQtdeAulaB2.Text = tb43.QT_AULAS_BIM2.ToString();
                txtQtdeAulaB3.Text = tb43.QT_AULAS_BIM3.ToString();
                txtQtdeAulaB4.Text = tb43.QT_AULAS_BIM4.ToString();
                txtDataSituacao.Text = tb43.DT_SITU_MATE_GRC.ToString("dd/MM/yyyy");
                txtAno.Text = tb43.CO_ANO_GRADE;
                ddlModalidade.SelectedValue = tb43.TB44_MODULO.CO_MODU_CUR.ToString();
                CarregaSerieCurso();
                ddlSerieCurso.SelectedValue = tb43.CO_CUR.ToString();
                CarregaMaterias();
                ddlMateria.SelectedValue = tb43.CO_MAT.ToString();
                CarregaAgrupadorMaterias();
                ddlAgrupMateria.SelectedValue = tb43.ID_MATER_AGRUP != null ? tb43.ID_MATER_AGRUP.ToString() : "";
                ddlSituacao.SelectedValue = tb43.CO_SITU_MATE_GRC;
                txtOrdImp.Text = tb43.CO_ORDEM_IMPRE.ToString();
                chkAgrupadora.Checked = tb43.FL_DISCI_AGRUPA == "S" ? true : false;
                txtNtMaxi.Text = tb43.VL_NOTA_MAXIM.ToString();
                txtNtMaxSimu.Text = tb43.VL_NOTA_MAXIM_SIMUL.ToString();
                txtNtMaxProva.Text = tb43.VL_NOTA_MAXIM_PROVA.ToString();
                txtNtMaxAtiv.Text = tb43.VL_NOTA_MAXIM_ATIVI.ToString();
                chkNota1Media.Checked = (tb43.FL_NOTA1_MEDIA == "S" ? true : false);
                chkLancFreq.Checked = (tb43.FL_LANCA_FREQU == "S" ? true : false);
                chkLancNota.Checked = (tb43.FL_LANCA_NOTA == "S" ? true : false);
                CarregaGridAgrupadora();
            }
        }

        /// <summary>
        /// Carrega a grid de Disciplinas filhas da matéria selecionada quando ela for agrupadora
        /// </summary>
        private void CarregaGridAgrupadora()
        {
            if (chkAgrupadora.Checked)
            {
                int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
                int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
                int materia = ddlMateria.SelectedValue != "" ? int.Parse(ddlMateria.SelectedValue) : 0;
                string ano = txtAno.Text;
                var res = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                           join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                           join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                           where tb43.TB44_MODULO.CO_MODU_CUR == modalidade
                           && tb43.CO_CUR == serie
                           && tb43.ID_MATER_AGRUP == materia
                           && tb43.CO_ANO_GRADE == ano
                           select new
                           {
                               tb107.NO_MATERIA,
                               tb107.NO_SIGLA_MATERIA,
                               tb02.CO_MAT,
                               tb43.CO_CUR,
                               tb43.CO_ANO_GRADE,
                           }).ToList();

                grdMatAgrup.DataSource = res;
                grdMatAgrup.DataBind();

                if (res.Count > 0)
                    infoAgrup.Visible = true;
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB43_GRD_CURSO</returns>
        private TB43_GRD_CURSO RetornaEntidade()
        {
            return TB43_GRD_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Ano),
             QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoCur), QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoMat));
        }
        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where(m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                        where tb01.CO_MODU_CUR == modalidade
                                        select new { tb01.CO_SIGL_CUR, tb01.CO_CUR }).OrderBy(c => c.CO_SIGL_CUR);

            ddlSerieCurso.DataTextField = "CO_SIGL_CUR";
            ddlSerieCurso.DataValueField = "CO_CUR";
            ddlSerieCurso.DataBind();

            ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Matérias
        /// </summary>
        private void CarregaMaterias()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            ddlMateria.DataSource = (from tb02 in TB02_MATERIA.RetornaTodosRegistros()
                                     where tb02.CO_MODU_CUR == modalidade && tb02.CO_CUR == serie
                                     join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                     select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy(m => m.NO_MATERIA);

            ddlMateria.DataTextField = "NO_MATERIA";
            ddlMateria.DataValueField = "CO_MAT";
            ddlMateria.DataBind();

            ddlMateria.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Agrupador de Matérias
        /// </summary>
        private void CarregaAgrupadorMaterias()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            ddlAgrupMateria.DataSource = (from tb02 in TB02_MATERIA.RetornaTodosRegistros()
                                          where tb02.CO_MODU_CUR == modalidade && tb02.CO_CUR == serie
                                          join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                          select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy(m => m.NO_MATERIA);

            ddlAgrupMateria.DataTextField = "NO_MATERIA";
            ddlAgrupMateria.DataValueField = "CO_MAT";
            ddlAgrupMateria.DataBind();

            ddlAgrupMateria.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega informação de carga horária da matéria selecionada
        /// </summary>
        private void CarregaCargaHoraria()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int materia = ddlMateria.SelectedValue != "" ? int.Parse(ddlMateria.SelectedValue) : 0;

            if (materia != 0)
                txtCargaHoraria.Text = TB02_MATERIA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, materia, serie).QT_CARG_HORA_MAT.ToString();
        }

        /// <summary>
        /// Deleta a associação da matéria agrupada com a agrupadora
        /// </summary>
        private void DeletaAssociacao()
        {
            foreach (GridViewRow li in grdMatAgrup.Rows)
            {
                if ((((CheckBox)li.Cells[0].FindControl("ckSelect")).Checked) == true)
                {
                    int coMat = int.Parse((((HiddenField)li.Cells[0].FindControl("hidCoMat")).Value));
                    int coCur = int.Parse((((HiddenField)li.Cells[0].FindControl("hidCoCur")).Value));
                    string coAno = (((HiddenField)li.Cells[0].FindControl("hidCoAno")).Value);

                    TB43_GRD_CURSO tb43 = TB43_GRD_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, coAno, coCur, coMat);
                    tb43.ID_MATER_AGRUP = (int?)null;
                    TB43_GRD_CURSO.SaveOrUpdate(tb43, true);
                }
            }
            CarregaGridAgrupadora();
        }

        #endregion

        protected void lnkApagaAssoci_OnClick(object sender, EventArgs e)
        {
            DeletaAssociacao();            
        }

        protected void chkAgrupadora_OnCheckedChanged(object sender, EventArgs e)
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int materia = ddlMateria.SelectedValue != "" ? int.Parse(ddlMateria.SelectedValue) : 0;
            string ano = txtAno.Text;
            var res = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                       where tb43.TB44_MODULO.CO_MODU_CUR == modalidade
                       && tb43.CO_CUR == serie
                       && tb43.ID_MATER_AGRUP == materia
                       && tb43.CO_ANO_GRADE == ano
                       select new
                       {
                           tb43.CO_MAT,
                       }).ToList();

            if (chkAgrupadora.Checked == false)
            {
                if (res.Count > 0)
                {
                    chkAgrupadora.Checked = true;
                    bool plural = res.Count > 1 ? true : false;
                    string msg = "Não é possível que a disciplina em questão deixe de ser agrupadora, pois " + (plural ? "existem " : "existe ") + res.Count + (plural ? " disciplinas" : " uma disciplina") + (plural ? " agrupadas" : " agrupada") + " à ela.";
                    AuxiliPagina.EnvioMensagemErro(this.Page, msg);
                }
            }
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaMaterias();
            CarregaAgrupadorMaterias();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaMaterias();
            CarregaAgrupadorMaterias();
        }

        protected void ddlMateria_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCargaHoraria();
        }
    }
}
