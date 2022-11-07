//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE DE AVALIAÇÃO ESCOLAR DO ALUNO
// OBJETIVO: ******
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 03/05/2013| André Nobre Vinagre        | Criada a funcionalidade
//           |                            |
// ----------+----------------------------+-------------------------------------
// 07/05/2013| André Nobre Vinagre        | Implementada lógica de um botão para busca da 
//           |                            | gride de notas do aluno
//           |                            |
// ----------+----------------------------+-------------------------------------
// 17/05/2013| André Nobre Vinagre        | Alterado o nome N3 para SM e Simulado para Extra
//           |                            |
// ----------+----------------------------+-------------------------------------
// 23/05/2013| André Nobre Vinagre        | Alterada a lógica que não estava pegando a nota corretamente
//           |                            | quando alterava a matéria
//           |                            |
// ----------+----------------------------+-------------------------------------
// 04/06/2013| Victor Martins Machado     | Foi incluído o código da matéria no WHERE da consulta
//           |                            | utilizada na inclusão dos dados no banco, na consulta
//           |                            | que valida se já existem dados com aqueles filtros.
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
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3500_CtrlDesempenhoEscolar.F3506_LancNotaAtivPorMateria
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            CarregaAnos();
            CarregaModalidades();
            CarretaTipoAtividade();
            divGrid.Visible = false;
        }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool flgOcoNota = false;            

            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int coMat = ddlDisciplina.SelectedValue != "" ? int.Parse(ddlDisciplina.SelectedValue) : 0;
            int coTipoAtiv = ddlTipoAtiv.SelectedValue != "" ? int.Parse(ddlTipoAtiv.SelectedValue) : 0;
            string anoRef = ddlAno.SelectedValue;
            DateTime dataAtiv = DateTime.Parse(txtDataAtiv.Text);

            if (modalidade == 0 || serie == 0 || turma == 0 || coMat == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Modalidade, Série, Turma e Disciplina devem ser informados.");
                return;
            }

            int materia = (from tb02 in TB02_MATERIA.RetornaTodosRegistros()
                           where tb02.CO_MAT == coMat
                           select new { tb02.ID_MATERIA }).First().ID_MATERIA;

            string siglaTipo = TB273_TIPO_ATIVIDADE.RetornaPelaChavePrimaria(coTipoAtiv).CO_SIGLA_ATIV;

            foreach (GridViewRow linha in grdBusca.Rows)
            {
                /*========================================================================
                 * Este IF foi comentado por que o cliente precisar ter a possibilidade de
                 * incluir uma nota vazia, no caso de uma nota errada ser lançada.
                 * =======================================================================
                 * Victor Martins Machado - 05/08/2013 15:38
                 *=======================================================================*/
                //Verifica se existiu ocorrência de nota
                //if (((TextBox)linha.Cells[2].FindControl("txtNota")).Text != "")
                //{
                    flgOcoNota = true;
                    //----------------> Média deve estar entre 0 e 100
                    decimal dcmM = 0;
                    if (decimal.TryParse(((TextBox)linha.Cells[2].FindControl("txtNota")).Text, out dcmM))
                    {
                        if (Decimal.Parse(((TextBox)linha.Cells[2].FindControl("txtNota")).Text) > 100)
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Nota deve estar entre 0 e 100");
                            return;
                        }
                        decimal nota = (Decimal.Parse(((TextBox)linha.Cells[2].FindControl("txtNota")).Text));
                        string noMate = TB107_CADMATERIAS.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, materia).NO_MATERIA;
                        int codalu = Convert.ToInt32(grdBusca.DataKeys[linha.RowIndex].Values[0]);
                        string noAluno = TB07_ALUNO.RetornaPelaChavePrimaria(codalu, LoginAuxili.CO_EMP).NO_ALU;

                        //Regra criada em atendimento à necessidade do Colégio específico, limitando a nota para atividade em 2 pontos
                        #region Trata a nota máxima
                        switch (siglaTipo)
                        {
                            case "AT":
                                decimal? resat = TB43_GRD_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, anoRef, serie, coMat).VL_NOTA_MAXIM_ATIVI;
                                if( nota > (resat.HasValue ? resat.Value : 2))
                                {
                                    if(resat.HasValue)
                                        AuxiliPagina.EnvioMensagemErro(this.Page, "A nota informada para o aluno(a) " + noAluno + " transcede a nota máxima da Disciplina " + noMate + ", que é " + resat);
                                    else
                                        AuxiliPagina.EnvioMensagemErro(this.Page, "Não há nota máxima para Atividades informada na grade Anual. A nota informada para o Aluno(a) " + noAluno + " é superior à nota limite padrão de 2,0");

                                    return;
                                }
                                break;

                            case "PR":
                                decimal? respr = TB43_GRD_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, anoRef, serie, coMat).VL_NOTA_MAXIM_PROVA;
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
                                decimal? ressi = TB43_GRD_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, anoRef, serie, coMat).VL_NOTA_MAXIM_SIMUL;

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
                    }
                //}
            }

            /*========================================================================
             * Este IF foi comentado por que o cliente precisar ter a possibilidade de
             * incluir uma nota vazia, no caso de uma nota errada ser lançada.
             * =======================================================================
             * Victor Martins Machado - 05/08/2013 15:38
             *=======================================================================*/
            //if (!flgOcoNota)
            //{
            //    AuxiliPagina.EnvioMensagemErro(this.Page, "Nenhuma nota foi informada");
            //    return;
            //}

            var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

            tb25.TB82_DTCT_EMPReference.Load();

            DateTime dataLacto = DateTime.Now.Date;
            if (tb25.TB82_DTCT_EMP != null)
            {
                var tipo = "";
                tb25.TB83_PARAMETROReference.Load();
                if (tb25.TB83_PARAMETRO != null)
                    tipo = tb25.TB83_PARAMETRO.TP_PERIOD_AVAL;

                switch (tipo)
                {
                    case "B":
                        if (ddlReferencia.SelectedValue == "B1")
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
                        else if (ddlReferencia.SelectedValue == "B2")
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
                        else if (ddlReferencia.SelectedValue == "B3")
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
                        break;
                    case "T":
                        if (ddlReferencia.SelectedValue == "T1")
                        {
                            if (tb25.TB82_DTCT_EMP.DT_LACTO_INICI_TRI1 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI1 != null)
                            {
                                if (!(tb25.TB82_DTCT_EMP.DT_LACTO_INICI_TRI1 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI1 >= dataLacto))
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Nota do 1º Trimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                                    return;
                                }
                            }
                        }
                        else if (ddlReferencia.SelectedValue == "T2")
                        {
                            if (tb25.TB82_DTCT_EMP.DT_LACTO_INICI_TRI2 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI2 != null)
                            {
                                if (!(tb25.TB82_DTCT_EMP.DT_LACTO_INICI_TRI2 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI2 >= dataLacto))
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Nota do 2º Trimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                                    return;
                                }
                            }
                        }
                        else
                        {
                            if (tb25.TB82_DTCT_EMP.DT_LACTO_INICI_TRI3 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI3 != null)
                            {
                                if (!(tb25.TB82_DTCT_EMP.DT_LACTO_INICI_TRI3 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI3 >= dataLacto))
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Nota do 3º Trimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                                    return;
                                }
                            }
                        }
                        break;
                }
            }
            decimal dcmMedia;
            
