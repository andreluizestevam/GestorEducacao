//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: MATRÍCULA NOVA PARA ALUNO
// OBJETIVO: ALTERAÇÃO DE STATUS DE MATRÍCULA
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
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
namespace C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2107_MatriculaAluno.AlteraStatusMatricula
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
            if (Page.IsPostBack)
                return;

            CarregaAno();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_ALU", "CO_ANO_MES_MAT", "CO_CUR", "NU_SEM_LET", "CO_MODU_CUR", "CO_SIT_MAT" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NU_NIRE",
                    HeaderText = "Nire"

            });
            
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_ALU_CAD",
                HeaderText = "Matrícula"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_MODU_CUR",
               
                HeaderText = "Modalidade"

            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_CUR",
                HeaderText = "Serie"

            });
            
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_ALU",
                HeaderText = "Nome"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_SIT_MAT",
                HeaderText = "Situação"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string anoMatricula = ddlAno.SelectedValue != "" ? ddlAno.SelectedValue : "0";
            string situacao = ddlSituacao.SelectedValue != "" ? ddlSituacao.SelectedValue : "-1";

            var resultado = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                             join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb08.CO_CUR equals tb01.CO_CUR
                             join alu in TB07_ALUNO.RetornaTodosRegistros() on tb08.CO_ALU equals alu.CO_ALU
                             where tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                             && tb08.CO_ANO_MES_MAT == anoMatricula 
                             && tb08.TB44_MODULO.CO_MODU_CUR == modalidade 
                             && tb08.CO_TUR == turma 
                             && tb08.CO_CUR == serie
                             && (situacao == "-1" ? 0 == 0 : tb08.CO_SIT_MAT == situacao)
                             select new
                             {
                                tb08.CO_ALU, 
                                tb08.CO_ANO_MES_MAT, 
                                tb08.TB07_ALUNO.NO_ALU, 
                                CO_ALU_CAD = tb08.CO_ALU_CAD.Insert(2, ".").Insert(6, "."),                                    
                                CO_SIT_MAT = (tb08.CO_SIT_MAT == "F" ? "Finalizado" : 
                                (tb08.CO_SIT_MAT == "A" ? "Matriculado" :
                                (tb08.CO_SIT_MAT == "X" ? "Transferido" :
                                (tb08.CO_SIT_MAT == "C" ? "Cancelado" : (tb08.CO_SIT_MAT == "R" ? "Pré-Matrícula" : "Trancado"))))),
                                tb01.NO_CUR,
                                tb08.CO_CUR,
                                alu.NU_NIRE,
                                tb08.TB44_MODULO.DE_MODU_CUR,
                                tb08.TB44_MODULO.CO_MODU_CUR,
                                tb08.NU_SEM_LET
                             }).OrderBy( m => m.NO_ALU );

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoCur, "CO_CUR"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoAlu, "CO_ALU"));
            queryStringKeys.Add(new KeyValuePair<string, string>("nuSemLet", "NU_SEM_LET"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Ano, "CO_ANO_MES_MAT"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoModuCur, "CO_MODU_CUR"));
            queryStringKeys.Add(new KeyValuePair<string, string>("tipoMatri", "CO_SIT_MAT"));
            

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o DropDown de Anos
        /// </summary>
        private void CarregaAno()
        {
            ddlAno.Items.Clear();
            ddlAno.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                 select new { ANO_CAD_MAT = tb08.CO_ANO_MES_MAT }).Distinct().OrderByDescending(m => m.ANO_CAD_MAT);

            ddlAno.DataTextField = "ANO_CAD_MAT";
            ddlAno.DataValueField = "ANO_CAD_MAT";
            ddlAno.DataBind();

            ddlAno.Items.Insert(0, new ListItem("Selecione",""));
            ddlAno.SelectedValue = "";

            ddlModalidade.Items.Clear();
            ddlSerieCurso.Items.Clear();
            ddlTurma.Items.Clear();
            ddlSituacao.Items.Clear();
        }

        /// <summary>
        /// Método que carrega o DropDown de Modalidades
        /// </summary>
        private void CarregaModalidade()
        {
            ddlModalidade.Items.Clear();
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where( m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();

            ddlModalidade.Items.Insert(0, new ListItem("Selecione", ""));
            ddlModalidade.SelectedValue = "";

            ddlSerieCurso.Items.Clear();
            ddlTurma.Items.Clear();
            ddlSituacao.Items.Clear();
        }

        /// <summary>
        /// Método que carrega o DropDown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            string anoGrade = ddlAno.SelectedValue;

            if (modalidade != 0)
            {
                ddlSerieCurso.Items.Clear();
                ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                            where tb01.CO_MODU_CUR == modalidade
                                            join tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on tb01.CO_CUR equals tb43.CO_CUR
                                            where tb43.CO_ANO_GRADE == anoGrade && tb43.TB44_MODULO.CO_MODU_CUR == tb01.CO_MODU_CUR
                                            select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct().OrderBy( c => c.NO_CUR );

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();
                ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
                ddlSerieCurso.SelectedValue = "";

                ddlTurma.Items.Clear();
                ddlSituacao.Items.Clear();

            }
                

            
        }

        /// <summary>
        /// Método que carrega o DropDown de Turmas
        /// </summary>
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            if ((modalidade != 0) && (serie != 0))
            {
                ddlTurma.Items.Clear();
                ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                       where tb06.CO_MODU_CUR == modalidade && tb06.CO_CUR == serie
                                       select new { tb06.TB129_CADTURMAS.NO_TURMA, tb06.CO_TUR });

                ddlTurma.DataTextField = "NO_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataBind();
                ddlTurma.Items.Insert(0, new ListItem("Selecione", ""));
                ddlTurma.SelectedValue = "";
                
                ddlSituacao.Items.Clear();
            }
                

            
        }

        /// <summary>
        /// Carrega o dropdown com as situações disponíveis
        /// </summary>
        private void CarregaSituacao()
        {
            ddlSituacao.Items.Clear();
            ddlSituacao.Items.Insert(0, new ListItem("Selecione", ""));
            ddlSituacao.Items.Insert(1, new ListItem("Ativo", "A"));
            ddlSituacao.Items.Insert(2, new ListItem("Cancelado", "C"));
            ddlSituacao.Items.Insert(2, new ListItem("Renovação", "R"));
            ddlSituacao.Items.Insert(3, new ListItem("Trancado", "T"));
            ddlSituacao.Items.Insert(4, new ListItem("Todos", "-1"));
            ddlSituacao.SelectedValue = "";
        }
        #endregion

        #region Eventos de componentes
        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "")
                CarregaModalidade();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "")
                CarregaTurma();
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "")
                CarregaSerieCurso();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "")
                CarregaSituacao();
        }
        #endregion

    }
}
