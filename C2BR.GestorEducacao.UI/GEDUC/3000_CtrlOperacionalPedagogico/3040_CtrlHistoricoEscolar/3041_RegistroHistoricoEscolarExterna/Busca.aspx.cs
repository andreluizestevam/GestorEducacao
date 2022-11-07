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
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//26/08/2014| Maxwell Almeida            | Alteração nos métodos de carregamentos existentes na página

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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3040_CtrlHistoricoEscolar.F3041_RegistroHistoricoEscolarExterna
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
                AuxiliCarregamentos.CarregaAno(ddlAno, LoginAuxili.CO_EMP, true);
                CarregaModalidades();
                CarregaSeries();
                CarregaAluno();
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_ALU", "CO_MODU_CUR", "CO_CUR", "CO_ANO_MES_MAT" };

            BoundField bf2 = new BoundField();
            bf2.DataField = "NU_NIRE";
            bf2.HeaderText = "NIRE";
            bf2.ItemStyle.CssClass = "numeroCol";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf2);

            BoundField bf5 = new BoundField();
            bf5.DataField = "CO_SITUA";
            bf5.HeaderText = "Situação";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf5);

            BoundField bf1 = new BoundField();
            bf1.DataField = "NO_ALU";
            bf1.HeaderText = "Aluno";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf1);

            BoundField bf3 = new BoundField();
            bf3.DataField = "DE_MODU_CUR";
            bf3.HeaderText = "Modalidade";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf3);

            BoundField bf4 = new BoundField();
            bf4.DataField = "NO_CUR";
            bf4.HeaderText = "Curso";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf4);
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coAlu = int.Parse(ddlAluno.SelectedValue);
            int coModuCur = int.Parse(ddlModalidade.SelectedValue);
            int coSerie = int.Parse(ddlSerie.SelectedValue);
            string coAno = ddlAno.SelectedValue;
            var resultado = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                             join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb08.CO_CUR equals tb01.CO_CUR
                             where tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                             && (tb08.CO_ANO_MES_MAT == coAno)
                             && (coAlu == 0 ? 0 == 0 : tb08.CO_ALU == coAlu)
                             && (tb08.CO_SIT_MAT != "C" && tb08.CO_SIT_MAT != "R")
                             && (coModuCur == 0 ? 0 == 0 : tb08.TB44_MODULO.CO_MODU_CUR == coModuCur)
                             && (coSerie == 0 ? 0 == 0 : tb08.CO_CUR == coSerie)
                             select new saida
                             {
                                 NU_NIRE_RECEB = tb08.TB07_ALUNO.NU_NIRE,
                                 CO_ALU = tb08.CO_ALU,
                                 NO_ALU = tb08.TB07_ALUNO.NO_ALU,
                                 CO_MODU_CUR = tb08.TB44_MODULO.CO_MODU_CUR,
                                 CO_CUR = tb08.CO_CUR,
                                 CO_ANO_MES_MAT = tb08.CO_ANO_MES_MAT,
                                 DE_MODU_CUR = tb08.TB44_MODULO.DE_MODU_CUR,
                                 NO_CUR = tb01.NO_CUR,
                                 CO_SITUA = (tb08.CO_SIT_MAT == "A" ? "Matriculado" : tb08.CO_SIT_MAT == "C" ? "Cancelado" : tb08.CO_SIT_MAT == "X" ? "Transferido" :
                               tb08.CO_SIT_MAT == "T" ? "MT" : tb08.CO_SIT_MAT == "D" ? "DE" : tb08.CO_SIT_MAT == "I" ? "IN" :
                               tb08.CO_SIT_MAT == "J" ? "JU" : tb08.CO_SIT_MAT == "Y" ? "TC" : "SM"),
                             }).OrderBy(w=>w.DE_MODU_CUR).ThenBy(y=>y.NO_CUR).ThenBy(m => m.NO_ALU).DistinctBy(d => d.CO_ALU).DefaultIfEmpty();

            CurrentPadraoBuscas.GridBusca.DataSource = ((resultado != null && resultado.Count() > 0)) ? resultado.ToList() : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoAlu, "CO_ALU"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoModuCur, "CO_MODU_CUR"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoCur, "CO_CUR"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Ano, "CO_ANO_MES_MAT"));
            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        public class saida
        {
            public int NU_NIRE_RECEB { get; set; }
            public string NU_NIRE {
                get
                {
                    string nire = this.NU_NIRE_RECEB.ToString().PadLeft(7, '0');
                    return nire;
                }
            }
            public string CO_SITUA { get; set; }
            public int CO_ALU { get; set; }
            public string NO_ALU { get; set; }
            public int CO_MODU_CUR { get; set; }
            public int CO_CUR { get; set; }
            public string CO_ANO_MES_MAT { get; set; }
            public string DE_MODU_CUR { get; set; }
            public string NO_CUR { get; set; }
        }

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o DropDown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        /// <summary>
        /// Metodo que carrega as séries de acordo com a modalidade
        /// </summary>
        private void CarregaSeries()
        {
            int mod = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            AuxiliCarregamentos.carregaSeriesCursos(ddlSerie, mod, LoginAuxili.CO_EMP, true);
        }

        /// <summary>
        ///  Método que carrega o DropDown de Alunos
        /// </summary>
        private void CarregaAluno()
        {
            int mod = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int ser = ddlSerie.SelectedValue != "" ? int.Parse(ddlSerie.SelectedValue) : 0;
            string ano = ddlAno.SelectedValue;
            AuxiliCarregamentos.CarregaAlunoMatriculadoSemTurma(ddlAluno, LoginAuxili.CO_EMP, mod, ser, ano, true);
        }

        #endregion

        #region Eventos dos componentes usado na página
        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSeries();
        }

        protected void ddlSerie_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAluno();
        }
        #endregion
    }
}