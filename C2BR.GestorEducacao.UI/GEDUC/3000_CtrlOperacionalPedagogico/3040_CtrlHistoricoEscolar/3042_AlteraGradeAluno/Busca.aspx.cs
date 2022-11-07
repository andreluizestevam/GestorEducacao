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
//18/02/2014| Débora Lohane              | Funcionalidade que permite a alteração de grade do aluno

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

namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3040_CtrlHistoricoEscolar._3042_AlteraGradeAluno
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaAnos();
                CarregaModalidades();
                CarregaSeries();
                CarregaTurma();
                CarregaAluno();
            }

        }
        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_ALU", "CO_MODU_CUR", "CO_CUR", "CO_TUR", "CO_ANO_MES_MAT" };

            BoundField bf2 = new BoundField();
            bf2.DataField = "NU_NIRE";
            bf2.HeaderText = "NIRE";
            bf2.ItemStyle.CssClass = "numeroCol";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf2);

            BoundField bf1 = new BoundField();
            bf1.DataField = "NO_ALU";
            bf1.HeaderText = "Aluno";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf1);

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CPF",
                HeaderText = "CPF"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            string ano = (ddlAno.SelectedValue);
            int turma = int.Parse(ddlTurma.SelectedValue);
            int coAlu = int.Parse(ddlAluno.SelectedValue);
            int coModuCur = int.Parse(ddlModalidade.SelectedValue);
            int coSerie = int.Parse(ddlSerie.SelectedValue);
            var resultado = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                             join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb08.TB07_ALUNO.CO_ALU equals tb07.CO_ALU
                             where tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                             && (coAlu == 0 ? 0 == 0 : tb08.CO_ALU == coAlu)
                             && (tb08.CO_SIT_MAT != "C" && tb08.CO_SIT_MAT != "R")
                             && (coModuCur == 0 ? 0 == 0 : tb08.TB44_MODULO.CO_MODU_CUR == coModuCur)
                             && (tb08.CO_ANO_MES_MAT == ano)
                             && (turma == 0 ? 0 == 0 : tb08.CO_TUR == turma)
                             && (coSerie == 0 ? 0 == 0 : tb08.CO_CUR == coSerie)
                             select new
                             {
                                 tb08.TB07_ALUNO.NU_NIRE,
                                 tb08.CO_ALU,
                                 tb08.TB07_ALUNO.NO_ALU,
                                 tb08.TB44_MODULO.CO_MODU_CUR,
                                 tb08.CO_CUR,
                                 tb08.CO_TUR,
                                 tb08.CO_ANO_MES_MAT,
                                 CPF = tb07.NU_CPF_ALU,
                             }).OrderBy(m => m.NO_ALU).DistinctBy(d => d.CO_ALU).ToList();

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;

            //if (resultado.Count > 0)
            //    CurrentPadraoBuscas.GridBusca.DataSource = resultado.ToList();
            //else
            //    CurrentPadraoBuscas.GridBusca.DataSource = null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoAlu, "CO_ALU"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoModuCur, "CO_MODU_CUR"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoCur, "CO_CUR"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Ano, "CO_ANO_MES_MAT"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoTur, "CO_TUR"));
            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        //public class SaidaAluno
        //{
        //    public string nome { get; set; }
        //    public string nire { get; set; }
        //    public int cpfReceb { get; set; }
        //    public string CPF
        //    {
        //        get
        //        {
        //            return this.cpfReceb.ToString()
        //        }
        //    }
        //}

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o DropDown de Modalidades
        /// </summary>
        private void CarregaAnos()
        {
            AuxiliCarregamentos.CarregaAno(ddlAno, LoginAuxili.CO_EMP, false);
        }
        private void CarregaModalidades()
        {
            AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        /// <summary>
        /// Metodo que carrega as séries de acordo com a modalidade
        /// </summary>
        private void CarregaSeries()
        {
            int coMod = int.Parse(ddlModalidade.SelectedValue);
            AuxiliCarregamentos.carregaSeriesCursos(ddlSerie, coMod, LoginAuxili.CO_EMP, true);
        }
        private void CarregaTurma()
        {
            int coEmp = LoginAuxili.CO_EMP;
            int coMod = int.Parse(ddlModalidade.SelectedValue);
            int coCur = int.Parse(ddlSerie.SelectedValue);
            AuxiliCarregamentos.CarregaTurmas(ddlTurma, LoginAuxili.CO_EMP, coMod, coCur, true);
        }

        /// <summary>
        ///  Método que carrega o DropDown de Alunos
        /// </summary>
        private void CarregaAluno()
        {
            int coEmp = LoginAuxili.CO_EMP;
            int coMod = int.Parse(ddlModalidade.SelectedValue);
            int coCur = int.Parse(ddlSerie.SelectedValue);
            int coTur = int.Parse(ddlTurma.SelectedValue);
            string coAno = ddlAno.SelectedValue;
            AuxiliCarregamentos.CarregaAlunoMatriculado(ddlAluno, LoginAuxili.CO_EMP, coMod, coCur, coTur, coAno, true);
        }

        #endregion

        #region Eventos dos componentes usado na página
        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "")
                CarregaSeries();
        }

        protected void ddlSerie_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "")
            {
                CarregaAluno();
                CarregaTurma();
            }
        }
        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "")
                CarregaAluno();
        }

        #endregion

    }
}