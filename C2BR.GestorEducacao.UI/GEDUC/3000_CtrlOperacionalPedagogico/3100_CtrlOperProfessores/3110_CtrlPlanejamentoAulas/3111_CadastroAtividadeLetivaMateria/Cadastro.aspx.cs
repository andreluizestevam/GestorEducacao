//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE LETIVO DE AULAS E ATIVIDADES
// OBJETIVO: CADASTRO DAS ATIVIDADES LETIVAS DE MATÉRIAS POR SÉRIE/TURMA
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+----------------------------------------
// 16/04/2013| André Nobre Vinagre        | Aumentei o campo de objetivo, metodologia e
//           |                            | material usado
// ----------+----------------------------+----------------------------------------
// 24/04/2013| Victor Martins Machado     | Foi alterada a combo de tipo de atividade
//           |                            | para pegar ta tabela TB273 e incluída
//           |                            | a função que pega o campo FL_LANCA_NOTA_ATIV
//           |                            | e atualiza no dropdownlist "Lanca nota?".
//           |                            | 
// ----------+----------------------------+----------------------------------------
// 09/08/2013| André Nobre Vinagre        | Na alteração coloquei para carregar o professor
//           |                            | direto da tabela de colaborador
//           |                            | 
// ----------+----------------------------+----------------------------------------
// 28/03/2014| Victor Martins Machado     | Inclusão da regra da matéria de turma única, sigla MSR.
//           |                            | Onde o sistema utilizará uma matéria pré-cadastrada
//           |                            | para cadastrar as informações de turma única.
//           |                            | 
// ----------+----------------------------+----------------------------------------
// 03/07/2014| Victor Martins Machado     | Comentado o código que inicia a combo de disciplina para
//           |                            | evitar um erro na hora de carregar o formulário
//           |                            | 

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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3100_CtrlOperProfessores.F3110_CtrlPlanejamentoAulas.F3111_CadastroAtividadeLetivaMateria
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
            string dataAtual = DateTime.Now.Date.ToString("dd/MM/yyyy");

            if (Page.IsPostBack) return;

            CarregaAnos();
            CarregaModalidades();

            if (LoginAuxili.TIPO_USU.Equals("A"))
            {
                var tb08 = TB08_MATRCUR.RetornaPeloAluno(LoginAuxili.CO_RESP);
                tb08.TB44_MODULOReference.Load();

                if (!string.IsNullOrEmpty(tb08.CO_ANO_MES_MAT) && ddlAno.Items.FindByValue(tb08.CO_ANO_MES_MAT) != null)
                    ddlAno.SelectedValue = tb08.CO_ANO_MES_MAT;

                if (tb08.TB44_MODULO.CO_MODU_CUR != 0 && ddlModalidade.Items.FindByValue(tb08.TB44_MODULO.CO_MODU_CUR.ToString()) != null)
                    ddlModalidade.SelectedValue = tb08.TB44_MODULO.CO_MODU_CUR.ToString();

                CarregaSerieCurso();

                if (tb08.CO_CUR != 0 && ddlSerieCurso.Items.FindByValue(tb08.CO_CUR.ToString()) != null)
                    ddlSerieCurso.SelectedValue = tb08.CO_CUR.ToString();

                CarregaTurma();

                if (tb08.CO_TUR != 0 && ddlTurma.Items.FindByValue(tb08.CO_TUR.ToString()) != null)
                    ddlTurma.SelectedValue = tb08.CO_TUR.ToString();

                ddlAno.Enabled =
                ddlModalidade.Enabled =
                ddlSerieCurso.Enabled =
                ddlTurma.Enabled = false;
            }
            else
            {
                CarregaSerieCurso();
                CarregaTurma();
            }

            CarregaMaterias();
            CarregaProfessor();
            CarregaTempo();
            CarregaTipoAtividade();
            CarregaGridBNCC();
            CarregaGrideConteudoBiblio();
            CarregaGrideConteudoProgra();
            txtDataCadastro.Text = txtDataStatus.Text = dataAtual;            
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            
            int coAnoRefPla = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int materia = ddlDisciplina.SelectedValue != "" ? int.Parse(ddlDisciplina.SelectedValue) : 0;
            int tipoAtiv = ddlTipoAtiv.SelectedValue != "" ? int.Parse(ddlTipoAtiv.SelectedValue) : 0;

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
            {
                int coPlaAula = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);

                if (TB17_PLANO_AULA.RetornaPelaChavePrimaria(coPlaAula).FLA_HOMOLOG == "S")
	            {
                    AuxiliPagina.EnvioMensagemErro(this, "Atividade Letiva já homologada.");
                    return;
	            }
            }

            TB17_PLANO_AULA tb17 = RetornaEntidade();

            if (tb17 == null)
            {
                tb17 = new TB17_PLANO_AULA();
                tb17.CO_EMP = LoginAuxili.CO_EMP;
                tb17.CO_CUR = serie;
                tb17.CO_MODU_CUR = modalidade;
                tb17.CO_TUR = turma;
                tb17.CO_MAT = materia;
            }

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                tb17.FL_INCLU_PLA = true;
                tb17.FL_ALTER_PLA = false;
            }

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
            {
                tb17.FL_ALTER_PLA = true;
            }

            TB273_TIPO_ATIVIDADE tb273 = TB273_TIPO_ATIVIDADE.RetornaPelaChavePrimaria(tipoAtiv);

            tb17.CO_ANO_REF_PLA = coAnoRefPla;
            tb17.DT_PREV_PLA = DateTime.Parse(txtDataAula.Text);
            tb17.NU_TEMP_PLA = ddlTempo.SelectedValue != "" && ddlTempo.SelectedValue != "0" ? decimal.Parse(ddlTempo.SelectedValue.ToString().Substring(0, 1)) : 0;
            tb17.HR_INI_AULA_PLA = txtHoraI.Text != "" ? txtHoraI.Text : "00:00";
            tb17.HR_FIM_AULA_PLA = txtHoraF.Text != "" ? txtHoraF.Text : "00:00";
            tb17.QT_CARG_HORA_PLA = txtDuracao.Text != "" ? decimal.Parse(txtDuracao.Text.Replace(":", "")) : 0;
            int coCol = int.Parse(ddlProfessor.SelectedValue);
            tb17.TB03_COLABOR = TB03_COLABOR.RetornaPeloCoCol(coCol);
            tb17.DE_TEMA_AULA = txtTemaAula.Text;
            tb17.DT_SITU_PLA = DateTime.Parse(txtDataCadastro.Text);
            tb17.CO_SITU_PLA = ddlSituacao.SelectedValue;
            tb17.DE_OBJE_AULA = txtResumoObjetivo.Text; 
            tb17.DE_METODOLOGIA = txtResumoMetodologia.Text;
            //tb17.DE_CONTEUDO = txtResumoConteudo.Text;
            tb17.TB273_TIPO_ATIVIDADE = tb273;
            tb17.DE_MATE_USADO = txtResumoMaterial.Text;
            tb17.FL_PLANEJ_ATIV = ddlAtivPlanej.SelectedValue;
            tb17.CO_TIPO_ATIV = tb273.CO_SIGLA_ATIV;
            
            tb17.FL_AVALIA_ATIV = ddlAvaliaAtiv.SelectedValue;

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                var lastCoPlaAula = (from lTb17 in TB17_PLANO_AULA.RetornaTodosRegistros()
                                     select lTb17);

                int maxCoPlaAula = 1;

                if (lastCoPlaAula.Count() > 0)
                {
                    maxCoPlaAula = lastCoPlaAula.Max( l => l.CO_PLA_AULA ) + 1;
                }

