//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE DE AVALIAÇÃO ESCOLAR DO ALUNO
// OBJETIVO: LANÇAMENTO E MANUTENÇÃO, POR SÉRIE/TURMA, DE MÉDIAS BIMESTRAIS DO ALUNO
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 14/05/13   ANDRÉ NOBRE VINAGRE          Adicionada população nos campos VL_NOTA_BIM1_ORI,
//                                         VL_NOTA_BIM2_ORI, VL_NOTA_BIM3_ORI e VL_NOTA_BIM4_ORI
//
// ----------+----------------------------+-------------------------------------
// 19/06/2013| André Nobre Vinagre        | Colocado o tratamento da data de lançamento do bimestre
//           |                            | vindo da tabela de unidade
//           |                            |
// ----------+----------------------------+-------------------------------------
// 02/07/2013| André Nobre Vinagre        | Colocado o tratamento para aceitar o valor zero
//           |                            |
// ----------+----------------------------+-------------------------------------
// 02/07/2013| Victor Martins Machado     | Incluido o cálculo da média do agrupador da matéria.
//           |                            |

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3500_CtrlDesempenhoEscolar.F3503_LancManutSerieTurmaMediaBim
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
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                AuxiliPagina.RedirecionaParaPaginaErro("Funcionalidade apenas de alteração, não disponível inclusão.", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());

            if (IsPostBack) return;

            CarregaAnos();
            CarregaModalidades();
            CarregaSerieCurso();
            CarregaTurma(); 
            CarregaMaterias();
            ddlReferencia.SelectedValue = QueryStringAuxili.RetornaQueryStringPelaChave("ref");
            CarregaTipoLanc();
            CarregaGrid();

            VerificaTPLanc();
        }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {            
            int modalidade = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoModuCur);
            int serie = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoCur);
            string anoRef = QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Ano);
            int materia = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoMat);

//------------> Recebe o bimestre
            int intBimestre = int.Parse(ddlReferencia.SelectedValue);

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
                    tb079.CO_MODU_CUR = modalidade;
                    tb079.TB44_MODULO = TB44_MODULO.RetornaPelaChavePrimaria(tb079.CO_MODU_CUR);
                    tb079.CO_ANO_REF = anoRef;
                    tb079.CO_CUR = serie;
                    tb079.CO_MAT = materia;                                        
                }

                decimal dcmMedia;
                int intQtdFaltas;
                string strCrit, strRespon, strAprend;

                switch (ddlTpLanc.SelectedValue)
                {
                    case "N":
                        tb079.FL_TIPO_LANC_MEDIA = "N";
                        break;
                    case "C":
                        tb079.FL_TIPO_LANC_MEDIA = "C";
                        break;
                }
                
//------------> Faz a verificação para saber se dado digitado para a média é válido
                if (decimal.TryParse(((TextBox)linha.Cells[2].FindControl("txtMedia")).Text, out dcmMedia)) 
                {
                    string dcntMax = (((HiddenField)linha.Cells[2].FindControl("hidNotaMaxi")).Value);
                    if (!string.IsNullOrEmpty(dcntMax))
                    {
                        decimal ntMaxDeci = decimal.Parse(dcntMax);
                        if (dcmMedia > ntMaxDeci)
                        {
                            int coMa = int.Parse(ddlMateria.SelectedValue);
                            string noMat = (from tb02 in TB02_MATERIA.RetornaTodosRegistros()
                                            join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                            where tb02.CO_MAT == coMa
                                            select new { tb107.NO_MATERIA }).FirstOrDefault().NO_MATERIA;

                            AuxiliPagina.EnvioMensagemErro(this.Page, "A nota máxima informada na grade anual do curso para a Disciplina " + noMat + " é " + dcntMax);
                            return;
                        }
                    }

//----------------> Média deve estar entre 0 e 10
                    if (dcmMedia < 0 || dcmMedia > 100)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Média deve estar entre 0 e 10");
                        return;
                    }

