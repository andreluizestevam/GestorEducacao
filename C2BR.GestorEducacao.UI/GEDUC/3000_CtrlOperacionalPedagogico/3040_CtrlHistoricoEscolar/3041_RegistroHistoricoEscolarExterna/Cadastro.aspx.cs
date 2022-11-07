//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: HISTÓRICO ESCOLAR DE ALUNOS 
// OBJETIVO: REGISTRO DE HISTÓRICO ESCOLAR (INSTITUIÇÃO EXTERNA)
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 04/04/2013|   André Nobre Vinagre      | Criada a gride de outras matérias
//           |                            |
// ----------+----------------------------+-------------------------------------
// 15/04/2013|   André Nobre Vinagre      | Aumentei os campos de horas e dias letivos 
//           |                            | para 4 caracteres
//           |                            |
// ----------+----------------------------+-------------------------------------
// 09/05/2013|   André Nobre Vinagre      | Retirada obrigatoriedade do CNPJ da instituição
//           |                            |
// 26/08/2014|  Maxwell Almeida           | Alteração do campo Ano de DropDownList para TextBox, para adequação à uma regra mais acertiva

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Generic;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3040_CtrlHistoricoEscolar.F3041_RegistroHistoricoEscolarExterna
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
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao) ||
                QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoExclusao))
                AuxiliPagina.RedirecionaParaPaginaMensagem("Favor, selecione um aluno.", Request.Url.AbsolutePath.ToLower().Replace("cadastro", "busca") + "&moduloNome=" + Request.QueryString["moduloNome"], C2BR.GestorEducacao.UI.RedirecionaMensagem.TipoMessagemRedirecionamento.Error);

            if (!Page.IsPostBack)
            {
                CarregaUnidades();
                CarregaAlunos();
                CarregaModalidades();
                CarregaSeries();
                CarregaAnos();
                CarregaUfs();

                if (C2BR.GestorEducacao.UI.Library.Auxiliares.QueryStringAuxili.OperacaoCorrenteQueryString.Equals(Resources.QueryStrings.OperacaoAlteracao))
                {
                    ddlUnidade.Enabled = ddlAluno.Enabled = false;
                    liBtnAdd.Visible = true;
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
            if (Page.IsValid)
            {
                bool flagNovoRegistro = false;
                ///Informações da Instituição de Ensino
                string strNomeInstituicao = txtInstituicao.Text.Trim();
                string strCnpj = txtCNPJ.Text.Replace(".", "").Replace("-", "").Replace("/", "").Trim();
                string strNomeCidade = txtCidade.Text.Trim();
                string strUf = ddlUF.SelectedValue;

                ///Informações da Disciplina
                int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
                int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;
                int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
                int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
                //string coAnoRef = ddlAno.SelectedValue;
                string coAnoRef = txtAno.Text;
                int materia, qtdFaltaTotal, cargaHorariaTotal, diasLetivos, totalHorasFalta, cargaHorariaMateria;
                decimal mediaFinal;
                string resultadoFinal;

                var matricula = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                 where tb08.CO_EMP == coEmp
                                 && tb08.CO_ALU == coAlu
                                 && tb08.CO_CUR == serie
                                 && tb08.CO_ANO_MES_MAT == coAnoRef
                                 && tb08.CO_SIT_MAT != "C"
                                 select tb08).FirstOrDefault();

                if (matricula != null)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "O aluno está matrículado no sistema na serie/curso informado.");
                    return;
                }

                if (grdMaterias.Rows.Count == 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Faça uma pesquisa para Listar as Matérias e preencha os dados do Histórico antes de gravar.");
                    return;
                }

                int[] lstIntCoMat = new int[5];
                int indice = 0;
                foreach (GridViewRow linha in grdOutrasMaterias.Rows)
                {
                    if (((DropDownList)linha.Cells[0].FindControl("ddlMateriaOutras")).SelectedValue != "")
                    {
                        if (lstIntCoMat.Contains(int.Parse(((DropDownList)linha.Cells[0].FindControl("ddlMateriaOutras")).SelectedValue)))
                        {
                            AuxiliPagina.EnvioMensagemErro(this, "Selecionada uma mesma matéria na gride de outras matérias.");
                            return;     
                        }
                        lstIntCoMat[indice] = int.Parse(((DropDownList)linha.Cells[0].FindControl("ddlMateriaOutras")).SelectedValue);
                        indice++;
                    }
                }