//------------> Varre toda a gride de Conteúdo Programático
                foreach (GridViewRow linha in grdConteuProgra.Rows)
                {                    
                    if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                    {
                        int idReferConte = Convert.ToInt32(((HiddenField)linha.Cells[0].FindControl("hdIdReferConte")).Value);

                        TB311_REFER_ATIVID_AULAS tb311 = new TB311_REFER_ATIVID_AULAS();

                        tb311.ID_CTRL_ACESSO = maxCoPlaAula;
                        tb311.FL_TIPO_ACESSO = "P";
                        tb311.CO_IP_CADAS = LoginAuxili.IP_USU;
                        tb311.CO_STATUS = "A";
                        tb311.DT_CADAS_REFER = DateTime.Now;
                        tb311.TB03_COLABOR = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL);
                        tb311.ID_REFER_CONTE = idReferConte;

                        TB311_REFER_ATIVID_AULAS.SaveOrUpdate(tb311, false);
                    }
                }

//------------> Varre toda a gride de Conteúdo Bibliográfico
                foreach (GridViewRow linha in grdConteuBibli.Rows)
                {
                    if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                    {
                        int idReferConte = Convert.ToInt32(((HiddenField)linha.Cells[0].FindControl("hdIdReferConte")).Value);

                        TB311_REFER_ATIVID_AULAS tb311 = new TB311_REFER_ATIVID_AULAS();

                        tb311.ID_CTRL_ACESSO = maxCoPlaAula;
                        tb311.FL_TIPO_ACESSO = "P";
                        tb311.CO_IP_CADAS = LoginAuxili.IP_USU;
                        tb311.CO_STATUS = "A";
                        tb311.DT_CADAS_REFER = DateTime.Now;
                        tb311.TB03_COLABOR = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL);
                        tb311.ID_REFER_CONTE = idReferConte;

                        TB311_REFER_ATIVID_AULAS.SaveOrUpdate(tb311, false);
                    }
                }
                //------------> Varre toda a gride de Conteúdo BNCC
                foreach (GridViewRow linha in grdbncc.Rows)
                {
                    if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                    {
                        int idReferConte = Convert.ToInt32(((HiddenField)linha.Cells[0].FindControl("hdIdReferConte")).Value);

                        TB311_REFER_ATIVID_AULAS tb311 = new TB311_REFER_ATIVID_AULAS();

                        tb311.ID_CTRL_ACESSO = maxCoPlaAula;
                        tb311.FL_TIPO_ACESSO = "P";
                        tb311.CO_IP_CADAS = LoginAuxili.IP_USU;
                        tb311.CO_STATUS = "A";
                        tb311.DT_CADAS_REFER = DateTime.Now;
                        tb311.TB03_COLABOR = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL);
                        tb311.ID_REFER_CONTE = idReferConte;

                        TB311_REFER_ATIVID_AULAS.SaveOrUpdate(tb311, false);
                    }
                }
            }
            else if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
            {
//------------> Varre toda a gride de Conteúdo Programático
                foreach (GridViewRow linha in grdConteuProgra.Rows)
                {
                    int idReferConte = Convert.ToInt32(((HiddenField)linha.Cells[0].FindControl("hdIdReferConte")).Value);

                    if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                    {
                        var tb311 = (from lTb311 in TB311_REFER_ATIVID_AULAS.RetornaTodosRegistros()
                                    where lTb311.ID_REFER_CONTE == idReferConte
                                    && lTb311.ID_CTRL_ACESSO == tb17.CO_PLA_AULA && lTb311.FL_TIPO_ACESSO == "P"
                                    select lTb311).FirstOrDefault();

                        if (tb311 == null)
                        {
                            tb311 = new TB311_REFER_ATIVID_AULAS();

                            tb311.ID_CTRL_ACESSO = tb17.CO_PLA_AULA;
                            tb311.FL_TIPO_ACESSO = "P";
                            tb311.CO_IP_CADAS = LoginAuxili.IP_USU;
                            tb311.CO_STATUS = "A";
                            tb311.DT_CADAS_REFER = DateTime.Now;
                            tb311.TB03_COLABOR = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL);
                            tb311.ID_REFER_CONTE = idReferConte;

                            TB311_REFER_ATIVID_AULAS.SaveOrUpdate(tb311, false);
                        }
                    }
                    else
                    {
                        var tb311 = (from lTb311 in TB311_REFER_ATIVID_AULAS.RetornaTodosRegistros()
                                     where lTb311.ID_REFER_CONTE == idReferConte
                                     && lTb311.ID_CTRL_ACESSO == tb17.CO_PLA_AULA && lTb311.FL_TIPO_ACESSO == "P"
                                     select lTb311).FirstOrDefault();

                        if (tb311 != null)
                        {
                            TB311_REFER_ATIVID_AULAS.Delete(tb311, false);
                        }
                    }
                }

