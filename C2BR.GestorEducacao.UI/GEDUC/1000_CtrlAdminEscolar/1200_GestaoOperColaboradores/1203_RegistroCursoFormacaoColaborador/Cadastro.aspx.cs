//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: GESTÃO OPERACIONAL DE COLABORADORES
// OBJETIVO: ASSOCIAÇÃO DE CURSOS DE FORMAÇÃO ACADÊMICA/ESPECÍFICA A COLABORADORES
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1203_RegistroCursoFormacaoColaborador
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
            if (IsPostBack) return;

            CarregaCategorias();
            CarregaColabs();
            CarregaEspecializacoes();

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                ddlGrauInstrucao.Enabled = ddlEspecializacao.Enabled = true;
                txtDataCadastro.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int coCol = ddlColaborador.SelectedValue != "" ? int.Parse(ddlColaborador.SelectedValue) : 0;
            int coEspec = ddlEspecializacao.SelectedValue != "" ? int.Parse(ddlEspecializacao.SelectedValue) : 0;

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                TB62_CURSO_FORM verOcor = TB62_CURSO_FORM.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, coCol, coEspec, LoginAuxili.ORG_CODIGO_ORGAO);
                if (verOcor != null)
                    AuxiliPagina.EnvioMensagemErro(this, "Colaborador já associado ao curso selecionado!");
            }

            TB62_CURSO_FORM tb62 = RetornaEntidade();

            if (!QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoExclusao))
            {
                if (tb62.CO_COL == 0)
                {
                    tb62.CO_COL = coCol;
                    tb62.CO_EMP = LoginAuxili.CO_EMP;
                    tb62.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
                    var tb03 = TB03_COLABOR.RetornaPeloCoCol(tb62.CO_COL);
                    tb62.TB03_COLABOR = tb03;
                    tb03.TB25_EMPRESA1Reference.Load();
                    tb62.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(tb03.TB25_EMPRESA1.CO_EMP);
                    tb62.TB100_ESPECIALIZACAO = TB100_ESPECIALIZACAO.RetornaPeloCoEspec(coEspec);
                }

                tb62.NU_CARGA_HORARIA = int.Parse(txtCargaHoraria.Text);
                tb62.FLA_SITUA_CURSO = ddlSituacao.SelectedValue;
                tb62.CO_MESANO_INICIO = txtMesAnoInicio.Text.Substring(3, 4) + "/" + txtMesAnoInicio.Text.Substring(0, 2);
                tb62.CO_MESANO_FIM = txtMesAnoFim.Text != "" ? txtMesAnoFim.Text.Substring(3, 4) + "/" + txtMesAnoFim.Text.Substring(0, 2) : "";
                tb62.NU_DIPLO_CURSO = txtNumeroDiploma.Text == "" ? null : (int?)int.Parse(txtNumeroDiploma.Text);
                tb62.NO_INSTIT_CURSO = txtNomeInstituicao.Text;
                tb62.NO_SIGLA_INSTIT_CURSO = txtSigla.Text;
                tb62.NO_CIDADE_CURSO = txtCidade.Text;
                tb62.CO_UF_CURSO = txtUf.Text;
                tb62.NO_INSTI_ORGREG = txtOrgaoRegulamentador.Text;
                tb62.REG_INST_ORGREG = txtRegistro.Text;
                tb62.NO_INSTR_CURSO = txtNomInstrutor.Text != "" ? txtNomInstrutor.Text : null;
                tb62.DT_CADASTRO = DateTime.Now;               

                if (chkCursoPrincipal.Checked)
                {                    
//----------------> Define todos os outros cursos como não principais, alterando o CO_FLAG_CURSO_PRINCIPAL para "N"
                    var regSim = (from lTb62 in TB62_CURSO_FORM.RetornaTodosRegistros()
                                  where lTb62.CO_COL == tb62.CO_COL && lTb62.CO_EMP == tb62.CO_EMP && lTb62.CO_FLAG_CURSO_PRINCIPAL == "S"
                                  select lTb62).FirstOrDefault();
                    
                    if (regSim != null)
                    {
                        regSim.CO_FLAG_CURSO_PRINCIPAL = "N";
                        if (TB62_CURSO_FORM.SaveOrUpdate(regSim, true) < 1) 
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao editar item");
                            return;
                        }
                    }

                    if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                        tb62.TB03_COLABORReference.Load();

                    if (tb62.TB100_ESPECIALIZACAO == null)
                        tb62.TB100_ESPECIALIZACAOReference.Load();

//----------------> Lança o grau de instruçao do colaborador de acordo com o curso selecionado
                    if (tb62.TB100_ESPECIALIZACAO.TP_ESPEC == ClassificacaoCurso.DO.ToString())
                        tb62.TB03_COLABOR.CO_INST = TB18_GRAUINS.RetornaPelaSigla(GrauInstrucao.DOU.ToString()).CO_INST;
                    else if (tb62.TB100_ESPECIALIZACAO.TP_ESPEC == ClassificacaoCurso.ES.ToString())
                        tb62.TB03_COLABOR.CO_INST = TB18_GRAUINS.RetornaPelaSigla(GrauInstrucao.ESP.ToString()).CO_INST;
                    else if (tb62.TB100_ESPECIALIZACAO.TP_ESPEC == ClassificacaoCurso.GR.ToString())
                    {
                        if (tb62.FLA_SITUA_CURSO == SituacaoCursoFormacao.F.ToString())
                            tb62.TB03_COLABOR.CO_INST = TB18_GRAUINS.RetornaPelaSigla(GrauInstrucao.SPR.ToString()).CO_INST;
                        else
                            tb62.TB03_COLABOR.CO_INST = TB18_GRAUINS.RetornaPelaSigla(GrauInstrucao.SPI.ToString()).CO_INST;
                    }
                    else if (tb62.TB100_ESPECIALIZACAO.TP_ESPEC == ClassificacaoCurso.GR.ToString())
                        tb62.TB03_COLABOR.CO_INST = TB18_GRAUINS.RetornaPelaSigla(GrauInstrucao.SPR.ToString()).CO_INST;
                    else if (tb62.TB100_ESPECIALIZACAO.TP_ESPEC == ClassificacaoCurso.MB.ToString())
                        tb62.TB03_COLABOR.CO_INST = TB18_GRAUINS.RetornaPelaSigla(GrauInstrucao.MBA.ToString()).CO_INST;
                    else if (tb62.TB100_ESPECIALIZACAO.TP_ESPEC == ClassificacaoCurso.ME.ToString())
                        tb62.TB03_COLABOR.CO_INST = TB18_GRAUINS.RetornaPelaSigla(GrauInstrucao.MES.ToString()).CO_INST;
                    else if (tb62.TB100_ESPECIALIZACAO.TP_ESPEC == ClassificacaoCurso.PD.ToString())
                        tb62.TB03_COLABOR.CO_INST = TB18_GRAUINS.RetornaPelaSigla(GrauInstrucao.PDR.ToString()).CO_INST;
                    else if (tb62.TB100_ESPECIALIZACAO.TP_ESPEC == ClassificacaoCurso.PG.ToString())
                        tb62.TB03_COLABOR.CO_INST = TB18_GRAUINS.RetornaPelaSigla(GrauInstrucao.PGR.ToString()).CO_INST;

//----------------> Define o curso como principal, alterando o CO_FLAG_CURSO_PRINCIPAL para "S"
                    tb62.CO_FLAG_CURSO_PRINCIPAL = "S";
                }
                else
                    tb62.CO_FLAG_CURSO_PRINCIPAL = "N";
            }

            CurrentPadraoCadastros.CurrentEntity = tb62;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB62_CURSO_FORM tb62 = RetornaEntidade();

            if (tb62 != null)
            {
                tb62.TB100_ESPECIALIZACAOReference.Load();

                ddlColaborador.SelectedValue = tb62.CO_COL.ToString();
                ddlGrauInstrucao.SelectedValue = tb62.TB100_ESPECIALIZACAO.TP_ESPEC;
                CarregaEspecializacoes();
                ddlEspecializacao.SelectedValue = tb62.CO_ESPEC.ToString();
                if (ddlEspecializacao.SelectedValue != "")
                {
                    CarregaPontuacao(int.Parse(ddlEspecializacao.SelectedValue));
                }
                txtCargaHoraria.Text = tb62.NU_CARGA_HORARIA.ToString();
                ddlSituacao.SelectedValue = tb62.FLA_SITUA_CURSO;
                txtMesAnoInicio.Text = tb62.CO_MESANO_INICIO.Substring(5, 2) + "/" + tb62.CO_MESANO_INICIO.Substring(0, 4);
                txtMesAnoFim.Text = tb62.CO_MESANO_FIM == null ? "" : tb62.CO_MESANO_FIM.Substring(5, 2) + "/" + tb62.CO_MESANO_FIM.Substring(0, 4);
                txtNumeroDiploma.Text = tb62.NU_DIPLO_CURSO.ToString();
                txtNomeInstituicao.Text = tb62.NO_INSTIT_CURSO;
                txtSigla.Text = tb62.NO_SIGLA_INSTIT_CURSO;
                txtCidade.Text = tb62.NO_CIDADE_CURSO;
                txtUf.Text = tb62.CO_UF_CURSO;
                txtOrgaoRegulamentador.Text = tb62.NO_INSTI_ORGREG;
                txtRegistro.Text = tb62.REG_INST_ORGREG;
                txtDataCadastro.Text = tb62.DT_CADASTRO.ToString("dd/MM/yyyy");
                chkCursoPrincipal.Checked = tb62.CO_FLAG_CURSO_PRINCIPAL == "S";
                txtNomInstrutor.Text = tb62.NO_INSTR_CURSO != null ? tb62.NO_INSTR_CURSO : "";
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB62_CURSO_FORM</returns>
        private TB62_CURSO_FORM RetornaEntidade()
        {
            TB62_CURSO_FORM tb62 = TB62_CURSO_FORM.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoCol),
                QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id), LoginAuxili.ORG_CODIGO_ORGAO);
            return (tb62 == null) ? new TB62_CURSO_FORM() : tb62;
        }        

        /// <summary>
        /// Método que preenche a pontuação para promoção
        /// </summary>
        /// <param name="id">Id da especialização</param>
        /// <returns>Entidade TB62_CURSO_FORM</returns>
        private void CarregaPontuacao(int id)
        {
            int? pontuEspec = TB100_ESPECIALIZACAO.RetornaPeloCoEspec(id).QT_PONTU;

            txtPontos.Text = pontuEspec != null ? pontuEspec.ToString() : "";
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Grau de Instrução
        /// </summary>
        private void CarregaCategorias() 
        {
            ddlGrauInstrucao.Load<ClassificacaoCurso>();
            ddlGrauInstrucao.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Colaborador
        /// </summary>
        private void CarregaColabs()
        {
            ddlColaborador.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(LoginAuxili.CO_EMP)
                                         select new { tb03.NO_COL, tb03.CO_COL }).OrderBy( c => c.NO_COL );

            ddlColaborador.DataTextField = "NO_COL";
            ddlColaborador.DataValueField = "CO_COL";
            ddlColaborador.DataBind();

            ddlColaborador.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Especialização
        /// </summary>
        private void CarregaEspecializacoes() 
        {
            ddlEspecializacao.DataSource = (from tb100 in TB100_ESPECIALIZACAO.RetornaPeloTipo(ddlGrauInstrucao.SelectedValue)
                                            select new { tb100.CO_ESPEC, tb100.DE_ESPEC }).OrderBy( e => e.DE_ESPEC );

            ddlEspecializacao.DataTextField = "DE_ESPEC";
            ddlEspecializacao.DataValueField = "CO_ESPEC";
            ddlEspecializacao.DataBind();

            ddlEspecializacao.Items.Insert(0, new ListItem("Selecione", ""));            
        }
        #endregion    
  
        #region Validadores

        protected void cvMesAnoInicio_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if (txtMesAnoFim.Enabled)
            {
                if (DateTime.Parse(txtMesAnoInicio.Text) > DateTime.Parse(txtMesAnoFim.Text))
                {
                    e.IsValid = false;
                    AuxiliPagina.EnvioMensagemErro(this, "");
                }
            }
        }

        protected void cvMesAnoTermino_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if (txtMesAnoFim.Enabled)
            {
                if (DateTime.Parse(txtMesAnoFim.Text) < DateTime.Parse(txtMesAnoInicio.Text))
                {
                    e.IsValid = false;
                    AuxiliPagina.EnvioMensagemErro(this, "");
                }
            }
        }
        #endregion

        protected void ddlSituacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMesAnoFim.Enabled = txtNumeroDiploma.Enabled = ddlSituacao.SelectedValue == SituacaoCursoFormacao.F.ToString();
        }

        protected void ddlGrauInstrucao_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaEspecializacoes();
        }

        protected void ddlEspecializacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEspecializacao.SelectedValue != "")
            {
                CarregaPontuacao(int.Parse(ddlEspecializacao.SelectedValue));
            }
        }
    }
}
