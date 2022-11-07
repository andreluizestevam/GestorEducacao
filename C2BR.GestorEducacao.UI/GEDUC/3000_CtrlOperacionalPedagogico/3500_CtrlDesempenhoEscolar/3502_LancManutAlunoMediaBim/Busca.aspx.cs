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
// 26/07/2016| Filipe Rodrigues           | Alterações para incluir trimestres
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3500_CtrlDesempenhoEscolar.F3502_LancManutAlunoMediaBim
{
    public partial class Busca : System.Web.UI.Page
    {
        public PadraoBuscas CurrentPadraoBuscas { get { return ((PadraoBuscas)this.Master); } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            CurrentPadraoBuscas.OnAcaoBuscaDefineGridView += new PadraoBuscas.OnAcaoBuscaDefineGridViewHandler(CurrentPadraoBuscas_OnAcaoBuscaDefineGridView);
            CurrentPadraoBuscas.OnDefineColunasGridView += new PadraoBuscas.OnDefineColunasGridViewHandler(CurrentPadraoBuscas_OnDefineColunasGridView);
            CurrentPadraoBuscas.OnDefineQueryStringIds += new PadraoBuscas.OnDefineQueryStringIdsHandler(CurrentPadraoBuscas_OnDefineQueryStringIds);
        }

        protected void Page_Load()
        {
            if (!IsPostBack)
            {
                bool reca = CarregaParametros();

                if (reca == false)
                {
                    CarregaAnos();
                    //CarregaTiposMedidas();
                    CarregaMedidas();
                    CarregaModalidades();
                    CarregaSerieCurso();
                    CarregaTurma();

                    ddlAno.SelectedValue = DateTime.Now.Year.ToString();
                }
            }
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

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {                                                                               //"TP_REF",
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_ANO_MES_MAT",  "CO_REF", "CO_MODU_CUR", "CO_CUR", "CO_TUR", "CO_ALU" };

            BoundField bf2 = new BoundField();
            bf2.DataField = "NU_NIRE";
            bf2.HeaderText = "NIRE";
            bf2.ItemStyle.CssClass = "numeroCol";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf2);

            BoundField bf1 = new BoundField();
            bf1.DataField = "NO_ALU";
            bf1.HeaderText = "Aluno";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf1);

            BoundField bf3 = new BoundField();
            bf3.DataField = "CO_SITUA";
            bf3.HeaderText = "STATUS";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf3);
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            //var tpRefer = ddlTipoMedida.SelectedValue;
            var coRefer = ddlReferencia.SelectedValue;

            var resultado = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                             where tb08.CO_ANO_MES_MAT == ddlAno.SelectedValue && tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP && tb08.CO_TUR == turma &&
                             tb08.TB44_MODULO.CO_MODU_CUR == modalidade && tb08.CO_CUR == serie && (tb08.CO_SIT_MAT == "A" || tb08.CO_SIT_MAT == "R")
                             select new
                             {
                                 tb08.TB07_ALUNO.NU_NIRE,
                                 tb08.TB07_ALUNO.CO_ALU,
                                 tb08.TB07_ALUNO.NO_ALU,
                                 tb08.CO_ANO_MES_MAT,
                                 //TP_REF = tpRefer,
                                 CO_REF = coRefer,
                                 tb08.TB44_MODULO.CO_MODU_CUR,
                                 tb08.CO_CUR,
                                 tb08.CO_TUR,
                                 CO_SITUA = (tb08.CO_SIT_MAT == "A" ? "Matriculado" : tb08.CO_SIT_MAT == "C" ? "Cancelado" : tb08.CO_SIT_MAT == "X" ? "Transferido" :
                               tb08.CO_SIT_MAT == "T" ? "MT" : tb08.CO_SIT_MAT == "D" ? "DE" : tb08.CO_SIT_MAT == "I" ? "IN" :
                               tb08.CO_SIT_MAT == "J" ? "JU" : tb08.CO_SIT_MAT == "Y" ? "TC" : "SM"),
                             }).OrderBy(r => r.NO_ALU);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Ano, "CO_ANO_MES_MAT"));
            //queryStringKeys.Add(new KeyValuePair<string, string>("tpRef", "TP_REF"));
            queryStringKeys.Add(new KeyValuePair<string, string>("ref", "CO_REF"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoModuCur, "CO_MODU_CUR"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoCur, "CO_CUR"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoTur, "CO_TUR"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoAlu, "CO_ALU"));
            HttpContext.Current.Session.Add("showAgrupadoras", (chkMosAgrupa.Checked ? "S" : "N"));
            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        /// <summary>
        /// Método responsável por recarregar determinadas informações, caso isso tenha previamente sido inserido em uma variável de sessão
        /// </summary>
        /// <returns></returns>
        private bool CarregaParametros()
        {
            bool persist = false;
            if (HttpContext.Current.Session["buscaLancMediasBimestrais"] != null)
            {
                var parametros = HttpContext.Current.Session["buscaLancMediasBimestrais"].ToString();

                if (!string.IsNullOrEmpty(parametros))
                {
                    var par = parametros.ToString().Split(';');
                    var ano = par[0];
                    var coTpMed = par[1];
                    var coRefer = par[2];
                    var modalidade = par[3];
                    var serieCurso = par[4];
                    var turma = par[5];

                    CarregaAnos();
                    ddlAno.SelectedValue = ano;


                    //CarregaTiposMedidas();
                    //ddlTipoMedida.SelectedValue = coTpMed;
                    //CarregaMedidas();
                    ddlReferencia.SelectedValue = coRefer;

                    CarregaModalidades();
                    ddlModalidade.SelectedValue = modalidade;

                    CarregaSerieCurso();
                    ddlSerieCurso.SelectedValue = serieCurso;

                    CarregaTurma();
                    ddlTurma.SelectedValue = turma;

                    CurrentPadraoBuscas_OnAcaoBuscaDefineGridView();

                    persist = true;
                    HttpContext.Current.Session.Remove("buscaLancMediasBimestrais");
                    CurrentPadraoBuscas_OnAcaoBuscaDefineGridView();
                }
            }
            return persist;
        }

        #region Carregamento DropDown

        //====> Método que carrega o DropDown de Anos
        private void CarregaAnos()
        {
            AuxiliCarregamentos.CarregaAnoGrdCurs(ddlAno, LoginAuxili.CO_EMP, false);
        }

        //====> Método que carrega o DropDown de Tipos de Medidas
        //private void CarregaTiposMedidas()
        //{
        //    AuxiliCarregamentos.CarregaTiposMedidasTemporais(ddlTipoMedida, false);
        //}

        //====> Método que carrega o DropDown de Medidas
        //private void CarregaMedidas()
        //{
        //    AuxiliCarregamentos.CarregaMedidasTemporais(ddlReferencia, false, ddlTipoMedida.SelectedValue, true);
        //}

        //====> Método que carrega o DropDown de Modalidades, verifica se o usuário logado é professor.
        private void CarregaModalidades()
        {
            int ano = (ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) :0);

            if (LoginAuxili.FLA_PROFESSOR != "S")
                AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, false);
            else
                AuxiliCarregamentos.carregaModalidadesProfeResp(ddlModalidade, LoginAuxili.CO_COL, ano, false);

            CarregaSerieCurso();
        }

        //====> Método que carrega o DropDown de Séries, verifica se o usuário logado é professor.
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            string coAnoGrade = ddlAno.SelectedValue;
            int ano = (ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0);
            if (LoginAuxili.FLA_PROFESSOR != "S")
                AuxiliCarregamentos.carregaSeriesCursos(ddlSerieCurso, modalidade, LoginAuxili.CO_EMP, false);
            else
                AuxiliCarregamentos.carregaSeriesCursosProfeResp(ddlSerieCurso, modalidade, LoginAuxili.CO_COL, ano, false);

            CarregaTurma();
        }
        //====> Método que carrega o DropDown de Turmas, verifica se o usuário logado é professor.

        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int ano = int.Parse(ddlAno.SelectedValue);
            if (LoginAuxili.FLA_PROFESSOR != "S")
                AuxiliCarregamentos.CarregaTurmas(ddlTurma, LoginAuxili.CO_EMP, modalidade, serie, false);
            else
                AuxiliCarregamentos.CarregaTurmasProfeResp(ddlTurma, LoginAuxili.CO_EMP, modalidade, serie, LoginAuxili.CO_COL, ano, false);
        }
        #endregion

        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
        }

        //protected void ddlTipoMedida_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    CarregaMedidas();
        //}

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
        }
    }
}