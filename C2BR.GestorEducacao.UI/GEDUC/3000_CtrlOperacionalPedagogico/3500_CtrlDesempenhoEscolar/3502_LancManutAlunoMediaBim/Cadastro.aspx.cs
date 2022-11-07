//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE DE AVALIAÇÃO ESCOLAR DO ALUNO
// OBJETIVO: LANÇAMENTO E MANUTENÇÃO, POR ALUNO, DE MÉDIAS BIMESTRAIS DO ALUNO
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 23/04/13   CORDOVA JUNIOR               Alteracao do limite de notas (20 -> 100)
//
// ----------+----------------------------+-------------------------------------
// 14/05/13   ANDRÉ NOBRE VINAGRE          Adicionada população nos campos VL_NOTA_BIM1_ORI,
//                                         VL_NOTA_BIM2_ORI, VL_NOTA_BIM3_ORI e VL_NOTA_BIM4_ORI
//
// ----------+----------------------------+-------------------------------------
// 19/06/2013| André Nobre Vinagre        | Colocado o tratamento da data de lançamento do bimestre
//           |                            | vindo da tabela de unidade
//           |                            |
// ----------+----------------------------+-------------------------------------
// 05/08/2013| Josemberg Farias Ferreira  | Inserido a opção para inserção de avaliação(texto) por matéria
//           |                            |
// ----------+----------------------------+-------------------------------------
// 27/07/2016| Filipe Rodrigues           | Alterações para incluir trimestres

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Web;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Collections.Generic;
using System.Text;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3500_CtrlDesempenhoEscolar.F3502_LancManutAlunoMediaBim
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
            CarregaMedidas();
            ddlReferencia.SelectedValue = QueryStringAuxili.RetornaQueryStringPelaChave("ref");
            CarregaModalidades();
            CarregaSerieCurso();
            CarregaTurma();
            CarregaAluno();
            CarregaTipoLanc();
            CarregaGrid();
            CarregaAvaliacao();

            VerificaTPLanc();
        }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int coAlu = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoAlu);
            int modalidade = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoModuCur);
            int serie = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoCur);
            string anoRef = QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Ano);
            int turma = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoTur);

            //------------> Recebe o bimestre
            var refer = ddlReferencia.SelectedValue;

            var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

            tb25.TB82_DTCT_EMPReference.Load();

            DateTime dataLacto = DateTime.Now.Date;
            if (tb25.TB82_DTCT_EMP != null)
            {
                if (refer == "B1")
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
                else if (refer == "B2")
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
                else if (refer == "B3")
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
                else if (refer == "B4")
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
            //Valores direto dados.ContainsKey("643")
            var json = new System.Web.Script.Serialization.JavaScriptSerializer();
            Dictionary<string, string> dados = new Dictionary<string, string>();
            if (hfValoresAval.Value != "")
                dados = json.Deserialize<Dictionary<string, string>>(hfValoresAval.Value);
            string valorAval = "";

            //--------> Varra toda a grid de Busca
            foreach (GridViewRow linha in grdBusca.Rows)
            {
                //------------> Recebe o código da matéria
                int materia = Convert.ToInt32(grdBusca.DataKeys[linha.RowIndex].Values[0]);

                //------------> Recebe o dado da tabela de histórico de acordo com as chaves passadas
                TB079_HIST_ALUNO tb079 = TB079_HIST_ALUNO.RetornaPelaChavePrimaria(coAlu, modalidade, serie, anoRef, materia);

                if (tb079 == null)
                {
                    tb079.CO_EMP = LoginAuxili.CO_EMP;
                    tb079.CO_ALU = coAlu;
                    tb079.CO_MODU_CUR = modalidade;
                    tb079.TB44_MODULO = TB44_MODULO.RetornaPelaChavePrimaria(tb079.CO_MODU_CUR);
                    tb079.CO_ANO_REF = anoRef;
                    tb079.CO_CUR = serie;
                    tb079.CO_MAT = materia;
                    //Verifica se há alteração na avaliação para a linha/materia atual
                    if (dados != null && dados.ContainsKey(materia.ToString()))
                        valorAval = dados[materia.ToString()];
                    switch (refer)
                    {
                        case "B1":
                            tb079.DE_RES_AVAL_BIM1 = valorAval;
                            break;
                        case "B2":
                            tb079.DE_RES_AVAL_BIM2 = valorAval;
                            break;
                        case "B3":
                            tb079.DE_RES_AVAL_BIM3 = valorAval;
                            break;
                        case "B4":
                            tb079.DE_RES_AVAL_BIM4 = valorAval;
                            break;
                        case "T1":
                            tb079.DE_RES_AVAL_TRI1 = valorAval;
                            break;
                        case "T2":
                            tb079.DE_RES_AVAL_TRI2 = valorAval;
                            break;
                        case "T3":
                            tb079.DE_RES_AVAL_TRI3 = valorAval;
                            break;
                    }
                }

                decimal dcmMedia;
                int intQtdFaltas;

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
                        //Verifica se a nota digitada está dentro do limite máximo estipulado
                        decimal ntMaxDeci = decimal.Parse(dcntMax);
                        if (dcmMedia > ntMaxDeci)
                        {
                            int coMa = int.Parse(((HiddenField)linha.Cells[2].FindControl("hidCoMat")).Value);
                            string noMat = (from tb02 in TB02_MATERIA.RetornaTodosRegistros()
                                            join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                            where tb02.CO_MAT == coMa
                                            select new { tb107.NO_MATERIA }).FirstOrDefault().NO_MATERIA;

                            AuxiliPagina.EnvioMensagemErro(this.Page, "A nota máxima informada na grade anual do curso para a Disciplina " + noMat + " é " +  dcntMax );
                            return;
                        }
                    }
                    //----------------> Média deve estar entre 0 e 100
                    if (dcmMedia < 0 || dcmMedia > 100)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Média deve estar entre 0 e 100");
                        return;
                    }

                    //----------------> Atribui o valor de média informado de acordo com o bimestre
                    switch (refer)
                    {
                        case "B1": 
                            tb079.VL_NOTA_BIM1 =
                            tb079.VL_NOTA_BIM1_ORI =
                            tb079.VL_MEDIA_BIM1 = dcmMedia;
                            break;
                        case "B2": 
                            tb079.VL_NOTA_BIM2 =
                            tb079.VL_NOTA_BIM2_ORI =
                            tb079.VL_MEDIA_BIM2 = dcmMedia;
                            break;
                        case "B3": 
                            tb079.VL_NOTA_BIM3 =
                            tb079.VL_NOTA_BIM3_ORI =
                            tb079.VL_MEDIA_BIM3 = dcmMedia;
                            break;
                        case "B4": 
                            tb079.VL_NOTA_BIM4 =
                            tb079.VL_NOTA_BIM4_ORI =
                            tb079.VL_MEDIA_BIM4 = dcmMedia;
                            break;
                        case "T1":
                            tb079.VL_NOTA_TRI1 =
                            tb079.VL_NOTA_TRI1_ORI =
                            tb079.VL_MEDIA_TRI1 = dcmMedia;
                            break;
                        case "T2":
                            tb079.VL_NOTA_TRI2 =
                            tb079.VL_NOTA_TRI2_ORI =
                            tb079.VL_MEDIA_TRI2 = dcmMedia;
                            break;
                        case "T3":
                            tb079.VL_NOTA_TRI3 =
                            tb079.VL_NOTA_TRI3_ORI =
                            tb079.VL_MEDIA_TRI3 = dcmMedia;
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
                    switch (refer)
                    {
                        case "B1": tb079.VL_CONC_BIM1 = ((DropDownList)linha.Cells[3].FindControl("ddlConceito")).SelectedValue; break;
                        case "B2": tb079.VL_CONC_BIM2 = ((DropDownList)linha.Cells[3].FindControl("ddlConceito")).SelectedValue; break;
                        case "B3": tb079.VL_CONC_BIM3 = ((DropDownList)linha.Cells[3].FindControl("ddlConceito")).SelectedValue; break;
                        case "B4": tb079.VL_CONC_BIM4 = ((DropDownList)linha.Cells[3].FindControl("ddlConceito")).SelectedValue; break;
                        case "T1": tb079.VL_CONC_TRI1 = ((DropDownList)linha.Cells[3].FindControl("ddlConceito")).SelectedValue; break;
                        case "T2": tb079.VL_CONC_TRI2 = ((DropDownList)linha.Cells[3].FindControl("ddlConceito")).SelectedValue; break;
                        case "T3": tb079.VL_CONC_TRI3 = ((DropDownList)linha.Cells[3].FindControl("ddlConceito")).SelectedValue; break;
                        case "MF": tb079.VL_CONC_FINAL = ((DropDownList)linha.Cells[3].FindControl("ddlConceito")).SelectedValue; break;
                    }
                }

                //------------> Faz a verificação para saber se dado digitad para a falta é válido
                if (int.TryParse(((TextBox)linha.Cells[3].FindControl("txtFaltas")).Text, out intQtdFaltas))
                {
                    //----------------> Atribui o valor de média informado de acordo com o bimestre
                    switch (refer)
                    {
                        case "B1": tb079.QT_FALTA_BIM1 = intQtdFaltas; break;
                        case "B2": tb079.QT_FALTA_BIM2 = intQtdFaltas; break;
                        case "B3": tb079.QT_FALTA_BIM3 = intQtdFaltas; break;
                        case "B4": tb079.QT_FALTA_BIM4 = intQtdFaltas; break;
                        case "T1": tb079.QT_FALTA_TRI1 = intQtdFaltas; break;
                        case "T2": tb079.QT_FALTA_TRI2 = intQtdFaltas; break;
                        case "T3": tb079.QT_FALTA_TRI3 = intQtdFaltas; break;
                        case "MF": tb079.QT_FALTA_FINAL = intQtdFaltas; break;
                    }
                }

                if (tb079.EntityState != System.Data.EntityState.Unchanged && GestorEntities.SaveOrUpdate(tb079) < 1)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao salvar os itens");
                    return;
                }
            }

            string deAval = txtAvalGeral.Text;
            TB126_HIST_ALUNO_AVAL tb126;

            tb126 = TB126_HIST_ALUNO_AVAL.RetornaTodosRegistros().Where(w => w.CO_MODU_CUR == modalidade && w.CO_CUR == serie && w.CO_TUR == turma && w.CO_BIMESTRE == refer && w.CO_ALU == coAlu && w.CO_ANO_REF == anoRef).FirstOrDefault();

            if (tb126 == null)
            {
                tb126 = new TB126_HIST_ALUNO_AVAL();
                tb126.CO_ALU = coAlu;
                tb126.CO_MODU_CUR = modalidade;
                tb126.CO_CUR = serie;
                tb126.CO_TUR = turma;
                tb126.CO_ANO_REF = anoRef;
                tb126.CO_BIMESTRE = refer;
                tb126.DE_AVAL = deAval;
                TB126_HIST_ALUNO_AVAL.SaveOrUpdate(tb126, true);
            }
            else
            {
                tb126.DE_AVAL = deAval;
                TB126_HIST_ALUNO_AVAL.SaveOrUpdate(tb126, true);
            }

            string tpRefer = QueryStringAuxili.RetornaQueryStringPelaChave("tpRef");
            //Persiste as informações inseridas nos parâmetros para serem colocadas novamente depois que tudo for salvo, facilitando no momento de calcular diversas médias.
            var parametros = ddlAno.SelectedValue + ";" + tpRefer + ";" + refer + ";" + ddlModalidade.SelectedValue + ";" + ddlSerieCurso.SelectedValue + ";" + ddlTurma.SelectedValue;
            HttpContext.Current.Session["buscaLancMediasBimestrais"] = parametros;

            AuxiliPagina.RedirecionaParaPaginaSucesso("Item editado com sucesso.", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
        }

        #endregion

        #region Carregamento

        private void CarregaTipoLanc()
        {
            //int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            //int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            //int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;
            //int bim = ddlReferencia.SelectedValue != "" ? int.Parse(ddlReferencia.SelectedValue) : 0;
            //string anoRef = ddlAno.SelectedValue;

            //string flLanc = (from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
            //                 where tb079.CO_EMP == LoginAuxili.CO_EMP && tb079.CO_ANO_REF == anoRef && tb079.CO_CUR == serie && tb079.CO_TUR == turma
            //                 && tb079.CO_MODU_CUR == modalidade && tb079.CO_ALU == coAlu
            //                 select new { tb079.FL_TIPO_LANC_MEDIA }).FirstOrDefault().FL_TIPO_LANC_MEDIA;

            string flLanc = (from tb01 in TB01_CURSO.RetornaTodosRegistros()
                             where tb01.CO_CUR == serie
                             select new
                             {
                                 tb01.CO_TIPO_LANC_NOTA
                             }).FirstOrDefault().CO_TIPO_LANC_NOTA;

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
            int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;
            var refer = ddlReferencia.SelectedValue;
            string anoRef = ddlAno.SelectedValue;

            grdBusca.DataKeyNames = new string[] { "CO_MAT" };

            string param = HttpContext.Current.Session["showAgrupadoras"].ToString();
            HttpContext.Current.Session.Remove("showAgrupadoras");

            //Verifica se o usuário deixou marcada a opção de gerar a grid com as disciplinas agrupadoras ou não
            if (param == "S")
            {
                var resultado = (from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                                 join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb079.CO_MAT equals tb02.CO_MAT
                                 join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                 join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on tb079.CO_MAT equals tb43.CO_MAT
                                 where tb079.CO_EMP == LoginAuxili.CO_EMP
                                 && tb43.FL_LANCA_NOTA == "S"
                                 && tb43.CO_MAT == tb079.CO_MAT
                                 && tb43.CO_CUR == tb079.CO_CUR
                                 && tb43.CO_ANO_GRADE == tb079.CO_ANO_REF
                                 && tb43.ID_MATER_AGRUP == null
                                 && tb079.CO_ANO_REF == anoRef
                                 && tb079.CO_CUR == serie
                                 && tb079.CO_TUR == turma
                                 && tb079.CO_MODU_CUR == modalidade
                                 && tb079.CO_ALU == coAlu
                                 select new GridSaida
                                 {
                                     VL_MAX = tb43.VL_NOTA_MAXIM,
                                     CO_MAT = tb079.CO_MAT,
                                     ordImp = tb43.CO_ORDEM_IMPRE ?? 20,
                                     NO_MATERIA = tb107.NO_MATERIA,
                                     NO_SIGLA_MATERIA = tb107.NO_SIGLA_MATERIA,
                                     MEDIA = refer == "B1" ? tb079.VL_NOTA_BIM1 : (refer == "B2" ? tb079.VL_NOTA_BIM2 : (refer == "B3" ? tb079.VL_NOTA_BIM3 : (refer == "B4" ? tb079.VL_NOTA_BIM4 : (refer == "T1" ? tb079.VL_NOTA_TRI1 : (refer == "T2" ? tb079.VL_NOTA_TRI2 : tb079.VL_NOTA_TRI3))))),
                                     FALTAS = refer == "B1" ? tb079.QT_FALTA_BIM1 : (refer == "B2" ? tb079.QT_FALTA_BIM2 : (refer == "B3" ? tb079.QT_FALTA_BIM3 : (refer == "B4" ? tb079.QT_FALTA_BIM4 : (refer == "T1" ? tb079.QT_FALTA_TRI1 : (refer == "T2" ? tb079.QT_FALTA_TRI2 : tb079.QT_FALTA_TRI3))))),
                                     CONCEITO = refer == "B1" ? tb079.VL_CONC_BIM1 : (refer == "B2" ? tb079.VL_CONC_BIM2 : (refer == "B3" ? tb079.VL_CONC_BIM3 : (refer == "B4" ? tb079.VL_CONC_BIM4 : (refer == "T1" ? tb079.VL_CONC_TRI1 : (refer == "T2" ? tb079.VL_CONC_TRI2 : tb079.VL_CONC_TRI3))))),
                                     CRIT = refer == "B1" ? (tb079.VL_CRIT_BIM1 == null ? "" : tb079.VL_CRIT_BIM1) : (refer == "B2" ? (tb079.VL_CRIT_BIM2 == null ? "" :
                                     tb079.VL_CRIT_BIM2) : (refer == "B3" ? (tb079.VL_CRIT_BIM3 == null ? "" : tb079.VL_CRIT_BIM3) : (tb079.VL_CRIT_BIM4 == null ? "" : tb079.VL_CRIT_BIM4))),
                                     RESP = refer == "B1" ? (tb079.VL_RESP_BIM1 == null ? "" : tb079.VL_RESP_BIM1) : (refer == "B2" ? (tb079.VL_RESP_BIM2 == null ? "" :
                                     tb079.VL_RESP_BIM2) : (refer == "B3" ? (tb079.VL_RESP_BIM3 == null ? "" : tb079.VL_RESP_BIM3) : (tb079.VL_RESP_BIM4 == null ? "" : tb079.VL_RESP_BIM4))),
                                     APRE = refer == "B1" ? (tb079.VL_APRE_BIM1 == null ? "" : tb079.VL_APRE_BIM1) : (refer == "B2" ? (tb079.VL_APRE_BIM2 == null ? "" :
                                     tb079.VL_APRE_BIM2) : (refer == "B3" ? (tb079.VL_APRE_BIM3 == null ? "" : tb079.VL_APRE_BIM3) : (tb079.VL_APRE_BIM4 == null ? "" : tb079.VL_APRE_BIM4))),
                                     AVAL = refer == "B1" ? tb079.DE_RES_AVAL_BIM1 : (refer == "B2" ? tb079.DE_RES_AVAL_BIM2 : (refer == "B3" ? tb079.DE_RES_AVAL_BIM3 : (refer == "B4" ? tb079.DE_RES_AVAL_BIM4 : (refer == "T1" ? tb079.DE_RES_AVAL_TRI1 : (refer == "T2" ? tb079.DE_RES_AVAL_TRI2 : tb079.DE_RES_AVAL_TRI3))))),
                                     FL_HOMOL = refer == "B1" ? tb079.FL_HOMOL_NOTA_BIM1 : (refer == "B2" ? tb079.FL_HOMOL_NOTA_BIM2 : (refer == "B3" ? tb079.FL_HOMOL_NOTA_BIM3 : (refer == "B4" ? tb079.FL_HOMOL_NOTA_BIM4 : (refer == "T1" ? tb079.FL_HOMOL_NOTA_TRI1 : (refer == "T2" ? tb079.FL_HOMOL_NOTA_TRI2 : tb079.FL_HOMOL_NOTA_TRI3)))))
                                 }).OrderBy(w => w.ordImp).ThenBy(r => r.NO_MATERIA);

                grdBusca.DataSource = resultado.Count() > 0 ? resultado : null;
                grdBusca.DataBind();
            }
            else
            {
                var resultado = (from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                                 join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb079.CO_MAT equals tb02.CO_MAT
                                 join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                 join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on tb079.CO_MAT equals tb43.CO_MAT
                                 where tb079.CO_EMP == LoginAuxili.CO_EMP
                                 && tb43.FL_LANCA_NOTA == "S"
                                 && tb43.CO_MAT == tb079.CO_MAT
                                 && tb43.CO_CUR == tb079.CO_CUR
                                 && tb43.CO_ANO_GRADE == tb079.CO_ANO_REF
                                 && ((tb43.FL_DISCI_AGRUPA == "N") || (tb43.FL_DISCI_AGRUPA == null))
                                 && tb079.CO_ANO_REF == anoRef
                                 && tb079.CO_CUR == serie
                                 && tb079.CO_TUR == turma
                                 && tb079.CO_MODU_CUR == modalidade
                                 && tb079.CO_ALU == coAlu
                                 select new GridSaida
                                 {
                                     VL_MAX = tb43.VL_NOTA_MAXIM,
                                     CO_MAT = tb079.CO_MAT,
                                     ordImp = tb43.CO_ORDEM_IMPRE ?? 20,
                                     NO_MATERIA = tb107.NO_MATERIA,
                                     NO_SIGLA_MATERIA = tb107.NO_SIGLA_MATERIA,
                                     MEDIA = refer == "B1" ? tb079.VL_NOTA_BIM1 : (refer == "B2" ? tb079.VL_NOTA_BIM2 : (refer == "B3" ? tb079.VL_NOTA_BIM3 : (refer == "B4" ? tb079.VL_NOTA_BIM4 : (refer == "T1" ? tb079.VL_NOTA_TRI1 : (refer == "T2" ? tb079.VL_NOTA_TRI2 : tb079.VL_NOTA_TRI3))))),
                                     FALTAS = refer == "B1" ? tb079.QT_FALTA_BIM1 : (refer == "B2" ? tb079.QT_FALTA_BIM2 : (refer == "B3" ? tb079.QT_FALTA_BIM3 : (refer == "B4" ? tb079.QT_FALTA_BIM4 : (refer == "T1" ? tb079.QT_FALTA_TRI1 : (refer == "T2" ? tb079.QT_FALTA_TRI2 : tb079.QT_FALTA_TRI3))))),
                                     CONCEITO = refer == "B1" ? tb079.VL_CONC_BIM1 : (refer == "B2" ? tb079.VL_CONC_BIM2 : (refer == "B3" ? tb079.VL_CONC_BIM3 : (refer == "B4" ? tb079.VL_CONC_BIM4 : (refer == "T1" ? tb079.VL_CONC_TRI1 : (refer == "T2" ? tb079.VL_CONC_TRI2 : tb079.VL_CONC_TRI3))))),
                                     CRIT = refer == "B1" ? (tb079.VL_CRIT_BIM1 == null ? "" : tb079.VL_CRIT_BIM1) : (refer == "B2" ? (tb079.VL_CRIT_BIM2 == null ? "" :
                                     tb079.VL_CRIT_BIM2) : (refer == "B3" ? (tb079.VL_CRIT_BIM3 == null ? "" : tb079.VL_CRIT_BIM3) : (tb079.VL_CRIT_BIM4 == null ? "" : tb079.VL_CRIT_BIM4))),
                                     RESP = refer == "B1" ? (tb079.VL_RESP_BIM1 == null ? "" : tb079.VL_RESP_BIM1) : (refer == "B2" ? (tb079.VL_RESP_BIM2 == null ? "" :
                                     tb079.VL_RESP_BIM2) : (refer == "B3" ? (tb079.VL_RESP_BIM3 == null ? "" : tb079.VL_RESP_BIM3) : (tb079.VL_RESP_BIM4 == null ? "" : tb079.VL_RESP_BIM4))),
                                     APRE = refer == "B1" ? (tb079.VL_APRE_BIM1 == null ? "" : tb079.VL_APRE_BIM1) : (refer == "B2" ? (tb079.VL_APRE_BIM2 == null ? "" :
                                     tb079.VL_APRE_BIM2) : (refer == "B3" ? (tb079.VL_APRE_BIM3 == null ? "" : tb079.VL_APRE_BIM3) : (tb079.VL_APRE_BIM4 == null ? "" : tb079.VL_APRE_BIM4))),
                                     AVAL = refer == "B1" ? tb079.DE_RES_AVAL_BIM1 : (refer == "B2" ? tb079.DE_RES_AVAL_BIM2 : (refer == "B3" ? tb079.DE_RES_AVAL_BIM3 : (refer == "B4" ? tb079.DE_RES_AVAL_BIM4 : (refer == "T1" ? tb079.DE_RES_AVAL_TRI1 : (refer == "T2" ? tb079.DE_RES_AVAL_TRI2 : tb079.DE_RES_AVAL_TRI3))))),
                                     FL_HOMOL = refer == "B1" ? tb079.FL_HOMOL_NOTA_BIM1 : (refer == "B2" ? tb079.FL_HOMOL_NOTA_BIM2 : (refer == "B3" ? tb079.FL_HOMOL_NOTA_BIM3 : (refer == "B4" ? tb079.FL_HOMOL_NOTA_BIM4 : (refer == "T1" ? tb079.FL_HOMOL_NOTA_TRI1 : (refer == "T2" ? tb079.FL_HOMOL_NOTA_TRI2 : tb079.FL_HOMOL_NOTA_TRI3)))))
                                 }).OrderBy(w => w.ordImp).ThenBy(r => r.NO_MATERIA);

                grdBusca.DataSource = resultado.Count() > 0 ? resultado : null;
                grdBusca.DataBind();
            }

            VerificaFaltas();
        }

        public class GridSaida
        {
            public decimal? VL_MAX { get; set; }
            public int? ordImp { get; set; }
            public int CO_MAT { get; set; }
            public string NO_MATERIA { get; set; }
            public string NO_SIGLA_MATERIA { get; set; }
            public string NO_ALU { get; set; }
            public decimal? MEDIA { get; set; }
            public int? FALTAS { get; set; }
            public string CONCEITO { get; set; }
            public string CRIT { get; set; }
            public string RESP { get; set; }
            public string APRE { get; set; }
            public string AVAL { get; set; }
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
            int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;
            var refer = ddlReferencia.SelectedValue;
            string anoRef = ddlAno.SelectedValue;

            foreach (GridViewRow li in grdBusca.Rows)
            {
                //Faz o Cálculo apenas se a nota e a falta ainda não tiverem sido lançadas
                string nota = (((TextBox)li.Cells[2].FindControl("txtMedia")).Text);
                string faltas = (((TextBox)li.Cells[3].FindControl("txtFaltas")).Text);
                int coMat = int.Parse((((HiddenField)li.Cells[2].FindControl("hidCoMat")).Value));
                TextBox txtFlt = ((TextBox)li.Cells[3].FindControl("txtFaltas"));

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
                               && tb132.CO_BIMESTRE == refer
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

        //====> Método que carrega o DropDown de Medidas
        private void CarregaMedidas()
        {
            string tpRefer = QueryStringAuxili.RetornaQueryStringPelaChave("tpRef");
            AuxiliCarregamentos.CarregaMedidasTemporais(ddlReferencia, false, tpRefer, true);
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            int ano =  Int32.Parse(QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Ano));
            int mod =  QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoModuCur);
            
            var tb44 = (TB44_MODULO)null;
            if (LoginAuxili.FLA_PROFESSOR != "S")
            {
                tb44 = TB44_MODULO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoModuCur));
            }
            else
            {
                tb44 = (from rm in TB_RESPON_MATERIA.RetornaTodosRegistros()
                        join mo in TB44_MODULO.RetornaTodosRegistros() on rm.CO_MODU_CUR equals mo.CO_MODU_CUR
                        where rm.CO_MODU_CUR == mod
                        && rm.CO_COL_RESP == LoginAuxili.CO_COL
                        && rm.CO_ANO_REF == ano 
                        select mo).Distinct().FirstOrDefault();
            }

            if (tb44 != null)
            {
                ddlModalidade.Items.Insert(0, new ListItem(tb44.DE_MODU_CUR, tb44.CO_MODU_CUR.ToString()));
            }
            else
            {
                ddlModalidade.Items.Insert(0, new ListItem("Todos", "0"));
            }
            
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {
            int ano = Int32.Parse(QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Ano));
            int mod = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoCur);
            var tb01 = (TB01_CURSO)null;
            if (LoginAuxili.FLA_PROFESSOR != "S")
            {
                tb01 = TB01_CURSO.RetornaPeloCoCur(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoCur));

            }
            else
            {
                tb01 = (from rm in TB_RESPON_MATERIA.RetornaTodosRegistros()
                        join mo in TB01_CURSO.RetornaTodosRegistros() on rm.CO_CUR equals mo.CO_CUR
                        where rm.CO_COL_RESP == LoginAuxili.CO_COL
                        && rm.CO_CUR == mod
                        && rm.CO_ANO_REF == ano
                        select mo).Distinct().FirstOrDefault();


            }
            if (tb01 != null)
            {
                ddlSerieCurso.Items.Insert(0, new ListItem(tb01.NO_CUR, tb01.CO_CUR.ToString()));
            }
            else
            {
                ddlSerieCurso.Items.Insert(0, new ListItem("Todos","0"));
            }
           
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        private void CarregaTurma()
        {
            
            int ano = Int32.Parse(QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Ano));
            int mod = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoTur);

            
            var tb129 = (TB129_CADTURMAS)null;

            if (LoginAuxili.FLA_PROFESSOR != "S")
            {
                tb129 = TB129_CADTURMAS.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoTur));
            }
            else
            {
                tb129 = (from rm in TB_RESPON_MATERIA.RetornaTodosRegistros()
                         join mo in TB129_CADTURMAS.RetornaTodosRegistros() on rm.CO_TUR equals mo.CO_TUR
                         where rm.CO_COL_RESP == LoginAuxili.CO_COL
                         && rm.CO_TUR == mod
                         && rm.CO_ANO_REF == ano
                         select mo).Distinct().FirstOrDefault();
            }

            if (tb129 != null)
            {
                ddlTurma.Items.Insert(0, new ListItem(tb129.NO_TURMA, tb129.CO_TUR.ToString()));
            }
            else
            {
                ddlTurma.Items.Insert(0, new ListItem("Todos", "0"));
            }
         
            
        }

        /// <summary>
        /// Verifica o tipo de lançamento selecionado e altera a grid de acordo com o que o usuário precisa.
        /// </summary>
        private void VerificaTPLanc()
        {
            if (ddlTpLanc.SelectedValue == "N")
            {
                //--------> Varra toda a grid de Busca
                foreach (GridViewRow linha in grdBusca.Rows)
                {
                    TextBox txtme = ((TextBox)linha.Cells[2].FindControl("txtMedia"));
                    txtme.Enabled = txtme.Visible = true;

                    DropDownList ddlcn = ((DropDownList)linha.Cells[3].FindControl("ddlConceito"));
                    ddlcn.Enabled = ddlcn.Visible = false;
                }

                //Muda o título da coluna de acordo com o que é selecionado no tipo de lançamento
                grdBusca.Columns[2].HeaderText = "Média";
            }
            else
            {
                //--------> Varra toda a grid de Busca
                foreach (GridViewRow linha in grdBusca.Rows)
                {
                    TextBox txtme = ((TextBox)linha.Cells[2].FindControl("txtMedia"));
                    txtme.Enabled = txtme.Visible = false;

                    DropDownList ddlcn = ((DropDownList)linha.Cells[3].FindControl("ddlConceito"));
                    ddlcn.Enabled = ddlcn.Visible = true;
                }

                //Muda o título da coluna de acordo com o que é selecionado no tipo de lançamento
                grdBusca.Columns[2].HeaderText = "Conceito";
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        private void CarregaAluno()
        {
            var tb07 = TB07_ALUNO.RetornaPeloCoAlu(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoAlu));
            ddlAluno.Items.Insert(0, new ListItem(tb07.NO_ALU, tb07.CO_ALU.ToString()));
        }

        private void CarregaAvaliacao()
        {
            int coAlu = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoAlu);
            int modalidade = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoModuCur);
            int serie = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoCur);
            string anoRef = QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Ano);
            int turma = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoTur);

            //------------> Recebe o bimestre
            var refer = ddlReferencia.SelectedValue;

            var res = (from tb126 in TB126_HIST_ALUNO_AVAL.RetornaTodosRegistros()
                       where tb126.CO_ALU == coAlu
                       && tb126.CO_MODU_CUR == modalidade
                       && tb126.CO_CUR == serie
                       && tb126.CO_TUR == turma
                       && tb126.CO_ANO_REF == anoRef
                       && tb126.CO_BIMESTRE == refer
                       select new
                       {
                           tb126.DE_AVAL
                       }).FirstOrDefault();

            if (res != null)
                txtAvalGeral.Text = res.DE_AVAL;
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
                string conc = ((HiddenField)e.Row.Cells[2].FindControl("hidConceito")).Value;

                CarregaConceito(ddl, conc);


                if (ddlTpLanc.SelectedValue == "N")
                {
                    if (((HiddenField)e.Row.Cells[2].FindControl("hidFlHomol")).Value == "S")
                    {
                        ((TextBox)e.Row.Cells[2].FindControl("txtMedia")).Enabled = false;
                        ((DropDownList)e.Row.Cells[3].FindControl("ddlConceito")).Enabled = false;
                    }
                    else
                    {
                        ((TextBox)e.Row.Cells[2].FindControl("txtMedia")).Enabled = true;
                        ((DropDownList)e.Row.Cells[3].FindControl("ddlConceito")).Enabled = false;
                    }
                    grdBusca.Columns[2].HeaderText = "Média";
                }
                else
                {
                    if (((HiddenField)e.Row.Cells[2].FindControl("hidFlHomol")).Value == "S")
                    {
                        ((TextBox)e.Row.Cells[2].FindControl("txtMedia")).Enabled = false;
                        ((DropDownList)e.Row.Cells[3].FindControl("ddlConceito")).Enabled = false;
                    }
                    else
                    {
                        ((TextBox)e.Row.Cells[2].FindControl("txtMedia")).Enabled = false;
                        ((DropDownList)e.Row.Cells[3].FindControl("ddlConceito")).Enabled = true;
                    }
                    grdBusca.Columns[2].HeaderText = "Conceito";
                }
            }
        }


    }
}