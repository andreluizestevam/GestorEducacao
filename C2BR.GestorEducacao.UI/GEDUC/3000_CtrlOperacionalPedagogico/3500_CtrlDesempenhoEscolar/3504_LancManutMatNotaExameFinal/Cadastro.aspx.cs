//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE DE AVALIAÇÃO ESCOLAR DO ALUNO
// OBJETIVO: LANÇAMENTO E MANUTENÇÃO, POR MATÉRIA, DE NOTA DE EXAME FINAL DO ALUNO
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 02/07/2013| André Nobre Vinagre        | Colocado o tratamento para aceitar o valor zero
//           |                            |

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using System.Drawing;
using System.Reflection;
using System.Data.Objects;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3500_CtrlDesempenhoEscolar;
//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3500_CtrlDesempenhoEscolar.F3504_LancManutMatNotaExameFinal
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
            CarregaSerieCurso();
            CarregaTurma();
            divGrid.Visible = false;
        }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int materia = ddlDisciplina.SelectedValue != "" ? int.Parse(ddlDisciplina.SelectedValue) : 0;
            string anoRef = ddlAno.SelectedValue;

            if (modalidade == 0 || serie == 0 || turma == 0 || materia == 0)
            {
                grdBusca.DataBind();
                return;
            }

            //--------> Varre toda a grid de Busca
            foreach (GridViewRow linha in grdBusca.Rows)
            {
                //------------> Recebe o código do aluno
                int coAlu = Convert.ToInt32(grdBusca.DataKeys[linha.RowIndex].Values[0]);

                //------------> Recebe o dado da tabela de histórico de acordo com as chaves passadas
                TB079_HIST_ALUNO tb079 = TB079_HIST_ALUNO.RetornaPelaChavePrimaria(coAlu, modalidade, serie, anoRef, materia);

                if (tb079 == null)
                {
                    var refAluno = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                    refAluno.TB25_EMPRESA1Reference.Load();
                    refAluno.TB25_EMPRESAReference.Load();
                    tb079.CO_EMP = refAluno.TB25_EMPRESA.CO_EMP;
                    tb079.CO_ALU = refAluno.CO_ALU;
                    tb079.TB25_EMPRESA = refAluno.TB25_EMPRESA1;
                    tb079.CO_ANO_REF = anoRef;
                    tb079.CO_MODU_CUR = modalidade;
                    tb079.TB44_MODULO = TB44_MODULO.RetornaPelaChavePrimaria(tb079.CO_MODU_CUR);
                    tb079.CO_CUR = serie;
                    tb079.CO_MAT = materia;
                }

                

                decimal dcmBim1, dcmBim2, dcmBim3, dcmBim4, dcmTri1, dcmTri2, dcmTri3,dcmMediaAnual, dcmNotaProvaFinal;
                
                ///<summary>
                ///Verificação Bimestral
                ///</summary>
                //------------> Faz a verificação para saber se dado digitado para a média do 1º Bimestre é válido 
                if (decimal.TryParse(((Label)linha.Cells[2].FindControl("txtMB1")).Text, out dcmBim1))
                {
                    //----------------> Média deve estar entre 0 e 10
                    if (dcmBim1 < 0 || dcmBim1 > 100)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "MB1 deve estar entre 0 e 10");
                        return;
                    }

                    tb079.VL_NOTA_BIM1 = (decimal?) dcmBim1;
                }

                //------------> Faz a verificação para saber se dado digitado para a média do 2º Bimestre é válido
                if (decimal.TryParse(((Label)linha.Cells[3].FindControl("txtMB2")).Text, out dcmBim2))
                {
                    //----------------> Média deve estar entre 0 e 10
                    if (dcmBim2 < 0 || dcmBim2 > 100)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "MB2 deve estar entre 0 e 10");
                        return;
                    }

                    tb079.VL_NOTA_BIM2 = (decimal?) dcmBim2;
                }

                //------------> Faz a verificação para saber se dado digitado para a média do 3º Bimestre é válido
                if (decimal.TryParse(((Label)linha.Cells[4].FindControl("txtMB3")).Text, out dcmBim3))
                {
                    //----------------> Média deve estar entre 0 e 10
                    if (dcmBim3 < 0 || dcmBim3 > 100)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "MB3 deve estar entre 0 e 10");
                        return;
                    }

                    tb079.VL_NOTA_BIM3 = (decimal?) dcmBim3;
                }

                //------------> Faz a verificação para saber se dado digitado para a média do 4º Bimestre é válido
                if (decimal.TryParse(((Label)linha.Cells[5].FindControl("txtMB4")).Text, out dcmBim4))
                {
                    //----------------> Média deve estar entre 0 e 10
                    if (dcmBim4 < 0 || dcmBim4 > 100)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "MB4 deve estar entre 0 e 10");
                        return;
                    }

                    tb079.VL_NOTA_BIM4 = (decimal?) dcmBim4;
                }

                ///<summary>
                ///Verificação Trimestral
                ///</summary>
                //------------> Faz a verificação para saber se dado digitado para a média do 1º Trimestre é válido
                if (decimal.TryParse(((Label)linha.Cells[6].FindControl("txtMT1")).Text, out dcmTri1))
                {
                    //----------------> Média deve estar entre 0 e 10
                    if (dcmTri1 < 0 || dcmTri1 > 100)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "MT1 deve estar entre 0 e 10");
                        return;
                    }

                    tb079.VL_NOTA_TRI1 = (decimal?)dcmTri1;
                }

                //------------> Faz a verificação para saber se dado digitado para a média do 2º Trimestre é válido
                if (decimal.TryParse(((Label)linha.Cells[7].FindControl("txtMT2")).Text, out dcmTri2))
                {
                    //----------------> Média deve estar entre 0 e 10
                    if (dcmTri2 < 0 || dcmTri2 > 100)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "MT2 deve estar entre 0 e 10");
                        return;
                    }

                    tb079.VL_NOTA_TRI2 = (decimal?)dcmTri2;
                }

                //------------> Faz a verificação para saber se dado digitado para a média do 3º Trimestre é válido
                if (decimal.TryParse(((Label)linha.Cells[8].FindControl("txtMT3")).Text, out dcmTri3))
                {
                    //----------------> Média deve estar entre 0 e 10
                    if (dcmTri3 < 0 || dcmTri3 > 100)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "MT3 deve estar entre 0 e 10");
                        return;
                    }

                    tb079.VL_NOTA_TRI3 = (decimal?)dcmTri3;
                }

                //------------> Faz a verificação para saber se dado digitado para a média do 3º Trimestre é válido
                if (decimal.TryParse(((Label)linha.Cells[10].FindControl("txtST")).Text, out dcmMediaAnual))
                {
                    tb079.VL_MEDIA_ANUAL = (decimal?)dcmMediaAnual;
                }

                // Verifica se foi digitado um valor válido para prova final
                //------------> Faz a verificação para saber se dado digitado para a prova final é válido
                if (((TextBox)linha.Cells[12].FindControl("txtProvaFinal")).Enabled && decimal.TryParse(((TextBox)linha.Cells[11].FindControl("txtProvaFinal")).Text, out dcmNotaProvaFinal))
                {
                    //----------------> Média deve estar entre 0 e 10
                    if (dcmNotaProvaFinal < 0 || dcmNotaProvaFinal > 100)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Prova Final deve estar entre 0 e 10");
                        return;
                    }

                    tb079.VL_PROVA_FINAL = (decimal?)dcmNotaProvaFinal;
                }

                if (tb079.EntityState != System.Data.EntityState.Unchanged && GestorEntities.SaveOrUpdate(tb079) < 1)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao salvar os itens");
                    return;
                }
                TB079_HIST_ALUNO.SaveOrUpdate(tb079);
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
            string anoMesMat = ddlAno.SelectedValue;

            if (modalidade == 0 || serie == 0 || turma == 0 || materia == 0)
            {
                grdBusca.DataBind();
                return;
            }

            divGrid.Visible = ligrid.Visible = true;

            //Verificação da referência utilizada pela empresa 
            var tipo = "B";

            var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

            tb25.TB83_PARAMETROReference.Load();
            if (tb25.TB83_PARAMETRO != null)
                tipo = tb25.TB83_PARAMETRO.TP_PERIOD_AVAL;

            if (tipo == "B")
            {
                grdBusca.Columns[6].Visible = false;
                grdBusca.Columns[7].Visible = false;
                grdBusca.Columns[8].Visible = false;
                grdBusca.Columns[10].Visible = false;
                lblLegenBim.Visible = true;
            }
            if (tipo == "T")
            {
                grdBusca.Columns[2].Visible = false;
                grdBusca.Columns[3].Visible = false;
                grdBusca.Columns[4].Visible = false;
                grdBusca.Columns[5].Visible = false;
                grdBusca.Columns[9].Visible = false;
                lblLegenTri.Visible = true;
            }


            decimal dcmMediaCurso, dcmMediaRecuperacao;

            // Carrega os parâmetros da instituição para recuperar o campo de média final do curso
            TB149_PARAM_INSTI tb149 = TB149_PARAM_INSTI.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
            TB83_PARAMETRO tb83 = null;
            TB44_MODULO tb44 = null;

            if (tb149.TP_CTRLE_AVAL == TipoControle.I.ToString())
            {
                dcmMediaCurso = tb149.VL_MEDIA_CURSO != null ? tb149.VL_MEDIA_CURSO.Value : 0;
                dcmMediaRecuperacao = tb149.VL_MEDIA_RECUPER != null ? tb149.VL_MEDIA_RECUPER.Value : 0;
            }
            else if (tb149.TP_CTRLE_AVAL == TipoControle.U.ToString())
            {
                if (tb83 == null)
                    tb83 = TB83_PARAMETRO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

                dcmMediaCurso = tb83.VL_MEDIA_CURSO != null ? tb83.VL_MEDIA_CURSO.Value : 0;
                dcmMediaRecuperacao = tb83.VL_MEDIA_RECUPER != null ? tb83.VL_MEDIA_RECUPER.Value : 0;
            }
            else
            {
                if (tb44 == null)
                    tb44 = TB44_MODULO.RetornaPelaChavePrimaria(modalidade);

                dcmMediaCurso = tb44.VL_MEDIA_CURSO != null ? tb44.VL_MEDIA_CURSO.Value : 0;
                dcmMediaRecuperacao = tb44.VL_MEDIA_RECUPER != null ? tb44.VL_MEDIA_RECUPER.Value : 0;
            }


            var resultado = (from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                             join tb08 in TB08_MATRCUR.RetornaTodosRegistros() on tb079.CO_ALU equals tb08.CO_ALU
                             where tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP && tb08.CO_ANO_MES_MAT == anoMesMat
                             && tb08.CO_CUR == serie && tb08.CO_TUR == turma && tb079.CO_MAT == materia
                             select new Saida
                             {
                                 CO_EMP = tb079.CO_EMP,
                                 CO_ALU = tb079.CO_ALU,
                                 NO_ALU = tb08.TB07_ALUNO.NO_ALU,
                                 CO_ANO_REF = tb079.CO_ANO_REF,
                                 CO_MAT = tb079.CO_MAT,
                                 CO_CUR = tb079.CO_CUR,
                                 CO_ALU_CAD_R = tb08.CO_ALU_CAD.Insert(2, ".").Insert(6, "."),
                                 NU_NIRE = tb08.TB07_ALUNO.NU_NIRE,
                                 VL_MEDIA_BIM1 = tb079.VL_MEDIA_BIM1,
                                 VL_MEDIA_BIM2 = tb079.VL_MEDIA_BIM2,
                                 VL_MEDIA_BIM3 = tb079.VL_MEDIA_BIM3,
                                 VL_MEDIA_BIM4 = tb079.VL_MEDIA_BIM4,
                                 VL_MEDIA_TRI1 = tb079.VL_MEDIA_TRI1,
                                 VL_MEDIA_TRI2 = tb079.VL_MEDIA_TRI2,
                                 VL_MEDIA_TRI3 = tb079.VL_MEDIA_TRI3,
                                 FL_HOMOL_NOTA_BIM1 = tb079.FL_HOMOL_NOTA_BIM1,
                                 FL_HOMOL_NOTA_BIM2 = tb079.FL_HOMOL_NOTA_BIM2,
                                 FL_HOMOL_NOTA_BIM3 = tb079.FL_HOMOL_NOTA_BIM3,
                                 FL_HOMOL_NOTA_BIM4 = tb079.FL_HOMOL_NOTA_BIM4,
                                 FL_HOMOL_NOTA_TRI1 = tb079.FL_HOMOL_NOTA_TRI1,
                                 FL_HOMOL_NOTA_TRI2 = tb079.FL_HOMOL_NOTA_TRI2,
                                 FL_HOMOL_NOTA_TRI3 = tb079.FL_HOMOL_NOTA_TRI3,
                                 VL_PROVA_FINAL = tb079.VL_PROVA_FINAL,
                                 CO_STA_APROV_MATERIA_R = tb079.CO_STA_APROV_MATERIA,
                                 CO_SIT_MAT = tb08.CO_SIT_MAT,
                             }).OrderBy(w => w.NO_ALU).ToList();

            if (resultado.Count() > 0)
            {
                // Habilita o botão de salvar
            }

            grdBusca.DataKeyNames = new string[] { "CO_ALU" };

            grdBusca.DataSource = resultado.Count() > 0 ? resultado : null;
            grdBusca.DataBind();

            DestacaAlunoTransferido();
        }

        public class Saida
        {
            //Dados do Aluno
            public int CO_ALU { get; set; }
            public string NO_ALU { get; set; }
            public int NU_NIRE { get; set; }
            public bool ENABLED_V
            {
                get
                {
                    return this.CO_STA_APROV_MATERIA_R == "P" ? true : false;
                }
            }
            public bool ENABLED
            {
                get
                {
                    return this.CO_SIT_MAT == "A" && this.ENABLED_V == true ? true : this.CO_STA_APROV_MATERIA == "P" ? false : false;
                }
            }
            public string TIPO 
            { 
                get 
                {
                    //Verificação da referência utilizada pela empresa 
                    var tipo = "B";

                    var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

                    tb25.TB83_PARAMETROReference.Load();
                    if (tb25.TB83_PARAMETRO != null)
                        tipo = tb25.TB83_PARAMETRO.TP_PERIOD_AVAL;

                    return tipo;
                } 
            }

            //Dados de Matrícula
            public string CO_ALU_CAD_R { get; set; }
            public string CO_ALU_CAD
            {
                get
                {
                    return this.NU_NIRE.ToString().PadLeft(7, '0') + " - " + this.DESC_CO_SIT_MAT;
                }
            }
            public string CO_ANO_REF { get; set; }
            public int CO_CUR { get; set; }
            public int CO_MAT { get; set; }
            public string CO_STA_APROV_MATERIA_R { get; set; }
            public string CO_STA_APROV_MATERIA
            {
                get
                {
                    //Forma que era feito anteriormente, mostrando a situação de acordo com o informado no histórico do aluno
                    //return (this.CO_STA_APROV_MATERIA_R == "A" ? "Aprovado" : this.CO_STA_APROV_MATERIA_R == "R" ? "Reprovado" : this.CO_STA_APROV_MATERIA_R == "P" ? "Prova Final" : "Pendente");

                    //Verificação da referência utilizada pela empresa 
                    var tipo = "B";

                    var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

                    tb25.TB83_PARAMETROReference.Load();
                    if (tb25.TB83_PARAMETRO != null)
                        tipo = tb25.TB83_PARAMETRO.TP_PERIOD_AVAL;

                    //Verifica o tipo da referência: Trimestral
                    #region Trimestral
                    if (tipo == "T")
                    {
                        bool pende = false;

                        if ((this.VL_MEDIA_TRI1.HasValue) && (this.FL_HOMOL_NOTA_TRI1 != "S"))
                            pende = true;
                        else if ((this.VL_MEDIA_TRI2.HasValue) && (this.FL_HOMOL_NOTA_TRI2 != "S"))
                            pende = true;
                        else if ((this.VL_MEDIA_TRI3.HasValue) && (this.FL_HOMOL_NOTA_TRI3 != "S"))
                            pende = true;

                        DateTime? dtIniLanc1 = TB82_DTCT_EMP.RetornaPelaEmpresa(this.CO_EMP).DT_LACTO_INICI_TRI1;
                        DateTime? dtIniLanc2 = TB82_DTCT_EMP.RetornaPelaEmpresa(this.CO_EMP).DT_LACTO_INICI_TRI2;
                        DateTime? dtIniLanc3 = TB82_DTCT_EMP.RetornaPelaEmpresa(this.CO_EMP).DT_LACTO_INICI_TRI3;

                        //Verifica se já iniciou o período de lançamento e caso ainda não exista nota coloca o status Em Aberto
                        bool emAberto = false;
                        int i = 0;
                        DateTime dt = FormataData(DateTime.Now);
                        if (dtIniLanc1.HasValue)
                        {
                            if (dt >= dtIniLanc1.Value)
                            {
                                if (this.VL_MEDIA_TRI1.HasValue)
                                    i++;
                                else
                                    emAberto = true;
                            }
                        }
                        if (dtIniLanc2.HasValue)
                        {
                            if (dt >= dtIniLanc2.Value)
                            {
                                if (this.VL_MEDIA_TRI2.HasValue)
                                    i++;
                                else
                                    emAberto = true;
                            }
                        }
                        if (dtIniLanc3.HasValue)
                        {
                            if (dt >= dtIniLanc3.Value)
                            {
                                if (this.VL_MEDIA_TRI3.HasValue)
                                    i++;
                                else
                                    emAberto = true;
                            }
                        }
                        
                        if (emAberto == false)
                            return (pende == false ? "Homologada" : "Pendente");
                        else
                            return "Em Aberto";
                    }
                    #endregion

                    //Caso a verificação seja falsa, seta o valor padrão: Bimestral
                    #region Bimestral
                    else
                    {
                        bool pende = false;

                        if ((this.VL_MEDIA_BIM1.HasValue) && (this.FL_HOMOL_NOTA_BIM1 != "S"))
                            pende = true;
                        else if ((this.VL_MEDIA_BIM2.HasValue) && (this.FL_HOMOL_NOTA_BIM2 != "S"))
                            pende = true;
                        else if ((this.VL_MEDIA_BIM3.HasValue) && (this.FL_HOMOL_NOTA_BIM3 != "S"))
                            pende = true;
                        else if ((this.VL_MEDIA_BIM4.HasValue) && (this.FL_HOMOL_NOTA_BIM4 != "S"))
                            pende = true;

                        DateTime? dtIniLanc1 = TB82_DTCT_EMP.RetornaPelaEmpresa(this.CO_EMP).DT_LACTO_INICI_BIM1;
                        DateTime? dtIniLanc2 = TB82_DTCT_EMP.RetornaPelaEmpresa(this.CO_EMP).DT_LACTO_INICI_BIM2;
                        DateTime? dtIniLanc3 = TB82_DTCT_EMP.RetornaPelaEmpresa(this.CO_EMP).DT_LACTO_INICI_BIM3;
                        DateTime? dtIniLanc4 = TB82_DTCT_EMP.RetornaPelaEmpresa(this.CO_EMP).DT_LACTO_INICI_BIM4;

                        //Verifica se já iniciou o período de lançamento e caso ainda não exista nota coloca o status Em Aberto
                        bool emAberto = false;
                        int i = 0;
                        DateTime dt = FormataData(DateTime.Now);
                        if (dtIniLanc1.HasValue)
                        {
                            if (dt >= dtIniLanc1.Value)
                            {
                                if (this.VL_MEDIA_BIM1.HasValue)
                                    i++;
                                else
                                    emAberto = true;
                            }
                        }
                        if (dtIniLanc2.HasValue)
                        {
                            if (dt >= dtIniLanc2.Value)
                            {
                                if (this.VL_MEDIA_BIM2.HasValue)
                                    i++;
                                else
                                    emAberto = true;
                            }
                        }
                        if (dtIniLanc3.HasValue)
                        {
                            if (dt >= dtIniLanc3.Value)
                            {
                                if (this.VL_MEDIA_BIM3.HasValue)
                                    i++;
                                else
                                    emAberto = true;
                            }
                        }
                        if (dtIniLanc4.HasValue)
                        {
                            if (dt >= dtIniLanc4.Value)
                            {
                                if (this.VL_MEDIA_BIM4.HasValue)
                                    i++;
                                else
                                    emAberto = true;
                            }
                        }

                        if (emAberto == false)
                            return (pende == false ? "Homologada" : "Pendente");
                        else
                            return "Em Aberto";
                    }
                    #endregion
                }
            }
            public string CO_STA_APROV { get; set; }
            public string STATUS
            {
                get
                {
                    return this.CO_STA_APROV == "A" ? "Aprovado" : this.CO_STA_APROV == "R" ? "Reprovado" : "";
                }
            }
            public string CO_SIT_MAT { get; set; }
            public string DESC_CO_SIT_MAT
            {
                get
                {
                    return this.CO_SIT_MAT == "A" ? "Matriculado" : this.CO_SIT_MAT == "X" ? "Transferido" : this.CO_SIT_MAT == "F" ? "Finalizado" : this.CO_SIT_MAT == "T" ? "Trancado" : this.CO_SIT_MAT == "C" ? "Cancelado" : "Pendente";
                }
            }
            public int CO_EMP { get; set; }

            //Dados de Notas
            public decimal? VL_MEDIA_BIM1 { get; set; }
            public decimal? VL_MEDIA_BIM2 { get; set; }
            public decimal? VL_MEDIA_BIM3 { get; set; }
            public decimal? VL_MEDIA_BIM4 { get; set; }

            public decimal? VL_MEDIA_TRI1 { get; set; }
            public decimal? VL_MEDIA_TRI2 { get; set; }
            public decimal? VL_MEDIA_TRI3 { get; set; }

            //Trata as notas Bimestrais
            public string MB1
            {
                get
                {
                    return (this.VL_MEDIA_BIM1.HasValue ? this.VL_MEDIA_BIM1.Value.ToString("N2") : " - ");
                }
            }
            public string MB2
            {
                get
                {
                    return (this.VL_MEDIA_BIM2.HasValue ? this.VL_MEDIA_BIM2.Value.ToString("N2") : " - ");
                }
            }
            public string MB3
            {
                get
                {
                    return (this.VL_MEDIA_BIM3.HasValue ? this.VL_MEDIA_BIM3.Value.ToString("N2") : " - ");
                }
            }
            public string MB4
            {
                get
                {
                    return (this.VL_MEDIA_BIM4.HasValue ? this.VL_MEDIA_BIM4.Value.ToString("N2") : " - ");
                }
            }

            //Trata notas Trimestrais
            public string MT1
            {
                get
                {
                    return (this.VL_MEDIA_TRI1.HasValue ? this.VL_MEDIA_TRI1.Value.ToString("N2") : " - ");
                }
            }
            public string MT2
            {
                get
                {
                    return (this.VL_MEDIA_TRI2.HasValue ? this.VL_MEDIA_TRI2.Value.ToString("N2") : " - ");
                }
            }
            public string MT3
            {
                get
                {
                    return (this.VL_MEDIA_TRI3.HasValue ? this.VL_MEDIA_TRI3.Value.ToString("N2") : " - ");
                }
            }

            public string FL_HOMOL_NOTA_BIM1 { get; set; }
            public string FL_HOMOL_NOTA_BIM2 { get; set; }
            public string FL_HOMOL_NOTA_BIM3 { get; set; }
            public string FL_HOMOL_NOTA_BIM4 { get; set; }

            public string FL_HOMOL_NOTA_TRI1 { get; set; }
            public string FL_HOMOL_NOTA_TRI2 { get; set; }
            public string FL_HOMOL_NOTA_TRI3 { get; set; }

            //Mostra asterisco ao lado da nota caso não esteja homologada Bimestral
            public bool FL_HOMOL_VISIBLE_1
            {
                get
                {
                    return (this.VL_MEDIA_BIM1.HasValue ? (FL_HOMOL_NOTA_BIM1 == "S" ? false : true) : false);
                }
            }
            public bool FL_HOMOL_VISIBLE_2
            {
                get
                {
                    return (this.VL_MEDIA_BIM2.HasValue ? (FL_HOMOL_NOTA_BIM2 == "S" ? false : true) : false);
                }
            }
            public bool FL_HOMOL_VISIBLE_3
            {
                get
                {
                    return (this.VL_MEDIA_BIM3.HasValue ? (FL_HOMOL_NOTA_BIM3 == "S" ? false : true) : false);
                }
            }
            public bool FL_HOMOL_VISIBLE_4
            {
                get
                {
                    return (this.VL_MEDIA_BIM4.HasValue ? (FL_HOMOL_NOTA_BIM4 == "S" ? false : true) : false);
                }
            }

            //Mostra asterisco ao lado da nota caso não esteja homologada Trimestral
            public bool FL_HOMOL_VISIBLE_5
            {
                get
                {
                    return (this.VL_MEDIA_TRI1.HasValue ? (FL_HOMOL_NOTA_TRI1 == "S" ? false : true) : false);
                }
            }
            public bool FL_HOMOL_VISIBLE_6
            {
                get
                {
                    return (this.VL_MEDIA_TRI2.HasValue ? (FL_HOMOL_NOTA_TRI2 == "S" ? false : true) : false);
                }
            }
            public bool FL_HOMOL_VISIBLE_7
            {
                get
                {
                    return (this.VL_MEDIA_TRI3.HasValue ? (FL_HOMOL_NOTA_TRI3 == "S" ? false : true) : false);
                }
            }

            public decimal? VL_PROVA_FINAL { get; set; }
            public string VL_PROVA_FINAL_V
            {
                get
                {
                    return this.VL_PROVA_FINAL == null && this.ENABLED == false ? "*****" : this.VL_PROVA_FINAL == null && this.ENABLED == true ? null : this.VL_PROVA_FINAL.Value.ToString("0.00");
                }
            }


            public string VL_MEDIA_FINAL
            {
                get
                {
                    //Calcula a Síntese anual dos bimestres
                    int coun = 0;

                    //Verificação da referência utilizada pela empresa 
                    var tipo = "B";

                    var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

                    tb25.TB83_PARAMETROReference.Load();
                    if (tb25.TB83_PARAMETRO != null)
                        tipo = tb25.TB83_PARAMETRO.TP_PERIOD_AVAL;

                    decimal notaTotal = 0;

                    if (tipo == "T")
                    {
                        if (FL_HOMOL_NOTA_TRI1 == "S")
                        {
                            coun++;
                            notaTotal += (this.VL_MEDIA_TRI1.HasValue ? this.VL_MEDIA_TRI1.Value : 0);
                        }
                        if (FL_HOMOL_NOTA_TRI2 == "S")
                        {
                            coun++;
                            notaTotal += (this.VL_MEDIA_TRI2.HasValue ? this.VL_MEDIA_TRI2.Value : 0);
                        }
                        if (FL_HOMOL_NOTA_TRI3 == "S")
                        {
                            coun++;
                            notaTotal += (this.VL_MEDIA_TRI3.HasValue ? this.VL_MEDIA_TRI3.Value : 0);
                        }
                    }
                    else
                    {   
                        if (FL_HOMOL_NOTA_BIM1 == "S")
                        {
                            coun++;
                            notaTotal += (this.VL_MEDIA_BIM1.HasValue ? this.VL_MEDIA_BIM1.Value : 0);
                        }
                        if (FL_HOMOL_NOTA_BIM2 == "S")
                        {
                            coun++;
                            notaTotal += (this.VL_MEDIA_BIM2.HasValue ? this.VL_MEDIA_BIM2.Value : 0);
                        }
                        if (FL_HOMOL_NOTA_BIM3 == "S")
                        {
                            coun++;
                            notaTotal += (this.VL_MEDIA_BIM3.HasValue ? this.VL_MEDIA_BIM3.Value : 0);
                        }
                        if (FL_HOMOL_NOTA_BIM4 == "S")
                        {
                            coun++;
                            notaTotal += (this.VL_MEDIA_BIM4.HasValue ? this.VL_MEDIA_BIM4.Value : 0);
                        }
                    }
                    if (coun > 0)
                    {
                        decimal final = 0;
                        final = notaTotal / coun;
                        return final.ToString("N2");
                    }
                    else
                        return "*****";
                }
            }
        }

        public static DateTime FormataData(DateTime dt)
        {
            string dtFormat = dt.ToString().Substring(0, 10);
            return DateTime.Parse(dtFormat);
        }

        /// <summary>
        /// Método responsável por destacar a linha do aluno que está com status de transferido
        /// </summary>
        private void DestacaAlunoTransferido()
        {
            foreach (GridViewRow li in grdBusca.Rows)
            {
                string hd = (((HiddenField)li.Cells[2].FindControl("hidSituAlu")).Value);
                if (hd == "X")
                {
                    li.BackColor = System.Drawing.Color.FromArgb(245, 222, 179);
                }
            }
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
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, false);
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            string anoGrade = ddlAno.SelectedValue;
            AuxiliCarregamentos.carregaSeriesGradeCurso(ddlSerieCurso, modalidade, anoGrade, LoginAuxili.CO_EMP, false);
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaTurmas(ddlTurma, LoginAuxili.CO_EMP, modalidade, serie, false);
        }

        /// <summary>
        /// Método que carrega o dropdown de Disciplinas
        /// </summary>
        private void CarregaDisciplina()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string anoGrade = ddlAno.SelectedValue;
            AuxiliCarregamentos.CarregaMateriasGradeCurso(ddlDisciplina, LoginAuxili.CO_EMP, modalidade, serie, anoGrade, false);
        }
        #endregion

        protected void lnkImprime_OnClick(object sender, EventArgs e)
        {
            int coEmp = LoginAuxili.CO_EMP;
            int lRetorno;
            string parametros = "";
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int materia = ddlDisciplina.SelectedValue != "" ? int.Parse(ddlDisciplina.SelectedValue) : 0;
            string anoMesMat = ddlAno.SelectedValue;
            string infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            string nomefunc = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GEDUC/3000_CtrlOperacionalPedagogico/3500_CtrlDesempenhoEscolar/3504_LancManutMatNotaExameFinal/Cadastro.aspx");
            string noDisciplina = "";

            if (materia != 0)
            {
                noDisciplina = (from tb02 in TB02_MATERIA.RetornaTodosRegistros()
                                join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                where tb02.CO_MAT == materia
                                select tb107.NO_MATERIA).FirstOrDefault();
            }
            else
                noDisciplina = "Todos";

            parametros = "( Ano: " + ddlAno.SelectedValue;
            parametros += " - Modalidade: " + (modalidade != 0 ? TB44_MODULO.RetornaPelaChavePrimaria(modalidade).DE_MODU_CUR : "Todos");
            parametros += " - Curso: " + (serie != 0 ? TB01_CURSO.RetornaTodosRegistros().Where(p => p.CO_CUR == serie).FirstOrDefault().NO_CUR : "Todos");
            parametros += " - Turma: " + (turma != 0 ? TB129_CADTURMAS.RetornaPelaChavePrimaria(turma).NO_TURMA : "Todos");
            parametros += " - Disciplina: " + noDisciplina + " )";

            RptConsultaMediasLetivas fpcb = new RptConsultaMediasLetivas();
            lRetorno = fpcb.InitReport(parametros, LoginAuxili.CO_EMP, infos, LoginAuxili.CO_EMP, anoMesMat, serie, turma, materia, modalidade, nomefunc);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.AbreNovaJanela(this, Session["URLRelatorio"].ToString());
            //----------------> Limpa a var de sessão com o url do relatório.
            Session.Remove("URLRelatorio");

            //----------------> Limpa a ref da url utilizada para carregar o relatório.
            PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
            isreadonly.SetValue(this.Request.QueryString, false, null);
            isreadonly.SetValue(this.Request.QueryString, true, null);
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaDisciplina();
        }

        protected void lbkPesq_OnClick(object sender, EventArgs e)
        {
            CarregaGrid();
        }

        protected void grdBusca_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtProvaFinal = (TextBox)e.Row.FindControl("txtProvaFinal");

                if (txtProvaFinal.Enabled == true)
                    e.Row.ControlStyle.BackColor = System.Drawing.Color.FromArgb(251, 233, 183);
            }
        }
    }
}