//--------> Varre toda a grid de Busca
            foreach (GridViewRow linha in grdBusca.Rows)
            {
                /*===============================================================
                 * Este IF valida se o campo de nota da grid possui valor, se for
                 * o caso, o sistema irá criar um registro na tabela TB49, caso
                 * contrário, o sistema irá deletar o registro referente, caso
                 * exista.
                 *===============================================================
                 * Victor Martins Machado
                 *==============================================================*/
                if (((TextBox)linha.Cells[2].FindControl("txtNota")).Text != "")
                {
                    if (decimal.TryParse(((TextBox)linha.Cells[2].FindControl("txtNota")).Text, out dcmMedia))
                    {
                        //------------> Recebe o código do aluno
                        int coAlu = Convert.ToInt32(grdBusca.DataKeys[linha.RowIndex].Values[0]);

                        int ID_MATERIA = (from tb02 in TB02_MATERIA.RetornaTodosRegistros() where tb02.CO_MAT == coMat && tb02.CO_EMP == LoginAuxili.CO_EMP && tb02.CO_MODU_CUR == modalidade && tb02.CO_CUR == serie select tb02).FirstOrDefault().ID_MATERIA;

                        //------------> Recebe o dado da tabela de histórico de acordo com as chaves passadas
                        TB49_NOTA_ATIV_ALUNO tb49 = (from iTb49 in TB49_NOTA_ATIV_ALUNO.RetornaTodosRegistros()
                                                     where iTb49.TB07_ALUNO.CO_ALU == coAlu && iTb49.DT_NOTA_ATIV.Year == dataAtiv.Year
                                                     && iTb49.DT_NOTA_ATIV.Month == dataAtiv.Month && iTb49.DT_NOTA_ATIV.Day == dataAtiv.Day
                                                     && iTb49.TB273_TIPO_ATIVIDADE.ID_TIPO_ATIV == coTipoAtiv
                                                         //Na tabela TB49_NOTA_ATIV_ALUNO não existe a coluna CO_TRIMESTRE 
                                                     && iTb49.CO_BIMESTRE == ddlReferencia.SelectedValue
                                                     && iTb49.CO_REFER_NOTA == ddlClassi.SelectedValue
                                                     && iTb49.TB107_CADMATERIAS.ID_MATERIA == ID_MATERIA
                                                     select iTb49).FirstOrDefault();

                        if (tb49 == null)
                        {
                            tb49 = new TB49_NOTA_ATIV_ALUNO();
                            tb49.DT_NOTA_ATIV_CAD = DateTime.Now;
                        }

                        tb49.CO_BIMESTRE = ddlReferencia.SelectedValue;
                        tb49.CO_SEMESTRE = ddlReferencia.SelectedValue == "B1" || ddlReferencia.SelectedValue == "B2" ? "1" : "2";
                        tb49.CO_ANO = int.Parse(anoRef);
                        tb49.TB01_CURSO = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie);
                        tb49.TB06_TURMAS = TB06_TURMAS.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie, turma);
                        tb49.TB07_ALUNO = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                        tb49.TB107_CADMATERIAS = TB107_CADMATERIAS.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, materia);
                        tb49.CO_TIPO_ATIV = " ";
                        tb49.CO_REFER_NOTA = ddlClassi.SelectedValue;
                        tb49.TB273_TIPO_ATIVIDADE = TB273_TIPO_ATIVIDADE.RetornaPelaChavePrimaria(coTipoAtiv);
                        tb49.DT_NOTA_ATIV = Convert.ToDateTime(txtDataAtiv.Text);
                        tb49.NO_NOTA_ATIV = ddlTipoAtiv.SelectedItem.ToString();
                        tb49.VL_NOTA = decimal.Parse(((TextBox)linha.Cells[2].FindControl("txtNota")).Text);
                        tb49.FL_NOTA_ATIV = "S";
                        tb49.FL_JUSTI_NOTA_ATIV = "N";
                        tb49.FL_LANCA_NOTA = "S";
                        tb49.CO_STATUS = "A";

                        tb49.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_UNID_FUNC, LoginAuxili.CO_COL);

                        if (tb49.EntityState != System.Data.EntityState.Unchanged && GestorEntities.SaveOrUpdate(tb49) < 1)
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao salvar os itens");
                            return;
                        }
                    }
                }
                else
                {
                    //------------> Recebe o código do aluno
                    int coAlu = Convert.ToInt32(grdBusca.DataKeys[linha.RowIndex].Values[0]);

                    int ID_MATERIA = (from tb02 in TB02_MATERIA.RetornaTodosRegistros() where tb02.CO_MAT == coMat && tb02.CO_EMP == LoginAuxili.CO_EMP && tb02.CO_MODU_CUR == modalidade && tb02.CO_CUR == serie select tb02).FirstOrDefault().ID_MATERIA;

                    //------------> Recebe o dado da tabela de histórico de acordo com as chaves passadas
                    TB49_NOTA_ATIV_ALUNO tb49 = (from iTb49 in TB49_NOTA_ATIV_ALUNO.RetornaTodosRegistros()
                                                 where iTb49.TB07_ALUNO.CO_ALU == coAlu && iTb49.DT_NOTA_ATIV.Year == dataAtiv.Year
                                                 && iTb49.DT_NOTA_ATIV.Month == dataAtiv.Month && iTb49.DT_NOTA_ATIV.Day == dataAtiv.Day
                                                 && iTb49.TB273_TIPO_ATIVIDADE.ID_TIPO_ATIV == coTipoAtiv
                                                     //Na tabela TB49_NOTA_ATIV_ALUNO não existe a coluna CO_TRIMESTRE 
                                                 && iTb49.CO_BIMESTRE == ddlReferencia.SelectedValue
                                                 && iTb49.CO_REFER_NOTA == ddlClassi.SelectedValue
                                                 && iTb49.TB107_CADMATERIAS.ID_MATERIA == ID_MATERIA
                                                 select iTb49).FirstOrDefault();
                    if (tb49 != null)
                    {
                        TB49_NOTA_ATIV_ALUNO.Delete(tb49, true);
                    }
                }
            }

            AuxiliPagina.RedirecionaParaPaginaSucesso("Registros Salvos com sucesso.", Request.Url.AbsoluteUri);
        }

        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega a grid
        /// </summary>
        private void CarregaGrid()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int materia = ddlDisciplina.SelectedValue != "" ? int.Parse(ddlDisciplina.SelectedValue) : 0;
            int coTipoAtiv = ddlTipoAtiv.SelectedValue != "" ? int.Parse(ddlTipoAtiv.SelectedValue) : 0;
            string anoMesMat = ddlAno.SelectedValue;
            DateTime dataAtiv = DateTime.Parse(txtDataAtiv.Text);

            int idMateria = (from tb02 in TB02_MATERIA.RetornaTodosRegistros()
                            where tb02.CO_MAT == materia
                            select new { tb02.ID_MATERIA }).First().ID_MATERIA;

            divGrid.Visible = true;            

            var resultado = (from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                             join tb08 in TB08_MATRCUR.RetornaTodosRegistros() on tb079.CO_ALU equals tb08.CO_ALU
                              where tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP && tb08.CO_ANO_MES_MAT == anoMesMat
                              && tb08.CO_CUR == serie && tb08.CO_TUR == turma && tb079.CO_MAT == materia 
                              && tb08.CO_SIT_MAT == "A"
                              select new NotasAluno
                              {
                                  CO_ALU = tb079.CO_ALU, NO_ALU = tb08.TB07_ALUNO.NO_ALU.ToUpper(), 
                                  NU_NIRE = tb08.TB07_ALUNO.NU_NIRE
                              }).ToList();

            foreach (var res in resultado)
            {
                TB49_NOTA_ATIV_ALUNO tb49 = (from iTb49 in TB49_NOTA_ATIV_ALUNO.RetornaTodosRegistros()
                                             where iTb49.TB07_ALUNO.CO_ALU == res.CO_ALU && iTb49.DT_NOTA_ATIV.Year == dataAtiv.Year
                                             && iTb49.DT_NOTA_ATIV.Month == dataAtiv.Month && iTb49.DT_NOTA_ATIV.Day == dataAtiv.Day
                                             && iTb49.TB273_TIPO_ATIVIDADE.ID_TIPO_ATIV == coTipoAtiv
                                                 //Na tabela TB49_NOTA_ATIV_ALUNO não existe a coluna CO_TRIMESTRE 
                                             && iTb49.CO_BIMESTRE == ddlReferencia.SelectedValue
                                             && iTb49.CO_REFER_NOTA == ddlClassi.SelectedValue && iTb49.TB107_CADMATERIAS.ID_MATERIA == idMateria
                                             select iTb49).FirstOrDefault();

                if (tb49 != null)
                {
                    res.NOTA = tb49.VL_NOTA;
                }
            }

            grdBusca.DataKeyNames = new string[] { "CO_ALU" };

            grdBusca.DataSource = resultado.Count() > 0 ? resultado.OrderBy(p => p.NO_ALU) : null;
            grdBusca.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            ddlAno.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                 select new { tb43.CO_ANO_GRADE }).Distinct().OrderByDescending( g => g.CO_ANO_GRADE );

            ddlAno.DataTextField = "CO_ANO_GRADE";
            ddlAno.DataValueField = "CO_ANO_GRADE";
            ddlAno.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades, verifica se o usuário é professor.
        /// </summary>
        private void CarregaModalidades()
        {
            int ano = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;
            if (LoginAuxili.FLA_PROFESSOR != "S")
                AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, false);
            else
                AuxiliCarregamentos.carregaModalidadesProfeResp(ddlModalidade, LoginAuxili.CO_COL, ano, false);

            CarregaSerieCurso();
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries, verifica se o usuário é professor.
        /// </summary>
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            string anoGrade = ddlAno.SelectedValue;
            int ano = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;

            if (LoginAuxili.FLA_PROFESSOR != "S")
                AuxiliCarregamentos.carregaSeriesCursos(ddlSerieCurso, modalidade, LoginAuxili.CO_EMP, false);
            else
                AuxiliCarregamentos.carregaSeriesCursosProfeResp(ddlSerieCurso, modalidade, LoginAuxili.CO_COL, ano, false);

            CarregaTurma();
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas, verifica se o usuáiro é professor.
        /// </summary>
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int ano = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;

            if (LoginAuxili.FLA_PROFESSOR != "S")
                AuxiliCarregamentos.CarregaTurmasSigla(ddlTurma, LoginAuxili.CO_EMP, modalidade, serie, false);
            else
                AuxiliCarregamentos.CarregaTurmasSiglaProfeResp(ddlTurma, LoginAuxili.CO_EMP, modalidade, serie, LoginAuxili.CO_COL, ano, false);

            CarregaDisciplina();
        }

        /// <summary>
        /// Método que carrega o dropdown de Disciplinas, verifica se o usuário é professor.
        /// </summary>
        private void CarregaDisciplina()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string ano = ddlAno.SelectedValue;
            int anoInt = int.Parse(ddlAno.SelectedValue);

            string turmaUnica = "N";
            if (modalidade != 0 && serie != 0 && turma != 0)
            {
                // Recebe o conteúdo da coluna CO_FLAG_RESP_TURMA, da tabela TB06_TURMAS, que informa se a turma é única ou não
                turmaUnica = TB06_TURMAS.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie, turma).CO_FLAG_RESP_TURMA;
            }
            ddlDisciplina.Items.Clear();

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


            // Verifica se a turma selecionada pelo usuário é turma única
            if (turmaUnica == "S")
            {
                // No caso de ser turma única o sistema deve retornar somente a matéria com sigla MSR, que é a matéria
                // padrão para turmas únicas, que não precisam de controle por matéria.
                if (LoginAuxili.FLA_PROFESSOR != "S")
                {
                    var res = (from tb02 in TB02_MATERIA.RetornaTodosRegistros()
                                                join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                                where tb02.CO_MODU_CUR == modalidade
                                                && tb02.CO_CUR == serie
                                                && tb107.NO_SIGLA_MATERIA == "MSR"
                                                select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy(m => m.NO_MATERIA).DistinctBy(d => d.CO_MAT);

                    ddlDisciplina.DataTextField = "NO_MATERIA";
                    ddlDisciplina.DataValueField = "CO_MAT";
                    ddlDisciplina.DataSource = res;
                    ddlDisciplina.DataBind();

                    if (res.Count() > 1)
                        ddlDisciplina.Items.Insert(0, new ListItem("Selecione", "0"));
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
                               select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy(m => m.NO_MATERIA).DistinctBy(d => d.CO_MAT);

                    if (res.Count() > 0)
                    {
                        ddlDisciplina.DataTextField = "NO_MATERIA";
                        ddlDisciplina.DataValueField = "CO_MAT";
                        ddlDisciplina.DataSource = res;
                        ddlDisciplina.DataBind();

                        if (res.Count() > 1)
                            ddlDisciplina.Items.Insert(0, new ListItem("Selecione", "0"));
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
                               select new { tb02.CO_MAT, tb107.NO_MATERIA }).Distinct().OrderBy(m => m.NO_MATERIA);

                    if (res != null)
                    {
                        ddlDisciplina.DataTextField = "NO_MATERIA";
                        ddlDisciplina.DataValueField = "CO_MAT";
                        ddlDisciplina.DataSource = res;
                        ddlDisciplina.DataBind();
                    }
                    if (res.Count() > 1)
                        ddlDisciplina.Items.Insert(0, new ListItem("Selecione", "0"));
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
                                 select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy(m => m.NO_MATERIA).DistinctBy(d => d.CO_MAT);

                    if (resuR.Count() > 0)
                    {
                        ddlDisciplina.DataTextField = "NO_MATERIA";
                        ddlDisciplina.DataValueField = "CO_MAT";
                        ddlDisciplina.DataSource = resuR;
                        ddlDisciplina.DataBind();
                    }

                    ddlDisciplina.Items.Insert(0, new ListItem("Selecione", "0"));
                }
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Tipo de Atividades
        /// </summary>
        private void CarretaTipoAtividade()
        {
            var res = (from tb273 in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros()
                       where tb273.FL_LANCA_NOTA_ATIV == "S"
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

        #endregion      

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
            CarregaDisciplina();
            grdBusca.DataSource = null;
            grdBusca.DataBind();
            divGrid.Visible = false;
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
            CarregaDisciplina();
            grdBusca.DataSource = null;
            grdBusca.DataBind();
            divGrid.Visible = false;
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaDisciplina();
            grdBusca.DataSource = null;
            grdBusca.DataBind();
            divGrid.Visible = false;
        }

        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
            CarregaModalidades();
            CarregaDisciplina();
            grdBusca.DataSource = null;
            grdBusca.DataBind();
            divGrid.Visible = false;
        }

        protected void grdBusca_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            }
        }

        protected void ddlDisciplina_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string ano = ddlAno.SelectedValue;
            int curso = (ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0);
            int modali = (ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0);
            int comat = (ddlDisciplina.SelectedValue != "" ? int.Parse(ddlDisciplina.SelectedValue) : 0);
            var res = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                       where tb43.TB44_MODULO.CO_MODU_CUR == modali
                       && tb43.CO_CUR == curso
                       && tb43.CO_MAT == comat
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

        protected void btnPesqGride_Click(object sender, EventArgs e)
        {
            if (ddlModalidade.SelectedValue == "" || ddlSerieCurso.SelectedValue == "" || ddlTurma.SelectedValue == "" || ddlDisciplina.SelectedValue == "0" ||  ddlDisciplina.SelectedValue == ""
                || ddlAno.SelectedValue == "" )
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Ano, Modalidade, Série, Turma e Disciplina devem ser informados.");
                return;
            }

            if (ddlTipoAtiv.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Tipo de Atividade deve ser informado.");
                return;
            }

            if (txtDataAtiv.Text == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Atividade deve ser informado.");
                return;
            }

            CarregaGrid();
        }

        #region Lista de Notas dos Alunos
        public class NotasAluno
        {
            public string NO_ALU { get; set; }
            public decimal? NOTA { get; set; }
            public int NU_NIRE { get; set; }
            public int CO_ALU { get; set; }

            public string DescNU_NIRE { get { return this.NU_NIRE.ToString().PadLeft(9, '0'); } }
        }
        #endregion
    }
}