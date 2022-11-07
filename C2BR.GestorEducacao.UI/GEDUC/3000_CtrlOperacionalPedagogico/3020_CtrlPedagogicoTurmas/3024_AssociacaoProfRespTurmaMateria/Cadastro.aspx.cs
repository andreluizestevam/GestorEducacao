//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: CONTROLE DE TURMAS POR SÉRIES/CURSOS
// OBJETIVO: ASSOCIAÇÃO PROFESSOR RESPONSÁVEL TURMA E/OU MATÉRIA.
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3020_CtrlPedagogicoTurmas.F3024_AssociacaoProfRespTurmaMateria
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            ///Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
            CurrentPadraoCadastros.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentPadraoCadastros_OnCarregaFormulario);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {
                    CarregaAnos();
                    CarregaModalidades();
                    CarregaSerieCurso();
                    CarregaTurma();
                    CarregaMaterias();
                    CarregaColabs(ddlColaboradorResponsavel);

                    ddlAno.Enabled = ddlModalidade.Enabled = ddlSerieCurso.Enabled = ddlTurma.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Chamada do método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        /// <summary>
        /// Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        /// </summary>
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int coAnoRef = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;
            int coColResp = ddlColaboradorResponsavel.SelectedValue != "" ? int.Parse(ddlColaboradorResponsavel.SelectedValue) : 0;
            int intRetorno = 0;

//--------> Salva a matéria se o professor for responsável por matéria
            if (TB06_TURMAS.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie, turma).CO_FLAG_RESP_TURMA == "N")
            {
                if (int.TryParse(ddlMateria.SelectedValue, out intRetorno))
                    intRetorno = int.Parse(ddlMateria.SelectedValue);
                else
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Matéria deve ser informada");
                    return;
                }
            }

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                var ocoReponMater = (from tbRespoMater in TB_RESPON_MATERIA.RetornaTodosRegistros()
                                     where tbRespoMater.CO_ANO_REF == coAnoRef 
                                     && tbRespoMater.CO_MODU_CUR == modalidade
                                     && tbRespoMater.CO_CUR == serie 
                                     && tbRespoMater.CO_TUR == turma
                                     && (intRetorno != 0 ? tbRespoMater.CO_MAT == intRetorno : tbRespoMater.CO_MAT == null)
                                     && tbRespoMater.CO_COL_RESP == coColResp && tbRespoMater.CO_EMP == LoginAuxili.CO_EMP
                                     select tbRespoMater).FirstOrDefault();

                if (ocoReponMater != null)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Responsável já associado a turma.");
                    return;
                }
            }
            else if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
            {
                int idRespMat = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);

                var ocoReponMater = (from tbRespoMater in TB_RESPON_MATERIA.RetornaTodosRegistros()
                                     where tbRespoMater.CO_ANO_REF == coAnoRef && tbRespoMater.CO_MODU_CUR == modalidade
                                     && tbRespoMater.CO_CUR == serie && tbRespoMater.CO_TUR == turma && tbRespoMater.CO_EMP == LoginAuxili.CO_EMP
                                     && (intRetorno != 0 ? tbRespoMater.CO_MAT == intRetorno : tbRespoMater.CO_MAT == null)
                                     && tbRespoMater.CO_COL_RESP == coColResp && tbRespoMater.ID_RESP_MAT != idRespMat
                                     select tbRespoMater).FirstOrDefault();

                if (ocoReponMater != null)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Responsável já associado a turma.");
                    return;
                }
            }

            TB_RESPON_MATERIA tbRespMater = RetornaEntidade();

            if (tbRespMater == null)
            {
                tbRespMater = new TB_RESPON_MATERIA();
                tbRespMater.CO_EMP = LoginAuxili.CO_EMP;
                tbRespMater.CO_MODU_CUR = modalidade;
                tbRespMater.CO_CUR = serie;
                tbRespMater.CO_TUR = turma;               
                tbRespMater.TB01_CURSO = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, tbRespMater.CO_MODU_CUR, tbRespMater.CO_CUR);
            }

            tbRespMater.CO_ANO_REF = coAnoRef;
            tbRespMater.CO_COL_RESP = coColResp;
            tbRespMater.CO_CLASS_RESP = ddlClassificacao.SelectedValue;
            tbRespMater.DT_INICIO = txtPeriodoDe.Text != "" ? (DateTime?)DateTime.Parse(txtPeriodoDe.Text) : null;
            tbRespMater.DT_FINAL = txtPeriodoAte.Text != "" ? (DateTime?)DateTime.Parse(txtPeriodoAte.Text) : null;
            tbRespMater.DE_OBSER = txtObservacao.Text != "" ? txtObservacao.Text : null;
            tbRespMater.CO_STATUS = ddlStatus.SelectedValue;
            tbRespMater.CO_MAT = intRetorno != 0 ? (int?)intRetorno : null;

            CurrentPadraoCadastros.CurrentEntity = tbRespMater;
        }

        #endregion

        #region Métodos
        
        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB_RESPON_MATERIA tbRespMater = RetornaEntidade();

            if (tbRespMater != null)
            {
                CarregaAnos();
                ddlAno.SelectedValue = tbRespMater.CO_ANO_REF.ToString();                
                CarregaModalidades();
                ddlModalidade.SelectedValue = tbRespMater.CO_MODU_CUR.ToString();
                CarregaSerieCurso();
                ddlSerieCurso.SelectedValue = tbRespMater.CO_CUR.ToString();
                CarregaTurma();
                ddlTurma.SelectedValue = tbRespMater.CO_TUR.ToString();
                CarregaMaterias();
                if (liMateria.Visible)
                    ddlMateria.SelectedValue = tbRespMater.CO_MAT.ToString();                
                CarregaColabs(ddlColaboradorResponsavel);
                ddlColaboradorResponsavel.SelectedValue = tbRespMater.CO_COL_RESP.ToString();
                ddlClassificacao.SelectedValue = tbRespMater.CO_CLASS_RESP;
                txtPeriodoDe.Text = tbRespMater.DT_INICIO != null ? tbRespMater.DT_INICIO.Value.ToString("dd/MM/yyyy") : "";
                txtPeriodoAte.Text = tbRespMater.DT_FINAL != null ? tbRespMater.DT_FINAL.Value.ToString("dd/MM/yyyy") : "";
                txtObservacao.Text = tbRespMater.DE_OBSER != null ? tbRespMater.DE_OBSER : "";
                ddlStatus.SelectedValue = tbRespMater.CO_STATUS;
            }                        
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB_RESPON_MATERIA</returns>
        private TB_RESPON_MATERIA RetornaEntidade()
        {
            return TB_RESPON_MATERIA.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));             
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Colaboradores
        /// </summary>
        /// <param name="ddl">DropDown de colaboradores</param>
        private void CarregaColabs(DropDownList ddl)
        {
            ddl.DataSource = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                              where tb03.CO_EMP == LoginAuxili.CO_EMP
                              select new { tb03.CO_COL, tb03.NO_COL }).OrderBy( c => c.NO_COL );

            ddl.DataTextField = "NO_COL";
            ddl.DataValueField = "CO_COL";
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            if ((modalidade != 0) && (serie != 0))
            {
                ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                       where tb06.CO_MODU_CUR == modalidade && tb06.CO_CUR == serie
                                       select new { tb06.TB129_CADTURMAS.NO_TURMA, tb06.CO_TUR });

                ddlTurma.DataTextField = "NO_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataBind();
            }
            else
                ddlTurma.Items.Clear();

            ddlTurma.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            ddlAno.Items.Clear();
            int intAnoReferencia = DateTime.Now.Year;
            ddlAno.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                                 where tb43.CO_SITU_MATE_GRC == "A"
                                 select new { tb43.CO_ANO_GRADE}).OrderByDescending(w => w.CO_ANO_GRADE).DistinctBy(d => d.CO_ANO_GRADE);
            ddlAno.DataTextField = "CO_ANO_GRADE";
            ddlAno.DataValueField = "CO_ANO_GRADE";
            ddlAno.DataBind();

            if (ddlAno.Items.FindByValue(DateTime.Now.Year.ToString()) != null)
                ddlAno.SelectedValue = DateTime.Now.Year.ToString();
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
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            if (modalidade != 0)
            {
                ddlSerieCurso.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                            where tb43.TB44_MODULO.CO_MODU_CUR == modalidade
                                            join tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on tb43.CO_CUR equals tb01.CO_CUR
                                            select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct().OrderBy(c => c.NO_CUR);

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();
            }

            ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Matérias
        /// </summary>
        private void CarregaMaterias()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string coAnoGrade = ddlAno.SelectedValue;

            if (turma > 0 && TB06_TURMAS.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie, turma).CO_FLAG_RESP_TURMA == "N")
            {
                liMateria.Visible = true;

                ddlMateria.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                                         where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.CO_CUR == serie && tb43.CO_ANO_GRADE == coAnoGrade
                                         join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                                         join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                         select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy( g => g.NO_MATERIA );

                ddlMateria.DataTextField = "NO_MATERIA";
                ddlMateria.DataValueField = "CO_MAT";
                ddlMateria.DataBind();
            }
            else
            {
                liMateria.Visible = false;
                ddlMateria.Items.Clear();
            }

            ddlMateria.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
            CarregaMaterias();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
            CarregaMaterias();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaMaterias();
        }
    }
}
