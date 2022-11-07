//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: CONTROLE DE SÉRIES OU CURSOS
// OBJETIVO: GERA GRADE ANUAL DE DISCIPLINA (MATÉRIA) DE SÉRIE/CURSO
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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3010_CtrlPedagogicoSeries.F3016_GeraGradeAnualDisciplina
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

        void Page_Load()
        {
            if (!IsPostBack)
            {
                CarregaAno();
                CarregaModalidades();
                CarregaSerieCurso();
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_MODU_CUR", "CO_CUR", "CO_ANO_GRADE", "CO_MAT" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_ANO_GRADE",
                HeaderText = "Ano"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_MODU_CUR",
                HeaderText = "Modalidade"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_CUR",
                HeaderText = "Curso"
            });
            
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_SIGLA_MATERIA",
                HeaderText = "Sigla"
            });
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_MATERIA",
                HeaderText = "Disciplina"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "QTDE_CH_SEM",
                HeaderText = "CH"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "QTDE_AULA_SEM",
                HeaderText = "QTA"
            });


            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_ORDEM_IMPRE",
                HeaderText = "Ord. Imp"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "AGRUP",
                HeaderText = "DISC AGRUP"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NOTA",
                HeaderText = "NOTA"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "FREQ",
                HeaderText = "FREQ"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            string ano = ddlAno.SelectedValue != "" ? ddlAno.SelectedValue : DateTime.Now.Year.ToString();
            var resultado = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                             where tb43.CO_EMP == LoginAuxili.CO_EMP
                             && (modalidade != 0 ? tb43.TB44_MODULO.CO_MODU_CUR == modalidade : modalidade == 0)
                             && (serie != 0 ? tb43.CO_CUR == serie : serie == 0)
                             && (tb43.CO_ANO_GRADE == ano)
                             join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                             join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                             join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb43.CO_CUR equals tb01.CO_CUR
                             join tb44 in TB44_MODULO.RetornaTodosRegistros() on tb43.TB44_MODULO.CO_MODU_CUR equals tb44.CO_MODU_CUR

                             join tb02ag in TB02_MATERIA.RetornaTodosRegistros() on tb43.ID_MATER_AGRUP equals tb02ag.CO_MAT into l1
                             from lr1 in l1.DefaultIfEmpty()

                             join tb107ag in TB107_CADMATERIAS.RetornaTodosRegistros() on lr1.ID_MATERIA equals tb107ag.ID_MATERIA into l2
                             from lrma in l2.DefaultIfEmpty()

                             select new
                             {
                                 tb43.CO_ANO_GRADE,
                                 tb43.CO_CUR,
                                 tb43.CO_MAT,
                                 tb107.NO_SIGLA_MATERIA,
                                 NO_MATERIA = (tb107.NO_MATERIA.Length > 60 ? tb107.NO_MATERIA.Substring(0, 60) + "..." : tb107.NO_MATERIA),
                                 tb43.TB44_MODULO.CO_MODU_CUR,
                                 DE_MODU_CUR = (tb44.DE_MODU_CUR.Length > 35 ? tb44.DE_MODU_CUR.Substring(0, 35) : tb44.DE_MODU_CUR),
                                 CO_ORDEM_IMPRE = tb43.CO_ORDEM_IMPRE,
                                 NO_CUR = (tb01.NO_CUR.Length > 35 ? tb01.NO_CUR.Substring(0, 35) : tb01.NO_CUR),
                                 tb43.QTDE_CH_SEM,
                                 tb43.QTDE_AULA_SEM,
                                 AGRUP = lrma.NO_MATERIA,
                                 NOTA = tb43.FL_LANCA_NOTA == "S" ? "SIM" : "NÃO",
                                 FREQ = tb43.FL_LANCA_FREQU == "S" ? "SIM" : "NÃO",
                             }).OrderBy(g => g.CO_ANO_GRADE).ThenBy(g => g.DE_MODU_CUR).ThenBy(g => g.NO_CUR).ThenBy(g => g.NO_MATERIA);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoCur, "CO_CUR"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Ano, "CO_ANO_GRADE"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoMat, "CO_MAT"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Carrega os anos cadastrados na grade
        /// </summary>
        private void CarregaAno()
        {
            ddlAno.Items.Clear();
            ddlAno.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                                 where tb43.CO_SITU_MATE_GRC == "A"
                                 select new { tb43.CO_ANO_GRADE }).DistinctBy(d => d.CO_ANO_GRADE).OrderByDescending(o => o.CO_ANO_GRADE);
            ddlAno.DataTextField = "CO_ANO_GRADE";
            ddlAno.DataValueField = "CO_ANO_GRADE";
            ddlAno.DataBind();

            ddlAno.SelectedValue = DateTime.Now.Year.ToString();
        }

        /// <summary>
        /// Método que carrega o DropDown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where(m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();

            ddlSerieCurso.Items.Insert(0, new ListItem("Todos", ""));
        }

        /// <summary>
        /// Método que carrega o DropDown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                        where tb01.CO_MODU_CUR == modalidade
                                        select new { tb01.CO_CUR, tb01.CO_SIGL_CUR }).OrderBy(c => c.CO_SIGL_CUR);

            ddlSerieCurso.DataTextField = "CO_SIGL_CUR";
            ddlSerieCurso.DataValueField = "CO_CUR";
            ddlSerieCurso.DataBind();

            ddlSerieCurso.Items.Insert(0, new ListItem("Todas", ""));
        }
        #endregion

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "")
                CarregaSerieCurso();
        }

        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "")
                CarregaModalidades();
        }
    }
}