//------------> Varre toda a gride de Conteúdo Bibliográfico
                foreach (GridViewRow linha in grdConteuBibli.Rows)
                {
                    int idReferConte = Convert.ToInt32(((HiddenField)linha.Cells[0].FindControl("hdIdReferConte")).Value);

                    if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                    {
                        var tb311 = (from lTb311 in TB311_REFER_ATIVID_AULAS.RetornaTodosRegistros()
                                     where lTb311.ID_REFER_CONTE == idReferConte
                                     && lTb311.ID_CTRL_ACESSO == tb17.CO_PLA_AULA && lTb311.FL_TIPO_ACESSO == "P"
                                     select lTb311).FirstOrDefault();

                        if (tb311 == null)
                        {
                            tb311 = new TB311_REFER_ATIVID_AULAS();

                            tb311.ID_CTRL_ACESSO = tb17.CO_PLA_AULA;
                            tb311.FL_TIPO_ACESSO = "P";
                            tb311.CO_IP_CADAS = LoginAuxili.IP_USU;
                            tb311.CO_STATUS = "A";
                            tb311.DT_CADAS_REFER = DateTime.Now;
                            tb311.TB03_COLABOR = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL);
                            tb311.ID_REFER_CONTE = idReferConte;

                            TB311_REFER_ATIVID_AULAS.SaveOrUpdate(tb311, false);
                        }
                    }
                    else
                    {
                        var tb311 = (from lTb311 in TB311_REFER_ATIVID_AULAS.RetornaTodosRegistros()
                                     where lTb311.ID_REFER_CONTE == idReferConte
                                     && lTb311.ID_CTRL_ACESSO == tb17.CO_PLA_AULA && lTb311.FL_TIPO_ACESSO == "P"
                                     select lTb311).FirstOrDefault();

                        if (tb311 != null)
                        {
                            TB311_REFER_ATIVID_AULAS.Delete(tb311, false);
                        }
                    }
                }

                //------------> Varre toda a gride de Conteúdo BNCC
                foreach (GridViewRow linha in grdbncc.Rows)
                {
                    if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                    {
                        int idReferConte = Convert.ToInt32(((HiddenField)linha.Cells[0].FindControl("hdIdReferConte")).Value);

                        TB311_REFER_ATIVID_AULAS tb311 = new TB311_REFER_ATIVID_AULAS();

                        tb311.ID_CTRL_ACESSO = tb17.CO_PLA_AULA;
                        tb311.FL_TIPO_ACESSO = "P";
                        tb311.CO_IP_CADAS = LoginAuxili.IP_USU;
                        tb311.CO_STATUS = "A";
                        tb311.DT_CADAS_REFER = DateTime.Now;
                        tb311.TB03_COLABOR = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL);
                        tb311.ID_REFER_CONTE = idReferConte;

                        TB311_REFER_ATIVID_AULAS.SaveOrUpdate(tb311, false);
                    }
                }
            }

            CurrentPadraoCadastros.CurrentEntity = tb17;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB17_PLANO_AULA tb17 = RetornaEntidade();

            if (tb17 != null)
            {
                ddlAno.SelectedValue = tb17.CO_ANO_REF_PLA.ToString() + "  ";
                CarregaModalidades();
                ddlModalidade.SelectedValue = tb17.CO_MODU_CUR.ToString();
                CarregaSerieCurso();
                ddlSerieCurso.SelectedValue = tb17.CO_CUR.ToString();
                CarregaTurma();
                ddlTurma.SelectedValue = tb17.CO_TUR.ToString();
                CarregaMaterias();
                ddlDisciplina.SelectedValue = tb17.CO_MAT.ToString();
                CarregaTempo();

                txtDataAula.Text = tb17.DT_PREV_PLA.ToString("dd/MM/yyyy");

                if (tb17.NU_TEMP_PLA > 0)
                {
                    int nuTempPla = int.Parse(tb17.NU_TEMP_PLA.ToString());

                    var tb131 = (from lTb131 in TB131_TEMPO_AULA.RetornaTodosRegistros()
                                 where lTb131.CO_EMP == LoginAuxili.CO_EMP && lTb131.CO_MODU_CUR == tb17.CO_MODU_CUR
                                 && lTb131.CO_CUR == tb17.CO_CUR && lTb131.NR_TEMPO == nuTempPla
                                 select new
                                 {
                                     lTb131.HR_INICIO,
                                     lTb131.HR_TERMI,
                                     lTb131.TP_TURNO,
                                     lTb131.NR_TEMPO
                                 }).FirstOrDefault();

                    string strDescTempo = "";
                    if(tb131 != null)
                        strDescTempo = tb131.NR_TEMPO.ToString() + tb131.TP_TURNO;

                    ddlTempo.SelectedValue = strDescTempo;

                    txtHoraI.Text = tb17.HR_INI_AULA_PLA;
                    txtHoraF.Text = tb17.HR_FIM_AULA_PLA;

                    int intTempoDeAula = (int.Parse(txtHoraF.Text.Replace(":", "")) - int.Parse(txtHoraI.Text.Replace(":", "")));

                    if (intTempoDeAula > 60)
                        intTempoDeAula = intTempoDeAula - 45;

                    txtDuracao.Text = intTempoDeAula.ToString();
                }
                else
                {
                    ddlTempo.SelectedValue = "";
                    txtHoraI.Text = tb17.HR_INI_AULA_PLA;
                    txtHoraF.Text = tb17.HR_FIM_AULA_PLA;
                    txtDuracao.Text = "0";
                }

                tb17.TB03_COLABORReference.Load();
                tb17.TB273_TIPO_ATIVIDADEReference.Load();

                //CarregaProfessor();
                ddlProfessor.Items.Clear();
                ddlProfessor.Items.Insert(0, new ListItem(tb17.TB03_COLABOR.NO_COL, tb17.TB03_COLABOR.CO_COL.ToString()));
                //ddlProfessor.SelectedValue = tb17.TB03_COLABOR.CO_COL.ToString();

                txtDataCadastro.Text = tb17.DT_SITU_PLA.ToString();
                txtDataStatus.Text = tb17.DT_SITU_PLA.ToString();

                ddlSituacao.SelectedValue = tb17.CO_SITU_PLA;

                txtResumoObjetivo.Text = tb17.DE_OBJE_AULA;
                //txtResumoConteudo.Text = tb17.DE_CONTEUDO;
                txtResumoMetodologia.Text = tb17.DE_METODOLOGIA;
                txtResumoMaterial.Text = tb17.DE_MATE_USADO;
                txtTemaAula.Text = tb17.DE_TEMA_AULA;
                ddlAtivPlanej.SelectedValue = tb17.FL_PLANEJ_ATIV;
                ddlTipoAtiv.SelectedValue = tb17.TB273_TIPO_ATIVIDADE != null ? tb17.TB273_TIPO_ATIVIDADE.ID_TIPO_ATIV.ToString() : "";
                ddlAvaliaAtiv.SelectedValue = tb17.FL_AVALIA_ATIV;

//------------> Se registro já homologado, desabilita os campos para edição, só habilitando para consulta
                if (tb17.FLA_HOMOLOG == "S")            
                    HabilitaControles("H");  
                else
                    HabilitaControles("E");

                CarregaGrideConteudoBiblio();
                CarregaGrideConteudoProgra();
                CarregaGridBNCC();
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB17_PLANO_AULA</returns>
        private TB17_PLANO_AULA RetornaEntidade()
        {
            TB17_PLANO_AULA tb17 = TB17_PLANO_AULA.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return tb17;
        }

        /// <summary>
        /// Método que controla se os campos que devem ficar habilitados ou não
        /// </summary>
        /// <param name="tipo">Se "H"omologado ou "E"m aberto</param>
        private void HabilitaControles(string tipo)
        {
            if (tipo == "E")
            {
                ddlAno.Enabled =                
                ddlSerieCurso.Enabled =
                ddlTurma.Enabled =
                ddlModalidade.Enabled =
                ddlProfessor.Enabled =
                ddlDisciplina.Enabled = false;

                txtDataStatus.Enabled = true;
            }

            if (tipo == "H")
            {
                ddlAno.Enabled =
                ddlSerieCurso.Enabled =
                ddlTurma.Enabled =
                ddlModalidade.Enabled =
                ddlProfessor.Enabled =
                ddlDisciplina.Enabled =
                ddlAtivPlanej.Enabled =
                ddlTipoAtiv.Enabled =
                ddlTempo.Enabled =
                ddlSituacao.Enabled =
                txtDataAula.Enabled =
                txtTemaAula.Enabled =
                txtDataStatus.Enabled =
                txtResumoMaterial.Enabled =
                txtResumoObjetivo.Enabled = 
                txtResumoMetodologia.Enabled = 
                grdConteuBibli.Enabled =
                grdConteuProgra.Enabled = false;
                grdbncc.Enabled = false;
            };
        }

        private void CarregaGrideConteudoBiblio()
        {
            int coModuCur = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int coCur = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int idMateria = ddlDisciplina.SelectedValue != "" ? int.Parse(ddlDisciplina.SelectedValue) : 0;

            if (System.Configuration.ConfigurationManager.AppSettings["Testes"] != null)
            {
                if (System.Configuration.ConfigurationManager.AppSettings["Testes"] == "Sim")
                {
                    coModuCur = 19;
                    coCur = 2402;
                    idMateria = 3402;
                }
            }
            //if (idMateria != 0)
            //{
            //    var tb02 = (from lTb02 in TB02_MATERIA.RetornaTodosRegistros()
            //                where lTb02.CO_MAT == idMateria
            //                select new { lTb02.ID_MATERIA }).FirstOrDefault();

            //    idMateria = tb02 != null ? tb02.ID_MATERIA : 0;
            //}

            var tb310 = from lTb310 in TB310_REFER_CONTEUDO.RetornaTodosRegistros()
                        where (coModuCur != 0 ? (lTb310.CO_MODU_CUR == coModuCur || lTb310.CO_MODU_CUR == null) : coModuCur == 0) && lTb310.CO_TIPO_REFER_CONTE == "B" &&
                        (coCur != 0 ? (lTb310.CO_CUR == coCur || lTb310.CO_CUR == null) : coCur == 0) && lTb310.ID_MATERIA == idMateria
                        select new
                        {
                            lTb310.NO_TITUL_REFER_CONTE, lTb310.ID_REFER_CONTE, DE_LINK_EXTER = lTb310.DE_LINK_EXTER != null ? lTb310.DE_LINK_EXTER : "#",
                            TARGET = lTb310.DE_LINK_EXTER != null ? "_blank" : "",
                            DE_REFER_CONTE = lTb310.DE_REFER_CONTE.Length > 100 ? lTb310.DE_REFER_CONTE.Substring(0, 100) + "..." : lTb310.DE_REFER_CONTE,                            
                            DES_NIVEL_APREN = lTb310.CO_NIVEL_APREN == "F" ? "Fácil" : lTb310.CO_NIVEL_APREN == "M" ? "Médio" :
                            lTb310.CO_NIVEL_APREN == "D" ? "Difícil" : lTb310.CO_NIVEL_APREN == "A" ? "Avançado" : "Sem Registro"
                        };

            grdConteuBibli.DataSource = tb310;

            grdConteuBibli.DataBind();
        }

        private void CarregaGridBNCC()
        {
            int coModuCur = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int coCur = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int idMateria = ddlDisciplina.SelectedValue != "" ? int.Parse(ddlDisciplina.SelectedValue) : 0;

            var tb310 = from lTb310 in TB310_REFER_CONTEUDO.RetornaTodosRegistros()
                        where (coModuCur != 0 ? (lTb310.CO_MODU_CUR == coModuCur || lTb310.CO_MODU_CUR == null) : coModuCur == 0) && lTb310.BNCC == "Sim" &&
                        (coCur != 0 ? (lTb310.CO_CUR == coCur || lTb310.CO_CUR == null) : coCur == 0) && lTb310.ID_MATERIA == idMateria
                        select new
                        {                            
                            lTb310.COD_BNCC,
                            lTb310.NO_TITUL_REFER_CONTE,
                            lTb310.ID_REFER_CONTE,
                            DE_LINK_EXTER = lTb310.DE_LINK_EXTER != null ? lTb310.DE_LINK_EXTER : "#",
                            TARGET = lTb310.DE_LINK_EXTER != null ? "_blank" : "",
                            DE_REFER_CONTE = lTb310.DE_REFER_CONTE.Length > 100 ? lTb310.DE_REFER_CONTE.Substring(0, 100) + "..." : lTb310.DE_REFER_CONTE,
                            DES_NIVEL_APREN = lTb310.CO_NIVEL_APREN == "F" ? "Fácil" : lTb310.CO_NIVEL_APREN == "M" ? "Médio" :
                            lTb310.CO_NIVEL_APREN == "D" ? "Difícil" : lTb310.CO_NIVEL_APREN == "A" ? "Avançado" : "Sem Registro"
                        };
            grdbncc.DataSource = tb310;
            grdbncc.DataBind();
        }

        private void CarregaGrideConteudoProgra()
        {
            int coModuCur = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int coCur = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int idMateria = ddlDisciplina.SelectedValue != "" ? int.Parse(ddlDisciplina.SelectedValue) : 0;

            // André
            if (System.Configuration.ConfigurationManager.AppSettings["Testes"] != null)
            {
                if (System.Configuration.ConfigurationManager.AppSettings["Testes"] == "Sim")
                {
                    coModuCur = 19;
                    coCur = 2402;
                    idMateria = 3402;
                }
            }
            var tb310 = from lTb310 in TB310_REFER_CONTEUDO.RetornaTodosRegistros()
                        where (coModuCur != 0 ? (lTb310.CO_MODU_CUR == coModuCur || lTb310.CO_MODU_CUR == null) : coModuCur == 0) && lTb310.CO_TIPO_REFER_CONTE == "C" &&
                        (coCur != 0 ? (lTb310.CO_CUR == coCur || lTb310.CO_CUR == null) : coCur == 0) && lTb310.ID_MATERIA == idMateria
                        select new
                        {
                            lTb310.NO_TITUL_REFER_CONTE,                            
                            lTb310.ID_REFER_CONTE,
                            DE_LINK_EXTER = lTb310.DE_LINK_EXTER != null ? lTb310.DE_LINK_EXTER : "#",
                            TARGET = lTb310.DE_LINK_EXTER != null ? "_blank" : "",
                            DE_REFER_CONTE = lTb310.DE_REFER_CONTE.Length > 100 ? lTb310.DE_REFER_CONTE.Substring(0, 100) + "..." : lTb310.DE_REFER_CONTE,
                            DES_NIVEL_APREN = lTb310.CO_NIVEL_APREN == "F" ? "Fácil" : lTb310.CO_NIVEL_APREN == "M" ? "Médio" :
                            lTb310.CO_NIVEL_APREN == "D" ? "Difícil" : lTb310.CO_NIVEL_APREN == "A" ? "Avançado" : "Sem Registro"
                        };

            grdConteuProgra.DataSource = tb310;
            grdConteuProgra.DataBind();
        }

        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            ddlAno.DataSource  = (from tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                 select new { tb43.CO_ANO_GRADE }).Distinct().OrderByDescending( g => g.CO_ANO_GRADE );

            //ddlAno.DataSource = from re in resultado
            //                    select new { CO_ANO_GRADE = re.CO_ANO_GRADE.Trim() };


            ddlAno.DataTextField = "CO_ANO_GRADE";
            ddlAno.DataValueField = "CO_ANO_GRADE";
            ddlAno.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades, verifica se o usuário logado é professor.
        /// </summary>
        private void CarregaModalidades()
        {
            int ano = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;

            if (LoginAuxili.FLA_PROFESSOR != "S")
            {
                //ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where(m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);
                AuxiliCarregamentos.carregaModalidades(ddlModalidade,LoginAuxili.ORG_CODIGO_ORGAO, false);
            }
            else
            {
                var res = (from rm in TB_RESPON_MATERIA.RetornaTodosRegistros()
                                            join mo in TB44_MODULO.RetornaTodosRegistros() on rm.CO_MODU_CUR equals mo.CO_MODU_CUR
                                            where rm.CO_COL_RESP == LoginAuxili.CO_COL
                                            && rm.CO_ANO_REF == ano
                                            select new
                                            {
                                                mo.DE_MODU_CUR,
                                                rm.CO_MODU_CUR
                                            }).Distinct();

                ddlModalidade.DataTextField = "DE_MODU_CUR";
                ddlModalidade.DataValueField = "CO_MODU_CUR";
                ddlModalidade.DataSource = res;
                ddlModalidade.DataBind();

                if (res.Count() != 1)               
                    ddlModalidade.Items.Insert(0, new ListItem("Selecione", ""));                 
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries, verifica se o usuário logado  é professor.
        /// </summary>
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            string anoGrade = ddlAno.SelectedValue;
            int ano = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;

            //if (modalidade != 0)
            //{
                if (LoginAuxili.FLA_PROFESSOR != "S")
                {
                    //ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                    //                            where tb01.CO_MODU_CUR == modalidade
                    //                            join tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on tb01.CO_CUR equals tb43.CO_CUR
                    //                            where tb43.CO_ANO_GRADE == anoGrade && tb43.TB44_MODULO.CO_MODU_CUR == tb01.CO_MODU_CUR
                    //                            select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct().OrderBy(c => c.NO_CUR);
                    
                    //ddlSerieCurso.DataTextField = "NO_CUR";
                    //ddlSerieCurso.DataValueField = "CO_CUR";
                    //ddlSerieCurso.DataBind();
                    //ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
                    AuxiliCarregamentos.carregaSeriesCursos(ddlSerieCurso, modalidade, LoginAuxili.CO_EMP, false);
                }
                else
                {
                    var res = (from rm in TB_RESPON_MATERIA.RetornaTodosRegistros()
                                                join c in TB01_CURSO.RetornaTodosRegistros() on rm.CO_CUR equals c.CO_CUR
                                                join tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on rm.CO_CUR equals tb43.CO_CUR
                                                where rm.CO_COL_RESP == LoginAuxili.CO_COL
                                                && rm.CO_MODU_CUR == modalidade
                                                && rm.CO_ANO_REF == ano
                                                select new
                                                {
                                                    c.NO_CUR,
                                                    rm.CO_CUR
                                                }).Distinct();

                    ddlSerieCurso.DataTextField = "NO_CUR";
                    ddlSerieCurso.DataValueField = "CO_CUR";
                    ddlSerieCurso.DataSource = res;
                    ddlSerieCurso.DataBind();

                    if (res.Count() != 1)                    
                        ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));                        
                }
            //}
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas, verifica se o usuário logado é professor.
        /// </summary>
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int ano = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;

            //if ((modalidade != 0) && (serie != 0))
            //{
                if (LoginAuxili.FLA_PROFESSOR != "S")
                {
                    //ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                    //                       where tb06.CO_MODU_CUR == modalidade && tb06.CO_CUR == serie
                    //                       select new { tb06.TB129_CADTURMAS.CO_SIGLA_TURMA, tb06.CO_TUR });
                    
                    //ddlTurma.DataTextField = "CO_SIGLA_TURMA";
                    //ddlTurma.DataValueField = "CO_TUR";
                    //ddlTurma.DataBind();
                    //ddlTurma.Items.Insert(0, new ListItem("Selecione", ""));
                    AuxiliCarregamentos.CarregaTurmas(ddlTurma, LoginAuxili.CO_EMP, modalidade, serie, false);
                }
                else
                {
                    var res = (from rm in TB_RESPON_MATERIA.RetornaTodosRegistros()
                                           join t in TB129_CADTURMAS.RetornaTodosRegistros() on rm.CO_TUR equals t.CO_TUR
                                           where rm.CO_COL_RESP == LoginAuxili.CO_COL
                                           && rm.CO_MODU_CUR == modalidade
                                           && rm.CO_CUR == serie
                                           && rm.CO_ANO_REF == ano
                                           select new
                                           {
                                               t.NO_TURMA,
                                               rm.CO_TUR,
                                               t.CO_SIGLA_TURMA
                                           }).Distinct();
                    
                ddlTurma.DataTextField = "CO_SIGLA_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataSource = res;
                ddlTurma.DataBind();

                if (res.Count() != 1)
                    ddlTurma.Items.Insert(0, new ListItem("Selecione", ""));
                 }
            //}
        }

        /// <summary>
        /// Método que carrega o dropdown de Disciplinas, verifica se o usuário logado é profesosr.
        /// </summary>
        private void CarregaMaterias()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int professor = ddlProfessor.SelectedValue != "" ? int.Parse(ddlProfessor.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int ano = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;
            string turmaUnica = "N";
            int idmatMsr = 0;
            int matMsr = 0;

            if (modalidade != 0 && serie != 0 && turma != 0)
            {
                // Recebe o conteúdo da coluna CO_FLAG_RESP_TURMA, da tabela TB06_TURMAS, que informa se a turma é única ou não
                turmaUnica = TB06_TURMAS.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie, turma).CO_FLAG_RESP_TURMA;
            }

            #region Verificação e criação da matéria de turma única
            //---------> Verifica se a turma será única ou não
            if (turmaUnica == "S")
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

                    CurrentPadraoCadastros.CurrentEntity = cm;

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

                    CurrentPadraoCadastros.CurrentEntity = m;
                }
                else
                {
                    //-----------------> Verifica se a matéria MSR está vinculada ao curso
                    int idMat = TB107_CADMATERIAS.RetornaTodosRegistros().Where(cm => cm.NO_SIGLA_MATERIA == "MSR").FirstOrDefault().ID_MATERIA;
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

                        CurrentPadraoCadastros.CurrentEntity = m;
                    }
                }
            }
            #endregion

            if (turmaUnica == "S")
            {
                if (LoginAuxili.FLA_PROFESSOR != "S")
                {
                    ddlDisciplina.DataSource = (from tb02 in TB02_MATERIA.RetornaPelaModalidadeSerie(LoginAuxili.CO_EMP, modalidade, serie)
                                                join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                                where tb107.NO_SIGLA_MATERIA == "MSR"
                                                select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy(m => m.NO_MATERIA);
                }
                else
                {
                    ddlDisciplina.DataSource = (from rm in TB_RESPON_MATERIA.RetornaTodosRegistros()
                                             join tb02 in TB02_MATERIA.RetornaTodosRegistros() on rm.CO_MAT equals tb02.CO_MAT
                                             join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                             where rm.CO_COL_RESP == LoginAuxili.CO_COL
                                             && rm.CO_MODU_CUR == modalidade
                                             && rm.CO_CUR == serie
                                             && rm.CO_ANO_REF == ano
                                             && rm.CO_TUR == turma
                                             select new
                                             {
                                                 tb107.NO_MATERIA,
                                                 rm.CO_MAT,
                                                 tb107.ID_MATERIA
                                             }).Distinct();
                }
            }
            else
            {
                if (LoginAuxili.FLA_PROFESSOR != "S")
                {
                    //ddlDisciplina.DataSource = (from tb02 in TB02_MATERIA.RetornaPelaModalidadeSerie(LoginAuxili.CO_EMP, modalidade, serie)
                    //                            join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                    //                            select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy(m => m.NO_MATERIA).ToList();
                    string anoGrade = ddlAno.SelectedValue;
                    AuxiliCarregamentos.CarregaMateriasGradeCurso(ddlDisciplina, LoginAuxili.CO_EMP, modalidade, serie, anoGrade,false);
                }
                else
                {
                    ddlDisciplina.DataSource = (from rm in TB_RESPON_MATERIA.RetornaTodosRegistros()
                                             join tb02 in TB02_MATERIA.RetornaTodosRegistros() on rm.CO_MAT equals tb02.CO_MAT
                                             join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                             where rm.CO_COL_RESP == LoginAuxili.CO_COL
                                             && rm.CO_MODU_CUR == modalidade
                                             && rm.CO_CUR == serie
                                             && rm.CO_ANO_REF == ano
                                             && rm.CO_TUR == turma
                                             select new
                                             {
                                                 tb107.NO_MATERIA,
                                                 rm.CO_MAT,
                                                 tb107.ID_MATERIA
                                             }).Distinct();
                }
            }

            //ddlDisciplina.Items.Clear();
            ddlDisciplina.DataTextField = "NO_MATERIA";
            ddlDisciplina.DataValueField = "CO_MAT";
            ddlDisciplina.DataBind();


            if (ddlDisciplina.Items.Count <= 0)             
                ddlDisciplina.Items.Insert(0, new ListItem("Selecione", ""));

            if (turmaUnica == "S")
            {
                idmatMsr = TB107_CADMATERIAS.RetornaTodosRegistros().Where(cm => cm.NO_SIGLA_MATERIA == "MSR").FirstOrDefault().ID_MATERIA;
                matMsr = TB02_MATERIA.RetornaTodosRegistros().Where(m => m.CO_MODU_CUR == modalidade && m.CO_CUR == serie && m.ID_MATERIA == idmatMsr).FirstOrDefault().CO_MAT;
                ddlDisciplina.SelectedValue = matMsr.ToString();
                ddlDisciplina.Enabled = false;
                CarregaProfessor();
            }
            else
            {
                // Esta linha foi comentada para evitar problemas na hora de recarregar a combo de matéria
                //ddlDisciplina.SelectedValue = "";
                ddlDisciplina.Enabled = true;
            }

        }

        /// <summary>
        /// Método que carrega o dropdown de Professor, verifica se o usuário logado é professor.
        /// </summary>
        private void CarregaProfessor()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int materia = ddlDisciplina.SelectedValue != "" ? int.Parse(ddlDisciplina.SelectedValue) : 0;
            int ano = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;
            string turmaUnica = "N";

            if (modalidade != 0 && serie != 0 && turma != 0)
            {
                // Recebe o conteúdo da coluna CO_FLAG_RESP_TURMA, da tabela TB06_TURMAS, que informa se a turma é única ou não
                turmaUnica = TB06_TURMAS.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie, turma).CO_FLAG_RESP_TURMA;
            }

            if (materia != 0 && turma != 0)
            {

                if (LoginAuxili.FLA_PROFESSOR != "S")
                {
                    if (turmaUnica == "S")
                    {
                        ddlProfessor.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(LoginAuxili.CO_EMP)
                                                   join tbres in TB_RESPON_MATERIA.RetornaTodosRegistros() on tb03.CO_COL equals tbres.CO_COL_RESP
                                                   join tb06 in TB06_TURMAS.RetornaTodosRegistros() on tbres.CO_TUR equals tb06.CO_TUR
                                                   where tb03.FLA_PROFESSOR == "S"
                                                   && tbres.CO_MODU_CUR == modalidade
                                                   && tbres.CO_CUR == serie
                                                   && tbres.CO_TUR == turma
                                                       //&& (tb06.CO_FLAG_RESP_TURMA != "S" ? tbres.CO_MAT == materia : 0 == 0)
                                                   && tbres.CO_ANO_REF == ano
                                                   select new { tb03.CO_COL, tb03.NO_COL }).OrderBy(c => c.NO_COL);
                    }
                    else
                    {
                        ddlProfessor.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(LoginAuxili.CO_EMP)
                                                   join tbres in TB_RESPON_MATERIA.RetornaTodosRegistros() on tb03.CO_COL equals tbres.CO_COL_RESP
                                                   join tb06 in TB06_TURMAS.RetornaTodosRegistros() on tbres.CO_TUR equals tb06.CO_TUR
                                                   where tb03.FLA_PROFESSOR == "S"
                                                   && tbres.CO_MODU_CUR == modalidade
                                                   && tbres.CO_CUR == serie
                                                   && tbres.CO_TUR == turma
                                                   && (tb06.CO_FLAG_RESP_TURMA != "S" ? tbres.CO_MAT == materia : 0 == 0)
                                                   && tbres.CO_ANO_REF == ano
                                                   select new { tb03.CO_COL, tb03.NO_COL }).OrderBy(c => c.NO_COL);
                    }
                }
                else
                {
                    ddlProfessor.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(LoginAuxili.CO_EMP)
                                               join tbres in TB_RESPON_MATERIA.RetornaTodosRegistros() on tb03.CO_COL equals tbres.CO_COL_RESP
                                               join tb06 in TB06_TURMAS.RetornaTodosRegistros() on tbres.CO_TUR equals tb06.CO_TUR
                                               where tb03.CO_COL == LoginAuxili.CO_COL
                                               select new { tb03.CO_COL, tb03.NO_COL }).OrderBy(c => c.NO_COL);
                }

                ddlProfessor.DataTextField = "NO_COL";
                ddlProfessor.DataValueField = "CO_COL";
                ddlProfessor.DataBind();
            }
            else
            {
                if (LoginAuxili.FLA_PROFESSOR != "S")
                {
                    ddlProfessor.Items.Clear();
                }
                else
                {
                    ddlProfessor.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(LoginAuxili.CO_EMP)
                                               join tbres in TB_RESPON_MATERIA.RetornaTodosRegistros() on tb03.CO_COL equals tbres.CO_COL_RESP
                                               join tb06 in TB06_TURMAS.RetornaTodosRegistros() on tbres.CO_TUR equals tb06.CO_TUR
                                               where tb03.CO_COL == LoginAuxili.CO_COL
                                               select new { tb03.CO_COL, tb03.NO_COL }).OrderBy(c => c.NO_COL);

                    ddlProfessor.DataTextField = "NO_COL";
                    ddlProfessor.DataValueField = "CO_COL";
                    ddlProfessor.DataBind();
                }
            }

            if (LoginAuxili.FLA_PROFESSOR != "S")
            {
                ddlProfessor.Items.Insert(0, new ListItem("Selecione", ""));
            }
            else
            {
                ddlProfessor.SelectedValue = LoginAuxili.CO_COL.ToString();
                ddlProfessor.Enabled = false;
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Tempo
        /// </summary>
        private void CarregaTempo()
        {
            int modalidade = this.ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = this.ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = this.ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;     

            var tb06 = (from lTb06 in TB06_TURMAS.RetornaTodosRegistros()
                            where lTb06.CO_EMP == LoginAuxili.CO_EMP
                            && (modalidade==0?0==0:lTb06.CO_MODU_CUR == modalidade)
                            && (serie==0?0==0:lTb06.CO_CUR == serie )
                            && (turma==0?0==0:lTb06.CO_TUR == turma)
                            select new { lTb06.CO_PERI_TUR }).FirstOrDefault();

            string strTurno = tb06 != null ? tb06.CO_PERI_TUR : "";
            ddlTempo.Items.Clear();
            ddlTempo.SelectedValue = null;
            ddlTempo.DataSource = (from tb131 in TB131_TEMPO_AULA.RetornaTodosRegistros()
                                   where tb131.CO_EMP == LoginAuxili.CO_EMP
                                   && (modalidade == 0 ? 0 == 0 : tb131.CO_MODU_CUR == modalidade)
                                   && (serie == 0 ? 0 == 0 : tb131.CO_CUR == serie)
                                   && (strTurno == "" ? 0 == 0 : tb131.TP_TURNO == strTurno)
                                   select new AuxiliFormatoExibicao.listaTempos
                                   {
                                       nrTempo = tb131.NR_TEMPO,
                                       turnoTempo = tb131.TP_TURNO,
                                       hrInicio = tb131.HR_INICIO,
                                       hrFim = tb131.HR_TERMI
                                   });


            ddlTempo.DataTextField = "tempoCompleto";
            ddlTempo.DataValueField = "nrTempo";
            ddlTempo.DataBind();
                ddlTempo.Items.Insert(0, new ListItem("Sem Registro de Tempo", "0"));
                ddlTempo.Items.Insert(0, new ListItem("Selecione", ""));
            ddlTempo.SelectedValue = "";
            if (ddlTempo.SelectedValue == "")
            {
                txtHoraI.Text = "00:00";
                txtHoraF.Text = "00:00";
                txtDuracao.Text = "0";
            }
        }

        /// <summary>
        /// Carrega os tipos de atividade ativos no sistema
        /// </summary>
        private void CarregaTipoAtividade()
        {
            ddlTipoAtiv.DataSource = (from tb273 in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros()
                                   where tb273.CO_SITUA_ATIV == "A"
                                   select new { tb273.ID_TIPO_ATIV, tb273.NO_TIPO_ATIV}).OrderBy(o => o.NO_TIPO_ATIV);

            ddlTipoAtiv.DataTextField = "NO_TIPO_ATIV";
            ddlTipoAtiv.DataValueField = "ID_TIPO_ATIV";
            ddlTipoAtiv.DataBind();

            ddlTipoAtiv.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion

        #region Eventos componentes

        protected void ddlTempo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Boolean resultado = ddlTempo.SelectedValue != "" && ddlTempo.SelectedValue != "0";
            txtHoraI.Enabled = !resultado;
            txtHoraF.Enabled = !resultado;
            txtDuracao.Enabled = !resultado;
            txtDuracao.ReadOnly = !resultado;
            
            if (resultado)
            {
                int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
                int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
                int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
                var turmas = (from tur in TB06_TURMAS.RetornaTodosRegistros()
                              where tur.CO_TUR == turma
                              && tur.CO_EMP == LoginAuxili.CO_EMP
                              && tur.CO_MODU_CUR == modalidade
                              && tur.CO_CUR == serie
                              select new
                              {
                                  tur.CO_PERI_TUR
                              }
                                 ).FirstOrDefault();
                if (turmas != null)
                {
                    string strTempoTurno = turmas.CO_PERI_TUR;
                    int nrTempo = int.Parse(ddlTempo.SelectedValue);

                    var tb131 = (from lTb131 in TB131_TEMPO_AULA.RetornaTodosRegistros()
                                 where lTb131.CO_EMP == LoginAuxili.CO_EMP && lTb131.CO_MODU_CUR == modalidade && lTb131.CO_CUR == serie
                                  && lTb131.NR_TEMPO == nrTempo && lTb131.TP_TURNO == strTempoTurno
                                 select new { lTb131.HR_INICIO, lTb131.HR_TERMI }).FirstOrDefault();
                    if (tb131 != null)
                    {
                        txtHoraI.Text = tb131.HR_INICIO;
                        txtHoraF.Text = tb131.HR_TERMI;

                        txtDuracao.Text = AuxiliCalculos.calculaTempo(tb131.HR_INICIO, tb131.HR_TERMI);
                    }
                    else
                        resultado = false;
                }
                else
                    resultado = false;
            }
            if (!resultado)
            {
                txtHoraI.Text = "00:00";
                txtHoraF.Text = "00:00";
                txtDuracao.Text = "0";
            }
        }

        protected void ddlAno_SelectedIndexChanged1(object sender, EventArgs e)
        {
            CarregaModalidades();
            CarregaSerieCurso();
            CarregaTurma();
            CarregaMaterias();
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
            CarregaMaterias();
            CarregaGrideConteudoBiblio();
            CarregaGrideConteudoProgra();
        }

        protected void ddlTipoAtiv_SelectedIndexChange(object sender, EventArgs e)
        {
            int tipoAtiv = ddlTipoAtiv.SelectedValue != "" ? int.Parse(ddlTipoAtiv.SelectedValue) : 0;

            if (tipoAtiv != 0)
            {
                string LancaNota = (from tb273 in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros() where tb273.CO_SITUA_ATIV == "A" && tb273.ID_TIPO_ATIV == tipoAtiv select new { tb273.FL_LANCA_NOTA_ATIV }).FirstOrDefault().FL_LANCA_NOTA_ATIV.ToString();
                ddlAvaliaAtiv.SelectedValue = LancaNota;
            }
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
            CarregaMaterias();
            CarregaGrideConteudoBiblio();
            CarregaGrideConteudoProgra();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaMaterias();
            CarregaTempo();
        }

        protected void ddlDisciplina_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGrideConteudoBiblio();
            CarregaGrideConteudoProgra();
            CarregaProfessor();
        }

        protected void grdConteuProgra_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                    {
                        HiddenField hfReferConte = (HiddenField)e.Row.FindControl("hdIdReferConte");
                        int idReferConte = Convert.ToInt32(hfReferConte.Value);
                        int coPlaAula = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);

                        var tb311 = (from lTb311 in TB311_REFER_ATIVID_AULAS.RetornaTodosRegistros()
                                     where lTb311.FL_TIPO_ACESSO == "P" && lTb311.ID_CTRL_ACESSO == coPlaAula
                                     && lTb311.ID_REFER_CONTE == idReferConte
                                     select lTb311).FirstOrDefault();

                        if (tb311 != null)
                        {
                            CheckBox chkSelect = (CheckBox)e.Row.FindControl("ckSelect");
                            chkSelect.Checked = true;
                        }
                    }
                }
                catch { }
            }
        }

        protected void grdConteuBibli_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                {
                    HiddenField hfReferConte = (HiddenField)e.Row.FindControl("hdIdReferConte");
                    int idReferConte = Convert.ToInt32(hfReferConte.Value);
                    int coPlaAula = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);

                    var tb311 = (from lTb311 in TB311_REFER_ATIVID_AULAS.RetornaTodosRegistros()
                                 where lTb311.FL_TIPO_ACESSO == "P" && lTb311.ID_CTRL_ACESSO == coPlaAula
                                 && lTb311.ID_REFER_CONTE == idReferConte
                                 select lTb311).FirstOrDefault();

                    if (tb311 != null)
                    {
                        CheckBox chkSelect = (CheckBox)e.Row.FindControl("ckSelect");
                        chkSelect.Checked = true;
                    }
                }
            }
        }
        protected void grdbncc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                {
                    HiddenField hfReferConte = (HiddenField)e.Row.FindControl("hdIdReferConte");
                    int idReferConte = Convert.ToInt32(hfReferConte.Value);
                    int coPlaAula = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);

                    var tb311 = (from lTb311 in TB311_REFER_ATIVID_AULAS.RetornaTodosRegistros()
                                 where lTb311.FL_TIPO_ACESSO == "P" && lTb311.ID_CTRL_ACESSO == coPlaAula
                                 && lTb311.ID_REFER_CONTE == idReferConte
                                 select lTb311).FirstOrDefault();

                    if (tb311 != null)
                    {
                        CheckBox chkSelect = (CheckBox)e.Row.FindControl("ckSelect");
                        chkSelect.Checked = true;
                    }
                }
            }
        }


        protected void txtHoraI_TextChanged(object sender, EventArgs e)
        {
            txtDuracao.Text = AuxiliCalculos.calculaTempo(((TextBox)sender).Text, txtHoraF.Text);
        }

        protected void txtHoraF_TextChanged(object sender, EventArgs e)
        {
            txtDuracao.Text = AuxiliCalculos.calculaTempo(txtHoraI.Text, ((TextBox)sender).Text);

        }
        #endregion
    }
}
