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
// 10/09/2014| Maxwell Almeida            | Criada a funcionalidade


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
using System.Web;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3500_CtrlDesempenhoEscolar._3507_CalculoMediaEspecificoSem2
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

            bool reca = CarregaParametros();

            if (reca == false)
            {
                CarregaAnos();
                CarregaModalidades();
                CarregaSerieCurso();
                CarregaTurma();
                CarregaAluno();
                CarregaDisciplina();
            }
            divGrid.Visible = false;
        }

        private bool CarregaParametros()
        {
            bool persist = false;
            if (HttpContext.Current.Session["BuscaSuperior"] != null)
            {
                var parametros = HttpContext.Current.Session["BuscaSuperior"].ToString();

                if (!string.IsNullOrEmpty(parametros))
                {
                    var par = parametros.ToString().Split(';');
                    var ano = par[0];
                    var modalidade = par[1];
                    var serieCurso = par[2];
                    var turma = par[3];
                    var coBime = par[4];
                    var Aluno = par[5];
                    var materia = par[6];

                    CarregaAnos();
                    ddlAno.SelectedValue = ano;

                    CarregaModalidades();
                    ddlModalidade.SelectedValue = modalidade;

                    CarregaSerieCurso();
                    ddlSerieCurso.SelectedValue = serieCurso;

                    CarregaTurma();
                    ddlTurma.SelectedValue = turma;

                    ddlBimestre.SelectedValue = coBime;

                    CarregaAluno();
                    ddlAluno.SelectedValue = Aluno;

                    CarregaDisciplina();
                    ddlDisciplina.SelectedValue = materia;

                    persist = true;
                    HttpContext.Current.Session.Remove("BuscaSuperior");
                }
            }
            return persist;
        }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool flgOcoMedia = false;

            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int intBimestre = ddlBimestre.SelectedValue == "B1" ? 1 : ddlBimestre.SelectedValue == "B2" ? 2 : ddlBimestre.SelectedValue == "B3" ? 3 : 4;
            string anoRef = ddlAno.SelectedValue;

            if (modalidade == 0 || serie == 0 || turma == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Modalidade, Série e Turma devem ser informadas.");
                return;
            }

            foreach (GridViewRow linha in grdBusca.Rows)
            {
                //Verifica se existiu ocorrência de nota
                if (((TextBox)linha.Cells[7].FindControl("txtMedBim")).Text != " - ")
                {
                    flgOcoMedia = true;
                }
            }

            if (!flgOcoMedia)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Nenhuma média foi calculada");
                return;
            }

            var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

            tb25.TB82_DTCT_EMPReference.Load();

            DateTime dataLacto = DateTime.Now.Date;
            if (tb25.TB82_DTCT_EMP != null)
            {
                if (intBimestre == 1)
                {
                    if (tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM1 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM1 != null)
                    {
                        if (!(tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM1 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM1 >= dataLacto))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Média do 1º Bimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                            return;
                        }
                    }
                }
                else if (intBimestre == 2)
                {
                    if (tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM2 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM2 != null)
                    {
                        if (!(tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM2 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM2 >= dataLacto))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Média do 2º Bimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                            return;
                        }
                    }
                }
                else if (intBimestre == 3)
                {
                    if (tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM3 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM3 != null)
                    {
                        if (!(tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM3 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM3 >= dataLacto))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Média do 3º Bimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
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
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Média do 4º Bimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                            return;
                        }
                    }
                }
            }

            //--------> Varre toda a grid de Busca
            foreach (GridViewRow linha in grdBusca.Rows)
            {
                if (((TextBox)linha.Cells[7].FindControl("txtMedBim")).Text != " - ")
                {
                    //------------> Recebe o código do aluno
                    int coAlu = Convert.ToInt32(grdBusca.DataKeys[linha.RowIndex].Values[0]);
                    int coMat = int.Parse(((HiddenField)linha.Cells[4].FindControl("hidCoMat")).Value);

                    //------------> Recebe o dado da tabela de histórico de acordo com as chaves passadas
                    TB079_HIST_ALUNO tb079 = (from iTb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                                              where iTb079.CO_ALU == coAlu && iTb079.CO_ANO_REF == ddlAno.SelectedValue
                                                 && iTb079.TB44_MODULO.CO_MODU_CUR == modalidade
                                                 && iTb079.CO_CUR == serie && iTb079.CO_TUR == turma
                                                 && iTb079.CO_MAT == coMat
                                              select iTb079).FirstOrDefault();

                    if (tb079 != null)
                    {
                        bool LancMediSemes = false;
                        if (((TextBox)linha.Cells[7].FindControl("txtMedSem")).Text != " - ")
                            LancMediSemes = true;

                        //----------------> Atribui o valor de média informado de acordo com o bimestre
                        switch (intBimestre)
                        {
                            case 1:
                                tb079.VL_MEDIA_BIM1 = decimal.Parse(((TextBox)linha.Cells[7].FindControl("txtMedBim")).Text);
                                tb079.FL_HOMOL_NOTA_BIM1 = "S";

                                //tb079.VL_NOTA_BIM1_ORI = decimal.Parse(((TextBox)linha.Cells[7].FindControl("txtMedia")).Text);
                                break;
                            case 2:
                                tb079.VL_MEDIA_BIM2 = decimal.Parse(((TextBox)linha.Cells[7].FindControl("txtMedBim")).Text);
                                tb079.FL_HOMOL_NOTA_BIM2 = "S";

                                //Salva a média do semstre caso esta tenha sido calculada
                                if (LancMediSemes == true)
                                {
                                    tb079.VL_NOTA_SEM1 = decimal.Parse(((TextBox)linha.Cells[7].FindControl("txtMedSem")).Text);
                                    tb079.FL_HOMOL_NOTA_SEM1 = "S";
                                }

                                //Implementado para viabilizar o lançamento de uma nota de recuperação no segundo bimestre que se propara para o primeiro, para o Colégio específico
                                //apenas no primeiro semestre do ano 2014
                                if (DateTime.Now.Year.ToString() == "2014")
                                {
                                    string vlnt1Stri = (((HiddenField)linha.Cells[7].FindControl("hidNotaBim1")).Value);
                                    decimal vlnt1 = ((!string.IsNullOrEmpty(vlnt1Stri)) && (vlnt1Stri != " - ") ? decimal.Parse(vlnt1Stri) : 0);
                                    
                                    if((!string.IsNullOrEmpty(vlnt1Stri) && (vlnt1Stri != " - ")))
                                    {
                                        tb079.VL_MEDIA_BIM1 = vlnt1;
                                        tb079.FL_HOMOL_NOTA_BIM1 = "S";
                                    }
                                }

                                break;
                            case 3:
                                tb079.VL_MEDIA_BIM3 = decimal.Parse(((TextBox)linha.Cells[7].FindControl("txtMedBim")).Text);
                                tb079.FL_HOMOL_NOTA_BIM3 = "S";
                                break;
                            case 4:
                                tb079.VL_MEDIA_BIM4 = decimal.Parse(((TextBox)linha.Cells[7].FindControl("txtMedBim")).Text);
                                tb079.FL_HOMOL_NOTA_BIM4 = "S";

                                //Salva a média do semestre caso esta tenha sido calculada
                                if (LancMediSemes == true)
                                {
                                    tb079.VL_NOTA_SEM2 = decimal.Parse(((TextBox)linha.Cells[7].FindControl("txtMedSem")).Text);
                                    tb079.FL_HOMOL_NOTA_SEM2 = "S";
                                }
                                break;
                        }

                        if (tb079.EntityState != System.Data.EntityState.Unchanged && GestorEntities.SaveOrUpdate(tb079) < 1)
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao salvar os itens");
                            return;
                        }
                    }
                }
            }

            //Persiste as informações inseridas nos parâmetros para serem colocadas novamente depois que tudo for salvo, facilitando no momento de calcular diversas médias.
            var parametros = ddlAno.SelectedValue + ";" + ddlModalidade.SelectedValue + ";" + ddlSerieCurso.SelectedValue + ";" + ddlTurma.SelectedValue + ";"
                      + ddlBimestre.SelectedValue + ";" + ddlAluno.SelectedValue + ";" + ddlDisciplina.SelectedValue;
            HttpContext.Current.Session["BuscaSuperior"] = parametros;

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
            int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;
            string anoMesMat = ddlAno.SelectedValue;
            int intAno = int.Parse(ddlAno.SelectedValue.Trim());
            decimal baseMedia = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie).MED_FINAL_CUR ?? 0;

            //int idMateria = (from tb02 in TB02_MATERIA.RetornaTodosRegistros()
            //                 where tb02.CO_MAT == materia
            //                 select new { tb02.ID_MATERIA }).First().ID_MATERIA;

            divGrid.Visible = true;

            var resultado = (from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                             join tb08 in TB08_MATRCUR.RetornaTodosRegistros() on tb079.CO_ALU equals tb08.CO_ALU
                             join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb079.CO_MAT equals tb02.CO_MAT
                             join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                             join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on tb079.CO_MAT equals tb43.CO_MAT
                             where tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP && tb08.CO_ANO_MES_MAT == anoMesMat
                             && tb43.CO_EMP == tb079.CO_EMP
                             && tb43.CO_MAT == tb079.CO_MAT
                             && tb43.CO_CUR == tb079.CO_CUR
                             && tb43.CO_ANO_GRADE == tb079.CO_ANO_REF
                             && tb079.CO_ANO_REF == anoMesMat
                             && tb079.CO_MODU_CUR == modalidade
                             && tb08.CO_CUR == serie && tb08.CO_TUR == turma 
                             && (coAlu != 0 ? tb08.CO_ALU == coAlu : 0 == 0)
                             && (materia != 0 ? tb079.CO_MAT == materia : 0 == 0)
                                //Verifica se foi selecionada a opção de gerar a grid com as matérias agrupadoras ou não
                             && (chkCalculAgrupadora.Checked ? tb43.ID_MATER_AGRUP == null && tb08.CO_SIT_MAT == "A" : tb08.CO_SIT_MAT == "A")
                             select new NotasAluno
                             {
                                 FL_AGRUPADORA = tb43.FL_DISCI_AGRUPA,
                                 ID_AGRUP_MATER = tb43.ID_MATER_AGRUP,
                                 ordImp = tb43.CO_ORDEM_IMPRE ?? 20,
                                 CO_ALU = tb079.CO_ALU,
                                 NO_ALU_RECEB = tb08.TB07_ALUNO.NO_ALU.ToUpper(),
                                 NU_NIRE = tb08.TB07_ALUNO.NU_NIRE,
                                 CO_MAT = tb079.CO_MAT,
                                 NTBIM = tb079.VL_RECU_BIM1,
                                 NTAuxBim1 = tb079.VL_NOTA_BIM1,
                                 NTAuxBim2 = tb079.VL_NOTA_BIM2,
                                 NTAuxBim3 = tb079.VL_NOTA_BIM3,
                                 NTAuxBim4 = tb079.VL_NOTA_BIM4,
                                 NTAuxRecp1 = tb079.VL_RECU_BIM1,
                                 NTAuxRecp2 = tb079.VL_RECU_BIM2,
                                 NTAuxRecp3 = tb079.VL_RECU_BIM3,
                                 NTAuxRecp4 = tb079.VL_RECU_BIM4,
                                 MEDBIM = tb079.VL_MEDIA_BIM1,
                                 NTAuxMedSem1 = tb079.VL_NOTA_SEM1,
                                 NTAuxMedSem2 = tb079.VL_NOTA_SEM2,
                                 NTMediaBim1 = tb079.VL_MEDIA_BIM1,
                                 NTMediaBim2 = tb079.VL_MEDIA_BIM2,
                                 NTMediaBim3 = tb079.VL_MEDIA_BIM3,
                                 NTMediaBim4 = tb079.VL_MEDIA_BIM4,
                                 noMateria = tb107.NO_MATERIA,
                                 baseMedia = baseMedia,
                                 //coMateria = tb079.CO_MAT,
                             }).OrderBy(w => w.NO_ALU_RECEB).ThenBy(o => o.ordImp).ThenBy(y => y.noMateria).ToList();

            //Loop para mostrar as notas de acordo com o bimestre selecionado
            foreach (var li in resultado)
            {
                switch (ddlBimestre.SelectedValue)
                {
                    case "B1":
                        li.NTBIM = li.NTAuxBim1;
                        li.NTRECUP = li.NTAuxRecp1;
                        li.MEDSEM = (li.NTAuxMedSem1.HasValue ? li.NTAuxMedSem1.Value : (decimal?)null);

                        //Calcula a média do bimestre
                        if (li.NTAuxRecp1.HasValue)
                        {
                            li.MEDBIM = (li.NTAuxBim1.HasValue ? (li.NTAuxRecp1.Value > li.NTAuxBim1.Value ? li.NTAuxRecp1.Value : li.NTAuxBim1.Value) : li.NTAuxRecp1.Value);
                        }
                        else
                            li.MEDBIM = li.NTAuxBim1.HasValue ? li.NTAuxBim1 : null;

                        li.MEDSEM = null;

                        break;
                    case "B2":
                        li.NTBIM = li.NTAuxBim2;
                        li.NTRECUP = li.NTAuxRecp2;
                        li.MEDSEM = li.NTAuxMedSem1;

                        //Calcula a média do bimestre
                        if (li.NTAuxRecp2.HasValue)
                        {
                            //Verifica se a nota do bimestre2 existe, caso exista verifica se ela é abaixo da média, caso seja, é verificado se a nota de recuperação supera a nota do 2bimestre, caso seja ela é quem vale. Caso a nota do bimestre seja
                            //superior a média é ela quem vale. Caso não haja nota do bimestre é a nota de recuperação quem vale
                            li.MEDBIM = (li.NTAuxBim2.HasValue ? (li.NTAuxBim2.Value < li.baseMedia ? (li.NTAuxRecp2.Value > li.NTAuxBim2.Value ? li.NTAuxRecp2.Value : li.NTAuxBim2.Value) : li.NTAuxBim2.Value) : li.NTAuxRecp2.Value);
                        }
                        else
                            li.MEDBIM = li.NTAuxBim2.HasValue ? li.NTAuxBim2 : null;

                        //Implementado para viabilizar o lançamento de uma nota de recuperação no segundo bimestre que se propara para o primeiro, para o Colégio específico
                        //apenas no primeiro semestre do ano 2014
                        if (DateTime.Now.Year.ToString() == "2014")
                        {
                            if (li.NTAuxRecp2.HasValue)
                            {
                                //Verifica se a nota do bimestre1 existe, caso exista verifica se ela é abaixo da média, caso seja, é verificado se a nota de recuperação supera a nota do 1bimestre, caso seja ela é quem vale. Caso a nota do bimestre seja
                                //superior a média é ela quem vale. Caso não haja nota do bimestre é a nota de recuperação quem vale
                                li.hdNotab1Aux = (li.NTAuxBim1.HasValue ? (li.NTAuxBim1.Value < li.baseMedia ? (li.NTAuxRecp2.Value > li.NTAuxBim1.Value ? li.NTAuxRecp2.Value : li.NTAuxBim1.Value) : li.NTAuxBim1) : li.NTAuxRecp2.Value);
                            }
                            else
                                li.hdNotab1Aux = li.NTAuxBim1.HasValue ? li.NTAuxBim1 : null;
                        }

                        //decimal auxm1s1 = (li.NTMediaBim1.HasValue ? li.NTMediaBim1.Value : 0);
                        decimal auxm1s1 = (li.hdNotab1Aux.HasValue ? li.hdNotab1Aux.Value : 0);
                        decimal auxm2s1 = (li.MEDBIM.HasValue ? li.MEDBIM.Value : 0);
                        decimal auxsoms1 = auxm1s1 + auxm2s1;
                        li.MEDSEM = auxsoms1 / 2;

                        break;
                    case "B3":
                        li.NTBIM = li.NTAuxBim3;
                        li.NTRECUP = li.NTAuxRecp3;
                        li.MEDSEM = (li.NTAuxMedSem2.HasValue ? li.NTAuxMedSem2.Value : (decimal?)null);

                        //Calcula a média do bimestre
                        if (li.NTAuxRecp3.HasValue)
                        {
                            li.MEDBIM = (li.NTAuxBim3.HasValue ? (li.NTAuxRecp3.Value > li.NTAuxBim3.Value ? li.NTAuxRecp3.Value : li.NTAuxBim3.Value) : li.NTAuxRecp3.Value);
                        }
                        else
                            li.MEDBIM = li.NTAuxBim3.HasValue ? li.NTAuxBim3 : null;

                        li.MEDSEM = null;

                        break;
                    case "B4":
                        li.NTBIM = li.NTAuxBim4;
                        li.NTRECUP = li.NTAuxRecp4;
                        li.MEDSEM = li.NTAuxMedSem2;

                        //Calcula a média do bimestre
                        if (li.NTAuxRecp4.HasValue)
                        {
                            li.MEDBIM = (li.NTAuxBim4.HasValue ? (li.NTAuxRecp4.Value > li.NTAuxBim4.Value ? li.NTAuxRecp4.Value : li.NTAuxBim4.Value) : li.NTAuxRecp4.Value);
                        }
                        else
                            li.MEDBIM = li.NTAuxBim4.HasValue ? li.NTAuxBim4 : null;

                        decimal auxm1s2 = (li.NTMediaBim3.HasValue ? li.NTMediaBim3.Value : 0);
                        decimal auxm2s2 = (li.MEDBIM.HasValue ? li.MEDBIM.Value : 0);
                        decimal auxsoms2 = auxm1s2 + auxm2s2;
                        li.MEDSEM = auxsoms2 / 2;

                        break;
                }
            }

            divGrid.Visible = true;

            grdBusca.DataKeyNames = new string[] { "CO_ALU" };

            grdBusca.DataSource = resultado.Count() > 0 ? resultado.OrderBy(p => p.NO_ALU) : null;
            grdBusca.DataBind();

            VerificaAgrupadora();
        }

        /// <summary>
        /// Método responsável por verificar se a disciplina é agrupadora, e se for fazer a soma entre as associadas
        /// </summary>
        private void VerificaAgrupadora()
        {
            if (chkCalculAgrupadora.Checked == false)
            {
                int coMatAgrupa = 0;
                //Percorre cada linha da grid verificando se é a linha de uma disciplina agrupadora
                foreach (GridViewRow li in grdBusca.Rows)
                {
                    //Disciplina agrupadora
                    if ((((HiddenField)li.Cells[4].FindControl("hidAgrupadora")).Value) == "S")
                    {
                        coMatAgrupa = int.Parse((((HiddenField)li.Cells[4].FindControl("hidCoMat")).Value));
                        int coAlu = int.Parse((((HiddenField)li.Cells[4].FindControl("hidCoAlu")).Value));
                        //int coAlu = int.Parse((((HiddenField)li.Cells[4].FindControl("hidCoMat")).Value));

                        //Percorre a grid procurando por registros de disciplinas que sejam agrupadas pelo código citado acima
                        decimal total = 0;
                        decimal notaSemestr = 0;
                        foreach (GridViewRow ro in grdBusca.Rows)
                        {
                            string coMat = (((HiddenField)ro.Cells[4].FindControl("hidCoMatAgrupadora")).Value);
                            int coMatI = (!string.IsNullOrEmpty(coMat) ? int.Parse(coMat) : 0);

                            string coAluStr = (((HiddenField)ro.Cells[4].FindControl("hidCoAlu")).Value);
                            int coAluIn = (!string.IsNullOrEmpty(coAluStr) ? int.Parse(coAluStr) : 0);
                            
                            //Verifica se o aluno da linha é o mesmo do aluno em contexto, para não misturar notas
                            if (coAluIn == coAlu)
                            {
                                if (coMatI == coMatAgrupa)
                                {
                                    string vlnota = (((TextBox)ro.Cells[4].FindControl("txtMedBim")).Text);
                                    decimal vlNotaDec = (vlnota != " - " ? decimal.Parse(vlnota) : 0);
                                    total += vlNotaDec;
                                }
                            }
                        }

                        //Contabiliza a média semestral da matéria em questão
                        switch (ddlBimestre.SelectedValue)
                        {
                            case "B2":
                                string vlnotab1Aux = (((HiddenField)li.Cells[4].FindControl("hidNtMDB1")).Value);
                                decimal vlnotab1Aux_Valid = (!string.IsNullOrEmpty(vlnotab1Aux) ? decimal.Parse(vlnotab1Aux) : 0);
                                decimal som = vlnotab1Aux_Valid + total;
                                decimal div = som / 2;
                                notaSemestr = div;
                                break;

                            case "B4":
                                string vlnotab1Auxb4 = (((HiddenField)li.Cells[4].FindControl("hidNtMDB1")).Value);
                                decimal vlnotab1Aux_Validb4 = (!string.IsNullOrEmpty(vlnotab1Auxb4) ? decimal.Parse(vlnotab1Auxb4) : 0);
                                decimal somb4 = vlnotab1Aux_Validb4 + total;
                                decimal divb4 = somb4 / 2;
                                notaSemestr = divb4;
                                break;
                        }

                        (((TextBox)li.Cells[6].FindControl("txtMedBim")).Text) = 
                        (((TextBox)li.Cells[4].FindControl("txtNota1")).Text) = total.ToString();

                        (((TextBox)li.Cells[7].FindControl("txtMedSem")).Text) = notaSemestr.ToString();
                    }
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

        private void CarregaAluno()
        {
            ddlAluno.Items.Clear();

            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;

            AuxiliCarregamentos.CarregaAlunoMatriculado(ddlAluno, LoginAuxili.CO_EMP, modalidade, serie, turma, ddlAno.SelectedValue, true);
        }

        /// <summary>
        /// Método que carrega o dropdown de Disciplinas
        /// </summary>
        private void CarregaDisciplina()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaMateriasGradeCurso(ddlDisciplina, LoginAuxili.CO_EMP, modalidade, serie, ddlAno.SelectedValue, true);
        }
        #endregion

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
            CarregaAluno();
            CarregaDisciplina();
            grdBusca.DataSource = null;
            grdBusca.DataBind();
            divGrid.Visible = false;
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
            CarregaAluno();
            CarregaDisciplina();
            grdBusca.DataSource = null;
            grdBusca.DataBind();
            divGrid.Visible = false;
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAluno();
            CarregaDisciplina();
            grdBusca.DataSource = null;
            grdBusca.DataBind();
            divGrid.Visible = false;
        }

        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
            CarregaAluno();
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

        protected void btnPesqGride_Click(object sender, EventArgs e)
        {
            if (ddlModalidade.SelectedValue == "" || ddlSerieCurso.SelectedValue == "" || ddlTurma.SelectedValue == "" || ddlDisciplina.SelectedValue == ""
                || ddlAno.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Ano, Modalidade, Série, Turma e Disciplina devem ser informados.");
                return;
            }

            CarregaGrid();
        }

        #region Lista de Notas dos Alunos
        public class NotasAluno
        {
            public string FL_AGRUPADORA { get; set; }
            public string FL_AGRUPADORA_V
            {
                get
                {
                    return (this.FL_AGRUPADORA == "S" ? "S" : "N");
                }
            }
            public int? ID_AGRUP_MATER { get; set; }
            public int? ordImp { get; set; }
            public decimal baseMedia { get; set; }
            public decimal? hdNotab1Aux { get; set; }
            public string hdNota1Aux_Valid
            {
                get
                {
                    return (this.hdNotab1Aux.HasValue ? this.hdNotab1Aux.ToString() : "");
                }
            }
            public string noMateria { get; set; }
            public int CO_MAT { get; set; }
            public string NO_ALU_RECEB { get; set; }
            public string NO_ALU
            {
                get
                {
                    return (this.NO_ALU_RECEB.Length > 29 ? this.NO_ALU_RECEB.Substring(0, 29) + "..." : NO_ALU_RECEB);
                }
            }
            public string CO_BIMESTRE { get; set; }
            public string NOTA1 { get; set; }
            public string NOTA2 { get; set; }
            public string NOTA3 { get; set; }
            public string NOTA_SIMU { get; set; }
            public string MEDIA { get; set; }
            public int NU_NIRE { get; set; }
            public int CO_ALU { get; set; }

            public decimal? NTBIM { get; set; }
            public string NTBIM_VALID
            {
                get
                {
                    return (this.NTBIM != null ? this.NTBIM.Value.ToString() : " - ");
                }
            }
            public decimal? NTRECUP { get; set; }
            public string NTRECUP_VALID
            {
                get
                {
                    return (this.NTRECUP != null ? this.NTRECUP.Value.ToString() : " - ");
                }
            }
            public decimal? MEDBIM { get; set; }
            public string MEDBIM_VALID
            {
                get
                {
                    return this.MEDBIM != null ? this.MEDBIM.Value.ToString() : " - ";
                }
            }
            public decimal? MEDSEM { get; set; }
            public string MEDSEM_VALID
            {
                get
                {
                    return this.MEDSEM != null ? this.MEDSEM.Value.ToString() : " - ";
                }
            }

            public decimal? NTAuxBim1 { get; set; }
            public decimal? NTAuxBim2 { get; set; }
            public decimal? NTAuxBim3 { get; set; }
            public decimal? NTAuxBim4 { get; set; }

            public decimal? NTAuxRecp1 { get; set; }
            public decimal? NTAuxRecp2 { get; set; }
            public decimal? NTAuxRecp3 { get; set; }
            public decimal? NTAuxRecp4 { get; set; }

            public decimal? NTMediaBim1 { get; set; }
            public decimal? NTMediaBim2 { get; set; }
            public decimal? NTMediaBim3 { get; set; }
            public decimal? NTMediaBim4 { get; set; }

            public decimal? NTAuxMedSem1 { get; set; }
            public decimal? NTAuxMedSem2 { get; set; }

            public string DescNU_NIRE { get { return this.NU_NIRE.ToString().PadLeft(9, '0'); } }
        }
        #endregion
    }
}