//------------> Varre toda a grid de Materias
                foreach (GridViewRow linha in grdMaterias.Rows)
                {
                    if (((DropDownList)linha.Cells[7].FindControl("ddlResultadoFinal")).SelectedValue != "")
                    {
                        int.TryParse(grdMaterias.DataKeys[linha.RowIndex].Values[0].ToString(), out materia);
                        decimal.TryParse(((TextBox)linha.Cells[1].FindControl("txtMedia")).Text, out mediaFinal);
                        int.TryParse(((TextBox)linha.Cells[2].FindControl("txtCargaHoraria")).Text, out cargaHorariaMateria);
                        int.TryParse(((TextBox)linha.Cells[3].FindControl("txtTotalDiasLetivos")).Text, out diasLetivos);
                        int.TryParse(((TextBox)linha.Cells[4].FindControl("txtTotalCargaHoraria")).Text, out cargaHorariaTotal);
                        int.TryParse(((TextBox)linha.Cells[5].FindControl("txtTotalFaltas")).Text, out totalHorasFalta);
                        int.TryParse(((TextBox)linha.Cells[6].FindControl("txtFaltas")).Text, out qtdFaltaTotal);
                        resultadoFinal = ((DropDownList)linha.Cells[7].FindControl("ddlResultadoFinal")).SelectedValue;

                        TB130_HIST_EXT_ALUNO tb130 = TB130_HIST_EXT_ALUNO.RetornaPelaChavePrimaria(coEmp, coAlu, modalidade, serie, coAnoRef, materia);

                        if (tb130 == null)
                        {
                            tb130 = new TB130_HIST_EXT_ALUNO();

                            tb130.CO_EMP = coEmp;
                            tb130.CO_ALU = coAlu;
                            tb130.CO_MODU_CUR = modalidade;
                            tb130.CO_CUR = serie;
                            tb130.CO_ANO_REF = coAnoRef;
                            tb130.CO_MAT = materia;

                            flagNovoRegistro = true;
                        }

                        tb130.NO_INST = strNomeInstituicao;
                        tb130.CO_CPFCGC_INST = txtCNPJ.Text != "" ? strCnpj : null;
                        tb130.NO_CIDADE_INST = strNomeCidade;
                        tb130.CO_UF_INST = strUf;
                        tb130.QT_FALTA_FINAL = qtdFaltaTotal;
                        tb130.VL_MEDIA_FINAL = mediaFinal;
                        tb130.QT_CH_FINAL = cargaHorariaTotal;
                        tb130.CO_STA_APROV = resultadoFinal;
                        tb130.QT_TOTAL_DIAS_ANO = diasLetivos;
                        tb130.QT_TOTAL_FALTAS_HORA = totalHorasFalta;
                        tb130.QT_CH_MAT = cargaHorariaMateria;
                        tb130.FLA_OUTRA_MATER = "N";

                        TB130_HIST_EXT_ALUNO.SaveOrUpdate(tb130, true);
                    }                    
                }

                //------------> Varre toda a grid de Outras Materias
                foreach (GridViewRow linha in grdOutrasMaterias.Rows)
                {
                    if (((DropDownList)linha.Cells[7].FindControl("ddlResultadoFinal")).SelectedValue != "" && ((DropDownList)linha.Cells[0].FindControl("ddlMateriaOutras")).SelectedValue != "")
                    {
                        int.TryParse(grdMaterias.DataKeys[linha.RowIndex].Values[0].ToString(), out materia);
                        materia = int.Parse(((DropDownList)linha.Cells[0].FindControl("ddlMateriaOutras")).SelectedValue);
                        decimal.TryParse(((TextBox)linha.Cells[1].FindControl("txtMedia")).Text, out mediaFinal);
                        int.TryParse(((TextBox)linha.Cells[2].FindControl("txtCargaHoraria")).Text, out cargaHorariaMateria);
                        int.TryParse(((TextBox)linha.Cells[3].FindControl("txtTotalDiasLetivos")).Text, out diasLetivos);
                        int.TryParse(((TextBox)linha.Cells[4].FindControl("txtTotalCargaHoraria")).Text, out cargaHorariaTotal);
                        int.TryParse(((TextBox)linha.Cells[5].FindControl("txtTotalFaltas")).Text, out totalHorasFalta);
                        int.TryParse(((TextBox)linha.Cells[6].FindControl("txtFaltas")).Text, out qtdFaltaTotal);
                        resultadoFinal = ((DropDownList)linha.Cells[7].FindControl("ddlResultadoFinal")).SelectedValue;

                        TB130_HIST_EXT_ALUNO tb130 = TB130_HIST_EXT_ALUNO.RetornaPelaChavePrimaria(coEmp, coAlu, modalidade, serie, coAnoRef, materia);

                        if (tb130 == null)
                        {
                            tb130 = new TB130_HIST_EXT_ALUNO();

                            tb130.CO_EMP = coEmp;
                            tb130.CO_ALU = coAlu;
                            tb130.CO_MODU_CUR = modalidade;
                            tb130.CO_CUR = serie;
                            tb130.CO_ANO_REF = coAnoRef;
                            tb130.CO_MAT = materia;

                            flagNovoRegistro = true;
                        }

                        tb130.NO_INST = strNomeInstituicao;
                        tb130.CO_CPFCGC_INST = strCnpj != "" ? strCnpj : null;
                        tb130.NO_CIDADE_INST = strNomeCidade;
                        tb130.CO_UF_INST = strUf;
                        tb130.QT_FALTA_FINAL = qtdFaltaTotal;
                        tb130.VL_MEDIA_FINAL = mediaFinal;
                        tb130.QT_CH_FINAL = cargaHorariaTotal;
                        tb130.CO_STA_APROV = resultadoFinal;
                        tb130.QT_TOTAL_DIAS_ANO = diasLetivos;
                        tb130.QT_TOTAL_FALTAS_HORA = totalHorasFalta;
                        tb130.QT_CH_MAT = cargaHorariaMateria;
                        tb130.FLA_OUTRA_MATER = "S";

                        TB130_HIST_EXT_ALUNO.SaveOrUpdate(tb130, true);
                    }
                }

                if (flagNovoRegistro)
                    AuxiliPagina.RedirecionaParaPaginaSucesso("Item adicionado com sucesso!", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
                else
                    AuxiliPagina.RedirecionaParaPaginaSucesso("Item editado com sucesso!", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB07_ALUNO tb07 = RetornaEntidade();
            
            if (tb07 != null)
            {
                ddlUnidade.SelectedValue = tb07.CO_EMP.ToString();
                ddlAluno.SelectedValue = tb07.CO_ALU.ToString();
                atualizarDadosAluno();
            }
            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB07_ALUNO</returns>
        private TB07_ALUNO RetornaEntidade()
        {
            int coAlu = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoAlu);
            int modalidade = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoModuCur);
            int serie = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoCur);
            string ano = QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Ano);
            if (ano == null)
                CarregaAnos(ano);
            else
                CarregaAnos(DateTime.Now.Year.ToString());
            CarregaModalidades(modalidade, serie);
            CarregaSeries(serie);

            
            return TB07_ALUNO.RetornaPeloCoAlu(coAlu);
        }
        /// <summary>
        /// Atualiza os dados do aluno a ser mostrado na tela
        /// </summary>
        private void atualizarDadosAluno()
        {
            int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            if (coAlu > 0 && modalidade > 0 && serie > 0)
            {
                var matriculaAluno = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros().Include(typeof(TB44_MODULO).Name)
                                      join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb08.CO_CUR equals tb01.CO_CUR
                                      join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tb08.CO_TUR equals tb129.CO_TUR
                                      where
                                      tb08.CO_ALU == coAlu
                                      && tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                      && (modalidade == -1 ? 0 == 0 : tb08.TB44_MODULO.CO_MODU_CUR == modalidade)
                                      && (serie == -1 ? 0 == 0 : tb08.CO_CUR == serie)
                                      select new
                                      {
                                          tb01.NO_CUR,
                                          tb08.CO_ANO_MES_MAT,
                                          tb129.NO_TURMA,
                                          tb08.DT_CADASTRO,
                                          CO_TURN_MAT = (tb08.CO_TURN_MAT == "M" ? "Matutino" : (tb08.CO_TURN_MAT == "N" ? "Noturno" : "Vespertino"))
                                      }).OrderByDescending(m => m.DT_CADASTRO).FirstOrDefault();
                if (matriculaAluno != null)
                {
                    lblDadosMatricula.Text = "Modalidade: " + ddlModalidade.SelectedItem + " - Série: " + matriculaAluno.NO_CUR +
                        " - Turma: " + matriculaAluno.NO_TURMA + " - Turno: " + matriculaAluno.CO_TURN_MAT;
                }
                else
                    lblDadosMatricula.Text = "";
            }
        }
        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaUnidades()
        {
            ddlUnidade.Items.Clear();
            ddlUnidade.Items.AddRange(AuxiliBaseApoio.UnidadesDDL(LoginAuxili.IDEADMUSUARIO));
            if (ddlUnidade.Items.FindByValue(LoginAuxili.CO_EMP.ToString()) != null)
                ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        private void CarregaAlunos()
        {
            TB07_ALUNO tb07 = RetornaEntidade();
            
            ddlAluno.Items.Clear();
            ddlAluno.Items.Insert(0, new ListItem(tb07.NO_ALU, tb07.CO_ALU.ToString()));
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        /// <param name="coModuCur">Id da modalidade</param>
        private void CarregaModalidades(int coModuCur = -1, int serie = -1)
        {
            ddlModalidade.Items.Clear();
            if (serie < 0)
            {
                ddlModalidade.Items.AddRange(AuxiliBaseApoio.ModulosDDL(LoginAuxili.ORG_CODIGO_ORGAO, true));
            }
            else
            {
                var curso = TB01_CURSO.RetornaPeloCoCur(serie);
                int seqCurso = curso.SEQ_IMPRESSAO ?? 0;
                string jardim = "";
                string fundamental = "";
                if (curso.CO_NIVEL_CUR == "F" || curso.CO_NIVEL_CUR == "M")
                    jardim = "I";
                if (curso.CO_NIVEL_CUR == "M")
                    fundamental = "F";
                var modalidades = (from tb01 in TB01_CURSO.RetornaTodosRegistros()
                                   where tb01.SEQ_IMPRESSAO != null
                                   && tb01.SEQ_IMPRESSAO < seqCurso
                                   && ((tb01.CO_NIVEL_CUR == curso.CO_NIVEL_CUR) 
                                    || (jardim != "" ? tb01.CO_NIVEL_CUR == jardim : 0==1) 
                                    || (fundamental != "" ? tb01.CO_NIVEL_CUR == fundamental : 0==1))
                                   join tb44 in TB44_MODULO.RetornaTodosRegistros() on tb01.CO_MODU_CUR equals tb44.CO_MODU_CUR
                                   select new
                                   {
                                       tb44.CO_MODU_CUR,
                                       tb44.DE_MODU_CUR
                                   }).DistinctBy(d => d.CO_MODU_CUR).ToList();
                ddlModalidade.DataSource = modalidades;
                ddlModalidade.DataTextField = "DE_MODU_CUR";
                ddlModalidade.DataValueField = "CO_MODU_CUR";
                ddlModalidade.DataBind();
            }
            if (coModuCur > 0 && ddlModalidade.Items.FindByValue(coModuCur.ToString()) != null)
                ddlModalidade.SelectedValue = coModuCur.ToString();
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSeries(int coSerie = -1)
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            ddlSerieCurso.Items.Clear();
            ddlSerieCurso.Items.AddRange(AuxiliBaseApoio.SeriesDDL(LoginAuxili.CO_EMP, ddlModalidade.SelectedValue, txtAno.Text, true));
            if (coSerie > 0 && ddlSerieCurso.Items.FindByValue(coSerie.ToString()) != null)
                ddlSerieCurso.SelectedValue = coSerie.ToString();
        }

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        /// <param name="ano">Ano a ser selecionado</param>
        private void CarregaAnos(string ano = "")
        {
            //ddlAno.Items.Clear();
            //ddlAno.Items.AddRange(AuxiliBaseApoio.AnosDDL(LoginAuxili.CO_EMP, true));
            //if (ano != "" && ddlAno.Items.FindByValue(ano) != null)
            //    ddlAno.SelectedValue = ano;

            //var res = (from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
            //           where tb079.CO_EMP == LoginAuxili.CO_EMP
            //           select new { tb079.CO_ANO_REF }).OrderByDescending(w => w.CO_ANO_REF).ToList();

            //ddlAno.DataTextField = "CO_ANO_REF";
            //ddlAno.DataValueField = "CO_ANO_REF";
            //ddlAno.DataSource = res;
            //ddlAno.DataBind();

            //if (ano != "" && ddlAno.Items.FindByValue(ano) != null)
            //    ddlAno.SelectedValue = ano;

            if (ano != "")
                txtAno.Text = ano;
            else
                txtAno.Text = DateTime.Now.Year.ToString();
        }

        /// <summary>
        /// Método que carrega o dropdown de UFs
        /// </summary>
        public void CarregaUfs()
        {
            ddlUF.DataSource = TB74_UF.RetornaTodosRegistros();

            ddlUF.DataTextField = "CODUF";
            ddlUF.DataValueField = "CODUF";
            ddlUF.DataBind();

            ddlUF.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega a gride de outras matérias
        /// </summary>
        public void CarregaGrideOutrasMaterias()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;
            decimal? vlr = 0;
            int? vlr2 = 0;

            if (coEmp != 0 && coAlu != 0 && modalidade != 0 && serie != 0 && txtAno.Text != "")
            {
                var resultado = (from tb02 in TB02_MATERIA.RetornaTodosRegistros()
                                 join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                 join tb130 in TB130_HIST_EXT_ALUNO.RetornaTodosRegistros() on tb02.CO_MAT equals tb130.CO_MAT
                                 where tb02.CO_EMP == coEmp && tb02.CO_MODU_CUR == modalidade && tb02.CO_CUR == serie
                                 && tb130.CO_ANO_REF == txtAno.Text && tb130.CO_ALU == coAlu
                                 && tb130.FLA_OUTRA_MATER == "S"
                                 select new
                                 {
                                     CO_MAT = tb02.CO_MAT,
                                     NO_MATERIA = tb107.NO_MATERIA == null ? "" : tb107.NO_MATERIA,
                                     VL_MEDIA_FINAL = tb130.VL_MEDIA_FINAL == null ? 0 : tb130.VL_MEDIA_FINAL,
                                     QT_CH_MAT = tb130.QT_CH_MAT == null ? 0 : tb130.QT_CH_MAT,
                                     QT_TOTAL_DIAS_ANO = tb130.QT_TOTAL_DIAS_ANO == null ? 0 : tb130.QT_TOTAL_DIAS_ANO,
                                     QT_CH_FINAL = tb130.QT_CH_FINAL == null ? 0 : tb130.QT_CH_FINAL,
                                     QT_FALTA_FINAL = tb130.QT_FALTA_FINAL == null ? 0 : tb130.QT_FALTA_FINAL,
                                     QT_TOTAL_FALTAS_HORA = tb130.QT_TOTAL_FALTAS_HORA == null ? 0 : tb130.QT_TOTAL_FALTAS_HORA,
                                     CO_STA_APROV = tb130.CO_STA_APROV == null ? "" : tb130.CO_STA_APROV,
                                     NO_INST = tb130.NO_INST == null ? "" : tb130.NO_INST,
                                     CO_UF_INST = tb130.CO_UF_INST == null ? "" : tb130.CO_UF_INST,
                                     CO_CPFCGC_INST = tb130.CO_CPFCGC_INST == null ? "" : tb130.CO_CPFCGC_INST,
                                     NO_CIDADE_INST = tb130.NO_CIDADE_INST == null ? "" : tb130.NO_CIDADE_INST
                                 });                

                if (resultado.Count() > 0)
                {
                    grdOutrasMaterias.DataKeyNames = new string[] { "CO_MAT" };

                    grdOutrasMaterias.DataSource = resultado;
                }
                else
                {
                    var gradeSerie = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                                      select new
                                      {
                                          CO_MAT = "",
                                          NO_MATERIA = "",
                                          VL_MEDIA_FINAL = vlr,
                                          QT_CH_MAT = vlr2,
                                          QT_TOTAL_DIAS_ANO = vlr2,
                                          QT_CH_FINAL = vlr2,
                                          QT_FALTA_FINAL = vlr2,
                                          QT_TOTAL_FALTAS_HORA = vlr2,
                                          CO_STA_APROV = "",
                                          NO_INST = "",
                                          CO_CPFCGC_INST = "",
                                          NO_CIDADE_INST = "",
                                          CO_UF_INST = ""
                                      }).Take(5);

                    grdOutrasMaterias.DataSource = gradeSerie;
                }

                grdOutrasMaterias.DataBind();
            }
        }

        /// <summary>
        /// Método que carrega dropdown de outras matérias
        /// </summary>
        public void CarregaOutrasMaterias(DropDownList ddlOutraMateria)
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            var gradeSerie = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                              join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                              join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                              where tb43.CO_EMP == coEmp && tb43.TB44_MODULO.CO_MODU_CUR == modalidade
                              && tb43.CO_CUR == serie && tb43.CO_ANO_GRADE == txtAno.Text
                              select new
                              {
                                  CO_MAT = tb02.CO_MAT,
                                  NO_MATERIA = tb107.NO_MATERIA
                              });

            var resultado2 = (from tb02 in TB02_MATERIA.RetornaTodosRegistros()
                              join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                              where tb02.CO_EMP == coEmp && tb02.CO_MODU_CUR == modalidade
                              && tb02.CO_CUR == serie
                              select new
                              {
                                  CO_MAT = tb02.CO_MAT,
                                  NO_MATERIA = tb107.NO_MATERIA
                              }).Except(gradeSerie);

            ddlOutraMateria.DataSource = resultado2;

            ddlOutraMateria.DataTextField = "NO_MATERIA";
            ddlOutraMateria.DataValueField = "CO_MAT";

            ddlOutraMateria.DataBind();

            if (resultado2.Count() > 0)
            {
                ddlOutraMateria.Items.Insert(0, new ListItem("", ""));
            }
            else
            {
                ddlOutraMateria.Items.Insert(0, new ListItem("Não existem matérias adicionais", ""));
            }
        }
        #endregion                

        #region Eventos do componentes da pagina

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAlunos();
            CarregaSeries();
            CarregaAnos();
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(((DropDownList)sender).SelectedValue != "")
                CarregaSeries();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            atualizarDadosAluno();
        }

        protected void btnListarMaterias_Click(object sender, EventArgs e)
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;            
            decimal? vlr = 0;
            int? vlr2 = 0;

            if (coEmp != 0 && coAlu != 0 && modalidade != 0 && serie != 0 && txtAno.Text != "")
            {
                var resultado = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                                 join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                                 join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                 join tb130 in TB130_HIST_EXT_ALUNO.RetornaTodosRegistros() on tb02.CO_MAT equals tb130.CO_MAT into h
                                 from tb130 in h.DefaultIfEmpty()
                                 where tb43.CO_EMP == coEmp 
                                 && tb43.TB44_MODULO.CO_MODU_CUR == modalidade 
                                 && tb43.CO_CUR == serie
                                 && tb43.CO_ANO_GRADE == txtAno.Text 
                                 && tb130.CO_ALU == coAlu
                                 && tb130.FLA_OUTRA_MATER != "S"
                                 select new
                                 {
                                     CO_MAT = tb02.CO_MAT,
                                     NO_MATERIA = tb107.NO_MATERIA == null ? "" : tb107.NO_MATERIA,
                                     VL_MEDIA_FINAL = tb130.VL_MEDIA_FINAL == null ? 0 : tb130.VL_MEDIA_FINAL,
                                     QT_CH_MAT = tb130.QT_CH_MAT == null ? 0 : tb130.QT_CH_MAT,
                                     QT_TOTAL_DIAS_ANO = tb130.QT_TOTAL_DIAS_ANO == null ? 0 : tb130.QT_TOTAL_DIAS_ANO,
                                     QT_CH_FINAL = tb130.QT_CH_FINAL == null ? 0 : tb130.QT_CH_FINAL,
                                     QT_FALTA_FINAL = tb130.QT_FALTA_FINAL == null ? 0 : tb130.QT_FALTA_FINAL,
                                     QT_TOTAL_FALTAS_HORA = tb130.QT_TOTAL_FALTAS_HORA == null ? 0 : tb130.QT_TOTAL_FALTAS_HORA,
                                     CO_STA_APROV = tb130.CO_STA_APROV == null ? "" : tb130.CO_STA_APROV,
                                     NO_INST = tb130.NO_INST == null ? "" : tb130.NO_INST,
                                     CO_UF_INST = tb130.CO_UF_INST == null ? "" : tb130.CO_UF_INST,
                                     CO_CPFCGC_INST = tb130.CO_CPFCGC_INST == null ? "" : tb130.CO_CPFCGC_INST,
                                     NO_CIDADE_INST = tb130.NO_CIDADE_INST == null ? "" : tb130.NO_CIDADE_INST
                                 });

                var resultado2 = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                                  join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                                  join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                  where tb43.CO_EMP == coEmp && tb43.TB44_MODULO.CO_MODU_CUR == modalidade
                                  && tb43.CO_CUR == serie && tb43.CO_ANO_GRADE == txtAno.Text
                                  select new
                                  {
                                      CO_MAT = tb02.CO_MAT, NO_MATERIA = tb107.NO_MATERIA, VL_MEDIA_FINAL = vlr, QT_CH_MAT = vlr2,
                                      QT_TOTAL_DIAS_ANO = vlr2, QT_CH_FINAL = vlr2, QT_FALTA_FINAL = vlr2, QT_TOTAL_FALTAS_HORA = vlr2,
                                      CO_STA_APROV = "", NO_INST = "", CO_CPFCGC_INST = "", NO_CIDADE_INST = "", CO_UF_INST = ""
                                  });

                if (resultado.Count() > 0)
                {
                    txtInstituicao.Text = resultado.FirstOrDefault().NO_INST != "" ? resultado.FirstOrDefault().NO_INST : txtInstituicao.Text;
                    txtCNPJ.Text = resultado.FirstOrDefault().CO_CPFCGC_INST != "" ? resultado.FirstOrDefault().CO_CPFCGC_INST : txtCNPJ.Text;
                    txtCidade.Text = resultado.FirstOrDefault().NO_CIDADE_INST != "" ? resultado.FirstOrDefault().NO_CIDADE_INST : txtCidade.Text;
                    ddlUF.SelectedValue = resultado.FirstOrDefault().CO_UF_INST != "" ? resultado.FirstOrDefault().CO_UF_INST : ddlUF.SelectedValue;
                }

                grdMaterias.DataKeyNames = new string[] { "CO_MAT" };

                if (resultado.Count() > 0)
                    grdMaterias.DataSource = resultado;
                else
                {
                    if (resultado2.Count() > 0)
                        grdMaterias.DataSource = resultado2;
                    else
                        grdMaterias.DataSource = null;
                }
                grdMaterias.DataBind();

                CarregaGrideOutrasMaterias();
            }            
        }

        protected void grdMaterias_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hfStatus = (HiddenField)e.Row.FindControl("hdStatus");
                ((DropDownList)e.Row.FindControl("ddlResultadoFinal")).SelectedValue = hfStatus.Value;
            }
        }

        protected void grdOutrasMaterias_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField lblCoMat = (HiddenField)e.Row.FindControl("hdCoMat");
                int coMat = lblCoMat.Value != "" ? Convert.ToInt32(lblCoMat.Value) : 0;

                DropDownList ddlMateriaOutras = (DropDownList)e.Row.FindControl("ddlMateriaOutras");

                CarregaOutrasMaterias(ddlMateriaOutras);

                if (coMat != 0)
                {
                    ddlMateriaOutras.SelectedValue = coMat.ToString();
                    ddlMateriaOutras.Enabled = false;
                }
                else
                {
                    ddlMateriaOutras.Enabled = true;
                }

                HiddenField hfStatus = (HiddenField)e.Row.FindControl("hdStatus");
                ((DropDownList)e.Row.FindControl("ddlResultadoFinal")).SelectedValue = hfStatus.Value;
            }
        }

        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "")
                CarregaModalidades();
        }

        #endregion
    }
}