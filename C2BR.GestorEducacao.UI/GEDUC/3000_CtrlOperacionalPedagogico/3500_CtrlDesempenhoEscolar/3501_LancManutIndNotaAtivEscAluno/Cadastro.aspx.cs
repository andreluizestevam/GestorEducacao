//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE DE AVALIAÇÃO ESCOLAR DO ALUNO
// OBJETIVO: LANÇAMENTO E MANUTENÇÃO INDIVIDUAL DE NOTAS DE ATIVIDADES ESCOLARES DE ALUNO
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//------------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 23/04/2013| Victor Martins Machado     | A combo de tipo de atividade foi alterada
//           |                            | para pegar os valores da tabela TB273.
// ----------+----------------------------+-------------------------------------
// 25/04/2013| Victor Martins Machado     | A consulta que carrega a grid foi alterada
//           |                            | para retornar a descrição do tipo de atividade
//           |                            | da tabela TB273.
//           |                            | 
// ----------+----------------------------+-------------------------------------
// 19/06/2013| André Nobre Vinagre        | Colocado o tratamento da data de lançamento do bimestre
//           |                            | vindo da tabela de unidade
//           |                            |
// ----------+----------------------------+-------------------------------------
// 02/07/2013| André Nobre Vinagre        | Colocado o tratamento para aceitar o valor zero
//           |                            |

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3500_CtrlDesempenhoEscolar.F3501_LancManutIndNotaAtivEscAluno
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
                CarregaUnidades();
                CarregaAnos();
                CarregaModalidades();
                CarregaAluno();
                CarretaTipoAtividade();
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                    txtDataCad.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        private void CurrentCadastroMasterPage_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentCadastroMasterPage_OnAcaoBarraCadastro()
        {

            decimal dcmNota;

            if (!decimal.TryParse(txtNota.Text,out dcmNota))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Nota informada está no formato inválido.");
                return;
            }

            if (dcmNota > 100)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Nota não pode ser superior a 100.");
                return;
            }

            var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(int.Parse(ddlUnidade.SelectedValue));

            tb25.TB82_DTCT_EMPReference.Load();

            DateTime dataLacto = DateTime.Now.Date;
            if (tb25.TB82_DTCT_EMP != null)
            {
                if (ddlBimestre.SelectedValue == "B1")
                {
                    if (tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM1 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM1 != null)
                    {
                        if (!(tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM1 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM1 >= dataLacto))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Nota do 1º Bimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                            return;
                        }
                    }
                }
                else if (ddlBimestre.SelectedValue == "B2")
                {
                    if (tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM2 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM2 != null)
                    {
                        if (!(tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM2 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM2 >= dataLacto))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Nota do 2º Bimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                            return;
                        }
                    }
                }
                else if (ddlBimestre.SelectedValue == "B3")
                {
                    if (tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM3 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM3 != null)
                    {
                        if (!(tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM3 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM3 >= dataLacto))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Nota do 3º Bimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                            return;
                        }
                    }
                }
                else
                {
                    if (tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM4 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM4 != null)
                    {
                        if (!(tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM4 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM4 >= dataLacto))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Nota do 4º Bimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                            return;
                        }
                    }
                }
            }

            //Cria variáveis com os códigos usados
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int coAno = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int materia = ddlMateria.SelectedValue != "" ? int.Parse(ddlMateria.SelectedValue) : 0;
            int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;
            int tipoAtiv = ddlTipoAtiv.SelectedValue != "" ? int.Parse(ddlTipoAtiv.SelectedValue) : 0;

            int coMat = (from tb107 in TB107_CADMATERIAS.RetornaTodosRegistros()
                         join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb107.ID_MATERIA equals tb02.ID_MATERIA
                         where tb02.CO_MODU_CUR == modalidade
                         && tb02.CO_CUR == serie
                         && tb02.CO_EMP == coEmp
                         && tb107.ID_MATERIA == materia
                         select new { tb02.CO_MAT }).FirstOrDefault().CO_MAT;

            string siglaTipo = TB273_TIPO_ATIVIDADE.RetornaPelaChavePrimaria(tipoAtiv).CO_SIGLA_ATIV;
            decimal nota = decimal.Parse(txtNota.Text);
            string noMate = TB107_CADMATERIAS.RetornaPelaChavePrimaria(coEmp, materia).NO_MATERIA;
            string noAluno = TB07_ALUNO.RetornaPelaChavePrimaria(coAlu, coEmp).NO_ALU;

            //Regra criada em atendimento à necessidade do Colégio específico, limitando a nota para atividade em 2 pontos
            #region Trata a nota máxima
            switch (siglaTipo)
            {
                case "AT":
                    decimal? resat = TB43_GRD_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, coAno.ToString(), serie, coMat).VL_NOTA_MAXIM_ATIVI;
                    if (nota > (resat.HasValue ? resat.Value : 2))
                    {
                        if (resat.HasValue)
                            AuxiliPagina.EnvioMensagemErro(this.Page, "A nota informada para o aluno(a) " + noAluno + " transcede a nota máxima da Disciplina " + noMate + ", que é " + resat);
                        else
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Não há nota máxima para Atividades informada na grade Anual. A nota informada para o Aluno(a) " + noAluno + " é superior à nota limite padrão de 2,0");

                        return;
                    }
                    break;

                case "PR":
                    decimal? respr = TB43_GRD_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, coAno.ToString(), serie, coMat).VL_NOTA_MAXIM_PROVA;
                    if (respr.HasValue)
                    {
                        if (nota > respr)
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "A nota informada para o aluno(a) " + noAluno + " transcede a nota máxima da Disciplina " + noMate + ", que é " + respr);
                            return;
                        }
                    }
                    break;

                case "SI":
                    decimal? ressi = TB43_GRD_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, coAno.ToString(), serie, coMat).VL_NOTA_MAXIM_SIMUL;

                    if (ressi.HasValue)
                    {
                        if (nota > ressi)
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "A nota informada para o aluno(a) " + noAluno + " transcede a nota máxima da Disciplina " + noMate + ", que é " + ressi);
                            return;
                        }
                    }

                    break;
            }
            #endregion

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
            {
                int idNota = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);

                var ocoTb49 = (from lTb49 in TB49_NOTA_ATIV_ALUNO.RetornaTodosRegistros()
                               where lTb49.ID_NOTA_ATIV == idNota && lTb49.FL_LANCA_NOTA == "N"
                               select lTb49).FirstOrDefault();

                if (ocoTb49 != null)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Registro não pode ser alterado.");
                    return;
                }
            }

            TB49_NOTA_ATIV_ALUNO tb49 = RetornaEntidade();

            TB273_TIPO_ATIVIDADE tb273 = TB273_TIPO_ATIVIDADE.RetornaTodosRegistros().Where(r => r.ID_TIPO_ATIV == tipoAtiv).FirstOrDefault();

            tb49.CO_BIMESTRE = ddlBimestre.SelectedValue;
            tb49.CO_SEMESTRE = ddlSemestre.SelectedValue;
            tb49.CO_ANO = coAno;
            tb49.TB01_CURSO = TB01_CURSO.RetornaPelaChavePrimaria(coEmp, modalidade, serie);
            tb49.TB06_TURMAS = TB06_TURMAS.RetornaPelaChavePrimaria(coEmp, modalidade, serie, turma);
            tb49.TB07_ALUNO = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
            tb49.TB107_CADMATERIAS = TB107_CADMATERIAS.RetornaPelaChavePrimaria(coEmp, materia);
            tb49.CO_TIPO_ATIV = " ";
            tb49.TB273_TIPO_ATIVIDADE = tb273;
            tb49.DT_NOTA_ATIV = Convert.ToDateTime(txtDataAtiv.Text);
            tb49.NO_NOTA_ATIV = txtNomeAtiv.Text;
            tb49.VL_NOTA = dcmNota;
            tb49.FL_NOTA_ATIV = ddlAvaliNota.SelectedValue;
            tb49.FL_JUSTI_NOTA_ATIV = ddlJusti.SelectedValue;
            tb49.DE_JUSTI_AVALI = txtJustificativa.Text != "" ? txtJustificativa.Text : null;
            tb49.CO_STATUS = ddlStatus.SelectedValue;
            tb49.CO_REFER_NOTA = ddlClassi.SelectedValue;
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                tb49.DT_NOTA_ATIV_CAD = DateTime.Now;

                if (hdCodPlanAula.Value != "")
                {
                    int idAtivd = int.Parse(hdCodPlanAula.Value);

                    tb49.CO_ATIV_PROF_TUR = idAtivd;
                }
                else
                    tb49.CO_ATIV_PROF_TUR = null;
                tb49.FL_LANCA_NOTA = "S";
            }            

            tb49.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_UNID_FUNC, LoginAuxili.CO_COL);            

            CurrentCadastroMasterPage.CurrentEntity = tb49;

        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB49_NOTA_ATIV_ALUNO tb49 = RetornaEntidade();

            if (tb49 != null)
            {
                tb49.TB01_CURSOReference.Load();
                tb49.TB06_TURMASReference.Load();
                tb49.TB07_ALUNOReference.Load();
                tb49.TB107_CADMATERIASReference.Load();
                tb49.TB273_TIPO_ATIVIDADEReference.Load();

                txtDataAtiv.Text = tb49.DT_NOTA_ATIV.ToString("dd/MM/yyyy");
                ddlUnidade.SelectedValue = tb49.TB06_TURMAS.CO_EMP.ToString();
                ddlBimestre.SelectedValue = tb49.CO_BIMESTRE;
                ddlSemestre.SelectedValue = tb49.CO_SEMESTRE;
                CarregaAnos();
                ddlAno.SelectedValue = tb49.CO_ANO.ToString();
                CarregaModalidades();
                ddlModalidade.SelectedValue = tb49.TB06_TURMAS.CO_MODU_CUR.ToString();
                CarregaSerieCurso();
                ddlSerieCurso.SelectedValue = tb49.TB01_CURSO.CO_CUR.ToString();
                CarregaTurma();
                ddlTurma.SelectedValue = tb49.TB06_TURMAS.CO_TUR.ToString();
                CarregaAluno();
                ddlAluno.SelectedValue = tb49.TB07_ALUNO.CO_ALU.ToString();
                CarregaMaterias();
                ddlMateria.SelectedValue = tb49.TB107_CADMATERIAS.ID_MATERIA.ToString();
                ddlAvaliNota.SelectedValue = tb49.FL_NOTA_ATIV;
                ddlJusti.SelectedValue = tb49.FL_JUSTI_NOTA_ATIV;
                txtJustificativa.Text = tb49.DE_JUSTI_AVALI != null ? tb49.DE_JUSTI_AVALI : "";
                txtJustificativa.Enabled = tb49.FL_JUSTI_NOTA_ATIV == "S";
                ddlTipoAtiv.SelectedValue = tb49.TB273_TIPO_ATIVIDADE != null ? tb49.TB273_TIPO_ATIVIDADE.ID_TIPO_ATIV.ToString() : "";
                ddlStatus.SelectedValue = tb49.CO_STATUS;
                txtNomeAtiv.Text = tb49.NO_NOTA_ATIV;
                txtNota.Text = tb49.VL_NOTA.ToString();
                txtDataCad.Text = tb49.DT_NOTA_ATIV_CAD.ToString("dd/MM/yyyy");
                ddlClassi.SelectedValue = tb49.CO_REFER_NOTA;
                ddlModalidade.Enabled = ddlSerieCurso.Enabled = ddlTurma.Enabled = ddlMateria.Enabled = ddlTipoAtiv.Enabled = ddlAluno.Enabled = ddlAno.Enabled = ddlClassi.Enabled = false;

                int idAtiv = tb49.CO_ATIV_PROF_TUR != null ? (int)tb49.CO_ATIV_PROF_TUR : 0;

                hdCodPlanAula.Value = idAtiv != 0 ? idAtiv.ToString() : "";

                var resultado = (from tb119 in TB119_ATIV_PROF_TURMA.RetornaTodosRegistros()
                                 where tb119.CO_ATIV_PROF_TUR == idAtiv
                                 select new
                                 {
                                    tb119.DT_ATIV_REAL, tb119.DE_TEMA_AULA, tb119.CO_ATIV_PROF_TUR,
                                    FL_PLANEJ_ATIV = tb119.FLA_AULA_PLAN ? "Sim" : "Não",
                                    DES_TIPO_ATIV = tb119.TB273_TIPO_ATIVIDADE.DE_TIPO_ATIV
                                 });

                divGrid.Visible = true;

                grdAtividades.DataKeyNames = new string[] { "CO_ATIV_PROF_TUR" };

                grdAtividades.DataSource = resultado;
                grdAtividades.DataBind();
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB49_NOTA_ATIV_ALUNO</returns>
        private TB49_NOTA_ATIV_ALUNO RetornaEntidade()
        {
            TB49_NOTA_ATIV_ALUNO tb49 = TB49_NOTA_ATIV_ALUNO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb49 == null) ? new TB49_NOTA_ATIV_ALUNO() : tb49;
        }

        /// <summary>
        /// Método que carrega a grid de Alunos
        /// </summary>
        private void CarregaGrid()
        {
            string coAnoRefPla = ddlAno.SelectedValue != "" ? ddlAno.SelectedValue : "";
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int materia = ddlMateria.SelectedValue != "" ? int.Parse(ddlMateria.SelectedValue) : 0;

            var resultado = (from tb119 in TB119_ATIV_PROF_TURMA.RetornaTodosRegistros()
                             join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb119.CO_MAT equals tb02.CO_MAT
                             where tb119.CO_EMP == LoginAuxili.CO_EMP && tb119.CO_ANO_MES_MAT == coAnoRefPla
                             && tb02.ID_MATERIA == materia && tb119.CO_MODU_CUR == modalidade && tb119.FL_AVALIA_ATIV == "S"
                             && tb119.CO_CUR == serie && tb119.CO_TUR == turma && tb119.FL_LANCA_NOTA == "S"
                             && tb02.CO_EMP == LoginAuxili.CO_EMP && tb02.CO_CUR == tb119.CO_CUR && tb02.CO_MODU_CUR == tb119.CO_MODU_CUR
                             select new
                             {
                                 tb119.DT_ATIV_REAL, tb119.CO_PLA_AULA, tb119.DE_TEMA_AULA,  tb119.CO_ATIV_PROF_TUR,
                                 FL_PLANEJ_ATIV = tb119.FLA_AULA_PLAN ? "Sim" : "Não",
                                 DES_TIPO_ATIV = tb119.TB273_TIPO_ATIVIDADE.DE_TIPO_ATIV
                             }).OrderBy(p => p.DT_ATIV_REAL);

            divGrid.Visible = true;

            grdAtividades.DataKeyNames = new string[] { "CO_ATIV_PROF_TUR" };

            grdAtividades.DataSource = resultado;
            grdAtividades.DataBind();
        }

        /// <summary>
        /// Método que carrega as informações da Atividade de Aula
        /// </summary>
        /// <param name="idAtiv">Id da atividade</param>
        private void CarregaControlesPlano(int idAtiv)
        {
            var tb119 = TB119_ATIV_PROF_TURMA.RetornaPelaChavePrimaria(idAtiv);

            txtDataAtiv.Text = tb119.DT_ATIV_REAL.ToString("dd/MM/yyyy");
            ddlTipoAtiv.SelectedValue = tb119.CO_TIPO_ATIV;
            txtNomeAtiv.Text = tb119.DE_TEMA_AULA;
            ddlTipoAtiv.Enabled = txtNomeAtiv.Enabled = false;
        }

        /// <summary>
        /// Método que atualiza campos da Atividade
        /// </summary>
        private void AtualizaCamposAtividade()
        {
            hdCodPlanAula.Value = txtDataAtiv.Text = txtNomeAtiv.Text = "";
            ddlTipoAtiv.Enabled = txtNomeAtiv.Enabled = true;
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Matérias, verifica se o usuário logado é professor.
        /// </summary>
        private void CarregaMaterias() 
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int ano = (ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0);
            string coAnoGrade = ddlAno.SelectedValue;
            int anoInt = int.Parse(ddlAno.SelectedValue);

            string turmaUnica = "N";
            if (modalidade != 0 && serie != 0 && turma != 0)
            {
                // Recebe o conteúdo da coluna CO_FLAG_RESP_TURMA, da tabela TB06_TURMAS, que informa se a turma é única ou não
                turmaUnica = TB06_TURMAS.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie, turma).CO_FLAG_RESP_TURMA;
            }
            ddlMateria.Items.Clear();

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
                    TB107_CADMATERIAS.SaveOrUpdate(cm, true);

                    //CurrentPadraoCadastros.CurrentEntity = cm;

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
                    TB02_MATERIA.SaveOrUpdate(m, true);

                    //CurrentPadraoCadastros.CurrentEntity = m;
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
                        TB02_MATERIA.SaveOrUpdate(m, true);

                        //CurrentPadraoCadastros.CurrentEntity = m;
                    }
                }
            }
            #endregion


            // Verifica se a turma selecionada pelo usuário é turma única
            if (turmaUnica == "S")
            {
                // No caso de ser turma única o sistema deve retornar somente a matéria com sigla MSR, que é a matéria
                // padrão para turmas únicas, que não precisam de controle por matéria.
                if (LoginAuxili.FLA_PROFESSOR != "S")
                {
                    ddlMateria.DataSource = (from tb02 in TB02_MATERIA.RetornaTodosRegistros()
                                                join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                                where tb02.CO_MODU_CUR == modalidade
                                                && tb02.CO_CUR == serie
                                                && tb107.NO_SIGLA_MATERIA == "MSR"
                                                select new { tb107.ID_MATERIA, tb107.NO_MATERIA }).OrderBy(m => m.NO_MATERIA).DistinctBy(d => d.ID_MATERIA);

                    ddlMateria.DataTextField = "NO_MATERIA";
                    ddlMateria.DataValueField = "ID_MATERIA";
                    ddlMateria.DataBind();

                    ddlMateria.Items.Insert(0, new ListItem("Selecione", "0"));
                }
                else
                {
                    // No caso de ser turma única o sistema deve retornar somente a matéria com sigla MSR, que é a matéria
                    // padrão para turmas únicas, que não precisam de controle por matéria.
                    var res = (from tb02 in TB02_MATERIA.RetornaTodosRegistros()
                               join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                               where tb02.CO_MODU_CUR == modalidade
                               && tb02.CO_CUR == serie
                               && tb107.NO_SIGLA_MATERIA == "MSR"
                               select new { tb107.ID_MATERIA, tb107.NO_MATERIA }).OrderBy(m => m.NO_MATERIA).DistinctBy(d => d.ID_MATERIA);

                    if (res.Count() > 0)
                    {
                        ddlMateria.DataTextField = "NO_MATERIA";
                        ddlMateria.DataValueField = "ID_MATERIA";
                        ddlMateria.DataSource = res;
                        ddlMateria.DataBind();

                            ddlMateria.Items.Insert(0, new ListItem("Selecione", "0"));
                    }
                }

            }
            else
            {
                if (LoginAuxili.FLA_PROFESSOR != "S")
                {
                    string anog = anoInt.ToString();
                    int coem = LoginAuxili.CO_EMP;
                    var res = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                               where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.CO_CUR == serie && tb43.CO_ANO_GRADE == anog && tb43.CO_EMP == coem 
                               join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                               join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                               select new { tb107.ID_MATERIA, tb107.NO_MATERIA }).Distinct().OrderBy(m => m.NO_MATERIA);

                    if (res != null)
                    {
                        ddlMateria.DataTextField = "NO_MATERIA";
                        ddlMateria.DataValueField = "ID_MATERIA";
                        ddlMateria.DataSource = res;
                        ddlMateria.DataBind();
                    }
                    ddlMateria.Items.Insert(0, new ListItem("Selecione", "0"));
                }
                else
                {
                    var resuR = (from tbres in TB_RESPON_MATERIA.RetornaTodosRegistros()
                                 join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tbres.CO_MAT equals tb02.CO_MAT
                                 join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                 where tbres.CO_MODU_CUR == modalidade
                                 && tbres.CO_CUR == serie
                                 && tbres.CO_COL_RESP == LoginAuxili.CO_COL
                                 && tbres.CO_TUR == turma
                                 select new { tb107.ID_MATERIA, tb107.NO_MATERIA }).OrderBy(m => m.NO_MATERIA).DistinctBy(d => d.ID_MATERIA);

                    if (resuR.Count() > 0)
                    {
                        ddlMateria.DataTextField = "NO_MATERIA";
                        ddlMateria.DataValueField = "ID_MATERIA";
                        ddlMateria.DataSource = resuR;
                        ddlMateria.DataBind();
                    }

                    ddlMateria.Items.Insert(0, new ListItem("Selecione", "0"));
                }
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaUnidades() 
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP }).OrderBy( e => e.NO_FANTAS_EMP );

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            int coEmp = int.Parse(ddlUnidade.SelectedValue);

            ddlAno.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(coEmp)
                                 select new { CO_ANO_GRADE = tb43.CO_ANO_GRADE.Trim() }).Distinct().OrderByDescending( g => g.CO_ANO_GRADE );

            ddlAno.DataTextField = "CO_ANO_GRADE";
            ddlAno.DataValueField = "CO_ANO_GRADE";
            ddlAno.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades, verifica se o usuário logado é professor.
        /// </summary>
        private void CarregaModalidades() 
        {
            int ano = (ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0);
            if (LoginAuxili.FLA_PROFESSOR != "S")
                AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, false);
            else
                AuxiliCarregamentos.carregaModalidadesProfeResp(ddlModalidade, LoginAuxili.CO_COL, ano, false);

            CarregaSerieCurso();
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries, verifica se o usuário logado é professor.
        /// </summary>
        private void CarregaSerieCurso() 
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int ano = (ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0);
            string coAnoGrade = ddlAno.SelectedValue;

            if (LoginAuxili.FLA_PROFESSOR != "S")
                AuxiliCarregamentos.carregaSeriesCursos(ddlSerieCurso, modalidade, coEmp, false);
            else
                AuxiliCarregamentos.carregaSeriesCursosProfeResp(ddlSerieCurso, modalidade, LoginAuxili.CO_COL, ano, false);

            CarregaTurma();
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas, verifica se o usuário logado é professor.
        /// </summary>
        private void CarregaTurma() 
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int ano = (ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0);
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            if (LoginAuxili.FLA_PROFESSOR != "S")
                AuxiliCarregamentos.CarregaTurmasSigla(ddlTurma, coEmp, modalidade, serie, false);
            else
                AuxiliCarregamentos.CarregaTurmasSiglaProfeResp(ddlTurma, coEmp, modalidade, serie, LoginAuxili.CO_COL, ano, false);

            CarregaMaterias();
        }

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        private void CarregaAluno() 
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string ano = ddlAno.SelectedValue != "" ? ddlAno.SelectedValue : "";
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaAlunoMatriculado(ddlAluno, coEmp, modalidade, serie, turma, ano, false);
        }

        /// <summary>
        /// Carrega os tipos de atividades cadastrados verificando e criando caso necessário dinamicamente os tipos padrões :Prova, Simulado, Atividade
        /// </summary>
        private void CarretaTipoAtividade(){

            var res = (from tb273 in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros() 
                                      where tb273.FL_LANCA_NOTA_ATIV == "S"
                                      && tb273.CO_SITUA_ATIV == "A"
                                      select new { tb273.CO_SIGLA_ATIV }).ToList();

            //Verifica se existe alguma atividade do tipo prova
            bool temProva = false;
            bool temAtividade = false;
            bool temSimulado = false;
            foreach (var li in res)
            {
                switch (li.CO_SIGLA_ATIV)
                {
                    case "PR":
                        temProva = true;
                    break;

                    case "AT":
                        temAtividade = true;
                    break;

                    case "SI":
                        temSimulado = true;
                    break;
                }
            }

            //Caso não exista um tipo de atividade "Prova", criar um dinamicamente
            if (temProva == false)
            {
                TB273_TIPO_ATIVIDADE tb273np = new TB273_TIPO_ATIVIDADE();
                tb273np.NO_TIPO_ATIV = "Prova";
                tb273np.DE_TIPO_ATIV = "Prova";
                tb273np.CO_SIGLA_ATIV = "PR";
                tb273np.CO_CLASS_ATIV = "N";
                tb273np.CO_PESO_ATIV = 1;
                tb273np.FL_LANCA_NOTA_ATIV = "S";
                tb273np.CO_SITUA_ATIV = "A";
                TB273_TIPO_ATIVIDADE.SaveOrUpdate(tb273np, true);
            }

            //Caso não exista um tipo de atividade "Simulado", criar um dinamicamente
            if (temSimulado == false)
            {
                TB273_TIPO_ATIVIDADE tb273np = new TB273_TIPO_ATIVIDADE();
                tb273np.NO_TIPO_ATIV = "Simulado";
                tb273np.DE_TIPO_ATIV = "Simulado";
                tb273np.CO_SIGLA_ATIV = "SI";
                tb273np.CO_CLASS_ATIV = "N";
                tb273np.CO_PESO_ATIV = 1;
                tb273np.FL_LANCA_NOTA_ATIV = "S";
                tb273np.CO_SITUA_ATIV = "A";
                TB273_TIPO_ATIVIDADE.SaveOrUpdate(tb273np, true);
            }

            //Caso não exista um tipo de atividade "Atividade", criar um dinamicamente
            if (temAtividade == false)
            {
                TB273_TIPO_ATIVIDADE tb273np = new TB273_TIPO_ATIVIDADE();
                tb273np.NO_TIPO_ATIV = "Atividade";
                tb273np.DE_TIPO_ATIV = "Atividade";
                tb273np.CO_SIGLA_ATIV = "AT";
                tb273np.CO_CLASS_ATIV = "N";
                tb273np.CO_PESO_ATIV = 1;
                tb273np.FL_LANCA_NOTA_ATIV = "S";
                tb273np.CO_SITUA_ATIV = "A";
                TB273_TIPO_ATIVIDADE.SaveOrUpdate(tb273np, true);
            }

            ///Atribui as informações finais ao campo correspondente
            var resul = (from tb273 in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros()
                       where tb273.FL_LANCA_NOTA_ATIV == "S"
                       select new { tb273.ID_TIPO_ATIV, tb273.NO_TIPO_ATIV, }).OrderBy(o => o.NO_TIPO_ATIV);

            ddlTipoAtiv.DataTextField = "NO_TIPO_ATIV";
            ddlTipoAtiv.DataValueField = "ID_TIPO_ATIV";
            ddlTipoAtiv.DataSource = resul;
            ddlTipoAtiv.DataBind();

            ddlTipoAtiv.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion

        protected void grdAtividades_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {                
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                {
                    CheckBox cbSelecionado = (CheckBox)e.Row.FindControl("ckSelect");
                    cbSelecionado.Enabled = false;
                    cbSelecionado.Checked = true;
                }
            }
        }

        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e) 
        {
            CarregaModalidades();
            CarregaSerieCurso();
            CarregaTurma();
            CarregaMaterias();
            CarregaAluno();
            CarregaGrid();
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e) 
        {
            CarregaSerieCurso();
            CarregaTurma();
            CarregaMaterias();
            CarregaAluno();
            CarregaGrid();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e) 
        {
            CarregaTurma();
            CarregaMaterias();
            CarregaAluno();
            CarregaGrid();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e) 
        {
            CarregaMaterias();
            CarregaAluno();
            CarregaGrid();
        }

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e) 
        {            
            CarregaAnos();
            CarregaModalidades();
            CarregaSerieCurso();
            CarregaTurma();
            CarregaMaterias();
            CarregaAluno();
            CarregaGrid();
        }

        protected void ckSelect_CheckedChanged(object sender, EventArgs e)
        {
            HiddenField hfTemporario = new HiddenField();

            int qtdChecada = 0;

            foreach (GridViewRow linha in grdAtividades.Rows)
            {
                CheckBox cbSelecionado = (CheckBox)linha.Cells[0].FindControl("ckSelect");

                if ((cbSelecionado).Checked)
                {
                    qtdChecada++;
                }
            }

            foreach (GridViewRow linha in grdAtividades.Rows)
            {
                CheckBox cbSelecionado = (CheckBox)linha.Cells[0].FindControl("ckSelect");

                if ((cbSelecionado).Checked)
                {
                    if (qtdChecada == 1)
                    {
                        hdCodPlanAula.Value = ((HiddenField)linha.Cells[2].FindControl("hdPlaAula")).Value;

                        int coPlaAula = Convert.ToInt32(hdCodPlanAula.Value);

                        if (coPlaAula > 0)
                            CarregaControlesPlano(coPlaAula);
                    }
                    else
                    {
                        CheckBox checkBox = (CheckBox)linha.Cells[0].FindControl("ckSelect");
                        HiddenField hfCoPlaAula = ((HiddenField)linha.Cells[2].FindControl("hdPlaAula"));

                        if (hfCoPlaAula.Value == hdCodPlanAula.Value)
                        {
                            checkBox.Checked = false;
                        }
                        else
                        {
                            hfTemporario.Value = ((HiddenField)linha.Cells[2].FindControl("hdPlaAula")).Value;

                            int coPlaAula = Convert.ToInt32(hfCoPlaAula.Value);

                            if (coPlaAula > 0)
                                CarregaControlesPlano(coPlaAula);
                        }
                    }
                }
            }

            if (qtdChecada > 1)
            {
                hdCodPlanAula.Value = hfTemporario.Value;
            }

            if (qtdChecada == 0)
                AtualizaCamposAtividade();
        }

        protected void ddlJusti_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlJusti.SelectedValue == "S")
            {
                txtJustificativa.Enabled = true;
            }
            else
            {
                txtJustificativa.Text = "";
                txtJustificativa.Enabled = false;
            }
        }

        protected void ddlTipoAtiv_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoAtiv.SelectedValue != "")
            {
                switch (ddlTipoAtiv.SelectedItem.Text)
                {
                    case "Atividade":
                        ddlClassi.Items.Clear();
                        ddlClassi.Items.Insert(0, new ListItem("Extra", "S1"));
                        break;
                    case "Prova":
                        ddlClassi.Items.Clear();
                        ddlClassi.Items.Insert(0, new ListItem("Nota 2", "N2"));
                        ddlClassi.Items.Insert(0, new ListItem("Nota 1", "N1"));
                        break;
                    case "Simulado":
                        ddlClassi.Items.Clear();
                        ddlClassi.Items.Insert(0, new ListItem("Simulado", "N3"));
                        break;
                    case "Recuperação":
                        ddlClassi.Items.Clear();
                        ddlClassi.Items.Insert(0, new ListItem("Nota 2", "N2"));
                        ddlClassi.Items.Insert(0, new ListItem("Nota 1", "N1"));
                        break;
                    default:
                        ddlClassi.Items.Clear();
                        ddlClassi.Items.Insert(0, new ListItem("Extra", "S1"));
                        break;
                }
            }

        }


        /// <summary>
        /// Trata para mostrar os tipos pertinentes à disciplina selecionada
        /// </summary>
        /// <param name="agrupadora"></param>
        /// <param name="DiscFilhas"></param>
        private void RecarregaTipos(bool agrupadora, bool DiscFilhas, bool Padrao)
        {
            if (agrupadora)
            {
                ddlTipoAtiv.Items.Clear();
                var resul = (from tb273 in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros()
                             where tb273.FL_LANCA_NOTA_ATIV == "S"
                             && tb273.CO_SIGLA_ATIV != "PR"
                             && tb273.CO_SIGLA_ATIV != "AT"
                             select new { tb273.ID_TIPO_ATIV, tb273.NO_TIPO_ATIV, }).OrderBy(o => o.NO_TIPO_ATIV);

                ddlTipoAtiv.DataTextField = "NO_TIPO_ATIV";
                ddlTipoAtiv.DataValueField = "ID_TIPO_ATIV";
                ddlTipoAtiv.DataSource = resul;
                ddlTipoAtiv.DataBind();

                ddlTipoAtiv.Items.Insert(0, new ListItem("Selecione", ""));
            }
            else if (DiscFilhas)
            {
                ddlTipoAtiv.Items.Clear();
                var resul = (from tb273 in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros()
                             where tb273.FL_LANCA_NOTA_ATIV == "S"
                             && tb273.CO_SIGLA_ATIV != "SI"
                             select new { tb273.ID_TIPO_ATIV, tb273.NO_TIPO_ATIV, }).OrderBy(o => o.NO_TIPO_ATIV);

                ddlTipoAtiv.DataTextField = "NO_TIPO_ATIV";
                ddlTipoAtiv.DataValueField = "ID_TIPO_ATIV";
                ddlTipoAtiv.DataSource = resul;
                ddlTipoAtiv.DataBind();

                ddlTipoAtiv.Items.Insert(0, new ListItem("Selecione", ""));
            }
            else if (Padrao)
            {
                ///Atribui as informações finais ao campo correspondente
                var resul = (from tb273 in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros()
                             where tb273.FL_LANCA_NOTA_ATIV == "S"
                             select new { tb273.ID_TIPO_ATIV, tb273.NO_TIPO_ATIV, }).OrderBy(o => o.NO_TIPO_ATIV);

                ddlTipoAtiv.DataTextField = "NO_TIPO_ATIV";
                ddlTipoAtiv.DataValueField = "ID_TIPO_ATIV";
                ddlTipoAtiv.DataSource = resul;
                ddlTipoAtiv.DataBind();

                ddlTipoAtiv.Items.Insert(0, new ListItem("Selecione", ""));
            }
        }

        protected void ddlMateria_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAluno();
            CarregaGrid();

            string ano = ddlAno.SelectedValue;
            int curso = (ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0);
            int modali = (ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0);
            int comat = (ddlMateria.SelectedValue != "" ? int.Parse(ddlMateria.SelectedValue) : 0);
            var res = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                       join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                       join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                       where tb43.TB44_MODULO.CO_MODU_CUR == modali
                       && tb43.CO_CUR == curso
                       && tb43.CO_MAT == tb02.CO_MAT
                       && tb107.ID_MATERIA == comat
                       && tb43.CO_ANO_GRADE == ano
                       select new { tb43.ID_MATER_AGRUP, tb43.FL_DISCI_AGRUPA }).FirstOrDefault();

            //Tratamentos especiais para disciplinas agrupadores e agrupadas
            if (res.FL_DISCI_AGRUPA == "S")
                RecarregaTipos(true, false, false);
            else if (res.ID_MATER_AGRUP != (int?)null)
                RecarregaTipos(false, true, false);
            else
                RecarregaTipos(false, false, true);

        }
      
  }
}