//----------------> Atribui o valor de média informado de acordo com o bimestre
                    switch (intBimestre) 
                    {
                        case 1: tb079.VL_NOTA_BIM1 = dcmMedia;
                            tb079.VL_NOTA_BIM1_ORI = dcmMedia;
                            tb079.VL_MEDIA_BIM1 = dcmMedia;
                            break;
                        case 2: tb079.VL_NOTA_BIM2 = dcmMedia;
                            tb079.VL_NOTA_BIM2_ORI = dcmMedia;
                            tb079.VL_MEDIA_BIM2 = dcmMedia;
                            break;
                        case 3: tb079.VL_NOTA_BIM3 = dcmMedia;
                            tb079.VL_NOTA_BIM3_ORI = dcmMedia;
                            tb079.VL_MEDIA_BIM3 = dcmMedia;
                            break;
                        case 4: tb079.VL_NOTA_BIM4 = dcmMedia;
                            tb079.VL_NOTA_BIM4_ORI = dcmMedia;
                            tb079.VL_MEDIA_BIM4 = dcmMedia;
                            break;
                    }

                    if (tb079.VL_NOTA_SEM1.HasValue && tb079.VL_NOTA_BIM1.HasValue && tb079.VL_NOTA_BIM2.HasValue)
                        tb079.VL_NOTA_SEM1 = (tb079.VL_NOTA_BIM1 + tb079.VL_NOTA_BIM2) / 2;

                    if (tb079.VL_NOTA_SEM2.HasValue && tb079.VL_NOTA_BIM3.HasValue && tb079.VL_NOTA_BIM4.HasValue)
                        tb079.VL_NOTA_SEM2 = (tb079.VL_NOTA_BIM3 + tb079.VL_NOTA_BIM4) / 2;

                }

//------------> Faz a verificação para saber se dado digitado para o conceito é válido
                if (((DropDownList)linha.Cells[2].FindControl("ddlConceito")).SelectedValue != "")
                {
//----------------> Atribui o valor de conceito informado de acordo com o bimestre
                    switch (intBimestre)
                    {
                        case 1: tb079.VL_CONC_BIM1 = ((DropDownList)linha.Cells[3].FindControl("ddlConceito")).SelectedValue; break;
                        case 2: tb079.VL_CONC_BIM2 = ((DropDownList)linha.Cells[3].FindControl("ddlConceito")).SelectedValue; break;
                        case 3: tb079.VL_CONC_BIM3 = ((DropDownList)linha.Cells[3].FindControl("ddlConceito")).SelectedValue; break;
                        case 4: tb079.VL_CONC_BIM4 = ((DropDownList)linha.Cells[3].FindControl("ddlConceito")).SelectedValue; break;
                    }
                }

