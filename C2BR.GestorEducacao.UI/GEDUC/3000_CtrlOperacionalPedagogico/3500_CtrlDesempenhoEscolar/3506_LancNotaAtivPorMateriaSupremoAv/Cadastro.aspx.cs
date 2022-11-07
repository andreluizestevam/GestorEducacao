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

namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3500_CtrlDesempenhoEscolar._3506_LancNotaAtivPorMateriaSupremoAv
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
            CarregaMedidas();
            divGrid.Visible = false;



        }

        //====> Carrega o tipo de medida da Referência (Mensal/Bimestre/Trimestre/Semestre/Anual)
        private void CarregaMedidas()
        {
            var tipo = "B";

            var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

            tb25.TB83_PARAMETROReference.Load();
            if (tb25.TB83_PARAMETRO != null)
                tipo = tb25.TB83_PARAMETRO.TP_PERIOD_AVAL;

            AuxiliCarregamentos.CarregaMedidasTemporais(ddlReferencia, false, tipo, false, true);
        }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool flgOcoNota = false;

            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int coMat = ddlDisciplina.SelectedValue != "" ? int.Parse(ddlDisciplina.SelectedValue) : 0;
            //int coTipoAtiv = ddlTipoAtiv.SelectedValue != "" ? int.Parse(ddlTipoAtiv.SelectedValue) : 0;
            string anoRef = ddlAno.SelectedValue;
            //DateTime dataAtiv = DateTime.Parse(txtDataAtiv.Text);

            if (modalidade == 0 || serie == 0 || turma == 0 || ddlDisciplina.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Modalidade, Série, Turma e Disciplina devem ser informados.");
                return;
            }

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
                if (decimal.TryParse(((TextBox)linha.Cells[1].FindControl("txtNotaAv1")).Text, out dcmM))
                {
                    if (Decimal.Parse(((TextBox)linha.Cells[1].FindControl("txtNotaAv1")).Text) > 100)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "A nota da avalição 1  deve estar entre 0 e 100");
                        return;
                    }

                    decimal nota = (Decimal.Parse(((TextBox)linha.Cells[1].FindControl("txtNotaAv1")).Text));

                }
                if (decimal.TryParse(((TextBox)linha.Cells[2].FindControl("txtNotaAv2")).Text, out dcmM))
                {
                    if (Decimal.Parse(((TextBox)linha.Cells[2].FindControl("txtNotaAv2")).Text) > 100)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "A nota da avalição 2  deve estar entre 0 e 100");
                        return;
                    }
                    decimal nota = (Decimal.Parse(((TextBox)linha.Cells[2].FindControl("txtNotaAv2")).Text));

                }
                if (decimal.TryParse(((TextBox)linha.Cells[3].FindControl("txtNotaAv3")).Text, out dcmM))
                {
                    if (Decimal.Parse(((TextBox)linha.Cells[3].FindControl("txtNotaAv3")).Text) > 100)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "A nota da avalição 3  deve estar entre 0 e 100");
                        return;
                    }
                    decimal nota = (Decimal.Parse(((TextBox)linha.Cells[3].FindControl("txtNotaAv3")).Text));
                }
                if (decimal.TryParse(((TextBox)linha.Cells[4].FindControl("txtNotaAv4")).Text, out dcmM))
                {
                    if (Decimal.Parse(((TextBox)linha.Cells[4].FindControl("txtNotaAv4")).Text) > 100)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "A nota da avalição 4  deve estar entre 0 e 100");
                        return;
                    }
                    decimal nota = (Decimal.Parse(((TextBox)linha.Cells[4].FindControl("txtNotaAv4")).Text));

                }
                if (decimal.TryParse(((TextBox)linha.Cells[5].FindControl("txtNotaAv5")).Text, out dcmM))
                {
                    if (Decimal.Parse(((TextBox)linha.Cells[5].FindControl("txtNotaAv5")).Text) > 100)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "A nota da avalição 5  deve estar entre 0 e 100");
                        return;
                    }
                    decimal nota = (Decimal.Parse(((TextBox)linha.Cells[5].FindControl("txtNotaAv5")).Text));

                }

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

            int materia = (from tb02 in TB02_MATERIA.RetornaTodosRegistros()
                           where tb02.CO_MAT == coMat
                           select new { tb02.ID_MATERIA }).First().ID_MATERIA;

            //--------> Varre toda a grid de Busca
            foreach (GridViewRow linha in grdBusca.Rows)
            {
                #region AV1

                //Se tem nota av1
                if (((TextBox)linha.Cells[1].FindControl("txtNotaAv1")).Text != "")
                {
                    //Verifica se informou a data
                    string DataAV = ((TextBox)grdBusca.HeaderRow.Cells[1].FindControl("txtDataAtiv1")).Text;
                    string idNota = (((HiddenField)linha.Cells[1].FindControl("hidIdNotaAV1")).Value);
                    if (DataAV == "")
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a Data da AV1");
                        return;
                    }

                    decimal av1;
                    if (decimal.TryParse(((TextBox)linha.Cells[1].FindControl("txtNotaAv1")).Text, out av1))
                    {
                        //------------> Recebe o código do aluno
                        DateTime DataAtiv = DateTime.Parse(DataAV);
                        int coAlu = Convert.ToInt32(grdBusca.DataKeys[linha.RowIndex].Values[0]);
                        TB273_TIPO_ATIVIDADE coTipoAtiv = TB273_TIPO_ATIVIDADE.RetornaPelaSigla("SI");
                        SalvarNotasAvs("AV1", av1, coAlu, materia, modalidade, serie, anoRef, DataAtiv, coTipoAtiv, turma, idNota);
                    }
                }
                else
                {
                    int idAtiv = int.Parse(((HiddenField)linha.Cells[1].FindControl("hidIdNotaAV1")).Value);
                    DeletarAtividadeAluno(idAtiv);
                }

                #endregion

                #region AV2

                if (((TextBox)linha.Cells[2].FindControl("txtNotaAv2")).Text != "")
                {
                    //Verifica se informou a data
                    string DataAV = ((TextBox)grdBusca.HeaderRow.Cells[2].FindControl("txtDataAtiv2")).Text;
                    string idNota = (((HiddenField)linha.Cells[2].FindControl("hidIdNotaAV2")).Value);
                    if (DataAV == "")
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a Data da AV2");
                        return;
                    }

                    decimal av2;
                    if (decimal.TryParse(((TextBox)linha.Cells[2].FindControl("txtNotaAv2")).Text, out av2))
                    {
                        DateTime DataAtiv = DateTime.Parse(DataAV);
                        int coAlu = Convert.ToInt32(grdBusca.DataKeys[linha.RowIndex].Values[0]);
                        TB273_TIPO_ATIVIDADE coTipoAtiv = TB273_TIPO_ATIVIDADE.RetornaPelaSigla("PR");
                        SalvarNotasAvs("AV2", av2, coAlu, materia, modalidade, serie, anoRef, DataAtiv, coTipoAtiv, turma, idNota);
                    }
                }
                else
                {
                    int idAtiv = int.Parse(((HiddenField)linha.Cells[2].FindControl("hidIdNotaAV2")).Value);
                    DeletarAtividadeAluno(idAtiv);
                }

                #endregion

                #region AV3

                if (((TextBox)linha.Cells[3].FindControl("txtNotaAv3")).Text != "")
                {
                    //Verifica se informou a data
                    string DataAV = ((TextBox)grdBusca.HeaderRow.Cells[3].FindControl("txtDataAtiv3")).Text;
                    string idNota = (((HiddenField)linha.Cells[3].FindControl("hidIdNotaAV3")).Value);
                    if (DataAV == "")
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a Data da AV3");
                        return;
                    }

                    decimal av3;
                    if (decimal.TryParse(((TextBox)linha.Cells[3].FindControl("txtNotaAv3")).Text, out av3))
                    {
                        DateTime DataAtiv = DateTime.Parse(DataAV);
                        int coAlu = Convert.ToInt32(grdBusca.DataKeys[linha.RowIndex].Values[0]);
                        TB273_TIPO_ATIVIDADE coTipoAtiv = TB273_TIPO_ATIVIDADE.RetornaPelaSigla("SI");
                        SalvarNotasAvs("AV3", av3, coAlu, materia, modalidade, serie, anoRef, DataAtiv, coTipoAtiv, turma, idNota);
                    }
                }
                else
                {
                    int idAtiv = int.Parse(((HiddenField)linha.Cells[3].FindControl("hidIdNotaAV3")).Value);
                    DeletarAtividadeAluno(idAtiv);
                }

                #endregion

                #region AV4

                if (((TextBox)linha.Cells[4].FindControl("txtNotaAv4")).Text != "")
                {
                    //Verifica se informou a data
                    string DataAV = ((TextBox)grdBusca.HeaderRow.Cells[4].FindControl("txtDataAtiv4")).Text;
                    string idNota = (((HiddenField)linha.Cells[4].FindControl("hidIdNotaAV4")).Value);
                    if (DataAV == "")
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a Data da AV4");
                        return;
                    }

                    decimal av4;
                    if (decimal.TryParse(((TextBox)linha.Cells[4].FindControl("txtNotaAv4")).Text, out av4))
                    {
                        DateTime DataAtiv = DateTime.Parse(DataAV);
                        int coAlu = Convert.ToInt32(grdBusca.DataKeys[linha.RowIndex].Values[0]);
                        TB273_TIPO_ATIVIDADE coTipoAtiv = TB273_TIPO_ATIVIDADE.RetornaPelaSigla("PR");
                        SalvarNotasAvs("AV4", av4, coAlu, materia, modalidade, serie, anoRef, DataAtiv, coTipoAtiv, turma, idNota);
                    }
                }
                else
                {
                    int idAtiv = int.Parse(((HiddenField)linha.Cells[4].FindControl("hidIdNotaAV4")).Value);
                    DeletarAtividadeAluno(idAtiv);
                }

                #endregion

                #region AV5

                // if (((TextBox)linha.Cells[5].FindControl("txtNotaAv5")).Text != "" || ((TextBox)linha.Cells[5].FindControl("txtDataAtiv5")).Text != "")
                if (((TextBox)linha.Cells[5].FindControl("txtNotaAv5")).Text != "")
                {
                    //Verifica se informou a data
                    string DataAV = ((TextBox)grdBusca.HeaderRow.Cells[5].FindControl("txtDataAtiv5")).Text;
                    string idNota = (((HiddenField)linha.Cells[5].FindControl("hidIdNotaAV5")).Value);
                    if (DataAV == "")
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a Data da AV5");
                        return;
                    }

                    decimal av5;
                    if (decimal.TryParse(((TextBox)linha.Cells[5].FindControl("txtNotaAv5")).Text, out av5))
                    {
                        DateTime DataAtiv = DateTime.Parse(DataAV);
                        int coAlu = Convert.ToInt32(grdBusca.DataKeys[linha.RowIndex].Values[0]);
                        TB273_TIPO_ATIVIDADE coTipoAtiv = TB273_TIPO_ATIVIDADE.RetornaPelaSigla("AT");
                        SalvarNotasAvs("AV5", av5, coAlu, materia, modalidade, serie, anoRef, DataAtiv, coTipoAtiv, turma, idNota);
                    }
                }
                else
                {
                    int idAtiv = int.Parse(((HiddenField)linha.Cells[5].FindControl("hidIdNotaAV5")).Value);
                    DeletarAtividadeAluno(idAtiv);
                }

                #endregion
            }

            AuxiliPagina.RedirecionaParaPaginaSucesso("Registros Salvos com sucesso.", Request.Url.AbsoluteUri);
        }

        private void SalvarNotasAvs(string TipoAvaliacao, decimal TipoNota, int coAlu, int materia, int modalidade, int serie, string anoRef, DateTime dataAtiv, TB273_TIPO_ATIVIDADE coTipoAtiv, int turma, string idNota)
        {
            if (TipoNota != 0)
            {
                #region individual

                //------------> Recebe o dado da tabela de histórico de acordo com as chaves passadas
                TB49_NOTA_ATIV_ALUNO tb49 = (from iTb49 in TB49_NOTA_ATIV_ALUNO.RetornaTodosRegistros()
                                             where iTb49.TB07_ALUNO.CO_ALU == coAlu && iTb49.DT_NOTA_ATIV.Year == dataAtiv.Year
                                             && iTb49.DT_NOTA_ATIV.Month == dataAtiv.Month && iTb49.DT_NOTA_ATIV.Day == dataAtiv.Day
                                             && iTb49.TB273_TIPO_ATIVIDADE.ID_TIPO_ATIV == coTipoAtiv.ID_TIPO_ATIV
                                                 //Na tabela TB49_NOTA_ATIV_ALUNO não existe a coluna CO_TRIMESTRE 
                                             && iTb49.CO_BIMESTRE == ddlReferencia.SelectedValue
                                             && iTb49.CO_REFER_NOTA == TipoAvaliacao
                                             && iTb49.TB107_CADMATERIAS.ID_MATERIA == materia
                                             select iTb49).FirstOrDefault();

                //TB49_NOTA_ATIV_ALUNO tb49;
                //if (!string.IsNullOrEmpty(idNota)) //Se for de um registro que exite, instancia o objeto da entidade para alteração
                //    tb49 = TB49_NOTA_ATIV_ALUNO.RetornaPelaChavePrimaria(int.Parse(idNota));
                //else //Se não existir, cria um novo objeto da entidade
                if (tb49 == null)
                {
                    tb49 = new TB49_NOTA_ATIV_ALUNO();
                    tb49.DT_NOTA_ATIV_CAD = DateTime.Now;
                }

                tb49.CO_BIMESTRE = ddlReferencia.SelectedValue;
                tb49.CO_SEMESTRE = ddlReferencia.SelectedValue == "B1" || ddlReferencia.SelectedValue == "B2" ? "1" : "2";
                tb49.CO_ANO = Convert.ToInt32(anoRef);
                tb49.TB01_CURSO = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie);
                tb49.TB06_TURMAS = TB06_TURMAS.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie, turma);
                tb49.TB07_ALUNO = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                tb49.TB107_CADMATERIAS = TB107_CADMATERIAS.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, materia);
                tb49.CO_TIPO_ATIV = " ";
                tb49.CO_REFER_NOTA = TipoAvaliacao;
                tb49.TB273_TIPO_ATIVIDADE = coTipoAtiv;
                tb49.DT_NOTA_ATIV = dataAtiv;
                tb49.NO_NOTA_ATIV = coTipoAtiv.NO_TIPO_ATIV;
                tb49.VL_NOTA = TipoNota;
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

                #endregion
            }
            else
            {
                //------------> Recebe o dado da tabela de histórico de acordo com as chaves passadas
                TB49_NOTA_ATIV_ALUNO tb49 = (from iTb49 in TB49_NOTA_ATIV_ALUNO.RetornaTodosRegistros()
                                             where iTb49.TB07_ALUNO.CO_ALU == coAlu && iTb49.DT_NOTA_ATIV.Year == dataAtiv.Year
                                             && iTb49.DT_NOTA_ATIV.Month == dataAtiv.Month && iTb49.DT_NOTA_ATIV.Day == dataAtiv.Day
                                             && iTb49.TB273_TIPO_ATIVIDADE.ID_TIPO_ATIV == coTipoAtiv.ID_TIPO_ATIV
                                                 //Na tabela TB49_NOTA_ATIV_ALUNO não existe a coluna CO_TRIMESTRE 
                                             && iTb49.CO_BIMESTRE == ddlReferencia.SelectedValue
                                             && iTb49.CO_REFER_NOTA == TipoAvaliacao
                                             && iTb49.TB107_CADMATERIAS.ID_MATERIA == materia
                                             select iTb49).FirstOrDefault();
                if (tb49 != null)
                    TB49_NOTA_ATIV_ALUNO.Delete(tb49, true);
            }
        }

        /// <summary>
        /// Deleta a atividade de id recebido como parametro
        /// </summary>
        /// <param name="ID_ATIV"></param>
        private void DeletarAtividadeAluno(int ID_ATIV)
        {
            var res = TB49_NOTA_ATIV_ALUNO.RetornaPelaChavePrimaria(ID_ATIV);
            TB49_NOTA_ATIV_ALUNO.Delete(res, true);
        }
        #endregion

        #region Carregamento

        ///// <summary>
        ///// Salva a mesma nota em todas as disciplinas do aluno
        ///// </summary>
        //private void SalvaNotaTodasDisciplinas(int modalidade, int serie, string anog, int coem, int coAlu,
        //    DateTime dataAtiv, int coTipoAtiv, int turma, decimal TipoNota, string TipoAvaliacao, string noAluno)
        //{

        //    //Coleta todas as disciplinas da grade do curso em questão
        //    var res = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
        //               where tb43.TB44_MODULO.CO_MODU_CUR == modalidade
        //               && tb43.CO_CUR == serie
        //               && tb43.CO_ANO_GRADE == anog
        //               && tb43.CO_EMP == coem
        //               join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
        //               join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
        //               select new { tb02.CO_MAT, tb107.NO_MATERIA, tb107.ID_MATERIA }).Distinct().OrderBy(m => m.NO_MATERIA).ToList();

        //    if (res.Count > 0)
        //    {
        //        //Percorre todas as disciplinas da grade do curso em questão
        //        foreach (var i in res)
        //        {
        //            string siglaTipo = TB273_TIPO_ATIVIDADE.RetornaPelaChavePrimaria(coTipoAtiv).CO_SIGLA_ATIV;

        //            //Regra criada em atendimento à necessidade do Colégio específico, limitando a nota para atividade em 2 pontos
        //            #region Trata a nota máxima
        //            switch (siglaTipo)
        //            {
        //                case "AT":
        //                    decimal? resat = TB43_GRD_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, anog, serie, i.CO_MAT).VL_NOTA_MAXIM_ATIVI;
        //                    if (TipoNota > (resat.HasValue ? resat.Value : 2))
        //                    {
        //                        if (resat.HasValue)
        //                            AuxiliPagina.EnvioMensagemErro(this.Page, "A nota informada para o aluno(a) " + noAluno + " transcede a nota máxima da Disciplina " + i.NO_MATERIA + ", que é " + resat);
        //                        else
        //                            AuxiliPagina.EnvioMensagemErro(this.Page, "Não há nota máxima para Atividades informada na grade Anual. A nota informada para o Aluno(a) " + noAluno + " é superior à nota limite padrão de 2,0");

        //                        return;
        //                    }
        //                    break;

        //                case "PR":
        //                    decimal? respr = TB43_GRD_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, anog, serie, i.CO_MAT).VL_NOTA_MAXIM_PROVA;
        //                    if (respr.HasValue)
        //                    {
        //                        if (TipoNota > respr)
        //                        {
        //                            AuxiliPagina.EnvioMensagemErro(this.Page, "A nota informada para o aluno(a) " + noAluno + " transcede a nota máxima da Disciplina " + i.NO_MATERIA + ", que é " + respr);
        //                            return;
        //                        }
        //                    }
        //                    break;

        //                case "SI":
        //                    decimal? ressi = TB43_GRD_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, anog, serie, i.CO_MAT).VL_NOTA_MAXIM_SIMUL;

        //                    if (ressi.HasValue)
        //                    {
        //                        if (TipoNota > ressi)
        //                        {
        //                            AuxiliPagina.EnvioMensagemErro(this.Page, "A nota informada para o aluno(a) " + noAluno + " transcede a nota máxima da Disciplina " + i.NO_MATERIA + ", que é " + ressi);
        //                            return;
        //                        }
        //                    }

        //                    break;
        //            }
        //            #endregion

        //            //Para cada disciplina, grava a nota replicada recebida como parâmetro
        //            TB49_NOTA_ATIV_ALUNO tb49 = (from iTb49 in TB49_NOTA_ATIV_ALUNO.RetornaTodosRegistros()
        //                                         where iTb49.TB07_ALUNO.CO_ALU == coAlu && iTb49.DT_NOTA_ATIV.Year == dataAtiv.Year
        //                                         && iTb49.DT_NOTA_ATIV.Month == dataAtiv.Month && iTb49.DT_NOTA_ATIV.Day == dataAtiv.Day
        //                                         && iTb49.TB273_TIPO_ATIVIDADE.ID_TIPO_ATIV == coTipoAtiv
        //                                         && iTb49.CO_BIMESTRE == ddlBimestre.SelectedValue
        //                                         && iTb49.CO_REFER_NOTA == TipoAvaliacao
        //                                         && iTb49.TB107_CADMATERIAS.ID_MATERIA == i.ID_MATERIA
        //                                         select iTb49).FirstOrDefault();

        //            //Se for igual a nulo, instancia nova entidade
        //            if (tb49 == null)
        //            {
        //                tb49 = new TB49_NOTA_ATIV_ALUNO();
        //                tb49.DT_NOTA_ATIV_CAD = DateTime.Now;
        //            }

        //            tb49.CO_BIMESTRE = ddlBimestre.SelectedValue;
        //            tb49.CO_SEMESTRE = ddlBimestre.SelectedValue == "B1" || ddlBimestre.SelectedValue == "B2" ? "1" : "2";
        //            tb49.CO_ANO = int.Parse(anog);
        //            tb49.TB01_CURSO = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie);
        //            tb49.TB06_TURMAS = TB06_TURMAS.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie, turma);
        //            tb49.TB07_ALUNO = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
        //            tb49.TB107_CADMATERIAS = TB107_CADMATERIAS.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, i.ID_MATERIA);
        //            tb49.CO_TIPO_ATIV = " ";
        //            tb49.CO_REFER_NOTA = TipoAvaliacao;
        //            tb49.TB273_TIPO_ATIVIDADE = TB273_TIPO_ATIVIDADE.RetornaPelaChavePrimaria(coTipoAtiv);
        //            //tb49.DT_NOTA_ATIV = Convert.ToDateTime(txtDataAtiv.Text);
        //            tb49.NO_NOTA_ATIV = ddlTipoAtiv.SelectedItem.ToString();
        //            tb49.VL_NOTA = TipoNota;
        //            tb49.FL_NOTA_ATIV = "S";
        //            tb49.FL_JUSTI_NOTA_ATIV = "N";
        //            tb49.FL_LANCA_NOTA = "S";
        //            tb49.CO_STATUS = "A";

        //            tb49.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_UNID_FUNC, LoginAuxili.CO_COL);

        //            if (tb49.EntityState != System.Data.EntityState.Unchanged && GestorEntities.SaveOrUpdate(tb49) < 1)
        //            {
        //                AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao salvar os itens");
        //                return;
        //            }

        //        }
        //    }
        //}

        ///// <summary>
        ///// Verifica a permissão para lançamento múltiplo de notas
        ///// </summary>
        //private void VerificarPermissaoLancMulti()
        //{
        //    string perLanc = ADMUSUARIO.RetornaPelaChavePrimaria(LoginAuxili.IDEADMUSUARIO).FLA_PERM_LANC_MULTI;

        //    if (perLanc == "N")
        //        ddlLanctoTM.Enabled = false;
        //    else
        //        ddlLanctoTM.Enabled = true;
        //}

        /// <summary>
        /// Método que carrega a grid
        /// </summary>
        private void CarregaGrid()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int materia = ddlDisciplina.SelectedValue != "" ? int.Parse(ddlDisciplina.SelectedValue) : 0;
            string referencia = ddlReferencia.SelectedValue;

            //int coTipoAtiv = ddlTipoAtiv.SelectedValue != "" ? int.Parse(ddlTipoAtiv.SelectedValue) : 0;
            string anoMesMat = ddlAno.SelectedValue;
            //DateTime dataAtiv = DateTime.Now;

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
                                 CO_ALU = tb079.CO_ALU,
                                 NO_ALU = tb08.TB07_ALUNO.NO_ALU.ToUpper(),
                                 NU_NIRE = tb08.TB07_ALUNO.NU_NIRE,
                                 MEDIA_HOMOLOGADA_R = (referencia == "B1" ? tb079.FL_HOMOL_NOTA_BIM1 : referencia == "B2" ?
                                 tb079.FL_HOMOL_NOTA_BIM2 : referencia == "B3" ? tb079.FL_HOMOL_NOTA_BIM3 : referencia == "B4" ?
                                 tb079.FL_HOMOL_NOTA_BIM4 : referencia == "T1" ? tb079.FL_HOMOL_NOTA_TRI1 : referencia == "T2" ? tb079.FL_HOMOL_NOTA_TRI2 : tb079.FL_HOMOL_NOTA_TRI3),
                             }).ToList();

            foreach (var res in resultado)
            {

                //----------------------------------------------------------------------------------------------------------------------------------------------------------
                var tb49 = (from iTb49 in TB49_NOTA_ATIV_ALUNO.RetornaTodosRegistros()
                            where iTb49.TB07_ALUNO.CO_ALU == res.CO_ALU
                                //Na tabela TB49_NOTA_ATIV_ALUNO não existe a coluna CO_TRIMESTRE 
                            && iTb49.CO_BIMESTRE == ddlReferencia.SelectedValue
                            && iTb49.TB107_CADMATERIAS.ID_MATERIA == idMateria
                            select iTb49).ToList();

                //----------------------------------------------------------------------------------------------------------------------------------------------------------



                foreach (var item in tb49)
                {

                    switch (item.CO_REFER_NOTA)
                    {
                        case "AV1":
                            res.NOTA_AV1 = item.VL_NOTA.ToString("N1");
                            res.DATA_AV1 = item.DT_NOTA_ATIV;
                            res.ID_NOTA_AV1 = item.ID_NOTA_ATIV;
                            break;
                        case "AV2":
                            res.NOTA_AV2 = item.VL_NOTA.ToString("N1");
                            res.DATA_AV2 = item.DT_NOTA_ATIV;
                            res.ID_NOTA_AV2 = item.ID_NOTA_ATIV;
                            break;
                        case "AV3":
                            res.NOTA_AV3 = item.VL_NOTA.ToString("N1");
                            res.DATA_AV3 = item.DT_NOTA_ATIV;
                            res.ID_NOTA_AV3 = item.ID_NOTA_ATIV;
                            break;
                        case "AV4":
                            res.NOTA_AV4 = item.VL_NOTA.ToString("N1");
                            res.DATA_AV4 = item.DT_NOTA_ATIV;
                            res.ID_NOTA_AV4 = item.ID_NOTA_ATIV;
                            break;
                        case "AV5":
                            res.NOTA_AV5 = item.VL_NOTA.ToString("N1");
                            res.DATA_AV5 = item.DT_NOTA_ATIV;
                            res.ID_NOTA_AV5 = item.ID_NOTA_ATIV;
                            break;
                        default:
                            break;
                    }

                    res.MD = (Convert.ToDecimal(res.NOTA_AV1) + Convert.ToDecimal(res.NOTA_AV2) + Convert.ToDecimal(res.NOTA_AV3) + Convert.ToDecimal(res.NOTA_AV4) + Convert.ToDecimal(res.NOTA_AV5)) / 3;
                }



            }

            grdBusca.DataKeyNames = new string[] { "CO_ALU" };

            grdBusca.DataSource = resultado.Count() > 0 ? resultado.OrderBy(p => p.NO_ALU) : null;
            grdBusca.DataBind();

            if (grdBusca.DataSource != null)
            {

                //Ver se o nível de acesso do usuário para dar a premiação  de edição das avaliações 
                #region Verifica   usuário

                //if (LoginAuxili.CLASSIFICACAO_USU_LOGADO == "M")
                //{

                //    return;
                //}
                //else if (LoginAuxili.CLASSIFICACAO_USU_LOGADO == "M" && LoginAuxili.FLA_PROFESSOR == "S")
                //{
                //    return;
                //}
                //else if (LoginAuxili.FLA_PROFESSOR == "S")
                //{

                //    foreach (GridViewRow linha in grdBusca.Rows)
                //    {
                //        TextBox txt1;
                //        txt1 = (TextBox)linha.Cells[1].FindControl("txtNotaAv1");
                //        txt1.Enabled = false;

                //        TextBox txtData1;
                //        txtData1 = (TextBox)grdBusca.HeaderRow.Cells[1].FindControl("txtDataAtiv1");
                //        txtData1.Enabled = false;
                //        //-----------------------------------------------------------------------------------------------------------------------------------
                //        TextBox txt3;
                //        txt3 = (TextBox)linha.Cells[3].FindControl("txtNotaAv3");
                //        txt3.Enabled = false;

                //        TextBox txtData3;
                //        txtData3 = (TextBox)grdBusca.HeaderRow.Cells[4].FindControl("txtDataAtiv3");
                //        txtData3.Enabled = false;
                //        //-----------------------------------------------------------------------------------------------------------------------------------

                //    }
                //}
                //else
                //{
                //    foreach (GridViewRow linha in grdBusca.Rows)
                //    {

                //        #region Notas
                //        TextBox txt1;
                //        txt1 = (TextBox)linha.Cells[1].FindControl("txtNotaAv1");
                //        txt1.Enabled = false;

                //        TextBox txt2;
                //        txt2 = (TextBox)linha.Cells[2].FindControl("txtNotaAv2");
                //        txt2.Enabled = false;

                //        TextBox txt3;
                //        txt3 = (TextBox)linha.Cells[3].FindControl("txtNotaAv3");
                //        txt3.Enabled = false;

                //        TextBox txt4;
                //        txt4 = (TextBox)linha.Cells[4].FindControl("txtNotaAv4");
                //        txt4.Enabled = false;

                //        TextBox txt5;
                //        txt5 = (TextBox)linha.Cells[5].FindControl("txtNotaAv5");
                //        txt5.Enabled = false;
                //        #endregion

                //        #region Datas das Avs

                //        TextBox txtData1;
                //        txtData1 = (TextBox)grdBusca.HeaderRow.Cells[1].FindControl("txtDataAtiv1");
                //        txtData1.Enabled = false;

                //        TextBox txtData2;
                //        txtData2 = (TextBox)grdBusca.HeaderRow.Cells[2].FindControl("txtDataAtiv2");
                //        txtData2.Enabled = false;

                //        TextBox txtData3;
                //        txtData3 = (TextBox)grdBusca.HeaderRow.Cells[3].FindControl("txtDataAtiv3");
                //        txtData3.Enabled = false;

                //        TextBox txtData4;
                //        txtData4 = (TextBox)grdBusca.HeaderRow.Cells[4].FindControl("txtDataAtiv4");
                //        txtData4.Enabled = false;

                //        TextBox txtData5;
                //        txtData5 = (TextBox)grdBusca.HeaderRow.Cells[5].FindControl("txtDataAtiv5");
                //        txtData5.Enabled = false;

                //        #endregion

                //    }
                //}

                foreach (GridViewRow linha in grdBusca.Rows)
                {
                    #region Notas
                    TextBox txt1;
                    txt1 = (TextBox)linha.Cells[1].FindControl("txtNotaAv1");
                    txt1.Enabled = true;

                    TextBox txt2;
                    txt2 = (TextBox)linha.Cells[2].FindControl("txtNotaAv2");
                    txt2.Enabled = true;

                    TextBox txt3;
                    txt3 = (TextBox)linha.Cells[3].FindControl("txtNotaAv3");
                    txt3.Enabled = true;

                    TextBox txt4;
                    txt4 = (TextBox)linha.Cells[4].FindControl("txtNotaAv4");
                    txt4.Enabled = true;

                    TextBox txt5;
                    txt5 = (TextBox)linha.Cells[5].FindControl("txtNotaAv5");
                    txt5.Enabled = true;
                    #endregion

                    #region Datas das Avs

                    TextBox txtData1;
                    txtData1 = (TextBox)grdBusca.HeaderRow.Cells[1].FindControl("txtDataAtiv1");
                    txtData1.Enabled = true;

                    TextBox txtData2;
                    txtData2 = (TextBox)grdBusca.HeaderRow.Cells[2].FindControl("txtDataAtiv2");
                    txtData2.Enabled = true;

                    TextBox txtData3;
                    txtData3 = (TextBox)grdBusca.HeaderRow.Cells[3].FindControl("txtDataAtiv3");
                    txtData3.Enabled = true;

                    TextBox txtData4;
                    txtData4 = (TextBox)grdBusca.HeaderRow.Cells[4].FindControl("txtDataAtiv4");
                    txtData4.Enabled = true;

                    TextBox txtData5;
                    txtData5 = (TextBox)grdBusca.HeaderRow.Cells[5].FindControl("txtDataAtiv5");
                    txtData5.Enabled = true;


                    #endregion
                }

                #endregion

                return;
            }


        }

        /// <summary>
        /// Carrega os alunos matriculados no contexto
        /// </summary>
        private void CarregaAlunosMatriculados()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int materia = ddlDisciplina.SelectedValue != "" ? int.Parse(ddlDisciplina.SelectedValue) : 0;
            string anoMesMat = ddlAno.SelectedValue;


            divGrid.Visible = true;

            var resultado = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                             where tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                             && tb08.TB44_MODULO.CO_MODU_CUR == modalidade
                             && tb08.CO_ANO_MES_MAT == anoMesMat
                             && tb08.CO_CUR == serie
                             && tb08.CO_TUR == turma
                             && tb08.CO_SIT_MAT == "A"
                             select new NotasAluno
                             {
                                 CO_ALU = tb08.CO_ALU,
                                 NO_ALU = tb08.TB07_ALUNO.NO_ALU.ToUpper(),
                                 NU_NIRE = tb08.TB07_ALUNO.NU_NIRE
                             }).ToList();

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
                                 select new { tb43.CO_ANO_GRADE }).Distinct().OrderByDescending(g => g.CO_ANO_GRADE);

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
        //private void CarregaDisciplina()
        //{
        //    int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
        //    int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
        //    int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
        //    string ano = ddlAno.SelectedValue;
        //    int anoInt = int.Parse(ddlAno.SelectedValue);

        //    string turmaUnica = "N";
        //    if (modalidade != 0 && serie != 0 && turma != 0)
        //    {
        //        // Recebe o conteúdo da coluna CO_FLAG_RESP_TURMA, da tabela TB06_TURMAS, que informa se a turma é única ou não
        //        turmaUnica = TB06_TURMAS.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie, turma).CO_FLAG_RESP_TURMA;
        //    }
        //    ddlDisciplina.Items.Clear();

        //    #region Verificação e criação da matéria de turma única
        //    //---------> Verifica se a turma será única ou não
        //    if (turmaUnica == "S")
        //    {
        //        //-------------> Verifica se existe uma matéria com sigla "MSR", que é a matéria padrão para turma única
        //        if (!TB107_CADMATERIAS.RetornaTodosRegistros().Where(cm => cm.NO_SIGLA_MATERIA == "MSR").Any())
        //        {
        //            //-----------------> Cria uma matéria para ser a padrão de turma única, MSR.
        //            TB107_CADMATERIAS cm = new TB107_CADMATERIAS();

        //            cm.CO_EMP = LoginAuxili.CO_EMP;
        //            cm.NO_SIGLA_MATERIA = "MSR";
        //            cm.NO_MATERIA = "Atividades Letivas";
        //            cm.NO_RED_MATERIA = "Atividades";
        //            cm.DE_MATERIA = "Matéria utilizada no lançamento de atividades para professores de turma única.";
        //            cm.CO_STATUS = "A";
        //            cm.DT_STATUS = DateTime.Now;
        //            cm.CO_CLASS_BOLETIM = 4;
        //            TB107_CADMATERIAS.SaveOrUpdate(cm);

        //            CurrentPadraoCadastros.CurrentEntity = cm;

        //            //-----------------> Vincula a matéria MSR ao curso selecionado
        //            int idMat = TB107_CADMATERIAS.RetornaTodosRegistros().Where(cma => cma.NO_SIGLA_MATERIA == "MSR").FirstOrDefault().ID_MATERIA;
        //            TB02_MATERIA m = new TB02_MATERIA();

        //            m.CO_EMP = LoginAuxili.CO_EMP;
        //            m.CO_MODU_CUR = modalidade;
        //            m.CO_CUR = serie;
        //            m.TB01_CURSO = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie);
        //            m.ID_MATERIA = idMat;
        //            m.QT_CRED_MAT = null;
        //            m.QT_CARG_HORA_MAT = 800;
        //            m.DT_INCL_MAT = DateTime.Now;
        //            m.DT_SITU_MAT = DateTime.Now;
        //            m.CO_SITU_MAT = "I";
        //            TB02_MATERIA.SaveOrUpdate(m);

        //            CurrentPadraoCadastros.CurrentEntity = m;
        //        }
        //        else
        //        {
        //            //-----------------> Verifica se a matéria MSR está vinculada ao curso
        //            int idMat = TB107_CADMATERIAS.RetornaTodosRegistros().Where(cm => cm.NO_SIGLA_MATERIA == "MSR").FirstOrDefault().ID_MATERIA;
        //            if (!TB02_MATERIA.RetornaTodosRegistros().Where(m => m.CO_EMP == LoginAuxili.CO_EMP && m.CO_MODU_CUR == modalidade && m.CO_CUR == serie && m.ID_MATERIA == idMat).Any())
        //            {
        //                //---------------------> Vincula a matéria MSR ao curso selecionado.
        //                TB02_MATERIA m = new TB02_MATERIA();

        //                m.CO_EMP = LoginAuxili.CO_EMP;
        //                m.CO_MODU_CUR = modalidade;
        //                m.CO_CUR = serie;
        //                m.TB01_CURSO = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie);
        //                m.ID_MATERIA = idMat;
        //                m.QT_CRED_MAT = null;
        //                m.QT_CARG_HORA_MAT = 800;
        //                m.DT_INCL_MAT = DateTime.Now;
        //                m.DT_SITU_MAT = DateTime.Now;
        //                m.CO_SITU_MAT = "I";
        //                TB02_MATERIA.SaveOrUpdate(m);

        //                CurrentPadraoCadastros.CurrentEntity = m;
        //            }
        //        }
        //    }
        //    #endregion


        //    // Verifica se a turma selecionada pelo usuário é turma única
        //    if (turmaUnica == "S")
        //    {
        //        // No caso de ser turma única o sistema deve retornar somente a matéria com sigla MSR, que é a matéria
        //        // padrão para turmas únicas, que não precisam de controle por matéria.
        //        if (LoginAuxili.FLA_PROFESSOR != "S")
        //        {
        //            var res = (from tb02 in TB02_MATERIA.RetornaTodosRegistros()
        //                       join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
        //                       where tb02.CO_MODU_CUR == modalidade
        //                       && tb02.CO_CUR == serie
        //                       && tb107.NO_SIGLA_MATERIA == "MSR"
        //                       select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy(m => m.NO_MATERIA).DistinctBy(d => d.CO_MAT);

        //            ddlDisciplina.DataTextField = "NO_MATERIA";
        //            ddlDisciplina.DataValueField = "CO_MAT";
        //            ddlDisciplina.DataSource = res;
        //            ddlDisciplina.DataBind();

        //            if (res.Count() > 1)
        //                ddlDisciplina.Items.Insert(0, new ListItem("Selecione", "0"));
        //        }
        //        else
        //        {
        //            // No caso de ser turma única o sistema deve retornar somente a matéria com sigla MSR, que é a matéria
        //            // padrão para turmas únicas, que não precisam de controle por matéria.
        //            var res = (from tb02 in TB02_MATERIA.RetornaTodosRegistros()
        //                       join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
        //                       where tb02.CO_MODU_CUR == modalidade
        //                       && tb02.CO_CUR == serie
        //                       && tb107.NO_SIGLA_MATERIA == "MSR"
        //                       select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy(m => m.NO_MATERIA).DistinctBy(d => d.CO_MAT);

        //            if (res.Count() > 0)
        //            {
        //                ddlDisciplina.DataTextField = "NO_MATERIA";
        //                ddlDisciplina.DataValueField = "CO_MAT";
        //                ddlDisciplina.DataSource = res;
        //                ddlDisciplina.DataBind();

        //                if (res.Count() > 1)
        //                    ddlDisciplina.Items.Insert(0, new ListItem("Selecione", "0"));
        //            }
        //        }

        //    }
        //    else
        //    {
        //        if (LoginAuxili.FLA_PROFESSOR != "S")
        //        {
        //            string anog = anoInt.ToString();
        //            int coem = LoginAuxili.CO_EMP;
        //            var res = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
        //                       where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.CO_CUR == serie && tb43.CO_ANO_GRADE == anog && tb43.CO_EMP == coem
        //                       join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
        //                       join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
        //                       select new { tb02.CO_MAT, tb107.NO_MATERIA }).Distinct().OrderBy(m => m.NO_MATERIA);

        //            if (res != null)
        //            {
        //                ddlDisciplina.DataTextField = "NO_MATERIA";
        //                ddlDisciplina.DataValueField = "CO_MAT";
        //                ddlDisciplina.DataSource = res;
        //                ddlDisciplina.DataBind();
        //            }
        //            if (res.Count() > 1)
        //                ddlDisciplina.Items.Insert(0, new ListItem("Selecione", "0"));
        //        }
        //        else
        //        {
        //            var resuR = (from tbres in TB_RESPON_MATERIA.RetornaTodosRegistros()
        //                         join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tbres.CO_MAT equals tb02.CO_MAT
        //                         join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
        //                         where tbres.CO_MODU_CUR == modalidade
        //                         && tbres.CO_CUR == serie
        //                         && tbres.CO_COL_RESP == LoginAuxili.CO_COL
        //                         && tbres.CO_TUR == turma
        //                         select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy(m => m.NO_MATERIA).DistinctBy(d => d.CO_MAT);

        //            if (resuR.Count() > 0)
        //            {
        //                ddlDisciplina.DataTextField = "NO_MATERIA";
        //                ddlDisciplina.DataValueField = "CO_MAT";
        //                ddlDisciplina.DataSource = resuR;
        //                ddlDisciplina.DataBind();
        //            }

        //            ddlDisciplina.Items.Insert(0, new ListItem("Selecione", "0"));
        //        }
        //    }
        //}

        /// <summary>
        /// Método que carrega o dropdown de Disciplinas
        /// </summary>
        private void CarregaDisciplina()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int professor = LoginAuxili.CO_COL;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
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
                        ddlDisciplina.Items.Insert(0, new ListItem("Selecione", ""));
                }
            }
            else
            {
                if (LoginAuxili.FLA_PROFESSOR == "S")
                {
                    var res = (from tbres in TB_RESPON_MATERIA.RetornaTodosRegistros()
                               join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tbres.CO_MAT equals tb02.CO_MAT
                               join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                               where tbres.CO_MODU_CUR == modalidade
                               && tbres.CO_CUR == serie
                               && tbres.CO_COL_RESP == professor
                               && tbres.CO_TUR == turma
                               select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy(m => m.NO_MATERIA).DistinctBy(d => d.CO_MAT);

                    if (res.Count() > 0)
                    {
                        ddlDisciplina.DataTextField = "NO_MATERIA";
                        ddlDisciplina.DataValueField = "CO_MAT";
                        ddlDisciplina.DataSource = res;
                        ddlDisciplina.DataBind();

                        if (res.Count() > 1)
                            ddlDisciplina.Items.Insert(0, new ListItem("Selecione", ""));
                    }
                }
                else
                {
                    string anog = ddlAno.SelectedValue.ToString();
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
                        ddlDisciplina.Items.Insert(0, new ListItem("Selecione", ""));
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

            /////Atribui as informações finais ao campo correspondente
            //var resul = (from tb273 in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros()
            //             where tb273.FL_LANCA_NOTA_ATIV == "S"
            //             select new { tb273.ID_TIPO_ATIV, tb273.NO_TIPO_ATIV, }).OrderBy(o => o.NO_TIPO_ATIV);

            //ddlTipoAtiv.DataTextField = "NO_TIPO_ATIV";
            //ddlTipoAtiv.DataValueField = "ID_TIPO_ATIV";
            //ddlTipoAtiv.DataSource = resul;
            //ddlTipoAtiv.DataBind();

            //ddlTipoAtiv.Items.Insert(0, new ListItem("Selecione", ""));
        }

        #endregion

        #region Funções de Campo

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

        protected void btnPesqGride_Click(object sender, EventArgs e)
        {
            if (ddlModalidade.SelectedValue == "" || ddlSerieCurso.SelectedValue == "" || ddlTurma.SelectedValue == "" || ddlDisciplina.SelectedValue == "" || ddlDisciplina.SelectedValue == ""
                || ddlAno.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Ano, Modalidade, Série, Turma e Disciplina devem ser informados.");
                return;
            }



            //Se não for lançamento múltiplo

            //CarregaAlunosMatriculados();
            CarregaGrid();
        }

        protected void ddlReferencia_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

            tb25.TB82_DTCT_EMPReference.Load();

            DateTime dataLacto = DateTime.Now.Date;
            if (tb25.TB82_DTCT_EMP != null)
            {
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
                else
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
            }
        }

        #endregion

        #region Lista de Notas dos Alunos

        public class NotasAluno
        {
            public string NIREALUNO
            {

                get
                {

                    return this.NU_NIRE.ToString().PadLeft(9, '0') + " - " + this.NO_ALU;

                }


            }

            public Decimal? MD { get; set; }
            public string MDVALOR
            {
                get
                {

                    return (this.MD.HasValue ? this.MD.Value.ToString("N1") : "");

                }

            }
            public string NO_ALU { get; set; }
            public string NOTA_AV1 { get; set; }
            public DateTime? DATA_AV1 { get; set; }
            public string NOTA_AV2 { get; set; }
            public DateTime? DATA_AV2 { get; set; }
            public string NOTA_AV3 { get; set; }
            public DateTime? DATA_AV3 { get; set; }
            public string NOTA_AV4 { get; set; }
            public DateTime? DATA_AV4 { get; set; }
            public string NOTA_AV5 { get; set; }
            public DateTime? DATA_AV5 { get; set; }
            public int NU_NIRE { get; set; }
            public int CO_ALU { get; set; }
            public string MEDIA_HOMOLOGADA_R { get; set; }
            public bool MEDIA_HOMOLOGADA
            {
                get
                {
                    //Se a média estiver homologada, retorna false para desabiltar o campo, caso contrário, habilita
                    return (this.MEDIA_HOMOLOGADA_R == "S" ? false : true);
                }
            }
            public bool MOSTRA_ASTERISCO
            {
                get
                {
                    //Se a média estiver homologada, retorna false para desabiltar o campo, caso contrário, habilita
                    return (this.MEDIA_HOMOLOGADA_R == "S" ? true : false);
                }
            }

            public int ID_NOTA_AV1 { get; set; }
            public int ID_NOTA_AV2 { get; set; }
            public int ID_NOTA_AV3 { get; set; }
            public int ID_NOTA_AV4 { get; set; }
            public int ID_NOTA_AV5 { get; set; }

            public string DescNU_NIRE { get { return this.NU_NIRE.ToString().PadLeft(9, '0'); } }
        }

        /// <summary>
        /// Evento necessário para que a grid de exames médicos disponíveis "clicável" funcione
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdBusca_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //--------> Criação do estilo e links das linhas da GRID
            if (e.Row.DataItem != null)
            {
                #region Data 1

                string data1 = ((HiddenField)e.Row.Cells[1].FindControl("hidDtAV1")).Value;
                if (!string.IsNullOrEmpty(data1))
                {
                    TextBox txtData = ((TextBox)grdBusca.HeaderRow.Cells[1].FindControl("txtDataAtiv1"));
                    txtData.Text = data1;
                }

                #endregion

                #region Data 2

                string data2 = ((HiddenField)e.Row.Cells[2].FindControl("hidDtAV2")).Value;
                if (!string.IsNullOrEmpty(data2))
                {
                    TextBox txtData = ((TextBox)grdBusca.HeaderRow.Cells[2].FindControl("txtDataAtiv2"));
                    txtData.Text = data2;
                }

                #endregion

                #region Data 3

                string data3 = ((HiddenField)e.Row.Cells[3].FindControl("hidDtAV3")).Value;
                if (!string.IsNullOrEmpty(data3))
                {
                    TextBox txtData = ((TextBox)grdBusca.HeaderRow.Cells[3].FindControl("txtDataAtiv3"));
                    txtData.Text = data3;
                }

                #endregion

                #region Data 4

                string data4 = ((HiddenField)e.Row.Cells[4].FindControl("hidDtAV4")).Value;
                if (!string.IsNullOrEmpty(data4))
                {
                    TextBox txtData = ((TextBox)grdBusca.HeaderRow.Cells[4].FindControl("txtDataAtiv4"));
                    txtData.Text = data4;
                }

                #endregion

                #region Data 5

                string data5 = ((HiddenField)e.Row.Cells[5].FindControl("hidDtAV5")).Value;
                if (!string.IsNullOrEmpty(data5))
                {
                    TextBox txtData = ((TextBox)grdBusca.HeaderRow.Cells[5].FindControl("txtDataAtiv5"));
                    txtData.Text = data5;
                }

                #endregion
            }
        }

        #endregion
    }
}