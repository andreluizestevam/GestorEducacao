//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: CONTROLE DE TURMAS POR SÉRIES/CURSOS
// OBJETIVO: ASSOCIAÇÃO DE MODALIDADE/SÉRIE A TURMA
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+----------------------------------------
// 17/04/2013| André Nobre Vinagre        | Alterado tooltip de Turma Única
//           |                            | 

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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3020_CtrlPedagogicoTurmas.F3023_AssociacaoModSerieTurma
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentCadastroMasterPage { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

//--------> Criação das instâncias utilizadas
            CurrentCadastroMasterPage.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentCadastroMasterPage_OnAcaoBarraCadastro);
            CurrentCadastroMasterPage.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentCadastroMasterPage_OnCarregaFormulario);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaDropDown();
                CarregaModalidade();
                CarregaSerieCurso();
                CarregaTurma();
                if (ddlTurma.SelectedValue != "")
                    CarregaMultiSerie(int.Parse(ddlTurma.SelectedValue));
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        protected void CurrentCadastroMasterPage_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        protected void CurrentCadastroMasterPage_OnAcaoBarraCadastro()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;

            if (modalidade < 1)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Campo Modalidade é obrigatório.");
                return;
            }


            if (serie < 1)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Campo Série é obrigatório.");
                return;
            }

            if (turma < 1)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Campo Turma é obrigatório.");
                return;
            }

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
//------------>
                if (TB129_CADTURMAS.RetornaPelaChavePrimaria(turma).CO_FLAG_MULTI_SERIE != "S")
                {
                    var ocorTurma = (from iTB06 in TB06_TURMAS.RetornaTodosRegistros()
                                     where iTB06.CO_EMP == LoginAuxili.CO_EMP && iTB06.TB129_CADTURMAS.CO_TUR == turma
                                     select iTB06).FirstOrDefault();

                    if (ocorTurma != null)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Turma não é multisérie e já existe série associada a mesma.");
                        return;
                    }
                }
            }

            #region Verificação e criação da matéria de turma única
            //---------> Verifica se a turma será única ou não
            if (ddlCO_FLAG_RESP_TURMA.SelectedValue == "S")
            {
//-------------> Verifica se existe uma matéria com sigla "MSR", que é a matéria padrão para turma única
                if (!TB107_CADMATERIAS.RetornaTodosRegistros().Where(cm => cm.NO_SIGLA_MATERIA == "MSR").Any())
                {
//-----------------> Cria uma matéria para ser a padrão de turma única, MSR.
                    TB107_CADMATERIAS cm = new TB107_CADMATERIAS();

                    cm.CO_EMP = LoginAuxili.CO_EMP;
                    cm.NO_SIGLA_MATERIA = "MSR";
                    cm.NO_MATERIA = "Atividades Letivas";
                    cm.NO_RED_MATERIA = "Atividades";
                    cm.DE_MATERIA = "Matéria utilizada no lançamento de atividades para professores de turma única.";
                    cm.CO_STATUS = "A";
                    cm.DT_STATUS = DateTime.Now;
                    cm.CO_CLASS_BOLETIM = 4;
                    TB107_CADMATERIAS.SaveOrUpdate(cm);

                    CurrentCadastroMasterPage.CurrentEntity = cm;

//-----------------> Vincula a matéria MSR ao curso selecionado
                    int idMat = TB107_CADMATERIAS.RetornaTodosRegistros().Where(cma => cma.NO_SIGLA_MATERIA == "MSR").FirstOrDefault().ID_MATERIA;
                    TB02_MATERIA m = new TB02_MATERIA();

                    m.CO_EMP = LoginAuxili.CO_EMP;
                    m.CO_MODU_CUR = modalidade;
                    m.CO_CUR = serie;
                    m.TB01_CURSO = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie);
                    m.ID_MATERIA = idMat;
                    m.QT_CRED_MAT = null;
                    m.QT_CARG_HORA_MAT = 800;
                    m.DT_INCL_MAT = DateTime.Now;
                    m.DT_SITU_MAT = DateTime.Now;
                    m.CO_SITU_MAT = "I";
                    TB02_MATERIA.SaveOrUpdate(m);

                    CurrentCadastroMasterPage.CurrentEntity = m;
                }
                else
                {
//-----------------> Verifica se a matéria MSR está vinculada ao curso
                    var materia = TB107_CADMATERIAS.RetornaTodosRegistros().Where(cm => cm.NO_SIGLA_MATERIA == "MSR" && cm.CO_EMP == LoginAuxili.CO_EMP).FirstOrDefault();

                    if (materia == null)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Não existe uma matéria utilizada no lançamento de atividades para professores de turma única para esta únidade, favor cadastrar a matéria e tentar novamente.");
                        return;
                    }

                    var idMat = materia.ID_MATERIA;

                    if (!TB02_MATERIA.RetornaTodosRegistros().Where(m => m.CO_EMP == LoginAuxili.CO_EMP && m.CO_MODU_CUR == modalidade && m.CO_CUR == serie && m.ID_MATERIA == idMat).Any()) 
                    {
//---------------------> Vincula a matéria MSR ao curso selecionado.
                        TB02_MATERIA m = new TB02_MATERIA();

                        m.CO_EMP = LoginAuxili.CO_EMP;
                        m.CO_MODU_CUR = modalidade;
                        m.CO_CUR = serie;
                        m.TB01_CURSO = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie);
                        m.ID_MATERIA = idMat;
                        m.QT_CRED_MAT = null;
                        m.QT_CARG_HORA_MAT = 800;
                        m.DT_INCL_MAT = DateTime.Now;
                        m.DT_SITU_MAT = DateTime.Now;
                        m.CO_SITU_MAT = "I";
                        TB02_MATERIA.SaveOrUpdate(m);

                        CurrentCadastroMasterPage.CurrentEntity = m;
                    }
                }
            }
            #endregion

            if (Page.IsValid)
            {
                TB06_TURMAS tb06 = TB06_TURMAS.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie, turma);

                if (tb06 == null)
                {
                    tb06 = new TB06_TURMAS();

                    tb06.CO_EMP = LoginAuxili.CO_EMP;
                    tb06.CO_MODU_CUR = modalidade;
                    tb06.CO_CUR = serie;                    
                    tb06.CO_TUR = turma;
                }
                else if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                        AuxiliPagina.EnvioMensagemErro(this, "Já existe uma associação");

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {
                    tb06.FL_INCLU_TUR = true;
                    tb06.FL_ALTER_TUR = false;
                }

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                {
                    tb06.FL_ALTER_TUR = true;
                }
                
                tb06.TB129_CADTURMAS = TB129_CADTURMAS.RetornaPelaChavePrimaria(turma);
                tb06.TB01_CURSO = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie);
                tb06.NO_LOCA_AULA_TUR = txtNO_LOCA_AULA_TUR.Text;
                tb06.NU_SALA_AULA_TUR = txtNU_SALA_AULA_TUR.Text;
                tb06.CO_PERI_TUR = ddlCO_PERI_TUR.SelectedValue;
                tb06.DT_ALT_REGISTRO = DateTime.Now;
                tb06.CO_FLAG_RESP_TURMA = ddlCO_FLAG_RESP_TURMA.SelectedValue;
                if (ckTurmaOnline.Checked)
                {
                    TB06_TURMAS turmaMarcada = (from turmas in TB06_TURMAS.RetornaTodosRegistros()
                                           where turmas.CO_EMP == LoginAuxili.CO_EMP
                                           && turmas.CO_MODU_CUR == modalidade
                                           && turmas.CO_CUR == serie
                                           && (turmas.FL_PREMAT_ONLINE ?? false)
                                           select turmas).FirstOrDefault();
                    if (turmaMarcada != null)
                    {
                        turmaMarcada.FL_PREMAT_ONLINE = false;

                        TB06_TURMAS.SaveOrUpdate(turmaMarcada, false);
                        GestorEntities.CurrentContext.SaveChanges();
                    }
                    tb06.FL_PREMAT_ONLINE = ckTurmaOnline.Checked;
                }
                
                CurrentCadastroMasterPage.CurrentEntity = tb06;
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB06_TURMAS tb06 = RetornaEntidade();

            if (tb06 != null)
            {
                tb06.TB03_COLABORReference.Load();

                ddlModalidade.SelectedValue = tb06.CO_MODU_CUR.ToString();

                CarregaSerieCurso();
                ddlSerieCurso.SelectedValue = tb06.CO_CUR.ToString();

                if (ddlSerieCurso.SelectedValue != "")
                    CarregaTurma();
                
                ddlTurma.SelectedValue = tb06.CO_TUR.ToString();
                txtNO_LOCA_AULA_TUR.Text = tb06.NO_LOCA_AULA_TUR;
                txtNU_SALA_AULA_TUR.Text = tb06.NU_SALA_AULA_TUR;
                ddlCO_PERI_TUR.SelectedValue = tb06.CO_PERI_TUR.ToString();
                ddlCO_FLAG_RESP_TURMA.SelectedValue = tb06.CO_FLAG_RESP_TURMA;
                bool preOnline = (tb06.FL_PREMAT_ONLINE ?? false);
                ckTurmaOnline.Checked = preOnline;
                if (preOnline)
                    ckTurmaOnline.Enabled = false;
                ddlModalidade.Enabled = ddlSerieCurso.Enabled = ddlTurma.Enabled = false;

                if (ddlTurma.SelectedValue != "")
                    CarregaMultiSerie(int.Parse(ddlTurma.SelectedValue));
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB06_TURMAS</returns>
        private TB06_TURMAS RetornaEntidade()
        {
            int modalidade = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoModuCur);
            int serie = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoCur);
            int turma = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoTur);

            TB06_TURMAS tb06 = TB06_TURMAS.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie, turma);
            return (tb06 == null) ? new TB06_TURMAS() : tb06;
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega os dropdowns de turma multiserie, se turma tem responsável e turno da turma.
        /// </summary>
        private void CarregaDropDown()
        {
            ddlCO_FLAG_MULTI_SERIE.Items.Add(new ListItem("Sim", "S"));
            ddlCO_FLAG_MULTI_SERIE.Items.Add(new ListItem("Não", "N"));

            ddlCO_FLAG_RESP_TURMA.Items.Add(new ListItem("Sim", "S"));
            ddlCO_FLAG_RESP_TURMA.Items.Add(new ListItem("Não", "N"));

            ddlCO_PERI_TUR.Items.Add(new ListItem("Matutino", "M"));
            ddlCO_PERI_TUR.Items.Add(new ListItem("Vespertino", "V"));
            ddlCO_PERI_TUR.Items.Add(new ListItem("Noturno", "N"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidade()
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
                ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                            where tb01.CO_MODU_CUR == modalidade
                                            select new { tb01.CO_CUR, tb01.CO_SIGL_CUR }).OrderBy(c => c.CO_SIGL_CUR);

                ddlSerieCurso.DataTextField = "CO_SIGL_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();

                ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
            }
            else
                ddlSerieCurso.Items.Clear();
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            if (modalidade != 0)
            {
                ddlTurma.DataSource = (from tb129 in TB129_CADTURMAS.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                       where tb129.TB44_MODULO.CO_MODU_CUR == modalidade
                                       select new { tb129.CO_SIGLA_TURMA, tb129.CO_TUR }).OrderBy( c => c.CO_SIGLA_TURMA );

                ddlTurma.DataTextField = "CO_SIGLA_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataBind();

                ddlTurma.Items.Insert(0, new ListItem("Selecione", ""));
            }
            else
                ddlTurma.Items.Clear();
        }

        /// <summary>
        /// Método que carrega se Turma selecionada é MultiSerie ou não
        /// </summary>
        /// <param name="cO_TUR">Id da turma</param>
        private void CarregaMultiSerie(int cO_TUR)
        {
            if (cO_TUR != 0)
                ddlCO_FLAG_MULTI_SERIE.SelectedValue = TB129_CADTURMAS.RetornaPelaChavePrimaria(cO_TUR).CO_FLAG_MULTI_SERIE;
        }
        #endregion

        #region Validações

        protected void MultiSerie_ServerValidate(object source, ServerValidateEventArgs e)
        {
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int ocorMultiSerie = 0;

            TB129_CADTURMAS tb129 = TB129_CADTURMAS.RetornaPelaChavePrimaria(turma);

            if (tb129 != null)
            {
                if (tb129.CO_FLAG_MULTI_SERIE.ToUpper() == "N")
                {
                    ocorMultiSerie = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                      where tb06.CO_TUR == turma
                                      select new { tb06.CO_TUR }).ToList().Count;

                    if (ocorMultiSerie <= 1 || QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                        AuxiliPagina.EnvioMensagemErro(this, "Esta série não pode ser associada a uma turma não Multi Série");
                }
            }
            else
                AuxiliPagina.EnvioMensagemErro(this, "É necessário selecionar uma Turma");
        }

        #endregion

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaMultiSerie(int.Parse(ddlTurma.SelectedValue));
        }
    }
}