//------------> Faz a verificação para saber se dado digitado para a falta é válido
                if (int.TryParse(((TextBox)linha.Cells[3].FindControl("txtFaltas")).Text, out intQtdFaltas))
                {
//----------------> Atribui o valor de falta informado de acordo com o bimestre
                    switch (intBimestre)
                    {
                        case 1: tb079.QT_FALTA_BIM1 = intQtdFaltas; break;
                        case 2: tb079.QT_FALTA_BIM2 = intQtdFaltas; break;
                        case 3: tb079.QT_FALTA_BIM3 = intQtdFaltas; break;
                        case 4: tb079.QT_FALTA_BIM4 = intQtdFaltas; break;
                    }
                }

                if (tb079.EntityState != System.Data.EntityState.Unchanged && GestorEntities.SaveOrUpdate(tb079) < 1)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao salvar os itens");
                    return;
                }

                if (decimal.TryParse(((TextBox)linha.Cells[2].FindControl("txtMedia")).Text, out dcmMedia))
                {
                    #region Calcula a média do Agrupador
                    //----------------> Pega o agrupador da matéria, se houver.
                    int? idAgr = TB43_GRD_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, anoRef, serie, materia).ID_MATER_AGRUP != null ? TB43_GRD_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, anoRef, serie, materia).ID_MATER_AGRUP : null;

                    int turma = tb079.CO_TUR;

                    //----------------> Se a matéria possuir agrupador, o sistema calculará a média do agrupador para incluir na tabela de histórico
                    if (idAgr != null)
                    {
                        decimal mA = 0; // Média do agrupador
                        int cF = 0; // Contador de filhos do agrupador
                        decimal sF = 0; // Soma das notas dos filhos

                        //--------------------> Retorna a quantidade de filhos que o agrupador possui
                        cF = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                              where tb43.CO_ANO_GRADE == anoRef
                              && tb43.CO_CUR == serie
                              && tb43.TB44_MODULO.CO_MODU_CUR == modalidade
                              && tb43.ID_MATER_AGRUP == idAgr.Value
                              select tb43).Count();

                        var lstG = (from gc in TB43_GRD_CURSO.RetornaTodosRegistros()
                                    where gc.CO_ANO_GRADE == anoRef
                                    && gc.CO_CUR == serie
                                    && gc.TB44_MODULO.CO_MODU_CUR == modalidade
                                    && gc.ID_MATER_AGRUP == idAgr.Value
                                    select new
                                    {
                                        gc.CO_MAT
                                    });

                        List<int> lstGl = new List<int>();

                        foreach (var v in lstG)
                        {
                            lstGl.Add(v.CO_MAT);
                        }

                        //--------------------> Cria uma lista com as notas, dos bimestres, de todos os filhos
                        var lstF = (from ha in TB079_HIST_ALUNO.RetornaTodosRegistros()
                                    where ha.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                    && ha.CO_ANO_REF == anoRef
                                    && ha.CO_CUR == serie
                                    && ha.CO_TUR == turma
                                    && ha.CO_ALU == coAlu
                                    && lstGl.Contains(ha.CO_MAT)
                                    select new
                                    {
                                        VL_NOTA_BIM1 = ha.VL_NOTA_BIM1 != null ? ha.VL_NOTA_BIM1.Value : 0,
                                        VL_NOTA_BIM2 = ha.VL_NOTA_BIM2 != null ? ha.VL_NOTA_BIM2.Value : 0,
                                        VL_NOTA_BIM3 = ha.VL_NOTA_BIM3 != null ? ha.VL_NOTA_BIM3.Value : 0,
                                        VL_NOTA_BIM4 = ha.VL_NOTA_BIM4 != null ? ha.VL_NOTA_BIM4.Value : 0
                                    });

                        //--------------------> Soma as médias dos filhos de acordo com o bimestre
                        switch (intBimestre)
                        {
                            case 1:
                                sF = lstF.Count() > 0 ? lstF.Sum(s => s.VL_NOTA_BIM1) : 0;
                                break;
                            case 2:
                                sF = lstF.Count() > 0 ? lstF.Sum(s => s.VL_NOTA_BIM2) : 0;
                                break;
                            case 3:
                                sF = lstF.Count() > 0 ? lstF.Sum(s => s.VL_NOTA_BIM3) : 0;
                                break;
                            case 4:
                                sF = lstF.Count() > 0 ? lstF.Sum(s => s.VL_NOTA_BIM4) : 0;
                                break;
                        }

                        mA = (decimal)sF / cF;

                        TB079_HIST_ALUNO haAgr = TB079_HIST_ALUNO.RetornaPelaChavePrimaria(coAlu, modalidade, serie, anoRef, idAgr.Value);

                        switch (intBimestre)
                        {
                            case 1:
                                haAgr.VL_NOTA_BIM1 = mA;
                                break;
                            case 2:
                                haAgr.VL_NOTA_BIM2 = mA;
                                break;
                            case 3:
                                haAgr.VL_NOTA_BIM3 = mA;
                                break;
                            case 4:
                                haAgr.VL_NOTA_BIM4 = mA;
                                break;
                        }

                        GestorEntities.SaveOrUpdate(haAgr);
                    }
                    #endregion
                }
            }

            //Persiste as informações inseridas nos parâmetros para serem colocadas novamente depois que tudo for salvo, facilitando no momento de calcular diversas médias.
            var parametros = ddlAno.SelectedValue + ";" + ddlReferencia.SelectedValue + ";" + ddlModalidade.SelectedValue + ";" + ddlSerieCurso.SelectedValue + ";" + ddlTurma.SelectedValue;
            HttpContext.Current.Session["buscaLancMediasBimestraisMD"] = parametros;

            AuxiliPagina.RedirecionaParaPaginaSucesso("Item editado com sucesso.", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
        }

        #endregion

        #region Carregamento

        /// <summary>
        /// Verifica qual é o item selecionado no dropdownlist de tipo de lançamento e age de acordo com o selecionado
        /// </summary>
        private void VerificaTPLanc()
        {
            if (ddlTpLanc.SelectedValue == "N")
            {
                //--------> Varra toda a grid de Busca
                foreach (GridViewRow linha in grdBusca.Rows)
                {
                    TextBox txtme = ((TextBox)linha.Cells[2].FindControl("txtMedia"));
                    DropDownList ddlcn = ((DropDownList)linha.Cells[3].FindControl("ddlConceito"));
                    txtme.Visible = true;
                    ddlcn.Visible = false;

                    if (((HiddenField)linha.Cells[2].FindControl("hidFlHomol")).Value == "S")
                    {
                        txtme.Enabled = ddlcn.Enabled = false;
                    }
                    else
                    {
                        ddlcn.Enabled = txtme.Enabled = true;
                    }
                }

                //Muda o título da coluna de acordo com o que é selecionado no tipo de lançamento
                grdBusca.Columns[2].HeaderText = "Conceito";
            }
            else
            {
                //--------> Varra toda a grid de Busca
                foreach (GridViewRow linha in grdBusca.Rows)
                {
                    TextBox txtme = ((TextBox)linha.Cells[2].FindControl("txtMedia"));
                    DropDownList ddlcn = ((DropDownList)linha.Cells[3].FindControl("ddlConceito"));
                    ddlcn.Visible = true;
                    txtme.Visible = false;

                    if (((HiddenField)linha.Cells[2].FindControl("hidFlHomol")).Value == "S")
                    {
                        ddlcn.Enabled = txtme.Enabled = false;
                    }
                    else
                    {
                        ddlcn.Enabled = txtme.Enabled = false;
                    }
                }

                //Muda o título da coluna de acordo com o que é selecionado no tipo de lançamento
                grdBusca.Columns[2].HeaderText = "Média";
            }
        }

        private void CarregaTipoLanc()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int materia = ddlMateria.SelectedValue != "" ? int.Parse(ddlMateria.SelectedValue) : 0;
            int coReferencia = ddlReferencia.SelectedValue == "B1" ? 1 : ddlReferencia.SelectedValue == "B2" ? 2 :
                ddlReferencia.SelectedValue == "B3" ? 3 : ddlReferencia.SelectedValue == "B4" ? 4 : ddlReferencia.SelectedValue == "T1" ? 5 :
                ddlReferencia.SelectedValue == "T2" ? 6 : ddlReferencia.SelectedValue == "T3" ? 7 : 0;
            string anoRef = ddlAno.SelectedValue;

            //var lstLanc = (from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
            //                 where tb079.CO_EMP == LoginAuxili.CO_EMP && tb079.CO_ANO_REF == anoRef && tb079.CO_CUR == serie && tb079.CO_TUR == turma
            //                 && tb079.CO_MODU_CUR == modalidade && tb079.CO_MAT == materia
            //                 select new { tb079.FL_TIPO_LANC_MEDIA }).FirstOrDefault();


            string flLanc = (from tb01 in TB01_CURSO.RetornaTodosRegistros()
                             where tb01.CO_CUR == serie
                             select new
                             {
                                 tb01.CO_TIPO_LANC_NOTA
                             }).FirstOrDefault().CO_TIPO_LANC_NOTA;
            //string flLanc = (lstLanc != null) ? lstLanc.FL_TIPO_LANC_MEDIA.ToString() : "";
            ddlTpLanc.SelectedValue = flLanc;
        }

        /// <summary>
        /// Método que carrega os conceitos cadastrados
        /// </summary>
        /// <param name="ddl">DropDownList que recebera os conceitos</param>
        protected void CarregaConceito(DropDownList ddl, string conc)
        {
            var res = (from tb200 in TB200_EQUIV_NOTA_CONCEITO.RetornaTodosRegistros()
                       where tb200.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                       && tb200.CO_SITU_CONC == "A"
                       select new
                       {
                           tb200.CO_SIGLA_CONCEITO,
                           tb200.DE_CONCEITO
                       });

            ddl.DataTextField = "DE_CONCEITO";
            ddl.DataValueField = "CO_SIGLA_CONCEITO";

            ddl.DataSource = res;
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("Selecione", ""));

            if (conc != "")
                ddl.SelectedValue = conc;
        }


        /// <summary>
        /// Método que carrega a grid de Notas e Conceitos da Matérias
        /// </summary>
        private void CarregaGrid()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int materia = ddlMateria.SelectedValue != "" ? int.Parse(ddlMateria.SelectedValue) : 0;
            int coReferencia = ddlMateria.SelectedValue != "" ? int.Parse(ddlReferencia.SelectedValue) : 0;
            string anoRef = ddlAno.SelectedValue;

            grdBusca.DataKeyNames = new string[] { "CO_ALU" };

            var resultado = (from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                             join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on tb079.CO_MAT equals tb43.CO_MAT
                             join tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                on new { tb079.CO_ALU, tb079.CO_EMP } equals new { tb08.CO_ALU, tb08.CO_EMP}
                             join tb07 in TB07_ALUNO.RetornaTodosRegistros()
                                on tb079.CO_ALU equals tb07.CO_ALU
                             where tb079.CO_EMP == LoginAuxili.CO_EMP 
                             && tb079.CO_ANO_REF == anoRef 
                             && tb079.CO_CUR == serie 
                             && tb079.CO_TUR == turma
                             && tb079.CO_MODU_CUR == modalidade 
                             && tb079.CO_MAT == materia
                             && (tb08.CO_SIT_MAT == "R" || tb08.CO_SIT_MAT == "A")
                             && tb08.CO_ANO_MES_MAT == anoRef
                             && tb43.CO_MAT == tb079.CO_MAT
                             && tb43.CO_CUR == tb079.CO_CUR
                             && tb43.CO_ANO_GRADE == tb079.CO_ANO_REF
                             && tb43.CO_EMP == tb079.CO_EMP
                             select new GridSaida
                             {
                                 VL_MAX = tb43.VL_NOTA_MAXIM,
                                 CO_ALU = tb079.CO_ALU,
                                 NU_NIRE = tb07.NU_NIRE,
                                 CO_ALU_CAD = tb08.CO_ALU_CAD.Length == 11 ? tb08.CO_ALU_CAD.Insert(5, ".").Insert(2, ".") : tb08.CO_ALU_CAD,
                                 NO_ALU = tb07.NO_ALU,
                                 MEDIA = coReferencia == 1 ? tb079.VL_NOTA_BIM1 : coReferencia == 2 ? tb079.VL_NOTA_BIM2 : coReferencia == 3 ? tb079.VL_NOTA_BIM3 : coReferencia == 4 ? tb079.VL_NOTA_BIM4 :
                                         coReferencia == 5 ? tb079.VL_NOTA_TRI1 : coReferencia == 6 ? tb079.VL_NOTA_TRI2 : tb079.VL_NOTA_TRI3,
                                 FALTAS = coReferencia == 1 ? tb079.QT_FALTA_BIM1 : coReferencia == 2 ? tb079.QT_FALTA_BIM2 : coReferencia == 3 ? tb079.QT_FALTA_BIM3 : coReferencia == 4 ? tb079.QT_FALTA_BIM4 :
                                          coReferencia == 5 ? tb079.QT_FALTA_TRI1 : coReferencia == 2 ? tb079.QT_FALTA_TRI2 : tb079.QT_FALTA_TRI3,
                                 CONCEITO = coReferencia == 1 ? tb079.VL_CONC_BIM1 : coReferencia == 2 ? tb079.VL_CONC_BIM2 : coReferencia == 3 ? tb079.VL_CONC_BIM3 : coReferencia == 3 ? tb079.VL_CONC_BIM4 : 
                                            coReferencia == 5 ? tb079.VL_CONC_TRI1 : coReferencia == 6 ? tb079.VL_CONC_TRI2 : tb079.VL_CONC_TRI3,
                                 CRIT = coReferencia == 1 ? tb079.VL_CRIT_BIM1 == null ? "" : tb079.VL_CRIT_BIM1 : coReferencia == 2 ? tb079.VL_CRIT_BIM2 == null ? "" :
                                        tb079.VL_CRIT_BIM2 : coReferencia == 3 ? tb079.VL_CRIT_BIM3 == null ? "" : tb079.VL_CRIT_BIM3 : coReferencia == 4 ? tb079.VL_CRIT_BIM4 : tb079.VL_CRIT_BIM4 == null ? "" :
                                        coReferencia == 5 ? tb079.VL_CRIT_TRI1 == null ? "" : tb079.VL_CRIT_TRI1 : coReferencia == 6 ? tb079.VL_CRIT_TRI2 == null ? "" : tb079.VL_CRIT_TRI2 : tb079.VL_CRIT_TRI3 == null ? "" : tb079.VL_CRIT_TRI3,
                                 RESP = coReferencia == 1 ? tb079.VL_RESP_BIM1 == null ? "" : tb079.VL_RESP_BIM1 : coReferencia == 2 ? tb079.VL_RESP_BIM2 == null ? "" :
                                        tb079.VL_RESP_BIM2 : coReferencia == 3 ? tb079.VL_RESP_BIM3 == null ? "" : tb079.VL_RESP_BIM3 : coReferencia == 4 ? tb079.VL_RESP_BIM4 == null ? "" : tb079.VL_RESP_BIM4 : 
                                         coReferencia == 5 ? tb079.VL_RESP_TRI1 == null ? "" : tb079.VL_RESP_TRI1 : coReferencia == 6 ? tb079.VL_RESP_TRI2 == null ? "" : tb079.VL_RESP_TRI2 : tb079.VL_RESP_TRI3 == null ? "" : tb079.VL_RESP_TRI3,
                                 APRE = coReferencia == 1 ? tb079.VL_APRE_BIM1 == null ? "" : tb079.VL_APRE_BIM1 : coReferencia == 2 ? tb079.VL_APRE_BIM2 == null ? "" :
                                        tb079.VL_APRE_BIM2 : coReferencia == 3 ? tb079.VL_APRE_BIM3 == null ? "" : tb079.VL_APRE_BIM3 : coReferencia == 4 ? tb079.VL_APRE_BIM4 == null ? "" : tb079.VL_APRE_BIM4 :
                                        coReferencia == 5 ? tb079.VL_APRE_TRI1 == null ? "" : tb079.VL_APRE_TRI1 : coReferencia == 6 ? tb079.VL_APRE_TRI2 == null ? "" : tb079.VL_APRE_TRI2 : tb079.VL_APRE_TRI3 == null ? "" : tb079.VL_APRE_TRI3,
                                 FL_HOMOL = coReferencia == 1 ? tb079.FL_HOMOL_NOTA_BIM1 : coReferencia == 2 ? tb079.FL_HOMOL_NOTA_BIM2 : coReferencia == 3 ? tb079.FL_HOMOL_NOTA_BIM3 : coReferencia == 4 ? tb079.FL_HOMOL_NOTA_BIM4 : 
                                            coReferencia == 5 ? tb079.FL_HOMOL_NOTA_TRI1 : coReferencia == 6 ? tb079.FL_HOMOL_NOTA_TRI2 : tb079.FL_HOMOL_NOTA_TRI2  
                             }).Distinct().OrderBy(h => h.NO_ALU);

            grdBusca.DataSource = resultado.Count() > 0 ? resultado : null;
            grdBusca.DataBind();

            VerificaFaltas();
        }

        public class GridSaida
        {
            public decimal? VL_MAX { get; set; }
            public int CO_ALU { get; set; }
            public int NU_NIRE { get; set; }
            public string CO_ALU_CAD { get; set; }
            public string NO_ALU { get; set; }
            public decimal? MEDIA { get; set; }
            public int? FALTAS { get; set; }
            public string CONCEITO { get; set; }
            public string CRIT { get; set; }
            public string RESP { get; set; }
            public string APRE { get; set; }
            public string FL_HOMOL { get; set; }
            public bool HOMOL
            {
                get
                {
                    return this.FL_HOMOL == "S" ? false : true;
                }
            }
        }


        /// <summary>
        /// Método responsável por preencher a quantidade de faltas de acordo com o calculado no diário para os parâmetros em questão
        /// </summary>
        private void VerificaFaltas()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int coMat = ddlMateria.SelectedValue != "" ? int.Parse(ddlMateria.SelectedValue) : 0;
            int bim = ddlReferencia.SelectedValue != "" ? int.Parse(ddlReferencia.SelectedValue) : 0;
            string anoRef = ddlAno.SelectedValue;

            foreach (GridViewRow li in grdBusca.Rows)
            {
                //Faz o Cálculo apenas se a nota e a falta ainda não tiverem sido lançadas
                string nota = (((TextBox)li.Cells[2].FindControl("txtMedia")).Text);
                string faltas = (((TextBox)li.Cells[3].FindControl("txtFaltas")).Text);
                int coAlu = int.Parse((((HiddenField)li.Cells[2].FindControl("hidCoAlu")).Value));
                TextBox txtFlt = ((TextBox)li.Cells[3].FindControl("txtFaltas"));
                string bimes = "";
                switch (ddlReferencia.SelectedValue)
                {
                    case "1":
                        bimes = "B1";
                        break;
                    case "2":
                        bimes = "B2";
                        break;
                    case "3":
                        bimes = "B3";
                        break;
                    case "4":
                        bimes = "B4";
                        break;
                    case "5":
                        bimes = "T1";
                        break;
                    case "6":
                        bimes = "T2";
                        break;
                    case "7":
                        bimes = "T3";
                        break;
                    default:
                        bimes = "";
                        break;
                }

                if (((string.IsNullOrEmpty(nota)) && ((string.IsNullOrEmpty(faltas)) || (faltas == "0"))))
                {
                    int flt = (from tb132 in TB132_FREQ_ALU.RetornaTodosRegistros()
                               where tb132.TB01_CURSO.CO_CUR == serie
                               && tb132.TB01_CURSO.TB44_MODULO.CO_MODU_CUR == modalidade
                               && tb132.CO_TUR == turma
                               && tb132.TB07_ALUNO.CO_ALU == coAlu
                               && tb132.CO_MAT == coMat
                               && tb132.FL_HOMOL_FREQU == "S"
                               && tb132.CO_FLAG_FREQ_ALUNO == "N"
                               && tb132.CO_BIMESTRE == bimes
                               select tb132).Count();

                    txtFlt.Text = flt.ToString();
                }
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            string strAno = QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Ano);
            ddlAno.Items.Insert(0, new ListItem(strAno, strAno));
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            var tb44 = TB44_MODULO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoModuCur));
            ddlModalidade.Items.Insert(0, new ListItem(tb44.DE_MODU_CUR, tb44.CO_MODU_CUR.ToString()));
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {
            var tb01 = TB01_CURSO.RetornaPeloCoCur(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoCur));
            ddlSerieCurso.Items.Insert(0, new ListItem(tb01.NO_CUR, tb01.CO_CUR.ToString()));
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        private void CarregaTurma()
        {
            var tb129 = TB129_CADTURMAS.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoTur));
            ddlTurma.Items.Insert(0, new ListItem(tb129.NO_TURMA, tb129.CO_TUR.ToString()));
        }

        /// <summary>
        /// Método que carrega o dropdown de Matérias
        /// </summary>
        private void CarregaMaterias()
        {
            int materia = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoMat);

            var varMateria = (from tb02 in TB02_MATERIA.RetornaTodosRegistros()
                              where tb02.CO_MAT == materia
                              join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                              select new { tb02.CO_MAT, tb107.NO_MATERIA }).FirstOrDefault();

            ddlMateria.Items.Insert(0, new ListItem(varMateria.NO_MATERIA, varMateria.CO_MAT.ToString()));
        }
        #endregion

        public void ddlTpLanc_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            VerificaTPLanc();
        }

        protected void grdBusca_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddl = ((DropDownList)e.Row.Cells[3].FindControl("ddlConceito"));
                string conc = ((HiddenField)e.Row.Cells[3].FindControl("hidConceito")).Value;

                if (((HiddenField)e.Row.Cells[2].FindControl("hidFlHomol")).Value != "S")
                {
                    CarregaConceito(ddl, conc);
                }
            }
        }
    }
}