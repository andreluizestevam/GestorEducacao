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
using System.Collections.Generic;

namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3500_CtrlDesempenhoEscolar._3540_HomologacaoMediaTrimestral
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
            //liChk.Visible = false;

        }

        //====> Carrega o tipo de medida da Referência (Mensal/Bimestre/Trimestre/Semestre/Anual)
        private void CarregaMedidas()
        {
            var tipo = "B";

            var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

            tb25.TB83_PARAMETROReference.Load();
            if (tb25.TB83_PARAMETRO != null)
                tipo = tb25.TB83_PARAMETRO.TP_PERIOD_AVAL;

            AuxiliCarregamentos.CarregaMedidasTemporais(ddlTrimestre, false, tipo, false, true);
        }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        protected void btnSave_Click(object sender, EventArgs e)
        {

            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int materia = ddlDisciplina.SelectedValue != "" ? int.Parse(ddlDisciplina.SelectedValue) : 0;
            string anoRef = ddlAno.SelectedValue;
            string referencia = ddlTrimestre.SelectedValue;
            if (modalidade == 0 || serie == 0 || turma == 0 || materia == 0)
            {
                grdBusca.DataBind();
                return;
            }

            //--------> Varre toda a grid de Busca
            foreach (GridViewRow linha in grdBusca.Rows)
            {
                TB079_HIST_ALUNO tb079;

                int coAluno = int.Parse((((HiddenField)linha.Cells[7].FindControl("hidCoAtividade")).Value));

                tb079 = TB079_HIST_ALUNO.RetornaPelaChavePrimaria(coAluno, modalidade, serie, anoRef, materia);

                if (((CheckBox)linha.Cells[7].FindControl("ckSelect")).Checked)
                {
                    switch (referencia)
                    {
                        case "B1":
                            tb079.FL_HOMOL_NOTA_BIM1 = "S";
                            break;
                        case "B2":
                            tb079.FL_HOMOL_NOTA_BIM2 = "S";
                            break;
                        case "B3":
                            tb079.FL_HOMOL_NOTA_BIM3 = "S";
                            break;
                        case "B4":
                            tb079.FL_HOMOL_NOTA_BIM4 = "S";
                            break;
                        case "T1":
                            tb079.FL_HOMOL_NOTA_TRI1 = "S";
                            break;
                        case "T2":
                            tb079.FL_HOMOL_NOTA_TRI2 = "S";
                            break;
                        case "T3":
                            tb079.FL_HOMOL_NOTA_TRI3 = "S";
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (referencia)
                    {
                        case "B1":
                            tb079.FL_HOMOL_NOTA_BIM1 = "N";
                            break;
                        case "B2":
                            tb079.FL_HOMOL_NOTA_BIM2 = "N";
                            break;
                        case "B3":
                            tb079.FL_HOMOL_NOTA_BIM3 = "N";
                            break;
                        case "B4":
                            tb079.FL_HOMOL_NOTA_BIM4 = "N";
                            break;
                        case "T1":
                            tb079.FL_HOMOL_NOTA_TRI1 = "N";
                            break;
                        case "T2":
                            tb079.FL_HOMOL_NOTA_TRI2 = "N";
                            break;
                        case "T3":
                            tb079.FL_HOMOL_NOTA_TRI3 = "N";
                            break;
                        default:
                            break;
                    }
                }
                TB079_HIST_ALUNO.SaveOrUpdate(tb079, true);


            }

            AuxiliPagina.RedirecionaParaPaginaSucesso("Registros Salvos com sucesso.", Request.Url.AbsoluteUri);



            //var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

            //tb25.TB82_DTCT_EMPReference.Load();        
            //}                      

            //AuxiliPagina.RedirecionaParaPaginaSucesso("Registros Salvos com sucesso.", Request.Url.AbsoluteUri);
        }

        //private void SalvarNotasAvs(string TipoAvaliacao, decimal TipoNota, int coAlu, int materia, int modalidade, int serie, string anoRef, DateTime dataAtiv, TB273_TIPO_ATIVIDADE coTipoAtiv, int turma, string idNota)
        //{
        //    if (TipoNota != 0)
        //    {
        //        #region individual

        //        //------------> Recebe o dado da tabela de histórico de acordo com as chaves passadas
        //        TB49_NOTA_ATIV_ALUNO tb49 = (from iTb49 in TB49_NOTA_ATIV_ALUNO.RetornaTodosRegistros()
        //                                     where iTb49.TB07_ALUNO.CO_ALU == coAlu && iTb49.DT_NOTA_ATIV.Year == dataAtiv.Year
        //                                     && iTb49.DT_NOTA_ATIV.Month == dataAtiv.Month && iTb49.DT_NOTA_ATIV.Day == dataAtiv.Day
        //                                     && iTb49.TB273_TIPO_ATIVIDADE.ID_TIPO_ATIV == coTipoAtiv.ID_TIPO_ATIV
        //                                         //Na tabela TB49_NOTA_ATIV_ALUNO não existe a coluna CO_TRIMESTRE 
        //                                     && iTb49.CO_BIMESTRE == ddlReferencia.SelectedValue
        //                                     && iTb49.CO_REFER_NOTA == TipoAvaliacao
        //                                     && iTb49.TB107_CADMATERIAS.ID_MATERIA == materia
        //                                     select iTb49).FirstOrDefault();

        //        //TB49_NOTA_ATIV_ALUNO tb49;
        //        //if (!string.IsNullOrEmpty(idNota)) //Se for de um registro que exite, instancia o objeto da entidade para alteração
        //        //    tb49 = TB49_NOTA_ATIV_ALUNO.RetornaPelaChavePrimaria(int.Parse(idNota));
        //        //else //Se não existir, cria um novo objeto da entidade
        //        if (tb49 == null)
        //        {
        //            tb49 = new TB49_NOTA_ATIV_ALUNO();
        //            tb49.DT_NOTA_ATIV_CAD = DateTime.Now;
        //        }

        //        tb49.CO_BIMESTRE = ddlReferencia.SelectedValue;
        //        tb49.CO_SEMESTRE = ddlReferencia.SelectedValue == "B1" || ddlReferencia.SelectedValue == "B2" ? "1" : "2";
        //        tb49.CO_ANO = Convert.ToInt32(anoRef);
        //        tb49.TB01_CURSO = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie);
        //        tb49.TB06_TURMAS = TB06_TURMAS.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie, turma);
        //        tb49.TB07_ALUNO = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
        //        tb49.TB107_CADMATERIAS = TB107_CADMATERIAS.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, materia);
        //        tb49.CO_TIPO_ATIV = " ";
        //        tb49.CO_REFER_NOTA = TipoAvaliacao;
        //        tb49.TB273_TIPO_ATIVIDADE = coTipoAtiv;
        //        tb49.DT_NOTA_ATIV = dataAtiv;
        //        tb49.NO_NOTA_ATIV = coTipoAtiv.NO_TIPO_ATIV;
        //        tb49.VL_NOTA = TipoNota;
        //        tb49.FL_NOTA_ATIV = "S";
        //        tb49.FL_JUSTI_NOTA_ATIV = "N";
        //        tb49.FL_LANCA_NOTA = "S";
        //        tb49.CO_STATUS = "A";

        //        tb49.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_UNID_FUNC, LoginAuxili.CO_COL);

        //        if (tb49.EntityState != System.Data.EntityState.Unchanged && GestorEntities.SaveOrUpdate(tb49) < 1)
        //        {
        //            AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao salvar os itens");
        //            return;
        //        }

        //        #endregion
        //    }
        //    else
        //    {
        //        //------------> Recebe o dado da tabela de histórico de acordo com as chaves passadas
        //        TB49_NOTA_ATIV_ALUNO tb49 = (from iTb49 in TB49_NOTA_ATIV_ALUNO.RetornaTodosRegistros()
        //                                     where iTb49.TB07_ALUNO.CO_ALU == coAlu && iTb49.DT_NOTA_ATIV.Year == dataAtiv.Year
        //                                     && iTb49.DT_NOTA_ATIV.Month == dataAtiv.Month && iTb49.DT_NOTA_ATIV.Day == dataAtiv.Day
        //                                     && iTb49.TB273_TIPO_ATIVIDADE.ID_TIPO_ATIV == coTipoAtiv.ID_TIPO_ATIV
        //                                         //Na tabela TB49_NOTA_ATIV_ALUNO não existe a coluna CO_TRIMESTRE 
        //                                     && iTb49.CO_BIMESTRE == ddlReferencia.SelectedValue
        //                                     && iTb49.CO_REFER_NOTA == TipoAvaliacao
        //                                     && iTb49.TB107_CADMATERIAS.ID_MATERIA == materia
        //                                     select iTb49).FirstOrDefault();
        //        if (tb49 != null)
        //            TB49_NOTA_ATIV_ALUNO.Delete(tb49, true);
        //    }
        //}

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

        /// <summary>
        /// Método que carrega a grid
        /// </summary>
        private void CarregaGrid()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int materia = ddlDisciplina.SelectedValue != "" ? int.Parse(ddlDisciplina.SelectedValue) : 0;
            string trimestre = ddlTrimestre.SelectedValue;

            //int coTipoAtiv = ddlTipoAtiv.SelectedValue != "" ? int.Parse(ddlTipoAtiv.SelectedValue) : 0;
            string anoMesMat = ddlAno.SelectedValue;
            //DateTime dataAtiv = DateTime.Now;

            var materias = (from tb02 in TB02_MATERIA.RetornaTodosRegistros()
                            where tb02.CO_MAT == materia
                            select tb02).FirstOrDefault();

            if (materias.FL_TESTE_PROVA == "N")
            {
                grdBusca.Columns[1].Visible = false;
                grdBusca.Columns[2].Visible = false;
            }
            if (materias.FL_TRABA == "N")
            {
                grdBusca.Columns[3].Visible = false;
            }
            if (materias.FL_PROJE == "N")
            {
                grdBusca.Columns[4].Visible = false;
            }
            if (materias.FL_CONCE == "N")
            {
                grdBusca.Columns[5].Visible = false;
            }
            if (materias.FL_AVALI_ESPEC == "N")
            {
                grdBusca.Columns[6].Visible = false;
            }
            if (materias.FL_AVALI_GLOBA == "N")
            {
                grdBusca.Columns[7].Visible = false;
            }
            if (materias.FL_SIMUL == "N")
            {
                grdBusca.Columns[8].Visible = false;
            }
            if (materias.FL_ATIVI_AVALI == "N")
            {
                grdBusca.Columns[9].Visible = false;
            }
            if (materias.FL_ATIVI_PRATI == "N")
            {
                grdBusca.Columns[10].Visible = false;
            }
            if (materias.FL_REDAC == "N")
            {
                grdBusca.Columns[11].Visible = false;

            }

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
                                 MEDIA_HOMOLOGADA_R = (trimestre == "T1" ? tb079.FL_HOMOL_NOTA_TRI1 : trimestre == "T2" ? tb079.FL_HOMOL_NOTA_TRI2 : tb079.FL_HOMOL_NOTA_TRI3)
                             }).ToList();

            List<NotasAluno> resultadoCompleto = new List<NotasAluno>();

            foreach (var res in resultado)
            {

                //----------------------------------------------------------------------------------------------------------------------------------------------------------
                var tb49 = (from iTb49 in TB49_NOTA_ATIV_ALUNO.RetornaTodosRegistros()
                            where iTb49.TB07_ALUNO.CO_ALU == res.CO_ALU
                            && iTb49.CO_BIMESTRE == ddlTrimestre.SelectedValue
                            && iTb49.TB107_CADMATERIAS.ID_MATERIA == materias.ID_MATERIA
                            select iTb49).ToList();

                //----------------------------------------------------------------------------------------------------------------------------------------------------------



                foreach (var item in tb49)
                {
                    if (String.Equals(trimestre.Trim(), item.CO_REFER_NOTA.Trim()))
                    {
                        item.TB273_TIPO_ATIVIDADEReference.Load();

                        int id = item.TB273_TIPO_ATIVIDADE != null ? item.TB273_TIPO_ATIVIDADE.ID_TIPO_ATIV : 0;

                        var itb273 = (from tb273 in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros()
                                      where tb273.ID_TIPO_ATIV == id
                                      select
                                      tb273.CO_SIGLA_ATIV).FirstOrDefault();

                        List<String> sigla = new List<string>();
                        sigla.Add(itb273);

                        foreach (var notas in sigla)
                        {
                            switch (notas)
                            {
                                case "PR":
                                    if (item.CO_TIPO_ATIV.Trim() == "1")
                                    {
                                        res.NOTA_AV1 = item.VL_NOTA.ToString("N1");
                                        res.DATA_AV1 = item.DT_NOTA_ATIV;
                                        res.ID_NOTA_AV1 = item.ID_NOTA_ATIV;
                                    }
                                    else
                                    {
                                        res.NOTA_AV2 = item.VL_NOTA.ToString("N1");
                                        res.DATA_AV2 = item.DT_NOTA_ATIV;
                                        res.ID_NOTA_AV2 = item.ID_NOTA_ATIV;
                                    }
                                    break;
                                case "TB":
                                    res.NOTA_AV3 = item.VL_NOTA.ToString("N1");
                                    res.DATA_AV3 = item.DT_NOTA_ATIV;
                                    res.ID_NOTA_AV3 = item.ID_NOTA_ATIV;
                                    break;
                                case "PJ":
                                    res.NOTA_AV4 = item.VL_NOTA.ToString("N1");
                                    res.DATA_AV4 = item.DT_NOTA_ATIV;
                                    res.ID_NOTA_AV4 = item.ID_NOTA_ATIV;
                                    break;
                                case "CT":
                                    res.NOTA_AV5 = item.VL_NOTA.ToString("N1");
                                    res.DATA_AV5 = item.DT_NOTA_ATIV;
                                    res.ID_NOTA_AV5 = item.ID_NOTA_ATIV;
                                    break;
                                case "AE":
                                    res.NOTA_AV6 = item.VL_NOTA.ToString("N1");
                                    res.DATA_AV6 = item.DT_NOTA_ATIV;
                                    res.ID_NOTA_AV6 = item.ID_NOTA_ATIV;
                                    break;
                                case "AG":
                                    res.NOTA_AV7 = item.VL_NOTA.ToString("N1");
                                    res.DATA_AV7 = item.DT_NOTA_ATIV;
                                    res.ID_NOTA_AV7 = item.ID_NOTA_ATIV;
                                    break;
                                case "SI":
                                    res.NOTA_AV8 = item.VL_NOTA.ToString("N1");
                                    res.DATA_AV8 = item.DT_NOTA_ATIV;
                                    res.ID_NOTA_AV8 = item.ID_NOTA_ATIV;
                                    break;
                                case "AA":
                                    res.NOTA_AV9 = item.VL_NOTA.ToString("N1");
                                    res.DATA_AV9 = item.DT_NOTA_ATIV;
                                    res.ID_NOTA_AV9 = item.ID_NOTA_ATIV;
                                    break;
                                case "AP":
                                    res.NOTA_AV10 = item.VL_NOTA.ToString("N1");
                                    res.DATA_AV10 = item.DT_NOTA_ATIV;
                                    res.ID_NOTA_AV10 = item.ID_NOTA_ATIV;
                                    break;
                                case "RD":
                                    res.NOTA_AV11 = item.VL_NOTA.ToString("N1");
                                    res.DATA_AV11 = item.DT_NOTA_ATIV;
                                    res.ID_NOTA_AV11 = item.ID_NOTA_ATIV;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    res.MD = (Convert.ToDecimal(res.NOTA_AV1) + Convert.ToDecimal(res.NOTA_AV2) + Convert.ToDecimal(res.NOTA_AV3) + Convert.ToDecimal(res.NOTA_AV4) + Convert.ToDecimal(res.NOTA_AV5) + Convert.ToDecimal(res.NOTA_AV6) + Convert.ToDecimal(res.NOTA_AV7) + Convert.ToDecimal(res.NOTA_AV8) + Convert.ToDecimal(res.NOTA_AV9) + Convert.ToDecimal(res.NOTA_AV10) + Convert.ToDecimal(res.NOTA_AV11)) / (materias.VL_CALCU_MEDIA == null ? 1 : materias.VL_CALCU_MEDIA);

                }
                resultadoCompleto.Add(res);
            }

            grdBusca.DataKeyNames = new string[] { "CO_ALU" };

            grdBusca.DataSource = resultadoCompleto.Count() > 0 ? resultadoCompleto.OrderBy(p => p.NO_ALU) : null;
            grdBusca.DataBind();

            if (grdBusca.DataSource != null)
            {
                //liChk.Visible = true;

                #region Notas
                foreach (GridViewRow linha in grdBusca.Rows)
                {
                    TextBox txt1;
                    txt1 = (TextBox)linha.Cells[1].FindControl("txtNotaAv1");
                    txt1.Enabled = false;

                    TextBox txt2;
                    txt2 = (TextBox)linha.Cells[2].FindControl("txtNotaAv2");
                    txt2.Enabled = false;

                    TextBox txt3;
                    txt3 = (TextBox)linha.Cells[3].FindControl("txtNotaAv3");
                    txt3.Enabled = false;

                    TextBox txt4;
                    txt4 = (TextBox)linha.Cells[4].FindControl("txtNotaAv4");
                    txt4.Enabled = false;

                    TextBox txt5;
                    txt5 = (TextBox)linha.Cells[5].FindControl("txtNotaAv5");
                    txt5.Enabled = false;

                    TextBox txt6;
                    txt6 = (TextBox)linha.Cells[6].FindControl("txtNotaAv6");
                    txt6.Enabled = false;

                    TextBox txt7;
                    txt7 = (TextBox)linha.Cells[7].FindControl("txtNotaAv7");
                    txt7.Enabled = false;

                    TextBox txt8;
                    txt8 = (TextBox)linha.Cells[8].FindControl("txtNotaAv8");
                    txt8.Enabled = false;

                    TextBox txt9;
                    txt9 = (TextBox)linha.Cells[9].FindControl("txtNotaAv9");
                    txt9.Enabled = false;

                    TextBox txt10;
                    txt10 = (TextBox)linha.Cells[10].FindControl("txtNotaAv10");
                    txt10.Enabled = false;

                    TextBox txt11;
                    txt11 = (TextBox)linha.Cells[11].FindControl("txtNotaAv11");
                    txt11.Enabled = false;
                }
                #endregion

                #region Datas das Avs

                TextBox txtData1;
                txtData1 = (TextBox)grdBusca.HeaderRow.Cells[1].FindControl("txtDataAtiv1");
                txtData1.Enabled = false;

                TextBox txtData2;
                txtData2 = (TextBox)grdBusca.HeaderRow.Cells[2].FindControl("txtDataAtiv2");
                txtData2.Enabled = false;

                TextBox txtData3;
                txtData3 = (TextBox)grdBusca.HeaderRow.Cells[3].FindControl("txtDataAtiv3");
                txtData3.Enabled = false;

                TextBox txtData4;
                txtData4 = (TextBox)grdBusca.HeaderRow.Cells[4].FindControl("txtDataAtiv4");
                txtData4.Enabled = false;

                TextBox txtData5;
                txtData5 = (TextBox)grdBusca.HeaderRow.Cells[5].FindControl("txtDataAtiv5");
                txtData5.Enabled = false;

                TextBox txtData6;
                txtData6 = (TextBox)grdBusca.HeaderRow.Cells[6].FindControl("txtDataAtiv6");
                txtData6.Enabled = false;

                TextBox txtData7;
                txtData7 = (TextBox)grdBusca.HeaderRow.Cells[7].FindControl("txtDataAtiv7");
                txtData7.Enabled = false;

                TextBox txtData8;
                txtData8 = (TextBox)grdBusca.HeaderRow.Cells[8].FindControl("txtDataAtiv8");
                txtData8.Enabled = false;

                TextBox txtData9;
                txtData9 = (TextBox)grdBusca.HeaderRow.Cells[9].FindControl("txtDataAtiv9");
                txtData9.Enabled = false;

                TextBox txtData10;
                txtData10 = (TextBox)grdBusca.HeaderRow.Cells[10].FindControl("txtDataAtiv10");
                txtData10.Enabled = false;

                TextBox txtData11;
                txtData11 = (TextBox)grdBusca.HeaderRow.Cells[11].FindControl("txtDataAtiv11");
                txtData11.Enabled = false;
                #endregion

                //Ver se o nível de acesso do usuário para dar a premiação  de edição das avaliações 
                #region Verifica   usuário

                if (LoginAuxili.CLASSIFICACAO_USU_LOGADO == "M")
                {

                    return;
                }
                else if (LoginAuxili.CLASSIFICACAO_USU_LOGADO == "M" && LoginAuxili.FLA_PROFESSOR == "S")
                {
                    return;
                }
                else if (LoginAuxili.FLA_PROFESSOR == "S")
                {

                    foreach (GridViewRow linha in grdBusca.Rows)
                    {
                        //TextBox txt1;
                        //txt1 = (TextBox)linha.Cells[1].FindControl("txtNotaAv1");
                        //txt1.Enabled = false;

                        //TextBox txtData1;
                        //txtData1 = (TextBox)grdBusca.HeaderRow.Cells[1].FindControl("txtDataAtiv1");
                        //txtData1.Enabled = false;
                        ////-----------------------------------------------------------------------------------------------------------------------------------
                        //TextBox txt3;
                        //txt3 = (TextBox)linha.Cells[3].FindControl("txtNotaAv3");
                        //txt3.Enabled = false;

                        //TextBox txtData3;
                        //txtData3 = (TextBox)grdBusca.HeaderRow.Cells[4].FindControl("txtDataAtiv3");
                        //txtData3.Enabled = false;
                        //-----------------------------------------------------------------------------------------------------------------------------------

                    }
                }
                else
                {
                    foreach (GridViewRow linha in grdBusca.Rows)
                    {

                        //#region Notas
                        //TextBox txt1;
                        //txt1 = (TextBox)linha.Cells[1].FindControl("txtNotaAv1");
                        //txt1.Enabled = false;

                        //TextBox txt2;
                        //txt2 = (TextBox)linha.Cells[2].FindControl("txtNotaAv2");
                        //txt2.Enabled = false;

                        //TextBox txt3;
                        //txt3 = (TextBox)linha.Cells[3].FindControl("txtNotaAv3");
                        //txt3.Enabled = false;

                        //TextBox txt4;
                        //txt4 = (TextBox)linha.Cells[4].FindControl("txtNotaAv4");
                        //txt4.Enabled = false;

                        //TextBox txt5;
                        //txt5 = (TextBox)linha.Cells[5].FindControl("txtNotaAv5");
                        //txt5.Enabled = false;

                        //TextBox txt6;
                        //txt6 = (TextBox)linha.Cells[6].FindControl("txtNotaAv6");
                        //txt6.Enabled = false;

                        //TextBox txt7;
                        //txt7 = (TextBox)linha.Cells[7].FindControl("txtNotaAv7");
                        //txt7.Enabled = false;

                        //TextBox txt8;
                        //txt8 = (TextBox)linha.Cells[8].FindControl("txtNotaAv8");
                        //txt8.Enabled = false;

                        //TextBox txt9;
                        //txt9 = (TextBox)linha.Cells[9].FindControl("txtNotaAv9");
                        //txt9.Enabled = false;

                        //TextBox txt10;
                        //txt10 = (TextBox)linha.Cells[10].FindControl("txtNotaAv10");
                        //txt10.Enabled = false;

                        //TextBox txt11;
                        //txt11 = (TextBox)linha.Cells[11].FindControl("txtNotaAv11");
                        //txt11.Enabled = false;
                        //#endregion

                        //#region Datas das Avs

                        //TextBox txtData1;
                        //txtData1 = (TextBox)grdBusca.HeaderRow.Cells[1].FindControl("txtDataAtiv1");
                        //txtData1.Enabled = false;

                        //TextBox txtData2;
                        //txtData2 = (TextBox)grdBusca.HeaderRow.Cells[2].FindControl("txtDataAtiv2");
                        //txtData2.Enabled = false;

                        //TextBox txtData3;
                        //txtData3 = (TextBox)grdBusca.HeaderRow.Cells[3].FindControl("txtDataAtiv3");
                        //txtData3.Enabled = false;

                        //TextBox txtData4;
                        //txtData4 = (TextBox)grdBusca.HeaderRow.Cells[4].FindControl("txtDataAtiv4");
                        //txtData4.Enabled = false;

                        //TextBox txtData5;
                        //txtData5 = (TextBox)grdBusca.HeaderRow.Cells[5].FindControl("txtDataAtiv5");
                        //txtData5.Enabled = false;

                        //TextBox txtData6;
                        //txtData6 = (TextBox)grdBusca.HeaderRow.Cells[6].FindControl("txtDataAtiv6");
                        //txtData6.Enabled = false;

                        //TextBox txtData7;
                        //txtData7 = (TextBox)grdBusca.HeaderRow.Cells[7].FindControl("txtDataAtiv7");
                        //txtData7.Enabled = false;

                        //TextBox txtData8;
                        //txtData8 = (TextBox)grdBusca.HeaderRow.Cells[8].FindControl("txtDataAtiv8");
                        //txtData8.Enabled = false;

                        //TextBox txtData9;
                        //txtData9 = (TextBox)grdBusca.HeaderRow.Cells[9].FindControl("txtDataAtiv9");
                        //txtData9.Enabled = false;

                        //TextBox txtData10;
                        //txtData10 = (TextBox)grdBusca.HeaderRow.Cells[10].FindControl("txtDataAtiv10");
                        //txtData10.Enabled = false;

                        //TextBox txtData11;
                        //txtData11 = (TextBox)grdBusca.HeaderRow.Cells[11].FindControl("txtDataAtiv11");
                        //txtData11.Enabled = false;
                        //#endregion

                    }
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

        /// <summary>
        /// Homologa  todos que não estão homologados menos os que não tem média 
        /// </summary>
        protected void ChkHomologaTodos_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = ((CheckBox)sender);
            foreach (GridViewRow row in grdBusca.Rows)
            {                   
                if (chk.Checked)
                {
                    SelecionarTodos(true);
                }
                else
                {
                    SelecionarTodos(false);
                }
            }
        }

        private void SelecionarTodos(bool t)
        {

            if (t)
            {
                foreach (GridViewRow linha in grdBusca.Rows)
                {
                    string coMB = (((HiddenField)linha.FindControl("HiddenMB")).Value);
                    CheckBox chkN = ((CheckBox)linha.FindControl("ckSelect"));
                    if (coMB == "")
                    {                        
                        chkN.Checked = false;
                    }
                    else
                    {
                        chkN.Checked = true;
                    }
                }
            }
            else
            {
                foreach (GridViewRow linha in grdBusca.Rows)
                {
                    CheckBox chkN = ((CheckBox)linha.FindControl("ckSelect"));
                    chkN.Checked = false;
                }
            }
        }

        /// <summary>
        /// Vejo se ah média caso não (não homologa) 
        /// </summary>

        private void ChkAMedia()
        {
            foreach (GridViewRow linha in grdBusca.Rows)
            {
                if (((CheckBox)linha.Cells[8].FindControl("ckSelect")).Checked)
                {
                    string coMB = (((HiddenField)linha.Cells[8].FindControl("HiddenMB")).Value);
                    if (coMB == "")
                    {

                        CheckBox chks = ((CheckBox)linha.Cells[8].FindControl("ckSelect"));
                        chks.Checked = false;
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Não a média  cadastrada !! Por isso não e possível realizar está operação ");
                    }
                    else
                    {

                        CheckBox chkN = ((CheckBox)linha.Cells[8].FindControl("ckSelect"));
                        chkN.Checked = true;
                    }
                }
            }
        }

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

        protected void ckSelect_SelectedIndexChanged(object sender, EventArgs e)
        {

            ChkAMedia();

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

            //NotasAluno notasAluno = new NotasAluno();
            //foreach (GridViewRow linha in grdBusca.Rows)
            //{
            //    if (notasAluno.MEDIA_HOMOLOGADA)
            //    {
            //        CheckBox chkN = ((CheckBox)linha.Cells[7].FindControl("ckSelect"));
            //        chkN.Checked = true;
            //    }
            //}
        }

        protected void ddlTrimestre_OnSelectedIndexChanged(object sender, EventArgs e)
        {
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
                    case "T":
                        if (ddlTrimestre.SelectedValue == "T1")
                        {
                            if (tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI1 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI1 != null)
                            {
                                if (!(tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI1 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI1 >= dataLacto))
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Nota do 1º Trimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                                    return;
                                }
                            }
                        }
                        else if (ddlTrimestre.SelectedValue == "T2")
                        {
                            if (tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI2 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI2 != null)
                            {
                                if (!(tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI2 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI2 >= dataLacto))
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Nota do 2º Trimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                                    return;
                                }
                            }
                        }
                        else
                        {
                            if (tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI3 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI3 != null)
                            {
                                if (!(tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI3 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI3 >= dataLacto))
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Nota do 3º Trimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                                    return;
                                }
                            }
                        }
                        break;

                    case "B":
                        if (ddlTrimestre.SelectedValue == "B1")
                        {
                            if (tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM1 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM1 != null)
                            {
                                if (!(tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM1 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM1 >= dataLacto))
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Nota do 1º Bimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                                    return;
                                }
                            }
                        }
                        else if (ddlTrimestre.SelectedValue == "B2")
                        {
                            if (tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM2 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM2 != null)
                            {
                                if (!(tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM2 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM2 >= dataLacto))
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Nota do 2º Bimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                                    return;
                                }
                            }
                        }
                        else if (ddlTrimestre.SelectedValue == "B3")
                        {
                            if (tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM3 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM3 != null)
                            {
                                if (!(tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM3 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM3 >= dataLacto))
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Nota do 3º Bimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                                    return;
                                }
                            }
                        }
                        else
                        {
                            if (tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM4 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM4 != null)
                            {
                                if (!(tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM4 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM4 >= dataLacto))
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Nota do 4º Bimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                                    return;
                                }
                            }
                        }
                        break;
                }
            }
        }

        protected void ddlReferencia_OnSelectedIndexChanged(object sender, EventArgs e)
        {
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
                    case "T":
                        if (ddlTrimestre.SelectedValue == "T1")
                        {
                            if (tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI1 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI1 != null)
                            {
                                if (!(tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI1 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI1 >= dataLacto))
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Nota do 1º Trimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                                    return;
                                }
                            }
                        }
                        else if (ddlTrimestre.SelectedValue == "T2")
                        {
                            if (tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI2 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI2 != null)
                            {
                                if (!(tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI2 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI2 >= dataLacto))
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Nota do 2º Trimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                                    return;
                                }
                            }
                        }
                        else
                        {
                            if (tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI3 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI3 != null)
                            {
                                if (!(tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI3 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI3 >= dataLacto))
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Nota do 3º Trimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                                    return;
                                }
                            }
                        }
                        break;

                    case "B":
                        if (ddlTrimestre.SelectedValue == "B1")
                        {
                            if (tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM1 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM1 != null)
                            {
                                if (!(tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM1 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM1 >= dataLacto))
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Nota do 1º Bimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                                    return;
                                }
                            }
                        }
                        else if (ddlTrimestre.SelectedValue == "B2")
                        {
                            if (tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM2 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM2 != null)
                            {
                                if (!(tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM2 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM2 >= dataLacto))
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Nota do 2º Bimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                                    return;
                                }
                            }
                        }
                        else if (ddlTrimestre.SelectedValue == "B3")
                        {
                            if (tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM3 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM3 != null)
                            {
                                if (!(tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM3 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM3 >= dataLacto))
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Nota do 3º Bimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                                    return;
                                }
                            }
                        }
                        else
                        {
                            if (tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM4 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM4 != null)
                            {
                                if (!(tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM4 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM4 >= dataLacto))
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Nota do 4º Bimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                                    return;
                                }
                            }
                        }
                        break;
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
            public string NOTA_AV6 { get; set; }
            public DateTime? DATA_AV6 { get; set; }
            public string NOTA_AV7 { get; set; }
            public DateTime? DATA_AV7 { get; set; }
            public string NOTA_AV8 { get; set; }
            public DateTime? DATA_AV8 { get; set; }
            public string NOTA_AV9 { get; set; }
            public DateTime? DATA_AV9 { get; set; }
            public string NOTA_AV10 { get; set; }
            public DateTime? DATA_AV10 { get; set; }
            public string NOTA_AV11 { get; set; }
            public DateTime? DATA_AV11 { get; set; }
            public int NU_NIRE { get; set; }
            public int CO_ALU { get; set; }
            public string MEDIA_HOMOLOGADA_R { get; set; }
            public bool MEDIA_HOMOLOGADA
            {
                get
                {
                    //Se a média estiver homologada, retorna false para desabiltar o campo, caso contrário, habilita
                    return (this.MEDIA_HOMOLOGADA_R == "S" ? true : false);
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
            public int ID_NOTA_AV6 { get; set; }
            public int ID_NOTA_AV7 { get; set; }
            public int ID_NOTA_AV8 { get; set; }
            public int ID_NOTA_AV9 { get; set; }
            public int ID_NOTA_AV10 { get; set; }
            public int ID_NOTA_AV11 { get; set; }

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
                    txtData.Enabled = false;
                }

                #endregion

                #region Data 2

                string data2 = ((HiddenField)e.Row.Cells[2].FindControl("hidDtAV2")).Value;
                if (!string.IsNullOrEmpty(data2))
                {
                    TextBox txtData = ((TextBox)grdBusca.HeaderRow.Cells[2].FindControl("txtDataAtiv2"));
                    txtData.Text = data2;
                    txtData.Enabled = false;
                }

                #endregion

                #region Data 3

                string data3 = ((HiddenField)e.Row.Cells[3].FindControl("hidDtAV3")).Value;
                if (!string.IsNullOrEmpty(data3))
                {
                    TextBox txtData = ((TextBox)grdBusca.HeaderRow.Cells[3].FindControl("txtDataAtiv3"));
                    txtData.Text = data3;
                    txtData.Enabled = false;
                }

                #endregion

                #region Data 4

                string data4 = ((HiddenField)e.Row.Cells[4].FindControl("hidDtAV4")).Value;
                if (!string.IsNullOrEmpty(data4))
                {
                    TextBox txtData = ((TextBox)grdBusca.HeaderRow.Cells[4].FindControl("txtDataAtiv4"));
                    TextBox txtNota = ((TextBox)grdBusca.HeaderRow.Cells[4].FindControl("txtNotaAv4"));
                    txtData.Text = data4;
                    txtData.Enabled = false;
                }

                #endregion

                #region Data 5

                string data5 = ((HiddenField)e.Row.Cells[5].FindControl("hidDtAV5")).Value;
                if (!string.IsNullOrEmpty(data5))
                {
                    TextBox txtData = ((TextBox)grdBusca.HeaderRow.Cells[5].FindControl("txtDataAtiv5"));
                    txtData.Text = data5;
                    txtData.Enabled = false;
                }

                #endregion

                #region Data 6

                string data6 = ((HiddenField)e.Row.Cells[6].FindControl("hidDtAV6")).Value;
                if (!string.IsNullOrEmpty(data6))
                {
                    TextBox txtData = ((TextBox)grdBusca.HeaderRow.Cells[6].FindControl("txtDataAtiv6"));
                    txtData.Text = data6;
                    txtData.Enabled = false;
                }

                #endregion

                #region Data 7

                string data7 = ((HiddenField)e.Row.Cells[7].FindControl("hidDtAV7")).Value;
                if (!string.IsNullOrEmpty(data7))
                {
                    TextBox txtData = ((TextBox)grdBusca.HeaderRow.Cells[7].FindControl("txtDataAtiv7"));
                    txtData.Text = data7;
                }

                #endregion

                #region Data 8

                string data8 = ((HiddenField)e.Row.Cells[8].FindControl("hidDtAV8")).Value;
                if (!string.IsNullOrEmpty(data8))
                {
                    TextBox txtData = ((TextBox)grdBusca.HeaderRow.Cells[8].FindControl("txtDataAtiv8"));
                    txtData.Text = data8;
                }

                #endregion

                #region Data 9

                string data9 = ((HiddenField)e.Row.Cells[9].FindControl("hidDtAV9")).Value;
                if (!string.IsNullOrEmpty(data9))
                {
                    TextBox txtData = ((TextBox)grdBusca.HeaderRow.Cells[9].FindControl("txtDataAtiv9"));
                    txtData.Text = data9;
                }

                #endregion

                #region Data 10

                string data10 = ((HiddenField)e.Row.Cells[10].FindControl("hidDtAV10")).Value;
                if (!string.IsNullOrEmpty(data10))
                {
                    TextBox txtData = ((TextBox)grdBusca.HeaderRow.Cells[10].FindControl("txtDataAtiv10"));
                    txtData.Text = data10;
                }

                #endregion

                #region Data 11

                string data11 = ((HiddenField)e.Row.Cells[11].FindControl("hidDtAV11")).Value;
                if (!string.IsNullOrEmpty(data11))
                {
                    TextBox txtData = ((TextBox)grdBusca.HeaderRow.Cells[11].FindControl("txtDataAtiv11"));
                    txtData.Text = data11;
                }

                #endregion
            }
        }

        #endregion
    }
}